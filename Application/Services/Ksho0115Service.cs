using System.Globalization;
using System.Text;
using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Ksho0115Service : IKsho0115Service
{
    private static readonly Encoding ShiftJis;

    static Ksho0115Service()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        ShiftJis = Encoding.GetEncoding(932);
    }

    private readonly IVtUjnlRepository _ujnlRepo;
    private readonly IKmBnriRepository _bnriRepo;
    private readonly IKmKyakRepository _kyakRepo;

    public Ksho0115Service(
        IVtUjnlRepository ujnlRepo,
        IKmBnriRepository bnriRepo,
        IKmKyakRepository kyakRepo)
    {
        _ujnlRepo = ujnlRepo;
        _bnriRepo = bnriRepo;
        _kyakRepo = kyakRepo;
    }

    public void CheckDatabaseConnection()
    {
        _ujnlRepo.TestConnection();
    }

    public Ksho0115ExportResult ExecuteExport(Ksho0115ExportRequest request)
    {
        // 入力チェック (VB6 cmdSTR_Click 1:1 準拠)
        if (string.IsNullOrEmpty(request.KNenText))
        {
            return Ksho0115ExportResult.Fail("販売日の指定に誤りがあります。", Ksho0115Field.KNen);
        }

        if (string.IsNullOrEmpty(request.KTukiText))
        {
            return Ksho0115ExportResult.Fail("販売日の指定に誤りがあります。", Ksho0115Field.KTuki);
        }

        if (string.IsNullOrEmpty(request.KHiText))
        {
            return Ksho0115ExportResult.Fail("販売日の指定に誤りがあります。", Ksho0115Field.KHi);
        }

        if (string.IsNullOrEmpty(request.MNenText))
        {
            return Ksho0115ExportResult.Fail("販売日の指定に誤りがあります。", Ksho0115Field.MNen);
        }

        if (string.IsNullOrEmpty(request.MTukiText))
        {
            return Ksho0115ExportResult.Fail("販売日の指定に誤りがあります。", Ksho0115Field.MTuki);
        }

        if (string.IsNullOrEmpty(request.MHiText))
        {
            return Ksho0115ExportResult.Fail("販売日の指定に誤りがあります。", Ksho0115Field.MHi);
        }

        if (request.HaniMode == 2) // 範囲指定 (optHANI)
        {
            if (string.IsNullOrEmpty(request.FromDaiCdText))
            {
                return Ksho0115ExportResult.Fail("大分類ｺｰﾄﾞの指定に誤りがあります。", Ksho0115Field.FromDaiCd);
            }

            if (string.IsNullOrEmpty(request.ToDaiCdText))
            {
                return Ksho0115ExportResult.Fail("大分類ｺｰﾄﾞの指定に誤りがあります。", Ksho0115Field.ToDaiCd);
            }

            if (string.CompareOrdinal(request.FromDaiCdText, request.ToDaiCdText) > 0)
            {
                return Ksho0115ExportResult.Fail("大分類ｺｰﾄﾞの指定に誤りがあります。", Ksho0115Field.FromDaiCd);
            }
        }
        else if (request.HaniMode == 3) // 営業担当者コード指定 (OptTANT)
        {
            if (string.IsNullOrEmpty(request.TantCdText))
            {
                return Ksho0115ExportResult.Fail("営業担当者ｺｰﾄﾞの指定に誤りがあります。", Ksho0115Field.TantCd);
            }
        }

        // 入力値比較大小チェック
        string strDate1 = request.KNenText + request.KTukiText + request.KHiText;
        string strDate2 = request.MNenText + request.MTukiText + request.MHiText;
        long fromYmd = (long)LegacyTextHelper.VbVal(strDate1);
        long toYmd = (long)LegacyTextHelper.VbVal(strDate2);

        if (fromYmd > toYmd)
        {
            return Ksho0115ExportResult.Fail("販売日の範囲指定に誤りがあります。", Ksho0115Field.KNen, isExclamation: true);
        }

        // 販売日チェック (2ヶ月超判定)
        int kNen = (int)LegacyTextHelper.VbVal(request.KNenText);
        int kTuki = (int)LegacyTextHelper.VbVal(request.KTukiText);
        int kHi = (int)LegacyTextHelper.VbVal(request.KHiText);
        long wKeiYmd;
        if (kTuki < 11)
        {
            wKeiYmd = fromYmd + 200L;
        }
        else
        {
            string keiStr =
                (kNen + 1).ToString("0000", CultureInfo.InvariantCulture) +
                (kTuki - 10).ToString("00", CultureInfo.InvariantCulture) +
                kHi.ToString("00", CultureInfo.InvariantCulture);
            wKeiYmd = (long)LegacyTextHelper.VbVal(keiStr);
        }

        if (toYmd > wKeiYmd)
        {
            return Ksho0115ExportResult.Fail("２ヶ月以上の期間は指定出来ません。", Ksho0115Field.KNen, isExclamation: true);
        }

        int wHani = request.HaniMode;
        int wFrom = wHani == 2 ? (int)LegacyTextHelper.VbVal(request.FromDaiCdText) : 0;
        int wTo = wHani == 2 ? (int)LegacyTextHelper.VbVal(request.ToDaiCdText) : 0;
        int wTantCd = wHani == 3 ? (int)LegacyTextHelper.VbVal(request.TantCdText) : 0;

        if (request.OutputKubun == 1)
        {
            IReadOnlyList<VtUjnl> rows = _ujnlRepo.GetDetails(fromYmd, toYmd, wHani, wFrom, wTo, wTantCd);
            if (rows.Count == 0)
            {
                return Ksho0115ExportResult.Fail("該当レコードがありません。", Ksho0115Field.KNen, isExclamation: true);
            }

            string filePath = LegacyTextHelper.GetInitialPath("uriage", "売上ﾃﾞｰﾀ.CSV");
            WriteDetailsCsv(filePath, rows);
            return Ksho0115ExportResult.Ok(
                $"売上情報（売上明細） {rows.Count}件 出力しました。",
                rows.Count,
                filePath);
        }
        else
        {
            IReadOnlyList<VtUjnlSummary> rows = _ujnlRepo.GetSummaries(fromYmd, toYmd, wHani, wFrom, wTo, wTantCd);
            if (rows.Count == 0)
            {
                return Ksho0115ExportResult.Fail("該当レコードがありません。", Ksho0115Field.KNen, isExclamation: true);
            }

            string filePath = LegacyTextHelper.GetInitialPath("uriage2", "売上ﾃﾞｰﾀ(大分類販売先別).CSV");
            WriteSummariesCsv(filePath, rows);
            return Ksho0115ExportResult.Ok(
                $"売上情報（大分類販売先別明細） {rows.Count}件 出力しました。",
                rows.Count,
                filePath);
        }
    }

    /// <summary>
    /// VB6 get_kmbnri(wCODE, wFLG, strNAME) + FUNC_SPACE_ADD(strP_DAINM, 20) 完全互換
    /// </summary>
    private string FormatDaiBunruiName(int daiCd, int bkFlg, Dictionary<(int, int), string> cache)
    {
        if (cache.TryGetValue((daiCd, bkFlg), out string? cached))
        {
            return cached;
        }

        KmBnri? bnri = _bnriRepo.GetByKey(daiCd, 0);
        string result;
        if (bnri is null)
        {
            result = LegacyTextHelper.FuncSpaceAdd(string.Empty, 20);
        }
        else
        {
            string strName;
            if (bnri.DDate.HasValue)
            {
                strName = daiCd.ToString("00", CultureInfo.InvariantCulture);
            }
            else
            {
                strName = LegacyTextHelper.SubStringByByte(bnri.DtName, 8);
                if (bkFlg == 1)
                {
                    strName += "割賦";
                }
            }

            result = LegacyTextHelper.FuncSpaceAdd(strName, 20);
        }

        cache[(daiCd, bkFlg)] = result;
        return result;
    }

    /// <summary>
    /// VB6 get_kmkyak(strCODE, strNAME) + FUNC_SPACE_ADD(strName, 30) 完全互換
    /// </summary>
    private string FormatCustomerName(string? code, Dictionary<string, string> cache)
    {
        string normalizedCode = LegacyTextHelper.NormalizeString(code);
        if (cache.TryGetValue(normalizedCode, out string? cached))
        {
            return cached;
        }

        string name = string.IsNullOrEmpty(normalizedCode)
            ? string.Empty
            : _kyakRepo.GetActiveCustomerName(normalizedCode);

        string padded = LegacyTextHelper.FuncSpaceAdd(name, 30);
        cache[normalizedCode] = padded;
        return padded;
    }

    private static string FormatNum(decimal value)
        => value.ToString("G29", CultureInfo.InvariantCulture);

    private void WriteDetailsCsv(string filePath, IReadOnlyList<VtUjnl> rows)
    {
        var daiCache = new Dictionary<(int, int), string>();
        var kyakCache = new Dictionary<string, string>(StringComparer.Ordinal);

        using var writer = new StreamWriter(filePath, append: false, ShiftJis);

        // ヘッダー行 (prt_all1 1:1)
        writer.WriteLine(
            "伝票番号,行NO,販売先コード,販売先名,グループ会社コード,所属コード,社員番号,販売日,取扱部門コード,営業担当コード," +
            "入力担当コード,取引区分,計上区分,計上月,社番,大分類コード,大分類名,中分類コード,商品コード,商品名1," +
            "商品名2,売上数量,単位,売上単価,販売金額,消費税,税込金額,税区分,摘要,入金条件フラグ," +
            "請求先コード,請求先名,入金方法,伝票発行区分,伝票発行フラグ,月次更新フラグ,仕訳完了フラグ,直送フラグ,削除フラグ,分割区分");

        foreach (VtUjnl r in rows)
        {
            string hbsCd = LegacyTextHelper.NormalizeString(r.HbsCd);
            string sksCd = LegacyTextHelper.NormalizeString(r.SksCd);
            string strPDaiNm = FormatDaiBunruiName(r.DaiCd, r.BkFlg, daiCache);
            string strPHbsNm = FormatCustomerName(hbsCd, kyakCache);
            string strPSksNm = FormatCustomerName(sksCd, kyakCache);
            string shnName1 = LegacyTextHelper.NormalizeString(r.ShnNamen1);
            string shnName2 = LegacyTextHelper.NormalizeString(r.ShnNamen2);
            string tekiyo = LegacyTextHelper.NormalizeString(r.Tekiyo);

            writer.WriteLine(string.Join(",",
                r.KDenNo.ToString(CultureInfo.InvariantCulture),
                r.KGyoNo.ToString(CultureInfo.InvariantCulture),
                $"\"{hbsCd}\"",
                $"\"{strPHbsNm}\"",
                r.GpkCd.ToString(CultureInfo.InvariantCulture),
                r.SzCd.ToString(CultureInfo.InvariantCulture),
                r.SinNo.ToString(CultureInfo.InvariantCulture),
                r.HbiDay.ToString(CultureInfo.InvariantCulture),
                r.TBmCd.ToString(CultureInfo.InvariantCulture),
                r.STantCd.ToString(CultureInfo.InvariantCulture),
                r.NTantCd.ToString(CultureInfo.InvariantCulture),
                r.ToriCd.ToString(CultureInfo.InvariantCulture),
                r.KeiKu.ToString(CultureInfo.InvariantCulture),
                r.KeiDd.ToString(CultureInfo.InvariantCulture),
                r.SybnCd.ToString(CultureInfo.InvariantCulture),
                r.DaiCd.ToString(CultureInfo.InvariantCulture),
                $"\"{strPDaiNm}\"",
                r.TyuCd.ToString(CultureInfo.InvariantCulture),
                r.ShnCd.ToString(CultureInfo.InvariantCulture),
                $"\"{shnName1}\"",
                $"\"{shnName2}\"",
                FormatNum(r.UriSu),
                r.TniCd.ToString(CultureInfo.InvariantCulture),
                FormatNum(r.UriTan),
                FormatNum(r.HanKin),
                FormatNum(r.ZeiKin),
                FormatNum(r.GokKin),
                r.ZeiKu.ToString(CultureInfo.InvariantCulture),
                $"\"{tekiyo}\"",
                r.NkzFlg.ToString(CultureInfo.InvariantCulture),
                $"\"{sksCd}\"",
                $"\"{strPSksNm}\"",
                r.NkHoho.ToString(CultureInfo.InvariantCulture),
                r.DnpKu.ToString(CultureInfo.InvariantCulture),
                r.DnpFlg.ToString(CultureInfo.InvariantCulture),
                r.GtjFlg.ToString(CultureInfo.InvariantCulture),
                r.SwkFlg.ToString(CultureInfo.InvariantCulture),
                r.CksFlg.ToString(CultureInfo.InvariantCulture),
                r.DelFlg.ToString(CultureInfo.InvariantCulture),
                r.BkFlg.ToString(CultureInfo.InvariantCulture)));
        }
    }

    private void WriteSummariesCsv(string filePath, IReadOnlyList<VtUjnlSummary> rows)
    {
        var daiCache = new Dictionary<(int, int), string>();
        var kyakCache = new Dictionary<string, string>(StringComparer.Ordinal);

        using var writer = new StreamWriter(filePath, append: false, ShiftJis);

        // ヘッダー行 (prt_all2 1:1)
        writer.WriteLine("大分類コード,大分類名,分割区分,計上区分,計上月,請求先コード,請求先名,販売先コード,販売先名,販売金額,消費税,税込金額");

        foreach (VtUjnlSummary r in rows)
        {
            string hbsCd = LegacyTextHelper.NormalizeString(r.HbsCd);
            string sksCd = LegacyTextHelper.NormalizeString(r.SksCd);
            string strPDaiNm = FormatDaiBunruiName(r.DaiCd, r.BkFlg, daiCache);
            string strPHbsNm = FormatCustomerName(hbsCd, kyakCache);
            string strPSksNm = FormatCustomerName(sksCd, kyakCache);

            writer.WriteLine(string.Join(",",
                r.DaiCd.ToString(CultureInfo.InvariantCulture),
                $"\"{strPDaiNm}\"",
                r.BkFlg.ToString(CultureInfo.InvariantCulture),
                r.KeiKu.ToString(CultureInfo.InvariantCulture),
                r.KeiDd.ToString(CultureInfo.InvariantCulture),
                $"\"{sksCd}\"",
                $"\"{strPSksNm}\"",
                $"\"{hbsCd}\"",
                $"\"{strPHbsNm}\"",
                FormatNum(r.SumHanKin),
                FormatNum(r.SumZeiKin),
                FormatNum(r.SumGokKin)));
        }
    }
}

