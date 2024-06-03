namespace Wms.Inventario
{
    partial class FrmImpressaoContagens
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtContagem = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbInventario = new System.Windows.Forms.ComboBox();
            this.cmbRegiao = new System.Windows.Forms.ComboBox();
            this.lblRua = new System.Windows.Forms.Label();
            this.cmbRua = new System.Windows.Forms.ComboBox();
            this.lblRegiao = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBloco = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLado = new System.Windows.Forms.ComboBox();
            this.rbtCaixa = new System.Windows.Forms.RadioButton();
            this.rbtFlowRack = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtCriticaContagens = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbEstacao = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.rbtSemContagem = new System.Windows.Forms.RadioButton();
            this.rbtContagem3 = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.gridProduto = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniRemover = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rbtSemContagem2 = new System.Windows.Forms.RadioButton();
            this.rbtVolumeSemContagem = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProduto)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 409);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 20);
            this.panel1.TabIndex = 74;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(13, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 15);
            this.label9.TabIndex = 269;
            this.label9.Text = "Tipo de Picking";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Location = new System.Drawing.Point(272, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 15);
            this.label8.TabIndex = 268;
            this.label8.Text = "Tipo de Relatório de Contagem";
            // 
            // rbtContagem
            // 
            this.rbtContagem.AutoSize = true;
            this.rbtContagem.BackColor = System.Drawing.Color.DimGray;
            this.rbtContagem.Checked = true;
            this.rbtContagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtContagem.ForeColor = System.Drawing.Color.White;
            this.rbtContagem.Location = new System.Drawing.Point(274, 128);
            this.rbtContagem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtContagem.Name = "rbtContagem";
            this.rbtContagem.Size = new System.Drawing.Size(91, 19);
            this.rbtContagem.TabIndex = 266;
            this.rbtContagem.TabStop = true;
            this.rbtContagem.Text = "Contagem   ";
            this.rbtContagem.UseVisualStyleBackColor = false;
            this.rbtContagem.CheckedChanged += new System.EventHandler(this.rbtContagem_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 256;
            this.label3.Text = "Tipo";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Enabled = false;
            this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.ForeColor = System.Drawing.Color.SeaGreen;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "PICKING",
            "PULMAO"});
            this.cmbTipo.Location = new System.Drawing.Point(20, 110);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(181, 23);
            this.cmbTipo.TabIndex = 255;
            this.cmbTipo.Text = "SELECIONE";
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(784, 371);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 31);
            this.btnSair.TabIndex = 254;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            // 
            // btnAnalisar
            // 
            this.btnAnalisar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalisar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAnalisar.ForeColor = System.Drawing.Color.White;
            this.btnAnalisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnalisar.Location = new System.Drawing.Point(691, 371);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(88, 31);
            this.btnAnalisar.TabIndex = 253;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = false;
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisar_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(11, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(317, 31);
            this.label13.TabIndex = 246;
            this.label13.Text = "Impressão de Contagens";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 274;
            this.label4.Text = "Inventário";
            // 
            // cmbInventario
            // 
            this.cmbInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInventario.ForeColor = System.Drawing.Color.OrangeRed;
            this.cmbInventario.FormattingEnabled = true;
            this.cmbInventario.Location = new System.Drawing.Point(20, 60);
            this.cmbInventario.Name = "cmbInventario";
            this.cmbInventario.Size = new System.Drawing.Size(181, 23);
            this.cmbInventario.TabIndex = 273;
            this.cmbInventario.Text = "SELECIONE";
            this.cmbInventario.SelectedIndexChanged += new System.EventHandler(this.cbmInventario_SelectedIndexChanged);
            this.cmbInventario.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbmInventario_MouseClick);
            // 
            // cmbRegiao
            // 
            this.cmbRegiao.Enabled = false;
            this.cmbRegiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegiao.FormattingEnabled = true;
            this.cmbRegiao.Location = new System.Drawing.Point(15, 316);
            this.cmbRegiao.Name = "cmbRegiao";
            this.cmbRegiao.Size = new System.Drawing.Size(58, 23);
            this.cmbRegiao.TabIndex = 302;
            this.cmbRegiao.Text = "SEL...";
            this.cmbRegiao.SelectedIndexChanged += new System.EventHandler(this.cmbRegiao_SelectedIndexChanged);
            this.cmbRegiao.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbRegiao_MouseClick);
            // 
            // lblRua
            // 
            this.lblRua.AutoSize = true;
            this.lblRua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRua.ForeColor = System.Drawing.Color.Black;
            this.lblRua.Location = new System.Drawing.Point(76, 299);
            this.lblRua.Name = "lblRua";
            this.lblRua.Size = new System.Drawing.Size(30, 15);
            this.lblRua.TabIndex = 305;
            this.lblRua.Text = "Rua";
            // 
            // cmbRua
            // 
            this.cmbRua.Enabled = false;
            this.cmbRua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRua.FormattingEnabled = true;
            this.cmbRua.Location = new System.Drawing.Point(78, 316);
            this.cmbRua.Name = "cmbRua";
            this.cmbRua.Size = new System.Drawing.Size(54, 23);
            this.cmbRua.TabIndex = 303;
            this.cmbRua.Text = "SEL...";
            this.cmbRua.SelectedIndexChanged += new System.EventHandler(this.cmbRua_SelectedIndexChanged);
            // 
            // lblRegiao
            // 
            this.lblRegiao.AutoSize = true;
            this.lblRegiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegiao.ForeColor = System.Drawing.Color.Black;
            this.lblRegiao.Location = new System.Drawing.Point(13, 298);
            this.lblRegiao.Name = "lblRegiao";
            this.lblRegiao.Size = new System.Drawing.Size(47, 15);
            this.lblRegiao.TabIndex = 304;
            this.lblRegiao.Text = "Região";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(136, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 307;
            this.label1.Text = "Bloco";
            // 
            // cmbBloco
            // 
            this.cmbBloco.Enabled = false;
            this.cmbBloco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBloco.FormattingEnabled = true;
            this.cmbBloco.Location = new System.Drawing.Point(137, 316);
            this.cmbBloco.Name = "cmbBloco";
            this.cmbBloco.Size = new System.Drawing.Size(58, 23);
            this.cmbBloco.TabIndex = 306;
            this.cmbBloco.Text = "SEL...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(11, 345);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 309;
            this.label2.Text = "Lado";
            // 
            // cmbLado
            // 
            this.cmbLado.Enabled = false;
            this.cmbLado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLado.FormattingEnabled = true;
            this.cmbLado.Items.AddRange(new object[] {
            "PAR",
            "IMPAR",
            "TODOS"});
            this.cmbLado.Location = new System.Drawing.Point(14, 364);
            this.cmbLado.Name = "cmbLado";
            this.cmbLado.Size = new System.Drawing.Size(117, 23);
            this.cmbLado.TabIndex = 308;
            this.cmbLado.Text = "SELECIONE";
            // 
            // rbtCaixa
            // 
            this.rbtCaixa.AutoSize = true;
            this.rbtCaixa.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.rbtCaixa.Checked = true;
            this.rbtCaixa.Enabled = false;
            this.rbtCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCaixa.ForeColor = System.Drawing.Color.White;
            this.rbtCaixa.Location = new System.Drawing.Point(2, 13);
            this.rbtCaixa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtCaixa.Name = "rbtCaixa";
            this.rbtCaixa.Size = new System.Drawing.Size(89, 19);
            this.rbtCaixa.TabIndex = 310;
            this.rbtCaixa.TabStop = true;
            this.rbtCaixa.Text = "Caixa           ";
            this.rbtCaixa.UseVisualStyleBackColor = false;
            this.rbtCaixa.CheckedChanged += new System.EventHandler(this.rbtCaixa_CheckedChanged);
            // 
            // rbtFlowRack
            // 
            this.rbtFlowRack.AutoSize = true;
            this.rbtFlowRack.BackColor = System.Drawing.Color.Orange;
            this.rbtFlowRack.Enabled = false;
            this.rbtFlowRack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtFlowRack.ForeColor = System.Drawing.Color.White;
            this.rbtFlowRack.Location = new System.Drawing.Point(92, 13);
            this.rbtFlowRack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtFlowRack.Name = "rbtFlowRack";
            this.rbtFlowRack.Size = new System.Drawing.Size(94, 19);
            this.rbtFlowRack.TabIndex = 311;
            this.rbtFlowRack.Text = "Flow Rack    ";
            this.rbtFlowRack.UseVisualStyleBackColor = false;
            this.rbtFlowRack.CheckedChanged += new System.EventHandler(this.rbtFlowRack_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(13, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 312;
            this.label5.Text = "Endereço";
            // 
            // rbtCriticaContagens
            // 
            this.rbtCriticaContagens.AutoSize = true;
            this.rbtCriticaContagens.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCriticaContagens.Location = new System.Drawing.Point(274, 246);
            this.rbtCriticaContagens.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtCriticaContagens.Name = "rbtCriticaContagens";
            this.rbtCriticaContagens.Size = new System.Drawing.Size(144, 19);
            this.rbtCriticaContagens.TabIndex = 314;
            this.rbtCriticaContagens.Text = "Crítica das Contagens";
            this.rbtCriticaContagens.UseVisualStyleBackColor = true;
            this.rbtCriticaContagens.CheckedChanged += new System.EventHandler(this.rbtCriticaContagens_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.OrangeRed;
            this.label6.Location = new System.Drawing.Point(272, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 15);
            this.label6.TabIndex = 316;
            this.label6.Text = "Tipo de Relatório de Divergência";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkOrange;
            this.label7.Location = new System.Drawing.Point(272, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 15);
            this.label7.TabIndex = 317;
            this.label7.Text = "Tipo de Relatório de Crítica";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtCaixa);
            this.panel2.Controls.Add(this.rbtFlowRack);
            this.panel2.Location = new System.Drawing.Point(15, 164);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 38);
            this.panel2.TabIndex = 318;
            // 
            // cmbEstacao
            // 
            this.cmbEstacao.Enabled = false;
            this.cmbEstacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEstacao.FormattingEnabled = true;
            this.cmbEstacao.Location = new System.Drawing.Point(15, 245);
            this.cmbEstacao.Name = "cmbEstacao";
            this.cmbEstacao.Size = new System.Drawing.Size(109, 23);
            this.cmbEstacao.TabIndex = 319;
            this.cmbEstacao.Text = "SELECIONE";
            this.cmbEstacao.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbEstacao_MouseClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(12, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 15);
            this.label10.TabIndex = 320;
            this.label10.Text = "Estação";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label11.Location = new System.Drawing.Point(273, 275);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(207, 15);
            this.label11.TabIndex = 321;
            this.label11.Text = "Tipo de Relatório de Sem Contagem";
            // 
            // rbtSemContagem
            // 
            this.rbtSemContagem.AutoSize = true;
            this.rbtSemContagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSemContagem.Location = new System.Drawing.Point(274, 296);
            this.rbtSemContagem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtSemContagem.Name = "rbtSemContagem";
            this.rbtSemContagem.Size = new System.Drawing.Size(111, 19);
            this.rbtSemContagem.TabIndex = 322;
            this.rbtSemContagem.Text = "Sem Contagem";
            this.rbtSemContagem.UseVisualStyleBackColor = true;
            this.rbtSemContagem.CheckedChanged += new System.EventHandler(this.rbtSemContagem_CheckedChanged);
            // 
            // rbtContagem3
            // 
            this.rbtContagem3.AutoSize = true;
            this.rbtContagem3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtContagem3.Location = new System.Drawing.Point(274, 176);
            this.rbtContagem3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtContagem3.Name = "rbtContagem3";
            this.rbtContagem3.Size = new System.Drawing.Size(177, 19);
            this.rbtContagem3.TabIndex = 313;
            this.rbtContagem3.Text = "Divergência da Contagem 3";
            this.rbtContagem3.UseVisualStyleBackColor = true;
            this.rbtContagem3.CheckedChanged += new System.EventHandler(this.rbtContagem3_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(480, 58);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 17);
            this.label12.TabIndex = 323;
            this.label12.Text = "Produto";
            // 
            // txtProduto
            // 
            this.txtProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduto.Location = new System.Drawing.Point(482, 76);
            this.txtProduto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(114, 23);
            this.txtProduto.TabIndex = 324;
            this.txtProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProduto_KeyPress);
            // 
            // gridProduto
            // 
            this.gridProduto.AllowUserToAddRows = false;
            this.gridProduto.AllowUserToResizeColumns = false;
            this.gridProduto.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.gridProduto.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridProduto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridProduto.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridProduto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridProduto.ColumnHeadersHeight = 29;
            this.gridProduto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridProduto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column2});
            this.gridProduto.ContextMenuStrip = this.menu;
            this.gridProduto.GridColor = System.Drawing.Color.Gainsboro;
            this.gridProduto.Location = new System.Drawing.Point(482, 103);
            this.gridProduto.MultiSelect = false;
            this.gridProduto.Name = "gridProduto";
            this.gridProduto.ReadOnly = true;
            this.gridProduto.RowHeadersVisible = false;
            this.gridProduto.RowHeadersWidth = 51;
            this.gridProduto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProduto.Size = new System.Drawing.Size(377, 257);
            this.gridProduto.TabIndex = 325;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Nº";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 40;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ID PRODUTO";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            this.Column3.Width = 125;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Produto";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniRemover});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(168, 26);
            // 
            // mniRemover
            // 
            this.mniRemover.Name = "mniRemover";
            this.mniRemover.Size = new System.Drawing.Size(167, 22);
            this.mniRemover.Text = "Remover Produto";
            this.mniRemover.Click += new System.EventHandler(this.mniRemover_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(367, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 60);
            this.pictureBox1.TabIndex = 245;
            this.pictureBox1.TabStop = false;
            // 
            // rbtSemContagem2
            // 
            this.rbtSemContagem2.AutoSize = true;
            this.rbtSemContagem2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSemContagem2.Location = new System.Drawing.Point(274, 319);
            this.rbtSemContagem2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtSemContagem2.Name = "rbtSemContagem2";
            this.rbtSemContagem2.Size = new System.Drawing.Size(121, 19);
            this.rbtSemContagem2.TabIndex = 326;
            this.rbtSemContagem2.Text = "Sem Contagem 2";
            this.rbtSemContagem2.UseVisualStyleBackColor = true;
            this.rbtSemContagem2.CheckedChanged += new System.EventHandler(this.rbtSemContagem2_CheckedChanged);
            // 
            // rbtVolumeSemContagem
            // 
            this.rbtVolumeSemContagem.AutoSize = true;
            this.rbtVolumeSemContagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtVolumeSemContagem.Location = new System.Drawing.Point(274, 341);
            this.rbtVolumeSemContagem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtVolumeSemContagem.Name = "rbtVolumeSemContagem";
            this.rbtVolumeSemContagem.Size = new System.Drawing.Size(156, 19);
            this.rbtVolumeSemContagem.TabIndex = 327;
            this.rbtVolumeSemContagem.Text = "Volume Sem Contagem";
            this.rbtVolumeSemContagem.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label14.Location = new System.Drawing.Point(629, 56);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 17);
            this.label14.TabIndex = 329;
            this.label14.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(631, 75);
            this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(135, 23);
            this.cmbEmpresa.TabIndex = 328;
            // 
            // FrmImpressaoContagens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(869, 429);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.rbtVolumeSemContagem);
            this.Controls.Add(this.rbtSemContagem2);
            this.Controls.Add(this.gridProduto);
            this.Controls.Add(this.txtProduto);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.rbtSemContagem);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbEstacao);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rbtCriticaContagens);
            this.Controls.Add(this.rbtContagem3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBloco);
            this.Controls.Add(this.cmbRegiao);
            this.Controls.Add(this.lblRua);
            this.Controls.Add(this.cmbRua);
            this.Controls.Add(this.lblRegiao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbInventario);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rbtContagem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAnalisar);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmImpressaoContagens";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impressão de Contagens";
            this.Load += new System.EventHandler(this.FrmImpressaoContagens_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProduto)).EndInit();
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbtContagem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbInventario;
        private System.Windows.Forms.ComboBox cmbRegiao;
        private System.Windows.Forms.Label lblRua;
        private System.Windows.Forms.ComboBox cmbRua;
        private System.Windows.Forms.Label lblRegiao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBloco;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLado;
        private System.Windows.Forms.RadioButton rbtCaixa;
        private System.Windows.Forms.RadioButton rbtFlowRack;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtCriticaContagens;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbEstacao;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbtSemContagem;
        private System.Windows.Forms.RadioButton rbtContagem3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.DataGridView gridProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mniRemover;
        private System.Windows.Forms.RadioButton rbtSemContagem2;
        private System.Windows.Forms.RadioButton rbtVolumeSemContagem;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbEmpresa;
    }
}