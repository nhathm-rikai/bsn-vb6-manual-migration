using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Common;

/// <summary>
/// GNKKEN — 銀行コード検索 (VB6 cmn/GNKKEN.FRM 1:1 移行画面)
/// </summary>
public partial class FrmGNKKEN : Form
{
    private readonly IKuno0040Service _service;
    private IReadOnlyList<GnkkenBankItemDto> _banks = Array.Empty<GnkkenBankItemDto>();
    private IReadOnlyList<GnkkenBranchItemDto> _branches = Array.Empty<GnkkenBranchItemDto>();

    public bool IsSelected { get; private set; }
    public string SelectedBankCode { get; private set; } = string.Empty;
    public string SelectedBankShortName { get; private set; } = string.Empty;

    public FrmGNKKEN()
        : this(CreateDefaultService())
    {
    }

    public FrmGNKKEN(IKuno0040Service service)
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

    private void FrmGNKKEN_Load(object? sender, EventArgs e)
    {
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        try
        {
            _banks = _service.GetActiveMainBanks();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("GNKKEN", "銀行ﾏｽﾀ", "銀行ｺｰﾄﾞ 下3桁000ｺｰﾄﾞ検索", ex);
            return;
        }

        cmbGCD.Items.Clear();
        foreach (GnkkenBankItemDto bank in _banks)
        {
            cmbGCD.Items.Add(bank.DisplayText);
        }
    }

    private void cmbGCD_SelectedIndexChanged(object? sender, EventArgs e)
    {
        cmbHSGCD.Items.Clear();
        _branches = Array.Empty<GnkkenBranchItemDto>();

        if (cmbGCD.SelectedIndex < 0 || cmbGCD.SelectedIndex >= _banks.Count)
        {
            return;
        }

        long mainCode = _banks[cmbGCD.SelectedIndex].Code;
        try
        {
            _branches = _service.GetActiveBranches(mainCode);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("GNKKEN", "銀行ﾏｽﾀ", "銀行ｺｰﾄﾞ 下3桁000ｺｰﾄﾞ検索", ex);
            return;
        }

        foreach (GnkkenBranchItemDto branch in _branches)
        {
            cmbHSGCD.Items.Add(branch.DisplayText);
        }
    }

    private void cmdRET_Click(object? sender, EventArgs e)
    {
        if (cmbHSGCD.SelectedIndex < 0 || cmbHSGCD.SelectedIndex >= _branches.Count)
        {
            IsSelected = false;
            SelectedBankCode = string.Empty;
            SelectedBankShortName = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }

        GnkkenBranchItemDto selected = _branches[cmbHSGCD.SelectedIndex];
        IsSelected = true;
        SelectedBankCode = selected.CodeText;
        SelectedBankShortName = selected.NameS;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        IsSelected = false;
        SelectedBankCode = string.Empty;
        SelectedBankShortName = string.Empty;
        DialogResult = DialogResult.Cancel;
        Close();
    }
}

