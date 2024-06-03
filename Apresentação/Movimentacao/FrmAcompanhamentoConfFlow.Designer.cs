
namespace Wms
{
    partial class FrmAcompanhamentoConfFlow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label13 = new System.Windows.Forms.Label();
            this.gridItens = new System.Windows.Forms.DataGridView();
            this.lblPedido = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDataProcessamento = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblItens = new System.Windows.Forms.Label();
            this.lblConferidos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCorte = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUsuário = new System.Windows.Forms.Label();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(16, 11);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(417, 39);
            this.label13.TabIndex = 63;
            this.label13.Text = "Conferência de Flow Rack";
            // 
            // gridItens
            // 
            this.gridItens.AllowUserToAddRows = false;
            this.gridItens.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridItens.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridItens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridItens.BackgroundColor = System.Drawing.Color.White;
            this.gridItens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridItens.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.SlateGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItens.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridItens.ColumnHeadersHeight = 28;
            this.gridItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10,
            this.Column1,
            this.Column2,
            this.Column12,
            this.dataGridViewTextBoxColumn5,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column11,
            this.Column6});
            this.gridItens.EnableHeadersVisualStyles = false;
            this.gridItens.GridColor = System.Drawing.Color.Gainsboro;
            this.gridItens.Location = new System.Drawing.Point(13, 194);
            this.gridItens.Margin = new System.Windows.Forms.Padding(4);
            this.gridItens.MultiSelect = false;
            this.gridItens.Name = "gridItens";
            this.gridItens.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItens.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridItens.RowHeadersVisible = false;
            this.gridItens.RowHeadersWidth = 20;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridItens.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.gridItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridItens.Size = new System.Drawing.Size(1296, 345);
            this.gridItens.TabIndex = 117;
            this.gridItens.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridItens_CellMouseClick);
            this.gridItens.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridItens_KeyUp);
            // 
            // lblPedido
            // 
            this.lblPedido.AutoSize = true;
            this.lblPedido.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblPedido.Location = new System.Drawing.Point(20, 150);
            this.lblPedido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(11, 16);
            this.lblPedido.TabIndex = 119;
            this.lblPedido.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 123);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 118;
            this.label3.Text = "Pedido";
            // 
            // lblDataProcessamento
            // 
            this.lblDataProcessamento.AutoSize = true;
            this.lblDataProcessamento.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblDataProcessamento.Location = new System.Drawing.Point(20, 94);
            this.lblDataProcessamento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataProcessamento.Name = "lblDataProcessamento";
            this.lblDataProcessamento.Size = new System.Drawing.Size(11, 16);
            this.lblDataProcessamento.TabIndex = 121;
            this.lblDataProcessamento.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 16);
            this.label5.TabIndex = 120;
            this.label5.Text = "Data de Processamento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 123);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 122;
            this.label1.Text = "Itens";
            // 
            // lblItens
            // 
            this.lblItens.AutoSize = true;
            this.lblItens.ForeColor = System.Drawing.Color.Black;
            this.lblItens.Location = new System.Drawing.Point(190, 150);
            this.lblItens.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItens.Name = "lblItens";
            this.lblItens.Size = new System.Drawing.Size(14, 16);
            this.lblItens.TabIndex = 123;
            this.lblItens.Text = "0";
            // 
            // lblConferidos
            // 
            this.lblConferidos.AutoSize = true;
            this.lblConferidos.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblConferidos.Location = new System.Drawing.Point(291, 150);
            this.lblConferidos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConferidos.Name = "lblConferidos";
            this.lblConferidos.Size = new System.Drawing.Size(14, 16);
            this.lblConferidos.TabIndex = 125;
            this.lblConferidos.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(291, 123);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 124;
            this.label6.Text = "Conferidos";
            // 
            // lblCorte
            // 
            this.lblCorte.AutoSize = true;
            this.lblCorte.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblCorte.Location = new System.Drawing.Point(418, 150);
            this.lblCorte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCorte.Name = "lblCorte";
            this.lblCorte.Size = new System.Drawing.Size(14, 16);
            this.lblCorte.TabIndex = 127;
            this.lblCorte.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(418, 123);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 16);
            this.label8.TabIndex = 126;
            this.label8.Text = "Corte";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 16);
            this.label2.TabIndex = 128;
            this.label2.Text = "Iníciado Por";
            // 
            // lblUsuário
            // 
            this.lblUsuário.AutoSize = true;
            this.lblUsuário.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblUsuário.Location = new System.Drawing.Point(190, 94);
            this.lblUsuário.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuário.Name = "lblUsuário";
            this.lblUsuário.Size = new System.Drawing.Size(11, 16);
            this.lblUsuário.TabIndex = 129;
            this.lblUsuário.Text = "-";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Nº";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 40;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Data Processamento";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            this.Column10.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Estação";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Volume";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 60;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Endereço";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 90;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn5.HeaderText = "Produto";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 3276;
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Qtd";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Conferido";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 70;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Corte";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 50;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Conferência";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 120;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Conferênte";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 110;
            // 
            // FrmAcompanhamentoConfFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1322, 554);
            this.Controls.Add(this.lblUsuário);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCorte);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblConferidos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblItens);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDataProcessamento);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblPedido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gridItens);
            this.Controls.Add(this.label13);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "FrmAcompanhamentoConfFlow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conferência de Flow Rack";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAcompanhamentoConfFlow_FormClosing);
            this.Load += new System.EventHandler(this.FrmAcompanhamentoConfFlow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView gridItens;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDataProcessamento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItens;
        private System.Windows.Forms.Label lblConferidos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCorte;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUsuário;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}