using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmMensagem : Form
    {
        public string texto;
        public string cor;

        public FrmMensagem()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmMensagem_Load(object sender, EventArgs e)
        {
            timer.Tick += new System.EventHandler(timer1_Tick);
            timer.Interval = 2000; // 2 segundos
            timer.Start();
        }

        //Evento do timer ao atingir o tempo limite
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Hide();
        }

        public void Texto(string texto, string cor)
        {
            lblMensagem.Text = texto;

            if (cor.Equals("Red"))
            {
                lblMensagem.ForeColor = Color.Red;
            }

            if (cor.Equals("Green"))
            {
                lblMensagem.ForeColor = Color.Green;
            }

            if (cor.Equals("Blue"))
            {
                lblMensagem.ForeColor = Color.SteelBlue;
            }
        }
    }
}
