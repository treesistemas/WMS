using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Relatorio
{
    public class ItemMapaSeparacao
    {
        public int codPedido { get; set; } //Código do pedido
        public string enderecoProduto { get; set; } //Picking do produto
        public string codProduto { get; set; } //Código do Produto
        public string descProduto { get; set; } //Descrição do produto
        public int qtdCaixaProduto { get; set; } //Caixa fechada do produto
        public string uniCaixa { get; set; } //unidade da caixa
        public int qtdUnidadeProduto { get; set; } //quantidade fracionada
        public string uniUnidade { get; set; } //unidade do fracionado
        public string lote { get; set; } //unidade do fracionado
        public int codManifesto { get; set; } //Código do Manifesto
        public string ordem { get; set; } //Ordem de separacao


    }
}
