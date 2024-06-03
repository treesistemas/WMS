
namespace Wms
{
    partial class FrmEdicaoEstoque
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCapacidade = new System.Windows.Forms.TextBox();
            this.lblAbastecimento = new System.Windows.Forms.Label();
            this.lblEstoque = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDescProduto = new System.Windows.Forms.Label();
            this.lblCapacidade = new System.Windows.Forms.Label();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.txtAbastecimento = new System.Windows.Forms.TextBox();
            this.txtEstoque = new System.Windows.Forms.TextBox();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPeso = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.txtVencimento = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(280, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 60);
            this.pictureBox1.TabIndex = 185;
            this.pictureBox1.TabStop = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.DimGray;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(194, 296);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(77, 31);
            this.btnSalvar.TabIndex = 184;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Tomato;
            this.label13.Location = new System.Drawing.Point(12, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(241, 31);
            this.label13.TabIndex = 183;
            this.label13.Text = "Edição de Estoque";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(15, 53);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(29, 13);
            this.lblCodigo.TabIndex = 196;
            this.lblCodigo.Text = "SKU";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(18, 70);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 195;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(136, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 197;
            this.label8.Text = "Endereço";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(15, 173);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 13);
            this.label16.TabIndex = 210;
            this.label16.Text = "Lote";
            // 
            // txtCapacidade
            // 
            this.txtCapacidade.BackColor = System.Drawing.Color.White;
            this.txtCapacidade.Location = new System.Drawing.Point(127, 189);
            this.txtCapacidade.Name = "txtCapacidade";
            this.txtCapacidade.Size = new System.Drawing.Size(99, 20);
            this.txtCapacidade.TabIndex = 206;
            this.txtCapacidade.Visible = false;
            this.txtCapacidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapacidade_KeyPress);
            // 
            // lblAbastecimento
            // 
            this.lblAbastecimento.AutoSize = true;
            this.lblAbastecimento.ForeColor = System.Drawing.Color.Black;
            this.lblAbastecimento.Location = new System.Drawing.Point(16, 221);
            this.lblAbastecimento.Name = "lblAbastecimento";
            this.lblAbastecimento.Size = new System.Drawing.Size(77, 13);
            this.lblAbastecimento.TabIndex = 209;
            this.lblAbastecimento.Text = "Abastecimento";
            this.lblAbastecimento.Visible = false;
            // 
            // lblEstoque
            // 
            this.lblEstoque.AutoSize = true;
            this.lblEstoque.ForeColor = System.Drawing.Color.Black;
            this.lblEstoque.Location = new System.Drawing.Point(15, 124);
            this.lblEstoque.Name = "lblEstoque";
            this.lblEstoque.Size = new System.Drawing.Size(46, 13);
            this.lblEstoque.TabIndex = 198;
            this.lblEstoque.Text = "Estoque";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(126, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 199;
            this.label10.Text = "Vencimento";
            // 
            // lblDescProduto
            // 
            this.lblDescProduto.AutoSize = true;
            this.lblDescProduto.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblDescProduto.Location = new System.Drawing.Point(15, 99);
            this.lblDescProduto.Name = "lblDescProduto";
            this.lblDescProduto.Size = new System.Drawing.Size(113, 13);
            this.lblDescProduto.TabIndex = 202;
            this.lblDescProduto.Text = "DESCRIÇÃO DO SKU";
            // 
            // lblCapacidade
            // 
            this.lblCapacidade.AutoSize = true;
            this.lblCapacidade.ForeColor = System.Drawing.Color.Black;
            this.lblCapacidade.Location = new System.Drawing.Point(126, 173);
            this.lblCapacidade.Name = "lblCapacidade";
            this.lblCapacidade.Size = new System.Drawing.Size(64, 13);
            this.lblCapacidade.TabIndex = 208;
            this.lblCapacidade.Text = "Capacidade";
            this.lblCapacidade.Visible = false;
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.ForeColor = System.Drawing.Color.Green;
            this.lblEndereco.Location = new System.Drawing.Point(136, 73);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(10, 13);
            this.lblEndereco.TabIndex = 203;
            this.lblEndereco.Text = "-";
            // 
            // txtAbastecimento
            // 
            this.txtAbastecimento.BackColor = System.Drawing.Color.White;
            this.txtAbastecimento.Location = new System.Drawing.Point(18, 237);
            this.txtAbastecimento.Name = "txtAbastecimento";
            this.txtAbastecimento.Size = new System.Drawing.Size(101, 20);
            this.txtAbastecimento.TabIndex = 207;
            this.txtAbastecimento.Visible = false;
            this.txtAbastecimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAbastecimento_KeyPress);
            // 
            // txtEstoque
            // 
            this.txtEstoque.BackColor = System.Drawing.Color.White;
            this.txtEstoque.Location = new System.Drawing.Point(18, 140);
            this.txtEstoque.Name = "txtEstoque";
            this.txtEstoque.Size = new System.Drawing.Size(100, 20);
            this.txtEstoque.TabIndex = 1;
            this.txtEstoque.TextChanged += new System.EventHandler(this.txtEstoque_TextChanged);
            this.txtEstoque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            // 
            // txtLote
            // 
            this.txtLote.BackColor = System.Drawing.Color.White;
            this.txtLote.Location = new System.Drawing.Point(18, 189);
            this.txtLote.Name = "txtLote";
            this.txtLote.Size = new System.Drawing.Size(100, 20);
            this.txtLote.TabIndex = 215;
            this.txtLote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLote_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.DimGray;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(277, 296);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(77, 31);
            this.btnCancelar.TabIndex = 216;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblPeso);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 335);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 20);
            this.panel1.TabIndex = 217;
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.ForeColor = System.Drawing.SystemColors.Window;
            this.lblPeso.Location = new System.Drawing.Point(40, 5);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(28, 13);
            this.lblPeso.TabIndex = 1;
            this.lblPeso.Text = "0,00";
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.ForeColor = System.Drawing.SystemColors.Window;
            this.lblInformacao.Location = new System.Drawing.Point(7, 5);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(34, 13);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Peso:";
            // 
            // txtVencimento
            // 
            this.txtVencimento.Location = new System.Drawing.Point(129, 140);
            this.txtVencimento.Mask = "00/00/0000";
            this.txtVencimento.Name = "txtVencimento";
            this.txtVencimento.Size = new System.Drawing.Size(100, 20);
            this.txtVencimento.TabIndex = 218;
            this.txtVencimento.ValidatingType = typeof(System.DateTime);
            // 
            // FrmEdicaoEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(366, 355);
            this.Controls.Add(this.txtVencimento);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtLote);
            this.Controls.Add(this.txtEstoque);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtCapacidade);
            this.Controls.Add(this.lblAbastecimento);
            this.Controls.Add(this.lblEstoque);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblDescProduto);
            this.Controls.Add(this.lblCapacidade);
            this.Controls.Add(this.lblEndereco);
            this.Controls.Add(this.txtAbastecimento);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEdicaoEstoque";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edição de Estoque";
            this.Load += new System.EventHandler(this.FrmEdicaoEstoque_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCapacidade;
        private System.Windows.Forms.Label lblAbastecimento;
        private System.Windows.Forms.Label lblEstoque;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDescProduto;
        private System.Windows.Forms.Label lblCapacidade;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.TextBox txtAbastecimento;
        private System.Windows.Forms.TextBox txtEstoque;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.MaskedTextBox txtVencimento;
    }
}