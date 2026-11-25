using System.Globalization;
using Application.DTOs;
using Application.Services;
using KousyoERP.Common;

namespace KousyoERP.Forms.Common;

/// <summary>
/// KYKKEN — 顧客コード検索 (VB6 cmn/KYKKEN.FRM 1:1 移行画面)
/// </summary>
public partial class FrmKYKKEN : Form
{
    private const string GlbKana = "ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜ";
    private const string GlbAlpUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string GlbAlpLower = "abcdefghijklmnopqrstuvwxyz";

    private readonly IKuno0040Service _service;
    private readonly RadioButton[] _optKN = new RadioButton[44];
    private readonly RadioButton[] _optAL = new RadioButton[26];

    private IReadOnlyList<GpkenItemDto> _groups = Array.Empty<GpkenItemDto>();
    private IReadOnlyList<KykkenCustomerItemDto> _customers = Array.Empty<KykkenCustomerItemDto>();

    public bool IsSelected { get; private set; }
    public string SelectedCustomerCode { get; private set; } = string.Empty;
    public string SelectedCustomerShortName { get; private set; } = string.Empty;

    public FrmKYKKEN()
        : this(CreateDefaultService())
    {
    }

    public FrmKYKKEN(IKuno0040Service service)
    {
        _service = service;
        InitializeComponent();
        InitializeKanaAlphaButtons();
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

    private void InitializeKanaAlphaButtons()
    {
        var font = new Font("ＭＳ Ｐゴシック", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
        var bg = Color.FromArgb(192, 192, 192);

        // カナ44文字 (10列: ｱ..ｵ, ｶ..ｺ, ｻ..ｿ, ﾀ..ﾄ, ﾅ..ﾉ, ﾊ..ﾎ, ﾏ..ﾓ, ﾔﾕﾖ, ﾗ..ﾛ, ﾜ)
        int[] colStarts = { 0, 5, 10, 15, 20, 25, 30, 35, 38, 43, 44 };
        for (int col = 0; col < 10; col++)
        {
            int start = colStarts[col];
            int end = colStarts[col + 1];
            for (int row = 0; row < end - start; row++)
            {
                int idx = start + row;
                var rb = new RadioButton
                {
                    Name = $"optKN_{idx + 1}",
                    Text = GlbKana[idx].ToString(),
                    Font = font,
                    BackColor = bg,
                    UseVisualStyleBackColor = false,
                    Location = new Point(8 + col * 40, 24 + row * 24),
                    Size = new Size(38, 22),
                    Tag = idx + 1
                };
                rb.Click += optKN_Click;
                _optKN[idx] = rb;
                Frame2.Controls.Add(rb);
            }
        }

        // 英字26文字 (6列: A-E, F-J, K-O, P-T, U-Y, Z)
        for (int i = 0; i < 26; i++)
        {
            int col = i / 5;
            int row = i % 5;
            var rb = new RadioButton
            {
                Name = $"optAL_{i + 1}",
                Text = GlbAlpUpper[i].ToString(),
                Font = font,
                BackColor = bg,
                UseVisualStyleBackColor = false,
                Location = new Point(416 + col * 40, 24 + row * 24),
                Size = new Size(38, 22),
                Tag = i + 1
            };
            rb.Click += optAL_Click;
            _optAL[i] = rb;
            Frame2.Controls.Add(rb);
        }
    }

    private void FrmKYKKEN_Load(object? sender, EventArgs e)
    {
        lblYMD.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);

        try
        {
            _groups = _service.GetAllActiveGroups();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("KYKKEN", "ｸﾞﾙｰﾌﾟ会社ﾏｽﾀ", "全ﾃﾞｰﾀ対象", ex);
            return;
        }

        cmbGPCD.Items.Clear();
        foreach (GpkenItemDto grp in _groups)
        {
            cmbGPCD.Items.Add(grp.DisplayText);
        }
    }

    private void ClearCustomerList()
    {
        _customers = Array.Empty<KykkenCustomerItemDto>();
        cmbKCD.Items.Clear();
    }

    private void cmbGPCD_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ClearCustomerList();
    }

    private void optGP_Click(object? sender, EventArgs e) => ClearCustomerList();
    private void optJN_Click(object? sender, EventArgs e) => ClearCustomerList();
    private void optKN_Click(object? sender, EventArgs e) => ClearCustomerList();
    private void optAL_Click(object? sender, EventArgs e) => ClearCustomerList();
    private void optNG_Click(object? sender, EventArgs e) => ClearCustomerList();

    private void optIG_Click(object? sender, EventArgs e)
    {
        ClearCustomerList();
        SelectGroupByPrefix("997");
    }

    private void optIK_Click(object? sender, EventArgs e)
    {
        ClearCustomerList();
        SelectGroupByPrefix("999");
    }

