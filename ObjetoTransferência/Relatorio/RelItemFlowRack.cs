using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Relatorio
{
    public class RelItemFlowRack
    {
        public string empresa { get; set; } //Nome da empresa
        public string cliente { get; set; } //Nome do cliente
        public int pedido { get; set; } //código do pedido
        public string volume { get; set; } //Número do Volume
        public string estacao { get; set; } //descrição da estação
        public string endereco { get; set; } //Endereço do produto
        public string produto { get; set; } //Descrição do Produto
        public int quantidade { get; set; } //Quantidade conferida
        public string unidade { get; set; } //unidade do Produto
        public string conferente { get; set; } //Nome do conferente


    }
}
