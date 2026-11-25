using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmZaikoRepository
{
    /// <summary>
    /// VB6 KZKO0030 get_zaiko(strYMD) 相当。
    /// 指定年月以降・指定商品コードの在庫マスタ履歴を KZKDAY 昇順で取得する。
    /// </summary>
    IReadOnlyList<KmZaiko> GetHistoryFromMonth(long kshncd, long fromYearMonth);

    /// <summary>
    /// VB6 KZKO0030 get_znzaiko(zkym, syocd, suu, kin) 相当。
    /// 指定年月・商品コードの当月末在庫数量(TGMTSUU)・簿価(TGMSTKIN)を取得する。
    /// </summary>
    (decimal TgmtSuu, long TgmstKin)? GetEndingBalance(long kshncd, long yearMonth);

    /// <summary>
    /// VB6 KZKO0030 get_siirej(zkym, syocd, suu, kin) 相当。
    /// 仕入ジャーナル(HAN01.KTSJNL)から当月入庫数量・金額合計を取得する (CKSFLG = 1)。
    /// </summary>
    (decimal SumSirSu, long SumSirKin)? GetMonthlyPurchaseTotal(long shncd, long fromYmd, long toYmd);

    /// <summary>
    /// VB6 KZKO0030 get_zaicyoj(zkym, syocd, suu, kin) 相当。
    /// 在庫調整ジャーナル(HAN01.KTZJNL)から当月調整数量・金額合計を取得する。
    /// </summary>
    (decimal SumChoSuu, long SumChoKin)? GetMonthlyAdjustmentTotal(long shncd, long fromYmd, long toYmd);

    /// <summary>
    /// VB6 KZKO0030 get_zaicyoj2(zkym, syocd, suu, kin) 相当。
    /// 在庫調整ジャーナル(HAN01.KTZJNL)から増加分(CHOKU IN 21, 22, 23)の当月調整数量・金額合計を取得する。
    /// </summary>
    (decimal SumChoSuu, long SumChoKin)? GetMonthlyPositiveAdjustmentTotal(long shncd, long fromYmd, long toYmd);

    /// <summary>
    /// VB6 KZKO0030 get_uriagej(zkym, syocd, suu, kin) 相当。
    /// 売上ジャーナル(HAN01.KTUJNL)から当月出庫数量合計(SUM(URISU))を取得する (CKSFLG = 1)。
    /// </summary>
    decimal? GetMonthlySalesQuantity(long shncd, long fromYmd, long toYmd);

    /// <summary>
    /// VB6 KZKO0010 del_KMZAIKO(dwTOUYMD) 相当。
    /// 指定年月の在庫マスタから、論理削除済みまたは在庫対象外(ZKKB=0)の商品を削除する。
    /// </summary>
    int DeleteInvalidProductsForMonth(long kzkDay);

    /// <summary>
    /// VB6 KZKO0010 upd_zaiko (INSERT) 相当。
    /// HAN01.KMZAIKO へ新規登録する。
    /// </summary>
    void Insert(KmZaiko entity);

    /// <summary>
    /// VB6 KZKO0010 upd_zaiko (UPDATE) 相当。
    /// HAN01.KMZAIKO の既存レコードを更新する。
    /// </summary>
    void Update(KmZaiko entity);
}

