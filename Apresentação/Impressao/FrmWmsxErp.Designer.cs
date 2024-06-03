
namespace Wms.Impressao
{
    partial class FrmWmsxErp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWmsxErp));
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbRegiao = new System.Windows.Forms.ComboBox();
            this.lblRua = new System.Windows.Forms.Label();
            this.cmbRua = new System.Windows.Forms.ComboBox();
            this.lblRegiao = new System.Windows.Forms.Label();
            this.cmbLado = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDescProduto = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTipo
            // 
            this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "TODOS",
            "PRODUTO",
            "ENDERECO"});
            this.cmbTipo.Location = new System.Drawing.Point(20, 80);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(173, 26);
            this.cmbTipo.TabIndex = 284;
            this.cmbTipo.Text = "TODOS";
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            this.cmbTipo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbTipo_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 332);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 25);
            this.panel1.TabIndex = 280;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(17, 56);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 18);
            this.label9.TabIndex = 279;
            this.label9.Text = "Tipo";
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(470, 286);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 276;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnAnalisar
            // 
            this.btnAnalisar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalisar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAnalisar.ForeColor = System.Drawing.Color.White;
            this.btnAnalisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnalisar.Location = new System.Drawing.Point(345, 286);
            this.btnAnalisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(117, 38);
            this.btnAnalisar.TabIndex = 275;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = false;
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisar_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.SteelBlue;
            this.label18.Location = new System.Drawing.Point(13, 5);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(200, 39);
            this.label18.TabIndex = 274;
            this.label18.Text = "Wms x ERP";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(451, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 74);
            this.pictureBox1.TabIndex = 273;
            this.pictureBox1.TabStop = false;
            // 
            // cmbRegiao
            // 
            this.cmbRegiao.Enabled = false;
            this.cmbRegiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegiao.FormattingEnabled = true;
            this.cmbRegiao.Location = new System.Drawing.Point(21, 227);
            this.cmbRegiao.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRegiao.Name = "cmbRegiao";
            this.cmbRegiao.Size = new System.Drawing.Size(77, 26);
            this.cmbRegiao.TabIndex = 285;
            this.cmbRegiao.SelectedIndexChanged += new System.EventHandler(this.cmbRegiao_SelectedIndexChanged);
            this.cmbRegiao.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbRegiao_MouseClick);
            // 
            // lblRua
            // 
            this.lblRua.AutoSize = true;
            this.lblRua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRua.ForeColor = System.Drawing.Color.Black;
            this.lblRua.Location = new System.Drawing.Point(102, 204);
            this.lblRua.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRua.Name = "lblRua";
            this.lblRua.Size = new System.Drawing.Size(35, 18);
            this.lblRua.TabIndex = 288;
            this.lblRua.Text = "Rua";
            // 
            // cmbRua
            // 
            this.cmbRua.Enabled = false;
            this.cmbRua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRua.FormattingEnabled = true;
            this.cmbRua.Location = new System.Drawing.Point(106, 227);
            this.cmbRua.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRua.Name = "cmbRua";
            this.cmbRua.Size = new System.Drawing.Size(88, 26);
            this.cmbRua.TabIndex = 286;
            // 
            // lblRegiao
            // 
            this.lblRegiao.AutoSize = true;
            this.lblRegiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegiao.ForeColor = System.Drawing.Color.Black;
            this.lblRegiao.Location = new System.Drawing.Point(17, 203);
            this.lblRegiao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegiao.Name = "lblRegiao";
            this.lblRegiao.Size = new System.Drawing.Size(55, 18);
            this.lblRegiao.TabIndex = 287;
            this.lblRegiao.Text = "Região";
            // 
            // cmbLado
            // 
            this.cmbLado.Enabled = false;
            this.cmbLado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLado.FormattingEnabled = true;
            this.cmbLado.Items.AddRange(new object[] {
            "TODOS",
            "IMPAR",
            "PAR"});
            this.cmbLado.Location = new System.Drawing.Point(202, 227);
            this.cmbLado.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLado.Name = "cmbLado";
            this.cmbLado.Size = new System.Drawing.Size(85, 26);
            this.cmbLado.TabIndex = 290;
            this.cmbLado.Text = "TODOS";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(198, 203);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 18);
            this.label20.TabIndex = 289;
            this.label20.Text = "Lado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(17, 121);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 18);
            this.label1.TabIndex = 291;
            this.label1.Text = "Produto";
            // 
            // txtProduto
            // 
            this.txtProduto.Enabled = false;
            this.txtProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduto.Location = new System.Drawing.Point(20, 143);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(173, 24);
            this.txtProduto.TabIndex = 292;
            this.txtProduto.TextChanged += new System.EventHandler(this.txtProduto_TextChanged);
            this.txtProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProduto_KeyDown);
            this.txtProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProduto_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(86, 124);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 16);
            this.label4.TabIndex = 293;
            this.label4.Text = "*F2";
            // 
            // lblDescProduto
            // 
            this.lblDescProduto.AutoSize = true;
            this.lblDescProduto.ForeColor = System.Drawing.Color.Black;
            this.lblDescProduto.Location = new System.Drawing.Point(18, 172);
            this.lblDescProduto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescProduto.Name = "lblDescProduto";
            this.lblDescProduto.Size = new System.Drawing.Size(11, 16);
            this.lblDescProduto.TabIndex = 294;
            this.lblDescProduto.Text = "-";
            // 
            // FrmWmsxErp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(583, 357);
            this.Controls.Add(this.lblDescProduto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProduto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLado);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cmbRegiao);
            this.Controls.Add(this.lblRua);
            this.Controls.Add(this.cmbRua);
            this.Controls.Add(this.lblRegiao);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAnalisar);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmWmsxErp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbRegiao;
        private System.Windows.Forms.Label lblRua;
        private System.Windows.Forms.ComboBox cmbRua;
        private System.Windows.Forms.Label lblRegiao;
        private System.Windows.Forms.ComboBox cmbLado;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDescProduto;
    }
}