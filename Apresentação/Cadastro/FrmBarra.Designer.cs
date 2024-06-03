namespace Wms
{
    partial class FrmBarra
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
            this.lblCodBarra = new System.Windows.Forms.Label();
            this.txtCodigoBarra = new System.Windows.Forms.TextBox();
            this.txtMultiplicador = new System.Windows.Forms.TextBox();
            this.lblMultiplicador = new System.Windows.Forms.Label();
            this.txtAltura = new System.Windows.Forms.TextBox();
            this.lblAltura = new System.Windows.Forms.Label();
            this.txtLargura = new System.Windows.Forms.TextBox();
            this.lblLargura = new System.Windows.Forms.Label();
            this.txtComprimento = new System.Windows.Forms.TextBox();
            this.lblComprimento = new System.Windows.Forms.Label();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.lblPeso = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblCubagem = new System.Windows.Forms.Label();
            this.txtCubagem = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCodBarra
            // 
            this.lblCodBarra.AutoSize = true;
            this.lblCodBarra.Location = new System.Drawing.Point(12, 60);
            this.lblCodBarra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodBarra.Name = "lblCodBarra";
            this.lblCodBarra.Size = new System.Drawing.Size(106, 16);
            this.lblCodBarra.TabIndex = 0;
            this.lblCodBarra.Text = "Código de Barra";
            // 
            // txtCodigoBarra
            // 
            this.txtCodigoBarra.BackColor = System.Drawing.Color.White;
            this.txtCodigoBarra.Enabled = false;
            this.txtCodigoBarra.Location = new System.Drawing.Point(16, 80);
            this.txtCodigoBarra.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigoBarra.Name = "txtCodigoBarra";
            this.txtCodigoBarra.Size = new System.Drawing.Size(152, 22);
            this.txtCodigoBarra.TabIndex = 0;
            this.txtCodigoBarra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoBarra_KeyPress);
            // 
            // txtMultiplicador
            // 
            this.txtMultiplicador.BackColor = System.Drawing.Color.White;
            this.txtMultiplicador.Enabled = false;
            this.txtMultiplicador.Location = new System.Drawing.Point(16, 135);
            this.txtMultiplicador.Margin = new System.Windows.Forms.Padding(4);
            this.txtMultiplicador.Name = "txtMultiplicador";
            this.txtMultiplicador.Size = new System.Drawing.Size(152, 22);
            this.txtMultiplicador.TabIndex = 3;
            this.txtMultiplicador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMultiplicador_KeyPress);
            // 
            // lblMultiplicador
            // 
            this.lblMultiplicador.AutoSize = true;
            this.lblMultiplicador.Location = new System.Drawing.Point(12, 116);
            this.lblMultiplicador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMultiplicador.Name = "lblMultiplicador";
            this.lblMultiplicador.Size = new System.Drawing.Size(83, 16);
            this.lblMultiplicador.TabIndex = 2;
            this.lblMultiplicador.Text = "Multiplicador";
            // 
            // txtAltura
            // 
            this.txtAltura.Location = new System.Drawing.Point(16, 187);
            this.txtAltura.Margin = new System.Windows.Forms.Padding(4);
            this.txtAltura.Name = "txtAltura";
            this.txtAltura.Size = new System.Drawing.Size(152, 22);
            this.txtAltura.TabIndex = 5;
            this.txtAltura.TextChanged += new System.EventHandler(this.txtAltura_TextChanged);
            this.txtAltura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAltura_KeyPress);
            // 
            // lblAltura
            // 
            this.lblAltura.AutoSize = true;
            this.lblAltura.Location = new System.Drawing.Point(12, 167);
            this.lblAltura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(62, 16);
            this.lblAltura.TabIndex = 4;
            this.lblAltura.Text = "Altura cm";
            // 
            // txtLargura
            // 
            this.txtLargura.Location = new System.Drawing.Point(16, 241);
            this.txtLargura.Margin = new System.Windows.Forms.Padding(4);
            this.txtLargura.Name = "txtLargura";
            this.txtLargura.Size = new System.Drawing.Size(152, 22);
            this.txtLargura.TabIndex = 7;
            this.txtLargura.TextChanged += new System.EventHandler(this.txtLargura_TextChanged);
            this.txtLargura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLargura_KeyPress);
            // 
            // lblLargura
            // 
            this.lblLargura.AutoSize = true;
            this.lblLargura.Location = new System.Drawing.Point(12, 222);
            this.lblLargura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLargura.Name = "lblLargura";
            this.lblLargura.Size = new System.Drawing.Size(70, 16);
            this.lblLargura.TabIndex = 6;
            this.lblLargura.Text = "largura cm";
            // 
            // txtComprimento
            // 
            this.txtComprimento.Location = new System.Drawing.Point(16, 299);
            this.txtComprimento.Margin = new System.Windows.Forms.Padding(4);
            this.txtComprimento.Name = "txtComprimento";
            this.txtComprimento.Size = new System.Drawing.Size(152, 22);
            this.txtComprimento.TabIndex = 9;
            this.txtComprimento.TextChanged += new System.EventHandler(this.txtComprimento_TextChanged);
            this.txtComprimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComprimento_KeyPress);
            // 
            // lblComprimento
            // 
            this.lblComprimento.AutoSize = true;
            this.lblComprimento.Location = new System.Drawing.Point(12, 279);
            this.lblComprimento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComprimento.Name = "lblComprimento";
            this.lblComprimento.Size = new System.Drawing.Size(108, 16);
            this.lblComprimento.TabIndex = 8;
            this.lblComprimento.Text = "Comprimento cm";
            // 
            // txtPeso
            // 
            this.txtPeso.Location = new System.Drawing.Point(16, 411);
            this.txtPeso.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Size = new System.Drawing.Size(152, 22);
            this.txtPeso.TabIndex = 11;
            this.txtPeso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPeso_KeyPress);
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.Location = new System.Drawing.Point(12, 391);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(58, 16);
            this.lblPeso.TabIndex = 10;
            this.lblPeso.Text = "Peso Kg";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.DimGray;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(120, 453);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(103, 38);
            this.btnSalvar.TabIndex = 162;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(244, 453);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 163;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.IndianRed;
            this.label13.Location = new System.Drawing.Point(9, 5);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(232, 36);
            this.label13.TabIndex = 159;
            this.label13.Text = "Código de Barra";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(209, 193);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(117, 102);
            this.pictureBox2.TabIndex = 165;
            this.pictureBox2.TabStop = false;
            // 
            // lblCubagem
            // 
            this.lblCubagem.AutoSize = true;
            this.lblCubagem.Location = new System.Drawing.Point(12, 335);
            this.lblCubagem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCubagem.Name = "lblCubagem";
            this.lblCubagem.Size = new System.Drawing.Size(84, 16);
            this.lblCubagem.TabIndex = 166;
            this.lblCubagem.Text = "Cubagem m³";
            // 
            // txtCubagem
            // 
            this.txtCubagem.Enabled = false;
            this.txtCubagem.Location = new System.Drawing.Point(16, 354);
            this.txtCubagem.Margin = new System.Windows.Forms.Padding(4);
            this.txtCubagem.Name = "txtCubagem";
            this.txtCubagem.Size = new System.Drawing.Size(152, 22);
            this.txtCubagem.TabIndex = 169;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 502);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 25);
            this.panel1.TabIndex = 170;
            // 
            // FrmBarra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(360, 527);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtCubagem);
            this.Controls.Add(this.lblCubagem);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.txtPeso);
            this.Controls.Add(this.lblPeso);
            this.Controls.Add(this.txtComprimento);
            this.Controls.Add(this.lblComprimento);
            this.Controls.Add(this.txtLargura);
            this.Controls.Add(this.lblLargura);
            this.Controls.Add(this.txtAltura);
            this.Controls.Add(this.lblAltura);
            this.Controls.Add(this.txtMultiplicador);
            this.Controls.Add(this.lblMultiplicador);
            this.Controls.Add(this.txtCodigoBarra);
            this.Controls.Add(this.lblCodBarra);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBarra";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Código de Barra";
            this.Load += new System.EventHandler(this.FrmBarra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCodBarra;
        private System.Windows.Forms.TextBox txtCodigoBarra;
        private System.Windows.Forms.TextBox txtMultiplicador;
        private System.Windows.Forms.Label lblMultiplicador;
        private System.Windows.Forms.TextBox txtAltura;
        private System.Windows.Forms.Label lblAltura;
        private System.Windows.Forms.TextBox txtLargura;
        private System.Windows.Forms.Label lblLargura;
        private System.Windows.Forms.TextBox txtComprimento;
        private System.Windows.Forms.Label lblComprimento;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblCubagem;
        private System.Windows.Forms.TextBox txtCubagem;
        private System.Windows.Forms.Panel panel1;
    }
}