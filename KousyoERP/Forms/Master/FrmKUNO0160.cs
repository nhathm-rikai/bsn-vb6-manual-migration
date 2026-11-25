using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0160 — 担当マスタ登録 (VB6 frmKUNO0160 1:1 移行画面)
/// </summary>
public partial class FrmKUNO0160 : Form
{
    private const string ScreenTitle = "KUNO0160";

    private readonly IKuno0160Service _service;

    // 更新登録フラグ (0 = INSERT, 1 = UPDATE)
    private int _upflg;
    private bool _suppressFocusEvents;
    private bool _validationFailedInLeave;

    public FrmKUNO0160()
        : this(CreateDefaultService())
    {
    }

    public FrmKUNO0160(IKuno0160Service service)
    {
        _service = service;
        InitializeComponent();
    }

    private static IKuno0160Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0160Service(
            new Infrastructure.Repositories.KmTantRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    // **********************************
    //  Form_Load
    // **********************************
    private void FrmKUNO0160_Load(object? sender, EventArgs e)
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

        // 更新登録フラグ (0 = INSERT, 1 = UPDATE)
        _upflg = 0;

        BeginInvoke(() => txtTACD.Focus());
    }

    // ***************************************
    //  画面クリア(キー値除く) (All_clear)
    // ***************************************
    private void AllClear()
    {
        lblTACD.Text = string.Empty;
        txtTANMK.Text = string.Empty;
        txtTANMKJ.Text = string.Empty;
        txtTANMS.Text = string.Empty;
        lblBUCD.Text = string.Empty;
        txtBUCD.Text = string.Empty;
    }

