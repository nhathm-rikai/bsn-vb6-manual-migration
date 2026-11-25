using Oracle.ManagedDataAccess.Client;

namespace KousyoERP.Common;

/// <summary>
/// VB6 GLOBAL.BAS gsConnect_DB / sqlerror 完全互換のエラーメッセージダイアログ
/// </summary>
public static class LegacyErrorDialog
{
    public static void ShowConnectError(string programName, Exception ex)
    {
        string msg = $"プログラム名 {programName}\nデータベースに接続できません。\n{ex.Message}";
        MessageBox.Show(msg, programName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    public static void ShowSqlError(string programName, string tableName, string keyValue, Exception ex)
    {
        if (ex is OracleException oraEx)
        {
            string msg =
                $"プログラム名   {programName}\n" +
                $"テーブル名      {tableName}\n" +
                $"キー値           {keyValue}\n\r\n" +
                $"SQLｴﾗｰNo= {oraEx.Number}\r\n" +
                $"ﾒｯｾｰｼﾞ= {oraEx.Message}";

            MessageBox.Show(msg, "データベースエラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
            string msg =
                $"プログラム名   {programName}\n" +
                $"テーブル名      {tableName}\n" +
                $"キー値           {keyValue}\n\r\n" +
                $"VBｴﾗｰNo= {ex.HResult}\r\n" +
                $"ﾒｯｾｰｼﾞ= {ex.Message}";

            MessageBox.Show(msg, "ＶＢエラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}

