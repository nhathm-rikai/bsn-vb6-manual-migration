using System.Globalization;
using System.Text;

namespace Application.Common;

/// <summary>
/// VB6 UserCmn.BAS / GLOBAL.BAS / KUNO0010.FRM の文字列・数値・バイト数チェック完全互換ヘルパー
/// </summary>
public static class LegacyTextHelper
{
    private static readonly Encoding ShiftJis;

    static LegacyTextHelper()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        ShiftJis = Encoding.GetEncoding(932);
    }

    /// <summary>
    /// VB6 FUNC_CSTR 相当: NULL -> "", 波ダッシュ(U+301C) -> 全角チルダ(U+FF5E) 変換
    /// </summary>
    public static string NormalizeString(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        return value.Contains('\u301C')
            ? value.Replace('\u301C', '\uFF5E')
            : value;
    }

    /// <summary>
    /// VB6 UserCmn.BAS FUNC_CHK_MOJISU(pMoji, pByte) 完全互換。
    /// Shift-JIS(CP932) バイト数で判定し、10以上は桁数オーバーエラー。
    ///   0: 半角のみ(正常), 1: 全角のみ(正常), 2: 全角と半角混在(正常)
    ///  10: 半角のみ(超過), 11: 全角のみ(超過), 12: 全角と半角混在(超過)
    /// </summary>
    public static int CheckMojisu(string? text, int maxBytes)
    {
        string trimmed = NormalizeString(text).Trim();
        int byteCount = ShiftJis.GetByteCount(trimmed);
        int charCount = trimmed.Length;

        if (byteCount > maxBytes)
        {
            if (byteCount == charCount)
            {
                return 10;
            }

            return (charCount > 0 && byteCount == charCount * 2) ? 11 : 12;
        }

        if (byteCount == charCount)
        {
            return 0;
        }

        return (charCount > 0 && byteCount == charCount * 2) ? 1 : 2;
    }

    /// <summary>
    /// VB6 StrConv(MidB$(StrConv(text, vbFromUnicode), 1, maxBytes), vbUnicode) 相当。
    /// Shift-JIS(CP932) バイト数で先頭 maxBytes 以内の文字列を切り出す（2バイト文字の境界で安全に切り詰める）。
    /// </summary>
    public static string SubStringByByte(string? text, int maxBytes)
    {
        string normalized = NormalizeString(text);
        if (string.IsNullOrEmpty(normalized) || maxBytes <= 0)
        {
            return string.Empty;
        }

        if (ShiftJis.GetByteCount(normalized) <= maxBytes)
        {
            return normalized;
        }

        var sb = new StringBuilder();
        int currentBytes = 0;
        foreach (char c in normalized)
        {
            int charBytes = ShiftJis.GetByteCount(new[] { c });
            if (currentBytes + charBytes > maxBytes)
            {
                break;
            }

            sb.Append(c);
            currentBytes += charBytes;
        }

        return sb.ToString();
    }

    /// <summary>
    /// VB6 LenB(StrConv(text, vbFromUnicode)) 相当の Shift-JIS(CP932) バイト長を返す。
    /// </summary>
    public static int GetShiftJisByteCount(string? text)
    {
        string normalized = NormalizeString(text);
        return ShiftJis.GetByteCount(normalized);
    }

    /// <summary>
    /// VB6 KUNO0040 (2011/07/22) 顧客名漢字 26バイト / 14バイト分割読込互換:
    /// LeftB(f030, 26) / MidB(f030, 27, 14)
    /// </summary>
    public static (string Part1, string Part2) SplitCustomerKanjiName(string? fullText)
    {
        string normalized = NormalizeString(fullText);
        if (string.IsNullOrEmpty(normalized))
        {
            return (string.Empty, string.Empty);
        }

        byte[] bytes = ShiftJis.GetBytes(normalized);
        int len1 = Math.Min(bytes.Length, 26);
        string part1 = ShiftJis.GetString(bytes, 0, len1);
        string part2 = bytes.Length > 26
            ? ShiftJis.GetString(bytes, 26, Math.Min(bytes.Length - 26, 14))
            : string.Empty;
        return (part1, part2);
    }

    /// <summary>
    /// VB6 KUNO0040 (2011/07/22) 顧客名漢字結合互換:
    /// Part1 を Trim 後に 26バイト未満なら半角スペース充填し、Part2 を連結する。
    /// </summary>
    public static string CombineCustomerKanjiName(string? part1, string? part2)
    {
        string p1 = NormalizeString(part1).Trim();
        int byteLen = ShiftJis.GetByteCount(p1);
        if (byteLen < 26)
        {
            p1 += new string(' ', 26 - byteLen);
        }

        return p1 + NormalizeString(part2);
    }

    /// <summary>
    /// VB6 Val() 相当の数値変換（先頭の数値部分のみを解釈、空文字や非数値は 0）
    /// </summary>
    public static decimal VbVal(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0m;
        }

        string s = text.TrimStart();
        int i = 0;
        bool hasDot = false;

        if (i < s.Length && (s[i] == '+' || s[i] == '-'))
        {
            i++;
        }

        while (i < s.Length)
        {
            char c = s[i];
            if (c >= '0' && c <= '9')
            {
                i++;
            }
            else if (c == '.' && !hasDot)
            {
                hasDot = true;
                i++;
            }
            else
            {
                break;
            }
        }

        string numericPart = s[..i];
        if (numericPart.Length == 0 || numericPart == "+" || numericPart == "-" || numericPart == "." || numericPart == "+." || numericPart == "-.")
        {
            return 0m;
        }

        return decimal.TryParse(numericPart, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result)
            ? result
            : 0m;
    }

    /// <summary>
    /// VB6 UserCmn.BAS FUNC_CDBL(STR) 相当: カンマ編集済み文字列も含めて数値に変換（空文字は 0）
    /// </summary>
    public static decimal FuncCdbl(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0m;
        }

        string clean = text.Replace(",", string.Empty).Trim();
        return VbVal(clean);
    }

    /// <summary>
    /// VB6 UserCmn.BAS FUNC_CINT(STR) 相当: 文字列を整数に変換（空文字は 0）
    /// </summary>
    public static int FuncCint(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }

        decimal val = FuncCdbl(text);
        return (int)Math.Round(val, MidpointRounding.ToEven);
    }

    /// <summary>
    /// VB6 KUNO0010.FRM numeric_hensyu(strtxt, wksu, wsu, strtext) 完全互換:
    /// ・空文字の場合は true, formattedText = ""
    /// ・10^wsu 倍した値に小数点が含まれる場合（小数点以下 wsu 桁超）は false
    /// ・10^wsu 倍した文字列長が wksu を超える場合は false
    /// ・正常時は "##,###,##0" または "##,###,##0.##" 形式でフォーマット
    /// </summary>
    public static bool TryFormatNumericHensyu(string? strtxt, int wksu, int wsu, out string formattedText)
    {
        formattedText = string.Empty;
        if (string.IsNullOrEmpty(strtxt))
        {
            return true;
        }

        // VB6 では呼び出し側で Format$(txt.Text) を渡すためカンマが除去される
        string clean = strtxt.Replace(",", string.Empty).Trim();
        if (string.IsNullOrEmpty(clean))
        {
            return true;
        }

        // 小数点が複数ある場合などの不正入力チェック
        int firstDot = clean.IndexOf('.');
        if (firstDot >= 0 && clean.IndexOf('.', firstDot + 1) >= 0)
        {
            return false;
        }

        decimal val = VbVal(clean);
        decimal wx = 1m;
        for (int i = 0; i < wsu; i++)
        {
            wx *= 10m;
        }

        double scaled = (double)(val * wx);
        string scaledStr = scaled.ToString("G15", CultureInfo.InvariantCulture);

        // 小数点以下桁数オーバー判定 (Mid$(Format$(Val(Trim$(strtxt)) * WX), wi, 1) = ".")
        if (scaledStr.Contains('.'))
        {
            return false;
        }

        // 有効桁数オーバー判定 (wlen > wksu)
        if (scaledStr.Length > wksu)
        {
            return false;
        }

        formattedText = val.ToString("#,##0.##", CultureInfo.InvariantCulture);
        return true;
    }

    /// <summary>
    /// VB6 KUNO0020 cmdSET_Click 消費税率チェック互換:
    /// Len(Format$(Val(Trim$(txtSRT.Text)) * 100)) &lt;= 4
    /// </summary>
    public static bool IsTaxRateValid(string? text)
    {
        decimal val = VbVal(text?.Trim());
        double scaled = (double)(val * 100m);
        string formatted = scaled.ToString("G15", CultureInfo.InvariantCulture);
        return formatted.Length <= 4;
    }

    /// <summary>
    /// VB6 Format$(FUNC_CSTR(SRITU), "#0.00") 相当（NULL時は ""）
    /// </summary>
    public static string FormatRate(decimal? value)
    {
        return value.HasValue
            ? value.Value.ToString("0.00", CultureInfo.InvariantCulture)
            : string.Empty;
    }

    /// <summary>
    /// VB6 Format$(FUNC_CSTR(STEKIYO), "00000000") 相当（NULL時は ""）
    /// </summary>
    public static string FormatTekiyo(long? value)
    {
        return value.HasValue
            ? value.Value.ToString("00000000", CultureInfo.InvariantCulture)
            : string.Empty;
    }

    /// <summary>
    /// VB6 UserCmn.BAS FUNC_SPACE_ADD(pTEXT, pLENG) 完全互換:
    /// Shift-JIS(CP932) バイト数で pLENG バイトに切り詰め、または半角空白を充填する。
    /// </summary>
    public static string FuncSpaceAdd(string? pText, int pLeng)
    {
        string strText = NormalizeString(pText).Trim();
        int byteLen = ShiftJis.GetByteCount(strText);

        if (pLeng == byteLen)
        {
            return strText;
        }

        if (pLeng < byteLen)
        {
            string truncated = SubStringByByte(strText, pLeng);
            int truncBytes = ShiftJis.GetByteCount(truncated);
            return truncBytes < pLeng
                ? truncated + new string(' ', pLeng - truncBytes)
                : truncated;
        }

        return strText + new string(' ', pLeng - byteLen);
    }

    /// <summary>
    /// VB6 INITIAL.BAS GetInitial(strKey) 互換:
    /// C:\kousyo\INITIAL.DAT からキーに対応する出力パスを取得し、"XXXX" を Windows ユーザー名に置換する。
    /// INITIAL.DAT が存在しない場合は C:\kousyo\{defaultFileName} を自動生成して返す。
    /// </summary>
    public static string GetInitialPath(string key, string defaultFileName)
    {
        const string initialDatPath = @"C:\kousyo\INITIAL.DAT";
        try
        {
            if (File.Exists(initialDatPath))
            {
                foreach (string rawLine in File.ReadAllLines(initialDatPath, ShiftJis))
                {
                    string line = rawLine.Trim();
                    if (string.IsNullOrEmpty(line) || line.StartsWith(';'))
                    {
                        continue;
                    }

                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && string.Equals(parts[0].Trim(), key, StringComparison.OrdinalIgnoreCase))
                    {
                        string val = parts[1].Trim().Replace("XXXX", Environment.UserName, StringComparison.OrdinalIgnoreCase);
                        if (!string.IsNullOrEmpty(val))
                        {
                            string? dir = Path.GetDirectoryName(val);
                            if (!string.IsNullOrEmpty(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }
                            return val;
                        }
                    }
                }
            }
        }
        catch
        {
            // フォールバックへ
        }

        const string defaultDir = @"C:\kousyo";
        Directory.CreateDirectory(defaultDir);
        return Path.Combine(defaultDir, defaultFileName);
    }
}
