using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;
using KousyoERP.Forms.Common;

namespace KousyoERP.Forms.Zaiko;

/// <summary>
/// KZKO0020 — 在庫調整データ登録 (VB6 frmKZKO0020 1:1 移行画面)
/// </summary>
public partial class FrmKZKO0020 : Form
{
    private const string ScreenTitle = "KZKO0020";

    private readonly IKzko0020Service _service;

    // フォーム内グローバル変数 (VB6 glbCYY / glbCMM / glbCDD / glbswkflg / glbKEIRIDDS / svKEIYM / svKEIYMD)
    private string _glbCYY = string.Empty;
    private string _glbCMM = string.Empty;
    private string _glbCDD = string.Empty;
    private int _glbswkflg;
    private int _glbKEIRIDDS;
    private long _svKEIYM;
    private long _svKEIYMD;

    private bool _suppressFocusEvents;
    private bool _validationFailedInLeave;

    public FrmKZKO0020()
        : this(CreateDefaultService())
    {
    }

    public FrmKZKO0020(IKzko0020Service service)
    {
        _service = service;
        InitializeComponent();
    }

    private static IKzko0020Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kzko0020Service(
            new Infrastructure.Repositories.KmLockRepository(factory),
            new Infrastructure.Repositories.KtZjnlRepository(factory),
            new Infrastructure.Repositories.KmTantRepository(factory),
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    // **********************************
    //  Form_Load
    // **********************************
    private void FrmKZKO0020_Load(object? sender, EventArgs e)
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

        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        Kzko0020InitContextDto initContext;
        try
        {
            initContext = _service.GetInitContext();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "ロックマスタ", "1", ex);
            Close();
            return;
        }

        _svKEIYM = initContext.SvKeiYm;
        _svKEIYMD = initContext.SvKeiYmd;
        _glbCYY = initContext.GlbCyy;
        _glbCMM = initContext.GlbCmm;
        _glbCDD = initContext.GlbCdd;
        lblYMD.Text = initContext.SystemDateText;

        // 画面内容クリア
        DataClear();

        // 伝票入力無効・ボタン初期状態
        txtDENNO.Enabled = false;
        cmdOK.Enabled = false;
        cmdINS.Enabled = true;
        cmdUPD.Enabled = true;
        cmdDEL.Enabled = true;

