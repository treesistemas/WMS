
namespace Wms
{
    partial class FrmExportacaoCheckNow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtManifestoFinal = new System.Windows.Forms.TextBox();
            this.txtManifestoInicial = new System.Windows.Forms.TextBox();
            this.lblManifestoFinal = new System.Windows.Forms.Label();
            this.lblManifestoInicial = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblManifestos = new System.Windows.Forms.Label();
            this.lblPeso = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPedidos = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkExportar = new System.Windows.Forms.CheckBox();
            this.gridManifesto = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniRemoverManifesto = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridManifesto)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(17, 13);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(373, 39);
            this.label13.TabIndex = 66;
            this.label13.Text = "Exportação Check Now";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(400, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 74);
            this.pictureBox1.TabIndex = 65;
            this.pictureBox1.TabStop = false;
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.Location = new System.Drawing.Point(470, 412);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(100, 38);
            this.btnExportar.TabIndex = 64;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(580, 412);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 59;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtManifestoFinal
            // 
            this.txtManifestoFinal.BackColor = System.Drawing.Color.White;
            this.txtManifestoFinal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtManifestoFinal.Location = new System.Drawing.Point(25, 165);
            this.txtManifestoFinal.Margin = new System.Windows.Forms.Padding(4);
            this.txtManifestoFinal.MaxLength = 40;
            this.txtManifestoFinal.Name = "txtManifestoFinal";
            this.txtManifestoFinal.Size = new System.Drawing.Size(125, 22);
            this.txtManifestoFinal.TabIndex = 1;
            this.txtManifestoFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtManifestoFinal_KeyPress);
            // 
            // txtManifestoInicial
            // 
            this.txtManifestoInicial.BackColor = System.Drawing.Color.White;
            this.txtManifestoInicial.Location = new System.Drawing.Point(25, 109);
            this.txtManifestoInicial.Margin = new System.Windows.Forms.Padding(4);
            this.txtManifestoInicial.Name = "txtManifestoInicial";
            this.txtManifestoInicial.Size = new System.Drawing.Size(125, 22);
            this.txtManifestoInicial.TabIndex = 0;
            this.txtManifestoInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtManifestoInicial_KeyPress);
            // 
            // lblManifestoFinal
            // 
            this.lblManifestoFinal.AutoSize = true;
            this.lblManifestoFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManifestoFinal.ForeColor = System.Drawing.Color.Black;
            this.lblManifestoFinal.Location = new System.Drawing.Point(21, 146);
            this.lblManifestoFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblManifestoFinal.Name = "lblManifestoFinal";
            this.lblManifestoFinal.Size = new System.Drawing.Size(108, 18);
            this.lblManifestoFinal.TabIndex = 60;
            this.lblManifestoFinal.Text = "Manifesto Final";
            // 
            // lblManifestoInicial
            // 
            this.lblManifestoInicial.AutoSize = true;
            this.lblManifestoInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManifestoInicial.ForeColor = System.Drawing.Color.Black;
            this.lblManifestoInicial.Location = new System.Drawing.Point(21, 88);
            this.lblManifestoInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblManifestoInicial.Name = "lblManifestoInicial";
            this.lblManifestoInicial.Size = new System.Drawing.Size(113, 18);
            this.lblManifestoInicial.TabIndex = 62;
            this.lblManifestoInicial.Text = "Manifesto Inícial";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblManifestos);
            this.panel1.Controls.Add(this.lblPeso);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblPedidos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 471);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 25);
            this.panel1.TabIndex = 63;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(558, 3);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(16, 18);
            this.lblTotal.TabIndex = 102;
            this.lblTotal.Text = "0";
            // 
            // lblInformacao
            // 
            this.lblInformacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacao.ForeColor = System.Drawing.Color.White;
            this.lblInformacao.Location = new System.Drawing.Point(13, 3);
            this.lblInformacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(85, 18);
            this.lblInformacao.TabIndex = 95;
            this.lblInformacao.Text = "Manifestos:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(473, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 18);
            this.label7.TabIndex = 101;
            this.label7.Text = "Valor Total:";
            // 
            // lblManifestos
            // 
            this.lblManifestos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblManifestos.AutoSize = true;
            this.lblManifestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManifestos.ForeColor = System.Drawing.Color.White;
            this.lblManifestos.Location = new System.Drawing.Point(101, 3);
            this.lblManifestos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblManifestos.Name = "lblManifestos";
            this.lblManifestos.Size = new System.Drawing.Size(16, 18);
            this.lblManifestos.TabIndex = 96;
            this.lblManifestos.Text = "0";
            // 
            // lblPeso
            // 
            this.lblPeso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPeso.AutoSize = true;
            this.lblPeso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeso.ForeColor = System.Drawing.Color.White;
            this.lblPeso.Location = new System.Drawing.Point(375, 3);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(16, 18);
            this.lblPeso.TabIndex = 100;
            this.lblPeso.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(164, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 18);
            this.label3.TabIndex = 97;
            this.label3.Text = "Pedidos:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(327, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 18);
            this.label5.TabIndex = 99;
            this.label5.Text = "Peso:";
            // 
            // lblPedidos
            // 
            this.lblPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPedidos.AutoSize = true;
            this.lblPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidos.ForeColor = System.Drawing.Color.White;
            this.lblPedidos.Location = new System.Drawing.Point(235, 3);
            this.lblPedidos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(16, 18);
            this.lblPedidos.TabIndex = 98;
            this.lblPedidos.Text = "0";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(355, 251);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 17);
            this.label15.TabIndex = 93;
            this.label15.Text = "|";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInstrucoes.AutoSize = true;
            this.lblInstrucoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucoes.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblInstrucoes.Location = new System.Drawing.Point(365, 248);
            this.lblInstrucoes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Size = new System.Drawing.Size(119, 25);
            this.lblInstrucoes.TabIndex = 92;
            this.lblInstrucoes.Text = "Informações";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 293);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.MaximumSize = new System.Drawing.Size(340, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 68);
            this.label1.TabIndex = 94;
            this.label1.Text = "A exportação irá gerar um arquivo no formato \"txt\"  dentro   da  pasta  \"EXPORTAÇ" +
    "ÃO CHECKNOW\" que deverá  ser  importado  para o sistema check now, permitindo as" +
    "sim sua usabilidade.";
            // 
            // chkExportar
            // 
            this.chkExportar.AutoSize = true;
            this.chkExportar.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkExportar.Location = new System.Drawing.Point(24, 210);
            this.chkExportar.Name = "chkExportar";
            this.chkExportar.Size = new System.Drawing.Size(273, 21);
            this.chkExportar.TabIndex = 95;
            this.chkExportar.Text = "Exportar manifestos fora de sequencia";
            this.chkExportar.UseVisualStyleBackColor = true;
            this.chkExportar.CheckedChanged += new System.EventHandler(this.chkExportar_CheckedChanged);
            // 
            // gridManifesto
            // 
            this.gridManifesto.AllowUserToAddRows = false;
            this.gridManifesto.AllowUserToResizeColumns = false;
            this.gridManifesto.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridManifesto.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridManifesto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridManifesto.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridManifesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridManifesto.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridManifesto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridManifesto.ColumnHeadersHeight = 28;
            this.gridManifesto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridManifesto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column4,
            this.Column1});
            this.gridManifesto.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridManifesto.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridManifesto.Enabled = false;
            this.gridManifesto.EnableHeadersVisualStyles = false;
            this.gridManifesto.GridColor = System.Drawing.Color.Gainsboro;
            this.gridManifesto.Location = new System.Drawing.Point(24, 248);
            this.gridManifesto.Margin = new System.Windows.Forms.Padding(4);
            this.gridManifesto.MultiSelect = false;
            this.gridManifesto.Name = "gridManifesto";
            this.gridManifesto.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridManifesto.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridManifesto.RowHeadersVisible = false;
            this.gridManifesto.RowHeadersWidth = 20;
            this.gridManifesto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridManifesto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridManifesto.Size = new System.Drawing.Size(273, 175);
            this.gridManifesto.TabIndex = 96;
            // 
            // Column5
            // 
            this.Column5.Frozen = true;
            this.Column5.HeaderText = "Nº";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 50;
            // 
            // Column4
            // 
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column4.HeaderText = "Manifesto";
            this.Column4.MaxInputLength = 3276;
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.Width = 90;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Pedido";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniRemoverManifesto});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 56);
            // 
            // mniRemoverManifesto
            // 
            this.mniRemoverManifesto.Name = "mniRemoverManifesto";
            this.mniRemoverManifesto.Size = new System.Drawing.Size(210, 24);
            this.mniRemoverManifesto.Text = "Remover Manifesto";
            this.mniRemoverManifesto.Click += new System.EventHandler(this.mniRemoverManifesto_Click);
            // 
            // FrmExportacaoCheckNow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(693, 496);
            this.Controls.Add(this.gridManifesto);
            this.Controls.Add(this.chkExportar);
            this.Controls.Add(this.lblManifestoInicial);
            this.Controls.Add(this.lblManifestoFinal);
            this.Controls.Add(this.txtManifestoInicial);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtManifestoFinal);
            this.Controls.Add(this.lblInstrucoes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportacaoCheckNow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridManifesto)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.TextBox txtManifestoFinal;
        private System.Windows.Forms.TextBox txtManifestoInicial;
        private System.Windows.Forms.Label lblManifestoFinal;
        private System.Windows.Forms.Label lblManifestoInicial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblManifestos;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkExportar;
        private System.Windows.Forms.DataGridView gridManifesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mniRemoverManifesto;
    }
}