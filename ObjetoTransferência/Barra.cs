using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Barra
    {
        
        public int idProduto { get; set; } //id do produto
        public int codBarra { get; set; } //código do código de barra
        public string numeroBarra { get; set; } //número de barra
        public int multiplicador { get; set; } //multiplicador do produto
        public double altura { get; set; } //altura do produto
        public double largura { get; set; } //largura do produto
        public double comprimento { get; set; } //comprimento do produto
        public double cubagem { get; set; } //cubagem do produto
        public double peso { get; set; } //peso do produto
        public string tipo { get; set; } //Tipo do código de barra

    }
}
