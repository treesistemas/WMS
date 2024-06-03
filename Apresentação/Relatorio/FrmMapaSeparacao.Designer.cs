namespace Wms.Relatorio
{
    partial class FrmMapaSeparacao
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
            this.crystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.PedidoMapaSeparacao1 = new Wms.Relatorio.PedidoMapaSeparacao();
            this.SuspendLayout();
            // 
            // crystalReportViewer
            // 
            this.crystalReportViewer.ActiveViewIndex = 0;
            this.crystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.crystalReportViewer.Name = "crystalReportViewer";
            this.crystalReportViewer.ReportSource = this.PedidoMapaSeparacao1;
            this.crystalReportViewer.Size = new System.Drawing.Size(1216, 554);
            this.crystalReportViewer.TabIndex = 0;
            this.crystalReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer.Load += new System.EventHandler(this.crystalReportViewer_Load);
            // 
            // FrmMapaSeparacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 554);
            this.Controls.Add(this.crystalReportViewer);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmMapaSeparacao";
            this.ShowIcon = false;
            this.Text = "Mapa de Separação";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer;
        private PedidoMapaSeparacao PedidoMapaSeparacao1;
    }
}