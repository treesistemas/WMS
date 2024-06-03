using System;

namespace ObjetoTransferencia
{
    public class Inventarios
    {
        public int codInventario { get; set; } //código do inventário
        public string descInventario { get; set; } //descrição do inventário
        public int codUsuarioInicial { get; set; } //código do usuario que abre o inventário
        public string usuarioInicial { get; set; } //login do usuário que abre o inventário
        public int codUsuarioFinal { get; set; } //código do usuario que fecha o inventário
        public DateTime dataInicial { get; set; } //data inicial do inventário
        public DateTime? dataFinal { get; set; } //data final do inventário
        public string tipoInventario { get; set; } //tipo do inventário
        public string descRotativo { get; set; } //descrição do tipo de inventário rotativo
        public string tipoAuditoria { get; set; } //tipo de auditoria do inventário
        public bool importarERP { get; set; } //opção de importar o inventário para o erp
        public bool contPicking { get; set; } //opção de contar a primeira contagem do picking automática
        public bool contPickingFlow { get; set; } //opção de contar a primeira contagem do picking do flow rack automática
        public bool contPulmao { get; set; } ////opção de contar a primeira contagem do pulmão automática
        public bool contAvaria { get; set; } ////opção de contar a avaria no invetário
        public bool contVolumeFlow { get; set; } ////opção de contar os volumes do flow rack automática
        public bool contVencimento { get; set; } ///opção de controlar o vencimento
        public bool contLote { get; set; } ////opção de controlar  vencimento
        public string statusInventario { get; set; } //status do inventário

    }
}
