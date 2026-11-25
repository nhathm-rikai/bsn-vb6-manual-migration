#nullable enable
namespace KousyoERP.Forms.Uriage;

partial class FrmKTBO0021
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Label Label1 = null!;
    private System.Windows.Forms.Label lblYMD = null!;
    private System.Windows.Forms.Label Label2 = null!;
    private System.Windows.Forms.Label Label3 = null!;
    private System.Windows.Forms.Label Label12 = null!;
    private System.Windows.Forms.Label Label13 = null!;
    private System.Windows.Forms.Label Label14 = null!;
    private System.Windows.Forms.Label Label6 = null!;
    private System.Windows.Forms.Label Label4 = null!;
    private System.Windows.Forms.Label Label5 = null!;
    private System.Windows.Forms.Label Label16 = null!;
    private System.Windows.Forms.Label Label10 = null!;

    private readonly System.Windows.Forms.Label[] lblNO = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblHNM = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblDENNO = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblHANDAY = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblSUU = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblTNK = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblHANKIN = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblSNM1 = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblSNM2 = new System.Windows.Forms.Label[10];
    private readonly System.Windows.Forms.Label[] lblTANNM = new System.Windows.Forms.Label[10];

    private System.Windows.Forms.Button cmdBACK = null!;
    private System.Windows.Forms.Button cmdNEXT = null!;
    private System.Windows.Forms.Button cmdprt = null!;
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
        var bg = System.Drawing.Color.FromArgb(192, 192, 192);
        var font12 = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        var font12Fixed = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        var font14 = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
        var font18 = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);

        Label1 = new System.Windows.Forms.Label();
        lblYMD = new System.Windows.Forms.Label();
        Label2 = new System.Windows.Forms.Label();
        Label3 = new System.Windows.Forms.Label();
        Label12 = new System.Windows.Forms.Label();
        Label13 = new System.Windows.Forms.Label();
        Label14 = new System.Windows.Forms.Label();
        Label6 = new System.Windows.Forms.Label();
        Label4 = new System.Windows.Forms.Label();
        Label5 = new System.Windows.Forms.Label();
        Label16 = new System.Windows.Forms.Label();
        Label10 = new System.Windows.Forms.Label();
        cmdBACK = new System.Windows.Forms.Button();
        cmdNEXT = new System.Windows.Forms.Button();
        cmdprt = new System.Windows.Forms.Button();
        cmdCAN = new System.Windows.Forms.Button();

        SuspendLayout();

        // Label1 ("売上履歴照会")
        Label1.BackColor = bg;
        Label1.Font = font18;
        Label1.Location = new System.Drawing.Point(312, 8);
        Label1.Name = "Label1";
        Label1.Size = new System.Drawing.Size(249, 28);
        Label1.TabIndex = 0;
        Label1.Text = "売上履歴照会";
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // lblYMD
        lblYMD.BackColor = bg;
        lblYMD.Font = font14;
        lblYMD.Location = new System.Drawing.Point(744, 8);
        lblYMD.Name = "lblYMD";
        lblYMD.Size = new System.Drawing.Size(121, 22);
        lblYMD.TabIndex = 1;
        lblYMD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // ヘッダーラベル群
        Label2.BackColor = bg;
        Label2.Font = font12;
        Label2.Location = new System.Drawing.Point(10, 72);
        Label2.Name = "Label2";
        Label2.Size = new System.Drawing.Size(36, 18);
        Label2.Text = "Ｎｏ";
        Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        Label3.BackColor = bg;
        Label3.Font = font12;
        Label3.Location = new System.Drawing.Point(168, 62);
        Label3.Name = "Label3";
        Label3.Size = new System.Drawing.Size(80, 18);
        Label3.Text = "販売先名";

        Label12.BackColor = bg;
        Label12.Font = font12;
        Label12.Location = new System.Drawing.Point(168, 83);
        Label12.Name = "Label12";
        Label12.Size = new System.Drawing.Size(80, 18);
        Label12.Text = "商品名１";

        Label13.BackColor = bg;
        Label13.Font = font12;
        Label13.Location = new System.Drawing.Point(382, 62);
        Label13.Name = "Label13";
        Label13.Size = new System.Drawing.Size(70, 18);
        Label13.Text = "伝票番号";

        Label14.BackColor = bg;
        Label14.Font = font12;
        Label14.Location = new System.Drawing.Point(464, 62);
        Label14.Name = "Label14";
        Label14.Size = new System.Drawing.Size(64, 18);
        Label14.Text = "販売日";

        Label6.BackColor = bg;
        Label6.Font = font12;
        Label6.Location = new System.Drawing.Point(560, 62);
        Label6.Name = "Label6";
        Label6.Size = new System.Drawing.Size(48, 18);
        Label6.Text = "数量";

        Label4.BackColor = bg;
        Label4.Font = font12;
        Label4.Location = new System.Drawing.Point(464, 83);
        Label4.Name = "Label4";
        Label4.Size = new System.Drawing.Size(80, 18);
        Label4.Text = "商品名２";

        Label5.BackColor = bg;
        Label5.Font = font12;
        Label5.Location = new System.Drawing.Point(672, 62);
        Label5.Name = "Label5";
        Label5.Size = new System.Drawing.Size(48, 18);
        Label5.Text = "単価";

        Label16.BackColor = bg;
        Label16.Font = font12;
        Label16.Location = new System.Drawing.Point(656, 83);
        Label16.Name = "Label16";
        Label16.Size = new System.Drawing.Size(96, 18);
        Label16.Text = "営業担当者";

        Label10.BackColor = bg;
        Label10.Font = font12;
        Label10.Location = new System.Drawing.Point(792, 62);
        Label10.Name = "Label10";
        Label10.Size = new System.Drawing.Size(74, 18);
        Label10.Text = "販売金額";

        // 10行分の明細ラベル生成 (行ピッチ 48px, 1行目 Y=104..152)
        for (int i = 0; i < 10; i++)
        {
            int topUpper = 110 + i * 48;
            int topLower = 129 + i * 48;

            lblNO[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12,
                Location = new System.Drawing.Point(12, topUpper),
                Name = $"lblNO_{i + 1}",
                Size = new System.Drawing.Size(34, 24),
                TextAlign = System.Drawing.ContentAlignment.TopCenter
            };

            lblHNM[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12Fixed,
                Location = new System.Drawing.Point(51, topUpper),
                Name = $"lblHNM_{i + 1}",
                Size = new System.Drawing.Size(322, 18)
            };

            lblDENNO[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12,
                Location = new System.Drawing.Point(382, topUpper),
                Name = $"lblDENNO_{i + 1}",
                Size = new System.Drawing.Size(70, 18)
            };

            lblHANDAY[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12,
                Location = new System.Drawing.Point(459, topUpper),
                Name = $"lblHANDAY_{i + 1}",
                Size = new System.Drawing.Size(83, 18)
            };

            lblSUU[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12,
                Location = new System.Drawing.Point(547, topUpper),
                Name = $"lblSUU_{i + 1}",
                Size = new System.Drawing.Size(82, 18),
                TextAlign = System.Drawing.ContentAlignment.TopRight
            };

            lblTNK[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12,
                Location = new System.Drawing.Point(636, topUpper),
                Name = $"lblTNK_{i + 1}",
                Size = new System.Drawing.Size(134, 18),
                TextAlign = System.Drawing.ContentAlignment.TopRight
            };

            lblHANKIN[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12,
                Location = new System.Drawing.Point(780, topUpper),
                Name = $"lblHANKIN_{i + 1}",
                Size = new System.Drawing.Size(88, 18),
                TextAlign = System.Drawing.ContentAlignment.TopRight
            };

            lblSNM1[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12Fixed,
                Location = new System.Drawing.Point(51, topLower),
                Name = $"lblSNM1_{i + 1}",
                Size = new System.Drawing.Size(322, 18)
            };

            lblSNM2[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12Fixed,
                Location = new System.Drawing.Point(382, topLower),
                Name = $"lblSNM2_{i + 1}",
                Size = new System.Drawing.Size(246, 18)
            };

            lblTANNM[i] = new System.Windows.Forms.Label
            {
                BackColor = bg,
                Font = font12,
                Location = new System.Drawing.Point(638, topLower),
                Name = $"lblTANNM_{i + 1}",
                Size = new System.Drawing.Size(132, 18)
            };

            Controls.Add(lblNO[i]);
            Controls.Add(lblHNM[i]);
            Controls.Add(lblDENNO[i]);
            Controls.Add(lblHANDAY[i]);
            Controls.Add(lblSUU[i]);
            Controls.Add(lblTNK[i]);
            Controls.Add(lblHANKIN[i]);
            Controls.Add(lblSNM1[i]);
            Controls.Add(lblSNM2[i]);
            Controls.Add(lblTANNM[i]);
        }

        // cmdBACK ("前 頁")
        cmdBACK.BackColor = System.Drawing.SystemColors.Control;
        cmdBACK.Font = font14;
        cmdBACK.Location = new System.Drawing.Point(144, 608);
        cmdBACK.Name = "cmdBACK";
        cmdBACK.Size = new System.Drawing.Size(89, 33);
        cmdBACK.TabIndex = 2;
        cmdBACK.Text = "前 頁";
        cmdBACK.UseVisualStyleBackColor = false;
        cmdBACK.Click += cmdBACK_Click;

        // cmdNEXT ("次 頁")
        cmdNEXT.BackColor = System.Drawing.SystemColors.Control;
        cmdNEXT.Font = font14;
        cmdNEXT.Location = new System.Drawing.Point(272, 608);
        cmdNEXT.Name = "cmdNEXT";
        cmdNEXT.Size = new System.Drawing.Size(89, 33);
        cmdNEXT.TabIndex = 3;
        cmdNEXT.Text = "次 頁";
        cmdNEXT.UseVisualStyleBackColor = false;
        cmdNEXT.Click += cmdNEXT_Click;

        // cmdprt ("印刷")
        cmdprt.BackColor = System.Drawing.SystemColors.Control;
        cmdprt.Font = font14;
        cmdprt.Location = new System.Drawing.Point(458, 608);
        cmdprt.Name = "cmdprt";
        cmdprt.Size = new System.Drawing.Size(91, 33);
        cmdprt.TabIndex = 4;
        cmdprt.Text = "印刷";
        cmdprt.UseVisualStyleBackColor = false;
        cmdprt.Click += cmdprt_Click;

        // cmdCAN ("戻 る")
        cmdCAN.BackColor = System.Drawing.SystemColors.Control;
        cmdCAN.Font = font14;
        cmdCAN.Location = new System.Drawing.Point(736, 608);
        cmdCAN.Name = "cmdCAN";
        cmdCAN.Size = new System.Drawing.Size(89, 33);
        cmdCAN.TabIndex = 5;
        cmdCAN.Text = "戻 る";
        cmdCAN.UseVisualStyleBackColor = false;
        cmdCAN.Click += cmdCAN_Click;

        // FrmKTBO0021
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = bg;
        ClientSize = new System.Drawing.Size(883, 660);
        ControlBox = false;
        Controls.Add(cmdCAN);
        Controls.Add(cmdprt);
        Controls.Add(cmdNEXT);
        Controls.Add(cmdBACK);
        Controls.Add(Label10);
        Controls.Add(Label16);
        Controls.Add(Label5);
        Controls.Add(Label4);
        Controls.Add(Label6);
        Controls.Add(Label14);
        Controls.Add(Label13);
        Controls.Add(Label12);
        Controls.Add(Label3);
        Controls.Add(Label2);
        Controls.Add(lblYMD);
        Controls.Add(Label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmKTBO0021";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KTBO0020";
        Load += FrmKTBO0021_Load;

        ResumeLayout(false);
    }

    #endregion
}

