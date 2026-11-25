#nullable enable
namespace KousyoERP.Forms;

partial class FrmMainMenu
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label lblTitle = null!;
    private System.Windows.Forms.Label lblDate = null!;
    private System.Windows.Forms.GroupBox grpMaster = null!;
    private System.Windows.Forms.Button btnKUNO0020 = null!;
    private System.Windows.Forms.Button btnKUNO0010 = null!;
    private System.Windows.Forms.Button btnKUNO0060 = null!;
    private System.Windows.Forms.Button btnKUNO0160 = null!;
    private System.Windows.Forms.Button btnKUNO0040 = null!;
    private System.Windows.Forms.Button btnKUNO0110 = null!;
    private System.Windows.Forms.GroupBox grpZaiko = null!;
    private System.Windows.Forms.Button btnKZKO0030 = null!;
    private System.Windows.Forms.Button btnKSHO0115 = null!;
    private System.Windows.Forms.Button btnKZKO0020 = null!;
    private System.Windows.Forms.Button btnKZKO0010 = null!;
    private System.Windows.Forms.Button btnKTBO0020 = null!;
    private System.Windows.Forms.Button btnExit = null!;

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
        lblTitle = new System.Windows.Forms.Label();
        lblDate = new System.Windows.Forms.Label();
        grpMaster = new System.Windows.Forms.GroupBox();
        btnKUNO0020 = new System.Windows.Forms.Button();
        btnKUNO0010 = new System.Windows.Forms.Button();
        btnKUNO0060 = new System.Windows.Forms.Button();
        btnKUNO0160 = new System.Windows.Forms.Button();
        btnKUNO0040 = new System.Windows.Forms.Button();
        btnKUNO0110 = new System.Windows.Forms.Button();
        grpZaiko = new System.Windows.Forms.GroupBox();
        btnKZKO0030 = new System.Windows.Forms.Button();
        btnKSHO0115 = new System.Windows.Forms.Button();
        btnKZKO0020 = new System.Windows.Forms.Button();
        btnKZKO0010 = new System.Windows.Forms.Button();
        btnKTBO0020 = new System.Windows.Forms.Button();
        btnExit = new System.Windows.Forms.Button();
        grpMaster.SuspendLayout();
        grpZaiko.SuspendLayout();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblTitle.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblTitle.Location = new System.Drawing.Point(125, 16);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new System.Drawing.Size(270, 24);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "販売・在庫管理メニュー";
        // 
        // lblDate
        // 
        lblDate.AutoSize = true;
        lblDate.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        lblDate.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        lblDate.Location = new System.Drawing.Point(425, 18);
        lblDate.Name = "lblDate";
        lblDate.Size = new System.Drawing.Size(0, 19);
        lblDate.TabIndex = 1;
        // 
        // grpMaster
        // 
        grpMaster.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        grpMaster.Controls.Add(btnKUNO0020);
        grpMaster.Controls.Add(btnKUNO0010);
        grpMaster.Controls.Add(btnKUNO0060);
        grpMaster.Controls.Add(btnKUNO0160);
        grpMaster.Controls.Add(btnKUNO0040);
        grpMaster.Controls.Add(btnKUNO0110);
        grpMaster.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        grpMaster.Location = new System.Drawing.Point(48, 56);
        grpMaster.Name = "grpMaster";
        grpMaster.Size = new System.Drawing.Size(444, 290);
        grpMaster.TabIndex = 2;
        grpMaster.TabStop = false;
        grpMaster.Text = "マスタ保守";
        // 
        // btnKUNO0020
        // 
        btnKUNO0020.BackColor = System.Drawing.SystemColors.Control;
        btnKUNO0020.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKUNO0020.Location = new System.Drawing.Point(32, 26);
        btnKUNO0020.Name = "btnKUNO0020";
        btnKUNO0020.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKUNO0020.Size = new System.Drawing.Size(380, 36);
        btnKUNO0020.TabIndex = 0;
        btnKUNO0020.Text = "1. 商品分類マスタ登録 (KUNO0020)";
        btnKUNO0020.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKUNO0020.UseVisualStyleBackColor = false;
        btnKUNO0020.Click += BtnKUNO0020_Click;
        // 
        // btnKUNO0010
        // 
        btnKUNO0010.BackColor = System.Drawing.SystemColors.Control;
        btnKUNO0010.Enabled = true;
        btnKUNO0010.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKUNO0010.Location = new System.Drawing.Point(32, 68);
        btnKUNO0010.Name = "btnKUNO0010";
        btnKUNO0010.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKUNO0010.Size = new System.Drawing.Size(380, 36);
        btnKUNO0010.TabIndex = 1;
        btnKUNO0010.Text = "2. 商品マスタ登録 (KUNO0010)";
        btnKUNO0010.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKUNO0010.UseVisualStyleBackColor = false;
        btnKUNO0010.Click += BtnKUNO0010_Click;
        // 
        // btnKUNO0060
        // 
        btnKUNO0060.BackColor = System.Drawing.SystemColors.Control;
        btnKUNO0060.Enabled = true;
        btnKUNO0060.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKUNO0060.Location = new System.Drawing.Point(32, 110);
        btnKUNO0060.Name = "btnKUNO0060";
        btnKUNO0060.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKUNO0060.Size = new System.Drawing.Size(380, 36);
        btnKUNO0060.TabIndex = 2;
        btnKUNO0060.Text = "3. 所属マスタ登録 (KUNO0060)";
        btnKUNO0060.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKUNO0060.UseVisualStyleBackColor = false;
        btnKUNO0060.Click += BtnKUNO0060_Click;
        // 
        // btnKUNO0160
        // 
        btnKUNO0160.BackColor = System.Drawing.SystemColors.Control;
        btnKUNO0160.Enabled = true;
        btnKUNO0160.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKUNO0160.Location = new System.Drawing.Point(32, 152);
        btnKUNO0160.Name = "btnKUNO0160";
        btnKUNO0160.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKUNO0160.Size = new System.Drawing.Size(380, 36);
        btnKUNO0160.TabIndex = 3;
        btnKUNO0160.Text = "4. 担当マスタ登録 (KUNO0160)";
        btnKUNO0160.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKUNO0160.UseVisualStyleBackColor = false;
        btnKUNO0160.Click += BtnKUNO0160_Click;
        // 
        // btnKUNO0040
        // 
        btnKUNO0040.BackColor = System.Drawing.SystemColors.Control;
        btnKUNO0040.Enabled = true;
        btnKUNO0040.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKUNO0040.Location = new System.Drawing.Point(32, 194);
        btnKUNO0040.Name = "btnKUNO0040";
        btnKUNO0040.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKUNO0040.Size = new System.Drawing.Size(380, 36);
        btnKUNO0040.TabIndex = 4;
        btnKUNO0040.Text = "5. 顧客マスタ登録 (KUNO0040)";
        btnKUNO0040.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKUNO0040.UseVisualStyleBackColor = false;
        btnKUNO0040.Click += BtnKUNO0040_Click;
        // 
        // btnKUNO0110
        // 
        btnKUNO0110.BackColor = System.Drawing.SystemColors.Control;
        btnKUNO0110.Enabled = true;
        btnKUNO0110.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKUNO0110.Location = new System.Drawing.Point(32, 236);
        btnKUNO0110.Name = "btnKUNO0110";
        btnKUNO0110.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKUNO0110.Size = new System.Drawing.Size(380, 36);
        btnKUNO0110.TabIndex = 5;
        btnKUNO0110.Text = "6. 名称マスタ登録 (KUNO0110)";
        btnKUNO0110.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKUNO0110.UseVisualStyleBackColor = false;
        btnKUNO0110.Click += BtnKUNO0110_Click;
        // 
        // grpZaiko
        // 
        grpZaiko.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        grpZaiko.Controls.Add(btnKZKO0030);
        grpZaiko.Controls.Add(btnKSHO0115);
        grpZaiko.Controls.Add(btnKZKO0020);
        grpZaiko.Controls.Add(btnKZKO0010);
        grpZaiko.Controls.Add(btnKTBO0020);
        grpZaiko.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        grpZaiko.Location = new System.Drawing.Point(48, 358);
        grpZaiko.Name = "grpZaiko";
        grpZaiko.Size = new System.Drawing.Size(444, 248);
        grpZaiko.TabIndex = 3;
        grpZaiko.TabStop = false;
        grpZaiko.Text = "在庫・照会・出力";
        // 
        // btnKZKO0030
        // 
        btnKZKO0030.BackColor = System.Drawing.SystemColors.Control;
        btnKZKO0030.Enabled = true;
        btnKZKO0030.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKZKO0030.Location = new System.Drawing.Point(32, 26);
        btnKZKO0030.Name = "btnKZKO0030";
        btnKZKO0030.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKZKO0030.Size = new System.Drawing.Size(380, 36);
        btnKZKO0030.TabIndex = 0;
        btnKZKO0030.Text = "5. 在庫履歴照会 (KZKO0030)";
        btnKZKO0030.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKZKO0030.UseVisualStyleBackColor = false;
        btnKZKO0030.Click += BtnKZKO0030_Click;
        // 
        // btnKSHO0115
        // 
        btnKSHO0115.BackColor = System.Drawing.SystemColors.Control;
        btnKSHO0115.Enabled = true;
        btnKSHO0115.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKSHO0115.Location = new System.Drawing.Point(32, 68);
        btnKSHO0115.Name = "btnKSHO0115";
        btnKSHO0115.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKSHO0115.Size = new System.Drawing.Size(380, 36);
        btnKSHO0115.TabIndex = 1;
        btnKSHO0115.Text = "6. 売上情報EXCEL出力 (KSHO0115)";
        btnKSHO0115.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKSHO0115.UseVisualStyleBackColor = false;
        btnKSHO0115.Click += BtnKSHO0115_Click;
        // 
        // btnKZKO0020
        // 
        btnKZKO0020.BackColor = System.Drawing.SystemColors.Control;
        btnKZKO0020.Enabled = true;
        btnKZKO0020.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKZKO0020.Location = new System.Drawing.Point(32, 110);
        btnKZKO0020.Name = "btnKZKO0020";
        btnKZKO0020.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKZKO0020.Size = new System.Drawing.Size(380, 36);
        btnKZKO0020.TabIndex = 2;
        btnKZKO0020.Text = "7. 在庫調整データ登録 (KZKO0020)";
        btnKZKO0020.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKZKO0020.UseVisualStyleBackColor = false;
        btnKZKO0020.Click += BtnKZKO0020_Click;
        // 
        // btnKZKO0010
        // 
        btnKZKO0010.BackColor = System.Drawing.SystemColors.Control;
        btnKZKO0010.Enabled = true;
        btnKZKO0010.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKZKO0010.Location = new System.Drawing.Point(32, 152);
        btnKZKO0010.Name = "btnKZKO0010";
        btnKZKO0010.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKZKO0010.Size = new System.Drawing.Size(380, 36);
        btnKZKO0010.TabIndex = 3;
        btnKZKO0010.Text = "8. 在庫マスタ更新処理 (KZKO0010)";
        btnKZKO0010.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKZKO0010.UseVisualStyleBackColor = false;
        btnKZKO0010.Click += BtnKZKO0010_Click;
        // 
        // btnKTBO0020
        // 
        btnKTBO0020.BackColor = System.Drawing.SystemColors.Control;
        btnKTBO0020.Enabled = true;
        btnKTBO0020.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnKTBO0020.Location = new System.Drawing.Point(32, 194);
        btnKTBO0020.Name = "btnKTBO0020";
        btnKTBO0020.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
        btnKTBO0020.Size = new System.Drawing.Size(380, 36);
        btnKTBO0020.TabIndex = 4;
        btnKTBO0020.Text = "9. 売上履歴照会 (KTBO0020)";
        btnKTBO0020.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btnKTBO0020.UseVisualStyleBackColor = false;
        btnKTBO0020.Click += BtnKTBO0020_Click;
        // 
        // btnExit
        // 
        btnExit.BackColor = System.Drawing.SystemColors.Control;
        btnExit.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        btnExit.Location = new System.Drawing.Point(406, 622);
        btnExit.Name = "btnExit";
        btnExit.Size = new System.Drawing.Size(86, 36);
        btnExit.TabIndex = 4;
        btnExit.Text = "終 了";
        btnExit.UseVisualStyleBackColor = false;
        btnExit.Click += BtnExit_Click;
        // 
        // FrmMainMenu
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        ClientSize = new System.Drawing.Size(540, 676);
        Controls.Add(btnExit);
        Controls.Add(grpZaiko);
        Controls.Add(grpMaster);
        Controls.Add(lblDate);
        Controls.Add(lblTitle);
        Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FrmMainMenu";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KousyoERP";
        Load += FrmMainMenu_Load;
        grpMaster.ResumeLayout(false);
        grpZaiko.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
