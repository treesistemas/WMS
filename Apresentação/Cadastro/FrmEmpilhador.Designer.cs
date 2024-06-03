namespace Wms
{
    partial class FrmEmpilhador
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmpilhador));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTurno = new System.Windows.Forms.ComboBox();
            this.cmbRuaFinal = new System.Windows.Forms.ComboBox();
            this.lblRuaFinal = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridEndereco = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbRuaInicial = new System.Windows.Forms.ComboBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.lblRuaInicial = new System.Windows.Forms.Label();
            this.cmbRegiao = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridEmpilhador = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtEmpilhador = new System.Windows.Forms.TextBox();
            this.lblRegiao = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblEmpilhador = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEndereco)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmpilhador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Location = new System.Drawing.Point(0, 559);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 25);
            this.panel1.TabIndex = 9;
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtd.Location = new System.Drawing.Point(93, 5);
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
            this.lblInformacao.Location = new System.Drawing.Point(13, 5);
            this.lblInformacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(79, 16);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Empilhador:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 262);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Turno";
            // 
            // cmbTurno
            // 
            this.cmbTurno.Enabled = false;
            this.cmbTurno.FormattingEnabled = true;
            this.cmbTurno.Items.AddRange(new object[] {
            "Selecione",
            "DIURNO",
            "NOTURNO"});
            this.cmbTurno.Location = new System.Drawing.Point(16, 282);
            this.cmbTurno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTurno.Name = "cmbTurno";
            this.cmbTurno.Size = new System.Drawing.Size(201, 24);
            this.cmbTurno.TabIndex = 17;
            this.cmbTurno.Text = "Selecione";
            // 
            // cmbRuaFinal
            // 
            this.cmbRuaFinal.DropDownWidth = 65;
            this.cmbRuaFinal.Enabled = false;
            this.cmbRuaFinal.FormattingEnabled = true;
            this.cmbRuaFinal.Location = new System.Drawing.Point(231, 220);
            this.cmbRuaFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRuaFinal.Name = "cmbRuaFinal";
            this.cmbRuaFinal.Size = new System.Drawing.Size(95, 24);
            this.cmbRuaFinal.TabIndex = 22;
            this.cmbRuaFinal.Text = "Selecione";
            this.cmbRuaFinal.SelectedIndexChanged += new System.EventHandler(this.cmbRuaFinal_SelectedIndexChanged);
            // 
            // lblRuaFinal
            // 
            this.lblRuaFinal.AutoSize = true;
            this.lblRuaFinal.Location = new System.Drawing.Point(227, 200);
            this.lblRuaFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRuaFinal.Name = "lblRuaFinal";
            this.lblRuaFinal.Size = new System.Drawing.Size(64, 16);
            this.lblRuaFinal.TabIndex = 19;
            this.lblRuaFinal.Text = "Rua Final";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridEndereco);
            this.groupBox2.Location = new System.Drawing.Point(556, 22);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(420, 302);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Responsável Por";
            // 
            // gridEndereco
            // 
            this.gridEndereco.AllowUserToAddRows = false;
            this.gridEndereco.AllowUserToResizeColumns = false;
            this.gridEndereco.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridEndereco.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridEndereco.BackgroundColor = System.Drawing.Color.White;
            this.gridEndereco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridEndereco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEndereco.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column8});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridEndereco.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridEndereco.Enabled = false;
            this.gridEndereco.GridColor = System.Drawing.Color.Gainsboro;
            this.gridEndereco.Location = new System.Drawing.Point(8, 23);
            this.gridEndereco.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridEndereco.MultiSelect = false;
            this.gridEndereco.Name = "gridEndereco";
            this.gridEndereco.ReadOnly = true;
            this.gridEndereco.RowHeadersVisible = false;
            this.gridEndereco.RowHeadersWidth = 51;
            this.gridEndereco.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridEndereco.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridEndereco.Size = new System.Drawing.Size(404, 266);
            this.gridEndereco.TabIndex = 24;
            this.gridEndereco.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridEndereco_CellMouseClick);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Código";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Região";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Rua Inícial";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Rua Final";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 125;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Posição da Lista";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Visible = false;
            this.Column8.Width = 125;
            // 
            // cmbRuaInicial
            // 
            this.cmbRuaInicial.Enabled = false;
            this.cmbRuaInicial.FormattingEnabled = true;
            this.cmbRuaInicial.Location = new System.Drawing.Point(121, 220);
            this.cmbRuaInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRuaInicial.Name = "cmbRuaInicial";
            this.cmbRuaInicial.Size = new System.Drawing.Size(96, 24);
            this.cmbRuaInicial.TabIndex = 21;
            this.cmbRuaInicial.Text = "Selecione";
            this.cmbRuaInicial.SelectedIndexChanged += new System.EventHandler(this.cmbRuaInicial_SelectedIndexChanged);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(231, 272);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(96, 38);
            this.btnPesquisar.TabIndex = 13;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // lblRuaInicial
            // 
            this.lblRuaInicial.AutoSize = true;
            this.lblRuaInicial.Location = new System.Drawing.Point(117, 200);
            this.lblRuaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRuaInicial.Name = "lblRuaInicial";
            this.lblRuaInicial.Size = new System.Drawing.Size(69, 16);
            this.lblRuaInicial.TabIndex = 17;
            this.lblRuaInicial.Text = "Rua Inicial";
            // 
            // cmbRegiao
            // 
            this.cmbRegiao.Enabled = false;
            this.cmbRegiao.FormattingEnabled = true;
            this.cmbRegiao.Location = new System.Drawing.Point(16, 220);
            this.cmbRegiao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRegiao.Name = "cmbRegiao";
            this.cmbRegiao.Size = new System.Drawing.Size(93, 24);
            this.cmbRegiao.TabIndex = 20;
            this.cmbRegiao.Text = "Selecione";
            this.cmbRegiao.SelectedIndexChanged += new System.EventHandler(this.cmbRegiao_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridEmpilhador);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(16, 332);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(505, 165);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Empilhadores Cadastrados";
            // 
            // gridEmpilhador
            // 
            this.gridEmpilhador.AllowUserToAddRows = false;
            this.gridEmpilhador.AllowUserToResizeColumns = false;
            this.gridEmpilhador.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridEmpilhador.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridEmpilhador.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridEmpilhador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridEmpilhador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEmpilhador.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column9});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridEmpilhador.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridEmpilhador.GridColor = System.Drawing.Color.Gainsboro;
            this.gridEmpilhador.Location = new System.Drawing.Point(8, 23);
            this.gridEmpilhador.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridEmpilhador.MultiSelect = false;
            this.gridEmpilhador.Name = "gridEmpilhador";
            this.gridEmpilhador.ReadOnly = true;
            this.gridEmpilhador.RowHeadersVisible = false;
            this.gridEmpilhador.RowHeadersWidth = 51;
            this.gridEmpilhador.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridEmpilhador.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridEmpilhador.Size = new System.Drawing.Size(489, 124);
            this.gridEmpilhador.TabIndex = 0;
            this.gridEmpilhador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridEmpilhador_KeyDown);
            this.gridEmpilhador.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridEmpilhador_KeyUp);
            this.gridEmpilhador.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridEmpilhador_MouseClick);
            this.gridEmpilhador.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridEmpilhador_MouseDoubleClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "IdCategoria";
            this.Column1.HeaderText = "Código";
            this.Column1.MaxInputLength = 3276;
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Descricao";
            this.Column2.HeaderText = "Empilhador";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 200;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Turno";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 160;
            // 
            // txtEmpilhador
            // 
            this.txtEmpilhador.BackColor = System.Drawing.Color.White;
            this.txtEmpilhador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpilhador.Enabled = false;
            this.txtEmpilhador.Location = new System.Drawing.Point(16, 165);
            this.txtEmpilhador.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmpilhador.MaxLength = 40;
            this.txtEmpilhador.Name = "txtEmpilhador";
            this.txtEmpilhador.Size = new System.Drawing.Size(309, 22);
            this.txtEmpilhador.TabIndex = 0;
            // 
            // lblRegiao
            // 
            this.lblRegiao.AutoSize = true;
            this.lblRegiao.Location = new System.Drawing.Point(12, 200);
            this.lblRegiao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegiao.Name = "lblRegiao";
            this.lblRegiao.Size = new System.Drawing.Size(52, 16);
            this.lblRegiao.TabIndex = 16;
            this.lblRegiao.Text = "Região";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(193, 111);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(132, 22);
            this.txtCodigo.TabIndex = 3;
            // 
            // lblEmpilhador
            // 
            this.lblEmpilhador.AutoSize = true;
            this.lblEmpilhador.Location = new System.Drawing.Point(12, 145);
            this.lblEmpilhador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpilhador.Name = "lblEmpilhador";
            this.lblEmpilhador.Size = new System.Drawing.Size(76, 16);
            this.lblEmpilhador.TabIndex = 2;
            this.lblEmpilhador.Text = "Empilhador";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(189, 90);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 4;
            this.lblCodigo.Text = "Código";
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(888, 513);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.BackColor = System.Drawing.Color.DimGray;
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNovo.Location = new System.Drawing.Point(556, 513);
            this.btnNovo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(103, 38);
            this.btnNovo.TabIndex = 10;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.DimGray;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(667, 513);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(103, 38);
            this.btnSalvar.TabIndex = 11;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemover.BackColor = System.Drawing.Color.DimGray;
            this.btnRemover.Enabled = false;
            this.btnRemover.ForeColor = System.Drawing.Color.White;
            this.btnRemover.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemover.Location = new System.Drawing.Point(777, 513);
            this.btnRemover.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(103, 38);
            this.btnRemover.TabIndex = 12;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(436, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 74);
            this.pictureBox1.TabIndex = 72;
            this.pictureBox1.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(9, 22);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(384, 39);
            this.label13.TabIndex = 73;
            this.label13.Text = "Cadastro de Empilhador";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(552, 335);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(10, 16);
            this.label15.TabIndex = 76;
            this.label15.Text = "|";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.AutoSize = true;
            this.lblInstrucoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucoes.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblInstrucoes.Location = new System.Drawing.Point(563, 331);
            this.lblInstrucoes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Size = new System.Drawing.Size(119, 25);
            this.lblInstrucoes.TabIndex = 75;
            this.lblInstrucoes.Text = "Informações";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(552, 368);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.MaximumSize = new System.Drawing.Size(423, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(423, 48);
            this.label3.TabIndex = 77;
            this.label3.Text = "O empilhador é definido através  do  cadastro  de  usuário, aqui somente é defeni" +
    "do  algumas de suas  responsabilidades como, qual empilhador fica responsável po" +
    "r qual região e ruas.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label6.Location = new System.Drawing.Point(12, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 92;
            this.label6.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(15, 110);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(153, 24);
            this.cmbEmpresa.TabIndex = 91;
            // 
            // FrmEmpilhador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(999, 582);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblInstrucoes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.cmbTurno);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.cmbRuaFinal);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.lblRuaFinal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbRuaInicial);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.lblRuaInicial);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.cmbRegiao);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtEmpilhador);
            this.Controls.Add(this.lblEmpilhador);
            this.Controls.Add(this.lblRegiao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmEmpilhador";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Empilhador";
            this.Load += new System.EventHandler(this.FrmEmpilhador_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEndereco)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEmpilhador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridEmpilhador;
        private System.Windows.Forms.TextBox txtEmpilhador;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblEmpilhador;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.ComboBox cmbRuaFinal;
        private System.Windows.Forms.ComboBox cmbRuaInicial;
        private System.Windows.Forms.ComboBox cmbRegiao;
        private System.Windows.Forms.Label lblRuaFinal;
        private System.Windows.Forms.Label lblRuaInicial;
        private System.Windows.Forms.Label lblRegiao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTurno;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.DataGridView gridEndereco;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}