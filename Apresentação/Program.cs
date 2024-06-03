using System;
using System.Windows.Forms;
using Wms.Expedicao;
using Wms.Impressao;
using Wms.Integracao;
using Wms.Inventario;
using Wms.Movimentacao;
using Wms.Relatorio;

namespace Wms
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmResumoSaida());
            Application.Run(new FrmSplash());

        }
    }
}
