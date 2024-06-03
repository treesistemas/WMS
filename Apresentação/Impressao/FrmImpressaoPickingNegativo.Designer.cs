
namespace Wms.Impressao
{
    partial class FrmImpressaoPickingNegativo
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
            this.cmbRegiao = new System.Windows.Forms.ComboBox();
            this.lblRua = new System.Windows.Forms.Label();
            this.cmbRua = new System.Windows.Forms.ComboBox();
            this.lblRegiao = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbRegiao
            // 
            this.cmbRegiao.Enabled = false;
            this.cmbRegiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegiao.FormattingEnabled = true;
            this.cmbRegiao.Location = new System.Drawing.Point(15, 128);
            this.cmbRegiao.Name = "cmbRegiao";
            this.cmbRegiao.Size = new System.Drawing.Size(59, 23);
            this.cmbRegiao.TabIndex = 298;
            this.cmbRegiao.SelectedIndexChanged += new System.EventHandler(this.cmbRegiao_SelectedIndexChanged);
            this.cmbRegiao.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbRegiao_MouseClick);
            // 
            // lblRua
            // 
            this.lblRua.AutoSize = true;
            this.lblRua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRua.ForeColor = System.Drawing.Color.Black;
            this.lblRua.Location = new System.Drawing.Point(76, 110);
            this.lblRua.Name = "lblRua";
            this.lblRua.Size = new System.Drawing.Size(30, 15);
            this.lblRua.TabIndex = 301;
            this.lblRua.Text = "Rua";
            // 
            // cmbRua
            // 
            this.cmbRua.Enabled = false;
            this.cmbRua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRua.FormattingEnabled = true;
            this.cmbRua.Location = new System.Drawing.Point(79, 128);
            this.cmbRua.Name = "cmbRua";
            this.cmbRua.Size = new System.Drawing.Size(67, 23);
            this.cmbRua.TabIndex = 299;
            // 
            // lblRegiao
            // 
            this.lblRegiao.AutoSize = true;
            this.lblRegiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegiao.ForeColor = System.Drawing.Color.Black;
            this.lblRegiao.Location = new System.Drawing.Point(12, 109);
            this.lblRegiao.Name = "lblRegiao";
            this.lblRegiao.Size = new System.Drawing.Size(47, 15);
            this.lblRegiao.TabIndex = 300;
            this.lblRegiao.Text = "Região";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "TODOS",
            "ENDERECO"});
            this.cmbTipo.Location = new System.Drawing.Point(15, 73);
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(131, 23);
            this.cmbTipo.TabIndex = 297;
            this.cmbTipo.Text = "TODOS";
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            this.cmbTipo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbTipo_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 251);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 20);
            this.panel1.TabIndex = 296;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(13, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 15);
            this.label9.TabIndex = 295;
            this.label9.Text = "Tipo";
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(341, 214);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 31);
            this.btnSair.TabIndex = 294;
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
            this.btnAnalisar.Location = new System.Drawing.Point(248, 214);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(88, 31);
            this.btnAnalisar.TabIndex = 293;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = false;
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisar_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.SteelBlue;
            this.label18.Location = new System.Drawing.Point(10, 2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(245, 31);
            this.label18.TabIndex = 292;
            this.label18.Text = "Pickings Negativos";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(338, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 60);
            this.pictureBox1.TabIndex = 291;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(167, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 303;
            this.label6.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(167, 75);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(113, 21);
            this.cmbEmpresa.TabIndex = 302;
            // 
            // FrmImpressaoPickingNegativo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(430, 271);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEmpresa);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "FrmImpressaoPickingNegativo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmImpressaoPickingNegativo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRegiao;
        private System.Windows.Forms.Label lblRua;
        private System.Windows.Forms.ComboBox cmbRua;
        private System.Windows.Forms.Label lblRegiao;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}