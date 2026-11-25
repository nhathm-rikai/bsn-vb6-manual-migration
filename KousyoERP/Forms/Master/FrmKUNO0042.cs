using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;
using KousyoERP.Forms.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0042 — 銀行口座情報登録 (VB6 frmKUNO0042 1:1 移行画面)
/// </summary>
public partial class FrmKUNO0042 : Form
{
    private const string ScreenTitle = "KUNO0040";

    private readonly IKuno0040Service _service;
    private readonly Kuno0040State _state;
    private bool _isHandlingLeave;

    public FrmKUNO0042()
        : this(CreateDefaultService(), new Kuno0040State())
    {
    }

    public FrmKUNO0042(IKuno0040Service service, Kuno0040State state)
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

    private void FrmKUNO0042_Load(object? sender, EventArgs e)
    {
        _state.CanFlg3 = 0;
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        lblKCD.Text = _state.Kokyaku;
        lblKNM.Text = _state.SnmKokyaku;

        if (_state.UpFlg40 == 0)
        {
            AllClear();
        }
        else
        {
            OnelistSet();
        }
    }

    private void AllClear()
    {
        _state.ResetBankFields();

        txtGCD1.Text = string.Empty;
        lblGCD1.Text = string.Empty;
        optTY.Checked = true;
        txtKNO.Text = string.Empty;
        txtKMG1.Text = string.Empty;

        txtGCD2.Text = string.Empty;
        lblGCD2.Text = string.Empty;
        optTY2.Checked = true;
        txtKNO2.Text = string.Empty;
        txtKMG2.Text = string.Empty;
    }

    private void OnelistSet()
    {
        if (_state.F450 != 0)
        {
            txtGCD1.Text = _state.F450.ToString("0000000", CultureInfo.InvariantCulture);
            lblGCD1.Text = LookupBankShortName(_state.F450) ?? string.Empty;
        }
        else
        {
            txtGCD1.Text = string.Empty;
            lblGCD1.Text = string.Empty;
        }

        switch (_state.F460)
        {
            case 0: optNOBK.Checked = true; break;
            case 1: optTY.Checked = true; break;
            case 2: optFY.Checked = true; break;
        }

        txtKNO.Text = _state.F470;
        txtKMG1.Text = _state.F480;

        if (_state.F490 != 0)
        {
            txtGCD2.Text = _state.F490.ToString("0000000", CultureInfo.InvariantCulture);
            lblGCD2.Text = LookupBankShortName(_state.F490) ?? string.Empty;
        }
        else
        {
            txtGCD2.Text = string.Empty;
            lblGCD2.Text = string.Empty;
        }

        switch (_state.F500)
        {
            case 0: optNOBK2.Checked = true; break;
            case 1: optTY2.Checked = true; break;
            case 2: optFY2.Checked = true; break;
        }

        txtKNO2.Text = _state.F510;
        txtKMG2.Text = _state.F520;
    }

    private string? LookupBankShortName(long gkCd)
    {
        try
        {
            return _service.GetActiveBankShortName(gkCd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "銀行マスタ", gkCd.ToString("0000000", CultureInfo.InvariantCulture), ex);
            return null;
        }
    }

    private void cmdGCD1_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmGNKKEN(_service);
        if (frm.ShowDialog(this) == DialogResult.OK && frm.IsSelected)
        {
            txtGCD1.Text = frm.SelectedBankCode;
            lblGCD1.Text = frm.SelectedBankShortName;
        }
        else
        {
            txtGCD1.Text = string.Empty;
            lblGCD1.Text = string.Empty;
        }

