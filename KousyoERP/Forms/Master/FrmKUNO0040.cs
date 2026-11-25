using System.Globalization;
using Application.Common;
using Application.DTOs;
using Application.Services;
using Domain.Entities;
using KousyoERP.Common;
using KousyoERP.Forms.Common;

namespace KousyoERP.Forms.Master;

/// <summary>
/// KUNO0040 — 顧客マスタ登録 (VB6 frmKUNO0040 1:1 移行画面)
/// </summary>
public partial class FrmKUNO0040 : Form
{
    private const string ScreenTitle = "KUNO0040";

    private readonly IKuno0040Service _service;
    private readonly Kuno0040State _state = new();
    private bool _isHandlingLeave;

    public FrmKUNO0040()
        : this(CreateDefaultService())
    {
    }

    public FrmKUNO0040(IKuno0040Service service)
    {
        _service = service;
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

    // **********************************
    //  Form_Load
    // **********************************
    private void FrmKUNO0040_Load(object? sender, EventArgs e)
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

        cmdSET.Enabled = false;
        cmdDEL.Enabled = false;
        cmdGKJ.Enabled = false;
        cmdSSJ.Enabled = false;

        _state.UpFlg40 = 0;
        _state.ResetAll();

        optGP.Checked = true;
        optDS.Checked = true;
        optNO.Checked = true;
        optKAZEI.Checked = true;
    }

    // ********************************************
    //  画面初期化(但し 顧客コード除く)
    // ********************************************
    private void AllClear()
    {
        optGP.Checked = true;
        txtKNMK.Text = string.Empty;
        txtKNMKJ.Text = string.Empty;
        txtKNMKJ2.Text = string.Empty;
        txtTENAME.Text = string.Empty;
        txtKNMS.Text = string.Empty;
        txtUBBG.Text = string.Empty;
        txtJS1.Text = string.Empty;
        txtJS2.Text = string.Empty;
        txtJS3.Text = string.Empty;
        txtTEL.Text = string.Empty;
        txtFAX.Text = string.Empty;

        txtSKCD.Text = string.Empty;
        lblSKCD.Text = string.Empty;
        txtSSCD.Text = string.Empty;
        lblSSCD.Text = string.Empty;
        txtGKCD.Text = string.Empty;
        lblGKCD.Text = string.Empty;
        txtJIGCD.Text = string.Empty;
        lblJIGCD.Text = string.Empty;
        txtCARDNO.Text = string.Empty;
        txtMCD.Text = string.Empty;
        lblMCD.Text = string.Empty;
        txtSZG.Text = string.Empty;
        lblSZG.Text = string.Empty;
        txtBKG.Text = string.Empty;
        lblBKG.Text = string.Empty;
        txtMBN.Text = string.Empty;
        lblMBN.Text = string.Empty;
        txtSKNM.Text = string.Empty;
        lblSKNM.Text = string.Empty;

        txtNEN.Text = string.Empty;
        txtTUKI.Text = string.Empty;
        txtHI.Text = string.Empty;
        txtSBG.Text = string.Empty;
        txtHBG.Text = string.Empty;
        txtTNEN.Text = string.Empty;
        txtTTUKI.Text = string.Empty;
        txtTHI.Text = string.Empty;
        optDS.Checked = true;
        optNO.Checked = true;
        txtJIGYONO.Text = string.Empty;
        optKAZEI.Checked = true;

        _state.ResetAll();
    }

