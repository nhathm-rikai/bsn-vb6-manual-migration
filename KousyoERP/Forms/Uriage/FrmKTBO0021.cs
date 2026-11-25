using System.Drawing.Printing;
using System.Globalization;
using System.Text;
using Application.DTOs;

namespace KousyoERP.Forms.Uriage;

/// <summary>
/// KTBO0021 — 売上履歴照会明細一覧 (VB6 KTB/KTBO0021.FRM 1:1 移行画面)
/// </summary>
public partial class FrmKTBO0021 : Form
{
    private const string ScreenTitle = "KTBO0020";
    private static readonly Encoding ShiftJis = Encoding.GetEncoding(932);

    private readonly IReadOnlyList<Ktbo0020RowDto> _rows;
    private readonly int _totalPages;
    private int _currentPage = 1;

    public FrmKTBO0021(Ktbo0020SearchResultDto searchResult)
    {
        _rows = searchResult.Rows;
        _totalPages = searchResult.TotalPages;
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using var pen = new Pen(Color.Black, 1f);
        Graphics g = e.Graphics;

        // 外枠とヘッダー線 (VB6 Line1..Line20: X=8..872, Y=56..584)
        g.DrawLine(pen, 8, 56, 872, 56);
        g.DrawLine(pen, 8, 104, 872, 104);
        for (int i = 1; i <= 10; i++)
        {
            int y = 104 + i * 48;
            g.DrawLine(pen, 8, y, 872, y);
        }

        // 通し縦線 (VB6 Line2, Line7, Line8, Line11, Line12, Line3)
        int[] fullVerticalXs = { 8, 48, 376, 632, 776, 872 };
        foreach (int x in fullVerticalXs)
        {
            g.DrawLine(pen, x, 56, x, 584);
        }

        // ヘッダー内上段区切り (VB6 Line9, Line10: X=456, 544, Y=56..88)
        g.DrawLine(pen, 456, 56, 456, 82);
        g.DrawLine(pen, 544, 56, 544, 82);

        // 各明細行の上段区切り (VB6 Line21..Line40: X=456, 544, 各行上半分 24px)
        for (int i = 0; i < 10; i++)
        {
            int yTop = 104 + i * 48;
            g.DrawLine(pen, 456, yTop, 456, yTop + 24);
            g.DrawLine(pen, 544, yTop, 544, yTop + 24);
        }
    }

    private void FrmKTBO0021_Load(object? sender, EventArgs e)
    {
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        _currentPage = 1;
        ClearPage();
        DisplayCurrentPage();
    }

    private void ClearPage()
    {
        for (int i = 0; i < 10; i++)
        {
            lblNO[i].Text = string.Empty;
            lblHNM[i].Text = string.Empty;
            lblDENNO[i].Text = string.Empty;
            lblHANDAY[i].Text = string.Empty;
            lblSUU[i].Text = string.Empty;
            lblTNK[i].Text = string.Empty;
            lblHANKIN[i].Text = string.Empty;
            lblSNM1[i].Text = string.Empty;
            lblSNM2[i].Text = string.Empty;
            lblTANNM[i].Text = string.Empty;
        }
    }

    private void DisplayCurrentPage()
    {
        int startIdx = (_currentPage - 1) * 10;

        for (int slot = 0; slot < 10; slot++)
        {
            int idx = startIdx + slot;
            if (idx >= _rows.Count)
            {
                break;
            }

            Ktbo0020RowDto row = _rows[idx];

            lblNO[slot].Text = row.RowNo.ToString(CultureInfo.InvariantCulture);
            lblHNM[slot].Text = TruncateShiftJis(row.CustomerName, 30);
            lblDENNO[slot].Text = row.Kdenno.ToString("0000000", CultureInfo.InvariantCulture);
            lblHANDAY[slot].Text = FormatYmd(row.Hbiday);
            lblSUU[slot].Text = FormatDecimalWithLegacyPadding(row.Urisu);
            lblTNK[slot].Text = FormatDecimalWithLegacyPadding(row.Uritan);
            lblHANKIN[slot].Text = row.Hankin.ToString("#,##0", CultureInfo.InvariantCulture);
            lblSNM1[slot].Text = row.ShnNameN1;
            lblSNM2[slot].Text = TruncateShiftJis(row.ShnNameN2, 28);

            // VB6 KTBO0021.FRM line 3101 (2003/04/11 宏商物産の意向により営業担当者を非表示)
            lblTANNM[slot].Text = string.Empty;
        }

        // VB6 KTBO0021.FRM lines 3112-3124: 次頁・前頁ボタン活性制御
        cmdNEXT.Enabled = _totalPages > _currentPage;
        cmdBACK.Enabled = _currentPage > 1;
    }

