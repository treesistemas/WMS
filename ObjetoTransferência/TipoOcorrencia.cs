using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class TipoOcorrencia
    {
        public int codigo { get; set; } //Codigo do tipo de ocorrência
        public string descricao { get; set; } //Descrição do tipo de ocorrência
        public string area { get; set; } //Área da ocorrência

        public bool ativo { get; set; } //Status da ocorrência
    }
}
