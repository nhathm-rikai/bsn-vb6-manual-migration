using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;
using KousyoERP.Forms.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0010 — 商品マスタ登録 (VB6 frmKUNO0010 1:1 移行画面)
/// </summary>
public partial class FrmKUNO0010 : Form
{
    private const string ScreenTitle = "KUNO0010";

    private readonly IKuno0010Service _service;

    // フォーム内状態変数 (VB6 frmKUNO0010 と同一)
    private int _sSzkb; // 消費税区分: 0 = 外税(optSOTO), 1 = 内税(optUTI), 2 = 非課税(optHI)
    private int _sZkkb; // 在庫区分:   0 = なし(optNO),   1 = あり(optYES)

    private bool _suppressFocusEvents;
    private bool _validationFailedInLeave;

    public FrmKUNO0010()
        : this(CreateDefaultService())
    {
    }

    public FrmKUNO0010(IKuno0010Service service)
    {
        _service = service;
        InitializeComponent();
        WireControlEvents();
    }

    private static IKuno0010Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0010Service(
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private void WireControlEvents()
    {
        // txtSCD
        txtSCD.KeyDown += txtSCD_KeyDown;
        txtSCD.KeyPress += DigitsOnly_KeyPress;
        txtSCD.Leave += txtSCD_LostFocus;

        // txtSNMK
        txtSNMK.KeyDown += txtSNMK_KeyDown;
        txtSNMK.Leave += txtSNMK_LostFocus;

        // txtSNMKJ
        txtSNMKJ.KeyDown += txtSNMKJ_KeyDown;
        txtSNMKJ.Leave += txtSNMKJ_LostFocus;

        // txtSNMS
        txtSNMS.KeyDown += txtSNMS_KeyDown;
        txtSNMS.Leave += txtSNMS_LostFocus;

        // txtDCD
        txtDCD.KeyDown += txtDCD_KeyDown;
        txtDCD.KeyPress += DigitsOnly_KeyPress;
        txtDCD.Leave += txtDCD_LostFocus;

        // txtTCD
        txtTCD.KeyDown += txtTCD_KeyDown;
        txtTCD.KeyPress += DigitsOnly_KeyPress;
        txtTCD.Leave += txtTCD_LostFocus;

        // txtDSCD
        txtDSCD.KeyDown += txtDSCD_KeyDown;
        txtDSCD.KeyPress += DigitsOnly_KeyPress;
        txtDSCD.Leave += txtDSCD_LostFocus;

        // txtMCD
        txtMCD.KeyDown += txtMCD_KeyDown;
        txtMCD.KeyPress += DigitsOnly_KeyPress;
        txtMCD.Leave += txtMCD_LostFocus;

        // 消費税区分 (optSOTO / optUTI / optHI)
        optSOTO.CheckedChanged += optSOTO_CheckedChanged;
        optSOTO.KeyDown += optTax_KeyDown;
        optUTI.CheckedChanged += optUTI_CheckedChanged;
        optUTI.KeyDown += optTax_KeyDown;
        optHI.CheckedChanged += optHI_CheckedChanged;
        optHI.KeyDown += optTax_KeyDown;

        // 単価項目 (txtHJKK, txtSTK, txtGHTK, txtJHTK, txtIKHTK, txtIOHTK)
        txtHJKK.KeyDown += txtHJKK_KeyDown;
        txtHJKK.KeyPress += DigitsAndDot_KeyPress;
        txtHJKK.Leave += txtHJKK_LostFocus;

        txtSTK.KeyDown += txtSTK_KeyDown;
        txtSTK.KeyPress += DigitsAndDot_KeyPress;
        txtSTK.Leave += txtSTK_LostFocus;

        txtGHTK.KeyDown += txtGHTK_KeyDown;
        txtGHTK.KeyPress += DigitsAndDot_KeyPress;
        txtGHTK.Leave += txtGHTK_LostFocus;

        txtJHTK.KeyDown += txtJHTK_KeyDown;
        txtJHTK.KeyPress += DigitsAndDot_KeyPress;
        txtJHTK.Leave += txtJHTK_LostFocus;

        txtIKHTK.KeyDown += txtIKHTK_KeyDown;
        txtIKHTK.KeyPress += DigitsAndDot_KeyPress;
        txtIKHTK.Leave += txtIKHTK_LostFocus;

        txtIOHTK.KeyDown += txtIOHTK_KeyDown;
        txtIOHTK.KeyPress += DigitsAndDot_KeyPress;
        txtIOHTK.Leave += txtIOHTK_LostFocus;

        // txtTANI
        txtTANI.KeyDown += txtTANI_KeyDown;
        txtTANI.KeyPress += DigitsOnly_KeyPress;
        txtTANI.Leave += txtTANI_LostFocus;

        // txtISU
        txtISU.KeyDown += txtISU_KeyDown;
        txtISU.KeyPress += DigitsAndDot_KeyPress;
        txtISU.Leave += txtISU_LostFocus;

        // 在庫区分 (optNO / optYES)
        optNO.CheckedChanged += optNO_CheckedChanged;
        optNO.KeyDown += optZaiko_KeyDown;
        optYES.CheckedChanged += optYES_CheckedChanged;
        optYES.KeyDown += optZaiko_KeyDown;

        // txtZSU / txtBK
        txtZSU.KeyDown += txtZSU_KeyDown;
        txtZSU.KeyPress += DigitsAndDot_KeyPress;
        txtZSU.Leave += txtZSU_LostFocus;

        txtBK.KeyDown += txtBK_KeyDown;
        txtBK.KeyPress += DigitsAndDot_KeyPress;
        txtBK.Leave += txtBK_LostFocus;
    }

    // **********************************
    //  Form_Load
    // **********************************
    private void FrmKUNO0010_Load(object? sender, EventArgs e)
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

        // システム日付設定 (yy/mm/dd)
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        // ボタン・テキスト無効設定
        cmdOK.Enabled = false;
        txtSCD.Enabled = false;

        // デフォルト設定
        optNO.Checked = true;
        _sZkkb = 0;
        optSOTO.Checked = true;
        _sSzkb = 0;

        BeginInvoke(() => cmdTOU.Focus());
    }

