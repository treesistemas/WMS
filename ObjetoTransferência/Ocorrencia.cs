using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Ocorrencia
    {
        public int codOcorrencia { get; set; } //Código da ocorrencia
        public DateTime dataOcorrencia { get; set; } //Data da ocorrencia
        public string areaOcorrencia { get; set; } //Area da Ocorrencia
        public int codTipoOcorrencia { get; set; } //Código do tipo ocorrencia
        public string descTipoOcorrencia { get; set; } //descrição do tipo de Ocorrencia
        public bool gerarPendencia { get; set; } //gerar pendencia da Ocorrencia
        public bool gerarClienteAguardo { get; set; } //gerar pendencia para o cliente da Ocorrencia
        public bool gerarDevolucao { get; set; } //gerar devolução da Ocorrencia
        public bool gerarReentrega { get; set; } //gerarReentrega da Ocorrencia
        public DateTime dataReentrega { get; set; } //Data da reentrega
        public string obsOcorrencia { get; set; } //observencia da Ocorrencia
        public int codUsuarioOcorrencia { get; set; } //Código do usuário ocorrencia
        public string loginUsuario { get; set; } //login do usuário da Ocorrencia
        public string statusOcorrencia { get; set; } //status da Ocorrencia
        public int codPedido { get; set; } //Código do pedido
        public DateTime dataFaturamento { get; set; } //Data da nota fiscal
        public int notaFiscal { get; set; } //Número da nota Fiscal
        public string tipoPedido { get; set; } //Tipo de pedido
        public string pagamentoPedido { get; set; } //Pagamento de pedido
        public double totalPedido { get; set; } //Valor total
        public double pesoPedido { get; set; } //Peso total
        public int manifesto { get; set; } //Manifesto ocorrencia
        public int manifestoOcorrencia { get; set; } //Manifesto reentrega
        public string veiculoPedido { get; set; } //Placa
        public int codMotorista { get; set; } //Código do motorista ocorrencia
        public int codMotoristaOcorrencia { get; set; } //Código do motorista reentrega
        public string nomeMotorista { get; set; } //Motorista
        public string motorista { get; set; } //Apelido do motorista ocorrencia
        public string motoristaOcorrencia { get; set; } //Apelido do motorista reentrega
        public string celularMotorista { get; set; } //Contado do motorista
        public string obsPedido { get; set; } //Observação do pedido
        public string codCliente { get; set; } //Código do cliente
        public string nomeCliente { get; set; } //Nome do cliente
        public string fantasiaCliente { get; set; } //Nome fantasia do cliente
        public string emailCliente { get; set; } //Email do cliente
        public string ufCliente { get; set; } //UF do cliente
        public string cidadeCliente { get; set; } //cidade do cliente
        public string bairroCliente { get; set; } //Bairro do cliente
        public string enderecoCliente { get; set; } //Endereço do cliente
        public string numeroCliente { get; set; } //Número do cliente
        public string nomeRepresentante { get; set; } //Nome do representante
        public string celularRepresentante { get; set; } //celularRepresentante
        public string nomeSupervisor { get; set; } //Nome do representante
        public string celularSupervisor { get; set; } //celularRepresentante
        public string pedImplantador { get; set; } //Usuário que implantou o pedido

        public string tempoOcorrencia { get; set; } //Tempo de Ocorrência
        
        public string nomeEmpresa { get; set; } //Nome da Empresa
        public string rotaOcorrencia { get; set; } //Nome da Empresa



    }
}
