#nullable enable
namespace KousyoERP.Forms.Common;

partial class FrmGPKEN
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.ComboBox cmbGPCD = null!;
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
        cmbGPCD = new System.Windows.Forms.ComboBox();
        cmdRET = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // Label1
        // 
        Label1.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label1.ForeColor = System.Drawing.SystemColors.WindowText;
        Label1.Location = new System.Drawing.Point(120, 16);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(241, 25);
        Label1.TabIndex = 0;
        Label1.Text = "グループコード検索";
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblYMD
        // 
        lblYMD.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblYMD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblYMD.ForeColor = System.Drawing.SystemColors.WindowText;
        lblYMD.Location = new System.Drawing.Point(376, 16);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(97, 21);
        lblYMD.TabIndex = 1;
        lblYMD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // Label2
        // 
        Label2.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        Label2.ForeColor = System.Drawing.SystemColors.WindowText;
        Label2.Location = new System.Drawing.Point(32, 68);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(88, 21);
        Label2.TabIndex = 2;
        Label2.Text = "ｸﾞﾙｰﾌﾟｺｰﾄﾞ";
        // 
        // cmbGPCD
        // 
        cmbGPCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbGPCD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmbGPCD.FormattingEnabled = true;
        cmbGPCD.Location = new System.Drawing.Point(128, 64);
        cmbGPCD.Name = "cmbGPCD";
        cmbGPCD.Size = new System.Drawing.Size(345, 24);
        cmbGPCD.TabIndex = 3;
        // 
        // cmdRET
        // 
        cmdRET.BackColor = System.Drawing.SystemColors.Control;
        cmdRET.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdRET.Location = new System.Drawing.Point(32, 120);
        cmdRET.Name = "cmdRET";
        cmdRET.Size = new System.Drawing.Size(89, 33);
        cmdRET.TabIndex = 4;
        cmdRET.Text = "戻  る";
        cmdRET.UseVisualStyleBackColor = false;
        cmdRET.Click += cmdRET_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        cmdCAN.Location = new System.Drawing.Point(144, 120);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(89, 33);
        cmdCAN.TabIndex = 5;
        cmdCAN.Text = "ｷｬﾝｾﾙ";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // FrmGPKEN
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(508, 176);
        ControlBox = false;
        Controls.Add(cmdCAN);
        Controls.Add(cmdRET);
        Controls.Add(cmbGPCD);
        Controls.Add(Label2);
        Controls.Add(lblYMD);
        Controls.Add(Label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmGPKEN";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "GPKEN";
        Load += FrmGPKEN_Load;
        ResumeLayout(false);
    }

    #endregion
}

