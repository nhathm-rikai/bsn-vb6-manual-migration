using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0020 — 商品分類マスタ登録 (VB6 frmKUNO0020 1:1 移行画面)
/// </summary>
public partial class FrmKUNO0020 : Form
{
    private const string ScreenTitle = "KUNO0020";

    private readonly IKuno0020Service _service;

    // フォーム内状態変数 (VB6 frmKUNO0020 と同一)
    private int _upflg;       // 0 = INSERT, 1 = UPDATE
    private int _noflg;       // 0 = 大分類未存在/未確認, 1 = 大分類存在確認済
    private string _sStrName = string.Empty;
    private int _sWbcd;
    private string _sStrSrt = string.Empty;
    private string _sStrSrt2 = string.Empty;
    private string _sStrTekyo = string.Empty;
    private string _sStrDdate = string.Empty;
    private int _sKkb;        // 0 = 月末締切(opt31), 1 = ２０日締切(opt20)

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

        // システム日付設定 (yy/mm/dd)
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        // ボタン無効
        cmdSET.Enabled = false;
        cmdDEL.Enabled = false;

        // デフォルト設定
        opt31.Checked = true;
        _sKkb = 0;

        BeginInvoke(() => txtDCD.Focus());
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
            txtDCD.Text = string.Empty;
            txtTCD.Text = string.Empty;
            txtDTNM.Text = string.Empty;
            txtBCD.Text = string.Empty;
            lblBCD.Text = string.Empty;
            txtSRT.Text = string.Empty;
            txtTEKYO.Text = string.Empty;
            txtSRT2.Text = string.Empty;

            // ボタン無効
            cmdSET.Enabled = false;
            cmdDEL.Enabled = false;

            // デフォルト設定
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
    //  データ表示 (DATA_HYOJI)
    // **********************************
    private void DataHyoji()
    {
        // 更新登録フラグ(0 = INSERT, 1 = UPDATE)
        _upflg = 0;

        // 大・中分類コード入力時チェック
        if (string.IsNullOrWhiteSpace(txtTCD.Text) || string.IsNullOrWhiteSpace(txtDCD.Text))
        {
            cmdSET.Enabled = false;
            cmdDEL.Enabled = false;
            return;
        }

        _upflg = 1; // データ修正

        // 削除対象データ判別
        if (!string.IsNullOrWhiteSpace(_sStrDdate))
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

        // テキスト値設定
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

        // ボタン有効
        cmdSET.Enabled = true;
        cmdDEL.Enabled = true;

        // 経理締切日区分設定
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
    //  商品分類マスタ存在チェック (kmbnri_check)
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
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類マスタ", $"{daiCd},{tyuCd}", ex);
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
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"4,{bmCd}", ex);
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

    // **********************************
    //  txtDCD イベント
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

        // 大分類(中分類00)存在チェック
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
    //  txtTCD イベント
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

        // 大分類未登録時は中分類00以外不可
        if (_noflg == 0 && tyuVal != 0)
        {
            _validationFailedInLeave = true;
            MessageBox.Show(
                "大分類コード（中分類コード00）を登録してください。",
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

        // 編集表示 (VB6: Format$(Trim$(txtTCD), "00") -> 空文字時は "")
        string trimmedTcd = txtTCD.Text.Trim();
        txtTCD.Text = string.IsNullOrEmpty(trimmedTcd)
            ? string.Empty
            : tyuVal.ToString("00", CultureInfo.InvariantCulture);

        int daiVal = (int)LegacyTextHelper.VbVal(txtDCD.Text);

        // 入力チェック: 大分類(00)存在確認
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

        // 入力チェック: 指定された大分類・中分類の存在確認
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

        // データ表示
        _noflg = 1;
        DataHyoji();
    }

    // **********************************
    //  txtDTNM イベント
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
                "文字数オーバーです。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            RedirectFocus(txtDTNM);
        }
    }

    // **********************************
    //  txtBCD イベント
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
                "指定された部門コードは存在しません。",
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
    //  txtSRT / txtTEKYO / txtSRT2 イベント
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
    //  opt31 / opt20 イベント
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
    //  マスタ登録・更新 (cmdSET_Click)
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
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類マスタ", $"{txtDCD.Text},{txtTCD.Text}", ex);
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
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類マスタ", $"{txtDCD.Text},{txtTCD.Text}", ex);
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
