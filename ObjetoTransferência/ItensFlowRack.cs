using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class ItensFlowRack
    {
        public DateTime? dataProcessamento { get; set; } //Data de processamento - (? = Recebe null)
        public int codEstacao { get; set; } //Estaçao do produto
        public string descEstacao { get; set; } //descrição da estação
        public int bloco { get; set; } //Bloco do Produto
        public int apartamento { get; set; } //Apartamento do Produto
        public string endereco { get; set; } //Endereço do produto
        public string tipoEndereco { get; set; } //Tipo do endereço
        public int idProdutoVolume { get; set; } //id do Volume do Produto                                                
        public long notaFiscal { get; set; } //Nota Fiscal
        public int codPedido { get; set; } //Código do pedido
        public string barraVolume { get; set; } //Número do Volume
        public int numeroVolume { get; set; } //Número do Volume
        public int idProduto { get; set; } //Id do Produto
        public string codProduto { get; set; } //Código do Produto
        public string descProduto { get; set; } //Descrição do Produto
        public int qtdProduto { get; set; } //Quantidade do Produto
       
        public int qtdConferidaProduto { get; set; } //Quantidade conferida
        public int qtdCorteProduto { get; set; } //Quantidade de corte
        public string uniProduto { get; set; } //unidade do Produto

        public DateTime? dataConferencia { get; set; } //Data de conferência - (? = Recebe null)
        public DateTime? dataAuditoria { get; set; } //Data de auditoria - (? = Recebe null)
        public string auditaProduto { get; set; } //Auditoria do Produto
        public DateTime validadeProduto { get; set; } //Validade do Produto na área de separação
        public double pesoProduto { get; set; } //Peso do Produto
        public double cubagemProduto { get; set; } //Cubagem do Produto
        public string descCategoria { get; set; } //Categoria do Produto

        public string nomeUsuario { get; set; } //Nome do usuário
        public string perfilUsuario { get; set; } //Nome do usuário
        public string turnoUsuario { get; set; } //Nome do usuário
        public string nomeAuditor { get; set; } //Nome do auditor
        public string nomeEndereco { get; set; } //Nome do endereço

        public int codEstacaoAtual { get; set; } //código da estação atual


    }
}
