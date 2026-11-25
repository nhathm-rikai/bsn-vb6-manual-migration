using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kuno0160Service : IKuno0160Service
{
    public const int RecKbBumon = 4; // 名称マスタ KRECKB = 4 (部門)

    private readonly IKmTantRepository _tantRepo;
    private readonly IKmMeisRepository _meisRepo;

    public Kuno0160Service(
        IKmTantRepository tantRepo,
        IKmMeisRepository meisRepo)
    {
        _tantRepo = tantRepo;
        _meisRepo = meisRepo;
    }

    public void CheckDatabaseConnection()
    {
        _tantRepo.TestConnection();
    }

    public TantRecordDto FindByCode(int ktantCd)
    {
        KmTant? entity = _tantRepo.GetByCode(ktantCd);
        if (entity is null)
        {
            return new TantRecordDto
            {
                Exists = false,
                KTantCd = ktantCd
            };
        }

        string bmName = entity.BmCd != 0
            ? (_meisRepo.GetShortName(RecKbBumon, entity.BmCd) ?? string.Empty)
            : string.Empty;

        return new TantRecordDto
        {
            Exists = true,
            IsDeleted = entity.DDate.HasValue,
            KTantCd = entity.KTantCd,
            TantNameKn = LegacyTextHelper.NormalizeString(entity.TantNameKn),
            TantNameKj = LegacyTextHelper.NormalizeString(entity.TantNameKj),
            TantNameS = LegacyTextHelper.NormalizeString(entity.TantNameS),
            BmCd = entity.BmCd,
            BmName = bmName
        };
    }

    public string? GetBumonName(int bmCd)
        => _meisRepo.GetShortName(RecKbBumon, bmCd);

    public Kuno0160ActionResult Save(Kuno0160SaveInput input, DateTime now)
    {
        // 必須チェック (VB6 cmdOK_Click 準拠 — MB_ICONEXCLAMATION)
        if (string.IsNullOrEmpty(input.TaCdText.Trim()))
        {
            return Kuno0160ActionResult.Fail("担当コードを設定してください。", Kuno0160Field.TaCd, isExclamation: true);
        }

        if (string.IsNullOrEmpty(input.TaNmkjText.Trim()))
        {
            return Kuno0160ActionResult.Fail("担当名（漢字）を設定してください。", Kuno0160Field.TaNmkj, isExclamation: true);
        }

        if (string.IsNullOrEmpty(input.TaNmsText.Trim()))
        {
            return Kuno0160ActionResult.Fail("担当名（略称）を設定してください。", Kuno0160Field.TaNms, isExclamation: true);
        }

        if (!string.IsNullOrEmpty(input.BuCdText.Trim()) && string.IsNullOrEmpty(input.BuCdLabelText.Trim()))
        {
            return Kuno0160ActionResult.Fail(
                "指定された部門コードは存在しません。",
                Kuno0160Field.BuCd,
                isExclamation: true,
                clearBuCdOnError: true);
        }

        // 文字数チェック (VB6 FUNC_CHK_MOJISU 準拠)
        if (LegacyTextHelper.CheckMojisu(input.TaNmkText, 20) > 9)
        {
            return Kuno0160ActionResult.Fail("文字数オーバーです。", Kuno0160Field.TaNmk, isExclamation: true);
        }

        if (LegacyTextHelper.CheckMojisu(input.TaNmkjText, 30) > 9)
        {
            return Kuno0160ActionResult.Fail("文字数オーバーです。", Kuno0160Field.TaNmkj, isExclamation: true);
        }

        if (LegacyTextHelper.CheckMojisu(input.TaNmsText, 16) > 9)
        {
            return Kuno0160ActionResult.Fail("文字数オーバーです。", Kuno0160Field.TaNms, isExclamation: true);
        }

        int ktantCd = (int)LegacyTextHelper.VbVal(input.TaCdText);
        int bmCd = (int)LegacyTextHelper.VbVal(input.BuCdText);

        var entity = new KmTant
        {
            TDate = now.Date,
            KDate = now.Date,
            DDate = null,
            KTantCd = ktantCd,
            TantNameKn = LegacyTextHelper.NormalizeString(input.TaNmkText),
            TantNameKj = LegacyTextHelper.NormalizeString(input.TaNmkjText),
            TantNameS = LegacyTextHelper.NormalizeString(input.TaNmsText),
            BmCd = bmCd
        };

        if (!input.IsUpdate)
        {
            _tantRepo.Insert(entity);
            return Kuno0160ActionResult.Ok("登録されました。");
        }
        else
        {
            _tantRepo.Update(entity);
            return Kuno0160ActionResult.Ok("修正されました。");
        }
    }

    public Kuno0160ActionResult Delete(int ktantCd, DateTime now)
    {
        _tantRepo.SoftDelete(ktantCd, now.Date);
        return Kuno0160ActionResult.Ok("削除されました。");
    }
}

