using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Parametro
    {
        public int codEmpresa {  get; set; } //Código da empresa
        public int codItem {  get; set; } //Código do parâmetro
        public string descParametro { get; set; } //descrição do parâmetro
        public string status { get; set; } //Status do parâmetro
        public double valor {  get; set; } //Valor do parâmetro
        public int valor_II { get; set; } //Valor_II do parâmetro
    }
}
