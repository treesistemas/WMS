
namespace Wms.Impressao
{
    partial class FrmImpressaoEndereco
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbLado = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbDisponibilidade = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRegiao = new System.Windows.Forms.ComboBox();
            this.cmbBloco = new System.Windows.Forms.ComboBox();
            this.lblBlocoInicial = new System.Windows.Forms.Label();
            this.lblRua = new System.Windows.Forms.Label();
            this.cmbRua = new System.Windows.Forms.ComboBox();
            this.lblRegiao = new System.Windows.Forms.Label();
            this.cmbEndereco = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(498, 312);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 276;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            // 
            // btnAnalisar
            // 
            this.btnAnalisar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalisar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAnalisar.ForeColor = System.Drawing.Color.White;
            this.btnAnalisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnalisar.Location = new System.Drawing.Point(373, 312);
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
            this.label18.Location = new System.Drawing.Point(13, 9);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(373, 39);
            this.label18.TabIndex = 274;
            this.label18.Text = "Relatório de Endereços";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(451, 9);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 74);
            this.pictureBox1.TabIndex = 273;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 366);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(606, 25);
            this.panel1.TabIndex = 284;
            // 
            // cmbLado
            // 
            this.cmbLado.FormattingEnabled = true;
            this.cmbLado.Items.AddRange(new object[] {
            "Todos",
            "Impar",
            "Par"});
            this.cmbLado.Location = new System.Drawing.Point(21, 209);
            this.cmbLado.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLado.Name = "cmbLado";
            this.cmbLado.Size = new System.Drawing.Size(81, 24);
            this.cmbLado.TabIndex = 296;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(20, 189);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 17);
            this.label20.TabIndex = 295;
            this.label20.Text = "Lado";
            // 
            // cmbDisponibilidade
            // 
            this.cmbDisponibilidade.FormattingEnabled = true;
            this.cmbDisponibilidade.Items.AddRange(new object[] {
            "SIM",
            "NÃO"});
            this.cmbDisponibilidade.Location = new System.Drawing.Point(245, 209);
            this.cmbDisponibilidade.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDisponibilidade.Name = "cmbDisponibilidade";
            this.cmbDisponibilidade.Size = new System.Drawing.Size(125, 24);
            this.cmbDisponibilidade.TabIndex = 293;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(241, 189);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 294;
            this.label2.Text = "Disponível";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "VAGO",
            "OCUPADO"});
            this.cmbStatus.Location = new System.Drawing.Point(113, 209);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(123, 24);
            this.cmbStatus.TabIndex = 291;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(109, 189);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 292;
            this.label1.Text = "Status";
            // 
            // cmbRegiao
            // 
            this.cmbRegiao.FormattingEnabled = true;
            this.cmbRegiao.Location = new System.Drawing.Point(20, 98);
            this.cmbRegiao.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRegiao.Name = "cmbRegiao";
            this.cmbRegiao.Size = new System.Drawing.Size(81, 24);
            this.cmbRegiao.TabIndex = 285;
            // 
            // cmbBloco
            // 
            this.cmbBloco.FormattingEnabled = true;
            this.cmbBloco.Location = new System.Drawing.Point(112, 155);
            this.cmbBloco.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBloco.Name = "cmbBloco";
            this.cmbBloco.Size = new System.Drawing.Size(80, 24);
            this.cmbBloco.TabIndex = 286;
            // 
            // lblBlocoInicial
            // 
            this.lblBlocoInicial.AutoSize = true;
            this.lblBlocoInicial.ForeColor = System.Drawing.Color.Black;
            this.lblBlocoInicial.Location = new System.Drawing.Point(108, 135);
            this.lblBlocoInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlocoInicial.Name = "lblBlocoInicial";
            this.lblBlocoInicial.Size = new System.Drawing.Size(43, 17);
            this.lblBlocoInicial.TabIndex = 290;
            this.lblBlocoInicial.Text = "Bloco";
            // 
            // lblRua
            // 
            this.lblRua.AutoSize = true;
            this.lblRua.ForeColor = System.Drawing.Color.Black;
            this.lblRua.Location = new System.Drawing.Point(17, 136);
            this.lblRua.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRua.Name = "lblRua";
            this.lblRua.Size = new System.Drawing.Size(34, 17);
            this.lblRua.TabIndex = 289;
            this.lblRua.Text = "Rua";
            // 
            // cmbRua
            // 
            this.cmbRua.FormattingEnabled = true;
            this.cmbRua.Location = new System.Drawing.Point(20, 155);
            this.cmbRua.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRua.Name = "cmbRua";
            this.cmbRua.Size = new System.Drawing.Size(81, 24);
            this.cmbRua.TabIndex = 287;
            // 
            // lblRegiao
            // 
            this.lblRegiao.AutoSize = true;
            this.lblRegiao.ForeColor = System.Drawing.Color.Black;
            this.lblRegiao.Location = new System.Drawing.Point(17, 78);
            this.lblRegiao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegiao.Name = "lblRegiao";
            this.lblRegiao.Size = new System.Drawing.Size(53, 17);
            this.lblRegiao.TabIndex = 288;
            this.lblRegiao.Text = "Região";
            // 
            // cmbEndereco
            // 
            this.cmbEndereco.FormattingEnabled = true;
            this.cmbEndereco.Items.AddRange(new object[] {
            "TODOS",
            "PICKING CAIXA",
            "PICKING FLOWRACK",
            "PULMÃO",
            "BLOCADO"});
            this.cmbEndereco.Location = new System.Drawing.Point(22, 271);
            this.cmbEndereco.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEndereco.Name = "cmbEndereco";
            this.cmbEndereco.Size = new System.Drawing.Size(214, 24);
            this.cmbEndereco.TabIndex = 297;
            this.cmbEndereco.Text = "TODOS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(18, 251);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 298;
            this.label3.Text = "Tipo de Endereço";
            // 
            // FrmImpressaoEndereco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(606, 391);
            this.Controls.Add(this.cmbEndereco);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbLado);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cmbDisponibilidade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRegiao);
            this.Controls.Add(this.cmbBloco);
            this.Controls.Add(this.lblBlocoInicial);
            this.Controls.Add(this.lblRua);
            this.Controls.Add(this.cmbRua);
            this.Controls.Add(this.lblRegiao);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAnalisar);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "FrmImpressaoEndereco";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbLado;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbDisponibilidade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRegiao;
        private System.Windows.Forms.ComboBox cmbBloco;
        private System.Windows.Forms.Label lblBlocoInicial;
        private System.Windows.Forms.Label lblRua;
        private System.Windows.Forms.ComboBox cmbRua;
        private System.Windows.Forms.Label lblRegiao;
        private System.Windows.Forms.ComboBox cmbEndereco;
        private System.Windows.Forms.Label label3;
    }
}