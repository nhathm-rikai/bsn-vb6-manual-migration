using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Common;

/// <summary>
/// TAIKEN — 単位コード検索 (VB6 frmTAIKEN / TAIKEN.BAS 1:1 移行画面)
/// </summary>
public partial class FrmTAIKEN : Form
{
    private const string ScreenTitle = "TAIKEN";

    private readonly IKuno0010Service _service;
    private IReadOnlyList<UnitLookupItem> _items = Array.Empty<UnitLookupItem>();

    public bool Selected { get; private set; }
    public string SelectedUnitCode { get; private set; } = string.Empty;
    public string SelectedUnitShortName { get; private set; } = string.Empty;

    public FrmTAIKEN(IKuno0010Service service)
    {
        _service = service;
        InitializeComponent();
    }

    private void FrmTAIKEN_Load(object? sender, EventArgs e)
    {
        // システム日付設定 (yy/mm/dd)
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        try
        {
            _items = _service.GetUnitLookupList();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("SYOKEN", "名称ﾏｽﾀ･単位ｺｰﾄﾞ検索", "ﾚｺｰﾄﾞ区分 16", ex);
            Close();
            return;
        }

        cmbTCD.Items.Clear();
        if (_items.Count == 0)
        {
            return;
        }

        foreach (UnitLookupItem item in _items)
        {
            cmbTCD.Items.Add(item.DisplayText);
        }
    }

    private void cmdRET_Click(object? sender, EventArgs e)
    {
        if (cmbTCD.SelectedIndex < 0 || cmbTCD.SelectedIndex >= _items.Count)
        {
            Selected = false;
            SelectedUnitCode = string.Empty;
            SelectedUnitShortName = string.Empty;
            Close();
            return;
        }

        UnitLookupItem chosen = _items[cmbTCD.SelectedIndex];
        Selected = true;
        SelectedUnitCode = chosen.CodeText;
        SelectedUnitShortName = chosen.ShortName;
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        Selected = false;
        SelectedUnitCode = string.Empty;
        SelectedUnitShortName = string.Empty;
        Close();
    }
}

