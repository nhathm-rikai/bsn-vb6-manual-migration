#nullable enable
namespace KousyoERP.Forms.Master;

partial class FrmKUNO0110
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label19 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.GroupBox Frame1 = null!;
    private System.Windows.Forms.RadioButton opt1 = null!;
    private System.Windows.Forms.RadioButton opt2 = null!;
    private System.Windows.Forms.RadioButton opt3 = null!;
    private System.Windows.Forms.RadioButton opt4 = null!;
    private System.Windows.Forms.RadioButton opt5 = null!;
    private System.Windows.Forms.RadioButton opt6 = null!;
    private System.Windows.Forms.RadioButton opt7 = null!;
    private System.Windows.Forms.RadioButton opt8 = null!;
    private System.Windows.Forms.RadioButton opt9 = null!;
    private System.Windows.Forms.RadioButton opt10 = null!;
    private System.Windows.Forms.RadioButton opt11 = null!;
    private System.Windows.Forms.RadioButton opt12 = null!;
    private System.Windows.Forms.RadioButton opt13 = null!;
    private System.Windows.Forms.RadioButton opt14 = null!;
    private System.Windows.Forms.RadioButton opt15 = null!;
    private System.Windows.Forms.RadioButton opt16 = null!;
    private System.Windows.Forms.RadioButton opt17 = null!;

    private System.Windows.Forms.Label LblCD = null!;
    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.TextBox TXTCD = null!;
    private System.Windows.Forms.Label LblNMK = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.TextBox txtNMK = null!;
    private System.Windows.Forms.Label LblNMKL = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.TextBox txtNMKJ = null!;
    private System.Windows.Forms.Label LblNMS = null!;
    private System.Windows.Forms.Label Label4 = null!;
    private System.Windows.Forms.TextBox txtNMS = null!;

    private System.Windows.Forms.Button cmdOK = null!;
    private System.Windows.Forms.Button cmdCAN = null!;
    private System.Windows.Forms.Button cmdDEL = null!;
    private System.Windows.Forms.Button cmdEND = null!;

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
        var font12 = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        var bg = System.Drawing.Color.FromArgb(192, 192, 192);

        Label19 = new System.Windows.Forms.Label();
        lblYMD = new System.Windows.Forms.Label();
        Frame1 = new System.Windows.Forms.GroupBox();
        opt1 = new System.Windows.Forms.RadioButton();
        opt2 = new System.Windows.Forms.RadioButton();
        opt3 = new System.Windows.Forms.RadioButton();
        opt4 = new System.Windows.Forms.RadioButton();
        opt5 = new System.Windows.Forms.RadioButton();
        opt6 = new System.Windows.Forms.RadioButton();
        opt7 = new System.Windows.Forms.RadioButton();
        opt8 = new System.Windows.Forms.RadioButton();
        opt9 = new System.Windows.Forms.RadioButton();
        opt10 = new System.Windows.Forms.RadioButton();
        opt11 = new System.Windows.Forms.RadioButton();
        opt12 = new System.Windows.Forms.RadioButton();
        opt13 = new System.Windows.Forms.RadioButton();
        opt14 = new System.Windows.Forms.RadioButton();
        opt15 = new System.Windows.Forms.RadioButton();
        opt16 = new System.Windows.Forms.RadioButton();
        opt17 = new System.Windows.Forms.RadioButton();
        LblCD = new System.Windows.Forms.Label();
        Label1 = new System.Windows.Forms.Label();
        TXTCD = new System.Windows.Forms.TextBox();
        LblNMK = new System.Windows.Forms.Label();
        Label2 = new System.Windows.Forms.Label();
        txtNMK = new System.Windows.Forms.TextBox();
        LblNMKL = new System.Windows.Forms.Label();
        Label3 = new System.Windows.Forms.Label();
        txtNMKJ = new System.Windows.Forms.TextBox();
        LblNMS = new System.Windows.Forms.Label();
        Label4 = new System.Windows.Forms.Label();
        txtNMS = new System.Windows.Forms.TextBox();
        cmdOK = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();
        cmdDEL = new System.Windows.Forms.Button();
        cmdEND = new System.Windows.Forms.Button();

        Frame1.SuspendLayout();
        SuspendLayout();

        // Header
        Label19.BackColor = bg;
        Label19.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label19.Location = new System.Drawing.Point(248, 8);
        Label19.Name = "Label19";
        Label19.Size = new System.Drawing.Size(185, 25);
        Label19.TabIndex = 28;
        Label19.Text = "名称マスタ登録";

        lblYMD.BackColor = bg;
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.Location = new System.Drawing.Point(544, 8);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(97, 21);
        lblYMD.TabIndex = 0;

        // Frame1: 名称情報の選択
        Frame1.BackColor = bg;
        Frame1.Font = font12;
        Frame1.Location = new System.Drawing.Point(8, 40);
        Frame1.Name = "Frame1";
        Frame1.Size = new System.Drawing.Size(656, 217);
        Frame1.TabIndex = 1;
        Frame1.TabStop = false;
        Frame1.Text = "名称情報の選択";
        Frame1.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            opt1, opt2, opt3, opt4,
            opt5, opt6, opt7, opt8,
            opt9,
            opt10, opt11, opt12, opt13,
            opt14,
            opt15, opt16, opt17
        });

        // Row 1 (y = 32)
        ConfigureOption(opt1, "opt1", "ブロック情報1桁", 12, 32, 156, 2, 1, font12, bg);
        ConfigureOption(opt2, "opt2", "主管情報2桁", 172, 32, 152, 3, 2, font12, bg);
        ConfigureOption(opt3, "opt3", "部課名情報2桁", 328, 32, 150, 4, 3, font12, bg);
        ConfigureOption(opt4, "opt4", "部門情報2桁", 482, 32, 166, 5, 4, font12, bg);

        // Row 2 (y = 64)
        ConfigureOption(opt5, "opt5", "身分情報2桁", 12, 64, 156, 6, 5, font12, bg);
        ConfigureOption(opt6, "opt6", "職名情報2桁", 172, 64, 152, 7, 6, font12, bg);
        ConfigureOption(opt7, "opt7", "続柄情報2桁", 328, 64, 150, 8, 7, font12, bg);
        ConfigureOption(opt8, "opt8", "所属区分情報１桁", 482, 64, 166, 9, 8, font12, bg);

        // Row 3 (y = 96)
        ConfigureOption(opt9, "opt9", "メーカー情報4桁", 12, 96, 156, 10, 9, font12, bg);

        // Row 4 (y = 128)
        ConfigureOption(opt10, "opt10", "用途情報2桁", 12, 128, 156, 11, 10, font12, bg);
        ConfigureOption(opt11, "opt11", "燃料種類情報1桁", 172, 128, 152, 12, 11, font12, bg);
        ConfigureOption(opt12, "opt12", "石油元売情報2桁", 328, 128, 150, 13, 12, font12, bg);
        ConfigureOption(opt13, "opt13", "代行SS区分情報2桁", 482, 128, 168, 14, 13, font12, bg);

        // Row 5 (y = 160)
        ConfigureOption(opt14, "opt14", "自販機機種情報2桁", 12, 160, 170, 15, 14, font12, bg);

        // Row 6 (y = 192)
        ConfigureOption(opt15, "opt15", "単位情報2桁", 12, 192, 156, 16, 15, font12, bg);
        ConfigureOption(opt16, "opt16", "摘要情報3桁", 172, 192, 152, 34, 16, font12, bg);
        ConfigureOption(opt17, "opt17", "事業所記号3桁", 328, 192, 150, 35, 17, font12, bg);

        // Input rows
        LblCD.BackColor = bg; LblCD.Font = font12; LblCD.Location = new System.Drawing.Point(32, 272); LblCD.Name = "LblCD"; LblCD.Size = new System.Drawing.Size(113, 19); LblCD.TabIndex = 29;
        Label1.BackColor = bg; Label1.Font = font12; Label1.Location = new System.Drawing.Point(160, 272); Label1.Name = "Label1"; Label1.Size = new System.Drawing.Size(89, 19); Label1.TabIndex = 30; Label1.Text = "コード";
        TXTCD.BackColor = System.Drawing.Color.White; TXTCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        TXTCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        TXTCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        TXTCD.Location = new System.Drawing.Point(248, 264); TXTCD.MaxLength = 4; TXTCD.Name = "TXTCD"; TXTCD.Size = new System.Drawing.Size(60, 26); TXTCD.TabIndex = 17;
        TXTCD.Enter += TXTCD_Enter; TXTCD.KeyDown += TXTCD_KeyDown; TXTCD.KeyPress += TXTCD_KeyPress; TXTCD.Leave += TXTCD_Leave;

        LblNMK.BackColor = bg; LblNMK.Font = font12; LblNMK.Location = new System.Drawing.Point(32, 304); LblNMK.Name = "LblNMK"; LblNMK.Size = new System.Drawing.Size(113, 19); LblNMK.TabIndex = 31;
        Label2.BackColor = bg; Label2.Font = font12; Label2.Location = new System.Drawing.Point(160, 304); Label2.Name = "Label2"; Label2.Size = new System.Drawing.Size(89, 19); Label2.TabIndex = 25; Label2.Text = "名称(カナ)";
        txtNMK.BackColor = System.Drawing.Color.White; txtNMK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; txtNMK.Font = font12; txtNMK.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
        txtNMK.Location = new System.Drawing.Point(248, 296); txtNMK.MaxLength = 20; txtNMK.Name = "txtNMK"; txtNMK.Size = new System.Drawing.Size(249, 23); txtNMK.TabIndex = 18;
        txtNMK.KeyDown += txtNMK_KeyDown; txtNMK.Leave += txtNMK_Leave;

        LblNMKL.BackColor = bg; LblNMKL.Font = font12; LblNMKL.Location = new System.Drawing.Point(32, 336); LblNMKL.Name = "LblNMKL"; LblNMKL.Size = new System.Drawing.Size(113, 19); LblNMKL.TabIndex = 32;
        Label3.BackColor = bg; Label3.Font = font12; Label3.Location = new System.Drawing.Point(160, 336); Label3.Name = "Label3"; Label3.Size = new System.Drawing.Size(89, 19); Label3.TabIndex = 26; Label3.Text = "名称(漢字)";
        txtNMKJ.BackColor = System.Drawing.Color.White; txtNMKJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; txtNMKJ.Font = font12; txtNMKJ.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
        txtNMKJ.Location = new System.Drawing.Point(248, 328); txtNMKJ.MaxLength = 30; txtNMKJ.Name = "txtNMKJ"; txtNMKJ.Size = new System.Drawing.Size(369, 23); txtNMKJ.TabIndex = 19;
        txtNMKJ.KeyDown += txtNMKJ_KeyDown; txtNMKJ.Leave += txtNMKJ_Leave;

        LblNMS.BackColor = bg; LblNMS.Font = font12; LblNMS.Location = new System.Drawing.Point(32, 368); LblNMS.Name = "LblNMS"; LblNMS.Size = new System.Drawing.Size(113, 19); LblNMS.TabIndex = 33;
        Label4.BackColor = bg; Label4.Font = font12; Label4.Location = new System.Drawing.Point(160, 368); Label4.Name = "Label4"; Label4.Size = new System.Drawing.Size(89, 19); Label4.TabIndex = 27; Label4.Text = "名称(略称)";
        txtNMS.BackColor = System.Drawing.Color.White; txtNMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; txtNMS.Font = font12; txtNMS.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
        txtNMS.Location = new System.Drawing.Point(248, 360); txtNMS.MaxLength = 16; txtNMS.Name = "txtNMS"; txtNMS.Size = new System.Drawing.Size(153, 23); txtNMS.TabIndex = 20;
        txtNMS.KeyDown += txtNMS_KeyDown; txtNMS.Leave += txtNMS_Leave;

        // Bottom buttons
        var font14 = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdOK.BackColor = System.Drawing.SystemColors.Control; cmdOK.Font = font14; cmdOK.Location = new System.Drawing.Point(32, 408); cmdOK.Name = "cmdOK"; cmdOK.Size = new System.Drawing.Size(81, 33); cmdOK.TabIndex = 21; cmdOK.Text = "設 定"; cmdOK.UseVisualStyleBackColor = false; cmdOK.Click += cmdOK_Click;
        cmdCAN.BackColor = System.Drawing.SystemColors.Control; cmdCAN.Font = font14; cmdCAN.Location = new System.Drawing.Point(136, 408); cmdCAN.Name = "cmdCAN"; cmdCAN.Size = new System.Drawing.Size(81, 33); cmdCAN.TabIndex = 22; cmdCAN.Text = "ｷｬﾝｾﾙ"; cmdCAN.UseVisualStyleBackColor = false; cmdCAN.Click += cmdCAN_Click;
        cmdDEL.BackColor = System.Drawing.SystemColors.Control; cmdDEL.Font = font14; cmdDEL.Location = new System.Drawing.Point(240, 408); cmdDEL.Name = "cmdDEL"; cmdDEL.Size = new System.Drawing.Size(81, 33); cmdDEL.TabIndex = 23; cmdDEL.Text = "削 除"; cmdDEL.UseVisualStyleBackColor = false; cmdDEL.Click += cmdDEL_Click;
        cmdEND.BackColor = System.Drawing.SystemColors.Control; cmdEND.Font = font14; cmdEND.Location = new System.Drawing.Point(552, 408); cmdEND.Name = "cmdEND"; cmdEND.Size = new System.Drawing.Size(89, 33); cmdEND.TabIndex = 24; cmdEND.Text = "終 了"; cmdEND.UseVisualStyleBackColor = false; cmdEND.Click += cmdEND_Click;

        // FrmKUNO0110
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = bg;
        ClientSize = new System.Drawing.Size(672, 452);
        ControlBox = false;
        Controls.AddRange(new System.Windows.Forms.Control[]
        {
            Label19, lblYMD, Frame1,
            LblCD, Label1, TXTCD,
            LblNMK, Label2, txtNMK,
            LblNMKL, Label3, txtNMKJ,
            LblNMS, Label4, txtNMS,
            cmdOK, cmdCAN, cmdDEL, cmdEND
        });
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmKUNO0110";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KUNO0110";
        Load += FrmKUNO0110_Load;
        Shown += FrmKUNO0110_Shown;

        Frame1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private void ConfigureOption(
        System.Windows.Forms.RadioButton rb,
        string name,
        string text,
        int x,
        int y,
        int w,
        int tabIndex,
        int optIndex,
        System.Drawing.Font font,
        System.Drawing.Color bg)
    {
        rb.BackColor = bg;
        rb.Font = font;
        rb.Location = new System.Drawing.Point(x, y);
        rb.Name = name;
        rb.Size = new System.Drawing.Size(w, 22);
        rb.TabIndex = tabIndex;
        rb.Text = text;
        rb.Tag = optIndex;
        rb.UseVisualStyleBackColor = false;
        rb.Click += Opt_Click;
        rb.Enter += Opt_Enter;
        rb.KeyDown += Opt_KeyDown;
    }

    #endregion
}

