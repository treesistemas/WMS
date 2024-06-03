using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Manifesto
    {
        public DateTime dataManifesto { get; set; } //Data do manifesto
        public int codManifesto { get; set; } //código do manifesto
        public string veiculoManifesto { get; set; } //veículo do manifesto
        public double cubagemVeiculo { get; set; } //Cubagem do veículo
        public string tipoVeiculo { get; set; } //tipo do veículo
        public string tipoRota { get; set; } //tipo de rota
        public int pedidoManifesto { get; set; } //Quantidade de pedido do manifesto
        public int pedPendenteManifesto { get; set; } //Quantidade de pedido pendente do manifesto
        public int pedidoBloqueado { get; set; } //Quantidade de pedido bloqueado
        public int pendenteFlow { get; set; } //Quantidade de pedido pendente no flow rack
        public int itensManifesto { get; set; } //Quantidade de itens do manifesto
        public int volumesManifesto { get; set; } //Quantidade de volumes do manifesto
        public double pesoManifesto { get; set; } //Peso do manifesto
        public double cubagemManifesto { get; set; } //Cubagem do manifesto
        
        public string impressoManifesto { get; set; } //Status de impressão
        public string conferenteManifesto { get; set; } //Conferente do Manifesto

        public int codSeparador { get; set; } //código do separador
        public string loginSeparador { get; set; } //login do separador
        public DateTime? inicioConferencia { get; set; } //Início de Conferencia do Manifesto
        public DateTime? fimConferencia { get; set; } //Fim de Conferencia do Manifesto
        public int NFFaturar { get; set; } //Notas fiscal para faturar do Manifesto
    }
}
