using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;
using KousyoERP.Forms.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0041 — 請求支払条件登録 (VB6 frmKUNO0041 1:1 移行画面)
/// </summary>
public partial class FrmKUNO0041 : Form
{
    private const string ScreenTitle = "KUNO0040";

    private readonly IKuno0040Service _service;
    private readonly Kuno0040State _state;
    private bool _isHandlingLeave;

    public FrmKUNO0041()
        : this(CreateDefaultService(), new Kuno0040State())
    {
    }

    public FrmKUNO0041(IKuno0040Service service, Kuno0040State state)
    {
        _service = service;
        _state = state;
        InitializeComponent();
    }

    private static IKuno0040Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0040Service(
            new Infrastructure.Repositories.KmKyakRepository(factory),
            new Infrastructure.Repositories.KmGrpfRepository(factory),
            new Infrastructure.Repositories.KmSyzkRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory),
            new Infrastructure.Repositories.KmGnkoRepository(factory));
    }

    private RadioButton[] GetShrHohoOptions() =>
        new[] { optSHRHOHO0, optSHRHOHO1, optSHRHOHO2, optSHRHOHO3, optSHRHOHO4, optSHRHOHO5 };

    private int GetSelectedShrHohoIndex()
    {
        RadioButton[] opts = GetShrHohoOptions();
        for (int i = 0; i < opts.Length; i++)
        {
            if (opts[i].Checked) return i;
        }
        return 0;
    }

    private void SelectShrHoho(int index)
    {
        RadioButton[] opts = GetShrHohoOptions();
        int safeIdx = (index >= 0 && index < opts.Length) ? index : 0;
        opts[safeIdx].Checked = true;
        OnShrHohoChanged(safeIdx);
    }

    private void OnShrHohoChanged(int index)
    {
        frmTEGATA.Enabled = index is 0 or 2 or 3;
    }

    private void FrmKUNO0041_Load(object? sender, EventArgs e)
    {
        _state.CanFlg2 = 0;
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        lblKCD.Text = _state.Kokyaku;
        txtSKCD.Text = _state.Seikyu;
        txtSSCD.Text = _state.Siharai;
        lblKNM.Text = _state.SnmKokyaku;
        lblSKCD.Text = _state.SnmSeikyu;
        lblSSCD.Text = _state.SnmSiharai;

        if (_state.UpFlg40 == 0)
        {
            AllClear();
            txtSKCD.Text = _state.Seikyu;
            txtSSCD.Text = _state.Siharai;
            lblSKCD.Text = _state.SnmSeikyu;
            lblSSCD.Text = _state.SnmSiharai;

            if (string.IsNullOrEmpty(txtSKCD.Text.Trim()))
            {
                optNO.Checked = true;
                optPOFF.Checked = true;
                optPNO.Checked = true;
            }
            if (string.IsNullOrEmpty(txtSSCD.Text.Trim()))
            {
                optNO2.Checked = true;
            }
            if (_state.SihFlg == 1)
            {
                txtS1.Text = _state.Siha.ToString("00", CultureInfo.InvariantCulture);
            }
        }
        else
        {
            OnelistSet();
            if (string.IsNullOrEmpty(txtSKCD.Text.Trim()))
            {
                optNO.Checked = true;
                optPOFF.Checked = true;
                optPNO.Checked = true;
            }
            if (string.IsNullOrEmpty(txtSSCD.Text.Trim()))
            {
                optNO2.Checked = true;
            }
        }
    }

    private void AllClear()
    {
        txtSKCD.Text = string.Empty;
        lblSKCD.Text = string.Empty;
        txtS1.Text = string.Empty;
        txtS2.Text = string.Empty;
        txtS3.Text = string.Empty;
        txtANY1.Text = string.Empty;
        txtANY2.Text = string.Empty;
        txtANY3.Text = string.Empty;
        txtNY1.Text = string.Empty;
        txtNY2.Text = string.Empty;
        txtNY3.Text = string.Empty;
        optUPDN.Checked = true;
        optPON.Checked = true;
        optMK.Checked = true;

        txtSSCD.Text = string.Empty;
        lblSSCD.Text = string.Empty;
        txtSS1.Text = string.Empty;
        txtSS2.Text = string.Empty;
        txtSS3.Text = string.Empty;
        txtASNY1.Text = string.Empty;
        txtASNY2.Text = string.Empty;
        txtASNY3.Text = string.Empty;
        txtSSNY1.Text = string.Empty;
        txtSSNY2.Text = string.Empty;
        txtSSNY3.Text = string.Empty;
        optUPDN2.Checked = true;
        SelectShrHoho(0);
        txtSITE.Text = string.Empty;
        txtKSMM.Text = string.Empty;
        txtKSDD.Text = string.Empty;

        _state.ResetBillingPaymentFields();
    }

    private void OnelistSet()
    {
        txtS1.Text = _state.F230 != 0 ? _state.F230.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtANY1.Text = _state.F230 != 0 ? _state.F240.ToString("0", CultureInfo.InvariantCulture) : string.Empty;
        txtNY1.Text = _state.F250 != 0 ? _state.F250.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtS2.Text = _state.F260 != 0 ? _state.F260.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtANY2.Text = _state.F260 != 0 ? _state.F270.ToString("0", CultureInfo.InvariantCulture) : string.Empty;
        txtNY2.Text = _state.F280 != 0 ? _state.F280.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtS3.Text = _state.F290 != 0 ? _state.F290.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtANY3.Text = _state.F290 != 0 ? _state.F300.ToString("0", CultureInfo.InvariantCulture) : string.Empty;
        txtNY3.Text = _state.F310 != 0 ? _state.F310.ToString("00", CultureInfo.InvariantCulture) : string.Empty;

        switch (_state.F320)
        {
            case 0: optUPDN.Checked = true; break;
            case 1: optDEL.Checked = true; break;
            case 2: optUP.Checked = true; break;
            case 3: optNO.Checked = true; break;
        }

        switch (_state.F330)
        {
            case 0: optPOFF.Checked = true; break;
            case 1: optPON.Checked = true; break;
        }

        switch (_state.F340)
        {
            case 0: optPNO.Checked = true; break;
            case 1: optMEI.Checked = true; break;
            case 2: optKAN.Checked = true; break;
            case 3: optMK.Checked = true; break;
        }

        txtSS1.Text = _state.F350 != 0 ? _state.F350.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtASNY1.Text = _state.F350 != 0 ? _state.F360.ToString("0", CultureInfo.InvariantCulture) : string.Empty;
        txtSSNY1.Text = _state.F370 != 0 ? _state.F370.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtSS2.Text = _state.F380 != 0 ? _state.F380.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtASNY2.Text = _state.F380 != 0 ? _state.F390.ToString("0", CultureInfo.InvariantCulture) : string.Empty;
        txtSSNY2.Text = _state.F400 != 0 ? _state.F400.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtSS3.Text = _state.F410 != 0 ? _state.F410.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtASNY3.Text = _state.F410 != 0 ? _state.F420.ToString("0", CultureInfo.InvariantCulture) : string.Empty;
        txtSSNY3.Text = _state.F430 != 0 ? _state.F430.ToString("00", CultureInfo.InvariantCulture) : string.Empty;

        switch (_state.F440)
        {
            case 0: optUPDN2.Checked = true; break;
            case 1: optDEL2.Checked = true; break;
            case 2: optUP2.Checked = true; break;
            case 3: optNO2.Checked = true; break;
        }

        SelectShrHoho(_state.F800);
        txtSITE.Text = _state.F810 != 0 ? _state.F810.ToString(CultureInfo.InvariantCulture) : string.Empty;
        txtKSMM.Text = _state.F820 != 0 ? _state.F820.ToString(CultureInfo.InvariantCulture) : string.Empty;
        txtKSDD.Text = _state.F830 != 0 ? _state.F830.ToString(CultureInfo.InvariantCulture) : string.Empty;
    }

    private void cmdKYKKEN2_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmKYKKEN(_service);
        if (frm.ShowDialog(this) == DialogResult.OK && frm.IsSelected)
        {
            txtSKCD.Text = frm.SelectedCustomerCode;
            lblSKCD.Text = frm.SelectedCustomerShortName;
        }
        else
        {
            txtSKCD.Text = string.Empty;
            lblSKCD.Text = string.Empty;
        }
        txtSKCD.Focus();
    }

    private void cmdKYKKEN3_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmKYKKEN(_service);
        if (frm.ShowDialog(this) == DialogResult.OK && frm.IsSelected)
        {
            txtSSCD.Text = frm.SelectedCustomerCode;
            lblSSCD.Text = frm.SelectedCustomerShortName;
        }
        else
        {
            txtSSCD.Text = string.Empty;
            lblSSCD.Text = string.Empty;
        }
        txtSSCD.Focus();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        _state.CanFlg2 = 1;
        _state.SetFlg2 = 0;
        AllClear();

        _state.Seikyu = txtSKCD.Text;
        _state.Siharai = txtSSCD.Text;
        _state.SnmSeikyu = lblSKCD.Text;
        _state.SnmSiharai = lblSSCD.Text;

        if (string.IsNullOrEmpty(txtSKCD.Text.Trim()))
        {
            optNO.Checked = true;
            optPOFF.Checked = true;
            optPNO.Checked = true;
        }
        if (string.IsNullOrEmpty(txtSSCD.Text.Trim()))
        {
            optNO2.Checked = true;
        }

        Close();
    }

    private void cmdSET_Click(object? sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtSKCD.Text.Trim()) && string.IsNullOrEmpty(lblSKCD.Text.Trim()))
        {
            txtSKCD.Text = string.Empty;
            lblSKCD.Text = string.Empty;
            MessageBox.Show("指定された請求先コードは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSKCD.Focus();
            return;
        }

        if (!string.IsNullOrEmpty(txtSSCD.Text.Trim()) && string.IsNullOrEmpty(lblSSCD.Text.Trim()))
        {
            txtSSCD.Text = string.Empty;
            lblSSCD.Text = string.Empty;
            MessageBox.Show("指定された支払先コードは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSSCD.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtSKCD.Text.Trim()) && string.IsNullOrEmpty(txtSSCD.Text.Trim()))
        {
            txtSKCD.Text = string.Empty;
            lblSKCD.Text = string.Empty;
            txtSSCD.Text = string.Empty;
            lblSSCD.Text = string.Empty;
            ShowRequiredError("請求先コード・支払先コード", txtSKCD);
            return;
        }

        if (!string.IsNullOrEmpty(txtSKCD.Text.Trim()))
        {
            if (string.IsNullOrEmpty(txtS1.Text.Trim())) { ShowRequiredError("請求締日１", txtS1); return; }
            if (string.IsNullOrEmpty(txtANY1.Text.Trim())) { ShowRequiredError("入金予定日１", txtANY1); return; }
            if (string.IsNullOrEmpty(txtNY1.Text.Trim())) { ShowRequiredError("入金予定日１", txtNY1); return; }
        }

        if (!string.IsNullOrEmpty(txtSSCD.Text.Trim()))
        {
            if (string.IsNullOrEmpty(txtSS1.Text.Trim())) { ShowRequiredError("支払締日１", txtSS1); return; }
            if (string.IsNullOrEmpty(txtASNY1.Text.Trim())) { ShowRequiredError("支払予定日１", txtASNY1); return; }
            if (string.IsNullOrEmpty(txtSSNY1.Text.Trim())) { ShowRequiredError("支払予定日１", txtSSNY1); return; }
        }

        // 請求締日チェック
        if (!IsContiguousFilled(txtS1.Text, txtS2.Text, txtS3.Text))
        {
            ShowDateError(txtS1);
            return;
        }

        // 入金月日チェック
        if (!IsContiguousFilled(txtANY1.Text, txtANY2.Text, txtANY3.Text))
        {
            ShowDateError(txtANY1);
            return;
        }

        if (!string.IsNullOrEmpty(txtANY1.Text.Trim()) && string.IsNullOrEmpty(txtNY1.Text.Trim())) { ShowDateError(txtNY1); return; }
        if (!string.IsNullOrEmpty(txtANY2.Text.Trim()) && string.IsNullOrEmpty(txtNY2.Text.Trim())) { ShowDateError(txtNY2); return; }
        if (!string.IsNullOrEmpty(txtANY3.Text.Trim()) && string.IsNullOrEmpty(txtNY3.Text.Trim())) { ShowDateError(txtNY3); return; }
        if (string.IsNullOrEmpty(txtANY1.Text.Trim()) && !string.IsNullOrEmpty(txtNY1.Text.Trim())) { ShowDateError(txtANY1); return; }
        if (string.IsNullOrEmpty(txtANY2.Text.Trim()) && !string.IsNullOrEmpty(txtNY2.Text.Trim())) { ShowDateError(txtANY2); return; }
        if (string.IsNullOrEmpty(txtANY3.Text.Trim()) && !string.IsNullOrEmpty(txtNY3.Text.Trim())) { ShowDateError(txtANY3); return; }

        // 請求書・帳票印刷区分チェック
        if (optPON.Checked && optPNO.Checked)
        {
            MessageBox.Show("指定された請求書区分・帳票区分の組合わせは正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            optPON.Focus();
            return;
        }

        if (optPOFF.Checked && (optMEI.Checked || optKAN.Checked || optMK.Checked))
        {
            MessageBox.Show("指定された請求書区分・帳票区分の組合わせは正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            optPOFF.Focus();
            return;
        }

        // 支払締日チェック
        if (!IsContiguousFilled(txtSS1.Text, txtSS2.Text, txtSS3.Text))
        {
            ShowDateError(txtSS1);
            return;
        }

        // 支払月日チェック
        if (!IsContiguousFilled(txtASNY1.Text, txtASNY2.Text, txtASNY3.Text))
        {
            ShowDateError(txtASNY1);
            return;
        }

        if (!string.IsNullOrEmpty(txtASNY1.Text.Trim()) && string.IsNullOrEmpty(txtSSNY1.Text.Trim())) { ShowDateError(txtSSNY1); return; }
        if (!string.IsNullOrEmpty(txtASNY2.Text.Trim()) && string.IsNullOrEmpty(txtSSNY2.Text.Trim())) { ShowDateError(txtSSNY2); return; }
        if (!string.IsNullOrEmpty(txtASNY3.Text.Trim()) && string.IsNullOrEmpty(txtSSNY3.Text.Trim())) { ShowDateError(txtSSNY3); return; }
        if (string.IsNullOrEmpty(txtASNY1.Text.Trim()) && !string.IsNullOrEmpty(txtSSNY1.Text.Trim())) { ShowDateError(txtASNY1, "KUNO0100"); return; }
        if (string.IsNullOrEmpty(txtASNY2.Text.Trim()) && !string.IsNullOrEmpty(txtSSNY2.Text.Trim())) { ShowDateError(txtASNY2, "KUNO0100"); return; }
        if (string.IsNullOrEmpty(txtASNY3.Text.Trim()) && !string.IsNullOrEmpty(txtSSNY3.Text.Trim())) { ShowDateError(txtASNY3, "KUNO0100"); return; }

        // 支払方法チェック
        int shrIdx = GetSelectedShrHohoIndex();
        if (!string.IsNullOrEmpty(txtSSCD.Text.Trim()) && shrIdx is 0 or 2 or 3)
        {
            int siteVal = (int)LegacyTextHelper.VbVal(txtSITE.Text);
            int ksMmVal = (int)LegacyTextHelper.VbVal(txtKSMM.Text);
            int ksDdVal = (int)LegacyTextHelper.VbVal(txtKSDD.Text);

            if (siteVal == 0 && ksDdVal == 0)
            {
                MessageBox.Show("手形支払のサイトまたは決済日のどちらかを指定してください。", ScreenTitle);
                txtSITE.Focus();
                return;
            }

            if (siteVal != 0 && (ksMmVal != 0 || ksDdVal != 0))
            {
                MessageBox.Show("手形支払はサイトまたは決済日のどちらか一つのみ指定してください。", ScreenTitle);
                txtSITE.Focus();
                return;
            }
        }

        // 共有ステートへ反映
        _state.Seikyu = txtSKCD.Text;
        _state.Siharai = txtSSCD.Text;
        _state.SnmSeikyu = lblSKCD.Text;
        _state.SnmSiharai = lblSSCD.Text;

        _state.F230 = (int)LegacyTextHelper.VbVal(txtS1.Text);
        _state.F240 = (int)LegacyTextHelper.VbVal(txtANY1.Text);
        _state.F250 = (int)LegacyTextHelper.VbVal(txtNY1.Text);
        _state.F260 = (int)LegacyTextHelper.VbVal(txtS2.Text);
        _state.F270 = (int)LegacyTextHelper.VbVal(txtANY2.Text);
        _state.F280 = (int)LegacyTextHelper.VbVal(txtNY2.Text);
        _state.F290 = (int)LegacyTextHelper.VbVal(txtS3.Text);
        _state.F300 = (int)LegacyTextHelper.VbVal(txtANY3.Text);
        _state.F310 = (int)LegacyTextHelper.VbVal(txtNY3.Text);

        if (optUPDN.Checked) _state.F320 = 0;
        if (optDEL.Checked) _state.F320 = 1;
        if (optUP.Checked) _state.F320 = 2;
        if (optNO.Checked) _state.F320 = 3;

        if (optPOFF.Checked) _state.F330 = 0;
        if (optPON.Checked) _state.F330 = 1;

        if (optPNO.Checked) _state.F340 = 0;
        if (optMEI.Checked) _state.F340 = 1;
        if (optKAN.Checked) _state.F340 = 2;
        if (optMK.Checked) _state.F340 = 3;

        _state.F350 = (int)LegacyTextHelper.VbVal(txtSS1.Text);
        _state.F360 = (int)LegacyTextHelper.VbVal(txtASNY1.Text);
        _state.F370 = (int)LegacyTextHelper.VbVal(txtSSNY1.Text);
        _state.F380 = (int)LegacyTextHelper.VbVal(txtSS2.Text);
        _state.F390 = (int)LegacyTextHelper.VbVal(txtASNY2.Text);
        _state.F400 = (int)LegacyTextHelper.VbVal(txtSSNY2.Text);
        _state.F410 = (int)LegacyTextHelper.VbVal(txtSS3.Text);
        _state.F420 = (int)LegacyTextHelper.VbVal(txtASNY3.Text);
        _state.F430 = (int)LegacyTextHelper.VbVal(txtSSNY3.Text);

        if (optUPDN2.Checked) _state.F440 = 0;
        if (optDEL2.Checked) _state.F440 = 1;
        if (optUP2.Checked) _state.F440 = 2;
        if (optNO2.Checked) _state.F440 = 3;

        if (string.IsNullOrEmpty(txtSSCD.Text.Trim()))
        {
            _state.F800 = 0;
            _state.F810 = 0;
            _state.F820 = 0;
            _state.F830 = 0;
        }
        else
        {
            _state.F800 = shrIdx;
            if (shrIdx is 0 or 2 or 3)
            {
                _state.F810 = (int)LegacyTextHelper.VbVal(txtSITE.Text);
                _state.F820 = (int)LegacyTextHelper.VbVal(txtKSMM.Text);
                _state.F830 = (int)LegacyTextHelper.VbVal(txtKSDD.Text);
            }
            else
            {
                _state.F810 = 0;
                _state.F820 = 0;
                _state.F830 = 0;
            }
        }

        _state.SetFlg2 = 1;
        Close();
    }

    private static bool IsContiguousFilled(string s1, string s2, string s3)
    {
        bool b1 = !string.IsNullOrEmpty(s1.Trim());
        bool b2 = !string.IsNullOrEmpty(s2.Trim());
        bool b3 = !string.IsNullOrEmpty(s3.Trim());
        return (!b1 && !b2 && !b3) || (b1 && !b2 && !b3) || (b1 && b2 && !b3) || (b1 && b2 && b3);
    }

    private static void ShowRequiredError(string itemName, Control target)
    {
        MessageBox.Show($"{itemName}を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        target.Focus();
    }

    private static void ShowDateError(Control target, string title = ScreenTitle)
    {
        MessageBox.Show("指定された日付は正しくありません。", title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        target.Focus();
    }

    // --- SpinButton 共通 ---
    private static void SpinDay(TextBox txt, int delta)
    {
        string trimmed = txt.Text.Trim();
        if (string.IsNullOrEmpty(trimmed))
        {
            txt.Focus();
            return;
        }

        int val = (int)LegacyTextHelper.VbVal(trimmed);
        if ((delta < 0 && val == 1) || (delta > 0 && val == 31))
        {
            txt.Focus();
            return;
        }

        txt.Text = (val + delta).ToString("00", CultureInfo.InvariantCulture);
        txt.Refresh();
        txt.Focus();
    }

    // --- 請求先・支払先コード ---
    private void txtSKCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; txtSKCD.Text = string.Empty; lblSKCD.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; txtS1.Focus(); }
    }

    private void txtSKCD_Leave(object? sender, EventArgs e)
    {
        HandleCustomerRefLeave(txtSKCD, lblSKCD, "指定された請求先コードは存在しません。");
    }

    private void txtSSCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; txtSSCD.Text = string.Empty; lblSSCD.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; txtSS1.Focus(); }
    }

    private void txtSSCD_Leave(object? sender, EventArgs e)
    {
        HandleCustomerRefLeave(txtSSCD, lblSSCD, "指定された支払先コードは存在しません。");
    }

    private void HandleCustomerRefLeave(TextBox txt, Label lbl, string notFoundMsg)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            txt.Text = txt.Text.Trim();
            long val = (long)LegacyTextHelper.VbVal(txt.Text);
            if (string.IsNullOrEmpty(txt.Text) || val == 0)
            {
                txt.Text = string.Empty;
                lbl.Text = string.Empty;
                return;
            }

            string scd = val.ToString("0000000000", CultureInfo.InvariantCulture);
            txt.Text = scd;

            if (_state.UpFlg40 == 0 && scd == lblKCD.Text.Trim())
            {
                lbl.Text = lblKNM.Text;
                return;
            }

            string? snm;
            try
            {
                snm = _service.GetActiveCustomerShortName(scd);
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "顧客マスタ", scd, ex);
                return;
            }

            if (snm is null)
            {
                txt.Text = string.Empty;
                lbl.Text = string.Empty;
                MessageBox.Show(notFoundMsg, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txt.Focus());
                return;
            }

            lbl.Text = snm;
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    // --- 請求締日 1..3 ---
    private void txtS1_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtS1, txtANY1, e);
    private void txtS2_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtS2, txtANY2, e);
    private void txtS3_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtS3, txtANY3, e);
    private void txtS1_Leave(object? sender, EventArgs e) => ValidateClosingDay(1, txtS1, txtS2, txtS3);
    private void txtS2_Leave(object? sender, EventArgs e) => ValidateClosingDay(2, txtS1, txtS2, txtS3);
    private void txtS3_Leave(object? sender, EventArgs e) => ValidateClosingDay(3, txtS1, txtS2, txtS3);

    // --- 支払締日 1..3 ---
    private void txtSS1_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtSS1, txtASNY1, e);
    private void txtSS2_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtSS2, txtASNY2, e);
    private void txtSS3_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtSS3, txtASNY3, e);
    private void txtSS1_Leave(object? sender, EventArgs e) => ValidateClosingDay(1, txtSS1, txtSS2, txtSS3);
    private void txtSS2_Leave(object? sender, EventArgs e) => ValidateClosingDay(2, txtSS1, txtSS2, txtSS3);
    private void txtSS3_Leave(object? sender, EventArgs e) => ValidateClosingDay(3, txtSS1, txtSS2, txtSS3);

    private void ValidateClosingDay(int slot, TextBox t1, TextBox t2, TextBox t3)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            TextBox cur = slot == 1 ? t1 : slot == 2 ? t2 : t3;
            cur.Text = cur.Text.Trim();
            int curVal = (int)LegacyTextHelper.VbVal(cur.Text);
            if (string.IsNullOrEmpty(cur.Text) || curVal == 0)
            {
                cur.Text = string.Empty;
                return;
            }

            cur.Text = curVal.ToString("00", CultureInfo.InvariantCulture);
            if (curVal < 1 || curVal > 31)
            {
                FailDateLeave(cur);
                return;
            }

            int v1 = (int)LegacyTextHelper.VbVal(t1.Text);
            int v2 = (int)LegacyTextHelper.VbVal(t2.Text);
            int v3 = (int)LegacyTextHelper.VbVal(t3.Text);

            if (slot == 1)
            {
                if (v2 == 0 && v3 == 0) { t2.Text = string.Empty; t3.Text = string.Empty; return; }
                if (v3 == 0)
                {
                    t3.Text = string.Empty;
                    if (v1 >= v2) FailDateLeave(t1);
                    return;
                }
                if (!(v1 < v2 && v2 < v3)) FailDateLeave(t1);
            }
            else if (slot == 2)
            {
                if (v1 == 0) { t1.Text = string.Empty; FailDateLeave(t2); return; }
                if (v3 == 0)
                {
                    t3.Text = string.Empty;
                    if (v1 >= v2) FailDateLeave(t2);
                    return;
                }
                if (!(v1 < v2 && v2 < v3)) FailDateLeave(t2);
            }
            else
            {
                if (v1 == 0 || v2 == 0 || !(v1 < v2 && v2 < v3)) FailDateLeave(t3);
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    // --- 入金月度 1..3 / 支払月度 1..3 ---
    private void txtANY1_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtANY1, txtNY1, e);
    private void txtANY2_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtANY2, txtNY2, e);
    private void txtANY3_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtANY3, txtNY3, e);
    private void txtANY1_Leave(object? sender, EventArgs e) => ValidateMonthOffset(1, txtANY1, txtANY2, txtANY3);
    private void txtANY2_Leave(object? sender, EventArgs e) => ValidateMonthOffset(2, txtANY1, txtANY2, txtANY3);
    private void txtANY3_Leave(object? sender, EventArgs e) => ValidateMonthOffset(3, txtANY1, txtANY2, txtANY3);

    private void txtASNY1_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtASNY1, txtSSNY1, e);
    private void txtASNY2_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtASNY2, txtSSNY2, e);
    private void txtASNY3_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtASNY3, txtSSNY3, e);
    private void txtASNY1_Leave(object? sender, EventArgs e) => ValidateMonthOffset(1, txtASNY1, txtASNY2, txtASNY3);
    private void txtASNY2_Leave(object? sender, EventArgs e) => ValidateMonthOffset(2, txtASNY1, txtASNY2, txtASNY3);
    private void txtASNY3_Leave(object? sender, EventArgs e) => ValidateMonthOffset(3, txtASNY1, txtASNY2, txtASNY3);

    private void ValidateMonthOffset(int slot, TextBox m1, TextBox m2, TextBox m3)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            TextBox cur = slot == 1 ? m1 : slot == 2 ? m2 : m3;
            cur.Text = cur.Text.Trim();
            if (string.IsNullOrEmpty(cur.Text))
            {
                return;
            }

            bool has1 = !string.IsNullOrEmpty(m1.Text.Trim());
            bool has2 = !string.IsNullOrEmpty(m2.Text.Trim());
            bool has3 = !string.IsNullOrEmpty(m3.Text.Trim());
            int v1 = (int)LegacyTextHelper.VbVal(m1.Text);
            int v2 = (int)LegacyTextHelper.VbVal(m2.Text);
            int v3 = (int)LegacyTextHelper.VbVal(m3.Text);

            if (slot == 1)
            {
                if (!has2 && !has3) { m2.Text = string.Empty; m3.Text = string.Empty; return; }
                if (!has3) { m3.Text = string.Empty; if (v1 > v2) FailDateLeave(m1); return; }
                if (!(v1 <= v2 && v2 <= v3)) FailDateLeave(m1);
            }
            else if (slot == 2)
            {
                if (!has1) { FailDateLeave(m2); return; }
                if (!has3) { m3.Text = string.Empty; if (v1 > v2) FailDateLeave(m2); return; }
                if (!(v1 <= v2 && v2 <= v3)) FailDateLeave(m2);
            }
            else
            {
                if (!has1 || !has2 || !(v1 <= v2 && v2 <= v3)) FailDateLeave(m3);
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    // --- 入金予定日 1..3 / 支払予定日 1..3 ---
    private void txtNY1_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtNY1, txtS2, e);
    private void txtNY2_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtNY2, txtS3, e);
    private void txtNY3_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; txtNY3.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optUPDN.Checked) optUPDN.Focus();
            else if (optDEL.Checked) optDEL.Focus();
            else if (optUP.Checked) optUP.Focus();
            else if (optNO.Checked) optNO.Focus();
        }
    }
    private void txtNY1_Leave(object? sender, EventArgs e) => ValidateDueDay(1, txtANY1, txtNY1, txtANY2, txtNY2, txtANY3, txtNY3);
    private void txtNY2_Leave(object? sender, EventArgs e) => ValidateDueDay(2, txtANY1, txtNY1, txtANY2, txtNY2, txtANY3, txtNY3);
    private void txtNY3_Leave(object? sender, EventArgs e) => ValidateDueDay(3, txtANY1, txtNY1, txtANY2, txtNY2, txtANY3, txtNY3);

    private void txtSSNY1_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtSSNY1, txtSS2, e);
    private void txtSSNY2_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtSSNY2, txtSS3, e);
    private void txtSSNY3_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; txtSSNY3.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optUPDN2.Checked) optUPDN2.Focus();
            else if (optDEL2.Checked) optDEL2.Focus();
            else if (optUP2.Checked) optUP2.Focus();
            else if (optNO2.Checked) optNO2.Focus();
        }
    }
    private void txtSSNY1_Leave(object? sender, EventArgs e) => ValidateDueDay(1, txtASNY1, txtSSNY1, txtASNY2, txtSSNY2, txtASNY3, txtSSNY3);
    private void txtSSNY2_Leave(object? sender, EventArgs e) => ValidateDueDay(2, txtASNY1, txtSSNY1, txtASNY2, txtSSNY2, txtASNY3, txtSSNY3);
    private void txtSSNY3_Leave(object? sender, EventArgs e) => ValidateDueDay(3, txtASNY1, txtSSNY1, txtASNY2, txtSSNY2, txtASNY3, txtSSNY3);

    private void ValidateDueDay(int slot, TextBox m1, TextBox d1, TextBox m2, TextBox d2, TextBox m3, TextBox d3)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            TextBox cur = slot == 1 ? d1 : slot == 2 ? d2 : d3;
            cur.Text = cur.Text.Trim();
            int curVal = (int)LegacyTextHelper.VbVal(cur.Text);
            if (string.IsNullOrEmpty(cur.Text) || curVal == 0)
            {
                cur.Text = string.Empty;
                return;
            }

            cur.Text = curVal.ToString("00", CultureInfo.InvariantCulture);
            if (curVal < 1 || curVal > 31)
            {
                FailDateLeave(cur);
                return;
            }

            int dv1 = (int)LegacyTextHelper.VbVal(d1.Text);
            int dv2 = (int)LegacyTextHelper.VbVal(d2.Text);
            int dv3 = (int)LegacyTextHelper.VbVal(d3.Text);
            int k1 = (int)LegacyTextHelper.VbVal(m1.Text) * 100 + dv1;
            int k2 = (int)LegacyTextHelper.VbVal(m2.Text) * 100 + dv2;
            int k3 = (int)LegacyTextHelper.VbVal(m3.Text) * 100 + dv3;

            if (slot == 1)
            {
                if (dv2 == 0 && dv3 == 0) { d2.Text = string.Empty; d3.Text = string.Empty; return; }
                if (dv3 == 0) { d3.Text = string.Empty; if (k1 > k2) FailDateLeave(d1); return; }
                if (!(k1 <= k2 && k2 <= k3)) FailDateLeave(d1);
            }
            else if (slot == 2)
            {
                if (dv1 == 0) { FailDateLeave(d2); return; }
                if (dv3 == 0) { d3.Text = string.Empty; if (k1 > k2) FailDateLeave(d2); return; }
                if (!(k1 <= k2 && k2 <= k3)) FailDateLeave(d2);
            }
            else
            {
                if (dv1 == 0 || dv2 == 0 || !(k1 <= k2 && k2 <= k3)) FailDateLeave(d3);
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void FailDateLeave(TextBox target)
    {
        MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        target.Text = string.Empty;
        BeginInvoke(() => target.Focus());
    }

    // --- Radio & 手形フォーカス移動 ---
    private void optTax1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optPON.Checked) optPON.Focus();
            else if (optPOFF.Checked) optPOFF.Focus();
        }
    }

    private void optInvoice_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optPNO.Checked) optPNO.Focus();
            else if (optMEI.Checked) optMEI.Focus();
            else if (optKAN.Checked) optKAN.Focus();
            else if (optMK.Checked) optMK.Focus();
        }
    }

    private void optReport_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; txtSSCD.Focus(); }
    }

    private void optTax2_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            GetShrHohoOptions()[GetSelectedShrHohoIndex()].Focus();
        }
    }

    private void optSHRHOHO_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (frmTEGATA.Enabled) txtSITE.Focus();
            else cmdSET.Focus();
        }
    }

    private void txtSITE_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtSITE, txtKSMM, e);

    private void txtSITE_Leave(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSITE.Text.Trim())) return;
        txtSITE.Text = ((int)LegacyTextHelper.VbVal(txtSITE.Text)).ToString("0", CultureInfo.InvariantCulture);
    }

    private void txtKSMM_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtKSMM, txtKSDD, e);
    private void txtKSDD_KeyDown(object? sender, KeyEventArgs e) => HandleTextBoxNav(txtKSDD, cmdSET, e);

    private void txtKSDD_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (string.IsNullOrEmpty(txtKSDD.Text.Trim())) return;
            int val = (int)LegacyTextHelper.VbVal(txtKSDD.Text);
            txtKSDD.Text = val.ToString("0", CultureInfo.InvariantCulture);
            if (val < 1 || val > 31)
            {
                MessageBox.Show("決済日付に誤りがあります。", ScreenTitle);
                BeginInvoke(() => txtKSDD.Focus());
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private static void HandleTextBoxNav(TextBox current, Control next, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            current.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            next.Focus();
        }
    }

    private void DigitsOnly_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }
}

