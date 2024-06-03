using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class ItensOcorrencia
    {
        public int codOcorrencia { get; set; } //Código da ocorrência
        public int codItemOcorrencia { get; set; } //Código do item ocorrência
        public int codPedido { get; set; } //Código do pedido
        public int idProduto { get; set; } //ID do produto
        public string codProduto { get; set; } //Código do Produto
        public string descProduto { get; set; } //Descrição do Produto
        public double qtdProduto { get; set; } //Quantidade do produto
        public string fatorProduto { get; set; } //Fator do Produto
        public double? qtdFaltaProduto { get; set; } //Falta do produto
        public double? qtdAvariaProduto { get; set; } //Avaria do Produto
        public double? qtdTrocaProduto { get; set; } //Troca do Produto
        public Double? qtdCriticaProduto { get; set; } //Quantidade critica do Produto
        public double? qtdDevolucao { get; set; } //Devolucao do produto
        public double? qtdErp { get; set; } //Estoque ERP
        public double? qtdWMS { get; set; } //Estoque WMS
        public DateTime? DataCriticaProduto { get; set; } //Data critica do produto
        public double valorProduto { get; set; } //Valor do produto
        public double valorUnitario { get; set; } //Valor Initario

        public string descMotivo { get; set; } //descrição do motivo
        public string obsOcorrencia { get; set; } //observação da ocorrência
        public string obsAuditoria { get; set; } //observação da auditoria
        public DateTime dataOcorrencia { get; set; } //data da ocorrência
        public DateTime? dataAuditoria { get; set; } //data da auditoria
        public string descricaoOcorrencia { get; set; } //descrição da ocorrência
        public string statusOcorrencia { get; set; } //Status da ocorrência
        public string nomeMonitor { get; set; } //Nome do monitor
        public byte []fotoAuditoria { get; set; } //Foto da auditoria
        public int codAuditor { get; set; } //Código da auditoria
        public string nomeAuditor { get; set; } //Nome da auditoria

        public int codUsuarioErro { get; set; } //Código do usuário que errou
        public string nomeUsuarioErro { get; set; } //Nome do usuário que errou
        public string reentregaOcorrencia { get; set; } //Reentrega
        public string apelidoMotorista { get; set; } //Apelido do motorista
        public string clienteAguardo { get; set; } //cliente que fica no aguardo
        public string unidadeFracionada { get; set; } //unidade
    }
}
