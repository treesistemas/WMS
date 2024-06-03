namespace Wms
{
    partial class FrmCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCaixa));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.lblPeso = new System.Windows.Forms.Label();
            this.txtCubagem = new System.Windows.Forms.TextBox();
            this.lblCubagem = new System.Windows.Forms.Label();
            this.txtComprimento = new System.Windows.Forms.TextBox();
            this.lblComprimento = new System.Windows.Forms.Label();
            this.txtLargura = new System.Windows.Forms.TextBox();
            this.lblLargura = new System.Windows.Forms.Label();
            this.txtAltura = new System.Windows.Forms.TextBox();
            this.lblAltura = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridCaixa = new System.Windows.Forms.DataGridView();
            this.ColCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditaFlow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colControlaValidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colControlaLote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCaixa)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(12, 23);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(299, 39);
            this.label13.TabIndex = 161;
            this.label13.Text = "Cadastro de Caixa";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.lblInstrucoes);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtPeso);
            this.groupBox2.Controls.Add(this.lblPeso);
            this.groupBox2.Controls.Add(this.txtCubagem);
            this.groupBox2.Controls.Add(this.lblCubagem);
            this.groupBox2.Controls.Add(this.txtComprimento);
            this.groupBox2.Controls.Add(this.lblComprimento);
            this.groupBox2.Controls.Add(this.txtLargura);
            this.groupBox2.Controls.Add(this.lblLargura);
            this.groupBox2.Controls.Add(this.txtAltura);
            this.groupBox2.Controls.Add(this.lblAltura);
            this.groupBox2.Controls.Add(this.txtDescricao);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.lblDescricao);
            this.groupBox2.Controls.Add(this.lblCodigo);
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(419, 23);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(531, 399);
            this.groupBox2.TabIndex = 176;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Formulário";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(173, 193);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(10, 16);
            this.label15.TabIndex = 185;
            this.label15.Text = "|";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInstrucoes.AutoSize = true;
            this.lblInstrucoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucoes.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblInstrucoes.Location = new System.Drawing.Point(184, 190);
            this.lblInstrucoes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Size = new System.Drawing.Size(119, 25);
            this.lblInstrucoes.TabIndex = 184;
            this.lblInstrucoes.Text = "Informações";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(173, 223);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.MaximumSize = new System.Drawing.Size(353, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 64);
            this.label1.TabIndex = 186;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // txtPeso
            // 
            this.txtPeso.BackColor = System.Drawing.Color.White;
            this.txtPeso.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPeso.Enabled = false;
            this.txtPeso.Location = new System.Drawing.Point(16, 343);
            this.txtPeso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPeso.MaxLength = 40;
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Size = new System.Drawing.Size(132, 22);
            this.txtPeso.TabIndex = 182;
            this.txtPeso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPeso_KeyPress);
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.ForeColor = System.Drawing.Color.Black;
            this.lblPeso.Location = new System.Drawing.Point(12, 324);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(66, 16);
            this.lblPeso.TabIndex = 183;
            this.lblPeso.Text = "Peso (Kg)";
            // 
            // txtCubagem
            // 
            this.txtCubagem.BackColor = System.Drawing.Color.White;
            this.txtCubagem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCubagem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCubagem.Enabled = false;
            this.txtCubagem.Location = new System.Drawing.Point(16, 288);
            this.txtCubagem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCubagem.MaxLength = 40;
            this.txtCubagem.Name = "txtCubagem";
            this.txtCubagem.Size = new System.Drawing.Size(133, 22);
            this.txtCubagem.TabIndex = 180;
            // 
            // lblCubagem
            // 
            this.lblCubagem.AutoSize = true;
            this.lblCubagem.ForeColor = System.Drawing.Color.Black;
            this.lblCubagem.Location = new System.Drawing.Point(12, 268);
            this.lblCubagem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCubagem.Name = "lblCubagem";
            this.lblCubagem.Size = new System.Drawing.Size(84, 16);
            this.lblCubagem.TabIndex = 181;
            this.lblCubagem.Text = "Cubagem m³";
            // 
            // txtComprimento
            // 
            this.txtComprimento.BackColor = System.Drawing.Color.White;
            this.txtComprimento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComprimento.Enabled = false;
            this.txtComprimento.Location = new System.Drawing.Point(16, 234);
            this.txtComprimento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtComprimento.MaxLength = 40;
            this.txtComprimento.Name = "txtComprimento";
            this.txtComprimento.Size = new System.Drawing.Size(133, 22);
            this.txtComprimento.TabIndex = 178;
            this.txtComprimento.TextChanged += new System.EventHandler(this.txtComprimento_TextChanged);
            this.txtComprimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComprimento_KeyPress);
            // 
            // lblComprimento
            // 
            this.lblComprimento.AutoSize = true;
            this.lblComprimento.ForeColor = System.Drawing.Color.Black;
            this.lblComprimento.Location = new System.Drawing.Point(13, 214);
            this.lblComprimento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComprimento.Name = "lblComprimento";
            this.lblComprimento.Size = new System.Drawing.Size(116, 16);
            this.lblComprimento.TabIndex = 179;
            this.lblComprimento.Text = "Comprimento (cm)";
            // 
            // txtLargura
            // 
            this.txtLargura.BackColor = System.Drawing.Color.White;
            this.txtLargura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLargura.Enabled = false;
            this.txtLargura.Location = new System.Drawing.Point(16, 172);
            this.txtLargura.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLargura.MaxLength = 40;
            this.txtLargura.Name = "txtLargura";
            this.txtLargura.Size = new System.Drawing.Size(132, 22);
            this.txtLargura.TabIndex = 176;
            this.txtLargura.TextChanged += new System.EventHandler(this.txtLargura_TextChanged);
            this.txtLargura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLargura_KeyPress);
            // 
            // lblLargura
            // 
            this.lblLargura.AutoSize = true;
            this.lblLargura.ForeColor = System.Drawing.Color.Black;
            this.lblLargura.Location = new System.Drawing.Point(12, 153);
            this.lblLargura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLargura.Name = "lblLargura";
            this.lblLargura.Size = new System.Drawing.Size(82, 16);
            this.lblLargura.TabIndex = 177;
            this.lblLargura.Text = "Largura (cm)";
            // 
            // txtAltura
            // 
            this.txtAltura.BackColor = System.Drawing.Color.White;
            this.txtAltura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAltura.Enabled = false;
            this.txtAltura.Location = new System.Drawing.Point(16, 116);
            this.txtAltura.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAltura.MaxLength = 40;
            this.txtAltura.Name = "txtAltura";
            this.txtAltura.Size = new System.Drawing.Size(132, 22);
            this.txtAltura.TabIndex = 174;
            this.txtAltura.TextChanged += new System.EventHandler(this.txtAltura_TextChanged);
            this.txtAltura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAltura_KeyPress);
            // 
            // lblAltura
            // 
            this.lblAltura.AutoSize = true;
            this.lblAltura.ForeColor = System.Drawing.Color.Black;
            this.lblAltura.Location = new System.Drawing.Point(12, 96);
            this.lblAltura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(70, 16);
            this.lblAltura.TabIndex = 175;
            this.lblAltura.Text = "Altura (cm)";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Enabled = false;
            this.txtDescricao.Location = new System.Drawing.Point(163, 58);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescricao.MaxLength = 40;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(356, 22);
            this.txtDescricao.TabIndex = 170;
            this.txtDescricao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescricao_KeyPress);
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(16, 58);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 22);
            this.txtCodigo.TabIndex = 172;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.ForeColor = System.Drawing.Color.Black;
            this.lblDescricao.Location = new System.Drawing.Point(159, 37);
            this.lblDescricao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(69, 16);
            this.lblDescricao.TabIndex = 171;
            this.lblDescricao.Text = "Descrição";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.ForeColor = System.Drawing.Color.Black;
            this.lblCodigo.Location = new System.Drawing.Point(12, 37);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 173;
            this.lblCodigo.Text = "Código";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.gridCaixa);
            this.groupBox1.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Location = new System.Drawing.Point(13, 177);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(395, 246);
            this.groupBox1.TabIndex = 177;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Caixas Cadastradas";
            // 
            // gridCaixa
            // 
            this.gridCaixa.AllowUserToAddRows = false;
            this.gridCaixa.AllowUserToResizeColumns = false;
            this.gridCaixa.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridCaixa.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCaixa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridCaixa.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridCaixa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridCaixa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCaixa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodigo,
            this.ColDescricao,
            this.colAuditaFlow,
            this.colControlaValidade,
            this.colControlaLote,
            this.peso});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCaixa.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridCaixa.GridColor = System.Drawing.Color.Gainsboro;
            this.gridCaixa.Location = new System.Drawing.Point(8, 23);
            this.gridCaixa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridCaixa.MultiSelect = false;
            this.gridCaixa.Name = "gridCaixa";
            this.gridCaixa.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCaixa.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridCaixa.RowHeadersVisible = false;
            this.gridCaixa.RowHeadersWidth = 20;
            this.gridCaixa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridCaixa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCaixa.Size = new System.Drawing.Size(379, 215);
            this.gridCaixa.TabIndex = 0;
            this.gridCaixa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridCaixa_KeyUp);
            this.gridCaixa.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridCaixa_MouseClick);
            this.gridCaixa.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridCaixa_MouseDoubleClick);
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
            this.ColDescricao.HeaderText = "Descrição";
            this.ColDescricao.MinimumWidth = 6;
            this.ColDescricao.Name = "ColDescricao";
            this.ColDescricao.ReadOnly = true;
            this.ColDescricao.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColDescricao.Width = 250;
            // 
            // colAuditaFlow
            // 
            this.colAuditaFlow.Frozen = true;
            this.colAuditaFlow.HeaderText = "Altura";
            this.colAuditaFlow.MinimumWidth = 6;
            this.colAuditaFlow.Name = "colAuditaFlow";
            this.colAuditaFlow.ReadOnly = true;
            this.colAuditaFlow.Visible = false;
            this.colAuditaFlow.Width = 106;
            // 
            // colControlaValidade
            // 
            this.colControlaValidade.HeaderText = "Largura";
            this.colControlaValidade.MinimumWidth = 6;
            this.colControlaValidade.Name = "colControlaValidade";
            this.colControlaValidade.ReadOnly = true;
            this.colControlaValidade.Visible = false;
            this.colControlaValidade.Width = 105;
            // 
            // colControlaLote
            // 
            this.colControlaLote.HeaderText = "Comprimento";
            this.colControlaLote.MinimumWidth = 6;
            this.colControlaLote.Name = "colControlaLote";
            this.colControlaLote.ReadOnly = true;
            this.colControlaLote.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colControlaLote.Visible = false;
            this.colControlaLote.Width = 80;
            // 
            // peso
            // 
            this.peso.HeaderText = "Peso";
            this.peso.MinimumWidth = 6;
            this.peso.Name = "peso";
            this.peso.ReadOnly = true;
            this.peso.Visible = false;
            this.peso.Width = 125;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 481);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(977, 25);
            this.panel1.TabIndex = 178;
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtd.Location = new System.Drawing.Point(69, 6);
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
            this.lblInformacao.Size = new System.Drawing.Size(51, 16);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Caixas:";
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(849, 436);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 181;
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
            this.btnSalvar.Location = new System.Drawing.Point(735, 436);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(103, 38);
            this.btnSalvar.TabIndex = 180;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.BackColor = System.Drawing.Color.DimGray;
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNovo.Location = new System.Drawing.Point(627, 436);
            this.btnNovo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(100, 38);
            this.btnNovo.TabIndex = 179;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(319, 62);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 74);
            this.pictureBox1.TabIndex = 182;
            this.pictureBox1.TabStop = false;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(157, 126);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(100, 38);
            this.btnPesquisar.TabIndex = 183;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // FrmCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(977, 506);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmCaixa";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Caixa";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCaixa)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridCaixa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditaFlow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colControlaValidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colControlaLote;
        private System.Windows.Forms.DataGridViewTextBoxColumn peso;
    }
}