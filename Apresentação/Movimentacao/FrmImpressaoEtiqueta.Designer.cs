namespace Wms.Movimentacao
{
    partial class FrmImpressaoEtiqueta
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkVolume = new System.Windows.Forms.CheckBox();
            this.txtInicio = new System.Windows.Forms.TextBox();
            this.lblInicio = new System.Windows.Forms.Label();
            this.txtFim = new System.Windows.Forms.TextBox();
            this.lblFim = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.DimGray;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(371, 230);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 32);
            this.btnCancelar.TabIndex = 95;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 274);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 25);
            this.panel1.TabIndex = 92;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(16, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(306, 39);
            this.label5.TabIndex = 90;
            this.label5.Text = "Impressão etiqueta";
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.SteelBlue;
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(263, 230);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(95, 32);
            this.btnImprimir.TabIndex = 89;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // txtVolume
            // 
            this.txtVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVolume.Location = new System.Drawing.Point(23, 111);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(142, 24);
            this.txtVolume.TabIndex = 85;
            this.txtVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVolume_KeyPress);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.Location = new System.Drawing.Point(20, 90);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(54, 18);
            this.lblTipo.TabIndex = 84;
            this.lblTipo.Text = "Pedido";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Wms.Properties.Resources.empresa;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(361, 9);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 73);
            this.pictureBox1.TabIndex = 91;
            this.pictureBox1.TabStop = false;
            // 
            // chkVolume
            // 
            this.chkVolume.AutoSize = true;
            this.chkVolume.Location = new System.Drawing.Point(306, 111);
            this.chkVolume.Name = "chkVolume";
            this.chkVolume.Size = new System.Drawing.Size(160, 20);
            this.chkVolume.TabIndex = 96;
            this.chkVolume.Text = "Volume de Flow Rack";
            this.chkVolume.UseVisualStyleBackColor = true;
            this.chkVolume.CheckedChanged += new System.EventHandler(this.chkVolume_CheckedChanged);
            // 
            // txtInicio
            // 
            this.txtInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInicio.Location = new System.Drawing.Point(23, 172);
            this.txtInicio.Name = "txtInicio";
            this.txtInicio.Size = new System.Drawing.Size(61, 24);
            this.txtInicio.TabIndex = 98;
            this.txtInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInicio_KeyPress);
            // 
            // lblInicio
            // 
            this.lblInicio.AutoSize = true;
            this.lblInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInicio.Location = new System.Drawing.Point(20, 151);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(42, 18);
            this.lblInicio.TabIndex = 97;
            this.lblInicio.Text = "Início";
            // 
            // txtFim
            // 
            this.txtFim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFim.Location = new System.Drawing.Point(101, 172);
            this.txtFim.Name = "txtFim";
            this.txtFim.Size = new System.Drawing.Size(64, 24);
            this.txtFim.TabIndex = 100;
            this.txtFim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFim_KeyPress);
            // 
            // lblFim
            // 
            this.lblFim.AutoSize = true;
            this.lblFim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFim.Location = new System.Drawing.Point(98, 151);
            this.lblFim.Name = "lblFim";
            this.lblFim.Size = new System.Drawing.Size(33, 18);
            this.lblFim.TabIndex = 99;
            this.lblFim.Text = "Fim";
            // 
            // FrmImpressaoEtiqueta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(479, 299);
            this.Controls.Add(this.txtFim);
            this.Controls.Add(this.lblFim);
            this.Controls.Add(this.txtInicio);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.chkVolume);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.txtVolume);
            this.Controls.Add(this.lblTipo);
            this.Name = "FrmImpressaoEtiqueta";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impressão de Etiqueta";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.CheckBox chkVolume;
        private System.Windows.Forms.TextBox txtInicio;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.TextBox txtFim;
        private System.Windows.Forms.Label lblFim;
    }
}