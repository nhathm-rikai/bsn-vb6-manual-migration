using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Common;

/// <summary>
/// SYOKEN — 商品コード検索 (VB6 frmSYOKEN / SYOKEN.BAS 1:1 移行画面)
/// </summary>
public partial class FrmSYOKEN : Form
{
    private const string ScreenTitle = "SYOKEN";

    private readonly ISyokenLookupService _service;

    private IReadOnlyList<SyokenCategoryItemDto> _daiItems = Array.Empty<SyokenCategoryItemDto>();
    private IReadOnlyList<SyokenCategoryItemDto> _tyuItems = Array.Empty<SyokenCategoryItemDto>();
    private IReadOnlyList<MakerLookupItem> _mekaItems = Array.Empty<MakerLookupItem>();
    private IReadOnlyList<SyokenProductItemDto> _productItems = Array.Empty<SyokenProductItemDto>();

    public bool Selected { get; private set; }
    public string SelectedProductCode { get; private set; } = string.Empty;
    public string SelectedProductName { get; private set; } = string.Empty;

    public FrmSYOKEN(ISyokenLookupService service)
    {
        _service = service;
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using var pen = new Pen(Color.Black, 1f);
        // コード検索枠 (Line1..Line4: X=8..536, Y=48..224)
        e.Graphics.DrawRectangle(pen, 8, 48, 528, 176);

        // 名称検索枠 (Line5..Line8: X=8..536, Y=248..328)
        e.Graphics.DrawRectangle(pen, 8, 248, 528, 80);
    }

    private void FrmSYOKEN_Load(object? sender, EventArgs e)
    {
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        try
        {
            _daiItems = _service.GetMajorCategories();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類ﾏｽﾀ", "大分類ｺｰﾄﾞ+中分類ｺｰﾄﾞ00検索", ex);
            return;
        }

        cmbDAI.Items.Clear();
        foreach (SyokenCategoryItemDto item in _daiItems)
        {
            cmbDAI.Items.Add(item.DisplayText);
        }
    }

    private void cmbDAI_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // 中・メーカー・商品コード削除
        cmbTYU.Items.Clear();
        _tyuItems = Array.Empty<SyokenCategoryItemDto>();

        cmbMCD.Items.Clear();
        _mekaItems = Array.Empty<MakerLookupItem>();

        cmbSCD.Items.Clear();
        _productItems = Array.Empty<SyokenProductItemDto>();

        if (cmbDAI.SelectedIndex < 0 || cmbDAI.SelectedIndex >= _daiItems.Count)
        {
            return;
        }

        int daiCd = _daiItems[cmbDAI.SelectedIndex].Code;
        try
        {
            _tyuItems = _service.GetMinorCategories(daiCd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品分類ﾏｽﾀ", $"大分類ｺｰﾄﾞ{daiCd:00}+中分類ｺｰﾄﾞ検索", ex);
            return;
        }

        foreach (SyokenCategoryItemDto item in _tyuItems)
        {
            cmbTYU.Items.Add(item.DisplayText);
        }
    }

    private void cmbTYU_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // メーカーコード・商品コード削除
        cmbMCD.Items.Clear();
        _mekaItems = Array.Empty<MakerLookupItem>();

        cmbSCD.Items.Clear();
        _productItems = Array.Empty<SyokenProductItemDto>();

        if (cmbTYU.SelectedIndex < 0 || cmbDAI.SelectedIndex < 0 || cmbDAI.SelectedIndex >= _daiItems.Count)
        {
            return;
        }

