using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Motorista
    {
        //Código
        public int codMotorista { get; set; }
        //Nome 
        public string nomeMotorista { get; set; }
        //Apelido
        public string apelidoMotorista { get; set; }
        //CNH
        public string CNHMotorista { get; set; }
        //Validade
        public DateTime validadeCNH { get; set; }
        //celular
        public string celularMotorista { get; set; }
        //status
        public bool statusMotorista { get; set; }
    }
}
