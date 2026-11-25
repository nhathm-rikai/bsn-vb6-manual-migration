using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0060 — 所属マスタ登録 (VB6 frmKUNO0060 1:1 移行画面)
/// </summary>
public partial class FrmKUNO0060 : Form
{
    private const string ScreenTitle = "KUNO0060";

    private readonly IKuno0060Service _service;

    // 更新登録フラグ (0 = INSERT, 1 = UPDATE)
    private int _upflg;
    private bool _suppressFocusEvents;
    private bool _validationFailedInLeave;

    public FrmKUNO0060()
        : this(CreateDefaultService())
    {
    }

    public FrmKUNO0060(IKuno0060Service service)
    {
        _service = service;
        InitializeComponent();
    }

    private static IKuno0060Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0060Service(
            new Infrastructure.Repositories.KmSyzkRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory),
            new Infrastructure.Repositories.KmGrpfRepository(factory));
    }

    // **********************************
    //  Form_Load
    // **********************************
    private void FrmKUNO0060_Load(object? sender, EventArgs e)
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

        // ボタン無効
        cmdOK.Enabled = false;
        cmdDEL.Enabled = false;

        BeginInvoke(() => txtSYCD.Focus());
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
            txtSYCD.Text = string.Empty;
            txtkubun.Text = string.Empty;
            lblkubun.Text = string.Empty;
            txtSYNMK.Text = string.Empty;
            txtSYNMKJ.Text = string.Empty;
            txtSYNMS.Text = string.Empty;
            txtBLK.Text = string.Empty;
            lblblk.Text = string.Empty;
            TxtSKAN.Text = string.Empty;
            lblskan.Text = string.Empty;
            txtGCD.Text = string.Empty;
            lblGCD.Text = string.Empty;

            cmdOK.Enabled = false;
            cmdDEL.Enabled = false;

            txtSYCD.Focus();
        }
        finally
        {
            _suppressFocusEvents = prev;
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
    //  txtSYCD イベント (所属コード)
    // **********************************
    private void txtSYCD_GotFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents) return;
        cmdOK.Enabled = false;
        cmdDEL.Enabled = false;
    }

    private void txtSYCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSYCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtkubun.Focus();
        }
    }

    private void txtSYCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        // 編集表示 (VB6: Format$(Trim$(txtSYCD), "000") -> 空文字時は "")
        string trimmed = txtSYCD.Text.Trim();
        int kszCd = (int)LegacyTextHelper.VbVal(trimmed);
        txtSYCD.Text = string.IsNullOrEmpty(trimmed)
            ? string.Empty
            : kszCd.ToString("000", CultureInfo.InvariantCulture);

        _upflg = 0;

        SyzkRecordDto record;
        try
        {
            record = _service.FindByCode(kszCd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "所属マスタ", txtSYCD.Text, ex);
            Close();
            return;
        }

        if (!record.Exists)
        {
            // 新規登録モード
            txtkubun.Text = string.Empty;
            lblkubun.Text = string.Empty;
            txtSYNMK.Text = string.Empty;
            txtSYNMKJ.Text = string.Empty;
            txtSYNMS.Text = string.Empty;
            txtBLK.Text = string.Empty;
            lblblk.Text = string.Empty;
            TxtSKAN.Text = string.Empty;
            lblskan.Text = string.Empty;
            txtGCD.Text = string.Empty;
            lblGCD.Text = string.Empty;

            cmdOK.Enabled = true;
            cmdDEL.Enabled = false;
            return;
        }

        // データ修正モード
        _upflg = 1;

        if (record.IsDeleted)
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

        txtSYCD.Text = record.KSzCd.ToString("000", CultureInfo.InvariantCulture);
        txtkubun.Text = record.SzKb.ToString("0", CultureInfo.InvariantCulture);
        lblkubun.Text = record.SzKbName;

        txtSYNMK.Text = record.SzNameKn;
        txtSYNMKJ.Text = record.SzNameKj;
        txtSYNMS.Text = record.SzNameS;

        txtBLK.Text = record.BrokCd == 0
            ? string.Empty
            : record.BrokCd.ToString("0", CultureInfo.InvariantCulture);
        lblblk.Text = record.BrokName;

        TxtSKAN.Text = record.SyuknCd == 0
            ? string.Empty
            : record.SyuknCd.ToString("00", CultureInfo.InvariantCulture);
        lblskan.Text = record.SyuknName;

        txtGCD.Text = record.GpkCd == 0
            ? string.Empty
            : record.GpkCd.ToString("000", CultureInfo.InvariantCulture);
        lblGCD.Text = record.GpkName;

        cmdOK.Enabled = true;
        cmdDEL.Enabled = true;
    }

    // **********************************
    //  txtkubun イベント (所属区分)
    // **********************************
    private void txtkubun_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtkubun.Text = string.Empty;
            lblkubun.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSYNMK.Focus();
        }
    }

    private void txtkubun_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        string trimmed = txtkubun.Text.Trim();
        int kubunVal = (int)LegacyTextHelper.VbVal(trimmed);
        txtkubun.Text = string.IsNullOrEmpty(trimmed)
            ? string.Empty
            : kubunVal.ToString("0", CultureInfo.InvariantCulture);

        string? kubunName;
        try
        {
            kubunName = _service.GetKubunName(kubunVal);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"8,{kubunVal}", ex);
            Close();
            return;
        }

        if (kubunName is null)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "指定された所属区分は存在しません。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txtkubun.Text = string.Empty;
            lblkubun.Text = string.Empty;
            RedirectFocus(txtkubun);
        }
        else
        {
            lblkubun.Text = kubunName;
        }
    }

    // **********************************
    //  txtSYNMK イベント (所属名称(カナ))
    // **********************************
    private void txtSYNMK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSYNMK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSYNMKJ.Focus();
        }
    }

    private void txtSYNMK_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtSYNMK.Text, 20) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtSYNMK);
        }
    }

    // **********************************
    //  txtSYNMKJ イベント (所属名称(漢字))
    // **********************************
    private void txtSYNMKJ_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSYNMKJ.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtSYNMS.Focus();
        }
    }

    private void txtSYNMKJ_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtSYNMKJ.Text, 30) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtSYNMKJ);
            return;
        }

        // 新規登録時(upflg = 0)かつ漢字名称入力時は先頭16バイトを所属略称へ自動セット
        if (!string.IsNullOrEmpty(txtSYNMKJ.Text) && _upflg == 0)
        {
            txtSYNMS.Text = LegacyTextHelper.SubStringByByte(txtSYNMKJ.Text.Trim(), 16);
        }
    }

    // **********************************
    //  txtSYNMS イベント (所属略称)
    // **********************************
    private void txtSYNMS_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSYNMS.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtBLK.Focus();
        }
    }

    private void txtSYNMS_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtSYNMS.Text, 16) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtSYNMS);
        }
    }

    // **********************************
    //  txtBLK イベント (ブロック)
    // **********************************
    private void txtBLK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtBLK.Text = string.Empty;
            lblblk.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            TxtSKAN.Focus();
        }
    }

    private void txtBLK_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        int blkVal = (int)LegacyTextHelper.VbVal(txtBLK.Text.Trim());
        if (blkVal == 0)
        {
            txtBLK.Text = string.Empty;
            lblblk.Text = string.Empty;
            return;
        }

        txtBLK.Text = blkVal.ToString("0", CultureInfo.InvariantCulture);

        string? blkName;
        try
        {
            blkName = _service.GetBlockName(blkVal);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"1,{blkVal}", ex);
            Close();
            return;
        }

        if (blkName is null)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "指定されたブロックコードは存在しません。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txtBLK.Text = string.Empty;
            lblblk.Text = string.Empty;
            RedirectFocus(txtBLK);
        }
        else
        {
            lblblk.Text = blkName;
        }
    }

    // **********************************
    //  TxtSKAN イベント (主管)
    // **********************************
    private void TxtSKAN_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            TxtSKAN.Text = string.Empty;
            lblskan.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtGCD.Focus();
        }
    }

    private void TxtSKAN_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        int skanVal = (int)LegacyTextHelper.VbVal(TxtSKAN.Text.Trim());
        if (skanVal == 0)
        {
            TxtSKAN.Text = string.Empty;
            lblskan.Text = string.Empty;
            return;
        }

        TxtSKAN.Text = skanVal.ToString("00", CultureInfo.InvariantCulture);

        string? skanName;
        try
        {
            skanName = _service.GetSyukanName(skanVal);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"2,{skanVal}", ex);
            Close();
            return;
        }

        if (skanName is null)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "指定された主管コードは存在しません。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSKAN.Text = string.Empty;
            lblskan.Text = string.Empty;
            RedirectFocus(TxtSKAN);
        }
        else
        {
            lblskan.Text = skanName;
        }
    }

    // **********************************
    //  txtGCD イベント (グループ会社コード)
    // **********************************
    private void txtGCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtGCD.Text = string.Empty;
            lblGCD.Text = string.Empty;
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

    private void txtGCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        int gcdVal = (int)LegacyTextHelper.VbVal(txtGCD.Text.Trim());
        if (gcdVal == 0)
        {
            txtGCD.Text = string.Empty;
            lblGCD.Text = string.Empty;
            return;
        }

        txtGCD.Text = gcdVal.ToString("000", CultureInfo.InvariantCulture);

        string? gcdName;
        try
        {
            gcdName = _service.GetGroupCompanyName(gcdVal);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "グループ会社マスタ", $"{gcdVal}", ex);
            Close();
            return;
        }

        if (gcdName is null)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "指定されたグループ会社コードは存在しません。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txtGCD.Text = string.Empty;
            lblGCD.Text = string.Empty;
            RedirectFocus(txtGCD);
        }
        else
        {
            lblGCD.Text = gcdName;
        }
    }

    // **********************************
    //  マスタ登録・更新 (cmdOK_Click)
    // **********************************
    private void cmdOK_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave) return;

        var input = new Kuno0060SaveInput
        {
            IsUpdate = _upflg == 1,
            SyCdText = txtSYCD.Text,
            KubunText = txtkubun.Text,
            KubunLabelText = lblkubun.Text,
            SyNmkText = txtSYNMK.Text,
            SyNmkjText = txtSYNMKJ.Text,
            SyNmsText = txtSYNMS.Text,
            BlkText = txtBLK.Text,
            BlkLabelText = lblblk.Text,
            SkanText = TxtSKAN.Text,
            SkanLabelText = lblskan.Text,
            GcdText = txtGCD.Text,
            GcdLabelText = lblGCD.Text
        };

        try
        {
            Kuno0060ActionResult result = _service.Save(input, DateTime.Today);
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
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "所属マスタ", txtSYCD.Text, ex);
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
    //  削除処理 (cmdDEL_Click)
    // **********************************
    private void cmdDEL_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave) return;

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

        int kszCd = (int)LegacyTextHelper.VbVal(txtSYCD.Text);

        try
        {
            Kuno0060ActionResult result = _service.Delete(kszCd, DateTime.Today);
            MessageBox.Show(result.Message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataClear();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "所属マスタ", txtSYCD.Text, ex);
            Close();
        }
    }

    // **********************************
    //  処理終了 (cmdEND_Click)
    // **********************************
    private void cmdEND_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        Close();
    }

    private void FocusField(Kuno0060Field field)
    {
        switch (field)
        {
            case Kuno0060Field.SyCd:
                txtSYCD.Focus();
                break;
            case Kuno0060Field.Kubun:
                txtkubun.Focus();
                break;
            case Kuno0060Field.SyNmk:
                txtSYNMK.Focus();
                break;
            case Kuno0060Field.SyNmkj:
                txtSYNMKJ.Focus();
                break;
            case Kuno0060Field.SyNms:
                txtSYNMS.Focus();
                break;
            case Kuno0060Field.Blk:
                txtBLK.Focus();
                break;
            case Kuno0060Field.Skan:
                TxtSKAN.Focus();
                break;
            case Kuno0060Field.Gcd:
                txtGCD.Focus();
                break;
        }
    }
}

