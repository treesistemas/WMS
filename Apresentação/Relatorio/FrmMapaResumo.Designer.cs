
namespace Wms.Relatorio
{
    partial class FrmMapaResumo
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
            this.crvMapaResumo = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.ResumoMapaSeparacao1 = new Wms.Relatorio.ResumoMapaSeparacao();
            this.SuspendLayout();
            // 
            // crvMapaResumo
            // 
            this.crvMapaResumo.ActiveViewIndex = 0;
            this.crvMapaResumo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvMapaResumo.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvMapaResumo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvMapaResumo.Location = new System.Drawing.Point(0, 0);
            this.crvMapaResumo.Name = "crvMapaResumo";
            this.crvMapaResumo.ReportSource = this.ResumoMapaSeparacao1;
            this.crvMapaResumo.Size = new System.Drawing.Size(1028, 450);
            this.crvMapaResumo.TabIndex = 0;
            this.crvMapaResumo.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FrmMapaResumo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 450);
            this.Controls.Add(this.crvMapaResumo);
            this.Name = "FrmMapaResumo";
            this.ShowIcon = false;
            this.Text = "Resumo de Carga";
            this.Load += new System.EventHandler(this.FrmMapaResumo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvMapaResumo;
        private ResumoMapaSeparacao ResumoMapaSeparacao1;
    }
}