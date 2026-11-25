namespace Application.DTOs;

/// <summary>
/// VB6 KUNO0040.BAS の画面間共有状態（frmKUNO0040 / frmKUNO0041 / frmKUNO0042 で共有）
/// </summary>
public sealed class Kuno0040State
{
    // 更新フラグ (0 = 登録, 1 = 更新)
    public int UpFlg40 { get; set; }

    // 他画面セット/キャンセルフラグ
    public int SetFlg2 { get; set; }
    public int SetFlg3 { get; set; }
    public int CanFlg2 { get; set; }
    public int CanFlg3 { get; set; }
    public int Siha { get; set; }
    public int SihFlg { get; set; }

    // 画面間引き継ぎコード・略称
    public string Kokyaku { get; set; } = string.Empty;
    public string Seikyu { get; set; } = string.Empty;
    public string Siharai { get; set; } = string.Empty;
    public string SnmKokyaku { get; set; } = string.Empty;
    public string SnmSeikyu { get; set; } = string.Empty;
    public string SnmSiharai { get; set; } = string.Empty;

    // 請求支払条件登録画面 (KUNO0041)
    public int F230 { get; set; }
    public int F240 { get; set; }
    public int F250 { get; set; }
    public int F260 { get; set; }
    public int F270 { get; set; }
    public int F280 { get; set; }
    public int F290 { get; set; }
    public int F300 { get; set; }
    public int F310 { get; set; }
    public int F320 { get; set; }
    public int F330 { get; set; } = 1;
    public int F340 { get; set; } = 3;
    public int F350 { get; set; }
    public int F360 { get; set; }
    public int F370 { get; set; }
    public int F380 { get; set; }
    public int F390 { get; set; }
    public int F400 { get; set; }
    public int F410 { get; set; }
    public int F420 { get; set; }
    public int F430 { get; set; }
    public int F440 { get; set; }
    public int F800 { get; set; }
    public int F810 { get; set; }
    public int F820 { get; set; }
    public int F830 { get; set; }

    // 銀行口座情報登録画面 (KUNO0042)
    public long F450 { get; set; }
    public int F460 { get; set; } = 1;
    public string F470 { get; set; } = string.Empty;
    public string F480 { get; set; } = string.Empty;
    public long F490 { get; set; }
    public int F500 { get; set; } = 1;
    public string F510 { get; set; } = string.Empty;
    public string F520 { get; set; } = string.Empty;

    public void ResetBillingPaymentFields()
    {
        F230 = 0; F240 = 0; F250 = 0;
        F260 = 0; F270 = 0; F280 = 0;
        F290 = 0; F300 = 0; F310 = 0;
        F320 = 0; F330 = 1; F340 = 3;
        F350 = 0; F360 = 0; F370 = 0;
        F380 = 0; F390 = 0; F400 = 0;
        F410 = 0; F420 = 0; F430 = 0;
        F440 = 0;
        F800 = 0; F810 = 0; F820 = 0; F830 = 0;
    }

    public void ResetBankFields()
    {
        SetFlg3 = 0;
        F450 = 0; F460 = 1; F470 = string.Empty; F480 = string.Empty;
        F490 = 0; F500 = 1; F510 = string.Empty; F520 = string.Empty;
    }

    public void ResetAll()
    {
        Siha = 0;
        SihFlg = 0;
        SetFlg2 = 0;
        SetFlg3 = 0;
        CanFlg2 = 0;
        CanFlg3 = 0;
        ResetBillingPaymentFields();
        ResetBankFields();
    }
}

public sealed record Kuno0040GroupInfoDto(
    string ShortName,
    int KyoDd,
    string KysCd);

public sealed record GpkenItemDto(
    int Code,
    string CodeText,
    string NameKj,
    string NameS,
    string DisplayText);

public sealed record GnkkenBankItemDto(
    long Code,
    string CodeText,
    string NameKj,
    string DisplayText);

public sealed record GnkkenBranchItemDto(
    long Code,
    string CodeText,
    string NameKj,
    string NameS,
    string DisplayText);

public sealed record KykkenCustomerItemDto(
    string Code,
    string NameKj,
    string DisplayText);

public enum KykkenNameMatchMode
{
    Partial = 0,
    Prefix = 1,
    Suffix = 2
}

