using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class ItemPendencia
    {
        public int codOcorrencia { get; set; } //Código da ocorrência
        public string endereco { get; set; } //Descrição do Produto
        public string descProduto { get; set; } //Descrição do Produto
        public double qtdFaltaProduto { get; set; } //Falta do produto
        public double qtdAvariaProduto { get; set; } //Avaria do Produto
        public double qtdTrocaProduto { get; set; } //Troca do Produto
        public double qtdCriticaProduto { get; set; } //Quantidade critica do Produto
        public string unidadeFracionada { get; set; } //unidade
        public string controlaPeso { get; set; } //unidade
    }
}
