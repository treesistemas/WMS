using System;


namespace ObjetoTransferencia
{
    public class MonitorPedido
    {
        //código do pedido
        public int codPedido { get; set; }
        public int pedido { get; set; }
        public string tipoPedido { get; set; }
        public int? notaFiscal { get; set; }
        public int? manifesto { get; set; }
        public string rota { get; set; }
        public int codCliente { get; set; }
        public string nmCliente { get; set; }
        public string ufCliente { get; set; }
        public string cidadeCliente { get; set; }
        public string bairroCliente { get; set; }
        public string enderecoCliente { get; set; }
        public string numeroCliente { get; set; }

        public DateTime dataPedido { get; set; }
        public DateTime? dataImpressao { get; set; }
        public DateTime? dataFaturamento { get; set; }

        public DateTime? inicioConferencia { get; set; }
        public DateTime? fimConferencia { get; set; }
        public DateTime? inicioSeparacao { get; set; }
        public DateTime? fimSeparacao { get; set; }
        public string tempoConferencia { get; set; }
        public string tempoSeparacao { get; set; }
        public string nmSeparador { get; set; }
        public string nmConferente { get; set; }

        public string nmMotorista { get; set; }
        public string Placa { get; set; }
        public string marcacao { get; set; }

        public int itensPedido { get; set; }
        public double? pesoPedido { get; set; }
        public int? volumePedido { get; set; }
        public int? faltaPedido { get; set; }

        public string pedBloqueado { get; set; }
        public string pedOcorrencia { get; set; }
        public string pedReentrega { get; set; }
        public string pedMotivoBloqueio { get; set; } //ped_mot_bloqueio
        public string pedDesbloPor { get; set; } //usu_codigo_desbloqueio
        public DateTime? pedDataDesbloqueio { get; set; } // ped_data_desbloqueio
        public DateTime? pedDataImportacao { get; set; } // ped_data_importacao
        public string pedImplantador { get; set; } //ped_implantador
        public string statusConferencia { get; set; }
    }
}
