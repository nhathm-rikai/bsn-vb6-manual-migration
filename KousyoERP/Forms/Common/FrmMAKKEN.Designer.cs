#nullable enable
namespace KousyoERP.Forms.Common;

partial class FrmMAKKEN
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.ComboBox cmbMCD = null!;
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
        Label1 = new Label();
        Label2 = new Label();
        lblYMD = new Label();
        cmbMCD = new ComboBox();
        cmdRET = new Button();
        cmdCAN = new Button();
        SuspendLayout();
        // 
        // Label1
        // 
        Label1.AutoSize = true;
        Label1.BackColor = Color.FromArgb(192, 192, 192);
        Label1.Font = new Font("MS Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
        Label1.ForeColor = SystemColors.WindowText;
        Label1.Location = new Point(96, 8);
        Label1.Name = "Label1";
        Label1.Size = new Size(225, 24);
        Label1.TabIndex = 0;
        Label1.Text = "メーカーコード検索";
        // 
        // Label2
        // 
        Label2.AutoSize = true;
        Label2.BackColor = Color.FromArgb(192, 192, 192);
        Label2.Font = new Font("MS PGothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
        Label2.ForeColor = SystemColors.WindowText;
        Label2.Location = new Point(16, 56);
        Label2.Name = "Label2";
        Label2.Size = new Size(65, 16);
        Label2.TabIndex = 1;
        Label2.Text = "ﾒｰｶｰｺｰﾄﾞ";
        // 
        // lblYMD
        // 
        lblYMD.AutoSize = true;
        lblYMD.BackColor = Color.FromArgb(192, 192, 192);
        lblYMD.Font = new Font("MS PGothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
        lblYMD.ForeColor = SystemColors.WindowText;
        lblYMD.Location = new Point(344, 16);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new Size(0, 19);
        lblYMD.TabIndex = 5;
        // 
        // cmbMCD
        // 
        cmbMCD.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbMCD.Font = new Font("MS PGothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
        cmbMCD.FormattingEnabled = true;
        cmbMCD.Location = new Point(88, 56);
        cmbMCD.Name = "cmbMCD";
        cmbMCD.Size = new Size(321, 24);
        cmbMCD.TabIndex = 2;
        // 
        // cmdRET
        // 
        cmdRET.BackColor = SystemColors.Control;
        cmdRET.Font = new Font("MS PGothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
        cmdRET.Location = new Point(32, 112);
        cmdRET.Name = "cmdRET";
        cmdRET.Size = new Size(89, 33);
        cmdRET.TabIndex = 3;
        cmdRET.Text = "戻  る";
        cmdRET.UseVisualStyleBackColor = false;
        cmdRET.Click += cmdRET_Click;
        // 
        // cmdCAN
        // 
        cmdCAN.BackColor = SystemColors.Control;
        cmdCAN.CausesValidation = false;
        cmdCAN.Font = new Font("MS PGothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
        cmdCAN.Location = new Point(144, 112);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new Size(89, 33);
        cmdCAN.TabIndex = 4;
        cmdCAN.Text = "ｷｬﾝｾﾙ";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;
        // 
        // FrmMAKKEN
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(192, 192, 192);
        ClientSize = new Size(439, 157);
        ControlBox = false;
        Controls.Add(cmdCAN);
        Controls.Add(cmdRET);
        Controls.Add(cmbMCD);
        Controls.Add(lblYMD);
        Controls.Add(Label2);
        Controls.Add(Label1);
        Font = new Font("MS PGothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmMAKKEN";
        StartPosition = FormStartPosition.CenterParent;
        Text = "MAKKEN";
        Load += FrmMAKKEN_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

