using System.Drawing.Printing;
using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;
using KousyoERP.Forms.Common;

namespace KousyoERP.Forms.Zaiko;

/// <summary>
/// KZKO0030 — 在庫履歴照会 (VB6 frmKZKO0030 / KZKO0030.BAS 1:1 移行画面)
/// </summary>
public partial class FrmKZKO0030 : Form
{
    private const string ScreenTitle = "KZKO0030";
    private const int RowsPerPage = 9;

    private readonly IKzko0030Service _service;

    private Kzko0030DateContextDto _dateContext = null!;
    private IReadOnlyList<ZaikoMeisaiDto> _tzaiko = Array.Empty<ZaikoMeisaiDto>();
    private int _glbMaxIdx;
    private int _glbDspIdx;

    private Label[] _lblKzkDay = null!;
    private Label[] _lblZnmtSuu = null!;
    private Label[] _lblZnmtKin = null!;
    private Label[] _lblTgnySuu = null!;
    private Label[] _lblTgnyKin = null!;
    private Label[] _lblTgsySuu = null!;
    private Label[] _lblTgsyKin = null!;
    private Label[] _lblTgcsSuu = null!;
    private Label[] _lblTgcsKin = null!;
    private Label[] _lblTgmtSuu = null!;
    private Label[] _lblTgmtKin = null!;

    public FrmKZKO0030(IKzko0030Service service)
    {
        _service = service;
        InitializeComponent();
        InitializeLabelArrays();
    }

