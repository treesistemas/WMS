namespace Wms
{
    partial class FrmConferenciaAutomatica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConferenciaAutomatica));
            this.lblNmCliente = new System.Windows.Forms.Label();
            this.txtPedido = new System.Windows.Forms.TextBox();
            this.lblPedido = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConferir = new System.Windows.Forms.Button();
            this.btnDesconferir = new System.Windows.Forms.Button();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNmCliente
            // 
            this.lblNmCliente.AutoSize = true;
            this.lblNmCliente.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblNmCliente.Location = new System.Drawing.Point(15, 121);
            this.lblNmCliente.Name = "lblNmCliente";
            this.lblNmCliente.Size = new System.Drawing.Size(127, 13);
            this.lblNmCliente.TabIndex = 15;
            this.lblNmCliente.Text = "+BRASIL INFORMÁTICA";
            this.lblNmCliente.Visible = false;
            // 
            // txtPedido
            // 
            this.txtPedido.Location = new System.Drawing.Point(18, 93);
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.Size = new System.Drawing.Size(113, 20);
            this.txtPedido.TabIndex = 1;
            this.txtPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPedido_KeyPress);
            // 
            // lblPedido
            // 
            this.lblPedido.AutoSize = true;
            this.lblPedido.Location = new System.Drawing.Point(15, 75);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(40, 13);
            this.lblPedido.TabIndex = 0;
            this.lblPedido.Text = "Pedido";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 394);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 20);
            this.panel1.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(12, -3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(289, 31);
            this.label13.TabIndex = 59;
            this.label13.Text = "Conferêcia Automática";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(293, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 60);
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 234);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(9, 13);
            this.label15.TabIndex = 67;
            this.label15.Text = "|";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInstrucoes.AutoSize = true;
            this.lblInstrucoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucoes.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblInstrucoes.Location = new System.Drawing.Point(23, 231);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Size = new System.Drawing.Size(69, 20);
            this.lblInstrucoes.TabIndex = 66;
            this.lblInstrucoes.Text = "Atenção";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 261);
            this.label1.MaximumSize = new System.Drawing.Size(355, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 65);
            this.label1.TabIndex = 68;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnConferir
            // 
            this.btnConferir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConferir.BackColor = System.Drawing.Color.DimGray;
            this.btnConferir.ForeColor = System.Drawing.Color.White;
            this.btnConferir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConferir.Location = new System.Drawing.Point(125, 348);
            this.btnConferir.Name = "btnConferir";
            this.btnConferir.Size = new System.Drawing.Size(75, 31);
            this.btnConferir.TabIndex = 69;
            this.btnConferir.Text = "Conferir";
            this.btnConferir.UseVisualStyleBackColor = false;
            this.btnConferir.Click += new System.EventHandler(this.btnConferir_Click);
            // 
            // btnDesconferir
            // 
            this.btnDesconferir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDesconferir.BackColor = System.Drawing.Color.DimGray;
            this.btnDesconferir.ForeColor = System.Drawing.Color.White;
            this.btnDesconferir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDesconferir.Location = new System.Drawing.Point(206, 348);
            this.btnDesconferir.Name = "btnDesconferir";
            this.btnDesconferir.Size = new System.Drawing.Size(75, 31);
            this.btnDesconferir.TabIndex = 70;
            this.btnDesconferir.Text = "Desconferir";
            this.btnDesconferir.UseVisualStyleBackColor = false;
            this.btnDesconferir.Click += new System.EventHandler(this.btnDesconferir_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(151, 86);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(75, 31);
            this.btnPesquisar.TabIndex = 71;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(287, 348);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 31);
            this.btnSair.TabIndex = 72;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtMotivo
            // 
            this.txtMotivo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMotivo.Location = new System.Drawing.Point(18, 155);
            this.txtMotivo.MaxLength = 200;
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(344, 50);
            this.txtMotivo.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Motivo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(18, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 208;
            this.label6.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(18, 47);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(121, 21);
            this.cmbEmpresa.TabIndex = 207;
            // 
            // FrmConferenciaAutomatica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(374, 414);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.btnDesconferir);
            this.Controls.Add(this.btnConferir);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblInstrucoes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtPedido);
            this.Controls.Add(this.lblNmCliente);
            this.Controls.Add(this.lblPedido);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConferenciaAutomatica";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conferência Automática";
            this.Load += new System.EventHandler(this.FrmConferenciaAutomatica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPedido;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNmCliente;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConferir;
        private System.Windows.Forms.Button btnDesconferir;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}