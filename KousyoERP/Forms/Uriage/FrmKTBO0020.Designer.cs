#nullable enable
namespace KousyoERP.Forms.Uriage;

partial class FrmKTBO0020
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.TextBox txtHANCD = null!;
    private System.Windows.Forms.Button cmdSEEKHAN = null!;
    private System.Windows.Forms.Label lblHANCD = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.TextBox txtTANCD = null!;
    private System.Windows.Forms.Button cmdSEEKTAN = null!;
    private System.Windows.Forms.Label lblTANCD = null!;
    private System.Windows.Forms.Label Label5 = null!;
    private System.Windows.Forms.TextBox txtDAICD = null!;
    private System.Windows.Forms.Label lblDAICD = null!;
    private System.Windows.Forms.Label Label6 = null!;
    private System.Windows.Forms.TextBox txtTYUCD = null!;
    private System.Windows.Forms.Label lblTYUCD = null!;
    private System.Windows.Forms.Label Label10 = null!;
    private System.Windows.Forms.TextBox txtSYOCD = null!;
    private System.Windows.Forms.Button cmdSEEKSYO = null!;
    private System.Windows.Forms.Label lblSYOCD = null!;
    private System.Windows.Forms.Label Label7 = null!;
    private System.Windows.Forms.TextBox txtKNEN = null!;
    private System.Windows.Forms.Panel spnKYY = null!;
    private System.Windows.Forms.Button btnSpnKyyUp = null!;
    private System.Windows.Forms.Button btnSpnKyyDown = null!;
    private System.Windows.Forms.Label Label8 = null!;
    private System.Windows.Forms.TextBox txtKTUKI = null!;
    private System.Windows.Forms.Panel spnKMM = null!;
    private System.Windows.Forms.Button btnSpnKmmUp = null!;
    private System.Windows.Forms.Button btnSpnKmmDown = null!;
    private System.Windows.Forms.Label Label9 = null!;
    private System.Windows.Forms.TextBox txtKHI = null!;
    private System.Windows.Forms.Panel spnKDD = null!;
    private System.Windows.Forms.Button btnSpnKddUp = null!;
    private System.Windows.Forms.Button btnSpnKddDown = null!;
    private System.Windows.Forms.Label Label11 = null!;
    private System.Windows.Forms.Label Label14 = null!;
    private System.Windows.Forms.TextBox txtSNEN = null!;
    private System.Windows.Forms.Panel spnSYY = null!;
    private System.Windows.Forms.Button btnSpnSyyUp = null!;
    private System.Windows.Forms.Button btnSpnSyyDown = null!;
    private System.Windows.Forms.Label Label17 = null!;
    private System.Windows.Forms.TextBox txtSTUKI = null!;
    private System.Windows.Forms.Panel spnSMM = null!;
    private System.Windows.Forms.Button btnSpnSmmUp = null!;
    private System.Windows.Forms.Button btnSpnSmmDown = null!;
    private System.Windows.Forms.Label Label16 = null!;
    private System.Windows.Forms.TextBox txtSHI = null!;
    private System.Windows.Forms.Panel spnSDD = null!;
    private System.Windows.Forms.Button btnSpnSddUp = null!;
    private System.Windows.Forms.Button btnSpnSddDown = null!;
    private System.Windows.Forms.Label Label15 = null!;
    private System.Windows.Forms.Button cmdSTART = null!;
    private System.Windows.Forms.Button cmdCAN = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private static void ConfigureSpinPanel(
        System.Windows.Forms.Panel panel,
        System.Windows.Forms.Button btnUp,
        System.Windows.Forms.Button btnDown,
        string panelName,
        System.Drawing.Point location)
    {
        panel.Location = location;
        panel.Name = panelName;
        panel.Size = new System.Drawing.Size(17, 25);
        panel.TabStop = false;

        var arrowFont = new System.Drawing.Font("ＭＳ Ｐゴシック", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);

        btnUp.BackColor = System.Drawing.SystemColors.Control;
        btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
        btnUp.Font = arrowFont;
        btnUp.Location = new System.Drawing.Point(0, 0);
        btnUp.Name = panelName + "_Up";
        btnUp.Size = new System.Drawing.Size(17, 13);
        btnUp.TabStop = false;
        btnUp.Text = "▲";
        btnUp.UseVisualStyleBackColor = false;

        btnDown.BackColor = System.Drawing.SystemColors.Control;
        btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
        btnDown.Font = arrowFont;
        btnDown.Location = new System.Drawing.Point(0, 12);
        btnDown.Name = panelName + "_Down";
        btnDown.Size = new System.Drawing.Size(17, 13);
        btnDown.TabStop = false;
        btnDown.Text = "▼";
        btnDown.UseVisualStyleBackColor = false;

        panel.Controls.Add(btnUp);
        panel.Controls.Add(btnDown);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        var bg = System.Drawing.Color.FromArgb(192, 192, 192);
        var font12 = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        var font14 = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        var font18 = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);

        Label1 = new System.Windows.Forms.Label();
        lblYMD = new System.Windows.Forms.Label();
        Label2 = new System.Windows.Forms.Label();
        txtHANCD = new System.Windows.Forms.TextBox();
        cmdSEEKHAN = new System.Windows.Forms.Button();
        lblHANCD = new System.Windows.Forms.Label();
        Label3 = new System.Windows.Forms.Label();
        txtTANCD = new System.Windows.Forms.TextBox();
        cmdSEEKTAN = new System.Windows.Forms.Button();
        lblTANCD = new System.Windows.Forms.Label();
        Label5 = new System.Windows.Forms.Label();
        txtDAICD = new System.Windows.Forms.TextBox();
        lblDAICD = new System.Windows.Forms.Label();
        Label6 = new System.Windows.Forms.Label();
        txtTYUCD = new System.Windows.Forms.TextBox();
        lblTYUCD = new System.Windows.Forms.Label();
        Label10 = new System.Windows.Forms.Label();
        txtSYOCD = new System.Windows.Forms.TextBox();
        cmdSEEKSYO = new System.Windows.Forms.Button();
        lblSYOCD = new System.Windows.Forms.Label();
        Label7 = new System.Windows.Forms.Label();
        txtKNEN = new System.Windows.Forms.TextBox();
        spnKYY = new System.Windows.Forms.Panel();
        btnSpnKyyUp = new System.Windows.Forms.Button();
        btnSpnKyyDown = new System.Windows.Forms.Button();
        Label8 = new System.Windows.Forms.Label();
        txtKTUKI = new System.Windows.Forms.TextBox();
        spnKMM = new System.Windows.Forms.Panel();
        btnSpnKmmUp = new System.Windows.Forms.Button();
        btnSpnKmmDown = new System.Windows.Forms.Button();
        Label9 = new System.Windows.Forms.Label();
        txtKHI = new System.Windows.Forms.TextBox();
        spnKDD = new System.Windows.Forms.Panel();
        btnSpnKddUp = new System.Windows.Forms.Button();
        btnSpnKddDown = new System.Windows.Forms.Button();
        Label11 = new System.Windows.Forms.Label();
        Label14 = new System.Windows.Forms.Label();
        txtSNEN = new System.Windows.Forms.TextBox();
        spnSYY = new System.Windows.Forms.Panel();
        btnSpnSyyUp = new System.Windows.Forms.Button();
        btnSpnSyyDown = new System.Windows.Forms.Button();
        Label17 = new System.Windows.Forms.Label();
        txtSTUKI = new System.Windows.Forms.TextBox();
        spnSMM = new System.Windows.Forms.Panel();
        btnSpnSmmUp = new System.Windows.Forms.Button();
        btnSpnSmmDown = new System.Windows.Forms.Button();
        Label16 = new System.Windows.Forms.Label();
        txtSHI = new System.Windows.Forms.TextBox();
        spnSDD = new System.Windows.Forms.Panel();
        btnSpnSddUp = new System.Windows.Forms.Button();
        btnSpnSddDown = new System.Windows.Forms.Button();
        Label15 = new System.Windows.Forms.Label();
        cmdSTART = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();

        SuspendLayout();

        // Label1
        Label1.BackColor = bg;
        Label1.Font = font18;
        Label1.Location = new System.Drawing.Point(200, 8);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(256, 28);
        Label1.TabIndex = 0;
        Label1.Text = "売上履歴照会条件指定";
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // lblYMD
        lblYMD.BackColor = bg;
        lblYMD.Font = font14;
        lblYMD.Location = new System.Drawing.Point(520, 8);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(121, 22);
        lblYMD.TabIndex = 17;
        lblYMD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // Label2
        Label2.BackColor = bg;
        Label2.Font = font12;
        Label2.Location = new System.Drawing.Point(32, 80);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(104, 20);
        Label2.TabIndex = 22;
        Label2.Text = "販売先コード";

        // txtHANCD
        txtHANCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtHANCD.Font = font12;
        txtHANCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtHANCD.Location = new System.Drawing.Point(144, 72);
        txtHANCD.MaxLength = 10;
        txtHANCD.Name = "txtHANCD";
        txtHANCD.Size = new System.Drawing.Size(89, 25);
        txtHANCD.TabIndex = 1;
        txtHANCD.KeyDown += txtHANCD_KeyDown;
        txtHANCD.KeyPress += NumericOnly_KeyPress;
        txtHANCD.Leave += txtHANCD_Leave;

        // cmdSEEKHAN
        cmdSEEKHAN.BackColor = System.Drawing.SystemColors.Control;
        cmdSEEKHAN.Font = font12;
        cmdSEEKHAN.Location = new System.Drawing.Point(240, 72);
        cmdSEEKHAN.Name = "cmdSEEKHAN";
        cmdSEEKHAN.Size = new System.Drawing.Size(22, 25);
        cmdSEEKHAN.TabIndex = 2;
        cmdSEEKHAN.Text = "";
        cmdSEEKHAN.UseVisualStyleBackColor = false;
        cmdSEEKHAN.Click += cmdSEEKHAN_Click;

        // lblHANCD
        lblHANCD.BackColor = bg;
        lblHANCD.Font = font12;
        lblHANCD.Location = new System.Drawing.Point(268, 76);
        lblHANCD.Name = "lblHANCD";
        lblHANCD.Size = new System.Drawing.Size(376, 25);
        lblHANCD.TabIndex = 23;

        // Label3
        Label3.BackColor = bg;
        Label3.Font = font12;
        Label3.Location = new System.Drawing.Point(32, 128);
        Label3.Name = "Label3";
        Label3.Size = new System.Drawing.Size(113, 20);
        Label3.TabIndex = 24;
        Label3.Text = "営業担当コード";

        // txtTANCD
        txtTANCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtTANCD.Font = font12;
        txtTANCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtTANCD.Location = new System.Drawing.Point(144, 120);
        txtTANCD.MaxLength = 4;
        txtTANCD.Name = "txtTANCD";
        txtTANCD.Size = new System.Drawing.Size(45, 25);
        txtTANCD.TabIndex = 3;
        txtTANCD.KeyDown += txtTANCD_KeyDown;
        txtTANCD.KeyPress += NumericOnly_KeyPress;
        txtTANCD.Leave += txtTANCD_Leave;

        // cmdSEEKTAN
        cmdSEEKTAN.BackColor = System.Drawing.SystemColors.Control;
        cmdSEEKTAN.Font = font12;
        cmdSEEKTAN.Location = new System.Drawing.Point(194, 120);
        cmdSEEKTAN.Name = "cmdSEEKTAN";
        cmdSEEKTAN.Size = new System.Drawing.Size(22, 25);
        cmdSEEKTAN.TabIndex = 4;
        cmdSEEKTAN.Text = "";
        cmdSEEKTAN.UseVisualStyleBackColor = false;
        cmdSEEKTAN.Click += cmdSEEKTAN_Click;

        // lblTANCD
        lblTANCD.BackColor = bg;
        lblTANCD.Font = font12;
        lblTANCD.Location = new System.Drawing.Point(248, 124);
        lblTANCD.Name = "lblTANCD";
        lblTANCD.Size = new System.Drawing.Size(369, 25);
        lblTANCD.TabIndex = 26;

        // Label5
        Label5.BackColor = bg;
        Label5.Font = font12;
        Label5.Location = new System.Drawing.Point(32, 176);
        Label5.Name = "Label5";
        Label5.Size = new System.Drawing.Size(104, 20);
        Label5.TabIndex = 25;
        Label5.Text = "大分類コード";

        // txtDAICD
        txtDAICD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtDAICD.Font = font12;
        txtDAICD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtDAICD.Location = new System.Drawing.Point(144, 168);
        txtDAICD.MaxLength = 2;
        txtDAICD.Name = "txtDAICD";
        txtDAICD.Size = new System.Drawing.Size(29, 25);
        txtDAICD.TabIndex = 5;
        txtDAICD.TextChanged += txtDAICD_TextChanged;
        txtDAICD.KeyDown += txtDAICD_KeyDown;
        txtDAICD.KeyPress += NumericOnly_KeyPress;
        txtDAICD.Leave += txtDAICD_Leave;

        // lblDAICD
        lblDAICD.BackColor = bg;
        lblDAICD.Font = font12;
        lblDAICD.Location = new System.Drawing.Point(248, 172);
        lblDAICD.Name = "lblDAICD";
        lblDAICD.Size = new System.Drawing.Size(241, 25);
        lblDAICD.TabIndex = 27;

        // Label6
        Label6.BackColor = bg;
        Label6.Font = font12;
        Label6.Location = new System.Drawing.Point(32, 224);
        Label6.Name = "Label6";
        Label6.Size = new System.Drawing.Size(104, 20);
        Label6.TabIndex = 28;
        Label6.Text = "中分類コード";

        // txtTYUCD
        txtTYUCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtTYUCD.Font = font12;
        txtTYUCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtTYUCD.Location = new System.Drawing.Point(144, 216);
        txtTYUCD.MaxLength = 2;
        txtTYUCD.Name = "txtTYUCD";
        txtTYUCD.Size = new System.Drawing.Size(29, 25);
        txtTYUCD.TabIndex = 6;
        txtTYUCD.KeyDown += txtTYUCD_KeyDown;
        txtTYUCD.KeyPress += NumericOnly_KeyPress;
        txtTYUCD.Leave += txtTYUCD_Leave;

        // lblTYUCD
        lblTYUCD.BackColor = bg;
        lblTYUCD.Font = font12;
        lblTYUCD.Location = new System.Drawing.Point(248, 220);
        lblTYUCD.Name = "lblTYUCD";
        lblTYUCD.Size = new System.Drawing.Size(241, 25);
        lblTYUCD.TabIndex = 29;

        // Label10
        Label10.BackColor = bg;
        Label10.Font = font12;
        Label10.Location = new System.Drawing.Point(32, 272);
        Label10.Name = "Label10";
        Label10.Size = new System.Drawing.Size(89, 20);
        Label10.TabIndex = 30;
        Label10.Text = "商品コード";

        // txtSYOCD
        txtSYOCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSYOCD.Font = font12;
        txtSYOCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtSYOCD.Location = new System.Drawing.Point(144, 264);
        txtSYOCD.MaxLength = 8;
        txtSYOCD.Name = "txtSYOCD";
        txtSYOCD.Size = new System.Drawing.Size(75, 25);
        txtSYOCD.TabIndex = 7;
        txtSYOCD.KeyDown += txtSYOCD_KeyDown;
        txtSYOCD.KeyPress += NumericOnly_KeyPress;
        txtSYOCD.Leave += txtSYOCD_Leave;

        // cmdSEEKSYO
        cmdSEEKSYO.BackColor = System.Drawing.SystemColors.Control;
        cmdSEEKSYO.Font = font12;
        cmdSEEKSYO.Location = new System.Drawing.Point(224, 264);
        cmdSEEKSYO.Name = "cmdSEEKSYO";
        cmdSEEKSYO.Size = new System.Drawing.Size(22, 25);
        cmdSEEKSYO.TabIndex = 8;
        cmdSEEKSYO.Text = "";
        cmdSEEKSYO.UseVisualStyleBackColor = false;
        cmdSEEKSYO.Click += cmdSEEKSYO_Click;

        // lblSYOCD
        lblSYOCD.BackColor = bg;
        lblSYOCD.Font = font12;
        lblSYOCD.Location = new System.Drawing.Point(252, 268);
        lblSYOCD.Name = "lblSYOCD";
        lblSYOCD.Size = new System.Drawing.Size(392, 25);
        lblSYOCD.TabIndex = 31;

        // Label7
        Label7.BackColor = bg;
        Label7.Font = font12;
        Label7.Location = new System.Drawing.Point(32, 320);
        Label7.Name = "Label7";
        Label7.Size = new System.Drawing.Size(104, 20);
        Label7.TabIndex = 18;
        Label7.Text = "販売日付範囲";

        // txtKNEN + spnKYY + Label8
        txtKNEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtKNEN.Font = font12;
        txtKNEN.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtKNEN.Location = new System.Drawing.Point(144, 312);
        txtKNEN.MaxLength = 4;
        txtKNEN.Name = "txtKNEN";
        txtKNEN.Size = new System.Drawing.Size(43, 25);
        txtKNEN.TabIndex = 9;
        txtKNEN.KeyDown += txtKNEN_KeyDown;
        txtKNEN.KeyPress += NumericOnly_KeyPress;
        txtKNEN.Leave += txtKNEN_Leave;

        ConfigureSpinPanel(spnKYY, btnSpnKyyUp, btnSpnKyyDown, "spnKYY", new System.Drawing.Point(187, 312));
        btnSpnKyyUp.Click += spnKYY_SpinUp;
        btnSpnKyyDown.Click += spnKYY_SpinDown;

        Label8.BackColor = bg;
        Label8.Font = font12;
        Label8.Location = new System.Drawing.Point(208, 317);
        Label8.Name = "Label8";
        Label8.Size = new System.Drawing.Size(22, 20);
        Label8.TabIndex = 19;
        Label8.Text = "年";

        // txtKTUKI + spnKMM + Label9
        txtKTUKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtKTUKI.Font = font12;
        txtKTUKI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtKTUKI.Location = new System.Drawing.Point(232, 312);
        txtKTUKI.MaxLength = 2;
        txtKTUKI.Name = "txtKTUKI";
        txtKTUKI.Size = new System.Drawing.Size(28, 25);
        txtKTUKI.TabIndex = 10;
        txtKTUKI.KeyDown += txtKTUKI_KeyDown;
        txtKTUKI.KeyPress += NumericOnly_KeyPress;
        txtKTUKI.Leave += txtKTUKI_Leave;

        ConfigureSpinPanel(spnKMM, btnSpnKmmUp, btnSpnKmmDown, "spnKMM", new System.Drawing.Point(260, 312));
        btnSpnKmmUp.Click += spnKMM_SpinUp;
        btnSpnKmmDown.Click += spnKMM_SpinDown;

        Label9.BackColor = bg;
        Label9.Font = font12;
        Label9.Location = new System.Drawing.Point(280, 317);
        Label9.Name = "Label9";
        Label9.Size = new System.Drawing.Size(22, 20);
        Label9.TabIndex = 20;
        Label9.Text = "月";

        // txtKHI + spnKDD + Label11
        txtKHI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtKHI.Font = font12;
        txtKHI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtKHI.Location = new System.Drawing.Point(304, 312);
        txtKHI.MaxLength = 2;
        txtKHI.Name = "txtKHI";
        txtKHI.Size = new System.Drawing.Size(28, 25);
        txtKHI.TabIndex = 11;
        txtKHI.KeyDown += txtKHI_KeyDown;
        txtKHI.KeyPress += NumericOnly_KeyPress;
        txtKHI.Leave += txtKHI_Leave;

        ConfigureSpinPanel(spnKDD, btnSpnKddUp, btnSpnKddDown, "spnKDD", new System.Drawing.Point(332, 312));
        btnSpnKddUp.Click += spnKDD_SpinUp;
        btnSpnKddDown.Click += spnKDD_SpinDown;

        Label11.BackColor = bg;
        Label11.Font = font12;
        Label11.Location = new System.Drawing.Point(352, 317);
        Label11.Name = "Label11";
        Label11.Size = new System.Drawing.Size(22, 20);
        Label11.TabIndex = 21;
        Label11.Text = "日";

        // Label14 ("～")
        Label14.BackColor = bg;
        Label14.Font = font12;
        Label14.Location = new System.Drawing.Point(144, 357);
        Label14.Name = "Label14";
        Label14.Size = new System.Drawing.Size(22, 20);
        Label14.TabIndex = 32;
        Label14.Text = "～";

        // txtSNEN + spnSYY + Label17
        txtSNEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSNEN.Font = font12;
        txtSNEN.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtSNEN.Location = new System.Drawing.Point(184, 352);
        txtSNEN.MaxLength = 4;
        txtSNEN.Name = "txtSNEN";
        txtSNEN.Size = new System.Drawing.Size(43, 25);
        txtSNEN.TabIndex = 12;
        txtSNEN.KeyDown += txtSNEN_KeyDown;
        txtSNEN.KeyPress += NumericOnly_KeyPress;
        txtSNEN.Leave += txtSNEN_Leave;

        ConfigureSpinPanel(spnSYY, btnSpnSyyUp, btnSpnSyyDown, "spnSYY", new System.Drawing.Point(227, 352));
        btnSpnSyyUp.Click += spnSYY_SpinUp;
        btnSpnSyyDown.Click += spnSYY_SpinDown;

        Label17.BackColor = bg;
        Label17.Font = font12;
        Label17.Location = new System.Drawing.Point(248, 357);
        Label17.Name = "Label17";
        Label17.Size = new System.Drawing.Size(22, 20);
        Label17.TabIndex = 35;
        Label17.Text = "年";

        // txtSTUKI + spnSMM + Label16
        txtSTUKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSTUKI.Font = font12;
        txtSTUKI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtSTUKI.Location = new System.Drawing.Point(272, 352);
        txtSTUKI.MaxLength = 2;
        txtSTUKI.Name = "txtSTUKI";
        txtSTUKI.Size = new System.Drawing.Size(28, 25);
        txtSTUKI.TabIndex = 13;
        txtSTUKI.KeyDown += txtSTUKI_KeyDown;
        txtSTUKI.KeyPress += NumericOnly_KeyPress;
        txtSTUKI.Leave += txtSTUKI_Leave;

        ConfigureSpinPanel(spnSMM, btnSpnSmmUp, btnSpnSmmDown, "spnSMM", new System.Drawing.Point(300, 352));
        btnSpnSmmUp.Click += spnSMM_SpinUp;
        btnSpnSmmDown.Click += spnSMM_SpinDown;

        Label16.BackColor = bg;
        Label16.Font = font12;
        Label16.Location = new System.Drawing.Point(320, 357);
        Label16.Name = "Label16";
        Label16.Size = new System.Drawing.Size(22, 20);
        Label16.TabIndex = 34;
        Label16.Text = "月";

        // txtSHI + spnSDD + Label15
        txtSHI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSHI.Font = font12;
        txtSHI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtSHI.Location = new System.Drawing.Point(344, 352);
        txtSHI.MaxLength = 2;
        txtSHI.Name = "txtSHI";
        txtSHI.Size = new System.Drawing.Size(28, 25);
        txtSHI.TabIndex = 14;
        txtSHI.KeyDown += txtSHI_KeyDown;
        txtSHI.KeyPress += NumericOnly_KeyPress;
        txtSHI.Leave += txtSHI_Leave;

        ConfigureSpinPanel(spnSDD, btnSpnSddUp, btnSpnSddDown, "spnSDD", new System.Drawing.Point(372, 352));
        btnSpnSddUp.Click += spnSDD_SpinUp;
        btnSpnSddDown.Click += spnSDD_SpinDown;

        Label15.BackColor = bg;
        Label15.Font = font12;
        Label15.Location = new System.Drawing.Point(392, 357);
        Label15.Name = "Label15";
        Label15.Size = new System.Drawing.Size(22, 20);
        Label15.TabIndex = 33;
        Label15.Text = "日";

        // cmdSTART
        cmdSTART.BackColor = System.Drawing.SystemColors.Control;
        cmdSTART.Font = font14;
        cmdSTART.Location = new System.Drawing.Point(296, 400);
        cmdSTART.Name = "cmdSTART";
        cmdSTART.Size = new System.Drawing.Size(96, 33);
        cmdSTART.TabIndex = 15;
        cmdSTART.Text = "検索開始";
        cmdSTART.UseVisualStyleBackColor = false;
        cmdSTART.Click += cmdSTART_Click;

        // cmdCAN
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.Font = font14;
        cmdCAN.Location = new System.Drawing.Point(416, 400);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(96, 33);
        cmdCAN.TabIndex = 16;
        cmdCAN.Text = "ｷｬﾝｾﾙ";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;

        // FrmKTBO0020
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = bg;
        ClientSize = new System.Drawing.Size(652, 449);
        ControlBox = false;
        Controls.Add(cmdCAN);
        Controls.Add(cmdSTART);
        Controls.Add(Label15);
        Controls.Add(spnSDD);
        Controls.Add(txtSHI);
        Controls.Add(Label16);
        Controls.Add(spnSMM);
        Controls.Add(txtSTUKI);
        Controls.Add(Label17);
        Controls.Add(spnSYY);
        Controls.Add(txtSNEN);
        Controls.Add(Label14);
        Controls.Add(Label11);
        Controls.Add(spnKDD);
        Controls.Add(txtKHI);
        Controls.Add(Label9);
        Controls.Add(spnKMM);
        Controls.Add(txtKTUKI);
        Controls.Add(Label8);
        Controls.Add(spnKYY);
        Controls.Add(txtKNEN);
        Controls.Add(Label7);
        Controls.Add(lblSYOCD);
        Controls.Add(cmdSEEKSYO);
        Controls.Add(txtSYOCD);
        Controls.Add(Label10);
        Controls.Add(lblTYUCD);
        Controls.Add(txtTYUCD);
        Controls.Add(Label6);
        Controls.Add(lblDAICD);
        Controls.Add(txtDAICD);
        Controls.Add(Label5);
        Controls.Add(lblTANCD);
        Controls.Add(cmdSEEKTAN);
        Controls.Add(txtTANCD);
        Controls.Add(Label3);
        Controls.Add(lblHANCD);
        Controls.Add(cmdSEEKHAN);
        Controls.Add(txtHANCD);
        Controls.Add(Label2);
        Controls.Add(lblYMD);
        Controls.Add(Label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmKTBO0020";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KTBO0020";
        Load += FrmKTBO0020_Load;

        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

