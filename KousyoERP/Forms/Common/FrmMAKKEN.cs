using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Common;

/// <summary>
/// MAKKEN — メーカーコード検索 (VB6 frmMAKKEN / MAKKEN.BAS 1:1 移行画面)
/// </summary> 
public partial class FrmMAKKEN : Form
{
    private const string ScreenTitle = "MAKKEN";

    private readonly IKuno0010Service _service;
    private readonly int _daiBnCd;
    private IReadOnlyList<MakerLookupItem> _items = Array.Empty<MakerLookupItem>();

    public bool Selected { get; private set; }
    public string SelectedMakerCode { get; private set; } = string.Empty;
    public string SelectedMakerShortName { get; private set; } = string.Empty;

    public FrmMAKKEN(IKuno0010Service service, int daiBnCd)
    {
        _service = service;
        _daiBnCd = daiBnCd;
        InitializeComponent();
    }

    private void FrmMAKKEN_Load(object? sender, EventArgs e)
    {
        // システム日付設定 (yy/mm/dd)
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        try
        {
            _items = _service.GetMakerLookupList(_daiBnCd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称ﾏｽﾀ･ﾒｰｶｰｺｰﾄﾞ検索", "ﾚｺｰﾄﾞ区分 9", ex);
            Close();
            return;
        }

        cmbMCD.Items.Clear();
        if (_items.Count == 0)
        {
            return;
        }

        foreach (MakerLookupItem item in _items)
        {
            cmbMCD.Items.Add(item.DisplayText);
        }
    }

    private void cmdRET_Click(object? sender, EventArgs e)
    {
        if (cmbMCD.SelectedIndex < 0 || cmbMCD.SelectedIndex >= _items.Count)
        {
            Selected = false;
            SelectedMakerCode = string.Empty;
            SelectedMakerShortName = string.Empty;
            Close();
            return;
        }

        MakerLookupItem chosen = _items[cmbMCD.SelectedIndex];
        Selected = true;
        SelectedMakerCode = chosen.CodeText;
        SelectedMakerShortName = chosen.ShortName;
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        Selected = false;
        SelectedMakerCode = string.Empty;
        SelectedMakerShortName = string.Empty;
        Close();
    }
}

