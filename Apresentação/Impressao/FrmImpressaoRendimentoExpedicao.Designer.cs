
namespace Wms
{
    partial class FrmImpressaoRendimentoExpedicao
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
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtTodos = new System.Windows.Forms.RadioButton();
            this.rbtConferencia = new System.Windows.Forms.RadioButton();
            this.rbtSeparacao = new System.Windows.Forms.RadioButton();
            this.txtDataInicial = new System.Windows.Forms.MaskedTextBox();
            this.txtDataFinal = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.BackColor = System.Drawing.Color.DimGray;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(485, 259);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 38);
            this.btnSair.TabIndex = 257;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnAnalisar
            // 
            this.btnAnalisar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalisar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAnalisar.ForeColor = System.Drawing.Color.White;
            this.btnAnalisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnalisar.Location = new System.Drawing.Point(360, 259);
            this.btnAnalisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(117, 38);
            this.btnAnalisar.TabIndex = 256;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = false;
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 315);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 25);
            this.panel1.TabIndex = 255;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(17, 88);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 20);
            this.label17.TabIndex = 254;
            this.label17.Text = "Período";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.SteelBlue;
            this.label18.Location = new System.Drawing.Point(12, 5);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(417, 39);
            this.label18.TabIndex = 251;
            this.label18.Text = "Rendimento da Expedição";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(479, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 74);
            this.pictureBox1.TabIndex = 250;
            this.pictureBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Location = new System.Drawing.Point(17, 152);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 16);
            this.label8.TabIndex = 262;
            this.label8.Text = "Tipo de Relatório";
            // 
            // rbtTodos
            // 
            this.rbtTodos.AutoSize = true;
            this.rbtTodos.BackColor = System.Drawing.Color.DimGray;
            this.rbtTodos.Checked = true;
            this.rbtTodos.ForeColor = System.Drawing.Color.White;
            this.rbtTodos.Location = new System.Drawing.Point(19, 176);
            this.rbtTodos.Name = "rbtTodos";
            this.rbtTodos.Size = new System.Drawing.Size(68, 20);
            this.rbtTodos.TabIndex = 260;
            this.rbtTodos.TabStop = true;
            this.rbtTodos.Text = "Todos";
            this.rbtTodos.UseVisualStyleBackColor = false;
            // 
            // rbtConferencia
            // 
            this.rbtConferencia.AutoSize = true;
            this.rbtConferencia.Location = new System.Drawing.Point(19, 203);
            this.rbtConferencia.Name = "rbtConferencia";
            this.rbtConferencia.Size = new System.Drawing.Size(100, 20);
            this.rbtConferencia.TabIndex = 261;
            this.rbtConferencia.Text = "Conferência";
            this.rbtConferencia.UseVisualStyleBackColor = true;
            this.rbtConferencia.Visible = false;
            // 
            // rbtSeparacao
            // 
            this.rbtSeparacao.AutoSize = true;
            this.rbtSeparacao.Location = new System.Drawing.Point(19, 230);
            this.rbtSeparacao.Name = "rbtSeparacao";
            this.rbtSeparacao.Size = new System.Drawing.Size(96, 20);
            this.rbtSeparacao.TabIndex = 264;
            this.rbtSeparacao.Text = "Separação";
            this.rbtSeparacao.UseVisualStyleBackColor = true;
            this.rbtSeparacao.Visible = false;
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicial.Location = new System.Drawing.Point(21, 111);
            this.txtDataInicial.Mask = "##/##/#### ##:##:##";
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(195, 27);
            this.txtDataInicial.TabIndex = 265;
            this.txtDataInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataInicial_KeyPress);
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Location = new System.Drawing.Point(230, 111);
            this.txtDataFinal.Mask = "##/##/#### ##:##:##";
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(195, 27);
            this.txtDataFinal.TabIndex = 266;
            this.txtDataFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataFinal_KeyPress);
            // 
            // FrmImpressaoRendimentoExpedicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(602, 340);
            this.Controls.Add(this.txtDataFinal);
            this.Controls.Add(this.txtDataInicial);
            this.Controls.Add(this.rbtSeparacao);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rbtConferencia);
            this.Controls.Add(this.rbtTodos);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAnalisar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.Name = "FrmImpressaoRendimentoExpedicao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbtTodos;
        private System.Windows.Forms.RadioButton rbtConferencia;
        private System.Windows.Forms.RadioButton rbtSeparacao;
        private System.Windows.Forms.MaskedTextBox txtDataInicial;
        private System.Windows.Forms.MaskedTextBox txtDataFinal;
    }
}