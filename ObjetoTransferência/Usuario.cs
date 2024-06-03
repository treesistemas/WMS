using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Usuario
    {
        //código do usuário
        public int codUsuario { get; set; }
        //lnome do usuário
        public string nome { get; set; }
        //login do usuário
        public string login { get; set; }
        //senha do usuario
        public string senha { get; set; }
        //turno do usuario
        public string turno { get; set; }
        //status do usuário
        public bool status { get; set; }
        //controla expiração
        public bool controlaSenha { get; set; }
        //código do perfíl
        public int codPerfil { get; set; }
        //perfíl do usuario
        public string perfil { get; set; }
        //email do usuário
        public string email { get; set; }
        //dias de expirar senha
        public int diasExpirar { get; set; }
        //data de expirar senha
        public DateTime? dataExpiracao { get; set; }
        //Foto do usuário
        public byte []foto { get; set; }
        //public int confCodigo { get; set; }


    }
}
