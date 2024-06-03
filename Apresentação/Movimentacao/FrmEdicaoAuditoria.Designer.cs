
namespace Wms
{
    partial class FrmEdicaoAuditoria
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
            this.txtVencimento = new System.Windows.Forms.MaskedTextBox();
            this.lblPeso = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.txtEstoque = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblEstoque = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDescProduto = new System.Windows.Forms.Label();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbAuditor = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtVencimento
            // 
            this.txtVencimento.Location = new System.Drawing.Point(172, 167);
            this.txtVencimento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVencimento.Mask = "00/00/0000";
            this.txtVencimento.Name = "txtVencimento";
            this.txtVencimento.Size = new System.Drawing.Size(132, 22);
            this.txtVencimento.TabIndex = 238;
            this.txtVencimento.ValidatingType = typeof(System.DateTime);
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.ForeColor = System.Drawing.SystemColors.Window;
            this.lblPeso.Location = new System.Drawing.Point(53, 6);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(31, 16);
            this.lblPeso.TabIndex = 1;
            this.lblPeso.Text = "0,00";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblPeso);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 390);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 25);
            this.panel1.TabIndex = 237;
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.ForeColor = System.Drawing.SystemColors.Window;
            this.lblInformacao.Location = new System.Drawing.Point(9, 6);
            this.lblInformacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(42, 16);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Peso:";
            // 
            // txtLote
            // 
            this.txtLote.BackColor = System.Drawing.Color.White;
            this.txtLote.Location = new System.Drawing.Point(316, 167);
            this.txtLote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLote.Name = "txtLote";
            this.txtLote.Size = new System.Drawing.Size(132, 22);
            this.txtLote.TabIndex = 235;
            // 
            // txtEstoque
            // 
            this.txtEstoque.BackColor = System.Drawing.Color.White;
            this.txtEstoque.Location = new System.Drawing.Point(24, 167);
            this.txtEstoque.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEstoque.Name = "txtEstoque";
            this.txtEstoque.Size = new System.Drawing.Size(132, 22);
            this.txtEstoque.TabIndex = 219;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(20, 60);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(34, 16);
            this.lblCodigo.TabIndex = 224;
            this.lblCodigo.Text = "SKU";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(24, 81);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 22);
            this.txtCodigo.TabIndex = 223;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(181, 60);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 225;
            this.label8.Text = "Endereço";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(312, 148);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 16);
            this.label16.TabIndex = 234;
            this.label16.Text = "Lote";
            // 
            // lblEstoque
            // 
            this.lblEstoque.AutoSize = true;
            this.lblEstoque.ForeColor = System.Drawing.Color.Black;
            this.lblEstoque.Location = new System.Drawing.Point(20, 148);
            this.lblEstoque.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstoque.Name = "lblEstoque";
            this.lblEstoque.Size = new System.Drawing.Size(57, 16);
            this.lblEstoque.TabIndex = 226;
            this.lblEstoque.Text = "Estoque";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(168, 148);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 16);
            this.label10.TabIndex = 227;
            this.label10.Text = "Vencimento";
            // 
            // lblDescProduto
            // 
            this.lblDescProduto.AutoSize = true;
            this.lblDescProduto.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblDescProduto.Location = new System.Drawing.Point(20, 117);
            this.lblDescProduto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescProduto.Name = "lblDescProduto";
            this.lblDescProduto.Size = new System.Drawing.Size(138, 16);
            this.lblDescProduto.TabIndex = 228;
            this.lblDescProduto.Text = "DESCRIÇÃO DO SKU";
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.ForeColor = System.Drawing.Color.Green;
            this.lblEndereco.Location = new System.Drawing.Point(181, 85);
            this.lblEndereco.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(11, 16);
            this.lblEndereco.TabIndex = 229;
            this.lblEndereco.Text = "-";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Tomato;
            this.label13.Location = new System.Drawing.Point(16, 6);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(313, 39);
            this.label13.TabIndex = 220;
            this.label13.Text = "Edição de Auditoria";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.DimGray;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(575, 330);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(103, 38);
            this.btnCancelar.TabIndex = 240;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.DimGray;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(464, 330);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(103, 38);
            this.btnSalvar.TabIndex = 239;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(373, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 74);
            this.pictureBox1.TabIndex = 222;
            this.pictureBox1.TabStop = false;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(460, 146);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 16);
            this.label15.TabIndex = 241;
            this.label15.Text = "Auditor";
            // 
            // cmbAuditor
            // 
            this.cmbAuditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAuditor.BackColor = System.Drawing.Color.White;
            this.cmbAuditor.Enabled = false;
            this.cmbAuditor.FormattingEnabled = true;
            this.cmbAuditor.Location = new System.Drawing.Point(464, 166);
            this.cmbAuditor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbAuditor.Name = "cmbAuditor";
            this.cmbAuditor.Size = new System.Drawing.Size(212, 24);
            this.cmbAuditor.TabIndex = 242;
            this.cmbAuditor.Text = "Selecione";
            // 
            // FrmEdicaoAuditoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(705, 415);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbAuditor);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.txtVencimento);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtLote);
            this.Controls.Add(this.txtEstoque);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lblEstoque);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblDescProduto);
            this.Controls.Add(this.lblEndereco);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEdicaoAuditoria";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edição de Auditoria";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txtVencimento;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.TextBox txtEstoque;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblEstoque;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDescProduto;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbAuditor;
    }
}