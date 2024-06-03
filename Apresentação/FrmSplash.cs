using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Wms
{
    public partial class FrmSplash : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
                int nTopRect,
                int RightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
        );

        public FrmSplash()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            progressBar.Value = 0;
        }


        private void tempo_Tick(object sender, EventArgs e)
        {
            //Adiciona o valor
            progressBar.Value += 5;
            //Exibe o texto
            progressBar.Text = progressBar.Value.ToString() + "%";

            //Para o time e exibe o frame de login
            if (progressBar.Value == 100)
            {
                tempo.Enabled = false;
                FrmLogin frame = new FrmLogin();
                frame.versao = lblVersao.Text;
                frame.Show();
                this.Hide();



            }

        }
    }
}