        // 登録ボタンへフォーカス移動
        BeginInvoke(() => cmdINS.Focus());
    }

    private bool IsBypassValidationTarget()
    {
        return ActiveControl == cmdCAN || ActiveControl == cmdEND;
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
    //  画面内容クリア (data_clear)
    // **********************************
    private void DataClear()
    {
        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            txtDENNO.Text = string.Empty;
            txtNTANCD.Text = string.Empty;
            lblNTANNM.Text = string.Empty;
            txtCYY.Text = string.Empty;
            txtCMM.Text = string.Empty;
            txtCDD.Text = string.Empty;
            txtSYOCD.Text = string.Empty;
            lblSYONM.Text = string.Empty;
            txtCHKBN.Text = string.Empty;
            txtNEWZSUU.Text = string.Empty;
            txtNEWZSUU.ForeColor = Color.Black;
            txtNEWZBOKA.Text = string.Empty;
            txtNEWZBOKA.ForeColor = Color.Black;
            txtTEKIYO.Text = string.Empty;

            txtCYY.Text = _glbCYY;
            txtCMM.Text = _glbCMM;
            txtCDD.Text = _glbCDD;
        }
        finally
        {
            _suppressFocusEvents = prev;
        }
    }

    // **********************************
    //  モードボタン・機能ボタンイベント
    // **********************************
    private void cmdINS_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        DataClear();

        cmdUPD.Enabled = false;
        cmdDEL.Enabled = false;
        cmdOK.Enabled = true;
        txtDENNO.Enabled = false;

        txtNTANCD.Focus();
    }

    private void cmdUPD_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        DataClear();

        cmdUPD.Enabled = true;
        cmdINS.Enabled = false;
        cmdDEL.Enabled = false;
        cmdOK.Enabled = false;

        txtDENNO.Enabled = true;
        txtDENNO.Focus();
    }

    private void cmdDEL_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        DataClear();

        cmdDEL.Enabled = true;
        cmdINS.Enabled = false;
        cmdUPD.Enabled = false;
        cmdOK.Enabled = false;

        txtDENNO.Enabled = true;
        txtDENNO.Focus();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        DataClear();

        txtDENNO.Enabled = false;
        cmdOK.Enabled = false;
        cmdINS.Enabled = true;
        cmdUPD.Enabled = true;
        cmdDEL.Enabled = true;

        cmdINS.Focus();
    }

    private void cmdEND_Click(object? sender, EventArgs e)
    {
        _validationFailedInLeave = false;
        Close();
    }

    private void cmdOK_Click(object? sender, EventArgs e)
    {
        if (_validationFailedInLeave)
        {
            return;
        }

        // 削除
        if (cmdDEL.Enabled)
        {
            FileDelete();
            return;
        }

        // 登録
        if (cmdINS.Enabled)
        {
            if (!InCheck())
            {
                return;
            }
            FileWrite();
            return;
        }

        // 修正
        if (cmdUPD.Enabled)
        {
            if (!InCheck())
            {
                return;
            }
            FileUpdate();
        }
    }

    // **********************************
    //  入力担当者参照 (cmdTANKEN_Click)
    // **********************************
    private void cmdTANKEN_Click(object? sender, EventArgs e)
    {
        using var dlg = new FrmTANKEN(_service);
        dlg.ShowDialog(this);

        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            if (dlg.Selected)
            {
                txtNTANCD.Text = dlg.SelectedTantCode;
                lblNTANNM.Text = dlg.SelectedTantShortName;
            }
            else
            {
                txtNTANCD.Text = dlg.SelectedTantCode;
                lblNTANNM.Text = string.Empty;
            }
        }
        finally
        {
            _suppressFocusEvents = prev;
        }

        txtNTANCD.Focus();
    }

    // **********************************
    //  商品コード参照 (cmdSYOKEN_Click)
    // **********************************
    private void cmdSYOKEN_Click(object? sender, EventArgs e)
    {
        using var dlg = new FrmSYOKEN(_service);
        dlg.ShowDialog(this);

        if (dlg.Selected)
        {
            txtSYOCD.Text = dlg.SelectedProductCode;
            lblSYONM.Text = dlg.SelectedProductName;
            txtSYOCD_LostFocus(txtSYOCD, EventArgs.Empty);
        }
        else
        {
            txtSYOCD.Text = dlg.SelectedProductCode;
            lblSYONM.Text = string.Empty;
        }

        txtSYOCD.Focus();
    }

    // **********************************
    //  マスタ削除処理 (file_delete)
    // **********************************
    private void FileDelete()
    {
        DialogResult ret = MessageBox.Show(
            "削除しますか？",
            ScreenTitle,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (ret == DialogResult.No)
        {
            DataClear();
            txtDENNO.Focus();
            return;
        }

        long kdenNo = (long)LegacyTextHelper.VbVal(txtDENNO.Text);
        string rawDenNoText = txtDENNO.Text;
        string errKey = kdenNo.ToString("0000000", CultureInfo.InvariantCulture);

        try
        {
            _service.DeleteAdjustment(kdenNo);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "在庫調整データ", errKey, ex);
            return;
        }

        MessageBox.Show(
            $"伝票NO.= {rawDenNoText}を削除しました。",
            ScreenTitle,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);

        DataClear();
        txtDENNO.Focus();
    }

    // **********************************
    //  マスタ更新処理 (file_update)
    // **********************************
    private void FileUpdate()
    {
        long kdenNo = (long)LegacyTextHelper.VbVal(txtDENNO.Text);
        string rawDenNoText = txtDENNO.Text;
        string errKey = kdenNo.ToString("0000000", CultureInfo.InvariantCulture);

        int stantCd = (int)LegacyTextHelper.VbVal(txtNTANCD.Text);
        long choDay = ParseChoDay();
        long shnCd = (long)LegacyTextHelper.VbVal(txtSYOCD.Text);
        int choku = (int)LegacyTextHelper.VbVal(txtCHKBN.Text);
        decimal absSuu = Math.Round(LegacyTextHelper.FuncCdbl(txtNEWZSUU.Text), 2, MidpointRounding.AwayFromZero);
        decimal absKin = Math.Round(LegacyTextHelper.FuncCdbl(txtNEWZBOKA.Text), 2, MidpointRounding.AwayFromZero);
        string tekiyo = txtTEKIYO.Text;

        try
        {
            _service.UpdateAdjustment(kdenNo, stantCd, choDay, shnCd, choku, absSuu, absKin, tekiyo);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "在庫調整ジャーナル", errKey, ex);
            return;
        }

        MessageBox.Show(
            $"伝票NO.= {rawDenNoText}を更新しました。",
            ScreenTitle,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);

        DataClear();
        txtDENNO.Focus();
    }

    // **********************************
    //  マスタ登録処理 (file_write)
    // **********************************
    private void FileWrite()
    {
        int stantCd = (int)LegacyTextHelper.VbVal(txtNTANCD.Text);
        long choDay = ParseChoDay();
        long shnCd = (long)LegacyTextHelper.VbVal(txtSYOCD.Text);
        int choku = (int)LegacyTextHelper.VbVal(txtCHKBN.Text);
        decimal absSuu = Math.Round(LegacyTextHelper.FuncCdbl(txtNEWZSUU.Text), 2, MidpointRounding.AwayFromZero);
        decimal absKin = Math.Round(LegacyTextHelper.FuncCdbl(txtNEWZBOKA.Text), 2, MidpointRounding.AwayFromZero);
        string tekiyo = txtTEKIYO.Text;

        long assignedDenNo = 0;
        long dfDenNo;
        try
        {
            dfDenNo = _service.InsertAdjustment(
                stantCd,
                choDay,
                shnCd,
                choku,
                absSuu,
                absKin,
                tekiyo,
                out assignedDenNo);
        }
        catch (Exception ex)
        {
            if (assignedDenNo == 0)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "伝票採番", string.Empty, ex);
            }
            else
            {
                LegacyErrorDialog.ShowSqlError(
                    ScreenTitle,
                    "在庫調整ジャーナル",
                    assignedDenNo.ToString("0000000", CultureInfo.InvariantCulture),
                    ex);
            }
            return;
        }

        MessageBox.Show(
            $"伝票NO.= {dfDenNo:0000000}を登録しました。",
            ScreenTitle,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);

        // 画面内容クリア（入力担当コードと調整日は保持し、商品コード以下のみクリア）
        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            txtSYOCD.Text = string.Empty;
            lblSYONM.Text = string.Empty;
            txtCHKBN.Text = string.Empty;
            txtNEWZSUU.Text = string.Empty;
            txtNEWZSUU.ForeColor = Color.Black;
            txtNEWZBOKA.Text = string.Empty;
            txtNEWZBOKA.ForeColor = Color.Black;
            txtTEKIYO.Text = string.Empty;
        }
        finally
        {
            _suppressFocusEvents = prev;
        }

        txtSYOCD.Focus();
    }

    // **********************************
    //  入力チェック (in_check)
    // **********************************
    private bool InCheck()
    {
        string rawTan = txtNTANCD.Text.Trim();
        if (!string.IsNullOrEmpty(rawTan))
        {
            int tanCd = (int)LegacyTextHelper.VbVal(rawTan);
            string? tanName;
            try
            {
                tanName = _service.GetActiveTantShortName(tanCd);
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "担当マスタ", tanCd.ToString(CultureInfo.InvariantCulture), ex);
                return false;
            }

            if (tanName is null)
            {
                MessageBox.Show("指定された入力担当ｺｰﾄﾞは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNTANCD.Focus();
                return false;
            }
        }

        if (string.IsNullOrEmpty(rawTan))
        {
            MessageBox.Show("入力担当コードを設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNTANCD.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(txtCYY.Text.Trim()))
        {
            MessageBox.Show("調整日を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtCYY.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(txtCMM.Text.Trim()))
        {
            MessageBox.Show("調整日を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtCMM.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(txtCDD.Text.Trim()))
        {
            MessageBox.Show("調整日を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtCDD.Focus();
            return false;
        }

        int yy = (int)LegacyTextHelper.VbVal(txtCYY.Text);
        int mm = (int)LegacyTextHelper.VbVal(txtCMM.Text);
        int dd = (int)LegacyTextHelper.VbVal(txtCDD.Text);
        string dateText = $"{yy:0000}/{mm:00}/{dd:00}";
        if (!DateTime.TryParseExact(dateText, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            MessageBox.Show("入力された調整日に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtCDD.Focus();
            return false;
        }

        string rawSyo = txtSYOCD.Text.Trim();
        if (string.IsNullOrEmpty(rawSyo))
        {
            MessageBox.Show("商品コードを設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSYOCD.Focus();
            return false;
        }

        long syoCd = (long)LegacyTextHelper.VbVal(rawSyo);
        if (!TryValidateAndLoadProduct(syoCd, out _))
        {
            txtSYOCD.Focus();
            return false;
        }

        // 経理締め日チェック
        UpdateSvKeiYmdByKeiridds();
        long inputYmd = yy * 10000L + mm * 100L + dd;
        if (inputYmd < _svKEIYMD)
        {
            MessageBox.Show("指定された日付は経理締め日以前の為登録できません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtCYY.Focus();
            return false;
        }

        // 調整区分
        int chKbn = (int)LegacyTextHelper.VbVal(txtCHKBN.Text);
        if (chKbn is not (10 or 11 or 12 or 13 or 21 or 22 or 23))
        {
            MessageBox.Show("調整区分に入力エラ－です。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtCHKBN.Text = string.Empty;
            txtCHKBN.Focus();
            return false;
        }

        // 数量
        if (string.IsNullOrEmpty(txtNEWZSUU.Text.Trim()))
        {
            MessageBox.Show("数量を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZSUU.Text = string.Empty;
            txtNEWZSUU.Focus();
            return false;
        }

        if (txtNEWZSUU.Text.Contains('-'))
        {
            MessageBox.Show("数量にマイナスは設定できません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZSUU.Text = string.Empty;
            txtNEWZSUU.Focus();
            return false;
        }

        // 金額
        if (string.IsNullOrEmpty(txtNEWZBOKA.Text.Trim()))
        {
            MessageBox.Show("金額を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZBOKA.Focus();
            return false;
        }

        if (txtNEWZBOKA.Text.Contains('-'))
        {
            MessageBox.Show("金額にマイナスは設定できません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZBOKA.Text = string.Empty;
            txtNEWZBOKA.Focus();
            return false;
        }

        if (LegacyTextHelper.VbVal(txtNEWZSUU.Text) == 0m && LegacyTextHelper.VbVal(txtNEWZBOKA.Text) == 0m)
        {
            MessageBox.Show("数量､簿価が設定されていません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZSUU.Text = string.Empty;
            txtNEWZSUU.Focus();
            return false;
        }

        return true;
    }

    private void UpdateSvKeiYmdByKeiridds()
    {
        long baseYm = _svKEIYMD / 100L;
        _svKEIYMD = _glbKEIRIDDS == 0
            ? baseYm * 100L + 99L
            : baseYm * 100L + 21L;
    }

    private long ParseChoDay()
    {
        int yy = (int)LegacyTextHelper.VbVal(txtCYY.Text);
        int mm = (int)LegacyTextHelper.VbVal(txtCMM.Text);
        int dd = (int)LegacyTextHelper.VbVal(txtCDD.Text);
        return yy * 10000L + mm * 100L + dd;
    }

    private bool TryValidateAndLoadProduct(long dwCd, out string productNameKn)
    {
        productNameKn = string.Empty;
        _glbKEIRIDDS = 0;

        Kzko0020ProductCheckResult result;
        try
        {
            result = _service.CheckProduct(dwCd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品マスタ", dwCd.ToString(CultureInfo.InvariantCulture), ex);
            return false;
        }

        if (result.Status != Kzko0020ProductCheckStatus.Ok)
        {
            MessageBox.Show(result.ErrorMessage, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        _glbKEIRIDDS = result.Keiridds;
        productNameKn = result.ProductNameKn;
        return true;
    }

    // **********************************
    //  スピンボタンイベント
    // **********************************
    private void spnCYY_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtCYY.Text.Trim());
        if (val != 0)
        {
            txtCYY.Text = (val - 1).ToString("0000", CultureInfo.InvariantCulture);
        }
        txtCYY.Focus();
        txtCYY.Refresh();
    }

    private void spnCYY_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtCYY.Text.Trim();
        if (!string.IsNullOrEmpty(trimmed))
        {
            int val = (int)LegacyTextHelper.VbVal(trimmed);
            txtCYY.Text = (val + 1).ToString("0000", CultureInfo.InvariantCulture);
        }
        txtCYY.Focus();
        txtCYY.Refresh();
    }

    private void spnCMM_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtCMM.Text.Trim());
        if (val != 1 && val > 1)
        {
            txtCMM.Text = (val - 1).ToString("00", CultureInfo.InvariantCulture);
        }
        txtCMM.Focus();
        txtCMM.Refresh();
    }

    private void spnCMM_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtCMM.Text.Trim();
        int val = (int)LegacyTextHelper.VbVal(trimmed);
        if (!string.IsNullOrEmpty(trimmed) && val != 12 && val < 12)
        {
            txtCMM.Text = (val + 1).ToString("00", CultureInfo.InvariantCulture);
        }
        txtCMM.Focus();
        txtCMM.Refresh();
    }

    private void spnCDD_SpinDown(object? sender, EventArgs e)
    {
        int val = (int)LegacyTextHelper.VbVal(txtCDD.Text.Trim());
        if (val != 1 && val > 1)
        {
            txtCDD.Text = (val - 1).ToString("00", CultureInfo.InvariantCulture);
        }
        txtCDD.Focus();
        txtCDD.Refresh();
    }

    private void spnCDD_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtCDD.Text.Trim();
        int val = (int)LegacyTextHelper.VbVal(trimmed);
        if (!string.IsNullOrEmpty(trimmed) && val != 31 && val < 31)
        {
            txtCDD.Text = (val + 1).ToString("00", CultureInfo.InvariantCulture);
        }
        txtCDD.Focus();
        txtCDD.Refresh();
    }

    // **********************************
    //  各テキストボックス KeyDown / KeyPress / LostFocus
    // **********************************
    private void txtDENNO_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtDENNO.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtNTANCD.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtDENNO_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtDENNO_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        long dfDenNo = (long)LegacyTextHelper.VbVal(txtDENNO.Text.Trim());
        if (dfDenNo == 0)
        {
            txtDENNO.Text = string.Empty;
            cmdOK.Enabled = false;
            return;
        }

        txtDENNO.Text = dfDenNo.ToString("0000000", CultureInfo.InvariantCulture);

        Kzko0020ZjnlLoadResult loadResult;
        try
        {
            loadResult = _service.LoadZjnl(dfDenNo);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "在庫調整ｼﾞｬｰﾅﾙ", $"{dfDenNo}検索", ex);
            return;
        }

        if (loadResult.Status == Kzko0020ZjnlLoadStatus.NotFound)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("指定された伝票番号は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            DataClear();
            RedirectFocus(txtDENNO);
            return;
        }

        _glbswkflg = loadResult.SwkFlg;
        if (loadResult.Status == Kzko0020ZjnlLoadStatus.AlreadyJournalized || _glbswkflg == 1)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("既に仕訳伝票が作成されているので修正できません｡", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            DataClear();
            RedirectFocus(txtDENNO);
            return;
        }

        bool prev = _suppressFocusEvents;
        _suppressFocusEvents = true;
        try
        {
            txtNTANCD.Text = loadResult.NTanCdText;
            lblNTANNM.Text = loadResult.NTanNameText;
            txtCYY.Text = loadResult.CyyText;
            txtCMM.Text = loadResult.CmmText;
            txtCDD.Text = loadResult.CddText;
            txtSYOCD.Text = loadResult.SyoCdText;

            if (!string.IsNullOrEmpty(loadResult.ProductWarningMessage))
            {
                MessageBox.Show(loadResult.ProductWarningMessage, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblSYONM.Text = string.Empty;
            }
            else
            {
                lblSYONM.Text = loadResult.SyoNameText;
            }

            _glbKEIRIDDS = loadResult.Keiridds;
            txtCHKBN.Text = loadResult.ChKbnText;

            int chkVal = (int)LegacyTextHelper.VbVal(txtCHKBN.Text);
            if (chkVal is 10 or 11 or 12 or 13)
            {
                txtNEWZSUU.ForeColor = Color.Red;
                txtNEWZBOKA.ForeColor = Color.Red;
            }
            else if (chkVal is 21 or 22 or 23)
            {
                txtNEWZSUU.ForeColor = Color.Black;
                txtNEWZBOKA.ForeColor = Color.Black;
            }

            txtNEWZSUU.Text = loadResult.NewZSuuText;
            txtNEWZBOKA.Text = loadResult.NewZBokaText;
            txtTEKIYO.Text = loadResult.TekiyoText;
        }
        finally
        {
            _suppressFocusEvents = prev;
        }

        // 経理締め日チェック
        UpdateSvKeiYmdByKeiridds();
        long choYmd = ParseChoDay();
        if (choYmd < _svKEIYMD)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("経理締め後の伝票は修正・削除できません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            DataClear();
            RedirectFocus(txtDENNO);
            return;
        }

        cmdOK.Enabled = true;
    }

    private void txtNTANCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtNTANCD.Text = string.Empty;
            lblNTANNM.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtCYY.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtNTANCD_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtNTANCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        int wCd = (int)LegacyTextHelper.VbVal(txtNTANCD.Text.Trim());
        if (wCd == 0)
        {
            txtNTANCD.Text = string.Empty;
            lblNTANNM.Text = string.Empty;
            return;
        }

        txtNTANCD.Text = wCd.ToString("0000", CultureInfo.InvariantCulture);

        string? shortName;
        try
        {
            shortName = _service.GetActiveTantShortName(wCd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "担当マスタ", wCd.ToString(CultureInfo.InvariantCulture), ex);
            return;
        }

        if (shortName is null)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("指定された入力担当ｺｰﾄﾞは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNTANCD.Text = string.Empty;
            lblNTANNM.Text = string.Empty;
            RedirectFocus(txtNTANCD);
        }
        else
        {
            lblNTANNM.Text = shortName;
        }
    }

    private void txtCYY_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtCYY.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtCMM.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtCYY_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtCYY_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        string trimmed = txtCYY.Text.Trim();
        if (!string.IsNullOrEmpty(trimmed))
        {
            int yy = (int)LegacyTextHelper.VbVal(trimmed);
            txtCYY.Text = yy.ToString("0000", CultureInfo.InvariantCulture);
        }
    }

    private void txtCMM_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtCMM.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtCDD.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtCMM_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtCMM_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        string strMm = txtCMM.Text.Trim();
        if (string.IsNullOrEmpty(strMm))
        {
            return;
        }

        int mm = (int)LegacyTextHelper.VbVal(strMm);
        if (mm < 1 || mm > 12)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("指定された月は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtCMM.Text = string.Empty;
            RedirectFocus(txtCMM);
            return;
        }

        txtCMM.Text = mm.ToString("00", CultureInfo.InvariantCulture);
    }

    private void txtCDD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtCDD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtSYOCD.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtCDD_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtCDD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        string strMm = txtCMM.Text.Trim();
        string strDd = txtCDD.Text.Trim();
        if (string.IsNullOrEmpty(strDd) || string.IsNullOrEmpty(strMm))
        {
            return;
        }

        int mm = (int)LegacyTextHelper.VbVal(strMm);
        int dd = (int)LegacyTextHelper.VbVal(strDd);
        bool invalid = mm switch
        {
            4 or 6 or 9 or 11 => dd < 1 || dd > 30,
            1 or 3 or 5 or 7 or 8 or 10 or 12 => dd < 1 || dd > 31,
            2 => dd < 1 || dd > 29,
            _ => false
        };

        if (invalid)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("指定された日は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtCDD.Text = string.Empty;
            RedirectFocus(txtCDD);
            return;
        }

        txtCDD.Text = dd.ToString("00", CultureInfo.InvariantCulture);
    }

    private void txtSYOCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtSYOCD.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtCHKBN.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtSYOCD_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtSYOCD_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        long dwCd = (long)LegacyTextHelper.VbVal(txtSYOCD.Text.Trim());
        if (dwCd == 0)
        {
            txtSYOCD.Text = string.Empty;
            lblSYONM.Text = string.Empty;
            return;
        }

        txtSYOCD.Text = dwCd.ToString("00000000", CultureInfo.InvariantCulture);

        if (!TryValidateAndLoadProduct(dwCd, out string strName))
        {
            _validationFailedInLeave = true;
            txtSYOCD.Text = string.Empty;
            lblSYONM.Text = string.Empty;
            RedirectFocus(txtSYOCD);
            return;
        }

        lblSYONM.Text = strName;
    }

    private void txtCHKBN_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtCHKBN.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtNEWZSUU.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtCHKBN_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '.')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtCHKBN_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        decimal rawVal = LegacyTextHelper.VbVal(txtCHKBN.Text.Trim());
        if (rawVal == 0m)
        {
            txtCHKBN.Text = string.Empty;
            return;
        }

        int chKbn = (int)Math.Round(rawVal, MidpointRounding.AwayFromZero);
        txtCHKBN.Text = chKbn.ToString("00", CultureInfo.InvariantCulture);

        switch (chKbn)
        {
            case 10:
            case 11:
            case 12:
            case 13:
                txtNEWZSUU.ForeColor = Color.Red;
                txtNEWZBOKA.ForeColor = Color.Red;
                break;

            case 21:
            case 22:
            case 23:
                txtNEWZSUU.ForeColor = Color.Black;
                txtNEWZBOKA.ForeColor = Color.Black;
                break;

            default:
                _validationFailedInLeave = true;
                MessageBox.Show("入力された調整区分に誤りがあります", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCHKBN.Text = string.Empty;
                RedirectFocus(txtCHKBN);
                break;
        }
    }

    private void txtNEWZSUU_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtNEWZSUU.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtNEWZBOKA.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtNEWZSUU_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '.')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtNEWZSUU_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        string raw = txtNEWZSUU.Text;
        string trimmed = raw.Trim();
        string clean = trimmed.Replace(",", string.Empty);

        int firstDot = raw.IndexOf('.');
        bool hasMultipleDots = firstDot >= 0 && raw.IndexOf('.', firstDot + 1) >= 0;
        bool hasMinus = raw.Contains('-');

        decimal val = LegacyTextHelper.FuncCdbl(clean);
        decimal rounded = Math.Round(val, 2, MidpointRounding.AwayFromZero);

        if (rounded == 0m && !hasMultipleDots && !hasMinus)
        {
            txtNEWZSUU.Text = "0.00".PadLeft(13, ' ');
            return;
        }

        if (hasMultipleDots || hasMinus)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("入力エラーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZSUU.Text = string.Empty;
            RedirectFocus(txtNEWZSUU);
            return;
        }

        string zeroPadded = rounded.ToString("00000000.00", CultureInfo.InvariantCulture);
        if (zeroPadded.Length > 11)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("入力エラーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZSUU.Text = string.Empty;
            RedirectFocus(txtNEWZSUU);
            return;
        }

        string formatted = rounded.ToString("#,##0.00", CultureInfo.InvariantCulture);
        txtNEWZSUU.Text = formatted.Length >= 13 ? formatted[..13] : formatted.PadLeft(13, ' ');
    }

    private void txtNEWZBOKA_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtNEWZBOKA.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            txtTEKIYO.Focus();
            e.SuppressKeyPress = true;
        }
    }

    private void txtNEWZBOKA_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void txtNEWZBOKA_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        string raw = txtNEWZBOKA.Text;
        string clean = raw.Trim().Replace(",", string.Empty);

        if (raw.Contains('-'))
        {
            _validationFailedInLeave = true;
            MessageBox.Show("金額にマイナスは設定できません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZBOKA.Text = string.Empty;
            RedirectFocus(txtNEWZBOKA);
            return;
        }

        decimal val = LegacyTextHelper.FuncCdbl(clean);
        long rounded = (long)Math.Round(val, 0, MidpointRounding.AwayFromZero);

        if (rounded == 0L)
        {
            txtNEWZBOKA.Text = "0".PadLeft(13, ' ');
            return;
        }

        string zeroPadded = rounded.ToString("0000000000", CultureInfo.InvariantCulture);
        if (zeroPadded.Length > 10)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("入力エラーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEWZBOKA.Text = string.Empty;
            RedirectFocus(txtNEWZBOKA);
            return;
        }

        string formatted = rounded.ToString("#,##0", CultureInfo.InvariantCulture);
        txtNEWZBOKA.Text = formatted.Length >= 13 ? formatted[..13] : formatted.PadLeft(13, ' ');
    }

    private void txtTEKIYO_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            txtTEKIYO.Text = string.Empty;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Return)
        {
            if (cmdOK.Enabled)
            {
                cmdOK.Focus();
            }
            else
            {
                cmdCAN.Focus();
            }
            e.SuppressKeyPress = true;
        }
    }

    private void txtTEKIYO_LostFocus(object? sender, EventArgs e)
    {
        if (_suppressFocusEvents || IsBypassValidationTarget())
        {
            return;
        }

        if (LegacyTextHelper.CheckMojisu(txtTEKIYO.Text, 30) > 9)
        {
            _validationFailedInLeave = true;
            MessageBox.Show("文字数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            RedirectFocus(txtTEKIYO);
        }
    }
}

