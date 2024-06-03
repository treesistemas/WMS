
namespace Wms
{
    partial class FrmConsultaFlowRack
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodPedido = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridItens = new System.Windows.Forms.DataGridView();
            this.ColCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditaFlow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colControlaValidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colControlaLote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSair = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDataProcessamento = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblItens = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniRelatorioFlowRack = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).BeginInit();
            this.panel1.SuspendLayout();
            this.mnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido";
            // 
            // txtCodPedido
            // 
            this.txtCodPedido.Enabled = false;
            this.txtCodPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodPedido.Location = new System.Drawing.Point(20, 97);
            this.txtCodPedido.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodPedido.Name = "txtCodPedido";
            this.txtCodPedido.Size = new System.Drawing.Size(132, 27);
            this.txtCodPedido.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(12, 11);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(375, 39);
            this.label13.TabIndex = 59;
            this.label13.Text = "Volumes do  Flow Rack";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.gridItens);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(16, 160);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1299, 302);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Consulta";
            // 
            // gridItens
            // 
            this.gridItens.AllowUserToAddRows = false;
            this.gridItens.AllowUserToResizeColumns = false;
            this.gridItens.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridItens.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridItens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridItens.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridItens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodigo,
            this.ColDescricao,
            this.colAuditaFlow,
            this.colControlaValidade,
            this.colControlaLote,
            this.Column1,
            this.Column3,
            this.Column2});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridItens.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridItens.GridColor = System.Drawing.Color.Gainsboro;
            this.gridItens.Location = new System.Drawing.Point(8, 23);
            this.gridItens.Margin = new System.Windows.Forms.Padding(4);
            this.gridItens.MultiSelect = false;
            this.gridItens.Name = "gridItens";
            this.gridItens.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItens.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridItens.RowHeadersVisible = false;
            this.gridItens.RowHeadersWidth = 20;
            this.gridItens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridItens.Size = new System.Drawing.Size(1283, 271);
            this.gridItens.TabIndex = 0;
            // 
            // ColCodigo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColCodigo.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColCodigo.Frozen = true;
            this.ColCodigo.HeaderText = "Nº";
            this.ColCodigo.MaxInputLength = 3276;
            this.ColCodigo.MinimumWidth = 6;
            this.ColCodigo.Name = "ColCodigo";
            this.ColCodigo.ReadOnly = true;
            this.ColCodigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColCodigo.Width = 40;
            // 
            // ColDescricao
            // 
            this.ColDescricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.ColDescricao.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColDescricao.Frozen = true;
            this.ColDescricao.HeaderText = "Volume";
            this.ColDescricao.MinimumWidth = 6;
            this.ColDescricao.Name = "ColDescricao";
            this.ColDescricao.ReadOnly = true;
            this.ColDescricao.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColDescricao.Width = 55;
            // 
            // colAuditaFlow
            // 
            this.colAuditaFlow.Frozen = true;
            this.colAuditaFlow.HeaderText = "Estação";
            this.colAuditaFlow.MinimumWidth = 6;
            this.colAuditaFlow.Name = "colAuditaFlow";
            this.colAuditaFlow.ReadOnly = true;
            this.colAuditaFlow.Width = 125;
            // 
            // colControlaValidade
            // 
            this.colControlaValidade.HeaderText = "Endereço";
            this.colControlaValidade.MinimumWidth = 6;
            this.colControlaValidade.Name = "colControlaValidade";
            this.colControlaValidade.ReadOnly = true;
            this.colControlaValidade.Width = 125;
            // 
            // colControlaLote
            // 
            this.colControlaLote.HeaderText = "Produto";
            this.colControlaLote.MinimumWidth = 6;
            this.colControlaLote.Name = "colControlaLote";
            this.colControlaLote.ReadOnly = true;
            this.colControlaLote.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colControlaLote.Width = 300;
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "Quantidade";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Conferência";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 120;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Auditoria";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(1215, 484);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 61;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQuantidade);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblVolume);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblDataProcessamento);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblItens);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 529);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1331, 25);
            this.panel1.TabIndex = 62;
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQuantidade.Location = new System.Drawing.Point(660, 5);
            this.lblQuantidade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(14, 16);
            this.lblQuantidade.TabIndex = 7;
            this.lblQuantidade.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(576, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Quantidade:";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.ForeColor = System.Drawing.SystemColors.Window;
            this.lblVolume.Location = new System.Drawing.Point(365, 4);
            this.lblVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(14, 16);
            this.lblVolume.TabIndex = 5;
            this.lblVolume.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(301, 4);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Volumes:";
            // 
            // lblDataProcessamento
            // 
            this.lblDataProcessamento.AutoSize = true;
            this.lblDataProcessamento.ForeColor = System.Drawing.SystemColors.Window;
            this.lblDataProcessamento.Location = new System.Drawing.Point(128, 4);
            this.lblDataProcessamento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataProcessamento.Name = "lblDataProcessamento";
            this.lblDataProcessamento.Size = new System.Drawing.Size(11, 16);
            this.lblDataProcessamento.TabIndex = 3;
            this.lblDataProcessamento.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(19, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Processamento:";
            // 
            // lblItens
            // 
            this.lblItens.AutoSize = true;
            this.lblItens.ForeColor = System.Drawing.SystemColors.Window;
            this.lblItens.Location = new System.Drawing.Point(505, 4);
            this.lblItens.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItens.Name = "lblItens";
            this.lblItens.Size = new System.Drawing.Size(14, 16);
            this.lblItens.TabIndex = 1;
            this.lblItens.Text = "0";
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.ForeColor = System.Drawing.SystemColors.Window;
            this.lblInformacao.Location = new System.Drawing.Point(457, 4);
            this.lblInformacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(38, 16);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Itens:";
            // 
            // mnu
            // 
            this.mnu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniRelatorioFlowRack});
            this.mnu.Name = "mnu";
            this.mnu.Size = new System.Drawing.Size(330, 28);
            // 
            // mniRelatorioFlowRack
            // 
            this.mniRelatorioFlowRack.Name = "mniRelatorioFlowRack";
            this.mniRelatorioFlowRack.Size = new System.Drawing.Size(329, 24);
            this.mniRelatorioFlowRack.Text = "Relatório de Separação no Flow Rack ";
            this.mniRelatorioFlowRack.Click += new System.EventHandler(this.mniRelatorioFlowRack_Click);
            // 
            // FrmConsultaFlowRack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1331, 554);
            this.ContextMenuStrip = this.mnu;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtCodPedido);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConsultaFlowRack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de Volumes do Flow Rack";
            this.Load += new System.EventHandler(this.FrmConsultaFlowRack_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mnu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodPedido;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridItens;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblItens;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDataProcessamento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditaFlow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colControlaValidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colControlaLote;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.ContextMenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem mniRelatorioFlowRack;
    }
}