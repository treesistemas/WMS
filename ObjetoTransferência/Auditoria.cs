using System;

namespace ObjetoTransferencia
{
    public class Auditoria
    {
        public int codAuditoria { get; set; } //código da auditoria
        public DateTime dataAuditoria { get; set; } //data da auditoria
        public string tipoAuditoria { get; set; } //tipo de auditoria
        public string responsavel { get; set; } //responsável da auditoria
       

        public int regiao { get; set; } //região da auditoria
        public int ruaIncial { get; set; } //rua inicial da auditoria
        public int ruaFinal { get; set; } //rua fina da auditoria
        public string tipoArmazenamento { get; set; } //tipo de armazenamento
        public string ladoRua { get; set; } //lado da rua

        public bool tipoRelatorio { get; set; } //auditoria tipo relatório
        public bool exigeContagem { get; set; } //exigem contagem
        public bool exigemVencimento { get; set; } //exige vencimento
        public bool exigeLote { get; set; } //exige lote
        public bool exigeBarra { get; set; } //exige código de barra

        public int qtdEndereco { get; set; } //quantidade de endereço da auditoria
        public int problemas { get; set; } //problemas da auditoria

        public string status { get; set; } //status


    }
}
