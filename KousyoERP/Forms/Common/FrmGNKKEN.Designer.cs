#nullable enable
namespace KousyoERP.Forms.Common;

partial class FrmGNKKEN
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.ComboBox cmbGCD = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.ComboBox cmbHSGCD = null!;
    private System.Windows.Forms.Button cmdRET = null!;
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
        Label2 = new System.Windows.Forms.Label();
        cmbGCD = new System.Windows.Forms.ComboBox();
        Label3 = new System.Windows.Forms.Label();
        cmbHSGCD = new System.Windows.Forms.ComboBox();
        cmdRET = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // Label1
        // 
        Label1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label1.ForeColor = System.Drawing.SystemColors.WindowText;
        Label1.Location = new System.Drawing.Point(160, 8);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(185, 25);
        Label1.TabIndex = 0;
        Label1.Text = "銀行コード検索";
        // 
        // lblYMD
        // 
        lblYMD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblYMD.Location = new System.Drawing.Point(400, 16);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(85, 21);
        lblYMD.TabIndex = 1;
        // 
        // Label2
        // 
        Label2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label2.ForeColor = System.Drawing.SystemColors.WindowText;
        Label2.Location = new System.Drawing.Point(16, 66);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(45, 21);
        Label2.TabIndex = 2;
        Label2.Text = "銀行";
        // 
        // cmbGCD
        // 
        cmbGCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbGCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbGCD.FormattingEnabled = true;
        cmbGCD.Location = new System.Drawing.Point(72, 64);
        cmbGCD.Name = "cmbGCD";
        cmbGCD.Size = new System.Drawing.Size(417, 25);
        cmbGCD.TabIndex = 3;
        cmbGCD.SelectedIndexChanged += cmbGCD_SelectedIndexChanged;
        // 
        // Label3
        // 
        Label3.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label3.ForeColor = System.Drawing.SystemColors.WindowText;
        Label3.Location = new System.Drawing.Point(16, 114);
        Label3.Name = "Label3";
        Label3.Size = new System.Drawing.Size(55, 21);
        Label3.TabIndex = 4;
        Label3.Text = "本支店";
        // 
        // cmbHSGCD
        // 
        cmbHSGCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbHSGCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbHSGCD.FormattingEnabled = true;
        cmbHSGCD.Location = new System.Drawing.Point(72, 112);
        cmbHSGCD.Name = "cmbHSGCD";
        cmbHSGCD.Size = new System.Drawing.Size(417, 25);
        cmbHSGCD.TabIndex = 5;
        // 
        // cmdRET
        // 
        cmdRET.BackColor = System.Drawing.SystemColors.Control;
        cmdRET.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdRET.Location = new System.Drawing.Point(24, 160);
        cmdRET.Name = "cmdRET";
        cmdRET.Size = new System.Drawing.Size(97, 33);
        cmdRET.TabIndex = 6;
        cmdRET.Text = "戻  る";
        cmdRET.UseVisualStyleBackColor = false;
        cmdRET.Click += cmdRET_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdCAN.Location = new System.Drawing.Point(144, 160);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(97, 33);
        cmdCAN.TabIndex = 7;
        cmdCAN.Text = "ｷｬﾝｾﾙ";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // FrmGNKKEN
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(501, 207);
        ControlBox = false;
        Controls.Add(cmdCAN);
        Controls.Add(cmdRET);
        Controls.Add(cmbHSGCD);
        Controls.Add(Label3);
        Controls.Add(cmbGCD);
        Controls.Add(Label2);
        Controls.Add(lblYMD);
        Controls.Add(Label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmGNKKEN";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "GNKKEN";
        Load += FrmGNKKEN_Load;
        ResumeLayout(false);
    }

    #endregion
}

