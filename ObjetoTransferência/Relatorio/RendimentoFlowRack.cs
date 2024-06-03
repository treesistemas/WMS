using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Relatorio
{
    public class RendimentoFlowRack
    {

        public string descEstacao { get; set; } //descrição da estação
        public string nomeUsuario { get; set; } //Nome do usuário
        public int pedido { get; set; } //Pedido
        public int acesso { get; set; } //Acesso
        public int qtdSeparada { get; set; } //Quantidade separada
        public int volume { get; set; } //Volume
        public int totalVolume { get; set; } //total de volumes produzidos
        public int totalPedido { get; set; } //total de pedidos produzidos
        public string empresa { get; set; } //nome da empresa
        public DateTime dataInicial { get; set; } //data inicial
        public DateTime dataFinal { get; set; } //data final
    }
}