    private static string FormatYmd(int ymd)
    {
        string s = ymd.ToString("00000000", CultureInfo.InvariantCulture);
        return $"{s[..4]}/{s[4..6]}/{s[6..8]}";
    }

    private static string FormatDecimalWithLegacyPadding(decimal value)
    {
        // VB6: 整数なら "#,##0" & "   "、小数ありなら "#,##0.0#"
        if (decimal.Truncate(value) == value)
        {
            return value.ToString("#,##0", CultureInfo.InvariantCulture) + "   ";
        }

        return value.ToString("#,##0.0#", CultureInfo.InvariantCulture);
    }

    private static string TruncateShiftJis(string text, int maxBytes)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        byte[] bytes = ShiftJis.GetBytes(text);
        if (bytes.Length <= maxBytes)
        {
            return text;
        }

        var sb = new StringBuilder(text.Length);
        int used = 0;
        foreach (char ch in text)
        {
            int charBytes = ShiftJis.GetByteCount(new[] { ch });
            if (used + charBytes > maxBytes)
            {
                break;
            }

            sb.Append(ch);
            used += charBytes;
        }

        return sb.ToString();
    }

    private void cmdBACK_Click(object? sender, EventArgs e)
    {
        if (_currentPage == 1)
        {
            MessageBox.Show("前頁が存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        _currentPage--;
        ClearPage();
        DisplayCurrentPage();
    }

    private void cmdNEXT_Click(object? sender, EventArgs e)
    {
        if (_currentPage >= _totalPages)
        {
            MessageBox.Show("次頁が存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        _currentPage++;
        ClearPage();
        DisplayCurrentPage();
    }

    private void cmdprt_Click(object? sender, EventArgs e)
    {
        // VB6 Me.PrintForm 相当: 背景を白にしボタン類を非表示にしてフォームイメージを印刷
        Color prevBackColor = BackColor;
        try
        {
            BackColor = Color.White;
            SetAllLabelsBackColor(Color.White);
            cmdBACK.Visible = false;
            cmdNEXT.Visible = false;
            cmdprt.Visible = false;
            cmdCAN.Visible = false;
            Refresh();

            using var bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            DrawToBitmap(bmp, new Rectangle(Point.Empty, ClientSize));

            using var printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.PrintPage += (_, ev) =>
            {
                Rectangle bounds = ev.MarginBounds;
                float scale = Math.Min(
                    (float)bounds.Width / bmp.Width,
                    (float)bounds.Height / bmp.Height);
                int w = (int)(bmp.Width * scale);
                int h = (int)(bmp.Height * scale);
                ev.Graphics?.DrawImage(bmp, bounds.Left, bounds.Top, w, h);
                ev.HasMorePages = false;
            };
            printDoc.Print();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"印刷エラー: {ex.Message}", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        finally
        {
            BackColor = prevBackColor;
            SetAllLabelsBackColor(prevBackColor);
            cmdBACK.Visible = true;
            cmdNEXT.Visible = true;
            cmdprt.Visible = true;
            cmdCAN.Visible = true;
            Refresh();
        }
    }

    private void SetAllLabelsBackColor(Color color)
    {
        foreach (Control ctrl in Controls)
        {
            if (ctrl is Label lbl)
            {
                lbl.BackColor = color;
            }
        }
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        Close();
    }
}

