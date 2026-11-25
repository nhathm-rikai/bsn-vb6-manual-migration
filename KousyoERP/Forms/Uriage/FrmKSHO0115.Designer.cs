#nullable enable
namespace KousyoERP.Forms.Uriage;

partial class FrmKSHO0115
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label lblTitle = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.GroupBox fraHANI = null!;
    private System.Windows.Forms.RadioButton optALL = null!;
    private System.Windows.Forms.RadioButton optHANI = null!;
    private System.Windows.Forms.TextBox txtFROM = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.TextBox txtTO = null!;
    private System.Windows.Forms.RadioButton OptTANT = null!;
    private System.Windows.Forms.TextBox TxtTANTCD = null!;
    private System.Windows.Forms.GroupBox fraKEISIKI = null!;
    private System.Windows.Forms.RadioButton opt1 = null!;
    private System.Windows.Forms.RadioButton opt2 = null!;
    private System.Windows.Forms.Label lblHANI = null!;
    private System.Windows.Forms.TextBox txtKNEN = null!;
    private System.Windows.Forms.Panel spnKNEN = null!;
    private System.Windows.Forms.Button btnSpnKNenUp = null!;
    private System.Windows.Forms.Button btnSpnKNenDown = null!;
    private System.Windows.Forms.Label lbl13 = null!;
    private System.Windows.Forms.TextBox txtKTUKI = null!;
    private System.Windows.Forms.Panel spnKTUKI = null!;
    private System.Windows.Forms.Button btnSpnKTukiUp = null!;
    private System.Windows.Forms.Button btnSpnKTukiDown = null!;
    private System.Windows.Forms.Label lbl14 = null!;
    private System.Windows.Forms.TextBox txtKHI = null!;
    private System.Windows.Forms.Panel spnKHI = null!;
    private System.Windows.Forms.Button btnSpnKHiUp = null!;
    private System.Windows.Forms.Button btnSpnKHiDown = null!;
    private System.Windows.Forms.Label lbl16 = null!;
    private System.Windows.Forms.Label Label4 = null!;
    private System.Windows.Forms.TextBox txtMNEN = null!;
    private System.Windows.Forms.Panel spnMNEN = null!;
    private System.Windows.Forms.Button btnSpnMNenUp = null!;
    private System.Windows.Forms.Button btnSpnMNenDown = null!;
    private System.Windows.Forms.Label lbl25 = null!;
    private System.Windows.Forms.TextBox txtMTUKI = null!;
    private System.Windows.Forms.Panel spnMTUKI = null!;
    private System.Windows.Forms.Button btnSpnMTukiUp = null!;
    private System.Windows.Forms.Button btnSpnMTukiDown = null!;
    private System.Windows.Forms.Label lbl26 = null!;
    private System.Windows.Forms.TextBox txtMHI = null!;
    private System.Windows.Forms.Panel spnMHI = null!;
    private System.Windows.Forms.Button btnSpnMHiUp = null!;
    private System.Windows.Forms.Button btnSpnMHiDown = null!;
    private System.Windows.Forms.Label lbl27 = null!;
    private System.Windows.Forms.Button cmdSTR = null!;
    private System.Windows.Forms.Button cmdCAN = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private static void ConfigureSpinPanel(
        System.Windows.Forms.Panel panel,
        System.Windows.Forms.Button btnUp,
        System.Windows.Forms.Button btnDown,
        string panelName,
        int x,
        int y,
        int tabIndex,
        System.EventHandler onUp,
        System.EventHandler onDown)
    {
        panel.Controls.Add(btnUp);
        panel.Controls.Add(btnDown);
        panel.Location = new System.Drawing.Point(x, y);
        panel.Name = panelName;
        panel.Size = new System.Drawing.Size(17, 25);
        panel.TabIndex = tabIndex;

        btnUp.BackColor = System.Drawing.SystemColors.Control;
        btnUp.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnUp.Location = new System.Drawing.Point(0, 0);
        btnUp.Name = panelName + "_Up";
        btnUp.Size = new System.Drawing.Size(17, 12);
        btnUp.TabIndex = 0;
        btnUp.TabStop = false;
        btnUp.Text = "▲";
        btnUp.UseVisualStyleBackColor = false;
        btnUp.Click += onUp;

        btnDown.BackColor = System.Drawing.SystemColors.Control;
        btnDown.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnDown.Location = new System.Drawing.Point(0, 12);
        btnDown.Name = panelName + "_Down";
        btnDown.Size = new System.Drawing.Size(17, 13);
        btnDown.TabIndex = 1;
        btnDown.TabStop = false;
        btnDown.Text = "▼";
        btnDown.UseVisualStyleBackColor = false;
        btnDown.Click += onDown;
    }

    private void InitializeComponent()
    {
        lblTitle = new System.Windows.Forms.Label();
        lblYMD = new System.Windows.Forms.Label();
        fraHANI = new System.Windows.Forms.GroupBox();
        optALL = new System.Windows.Forms.RadioButton();
        optHANI = new System.Windows.Forms.RadioButton();
        txtFROM = new System.Windows.Forms.TextBox();
        Label2 = new System.Windows.Forms.Label();
        txtTO = new System.Windows.Forms.TextBox();
        OptTANT = new System.Windows.Forms.RadioButton();
        TxtTANTCD = new System.Windows.Forms.TextBox();
        fraKEISIKI = new System.Windows.Forms.GroupBox();
        opt1 = new System.Windows.Forms.RadioButton();
        opt2 = new System.Windows.Forms.RadioButton();
        lblHANI = new System.Windows.Forms.Label();
        txtKNEN = new System.Windows.Forms.TextBox();
        spnKNEN = new System.Windows.Forms.Panel();
        btnSpnKNenUp = new System.Windows.Forms.Button();
        btnSpnKNenDown = new System.Windows.Forms.Button();
        lbl13 = new System.Windows.Forms.Label();
        txtKTUKI = new System.Windows.Forms.TextBox();
        spnKTUKI = new System.Windows.Forms.Panel();
        btnSpnKTukiUp = new System.Windows.Forms.Button();
        btnSpnKTukiDown = new System.Windows.Forms.Button();
        lbl14 = new System.Windows.Forms.Label();
        txtKHI = new System.Windows.Forms.TextBox();
        spnKHI = new System.Windows.Forms.Panel();
        btnSpnKHiUp = new System.Windows.Forms.Button();
        btnSpnKHiDown = new System.Windows.Forms.Button();
        lbl16 = new System.Windows.Forms.Label();
        Label4 = new System.Windows.Forms.Label();
        txtMNEN = new System.Windows.Forms.TextBox();
        spnMNEN = new System.Windows.Forms.Panel();
        btnSpnMNenUp = new System.Windows.Forms.Button();
        btnSpnMNenDown = new System.Windows.Forms.Button();
        lbl25 = new System.Windows.Forms.Label();
        txtMTUKI = new System.Windows.Forms.TextBox();
        spnMTUKI = new System.Windows.Forms.Panel();
        btnSpnMTukiUp = new System.Windows.Forms.Button();
        btnSpnMTukiDown = new System.Windows.Forms.Button();
        lbl26 = new System.Windows.Forms.Label();
        txtMHI = new System.Windows.Forms.TextBox();
        spnMHI = new System.Windows.Forms.Panel();
        btnSpnMHiUp = new System.Windows.Forms.Button();
        btnSpnMHiDown = new System.Windows.Forms.Button();
        lbl27 = new System.Windows.Forms.Label();
        cmdSTR = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();
        fraHANI.SuspendLayout();
        fraKEISIKI.SuspendLayout();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblTitle.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblTitle.ForeColor = System.Drawing.SystemColors.WindowText;
        lblTitle.Location = new System.Drawing.Point(160, 8);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new System.Drawing.Size(233, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "売上情報EXCEL出力";
        lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // lblYMD
        // 
        lblYMD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblYMD.Location = new System.Drawing.Point(445, 16);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(110, 19);
        lblYMD.TabIndex = 18;
        lblYMD.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // fraHANI
        // 
        fraHANI.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        fraHANI.Controls.Add(TxtTANTCD);
        fraHANI.Controls.Add(OptTANT);
        fraHANI.Controls.Add(txtTO);
        fraHANI.Controls.Add(txtFROM);
        fraHANI.Controls.Add(optHANI);
        fraHANI.Controls.Add(optALL);
        fraHANI.Controls.Add(Label2);
        fraHANI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        fraHANI.ForeColor = System.Drawing.SystemColors.WindowText;
        fraHANI.Location = new System.Drawing.Point(42, 48);
        fraHANI.Name = "fraHANI";
        fraHANI.Size = new System.Drawing.Size(254, 179);
        fraHANI.TabIndex = 15;
        fraHANI.TabStop = false;
        fraHANI.Text = "大分類範囲指定";
        // 
        // optALL
        // 
        optALL.AutoSize = true;
        optALL.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optALL.Checked = true;
        optALL.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        optALL.ForeColor = System.Drawing.SystemColors.WindowText;
        optALL.Location = new System.Drawing.Point(24, 30);
        optALL.Name = "optALL";
        optALL.Size = new System.Drawing.Size(69, 20);
        optALL.TabIndex = 1;
        optALL.TabStop = true;
        optALL.Text = "すべて";
        optALL.UseVisualStyleBackColor = false;
        optALL.Click += optALL_Click;
        optALL.KeyDown += optALL_KeyDown;
        // 
        // optHANI
        // 
        optHANI.AutoSize = true;
        optHANI.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optHANI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        optHANI.ForeColor = System.Drawing.SystemColors.WindowText;
        optHANI.Location = new System.Drawing.Point(24, 58);
        optHANI.Name = "optHANI";
        optHANI.Size = new System.Drawing.Size(156, 20);
        optHANI.TabIndex = 2;
        optHANI.Text = "大分類ｺｰﾄﾞ範囲指定";
        optHANI.UseVisualStyleBackColor = false;
        optHANI.Click += optHANI_Click;
        optHANI.KeyDown += OptHANI_KeyDown;
        // 
        // txtFROM
        // 
        txtFROM.BackColor = System.Drawing.Color.White;
        txtFROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtFROM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtFROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtFROM.Location = new System.Drawing.Point(40, 82);
        txtFROM.MaxLength = 2;
        txtFROM.Name = "txtFROM";
        txtFROM.Size = new System.Drawing.Size(33, 23);
        txtFROM.TabIndex = 3;
        txtFROM.KeyDown += txtFROM_KeyDown;
        txtFROM.KeyPress += DigitsOnly_KeyPress;
        txtFROM.Leave += txtFROM_LostFocus;
        // 
        // Label2
        // 
        Label2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label2.ForeColor = System.Drawing.SystemColors.WindowText;
        Label2.Location = new System.Drawing.Point(88, 86);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(33, 17);
        Label2.TabIndex = 29;
        Label2.Text = "～";
        Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // txtTO
        // 
        txtTO.BackColor = System.Drawing.Color.White;
        txtTO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtTO.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtTO.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtTO.Location = new System.Drawing.Point(136, 82);
        txtTO.MaxLength = 2;
        txtTO.Name = "txtTO";
        txtTO.Size = new System.Drawing.Size(33, 23);
        txtTO.TabIndex = 4;
        txtTO.KeyDown += txtTO_KeyDown;
        txtTO.KeyPress += DigitsOnly_KeyPress;
        txtTO.Leave += txtTO_LostFocus;
        // 
        // OptTANT
        // 
        OptTANT.AutoSize = true;
        OptTANT.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        OptTANT.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        OptTANT.ForeColor = System.Drawing.SystemColors.WindowText;
        OptTANT.Location = new System.Drawing.Point(24, 116);
        OptTANT.Name = "OptTANT";
        OptTANT.Size = new System.Drawing.Size(179, 20);
        OptTANT.TabIndex = 5;
        OptTANT.Text = "営業担当者コード指定";
        OptTANT.UseVisualStyleBackColor = false;
        OptTANT.Click += optTANT_Click;
        OptTANT.KeyDown += OptTANT_KeyDown;
        // 
        // TxtTANTCD
        // 
        TxtTANTCD.BackColor = System.Drawing.Color.White;
        TxtTANTCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        TxtTANTCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        TxtTANTCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        TxtTANTCD.Location = new System.Drawing.Point(40, 140);
        TxtTANTCD.MaxLength = 4;
        TxtTANTCD.Name = "TxtTANTCD";
        TxtTANTCD.Size = new System.Drawing.Size(45, 23);
        TxtTANTCD.TabIndex = 6;
        TxtTANTCD.KeyDown += TxtTANTCD_KeyDown;
        TxtTANTCD.KeyPress += DigitsOnly_KeyPress;
        TxtTANTCD.Leave += TxtTANTCD_LostFocus;
        // 
        // fraKEISIKI
        // 
        fraKEISIKI.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        fraKEISIKI.Controls.Add(opt1);
        fraKEISIKI.Controls.Add(opt2);
        fraKEISIKI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        fraKEISIKI.ForeColor = System.Drawing.SystemColors.WindowText;
        fraKEISIKI.Location = new System.Drawing.Point(335, 48);
        fraKEISIKI.Name = "fraKEISIKI";
        fraKEISIKI.Size = new System.Drawing.Size(219, 99);
        fraKEISIKI.TabIndex = 27;
        fraKEISIKI.TabStop = false;
        fraKEISIKI.Text = "出力形式指定";
        // 
        // opt1
        // 
        opt1.AutoSize = true;
        opt1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        opt1.Checked = true;
        opt1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        opt1.ForeColor = System.Drawing.SystemColors.WindowText;
        opt1.Location = new System.Drawing.Point(18, 26);
        opt1.Name = "opt1";
        opt1.Size = new System.Drawing.Size(89, 20);
        opt1.TabIndex = 13;
        opt1.TabStop = true;
        opt1.Text = "売上明細";
        opt1.UseVisualStyleBackColor = false;
        opt1.KeyDown += opt1_KeyDown;
        // 
        // opt2
        // 
        opt2.AutoSize = true;
        opt2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        opt2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        opt2.ForeColor = System.Drawing.SystemColors.WindowText;
        opt2.Location = new System.Drawing.Point(18, 58);
        opt2.Name = "opt2";
        opt2.Size = new System.Drawing.Size(169, 20);
        opt2.TabIndex = 14;
        opt2.Text = "大分類販売先別明細";
        opt2.UseVisualStyleBackColor = false;
        opt2.KeyDown += opt2_KeyDown;
        // 
        // lblHANI
        // 
        lblHANI.AutoSize = true;
        lblHANI.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblHANI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblHANI.ForeColor = System.Drawing.SystemColors.WindowText;
        lblHANI.Location = new System.Drawing.Point(51, 250);
        lblHANI.Name = "lblHANI";
        lblHANI.Size = new System.Drawing.Size(119, 16);
        lblHANI.TabIndex = 19;
        lblHANI.Text = "販売日範囲指定";
        // 
        // txtKNEN
        // 
        txtKNEN.BackColor = System.Drawing.Color.White;
        txtKNEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtKNEN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtKNEN.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtKNEN.Location = new System.Drawing.Point(59, 280);
        txtKNEN.MaxLength = 4;
        txtKNEN.Name = "txtKNEN";
        txtKNEN.Size = new System.Drawing.Size(49, 23);
        txtKNEN.TabIndex = 7;
        txtKNEN.KeyDown += txtKNEN_KeyDown;
        txtKNEN.KeyPress += DigitsOnly_KeyPress;
        txtKNEN.Leave += txtKNEN_LostFocus;
        // 
        // spnKNEN
        // 
        ConfigureSpinPanel(spnKNEN, btnSpnKNenUp, btnSpnKNenDown, "spnKNEN", 107, 280, 35, spnKNEN_SpinUp, spnKNEN_SpinDown);
        // 
        // lbl13
        // 
        lbl13.AutoSize = true;
        lbl13.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lbl13.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lbl13.ForeColor = System.Drawing.SystemColors.WindowText;
        lbl13.Location = new System.Drawing.Point(131, 286);
        lbl13.Name = "lbl13";
        lbl13.Size = new System.Drawing.Size(23, 16);
        lbl13.TabIndex = 20;
        lbl13.Text = "年";
        // 
        // txtKTUKI
        // 
        txtKTUKI.BackColor = System.Drawing.Color.White;
        txtKTUKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtKTUKI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtKTUKI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtKTUKI.Location = new System.Drawing.Point(155, 280);
        txtKTUKI.MaxLength = 2;
        txtKTUKI.Name = "txtKTUKI";
        txtKTUKI.Size = new System.Drawing.Size(33, 23);
        txtKTUKI.TabIndex = 8;
        txtKTUKI.KeyDown += txtKTUKI_KeyDown;
        txtKTUKI.KeyPress += DigitsOnly_KeyPress;
        txtKTUKI.Leave += txtKTUKI_LostFocus;
        // 
        // spnKTUKI
        // 
        ConfigureSpinPanel(spnKTUKI, btnSpnKTukiUp, btnSpnKTukiDown, "spnKTUKI", 187, 280, 34, spnKTUKI_SpinUp, spnKTUKI_SpinDown);
        // 
        // lbl14
        // 
        lbl14.AutoSize = true;
        lbl14.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lbl14.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lbl14.ForeColor = System.Drawing.SystemColors.WindowText;
        lbl14.Location = new System.Drawing.Point(211, 286);
        lbl14.Name = "lbl14";
        lbl14.Size = new System.Drawing.Size(23, 16);
        lbl14.TabIndex = 21;
        lbl14.Text = "月";
        // 
        // txtKHI
        // 
        txtKHI.BackColor = System.Drawing.Color.White;
        txtKHI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtKHI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtKHI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtKHI.Location = new System.Drawing.Point(235, 280);
        txtKHI.MaxLength = 2;
        txtKHI.Name = "txtKHI";
        txtKHI.Size = new System.Drawing.Size(33, 23);
        txtKHI.TabIndex = 9;
        txtKHI.KeyDown += txtKHI_KeyDown;
        txtKHI.KeyPress += DigitsOnly_KeyPress;
        txtKHI.Leave += txtKHI_LostFocus;
        // 
        // spnKHI
        // 
        ConfigureSpinPanel(spnKHI, btnSpnKHiUp, btnSpnKHiDown, "spnKHI", 267, 280, 33, spnKHI_SpinUp, spnKHI_SpinDown);
        // 
        // lbl16
        // 
        lbl16.AutoSize = true;
        lbl16.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lbl16.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lbl16.ForeColor = System.Drawing.SystemColors.WindowText;
        lbl16.Location = new System.Drawing.Point(291, 286);
        lbl16.Name = "lbl16";
        lbl16.Size = new System.Drawing.Size(23, 16);
        lbl16.TabIndex = 22;
        lbl16.Text = "日";
        // 
        // Label4
        // 
        Label4.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label4.ForeColor = System.Drawing.SystemColors.WindowText;
        Label4.Location = new System.Drawing.Point(51, 326);
        Label4.Name = "Label4";
        Label4.Size = new System.Drawing.Size(33, 17);
        Label4.TabIndex = 26;
        Label4.Text = "～";
        Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // txtMNEN
        // 
        txtMNEN.BackColor = System.Drawing.Color.White;
        txtMNEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtMNEN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtMNEN.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtMNEN.Location = new System.Drawing.Point(91, 320);
        txtMNEN.MaxLength = 4;
        txtMNEN.Name = "txtMNEN";
        txtMNEN.Size = new System.Drawing.Size(49, 23);
        txtMNEN.TabIndex = 10;
        txtMNEN.KeyDown += txtMNEN_KeyDown;
        txtMNEN.KeyPress += DigitsOnly_KeyPress;
        txtMNEN.Leave += txtMNEN_LostFocus;
        // 
        // spnMNEN
        // 
        ConfigureSpinPanel(spnMNEN, btnSpnMNenUp, btnSpnMNenDown, "spnMNEN", 139, 320, 32, spnMNEN_SpinUp, spnMNEN_SpinDown);
        // 
        // lbl25
        // 
        lbl25.AutoSize = true;
        lbl25.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lbl25.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lbl25.ForeColor = System.Drawing.SystemColors.WindowText;
        lbl25.Location = new System.Drawing.Point(163, 326);
        lbl25.Name = "lbl25";
        lbl25.Size = new System.Drawing.Size(23, 16);
        lbl25.TabIndex = 23;
        lbl25.Text = "年";
        // 
        // txtMTUKI
        // 
        txtMTUKI.BackColor = System.Drawing.Color.White;
        txtMTUKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtMTUKI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtMTUKI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtMTUKI.Location = new System.Drawing.Point(187, 320);
        txtMTUKI.MaxLength = 2;
        txtMTUKI.Name = "txtMTUKI";
        txtMTUKI.Size = new System.Drawing.Size(33, 23);
        txtMTUKI.TabIndex = 11;
        txtMTUKI.KeyDown += txtMTUKI_KeyDown;
        txtMTUKI.KeyPress += DigitsOnly_KeyPress;
        txtMTUKI.Leave += txtMTUKI_LostFocus;
        // 
        // spnMTUKI
        // 
        ConfigureSpinPanel(spnMTUKI, btnSpnMTukiUp, btnSpnMTukiDown, "spnMTUKI", 219, 320, 31, spnMTUKI_SpinUp, spnMTUKI_SpinDown);
        // 
        // lbl26
        // 
        lbl26.AutoSize = true;
        lbl26.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lbl26.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lbl26.ForeColor = System.Drawing.SystemColors.WindowText;
        lbl26.Location = new System.Drawing.Point(243, 326);
        lbl26.Name = "lbl26";
        lbl26.Size = new System.Drawing.Size(23, 16);
        lbl26.TabIndex = 24;
        lbl26.Text = "月";
        // 
        // txtMHI
        // 
        txtMHI.BackColor = System.Drawing.Color.White;
        txtMHI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtMHI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtMHI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtMHI.Location = new System.Drawing.Point(267, 320);
        txtMHI.MaxLength = 2;
        txtMHI.Name = "txtMHI";
        txtMHI.Size = new System.Drawing.Size(33, 23);
        txtMHI.TabIndex = 12;
        txtMHI.KeyDown += txtMHI_KeyDown;
        txtMHI.KeyPress += DigitsOnly_KeyPress;
        txtMHI.Leave += txtMHI_LostFocus;
        // 
        // spnMHI
        // 
        ConfigureSpinPanel(spnMHI, btnSpnMHiUp, btnSpnMHiDown, "spnMHI", 299, 320, 30, spnMHI_SpinUp, spnMHI_SpinDown);
        // 
        // lbl27
        // 
        lbl27.AutoSize = true;
        lbl27.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lbl27.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lbl27.ForeColor = System.Drawing.SystemColors.WindowText;
        lbl27.Location = new System.Drawing.Point(323, 326);
        lbl27.Name = "lbl27";
        lbl27.Size = new System.Drawing.Size(23, 16);
        lbl27.TabIndex = 25;
        lbl27.Text = "日";
        // 
        // cmdSTR
        // 
        cmdSTR.BackColor = System.Drawing.SystemColors.Control;
        cmdSTR.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdSTR.Location = new System.Drawing.Point(433, 206);
        cmdSTR.Name = "cmdSTR";
        cmdSTR.Size = new System.Drawing.Size(121, 65);
        cmdSTR.TabIndex = 16;
        cmdSTR.Text = "出力開始";
        cmdSTR.UseVisualStyleBackColor = false;
        cmdSTR.Click += cmdSTR_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.CausesValidation = false;
        cmdCAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdCAN.Location = new System.Drawing.Point(433, 284);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(121, 65);
        cmdCAN.TabIndex = 17;
        cmdCAN.Text = "キャンセル";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // FrmKSHO0115
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(596, 376);
        ControlBox = false;
        Controls.Add(fraKEISIKI);
        Controls.Add(spnMHI);
        Controls.Add(txtMHI);
        Controls.Add(spnMTUKI);
        Controls.Add(txtMTUKI);
        Controls.Add(spnMNEN);
        Controls.Add(txtMNEN);
        Controls.Add(spnKHI);
        Controls.Add(txtKHI);
        Controls.Add(spnKTUKI);
        Controls.Add(txtKTUKI);
        Controls.Add(spnKNEN);
        Controls.Add(txtKNEN);
        Controls.Add(cmdCAN);
        Controls.Add(cmdSTR);
        Controls.Add(fraHANI);
        Controls.Add(Label4);
        Controls.Add(lbl27);
        Controls.Add(lbl26);
        Controls.Add(lbl25);
        Controls.Add(lbl16);
        Controls.Add(lbl14);
        Controls.Add(lbl13);
        Controls.Add(lblHANI);
        Controls.Add(lblYMD);
        Controls.Add(lblTitle);
        Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmKSHO0115";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KSHO0115";
        Load += FrmKSHO0115_Load;
        fraHANI.ResumeLayout(false);
        fraHANI.PerformLayout();
        fraKEISIKI.ResumeLayout(false);
        fraKEISIKI.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