        txtGCD1.Focus();
    }

    private void cmdGCD2_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmGNKKEN(_service);
        if (frm.ShowDialog(this) == DialogResult.OK && frm.IsSelected)
        {
            txtGCD2.Text = frm.SelectedBankCode;
            lblGCD2.Text = frm.SelectedBankShortName;
        }
        else
        {
            txtGCD2.Text = string.Empty;
            lblGCD2.Text = string.Empty;
        }

        txtGCD2.Focus();
    }

    private void cmdSET_Click(object? sender, EventArgs e)
    {
        _state.F450 = (long)LegacyTextHelper.VbVal(txtGCD1.Text);
        if (optNOBK.Checked) _state.F460 = 0;
        if (optTY.Checked) _state.F460 = 1;
        if (optFY.Checked) _state.F460 = 2;
        _state.F470 = txtKNO.Text;
        _state.F480 = txtKMG1.Text;

        _state.F490 = (long)LegacyTextHelper.VbVal(txtGCD2.Text);
        if (optNOBK2.Checked) _state.F500 = 0;
        if (optTY2.Checked) _state.F500 = 1;
        if (optFY2.Checked) _state.F500 = 2;
        _state.F510 = txtKNO2.Text;
        _state.F520 = txtKMG2.Text;

        _state.SetFlg3 = 1;
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        AllClear();
        _state.CanFlg3 = 1;
        Close();
    }

    private void txtGCD1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtGCD1.Text = string.Empty;
            lblGCD1.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optNOBK.Checked) optNOBK.Focus();
            else if (optTY.Checked) optTY.Focus();
            else if (optFY.Checked) optFY.Focus();
        }
    }

    private void txtGCD1_KeyPress(object? sender, KeyPressEventArgs e) => AllowDigitsOnly(e);

    private void txtGCD1_Leave(object? sender, EventArgs e)
    {
        HandleBankCodeLeave(txtGCD1, lblGCD1);
    }

    private void txtGCD2_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtGCD2.Text = string.Empty;
            lblGCD2.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optNOBK2.Checked) optNOBK2.Focus();
            else if (optTY2.Checked) optTY2.Focus();
            else if (optFY2.Checked) optFY2.Focus();
        }
    }

    private void txtGCD2_KeyPress(object? sender, KeyPressEventArgs e) => AllowDigitsOnly(e);

    private void txtGCD2_Leave(object? sender, EventArgs e)
    {
        HandleBankCodeLeave(txtGCD2, lblGCD2);
    }

    private void HandleBankCodeLeave(TextBox txt, Label lbl)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            txt.Text = txt.Text.Trim();
            long scd = (long)LegacyTextHelper.VbVal(txt.Text);
            if (string.IsNullOrEmpty(txt.Text) || scd == 0)
            {
                txt.Text = string.Empty;
                lbl.Text = string.Empty;
                return;
            }

            txt.Text = scd.ToString("0000000", CultureInfo.InvariantCulture);
            string? snm = LookupBankShortName(scd);
            if (snm is null)
            {
                txt.Text = string.Empty;
                lbl.Text = string.Empty;
                MessageBox.Show("指定された銀行コードは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

    private void optAccountType1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtKNO.Focus();
        }
    }

    private void optAccountType2_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtKNO2.Focus();
        }
    }

    private void txtKNO_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtKNO.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtKMG1.Focus();
        }
    }

    private void txtKNO_KeyPress(object? sender, KeyPressEventArgs e) => AllowDigitsOnly(e);

    private void txtKNO2_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtKNO2.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtKMG2.Focus();
        }
    }

    private void txtKNO2_KeyPress(object? sender, KeyPressEventArgs e) => AllowDigitsOnly(e);

    private void txtKMG1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtKMG1.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtGCD2.Focus();
        }
    }

    private void txtKMG1_Leave(object? sender, EventArgs e) => ValidateHalfWidthKana(txtKMG1);

    private void txtKMG2_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtKMG2.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (cmdSET.Enabled) cmdSET.Focus();
            else if (cmdCAN.Enabled) cmdCAN.Focus();
        }
    }

    private void txtKMG2_Leave(object? sender, EventArgs e) => ValidateHalfWidthKana(txtKMG2);

    private void ValidateHalfWidthKana(TextBox txt)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            string trimmed = txt.Text.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                return;
            }

            if (LegacyTextHelper.CheckMojisu(txt.Text, 30) > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txt.Focus());
                return;
            }

            int charCount = trimmed.Length;
            int byteCount = LegacyTextHelper.GetShiftJisByteCount(trimmed);
            if (charCount != byteCount)
            {
                MessageBox.Show("全角が混在しています。半角カナで入力してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txt.Focus());
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private static void AllowDigitsOnly(KeyPressEventArgs e)
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

