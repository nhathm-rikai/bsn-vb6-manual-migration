using System.Globalization;
using Application.Common;
using Application.Services;
using Domain.Entities;
using KousyoERP.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0110 — 名称マスタ登録 (VB6 frmKUNO0110 1:1 移行画面)
/// </summary>
public partial class FrmKUNO0110 : Form
{
    private const string ScreenTitle = "KUNO0110";

    private readonly IKuno0110Service _service;

    // フォーム内グローバル変数 (VB6 upflg / optidx)
    private int _upflg;
    private int _optidx;
    private bool _isHandlingLeave;

    public FrmKUNO0110()
        : this(CreateDefaultService())
    {
    }

    public FrmKUNO0110(IKuno0110Service service)
    {
        _service = service;
        InitializeComponent();
    }

    private static IKuno0110Service CreateDefaultService()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0110Service(new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    // **********************************
    //  Form_Load / Form_Shown
    // **********************************
    private void FrmKUNO0110_Load(object? sender, EventArgs e)
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

        cmdOK.Enabled = false;
        cmdDEL.Enabled = false;
        _optidx = 0;
        _upflg = 0;
    }

    private void FrmKUNO0110_Shown(object? sender, EventArgs e)
    {
        // 初期表示時はどのオプションボタンも未選択状態とする
        foreach (Control c in Frame1.Controls)
        {
            if (c is RadioButton rb)
            {
                rb.Checked = false;
            }
        }
        TXTCD.Focus();
    }

    private void DataClear()
    {
        TXTCD.Text = string.Empty;
        txtNMK.Text = string.Empty;
        txtNMKJ.Text = string.Empty;
        txtNMS.Text = string.Empty;

        TXTCD.Focus();

        cmdOK.Enabled = false;
        cmdDEL.Enabled = false;
    }

    // **********************************
    //  名称情報の選択 (Opt_Click / Enter / KeyDown)
    // **********************************
    private void Opt_Click(object? sender, EventArgs e)
    {
        if (sender is not RadioButton rb || rb.Tag is not int index)
        {
            return;
        }

        DataClear();

        // VB6 Opt_Click(Index): Index 10..17 は KRECKB 11..18 へマッピング (10は欠番)
        string labelName;
        switch (index)
        {
            case 1: _optidx = 1; labelName = "ブロック"; break;
            case 2: _optidx = 2; labelName = "主管"; break;
            case 3: _optidx = 3; labelName = "部課名"; break;
            case 4: _optidx = 4; labelName = "部門"; break;
            case 5: _optidx = 5; labelName = "身分"; break;
            case 6: _optidx = 6; labelName = "職名"; break;
            case 7: _optidx = 7; labelName = "続柄"; break;
            case 8: _optidx = 8; labelName = "所属区分"; break;
            case 9: _optidx = 9; labelName = "メーカー"; break;
            case 10: _optidx = 11; labelName = "用途"; break;
            case 11: _optidx = 12; labelName = "燃料種別"; break;
            case 12: _optidx = 13; labelName = "石油元売"; break;
            case 13: _optidx = 14; labelName = "代行SS区分"; break;
            case 14: _optidx = 15; labelName = "自販機機種"; break;
            case 15: _optidx = 16; labelName = "単位"; break;
            case 16: _optidx = 17; labelName = "摘要情報"; break;
            case 17: _optidx = 18; labelName = "事業所記号"; break;
            default: return;
        }

        LblCD.Text = labelName;
        LblNMK.Text = labelName;
        LblNMKL.Text = labelName;
        LblNMS.Text = labelName;
    }

    private void Opt_Enter(object? sender, EventArgs e)
    {
        cmdOK.Enabled = false;
        cmdDEL.Enabled = false;
    }

