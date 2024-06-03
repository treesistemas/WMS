using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Veiculo
    {
        //Código do veículo
        public int codVeiculo { get; set; }
        //Placa do veículo
        public string placaVeiculo { get; set; }
        //Ano do veículo
        public int anoVeiculo { get; set; }
        //proprietário do veículo
        public string proprietarioVeiculo { get; set; }
        //código do rastreador
        public int codRastreador { get; set; }
        //Número do rastreador
        public int numeroRastreador { get; set; }
        //tipo de veículo
        public int codTipo { get; set; }
        //Descrição do tipo
        public string descTipo { get; set; }
        //altura do veículo
        public double alturaVeiculo { get; set; }
        //largura do veículo
        public double larguraVeiculo { get; set; }
        //comprimento do veículo
        public double comprimentoVeiculo { get; set; }
        //cubagem do veículo
        public double cubagemVeiculo { get; set; }
        //peso do veículo
        public double pesoVeiculo { get; set; }
        //status do veiculo
        public bool statusVeiculo { get; set; }

    }
}
