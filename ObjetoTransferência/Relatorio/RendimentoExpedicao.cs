using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Relatorio
{
    public class RendimentoExpedicao
    {
        public string empresa { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }
        public string login { get; set; }
        public int qtdPedido { get; set; }
        public int qtdAcesso { get; set; }
        public int qtdItem { get; set; }
        public int qtdVolume { get; set; }
        public int qtdSobra { get; set; }
        public int qtdFalta { get; set; }
        public int qtdTroca { get; set; }
        public int qtdAvaria { get; set; }
        public double peso { get; set; }
        public TimeSpan tempo { get; set; }
        public double segundos { get; set; }
        public int qtdTotal { get; set; } //Soma da qtd de acesso + a quantidade de volumes do flow rack
    }
}
