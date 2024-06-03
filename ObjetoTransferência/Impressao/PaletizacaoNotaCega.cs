using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Impressao
{
    public class PaletizacaoNotaCega
    {
        public string empresa { get; set;}
        public int notaCega {get; set;}
        public string fornecedor { get; set; }
        public string usuario { get; set; }
        public string descProduto { get; set; }
        public int fatorPulmao { get; set; }
        public string unidadePulmao { get; set; }
        public string controlaVencimento { get; set; }

        public int vidaUtil { get; set; }
        public int tolerancia { get; set; }
        public int diasMaxima { get; set; }
        public DateTime dataMaxima { get; set; }
        public int lastroP { get; set; }
        public int alturaP { get; set; }
        public int lastroM { get; set; }
        public int alturaM { get; set; }
        public int lastroG { get; set; }
        public int alturaG { get; set; }
        public int lastroB { get; set; }
        public int alturaB { get; set; }
        public string codProdutoFornecedor { get; set; }




    }
}
