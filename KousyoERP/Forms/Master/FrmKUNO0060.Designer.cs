#nullable enable
namespace KousyoERP.Forms.Master;

partial class FrmKUNO0060
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label19 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.TextBox txtSYCD = null!;
    private System.Windows.Forms.Label Label7 = null!;
    private System.Windows.Forms.TextBox txtkubun = null!;
    private System.Windows.Forms.Label lblkubun = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.TextBox txtSYNMK = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.TextBox txtSYNMKJ = null!;
    private System.Windows.Forms.Label Label4 = null!;
    private System.Windows.Forms.TextBox txtSYNMS = null!;
    private System.Windows.Forms.Label Label5 = null!;
    private System.Windows.Forms.TextBox txtBLK = null!;
    private System.Windows.Forms.Label lblblk = null!;
    private System.Windows.Forms.Label Label6 = null!;
    private System.Windows.Forms.TextBox TxtSKAN = null!;
    private System.Windows.Forms.Label lblskan = null!;
    private System.Windows.Forms.Label Label8 = null!;
    private System.Windows.Forms.TextBox txtGCD = null!;
    private System.Windows.Forms.Label lblGCD = null!;
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
        Label19 = new System.Windows.Forms.Label();
        lblYMD = new System.Windows.Forms.Label();
        Label1 = new System.Windows.Forms.Label();
        txtSYCD = new System.Windows.Forms.TextBox();
        Label7 = new System.Windows.Forms.Label();
        txtkubun = new System.Windows.Forms.TextBox();
        lblkubun = new System.Windows.Forms.Label();
        Label2 = new System.Windows.Forms.Label();
        txtSYNMK = new System.Windows.Forms.TextBox();
        Label3 = new System.Windows.Forms.Label();
        txtSYNMKJ = new System.Windows.Forms.TextBox();
        Label4 = new System.Windows.Forms.Label();
        txtSYNMS = new System.Windows.Forms.TextBox();
        Label5 = new System.Windows.Forms.Label();
        txtBLK = new System.Windows.Forms.TextBox();
        lblblk = new System.Windows.Forms.Label();
        Label6 = new System.Windows.Forms.Label();
        TxtSKAN = new System.Windows.Forms.TextBox();
        lblskan = new System.Windows.Forms.Label();
        Label8 = new System.Windows.Forms.Label();
        txtGCD = new System.Windows.Forms.TextBox();
        lblGCD = new System.Windows.Forms.Label();
        cmdOK = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();
        cmdDEL = new System.Windows.Forms.Button();
        cmdEND = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // Label19
        // 
        Label19.AutoSize = true;
        Label19.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label19.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label19.ForeColor = System.Drawing.SystemColors.WindowText;
        Label19.Location = new System.Drawing.Point(224, 8);
        Label19.Name = "Label19";
        Label19.Size = new System.Drawing.Size(178, 24);
        Label19.TabIndex = 13;
        Label19.Text = "所属マスタ登録";
        // 
        // lblYMD
        // 
        lblYMD.AutoSize = true;
        lblYMD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblYMD.Location = new System.Drawing.Point(480, 8);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(0, 19);
        lblYMD.TabIndex = 0;
        // 
        // Label1
        // 
        Label1.AutoSize = true;
        Label1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label1.ForeColor = System.Drawing.SystemColors.WindowText;
        Label1.Location = new System.Drawing.Point(40, 48);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(78, 16);
        Label1.TabIndex = 14;
        Label1.Text = "所属コード";
        // 
        // txtSYCD
        // 
        txtSYCD.BackColor = System.Drawing.Color.White;
        txtSYCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSYCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtSYCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtSYCD.Location = new System.Drawing.Point(192, 40);
        txtSYCD.MaxLength = 3;
        txtSYCD.Name = "txtSYCD";
        txtSYCD.Size = new System.Drawing.Size(41, 23);
        txtSYCD.TabIndex = 1;
        txtSYCD.Enter += txtSYCD_GotFocus;
        txtSYCD.KeyDown += txtSYCD_KeyDown;
        txtSYCD.KeyPress += DigitsOnly_KeyPress;
        txtSYCD.Leave += txtSYCD_LostFocus;
        // 
        // Label7
        // 
        Label7.AutoSize = true;
        Label7.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label7.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label7.ForeColor = System.Drawing.SystemColors.WindowText;
        Label7.Location = new System.Drawing.Point(40, 88);
        Label7.Name = "Label7";
        Label7.Size = new System.Drawing.Size(71, 16);
        Label7.TabIndex = 23;
        Label7.Text = "所属区分";
        // 
        // txtkubun
        // 
        txtkubun.BackColor = System.Drawing.Color.White;
        txtkubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtkubun.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtkubun.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtkubun.Location = new System.Drawing.Point(192, 80);
        txtkubun.MaxLength = 1;
        txtkubun.Name = "txtkubun";
        txtkubun.Size = new System.Drawing.Size(41, 23);
        txtkubun.TabIndex = 2;
        txtkubun.KeyDown += txtkubun_KeyDown;
        txtkubun.KeyPress += DigitsOnly_KeyPress;
        txtkubun.Leave += txtkubun_LostFocus;
        // 
        // lblkubun
        // 
        lblkubun.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblkubun.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblkubun.ForeColor = System.Drawing.SystemColors.WindowText;
        lblkubun.Location = new System.Drawing.Point(256, 84);
        lblkubun.Name = "lblkubun";
        lblkubun.Size = new System.Drawing.Size(201, 20);
        lblkubun.TabIndex = 24;
        // 
        // Label2
        // 
        Label2.AutoSize = true;
        Label2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label2.ForeColor = System.Drawing.SystemColors.WindowText;
        Label2.Location = new System.Drawing.Point(40, 128);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(113, 16);
        Label2.TabIndex = 15;
        Label2.Text = "所属名称(カナ)";
        // 
        // txtSYNMK
        // 
        txtSYNMK.BackColor = System.Drawing.Color.White;
        txtSYNMK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSYNMK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtSYNMK.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
        txtSYNMK.Location = new System.Drawing.Point(192, 120);
        txtSYNMK.MaxLength = 20;
        txtSYNMK.Name = "txtSYNMK";
        txtSYNMK.Size = new System.Drawing.Size(249, 23);
        txtSYNMK.TabIndex = 3;
        txtSYNMK.KeyDown += txtSYNMK_KeyDown;
        txtSYNMK.Leave += txtSYNMK_LostFocus;
        // 
        // Label3
        // 
        Label3.AutoSize = true;
        Label3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label3.ForeColor = System.Drawing.SystemColors.WindowText;
        Label3.Location = new System.Drawing.Point(40, 168);
        Label3.Name = "Label3";
        Label3.Size = new System.Drawing.Size(117, 16);
        Label3.TabIndex = 16;
        Label3.Text = "所属名称(漢字)";
        // 
        // txtSYNMKJ
        // 
        txtSYNMKJ.BackColor = System.Drawing.Color.White;
        txtSYNMKJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSYNMKJ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtSYNMKJ.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
        txtSYNMKJ.Location = new System.Drawing.Point(192, 160);
        txtSYNMKJ.MaxLength = 30;
        txtSYNMKJ.Name = "txtSYNMKJ";
        txtSYNMKJ.Size = new System.Drawing.Size(369, 23);
        txtSYNMKJ.TabIndex = 4;
        txtSYNMKJ.KeyDown += txtSYNMKJ_KeyDown;
        txtSYNMKJ.Leave += txtSYNMKJ_LostFocus;
        // 
        // Label4
        // 
        Label4.AutoSize = true;
        Label4.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label4.ForeColor = System.Drawing.SystemColors.WindowText;
        Label4.Location = new System.Drawing.Point(40, 208);
        Label4.Name = "Label4";
        Label4.Size = new System.Drawing.Size(71, 16);
        Label4.TabIndex = 17;
        Label4.Text = "所属略称";
        // 
        // txtSYNMS
        // 
        txtSYNMS.BackColor = System.Drawing.Color.White;
        txtSYNMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtSYNMS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtSYNMS.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
        txtSYNMS.Location = new System.Drawing.Point(192, 200);
        txtSYNMS.MaxLength = 16;
        txtSYNMS.Name = "txtSYNMS";
        txtSYNMS.Size = new System.Drawing.Size(201, 23);
        txtSYNMS.TabIndex = 5;
        txtSYNMS.KeyDown += txtSYNMS_KeyDown;
        txtSYNMS.Leave += txtSYNMS_LostFocus;
        // 
        // Label5
        // 
        Label5.AutoSize = true;
        Label5.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label5.ForeColor = System.Drawing.SystemColors.WindowText;
        Label5.Location = new System.Drawing.Point(40, 248);
        Label5.Name = "Label5";
        Label5.Size = new System.Drawing.Size(58, 16);
        Label5.TabIndex = 18;
        Label5.Text = "ブロック";
        // 
        // txtBLK
        // 
        txtBLK.BackColor = System.Drawing.Color.White;
        txtBLK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtBLK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtBLK.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtBLK.Location = new System.Drawing.Point(192, 240);
        txtBLK.MaxLength = 1;
        txtBLK.Name = "txtBLK";
        txtBLK.Size = new System.Drawing.Size(25, 23);
        txtBLK.TabIndex = 6;
        txtBLK.KeyDown += txtBLK_KeyDown;
        txtBLK.KeyPress += DigitsOnly_KeyPress;
        txtBLK.Leave += txtBLK_LostFocus;
        // 
        // lblblk
        // 
        lblblk.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblblk.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblblk.ForeColor = System.Drawing.SystemColors.WindowText;
        lblblk.Location = new System.Drawing.Point(240, 244);
        lblblk.Name = "lblblk";
        lblblk.Size = new System.Drawing.Size(193, 20);
        lblblk.TabIndex = 19;
        // 
        // Label6
        // 
        Label6.AutoSize = true;
        Label6.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label6.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label6.ForeColor = System.Drawing.SystemColors.WindowText;
        Label6.Location = new System.Drawing.Point(40, 288);
        Label6.Name = "Label6";
        Label6.Size = new System.Drawing.Size(39, 16);
        Label6.TabIndex = 20;
        Label6.Text = "主管";
        // 
        // TxtSKAN
        // 
        TxtSKAN.BackColor = System.Drawing.Color.White;
        TxtSKAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        TxtSKAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        TxtSKAN.ImeMode = System.Windows.Forms.ImeMode.Disable;
        TxtSKAN.Location = new System.Drawing.Point(192, 280);
        TxtSKAN.MaxLength = 2;
        TxtSKAN.Name = "TxtSKAN";
        TxtSKAN.Size = new System.Drawing.Size(25, 23);
        TxtSKAN.TabIndex = 7;
        TxtSKAN.KeyDown += TxtSKAN_KeyDown;
        TxtSKAN.KeyPress += DigitsOnly_KeyPress;
        TxtSKAN.Leave += TxtSKAN_LostFocus;
        // 
        // lblskan
        // 
        lblskan.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblskan.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblskan.ForeColor = System.Drawing.SystemColors.WindowText;
        lblskan.Location = new System.Drawing.Point(240, 284);
        lblskan.Name = "lblskan";
        lblskan.Size = new System.Drawing.Size(193, 20);
        lblskan.TabIndex = 21;
        // 
        // Label8
        // 
        Label8.AutoSize = true;
        Label8.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label8.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label8.ForeColor = System.Drawing.SystemColors.WindowText;
        Label8.Location = new System.Drawing.Point(40, 328);
        Label8.Name = "Label8";
        Label8.Size = new System.Drawing.Size(102, 16);
        Label8.TabIndex = 22;
        Label8.Text = "ｸﾞﾙｰﾌﾟ会社ｺｰﾄﾞ";
        // 
        // txtGCD
        // 
        txtGCD.BackColor = System.Drawing.Color.White;
        txtGCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtGCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtGCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtGCD.Location = new System.Drawing.Point(192, 320);
        txtGCD.MaxLength = 3;
        txtGCD.Name = "txtGCD";
        txtGCD.Size = new System.Drawing.Size(33, 23);
        txtGCD.TabIndex = 8;
        txtGCD.KeyDown += txtGCD_KeyDown;
        txtGCD.KeyPress += DigitsOnly_KeyPress;
        txtGCD.Leave += txtGCD_LostFocus;
        // 
        // lblGCD
        // 
        lblGCD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblGCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblGCD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblGCD.Location = new System.Drawing.Point(240, 324);
        lblGCD.Name = "lblGCD";
        lblGCD.Size = new System.Drawing.Size(209, 20);
        lblGCD.TabIndex = 25;
        // 
        // cmdOK
        // 
        cmdOK.BackColor = System.Drawing.SystemColors.Control;
        cmdOK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdOK.Location = new System.Drawing.Point(32, 376);
        cmdOK.Name = "cmdOK";
        cmdOK.Size = new System.Drawing.Size(81, 33);
        cmdOK.TabIndex = 9;
        cmdOK.Text = "設 定";
        cmdOK.UseVisualStyleBackColor = false;
        cmdOK.Click += cmdOK_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.CausesValidation = false;
        cmdCAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdCAN.Location = new System.Drawing.Point(136, 376);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(81, 33);
        cmdCAN.TabIndex = 10;
        cmdCAN.Text = "ｷｬﾝｾﾙ";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // cmdDEL
        // 
        cmdDEL.BackColor = System.Drawing.SystemColors.Control;
        cmdDEL.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdDEL.Location = new System.Drawing.Point(248, 376);
        cmdDEL.Name = "cmdDEL";
        cmdDEL.Size = new System.Drawing.Size(81, 33);
        cmdDEL.TabIndex = 11;
        cmdDEL.Text = "削 除";
        cmdDEL.UseVisualStyleBackColor = false;
        cmdDEL.Click += cmdDEL_Click;
        // 
        // cmdEND
        // 
        cmdEND.BackColor = System.Drawing.SystemColors.Control;
        cmdEND.CausesValidation = false;
        cmdEND.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdEND.Location = new System.Drawing.Point(464, 376);
        cmdEND.Name = "cmdEND";
        cmdEND.Size = new System.Drawing.Size(89, 33);
        cmdEND.TabIndex = 12;
        cmdEND.Text = "終 了";
        cmdEND.UseVisualStyleBackColor = false;
        cmdEND.Click += cmdEND_Click;
        // 
        // FrmKUNO0060
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(604, 421);
        ControlBox = false;
        Controls.Add(txtkubun);
        Controls.Add(cmdEND);
        Controls.Add(cmdDEL);
        Controls.Add(cmdCAN);
        Controls.Add(cmdOK);
        Controls.Add(txtGCD);
        Controls.Add(TxtSKAN);
        Controls.Add(txtBLK);
        Controls.Add(txtSYNMS);
        Controls.Add(txtSYNMKJ);
        Controls.Add(txtSYNMK);
        Controls.Add(txtSYCD);
        Controls.Add(lblGCD);
        Controls.Add(lblkubun);
        Controls.Add(Label7);
        Controls.Add(Label8);
        Controls.Add(lblskan);
        Controls.Add(Label6);
        Controls.Add(lblblk);
        Controls.Add(Label5);
        Controls.Add(Label4);
        Controls.Add(Label3);
        Controls.Add(Label2);
        Controls.Add(Label1);
        Controls.Add(Label19);
        Controls.Add(lblYMD);
        Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmKUNO0060";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KUNO0060";
        Load += FrmKUNO0060_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

