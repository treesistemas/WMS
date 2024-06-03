namespace Wms
{
    partial class FrmMotorista
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.txtPesqApelido = new System.Windows.Forms.TextBox();
            this.lblPesqApelido = new System.Windows.Forms.Label();
            this.txtPesqCodigo = new System.Windows.Forms.TextBox();
            this.lblPesqCodigo = new System.Windows.Forms.Label();
            this.chkPesqAtivo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridMotorista = new System.Windows.Forms.DataGridView();
            this.ColCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditaFlow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colControlaValidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colControlaLote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtValidade = new System.Windows.Forms.TextBox();
            this.lblValidade = new System.Windows.Forms.Label();
            this.txtCNH = new System.Windows.Forms.TextBox();
            this.lblCNH = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.txtCelular = new System.Windows.Forms.MaskedTextBox();
            this.lblCelular = new System.Windows.Forms.Label();
            this.txtApelido = new System.Windows.Forms.TextBox();
            this.lblApelido = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMotorista)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(6, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(281, 31);
            this.label13.TabIndex = 59;
            this.label13.Text = "Cadastro de Motorista";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(296, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 60);
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(169, 197);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(75, 31);
            this.btnPesquisar.TabIndex = 63;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(687, 386);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 31);
            this.btnSair.TabIndex = 62;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 427);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 20);
            this.panel1.TabIndex = 64;
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtd.Location = new System.Drawing.Point(62, 5);
            this.lblQtd.Name = "lblQtd";
            this.lblQtd.Size = new System.Drawing.Size(13, 13);
            this.lblQtd.TabIndex = 1;
            this.lblQtd.Text = "0";
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.ForeColor = System.Drawing.SystemColors.Window;
            this.lblInformacao.Location = new System.Drawing.Point(7, 5);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(53, 13);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Motorista:";
            // 
            // txtPesqApelido
            // 
            this.txtPesqApelido.BackColor = System.Drawing.Color.White;
            this.txtPesqApelido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPesqApelido.Location = new System.Drawing.Point(13, 162);
            this.txtPesqApelido.MaxLength = 40;
            this.txtPesqApelido.Name = "txtPesqApelido";
            this.txtPesqApelido.Size = new System.Drawing.Size(231, 20);
            this.txtPesqApelido.TabIndex = 65;
            this.txtPesqApelido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesqApelido_KeyPress);
            // 
            // lblPesqApelido
            // 
            this.lblPesqApelido.AutoSize = true;
            this.lblPesqApelido.Location = new System.Drawing.Point(10, 147);
            this.lblPesqApelido.Name = "lblPesqApelido";
            this.lblPesqApelido.Size = new System.Drawing.Size(35, 13);
            this.lblPesqApelido.TabIndex = 66;
            this.lblPesqApelido.Text = "Nome";
            // 
            // txtPesqCodigo
            // 
            this.txtPesqCodigo.BackColor = System.Drawing.Color.White;
            this.txtPesqCodigo.Location = new System.Drawing.Point(13, 114);
            this.txtPesqCodigo.Name = "txtPesqCodigo";
            this.txtPesqCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtPesqCodigo.TabIndex = 0;
            this.txtPesqCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesqCodigo_KeyPress);
            // 
            // lblPesqCodigo
            // 
            this.lblPesqCodigo.AutoSize = true;
            this.lblPesqCodigo.Location = new System.Drawing.Point(10, 97);
            this.lblPesqCodigo.Name = "lblPesqCodigo";
            this.lblPesqCodigo.Size = new System.Drawing.Size(40, 13);
            this.lblPesqCodigo.TabIndex = 68;
            this.lblPesqCodigo.Text = "Código";
            // 
            // chkPesqAtivo
            // 
            this.chkPesqAtivo.AutoSize = true;
            this.chkPesqAtivo.Checked = true;
            this.chkPesqAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPesqAtivo.Location = new System.Drawing.Point(136, 116);
            this.chkPesqAtivo.Name = "chkPesqAtivo";
            this.chkPesqAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkPesqAtivo.TabIndex = 69;
            this.chkPesqAtivo.Text = "Ativo";
            this.chkPesqAtivo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridMotorista);
            this.groupBox1.Location = new System.Drawing.Point(13, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 142);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motorista Cadastrados";
            // 
            // gridMotorista
            // 
            this.gridMotorista.AllowUserToAddRows = false;
            this.gridMotorista.AllowUserToResizeColumns = false;
            this.gridMotorista.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridMotorista.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridMotorista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridMotorista.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridMotorista.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridMotorista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMotorista.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodigo,
            this.ColDescricao,
            this.colAuditaFlow,
            this.Column1,
            this.Column2,
            this.colControlaValidade,
            this.colControlaLote});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridMotorista.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridMotorista.GridColor = System.Drawing.Color.Gainsboro;
            this.gridMotorista.Location = new System.Drawing.Point(6, 19);
            this.gridMotorista.MultiSelect = false;
            this.gridMotorista.Name = "gridMotorista";
            this.gridMotorista.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridMotorista.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridMotorista.RowHeadersVisible = false;
            this.gridMotorista.RowHeadersWidth = 20;
            this.gridMotorista.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridMotorista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMotorista.Size = new System.Drawing.Size(428, 117);
            this.gridMotorista.TabIndex = 1;
            this.gridMotorista.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridMotorista_KeyUp);
            this.gridMotorista.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridMotorista_MouseClick);
            // 
            // ColCodigo
            // 
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.ColCodigo.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColCodigo.Frozen = true;
            this.ColCodigo.HeaderText = "Código";
            this.ColCodigo.MaxInputLength = 3276;
            this.ColCodigo.MinimumWidth = 6;
            this.ColCodigo.Name = "ColCodigo";
            this.ColCodigo.ReadOnly = true;
            this.ColCodigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColCodigo.Width = 60;
            // 
            // ColDescricao
            // 
            this.ColDescricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.ColDescricao.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColDescricao.Frozen = true;
            this.ColDescricao.HeaderText = "Nome";
            this.ColDescricao.MinimumWidth = 6;
            this.ColDescricao.Name = "ColDescricao";
            this.ColDescricao.ReadOnly = true;
            this.ColDescricao.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColDescricao.Width = 230;
            // 
            // colAuditaFlow
            // 
            this.colAuditaFlow.Frozen = true;
            this.colAuditaFlow.HeaderText = "Apelido";
            this.colAuditaFlow.MinimumWidth = 6;
            this.colAuditaFlow.Name = "colAuditaFlow";
            this.colAuditaFlow.ReadOnly = true;
            this.colAuditaFlow.Width = 135;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "CNH";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Validade";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 125;
            // 
            // colControlaValidade
            // 
            this.colControlaValidade.HeaderText = "Celular";
            this.colControlaValidade.MinimumWidth = 6;
            this.colControlaValidade.Name = "colControlaValidade";
            this.colControlaValidade.ReadOnly = true;
            this.colControlaValidade.Visible = false;
            this.colControlaValidade.Width = 105;
            // 
            // colControlaLote
            // 
            this.colControlaLote.HeaderText = "Status";
            this.colControlaLote.MinimumWidth = 6;
            this.colControlaLote.Name = "colControlaLote";
            this.colControlaLote.ReadOnly = true;
            this.colControlaLote.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colControlaLote.Visible = false;
            this.colControlaLote.Width = 80;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtValidade);
            this.groupBox2.Controls.Add(this.lblValidade);
            this.groupBox2.Controls.Add(this.txtCNH);
            this.groupBox2.Controls.Add(this.lblCNH);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.lblInstrucoes);
            this.groupBox2.Controls.Add(this.chkAtivo);
            this.groupBox2.Controls.Add(this.txtCelular);
            this.groupBox2.Controls.Add(this.lblCelular);
            this.groupBox2.Controls.Add(this.txtApelido);
            this.groupBox2.Controls.Add(this.lblApelido);
            this.groupBox2.Controls.Add(this.txtNome);
            this.groupBox2.Controls.Add(this.lblNome);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.lblCodigo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(458, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 370);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Formulário";
            // 
            // txtValidade
            // 
            this.txtValidade.BackColor = System.Drawing.Color.White;
            this.txtValidade.Enabled = false;
            this.txtValidade.Location = new System.Drawing.Point(9, 188);
            this.txtValidade.Name = "txtValidade";
            this.txtValidade.Size = new System.Drawing.Size(132, 20);
            this.txtValidade.TabIndex = 85;
            // 
            // lblValidade
            // 
            this.lblValidade.AutoSize = true;
            this.lblValidade.ForeColor = System.Drawing.Color.Black;
            this.lblValidade.Location = new System.Drawing.Point(6, 171);
            this.lblValidade.Name = "lblValidade";
            this.lblValidade.Size = new System.Drawing.Size(48, 13);
            this.lblValidade.TabIndex = 86;
            this.lblValidade.Text = "Validade";
            // 
            // txtCNH
            // 
            this.txtCNH.BackColor = System.Drawing.Color.White;
            this.txtCNH.Enabled = false;
            this.txtCNH.Location = new System.Drawing.Point(154, 138);
            this.txtCNH.Name = "txtCNH";
            this.txtCNH.Size = new System.Drawing.Size(132, 20);
            this.txtCNH.TabIndex = 83;
            // 
            // lblCNH
            // 
            this.lblCNH.AutoSize = true;
            this.lblCNH.ForeColor = System.Drawing.Color.Black;
            this.lblCNH.Location = new System.Drawing.Point(151, 121);
            this.lblCNH.Name = "lblCNH";
            this.lblCNH.Size = new System.Drawing.Size(30, 13);
            this.lblCNH.TabIndex = 84;
            this.lblCNH.Text = "CNH";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(6, 278);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(9, 13);
            this.label15.TabIndex = 81;
            this.label15.Text = "|";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInstrucoes.AutoSize = true;
            this.lblInstrucoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucoes.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblInstrucoes.Location = new System.Drawing.Point(14, 275);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Size = new System.Drawing.Size(98, 20);
            this.lblInstrucoes.TabIndex = 80;
            this.lblInstrucoes.Text = "Informações";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Enabled = false;
            this.chkAtivo.ForeColor = System.Drawing.Color.Black;
            this.chkAtivo.Location = new System.Drawing.Point(121, 42);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 79;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // txtCelular
            // 
            this.txtCelular.BackColor = System.Drawing.Color.White;
            this.txtCelular.Enabled = false;
            this.txtCelular.Location = new System.Drawing.Point(154, 188);
            this.txtCelular.Mask = "(##) #####-####";
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(132, 20);
            this.txtCelular.TabIndex = 78;
            // 
            // lblCelular
            // 
            this.lblCelular.AutoSize = true;
            this.lblCelular.ForeColor = System.Drawing.Color.Black;
            this.lblCelular.Location = new System.Drawing.Point(151, 172);
            this.lblCelular.Name = "lblCelular";
            this.lblCelular.Size = new System.Drawing.Size(39, 13);
            this.lblCelular.TabIndex = 77;
            this.lblCelular.Text = "Celular";
            // 
            // txtApelido
            // 
            this.txtApelido.BackColor = System.Drawing.Color.White;
            this.txtApelido.Enabled = false;
            this.txtApelido.Location = new System.Drawing.Point(9, 138);
            this.txtApelido.Name = "txtApelido";
            this.txtApelido.Size = new System.Drawing.Size(132, 20);
            this.txtApelido.TabIndex = 73;
            // 
            // lblApelido
            // 
            this.lblApelido.AutoSize = true;
            this.lblApelido.ForeColor = System.Drawing.Color.Black;
            this.lblApelido.Location = new System.Drawing.Point(6, 121);
            this.lblApelido.Name = "lblApelido";
            this.lblApelido.Size = new System.Drawing.Size(42, 13);
            this.lblApelido.TabIndex = 74;
            this.lblApelido.Text = "Apelido";
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.White;
            this.txtNome.Enabled = false;
            this.txtNome.Location = new System.Drawing.Point(9, 90);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(277, 20);
            this.txtNome.TabIndex = 71;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.ForeColor = System.Drawing.Color.Black;
            this.lblNome.Location = new System.Drawing.Point(6, 73);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(35, 13);
            this.lblNome.TabIndex = 72;
            this.lblNome.Text = "Nome";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(9, 40);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 69;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.ForeColor = System.Drawing.Color.Black;
            this.lblCodigo.Location = new System.Drawing.Point(6, 23);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(40, 13);
            this.lblCodigo.TabIndex = 70;
            this.lblCodigo.Text = "Código";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 312);
            this.label4.MaximumSize = new System.Drawing.Size(300, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(300, 26);
            this.label4.TabIndex = 82;
            this.label4.Text = "O cadastro de  motorista traz informações diretamente do ERP e que não podem ser " +
    "alteradas pelo WMS.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(11, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 181;
            this.label6.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(13, 67);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(100, 21);
            this.cmbEmpresa.TabIndex = 180;
            // 
            // FrmMotorista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(772, 447);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkPesqAtivo);
            this.Controls.Add(this.txtPesqCodigo);
            this.Controls.Add(this.lblPesqCodigo);
            this.Controls.Add(this.txtPesqApelido);
            this.Controls.Add(this.lblPesqApelido);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMotorista";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Motorista";
            this.Load += new System.EventHandler(this.FrmMotorista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMotorista)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.TextBox txtPesqApelido;
        private System.Windows.Forms.Label lblPesqApelido;
        private System.Windows.Forms.TextBox txtPesqCodigo;
        private System.Windows.Forms.Label lblPesqCodigo;
        private System.Windows.Forms.CheckBox chkPesqAtivo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gridMotorista;
        private System.Windows.Forms.TextBox txtApelido;
        private System.Windows.Forms.Label lblApelido;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.MaskedTextBox txtCelular;
        private System.Windows.Forms.Label lblCelular;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValidade;
        private System.Windows.Forms.Label lblValidade;
        private System.Windows.Forms.TextBox txtCNH;
        private System.Windows.Forms.Label lblCNH;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditaFlow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colControlaValidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colControlaLote;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}