    private void Opt_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            TXTCD.Focus();
        }
    }

    // **********************************
    //  コード入力 (TXTCD)
    // **********************************
    private void TXTCD_Enter(object? sender, EventArgs e)
    {
        cmdOK.Enabled = false;
        cmdDEL.Enabled = false;
    }

    private void TXTCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            TXTCD.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            txtNMK.Focus();
        }
    }

    private void TXTCD_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar < '0' || e.KeyChar > '9')
        {
            if (e.KeyChar > ' ')
            {
                e.Handled = true;
            }
        }
    }

    private void TXTCD_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            string trimmed = TXTCD.Text.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                TXTCD.Text = string.Empty;
                return;
            }

            _upflg = 0;

            if (_optidx == 0)
            {
                cmdOK.Enabled = false;
                cmdDEL.Enabled = false;
                return;
            }

            // 入力桁数チェック (VB6 1:1: タイトル "KUNO0020")
            (int maxDigits, string format) = GetCodeFormat(_optidx);
            if (maxDigits > 0)
            {
                if (trimmed.Length > maxDigits)
                {
                    MessageBox.Show($"入力桁数は{maxDigits}です。", "KUNO0020", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TXTCD.Text = string.Empty;
                    BeginInvoke(() => TXTCD.Focus());
                    return;
                }

                int codeNum = (int)LegacyTextHelper.VbVal(trimmed);
                TXTCD.Text = codeNum.ToString(format, CultureInfo.InvariantCulture);
            }

            int meisCd = (int)LegacyTextHelper.VbVal(TXTCD.Text);
            KmMeis? row;
            try
            {
                row = _service.GetByKey(_optidx, meisCd);
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"{_optidx},{TXTCD.Text}", ex);
                return;
            }

            if (row is null)
            {
                _upflg = 0;
                txtNMK.Text = string.Empty;
                txtNMKJ.Text = string.Empty;
                txtNMS.Text = string.Empty;
                cmdOK.Enabled = true;
                return;
            }

            _upflg = 1;

            if (row.DDate.HasValue)
            {
                DialogResult res = MessageBox.Show(
                    "このレコードは削除されています。復元しますか？",
                    ScreenTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (res == DialogResult.No)
                {
                    DataClear();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(format))
            {
                TXTCD.Text = row.KMeisCd.ToString(format, CultureInfo.InvariantCulture);
            }

            txtNMK.Text = row.MeisyoKn;
            txtNMKJ.Text = row.MeisyoKj;
            txtNMS.Text = row.MeisyoS;

            cmdOK.Enabled = true;
            cmdDEL.Enabled = true;
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private static (int MaxDigits, string Format) GetCodeFormat(int recKb) => recKb switch
    {
        1 or 8 or 12 => (1, "0"),
        2 or 3 or 4 or 5 or 6 or 7 or 11 or 13 or 14 or 15 or 16 => (2, "00"),
        17 or 18 => (3, "000"),
        9 => (4, "0000"),
        _ => (0, string.Empty)
    };

    // **********************************
    //  名称カナ / 漢字 / 略称
    // **********************************
    private void txtNMK_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; txtNMK.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; txtNMKJ.Focus(); }
    }

    private void txtNMK_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (LegacyTextHelper.CheckMojisu(txtNMK.Text, 20) > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtNMK.Focus());
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void txtNMKJ_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; txtNMKJ.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; txtNMS.Focus(); }
    }

    private void txtNMKJ_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (LegacyTextHelper.CheckMojisu(txtNMKJ.Text, 30) > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtNMKJ.Focus());
                return;
            }

            if (!string.IsNullOrEmpty(txtNMKJ.Text) && _upflg == 0)
            {
                txtNMS.Text = LegacyTextHelper.SubStringByByte(txtNMKJ.Text.Trim(), 16);
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void txtNMS_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtNMS.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (cmdOK.Enabled) cmdOK.Focus();
            else if (cmdEND.Enabled) cmdEND.Focus();
        }
    }

    private void txtNMS_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (LegacyTextHelper.CheckMojisu(txtNMS.Text, 16) > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtNMS.Focus());
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    // **********************************
    //  設定 / キャンセル / 削除 / 終了
    // **********************************
    private void cmdOK_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TXTCD.Text.Trim()))
        {
            MessageBox.Show("名称コードを設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            TXTCD.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtNMKJ.Text.Trim()))
        {
            MessageBox.Show("名称を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNMKJ.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtNMS.Text.Trim()))
        {
            MessageBox.Show("名称略称を設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNMKJ.Focus();
            return;
        }

        var entity = new KmMeis
        {
            TDate = DateTime.Today,
            KDate = DateTime.Today,
            DDate = null,
            KRecKb = _optidx,
            KMeisCd = (int)LegacyTextHelper.VbVal(TXTCD.Text),
            MeisyoKn = txtNMK.Text,
            MeisyoKj = txtNMKJ.Text,
            MeisyoS = txtNMS.Text
        };

        try
        {
            if (_upflg == 0)
            {
                _service.Insert(entity);
                MessageBox.Show("登録されました。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _service.Update(entity);
                MessageBox.Show("更新されました。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"{_optidx},{TXTCD.Text}", ex);
            return;
        }

        DataClear();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        DataClear();
    }

    private void cmdDEL_Click(object? sender, EventArgs e)
    {
        DialogResult res = MessageBox.Show("削除しますか？", ScreenTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (res == DialogResult.No)
        {
            DataClear();
            return;
        }

        try
        {
            _service.SoftDelete(_optidx, (int)LegacyTextHelper.VbVal(TXTCD.Text), DateTime.Today);
            MessageBox.Show("削除されました。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"{_optidx},{TXTCD.Text}", ex);
            return;
        }

        DataClear();
    }

    private void cmdEND_Click(object? sender, EventArgs e)
    {
        Close();
    }
}

