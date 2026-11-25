using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;
using KousyoERP.Forms.Common;

namespace KousyoERP.Forms.Uriage;

/// <summary>
/// KTBO0020 — 売上履歴照会条件指定 (VB6 KTB/KTBO0020.FRM 1:1 移行画面)
/// </summary>
public partial class FrmKTBO0020 : Form
{
    private const string ScreenTitle = "KTBO0020";

    private readonly IKtbo0020Service _service;

    public FrmKTBO0020()
        : this(CreateDefaultService())
    {
    }

    public FrmKTBO0020(IKtbo0020Service service)
    {
        _service = service;
        InitializeComponent();
    }

    private static IKtbo0020Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Ktbo0020Service(
            new Infrastructure.Repositories.KtUjnlRepository(factory),
            new Infrastructure.Repositories.KmKyakRepository(factory),
            new Infrastructure.Repositories.KmTantRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private void FrmKTBO0020_Load(object? sender, EventArgs e)
    {
        try
        {
            _service.VerifyConnection();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "データベース接続", "Oracle接続確認", ex);
            Close();
            return;
        }

        DateTime now = DateTime.Now;
        lblYMD.Text = now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        // 画面クリア
        txtHANCD.Text = string.Empty;
        lblHANCD.Text = string.Empty;
        txtTANCD.Text = string.Empty;
        lblTANCD.Text = string.Empty;
        txtDAICD.Text = string.Empty;
        lblDAICD.Text = string.Empty;
        txtTYUCD.Text = string.Empty;
        lblTYUCD.Text = string.Empty;
        txtSYOCD.Text = string.Empty;
        lblSYOCD.Text = string.Empty;

        // システム日付設定
        string nowYyyy = now.ToString("yyyy", CultureInfo.InvariantCulture);
        string nowMm = now.ToString("MM", CultureInfo.InvariantCulture);
        string nowDd = now.ToString("dd", CultureInfo.InvariantCulture);

        txtKNEN.Text = nowYyyy;
        txtKTUKI.Text = nowMm;
        txtKHI.Text = nowDd;
        txtSNEN.Text = nowYyyy;
        txtSTUKI.Text = nowMm;
        txtSHI.Text = nowDd;
    }

    // =========================================================================
    // 共通数値キー制限
    // =========================================================================

