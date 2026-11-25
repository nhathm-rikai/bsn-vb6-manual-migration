using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Common;

/// <summary>
/// GPKEN — グループコード検索 (VB6 cmn/GPKEN.FRM 1:1 移行画面)
/// </summary>
public partial class FrmGPKEN : Form
{
    private readonly IKuno0040Service _service;
    private IReadOnlyList<GpkenItemDto> _items = Array.Empty<GpkenItemDto>();

    public bool IsSelected { get; private set; }
    public string SelectedGroupCode { get; private set; } = string.Empty;
    public string SelectedGroupShortName { get; private set; } = string.Empty;

    public FrmGPKEN()
        : this(CreateDefaultService())
    {
    }

    public FrmGPKEN(IKuno0040Service service)
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

    private void FrmGPKEN_Load(object? sender, EventArgs e)
    {
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        try
        {
            _items = _service.GetAllActiveGroups();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("GPKEN", "ｸﾞﾙｰﾌﾟｺｰﾄﾞ検索", "全件検索", ex);
            cmbGPCD.Items.Clear();
            return;
        }

        cmbGPCD.Items.Clear();
        foreach (GpkenItemDto item in _items)
        {
            cmbGPCD.Items.Add(item.DisplayText);
        }
    }

    private void cmdRET_Click(object? sender, EventArgs e)
    {
        if (cmbGPCD.SelectedIndex < 0 || cmbGPCD.SelectedIndex >= _items.Count)
        {
            IsSelected = false;
            SelectedGroupCode = string.Empty;
            SelectedGroupShortName = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }

        GpkenItemDto selected = _items[cmbGPCD.SelectedIndex];
        IsSelected = true;
        SelectedGroupCode = selected.CodeText;
        SelectedGroupShortName = selected.NameS;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        IsSelected = false;
        SelectedGroupCode = string.Empty;
        SelectedGroupShortName = string.Empty;
        DialogResult = DialogResult.Cancel;
        Close();
    }
}