    private void InitializeLabelArrays()
    {
        _lblKzkDay = [lblKZKDAY0, lblKZKDAY1, lblKZKDAY2, lblKZKDAY3, lblKZKDAY4, lblKZKDAY5, lblKZKDAY6, lblKZKDAY7, lblKZKDAY8];
        _lblZnmtSuu = [lblZNMTSUU0, lblZNMTSUU1, lblZNMTSUU2, lblZNMTSUU3, lblZNMTSUU4, lblZNMTSUU5, lblZNMTSUU6, lblZNMTSUU7, lblZNMTSUU8];
        _lblZnmtKin = [lblZNMTKIN0, lblZNMTKIN1, lblZNMTKIN2, lblZNMTKIN3, lblZNMTKIN4, lblZNMTKIN5, lblZNMTKIN6, lblZNMTKIN7, lblZNMTKIN8];
        _lblTgnySuu = [lblTGNYSUU0, lblTGNYSUU1, lblTGNYSUU2, lblTGNYSUU3, lblTGNYSUU4, lblTGNYSUU5, lblTGNYSUU6, lblTGNYSUU7, lblTGNYSUU8];
        _lblTgnyKin = [lblTGNYKIN0, lblTGNYKIN1, lblTGNYKIN2, lblTGNYKIN3, lblTGNYKIN4, lblTGNYKIN5, lblTGNYKIN6, lblTGNYKIN7, lblTGNYKIN8];
        _lblTgsySuu = [lblTGSYSUU0, lblTGSYSUU1, lblTGSYSUU2, lblTGSYSUU3, lblTGSYSUU4, lblTGSYSUU5, lblTGSYSUU6, lblTGSYSUU7, lblTGSYSUU8];
        _lblTgsyKin = [lblTGSYKIN0, lblTGSYKIN1, lblTGSYKIN2, lblTGSYKIN3, lblTGSYKIN4, lblTGSYKIN5, lblTGSYKIN6, lblTGSYKIN7, lblTGSYKIN8];
        _lblTgcsSuu = [lblTGCSSUU0, lblTGCSSUU1, lblTGCSSUU2, lblTGCSSUU3, lblTGCSSUU4, lblTGCSSUU5, lblTGCSSUU6, lblTGCSSUU7, lblTGCSSUU8];
        _lblTgcsKin = [lblTGCSKIN0, lblTGCSKIN1, lblTGCSKIN2, lblTGCSKIN3, lblTGCSKIN4, lblTGCSKIN5, lblTGCSKIN6, lblTGCSKIN7, lblTGCSKIN8];
        _lblTgmtSuu = [lblTGMTSUU0, lblTGMTSUU1, lblTGMTSUU2, lblTGMTSUU3, lblTGMTSUU4, lblTGMTSUU5, lblTGMTSUU6, lblTGMTSUU7, lblTGMTSUU8];
        _lblTgmtKin = [lblTGMTKIN0, lblTGMTKIN1, lblTGMTKIN2, lblTGMTKIN3, lblTGMTKIN4, lblTGMTKIN5, lblTGMTKIN6, lblTGMTKIN7, lblTGMTKIN8];
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using var pen = new Pen(Color.Black, 1f);

        // Shape1 外枠 (Left=9, Top=168, Width=920, Height=362)
        e.Graphics.DrawRectangle(pen, 9, 168, 920, 362);

        // 横線 Line1(0..10)
        e.Graphics.DrawLine(pen, 82, 199, 928, 199);   // Line1(0) ヘッダ2段目境界
        e.Graphics.DrawLine(pen, 10, 232, 928, 232);   // Line1(1) ヘッダ下
        e.Graphics.DrawLine(pen, 10, 265, 928, 265);   // Line1(2)
        e.Graphics.DrawLine(pen, 10, 298, 928, 298);   // Line1(3)
        e.Graphics.DrawLine(pen, 10, 331, 928, 331);   // Line1(4)
        e.Graphics.DrawLine(pen, 10, 364, 928, 364);   // Line1(5)
        e.Graphics.DrawLine(pen, 10, 397, 928, 397);   // Line1(6)
        e.Graphics.DrawLine(pen, 10, 430, 928, 430);   // Line1(7)
        e.Graphics.DrawLine(pen, 10, 464, 928, 464);   // Line1(8)
        e.Graphics.DrawLine(pen, 10, 496, 928, 496);   // Line1(10)

        // 縦線 Line3(0..9)
        e.Graphics.DrawLine(pen, 82, 168, 82, 529);    // Line3(0) 年月 | 前月末在庫
        e.Graphics.DrawLine(pen, 158, 199, 158, 529);  // Line3(1) 前月末在庫 数量 | 簿価
        e.Graphics.DrawLine(pen, 250, 168, 250, 529);  // Line3(3) 前月末在庫 | 入庫
        e.Graphics.DrawLine(pen, 326, 200, 326, 529);  // Line3(2) 入庫 数量 | 簿価
        e.Graphics.DrawLine(pen, 418, 168, 418, 529);  // Line3(4) 入庫 | 出庫
        e.Graphics.DrawLine(pen, 494, 200, 494, 529);  // Line3(5) 出庫 数量 | 簿価
        e.Graphics.DrawLine(pen, 586, 168, 586, 529);  // Line3(7) 出庫 | 在庫調整
        e.Graphics.DrawLine(pen, 668, 200, 668, 529);  // Line3(6) 在庫調整 数量 | 簿価
        e.Graphics.DrawLine(pen, 760, 168, 760, 529);  // Line3(8) 在庫調整 | 月末在庫
        e.Graphics.DrawLine(pen, 836, 200, 836, 529);  // Line3(9) 月末在庫 数量 | 簿価
    }

    private void FrmKZKO0030_Load(object? sender, EventArgs e)
    {
        _dateContext = _service.GetDateContext();
        lblYMD.Text = _dateContext.SystemDateText;
        txtkyy.Text = _dateContext.DefaultYearText;
        txtkmm.Text = _dateContext.DefaultMonthText;
        cmdPRN.Enabled = false;

        _glbMaxIdx = 0;
        _glbDspIdx = 0;
    }

