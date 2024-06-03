using System;

namespace ObjetoTransferencia
{
    public class NotaEntrada
    {
        //Nota de Entrada
        public DateTime dataNota { get; set; }
        public int nota { get; set;}
        public int codFornecedor { get; set; }
        public string nmFornecedor { get; set; }
        public double pesoNota { get; set; }
        public DateTime dataNotaCega { get; set; }
        public int? codNotaCega { get; set; }
        public int codUsuarioNotaCega { get; set; }
        public string usuarioNotaCega { get; set; }
        public DateTime? inicioConferencia { get; set; }
        public DateTime? fimConferencia { get; set; }
        public string tempoConferencia { get; set; }
        public string conferente { get; set; }

        public bool liberarEstoque { get; set; }
        public bool liberarArmazenagem { get; set; }
        public bool exigirValidade { get; set; }
        public bool armzenagemPicking { get; set; }
        public bool tipoRelatorio { get; set; }
        public bool crossDocking{ get; set; }

        //Utilizado na conferencia da nota cega
        public int quantidadeNota { get; set; }
        public int quantidadeItens { get; set; }
        //public string empresa { get; set; }

    }
}
