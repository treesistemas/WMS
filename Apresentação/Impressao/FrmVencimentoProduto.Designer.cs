
namespace Wms
{
    partial class FrmVencimentoProduto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrazo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtmFinal = new System.Windows.Forms.DateTimePicker();
            this.dtmInicial = new System.Windows.Forms.DateTimePicker();
            this.chkPicking = new System.Windows.Forms.CheckBox();
            this.chkPulmao = new System.Windows.Forms.CheckBox();
            this.rbtAnalitico = new System.Windows.Forms.RadioButton();
            this.rbtSintetico = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.lblProduto = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(10, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(293, 31);
            this.label13.TabIndex = 62;
            this.label13.Text = "Vencimento de produto";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(292, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 60);
            this.pictureBox1.TabIndex = 61;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 20);
            this.panel1.TabIndex = 73;
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.BackColor = System.Drawing.Color.White;
            this.txtFornecedor.Location = new System.Drawing.Point(15, 116);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(119, 20);
            this.txtFornecedor.TabIndex = 74;
            this.txtFornecedor.TextChanged += new System.EventHandler(this.txtFornecedor_TextChanged);
            this.txtFornecedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFornecedor_KeyDown);
            this.txtFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFornecedor_KeyPress);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 99);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(61, 13);
            this.lblCodigo.TabIndex = 76;
            this.lblCodigo.Text = "Fornecedor";
            // 
            // txtProduto
            // 
            this.txtProduto.BackColor = System.Drawing.Color.White;
            this.txtProduto.Location = new System.Drawing.Point(15, 171);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(119, 20);
            this.txtProduto.TabIndex = 77;
            this.txtProduto.TextChanged += new System.EventHandler(this.txtProduto_TextChanged);
            this.txtProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProduto_KeyDown);
            this.txtProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProduto_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "Produto";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(108, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 175;
            this.label12.Text = "* F2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(108, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 176;
            this.label2.Text = "* F2";
            // 
            // btnAnalisar
            // 
            this.btnAnalisar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalisar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAnalisar.ForeColor = System.Drawing.Color.White;
            this.btnAnalisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnalisar.Location = new System.Drawing.Point(374, 323);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(88, 31);
            this.btnAnalisar.TabIndex = 177;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = false;
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(467, 323);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 31);
            this.btnSair.TabIndex = 178;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 226;
            this.label3.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "BLOQUEADO PARA NEGOCIACAO",
            "TODOS"});
            this.cmbStatus.Location = new System.Drawing.Point(15, 228);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(209, 21);
            this.cmbStatus.TabIndex = 225;
            this.cmbStatus.Text = "TODOS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(50, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 229;
            this.label4.Text = "Dias";
            // 
            // txtPrazo
            // 
            this.txtPrazo.BackColor = System.Drawing.Color.White;
            this.txtPrazo.Location = new System.Drawing.Point(15, 275);
            this.txtPrazo.Name = "txtPrazo";
            this.txtPrazo.Size = new System.Drawing.Size(69, 20);
            this.txtPrazo.TabIndex = 227;
            this.txtPrazo.TextChanged += new System.EventHandler(this.txtPrazo_TextChanged);
            this.txtPrazo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrazo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 258);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 228;
            this.label5.Text = "Prazo (";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 13);
            this.label6.TabIndex = 230;
            this.label6.Text = ")";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 233;
            this.label7.Text = "Período";
            // 
            // dtmFinal
            // 
            this.dtmFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmFinal.Location = new System.Drawing.Point(144, 320);
            this.dtmFinal.Name = "dtmFinal";
            this.dtmFinal.Size = new System.Drawing.Size(100, 20);
            this.dtmFinal.TabIndex = 232;
            this.dtmFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtmFinal_KeyPress);
            // 
            // dtmInicial
            // 
            this.dtmInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmInicial.Location = new System.Drawing.Point(15, 320);
            this.dtmInicial.Name = "dtmInicial";
            this.dtmInicial.Size = new System.Drawing.Size(119, 20);
            this.dtmInicial.TabIndex = 231;
            this.dtmInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtmInicial_KeyPress);
            // 
            // chkPicking
            // 
            this.chkPicking.AutoSize = true;
            this.chkPicking.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.chkPicking.Checked = true;
            this.chkPicking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPicking.ForeColor = System.Drawing.Color.White;
            this.chkPicking.Location = new System.Drawing.Point(374, 85);
            this.chkPicking.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkPicking.Name = "chkPicking";
            this.chkPicking.Size = new System.Drawing.Size(128, 17);
            this.chkPicking.TabIndex = 234;
            this.chkPicking.Text = "Endereço de Picking ";
            this.chkPicking.UseVisualStyleBackColor = false;
            this.chkPicking.CheckedChanged += new System.EventHandler(this.chkPicking_CheckedChanged);
            // 
            // chkPulmao
            // 
            this.chkPulmao.AutoSize = true;
            this.chkPulmao.BackColor = System.Drawing.Color.Orange;
            this.chkPulmao.Checked = true;
            this.chkPulmao.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPulmao.ForeColor = System.Drawing.Color.White;
            this.chkPulmao.Location = new System.Drawing.Point(374, 107);
            this.chkPulmao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkPulmao.Name = "chkPulmao";
            this.chkPulmao.Size = new System.Drawing.Size(125, 17);
            this.chkPulmao.TabIndex = 235;
            this.chkPulmao.Text = "Endereço de Pulmão";
            this.chkPulmao.UseVisualStyleBackColor = false;
            this.chkPulmao.CheckedChanged += new System.EventHandler(this.chkPulmao_CheckedChanged);
            // 
            // rbtAnalitico
            // 
            this.rbtAnalitico.AutoSize = true;
            this.rbtAnalitico.BackColor = System.Drawing.Color.DimGray;
            this.rbtAnalitico.Checked = true;
            this.rbtAnalitico.ForeColor = System.Drawing.Color.White;
            this.rbtAnalitico.Location = new System.Drawing.Point(373, 167);
            this.rbtAnalitico.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtAnalitico.Name = "rbtAnalitico";
            this.rbtAnalitico.Size = new System.Drawing.Size(112, 17);
            this.rbtAnalitico.TabIndex = 237;
            this.rbtAnalitico.TabStop = true;
            this.rbtAnalitico.Text = "Relatório Analítico";
            this.rbtAnalitico.UseVisualStyleBackColor = false;
            this.rbtAnalitico.CheckedChanged += new System.EventHandler(this.rbtAnalitico_CheckedChanged);
            // 
            // rbtSintetico
            // 
            this.rbtSintetico.AutoSize = true;
            this.rbtSintetico.Location = new System.Drawing.Point(373, 188);
            this.rbtSintetico.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtSintetico.Name = "rbtSintetico";
            this.rbtSintetico.Size = new System.Drawing.Size(111, 17);
            this.rbtSintetico.TabIndex = 238;
            this.rbtSintetico.Text = "Relatório Sintético";
            this.rbtSintetico.UseVisualStyleBackColor = true;
            this.rbtSintetico.Visible = false;
            this.rbtSintetico.CheckedChanged += new System.EventHandler(this.rbtSintetico_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Location = new System.Drawing.Point(371, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 239;
            this.label8.Text = "Tipo de Relatório";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Green;
            this.label9.Location = new System.Drawing.Point(371, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 240;
            this.label9.Text = "Tipo de Endereço";
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblFornecedor.Location = new System.Drawing.Point(13, 138);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(10, 13);
            this.lblFornecedor.TabIndex = 241;
            this.lblFornecedor.Text = "-";
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblProduto.Location = new System.Drawing.Point(13, 193);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(10, 13);
            this.lblProduto.TabIndex = 242;
            this.lblProduto.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(371, 221);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 13);
            this.label10.TabIndex = 243;
            this.label10.Text = "Enviar relatório por e-mail";
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.BackColor = System.Drawing.Color.LightCoral;
            this.chkEmail.ForeColor = System.Drawing.Color.White;
            this.chkEmail.Location = new System.Drawing.Point(373, 244);
            this.chkEmail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(132, 17);
            this.chkEmail.TabIndex = 244;
            this.chkEmail.Text = "Gerar arquivo sintético";
            this.chkEmail.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label11.Location = new System.Drawing.Point(13, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 246;
            this.label11.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(14, 67);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(120, 21);
            this.cmbEmpresa.TabIndex = 245;
            // 
            // FrmVencimentoProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(552, 380);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.chkEmail);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rbtSintetico);
            this.Controls.Add(this.rbtAnalitico);
            this.Controls.Add(this.chkPulmao);
            this.Controls.Add(this.chkPicking);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtmFinal);
            this.Controls.Add(this.dtmInicial);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPrazo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAnalisar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtProduto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFornecedor);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "FrmVencimentoProduto";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmVencimentoProduto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrazo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtmFinal;
        private System.Windows.Forms.DateTimePicker dtmInicial;
        private System.Windows.Forms.CheckBox chkPicking;
        private System.Windows.Forms.CheckBox chkPulmao;
        private System.Windows.Forms.RadioButton rbtAnalitico;
        private System.Windows.Forms.RadioButton rbtSintetico;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}