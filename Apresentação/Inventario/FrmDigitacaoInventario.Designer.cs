
namespace Wms
{
    partial class FrmDigitacaoInventario
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQtdOrdem = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gridContagens = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtCodEndereco = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodProduto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQtdFechada = new System.Windows.Forms.TextBox();
            this.lblUniFechada = new System.Windows.Forms.Label();
            this.txtQtdFracionada = new System.Windows.Forms.TextBox();
            this.lblUniFracionada = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDescInventario = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.rbtPrimeira = new System.Windows.Forms.RadioButton();
            this.rbtSegunda = new System.Windows.Forms.RadioButton();
            this.rbtTerceira = new System.Windows.Forms.RadioButton();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.lblProduto = new System.Windows.Forms.Label();
            this.txtCodAuditor = new System.Windows.Forms.TextBox();
            this.lblAuditor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.dtmVencimento = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContagens)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQtdOrdem);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 541);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 20);
            this.panel1.TabIndex = 73;
            // 
            // lblQtdOrdem
            // 
            this.lblQtdOrdem.AutoSize = true;
            this.lblQtdOrdem.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtdOrdem.Location = new System.Drawing.Point(62, 3);
            this.lblQtdOrdem.Name = "lblQtdOrdem";
            this.lblQtdOrdem.Size = new System.Drawing.Size(13, 13);
            this.lblQtdOrdem.TabIndex = 3;
            this.lblQtdOrdem.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.SystemColors.Window;
            this.label19.Location = new System.Drawing.Point(9, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(54, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "Digitados:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(6, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(304, 31);
            this.label13.TabIndex = 75;
            this.label13.Text = "Digitação de Contagens";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(335, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 60);
            this.pictureBox1.TabIndex = 74;
            this.pictureBox1.TabStop = false;
            // 
            // gridContagens
            // 
            this.gridContagens.AllowUserToAddRows = false;
            this.gridContagens.AllowUserToResizeColumns = false;
            this.gridContagens.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridContagens.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridContagens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridContagens.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridContagens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridContagens.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridContagens.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridContagens.ColumnHeadersHeight = 28;
            this.gridContagens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridContagens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column4,
            this.Column8,
            this.Column6,
            this.Column3,
            this.Column7,
            this.Column21,
            this.Column2,
            this.Column1});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridContagens.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridContagens.EnableHeadersVisualStyles = false;
            this.gridContagens.GridColor = System.Drawing.Color.Gainsboro;
            this.gridContagens.Location = new System.Drawing.Point(12, 242);
            this.gridContagens.MultiSelect = false;
            this.gridContagens.Name = "gridContagens";
            this.gridContagens.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridContagens.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridContagens.RowHeadersVisible = false;
            this.gridContagens.RowHeadersWidth = 20;
            this.gridContagens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridContagens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridContagens.Size = new System.Drawing.Size(1010, 251);
            this.gridContagens.TabIndex = 76;
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
            this.Column4.HeaderText = "Contagem";
            this.Column4.MaxInputLength = 3276;
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.Width = 125;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Auditor";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Endereço";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 125;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "Produto";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.Width = 300;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Qtd Fechada";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 125;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "Qtd Fracionada";
            this.Column21.MinimumWidth = 6;
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            this.Column21.Width = 110;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Vencimento";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 90;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Lote";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(947, 504);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 31);
            this.btnSair.TabIndex = 77;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            // 
            // txtCodEndereco
            // 
            this.txtCodEndereco.BackColor = System.Drawing.Color.White;
            this.txtCodEndereco.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodEndereco.Location = new System.Drawing.Point(12, 190);
            this.txtCodEndereco.Name = "txtCodEndereco";
            this.txtCodEndereco.Size = new System.Drawing.Size(119, 23);
            this.txtCodEndereco.TabIndex = 78;
            this.txtCodEndereco.TextChanged += new System.EventHandler(this.txtCodEndereco_TextChanged);
            this.txtCodEndereco.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodEndereco_KeyPress);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.Location = new System.Drawing.Point(9, 171);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(69, 17);
            this.lblCodigo.TabIndex = 79;
            this.lblCodigo.Text = "Endereço";
            // 
            // txtCodProduto
            // 
            this.txtCodProduto.BackColor = System.Drawing.Color.White;
            this.txtCodProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProduto.Location = new System.Drawing.Point(137, 190);
            this.txtCodProduto.Name = "txtCodProduto";
            this.txtCodProduto.Size = new System.Drawing.Size(119, 23);
            this.txtCodProduto.TabIndex = 80;
            this.txtCodProduto.TextChanged += new System.EventHandler(this.txtCodProduto_TextChanged);
            this.txtCodProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodProduto_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(134, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 81;
            this.label1.Text = "Produto";
            // 
            // txtQtdFechada
            // 
            this.txtQtdFechada.BackColor = System.Drawing.Color.White;
            this.txtQtdFechada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtdFechada.Location = new System.Drawing.Point(262, 190);
            this.txtQtdFechada.Name = "txtQtdFechada";
            this.txtQtdFechada.Size = new System.Drawing.Size(57, 23);
            this.txtQtdFechada.TabIndex = 82;
            this.txtQtdFechada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQtdFechada_KeyPress);
            // 
            // lblUniFechada
            // 
            this.lblUniFechada.AutoSize = true;
            this.lblUniFechada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUniFechada.Location = new System.Drawing.Point(259, 171);
            this.lblUniFechada.Name = "lblUniFechada";
            this.lblUniFechada.Size = new System.Drawing.Size(35, 17);
            this.lblUniFechada.TabIndex = 83;
            this.lblUniFechada.Text = "CXA";
            // 
            // txtQtdFracionada
            // 
            this.txtQtdFracionada.BackColor = System.Drawing.Color.White;
            this.txtQtdFracionada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtdFracionada.Location = new System.Drawing.Point(325, 190);
            this.txtQtdFracionada.Name = "txtQtdFracionada";
            this.txtQtdFracionada.Size = new System.Drawing.Size(57, 23);
            this.txtQtdFracionada.TabIndex = 84;
            this.txtQtdFracionada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQtdFracionada_KeyPress);
            // 
            // lblUniFracionada
            // 
            this.lblUniFracionada.AutoSize = true;
            this.lblUniFracionada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUniFracionada.Location = new System.Drawing.Point(322, 171);
            this.lblUniFracionada.Name = "lblUniFracionada";
            this.lblUniFracionada.Size = new System.Drawing.Size(38, 17);
            this.lblUniFracionada.TabIndex = 85;
            this.lblUniFracionada.Text = "UND";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(385, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 87;
            this.label4.Text = "Vencimento";
            // 
            // txtLote
            // 
            this.txtLote.BackColor = System.Drawing.Color.White;
            this.txtLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLote.Location = new System.Drawing.Point(499, 190);
            this.txtLote.Name = "txtLote";
            this.txtLote.Size = new System.Drawing.Size(106, 23);
            this.txtLote.TabIndex = 88;
            this.txtLote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLote_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(496, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 17);
            this.label5.TabIndex = 89;
            this.label5.Text = "Lote";
            // 
            // lblDescInventario
            // 
            this.lblDescInventario.AutoSize = true;
            this.lblDescInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescInventario.ForeColor = System.Drawing.Color.Tomato;
            this.lblDescInventario.Location = new System.Drawing.Point(7, 59);
            this.lblDescInventario.Name = "lblDescInventario";
            this.lblDescInventario.Size = new System.Drawing.Size(19, 25);
            this.lblDescInventario.TabIndex = 90;
            this.lblDescInventario.Text = "-";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmar.Location = new System.Drawing.Point(617, 188);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(45, 25);
            this.btnConfirmar.TabIndex = 91;
            this.btnConfirmar.Text = "OK";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(9, 123);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 17);
            this.label17.TabIndex = 181;
            this.label17.Text = "Auditor";
            // 
            // rbtPrimeira
            // 
            this.rbtPrimeira.AutoSize = true;
            this.rbtPrimeira.Checked = true;
            this.rbtPrimeira.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPrimeira.Location = new System.Drawing.Point(12, 96);
            this.rbtPrimeira.Name = "rbtPrimeira";
            this.rbtPrimeira.Size = new System.Drawing.Size(107, 21);
            this.rbtPrimeira.TabIndex = 182;
            this.rbtPrimeira.TabStop = true;
            this.rbtPrimeira.Text = "1ª Contagem";
            this.rbtPrimeira.UseVisualStyleBackColor = true;
            this.rbtPrimeira.CheckedChanged += new System.EventHandler(this.rbtPrimeira_CheckedChanged);
            // 
            // rbtSegunda
            // 
            this.rbtSegunda.AutoSize = true;
            this.rbtSegunda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSegunda.Location = new System.Drawing.Point(118, 96);
            this.rbtSegunda.Name = "rbtSegunda";
            this.rbtSegunda.Size = new System.Drawing.Size(107, 21);
            this.rbtSegunda.TabIndex = 183;
            this.rbtSegunda.Text = "2ª Contagem";
            this.rbtSegunda.UseVisualStyleBackColor = true;
            this.rbtSegunda.CheckedChanged += new System.EventHandler(this.rbtSegunda_CheckedChanged);
            // 
            // rbtTerceira
            // 
            this.rbtTerceira.AutoSize = true;
            this.rbtTerceira.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtTerceira.Location = new System.Drawing.Point(222, 96);
            this.rbtTerceira.Name = "rbtTerceira";
            this.rbtTerceira.Size = new System.Drawing.Size(107, 21);
            this.rbtTerceira.TabIndex = 184;
            this.rbtTerceira.Text = "3ª Contagem";
            this.rbtTerceira.UseVisualStyleBackColor = true;
            this.rbtTerceira.CheckedChanged += new System.EventHandler(this.rbtTerceira_CheckedChanged);
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblEndereco.Location = new System.Drawing.Point(12, 217);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(10, 13);
            this.lblEndereco.TabIndex = 185;
            this.lblEndereco.Text = "-";
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblProduto.Location = new System.Drawing.Point(134, 217);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(10, 13);
            this.lblProduto.TabIndex = 186;
            this.lblProduto.Text = "-";
            // 
            // txtCodAuditor
            // 
            this.txtCodAuditor.BackColor = System.Drawing.Color.White;
            this.txtCodAuditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodAuditor.Location = new System.Drawing.Point(12, 142);
            this.txtCodAuditor.Name = "txtCodAuditor";
            this.txtCodAuditor.Size = new System.Drawing.Size(75, 23);
            this.txtCodAuditor.TabIndex = 0;
            this.txtCodAuditor.TextChanged += new System.EventHandler(this.txtCodAuditor_TextChanged);
            this.txtCodAuditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAuditor_KeyDown);
            this.txtCodAuditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAuditor_KeyPress);
            // 
            // lblAuditor
            // 
            this.lblAuditor.AutoSize = true;
            this.lblAuditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuditor.ForeColor = System.Drawing.Color.DimGray;
            this.lblAuditor.Location = new System.Drawing.Point(93, 145);
            this.lblAuditor.Name = "lblAuditor";
            this.lblAuditor.Size = new System.Drawing.Size(13, 17);
            this.lblAuditor.TabIndex = 188;
            this.lblAuditor.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(61, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 189;
            this.label2.Text = "*F2";
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.OrangeRed;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.ForeColor = System.Drawing.Color.White;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpar.Location = new System.Drawing.Point(675, 188);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(76, 25);
            this.btnLimpar.TabIndex = 190;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // dtmVencimento
            // 
            this.dtmVencimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtmVencimento.Location = new System.Drawing.Point(386, 190);
            this.dtmVencimento.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtmVencimento.Mask = "##/##/####";
            this.dtmVencimento.Name = "dtmVencimento";
            this.dtmVencimento.Size = new System.Drawing.Size(105, 23);
            this.dtmVencimento.TabIndex = 191;
            this.dtmVencimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtmVencimento_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(137, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 193;
            this.label6.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(137, 142);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(152, 24);
            this.cmbEmpresa.TabIndex = 192;
            // 
            // FrmDigitacaoInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1034, 561);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.dtmVencimento);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAuditor);
            this.Controls.Add(this.txtCodAuditor);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.lblEndereco);
            this.Controls.Add(this.rbtTerceira);
            this.Controls.Add(this.rbtSegunda);
            this.Controls.Add(this.rbtPrimeira);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.lblDescInventario);
            this.Controls.Add(this.txtLote);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtQtdFracionada);
            this.Controls.Add(this.lblUniFracionada);
            this.Controls.Add(this.txtQtdFechada);
            this.Controls.Add(this.lblUniFechada);
            this.Controls.Add(this.txtCodProduto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCodEndereco);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.gridContagens);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmDigitacaoInventario";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Digitação de Contagens";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDigitacaoInventario_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContagens)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQtdOrdem;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView gridContagens;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.TextBox txtCodEndereco;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodProduto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQtdFechada;
        private System.Windows.Forms.Label lblUniFechada;
        private System.Windows.Forms.TextBox txtQtdFracionada;
        private System.Windows.Forms.Label lblUniFracionada;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDescInventario;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RadioButton rbtPrimeira;
        private System.Windows.Forms.RadioButton rbtSegunda;
        private System.Windows.Forms.RadioButton rbtTerceira;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.TextBox txtCodAuditor;
        private System.Windows.Forms.Label lblAuditor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.MaskedTextBox dtmVencimento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}