using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class ItensPedido
    {
        public long codItem { get; set; } //Código do pedido
        public int codPedido { get; set; } //Código do pedido
        public int codManifesto { get; set; } //Código do pedido
        public string maniPlaca { get; set; } //Código do pedido
        public string enderecoProduto { get; set; } //Picking do produto
        public int idProduto { get; set; } //Id do Produto
        public string codProduto { get; set; } //Código do Produto
        public string descProduto { get; set; } //Descrição do produto
        public double qtdProduto { get; set; } //quantidade do produto
        public double qtdConferida { get; set; } //quantidade conferida
        public double qtdCaixaConferida { get; set; } //quantidade caixa conferida
        public double qtdUnidadeConferida { get; set; } //quantidade unidade conferida
        public DateTime? dataConferencia { get; set; } //Vencimento
        public double qtdCorte { get; set; } //quantidade corte
        public DateTime? vencimentoProduto { get; set; } //Vencimento
        public string loteProduto { get; set; } //Lote do produto
        public double pesoProduto { get; set; } //peso

        public string  qtdCaixaProduto { get; set; } //Caixa fechada do produto
        public string uniCaixa { get; set; } //unidade da caixa
        public int qtdUnidadeProduto { get; set; } //quantidade fracionada
        public string uniUnidade { get; set; } //unidade do fracionado

        public double valorUnitario { get; set; } //valor unitário do item
        public double valorTotal { get; set; } //valor total do item
        public double pesoTotal { get; set; } //peso do item
        public bool pesoVariavel { get; set; } //peso variável
        public string loginUsuario { get; set; } //Login do Usuário
        public int volume { get; set; } //volumes do item


    }
}