    private void SelectGroupByPrefix(string prefix)
    {
        for (int i = 0; i < _groups.Count; i++)
        {
            if (_groups[i].CodeText == prefix)
            {
                cmbGPCD.SelectedIndex = i;
                break;
            }
        }
    }

    private void cmdSER_Click(object? sender, EventArgs e)
    {
        ClearCustomerList();

        if (!optGP.Checked && !optJN.Checked && !optIG.Checked && !optIK.Checked)
        {
            return;
        }

        if (cmbGPCD.SelectedIndex < 0 || cmbGPCD.SelectedIndex >= _groups.Count)
        {
            return;
        }

        if (!TryGetSelectedKanaOrAlpha(out int syubetu, out string moji, out string moji2))
        {
            return;
        }

        int kykbn = GetSelectedCustomerType();
        int gpcd = _groups[cmbGPCD.SelectedIndex].Code;

        Cursor = Cursors.WaitCursor;
        try
        {
            _customers = _service.SearchCustomersByKanaOrAlpha(syubetu, moji, moji2, kykbn, gpcd);
            PopulateCustomerCombo();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("KYKKEN", "顧客ﾏｽﾀ", "全ﾃﾞｰﾀ対象", ex);
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
        ClearCustomerList();

        KykkenNameMatchMode matchMode = Option1.Checked
            ? KykkenNameMatchMode.Partial
            : Option2.Checked
                ? KykkenNameMatchMode.Prefix
                : KykkenNameMatchMode.Suffix;

        int? kykbn = Check1.Checked ? GetSelectedCustomerType() : null;
        int? gpcd = (Check2.Checked && cmbGPCD.SelectedIndex >= 0 && cmbGPCD.SelectedIndex < _groups.Count)
            ? _groups[cmbGPCD.SelectedIndex].Code
            : null;

        Cursor = Cursors.WaitCursor;
        try
        {
            _customers = _service.SearchCustomersByName(Text1.Text, matchMode, Check3.Checked, kykbn, gpcd);
            PopulateCustomerCombo();
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("KYKKEN", "顧客ﾏｽﾀ", "全ﾃﾞｰﾀ対象", ex);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void PopulateCustomerCombo()
    {
        cmbKCD.Items.Clear();
        if (_customers.Count == 0)
        {
            return;
        }

        foreach (KykkenCustomerItemDto item in _customers)
        {
            cmbKCD.Items.Add(item.DisplayText);
        }

        cmbKCD.SelectedIndex = 0;
    }

    private bool TryGetSelectedKanaOrAlpha(out int syubetu, out string moji, out string moji2)
    {
        if (optNG.Checked)
        {
            syubetu = 3;
            moji = "ﾆｲｶﾞﾀ";
            moji2 = string.Empty;
            return true;
        }

        for (int i = 0; i < _optKN.Length; i++)
        {
            if (_optKN[i].Checked)
            {
                // kn = 22 (1-based) は 'ﾆ' -> 種別4 (ﾆｲｶﾞﾀ除く)
                syubetu = (i + 1 == 22) ? 4 : 1;
                moji = GlbKana[i].ToString();
                moji2 = string.Empty;
                return true;
            }
        }

        for (int i = 0; i < _optAL.Length; i++)
        {
            if (_optAL[i].Checked)
            {
                syubetu = 2;
                moji = GlbAlpUpper[i].ToString();
                moji2 = GlbAlpLower[i].ToString();
                return true;
            }
        }

        syubetu = 0;
        moji = string.Empty;
        moji2 = string.Empty;
        return false;
    }

    private int GetSelectedCustomerType()
    {
        if (optGP.Checked) return 0;
        if (optJN.Checked) return 1;
        if (optIG.Checked) return 2;
        if (optIK.Checked) return 3;
        return 0;
    }

    private void cmdRET_Click(object? sender, EventArgs e)
    {
        if (cmbKCD.SelectedIndex < 0 || cmbKCD.SelectedIndex >= _customers.Count)
        {
            IsSelected = false;
            SelectedCustomerCode = string.Empty;
            SelectedCustomerShortName = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }

        KykkenCustomerItemDto selected = _customers[cmbKCD.SelectedIndex];
        string shortName = string.Empty;
        try
        {
            shortName = _service.GetActiveCustomerShortName(selected.Code) ?? string.Empty;
        }
        catch (Exception ex)
        {
            LegacyErrorDialog.ShowSqlError("KSHO0010", "顧客マスタ", selected.Code, ex);
        }

        IsSelected = true;
        SelectedCustomerCode = selected.Code;
        SelectedCustomerShortName = shortName;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void cmdCAN_Click(object? sender, EventArgs e)
    {
        IsSelected = false;
        SelectedCustomerCode = string.Empty;
        SelectedCustomerShortName = string.Empty;
        DialogResult = DialogResult.Cancel;
        Close();
    }
}

