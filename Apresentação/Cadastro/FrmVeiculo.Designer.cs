namespace Wms
{
    partial class FrmVeiculo
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPesqPlaca = new System.Windows.Forms.TextBox();
            this.lblPesPlaca = new System.Windows.Forms.Label();
            this.cmbTipoVeiculo = new System.Windows.Forms.ComboBox();
            this.lblTipoVeiculo = new System.Windows.Forms.Label();
            this.cmbRastreador = new System.Windows.Forms.ComboBox();
            this.lblPortatil = new System.Windows.Forms.Label();
            this.cmbProprietario = new System.Windows.Forms.ComboBox();
            this.lblProprietario = new System.Windows.Forms.Label();
            this.chkPesqAtivo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAno = new System.Windows.Forms.TextBox();
            this.lblAno = new System.Windows.Forms.Label();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.lblPeso = new System.Windows.Forms.Label();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.txtCubagem = new System.Windows.Forms.TextBox();
            this.lblCubagem = new System.Windows.Forms.Label();
            this.txtComprimento = new System.Windows.Forms.TextBox();
            this.lblComprimento = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.txtLargura = new System.Windows.Forms.TextBox();
            this.lblLargura = new System.Windows.Forms.Label();
            this.txtAltura = new System.Windows.Forms.TextBox();
            this.lblAltura = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.gridVeiculo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblPesqProprietario = new System.Windows.Forms.Label();
            this.cmbPesqProprietario = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVeiculo)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(307, 60);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(67, 57);
            this.pictureBox2.TabIndex = 163;
            this.pictureBox2.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(5, 5);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(324, 39);
            this.label13.TabIndex = 162;
            this.label13.Text = "Cadastro de Veículo";
            // 
            // txtPesqPlaca
            // 
            this.txtPesqPlaca.BackColor = System.Drawing.Color.White;
            this.txtPesqPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPesqPlaca.Location = new System.Drawing.Point(13, 129);
            this.txtPesqPlaca.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesqPlaca.MaxLength = 40;
            this.txtPesqPlaca.Name = "txtPesqPlaca";
            this.txtPesqPlaca.Size = new System.Drawing.Size(185, 22);
            this.txtPesqPlaca.TabIndex = 174;
            this.txtPesqPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesqPlaca_KeyPress);
            // 
            // lblPesPlaca
            // 
            this.lblPesPlaca.AutoSize = true;
            this.lblPesPlaca.ForeColor = System.Drawing.Color.Black;
            this.lblPesPlaca.Location = new System.Drawing.Point(9, 108);
            this.lblPesPlaca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPesPlaca.Name = "lblPesPlaca";
            this.lblPesPlaca.Size = new System.Drawing.Size(42, 16);
            this.lblPesPlaca.TabIndex = 175;
            this.lblPesPlaca.Text = "Placa";
            // 
            // cmbTipoVeiculo
            // 
            this.cmbTipoVeiculo.Enabled = false;
            this.cmbTipoVeiculo.FormattingEnabled = true;
            this.cmbTipoVeiculo.Location = new System.Drawing.Point(20, 240);
            this.cmbTipoVeiculo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTipoVeiculo.Name = "cmbTipoVeiculo";
            this.cmbTipoVeiculo.Size = new System.Drawing.Size(283, 24);
            this.cmbTipoVeiculo.TabIndex = 179;
            this.cmbTipoVeiculo.Text = "SELECIONE";
            this.cmbTipoVeiculo.SelectedIndexChanged += new System.EventHandler(this.cmbTipoVeiculo_SelectedIndexChanged);
            this.cmbTipoVeiculo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTipoVeiculo_KeyPress);
            // 
            // lblTipoVeiculo
            // 
            this.lblTipoVeiculo.AutoSize = true;
            this.lblTipoVeiculo.ForeColor = System.Drawing.Color.Black;
            this.lblTipoVeiculo.Location = new System.Drawing.Point(16, 218);
            this.lblTipoVeiculo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTipoVeiculo.Name = "lblTipoVeiculo";
            this.lblTipoVeiculo.Size = new System.Drawing.Size(102, 16);
            this.lblTipoVeiculo.TabIndex = 178;
            this.lblTipoVeiculo.Text = "Tipo de Veículo";
            // 
            // cmbRastreador
            // 
            this.cmbRastreador.Enabled = false;
            this.cmbRastreador.FormattingEnabled = true;
            this.cmbRastreador.Location = new System.Drawing.Point(217, 175);
            this.cmbRastreador.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRastreador.Name = "cmbRastreador";
            this.cmbRastreador.Size = new System.Drawing.Size(85, 24);
            this.cmbRastreador.TabIndex = 181;
            this.cmbRastreador.Text = "SELEC...";
            this.cmbRastreador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbRastreador_KeyPress);
            // 
            // lblPortatil
            // 
            this.lblPortatil.AutoSize = true;
            this.lblPortatil.ForeColor = System.Drawing.Color.Black;
            this.lblPortatil.Location = new System.Drawing.Point(213, 153);
            this.lblPortatil.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPortatil.Name = "lblPortatil";
            this.lblPortatil.Size = new System.Drawing.Size(75, 16);
            this.lblPortatil.TabIndex = 180;
            this.lblPortatil.Text = "Rastreador";
            // 
            // cmbProprietario
            // 
            this.cmbProprietario.Enabled = false;
            this.cmbProprietario.FormattingEnabled = true;
            this.cmbProprietario.Items.AddRange(new object[] {
            "EMPRESA",
            "TERCEIROS"});
            this.cmbProprietario.Location = new System.Drawing.Point(20, 175);
            this.cmbProprietario.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProprietario.Name = "cmbProprietario";
            this.cmbProprietario.Size = new System.Drawing.Size(188, 24);
            this.cmbProprietario.TabIndex = 183;
            this.cmbProprietario.Text = "SELECIONE";
            this.cmbProprietario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbProprietario_KeyPress);
            // 
            // lblProprietario
            // 
            this.lblProprietario.AutoSize = true;
            this.lblProprietario.ForeColor = System.Drawing.Color.Black;
            this.lblProprietario.Location = new System.Drawing.Point(16, 153);
            this.lblProprietario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProprietario.Name = "lblProprietario";
            this.lblProprietario.Size = new System.Drawing.Size(77, 16);
            this.lblProprietario.TabIndex = 182;
            this.lblProprietario.Text = "Proprietário";
            // 
            // chkPesqAtivo
            // 
            this.chkPesqAtivo.AutoSize = true;
            this.chkPesqAtivo.Checked = true;
            this.chkPesqAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPesqAtivo.Location = new System.Drawing.Point(212, 133);
            this.chkPesqAtivo.Margin = new System.Windows.Forms.Padding(4);
            this.chkPesqAtivo.Name = "chkPesqAtivo";
            this.chkPesqAtivo.Size = new System.Drawing.Size(59, 20);
            this.chkPesqAtivo.TabIndex = 184;
            this.chkPesqAtivo.Text = "Ativo";
            this.chkPesqAtivo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAno);
            this.groupBox1.Controls.Add(this.lblAno);
            this.groupBox1.Controls.Add(this.txtPeso);
            this.groupBox1.Controls.Add(this.lblPeso);
            this.groupBox1.Controls.Add(this.chkAtivo);
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Controls.Add(this.txtCubagem);
            this.groupBox1.Controls.Add(this.lblCubagem);
            this.groupBox1.Controls.Add(this.cmbProprietario);
            this.groupBox1.Controls.Add(this.lblProprietario);
            this.groupBox1.Controls.Add(this.txtComprimento);
            this.groupBox1.Controls.Add(this.lblComprimento);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.lblPlaca);
            this.groupBox1.Controls.Add(this.txtLargura);
            this.groupBox1.Controls.Add(this.lblLargura);
            this.groupBox1.Controls.Add(this.cmbRastreador);
            this.groupBox1.Controls.Add(this.lblPortatil);
            this.groupBox1.Controls.Add(this.txtAltura);
            this.groupBox1.Controls.Add(this.lblAltura);
            this.groupBox1.Controls.Add(this.lblCodigo);
            this.groupBox1.Controls.Add(this.cmbTipoVeiculo);
            this.groupBox1.Controls.Add(this.lblTipoVeiculo);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(396, 23);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(361, 465);
            this.groupBox1.TabIndex = 185;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formulário";
            // 
            // txtAno
            // 
            this.txtAno.BackColor = System.Drawing.Color.White;
            this.txtAno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAno.Enabled = false;
            this.txtAno.Location = new System.Drawing.Point(171, 116);
            this.txtAno.Margin = new System.Windows.Forms.Padding(4);
            this.txtAno.MaxLength = 40;
            this.txtAno.Name = "txtAno";
            this.txtAno.Size = new System.Drawing.Size(132, 22);
            this.txtAno.TabIndex = 196;
            // 
            // lblAno
            // 
            this.lblAno.AutoSize = true;
            this.lblAno.ForeColor = System.Drawing.Color.Black;
            this.lblAno.Location = new System.Drawing.Point(167, 95);
            this.lblAno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAno.Name = "lblAno";
            this.lblAno.Size = new System.Drawing.Size(31, 16);
            this.lblAno.TabIndex = 197;
            this.lblAno.Text = "Ano";
            // 
            // txtPeso
            // 
            this.txtPeso.BackColor = System.Drawing.Color.White;
            this.txtPeso.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPeso.Enabled = false;
            this.txtPeso.Location = new System.Drawing.Point(20, 416);
            this.txtPeso.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeso.MaxLength = 40;
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Size = new System.Drawing.Size(132, 22);
            this.txtPeso.TabIndex = 194;
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.ForeColor = System.Drawing.Color.Black;
            this.lblPeso.Location = new System.Drawing.Point(16, 396);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(66, 16);
            this.lblPeso.TabIndex = 195;
            this.lblPeso.Text = "Peso (Kg)";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Enabled = false;
            this.chkAtivo.ForeColor = System.Drawing.Color.Black;
            this.chkAtivo.Location = new System.Drawing.Point(171, 58);
            this.chkAtivo.Margin = new System.Windows.Forms.Padding(4);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(59, 20);
            this.chkAtivo.TabIndex = 189;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.Color.White;
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Enabled = false;
            this.txtPlaca.Location = new System.Drawing.Point(20, 116);
            this.txtPlaca.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlaca.MaxLength = 40;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(132, 22);
            this.txtPlaca.TabIndex = 185;
            // 
            // txtCubagem
            // 
            this.txtCubagem.BackColor = System.Drawing.Color.White;
            this.txtCubagem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCubagem.Enabled = false;
            this.txtCubagem.Location = new System.Drawing.Point(171, 363);
            this.txtCubagem.Margin = new System.Windows.Forms.Padding(4);
            this.txtCubagem.MaxLength = 40;
            this.txtCubagem.Name = "txtCubagem";
            this.txtCubagem.Size = new System.Drawing.Size(132, 22);
            this.txtCubagem.TabIndex = 192;
            // 
            // lblCubagem
            // 
            this.lblCubagem.AutoSize = true;
            this.lblCubagem.ForeColor = System.Drawing.Color.Black;
            this.lblCubagem.Location = new System.Drawing.Point(167, 343);
            this.lblCubagem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCubagem.Name = "lblCubagem";
            this.lblCubagem.Size = new System.Drawing.Size(84, 16);
            this.lblCubagem.TabIndex = 193;
            this.lblCubagem.Text = "Cubagem m³";
            // 
            // txtComprimento
            // 
            this.txtComprimento.BackColor = System.Drawing.Color.White;
            this.txtComprimento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComprimento.Enabled = false;
            this.txtComprimento.Location = new System.Drawing.Point(20, 363);
            this.txtComprimento.Margin = new System.Windows.Forms.Padding(4);
            this.txtComprimento.MaxLength = 40;
            this.txtComprimento.Name = "txtComprimento";
            this.txtComprimento.Size = new System.Drawing.Size(132, 22);
            this.txtComprimento.TabIndex = 190;
            // 
            // lblComprimento
            // 
            this.lblComprimento.AutoSize = true;
            this.lblComprimento.ForeColor = System.Drawing.Color.Black;
            this.lblComprimento.Location = new System.Drawing.Point(17, 343);
            this.lblComprimento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComprimento.Name = "lblComprimento";
            this.lblComprimento.Size = new System.Drawing.Size(109, 16);
            this.lblComprimento.TabIndex = 191;
            this.lblComprimento.Text = "Comprimento (m)";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(20, 58);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(132, 22);
            this.txtCodigo.TabIndex = 187;
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.ForeColor = System.Drawing.Color.Black;
            this.lblPlaca.Location = new System.Drawing.Point(16, 95);
            this.lblPlaca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(42, 16);
            this.lblPlaca.TabIndex = 186;
            this.lblPlaca.Text = "Placa";
            // 
            // txtLargura
            // 
            this.txtLargura.BackColor = System.Drawing.Color.White;
            this.txtLargura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLargura.Enabled = false;
            this.txtLargura.Location = new System.Drawing.Point(171, 299);
            this.txtLargura.Margin = new System.Windows.Forms.Padding(4);
            this.txtLargura.MaxLength = 40;
            this.txtLargura.Name = "txtLargura";
            this.txtLargura.Size = new System.Drawing.Size(132, 22);
            this.txtLargura.TabIndex = 188;
            // 
            // lblLargura
            // 
            this.lblLargura.AutoSize = true;
            this.lblLargura.ForeColor = System.Drawing.Color.Black;
            this.lblLargura.Location = new System.Drawing.Point(167, 279);
            this.lblLargura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLargura.Name = "lblLargura";
            this.lblLargura.Size = new System.Drawing.Size(75, 16);
            this.lblLargura.TabIndex = 189;
            this.lblLargura.Text = "Largura (m)";
            // 
            // txtAltura
            // 
            this.txtAltura.BackColor = System.Drawing.Color.White;
            this.txtAltura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAltura.Enabled = false;
            this.txtAltura.Location = new System.Drawing.Point(20, 299);
            this.txtAltura.Margin = new System.Windows.Forms.Padding(4);
            this.txtAltura.MaxLength = 40;
            this.txtAltura.Name = "txtAltura";
            this.txtAltura.Size = new System.Drawing.Size(132, 22);
            this.txtAltura.TabIndex = 186;
            // 
            // lblAltura
            // 
            this.lblAltura.AutoSize = true;
            this.lblAltura.ForeColor = System.Drawing.Color.Black;
            this.lblAltura.Location = new System.Drawing.Point(16, 279);
            this.lblAltura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(63, 16);
            this.lblAltura.TabIndex = 187;
            this.lblAltura.Text = "Altura (m)";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.ForeColor = System.Drawing.Color.Black;
            this.lblCodigo.Location = new System.Drawing.Point(16, 37);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 188;
            this.lblCodigo.Text = "Código";
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(212, 168);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(100, 38);
            this.btnPesquisar.TabIndex = 186;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // gridVeiculo
            // 
            this.gridVeiculo.AllowUserToAddRows = false;
            this.gridVeiculo.AllowUserToResizeColumns = false;
            this.gridVeiculo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridVeiculo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridVeiculo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridVeiculo.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridVeiculo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridVeiculo.ColumnHeadersHeight = 29;
            this.gridVeiculo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridVeiculo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column12,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            this.gridVeiculo.GridColor = System.Drawing.Color.Gainsboro;
            this.gridVeiculo.Location = new System.Drawing.Point(12, 31);
            this.gridVeiculo.Margin = new System.Windows.Forms.Padding(4);
            this.gridVeiculo.MultiSelect = false;
            this.gridVeiculo.Name = "gridVeiculo";
            this.gridVeiculo.ReadOnly = true;
            this.gridVeiculo.RowHeadersVisible = false;
            this.gridVeiculo.RowHeadersWidth = 51;
            this.gridVeiculo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridVeiculo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridVeiculo.Size = new System.Drawing.Size(348, 142);
            this.gridVeiculo.TabIndex = 187;
            this.gridVeiculo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridVeiculo_KeyUp);
            this.gridVeiculo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridVeiculo_MouseClick);
            this.gridVeiculo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridVeiculo_MouseDoubleClick);
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Código";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // Column3
            // 
            this.Column3.Frozen = true;
            this.Column3.HeaderText = "Placa";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Ano";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Visible = false;
            this.Column12.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Proprietário";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Rastreador";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Tipo";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Altura";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            this.Column6.Width = 125;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Largura";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            this.Column7.Width = 125;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Comprimento";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Visible = false;
            this.Column8.Width = 125;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Cubagem";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            this.Column9.Width = 125;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Peso";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            this.Column10.Width = 125;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Status";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Visible = false;
            this.Column11.Width = 125;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 410);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(10, 16);
            this.label15.TabIndex = 189;
            this.label15.Text = "|";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInstrucoes.AutoSize = true;
            this.lblInstrucoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucoes.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblInstrucoes.Location = new System.Drawing.Point(20, 406);
            this.lblInstrucoes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Size = new System.Drawing.Size(119, 25);
            this.lblInstrucoes.TabIndex = 188;
            this.lblInstrucoes.Text = "Informações";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 438);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.MaximumSize = new System.Drawing.Size(400, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(399, 48);
            this.label5.TabIndex = 190;
            this.label5.Text = "Aqui  mantemos  um  controle  sobre  a  frota de veículos, é importante vincular " +
    "às informações necessárias para conhe- cer o veículo e as despesas futuras com o" +
    " mesmo.";
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(657, 498);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 192;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.DimGray;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(543, 496);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(103, 38);
            this.btnSalvar.TabIndex = 191;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 542);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(773, 25);
            this.panel1.TabIndex = 193;
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtd.Location = new System.Drawing.Point(77, 6);
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
            this.lblInformacao.Location = new System.Drawing.Point(9, 6);
            this.lblInformacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(62, 16);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Veículos:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridVeiculo);
            this.groupBox2.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox2.Location = new System.Drawing.Point(13, 219);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(375, 183);
            this.groupBox2.TabIndex = 194;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Veículos Cadastrados";
            // 
            // lblPesqProprietario
            // 
            this.lblPesqProprietario.AutoSize = true;
            this.lblPesqProprietario.ForeColor = System.Drawing.Color.Black;
            this.lblPesqProprietario.Location = new System.Drawing.Point(9, 161);
            this.lblPesqProprietario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPesqProprietario.Name = "lblPesqProprietario";
            this.lblPesqProprietario.Size = new System.Drawing.Size(77, 16);
            this.lblPesqProprietario.TabIndex = 196;
            this.lblPesqProprietario.Text = "Proprietário";
            // 
            // cmbPesqProprietario
            // 
            this.cmbPesqProprietario.FormattingEnabled = true;
            this.cmbPesqProprietario.Items.AddRange(new object[] {
            "",
            "EMPRESA",
            "TERCEIROS"});
            this.cmbPesqProprietario.Location = new System.Drawing.Point(13, 181);
            this.cmbPesqProprietario.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPesqProprietario.Name = "cmbPesqProprietario";
            this.cmbPesqProprietario.Size = new System.Drawing.Size(185, 24);
            this.cmbPesqProprietario.TabIndex = 197;
            this.cmbPesqProprietario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPesqProprietario_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label6.Location = new System.Drawing.Point(9, 57);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 199;
            this.label6.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(13, 76);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(185, 24);
            this.cmbEmpresa.TabIndex = 198;
            // 
            // FrmVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(773, 567);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.cmbPesqProprietario);
            this.Controls.Add(this.lblPesqProprietario);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblInstrucoes);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkPesqAtivo);
            this.Controls.Add(this.txtPesqPlaca);
            this.Controls.Add(this.lblPesPlaca);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmVeiculo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Veículo";
            this.Load += new System.EventHandler(this.FrmVeiculo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVeiculo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPesqPlaca;
        private System.Windows.Forms.Label lblPesPlaca;
        private System.Windows.Forms.ComboBox cmbTipoVeiculo;
        private System.Windows.Forms.Label lblTipoVeiculo;
        private System.Windows.Forms.ComboBox cmbRastreador;
        private System.Windows.Forms.Label lblPortatil;
        private System.Windows.Forms.ComboBox cmbProprietario;
        private System.Windows.Forms.Label lblProprietario;
        private System.Windows.Forms.CheckBox chkPesqAtivo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.TextBox txtCubagem;
        private System.Windows.Forms.Label lblCubagem;
        private System.Windows.Forms.TextBox txtComprimento;
        private System.Windows.Forms.Label lblComprimento;
        private System.Windows.Forms.TextBox txtLargura;
        private System.Windows.Forms.Label lblLargura;
        private System.Windows.Forms.TextBox txtAltura;
        private System.Windows.Forms.Label lblAltura;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridView gridVeiculo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAno;
        private System.Windows.Forms.Label lblAno;
        private System.Windows.Forms.Label lblPesqProprietario;
        private System.Windows.Forms.ComboBox cmbPesqProprietario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}