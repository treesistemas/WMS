using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Acesso
    {
        //código de acesso
        public int codAcesso { get; set; }
        //código usuário
        public int codUsuario { get; set; }
        //código usuário
        public int codPerfil { get; set; }
        //código da função
        public int codFuncao { get; set; }
        //descrição da funcão
        public string descFuncao { get; set; }
        //função pai
        public string paiFuncao { get; set; }
        //funcão filho
        public string filhoFuncao { get; set; }
        //funcão item filho
        public string itemFilhoFuncao { get; set; }
        //ordem função
        public string ordemFuncao { get; set; }

        //visualisar
        public bool lerFuncao { get; set; }
        //salvar
        public bool escreverFuncao { get; set; }
        //editar
        public bool editarFuncao { get; set; }
        //excluir
        public bool excluirFuncao { get; set; }

    }

}