    // ********************************************
    //  顧客マスタ検索表示処理 (Onelist_Set)
    // ********************************************
    private bool OnelistSet()
    {
        string kcd = ((long)LegacyTextHelper.VbVal(txtKCD.Text.Trim())).ToString("0000000000", CultureInfo.InvariantCulture);

        KmKyak? row;
        try
        {
            row = _service.GetCustomerByCode(kcd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "顧客マスタ", txtKCD.Text, ex);
            return false;
        }

        if (row is null)
        {
            _state.UpFlg40 = 0;
            AllClear();
            return true;
        }

        _state.UpFlg40 = 1;

        // 削除対象データ判別
        if (row.DDate.HasValue)
        {
            DialogResult res = MessageBox.Show(
                "このレコードは削除されています。復元しますか？",
                "KUNO0140",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (res == DialogResult.No)
            {
                _state.UpFlg40 = 0;
                AllClear();
                txtKCD.Text = string.Empty;
                BeginInvoke(() => txtKCD.Focus());
                return false;
            }
        }

        // 2画面・3画面項目を共有ステートへ格納
        _state.F230 = row.Ssdd1; _state.F240 = row.IngtKb1; _state.F250 = row.Indd1;
        _state.F260 = row.Ssdd2; _state.F270 = row.IngtKb2; _state.F280 = row.Indd2;
        _state.F290 = row.Ssdd3; _state.F300 = row.IngtKb3; _state.F310 = row.Indd3;
        _state.F320 = row.SzkKb; _state.F330 = row.SsyoKb; _state.F340 = row.ThyoKb;
        _state.F350 = row.Shsdd1; _state.F360 = row.ShgtKb1; _state.F370 = row.Shydd1;
        _state.F380 = row.Shsdd2; _state.F390 = row.ShgtKb2; _state.F400 = row.Shydd2;
        _state.F410 = row.Shsdd3; _state.F420 = row.ShgtKb3; _state.F430 = row.Shydd3;
        _state.F440 = row.SyhzkKb;
        _state.F800 = row.ShrHoho; _state.F810 = row.Site; _state.F820 = row.KsMm; _state.F830 = row.KsDd;

        _state.F450 = row.GkCd1; _state.F460 = row.KzSbt1; _state.F470 = row.KzNo1; _state.F480 = row.KzMegi1;
        _state.F490 = row.GkCd2; _state.F500 = row.KzSbt2; _state.F510 = row.KzNo2; _state.F520 = row.KzMegi2;

        // 1画面項目設定
        switch (row.KKykKb)
        {
            case 0: optGP.Checked = true; break;
            case 1: optJG.Checked = true; break;
            case 2: optIPKG.Checked = true; break;
            case 3: optIPKJ.Checked = true; break;
        }

        txtKNMK.Text = row.KykNameKn;
        (string part1, string part2) = LegacyTextHelper.SplitCustomerKanjiName(row.KykNameKj);
        txtKNMKJ.Text = part1;
        txtKNMKJ2.Text = part2;
        txtTENAME.Text = row.KykNameTg;
        txtKNMS.Text = row.KykNameS;
        txtUBBG.Text = row.UbnNo;
        txtJS1.Text = row.Add1;
        txtJS2.Text = row.Add2;
        txtJS3.Text = row.Add3;
        txtTEL.Text = row.TelNo;
        txtFAX.Text = row.FaxNo;

        long sksVal = (long)LegacyTextHelper.VbVal(row.SksCd);
        txtSKCD.Text = sksVal != 0 ? sksVal.ToString("0000000000", CultureInfo.InvariantCulture) : string.Empty;
        long shsVal = (long)LegacyTextHelper.VbVal(row.ShsCd);
        txtSSCD.Text = shsVal != 0 ? shsVal.ToString("0000000000", CultureInfo.InvariantCulture) : string.Empty;
        txtGKCD.Text = row.GpkCd != 0 ? row.GpkCd.ToString("000", CultureInfo.InvariantCulture) : string.Empty;
        txtJIGCD.Text = row.Jigyo != 0 ? row.Jigyo.ToString("000", CultureInfo.InvariantCulture) : string.Empty;
        txtCARDNO.Text = row.CardNo != 0 ? row.CardNo.ToString("0000000000", CultureInfo.InvariantCulture) : string.Empty;
        txtMCD.Text = row.MakCd != 0 ? row.MakCd.ToString("0000", CultureInfo.InvariantCulture) : string.Empty;
        txtSZG.Text = row.SzCd != 0 ? row.SzCd.ToString("000", CultureInfo.InvariantCulture) : string.Empty;
        txtBKG.Text = row.BkCd != 0 ? row.BkCd.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtMBN.Text = row.MbnCd != 0 ? row.MbnCd.ToString("00", CultureInfo.InvariantCulture) : string.Empty;
        txtSKNM.Text = row.SknmCd != 0 ? row.SknmCd.ToString("00", CultureInfo.InvariantCulture) : string.Empty;

        string bdayStr = row.Birthday.ToString("00000000", CultureInfo.InvariantCulture);
        if (bdayStr != "00000000")
        {
            txtNEN.Text = bdayStr[..4];
            txtTUKI.Text = bdayStr.Substring(4, 2);
            txtHI.Text = bdayStr.Substring(6, 2);
        }
        else
        {
            txtNEN.Text = string.Empty;
            txtTUKI.Text = string.Empty;
            txtHI.Text = string.Empty;
        }

        txtSBG.Text = row.SinNo != 0 ? row.SinNo.ToString("00000", CultureInfo.InvariantCulture) : string.Empty;
        txtHBG.Text = row.HhknNo != 0 ? row.HhknNo.ToString("00000", CultureInfo.InvariantCulture) : string.Empty;

        string drpStr = row.DrpDay.ToString("00000000", CultureInfo.InvariantCulture);
        if (drpStr != "00000000")
        {
            txtTNEN.Text = drpStr[..4];
            txtTTUKI.Text = drpStr.Substring(4, 2);
            txtTHI.Text = drpStr.Substring(6, 2);
        }
        else
        {
            txtTNEN.Text = string.Empty;
            txtTTUKI.Text = string.Empty;
            txtTHI.Text = string.Empty;
        }

        if (row.Seibt == 0) optDS.Checked = true; else if (row.Seibt == 1) optJS.Checked = true;
        if (row.TkKb == 0) optNO.Checked = true; else if (row.TkKb == 1) optYES.Checked = true;

        txtJIGYONO.Text = row.TorokuNo;
        if (row.KazeiKb == 0) optKAZEI.Checked = true; else if (row.KazeiKb == 1) optMENZEI.Checked = true;

        // 各種略称ラベル設定
        lblSKCD.Text = !string.IsNullOrEmpty(txtSKCD.Text) ? LookupCustomerShortNameSafe(txtSKCD.Text) : string.Empty;
        lblSSCD.Text = !string.IsNullOrEmpty(txtSSCD.Text) ? LookupCustomerShortNameSafe(txtSSCD.Text) : string.Empty;
        lblGKCD.Text = row.GpkCd != 0 ? LookupGroupSafe(row.GpkCd)?.ShortName ?? string.Empty : string.Empty;
        lblJIGCD.Text = row.Jigyo != 0 ? LookupMeisSafe(18, row.Jigyo) : string.Empty;
        lblMCD.Text = row.MakCd != 0 ? LookupMeisSafe(9, row.MakCd) : string.Empty;
        lblSZG.Text = row.SzCd != 0 ? LookupSyzkSafe(row.SzCd) : string.Empty;
        lblBKG.Text = row.BkCd != 0 ? LookupMeisSafe(3, row.BkCd) : string.Empty;
        lblMBN.Text = row.MbnCd != 0 ? LookupMeisSafe(5, row.MbnCd) : string.Empty;
        lblSKNM.Text = row.SknmCd != 0 ? LookupMeisSafe(6, row.SknmCd) : string.Empty;

        return true;
    }

    private string LookupCustomerShortNameSafe(string cd)
    {
        try { return _service.GetActiveCustomerShortName(cd) ?? string.Empty; }
        catch (Exception ex) { LegacyErrorDialog.ShowSqlError(ScreenTitle, "顧客マスタ", cd, ex); return string.Empty; }
    }

    private Kuno0040GroupInfoDto? LookupGroupSafe(int cd)
    {
        try { return _service.GetActiveGroupInfo(cd); }
        catch (Exception ex) { LegacyErrorDialog.ShowSqlError(ScreenTitle, "ｸﾞﾙｰﾌﾟ会社ﾏｽﾀ", cd.ToString("000", CultureInfo.InvariantCulture), ex); return null; }
    }

    private string LookupSyzkSafe(int cd)
    {
        try { return _service.GetActiveAffiliationShortName(cd) ?? string.Empty; }
        catch (Exception ex) { LegacyErrorDialog.ShowSqlError(ScreenTitle, "所属マスタ", cd.ToString("000", CultureInfo.InvariantCulture), ex); return string.Empty; }
    }

    private string LookupMeisSafe(int recKb, int cd)
    {
        try { return _service.GetActiveMeisShortName(recKb, cd) ?? string.Empty; }
        catch (Exception ex) { LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"{recKb}:{cd}", ex); return string.Empty; }
    }

