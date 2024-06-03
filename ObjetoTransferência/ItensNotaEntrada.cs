

using System;

namespace ObjetoTransferencia
{
    public class ItensNotaEntrada
    {
        //Objeto controla os itens da nota, nota cega e armazenagem
        public int codApartamento { get; set; }
        public string descApartamento { get; set; }
        public int? numeroRegiao { get; set; }
        public int? numeroRua { get; set; }

        public int codNota { get; set; }
        public int codNotaCega { get; set; }
        public string tipoArmazenamento { get; set; }
        public int idProduto { get; set; }
        public string codProduto { get; set; }
        public string descProduto { get; set; }
        public int fatorPulmao { get; set; }
        public double quantidadeNota { get; set; }

        public DateTime dataConferencia { get; set; }
        public double quantidadeConferida { get; set; }
        public double quantidadeAvariada { get; set; }
        public double quantidadeFalta { get; set; }

        public string loteProduto { get; set; }
        public DateTime? validadeProduto { get; set; }
        public int paleteAssociado { get; set; }
       
        public double pesoProduto { get; set; }

        public DateTime dataArmazenamento { get; set; }
        public int quantidadeArmazenada { get; set; }

        public string undPulmao { get; set; }
        public int vidaProduto { get; set; }
        public int toleranciaProduto { get; set; }
        public int NivelMaximo { get; set; }

        public string descCategoria { get; set; }
        public bool controlaVencimentoCategoria { get; set; }
        public bool controlaVencimentoProduto { get; set; }
        public bool controlaLoteCategoria { get; set; }
       
        public string tipoPalete { get; set; }
        public bool aceitaBlocado { get; set; }
       
        //Lastro
        public int lastroPequeno { get; set; }
        public int alturaPequeno { get; set; }
        public int lastroMedio { get; set; }
        public int alturaMedio { get; set; }
        public int lastroGrande { get; set; }
        public int alturaGrande { get; set; }
        public int lastroBlocado { get; set; }
        public int alturaBlocado { get; set; }

        public string loginUsuario { get; set; }

        public bool pesoVariavel { get; set; }

    }
}
