
namespace ObjetoTransferencia
{
    public class Categoria
    {
        //código da categoria
        public int codCategoria { get; set; }
        //descrição da categoria
        public string descCategoria { get; set; }
        //controla auditoria do flow rack
        public bool auditaFlow { get; set; }
        //controla validade
        public bool controlaValidade { get; set; }
        //controla lote
        public bool controlaLote { get; set; }

    }
}