    // **********************************
    //  サブ画面・検索ダイアログ呼出
    // **********************************
    private void cmdSSJ_Click(object? sender, EventArgs e)
    {
        _state.Kokyaku = txtKCD.Text;
        _state.Seikyu = txtSKCD.Text;
        _state.Siharai = txtSSCD.Text;
        _state.SnmKokyaku = txtKNMS.Text;
        _state.SnmSeikyu = lblSKCD.Text;
        _state.SnmSiharai = lblSSCD.Text;

        using var frm = new FrmKUNO0041(_service, _state);
        frm.ShowDialog(this);

        txtKCD.Text = _state.Kokyaku;
        txtSKCD.Text = _state.Seikyu;
        txtSSCD.Text = _state.Siharai;
        txtKNMS.Text = _state.SnmKokyaku;
        lblSKCD.Text = _state.SnmSeikyu;
        lblSSCD.Text = _state.SnmSiharai;

        if (_state.CanFlg2 == 1)
        {
            AllClear();
            txtKCD.Text = string.Empty;
            txtKCD.Focus();
        }
    }

    private void cmdGKJ_Click(object? sender, EventArgs e)
    {
        _state.Kokyaku = txtKCD.Text;
        _state.SnmKokyaku = txtKNMS.Text;

        using var frm = new FrmKUNO0042(_service, _state);
        frm.ShowDialog(this);

        if (_state.CanFlg3 == 1)
        {
            AllClear();
            txtKCD.Text = string.Empty;
            txtKCD.Focus();
        }
    }

    private void cmdKYKKEN_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmKYKKEN(_service);
        if (frm.ShowDialog(this) == DialogResult.OK && frm.IsSelected)
        {
            txtKCD.Text = frm.SelectedCustomerCode;
            txtKNMS.Text = frm.SelectedCustomerShortName;
        }
        else
        {
            txtKCD.Text = string.Empty;
            txtKNMS.Text = string.Empty;
        }
        txtKCD.Focus();
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

