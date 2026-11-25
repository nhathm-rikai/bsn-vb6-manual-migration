using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Uriage;

/// <summary>
/// KSHO0115 — 売上情報EXCEL出力 (VB6 frmKSHO0115 1:1 移行画面)
/// </summary>
public partial class FrmKSHO0115 : Form
{
    private const string ScreenTitle = "KSHO0115";

    private readonly IKsho0115Service _service;
    private bool _suppressFocusEvents;
    private bool _validationFailedInLeave;

    public FrmKSHO0115()
        : this(CreateDefaultService())
    {
    }

    public FrmKSHO0115(IKsho0115Service service)
    {
        _service = service;
        InitializeComponent();
    }

    private static IKsho0115Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Ksho0115Service(
            new Infrastructure.Repositories.VtUjnlRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmKyakRepository(factory));
    }

    // **********************************
    //  Form_Load
    // **********************************
    private void FrmKSHO0115_Load(object? sender, EventArgs e)
    {
        try
        {
            _service.CheckDatabaseConnection();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowConnectError(ScreenTitle, ex);
            Close();
            return;
        }

        DateTime now = DateTime.Now;

        // システム日付設定 (yyyy/mm/dd)
        lblYMD.Text = now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        // 画面初期値設定 (VB6 Form_Load 1:1)
        optALL.Checked = true;
        opt1.Checked = true;
        txtFROM.Text = string.Empty;
        txtTO.Text = string.Empty;
        txtFROM.Enabled = false;
        txtTO.Enabled = false;
        TxtTANTCD.Text = string.Empty;
        TxtTANTCD.Enabled = false;

        txtKNEN.Text = now.ToString("yyyy", CultureInfo.InvariantCulture);
        txtKTUKI.Text = now.ToString("MM", CultureInfo.InvariantCulture);
        txtKHI.Text = now.ToString("dd", CultureInfo.InvariantCulture);
        txtMNEN.Text = now.ToString("yyyy", CultureInfo.InvariantCulture);
        txtMTUKI.Text = now.ToString("MM", CultureInfo.InvariantCulture);
        txtMHI.Text = now.ToString("dd", CultureInfo.InvariantCulture);
    }

    private bool IsBypassValidationTarget()
    {
        return ActiveControl == cmdCAN;
    }

    private void RedirectFocus(Control target)
    {
        BeginInvoke(() =>
        {
            if (IsDisposed)
            {
                return;
            }

            bool prev = _suppressFocusEvents;
            _suppressFocusEvents = true;
            try
            {
                target.Focus();
            }
            finally
            {
                _suppressFocusEvents = prev;
                _validationFailedInLeave = false;
            }
        });
    }

    // **********************************
    //  KeyPress 共通フィルタ (数字 0..9 のみ)
    // **********************************
    private static void DigitsOnly_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    // **********************************
    //  ラジオボタンイベント (optALL / optHANI / OptTANT / opt1 / opt2)
    // **********************************
    private void ApplyOptAllSelection()
    {
        txtFROM.Text = string.Empty;
        txtTO.Text = string.Empty;
        txtFROM.Enabled = false;
        txtTO.Enabled = false;
        TxtTANTCD.Text = string.Empty;
        TxtTANTCD.Enabled = false;
        txtKNEN.Focus();
    }

    private void ApplyOptHaniSelection()
    {
        TxtTANTCD.Text = string.Empty;
        TxtTANTCD.Enabled = false;
        txtFROM.Enabled = true;
        txtTO.Enabled = true;
        txtFROM.Focus();
    }

    private void ApplyOptTantSelection()
    {
        txtFROM.Text = string.Empty;
        txtTO.Text = string.Empty;
        txtFROM.Enabled = false;
        txtTO.Enabled = false;
        TxtTANTCD.Enabled = true;
        TxtTANTCD.Focus();
    }

    private void optALL_Click(object? sender, EventArgs e)
    {
        if (!IsHandleCreated) return;
        ApplyOptAllSelection();
    }

    private void optALL_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            ApplyOptAllSelection();
        }
    }

    private void optHANI_Click(object? sender, EventArgs e)
    {
        if (!IsHandleCreated) return;
        ApplyOptHaniSelection();
    }

    private void OptHANI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            ApplyOptHaniSelection();
        }
    }

    private void optTANT_Click(object? sender, EventArgs e)
    {
        if (!IsHandleCreated) return;
        ApplyOptTantSelection();
    }

    private void OptTANT_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            ApplyOptTantSelection();
        }
    }

    private void opt1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            cmdSTR.Focus();
        }
    }

    private void opt2_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            cmdSTR.Focus();
        }
    }

    // **********************************
    //  txtFROM / txtTO / TxtTANTCD イベント
    // **********************************
    private void txtFROM_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtFROM.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtTO.Focus();
        }
    }

    private void txtFROM_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string trimmed = txtFROM.Text.Trim();
        txtFROM.Text = string.IsNullOrEmpty(trimmed)
            ? string.Empty
            : ((int)LegacyTextHelper.VbVal(trimmed)).ToString("00", CultureInfo.InvariantCulture);
    }

    private void txtTO_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTO.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtKNEN.Focus();
        }
    }

    private void txtTO_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string trimmed = txtTO.Text.Trim();
        txtTO.Text = string.IsNullOrEmpty(trimmed)
            ? string.Empty
            : ((int)LegacyTextHelper.VbVal(trimmed)).ToString("00", CultureInfo.InvariantCulture);
    }

    private void TxtTANTCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            TxtTANTCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtKNEN.Focus();
        }
    }

    private void TxtTANTCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string trimmed = TxtTANTCD.Text.Trim();
        TxtTANTCD.Text = string.IsNullOrEmpty(trimmed)
            ? string.Empty
            : ((int)LegacyTextHelper.VbVal(trimmed)).ToString("0000", CultureInfo.InvariantCulture);
    }

    // **********************************
    //  販売日範囲 (開始・終了) テキストボックスイベント
    // **********************************
    private void txtKNEN_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtKNEN.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtKTUKI.Focus();
        }
    }

    private void txtKNEN_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string trimmed = txtKNEN.Text.Trim();
        txtKNEN.Text = string.IsNullOrEmpty(trimmed)
            ? string.Empty
            : ((int)LegacyTextHelper.VbVal(trimmed)).ToString("0000", CultureInfo.InvariantCulture);
    }

    private void txtKTUKI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtKTUKI.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtKHI.Focus();
        }
    }

    private void txtKTUKI_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string strMm = txtKTUKI.Text.Trim();
        if (string.IsNullOrEmpty(strMm))
        {
            return;
        }

        int mm = (int)LegacyTextHelper.VbVal(strMm);
        if (mm < 1 || mm > 12)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("指定された月は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtKTUKI.Text = string.Empty;
            RedirectFocus(txtKTUKI);
            return;
        }

        txtKTUKI.Text = mm.ToString("00", CultureInfo.InvariantCulture);
    }

    private void txtKHI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtKHI.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtMNEN.Focus();
        }
    }

    private void txtKHI_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string strDd = txtKHI.Text.Trim();
        if (string.IsNullOrEmpty(strDd))
        {
            return;
        }

        int dd = (int)LegacyTextHelper.VbVal(strDd);
        if (dd < 1 || dd > 31)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("指定された日は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtKHI.Text = string.Empty;
            RedirectFocus(txtKHI);
            return;
        }

        txtKHI.Text = dd.ToString("00", CultureInfo.InvariantCulture);
    }

    private void txtMNEN_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtMNEN.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtMTUKI.Focus();
        }
    }

    private void txtMNEN_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string trimmed = txtMNEN.Text.Trim();
        txtMNEN.Text = string.IsNullOrEmpty(trimmed)
            ? string.Empty
            : ((int)LegacyTextHelper.VbVal(trimmed)).ToString("0000", CultureInfo.InvariantCulture);
    }

    private void txtMTUKI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtMTUKI.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtMHI.Focus();
        }
    }

    private void txtMTUKI_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string strMm = txtMTUKI.Text.Trim();
        if (string.IsNullOrEmpty(strMm))
        {
            return;
        }

        int mm = (int)LegacyTextHelper.VbVal(strMm);
        if (mm < 1 || mm > 12)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("指定された月は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtMTUKI.Text = string.Empty;
            RedirectFocus(txtMTUKI);
            return;
        }

        txtMTUKI.Text = mm.ToString("00", CultureInfo.InvariantCulture);
    }

    private void txtMHI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtMHI.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (opt1.Checked)
            {
                opt1.Focus();
            }
            else if (opt2.Checked)
            {
                opt2.Focus();
            }
        }
    }

    private void txtMHI_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        string strDd = txtMHI.Text.Trim();
        if (string.IsNullOrEmpty(strDd))
        {
            return;
        }

        int dd = (int)LegacyTextHelper.VbVal(strDd);
        if (dd < 1 || dd > 31)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("指定された日は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtMHI.Text = string.Empty;
            RedirectFocus(txtMHI);
            return;
        }

        txtMHI.Text = dd.ToString("00", CultureInfo.InvariantCulture);
    }

    // **********************************
    //  スピンボタンイベント (SPIN32.ocx 互換)
    // **********************************
    private void spnKNEN_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtKNEN.Text.Trim());
        if (val != 0)
        {
            txtKNEN.Text = (val - 1).ToString("0000", CultureInfo.InvariantCulture);
            txtKNEN.Refresh();
        }
        txtKNEN.Focus();
    }

    private void spnKNEN_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtKNEN.Text.Trim();
        if (!string.IsNullOrEmpty(trimmed))
        {
            int val = (int)LegacyTextHelper.VbVal(trimmed);
            txtKNEN.Text = (val + 1).ToString("0000", CultureInfo.InvariantCulture);
            txtKNEN.Refresh();
        }
        txtKNEN.Focus();
    }

    private void spnKTUKI_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtKTUKI.Text.Trim());
        if (val != 1 && val > 1)
        {
            txtKTUKI.Text = (val - 1).ToString("00", CultureInfo.InvariantCulture);
            txtKTUKI.Refresh();
        }
        txtKTUKI.Focus();
    }

    private void spnKTUKI_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtKTUKI.Text.Trim();
        int val = (int)LegacyTextHelper.VbVal(trimmed);
        if (!string.IsNullOrEmpty(trimmed) && val < 12)
        {
            txtKTUKI.Text = (val + 1).ToString("00", CultureInfo.InvariantCulture);
            txtKTUKI.Refresh();
        }
        txtKTUKI.Focus();
    }

    private void spnKHI_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtKHI.Text.Trim());
        if (val != 1 && val > 1)
        {
            txtKHI.Text = (val - 1).ToString("00", CultureInfo.InvariantCulture);
            txtKHI.Refresh();
        }
        txtKHI.Focus();
    }

    private void spnKHI_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtKHI.Text.Trim();
        int val = (int)LegacyTextHelper.VbVal(trimmed);
        if (!string.IsNullOrEmpty(trimmed) && val < 31)
        {
            txtKHI.Text = (val + 1).ToString("00", CultureInfo.InvariantCulture);
            txtKHI.Refresh();
        }
        txtKHI.Focus();
    }

    private void spnMNEN_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtMNEN.Text.Trim());
        if (val != 0)
        {
            txtMNEN.Text = (val - 1).ToString("0000", CultureInfo.InvariantCulture);
            txtMNEN.Refresh();
        }
        txtMNEN.Focus();
    }

    private void spnMNEN_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtMNEN.Text.Trim();
        if (!string.IsNullOrEmpty(trimmed))
        {
            int val = (int)LegacyTextHelper.VbVal(trimmed);
            txtMNEN.Text = (val + 1).ToString("0000", CultureInfo.InvariantCulture);
            txtMNEN.Refresh();
        }
        txtMNEN.Focus();
    }

    private void spnMTUKI_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtMTUKI.Text.Trim());
        if (val != 1 && val > 1)
        {
            txtMTUKI.Text = (val - 1).ToString("00", CultureInfo.InvariantCulture);
            txtMTUKI.Refresh();
        }
        txtMTUKI.Focus();
    }

    private void spnMTUKI_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtMTUKI.Text.Trim();
        int val = (int)LegacyTextHelper.VbVal(trimmed);
        if (!string.IsNullOrEmpty(trimmed) && val < 12)
        {
            txtMTUKI.Text = (val + 1).ToString("00", CultureInfo.InvariantCulture);
            txtMTUKI.Refresh();
        }
        txtMTUKI.Focus();
    }

    private void spnMHI_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtMHI.Text.Trim());
        if (val != 1 && val > 1)
        {
            txtMHI.Text = (val - 1).ToString("00", CultureInfo.InvariantCulture);
            txtMHI.Refresh();
        }
        txtMHI.Focus();
    }

    private void spnMHI_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtMHI.Text.Trim();
        int val = (int)LegacyTextHelper.VbVal(trimmed);
        if (!string.IsNullOrEmpty(trimmed) && val < 31)
        {
            txtMHI.Text = (val + 1).ToString("00", CultureInfo.InvariantCulture);
            txtMHI.Refresh();
        }
        txtMHI.Focus();
    }

    // ******************************************
    //  出力開始ボタンクリック (cmdSTR_Click)
    // ******************************************
    private void cmdSTR_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave) return;

        int haniMode = optALL.Checked ? 1 : (optHANI.Checked ? 2 : 3);
        int outputKubun = opt1.Checked ? 1 : 2;

        var request = new Ksho0115ExportRequest
        {
            KNenText = txtKNEN.Text,
            KTukiText = txtKTUKI.Text,
            KHiText = txtKHI.Text,
            MNenText = txtMNEN.Text,
            MTukiText = txtMTUKI.Text,
            MHiText = txtMHI.Text,
            HaniMode = haniMode,
            FromDaiCdText = txtFROM.Text,
            ToDaiCdText = txtTO.Text,
            TantCdText = TxtTANTCD.Text,
            OutputKubun = outputKubun
        };

        Cursor = Cursors.WaitCursor;
        cmdCAN.Enabled = false;
        cmdSTR.Enabled = false;
        fraHANI.Enabled = false;
        fraKEISIKI.Enabled = false;

        try
        {
            Ksho0115ExportResult result = _service.ExecuteExport(request);

            Cursor = Cursors.Default;
            cmdCAN.Enabled = true;
            cmdSTR.Enabled = true;
            fraHANI.Enabled = true;
            fraKEISIKI.Enabled = true;

            if (!result.Success)
            {
                MessageBox.Show(
                    result.Message,
                    ScreenTitle,
                    MessageBoxButtons.OK,
                    result.IsExclamation ? MessageBoxIcon.Exclamation : MessageBoxIcon.Information);

                FocusField(result.ErrorField);
                return;
            }

            MessageBox.Show(result.Message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (optALL.Checked)
            {
                optALL.Focus();
            }
            else
            {
                optHANI.Focus();
            }
        }
        catch (Exception ex)
        {
            Cursor = Cursors.Default;
            cmdCAN.Enabled = true;
            cmdSTR.Enabled = true;
            fraHANI.Enabled = true;
            fraKEISIKI.Enabled = true;

            string strDate1 = txtKNEN.Text + txtKTUKI.Text + txtKHI.Text;
            string strDate2 = txtMNEN.Text + txtMTUKI.Text + txtMHI.Text;
            LegacyErrorDialog.ShowSqlError(
                ScreenTitle,
                "売上ｼﾞｬｰﾅﾙ",
                $"{strDate1},{strDate2},{haniMode},{txtFROM.Text},{txtTO.Text}範囲検索",
                ex);
            Close();
        }
    }

    // **********************************
    //  キャンセル (cmdCAN_Click)
    // **********************************
    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        Close();
    }

    private void FocusField(Ksho0115Field field)
    {
        switch (field)
        {
            case Ksho0115Field.KNen:
                txtKNEN.Focus();
                break;
            case Ksho0115Field.KTuki:
                txtKTUKI.Focus();
                break;
            case Ksho0115Field.KHi:
                txtKHI.Focus();
                break;
            case Ksho0115Field.MNen:
                txtMNEN.Focus();
                break;
            case Ksho0115Field.MTuki:
                txtMTUKI.Focus();
                break;
            case Ksho0115Field.MHi:
                txtMHI.Focus();
                break;
            case Ksho0115Field.FromDaiCd:
                txtFROM.Focus();
                break;
            case Ksho0115Field.ToDaiCd:
                txtTO.Focus();
                break;
            case Ksho0115Field.TantCd:
                TxtTANTCD.Focus();
                break;
        }
    }
}

