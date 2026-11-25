#nullable enable
namespace KousyoERP.Forms.Master;

partial class FrmKUNO0160
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label19 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.TextBox txtTACD = null!;
    private System.Windows.Forms.Label lblTACD = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.TextBox txtTANMK = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.TextBox txtTANMKJ = null!;
    private System.Windows.Forms.Label Label4 = null!;
    private System.Windows.Forms.TextBox txtTANMS = null!;
    private System.Windows.Forms.Label Label5 = null!;
    private System.Windows.Forms.TextBox txtBUCD = null!;
    private System.Windows.Forms.Label lblBUCD = null!;
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
        txtTACD = new System.Windows.Forms.TextBox();
        lblTACD = new System.Windows.Forms.Label();
        Label2 = new System.Windows.Forms.Label();
        txtTANMK = new System.Windows.Forms.TextBox();
        Label3 = new System.Windows.Forms.Label();
        txtTANMKJ = new System.Windows.Forms.TextBox();
        Label4 = new System.Windows.Forms.Label();
        txtTANMS = new System.Windows.Forms.TextBox();
        Label5 = new System.Windows.Forms.Label();
        txtBUCD = new System.Windows.Forms.TextBox();
        lblBUCD = new System.Windows.Forms.Label();
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
        Label19.Location = new System.Drawing.Point(200, 8);
        Label19.Name = "Label19";
        Label19.Size = new System.Drawing.Size(178, 24);
        Label19.TabIndex = 15;
        Label19.Text = "担当マスタ登録";
        // 
        // lblYMD
        // 
        lblYMD.AutoSize = true;
        lblYMD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblYMD.Location = new System.Drawing.Point(464, 8);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(0, 19);
        lblYMD.TabIndex = 10;
        // 
        // Label1
        // 
        Label1.AutoSize = true;
        Label1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label1.ForeColor = System.Drawing.SystemColors.WindowText;
        Label1.Location = new System.Drawing.Point(40, 72);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(78, 16);
        Label1.TabIndex = 9;
        Label1.Text = "担当コード";
        // 
        // txtTACD
        // 
        txtTACD.BackColor = System.Drawing.Color.White;
        txtTACD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtTACD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtTACD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtTACD.Location = new System.Drawing.Point(176, 64);
        txtTACD.MaxLength = 4;
        txtTACD.Name = "txtTACD";
        txtTACD.Size = new System.Drawing.Size(41, 23);
        txtTACD.TabIndex = 0;
        txtTACD.Enter += txtTACD_GotFocus;
        txtTACD.KeyDown += txtTACD_KeyDown;
        txtTACD.KeyPress += DigitsOnly_KeyPress;
        txtTACD.Leave += txtTACD_LostFocus;
        // 
        // lblTACD
        // 
        lblTACD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblTACD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblTACD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblTACD.Location = new System.Drawing.Point(232, 68);
        lblTACD.Name = "lblTACD";
        lblTACD.Size = new System.Drawing.Size(217, 20);
        lblTACD.TabIndex = 16;
        // 
        // Label2
        // 
        Label2.AutoSize = true;
        Label2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label2.ForeColor = System.Drawing.SystemColors.WindowText;
        Label2.Location = new System.Drawing.Point(40, 112);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(97, 16);
        Label2.TabIndex = 11;
        Label2.Text = "担当名(カナ)";
        // 
        // txtTANMK
        // 
        txtTANMK.BackColor = System.Drawing.Color.White;
        txtTANMK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtTANMK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtTANMK.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
        txtTANMK.Location = new System.Drawing.Point(176, 104);
        txtTANMK.MaxLength = 20;
        txtTANMK.Name = "txtTANMK";
        txtTANMK.Size = new System.Drawing.Size(249, 23);
        txtTANMK.TabIndex = 1;
        txtTANMK.KeyDown += txtTANMK_KeyDown;
        txtTANMK.Leave += txtTANMK_LostFocus;
        // 
        // Label3
        // 
        Label3.AutoSize = true;
        Label3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label3.ForeColor = System.Drawing.SystemColors.WindowText;
        Label3.Location = new System.Drawing.Point(40, 152);
        Label3.Name = "Label3";
        Label3.Size = new System.Drawing.Size(101, 16);
        Label3.TabIndex = 12;
        Label3.Text = "担当名(漢字)";
        // 
        // txtTANMKJ
        // 
        txtTANMKJ.BackColor = System.Drawing.Color.White;
        txtTANMKJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtTANMKJ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtTANMKJ.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
        txtTANMKJ.Location = new System.Drawing.Point(176, 144);
        txtTANMKJ.MaxLength = 30;
        txtTANMKJ.Name = "txtTANMKJ";
        txtTANMKJ.Size = new System.Drawing.Size(369, 23);
        txtTANMKJ.TabIndex = 2;
        txtTANMKJ.KeyDown += txtTANMKJ_KeyDown;
        txtTANMKJ.Leave += txtTANMKJ_LostFocus;
        // 
        // Label4
        // 
        Label4.AutoSize = true;
        Label4.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label4.ForeColor = System.Drawing.SystemColors.WindowText;
        Label4.Location = new System.Drawing.Point(40, 192);
        Label4.Name = "Label4";
        Label4.Size = new System.Drawing.Size(101, 16);
        Label4.TabIndex = 13;
        Label4.Text = "担当名(略称)";
        // 
        // txtTANMS
        // 
        txtTANMS.BackColor = System.Drawing.Color.White;
        txtTANMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtTANMS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtTANMS.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
        txtTANMS.Location = new System.Drawing.Point(176, 185);
        txtTANMS.MaxLength = 16;
        txtTANMS.Name = "txtTANMS";
        txtTANMS.Size = new System.Drawing.Size(193, 23);
        txtTANMS.TabIndex = 3;
        txtTANMS.KeyDown += txtTANMS_KeyDown;
        txtTANMS.Leave += txtTANMS_LostFocus;
        // 
        // Label5
        // 
        Label5.AutoSize = true;
        Label5.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label5.ForeColor = System.Drawing.SystemColors.WindowText;
        Label5.Location = new System.Drawing.Point(40, 232);
        Label5.Name = "Label5";
        Label5.Size = new System.Drawing.Size(78, 16);
        Label5.TabIndex = 14;
        Label5.Text = "部門コード";
        // 
        // txtBUCD
        // 
        txtBUCD.BackColor = System.Drawing.Color.White;
        txtBUCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtBUCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        txtBUCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
        txtBUCD.Location = new System.Drawing.Point(176, 224);
        txtBUCD.MaxLength = 2;
        txtBUCD.Name = "txtBUCD";
        txtBUCD.Size = new System.Drawing.Size(25, 23);
        txtBUCD.TabIndex = 4;
        txtBUCD.KeyDown += txtBUCD_KeyDown;
        txtBUCD.KeyPress += DigitsOnly_KeyPress;
        txtBUCD.Leave += txtBUCD_LostFocus;
        // 
        // lblBUCD
        // 
        lblBUCD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblBUCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblBUCD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblBUCD.Location = new System.Drawing.Point(232, 228);
        lblBUCD.Name = "lblBUCD";
        lblBUCD.Size = new System.Drawing.Size(217, 20);
        lblBUCD.TabIndex = 17;
        // 
        // cmdOK
        // 
        cmdOK.BackColor = System.Drawing.SystemColors.Control;
        cmdOK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdOK.Location = new System.Drawing.Point(40, 296);
        cmdOK.Name = "cmdOK";
        cmdOK.Size = new System.Drawing.Size(81, 33);
        cmdOK.TabIndex = 5;
        cmdOK.Text = "設 定";
        cmdOK.UseVisualStyleBackColor = false;
        cmdOK.Click += cmdOK_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.CausesValidation = false;
        cmdCAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdCAN.Location = new System.Drawing.Point(144, 296);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(81, 33);
        cmdCAN.TabIndex = 6;
        cmdCAN.Text = "ｷｬﾝｾﾙ";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // cmdDEL
        // 
        cmdDEL.BackColor = System.Drawing.SystemColors.Control;
        cmdDEL.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdDEL.Location = new System.Drawing.Point(248, 296);
        cmdDEL.Name = "cmdDEL";
        cmdDEL.Size = new System.Drawing.Size(81, 33);
        cmdDEL.TabIndex = 7;
        cmdDEL.Text = "削 除";
        cmdDEL.UseVisualStyleBackColor = false;
        cmdDEL.Click += cmdDEL_Click;
        // 
        // cmdEND
        // 
        cmdEND.BackColor = System.Drawing.SystemColors.Control;
        cmdEND.CausesValidation = false;
        cmdEND.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdEND.Location = new System.Drawing.Point(440, 296);
        cmdEND.Name = "cmdEND";
        cmdEND.Size = new System.Drawing.Size(89, 33);
        cmdEND.TabIndex = 8;
        cmdEND.Text = "終 了";
        cmdEND.UseVisualStyleBackColor = false;
        cmdEND.Click += cmdEND_Click;
        // 
        // FrmKUNO0160
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(580, 341);
        ControlBox = false;
        Controls.Add(txtBUCD);
        Controls.Add(txtTANMS);
        Controls.Add(txtTANMKJ);
        Controls.Add(txtTANMK);
        Controls.Add(txtTACD);
        Controls.Add(cmdEND);
        Controls.Add(cmdCAN);
        Controls.Add(cmdOK);
        Controls.Add(cmdDEL);
        Controls.Add(lblBUCD);
        Controls.Add(lblTACD);
        Controls.Add(Label19);
        Controls.Add(Label5);
        Controls.Add(Label4);
        Controls.Add(Label3);
        Controls.Add(Label2);
        Controls.Add(Label1);
        Controls.Add(lblYMD);
        Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmKUNO0160";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KUNO0160";
        Load += FrmKUNO0160_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