    private void NumericOnly_KeyPress(object? sender, KeyPressEventArgs e)
    {
        // VB6: If KeyAscii < Asc("0") Or KeyAscii > Asc("9") Then If KeyAscii > Asc(" ") Then KeyAscii = 0
        if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar > ' ')
        {
            e.Handled = true;
        }
    }

    private static long ParseVal(string text)
    {
        string trimmed = text.Trim();
        return long.TryParse(trimmed, NumberStyles.Integer, CultureInfo.InvariantCulture, out long val) ? val : 0;
    }

    // =========================================================================
    // 販売先コード (txtHANCD / cmdSEEKHAN)
    // =========================================================================

    private void cmdSEEKHAN_Click(object? sender, EventArgs e)
    {
        using var dialog = new FrmKYKKEN();
        dialog.ShowDialog(this);

        if (dialog.IsSelected)
        {
            string selectedCode = dialog.SelectedCustomerCode.Trim();
            txtHANCD.Text = selectedCode;
            try
            {
                string? name = _service.GetActiveCustomerName(selectedCode);
                lblHANCD.Text = name ?? string.Empty;
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "顧客マスタ", selectedCode, ex);
            }
        }
        else
        {
            txtHANCD.Text = string.Empty;
            lblHANCD.Text = string.Empty;
        }

        txtHANCD.Focus();
    }

    private void txtHANCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtHANCD.Text = string.Empty;
            lblHANCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtTANCD.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtHANCD_Leave(object? sender, EventArgs e)
    {
        long val = ParseVal(txtHANCD.Text);
        if (val == 0)
        {
            txtHANCD.Text = string.Empty;
            lblHANCD.Text = string.Empty;
            return;
        }

        string formatted = val.ToString("0000000000", CultureInfo.InvariantCulture);
        txtHANCD.Text = formatted;

        string? name;
        try
        {
            name = _service.GetActiveCustomerName(formatted);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "顧客マスタ", formatted, ex);
            return;
        }

        if (name is null)
        {
            MessageBox.Show("入力された販売先ｺｰﾄﾞは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtHANCD.Text = string.Empty;
            lblHANCD.Text = string.Empty;
            txtHANCD.Focus();
            return;
        }

        lblHANCD.Text = name;
    }

    // =========================================================================
    // 営業担当コード (txtTANCD / cmdSEEKTAN)
    // =========================================================================

    private void cmdSEEKTAN_Click(object? sender, EventArgs e)
    {
        using var dialog = new FrmTANKEN(_service);
        dialog.ShowDialog(this);

        if (dialog.Selected)
        {
            int code = (int)ParseVal(dialog.SelectedTantCode);
            txtTANCD.Text = code.ToString("0000", CultureInfo.InvariantCulture);
            try
            {
                string? name = _service.GetActiveTantName(code);
                lblTANCD.Text = name ?? string.Empty;
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "担当マスタ", code.ToString("0000", CultureInfo.InvariantCulture), ex);
            }
        }
        else
        {
            txtTANCD.Text = string.Empty;
            lblTANCD.Text = string.Empty;
        }

        txtTANCD.Focus();
    }

    private void txtTANCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTANCD.Text = string.Empty;
            lblTANCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtDAICD.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtTANCD_Leave(object? sender, EventArgs e)
    {
        int code = (int)ParseVal(txtTANCD.Text);
        if (code == 0)
        {
            txtTANCD.Text = string.Empty;
            lblTANCD.Text = string.Empty;
            return;
        }

        txtTANCD.Text = code.ToString("0000", CultureInfo.InvariantCulture);

        string? name;
        try
        {
            name = _service.GetActiveTantName(code);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "担当マスタ", txtTANCD.Text, ex);
            return;
        }

        if (name is null)
        {
            // VB6 KTBO0020.FRM line 2377 のオリジナルタイトル "KSHO0010" をそのまま保持
            MessageBox.Show("入力された営業担当ｺｰﾄﾞは存在しません。", "KSHO0010", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtTANCD.Text = string.Empty;
            lblTANCD.Text = string.Empty;
            txtTANCD.Focus();
            return;
        }

        lblTANCD.Text = name;
    }

    // =========================================================================
    // 大分類コード (txtDAICD)
    // =========================================================================

    private void txtDAICD_TextChanged(object? sender, EventArgs e)
    {
        txtTYUCD.Text = string.Empty;
        lblTYUCD.Text = string.Empty;
    }

    private void txtDAICD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtDAICD.Text = string.Empty;
            lblDAICD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtTYUCD.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtDAICD_Leave(object? sender, EventArgs e)
    {
        int code = (int)ParseVal(txtDAICD.Text);
        if (code == 0)
        {
            txtDAICD.Text = string.Empty;
            lblDAICD.Text = string.Empty;
            return;
        }

        txtDAICD.Text = code.ToString("00", CultureInfo.InvariantCulture);

        string? name;
        try
        {
            name = _service.GetActiveMajorCategoryName(code);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類マスタ", $"大分類コード:{code:00}中分類コード:00", ex);
            return;
        }

        if (name is null)
        {
            MessageBox.Show("入力された大分類ｺｰﾄﾞは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtDAICD.Text = string.Empty;
            lblDAICD.Text = string.Empty;
            txtDAICD.Focus();
            return;
        }

        lblDAICD.Text = name;
    }

    // =========================================================================
    // 中分類コード (txtTYUCD)
    // =========================================================================

    private void txtTYUCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTYUCD.Text = string.Empty;
            lblTYUCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtSYOCD.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtTYUCD_Leave(object? sender, EventArgs e)
    {
        int tyuCode = (int)ParseVal(txtTYUCD.Text);
        if (tyuCode == 0)
        {
            txtTYUCD.Text = string.Empty;
            lblTYUCD.Text = string.Empty;
            return;
        }

        txtTYUCD.Text = tyuCode.ToString("00", CultureInfo.InvariantCulture);
        int daiCode = (int)ParseVal(txtDAICD.Text);

        string? name;
        try
        {
            name = _service.GetActiveMinorCategoryName(daiCode, tyuCode);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類マスタ", $"大分類コード:{daiCode:00}中分類コード:{tyuCode:00}", ex);
            return;
        }

        if (name is null)
        {
            MessageBox.Show("入力された中分類ｺｰﾄﾞは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtTYUCD.Text = string.Empty;
            lblTYUCD.Text = string.Empty;
            txtTYUCD.Focus();
            return;
        }

        lblTYUCD.Text = name;
    }

    // =========================================================================
    // 商品コード (txtSYOCD / cmdSEEKSYO)
    // =========================================================================

    private void cmdSEEKSYO_Click(object? sender, EventArgs e)
    {
        using var dialog = new FrmSYOKEN(_service);
        dialog.ShowDialog(this);

        if (dialog.Selected)
        {
            long code = ParseVal(dialog.SelectedProductCode);
            txtSYOCD.Text = code.ToString("00000000", CultureInfo.InvariantCulture);
            try
            {
                string? name = _service.GetActiveProductKanaName(code);
                lblSYOCD.Text = name ?? string.Empty;
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品マスタ", code.ToString("00000000", CultureInfo.InvariantCulture), ex);
            }
        }
        else
        {
            txtSYOCD.Text = string.Empty;
            lblSYOCD.Text = string.Empty;
        }

        txtSYOCD.Focus();
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
            txtKNEN.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtSYOCD_Leave(object? sender, EventArgs e)
    {
        long code = ParseVal(txtSYOCD.Text);
        if (code == 0)
        {
            txtSYOCD.Text = string.Empty;
            lblSYOCD.Text = string.Empty;
            return;
        }

        txtSYOCD.Text = code.ToString("00000000", CultureInfo.InvariantCulture);

        string? name;
        try
        {
            name = _service.GetActiveProductKanaName(code);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品マスタ", txtSYOCD.Text, ex);
            return;
        }

        if (name is null)
        {
            MessageBox.Show("入力された商品ｺｰﾄﾞは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSYOCD.Text = string.Empty;
            lblSYOCD.Text = string.Empty;
            txtSYOCD.Focus();
            return;
        }

        lblSYOCD.Text = name;
    }

    // =========================================================================
    // 販売日付範囲 From (txtKNEN / txtKTUKI / txtKHI + SpinButtons)
    // =========================================================================

    private void txtKNEN_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtKNEN.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtKTUKI.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtKNEN_Leave(object? sender, EventArgs e)
    {
        long val = ParseVal(txtKNEN.Text);
        if (string.IsNullOrEmpty(txtKNEN.Text) || val == 0)
        {
            txtKNEN.Text = string.Empty;
            return;
        }

        txtKNEN.Text = val.ToString("0000", CultureInfo.InvariantCulture);
    }

    private void spnKYY_SpinUp(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKNEN.Text))
        {
            txtKNEN.Focus();
            return;
        }

        txtKNEN.Text = (ParseVal(txtKNEN.Text) + 1).ToString("0000", CultureInfo.InvariantCulture);
        txtKNEN.Focus();
    }

    private void spnKYY_SpinDown(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKNEN.Text))
        {
            txtKNEN.Focus();
            return;
        }

        txtKNEN.Text = (ParseVal(txtKNEN.Text) - 1).ToString("0000", CultureInfo.InvariantCulture);
        txtKNEN.Focus();
    }

    private void txtKTUKI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtKTUKI.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtKHI.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtKTUKI_Leave(object? sender, EventArgs e)
    {
        string strMonth = txtKTUKI.Text.Trim();
        string strDay = txtKHI.Text.Trim();

        long monthVal = ParseVal(strMonth);
        if (strMonth.Length == 0 || monthVal == 0)
        {
            txtKTUKI.Text = string.Empty;
            return;
        }

        if (monthVal < 1 || monthVal > 12)
        {
            MessageBox.Show("指定された月は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtKTUKI.Text = string.Empty;
            txtKTUKI.Focus();
            return;
        }

        txtKTUKI.Text = monthVal.ToString("00", CultureInfo.InvariantCulture);

        if (strDay.Length > 0 && !IsValidDayForMonth((int)monthVal, (int)ParseVal(strDay)))
        {
            MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtKHI.Text = string.Empty;
            txtKHI.Focus();
        }
    }

    private void spnKMM_SpinUp(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKTUKI.Text) || ParseVal(txtKTUKI.Text) == 12)
        {
            txtKTUKI.Focus();
            return;
        }

        txtKTUKI.Text = (ParseVal(txtKTUKI.Text) + 1).ToString("00", CultureInfo.InvariantCulture);
        txtKTUKI.Focus();
    }

    private void spnKMM_SpinDown(object? sender, EventArgs e)
    {
        if (ParseVal(txtKTUKI.Text) == 1)
        {
            txtKTUKI.Focus();
            return;
        }

        txtKTUKI.Text = (ParseVal(txtKTUKI.Text) - 1).ToString("00", CultureInfo.InvariantCulture);
        txtKTUKI.Focus();
    }

    private void txtKHI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtKHI.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtSNEN.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtKHI_Leave(object? sender, EventArgs e)
    {
        long dayVal = ParseVal(txtKHI.Text);
        if (string.IsNullOrWhiteSpace(txtKHI.Text) || dayVal == 0)
        {
            txtKHI.Text = string.Empty;
            return;
        }

        txtKHI.Text = dayVal.ToString("00", CultureInfo.InvariantCulture);

        if (dayVal < 1 || dayVal > 31 || !IsValidDayForMonth((int)ParseVal(txtKTUKI.Text), (int)dayVal))
        {
            MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtKHI.Text = string.Empty;
            txtKHI.Focus();
        }
    }

    private void spnKDD_SpinUp(object? sender, EventArgs e)
    {
        if (ParseVal(txtKHI.Text) == 31)
        {
            txtKHI.Focus();
            return;
        }

        txtKHI.Text = (ParseVal(txtKHI.Text) + 1).ToString("00", CultureInfo.InvariantCulture);
        txtKHI.Focus();
    }

    private void spnKDD_SpinDown(object? sender, EventArgs e)
    {
        if (ParseVal(txtKHI.Text) == 1)
        {
            txtKHI.Focus();
            return;
        }

        txtKHI.Text = (ParseVal(txtKHI.Text) - 1).ToString("00", CultureInfo.InvariantCulture);
        txtKHI.Focus();
    }

    // =========================================================================
    // 販売日付範囲 To (txtSNEN / txtSTUKI / txtSHI + SpinButtons)
    // =========================================================================

    private void txtSNEN_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSNEN.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtSTUKI.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtSNEN_Leave(object? sender, EventArgs e)
    {
        long val = ParseVal(txtSNEN.Text);
        if (string.IsNullOrEmpty(txtSNEN.Text) || val == 0)
        {
            txtSNEN.Text = string.Empty;
            return;
        }

        txtSNEN.Text = val.ToString("0000", CultureInfo.InvariantCulture);
    }

    private void spnSYY_SpinUp(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSNEN.Text))
        {
            txtSNEN.Focus();
            return;
        }

        txtSNEN.Text = (ParseVal(txtSNEN.Text) + 1).ToString("0000", CultureInfo.InvariantCulture);
        txtSNEN.Focus();
    }

    private void spnSYY_SpinDown(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSNEN.Text))
        {
            txtSNEN.Focus();
            return;
        }

        txtSNEN.Text = (ParseVal(txtSNEN.Text) - 1).ToString("0000", CultureInfo.InvariantCulture);
        txtSNEN.Focus();
    }

    private void txtSTUKI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSTUKI.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtSHI.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtSTUKI_Leave(object? sender, EventArgs e)
    {
        string strMonth = txtSTUKI.Text.Trim();
        string strDay = txtSHI.Text.Trim();

        long monthVal = ParseVal(strMonth);
        if (strMonth.Length == 0 || monthVal == 0)
        {
            txtSTUKI.Text = string.Empty;
            return;
        }

        if (monthVal < 1 || monthVal > 12)
        {
            MessageBox.Show("指定された月は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtSTUKI.Text = string.Empty;
            txtSTUKI.Focus();
            return;
        }

        txtSTUKI.Text = monthVal.ToString("00", CultureInfo.InvariantCulture);

        if (strDay.Length > 0 && !IsValidDayForMonth((int)monthVal, (int)ParseVal(strDay)))
        {
            MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSHI.Text = string.Empty;
            txtSHI.Focus();
        }
    }

    private void spnSMM_SpinUp(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSTUKI.Text) || ParseVal(txtSTUKI.Text) == 12)
        {
            txtSTUKI.Focus();
            return;
        }

        txtSTUKI.Text = (ParseVal(txtSTUKI.Text) + 1).ToString("00", CultureInfo.InvariantCulture);
        txtSTUKI.Focus();
    }

    private void spnSMM_SpinDown(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSTUKI.Text) || ParseVal(txtSTUKI.Text) == 1)
        {
            txtSTUKI.Focus();
            return;
        }

        txtSTUKI.Text = (ParseVal(txtSTUKI.Text) - 1).ToString("00", CultureInfo.InvariantCulture);
        txtSTUKI.Focus();
    }

    private void txtSHI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSHI.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            cmdSTART.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtSHI_Leave(object? sender, EventArgs e)
    {
        long dayVal = ParseVal(txtSHI.Text);
        if (string.IsNullOrWhiteSpace(txtSHI.Text) || dayVal == 0)
        {
            txtSHI.Text = string.Empty;
            return;
        }

        txtSHI.Text = dayVal.ToString("00", CultureInfo.InvariantCulture);

        if (dayVal < 1 || dayVal > 31 || !IsValidDayForMonth((int)ParseVal(txtSTUKI.Text), (int)dayVal))
        {
            MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSHI.Text = string.Empty;
            txtSHI.Focus();
        }
    }

    private void spnSDD_SpinUp(object? sender, EventArgs e)
    {
        if (ParseVal(txtSHI.Text) == 31)
        {
            txtSHI.Focus();
            return;
        }

        txtSHI.Text = (ParseVal(txtSHI.Text) + 1).ToString("00", CultureInfo.InvariantCulture);
        txtSHI.Focus();
    }

    private void spnSDD_SpinDown(object? sender, EventArgs e)
    {
        if (ParseVal(txtSHI.Text) == 1)
        {
            txtSHI.Focus();
            return;
        }

        txtSHI.Text = (ParseVal(txtSHI.Text) - 1).ToString("00", CultureInfo.InvariantCulture);
        txtSHI.Focus();
    }

    private static bool IsValidDayForMonth(int month, int day)
    {
        return month switch
        {
            1 or 3 or 5 or 7 or 8 or 10 or 12 => day is >= 1 and <= 31,
            4 or 6 or 9 or 11 => day is >= 1 and <= 30,
            2 => day is >= 1 and <= 29,
            _ => true
        };
    }

    // =========================================================================
    // 検索開始 (cmdSTART) / キャンセル (cmdCAN)
    // =========================================================================

    private void cmdSTART_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKNEN.Text))
        {
            MessageBox.Show("販売日付を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtKNEN.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtKTUKI.Text))
        {
            MessageBox.Show("販売日付を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtKTUKI.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtKHI.Text))
        {
            MessageBox.Show("販売日付を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtKHI.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtSNEN.Text))
        {
            MessageBox.Show("販売日付を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSNEN.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtSTUKI.Text))
        {
            MessageBox.Show("販売日付を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSTUKI.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtSHI.Text))
        {
            MessageBox.Show("販売日付を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSHI.Focus();
            return;
        }

        int fromYmd = (int)ParseVal($"{txtKNEN.Text.Trim()}{txtKTUKI.Text.Trim()}{txtKHI.Text.Trim()}");
        int toYmd = (int)ParseVal($"{txtSNEN.Text.Trim()}{txtSTUKI.Text.Trim()}{txtSHI.Text.Trim()}");

        if (fromYmd > toYmd)
        {
            MessageBox.Show("入力日の範囲指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtKNEN.Focus();
            return;
        }

        var criteria = new Ktbo0020SearchCriteriaDto
        {
            HbsCd = string.IsNullOrWhiteSpace(txtHANCD.Text) ? null : txtHANCD.Text.Trim(),
            StantCd = string.IsNullOrWhiteSpace(txtTANCD.Text) ? null : (int)ParseVal(txtTANCD.Text),
            DaiCd = string.IsNullOrWhiteSpace(txtDAICD.Text) ? null : (int)ParseVal(txtDAICD.Text),
            TyuCd = string.IsNullOrWhiteSpace(txtTYUCD.Text) ? null : (int)ParseVal(txtTYUCD.Text),
            ShnCd = string.IsNullOrWhiteSpace(txtSYOCD.Text) ? null : ParseVal(txtSYOCD.Text),
            FromYmd = fromYmd,
            ToYmd = toYmd
        };

        Ktbo0020SearchResultDto result;
        try
        {
            cmdSTART.Enabled = false;
            cmdCAN.Enabled = false;
            Cursor = Cursors.WaitCursor;

            result = _service.SearchTransactions(criteria);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "売上ジャーナル", "検索条件", ex);
            return;
        }
        finally
        {
            cmdSTART.Enabled = true;
            cmdCAN.Enabled = true;
            Cursor = Cursors.Default;
        }

        if (result.Rows.Count == 0)
        {
            MessageBox.Show("条件に該当する取引は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtHANCD.Focus();
            return;
        }

        if (result.TruncatedOver999)
        {
            MessageBox.Show(
                "９９９件より多く該当する取引がありました\r条件を絞り込んでください\r９９９件分のみ表示します",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }

        using var detailForm = new FrmKTBO0021(result);
        detailForm.ShowDialog(this);
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        Close();
    }
}

