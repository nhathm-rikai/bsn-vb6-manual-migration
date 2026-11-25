namespace Domain.Entities;

/// <summary>
/// 顧客マスタ (HAN01.KMKYAK) — 全65カラム
/// </summary>
public class KmKyak
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }
    public DateTime? DDate { get; set; }

    /// <summary>顧客コード — CHAR(10)</summary>
    public string KKykCd { get; set; } = string.Empty;

    /// <summary>顧客区分 (0:グループ 1:従業員 2:一般企業 3:一般個人)</summary>
    public int KKykKb { get; set; }

    /// <summary>顧客名(カナ)</summary>
    public string KykNameKn { get; set; } = string.Empty;

    /// <summary>顧客名(漢字)</summary>
    public string KykNameKj { get; set; } = string.Empty;

    /// <summary>手形振出先名(漢字)</summary>
    public string KykNameTg { get; set; } = string.Empty;

    /// <summary>顧客名(略称)</summary>
    public string KykNameS { get; set; } = string.Empty;

    public string UbnNo { get; set; } = string.Empty;
    public string Add1 { get; set; } = string.Empty;
    public string Add2 { get; set; } = string.Empty;
    public string Add3 { get; set; } = string.Empty;
    public string TelNo { get; set; } = string.Empty;
    public string FaxNo { get; set; } = string.Empty;

    /// <summary>請求先コード — CHAR(10)</summary>
    public string SksCd { get; set; } = string.Empty;

    /// <summary>支払先コード — CHAR(10)</summary>
    public string ShsCd { get; set; } = string.Empty;

    public int GpkCd { get; set; }
    public int SzCd { get; set; }
    public int BkCd { get; set; }
    public int MbnCd { get; set; }
    public int SknmCd { get; set; }
    public long Birthday { get; set; }
    public int Seibt { get; set; }
    public int SinNo { get; set; }
    public int HhknNo { get; set; }
    public long DrpDay { get; set; }
    public int TkKb { get; set; }

    // 請求支払条件 (2画面: KUNO0041)
    public int Ssdd1 { get; set; }
    public int IngtKb1 { get; set; }
    public int Indd1 { get; set; }
    public int Ssdd2 { get; set; }
    public int IngtKb2 { get; set; }
    public int Indd2 { get; set; }
    public int Ssdd3 { get; set; }
    public int IngtKb3 { get; set; }
    public int Indd3 { get; set; }
    public int SzkKb { get; set; }
    public int SsyoKb { get; set; }
    public int ThyoKb { get; set; }

    public int Shsdd1 { get; set; }
    public int ShgtKb1 { get; set; }
    public int Shydd1 { get; set; }
    public int Shsdd2 { get; set; }
    public int ShgtKb2 { get; set; }
    public int Shydd2 { get; set; }
    public int Shsdd3 { get; set; }
    public int ShgtKb3 { get; set; }
    public int Shydd3 { get; set; }
    public int SyhzkKb { get; set; }

    public int ShrHoho { get; set; }
    public int Site { get; set; }
    public int KsMm { get; set; }
    public int KsDd { get; set; }

    // 銀行口座情報 (3画面: KUNO0042)
    public long GkCd1 { get; set; }
    public int KzSbt1 { get; set; }
    public string KzNo1 { get; set; } = string.Empty;
    public string KzMegi1 { get; set; } = string.Empty;
    public long GkCd2 { get; set; }
    public int KzSbt2 { get; set; }
    public string KzNo2 { get; set; } = string.Empty;
    public string KzMegi2 { get; set; } = string.Empty;

    // その他拡張項目
    public int MakCd { get; set; }
    public int Jigyo { get; set; }
    public long CardNo { get; set; }

    // 2023/05/08 インボイス対応追加項目
    public string TorokuNo { get; set; } = string.Empty;
    public int KazeiKb { get; set; }
}

