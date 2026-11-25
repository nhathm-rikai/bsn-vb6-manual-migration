using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0020 â€” å•†å“åˆ†é¡žãƒžã‚¹ã‚¿ç™»éŒ² (VB6 frmKUNO0020 1:1 ç§»è¡Œç”»é¢)
/// </summary>
public partial class FrmKUNO0020 : Form
{
    private const string ScreenTitle = "KUNO0020";

    private readonly IKuno0020Service _service;

    // ãƒ•ã‚©ãƒ¼ãƒ å†…çŠ¶æ…‹å¤‰æ•° (VB6 frmKUNO0020 ã¨åŒä¸€)
    private int _upflg;       // 0 = INSERT, 1 = UPDATE
    private int _noflg;       // 0 = å¤§åˆ†é¡žæœªå­˜åœ¨/æœªç¢ºèª, 1 = å¤§åˆ†é¡žå­˜åœ¨ç¢ºèªæ¸ˆ
    private string _sStrName = string.Empty;
    private int _sWbcd;
    private string _sStrSrt = string.Empty;
    private string _sStrSrt2 = string.Empty;
    private string _sStrTekyo = string.Empty;
    private string _sStrDdate = string.Empty;
    private int _sKkb;        // 0 = æœˆæœ«ç· åˆ‡(opt31), 1 = ï¼’ï¼æ—¥ç· åˆ‡(opt20)

    private bool _suppressFocusEvents;
    private bool _validationFailedInLeave;

    public FrmKUNO0020()
        : this(CreateDefaultService())
    {
    }

    public FrmKUNO0020(IKuno0020Service service)
    {
        _service = service;
        InitializeComponent();
        WireEvents();
    }

    private static IKuno0020Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0020Service(
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private void WireEvents()
    {
        Load += FrmKUNO0020_Load;

        // txtDCD
        txtDCD.Enter += txtDCD_GotFocus;
        txtDCD.KeyDown += txtDCD_KeyDown;
        txtDCD.KeyPress += DigitsOnly_KeyPress;
        txtDCD.Leave += txtDCD_LostFocus;

        // txtTCD
        txtTCD.Enter += txtTCD_GotFocus;
        txtTCD.KeyDown += txtTCD_KeyDown;
        txtTCD.KeyPress += DigitsOnly_KeyPress;
        txtTCD.Leave += txtTCD_LostFocus;

        // txtDTNM
        txtDTNM.KeyDown += txtDTNM_KeyDown;
        txtDTNM.Leave += txtDTNM_LostFocus;

        // txtBCD
        txtBCD.KeyDown += txtBCD_KeyDown;
        txtBCD.KeyPress += DigitsOnly_KeyPress;
        txtBCD.Leave += txtBCD_LostFocus;

        // txtSRT
        txtSRT.KeyDown += txtSRT_KeyDown;
        txtSRT.KeyPress += DigitsAndDot_KeyPress;

        // txtTEKYO
        txtTEKYO.KeyDown += txtTEKYO_KeyDown;
        txtTEKYO.KeyPress += DigitsAndDot_KeyPress;

        // txtSRT2
        txtSRT2.KeyDown += txtSRT2_KeyDown;
        txtSRT2.KeyPress += DigitsAndDot_KeyPress;

        // opt31 / opt20
        opt31.CheckedChanged += opt31_CheckedChanged;
        opt31.KeyDown += opt_KeyDown;
        opt20.CheckedChanged += opt20_CheckedChanged;
        opt20.KeyDown += opt_KeyDown;
    }

    // **********************************
    //  Form_Load
    // **********************************
    private void FrmKUNO0020_Load(object? sender, EventArgs e)
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

        // ã‚·ã‚¹ãƒ†ãƒ æ—¥ä»˜è¨­å®š (yy/mm/dd)
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        // ãƒœã‚¿ãƒ³ç„¡åŠ¹
        cmdSET.Enabled = false;
        cmdDEL.Enabled = false;

        // ãƒ‡ãƒ•ã‚©ãƒ«ãƒˆè¨­å®š
        opt31.Checked = true;
        _sKkb = 0;

        BeginInvoke(() => txtDCD.Focus());
    }

