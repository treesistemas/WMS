
namespace Wms
{
    partial class FrmReserva
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigoAbastecimento = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTipoReserva = new System.Windows.Forms.ComboBox();
            this.dtmInicial = new System.Windows.Forms.DateTimePicker();
            this.dtmFinal = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.gridItens = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQtdCancelados = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblQtdPendentes = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblQtdFinalizados = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.btnTransferir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnLiberar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblProcesso = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chartAbastecimento = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbEstacao = new System.Windows.Forms.ComboBox();
            this.txtPedido = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAbastecimento)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 124);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ordem de Abastecimento";
            // 
            // txtCodigoAbastecimento
            // 
            this.txtCodigoAbastecimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtCodigoAbastecimento.Location = new System.Drawing.Point(21, 144);
            this.txtCodigoAbastecimento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigoAbastecimento.Name = "txtCodigoAbastecimento";
            this.txtCodigoAbastecimento.Size = new System.Drawing.Size(163, 23);
            this.txtCodigoAbastecimento.TabIndex = 1;
            this.txtCodigoAbastecimento.TextChanged += new System.EventHandler(this.txtCodigoAbastecimento_TextChanged);
            this.txtCodigoAbastecimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrdemAbastecimento_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 233);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Período";
            // 
            // cmbTipoReserva
            // 
            this.cmbTipoReserva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbTipoReserva.FormattingEnabled = true;
            this.cmbTipoReserva.Items.AddRange(new object[] {
            "ABASTECIMENTO",
            "RESERVADOS"});
            this.cmbTipoReserva.Location = new System.Drawing.Point(21, 197);
            this.cmbTipoReserva.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTipoReserva.Name = "cmbTipoReserva";
            this.cmbTipoReserva.Size = new System.Drawing.Size(163, 25);
            this.cmbTipoReserva.TabIndex = 3;
            this.cmbTipoReserva.Text = "SELECIONE";
            this.cmbTipoReserva.SelectedIndexChanged += new System.EventHandler(this.cmbTipoReserva_SelectedIndexChanged);
            this.cmbTipoReserva.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTipoReserva_KeyPress);
            // 
            // dtmInicial
            // 
            this.dtmInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtmInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmInicial.Location = new System.Drawing.Point(23, 252);
            this.dtmInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtmInicial.Name = "dtmInicial";
            this.dtmInicial.Size = new System.Drawing.Size(159, 23);
            this.dtmInicial.TabIndex = 4;
            this.dtmInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtmInicial_KeyPress);
            // 
            // dtmFinal
            // 
            this.dtmFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtmFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmFinal.Location = new System.Drawing.Point(199, 252);
            this.dtmFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtmFinal.Name = "dtmFinal";
            this.dtmFinal.Size = new System.Drawing.Size(141, 23);
            this.dtmFinal.TabIndex = 5;
            this.dtmFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtmFinal_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 177);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tipo de Reserva";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(12, 11);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(336, 39);
            this.label13.TabIndex = 60;
            this.label13.Text = "Reserva de Produtos";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(379, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 74);
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(241, 294);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(100, 38);
            this.btnPesquisar.TabIndex = 58;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // gridItens
            // 
            this.gridItens.AllowUserToAddRows = false;
            this.gridItens.AllowUserToResizeRows = false;
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridItens.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle37;
            this.gridItens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridItens.BackgroundColor = System.Drawing.Color.White;
            this.gridItens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridItens.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.SlateGray;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItens.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle38;
            this.gridItens.ColumnHeadersHeight = 28;
            this.gridItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10,
            this.colOA,
            this.colPedido,
            this.colStatus,
            this.Column1,
            this.Column7,
            this.Column8,
            this.Column15,
            this.Column13,
            this.Column12,
            this.Column16,
            this.Column14,
            this.Column2,
            this.dataGridViewTextBoxColumn5,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column11,
            this.Column17,
            this.Column6});
            this.gridItens.EnableHeadersVisualStyles = false;
            this.gridItens.GridColor = System.Drawing.Color.Gainsboro;
            this.gridItens.Location = new System.Drawing.Point(21, 356);
            this.gridItens.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridItens.Name = "gridItens";
            this.gridItens.ReadOnly = true;
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle44.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle44.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle44.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle44.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItens.RowHeadersDefaultCellStyle = dataGridViewCellStyle44;
            this.gridItens.RowHeadersVisible = false;
            this.gridItens.RowHeadersWidth = 20;
            dataGridViewCellStyle45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridItens.RowsDefaultCellStyle = dataGridViewCellStyle45;
            this.gridItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridItens.Size = new System.Drawing.Size(1139, 217);
            this.gridItens.TabIndex = 118;
            // 
            // Column9
            // 
            this.Column9.Frozen = true;
            this.Column9.HeaderText = "Nº";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 40;
            // 
            // Column10
            // 
            this.Column10.Frozen = true;
            this.Column10.HeaderText = "Reservado";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 120;
            // 
            // colOA
            // 
            this.colOA.Frozen = true;
            this.colOA.HeaderText = "O. A.";
            this.colOA.MinimumWidth = 6;
            this.colOA.Name = "colOA";
            this.colOA.ReadOnly = true;
            this.colOA.Width = 80;
            // 
            // colPedido
            // 
            this.colPedido.Frozen = true;
            this.colPedido.HeaderText = "Pedido";
            this.colPedido.MinimumWidth = 6;
            this.colPedido.Name = "colPedido";
            this.colPedido.ReadOnly = true;
            this.colPedido.Width = 70;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 85;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Tipo";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 110;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "CODIGO RESERVA";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            this.Column7.Width = 125;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "CODIGO PICKING";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Visible = false;
            this.Column8.Width = 125;
            // 
            // Column15
            // 
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column15.DefaultCellStyle = dataGridViewCellStyle39;
            this.Column15.HeaderText = "Picking";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 80;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "ID PRODUTO";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            this.Column13.Width = 125;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Produto";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 250;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "FATOR PULMÃO";
            this.Column16.MinimumWidth = 6;
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Visible = false;
            this.Column16.Width = 125;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "CODIGO PULMAO";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Visible = false;
            this.Column14.Width = 125;
            // 
            // Column2
            // 
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle40;
            this.Column2.HeaderText = "Pulmão";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle41.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle41;
            this.dataGridViewTextBoxColumn5.FillWeight = 80F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Qtd";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 3276;
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Fator";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 45;
            // 
            // Column4
            // 
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle42;
            this.Column4.HeaderText = "Vencimento";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 80;
            // 
            // Column5
            // 
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle43;
            this.Column5.HeaderText = "Peso";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 80;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Lote";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 125;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "TIPO DE ANÁLISE";
            this.Column17.MinimumWidth = 6;
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Visible = false;
            this.Column17.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Usuário";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 125;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQtdCancelados);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lblQtdPendentes);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblQtdFinalizados);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 637);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1312, 25);
            this.panel1.TabIndex = 119;
            // 
            // lblQtdCancelados
            // 
            this.lblQtdCancelados.AutoSize = true;
            this.lblQtdCancelados.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtdCancelados.Location = new System.Drawing.Point(607, 5);
            this.lblQtdCancelados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQtdCancelados.Name = "lblQtdCancelados";
            this.lblQtdCancelados.Size = new System.Drawing.Size(14, 16);
            this.lblQtdCancelados.TabIndex = 7;
            this.lblQtdCancelados.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.Window;
            this.label10.Location = new System.Drawing.Point(523, 5);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 16);
            this.label10.TabIndex = 6;
            this.label10.Text = "Cancelados:";
            // 
            // lblQtdPendentes
            // 
            this.lblQtdPendentes.AutoSize = true;
            this.lblQtdPendentes.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtdPendentes.Location = new System.Drawing.Point(279, 5);
            this.lblQtdPendentes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQtdPendentes.Name = "lblQtdPendentes";
            this.lblQtdPendentes.Size = new System.Drawing.Size(14, 16);
            this.lblQtdPendentes.TabIndex = 5;
            this.lblQtdPendentes.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.Window;
            this.label8.Location = new System.Drawing.Point(195, 5);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Pendentes:";
            // 
            // lblQtdFinalizados
            // 
            this.lblQtdFinalizados.AutoSize = true;
            this.lblQtdFinalizados.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtdFinalizados.Location = new System.Drawing.Point(449, 5);
            this.lblQtdFinalizados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQtdFinalizados.Name = "lblQtdFinalizados";
            this.lblQtdFinalizados.Size = new System.Drawing.Size(14, 16);
            this.lblQtdFinalizados.TabIndex = 3;
            this.lblQtdFinalizados.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Window;
            this.label6.Location = new System.Drawing.Point(365, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Finalizados:";
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtd.Location = new System.Drawing.Point(123, 5);
            this.lblQtd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQtd.Name = "lblQtd";
            this.lblQtd.Size = new System.Drawing.Size(14, 16);
            this.lblQtd.TabIndex = 1;
            this.lblQtd.Text = "0";
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.ForeColor = System.Drawing.SystemColors.Window;
            this.lblInformacao.Location = new System.Drawing.Point(17, 5);
            this.lblInformacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(100, 16);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Abastecimento:";
            // 
            // btnTransferir
            // 
            this.btnTransferir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransferir.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnTransferir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransferir.ForeColor = System.Drawing.Color.White;
            this.btnTransferir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransferir.Location = new System.Drawing.Point(1168, 415);
            this.btnTransferir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTransferir.Name = "btnTransferir";
            this.btnTransferir.Size = new System.Drawing.Size(131, 43);
            this.btnTransferir.TabIndex = 121;
            this.btnTransferir.Text = "Transferir";
            this.btnTransferir.UseVisualStyleBackColor = false;
            this.btnTransferir.Click += new System.EventHandler(this.btnTransferir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.Tomato;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(1168, 474);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(131, 43);
            this.btnCancelar.TabIndex = 122;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(1168, 529);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(131, 43);
            this.btnSair.TabIndex = 123;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnLiberar
            // 
            this.btnLiberar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLiberar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLiberar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLiberar.ForeColor = System.Drawing.Color.White;
            this.btnLiberar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLiberar.Location = new System.Drawing.Point(1168, 356);
            this.btnLiberar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLiberar.Name = "btnLiberar";
            this.btnLiberar.Size = new System.Drawing.Size(131, 43);
            this.btnLiberar.TabIndex = 120;
            this.btnLiberar.Text = "Liberar";
            this.btnLiberar.UseVisualStyleBackColor = false;
            this.btnLiberar.Click += new System.EventHandler(this.btnLiberar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 124);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 125;
            this.label4.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "CANCELADO",
            "PENDENTE",
            "FINALIZADO"});
            this.cmbStatus.Location = new System.Drawing.Point(201, 142);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(139, 25);
            this.cmbStatus.TabIndex = 124;
            this.cmbStatus.Text = "SELECIONE";
            // 
            // lblProcesso
            // 
            this.lblProcesso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProcesso.ForeColor = System.Drawing.Color.Black;
            this.lblProcesso.Location = new System.Drawing.Point(16, 582);
            this.lblProcesso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProcesso.MaximumSize = new System.Drawing.Size(400, 492);
            this.lblProcesso.Name = "lblProcesso";
            this.lblProcesso.Size = new System.Drawing.Size(400, 16);
            this.lblProcesso.TabIndex = 179;
            this.lblProcesso.Text = "-";
            this.lblProcesso.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(20, 602);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(407, 28);
            this.progressBar1.TabIndex = 178;
            // 
            // chartAbastecimento
            // 
            chartArea5.Name = "ChartArea1";
            this.chartAbastecimento.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartAbastecimento.Legends.Add(legend5);
            this.chartAbastecimento.Location = new System.Drawing.Point(672, 15);
            this.chartAbastecimento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartAbastecimento.Name = "chartAbastecimento";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series5.Legend = "Legend1";
            series5.Name = "Abastecimento";
            this.chartAbastecimento.Series.Add(series5);
            this.chartAbastecimento.Size = new System.Drawing.Size(488, 273);
            this.chartAbastecimento.TabIndex = 180;
            this.chartAbastecimento.Text = "chart1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 177);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 182;
            this.label5.Text = "Estação";
            // 
            // cmbEstacao
            // 
            this.cmbEstacao.Enabled = false;
            this.cmbEstacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbEstacao.FormattingEnabled = true;
            this.cmbEstacao.Location = new System.Drawing.Point(199, 197);
            this.cmbEstacao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbEstacao.Name = "cmbEstacao";
            this.cmbEstacao.Size = new System.Drawing.Size(141, 25);
            this.cmbEstacao.TabIndex = 181;
            this.cmbEstacao.Text = "SELECIONE";
            this.cmbEstacao.Click += new System.EventHandler(this.cmbEstacao_Click);
            // 
            // txtPedido
            // 
            this.txtPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPedido.Location = new System.Drawing.Point(199, 89);
            this.txtPedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.Size = new System.Drawing.Size(141, 23);
            this.txtPedido.TabIndex = 184;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 70);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 16);
            this.label7.TabIndex = 183;
            this.label7.Text = "Pedido";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label9.Location = new System.Drawing.Point(22, 68);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 17);
            this.label9.TabIndex = 186;
            this.label9.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(23, 88);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(159, 25);
            this.cmbEmpresa.TabIndex = 185;
            // 
            // FrmReserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1312, 662);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.txtPedido);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbEstacao);
            this.Controls.Add(this.chartAbastecimento);
            this.Controls.Add(this.lblProcesso);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnTransferir);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnLiberar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gridItens);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtmFinal);
            this.Controls.Add(this.dtmInicial);
            this.Controls.Add(this.cmbTipoReserva);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigoAbastecimento);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmReserva";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reserva de Produtos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmReseva_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAbastecimento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodigoAbastecimento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTipoReserva;
        private System.Windows.Forms.DateTimePicker dtmInicial;
        private System.Windows.Forms.DateTimePicker dtmFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridView gridItens;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnLiberar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblProcesso;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblQtdPendentes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblQtdFinalizados;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblQtdCancelados;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAbastecimento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbEstacao;
        private System.Windows.Forms.TextBox txtPedido;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}