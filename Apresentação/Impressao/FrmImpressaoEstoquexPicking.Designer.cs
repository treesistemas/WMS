
namespace Wms.Impressao
{
    partial class FrmImpressaoEstoquexPicking
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
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkComPicking = new System.Windows.Forms.CheckBox();
            this.chkSemPicking = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtComEstoque = new System.Windows.Forms.RadioButton();
            this.rbtSemEstoque = new System.Windows.Forms.RadioButton();
            this.cmbTipoPicking = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(332, 249);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 31);
            this.btnSair.TabIndex = 264;
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
            this.btnAnalisar.Location = new System.Drawing.Point(238, 249);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(88, 31);
            this.btnAnalisar.TabIndex = 263;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = false;
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisar_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.SteelBlue;
            this.label18.Location = new System.Drawing.Point(10, 7);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(229, 31);
            this.label18.TabIndex = 259;
            this.label18.Text = "Estoque x Picking";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(338, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 60);
            this.pictureBox1.TabIndex = 258;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(13, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 267;
            this.label9.Text = "Tipo de Picking";
            // 
            // chkComPicking
            // 
            this.chkComPicking.AutoSize = true;
            this.chkComPicking.BackColor = System.Drawing.Color.White;
            this.chkComPicking.Enabled = false;
            this.chkComPicking.ForeColor = System.Drawing.Color.Black;
            this.chkComPicking.Location = new System.Drawing.Point(142, 160);
            this.chkComPicking.Margin = new System.Windows.Forms.Padding(2);
            this.chkComPicking.Name = "chkComPicking";
            this.chkComPicking.Size = new System.Drawing.Size(85, 17);
            this.chkComPicking.TabIndex = 266;
            this.chkComPicking.Text = "Com Picking";
            this.chkComPicking.UseVisualStyleBackColor = false;
            // 
            // chkSemPicking
            // 
            this.chkSemPicking.AutoSize = true;
            this.chkSemPicking.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.chkSemPicking.Checked = true;
            this.chkSemPicking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSemPicking.Enabled = false;
            this.chkSemPicking.ForeColor = System.Drawing.Color.White;
            this.chkSemPicking.Location = new System.Drawing.Point(15, 160);
            this.chkSemPicking.Margin = new System.Windows.Forms.Padding(2);
            this.chkSemPicking.Name = "chkSemPicking";
            this.chkSemPicking.Size = new System.Drawing.Size(88, 17);
            this.chkSemPicking.TabIndex = 265;
            this.chkSemPicking.Text = "Sem Picking ";
            this.chkSemPicking.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 20);
            this.panel1.TabIndex = 268;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Location = new System.Drawing.Point(13, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 270;
            this.label8.Text = "Produto:";
            // 
            // rbtComEstoque
            // 
            this.rbtComEstoque.AutoSize = true;
            this.rbtComEstoque.BackColor = System.Drawing.Color.DimGray;
            this.rbtComEstoque.Checked = true;
            this.rbtComEstoque.ForeColor = System.Drawing.Color.White;
            this.rbtComEstoque.Location = new System.Drawing.Point(15, 138);
            this.rbtComEstoque.Margin = new System.Windows.Forms.Padding(2);
            this.rbtComEstoque.Name = "rbtComEstoque";
            this.rbtComEstoque.Size = new System.Drawing.Size(88, 17);
            this.rbtComEstoque.TabIndex = 269;
            this.rbtComEstoque.TabStop = true;
            this.rbtComEstoque.Text = "Com Estoque";
            this.rbtComEstoque.UseVisualStyleBackColor = false;
            this.rbtComEstoque.CheckedChanged += new System.EventHandler(this.rbtComEstoque_CheckedChanged);
            // 
            // rbtSemEstoque
            // 
            this.rbtSemEstoque.AutoSize = true;
            this.rbtSemEstoque.Location = new System.Drawing.Point(142, 138);
            this.rbtSemEstoque.Margin = new System.Windows.Forms.Padding(2);
            this.rbtSemEstoque.Name = "rbtSemEstoque";
            this.rbtSemEstoque.Size = new System.Drawing.Size(88, 17);
            this.rbtSemEstoque.TabIndex = 271;
            this.rbtSemEstoque.Text = "Sem Estoque";
            this.rbtSemEstoque.UseVisualStyleBackColor = true;
            this.rbtSemEstoque.CheckedChanged += new System.EventHandler(this.rbtSemEstoque_CheckedChanged);
            // 
            // cmbTipoPicking
            // 
            this.cmbTipoPicking.Enabled = false;
            this.cmbTipoPicking.FormattingEnabled = true;
            this.cmbTipoPicking.Items.AddRange(new object[] {
            "SELECIONE",
            "GRANDEZA",
            "FLOW RACK"});
            this.cmbTipoPicking.Location = new System.Drawing.Point(15, 79);
            this.cmbTipoPicking.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTipoPicking.Name = "cmbTipoPicking";
            this.cmbTipoPicking.Size = new System.Drawing.Size(119, 21);
            this.cmbTipoPicking.TabIndex = 272;
            this.cmbTipoPicking.Text = "SELECIONE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(13, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 274;
            this.label3.Text = "Não inserir a categoria";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(16, 211);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(209, 21);
            this.cmbCategoria.TabIndex = 273;
            this.cmbCategoria.Text = "SELECIONE";
            this.cmbCategoria.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbCategoria_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(151, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 276;
            this.label6.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(151, 79);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(113, 21);
            this.cmbEmpresa.TabIndex = 275;
            this.cmbEmpresa.SelectedIndexChanged += new System.EventHandler(this.cmbEmpresa_SelectedIndexChanged);
            // 
            // FrmImpressaoEstoquexPicking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(421, 307);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.cmbTipoPicking);
            this.Controls.Add(this.rbtSemEstoque);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rbtComEstoque);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkComPicking);
            this.Controls.Add(this.chkSemPicking);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAnalisar);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmImpressaoEstoquexPicking";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmImpressaoEstoquexPicking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkComPicking;
        private System.Windows.Forms.CheckBox chkSemPicking;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbtComEstoque;
        private System.Windows.Forms.RadioButton rbtSemEstoque;
        private System.Windows.Forms.ComboBox cmbTipoPicking;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}