    // **********************************
    //  ç”»é¢å†…å®¹ã‚¯ãƒªã‚¢ (DATA_CLEAR)
    // **********************************
    private void DataClear()
    {
        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            txtDCD.Text = string.Empty;
            txtTCD.Text = string.Empty;
            txtDTNM.Text = string.Empty;
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            txtSRT.Text = string.Empty;
            txtTEKYO.Text = string.Empty;
            txtSRT2.Text = string.Empty;

            // ãƒœã‚¿ãƒ³ç„¡åŠ¹
            cmdSET.Enabled = false;
            cmdDEL.Enabled = false;

            // ãƒ‡ãƒ•ã‚©ãƒ«ãƒˆè¨­å®š
            opt31.Checked = true;
            _sKkb = 0;

            txtDCD.Focus();
        }
        finally
        {
            _suppressFocusEvents = prev;
        }
    }

    // **********************************
    //  ãƒ‡ãƒ¼ã‚¿è¡¨ç¤º (DATA_HYOJI)
    // **********************************
    private void DataHyoji()
    {
        // æ›´æ–°ç™»éŒ²ãƒ•ãƒ©ã‚°(0 = INSERT, 1 = UPDATE)
        _upflg = 0;

        // å¤§ãƒ»ä¸­åˆ†é¡žã‚³ãƒ¼ãƒ‰å…¥åŠ›æ™‚ãƒã‚§ãƒƒã‚¯
        if (string.IsNullOrWhiteSpace(txtTCD.Text) || string.IsNullOrWhiteSpace(txtDCD.Text))
        {
            cmdSET.Enabled = false;
            cmdDEL.Enabled = false;
            return;
        }

        _upflg = 1; // ãƒ‡ãƒ¼ã‚¿ä¿®æ­£

        // å‰Šé™¤å¯¾è±¡ãƒ‡ãƒ¼ã‚¿åˆ¤åˆ¥
        if (!string.IsNullOrWhiteSpace(_sStrDdate))
        {
            DialogResult ret = MessageBox.Show(
                "ã“ã®ãƒ¬ã‚³ãƒ¼ãƒ‰ã¯å‰Šé™¤ã•ã‚Œã¦ã„ã¾ã™ã€‚å¾©å…ƒã—ã¾ã™ã‹ï¼Ÿ",
                ScreenTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ret == DialogResult.No)
            {
                DataClear();
                return;
            }
        }

        // ãƒ†ã‚­ã‚¹ãƒˆå€¤è¨­å®š
        txtDTNM.Text = _sStrName;
        txtBCD.Text = _sWbcd.ToString("00", CultureInfo.InvariantCulture);
        if ((int)LegacyTextHelper.VbVal(txtBCD.Text) == 0)
        {
            txtBCD.Text = string.Empty;
        }

        int bmCd = (int)LegacyTextHelper.VbVal(txtBCD.Text);
        if (bmCd == 0 || !TryGetBumonWithErrorHandling(bmCd, out string strBname))
        {
            lblBCD.Text = string.Empty;
        }
        else
        {
            lblBCD.Text = strBname;
        }

        txtSRT.Text = _sStrSrt;
        txtTEKYO.Text = _sStrTekyo;
        txtSRT2.Text = _sStrSrt2;

        // ãƒœã‚¿ãƒ³æœ‰åŠ¹
        cmdSET.Enabled = true;
        cmdDEL.Enabled = true;

        // çµŒç†ç· åˆ‡æ—¥åŒºåˆ†è¨­å®š
        if (_sKkb == 0)
        {
            opt31.Checked = true;
        }
        else
        {
            opt20.Checked = true;
        }
    }

    // **********************************
    //  å•†å“åˆ†é¡žãƒžã‚¹ã‚¿å­˜åœ¨ãƒã‚§ãƒƒã‚¯ (kmbnri_check)
    // **********************************
    private bool KmbnriCheck(int daiCd, int tyuCd)
    {
        try
        {
            CategoryLookupResult res = _service.CheckCategory(daiCd, tyuCd);
            _sStrDdate = res.DeletedDateText;
            _sStrName = res.DtName;
            _sWbcd = res.BmCd;
            _sStrSrt = res.SrtText;
            _sStrTekyo = res.TekyoText;
            _sStrSrt2 = res.Srt2Text;
            _sKkb = res.KeiriDds;
            return res.Exists;
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "å•†å“åˆ†é¡žãƒžã‚¹ã‚¿", $"{daiCd},{tyuCd}", ex);
            Close();
            return false;
        }
    }

    private bool TryGetBumonWithErrorHandling(int bmCd, out string bumonName)
    {
        try
        {
            return _service.TryGetBumonName(bmCd, out bumonName);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "åç§°ãƒžã‚¹ã‚¿", $"4,{bmCd}", ex);
            bumonName = string.Empty;
            Close();
            return false;
        }
    }

    private bool IsBypassValidationTarget()
    {
        return ActiveControl == cmdCAN || ActiveControl == cmdEND;
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
    //  KeyPress å…±é€šãƒ•ã‚£ãƒ«ã‚¿
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

    // **********************************
    //  txtDCD ã‚¤ãƒ™ãƒ³ãƒˆ
    // **********************************
    private void txtDCD_GotFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents) return;
        cmdSET.Enabled = false;
        cmdDEL.Enabled = false;
    }

    private void txtDCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtDCD.Text = string.Empty;
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

        int daiVal = (int)LegacyTextHelper.VbVal(txtDCD.Text.Trim());
        if (daiVal == 0)
        {
            txtDCD.Text = string.Empty;
            return;
        }

        txtDCD.Text = daiVal.ToString("00", CultureInfo.InvariantCulture);

        // å¤§åˆ†é¡ž(ä¸­åˆ†é¡ž00)å­˜åœ¨ãƒã‚§ãƒƒã‚¯
        if (!KmbnriCheck(daiVal, 0))
        {
            if (IsDisposed) return;

            _upflg = 0;
            _noflg = 0;
            txtTCD.Text = "00";
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            txtSRT.Text = _sStrSrt;
            txtTEKYO.Text = _sStrTekyo;
            txtSRT2.Text = _sStrSrt2;
            txtDTNM.Text = string.Empty;
            opt31.Checked = true;
            _sKkb = 0;

            RedirectFocus(txtDTNM, () =>
            {
                cmdSET.Enabled = true;
                cmdDEL.Enabled = false;
            });
            return;
        }

        int tyuVal = (int)LegacyTextHelper.VbVal(txtTCD.Text);
        if (!KmbnriCheck(daiVal, tyuVal))
        {
            if (IsDisposed) return;

            _upflg = 0;
            txtDTNM.Text = string.Empty;
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            txtSRT.Text = _sStrSrt;
            cmdSET.Enabled = true;
            return;
        }

        _noflg = 1;
        DataHyoji();
    }

    // **********************************
    //  txtTCD ã‚¤ãƒ™ãƒ³ãƒˆ
    // **********************************
    private void txtTCD_GotFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents) return;
        cmdSET.Enabled = false;
        cmdDEL.Enabled = false;
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
            txtDTNM.Focus();
        }
    }

    private void txtTCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        int tyuVal = (int)LegacyTextHelper.VbVal(txtTCD.Text.Trim());

        // å¤§åˆ†é¡žæœªç™»éŒ²æ™‚ã¯ä¸­åˆ†é¡ž00ä»¥å¤–ä¸å¯
        if (_noflg == 0 && tyuVal != 0)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "å¤§åˆ†é¡žã‚³ãƒ¼ãƒ‰ï¼ˆä¸­åˆ†é¡žã‚³ãƒ¼ãƒ‰00ï¼‰ã‚’ç™»éŒ²ã—ã¦ãã ã•ã„ã€‚",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txtTCD.Text = string.Empty;
            RedirectFocus(txtTCD, () =>
            {
                cmdSET.Enabled = false;
                cmdDEL.Enabled = false;
            });
            return;
        }

        // ç·¨é›†è¡¨ç¤º (VB6: Format$(Trim$(txtTCD), "00") -> ç©ºæ–‡å­—æ™‚ã¯ "")
        string trimmedTcd = txtTCD.Text.Trim();
        txtTCD.Text = string.IsNullOrEmpty(trimmedTcd)
            ? string.Empty
            : tyuVal.ToString("00", CultureInfo.InvariantCulture);

        int daiVal = (int)LegacyTextHelper.VbVal(txtDCD.Text);

        // å…¥åŠ›ãƒã‚§ãƒƒã‚¯: å¤§åˆ†é¡ž(00)å­˜åœ¨ç¢ºèª
        if (!KmbnriCheck(daiVal, 0))
        {
            if (IsDisposed) return;

            _upflg = 0;
            _noflg = 0;
            txtTCD.Text = "00";
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            txtDTNM.Text = string.Empty;
            txtSRT.Text = _sStrSrt;
            txtTEKYO.Text = _sStrTekyo;
            txtSRT2.Text = _sStrSrt2;
            opt31.Checked = true;
            _sKkb = 0;

            RedirectFocus(txtDTNM, () =>
            {
                cmdSET.Enabled = true;
                cmdDEL.Enabled = false;
            });
            return;
        }

        // å…¥åŠ›ãƒã‚§ãƒƒã‚¯: æŒ‡å®šã•ã‚ŒãŸå¤§åˆ†é¡žãƒ»ä¸­åˆ†é¡žã®å­˜åœ¨ç¢ºèª
        int currentTyuVal = (int)LegacyTextHelper.VbVal(txtTCD.Text);
        if (!KmbnriCheck(daiVal, currentTyuVal))
        {
            if (IsDisposed) return;

            _upflg = 0;
            txtDTNM.Text = string.Empty;
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            txtSRT.Text = _sStrSrt;
            txtTEKYO.Text = _sStrTekyo;
            txtSRT2.Text = _sStrSrt2;
            opt31.Checked = true;
            _sKkb = 0;
            cmdSET.Enabled = true;
            cmdDEL.Enabled = false;
            return;
        }

        // ãƒ‡ãƒ¼ã‚¿è¡¨ç¤º
        _noflg = 1;
        DataHyoji();
    }

    // **********************************
    //  txtDTNM ã‚¤ãƒ™ãƒ³ãƒˆ
    // **********************************
    private void txtDTNM_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtDTNM.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtBCD.Focus();
        }
    }

    private void txtDTNM_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtDTNM.Text, 20) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "æ–‡å­—æ•°ã‚ªãƒ¼ãƒãƒ¼ã§ã™ã€‚",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtDTNM);
        }
    }

    // **********************************
    //  txtBCD ã‚¤ãƒ™ãƒ³ãƒˆ
    // **********************************
    private void txtBCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSRT.Focus();
        }
    }

    private void txtBCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        int bmVal = (int)LegacyTextHelper.VbVal(txtBCD.Text.Trim());
        if (bmVal == 0)
        {
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            return;
        }

        txtBCD.Text = bmVal.ToString("00", CultureInfo.InvariantCulture);

        if (!TryGetBumonWithErrorHandling(bmVal, out string strBname))
        {
            if (IsDisposed) return;

            _validationFailedInLeave = true;
            MessageBox.Show(
                "æŒ‡å®šã•ã‚ŒãŸéƒ¨é–€ã‚³ãƒ¼ãƒ‰ã¯å­˜åœ¨ã—ã¾ã›ã‚“ã€‚",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            RedirectFocus(txtBCD);
        }
        else
        {
            lblBCD.Text = strBname;
        }
    }

    // **********************************
    //  txtSRT / txtTEKYO / txtSRT2 ã‚¤ãƒ™ãƒ³ãƒˆ
    // **********************************
    private void txtSRT_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSRT.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtTEKYO.Focus();
        }
    }

    private void txtTEKYO_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTEKYO.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSRT2.Focus();
        }
    }

    private void txtSRT2_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSRT2.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (opt31.Checked)
            {
                opt31.Focus();
            }
            else if (opt20.Checked)
            {
                opt20.Focus();
            }
        }
    }

    // **********************************
    //  opt31 / opt20 ã‚¤ãƒ™ãƒ³ãƒˆ
    // **********************************
    private void opt31_CheckedChanged(object? sender, EventArgs e)
    {
        if (opt31.Checked)
        {
            _sKkb = 0;
        }
    }

    private void opt20_CheckedChanged(object? sender, EventArgs e)
    {
        if (opt20.Checked)
        {
            _sKkb = 1;
        }
    }

    private void opt_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (cmdSET.Enabled)
            {
                cmdSET.Focus();
            }
            else if (cmdCAN.Enabled)
            {
                cmdCAN.Focus();
            }
        }
    }

    // **********************************
    //  ãƒžã‚¹ã‚¿ç™»éŒ²ãƒ»æ›´æ–° (cmdSET_Click)
    // **********************************
    private void cmdSET_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave) return;

        var request = new CategorySaveRequest
        {
            DaiCdText = txtDCD.Text,
            TyuCdText = txtTCD.Text,
            DtNameText = txtDTNM.Text,
            BmCdText = txtBCD.Text,
            BmNameLabelText = lblBCD.Text,
            SrtText = txtSRT.Text,
            TekyoText = txtTEKYO.Text,
            Srt2Text = txtSRT2.Text,
            KeiriDds = _sKkb
        };

        try
        {
            Kuno0020ActionResult result = _service.SaveCategory(request, isUpdate: _upflg == 1);
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
            DataClear();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "å•†å“åˆ†é¡žãƒžã‚¹ã‚¿", $"{txtDCD.Text},{txtTCD.Text}", ex);
            Close();
        }
    }

    // **********************************
    //  ã‚­ãƒ£ãƒ³ã‚»ãƒ« (cmdCAN_Click)
    // **********************************
    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        DataClear();
    }

    // **********************************
    //  å‰Šé™¤å‡¦ç† (cmdDEL_Click)
    // **********************************
    private void cmdDEL_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave) return;

        DialogResult confirm = MessageBox.Show(
            "å‰Šé™¤ã—ã¾ã™ã‹ï¼Ÿ",
            ScreenTitle,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm == DialogResult.No)
        {
            DataClear();
            return;
        }

        int daiCd = (int)LegacyTextHelper.VbVal(txtDCD.Text);
        int tyuCd = (int)LegacyTextHelper.VbVal(txtTCD.Text);

        try
        {
            Kuno0020ActionResult result = _service.DeleteCategory(daiCd, tyuCd);
            if (!result.Success)
            {
                MessageBox.Show(result.Message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(result.Message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataClear();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "å•†å“åˆ†é¡žãƒžã‚¹ã‚¿", $"{txtDCD.Text},{txtTCD.Text}", ex);
            Close();
        }
    }

    // **********************************
    //  å‡¦ç†çµ‚äº† (cmdEND_Click)
    // **********************************
    private void cmdEND_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        Close();
    }

    private void FocusField(Kuno0020Field field)
    {
        switch (field)
        {
            case Kuno0020Field.DaiCd:
                txtDCD.Focus();
                break;
            case Kuno0020Field.TyuCd:
                txtTCD.Focus();
                break;
            case Kuno0020Field.DtName:
                txtDTNM.Focus();
                break;
            case Kuno0020Field.BmCd:
                txtBCD.Focus();
                break;
            case Kuno0020Field.Srt:
                txtSRT.Focus();
                break;
            case Kuno0020Field.Tekyo:
                txtTEKYO.Focus();
                break;
            case Kuno0020Field.Srt2:
                txtSRT2.Focus();
                break;
        }
    }
}
