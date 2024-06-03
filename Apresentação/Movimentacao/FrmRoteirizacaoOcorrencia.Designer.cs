
namespace Wms
{
    partial class FrmRoteirizacaoOcorrencia
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
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtmFinal = new System.Windows.Forms.DateTimePicker();
            this.dtmInicial = new System.Windows.Forms.DateTimePicker();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.gridOcorrencia = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label26 = new System.Windows.Forms.Label();
            this.txtPesqManifesto = new System.Windows.Forms.TextBox();
            this.lblNomeMotorista = new System.Windows.Forms.Label();
            this.lblVeiculo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblNotaFiscal = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblUF = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.lblBairro = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblCidade = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblPedido = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOcorrencia)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(13, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(350, 39);
            this.label13.TabIndex = 204;
            this.label13.Text = "Manifestar Ocorrência";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(362, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 74);
            this.pictureBox1.TabIndex = 203;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 213;
            this.label1.Text = "Período (Reentrega)";
            // 
            // dtmFinal
            // 
            this.dtmFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmFinal.Location = new System.Drawing.Point(193, 94);
            this.dtmFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtmFinal.Name = "dtmFinal";
            this.dtmFinal.Size = new System.Drawing.Size(152, 22);
            this.dtmFinal.TabIndex = 212;
            // 
            // dtmInicial
            // 
            this.dtmInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmInicial.Location = new System.Drawing.Point(21, 94);
            this.dtmInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtmInicial.Name = "dtmInicial";
            this.dtmInicial.Size = new System.Drawing.Size(157, 22);
            this.dtmInicial.TabIndex = 211;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(244, 127);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(100, 38);
            this.btnPesquisar.TabIndex = 210;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            // 
            // gridOcorrencia
            // 
            this.gridOcorrencia.AllowUserToAddRows = false;
            this.gridOcorrencia.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridOcorrencia.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridOcorrencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridOcorrencia.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridOcorrencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridOcorrencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOcorrencia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column12,
            this.Column9,
            this.Column10,
            this.Column15,
            this.Column18,
            this.Column19,
            this.Column17,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridOcorrencia.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridOcorrencia.GridColor = System.Drawing.Color.Gainsboro;
            this.gridOcorrencia.Location = new System.Drawing.Point(20, 229);
            this.gridOcorrencia.Margin = new System.Windows.Forms.Padding(4);
            this.gridOcorrencia.MultiSelect = false;
            this.gridOcorrencia.Name = "gridOcorrencia";
            this.gridOcorrencia.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridOcorrencia.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridOcorrencia.RowHeadersVisible = false;
            this.gridOcorrencia.RowHeadersWidth = 20;
            this.gridOcorrencia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOcorrencia.Size = new System.Drawing.Size(434, 341);
            this.gridOcorrencia.TabIndex = 214;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Nº";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 40;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Ocorrência";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Visible = false;
            this.Column12.Width = 125;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "NF";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 80;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Pedido";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 125;
            // 
            // Column15
            // 
            this.Column15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column15.HeaderText = "Cliente";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Cidade";
            this.Column18.MinimumWidth = 6;
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Visible = false;
            this.Column18.Width = 150;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "Bairro";
            this.Column19.MinimumWidth = 6;
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Visible = false;
            this.Column19.Width = 125;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Endereço";
            this.Column17.MinimumWidth = 6;
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Visible = false;
            this.Column17.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Manifesto";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 125;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(26, 338);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 16);
            this.label26.TabIndex = 216;
            this.label26.Text = "Manifesto";
            // 
            // txtPesqManifesto
            // 
            this.txtPesqManifesto.BackColor = System.Drawing.Color.White;
            this.txtPesqManifesto.Location = new System.Drawing.Point(29, 359);
            this.txtPesqManifesto.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesqManifesto.Name = "txtPesqManifesto";
            this.txtPesqManifesto.Size = new System.Drawing.Size(102, 22);
            this.txtPesqManifesto.TabIndex = 215;
            // 
            // lblNomeMotorista
            // 
            this.lblNomeMotorista.AutoSize = true;
            this.lblNomeMotorista.ForeColor = System.Drawing.Color.Black;
            this.lblNomeMotorista.Location = new System.Drawing.Point(28, 422);
            this.lblNomeMotorista.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNomeMotorista.Name = "lblNomeMotorista";
            this.lblNomeMotorista.Size = new System.Drawing.Size(11, 16);
            this.lblNomeMotorista.TabIndex = 220;
            this.lblNomeMotorista.Text = "-";
            // 
            // lblVeiculo
            // 
            this.lblVeiculo.AutoSize = true;
            this.lblVeiculo.ForeColor = System.Drawing.Color.Black;
            this.lblVeiculo.Location = new System.Drawing.Point(26, 475);
            this.lblVeiculo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVeiculo.Name = "lblVeiculo";
            this.lblVeiculo.Size = new System.Drawing.Size(11, 16);
            this.lblVeiculo.TabIndex = 219;
            this.lblVeiculo.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(26, 398);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 16);
            this.label7.TabIndex = 218;
            this.label7.Text = "Motorista";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(26, 451);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 16);
            this.label6.TabIndex = 217;
            this.label6.Text = "Veículo";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblInformacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 638);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1118, 25);
            this.panel1.TabIndex = 221;
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.ForeColor = System.Drawing.SystemColors.Window;
            this.lblQtd.Location = new System.Drawing.Point(89, 6);
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
            this.lblInformacao.Size = new System.Drawing.Size(76, 16);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Ocorrência:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(1005, 592);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 38);
            this.btnCancelar.TabIndex = 224;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(897, 592);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 38);
            this.btnSalvar.TabIndex = 223;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.BackColor = System.Drawing.Color.SteelBlue;
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNovo.Location = new System.Drawing.Point(789, 592);
            this.btnNovo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(100, 38);
            this.btnNovo.TabIndex = 222;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.lblCodigo);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.lblNotaFiscal);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.lblNomeMotorista);
            this.groupBox1.Controls.Add(this.lblUF);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblVeiculo);
            this.groupBox1.Controls.Add(this.label69);
            this.groupBox1.Controls.Add(this.lblBairro);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.txtPesqManifesto);
            this.groupBox1.Controls.Add(this.lblCidade);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.lblPedido);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(461, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 555);
            this.groupBox1.TabIndex = 225;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Location = new System.Drawing.Point(29, 319);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(585, 1);
            this.panel2.TabIndex = 218;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCodigo.Location = new System.Drawing.Point(26, 60);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(11, 16);
            this.lblCodigo.TabIndex = 204;
            this.lblCodigo.Text = "-";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(26, 37);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(73, 16);
            this.label24.TabIndex = 217;
            this.label24.Text = "Ocorrência";
            // 
            // lblNotaFiscal
            // 
            this.lblNotaFiscal.AutoSize = true;
            this.lblNotaFiscal.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblNotaFiscal.Location = new System.Drawing.Point(172, 108);
            this.lblNotaFiscal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNotaFiscal.Name = "lblNotaFiscal";
            this.lblNotaFiscal.Size = new System.Drawing.Size(11, 16);
            this.lblNotaFiscal.TabIndex = 216;
            this.lblNotaFiscal.Text = "-";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(172, 84);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(75, 16);
            this.label27.TabIndex = 215;
            this.label27.Text = "Nota Fiscal";
            // 
            // lblUF
            // 
            this.lblUF.AutoSize = true;
            this.lblUF.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblUF.Location = new System.Drawing.Point(26, 214);
            this.lblUF.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUF.Name = "lblUF";
            this.lblUF.Size = new System.Drawing.Size(11, 16);
            this.lblUF.TabIndex = 214;
            this.lblUF.Text = "-";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.ForeColor = System.Drawing.Color.Black;
            this.label69.Location = new System.Drawing.Point(26, 189);
            this.label69.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(25, 16);
            this.label69.TabIndex = 213;
            this.label69.Text = "UF";
            // 
            // lblBairro
            // 
            this.lblBairro.AutoSize = true;
            this.lblBairro.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblBairro.Location = new System.Drawing.Point(327, 214);
            this.lblBairro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(11, 16);
            this.lblBairro.TabIndex = 212;
            this.lblBairro.Text = "-";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Location = new System.Drawing.Point(327, 189);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(43, 16);
            this.label39.TabIndex = 211;
            this.label39.Text = "Bairro";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(26, 242);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(66, 16);
            this.label37.TabIndex = 209;
            this.label37.Text = "Endereço";
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblCidade.Location = new System.Drawing.Point(172, 214);
            this.lblCidade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(11, 16);
            this.lblCidade.TabIndex = 208;
            this.lblCidade.Text = "-";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.Black;
            this.label35.Location = new System.Drawing.Point(172, 189);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(51, 16);
            this.label35.TabIndex = 207;
            this.label35.Text = "Cidade";
            // 
            // lblPedido
            // 
            this.lblPedido.AutoSize = true;
            this.lblPedido.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblPedido.Location = new System.Drawing.Point(26, 108);
            this.lblPedido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(11, 16);
            this.lblPedido.TabIndex = 203;
            this.lblPedido.Text = "-";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(327, 133);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 16);
            this.label15.TabIndex = 202;
            this.label15.Text = "Fantasia";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(26, 84);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 16);
            this.label14.TabIndex = 201;
            this.label14.Text = "Pedido";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(26, 133);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 16);
            this.label12.TabIndex = 200;
            this.label12.Text = "Cliente";
            // 
            // FrmRoteirizacaoOcorrencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1118, 663);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtmFinal);
            this.Controls.Add(this.dtmInicial);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.gridOcorrencia);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmRoteirizacaoOcorrencia";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manifestar Ocorrência";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOcorrencia)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtmFinal;
        private System.Windows.Forms.DateTimePicker dtmInicial;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridView gridOcorrencia;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtPesqManifesto;
        private System.Windows.Forms.Label lblNomeMotorista;
        private System.Windows.Forms.Label lblVeiculo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblNotaFiscal;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblUF;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}