    private void cmdGPCD_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmGPKEN(_service);
        if (frm.ShowDialog(this) == DialogResult.OK && frm.IsSelected)
        {
            txtGKCD.Text = frm.SelectedGroupCode;
            lblGKCD.Text = frm.SelectedGroupShortName;
        }
        else
        {
            txtGKCD.Text = string.Empty;
            lblGKCD.Text = string.Empty;
        }
        txtGKCD.Focus();
    }

    // **********************************
    //  設定 / キャンセル / 削除 / 終了
    // **********************************
    private void cmdSET_Click(object? sender, EventArgs e)
    {
        if (_state.UpFlg40 == 0 && _state.SetFlg2 == 0)
        {
            MessageBox.Show("請求支払条件が登録されていません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (txtKCD.Text.Trim() != txtSKCD.Text.Trim())
        {
            if (!string.IsNullOrEmpty(txtSKCD.Text.Trim()) && string.IsNullOrEmpty(lblSKCD.Text.Trim()))
            {
                MessageBox.Show("指定された請求先コードは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSKCD.Text = string.Empty;
                lblSKCD.Text = string.Empty;
                txtSKCD.Focus();
                return;
            }
        }

        if (txtKCD.Text.Trim() != txtSSCD.Text.Trim())
        {
            if (!string.IsNullOrEmpty(txtSSCD.Text.Trim()) && string.IsNullOrEmpty(lblSSCD.Text.Trim()))
            {
                MessageBox.Show("指定された支払先コードは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSSCD.Text = string.Empty;
                lblSSCD.Text = string.Empty;
                txtSSCD.Focus();
                return;
            }
        }

        if (string.IsNullOrEmpty(txtSKCD.Text.Trim()) && string.IsNullOrEmpty(txtSSCD.Text.Trim()))
        {
            MessageBox.Show("請求先コードまたは支払先コードを設定してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSKCD.Text = string.Empty; lblSKCD.Text = string.Empty;
            txtSSCD.Text = string.Empty; lblSSCD.Text = string.Empty;
            txtSKCD.Focus();
            return;
        }

        if (!ValidateCodeLabelPair(txtGKCD, lblGKCD, "指定されたグループ会社コードは存在しません。")) return;
        if (!ValidateCodeLabelPair(txtJIGCD, lblJIGCD, "指定された事業所記号は存在しません。")) return;
        if (!ValidateCodeLabelPair(txtMCD, lblMCD, "指定されたﾒｰｶｰｺｰﾄﾞは存在しません。")) return;
        if (!ValidateCodeLabelPair(txtSZG, lblSZG, "指定された所属コードは存在しません。")) return;
        if (!ValidateCodeLabelPair(txtBKG, lblBKG, "指定された部課コードは存在しません。")) return;
        if (!ValidateCodeLabelPair(txtMBN, lblMBN, "指定された身分コードは存在しません。")) return;
        if (!ValidateCodeLabelPair(txtSKNM, lblSKNM, "指定された職名コードは存在しません。")) return;

        if (!IsAllFilledOrAllEmpty(txtNEN.Text, txtTUKI.Text, txtHI.Text))
        {
            MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtNEN.Text = string.Empty;
            txtNEN.Focus();
            return;
        }

        if (!IsAllFilledOrAllEmpty(txtTNEN.Text, txtTTUKI.Text, txtTHI.Text))
        {
            MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtTNEN.Text = string.Empty;
            txtTNEN.Focus();
            return;
        }

        // カード番号の重複チェック
        long cardNo = (long)LegacyTextHelper.VbVal(txtCARDNO.Text);
        if (cardNo != 0)
        {
            try
            {
                if (_service.IsCardNoDuplicate(cardNo, txtKCD.Text.Trim()))
                {
                    MessageBox.Show("カード番号が既に登録されています", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCARDNO.Text = string.Empty;
                    txtCARDNO.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "顧客マスタ", "カード番号", ex);
                return;
            }
        }

        // 2011/07/22 名称漢字26バイトスペース充填 + 後半結合
        string combinedKanjiName = LegacyTextHelper.CombineCustomerKanjiName(txtKNMKJ.Text, txtKNMKJ2.Text);

        var entity = new KmKyak
        {
            TDate = DateTime.Today,
            KDate = DateTime.Today,
            DDate = null,
            KKykCd = txtKCD.Text,
            KKykKb = optGP.Checked ? 0 : optJG.Checked ? 1 : optIPKG.Checked ? 2 : 3,
            KykNameKn = txtKNMK.Text.Trim(),
            KykNameKj = combinedKanjiName,
            KykNameTg = txtTENAME.Text.Trim(),
            KykNameS = txtKNMS.Text.Trim(),
            UbnNo = txtUBBG.Text.Trim(),
            Add1 = txtJS1.Text.Trim(),
            Add2 = txtJS2.Text.Trim(),
            Add3 = txtJS3.Text.Trim(),
            TelNo = txtTEL.Text.Trim(),
            FaxNo = txtFAX.Text.Trim(),
            SksCd = txtSKCD.Text.Trim(),
            ShsCd = txtSSCD.Text.Trim(),
            GpkCd = (int)LegacyTextHelper.VbVal(txtGKCD.Text),
            MakCd = (int)LegacyTextHelper.VbVal(txtMCD.Text),
            SzCd = (int)LegacyTextHelper.VbVal(txtSZG.Text),
            BkCd = (int)LegacyTextHelper.VbVal(txtBKG.Text),
            MbnCd = (int)LegacyTextHelper.VbVal(txtMBN.Text),
            SknmCd = (int)LegacyTextHelper.VbVal(txtSKNM.Text),
            Seibt = optDS.Checked ? 0 : 1,
            Birthday = (long)LegacyTextHelper.VbVal(txtNEN.Text) * 10000L + (long)LegacyTextHelper.VbVal(txtTUKI.Text) * 100L + (long)LegacyTextHelper.VbVal(txtHI.Text),
            SinNo = (int)LegacyTextHelper.VbVal(txtSBG.Text),
            HhknNo = (int)LegacyTextHelper.VbVal(txtHBG.Text),
            DrpDay = (long)LegacyTextHelper.VbVal(txtTNEN.Text) * 10000L + (long)LegacyTextHelper.VbVal(txtTTUKI.Text) * 100L + (long)LegacyTextHelper.VbVal(txtTHI.Text),
            TkKb = optNO.Checked ? 0 : 1,
            Jigyo = (int)LegacyTextHelper.VbVal(txtJIGCD.Text),
            CardNo = cardNo,
            TorokuNo = txtJIGYONO.Text.Trim(),
            KazeiKb = optKAZEI.Checked ? 0 : 1,

            // 2画面 (請求支払条件)
            Ssdd1 = _state.F230, IngtKb1 = _state.F240, Indd1 = _state.F250,
            Ssdd2 = _state.F260, IngtKb2 = _state.F270, Indd2 = _state.F280,
            Ssdd3 = _state.F290, IngtKb3 = _state.F300, Indd3 = _state.F310,
            SzkKb = _state.F320, SsyoKb = _state.F330, ThyoKb = _state.F340,
            Shsdd1 = _state.F350, ShgtKb1 = _state.F360, Shydd1 = _state.F370,
            Shsdd2 = _state.F380, ShgtKb2 = _state.F390, Shydd2 = _state.F400,
            Shsdd3 = _state.F410, ShgtKb3 = _state.F420, Shydd3 = _state.F430,
            SyhzkKb = _state.F440,
            ShrHoho = _state.F800, Site = _state.F810, KsMm = _state.F820, KsDd = _state.F830,

            // 3画面 (銀行口座情報)
            GkCd1 = _state.F450, KzSbt1 = _state.F460, KzNo1 = _state.F470, KzMegi1 = _state.F480,
            GkCd2 = _state.F490, KzSbt2 = _state.F500, KzNo2 = _state.F510, KzMegi2 = _state.F520
        };

        try
        {
            if (_state.UpFlg40 == 0)
            {
                _service.InsertCustomer(entity);
                MessageBox.Show("登録されました。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _service.UpdateCustomer(entity);
                MessageBox.Show("更新されました。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "顧客マスタ", txtKCD.Text, ex);
            return;
        }

        AllClear();
        txtKCD.Text = string.Empty;
        txtKCD.Focus();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        AllClear();
        txtKCD.Text = string.Empty;
        txtKCD.Focus();
    }

    private void cmdDEL_Click(object? sender, EventArgs e)
    {
        DialogResult res = MessageBox.Show("削除しますか？", ScreenTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (res == DialogResult.No)
        {
            AllClear();
            txtKCD.Text = string.Empty;
            txtKCD.Focus();
            return;
        }

        try
        {
            _service.SoftDeleteCustomer(txtKCD.Text, DateTime.Today);
            MessageBox.Show("削除されました。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "顧客マスタ", txtKCD.Text, ex);
            return;
        }

        AllClear();
        txtKCD.Text = string.Empty;
        txtKCD.Focus();
    }

    private void cmdEND_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private static bool ValidateCodeLabelPair(TextBox txt, Label lbl, string message)
    {
        if (!string.IsNullOrEmpty(txt.Text.Trim()) && string.IsNullOrEmpty(lbl.Text.Trim()))
        {
            MessageBox.Show(message, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txt.Text = string.Empty;
            lbl.Text = string.Empty;
            txt.Focus();
            return false;
        }
        return true;
    }

    private static bool IsAllFilledOrAllEmpty(string a, string b, string c)
    {
        bool fa = !string.IsNullOrEmpty(a.Trim());
        bool fb = !string.IsNullOrEmpty(b.Trim());
        bool fc = !string.IsNullOrEmpty(c.Trim());
        return (fa && fb && fc) || (!fa && !fb && !fc);
    }

    // **********************************
    //  顧客コード (txtKCD)
    // **********************************
    private void txtKCD_Enter(object? sender, EventArgs e)
    {
        cmdSET.Enabled = false;
        cmdDEL.Enabled = false;
        cmdGKJ.Enabled = false;
        cmdSSJ.Enabled = false;
    }

    private void txtKCD_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtKCD.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optGP.Checked) optGP.Focus();
            else if (optJG.Checked) optJG.Focus();
            else if (optIPKG.Checked) optIPKG.Focus();
            else if (optIPKJ.Checked) optIPKJ.Focus();
        }
    }

    private void txtKCD_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            txtKCD.Text = txtKCD.Text.Trim();
            long val = (long)LegacyTextHelper.VbVal(txtKCD.Text);
            if (string.IsNullOrEmpty(txtKCD.Text) || val == 0)
            {
                txtKCD.Text = string.Empty;
                return;
            }

            txtKCD.Text = val.ToString("0000000000", CultureInfo.InvariantCulture);

            if (!OnelistSet())
            {
                return;
            }

            if (_state.UpFlg40 == 0)
            {
                cmdSET.Enabled = true;
                cmdDEL.Enabled = false;
                cmdGKJ.Enabled = true;
                cmdSSJ.Enabled = true;
            }
            else
            {
                _state.SetFlg3 = 1;
                cmdSET.Enabled = true;
                cmdDEL.Enabled = true;
                cmdGKJ.Enabled = true;
                cmdSSJ.Enabled = true;
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    // **********************************
    //  名称・住所・連絡先フィールド
    // **********************************
    private void optCustomerType_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; txtKNMK.Focus(); }
    }

    private void txtKNMK_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtKNMK, txtKNMKJ, e);

    private void txtKNMK_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            string trimmed = txtKNMK.Text.Trim();
            if (string.IsNullOrEmpty(trimmed)) return;

            if (LegacyTextHelper.CheckMojisu(txtKNMK.Text, 20) > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtKNMK.Focus());
                return;
            }

            if (trimmed.Length != LegacyTextHelper.GetShiftJisByteCount(trimmed))
            {
                MessageBox.Show("全角が混在しています。半角カナで入力してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtKNMK.Focus());
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void txtKNMKJ_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtKNMKJ, txtKNMKJ2, e);

    private void txtKNMKJ_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (LegacyTextHelper.CheckMojisu(txtKNMKJ.Text, 26) > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtKNMKJ.Focus());
                return;
            }

            if (string.IsNullOrEmpty(txtKNMKJ.Text.Trim())) return;

            if (_state.UpFlg40 == 0)
            {
                txtTENAME.Text = txtKNMKJ.Text;
                txtKNMS.Text = LegacyTextHelper.SubStringByByte(txtKNMKJ.Text, 16);
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void txtKNMKJ2_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtKNMKJ2, txtTENAME, e);

    private void txtKNMKJ2_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (LegacyTextHelper.CheckMojisu("a" + txtKNMKJ2.Text, 15) > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtKNMKJ2.Focus());
                return;
            }

            if (string.IsNullOrEmpty(txtKNMKJ2.Text.Trim())) return;

            if (_state.UpFlg40 == 0)
            {
                txtTENAME.Text = txtKNMKJ.Text + txtKNMKJ2.Text;
                txtKNMS.Text = LegacyTextHelper.SubStringByByte(txtKNMKJ.Text, 16);
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void txtTENAME_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtTENAME, txtKNMS, e);
    private void txtTENAME_Leave(object? sender, EventArgs e) => ValidateByteLimit(txtTENAME, 40);

    private void txtKNMS_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtKNMS, txtUBBG, e);
    private void txtKNMS_Leave(object? sender, EventArgs e) => ValidateByteLimit(txtKNMS, 16);

    private void txtUBBG_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtUBBG, txtJS1, e);

    private void txtJS1_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtJS1, txtJS2, e);
    private void txtJS1_Leave(object? sender, EventArgs e) => ValidateByteLimit(txtJS1, 30);

    private void txtJS2_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtJS2, txtJS3, e);
    private void txtJS2_Leave(object? sender, EventArgs e) => ValidateByteLimit(txtJS2, 30);

    private void txtJS3_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtJS3, txtTEL, e);
    private void txtJS3_Leave(object? sender, EventArgs e) => ValidateByteLimit(txtJS3, 30);

    private void txtTEL_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtTEL, txtFAX, e);
    private void txtFAX_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtFAX, txtGKCD, e);

    private void ValidateByteLimit(TextBox txt, int maxBytes)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (LegacyTextHelper.CheckMojisu(txt.Text, maxBytes) > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txt.Focus());
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    // **********************************
    //  グループ / 事業所 / 請求先 / 支払先 / メーカー
    // **********************************
    private void txtGKCD_KeyDown(object? sender, KeyEventArgs e) => HandleCodeNav(txtGKCD, lblGKCD, txtJIGCD, e);

    private void txtGKCD_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            txtGKCD.Text = txtGKCD.Text.Trim();
            int scd = (int)LegacyTextHelper.VbVal(txtGKCD.Text);
            if (string.IsNullOrEmpty(txtGKCD.Text) || scd == 0)
            {
                txtGKCD.Text = string.Empty;
                lblGKCD.Text = string.Empty;
                _state.Siha = 0;
                _state.SihFlg = 0;
                return;
            }

            txtGKCD.Text = scd.ToString("000", CultureInfo.InvariantCulture);
            Kuno0040GroupInfoDto? grp;
            try
            {
                grp = _service.GetActiveGroupInfo(scd);
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "ｸﾞﾙｰﾌﾟ会社ﾏｽﾀ", scd.ToString("000", CultureInfo.InvariantCulture), ex);
                return;
            }

            if (grp is null)
            {
                MessageBox.Show("指定されたグループ会社コードは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtGKCD.Text = string.Empty;
                lblGKCD.Text = string.Empty;
                BeginInvoke(() => txtGKCD.Focus());
                return;
            }

            lblGKCD.Text = grp.ShortName;
            if (_state.UpFlg40 == 0 && optJG.Checked)
            {
                string rawKysCd = grp.KysCd.Trim();
                string skcd = string.IsNullOrEmpty(rawKysCd)
                    ? string.Empty
                    : ((long)LegacyTextHelper.VbVal(rawKysCd)).ToString("0000000000", CultureInfo.InvariantCulture);
                txtSKCD.Text = skcd;
                string? snm = string.IsNullOrEmpty(skcd) ? null : _service.GetActiveCustomerShortName(skcd);
                if (snm is not null)
                {
                    lblSKCD.Text = snm;
                    _state.SihFlg = 1;
                    _state.Siha = grp.KyoDd;
                }
                else
                {
                    lblSKCD.Text = string.Empty;
                    _state.SihFlg = 0;
                    _state.Siha = 0;
                }
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void txtJIGCD_KeyDown(object? sender, KeyEventArgs e) => HandleCodeNav(txtJIGCD, lblJIGCD, txtSKCD, e);
    private void txtJIGCD_Leave(object? sender, EventArgs e) => HandleMeisLeave(txtJIGCD, lblJIGCD, 18, "000", "指定された事業所記号は存在しません。");

    private void txtSKCD_KeyDown(object? sender, KeyEventArgs e) => HandleCodeNav(txtSKCD, lblSKCD, txtSSCD, e);
    private void txtSKCD_Leave(object? sender, EventArgs e) => HandleCustomerRefLeave(txtSKCD, lblSKCD, "指定された請求先コードは存在しません。");

    private void txtSSCD_KeyDown(object? sender, KeyEventArgs e) => HandleCodeNav(txtSSCD, lblSSCD, txtMCD, e);
    private void txtSSCD_Leave(object? sender, EventArgs e) => HandleCustomerRefLeave(txtSSCD, lblSSCD, "指定された支払先コードは存在しません。");

    private void txtMCD_KeyDown(object? sender, KeyEventArgs e) => HandleCodeNav(txtMCD, lblMCD, txtSZG, e);
    private void txtMCD_Leave(object? sender, EventArgs e) => HandleMeisLeave(txtMCD, lblMCD, 9, "0000", "指定されたメーカーコードは存在しません。");

    // **********************************
    //  所属 / 部課 / 身分 / 職名 / 性別
    // **********************************
    private void txtSZG_KeyDown(object? sender, KeyEventArgs e) => HandleCodeNav(txtSZG, lblSZG, txtBKG, e);

    private void txtSZG_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            txtSZG.Text = txtSZG.Text.Trim();
            int scd = (int)LegacyTextHelper.VbVal(txtSZG.Text);
            if (string.IsNullOrEmpty(txtSZG.Text) || scd == 0)
            {
                txtSZG.Text = string.Empty;
                lblSZG.Text = string.Empty;
                return;
            }

            txtSZG.Text = scd.ToString("000", CultureInfo.InvariantCulture);
            string? snm;
            try
            {
                snm = _service.GetActiveAffiliationShortName(scd);
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "所属マスタ", scd.ToString("000", CultureInfo.InvariantCulture), ex);
                return;
            }

            if (snm is null)
            {
                MessageBox.Show("指定された所属コードは存在しません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSZG.Text = string.Empty;
                lblSZG.Text = string.Empty;
                BeginInvoke(() => txtSZG.Focus());
                return;
            }

            lblSZG.Text = snm;
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void txtBKG_KeyDown(object? sender, KeyEventArgs e) => HandleCodeNav(txtBKG, lblBKG, txtMBN, e);
    private void txtBKG_Leave(object? sender, EventArgs e) => HandleMeisLeave(txtBKG, lblBKG, 3, "00", "指定された部課は存在しません。");

    private void txtMBN_KeyDown(object? sender, KeyEventArgs e) => HandleCodeNav(txtMBN, lblMBN, txtSKNM, e);
    private void txtMBN_Leave(object? sender, EventArgs e) => HandleMeisLeave(txtMBN, lblMBN, 5, "00", "指定された身分は存在しません。");

    private void txtSKNM_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; txtSKNM.Text = string.Empty; lblSKNM.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optDS.Checked) optDS.Focus();
            else if (optJS.Checked) optJS.Focus();
        }
    }

    private void txtSKNM_Leave(object? sender, EventArgs e) => HandleMeisLeave(txtSKNM, lblSKNM, 6, "00", "指定された職名は存在しません。");

    private void optGender_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; txtNEN.Focus(); }
    }

    // **********************************
    //  生年月日 / 社員番号 / 被保険者番号 / 退職日
    // **********************************
    private void txtNEN_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtNEN, txtTUKI, e);
    private void txtNEN_Leave(object? sender, EventArgs e) => ValidateYearLeave(txtNEN);

    private void txtTUKI_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtTUKI, txtHI, e);
    private void txtTUKI_Leave(object? sender, EventArgs e) => ValidateMonthDayLeave(txtTUKI, txtHI, isMonthField: true);

    private void txtHI_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtHI, txtSBG, e);
    private void txtHI_Leave(object? sender, EventArgs e) => ValidateMonthDayLeave(txtTUKI, txtHI, isMonthField: false);

    private void txtSBG_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtSBG, txtHBG, e);
    private void txtSBG_Leave(object? sender, EventArgs e) => FormatFixedDigits(txtSBG, "00000");

    private void txtHBG_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtHBG, txtTNEN, e);
    private void txtHBG_Leave(object? sender, EventArgs e) => FormatFixedDigits(txtHBG, "00000");

    private void txtTNEN_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtTNEN, txtTTUKI, e);
    private void txtTNEN_Leave(object? sender, EventArgs e) => ValidateYearLeave(txtTNEN);

    private void txtTTUKI_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtTTUKI, txtTHI, e);
    private void txtTTUKI_Leave(object? sender, EventArgs e) => ValidateMonthDayLeave(txtTTUKI, txtTHI, isMonthField: true);

    private void txtTHI_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; txtTHI.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (optNO.Checked) optNO.Focus();
            else if (optYES.Checked) optYES.Focus();
        }
    }

    private void txtTHI_Leave(object? sender, EventArgs e) => ValidateMonthDayLeave(txtTTUKI, txtTHI, isMonthField: false);

    private void optCommute_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; txtCARDNO.Focus(); }
    }

    private void txtCARDNO_KeyDown(object? sender, KeyEventArgs e) => HandleNav(txtCARDNO, txtJIGYONO, e);
    private void txtCARDNO_Leave(object? sender, EventArgs e) => FormatFixedDigits(txtCARDNO, "0000000000");

    // **********************************
    //  インボイス対応 (課税業者登録番号・課税免税区分)
    // **********************************
    private void txtJIGYONO_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            txtJIGYONO.Text = string.Empty;
        }
        else if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (!string.IsNullOrEmpty(txtJIGYONO.Text))
            {
                optKAZEI.Checked = true;
            }

            if (optKAZEI.Checked) optKAZEI.Focus();
            else if (optMENZEI.Checked) optMENZEI.Focus();
        }
    }

    private void txtJIGYONO_Leave(object? sender, EventArgs e)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (string.IsNullOrEmpty(txtJIGYONO.Text))
            {
                return;
            }

            int rtc = LegacyTextHelper.CheckMojisu(txtJIGYONO.Text, 14);
            if (rtc > 9)
            {
                MessageBox.Show("桁数オーバーです。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtJIGYONO.Focus());
                return;
            }

            if (rtc != 0)
            {
                MessageBox.Show("全角が混在しています。半角で入力してください。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BeginInvoke(() => txtJIGYONO.Focus());
                return;
            }

            optKAZEI.Checked = true;
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private void optTaxExempt_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            e.SuppressKeyPress = true;
            if (cmdSSJ.Enabled) cmdSSJ.Focus();
            else if (cmdEND.Enabled) cmdEND.Focus();
        }
    }

    // **********************************
    //  共通検証・スピン・ヘルパー
    // **********************************
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

            if (_state.UpFlg40 == 0 && scd == txtKCD.Text)
            {
                lbl.Text = txtKNMS.Text;
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
                MessageBox.Show(notFoundMsg, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt.Text = string.Empty;
                lbl.Text = string.Empty;
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

    private void HandleMeisLeave(TextBox txt, Label lbl, int recKb, string format, string notFoundMsg)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            txt.Text = txt.Text.Trim();
            int scd = (int)LegacyTextHelper.VbVal(txt.Text);
            if (string.IsNullOrEmpty(txt.Text) || scd == 0)
            {
                txt.Text = string.Empty;
                lbl.Text = string.Empty;
                return;
            }

            txt.Text = scd.ToString(format, CultureInfo.InvariantCulture);
            string? snm;
            try
            {
                snm = _service.GetActiveMeisShortName(recKb, scd);
            }
            catch (Exception ex)
            {
                LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称マスタ", $"{recKb}:{scd}", ex);
                return;
            }

            if (snm is null)
            {
                MessageBox.Show(notFoundMsg, ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt.Text = string.Empty;
                lbl.Text = string.Empty;
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

    private void ValidateYearLeave(TextBox txt)
    {
        txt.Text = txt.Text.Trim();
        int val = (int)LegacyTextHelper.VbVal(txt.Text);
        if (string.IsNullOrEmpty(txt.Text) || val == 0)
        {
            txt.Text = string.Empty;
            return;
        }
        txt.Text = val.ToString("0000", CultureInfo.InvariantCulture);
    }

    private void ValidateMonthDayLeave(TextBox txtMonth, TextBox txtDay, bool isMonthField)
    {
        if (_isHandlingLeave) return;
        _isHandlingLeave = true;
        try
        {
            if (isMonthField)
            {
                int m = (int)LegacyTextHelper.VbVal(txtMonth.Text.Trim());
                if (string.IsNullOrEmpty(txtMonth.Text.Trim()) || m == 0) { txtMonth.Text = string.Empty; return; }
                txtMonth.Text = m.ToString("00", CultureInfo.InvariantCulture);
                if (m < 1 || m > 12)
                {
                    MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMonth.Text = string.Empty;
                    BeginInvoke(() => txtMonth.Focus());
                    return;
                }

                int d = (int)LegacyTextHelper.VbVal(txtDay.Text.Trim());
                if (string.IsNullOrEmpty(txtDay.Text.Trim()) || d == 0) { txtDay.Text = string.Empty; return; }
                txtDay.Text = d.ToString("00", CultureInfo.InvariantCulture);
                if (!IsValidDayForMonth(m, d))
                {
                    MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDay.Text = string.Empty;
                    BeginInvoke(() => txtDay.Focus());
                }
            }
            else
            {
                int d = (int)LegacyTextHelper.VbVal(txtDay.Text.Trim());
                if (string.IsNullOrEmpty(txtDay.Text.Trim()) || d == 0) { txtDay.Text = string.Empty; return; }
                txtDay.Text = d.ToString("00", CultureInfo.InvariantCulture);
                if (d < 1 || d > 31)
                {
                    MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDay.Text = string.Empty;
                    BeginInvoke(() => txtDay.Focus());
                    return;
                }

                int m = (int)LegacyTextHelper.VbVal(txtMonth.Text.Trim());
                if (string.IsNullOrEmpty(txtMonth.Text.Trim()) || m == 0) { txtMonth.Text = string.Empty; return; }
                if (m < 1 || m > 12)
                {
                    MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMonth.Text = string.Empty;
                    BeginInvoke(() => txtMonth.Focus());
                    return;
                }
                if (!IsValidDayForMonth(m, d))
                {
                    MessageBox.Show("指定された日付は正しくありません。", ScreenTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDay.Text = string.Empty;
                    BeginInvoke(() => txtDay.Focus());
                }
            }
        }
        finally
        {
            _isHandlingLeave = false;
        }
    }

    private static bool IsValidDayForMonth(int month, int day) => month switch
    {
        1 or 3 or 5 or 7 or 8 or 10 or 12 => day >= 1 && day <= 31,
        4 or 6 or 9 or 11 => day >= 1 && day <= 30,
        2 => day >= 1 && day <= 29,
        _ => false
    };

    private static void FormatFixedDigits(TextBox txt, string format)
    {
        txt.Text = txt.Text.Trim();
        long val = (long)LegacyTextHelper.VbVal(txt.Text);
        txt.Text = (string.IsNullOrEmpty(txt.Text) || val == 0)
            ? string.Empty
            : val.ToString(format, CultureInfo.InvariantCulture);
    }

    private static void SpinYear(TextBox txt, int delta)
    {
        if (string.IsNullOrEmpty(txt.Text.Trim())) { txt.Focus(); return; }
        int val = (int)LegacyTextHelper.VbVal(txt.Text) + delta;
        txt.Text = Math.Max(0, val).ToString("0000", CultureInfo.InvariantCulture);
        txt.Refresh();
        txt.Focus();
    }

    private static void SpinMonth(TextBox txt, int delta)
    {
        if (string.IsNullOrEmpty(txt.Text.Trim())) { txt.Focus(); return; }
        int val = (int)LegacyTextHelper.VbVal(txt.Text);
        if ((delta < 0 && val == 1) || (delta > 0 && val == 12)) { txt.Focus(); return; }
        txt.Text = (val + delta).ToString("00", CultureInfo.InvariantCulture);
        txt.Refresh();
        txt.Focus();
    }

    private static void SpinDay(TextBox txt, int delta)
    {
        if (string.IsNullOrEmpty(txt.Text.Trim())) { txt.Focus(); return; }
        int val = (int)LegacyTextHelper.VbVal(txt.Text);
        if ((delta < 0 && val == 1) || (delta > 0 && val == 31)) { txt.Focus(); return; }
        txt.Text = (val + delta).ToString("00", CultureInfo.InvariantCulture);
        txt.Refresh();
        txt.Focus();
    }

    private static void HandleNav(TextBox current, Control next, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; current.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; next.Focus(); }
    }

    private static void HandleCodeNav(TextBox current, Label lbl, Control next, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) { e.SuppressKeyPress = true; current.Text = string.Empty; lbl.Text = string.Empty; }
        else if (e.KeyCode == Keys.Return) { e.SuppressKeyPress = true; next.Focus(); }
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