    private void cmdEND_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        AllClear();
        txtSYOCD.Focus();
    }

    private void cmdDN_Click(object? sender, EventArgs e)
    {
        // 前頁クリック
        if (_glbDspIdx <= 1)
        {
            MessageBox.Show("前頁がありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        _glbDspIdx -= RowsPerPage;
        if (_glbDspIdx <= 0)
        {
            _glbDspIdx = 1;
        }

        MeisaiHyoji(_glbDspIdx);
    }

    private void cmdUP_Click(object? sender, EventArgs e)
    {
        // 次頁クリック
        if (_glbDspIdx + RowsPerPage > _glbMaxIdx)
        {
            MessageBox.Show("次頁がありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        _glbDspIdx += RowsPerPage;
        MeisaiHyoji(_glbDspIdx);
    }

    private void cmdTOP_Click(object? sender, EventArgs e)
    {
        // 先頭クリック
        if (_glbMaxIdx == 0)
        {
            return;
        }

        _glbDspIdx = 1;
        MeisaiHyoji(_glbDspIdx);
    }

    private void cmdBTM_Click(object? sender, EventArgs e)
    {
        // 最終クリック
        if (_glbMaxIdx <= RowsPerPage)
        {
            return;
        }

        _glbDspIdx = _glbMaxIdx - (RowsPerPage - 1);
        MeisaiHyoji(_glbDspIdx);
    }

    private void cmdPRN_Click(object? sender, EventArgs e)
    {
        // VB6 Me.PrintForm 相当: 背景を白にしボタン類を非表示にしてフォームイメージを印刷
        Color prevBackColor = BackColor;
        try
        {
            BackColor = Color.White;
            SetRowLabelsBackColor(Color.White);
            cmdPRN.Visible = false;
            cmdOK.Visible = false;
            cmdCAN.Visible = false;
            cmdEND.Visible = false;
            cmdUP.Visible = false;
            cmdDN.Visible = false;
            cmdTOP.Visible = false;
            cmdBTM.Visible = false;
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
            SetRowLabelsBackColor(prevBackColor);
            cmdPRN.Visible = true;
            cmdOK.Visible = true;
            cmdCAN.Visible = true;
            cmdEND.Visible = true;
            cmdUP.Visible = true;
            cmdDN.Visible = true;
            cmdTOP.Visible = true;
            cmdBTM.Visible = true;
            Refresh();
        }
    }

    private void SetRowLabelsBackColor(Color color)
    {
        foreach (Control ctrl in Controls)
        {
            if (ctrl is Label lbl)
            {
                lbl.BackColor = color;
            }
        }
    }

    private void txtSYOCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSYOCD.Text = string.Empty;
            lblSYOCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtkyy.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtSYOCD_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && (e.KeyChar < '0' || e.KeyChar > '9'))
        {
            e.Handled = true;
        }
    }

    private void txtSYOCD_Leave(object? sender, EventArgs e)
    {
        if (cmdEND.Focused || cmdCAN.Focused || cmdSEEKSYO.Focused)
        {
            return;
        }

        string raw = txtSYOCD.Text.Trim();
        if (!long.TryParse(raw, out long dwScd) || dwScd == 0)
        {
            txtSYOCD.Text = string.Empty;
            lblSYOCD.Text = string.Empty;
            return;
        }

        txtSYOCD.Text = dwScd.ToString("00000000", CultureInfo.InvariantCulture);

        string? shortName;
        try
        {
            shortName = _service.GetActiveProductShortName(dwScd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("KNRO1060", "商品マスタ", dwScd.ToString(CultureInfo.InvariantCulture), ex);
            return;
        }

        if (shortName is null)
        {
            MessageBox.Show("入力された商品ｺｰﾄﾞは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSYOCD.Text = string.Empty;
            lblSYOCD.Text = string.Empty;
            txtSYOCD.Focus();
            return;
        }

        lblSYOCD.Text = shortName;
    }

    private void cmdSEEKSYO_Click(object? sender, EventArgs e)
    {
        using var dlg = new FrmSYOKEN(_service);
        dlg.ShowDialog(this);

        if (dlg.Selected && long.TryParse(dlg.SelectedProductCode, out long code) && code > 0)
        {
            txtSYOCD.Text = code.ToString("00000000", CultureInfo.InvariantCulture);
            try
            {
                lblSYOCD.Text = _service.GetActiveProductShortName(code) ?? string.Empty;
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError("KNRO1060", "商品マスタ", code.ToString(CultureInfo.InvariantCulture), ex);
            }
        }
        else
        {
            txtSYOCD.Text = string.Empty;
            lblSYOCD.Text = string.Empty;
        }

        txtSYOCD.Focus();
    }

    private void txtkyy_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtkyy.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtkmm.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtkyy_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && (e.KeyChar < '0' || e.KeyChar > '9'))
        {
            e.Handled = true;
        }
    }

    private void txtkyy_Leave(object? sender, EventArgs e)
    {
        if (cmdEND.Focused || cmdCAN.Focused)
        {
            return;
        }

        string strYy = txtkyy.Text.Trim();
        if (string.IsNullOrEmpty(strYy))
        {
            return;
        }

        if (!int.TryParse(strYy, out int yy) || yy < 2001)
        {
            MessageBox.Show("指定された年は範囲外です。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtkyy.Text = string.Empty;
            txtkyy.Focus();
            return;
        }

        txtkyy.Text = yy.ToString("0000", CultureInfo.InvariantCulture);
    }

    private void txtkmm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtkmm.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            cmdOK.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtkmm_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && (e.KeyChar < '0' || e.KeyChar > '9'))
        {
            e.Handled = true;
        }
    }

    private void txtkmm_Leave(object? sender, EventArgs e)
    {
        if (cmdEND.Focused || cmdCAN.Focused)
        {
            return;
        }

        string strMm = txtkmm.Text.Trim();
        if (string.IsNullOrEmpty(strMm))
        {
            return;
        }

        if (!int.TryParse(strMm, out int mm) || mm < 1 || mm > 12)
        {
            MessageBox.Show("指定された月は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtkmm.Text = string.Empty;
            txtkmm.Focus();
            return;
        }

        txtkmm.Text = mm.ToString("00", CultureInfo.InvariantCulture);
    }

    private void spnKYY_SpinDown(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtkyy.Text.Trim(), out int yy) || yy <= 2001)
        {
            txtkyy.Focus();
        }
        else
        {
            txtkyy.Text = (yy - 1).ToString("0000", CultureInfo.InvariantCulture);
            txtkyy.Focus();
        }
        txtkyy.Refresh();
    }

    private void spnKYY_SpinUp(object? sender, EventArgs e)
    {
        string raw = txtkyy.Text.Trim();
        if (string.IsNullOrEmpty(raw) || !int.TryParse(raw, out int yy) || yy == 9999 || yy < 2000)
        {
            txtkyy.Focus();
        }
        else
        {
            txtkyy.Text = (yy + 1).ToString("0000", CultureInfo.InvariantCulture);
            txtkyy.Focus();
        }
        txtkyy.Refresh();
    }

    private void spnKMM_SpinDown(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtkmm.Text.Trim(), out int mm) || mm <= 1 || mm > 13)
        {
            txtkmm.Focus();
        }
        else
        {
            txtkmm.Text = (mm - 1).ToString("00", CultureInfo.InvariantCulture);
            txtkmm.Focus();
        }
        txtkmm.Refresh();
    }

    private void spnKMM_SpinUp(object? sender, EventArgs e)
    {
        string raw = txtkmm.Text.Trim();
        if (string.IsNullOrEmpty(raw) || !int.TryParse(raw, out int mm) || mm >= 12)
        {
            txtkmm.Focus();
        }
        else
        {
            txtkmm.Text = (mm + 1).ToString("00", CultureInfo.InvariantCulture);
            txtkmm.Focus();
        }
        txtkmm.Refresh();
    }

    private void cmdOK_Click(object? sender, EventArgs e)
    {
        // 入力チェック
        if (!long.TryParse(txtSYOCD.Text.Trim(), out long gSyocd) || gSyocd == 0)
        {
            MessageBox.Show("商品コードの指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSYOCD.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtkyy.Text) || !int.TryParse(txtkyy.Text.Trim(), out int yy))
        {
            MessageBox.Show("開始年月の指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtkyy.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtkmm.Text) || !int.TryParse(txtkmm.Text.Trim(), out int mm))
        {
            MessageBox.Show("開始年月の指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtkmm.Focus();
            return;
        }

        long gsYymm = yy * 100L + mm;
        if (gsYymm <= _dateContext.G36Yymm)
        {
            MessageBox.Show("開始年月の指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtkyy.Focus();
            return;
        }

        Cursor = Cursors.WaitCursor;
        try
        {
            _tzaiko = _service.SearchZaikoHistory(gSyocd, gsYymm, _dateContext);
        }
        catch (Exception ex)
        {
            Cursor = Cursors.Default;
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "在庫ﾏｽﾀ", $"{txtkyy.Text}年{txtkmm.Text}月　商品C : {gSyocd}", ex);
            return;
        }

        if (_tzaiko.Count == 0)
        {
            Cursor = Cursors.Default;
            MessageBox.Show("該当レコードが存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        // 入力コントロール無効化
        txtSYOCD.Enabled = false;
        cmdSEEKSYO.Enabled = false;
        txtkyy.Enabled = false;
        txtkmm.Enabled = false;
        spnkyy.Enabled = false;
        spnkmm.Enabled = false;
        cmdOK.Enabled = false;

        _glbMaxIdx = _tzaiko.Count;
        _glbDspIdx = 1;
        MeisaiHyoji(_glbDspIdx);

        Cursor = Cursors.Default;
        cmdUP.Enabled = true;
        cmdDN.Enabled = true;
        cmdTOP.Enabled = true;
        cmdBTM.Enabled = true;
        cmdCAN.Enabled = true;
        cmdEND.Enabled = true;
        cmdPRN.Enabled = true;
    }

    private void MeisaiHyoji(int dfIdx)
    {
        for (int i = 0; i < RowsPerPage; i++)
        {
            _lblKzkDay[i].Text = string.Empty;
            _lblZnmtSuu[i].Text = string.Empty;
            _lblZnmtKin[i].Text = string.Empty;
            _lblTgnySuu[i].Text = string.Empty;
            _lblTgnyKin[i].Text = string.Empty;
            _lblTgsySuu[i].Text = string.Empty;
            _lblTgsyKin[i].Text = string.Empty;
            _lblTgcsSuu[i].Text = string.Empty;
            _lblTgcsKin[i].Text = string.Empty;
            _lblTgmtSuu[i].Text = string.Empty;
            _lblTgmtKin[i].Text = string.Empty;
        }

        int idx2 = dfIdx; // 1-based index
        for (int i = 0; i < RowsPerPage; i++)
        {
            if (idx2 >= 1 && idx2 <= _glbMaxIdx)
            {
                ZaikoMeisaiDto row = _tzaiko[idx2 - 1];
                _lblKzkDay[i].Text = row.KzkDayText;
                _lblZnmtSuu[i].Text = row.ZnmtSuuText;
                _lblZnmtKin[i].Text = row.ZnmtKinText;
                _lblTgnySuu[i].Text = row.TgnySuuText;
                _lblTgnyKin[i].Text = row.TgnyKinText;
                _lblTgsySuu[i].Text = row.TgsySuuText;
                _lblTgsyKin[i].Text = row.TgsyKinText;
                _lblTgcsSuu[i].Text = row.TgcsSuuText;
                _lblTgcsKin[i].Text = row.TgcsKinText;
                _lblTgmtSuu[i].Text = row.TgmtSuuText;
                _lblTgmtKin[i].Text = row.TgmtKinText;
            }
            idx2++;
        }
    }

    private void AllClear()
    {
        txtSYOCD.Text = string.Empty;
        lblSYOCD.Text = string.Empty;
        txtkyy.Text = _dateContext.DefaultYearText;
        txtkmm.Text = _dateContext.DefaultMonthText;

        for (int i = 0; i < RowsPerPage; i++)
        {
            _lblKzkDay[i].Text = string.Empty;
            _lblZnmtSuu[i].Text = string.Empty;
            _lblZnmtKin[i].Text = string.Empty;
            _lblTgnySuu[i].Text = string.Empty;
            _lblTgnyKin[i].Text = string.Empty;
            _lblTgsySuu[i].Text = string.Empty;
            _lblTgsyKin[i].Text = string.Empty;
            _lblTgcsSuu[i].Text = string.Empty;
            _lblTgcsKin[i].Text = string.Empty;
            _lblTgmtSuu[i].Text = string.Empty;
            _lblTgmtKin[i].Text = string.Empty;
        }

        txtSYOCD.Enabled = true;
        cmdSEEKSYO.Enabled = true;
        txtkyy.Enabled = true;
        txtkmm.Enabled = true;
        spnkyy.Enabled = true;
        spnkmm.Enabled = true;
        cmdOK.Enabled = true;
        cmdUP.Enabled = true;
        cmdDN.Enabled = true;
        cmdTOP.Enabled = true;
        cmdBTM.Enabled = true;
        cmdCAN.Enabled = true;
        cmdEND.Enabled = true;
        cmdPRN.Enabled = false;

        _tzaiko = Array.Empty<ZaikoMeisaiDto>();
        _glbMaxIdx = 0;
        _glbDspIdx = 0;
    }
}