    // **********************************
    //  画面内容クリア (DATA_CLEAR)
    // **********************************
    private void DataClear()
    {
        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            txtBK.Text = string.Empty;
            txtDCD.Text = string.Empty;
            txtDSCD.Text = string.Empty;
            txtGHTK.Text = string.Empty;
            txtHJKK.Text = string.Empty;
            txtIKHTK.Text = string.Empty;
            txtIOHTK.Text = string.Empty;
            txtISU.Text = string.Empty;
            txtJHTK.Text = string.Empty;
            txtMCD.Text = string.Empty;
            txtSCD.Text = string.Empty;
            txtSNMK.Text = string.Empty;
            txtSNMKJ.Text = string.Empty;
            txtSNMS.Text = string.Empty;
            txtSTK.Text = string.Empty;
            txtTANI.Text = string.Empty;
            txtTCD.Text = string.Empty;
            txtZSU.Text = string.Empty;

            lbldcd.Text = string.Empty;
            lbldscd.Text = string.Empty;
            lblmcd.Text = string.Empty;
            lbltani.Text = string.Empty;
            lbltcd.Text = string.Empty;

            txtSCD.Enabled = false;

            // ボタン有効・無効設定
            cmdTOU.Enabled = true;
            cmdSYU.Enabled = true;
            cmdDEL.Enabled = true;
            cmdOK.Enabled = false;

            cmdTOU.Focus();
        }
        finally
        {
            _suppressFocusEvents = prev;
        }
    }

    private bool IsBypassValidationTarget()
    {
        return ActiveControl == cmdCAN
            || ActiveControl == cmdEND
            || ActiveControl == cmdMCD
            || ActiveControl == cmdTANI;
    }

    private void RedirectFocus(Control target, Action? afterFocus = null)
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
                afterFocus?.Invoke();
            }
            finally
            {
                _suppressFocusEvents = prev;
                _validationFailedInLeave = false;
            }
        });
    }

    // **********************************
    //  KeyPress 共通フィルタ
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

    private static void DigitsAndDot_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }

    private void ValidateAndFormatNumericTextBox(TextBox textBox, int wksu, int wsu)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;
        if (string.IsNullOrEmpty(textBox.Text)) return;

        if (LegacyTextHelper.TryFormatNumericHensyu(textBox.Text, wksu, wsu, out string formatted))
        {
            textBox.Text = formatted;
        }
        else
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "入力エラーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            textBox.Text = string.Empty;
            RedirectFocus(textBox);
        }
    }

    // **********************************
    //  モードボタン (cmdTOU / cmdSYU / cmdDEL)
    // **********************************
    private void cmdTOU_Click(object? sender, EventArgs e)
    {
        cmdTOU.Enabled = true;
        cmdOK.Enabled = true;

        cmdSYU.Enabled = false;
        cmdDEL.Enabled = false;

        txtSNMK.Focus();
    }

    private void cmdSYU_Click(object? sender, EventArgs e)
    {
        cmdSYU.Enabled = true;

        cmdTOU.Enabled = false;
        cmdDEL.Enabled = false;
        cmdOK.Enabled = false;

        txtSCD.Enabled = true;
        txtSCD.Focus();
    }

    private void cmdDEL_Click(object? sender, EventArgs e)
    {
        cmdDEL.Enabled = true;

        cmdTOU.Enabled = false;
        cmdSYU.Enabled = false;
        cmdOK.Enabled = false;

        txtSCD.Enabled = true;
        txtSCD.Focus();
    }

    // **********************************
    //  検索ポップアップボタン (cmdMCD / cmdTANI)
    // **********************************
    private void cmdMCD_Click(object? sender, EventArgs e)
    {
        int dcd = (int)LegacyTextHelper.VbVal(txtDCD.Text);
        using var dlg = new FrmMAKKEN(_service, dcd);
        dlg.ShowDialog(this);

        if (dlg.Selected)
        {
            txtMCD.Text = dlg.SelectedMakerCode;
            lblmcd.Text = dlg.SelectedMakerShortName;
        }
        else
        {
            txtMCD.Text = string.Empty;
            lblmcd.Text = string.Empty;
        }

        txtMCD.Focus();
    }

    private void cmdTANI_Click(object? sender, EventArgs e)
    {
        using var dlg = new FrmTAIKEN(_service);
        dlg.ShowDialog(this);

        if (dlg.Selected)
        {
            txtTANI.Text = dlg.SelectedUnitCode;
            lbltani.Text = dlg.SelectedUnitShortName;
        }
        else
        {
            txtTANI.Text = string.Empty;
            lbltani.Text = string.Empty;
        }

        txtTANI.Focus();
    }

    // **********************************
    //  txtSCD イベント (商品コード)
    // **********************************
    private void txtSCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSNMK.Focus();
        }
    }

    private void txtSCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        long scdVal = (long)LegacyTextHelper.VbVal(txtSCD.Text.Trim());
        if (scdVal == 0)
        {
            txtSCD.Text = string.Empty;
            return;
        }

        txtSCD.Text = scdVal.ToString("00000000", CultureInfo.InvariantCulture);

        if (string.IsNullOrWhiteSpace(txtSCD.Text))
        {
            cmdOK.Enabled = false;
            return;
        }

        ProductDetailResult detail;
        try
        {
            detail = _service.GetProductByCode(scdVal);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品マスタ", txtSCD.Text, ex);
            Close();
            return;
        }

        if (!detail.Exists)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "指定された商品ｺｰﾄﾞは存在しません。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ClearFieldsOnlyKeepMode();
            RedirectFocus(txtSCD);
            return;
        }

        // 削除対象データ判別
        if (detail.IsDeleted)
        {
            DialogResult ret = MessageBox.Show(
                "このレコードは削除されています。復元しますか？",
                ScreenTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ret == DialogResult.No)
            {
                DataClear();
                return;
            }
        }

        // テキスト・ラベル値反映
        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            txtSNMK.Text = detail.SnmkText;
            txtSNMKJ.Text = detail.SnmkjText;
            txtSNMS.Text = detail.SnmsText;
            txtDCD.Text = detail.DcdText;
            lbldcd.Text = detail.DcdName;
            txtTCD.Text = detail.TcdText;
            lbltcd.Text = detail.TcdName;
            txtDSCD.Text = detail.DscdText;
            lbldscd.Text = detail.DscdName;
            txtMCD.Text = detail.McdText;
            lblmcd.Text = detail.McdName;

            switch (detail.SzKb)
            {
                case 0:
                    optSOTO.Checked = true;
                    _sSzkb = 0;
                    break;
                case 1:
                    optUTI.Checked = true;
                    _sSzkb = 1;
                    break;
                case 2:
                    optHI.Checked = true;
                    _sSzkb = 2;
                    break;
            }

            txtHJKK.Text = detail.HjkkText;
            txtSTK.Text = detail.StkText;
            txtGHTK.Text = detail.GhtkText;
            txtJHTK.Text = detail.JhtkText;
            txtIKHTK.Text = detail.IkhtkText;
            txtIOHTK.Text = detail.IohtkText;
            txtTANI.Text = detail.TaniText;
            lbltani.Text = detail.TaniName;
            txtISU.Text = detail.IsuText;

            switch (detail.ZkKb)
            {
                case 0:
                    optNO.Checked = true;
                    _sZkkb = 0;
                    break;
                case 1:
                    optYES.Checked = true;
                    _sZkkb = 1;
                    break;
            }

            txtZSU.Text = detail.ZsuText;
            txtBK.Text = detail.BkText;
        }
        finally
        {
            _suppressFocusEvents = prev;
        }

        if (detail.NumericErrorField != Kuno0010Field.None)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "入力エラーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            FocusField(detail.NumericErrorField);
            return;
        }

        cmdOK.Enabled = true;
    }

    private void ClearFieldsOnlyKeepMode()
    {
        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            txtBK.Text = string.Empty;
            txtDCD.Text = string.Empty;
            txtDSCD.Text = string.Empty;
            txtGHTK.Text = string.Empty;
            txtHJKK.Text = string.Empty;
            txtIKHTK.Text = string.Empty;
            txtIOHTK.Text = string.Empty;
            txtISU.Text = string.Empty;
            txtJHTK.Text = string.Empty;
            txtMCD.Text = string.Empty;
            txtSCD.Text = string.Empty;
            txtSNMK.Text = string.Empty;
            txtSNMKJ.Text = string.Empty;
            txtSNMS.Text = string.Empty;
            txtSTK.Text = string.Empty;
            txtTANI.Text = string.Empty;
            txtTCD.Text = string.Empty;
            txtZSU.Text = string.Empty;

            lbldcd.Text = string.Empty;
            lbldscd.Text = string.Empty;
            lblmcd.Text = string.Empty;
            lbltani.Text = string.Empty;
            lbltcd.Text = string.Empty;
        }
        finally
        {
            _suppressFocusEvents = prev;
        }
    }

    // **********************************
    //  txtSNMK / txtSNMKJ / txtSNMS イベント
    // **********************************
    private void txtSNMK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSNMK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSNMKJ.Focus();
        }
    }

    private void txtSNMK_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtSNMK.Text, 25) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtSNMK);
        }
    }

    private void txtSNMKJ_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSNMKJ.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSNMS.Focus();
        }
    }

    private void txtSNMKJ_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtSNMKJ.Text, 40) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtSNMKJ);
            return;
        }

        // 略称表示 (登録モード時、先頭16バイトを自動セット)
        if (!string.IsNullOrEmpty(txtSNMKJ.Text) && cmdTOU.Enabled)
        {
            txtSNMS.Text = LegacyTextHelper.SubStringByByte(txtSNMKJ.Text, 16);
        }
    }

    private void txtSNMS_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSNMS.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtDCD.Focus();
        }
    }

    private void txtSNMS_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtSNMS.Text, 16) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtSNMS);
        }
    }

    // **********************************
    //  txtDCD / txtTCD / txtDSCD / txtMCD イベント
    // **********************************
    private void txtDCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtDCD.Text = string.Empty;
            lbldcd.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtTCD.Focus();
        }
    }

    private void txtDCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        int dcdVal = (int)LegacyTextHelper.VbVal(txtDCD.Text.Trim());
        if (dcdVal == 0)
        {
            txtDCD.Text = string.Empty;
            lbldcd.Text = string.Empty;
            return;
        }

        txtDCD.Text = dcdVal.ToString("00", CultureInfo.InvariantCulture);

        try
        {
            if (!_service.TryCheckCategory(dcdVal, 0, out string strBname, out _))
            {
                _validationFailedInLeave = true;
                MessageBox.Show(
                    "指定された大分類コードは存在しません。",
                    ScreenTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtDCD.Text = string.Empty;
                lbldcd.Text = string.Empty;
                RedirectFocus(txtDCD);
            }
            else
            {
                lbldcd.Text = strBname;
            }
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類マスタ", $"{dcdVal},0", ex);
            Close();
        }
    }

    private void txtTCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtDSCD.Focus();
        }
    }

    private void txtTCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        string trimmed = txtTCD.Text.Trim();
        if (string.IsNullOrEmpty(trimmed))
        {
            lbltcd.Text = string.Empty;
            return;
        }

        int tcdVal = (int)LegacyTextHelper.VbVal(trimmed);
        txtTCD.Text = tcdVal.ToString("00", CultureInfo.InvariantCulture);
        int dcdVal = (int)LegacyTextHelper.VbVal(txtDCD.Text);

        try
        {
            if (!_service.TryCheckCategory(dcdVal, tcdVal, out string strBname, out _))
            {
                _validationFailedInLeave = true;
                MessageBox.Show(
                    "指定された中分類コードは存在しません。",
                    ScreenTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtTCD.Text = string.Empty;
                lbltcd.Text = string.Empty;
                RedirectFocus(txtTCD);
            }
            else
            {
                lbltcd.Text = strBname;
            }
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類マスタ", $"{dcdVal},{tcdVal}", ex);
            Close();
        }
    }

    private void txtDSCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtDSCD.Text = string.Empty;
            lbldscd.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtMCD.Focus();
        }
    }

    private void txtDSCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        long dscdVal = (long)LegacyTextHelper.VbVal(txtDSCD.Text.Trim());
        if (dscdVal == 0)
        {
            txtDSCD.Text = string.Empty;
            lbldscd.Text = string.Empty;
            return;
        }

        txtDSCD.Text = dscdVal.ToString("00000000", CultureInfo.InvariantCulture);

        try
        {
            if (!_service.TryCheckRepresentativeProduct(dscdVal, out string strBname))
            {
                _validationFailedInLeave = true;
                MessageBox.Show(
                    "指定された代表商品コードは存在しません。",
                    ScreenTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtDSCD.Text = string.Empty;
                lbldscd.Text = string.Empty;
                RedirectFocus(txtDSCD);
            }
            else
            {
                lbldscd.Text = strBname;
            }
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品マスタ", txtDSCD.Text, ex);
            Close();
        }
    }

    private void txtMCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtMCD.Text = string.Empty;
            lblmcd.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (optSOTO.Checked)
            {
                optSOTO.Focus();
            }
            else if (optUTI.Checked)
            {
                optUTI.Focus();
            }
            else if (optHI.Checked)
            {
                optHI.Focus();
            }
            else
            {
                optSOTO.Focus();
            }
        }
    }

    private void txtMCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        int mcdVal = (int)LegacyTextHelper.VbVal(txtMCD.Text.Trim());
        if (mcdVal == 0)
        {
            txtMCD.Text = string.Empty;
            lblmcd.Text = string.Empty;
            return;
        }

        txtMCD.Text = mcdVal.ToString("0000", CultureInfo.InvariantCulture);

        try
        {
            if (!_service.TryGetMakerName(mcdVal, out string strBname))
            {
                _validationFailedInLeave = true;
                MessageBox.Show(
                    "指定されたメーカーコードは存在しません。",
                    ScreenTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtMCD.Text = string.Empty;
                lblmcd.Text = string.Empty;
                RedirectFocus(txtMCD);
            }
            else
            {
                lblmcd.Text = strBname;
            }
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"9,{mcdVal}", ex);
            Close();
        }
    }

    // **********************************
    //  消費税区分 (optSOTO / optUTI / optHI)
    // **********************************
    private void optSOTO_CheckedChanged(object? sender, EventArgs e)
    {
        if (optSOTO.Checked) _sSzkb = 0;
    }

    private void optUTI_CheckedChanged(object? sender, EventArgs e)
    {
        if (optUTI.Checked) _sSzkb = 1;
    }

    private void optHI_CheckedChanged(object? sender, EventArgs e)
    {
        if (optHI.Checked) _sSzkb = 2;
    }

    private void optTax_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtHJKK.Focus();
        }
    }

    // **********************************
    //  単価・数量・単位イベント
    // **********************************
    private void txtHJKK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtHJKK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSTK.Focus();
        }
    }

    private void txtHJKK_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtHJKK, 10, 2);

    private void txtSTK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSTK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtGHTK.Focus();
        }
    }

    private void txtSTK_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtSTK, 10, 2);

    private void txtGHTK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtGHTK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtJHTK.Focus();
        }
    }

    private void txtGHTK_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtGHTK, 10, 2);

    private void txtJHTK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtJHTK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtIKHTK.Focus();
        }
    }

    private void txtJHTK_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtJHTK, 10, 2);

    private void txtIKHTK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtIKHTK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtIOHTK.Focus();
        }
    }

    private void txtIKHTK_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtIKHTK, 10, 2);

    private void txtIOHTK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtIOHTK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtTANI.Focus();
        }
    }

    private void txtIOHTK_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtIOHTK, 10, 2);

    private void txtTANI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTANI.Text = string.Empty;
            lbltani.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtISU.Focus();
        }
    }

    private void txtTANI_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        int taniVal = (int)LegacyTextHelper.VbVal(txtTANI.Text.Trim());
        if (taniVal == 0)
        {
            txtTANI.Text = string.Empty;
            lbltani.Text = string.Empty;
            return;
        }

        txtTANI.Text = taniVal.ToString("00", CultureInfo.InvariantCulture);

        try
        {
            if (!_service.TryGetUnitName(taniVal, out string strBname))
            {
                _validationFailedInLeave = true;
                MessageBox.Show(
                    "指定された単位コードは存在しません。",
                    ScreenTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtTANI.Text = string.Empty;
                lbltani.Text = string.Empty;
                RedirectFocus(txtTANI);
            }
            else
            {
                lbltani.Text = strBname;
            }
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"16,{taniVal}", ex);
            Close();
        }
    }

    private void txtISU_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtISU.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (optNO.Checked)
            {
                optNO.Focus();
            }
            else if (optYES.Checked)
            {
                optYES.Focus();
            }
            else
            {
                optNO.Focus();
            }
        }
    }

    private void txtISU_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtISU, 6, 2);

    // **********************************
    //  在庫区分 (optNO / optYES)
    // **********************************
    private void optNO_CheckedChanged(object? sender, EventArgs e)
    {
        if (optNO.Checked) _sZkkb = 0;
    }

    private void optYES_CheckedChanged(object? sender, EventArgs e)
    {
        if (optYES.Checked) _sZkkb = 1;
    }

    private void optZaiko_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtZSU.Focus();
        }
    }

    private void txtZSU_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtZSU.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtBK.Focus();
        }
    }

    private void txtZSU_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtZSU, 10, 2);

    private void txtBK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtBK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (cmdOK.Enabled)
            {
                cmdOK.Focus();
            }
            else if (cmdEND.Enabled)
            {
                cmdEND.Focus();
            }
        }
    }

    private void txtBK_LostFocus(object? sender, EventArgs e)
        => ValidateAndFormatNumericTextBox(txtBK, 10, 2);

    // **********************************
    //  マスタ登録・更新・削除実行 (cmdOK_Click)
    // **********************************
    private void cmdOK_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave) return;

        // 削除モード
        if (cmdDEL.Enabled)
        {
            DialogResult confirm = MessageBox.Show(
                "削除しますか？",
                ScreenTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                DataClear();
                return;
            }

            long kshncd = (long)LegacyTextHelper.VbVal(txtSCD.Text);
            try
            {
                Kuno0010ActionResult delRes = _service.DeleteProduct(kshncd, txtSCD.Text);
                MessageBox.Show(delRes.Message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataClear();
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品マスタ", txtSCD.Text, ex);
                Close();
            }
            return;
        }

        // 登録・修正モード
        var request = new ProductSaveRequest
        {
            ScdText = txtSCD.Text,
            SnmkText = txtSNMK.Text,
            SnmkjText = txtSNMKJ.Text,
            SnmsText = txtSNMS.Text,
            DcdText = txtDCD.Text,
            DcdLabelText = lbldcd.Text,
            TcdText = txtTCD.Text,
            TcdLabelText = lbltcd.Text,
            DscdText = txtDSCD.Text,
            DscdLabelText = lbldscd.Text,
            McdText = txtMCD.Text,
            McdLabelText = lblmcd.Text,
            HasTaxSelection = optSOTO.Checked || optUTI.Checked || optHI.Checked,
            SzKb = _sSzkb,
            HjkkText = txtHJKK.Text,
            StkText = txtSTK.Text,
            GhtkText = txtGHTK.Text,
            JhtkText = txtJHTK.Text,
            IkhtkText = txtIKHTK.Text,
            IohtkText = txtIOHTK.Text,
            TaniText = txtTANI.Text,
            TaniLabelText = lbltani.Text,
            IsuText = txtISU.Text,
            HasZaikoSelection = optYES.Checked || optNO.Checked,
            ZkKb = _sZkkb,
            ZsuText = txtZSU.Text,
            BkText = txtBK.Text
        };

        try
        {
            Kuno0010ActionResult result = cmdTOU.Enabled
                ? _service.RegisterProduct(request)
                : _service.UpdateProduct(request);

            if (!result.Success)
            {
                MessageBoxIcon icon = result.IsStopFatal
                    ? MessageBoxIcon.Stop
                    : (result.IsExclamation ? MessageBoxIcon.Exclamation : MessageBoxIcon.Information);

                MessageBox.Show(result.Message, ScreenTitle, MessageBoxButtons.OK, icon);

                if (result.IsStopFatal)
                {
                    Close();
                    return;
                }

                if (result.ClearTcdOnError)
                {
                    txtTCD.Text = string.Empty;
                    lbltcd.Text = string.Empty;
                }

                FocusField(result.ErrorField);
                return;
            }

            MessageBox.Show(result.Message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataClear();
        }
        catch (Exception ex)
        {
            string errKey = cmdTOU.Enabled ? $"{txtDCD.Text},{txtTCD.Text}" : txtSCD.Text;
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品マスタ", errKey, ex);
            Close();
        }
    }

    // **********************************
    //  キャンセル (cmdCAN_Click)
    // **********************************
    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        DataClear();
    }

    // **********************************
    //  処理終了 (cmdEND_Click)
    // **********************************
    private void cmdEND_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        Close();
    }

    private void FocusField(Kuno0010Field field)
    {
        switch (field)
        {
            case Kuno0010Field.Scd:
                txtSCD.Focus();
                break;
            case Kuno0010Field.Snmk:
                txtSNMK.Focus();
                break;
            case Kuno0010Field.Snmkj:
                txtSNMKJ.Focus();
                break;
            case Kuno0010Field.Snms:
                txtSNMS.Focus();
                break;
            case Kuno0010Field.Dcd:
                txtDCD.Focus();
                break;
            case Kuno0010Field.Tcd:
                txtTCD.Focus();
                break;
            case Kuno0010Field.Dscd:
                txtDSCD.Focus();
                break;
            case Kuno0010Field.Mcd:
                txtMCD.Focus();
                break;
            case Kuno0010Field.Hjkk:
                txtHJKK.Focus();
                break;
            case Kuno0010Field.Stk:
                txtSTK.Focus();
                break;
            case Kuno0010Field.Ghtk:
                txtGHTK.Focus();
                break;
            case Kuno0010Field.Jhtk:
                txtJHTK.Focus();
                break;
            case Kuno0010Field.Ikhtk:
                txtIKHTK.Focus();
                break;
            case Kuno0010Field.Iohtk:
                txtIOHTK.Focus();
                break;
            case Kuno0010Field.Tani:
                txtTANI.Focus();
                break;
            case Kuno0010Field.Isu:
                txtISU.Focus();
                break;
            case Kuno0010Field.Zsu:
                txtZSU.Focus();
                break;
            case Kuno0010Field.Bk:
                txtBK.Focus();
                break;
        }
    }
}

