namespace Wms
{
    partial class FrmRastreador
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRastreador));
            this.lblModo = new System.Windows.Forms.Label();
            this.cmbModo = new System.Windows.Forms.ComboBox();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.txtRastreador = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRastreador = new System.Windows.Forms.Label();
            this.gridRastreador = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnuImpressao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniEtiqueta = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPesqAtivo = new System.Windows.Forms.CheckBox();
            this.lblQtd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPesqRastreador = new System.Windows.Forms.TextBox();
            this.lblPesqRastreador = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRastreador)).BeginInit();
            this.mnuImpressao.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblModo
            // 
            this.lblModo.AutoSize = true;
            this.lblModo.ForeColor = System.Drawing.Color.Black;
            this.lblModo.Location = new System.Drawing.Point(163, 84);
            this.lblModo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModo.Name = "lblModo";
            this.lblModo.Size = new System.Drawing.Size(42, 16);
            this.lblModo.TabIndex = 193;
            this.lblModo.Text = "Modo";
            // 
            // cmbModo
            // 
            this.cmbModo.BackColor = System.Drawing.Color.White;
            this.cmbModo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModo.Enabled = false;
            this.cmbModo.FormattingEnabled = true;
            this.cmbModo.Items.AddRange(new object[] {
            "EM USO",
            "EXTRAVIADO",
            "MANUTENÇÃO",
            "PERDIDO"});
            this.cmbModo.Location = new System.Drawing.Point(167, 103);
            this.cmbModo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbModo.Name = "cmbModo";
            this.cmbModo.Size = new System.Drawing.Size(160, 24);
            this.cmbModo.TabIndex = 192;
            this.cmbModo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbModo_KeyPress);
            // 
            // txtObservacao
            // 
            this.txtObservacao.BackColor = System.Drawing.Color.White;
            this.txtObservacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacao.Enabled = false;
            this.txtObservacao.Location = new System.Drawing.Point(16, 166);
            this.txtObservacao.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservacao.MaxLength = 200;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(365, 38);
            this.txtObservacao.TabIndex = 191;
            this.txtObservacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservacao_KeyPress);
            // 
            // lblObservacao
            // 
            this.lblObservacao.AutoSize = true;
            this.lblObservacao.ForeColor = System.Drawing.Color.Black;
            this.lblObservacao.Location = new System.Drawing.Point(12, 146);
            this.lblObservacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Size = new System.Drawing.Size(82, 16);
            this.lblObservacao.TabIndex = 190;
            this.lblObservacao.Text = "Observação";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Enabled = false;
            this.chkAtivo.ForeColor = System.Drawing.Color.Black;
            this.chkAtivo.Location = new System.Drawing.Point(167, 47);
            this.chkAtivo.Margin = new System.Windows.Forms.Padding(4);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(59, 20);
            this.chkAtivo.TabIndex = 189;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // txtRastreador
            // 
            this.txtRastreador.BackColor = System.Drawing.Color.White;
            this.txtRastreador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRastreador.Enabled = false;
            this.txtRastreador.Location = new System.Drawing.Point(16, 105);
            this.txtRastreador.Margin = new System.Windows.Forms.Padding(4);
            this.txtRastreador.MaxLength = 40;
            this.txtRastreador.Name = "txtRastreador";
            this.txtRastreador.Size = new System.Drawing.Size(132, 22);
            this.txtRastreador.TabIndex = 185;
            this.txtRastreador.TextChanged += new System.EventHandler(this.txtRastreador_TextChanged);
            this.txtRastreador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRastreador_KeyPress);
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(16, 47);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(132, 22);
            this.txtCodigo.TabIndex = 187;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.ForeColor = System.Drawing.Color.Black;
            this.lblCodigo.Location = new System.Drawing.Point(12, 26);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 188;
            this.lblCodigo.Text = "Código";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblModo);
            this.groupBox1.Controls.Add(this.cmbModo);
            this.groupBox1.Controls.Add(this.txtObservacao);
            this.groupBox1.Controls.Add(this.lblObservacao);
            this.groupBox1.Controls.Add(this.chkAtivo);
            this.groupBox1.Controls.Add(this.txtRastreador);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.lblRastreador);
            this.groupBox1.Controls.Add(this.lblCodigo);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(16, 172);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(393, 215);
            this.groupBox1.TabIndex = 218;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formulário";
            // 
            // lblRastreador
            // 
            this.lblRastreador.AutoSize = true;
            this.lblRastreador.ForeColor = System.Drawing.Color.Black;
            this.lblRastreador.Location = new System.Drawing.Point(12, 84);
            this.lblRastreador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRastreador.Name = "lblRastreador";
            this.lblRastreador.Size = new System.Drawing.Size(75, 16);
            this.lblRastreador.TabIndex = 186;
            this.lblRastreador.Text = "Rastreador";
            // 
            // gridRastreador
            // 
            this.gridRastreador.AllowUserToAddRows = false;
            this.gridRastreador.AllowUserToResizeColumns = false;
            this.gridRastreador.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.gridRastreador.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridRastreador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridRastreador.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridRastreador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridRastreador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRastreador.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column1,
            this.Column2,
            this.Column3});
            this.gridRastreador.ContextMenuStrip = this.mnuImpressao;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRastreador.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridRastreador.GridColor = System.Drawing.Color.Gainsboro;
            this.gridRastreador.Location = new System.Drawing.Point(8, 23);
            this.gridRastreador.Margin = new System.Windows.Forms.Padding(4);
            this.gridRastreador.MultiSelect = false;
            this.gridRastreador.Name = "gridRastreador";
            this.gridRastreador.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRastreador.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridRastreador.RowHeadersVisible = false;
            this.gridRastreador.RowHeadersWidth = 20;
            this.gridRastreador.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridRastreador.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRastreador.Size = new System.Drawing.Size(196, 458);
            this.gridRastreador.TabIndex = 83;
            this.gridRastreador.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridRastreador_KeyUp);
            this.gridRastreador.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridRastreador_MouseClick);
            this.gridRastreador.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridRastreador_MouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Código";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 3276;
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "Rastreador";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.Width = 330;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Status";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Modo";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Observação";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            this.Column3.Width = 125;
            // 
            // mnuImpressao
            // 
            this.mnuImpressao.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuImpressao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniEtiqueta});
            this.mnuImpressao.Name = "mnuImpressao";
            this.mnuImpressao.Size = new System.Drawing.Size(195, 28);
            // 
            // mniEtiqueta
            // 
            this.mniEtiqueta.Name = "mniEtiqueta";
            this.mniEtiqueta.Size = new System.Drawing.Size(194, 24);
            this.mniEtiqueta.Text = "Imprimir Etiqueta";
            this.mniEtiqueta.Click += new System.EventHandler(this.mniEtiqueta_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridRastreador);
            this.groupBox2.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox2.Location = new System.Drawing.Point(420, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(212, 489);
            this.groupBox2.TabIndex = 217;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Portáteis Cadastrados";
            // 
            // chkPesqAtivo
            // 
            this.chkPesqAtivo.AutoSize = true;
            this.chkPesqAtivo.Checked = true;
            this.chkPesqAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPesqAtivo.Location = new System.Drawing.Point(146, 85);
            this.chkPesqAtivo.Margin = new System.Windows.Forms.Padding(4);
            this.chkPesqAtivo.Name = "chkPesqAtivo";
            this.chkPesqAtivo.Size = new System.Drawing.Size(59, 20);
            this.chkPesqAtivo.TabIndex = 216;
            this.chkPesqAtivo.Text = "Ativo";
            this.chkPesqAtivo.UseVisualStyleBackColor = true;
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtd.Location = new System.Drawing.Point(91, 6);
            this.lblQtd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQtd.Name = "lblQtd";
            this.lblQtd.Size = new System.Drawing.Size(14, 16);
            this.lblQtd.TabIndex = 1;
            this.lblQtd.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 577);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 25);
            this.panel1.TabIndex = 215;
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.ForeColor = System.Drawing.SystemColors.Window;
            this.lblInformacao.Location = new System.Drawing.Point(9, 6);
            this.lblInformacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(78, 16);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Rastreador:";
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.BackColor = System.Drawing.Color.DimGray;
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNovo.Location = new System.Drawing.Point(307, 518);
            this.btnNovo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(103, 38);
            this.btnNovo.TabIndex = 211;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(532, 518);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 210;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.DimGray;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(417, 518);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(103, 38);
            this.btnSalvar.TabIndex = 209;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(146, 126);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(100, 38);
            this.btnPesquisar.TabIndex = 208;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(13, 402);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(10, 16);
            this.label15.TabIndex = 213;
            this.label15.Text = "|";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInstrucoes.AutoSize = true;
            this.lblInstrucoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucoes.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblInstrucoes.Location = new System.Drawing.Point(24, 399);
            this.lblInstrucoes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Size = new System.Drawing.Size(119, 25);
            this.lblInstrucoes.TabIndex = 212;
            this.lblInstrucoes.Text = "Informações";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 430);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.MaximumSize = new System.Drawing.Size(400, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 64);
            this.label1.TabIndex = 214;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // txtPesqRastreador
            // 
            this.txtPesqRastreador.BackColor = System.Drawing.Color.White;
            this.txtPesqRastreador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPesqRastreador.Location = new System.Drawing.Point(17, 85);
            this.txtPesqRastreador.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesqRastreador.MaxLength = 40;
            this.txtPesqRastreador.Name = "txtPesqRastreador";
            this.txtPesqRastreador.Size = new System.Drawing.Size(112, 22);
            this.txtPesqRastreador.TabIndex = 0;
            this.txtPesqRastreador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesqRastreador_KeyPress);
            // 
            // lblPesqRastreador
            // 
            this.lblPesqRastreador.AutoSize = true;
            this.lblPesqRastreador.ForeColor = System.Drawing.Color.Black;
            this.lblPesqRastreador.Location = new System.Drawing.Point(13, 66);
            this.lblPesqRastreador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPesqRastreador.Name = "lblPesqRastreador";
            this.lblPesqRastreador.Size = new System.Drawing.Size(75, 16);
            this.lblPesqRastreador.TabIndex = 207;
            this.lblPesqRastreador.Text = "Rastreador";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(7, 5);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(379, 39);
            this.label13.TabIndex = 204;
            this.label13.Text = "Cadastro de Rastreador";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(307, 66);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(92, 70);
            this.pictureBox2.TabIndex = 205;
            this.pictureBox2.TabStop = false;
            // 
            // FrmRastreador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(648, 602);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkPesqAtivo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblInstrucoes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPesqRastreador);
            this.Controls.Add(this.lblPesqRastreador);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmRastreador";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Rastreador";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRastreador)).EndInit();
            this.mnuImpressao.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.ComboBox cmbModo;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Label lblObservacao;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.TextBox txtRastreador;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblRastreador;
        private System.Windows.Forms.DataGridView gridRastreador;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkPesqAtivo;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPesqRastreador;
        private System.Windows.Forms.Label lblPesqRastreador;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ContextMenuStrip mnuImpressao;
        private System.Windows.Forms.ToolStripMenuItem mniEtiqueta;
    }
}