    // ***************************************
    //  画面全体クリア＋担当コードへフォーカス
    // ***************************************
    private void ClearAndFocusTaCd()
    {
        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            _upflg = 0;
            AllClear();
            txtTACD.Text = string.Empty;
            cmdOK.Enabled = false;
            cmdDEL.Enabled = false;
            txtTACD.Focus();
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
    //  txtTACD イベント (担当コード)
    // **********************************
    private void txtTACD_GotFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents) return;
        cmdOK.Enabled = false;
        cmdDEL.Enabled = false;
    }

    private void txtTACD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTACD.Text = string.Empty;
            lblTACD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtTANMK.Focus();
        }
    }

    private void txtTACD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        // 担当コード空白削除
        txtTACD.Text = txtTACD.Text.Trim();

        // 担当コード未入力時 (または 0)
        int ktantCd = (int)LegacyTextHelper.VbVal(txtTACD.Text);
        if (string.IsNullOrEmpty(txtTACD.Text) || ktantCd == 0)
        {
            txtTACD.Text = string.Empty;
            return;
        }

        txtTACD.Text = ktantCd.ToString("0000", CultureInfo.InvariantCulture);

        // 担当コード検索キー (Onelist_Set 相当)
        if (!OnelistSet(ktantCd))
        {
            return;
        }

        // ボタン有効
        if (_upflg == 0) // 登録時
        {
            cmdOK.Enabled = true;
            cmdDEL.Enabled = false;
        }
        else if (_upflg == 1) // 修正・削除時
        {
            cmdOK.Enabled = true;
            cmdDEL.Enabled = true;
        }
    }

    // *********************************
    //  担当コード表示処理 (Onelist_Set)
    // *********************************
    private bool OnelistSet(int ktantCd)
    {
        TantRecordDto record;
        try
        {
            record = _service.FindByCode(ktantCd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "担当マスタ", txtTACD.Text, ex);
            Close();
            return false;
        }

        if (!record.Exists)
        {
            // データ未存在 (データ登録モード)
            _upflg = 0;
            AllClear();
            return true;
        }

        // データ存在 (データ修正モード)
        _upflg = 1;

        // 削除対象データ判別
        if (record.IsDeleted)
        {
            DialogResult ret = MessageBox.Show(
                "このレコードは削除されています。復元しますか？",
                ScreenTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ret == DialogResult.No)
            {
                _upflg = 0;
                AllClear();
                txtTACD.Text = string.Empty;
                RedirectFocus(txtTACD, () =>
                {
                    cmdOK.Enabled = false;
                    cmdDEL.Enabled = false;
                });
                return false;
            }
        }

        // テキスト値設定
        lblTACD.Text = record.TantNameS;
        txtTANMK.Text = record.TantNameKn;
        txtTANMKJ.Text = record.TantNameKj;
        txtTANMS.Text = record.TantNameS;

        // 部門コード設定
        if (record.BmCd == 0)
        {
            txtBUCD.Text = string.Empty;
            lblBUCD.Text = string.Empty;
            return true;
        }

        txtBUCD.Text = record.BmCd.ToString("00", CultureInfo.InvariantCulture);
        lblBUCD.Text = record.BmName;
        return true;
    }

    // **********************************
    //  txtTANMK イベント (担当名(カナ))
    // **********************************
    private void txtTANMK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTANMK.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtTANMKJ.Focus();
        }
    }

    private void txtTANMK_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtTANMK.Text, 20) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtTANMK);
        }
    }

    // **********************************
    //  txtTANMKJ イベント (担当名(漢字))
    // **********************************
    private void txtTANMKJ_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTANMKJ.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtTANMS.Focus();
        }
    }

    private void txtTANMKJ_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (string.IsNullOrEmpty(txtTANMKJ.Text.Trim()))
        {
            txtTANMS.Text = string.Empty;
            return;
        }

        if (LegacyTextHelper.CheckMojisu(txtTANMKJ.Text, 30) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtTANMKJ);
            return;
        }

        // 登録コード時 (upflg = 0) は先頭16バイトを略称へ自動セット
        if (_upflg == 0)
        {
            txtTANMS.Text = LegacyTextHelper.SubStringByByte(txtTANMKJ.Text, 16);
        }
    }

    // **********************************
    //  txtTANMS イベント (担当名(略称))
    // **********************************
    private void txtTANMS_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTANMS.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            txtBUCD.Focus();
        }
    }

    private void txtTANMS_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        if (LegacyTextHelper.CheckMojisu(txtTANMS.Text, 16) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtTANMS);
        }
    }

    // **********************************
    //  txtBUCD イベント (部門コード)
    // **********************************
    private void txtBUCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtBUCD.Text = string.Empty;
            lblBUCD.Text = string.Empty;
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

    private void txtBUCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget()) return;

        // 部門コード空白削除
        txtBUCD.Text = txtBUCD.Text.Trim();

        // 部門コード未入力時
        int bcd = (int)LegacyTextHelper.VbVal(txtBUCD.Text);
        if (string.IsNullOrEmpty(txtBUCD.Text) || bcd == 0)
        {
            txtBUCD.Text = string.Empty;
            lblBUCD.Text = string.Empty;
            return;
        }

        txtBUCD.Text = bcd.ToString("00", CultureInfo.InvariantCulture);

        // 部門コード検索キー (get_meis(4, bcd, nm))
        string? nm;
        try
        {
            nm = _service.GetBumonName(bcd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"4,{bcd}", ex);
            Close();
            return;
        }

        if (nm is null)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "指定された部門コードは存在しません。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            txtBUCD.Text = string.Empty;
            lblBUCD.Text = string.Empty;
            RedirectFocus(txtBUCD);
        }
        else
        {
            lblBUCD.Text = nm;
        }
    }

    // *******************************************
    //  担当マスタ登録・更新 (cmdOK_Click)
    // *******************************************
    private void cmdOK_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave) return;

        var input = new Kuno0160SaveInput
        {
            IsUpdate = _upflg == 1,
            TaCdText = txtTACD.Text,
            TaNmkText = txtTANMK.Text,
            TaNmkjText = txtTANMKJ.Text,
            TaNmsText = txtTANMS.Text,
            BuCdText = txtBUCD.Text,
            BuCdLabelText = lblBUCD.Text
        };

        try
        {
            Kuno0160ActionResult result = _service.Save(input, DateTime.Today);
            if (!result.Success)
            {
                MessageBox.Show(
                    result.Message,
                    ScreenTitle,
                    MessageBoxButtons.OK,
                    result.IsExclamation ? MessageBoxIcon.Exclamation : MessageBoxIcon.Information);

                if (result.ClearBuCdOnError)
                {
                    txtBUCD.Text = string.Empty;
                    lblBUCD.Text = string.Empty;
                }

                FocusField(result.ErrorField);
                return;
            }

            MessageBox.Show(result.Message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearAndFocusTaCd();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "担当マスタ", txtTACD.Text, ex);
            Close();
        }
    }

    // **********************************
    //  キャンセル (cmdCAN_Click)
    // **********************************
    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        ClearAndFocusTaCd();
    }

    // **********************************
    //  削除処理 (cmdDEL_Click)
    // **********************************
    private void cmdDEL_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave) return;

        DialogResult ret = MessageBox.Show(
            "削除しますか？",
            ScreenTitle,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (ret == DialogResult.No)
        {
            ClearAndFocusTaCd();
            return;
        }

        int ktantCd = (int)LegacyTextHelper.VbVal(txtTACD.Text);

        try
        {
            Kuno0160ActionResult result = _service.Delete(ktantCd, DateTime.Today);
            MessageBox.Show(result.Message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearAndFocusTaCd();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "担当マスタ", txtTACD.Text, ex);
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

    private void FocusField(Kuno0160Field field)
    {
        switch (field)
        {
            case Kuno0160Field.TaCd:
                txtTACD.Focus();
                break;
            case Kuno0160Field.TaNmk:
                txtTANMK.Focus();
                break;
            case Kuno0160Field.TaNmkj:
                txtTANMKJ.Focus();
                break;
            case Kuno0160Field.TaNms:
                txtTANMS.Focus();
                break;
            case Kuno0160Field.BuCd:
                txtBUCD.Focus();
                break;
        }
    }
}

