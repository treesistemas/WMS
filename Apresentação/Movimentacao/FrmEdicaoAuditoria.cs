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
    public partial class FrmEdicaoAuditoria : Form
    {
        public int codUsuario;
        public int codItemAuditoria; //Picking ou Pulmao
        public int codEndereco;
        public string endereco;
        public int idProduto;
        public string codProduto;
        public string descProduto;
        public int qtdCaixa;
        public int estoque;
        public string tipoEstoque;
        public DateTime validade;
        public string lote;

        public FrmEdicaoAuditoria()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}
