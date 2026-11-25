using System.Globalization;
using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kuno0020Service : IKuno0020Service
{
    private const int BumonRecordKubun = 4; // 名称マスタ KRECKB = 4 (部門)

    private readonly IKmBnriRepository _kmBnriRepository;
    private readonly IKmMeisRepository _kmMeisRepository;

    public Kuno0020Service(IKmBnriRepository kmBnriRepository, IKmMeisRepository kmMeisRepository)
    {
        _kmBnriRepository = kmBnriRepository;
        _kmMeisRepository = kmMeisRepository;
    }

    public void CheckDatabaseConnection()
    {
        _kmBnriRepository.TestConnection();
    }

    /// <summary>
    /// VB6 kmbnri_check(pintDaiCD, pintTyuCD) 完全互換
    /// </summary>
    public CategoryLookupResult CheckCategory(int daiCd, int tyuCd)
    {
        KmBnri? entity = _kmBnriRepository.GetByKey(daiCd, tyuCd);
        if (entity is null)
        {
            return new CategoryLookupResult
            {
                Exists = false,
                DeletedDateText = string.Empty,
                DtName = string.Empty,
                BmCd = 0,
                SrtText = "0.00",
                TekyoText = string.Empty,
                Srt2Text = "0.00",
                KeiriDds = 0
            };
        }

        return new CategoryLookupResult
        {
            Exists = true,
            DeletedDateText = entity.DDate.HasValue
                ? entity.DDate.Value.ToString("yy/MM/dd", CultureInfo.InvariantCulture)
                : string.Empty,
            DtName = LegacyTextHelper.NormalizeString(entity.DtName),
            BmCd = entity.BmCd,
            SrtText = LegacyTextHelper.FormatRate(entity.Sritu),
            TekyoText = LegacyTextHelper.FormatTekiyo(entity.Stekiyo),
            Srt2Text = LegacyTextHelper.FormatRate(entity.Sritu2),
            KeiriDds = entity.Keiridds ?? 0
        };
    }

    /// <summary>
    /// VB6 get_meis(4, Val(txtBCD), strbname) 完全互換
    /// </summary>
    public bool TryGetBumonName(int bmCd, out string bumonName)
    {
        string? name = _kmMeisRepository.GetShortName(BumonRecordKubun, bmCd);
        if (name is null)
        {
            bumonName = string.Empty;
            return false;
        }

        bumonName = name;
        return true;
    }

    /// <summary>
    /// VB6 cmdSET_Click 完全互換（入力検証 + INSERT / UPDATE）
    /// </summary>
    public Kuno0020ActionResult SaveCategory(CategorySaveRequest request, bool isUpdate)
    {
        // 省略不可項目入力チェック(大分類コード)
        if (string.IsNullOrWhiteSpace(request.DaiCdText))
        {
            return Kuno0020ActionResult.Fail("大分類コードを設定してください。", Kuno0020Field.DaiCd);
        }

        // 省略不可項目入力チェック(中分類コード)
        if (string.IsNullOrWhiteSpace(request.TyuCdText))
        {
            return Kuno0020ActionResult.Fail("中分類コードを設定してください。", Kuno0020Field.TyuCd);
        }

        // 名称バイト数チェック（20バイト上限）
        if (LegacyTextHelper.CheckMojisu(request.DtNameText, 20) > 9)
        {
            return Kuno0020ActionResult.Fail("文字数オーバーです。", Kuno0020Field.DtName, isExclamation: true);
        }

        // 入力チェック（旧消費税）
        if (!LegacyTextHelper.IsTaxRateValid(request.SrtText))
        {
            return Kuno0020ActionResult.Fail("入力エラーです。", Kuno0020Field.Srt);
        }

        // 入力存在チェック（部門コード）
        if (string.IsNullOrWhiteSpace(request.BmNameLabelText) && !string.IsNullOrWhiteSpace(request.BmCdText))
        {
            return Kuno0020ActionResult.Fail("指定された部門コードは存在しません。", Kuno0020Field.BmCd);
        }

        // 入力チェック（新消費税）
        if (!LegacyTextHelper.IsTaxRateValid(request.Srt2Text))
        {
            return Kuno0020ActionResult.Fail("入力エラーです。", Kuno0020Field.Srt2);
        }

        DateTime today = DateTime.Today;
        var entity = new KmBnri
        {
            TDate = today,
            KDate = today,
            DDate = null,
            KDaiCd = (int)LegacyTextHelper.VbVal(request.DaiCdText),
            KTyuCd = (int)LegacyTextHelper.VbVal(request.TyuCdText),
            DtName = LegacyTextHelper.NormalizeString(request.DtNameText),
            BmCd = (int)LegacyTextHelper.VbVal(request.BmCdText),
            Sritu = string.IsNullOrEmpty(request.SrtText) ? 0m : LegacyTextHelper.VbVal(request.SrtText),
            Stekiyo = string.IsNullOrEmpty(request.TekyoText)
                ? 0L
                : (long)Math.Round(LegacyTextHelper.VbVal(request.TekyoText), MidpointRounding.AwayFromZero),
            Sritu2 = string.IsNullOrEmpty(request.Srt2Text) ? 0m : LegacyTextHelper.VbVal(request.Srt2Text),
            Keiridds = request.KeiriDds
        };

        if (!isUpdate)
        {
            _kmBnriRepository.Insert(entity);
            return Kuno0020ActionResult.Ok("登録されました。");
        }

        _kmBnriRepository.Update(entity);
        return Kuno0020ActionResult.Ok("更新されました。");
    }

    /// <summary>
    /// VB6 cmdDEL_Click 完全互換（商品マスタ使用チェック + DDATE更新による論理削除）
    /// </summary>
    public Kuno0020ActionResult DeleteCategory(int daiCd, int tyuCd)
    {
        if (_kmBnriRepository.ExistsInProductMaster(daiCd, tyuCd))
        {
            return Kuno0020ActionResult.Fail("商品マスタで使用されているため削除不可");
        }

        _kmBnriRepository.SoftDelete(daiCd, tyuCd, DateTime.Today);
        return Kuno0020ActionResult.Ok("削除されました。");
    }
}

