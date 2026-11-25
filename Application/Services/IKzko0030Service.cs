using Application.DTOs;

namespace Application.Services;

public interface IKzko0030Service : ISyokenLookupService
{
    /// <summary>
    /// VB6 KZKO0030 Form_Load でのシステム日付・基準年月(G0YYMM, G1YYMM, G6YYMM, G36YYMM, fromYMD, toYMD)算出
    /// </summary>
    Kzko0030DateContextDto GetDateContext(DateTime? now = null);

    /// <summary>
    /// VB6 KZKO0030 get_KMSYOF(dwCD, strNM) 相当。
    /// 有効な商品の略称(SHNNAMES)を返す。未存在・削除済みの場合は null。
    /// </summary>
    string? GetActiveProductShortName(long kshncd);

    /// <summary>
    /// VB6 KZKO0030 cmdOK_Click 内の get_zaiko(GSYYMM) + meisai_set 相当。
    /// KMZAIKO に該当レコードが1件も存在しない場合は空リストを返す。
    /// </summary>
    IReadOnlyList<ZaikoMeisaiDto> SearchZaikoHistory(
        long kshncd,
        long startYymm,
        Kzko0030DateContextDto dateContext);
}

