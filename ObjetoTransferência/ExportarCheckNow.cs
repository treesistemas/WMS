using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class ExportarCheckNow
    {
        public string regiaoRota { get; set; }  //
        public int qtdManifesto { get; set; }  //QUANTIDADE DE MANIFESTO
        public DateTime dataManifesto { get; set; }  //DATA ROTA
        public string plavaVeiculo { get; set; }  //PLACA
        public DateTime dataIncialConferencia { get; set; }  //DATA INICIO ROTA
        public DateTime dataFinalConferencia { get; set; }  //DATA FIM ROTA
        public string distanciaCliente { get; set; }  //DISTANCIA PLANEJADA
        public string descRota { get; set; }  //DESCRICAO ROTA
        public string nomeMotorista { get; set; }  //MOTORISTA
        public double capacidadeVeiculo { get; set; }  //TARA        
        public int sequenciaEntrega { get; set; }  //CODIGO PONTO
        public string codCliente { get; set; } //CODIGO CLIENTE
        public string nomeCliente { get; set; } //DESCRICAO DO PONTO
        public string latitudeCliente { get; set; } //LATITUDE
        public string longitudeCliente { get; set; } //LONGITUDE
        public string enderecoCliente { get; set; } //ENDERECO
        public string numeroCliente { get; set; } //NUMERO DO ENDERECO
        public string complementoCliente { get; set; } //COMPLEMENTO
        public string cepCliente { get; set; } //CEP
        public string UFCliente { get; set; } //REGIAO1
        public string cidadeCliente { get; set; } //REGIAO2
        public string bairroCliente { get; set; } //REGIAO3
        public string telefoneCliente { get; set; } //TELEFONE
        public int raioCliente { get; set; } //RAIO
        public DateTime dataChegaCliente { get; set; } //DATA CHEGADA PLANEJADA
        public DateTime dataSaidaCliente { get; set; } //DATA SAIDA PLANEJADA
        public DateTime tempoViagem { get; set; } //TEMPO VIAGEM
        public DateTime tempoEntrega { get; set; } //TEMPO ENTREGA
        public int codPedido { get; set; } //CÓDIGO DO PEDIDO
        public int numeroNotaFiscal { get; set; } //NUMERO NFE
        public double pesoPedido { get; set; } //PESO
        public double valorPedido { get; set; } //VALOR NFE
        public double cubagemPedido { get; set; } //CUBAGEM
        public int codigoRepresentante { get; set; } //CODIGO RCA
        public string nomeRepresentante { get; set; } //VENDEDOR
        public string celularRepresentante { get; set; }//TELEFONE RCA
        public string formaPagamentoPedido { get; set; }//FORMA DE PAGAMENTO
        public int codigoMotorista { get; set; }//CODIGO MOTORISTA
        public int codigoManifesto { get; set; }//MANIFESTO

    }
}
