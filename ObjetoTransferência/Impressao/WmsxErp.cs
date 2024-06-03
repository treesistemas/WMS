using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Impressao
{
    public class WmsxErp
    {
        public string empresa { get; set; }
        public string usuario { get; set; }
        public string endereco { get; set; }
        public int regiao { get; set; }
        public int rua { get; set; }
        public string lado { get; set; }
        public string produto { get; set; }
        public int estoqueEntrada { get; set; }
        public int estoquePicking { get; set; }
        public int estoquePickingFlowRack { get; set; }
        public int estoquePulmao { get; set; }
        public int estoqueVolume { get; set; }        
        public int estoqueEmconferencia { get; set; }
        public int estoqueDevolucao { get; set; }
        public int estoqueCancelados { get; set; }
        public int estoqueErp { get; set; }
        public string unidade { get; set; }


    }
}
