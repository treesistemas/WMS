namespace Wms.Impressao
{
    partial class FrmImpressaoCurvaABC
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtmFinal = new System.Windows.Forms.DateTimePicker();
            this.dtmInicial = new System.Windows.Forms.DateTimePicker();
            this.cmbRegiao = new System.Windows.Forms.ComboBox();
            this.lblRua = new System.Windows.Forms.Label();
            this.cmbRua = new System.Windows.Forms.ComboBox();
            this.lblRegiao = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.cmbEstacao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 373);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(683, 25);
            this.panel1.TabIndex = 62;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(13, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(188, 39);
            this.label13.TabIndex = 63;
            this.label13.Text = "Curva ABC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 256);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 18);
            this.label7.TabIndex = 236;
            this.label7.Text = "Período";
            // 
            // dtmFinal
            // 
            this.dtmFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtmFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmFinal.Location = new System.Drawing.Point(142, 276);
            this.dtmFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtmFinal.Name = "dtmFinal";
            this.dtmFinal.Size = new System.Drawing.Size(107, 24);
            this.dtmFinal.TabIndex = 235;
            // 
            // dtmInicial
            // 
            this.dtmInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtmInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmInicial.Location = new System.Drawing.Point(21, 276);
            this.dtmInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtmInicial.Name = "dtmInicial";
            this.dtmInicial.Size = new System.Drawing.Size(113, 24);
            this.dtmInicial.TabIndex = 234;
            // 
            // cmbRegiao
            // 
            this.cmbRegiao.Enabled = false;
            this.cmbRegiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegiao.FormattingEnabled = true;
            this.cmbRegiao.Location = new System.Drawing.Point(21, 217);
            this.cmbRegiao.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRegiao.Name = "cmbRegiao";
            this.cmbRegiao.Size = new System.Drawing.Size(113, 26);
            this.cmbRegiao.TabIndex = 304;
            this.cmbRegiao.SelectedIndexChanged += new System.EventHandler(this.cmbRegiao_SelectedIndexChanged);
            this.cmbRegiao.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbRegiao_MouseClick);
            // 
            // lblRua
            // 
            this.lblRua.AutoSize = true;
            this.lblRua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRua.ForeColor = System.Drawing.Color.Black;
            this.lblRua.Location = new System.Drawing.Point(138, 194);
            this.lblRua.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRua.Name = "lblRua";
            this.lblRua.Size = new System.Drawing.Size(35, 18);
            this.lblRua.TabIndex = 307;
            this.lblRua.Text = "Rua";
            // 
            // cmbRua
            // 
            this.cmbRua.Enabled = false;
            this.cmbRua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRua.FormattingEnabled = true;
            this.cmbRua.Location = new System.Drawing.Point(142, 217);
            this.cmbRua.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRua.Name = "cmbRua";
            this.cmbRua.Size = new System.Drawing.Size(107, 26);
            this.cmbRua.TabIndex = 305;
            // 
            // lblRegiao
            // 
            this.lblRegiao.AutoSize = true;
            this.lblRegiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegiao.ForeColor = System.Drawing.Color.Black;
            this.lblRegiao.Location = new System.Drawing.Point(17, 193);
            this.lblRegiao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegiao.Name = "lblRegiao";
            this.lblRegiao.Size = new System.Drawing.Size(55, 18);
            this.lblRegiao.TabIndex = 306;
            this.lblRegiao.Text = "Região";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "SELECIONE",
            "CAIXA",
            "FLOWRACK"});
            this.cmbTipo.Location = new System.Drawing.Point(20, 90);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(157, 26);
            this.cmbTipo.TabIndex = 303;
            this.cmbTipo.Text = "SELECIONE";
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(17, 66);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 18);
            this.label9.TabIndex = 302;
            this.label9.Text = "Tipo";
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(570, 322);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 309;
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
            this.btnAnalisar.Location = new System.Drawing.Point(445, 322);
            this.btnAnalisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(117, 38);
            this.btnAnalisar.TabIndex = 308;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = false;
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisar_Click);
            // 
            // cmbEstacao
            // 
            this.cmbEstacao.Enabled = false;
            this.cmbEstacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEstacao.FormattingEnabled = true;
            this.cmbEstacao.Location = new System.Drawing.Point(20, 154);
            this.cmbEstacao.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEstacao.Name = "cmbEstacao";
            this.cmbEstacao.Size = new System.Drawing.Size(114, 26);
            this.cmbEstacao.TabIndex = 310;
            this.cmbEstacao.SelectedIndexChanged += new System.EventHandler(this.cmbEstacao_SelectedIndexChanged);
            this.cmbEstacao.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbEstacao_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 130);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 18);
            this.label1.TabIndex = 311;
            this.label1.Text = "Estação";
            // 
            // FrmImpressaoCurvaABC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(683, 398);
            this.Controls.Add(this.cmbEstacao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAnalisar);
            this.Controls.Add(this.cmbRegiao);
            this.Controls.Add(this.lblRua);
            this.Controls.Add(this.cmbRua);
            this.Controls.Add(this.lblRegiao);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtmFinal);
            this.Controls.Add(this.dtmInicial);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "FrmImpressaoCurvaABC";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Curva ABC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtmFinal;
        private System.Windows.Forms.DateTimePicker dtmInicial;
        private System.Windows.Forms.ComboBox cmbRegiao;
        private System.Windows.Forms.Label lblRua;
        private System.Windows.Forms.ComboBox cmbRua;
        private System.Windows.Forms.Label lblRegiao;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.ComboBox cmbEstacao;
        private System.Windows.Forms.Label label1;
    }
}