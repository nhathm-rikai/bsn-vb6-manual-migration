using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kuno0060Service : IKuno0060Service
{
    public const int RecKbBlock = 1;
    public const int RecKbSyukan = 2;
    public const int RecKbSyozokuKubun = 8;

    private readonly IKmSyzkRepository _syzkRepo;
    private readonly IKmMeisRepository _meisRepo;
    private readonly IKmGrpfRepository _grpfRepo;

    public Kuno0060Service(
        IKmSyzkRepository syzkRepo,
        IKmMeisRepository meisRepo,
        IKmGrpfRepository grpfRepo)
    {
        _syzkRepo = syzkRepo;
        _meisRepo = meisRepo;
        _grpfRepo = grpfRepo;
    }

    public void CheckDatabaseConnection()
    {
        _syzkRepo.TestConnection();
    }

    public SyzkRecordDto FindByCode(int kszCd)
    {
        KmSyzk? entity = _syzkRepo.GetByCode(kszCd);
        if (entity is null)
        {
            return new SyzkRecordDto
            {
                Exists = false,
                KSzCd = kszCd
            };
        }

        string szKbName = _meisRepo.GetShortName(RecKbSyozokuKubun, entity.SzKb) ?? string.Empty;
        string brokName = _meisRepo.GetShortName(RecKbBlock, entity.BrokCd) ?? string.Empty;
        string syuknName = _meisRepo.GetShortName(RecKbSyukan, entity.SyuknCd) ?? string.Empty;
        string gpkName = entity.GpkCd != 0
            ? (_grpfRepo.GetActiveShortName(entity.GpkCd) ?? string.Empty)
            : string.Empty;

        return new SyzkRecordDto
        {
            Exists = true,
            IsDeleted = entity.DDate.HasValue,
            KSzCd = entity.KSzCd,
            SzKb = entity.SzKb,
            SzKbName = szKbName,
            SzNameKn = LegacyTextHelper.NormalizeString(entity.SzNameKn),
            SzNameKj = LegacyTextHelper.NormalizeString(entity.SzNameKj),
            SzNameS = LegacyTextHelper.NormalizeString(entity.SzNameS),
            BrokCd = entity.BrokCd,
            BrokName = brokName,
            SyuknCd = entity.SyuknCd,
            SyuknName = syuknName,
            GpkCd = entity.GpkCd,
            GpkName = gpkName
        };
    }

    public string? GetKubunName(int kubunCd)
        => _meisRepo.GetShortName(RecKbSyozokuKubun, kubunCd);

    public string? GetBlockName(int blkCd)
        => _meisRepo.GetShortName(RecKbBlock, blkCd);

    public string? GetSyukanName(int skanCd)
        => _meisRepo.GetShortName(RecKbSyukan, skanCd);

    public string? GetGroupCompanyName(int gpkCd)
        => _grpfRepo.GetActiveShortName(gpkCd);

    public Kuno0060ActionResult Save(Kuno0060SaveInput input, DateTime now)
    {
        // 必須チェック (VB6 cmdOK_Click 準拠)
        if (string.IsNullOrEmpty(input.SyCdText.Trim()))
        {
            return Kuno0060ActionResult.Fail("所属コードを設定してください。", Kuno0060Field.SyCd);
        }

        if (string.IsNullOrEmpty(input.KubunText.Trim()))
        {
            return Kuno0060ActionResult.Fail("所属区分を設定してください。", Kuno0060Field.Kubun);
        }

        if (string.IsNullOrEmpty(input.SyNmkjText.Trim()))
        {
            return Kuno0060ActionResult.Fail("所属名称を設定してください。", Kuno0060Field.SyNmkj);
        }

        if (string.IsNullOrEmpty(input.SyNmsText.Trim()))
        {
            return Kuno0060ActionResult.Fail("所属略称を設定してください。", Kuno0060Field.SyNms);
        }

        // マスタ存在チェック (VB6 cmdOK_Click 準拠)
        if (string.IsNullOrEmpty(input.KubunLabelText.Trim()) && !string.IsNullOrEmpty(input.KubunText.Trim()))
        {
            return Kuno0060ActionResult.Fail("指定された所属区分は存在しません。", Kuno0060Field.Kubun);
        }

        if (string.IsNullOrEmpty(input.BlkLabelText.Trim()) && !string.IsNullOrEmpty(input.BlkText.Trim()))
        {
            return Kuno0060ActionResult.Fail("指定されたブロックコードは存在しません。", Kuno0060Field.Blk);
        }

        if (string.IsNullOrEmpty(input.SkanLabelText.Trim()) && !string.IsNullOrEmpty(input.SkanText.Trim()))
        {
            return Kuno0060ActionResult.Fail("指定された主管コードは存在しません。", Kuno0060Field.Skan);
        }

        if (string.IsNullOrEmpty(input.GcdLabelText.Trim()) && !string.IsNullOrEmpty(input.GcdText.Trim()))
        {
            return Kuno0060ActionResult.Fail("指定されたグループ会社コードは存在しません。", Kuno0060Field.Gcd);
        }

        // 文字数チェック
        if (LegacyTextHelper.CheckMojisu(input.SyNmkText, 20) > 9)
        {
            return Kuno0060ActionResult.Fail("文字数オーバーです。", Kuno0060Field.SyNmk, isExclamation: true);
        }

        if (LegacyTextHelper.CheckMojisu(input.SyNmkjText, 30) > 9)
        {
            return Kuno0060ActionResult.Fail("文字数オーバーです。", Kuno0060Field.SyNmkj, isExclamation: true);
        }

        if (LegacyTextHelper.CheckMojisu(input.SyNmsText, 16) > 9)
        {
            return Kuno0060ActionResult.Fail("文字数オーバーです。", Kuno0060Field.SyNms, isExclamation: true);
        }

        int kszCd = (int)LegacyTextHelper.VbVal(input.SyCdText);
        int szKb = (int)LegacyTextHelper.VbVal(input.KubunText);
        int brokCd = (int)LegacyTextHelper.VbVal(input.BlkText);
        int syuknCd = (int)LegacyTextHelper.VbVal(input.SkanText);
        int gpkCd = (int)LegacyTextHelper.VbVal(input.GcdText);

        var entity = new KmSyzk
        {
            TDate = now.Date,
            KDate = now.Date,
            DDate = null,
            KSzCd = kszCd,
            SzKb = szKb,
            SzNameKn = LegacyTextHelper.NormalizeString(input.SyNmkText).Trim(),
            SzNameKj = LegacyTextHelper.NormalizeString(input.SyNmkjText).Trim(),
            SzNameS = LegacyTextHelper.NormalizeString(input.SyNmsText).Trim(),
            BrokCd = brokCd,
            SyuknCd = syuknCd,
            GpkCd = gpkCd
        };

        if (!input.IsUpdate)
        {
            _syzkRepo.Insert(entity);
            return Kuno0060ActionResult.Ok("登録されました。");
        }
        else
        {
            _syzkRepo.Update(entity);
            return Kuno0060ActionResult.Ok("更新されました。");
        }
    }

    public Kuno0060ActionResult Delete(int kszCd, DateTime now)
    {
        _syzkRepo.SoftDelete(kszCd, now.Date);
        return Kuno0060ActionResult.Ok("削除されました。");
    }
}
