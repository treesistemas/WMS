using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class SaidaManifestoModel
    {
        public int Numero{ get; set; }
        public int Manicodigo {  get; set; }
        public DateTime? ManiData { get; set; }
        public DateTime? Maniprogramado { get; set; }
        public int? Motcodigo { get; set; }
        public int? Veicodigo { get; set; }
        public string Empresa { get; set; }
        public string Motorista { get; set; }
        public string Veiculo { get; set; }
        public int PedTotal { get; set; }
        public int? PedCodigo { get; set; }
        public double PesoPedido { get; set; }
        public double Valor { get; set; }
        public int QtPedido {  get; set; }
        public string Regiao { get; set; }
        public string PedStatus { get; set; }
        public int QtCliente { get; set; }
        public int EfCliente { get; set; }
        public double porCliente { get; set; }
        public int EfPedido { get; set; }
        public double porPedido { get; set; }
        public int tipoRota { get; set; }
        public string Portatil { get; set; }
    }
}
