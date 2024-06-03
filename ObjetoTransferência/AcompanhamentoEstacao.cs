using System;

namespace ObjetoTransferencia
{
    public class AcompanhamentoEstacao
    {
        public int codEstacao {get; set;}
        public string descEstacao { get; set; }
        public string usuEstacao { get; set; }
        public int qtdProdutos { get; set; }
        public int qtdCategoria { get; set; }
        public int emAbastecimento { get; set; }
        public int pedidosEnviados { get; set; }
        public int pedidosConferidos { get; set; }
        public int produtosEnviados { get; set; }
        public int produtosConferidos { get; set; }
        public int volumesConferidos { get; set; }
        public int pedidoEmConferencia { get; set; }
        public int faltaAuditoria { get; set; }
        public int sobraAuditoria { get; set; }
        public int trocaAuditoria { get; set; }
    }
}
