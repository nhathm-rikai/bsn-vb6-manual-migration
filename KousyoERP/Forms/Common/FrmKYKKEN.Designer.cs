#nullable enable
namespace KousyoERP.Forms.Common;

partial class FrmKYKKEN
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.GroupBox grpKanaSearch = null!;
    private System.Windows.Forms.GroupBox Frame1 = null!;
    private System.Windows.Forms.RadioButton optGP = null!;
    private System.Windows.Forms.RadioButton optJN = null!;
    private System.Windows.Forms.RadioButton optIG = null!;
    private System.Windows.Forms.RadioButton optIK = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.ComboBox cmbGPCD = null!;
    private System.Windows.Forms.GroupBox Frame2 = null!;
    private System.Windows.Forms.RadioButton optNG = null!;
    private System.Windows.Forms.Button cmdSER = null!;
    private System.Windows.Forms.GroupBox grpNameSearch = null!;
    private System.Windows.Forms.Panel Frame3 = null!;
    private System.Windows.Forms.RadioButton Option1 = null!;
    private System.Windows.Forms.RadioButton Option2 = null!;
    private System.Windows.Forms.RadioButton Option3 = null!;
    private System.Windows.Forms.CheckBox Check3 = null!;
    private System.Windows.Forms.CheckBox Check1 = null!;
    private System.Windows.Forms.CheckBox Check2 = null!;
    private System.Windows.Forms.Label Label6 = null!;
    private System.Windows.Forms.TextBox Text1 = null!;
    private System.Windows.Forms.Button Command1 = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.ComboBox cmbKCD = null!;
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
        grpKanaSearch = new System.Windows.Forms.GroupBox();
        Frame1 = new System.Windows.Forms.GroupBox();
        optGP = new System.Windows.Forms.RadioButton();
        optJN = new System.Windows.Forms.RadioButton();
        optIG = new System.Windows.Forms.RadioButton();
        optIK = new System.Windows.Forms.RadioButton();
        Label2 = new System.Windows.Forms.Label();
        cmbGPCD = new System.Windows.Forms.ComboBox();
        Frame2 = new System.Windows.Forms.GroupBox();
        optNG = new System.Windows.Forms.RadioButton();
        cmdSER = new System.Windows.Forms.Button();
        grpNameSearch = new System.Windows.Forms.GroupBox();
        Frame3 = new System.Windows.Forms.Panel();
        Option1 = new System.Windows.Forms.RadioButton();
        Option2 = new System.Windows.Forms.RadioButton();
        Option3 = new System.Windows.Forms.RadioButton();
        Check3 = new System.Windows.Forms.CheckBox();
        Check1 = new System.Windows.Forms.CheckBox();
        Check2 = new System.Windows.Forms.CheckBox();
        Label6 = new System.Windows.Forms.Label();
        Text1 = new System.Windows.Forms.TextBox();
        Command1 = new System.Windows.Forms.Button();
        Label3 = new System.Windows.Forms.Label();
        cmbKCD = new System.Windows.Forms.ComboBox();
        cmdRET = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();
        toolTip1 = new System.Windows.Forms.ToolTip(components);
        grpKanaSearch.SuspendLayout();
        Frame1.SuspendLayout();
        Frame2.SuspendLayout();
        grpNameSearch.SuspendLayout();
        Frame3.SuspendLayout();
        SuspendLayout();
        // 
        // Label1
        // 
        Label1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label1.ForeColor = System.Drawing.SystemColors.WindowText;
        Label1.Location = new System.Drawing.Point(240, 8);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(220, 26);
        Label1.TabIndex = 0;
        Label1.Text = "顧客コード検索";
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblYMD
        // 
        lblYMD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblYMD.Location = new System.Drawing.Point(616, 16);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(105, 21);
        lblYMD.TabIndex = 1;
        lblYMD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // grpKanaSearch
        // 
        grpKanaSearch.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        grpKanaSearch.Controls.Add(Frame2);
        grpKanaSearch.Controls.Add(cmbGPCD);
        grpKanaSearch.Controls.Add(Label2);
        grpKanaSearch.Controls.Add(Frame1);
        grpKanaSearch.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        grpKanaSearch.Location = new System.Drawing.Point(20, 48);
        grpKanaSearch.Name = "grpKanaSearch";
        grpKanaSearch.Size = new System.Drawing.Size(705, 352);
        grpKanaSearch.TabIndex = 2;
        grpKanaSearch.TabStop = false;
        grpKanaSearch.Text = "区分・カナ検索";
        // 
        // Frame1
        // 
        Frame1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Frame1.Controls.Add(optIK);
        Frame1.Controls.Add(optIG);
        Frame1.Controls.Add(optJN);
        Frame1.Controls.Add(optGP);
        Frame1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Frame1.Location = new System.Drawing.Point(20, 26);
        Frame1.Name = "Frame1";
        Frame1.Size = new System.Drawing.Size(446, 52);
        Frame1.TabIndex = 2;
        Frame1.TabStop = false;
        Frame1.Text = "顧客区分";
        // 
        // optGP
        // 
        optGP.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optGP.Location = new System.Drawing.Point(16, 22);
        optGP.Name = "optGP";
        optGP.Size = new System.Drawing.Size(112, 22);
        optGP.TabIndex = 3;
        optGP.Text = "ｸﾞﾙｰﾌﾟ会社";
        optGP.UseVisualStyleBackColor = false;
        optGP.Click += optGP_Click;
        // 
        // optJN
        // 
        optJN.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optJN.Checked = true;
        optJN.Location = new System.Drawing.Point(136, 22);
        optJN.Name = "optJN";
        optJN.Size = new System.Drawing.Size(80, 22);
        optJN.TabIndex = 4;
        optJN.TabStop = true;
        optJN.Text = "従業員";
        optJN.UseVisualStyleBackColor = false;
        optJN.Click += optJN_Click;
        // 
        // optIG
        // 
        optIG.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optIG.Location = new System.Drawing.Point(232, 22);
        optIG.Name = "optIG";
        optIG.Size = new System.Drawing.Size(96, 22);
        optIG.TabIndex = 5;
        optIG.Text = "一般企業";
        optIG.UseVisualStyleBackColor = false;
        optIG.Click += optIG_Click;
        // 
        // optIK
        // 
        optIK.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optIK.Location = new System.Drawing.Point(344, 22);
        optIK.Name = "optIK";
        optIK.Size = new System.Drawing.Size(96, 22);
        optIK.TabIndex = 6;
        optIK.Text = "一般個人";
        optIK.UseVisualStyleBackColor = false;
        optIK.Click += optIK_Click;
        // 
        // Label2
        // 
        Label2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label2.Location = new System.Drawing.Point(24, 92);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(124, 20);
        Label2.TabIndex = 7;
        Label2.Text = "ｸﾞﾙｰﾌﾟ会社ｺｰﾄﾞ";
        // 
        // cmbGPCD
        // 
        cmbGPCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbGPCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbGPCD.FormattingEnabled = true;
        cmbGPCD.Location = new System.Drawing.Point(154, 88);
        cmbGPCD.Name = "cmbGPCD";
        cmbGPCD.Size = new System.Drawing.Size(390, 24);
        cmbGPCD.TabIndex = 8;
        cmbGPCD.SelectedIndexChanged += cmbGPCD_SelectedIndexChanged;
        // 
        // Frame2
        // 
        Frame2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Frame2.Controls.Add(cmdSER);
        Frame2.Controls.Add(optNG);
        Frame2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Frame2.Location = new System.Drawing.Point(20, 124);
        Frame2.Name = "Frame2";
        Frame2.Size = new System.Drawing.Size(665, 214);
        Frame2.TabIndex = 9;
        Frame2.TabStop = false;
        Frame2.Text = "顧客カナ";
        // 
        // optNG
        // 
        optNG.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        optNG.Location = new System.Drawing.Point(10, 156);
        optNG.Name = "optNG";
        optNG.Size = new System.Drawing.Size(64, 22);
        optNG.TabIndex = 11;
        optNG.Text = "新潟";
        optNG.UseVisualStyleBackColor = false;
        optNG.Click += optNG_Click;
        // 
        // cmdSER
        // 
        cmdSER.BackColor = System.Drawing.SystemColors.Control;
        cmdSER.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdSER.Location = new System.Drawing.Point(552, 168);
        cmdSER.Name = "cmdSER";
        cmdSER.Size = new System.Drawing.Size(96, 34);
        cmdSER.TabIndex = 16;
        cmdSER.Text = "検索開始";
        cmdSER.UseVisualStyleBackColor = false;
        cmdSER.Click += cmdSER_Click;
        // 
        // grpNameSearch
        // 
        grpNameSearch.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        grpNameSearch.Controls.Add(Command1);
        grpNameSearch.Controls.Add(Text1);
        grpNameSearch.Controls.Add(Label6);
        grpNameSearch.Controls.Add(Check2);
        grpNameSearch.Controls.Add(Check1);
        grpNameSearch.Controls.Add(Check3);
        grpNameSearch.Controls.Add(Frame3);
        grpNameSearch.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        grpNameSearch.Location = new System.Drawing.Point(20, 410);
        grpNameSearch.Name = "grpNameSearch";
        grpNameSearch.Size = new System.Drawing.Size(705, 102);
        grpNameSearch.TabIndex = 89;
        grpNameSearch.TabStop = false;
        grpNameSearch.Text = "名称検索";
        // 
        // Frame3
        // 
        Frame3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Frame3.Controls.Add(Option3);
        Frame3.Controls.Add(Option2);
        Frame3.Controls.Add(Option1);
        Frame3.Location = new System.Drawing.Point(12, 22);
        Frame3.Name = "Frame3";
        Frame3.Size = new System.Drawing.Size(288, 28);
        Frame3.TabIndex = 91;
        // 
        // Option1
        // 
        Option1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Option1.Checked = true;
        Option1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Option1.Location = new System.Drawing.Point(4, 4);
        Option1.Name = "Option1";
        Option1.Size = new System.Drawing.Size(88, 20);
        Option1.TabIndex = 92;
        Option1.TabStop = false;
        Option1.Text = "部分一致";
        Option1.UseVisualStyleBackColor = false;
        // 
        // Option2
        // 
        Option2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Option2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Option2.Location = new System.Drawing.Point(96, 4);
        Option2.Name = "Option2";
        Option2.Size = new System.Drawing.Size(88, 20);
        Option2.TabIndex = 93;
        Option2.TabStop = false;
        Option2.Text = "前方一致";
        Option2.UseVisualStyleBackColor = false;
        // 
        // Option3
        // 
        Option3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Option3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Option3.Location = new System.Drawing.Point(188, 4);
        Option3.Name = "Option3";
        Option3.Size = new System.Drawing.Size(88, 20);
        Option3.TabIndex = 94;
        Option3.TabStop = false;
        Option3.Text = "後方一致";
        Option3.UseVisualStyleBackColor = false;
        // 
        // Check3
        // 
        Check3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Check3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Check3.Location = new System.Drawing.Point(312, 26);
        Check3.Name = "Check3";
        Check3.Size = new System.Drawing.Size(112, 20);
        Check3.TabIndex = 97;
        Check3.Text = "あいまい検索";
        toolTip1.SetToolTip(Check3, "区分・カナ検索で指定したｸﾞﾙｰﾌﾟ会社ｺｰﾄﾞで絞込みます");
        Check3.UseVisualStyleBackColor = false;
        // 
        // Check1
        // 
        Check1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Check1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Check1.Location = new System.Drawing.Point(432, 26);
        Check1.Name = "Check1";
        Check1.Size = new System.Drawing.Size(112, 20);
        Check1.TabIndex = 95;
        Check1.Text = "顧客区分含む";
        toolTip1.SetToolTip(Check1, "区分・カナ検索で指定した顧客区分で絞込みます");
        Check1.UseVisualStyleBackColor = false;
        // 
        // Check2
        // 
        Check2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Check2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Check2.Location = new System.Drawing.Point(548, 26);
        Check2.Name = "Check2";
        Check2.Size = new System.Drawing.Size(150, 20);
        Check2.TabIndex = 96;
        Check2.Text = "ｸﾞﾙｰﾌﾟ会社ｺｰﾄﾞ含む";
        toolTip1.SetToolTip(Check2, "区分・カナ検索で指定したｸﾞﾙｰﾌﾟ会社ｺｰﾄﾞで絞込みます");
        Check2.UseVisualStyleBackColor = false;
        // 
        // Label6
        // 
        Label6.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label6.Location = new System.Drawing.Point(16, 60);
        Label6.Name = "Label6";
        Label6.Size = new System.Drawing.Size(68, 22);
        Label6.TabIndex = 90;
        Label6.Text = "顧客名";
        // 
        // Text1
        // 
        Text1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Text1.Location = new System.Drawing.Point(92, 56);
        Text1.Name = "Text1";
        Text1.Size = new System.Drawing.Size(492, 24);
        Text1.TabIndex = 87;
        Text1.KeyPress += Text1_KeyPress;
        // 
        // Command1
        // 
        Command1.BackColor = System.Drawing.SystemColors.Control;
        Command1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Command1.Location = new System.Drawing.Point(596, 54);
        Command1.Name = "Command1";
        Command1.Size = new System.Drawing.Size(96, 34);
        Command1.TabIndex = 88;
        Command1.Text = "検索開始";
        Command1.UseVisualStyleBackColor = false;
        Command1.Click += Command1_Click;
        // 
        // Label3
        // 
        Label3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label3.Location = new System.Drawing.Point(32, 532);
        Label3.Name = "Label3";
        Label3.Size = new System.Drawing.Size(96, 20);
        Label3.TabIndex = 12;
        Label3.Text = "顧客コード";
        // 
        // cmbKCD
        // 
        cmbKCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbKCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbKCD.FormattingEnabled = true;
        cmbKCD.Location = new System.Drawing.Point(160, 528);
        cmbKCD.Name = "cmbKCD";
        cmbKCD.Size = new System.Drawing.Size(466, 25);
        cmbKCD.TabIndex = 13;
        // 
        // cmdRET
        // 
        cmdRET.BackColor = System.Drawing.SystemColors.Control;
        cmdRET.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdRET.Location = new System.Drawing.Point(40, 568);
        cmdRET.Name = "cmdRET";
        cmdRET.Size = new System.Drawing.Size(92, 34);
        cmdRET.TabIndex = 14;
        cmdRET.Text = "戻  る";
        cmdRET.UseVisualStyleBackColor = false;
        cmdRET.Click += cmdRET_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdCAN.Location = new System.Drawing.Point(168, 568);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(92, 34);
        cmdCAN.TabIndex = 15;
        cmdCAN.Text = "ｷｬﾝｾﾙ";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // FrmKYKKEN
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(745, 616);
        ControlBox = false;
        Controls.Add(cmdCAN);
        Controls.Add(cmdRET);
        Controls.Add(cmbKCD);
        Controls.Add(Label3);
        Controls.Add(grpNameSearch);
        Controls.Add(grpKanaSearch);
        Controls.Add(lblYMD);
        Controls.Add(Label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmKYKKEN";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "KYKKEN";
        Load += FrmKYKKEN_Load;
        grpKanaSearch.ResumeLayout(false);
        Frame1.ResumeLayout(false);
        Frame2.ResumeLayout(false);
        grpNameSearch.ResumeLayout(false);
        grpNameSearch.PerformLayout();
        Frame3.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
}