        int daiCd = _daiItems[cmbDAI.SelectedIndex].Code;
        try
        {
            _mekaItems = _service.GetMakersByMajorCategory(daiCd);
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "名称ﾏｽﾀ", "ﾚｺｰﾄﾞ区分 9", ex);
            return;
        }

        foreach (MakerLookupItem item in _mekaItems)
        {
            cmbMCD.Items.Add(item.DisplayText);
        }
    }

    private void cmbMCD_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // 商品コード削除処理
        cmbSCD.Items.Clear();
        _productItems = Array.Empty<SyokenProductItemDto>();
    }

    private void cmdSER_Click(object? sender, EventArgs e)
    {
        cmbSCD.Items.Clear();
        _productItems = Array.Empty<SyokenProductItemDto>();

        if (cmbDAI.SelectedIndex < 0 || cmbDAI.SelectedIndex >= _daiItems.Count)
        {
            return;
        }

        if (cmbTYU.SelectedIndex < 0 || cmbTYU.SelectedIndex >= _tyuItems.Count)
        {
            return;
        }

        int daiCd = _daiItems[cmbDAI.SelectedIndex].Code;
        int tyuCd = _tyuItems[cmbTYU.SelectedIndex].Code;
        int? mekaCd = (cmbMCD.SelectedIndex >= 0 && cmbMCD.SelectedIndex < _mekaItems.Count)
            ? _mekaItems[cmbMCD.SelectedIndex].Code
            : null;

        Cursor = Cursors.WaitCursor;
        try
        {
            _productItems = _service.SearchProductsByCategory(daiCd, tyuCd, mekaCd);
            PopulateProductCombo();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品ﾏｽﾀ", $"{daiCd:00},{tyuCd:00}", ex);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void Command1_Click(object? sender, EventArgs e)
    {
        SearchByName();
    }

    private void Text1_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Return)
        {
            e.Handled = true;
            SearchByName();
        }
    }

    private void SearchByName()
    {
        cmbSCD.Items.Clear();
        _productItems = Array.Empty<SyokenProductItemDto>();

        SyokenNameMatchMode matchMode = Option2.Checked
            ? SyokenNameMatchMode.Prefix
            : Option3.Checked
                ? SyokenNameMatchMode.Suffix
                : SyokenNameMatchMode.Partial;

        bool fuzzySearch = Check2.Checked;
        int? daiCd = null;
        int? tyuCd = null;
        int? mekaCd = null;

        if (Check1.Checked)
        {
            if (cmbDAI.SelectedIndex >= 0 && cmbDAI.SelectedIndex < _daiItems.Count)
            {
                daiCd = _daiItems[cmbDAI.SelectedIndex].Code;
                if (cmbTYU.SelectedIndex >= 0 && cmbTYU.SelectedIndex < _tyuItems.Count)
                {
                    tyuCd = _tyuItems[cmbTYU.SelectedIndex].Code;
                }
            }

            if (cmbMCD.SelectedIndex >= 0 && cmbMCD.SelectedIndex < _mekaItems.Count)
            {
                mekaCd = _mekaItems[cmbMCD.SelectedIndex].Code;
            }
        }

        Cursor = Cursors.WaitCursor;
        try
        {
            _productItems = _service.SearchProductsByName(
                Text1.Text,
                matchMode,
                fuzzySearch,
                daiCd,
                tyuCd,
                mekaCd);
            PopulateProductCombo();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError(ScreenTitle, "商品ﾏｽﾀ", "名称検索", ex);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void PopulateProductCombo()
    {
        if (_productItems.Count == 0)
        {
            return;
        }

        foreach (SyokenProductItemDto item in _productItems)
        {
            cmbSCD.Items.Add(item.DisplayText);
        }

        cmbSCD.SelectedIndex = 0;
    }

    private void cmdRET_Click(object? sender, EventArgs e)
    {
        if (cmbSCD.SelectedIndex < 0 || cmbSCD.SelectedIndex >= _productItems.Count)
        {
            Selected = false;
            SelectedProductCode = string.Empty;
            SelectedProductName = string.Empty;
            Close();
            return;
        }

        SyokenProductItemDto chosen = _productItems[cmbSCD.SelectedIndex];
        Selected = true;
        SelectedProductCode = chosen.CodeText;
        SelectedProductName = chosen.ShnNameKn;
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        Selected = false;
        SelectedProductCode = string.Empty;
        SelectedProductName = string.Empty;
        Close();
    }
}

