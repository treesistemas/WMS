using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Impressora
    {
        public int codigo {  get; set; } //Código 
        public string IP { get; set; } //Protocolo de Internet
        public string nome { get; set; } //Nome da impressora
        public string descricao { get; set; } //Descrição da impressora
        public int? estacao { get; set; } //Estação da impressora

    }
}
