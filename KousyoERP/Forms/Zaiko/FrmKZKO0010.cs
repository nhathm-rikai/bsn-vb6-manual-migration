using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Zaiko;

/// <summary>
/// KZKO0010 — 在庫マスタ更新処理 (VB6 KZKO0010.FRM 1:1 移行画面)
/// </summary>
public partial class FrmKZKO0010 : Form
{
    private const string ScreenTitle = "KZKO0010";

    private readonly IKzko0010Service _service;

    // 2004/01/10 ADD ロック日付領域
    private long _svKEIYM;
    private long _svKEIYMD;

    private bool _isHandlingLeave;

    public FrmKZKO0010()
        : this(CreateDefaultService())
    {
    }

    public FrmKZKO0010(IKzko0010Service service)
    {
        _service = service;
        InitializeComponent();
    }

    private static IKzko0010Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kzko0010Service(
            new Infrastructure.Repositories.KmLockRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory),
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmZaikoRepository(factory));
    }

    // **********************************
    //  Form_Load
    // **********************************
    private void FrmKZKO0010_Load(object? sender, EventArgs e)
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

        // システム日付設定
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        Kzko0010InitContextDto initContext;
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
        lblYMD.Text = initContext.SystemDateText;

        txtZNEN.Text = initContext.DefaultYearText;
        txtZTUKI.Text = initContext.DefaultMonthText;
        txtZHI.Text = initContext.DefaultDayText;
        txtBMNCD.Text = string.Empty;
        txtBMNCD.Enabled = true;
        txtSHNCD.Text = string.Empty;
        txtSHNCD.Enabled = false;
    }

    // **********************************
    //  終了ボタン
    // **********************************
    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        Close();
    }

    // ******************************************
    //   実行ボタンクリック (VB6 cmdSTR_Click)
    // ******************************************
    private void cmdSTR_Click(object? sender, EventArgs e)
    {
        // 入力チェック
        if (optBMNCD.Checked)
        {
            if (ParseVal(txtBMNCD.Text) == 0)
            {
                MessageBox.Show("部門コードの指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBMNCD.Focus();
                return;
            }
        }
        else
        {
            if (ParseVal(txtSHNCD.Text) == 0)
            {
                MessageBox.Show("商品コードの指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSHNCD.Focus();
                return;
            }
        }

        if (txtZNEN.Text == string.Empty)
        {
            MessageBox.Show("在庫年月度の指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtZNEN.Focus();
            return;
        }

        if (txtZTUKI.Text == string.Empty)
        {
            MessageBox.Show("在庫年月度の指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtZTUKI.Focus();
            return;
        }

        if (txtZHI.Text == string.Empty)
        {
            MessageBox.Show("在庫年月度の指定に誤りがあります。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtZHI.Focus();
            return;
        }

        int znen = (int)ParseVal(txtZNEN.Text);
        int ztuki = (int)ParseVal(txtZTUKI.Text);
        int zhi = (int)ParseVal(txtZHI.Text);

        // 2004/01/10 ADD 経理締め日チェック
        if (znen * 10000L + ztuki * 100L + zhi < _svKEIYMD)
        {
            MessageBox.Show("指定された日付は経理締め日以前の為更新できません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtZNEN.Focus();
            return;
        }

        // 商品マスタ検索処理 (get_syohin)
        IReadOnlyList<Kzko0010TargetProductDto> products;
        try
        {
            if (optBMNCD.Checked)
            {
                products = _service.GetTargetProductsByDepartment((int)ParseVal(txtBMNCD.Text));
            }
            else
            {
                products = _service.GetTargetProductsByCode(ParseVal(txtSHNCD.Text));
            }
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品ﾏｽﾀ", string.Empty, ex);
            return;
        }

        // 商品マスタ検索結果(対象データ未存在時)
        if (products.Count == 0)
        {
            MessageBox.Show("該当レコードが存在しません。", "KZKO1010", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        Label5.Text = products.Count.ToString("000000", CultureInfo.InvariantCulture);
        Label5.Refresh();

        // マウス・ボタン使用不可能
        SetControlsEnabled(false);
        Cursor = Cursors.WaitCursor;

        try
        {
            // 在庫マスタ更新処理 (upd_zaiko)
            _service.ExecuteZaikoUpdate(
                znen,
                ztuki,
                zhi,
                products,
                processedCount =>
                {
                    Label3.Text = processedCount.ToString("000000", CultureInfo.InvariantCulture);
                    Label3.Refresh();
                    System.Windows.Forms.Application.DoEvents();
                });

            // 編集表示
            Label6.Text = "件処理終了しました";
            Label6.Refresh();

            // 終了メッセージ
            MessageBox.Show(
                $"{txtZNEN.Text}年{txtZTUKI.Text}月の在庫マスタ更新処理完了しました。",
                ScreenTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Kzko0010SqlContextException ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, ex.TableName, ex.KeyValue, ex.InnerException ?? ex);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "upd_zaiko", "VBｴﾗｰ", ex);
        }
        finally
        {
            // マウス・ボタン使用可能
            Cursor = Cursors.Default;
            SetControlsEnabled(true);
        }
    }

    private void SetControlsEnabled(bool enabled)
    {
        txtZNEN.Enabled = enabled;
        txtZTUKI.Enabled = enabled;
        txtZHI.Enabled = enabled;
        spnZNEN.Enabled = enabled;
        spnZTUKI.Enabled = enabled;
        spnZHI.Enabled = enabled;
        cmdCAN.Enabled = enabled;
        cmdSTR.Enabled = enabled;
    }

    // **********************************
    //  ラジオボタン (更新対象切替)
    // **********************************
    private void optBMNCD_Click(object? sender, EventArgs e)
    {
        txtBMNCD.Enabled = true;
        txtBMNCD.Text = string.Empty;
        lblBMNNAME.Text = string.Empty;
        txtSHNCD.Text = string.Empty;
        txtSHNCD.Enabled = false;
        lblSHNNAME.Text = string.Empty;
        txtBMNCD.Focus();
    }

    private void optBMNCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtBMNCD.Focus();
        }
    }

    private void optSHNCD_Click(object? sender, EventArgs e)
    {
        txtBMNCD.Text = string.Empty;
        txtBMNCD.Enabled = false;
        lblBMNNAME.Text = string.Empty;
        txtSHNCD.Enabled = true;
        txtSHNCD.Text = string.Empty;
        lblSHNNAME.Text = string.Empty;
        txtSHNCD.Focus();
    }

    private void optSHNCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtSHNCD.Focus();
        }
    }

    // **********************************
    //  部門コード (txtBMNCD)
    // **********************************
    private void txtBMNCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtBMNCD.Text = string.Empty;
            lblBMNNAME.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtZNEN.Focus();
        }
    }

    private void txtBMNCD_KeyPress(object? sender, KeyPressEventArgs e)
    {
        AllowDigitsOnly(e);
    }

    private void txtBMNCD_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave)
        {
            return;
        }

        _isHandlingLeave = true;
        try
        {
            long val = ParseVal(txtBMNCD.Text);
            if (val == 0)
            {
                txtBMNCD.Text = string.Empty;
                return;
            }

            txtBMNCD.Text = val.ToString("00", CultureInfo.InvariantCulture);

            string? bmnName;
            try
            {
                bmnName = _service.GetDepartmentName((int)val);
            }
            catch
            {
                lblBMNNAME.Text = string.Empty;
                MessageBox.Show("部門コードが存在しません。", ScreenTitle);
                txtBMNCD.Text = string.Empty;
                BeginInvoke(() => txtBMNCD.Focus());
                return;
            }

            if (bmnName is null)
            {
                lblBMNNAME.Text = string.Empty;
                MessageBox.Show("部門コードが存在しません。", ScreenTitle);
                txtBMNCD.Text = string.Empty;
                BeginInvoke(() => txtBMNCD.Focus());
                return;
            }

            lblBMNNAME.Text = bmnName;
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    // **********************************
    //  商品コード (txtSHNCD)
    // **********************************
    private void txtSHNCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtSHNCD.Text = string.Empty;
            lblSHNNAME.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtZNEN.Focus();
        }
    }

    private void txtSHNCD_KeyPress(object? sender, KeyPressEventArgs e)
    {
        AllowDigitsOnly(e);
    }

    private void txtSHNCD_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave)
        {
            return;
        }

        _isHandlingLeave = true;
        try
        {
            long val = ParseVal(txtSHNCD.Text);
            if (val == 0)
            {
                txtSHNCD.Text = string.Empty;
                return;
            }

            txtSHNCD.Text = val.ToString("00000000", CultureInfo.InvariantCulture);

            Kzko0010ProductLookupResultDto result;
            try
            {
                result = _service.LookupProduct(val);
            }
            catch
            {
                lblSHNNAME.Text = string.Empty;
                MessageBox.Show("商品コードが存在しません。", ScreenTitle);
                txtSHNCD.Text = string.Empty;
                BeginInvoke(() => txtSHNCD.Focus());
                return;
            }

            if (result.Status == Kzko0010ProductLookupStatus.NotFound)
            {
                lblSHNNAME.Text = string.Empty;
                MessageBox.Show("商品コードが存在しません。", ScreenTitle);
                txtSHNCD.Text = string.Empty;
                BeginInvoke(() => txtSHNCD.Focus());
                return;
            }

            if (result.Status == Kzko0010ProductLookupStatus.NotInventoryTarget)
            {
                MessageBox.Show("指定された商品コードは在庫対象外です。", ScreenTitle);
                txtSHNCD.Text = string.Empty;
                BeginInvoke(() => txtSHNCD.Focus());
                return;
            }

            lblSHNNAME.Text = result.ShortName;
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    // **********************************
    //  年 (txtZNEN / spnZNEN)
    // **********************************
    private void txtZNEN_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtZNEN.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtZTUKI.Focus();
        }
    }

    private void txtZNEN_KeyPress(object? sender, KeyPressEventArgs e)
    {
        AllowDigitsOnly(e);
    }

    private void txtZNEN_Leave(object? sender, EventArgs e)
    {
        string trimmed = txtZNEN.Text.Trim();
        if (string.IsNullOrEmpty(trimmed))
        {
            txtZNEN.Text = string.Empty;
            return;
        }

        txtZNEN.Text = ParseVal(trimmed).ToString("0000", CultureInfo.InvariantCulture);
    }

    private void spnZNEN_SpinUp(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtZNEN.Text.Trim()))
        {
            txtZNEN.Focus();
            txtZNEN.Refresh();
            return;
        }

        txtZNEN.Text = (ParseVal(txtZNEN.Text) + 1).ToString("0000", CultureInfo.InvariantCulture);
        txtZNEN.Focus();
        txtZNEN.Refresh();
    }

    private void spnZNEN_SpinDown(object? sender, EventArgs e)
    {
        long val = ParseVal(txtZNEN.Text.Trim());
        if (val == 0)
        {
            txtZNEN.Focus();
            txtZNEN.Refresh();
            return;
        }

        txtZNEN.Text = (val - 1).ToString("0000", CultureInfo.InvariantCulture);
        txtZNEN.Focus();
        txtZNEN.Refresh();
    }

    // **********************************
    //  月 (txtZTUKI / spnZTUKI)
    // **********************************
    private void txtZTUKI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtZTUKI.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtZHI.Focus();
        }
    }

    private void txtZTUKI_KeyPress(object? sender, KeyPressEventArgs e)
    {
        AllowDigitsOnly(e);
    }

    private void txtZTUKI_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave)
        {
            return;
        }

        _isHandlingLeave = true;
        try
        {
            string strMM = txtZTUKI.Text.Trim();
            if (string.IsNullOrEmpty(strMM))
            {
                return;
            }

            long val = ParseVal(strMM);
            if (val < 1 || val > 12)
            {
                MessageBox.Show("指定された月は存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtZTUKI.Text = string.Empty;
                BeginInvoke(() => txtZTUKI.Focus());
                return;
            }

            txtZTUKI.Text = val.ToString("00", CultureInfo.InvariantCulture);
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void spnZTUKI_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtZTUKI.Text.Trim();
        long val = ParseVal(trimmed);
        if (string.IsNullOrEmpty(trimmed) || val == 12)
        {
            txtZTUKI.Focus();
            txtZTUKI.Refresh();
            return;
        }

        txtZTUKI.Text = (val + 1).ToString("00", CultureInfo.InvariantCulture);
        txtZTUKI.Focus();
        txtZTUKI.Refresh();
    }

    private void spnZTUKI_SpinDown(object? sender, EventArgs e)
    {
        long val = ParseVal(txtZTUKI.Text.Trim());
        if (val <= 1)
        {
            txtZTUKI.Focus();
            txtZTUKI.Refresh();
            return;
        }

        txtZTUKI.Text = (val - 1).ToString("00", CultureInfo.InvariantCulture);
        txtZTUKI.Focus();
        txtZTUKI.Refresh();
    }

    // **********************************
    //  日 (txtZHI / spnZHI)
    // **********************************
    private void txtZHI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtZHI.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            cmdSTR.Focus();
        }
    }

    private void txtZHI_KeyPress(object? sender, KeyPressEventArgs e)
    {
        AllowDigitsOnly(e);
    }

    private void txtZHI_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave)
        {
            return;
        }

        _isHandlingLeave = true;
        try
        {
            string strDD = txtZHI.Text.Trim();
            if (string.IsNullOrEmpty(strDD))
            {
                return;
            }

            long val = ParseVal(strDD);
            if (val < 1 || val > 31)
            {
                MessageBox.Show("指定された日は存在しません。", "KZKOO010", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtZHI.Text = string.Empty;
                BeginInvoke(() => txtZHI.Focus());
                return;
            }

            txtZHI.Text = val.ToString("00", CultureInfo.InvariantCulture);
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void spnZHI_SpinUp(object? sender, EventArgs e)
    {
        string trimmed = txtZHI.Text.Trim();
        long val = ParseVal(trimmed);
        if (string.IsNullOrEmpty(trimmed) || val == 31)
        {
            txtZHI.Focus();
            txtZHI.Refresh();
            return;
        }

        txtZHI.Text = (val + 1).ToString("00", CultureInfo.InvariantCulture);
        txtZHI.Focus();
        txtZHI.Refresh();
    }

    private void spnZHI_SpinDown(object? sender, EventArgs e)
    {
        long val = ParseVal(txtZHI.Text.Trim());
        if (val <= 1)
        {
            txtZHI.Focus();
            txtZHI.Refresh();
            return;
        }

        txtZHI.Text = (val - 1).ToString("00", CultureInfo.InvariantCulture);
        txtZHI.Focus();
        txtZHI.Refresh();
    }

    // **********************************
    //  共通ヘルパー
    // **********************************
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

    private static long ParseVal(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0L;
        }

        string trimmed = text.Trim();
        int len = 0;
        while (len < trimmed.Length && trimmed[len] >= '0' && trimmed[len] <= '9')
        {
            len++;
        }

        if (len == 0)
        {
            return 0L;
        }

        return long.TryParse(trimmed[..len], NumberStyles.None, CultureInfo.InvariantCulture, out long value)
            ? value
            : 0L;
    }
}

