using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Estacao
    {
        //Código da estação
        public int codEstacao { get; set; }
        //Descrição
        public string descEstacao { get; set; }
        //Tipo
        public int nivel { get; set; }
        //Tipo
        public string tipo { get; set; }
        //Gera volume independente
        public bool volumeIndependente { get; set; }
        //Status
        public bool status { get; set; }
        //Código do usuário
        public int codUsuario { get; set; }
        //Login do usuário
        public string loginUsuario { get; set; }
        //Regiao da estação
        public int regiao { get; set; }
        //Rua da estação
        public int rua { get; set; }
        //código do bloco da estação
        public int codBloco { get; set; }
        //bloco da estação
        public int bloco { get; set; }

    }
}
