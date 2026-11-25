#nullable enable
namespace KousyoERP.Forms.Common;

partial class FrmSYOKEN
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.Label Label6 = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.ComboBox cmbDAI = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.ComboBox cmbTYU = null!;
    private System.Windows.Forms.Label Label4 = null!;
    private System.Windows.Forms.ComboBox cmbMCD = null!;
    private System.Windows.Forms.Button cmdSER = null!;
    private System.Windows.Forms.Label Label7 = null!;
    private System.Windows.Forms.Panel Frame1 = null!;
    private System.Windows.Forms.RadioButton Option1 = null!;
    private System.Windows.Forms.RadioButton Option2 = null!;
    private System.Windows.Forms.RadioButton Option3 = null!;
    private System.Windows.Forms.CheckBox Check2 = null!;
    private System.Windows.Forms.CheckBox Check1 = null!;
    private System.Windows.Forms.Label Label8 = null!;
    private System.Windows.Forms.TextBox Text1 = null!;
    private System.Windows.Forms.Button Command1 = null!;
    private System.Windows.Forms.Label Label5 = null!;
    private System.Windows.Forms.ComboBox cmbSCD = null!;
    private System.Windows.Forms.Button cmdRET = null!;
    private System.Windows.Forms.Button cmdCAN = null!;
    private System.Windows.Forms.ToolTip toolTip1 = null!;

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
        components = new System.ComponentModel.Container();
        Label1 = new System.Windows.Forms.Label();
        lblYMD = new System.Windows.Forms.Label();
        Label6 = new System.Windows.Forms.Label();
        Label2 = new System.Windows.Forms.Label();
        cmbDAI = new System.Windows.Forms.ComboBox();
        Label3 = new System.Windows.Forms.Label();
        cmbTYU = new System.Windows.Forms.ComboBox();
        Label4 = new System.Windows.Forms.Label();
        cmbMCD = new System.Windows.Forms.ComboBox();
        cmdSER = new System.Windows.Forms.Button();
        Label7 = new System.Windows.Forms.Label();
        Frame1 = new System.Windows.Forms.Panel();
        Option1 = new System.Windows.Forms.RadioButton();
        Option2 = new System.Windows.Forms.RadioButton();
        Option3 = new System.Windows.Forms.RadioButton();
        Check2 = new System.Windows.Forms.CheckBox();
        Check1 = new System.Windows.Forms.CheckBox();
        Label8 = new System.Windows.Forms.Label();
        Text1 = new System.Windows.Forms.TextBox();
        Command1 = new System.Windows.Forms.Button();
        Label5 = new System.Windows.Forms.Label();
        cmbSCD = new System.Windows.Forms.ComboBox();
        cmdRET = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();
        toolTip1 = new System.Windows.Forms.ToolTip(components);
        Frame1.SuspendLayout();
        SuspendLayout();
        // 
        // Label1
        // 
        Label1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label1.ForeColor = System.Drawing.SystemColors.WindowText;
        Label1.Location = new System.Drawing.Point(184, 8);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(185, 25);
        Label1.TabIndex = 0;
        Label1.Text = "商品コード検索";
        Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // lblYMD
        // 
        lblYMD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblYMD.Location = new System.Drawing.Point(424, 16);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(113, 17);
        lblYMD.TabIndex = 1;
        lblYMD.TextAlign = System.Drawing.ContentAlignment.TopRight;
        // 
        // Label6
        // 
        Label6.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label6.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label6.ForeColor = System.Drawing.SystemColors.WindowText;
        Label6.Location = new System.Drawing.Point(24, 40);
        Label6.Name = "Label6";
        Label6.Size = new System.Drawing.Size(89, 17);
        Label6.TabIndex = 13;
        Label6.Text = "コード検索";
        Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // Label2
        // 
        Label2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label2.ForeColor = System.Drawing.SystemColors.WindowText;
        Label2.Location = new System.Drawing.Point(24, 80);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(97, 17);
        Label2.TabIndex = 2;
        Label2.Text = "大分類コード";
        // 
        // cmbDAI
        // 
        cmbDAI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbDAI.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbDAI.Location = new System.Drawing.Point(144, 80);
        cmbDAI.Name = "cmbDAI";
        cmbDAI.Size = new System.Drawing.Size(217, 24);
        cmbDAI.TabIndex = 6;
        cmbDAI.SelectedIndexChanged += cmbDAI_SelectedIndexChanged;
        // 
        // Label3
        // 
        Label3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label3.ForeColor = System.Drawing.SystemColors.WindowText;
        Label3.Location = new System.Drawing.Point(24, 128);
        Label3.Name = "Label3";
        Label3.Size = new System.Drawing.Size(97, 17);
        Label3.TabIndex = 3;
        Label3.Text = "中分類コード";
        // 
        // cmbTYU
        // 
        cmbTYU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbTYU.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbTYU.Location = new System.Drawing.Point(144, 128);
        cmbTYU.Name = "cmbTYU";
        cmbTYU.Size = new System.Drawing.Size(217, 24);
        cmbTYU.TabIndex = 7;
        cmbTYU.SelectedIndexChanged += cmbTYU_SelectedIndexChanged;
        // 
        // Label4
        // 
        Label4.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label4.ForeColor = System.Drawing.SystemColors.WindowText;
        Label4.Location = new System.Drawing.Point(24, 176);
        Label4.Name = "Label4";
        Label4.Size = new System.Drawing.Size(113, 17);
        Label4.TabIndex = 4;
        Label4.Text = "メーカーコード";
        // 
        // cmbMCD
        // 
        cmbMCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbMCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbMCD.Location = new System.Drawing.Point(144, 176);
        cmbMCD.Name = "cmbMCD";
        cmbMCD.Size = new System.Drawing.Size(217, 24);
        cmbMCD.TabIndex = 8;
        cmbMCD.SelectedIndexChanged += cmbMCD_SelectedIndexChanged;
        // 
        // cmdSER
        // 
        cmdSER.BackColor = System.Drawing.SystemColors.Control;
        cmdSER.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdSER.Location = new System.Drawing.Point(432, 176);
        cmdSER.Name = "cmdSER";
        cmdSER.Size = new System.Drawing.Size(89, 33);
        cmdSER.TabIndex = 10;
        cmdSER.Text = "検索開始";
        cmdSER.UseVisualStyleBackColor = false;
        cmdSER.Click += cmdSER_Click;
        // 
        // Label7
        // 
        Label7.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label7.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label7.ForeColor = System.Drawing.SystemColors.WindowText;
        Label7.Location = new System.Drawing.Point(24, 240);
        Label7.Name = "Label7";
        Label7.Size = new System.Drawing.Size(81, 17);
        Label7.TabIndex = 16;
        Label7.Text = "名称検索";
        Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // Frame1
        // 
        Frame1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Frame1.Controls.Add(Option3);
        Frame1.Controls.Add(Option2);
        Frame1.Controls.Add(Option1);
        Frame1.Location = new System.Drawing.Point(16, 256);
        Frame1.Name = "Frame1";
        Frame1.Size = new System.Drawing.Size(305, 25);
        Frame1.TabIndex = 18;
        // 
        // Option1
        // 
        Option1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Option1.Checked = true;
        Option1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Option1.Location = new System.Drawing.Point(8, 0);
        Option1.Name = "Option1";
        Option1.Size = new System.Drawing.Size(81, 25);
        Option1.TabIndex = 19;
        Option1.TabStop = true;
        Option1.Text = "部分一致";
        Option1.UseVisualStyleBackColor = false;
        // 
        // Option2
        // 
        Option2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Option2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Option2.Location = new System.Drawing.Point(104, 0);
        Option2.Name = "Option2";
        Option2.Size = new System.Drawing.Size(73, 25);
        Option2.TabIndex = 20;
        Option2.Text = "前方一致";
        Option2.UseVisualStyleBackColor = false;
        // 
        // Option3
        // 
        Option3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Option3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Option3.Location = new System.Drawing.Point(200, 0);
        Option3.Name = "Option3";
        Option3.Size = new System.Drawing.Size(89, 25);
        Option3.TabIndex = 21;
        Option3.Text = "後方一致";
        Option3.UseVisualStyleBackColor = false;
        // 
        // Check2
        // 
        Check2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Check2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Check2.Location = new System.Drawing.Point(320, 256);
        Check2.Name = "Check2";
        Check2.Size = new System.Drawing.Size(97, 17);
        Check2.TabIndex = 23;
        Check2.Text = "あいまい検索";
        toolTip1.SetToolTip(Check2, "コード検索で指定した大分類・中分類・メーカーで絞込みます");
        Check2.UseVisualStyleBackColor = false;
        // 
        // Check1
        // 
        Check1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Check1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Check1.Location = new System.Drawing.Point(424, 256);
        Check1.Name = "Check1";
        Check1.Size = new System.Drawing.Size(105, 17);
        Check1.TabIndex = 22;
        Check1.Text = "分類コード含む";
        toolTip1.SetToolTip(Check1, "コード検索で指定した大分類・中分類・メーカーで絞込みます");
        Check1.UseVisualStyleBackColor = false;
        // 
        // Label8
        // 
        Label8.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label8.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label8.ForeColor = System.Drawing.SystemColors.WindowText;
        Label8.Location = new System.Drawing.Point(24, 284);
        Label8.Name = "Label8";
        Label8.Size = new System.Drawing.Size(65, 25);
        Label8.TabIndex = 17;
        Label8.Text = "商品名";
        // 
        // Text1
        // 
        Text1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Text1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Text1.Location = new System.Drawing.Point(104, 280);
        Text1.Name = "Text1";
        Text1.Size = new System.Drawing.Size(297, 25);
        Text1.TabIndex = 14;
        Text1.KeyPress += Text1_KeyPress;
        // 
        // Command1
        // 
        Command1.BackColor = System.Drawing.SystemColors.Control;
        Command1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Command1.Location = new System.Drawing.Point(432, 280);
        Command1.Name = "Command1";
        Command1.Size = new System.Drawing.Size(89, 33);
        Command1.TabIndex = 15;
        Command1.Text = "検索開始";
        Command1.UseVisualStyleBackColor = false;
        Command1.Click += Command1_Click;
        // 
        // Label5
        // 
        Label5.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label5.ForeColor = System.Drawing.SystemColors.WindowText;
        Label5.Location = new System.Drawing.Point(24, 360);
        Label5.Name = "Label5";
        Label5.Size = new System.Drawing.Size(81, 17);
        Label5.TabIndex = 5;
        Label5.Text = "商品コード";
        // 
        // cmbSCD
        // 
        cmbSCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbSCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbSCD.Location = new System.Drawing.Point(136, 360);
        cmbSCD.Name = "cmbSCD";
        cmbSCD.Size = new System.Drawing.Size(401, 24);
        cmbSCD.TabIndex = 9;
        // 
        // cmdRET
        // 
        cmdRET.BackColor = System.Drawing.SystemColors.Control;
        cmdRET.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdRET.Location = new System.Drawing.Point(30, 408);
        cmdRET.Name = "cmdRET";
        cmdRET.Size = new System.Drawing.Size(89, 33);
        cmdRET.TabIndex = 11;
        cmdRET.Text = "戻  る";
        cmdRET.UseVisualStyleBackColor = false;
        cmdRET.Click += cmdRET_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdCAN.Location = new System.Drawing.Point(144, 408);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(89, 33);
        cmdCAN.TabIndex = 12;
        cmdCAN.Text = "ｷｬﾝｾﾙ";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // FrmSYOKEN
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(546, 465);
        ControlBox = false;
        Controls.Add(Check2);
        Controls.Add(Check1);
        Controls.Add(Frame1);
        Controls.Add(Command1);
        Controls.Add(Text1);
        Controls.Add(Label8);
        Controls.Add(Label7);
        Controls.Add(Label6);
        Controls.Add(cmdCAN);
        Controls.Add(cmdRET);
        Controls.Add(cmdSER);
        Controls.Add(cmbSCD);
        Controls.Add(cmbMCD);
        Controls.Add(cmbTYU);
        Controls.Add(cmbDAI);
        Controls.Add(Label5);
        Controls.Add(Label4);
        Controls.Add(Label3);
        Controls.Add(Label2);
        Controls.Add(lblYMD);
        Controls.Add(Label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmSYOKEN";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "SYOKEN";
        Load += FrmSYOKEN_Load;
        Frame1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

