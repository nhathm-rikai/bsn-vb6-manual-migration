#nullable enable
namespace KousyoERP.Forms.Zaiko;

partial class FrmKZKO0010
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.GroupBox frmSHN = null!;
    private System.Windows.Forms.RadioButton optBMNCD = null!;
    private System.Windows.Forms.TextBox txtBMNCD = null!;
    private System.Windows.Forms.Label lblBMNNAME = null!;
    private System.Windows.Forms.RadioButton optSHNCD = null!;
    private System.Windows.Forms.TextBox txtSHNCD = null!;
    private System.Windows.Forms.Label lblSHNNAME = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.TextBox txtZNEN = null!;
    private System.Windows.Forms.Panel spnZNEN = null!;
    private System.Windows.Forms.Button btnSpnZNenUp = null!;
    private System.Windows.Forms.Button btnSpnZNenDown = null!;
    private System.Windows.Forms.Label Label13 = null!;
    private System.Windows.Forms.TextBox txtZTUKI = null!;
    private System.Windows.Forms.Panel spnZTUKI = null!;
    private System.Windows.Forms.Button btnSpnZTukiUp = null!;
    private System.Windows.Forms.Button btnSpnZTukiDown = null!;
    private System.Windows.Forms.Label Label14 = null!;
    private System.Windows.Forms.TextBox txtZHI = null!;
    private System.Windows.Forms.Panel spnZHI = null!;
    private System.Windows.Forms.Button btnSpnZHiUp = null!;
    private System.Windows.Forms.Button btnSpnZHiDown = null!;
    private System.Windows.Forms.Label Label7 = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.Label Label4 = null!;
    private System.Windows.Forms.Label Label5 = null!;
    private System.Windows.Forms.Label Label6 = null!;
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

    private void InitializeComponent()
    {
        Label1 = new System.Windows.Forms.Label();
        lblYMD = new System.Windows.Forms.Label();
        frmSHN = new System.Windows.Forms.GroupBox();
        optBMNCD = new System.Windows.Forms.RadioButton();
        txtBMNCD = new System.Windows.Forms.TextBox();
        lblBMNNAME = new System.Windows.Forms.Label();
        optSHNCD = new System.Windows.Forms.RadioButton();
        txtSHNCD = new System.Windows.Forms.TextBox();
        lblSHNNAME = new System.Windows.Forms.Label();
        Label2 = new System.Windows.Forms.Label();
        txtZNEN = new System.Windows.Forms.TextBox();
        spnZNEN = new System.Windows.Forms.Panel();
        btnSpnZNenUp = new System.Windows.Forms.Button();
        btnSpnZNenDown = new System.Windows.Forms.Button();
        Label13 = new System.Windows.Forms.Label();
        txtZTUKI = new System.Windows.Forms.TextBox();
        spnZTUKI = new System.Windows.Forms.Panel();
        btnSpnZTukiUp = new System.Windows.Forms.Button();
        btnSpnZTukiDown = new System.Windows.Forms.Button();
        Label14 = new System.Windows.Forms.Label();
        txtZHI = new System.Windows.Forms.TextBox();
        spnZHI = new System.Windows.Forms.Panel();
        btnSpnZHiUp = new System.Windows.Forms.Button();
        btnSpnZHiDown = new System.Windows.Forms.Button();
        Label7 = new System.Windows.Forms.Label();
        Label3 = new System.Windows.Forms.Label();
        Label4 = new System.Windows.Forms.Label();
        Label5 = new System.Windows.Forms.Label();
        Label6 = new System.Windows.Forms.Label();
        cmdSTR = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();
        frmSHN.SuspendLayout();
        spnZNEN.SuspendLayout();
        spnZTUKI.SuspendLayout();
        spnZHI.SuspendLayout();
        SuspendLayout();
        // 
        // Label1
        // 
        Label1.BackColor = System.Drawing.Color.Transparent;
        Label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label1.ForeColor = System.Drawing.SystemColors.WindowText;
        Label1.Location = new System.Drawing.Point(168, 8);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(249, 25);
        Label1.TabIndex = 7;
        Label1.Text = "在庫マスタ更新処理";
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblYMD
        // 
        lblYMD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblYMD.Location = new System.Drawing.Point(464, 16);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(113, 21);
        lblYMD.TabIndex = 5;
        lblYMD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // frmSHN
        // 
        frmSHN.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        frmSHN.Controls.Add(optBMNCD);
        frmSHN.Controls.Add(txtBMNCD);
        frmSHN.Controls.Add(lblBMNNAME);
        frmSHN.Controls.Add(optSHNCD);
        frmSHN.Controls.Add(txtSHNCD);
        frmSHN.Controls.Add(lblSHNNAME);
        frmSHN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        frmSHN.ForeColor = System.Drawing.SystemColors.WindowText;
        frmSHN.Location = new System.Drawing.Point(100, 56);
        frmSHN.Name = "frmSHN";
        frmSHN.Size = new System.Drawing.Size(445, 96);
        frmSHN.TabIndex = 15;
        frmSHN.TabStop = false;
        frmSHN.Text = "更新対象";
        // 
        // optBMNCD
        // 
        optBMNCD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optBMNCD.Checked = true;
        optBMNCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        optBMNCD.ForeColor = System.Drawing.SystemColors.WindowText;
        optBMNCD.Location = new System.Drawing.Point(12, 28);
        optBMNCD.Name = "optBMNCD";
        optBMNCD.Size = new System.Drawing.Size(137, 22);
        optBMNCD.TabIndex = 16;
        optBMNCD.TabStop = true;
        optBMNCD.Text = "部門コード指定";
        optBMNCD.UseVisualStyleBackColor = false;
        optBMNCD.Click += optBMNCD_Click;
        optBMNCD.KeyDown += optBMNCD_KeyDown;
        // 
        // txtBMNCD
        // 
        txtBMNCD.BackColor = System.Drawing.Color.White;
        txtBMNCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtBMNCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtBMNCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtBMNCD.Location = new System.Drawing.Point(156, 24);
        txtBMNCD.MaxLength = 2;
        txtBMNCD.Name = "txtBMNCD";
        txtBMNCD.Size = new System.Drawing.Size(36, 23);
        txtBMNCD.TabIndex = 0;
        txtBMNCD.KeyDown += txtBMNCD_KeyDown;
        txtBMNCD.KeyPress += txtBMNCD_KeyPress;
        txtBMNCD.Leave += txtBMNCD_Leave;
        // 
        // lblBMNNAME
        // 
        lblBMNNAME.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblBMNNAME.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblBMNNAME.ForeColor = System.Drawing.SystemColors.WindowText;
        lblBMNNAME.Location = new System.Drawing.Point(248, 30);
        lblBMNNAME.Name = "lblBMNNAME";
        lblBMNNAME.Size = new System.Drawing.Size(180, 19);
        lblBMNNAME.TabIndex = 18;
        // 
        // optSHNCD
        // 
        optSHNCD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optSHNCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        optSHNCD.ForeColor = System.Drawing.SystemColors.WindowText;
        optSHNCD.Location = new System.Drawing.Point(12, 60);
        optSHNCD.Name = "optSHNCD";
        optSHNCD.Size = new System.Drawing.Size(137, 22);
        optSHNCD.TabIndex = 17;
        optSHNCD.Text = "商品コード指定";
        optSHNCD.UseVisualStyleBackColor = false;
        optSHNCD.Click += optSHNCD_Click;
        optSHNCD.KeyDown += optSHNCD_KeyDown;
        // 
        // txtSHNCD
        // 
        txtSHNCD.BackColor = System.Drawing.Color.White;
        txtSHNCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSHNCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtSHNCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtSHNCD.Location = new System.Drawing.Point(156, 56);
        txtSHNCD.MaxLength = 8;
        txtSHNCD.Name = "txtSHNCD";
        txtSHNCD.Size = new System.Drawing.Size(85, 23);
        txtSHNCD.TabIndex = 1;
        txtSHNCD.KeyDown += txtSHNCD_KeyDown;
        txtSHNCD.KeyPress += txtSHNCD_KeyPress;
        txtSHNCD.Leave += txtSHNCD_Leave;
        // 
        // lblSHNNAME
        // 
        lblSHNNAME.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblSHNNAME.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblSHNNAME.ForeColor = System.Drawing.SystemColors.WindowText;
        lblSHNNAME.Location = new System.Drawing.Point(248, 60);
        lblSHNNAME.Name = "lblSHNNAME";
        lblSHNNAME.Size = new System.Drawing.Size(185, 19);
        lblSHNNAME.TabIndex = 19;
        // 
        // Label2
        // 
        Label2.BackColor = System.Drawing.Color.Transparent;
        Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label2.ForeColor = System.Drawing.SystemColors.WindowText;
        Label2.Location = new System.Drawing.Point(108, 184);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(95, 19);
        Label2.TabIndex = 13;
        Label2.Text = "在庫年月度";
        // 
        // txtZNEN
        // 
        txtZNEN.BackColor = System.Drawing.Color.White;
        txtZNEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtZNEN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtZNEN.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtZNEN.Location = new System.Drawing.Point(136, 216);
        txtZNEN.MaxLength = 4;
        txtZNEN.Name = "txtZNEN";
        txtZNEN.Size = new System.Drawing.Size(48, 23);
        txtZNEN.TabIndex = 2;
        txtZNEN.KeyDown += txtZNEN_KeyDown;
        txtZNEN.KeyPress += txtZNEN_KeyPress;
        txtZNEN.Leave += txtZNEN_Leave;
        // 
        // spnZNEN
        // 
        spnZNEN.Controls.Add(btnSpnZNenUp);
        spnZNEN.Controls.Add(btnSpnZNenDown);
        spnZNEN.Location = new System.Drawing.Point(184, 216);
        spnZNEN.Name = "spnZNEN";
        spnZNEN.Size = new System.Drawing.Size(20, 24);
        spnZNEN.TabIndex = 20;
        // 
        // btnSpnZNenUp
        // 
        btnSpnZNenUp.BackColor = System.Drawing.SystemColors.Control;
        btnSpnZNenUp.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnSpnZNenUp.Location = new System.Drawing.Point(0, 0);
        btnSpnZNenUp.Name = "btnSpnZNenUp";
        btnSpnZNenUp.Size = new System.Drawing.Size(20, 12);
        btnSpnZNenUp.TabIndex = 0;
        btnSpnZNenUp.TabStop = false;
        btnSpnZNenUp.Text = "▲";
        btnSpnZNenUp.UseVisualStyleBackColor = false;
        btnSpnZNenUp.Click += spnZNEN_SpinUp;
        // 
        // btnSpnZNenDown
        // 
        btnSpnZNenDown.BackColor = System.Drawing.SystemColors.Control;
        btnSpnZNenDown.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnSpnZNenDown.Location = new System.Drawing.Point(0, 12);
        btnSpnZNenDown.Name = "btnSpnZNenDown";
        btnSpnZNenDown.Size = new System.Drawing.Size(20, 12);
        btnSpnZNenDown.TabIndex = 1;
        btnSpnZNenDown.TabStop = false;
        btnSpnZNenDown.Text = "▼";
        btnSpnZNenDown.UseVisualStyleBackColor = false;
        btnSpnZNenDown.Click += spnZNEN_SpinDown;
        // 
        // Label13
        // 
        Label13.BackColor = System.Drawing.Color.Transparent;
        Label13.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label13.ForeColor = System.Drawing.SystemColors.WindowText;
        Label13.Location = new System.Drawing.Point(208, 220);
        Label13.Name = "Label13";
        Label13.Size = new System.Drawing.Size(20, 19);
        Label13.TabIndex = 12;
        Label13.Text = "年";
        // 
        // txtZTUKI
        // 
        txtZTUKI.BackColor = System.Drawing.Color.White;
        txtZTUKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtZTUKI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtZTUKI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtZTUKI.Location = new System.Drawing.Point(230, 216);
        txtZTUKI.MaxLength = 2;
        txtZTUKI.Name = "txtZTUKI";
        txtZTUKI.Size = new System.Drawing.Size(34, 23);
        txtZTUKI.TabIndex = 3;
        txtZTUKI.KeyDown += txtZTUKI_KeyDown;
        txtZTUKI.KeyPress += txtZTUKI_KeyPress;
        txtZTUKI.Leave += txtZTUKI_Leave;
        // 
        // spnZTUKI
        // 
        spnZTUKI.Controls.Add(btnSpnZTukiUp);
        spnZTUKI.Controls.Add(btnSpnZTukiDown);
        spnZTUKI.Location = new System.Drawing.Point(264, 216);
        spnZTUKI.Name = "spnZTUKI";
        spnZTUKI.Size = new System.Drawing.Size(20, 24);
        spnZTUKI.TabIndex = 21;
        // 
        // btnSpnZTukiUp
        // 
        btnSpnZTukiUp.BackColor = System.Drawing.SystemColors.Control;
        btnSpnZTukiUp.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnSpnZTukiUp.Location = new System.Drawing.Point(0, 0);
        btnSpnZTukiUp.Name = "btnSpnZTukiUp";
        btnSpnZTukiUp.Size = new System.Drawing.Size(20, 12);
        btnSpnZTukiUp.TabIndex = 0;
        btnSpnZTukiUp.TabStop = false;
        btnSpnZTukiUp.Text = "▲";
        btnSpnZTukiUp.UseVisualStyleBackColor = false;
        btnSpnZTukiUp.Click += spnZTUKI_SpinUp;
        // 
        // btnSpnZTukiDown
        // 
        btnSpnZTukiDown.BackColor = System.Drawing.SystemColors.Control;
        btnSpnZTukiDown.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnSpnZTukiDown.Location = new System.Drawing.Point(0, 12);
        btnSpnZTukiDown.Name = "btnSpnZTukiDown";
        btnSpnZTukiDown.Size = new System.Drawing.Size(20, 12);
        btnSpnZTukiDown.TabIndex = 1;
        btnSpnZTukiDown.TabStop = false;
        btnSpnZTukiDown.Text = "▼";
        btnSpnZTukiDown.UseVisualStyleBackColor = false;
        btnSpnZTukiDown.Click += spnZTUKI_SpinDown;
        // 
        // Label14
        // 
        Label14.BackColor = System.Drawing.Color.Transparent;
        Label14.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label14.ForeColor = System.Drawing.SystemColors.WindowText;
        Label14.Location = new System.Drawing.Point(288, 220);
        Label14.Name = "Label14";
        Label14.Size = new System.Drawing.Size(20, 19);
        Label14.TabIndex = 11;
        Label14.Text = "月";
        // 
        // txtZHI
        // 
        txtZHI.BackColor = System.Drawing.Color.White;
        txtZHI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtZHI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtZHI.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtZHI.Location = new System.Drawing.Point(310, 216);
        txtZHI.MaxLength = 2;
        txtZHI.Name = "txtZHI";
        txtZHI.Size = new System.Drawing.Size(34, 23);
        txtZHI.TabIndex = 22;
        txtZHI.KeyDown += txtZHI_KeyDown;
        txtZHI.KeyPress += txtZHI_KeyPress;
        txtZHI.Leave += txtZHI_Leave;
        // 
        // spnZHI
        // 
        spnZHI.Controls.Add(btnSpnZHiUp);
        spnZHI.Controls.Add(btnSpnZHiDown);
        spnZHI.Location = new System.Drawing.Point(344, 216);
        spnZHI.Name = "spnZHI";
        spnZHI.Size = new System.Drawing.Size(20, 24);
        spnZHI.TabIndex = 24;
        // 
        // btnSpnZHiUp
        // 
        btnSpnZHiUp.BackColor = System.Drawing.SystemColors.Control;
        btnSpnZHiUp.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnSpnZHiUp.Location = new System.Drawing.Point(0, 0);
        btnSpnZHiUp.Name = "btnSpnZHiUp";
        btnSpnZHiUp.Size = new System.Drawing.Size(20, 12);
        btnSpnZHiUp.TabIndex = 0;
        btnSpnZHiUp.TabStop = false;
        btnSpnZHiUp.Text = "▲";
        btnSpnZHiUp.UseVisualStyleBackColor = false;
        btnSpnZHiUp.Click += spnZHI_SpinUp;
        // 
        // btnSpnZHiDown
        // 
        btnSpnZHiDown.BackColor = System.Drawing.SystemColors.Control;
        btnSpnZHiDown.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnSpnZHiDown.Location = new System.Drawing.Point(0, 12);
        btnSpnZHiDown.Name = "btnSpnZHiDown";
        btnSpnZHiDown.Size = new System.Drawing.Size(20, 12);
        btnSpnZHiDown.TabIndex = 1;
        btnSpnZHiDown.TabStop = false;
        btnSpnZHiDown.Text = "▼";
        btnSpnZHiDown.UseVisualStyleBackColor = false;
        btnSpnZHiDown.Click += spnZHI_SpinDown;
        // 
        // Label7
        // 
        Label7.BackColor = System.Drawing.Color.Transparent;
        Label7.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label7.ForeColor = System.Drawing.SystemColors.WindowText;
        Label7.Location = new System.Drawing.Point(368, 220);
        Label7.Name = "Label7";
        Label7.Size = new System.Drawing.Size(20, 19);
        Label7.TabIndex = 23;
        Label7.Text = "日";
        // 
        // Label3
        // 
        Label3.BackColor = System.Drawing.Color.Transparent;
        Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label3.ForeColor = System.Drawing.SystemColors.WindowText;
        Label3.Location = new System.Drawing.Point(180, 256);
        Label3.Name = "Label3";
        Label3.Size = new System.Drawing.Size(60, 19);
        Label3.TabIndex = 14;
        // 
        // Label4
        // 
        Label4.BackColor = System.Drawing.Color.Transparent;
        Label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label4.ForeColor = System.Drawing.SystemColors.WindowText;
        Label4.Location = new System.Drawing.Point(244, 256);
        Label4.Name = "Label4";
        Label4.Size = new System.Drawing.Size(20, 19);
        Label4.TabIndex = 8;
        Label4.Text = "／";
        Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // Label5
        // 
        Label5.BackColor = System.Drawing.Color.Transparent;
        Label5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label5.ForeColor = System.Drawing.SystemColors.WindowText;
        Label5.Location = new System.Drawing.Point(276, 256);
        Label5.Name = "Label5";
        Label5.Size = new System.Drawing.Size(60, 19);
        Label5.TabIndex = 9;
        // 
        // Label6
        // 
        Label6.BackColor = System.Drawing.Color.Transparent;
        Label6.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label6.ForeColor = System.Drawing.SystemColors.WindowText;
        Label6.Location = new System.Drawing.Point(340, 256);
        Label6.Name = "Label6";
        Label6.Size = new System.Drawing.Size(177, 19);
        Label6.TabIndex = 10;
        Label6.Text = "件処理中です";
        // 
        // cmdSTR
        // 
        cmdSTR.BackColor = System.Drawing.SystemColors.Control;
        cmdSTR.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdSTR.Location = new System.Drawing.Point(200, 316);
        cmdSTR.Name = "cmdSTR";
        cmdSTR.Size = new System.Drawing.Size(81, 33);
        cmdSTR.TabIndex = 4;
        cmdSTR.Text = "実 行";
        cmdSTR.UseVisualStyleBackColor = false;
        cmdSTR.Click += cmdSTR_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdCAN.Location = new System.Drawing.Point(320, 316);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(81, 33);
        cmdCAN.TabIndex = 6;
        cmdCAN.Text = "終 了";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // FrmKZKO0010
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(600, 386);
        ControlBox = false;
        Controls.Add(cmdCAN);
        Controls.Add(cmdSTR);
        Controls.Add(Label6);
        Controls.Add(Label5);
        Controls.Add(Label4);
        Controls.Add(Label3);
        Controls.Add(Label7);
        Controls.Add(spnZHI);
        Controls.Add(txtZHI);
        Controls.Add(Label14);
        Controls.Add(spnZTUKI);
        Controls.Add(txtZTUKI);
        Controls.Add(Label13);
        Controls.Add(spnZNEN);
        Controls.Add(txtZNEN);
        Controls.Add(Label2);
        Controls.Add(frmSHN);
        Controls.Add(lblYMD);
        Controls.Add(Label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmKZKO0010";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KZKO0010";
        Load += FrmKZKO0010_Load;
        frmSHN.ResumeLayout(false);
        frmSHN.PerformLayout();
        spnZNEN.ResumeLayout(false);
        spnZTUKI.ResumeLayout(false);
        spnZHI.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

