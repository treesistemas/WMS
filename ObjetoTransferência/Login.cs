using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Login
    {

        public int codUsuario { get; set; }//código do usuário        
        public string loginUsuario { get; set; } //Login       
        public string senhaUsuario { get; set; } //senha   
        public int codPerfil { get; set; }//código do perfíl     
        public string descPerfil { get; set; } //descrição do perfil
        public DateTime dataExpiracao { get; set; } //expiração da data                                                    //
        public int codEstacao { get; set; }//código da estação
        public int nivelEstacao { get; set; }//nível da estação
        public string nomeEmpresa { get; set; } //Nome da empresa
        public string impressora { get; set; } //Impressora
        public string controlaSequenciaCarregamento { get; set; } //Sequencia de transportadora
        //Foto do usuário
        public byte[] foto { get; set; }
    }
}
