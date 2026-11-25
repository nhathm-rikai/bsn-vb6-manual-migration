using System.Globalization;
using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kuno0010Service : IKuno0010Service
{
    private const int MakerRecordKubun = 9; // 名称マスタ KRECKB = 9 (メーカー)
    private const int UnitRecordKubun = 16; // 名称マスタ KRECKB = 16 (単位)

    private readonly IKmSyofRepository _kmSyofRepository;
    private readonly IKmBnriRepository _kmBnriRepository;
    private readonly IKmMeisRepository _kmMeisRepository;

    public Kuno0010Service(
        IKmSyofRepository kmSyofRepository,
        IKmBnriRepository kmBnriRepository,
        IKmMeisRepository kmMeisRepository)
    {
        _kmSyofRepository = kmSyofRepository;
        _kmBnriRepository = kmBnriRepository;
        _kmMeisRepository = kmMeisRepository;
    }

    public void CheckDatabaseConnection()
    {
        _kmBnriRepository.TestConnection();
    }

    /// <summary>
    /// VB6 KUNO0010.FRM kmbnri_check(wdcd, wtcd, strname) 完全互換
    /// </summary>
    public bool TryCheckCategory(int daiCd, int tyuCd, out string dtName, out int shCdr)
    {
        KmBnri? entity = _kmBnriRepository.GetByKey(daiCd, tyuCd);
        if (entity is null)
        {
            dtName = string.Empty;
            shCdr = 0;
            return false;
        }

        dtName = LegacyTextHelper.NormalizeString(entity.DtName);
        shCdr = entity.ShCdr ?? 0;

        if (entity.DDate.HasValue)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// VB6 KUNO0010.FRM kmsyof_check(dfcode, strname) 完全互換
    /// </summary>
    public bool TryCheckRepresentativeProduct(long dhShnCd, out string shortName)
    {
        if (dhShnCd <= 0)
        {
            shortName = string.Empty;
            return false;
        }

        KmSyof? entity = _kmSyofRepository.GetByCode(dhShnCd);
        if (entity is null || entity.DDate.HasValue)
        {
            shortName = string.Empty;
            return false;
        }

        shortName = LegacyTextHelper.NormalizeString(entity.ShnNameS);
        return true;
    }

    /// <summary>
    /// VB6 get_meis(9, Val(txtMCD), strbname) 完全互換
    /// </summary>
    public bool TryGetMakerName(int mekaCd, out string makerName)
    {
        string? name = _kmMeisRepository.GetShortName(MakerRecordKubun, mekaCd);
        if (name is null)
        {
            makerName = string.Empty;
            return false;
        }

        makerName = name;
        return true;
    }

    /// <summary>
    /// VB6 get_meis(16, Val(txtTANI), strbname) 完全互換
    /// </summary>
    public bool TryGetUnitName(int tniCd, out string unitName)
    {
        string? name = _kmMeisRepository.GetShortName(UnitRecordKubun, tniCd);
        if (name is null)
        {
            unitName = string.Empty;
            return false;
        }

        unitName = name;
        return true;
    }

    /// <summary>
    /// VB6 MAKKEN.FRM get_mkacd 完全互換 (KRECKB = 9, KMEISCD BETWEEN daibncd*100 AND daibncd*100+99)
    /// </summary>
    public IReadOnlyList<MakerLookupItem> GetMakerLookupList(int daiCd)
    {
        int minCode = daiCd * 100;
        int maxCode = daiCd * 100 + 99;
        IReadOnlyList<KmMeis> rows = _kmMeisRepository.GetActiveByRange(MakerRecordKubun, minCode, maxCode);
        return rows
            .Select(r => new MakerLookupItem
            {
                Code = r.KMeisCd,
                FullName = LegacyTextHelper.NormalizeString(r.MeisyoKj),
                ShortName = LegacyTextHelper.NormalizeString(r.MeisyoS)
            })
            .ToList();
    }

    /// <summary>
    /// VB6 TAIKEN.FRM get_tanicd 完全互換 (KRECKB = 16)
    /// </summary>
    public IReadOnlyList<UnitLookupItem> GetUnitLookupList()
    {
        IReadOnlyList<KmMeis> rows = _kmMeisRepository.GetActiveByRecKb(UnitRecordKubun);
        return rows
            .Select(r => new UnitLookupItem
            {
                Code = r.KMeisCd,
                FullName = LegacyTextHelper.NormalizeString(r.MeisyoKj),
                ShortName = LegacyTextHelper.NormalizeString(r.MeisyoS)
            })
            .ToList();
    }

    /// <summary>
    /// VB6 KUNO0010.FRM txtSCD_LostFocus 完全互換
    /// </summary>
    public ProductDetailResult GetProductByCode(long kshncd)
    {
        KmSyof? entity = _kmSyofRepository.GetByCode(kshncd);
        if (entity is null)
        {
            return new ProductDetailResult { Exists = false };
        }

        string dcdText = entity.DaiCd.ToString("00", CultureInfo.InvariantCulture);
        string dcdName = TryCheckCategory(entity.DaiCd, 0, out string tmpDcdName, out _)
            ? tmpDcdName
            : string.Empty;

        string tcdText = entity.TyuCd.ToString("00", CultureInfo.InvariantCulture);
        string tcdName = TryCheckCategory(entity.DaiCd, entity.TyuCd, out string tmpTcdName, out _)
            ? tmpTcdName
            : string.Empty;

        string dscdText = entity.DhShnCd == 0
            ? string.Empty
            : entity.DhShnCd.ToString("00000000", CultureInfo.InvariantCulture);
        string dscdName = (entity.DhShnCd != 0 && TryCheckRepresentativeProduct(entity.DhShnCd, out string tmpDscdName))
            ? tmpDscdName
            : string.Empty;

        string mcdText = entity.MekaCd == 0
            ? string.Empty
            : entity.MekaCd.ToString("0000", CultureInfo.InvariantCulture);
        string mcdName = (entity.MekaCd != 0 && TryGetMakerName(entity.MekaCd, out string tmpMcdName))
            ? tmpMcdName
            : string.Empty;

        string taniText = entity.TniCd == 0
            ? string.Empty
            : entity.TniCd.ToString("00", CultureInfo.InvariantCulture);
        string taniName = (entity.TniCd != 0 && TryGetUnitName(entity.TniCd, out string tmpTaniName))
            ? tmpTaniName
            : string.Empty;

        Kuno0010Field numericErr = Kuno0010Field.None;

        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.StdKur.ToString(CultureInfo.InvariantCulture), 10, 2, out string hjkkText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Hjkk;
        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.SireTk.ToString(CultureInfo.InvariantCulture), 10, 2, out string stkText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Stk;
        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.GphnbTk.ToString(CultureInfo.InvariantCulture), 10, 2, out string ghtkText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Ghtk;
        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.JgyohbTk.ToString(CultureInfo.InvariantCulture), 10, 2, out string jhtkText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Jhtk;
        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.IpkghbTk.ToString(CultureInfo.InvariantCulture), 10, 2, out string ikhtkText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Ikhtk;
        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.IpkjhbTk.ToString(CultureInfo.InvariantCulture), 10, 2, out string iohtkText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Iohtk;
        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.InSu.ToString(CultureInfo.InvariantCulture), 6, 2, out string isuText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Isu;
        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.ZkSu.ToString(CultureInfo.InvariantCulture), 10, 2, out string zsuText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Zsu;
        if (!LegacyTextHelper.TryFormatNumericHensyu(entity.Boka.ToString(CultureInfo.InvariantCulture), 10, 2, out string bkText) && numericErr == Kuno0010Field.None)
            numericErr = Kuno0010Field.Bk;

        return new ProductDetailResult
        {
            Exists = true,
            DeletedDateText = entity.DDate.HasValue
                ? entity.DDate.Value.ToString("yy/MM/dd", CultureInfo.InvariantCulture)
                : string.Empty,
            // VB6 KUNO0010.FRM 2727-2728行目:
            // txtSNMK.Text  = FUNC_CSTR(objOraDs.Fields("SHNNAMEKJ"))
            // txtSNMKJ.Text = FUNC_CSTR(objOraDs.Fields("SHNNAMEKN"))
            SnmkText = LegacyTextHelper.NormalizeString(entity.ShnNameKj),
            SnmkjText = LegacyTextHelper.NormalizeString(entity.ShnNameKn),
            SnmsText = LegacyTextHelper.NormalizeString(entity.ShnNameS),
            DcdText = dcdText,
            DcdName = dcdName,
            TcdText = tcdText,
            TcdName = tcdName,
            DscdText = dscdText,
            DscdName = dscdName,
            McdText = mcdText,
            McdName = mcdName,
            SzKb = entity.SzKb,
            HjkkText = hjkkText,
            StkText = stkText,
            GhtkText = ghtkText,
            JhtkText = jhtkText,
            IkhtkText = ikhtkText,
            IohtkText = iohtkText,
            TaniText = taniText,
            TaniName = taniName,
            IsuText = isuText,
            ZkKb = entity.ZkKb,
            ZsuText = zsuText,
            BkText = bkText,
            NumericErrorField = numericErr
        };
    }

    /// <summary>
    /// VB6 KUNO0010.FRM cmdOK_Click (登録モード) 完全互換
    /// </summary>
    public Kuno0010ActionResult RegisterProduct(ProductSaveRequest request)
    {
        Kuno0010ActionResult? validationError = ValidateRequest(request);
        if (validationError is not null)
        {
            return validationError;
        }

        int daiCd = (int)LegacyTextHelper.VbVal(request.DcdText);

        // VB6 1425行目: If kmbnri_check(Val(txtDCD.Text), 0, strname) = False Then
        if (!TryCheckCategory(daiCd, 0, out _, out int sRenban))
        {
            return Kuno0010ActionResult.Fail(
                "指定された中分類コードは存在しません。",
                Kuno0010Field.Tcd,
                clearTcdOnError: true);
        }

        // VB6 1432行目: If s_renban = 999999 Then
        if (sRenban == 999999)
        {
            return Kuno0010ActionResult.Fail(
                "商品コードの採番限界(999999)をこえました。 中止します!!",
                isStopFatal: true);
        }

        int dfRenban = sRenban + 1;
        DateTime today = DateTime.Today;

        // 1. 商品分類マスタ(KMBNRI) 連番(SHCDR)更新
        _kmBnriRepository.UpdateProductSeq(daiCd, dfRenban, today);

        // 2. 商品マスタ(KMSYOF) 登録
        string strCode = $"{daiCd:00}{dfRenban:000000}";
        long kshncd = long.Parse(strCode, CultureInfo.InvariantCulture);

        KmSyof entity = BuildEntity(request, kshncd, today);
        _kmSyofRepository.Insert(entity);

        return Kuno0010ActionResult.Ok($"商品コード = {strCode}を登録しました。");
    }

    /// <summary>
    /// VB6 KUNO0010.FRM cmdOK_Click (修正モード) 完全互換
    /// </summary>
    public Kuno0010ActionResult UpdateProduct(ProductSaveRequest request)
    {
        Kuno0010ActionResult? validationError = ValidateRequest(request);
        if (validationError is not null)
        {
            return validationError;
        }

        long kshncd = (long)LegacyTextHelper.VbVal(request.ScdText);
        DateTime today = DateTime.Today;

        KmSyof entity = BuildEntity(request, kshncd, today);
        _kmSyofRepository.Update(entity);

        return Kuno0010ActionResult.Ok($"商品コード = {request.ScdText}を更新しました。");
    }

    /// <summary>
    /// VB6 KUNO0010.FRM cmdOK_Click (削除モード) 完全互換
    /// </summary>
    public Kuno0010ActionResult DeleteProduct(long kshncd, string formattedScd)
    {
        _kmSyofRepository.SoftDelete(kshncd, DateTime.Today);
        return Kuno0010ActionResult.Ok($"商品コード = {formattedScd}を削除しました。");
    }

    private static Kuno0010ActionResult? ValidateRequest(ProductSaveRequest request)
    {
        // 省略不可項目入力チェック (VB6 1306-1333行目)
        if (string.IsNullOrWhiteSpace(request.SnmkjText))
        {
            return Kuno0010ActionResult.Fail("商品名を設定してください。", Kuno0010Field.Snmkj);
        }

        if (string.IsNullOrWhiteSpace(request.SnmsText))
        {
            return Kuno0010ActionResult.Fail("商品名略称を設定してください。", Kuno0010Field.Snms);
        }

        if (string.IsNullOrWhiteSpace(request.DcdText))
        {
            return Kuno0010ActionResult.Fail("大分類ｺｰﾄﾞを設定してください。", Kuno0010Field.Dcd);
        }

        if (string.IsNullOrWhiteSpace(request.TcdText))
        {
            return Kuno0010ActionResult.Fail("中分類ｺｰﾄﾞを設定してください。", Kuno0010Field.Tcd);
        }

        if (!request.HasTaxSelection)
        {
            return Kuno0010ActionResult.Fail("消費税区分を選択してください。");
        }

        if (!request.HasZaikoSelection)
        {
            return Kuno0010ActionResult.Fail("在庫区分を選択してください。");
        }

        // 文字数(バイト数)チェック
        if (LegacyTextHelper.CheckMojisu(request.SnmkText, 25) > 9)
        {
            return Kuno0010ActionResult.Fail("文字数オーバーです。", Kuno0010Field.Snmk, isExclamation: true);
        }

        if (LegacyTextHelper.CheckMojisu(request.SnmkjText, 40) > 9)
        {
            return Kuno0010ActionResult.Fail("文字数オーバーです。", Kuno0010Field.Snmkj, isExclamation: true);
        }

        if (LegacyTextHelper.CheckMojisu(request.SnmsText, 16) > 9)
        {
            return Kuno0010ActionResult.Fail("文字数オーバーです。", Kuno0010Field.Snms, isExclamation: true);
        }

        // 入力存在チェック (VB6 1335-1359行目)
        if (string.IsNullOrWhiteSpace(request.DcdLabelText) && !string.IsNullOrWhiteSpace(request.DcdText))
        {
            return Kuno0010ActionResult.Fail("指定された大分類コードは存在しません。", Kuno0010Field.Dcd);
        }

        if (string.IsNullOrWhiteSpace(request.TcdLabelText) && !string.IsNullOrWhiteSpace(request.TcdText))
        {
            return Kuno0010ActionResult.Fail("指定された中分類コードは存在しません。", Kuno0010Field.Tcd);
        }

        if (string.IsNullOrWhiteSpace(request.DscdLabelText) && !string.IsNullOrWhiteSpace(request.DscdText))
        {
            return Kuno0010ActionResult.Fail("指定された代表商品コードは存在しません。", Kuno0010Field.Dscd);
        }

        if (string.IsNullOrWhiteSpace(request.McdLabelText) && !string.IsNullOrWhiteSpace(request.McdText))
        {
            return Kuno0010ActionResult.Fail("指定されたメーカーコードは存在しません。", Kuno0010Field.Mcd);
        }

        if (string.IsNullOrWhiteSpace(request.TaniLabelText) && !string.IsNullOrWhiteSpace(request.TaniText))
        {
            return Kuno0010ActionResult.Fail("指定された単位コードは存在しません。", Kuno0010Field.Tani);
        }

        return null;
    }

    private static KmSyof BuildEntity(ProductSaveRequest request, long kshncd, DateTime today)
    {
        return new KmSyof
        {
            TDate = today,
            KDate = today,
            DDate = null,
            KShnCd = kshncd,
            // VB6 KUNO0010.FRM 1518-1519 / 1572-1573行目:
            // DBカラム SHNNAMEKJ に strSHNNAMEKN(txtSNMK.Text = カナ) を格納し、
            // DBカラム SHNNAMEKN に strSHNNAMEKJ(txtSNMKJ.Text = 漢字) を格納する。
            ShnNameKj = LegacyTextHelper.NormalizeString(request.SnmkText),
            ShnNameKn = LegacyTextHelper.NormalizeString(request.SnmkjText),
            ShnNameS = LegacyTextHelper.NormalizeString(request.SnmsText),
            DaiCd = (int)LegacyTextHelper.VbVal(request.DcdText),
            TyuCd = (int)LegacyTextHelper.VbVal(request.TcdText),
            DhShnCd = (long)LegacyTextHelper.VbVal(request.DscdText),
            MekaCd = (int)LegacyTextHelper.VbVal(request.McdText),
            SzKb = request.SzKb,
            StdKur = string.IsNullOrEmpty(request.HjkkText) ? 0m : LegacyTextHelper.FuncCdbl(request.HjkkText),
            SireTk = string.IsNullOrEmpty(request.StkText) ? 0m : LegacyTextHelper.FuncCdbl(request.StkText),
            GphnbTk = string.IsNullOrEmpty(request.GhtkText) ? 0m : LegacyTextHelper.FuncCdbl(request.GhtkText),
            JgyohbTk = string.IsNullOrEmpty(request.JhtkText) ? 0m : LegacyTextHelper.FuncCdbl(request.JhtkText),
            IpkghbTk = string.IsNullOrEmpty(request.IkhtkText) ? 0m : LegacyTextHelper.FuncCdbl(request.IkhtkText),
            IpkjhbTk = string.IsNullOrEmpty(request.IohtkText) ? 0m : LegacyTextHelper.FuncCdbl(request.IohtkText),
            TniCd = LegacyTextHelper.FuncCint(request.TaniText),
            InSu = string.IsNullOrEmpty(request.IsuText) ? 0m : LegacyTextHelper.FuncCdbl(request.IsuText),
            ZkKb = request.ZkKb,
            ZkSu = string.IsNullOrEmpty(request.ZsuText) ? 0m : LegacyTextHelper.FuncCdbl(request.ZsuText),
            Boka = string.IsNullOrEmpty(request.BkText) ? 0m : LegacyTextHelper.FuncCdbl(request.BkText)
        };
    }
}

