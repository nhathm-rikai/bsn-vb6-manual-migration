using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Common;

/// <summary>
/// TANKEN — 担当コード検索 (VB6 frmTANKEN / TANKEN.BAS 1:1 移行画面)
/// </summary>
public partial class FrmTANKEN : Form
{
    private const string ScreenTitle = "TANKEN";

    private readonly ITankenLookupService _service;
    private IReadOnlyList<TantLookupItem> _items = Array.Empty<TantLookupItem>();

    public bool Selected { get; private set; }
    public string SelectedTantCode { get; private set; } = string.Empty;
    public string SelectedTantShortName { get; private set; } = string.Empty;

    public FrmTANKEN(ITankenLookupService service)
    {
        _service = service;
        InitializeComponent();
    }

    private void FrmTANKEN_Load(object? sender, EventArgs e)
    {
        // システム日付設定 (yy/mm/dd)
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        try
        {
            _items = _service.GetTantLookupList();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "担当ｺｰﾄﾞ検索", "全件検索", ex);
            Close();
            return;
        }

        cmbTANCD.Items.Clear();
        if (_items.Count == 0)
        {
            return;
        }

        foreach (TantLookupItem item in _items)
        {
            cmbTANCD.Items.Add(item.DisplayText);
        }
    }

    private void cmdRET_Click(object? sender, EventArgs e)
    {
        if (cmbTANCD.SelectedIndex < 0 || cmbTANCD.SelectedIndex >= _items.Count)
        {
            Selected = false;
            SelectedTantCode = string.Empty;
            SelectedTantShortName = string.Empty;
            Close();
            return;
        }

        TantLookupItem chosen = _items[cmbTANCD.SelectedIndex];
        Selected = true;
        SelectedTantCode = chosen.CodeText;
        SelectedTantShortName = chosen.ShortName;
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        Selected = false;
        SelectedTantCode = string.Empty;
        SelectedTantShortName = string.Empty;
        Close();
    }
}

