
namespace Wms.Movimentacao
{
    partial class FrmCorteManifesto
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblPedido = new System.Windows.Forms.Label();
            this.lblProduto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridItens = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblqtdConferida = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCorte = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(111, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Quantidade:";
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.ForeColor = System.Drawing.Color.White;
            this.lblQuantidade.Location = new System.Drawing.Point(193, 5);
            this.lblQuantidade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(21, 16);
            this.lblQuantidade.TabIndex = 14;
            this.lblQuantidade.Text = "00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 5);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 16);
            this.label12.TabIndex = 6;
            this.label12.Text = "Pedido:";
            // 
            // lblPedido
            // 
            this.lblPedido.AutoSize = true;
            this.lblPedido.ForeColor = System.Drawing.Color.White;
            this.lblPedido.Location = new System.Drawing.Point(65, 5);
            this.lblPedido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(21, 16);
            this.lblPedido.TabIndex = 7;
            this.lblPedido.Text = "00";
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.ForeColor = System.Drawing.Color.Red;
            this.lblProduto.Location = new System.Drawing.Point(17, 81);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(11, 16);
            this.lblProduto.TabIndex = 206;
            this.lblProduto.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 205;
            this.label1.Text = "Produto";
            // 
            // gridItens
            // 
            this.gridItens.AllowUserToAddRows = false;
            this.gridItens.AllowUserToResizeColumns = false;
            this.gridItens.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridItens.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridItens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridItens.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridItens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.ColCodigo,
            this.Column6,
            this.Column2,
            this.Column5,
            this.Column3});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridItens.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridItens.GridColor = System.Drawing.Color.Gainsboro;
            this.gridItens.Location = new System.Drawing.Point(14, 112);
            this.gridItens.Margin = new System.Windows.Forms.Padding(4);
            this.gridItens.MultiSelect = false;
            this.gridItens.Name = "gridItens";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItens.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridItens.RowHeadersVisible = false;
            this.gridItens.RowHeadersWidth = 20;
            this.gridItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridItens.Size = new System.Drawing.Size(480, 371);
            this.gridItens.TabIndex = 204;
            this.gridItens.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridItens_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.Frozen = true;
            this.dataGridViewTextBoxColumn11.HeaderText = "Nº";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 40;
            // 
            // ColCodigo
            // 
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.ColCodigo.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColCodigo.HeaderText = "Pedido";
            this.ColCodigo.MaxInputLength = 3276;
            this.ColCodigo.MinimumWidth = 6;
            this.ColCodigo.Name = "ColCodigo";
            this.ColCodigo.ReadOnly = true;
            this.ColCodigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColCodigo.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "ID PRODUTO";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            this.Column6.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Quantidade";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 90;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Corte";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 90;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Saldo";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblqtdConferida);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblCorte);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblQuantidade);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lblPedido);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 551);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 25);
            this.panel1.TabIndex = 203;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(251, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Conferida:";
            // 
            // lblqtdConferida
            // 
            this.lblqtdConferida.AutoSize = true;
            this.lblqtdConferida.ForeColor = System.Drawing.Color.White;
            this.lblqtdConferida.Location = new System.Drawing.Point(324, 5);
            this.lblqtdConferida.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblqtdConferida.Name = "lblqtdConferida";
            this.lblqtdConferida.Size = new System.Drawing.Size(21, 16);
            this.lblqtdConferida.TabIndex = 18;
            this.lblqtdConferida.Text = "00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(390, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Corte:";
            // 
            // lblCorte
            // 
            this.lblCorte.AutoSize = true;
            this.lblCorte.ForeColor = System.Drawing.Color.White;
            this.lblCorte.Location = new System.Drawing.Point(438, 5);
            this.lblCorte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCorte.Name = "lblCorte";
            this.lblCorte.Size = new System.Drawing.Size(21, 16);
            this.lblCorte.TabIndex = 16;
            this.lblCorte.Text = "00";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.SteelBlue;
            this.label38.Location = new System.Drawing.Point(13, 4);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(263, 39);
            this.label38.TabIndex = 202;
            this.label38.Text = "Corte de Pedido";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.DimGray;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(412, 504);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(82, 30);
            this.btnCancelar.TabIndex = 208;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(325, 504);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(79, 30);
            this.btnConfirmar.TabIndex = 207;
            this.btnConfirmar.Text = "OK";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // FrmCorteManifesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(507, 576);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridItens);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label38);
            this.Name = "FrmCorteManifesto";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Corte de Pedido";
            this.Load += new System.EventHandler(this.FrmCorteManifesto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridItens;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblqtdConferida;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCorte;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}