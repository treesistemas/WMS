using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Relatorio
{
    public class ItemMapaAbastecimento
    {
        public int codAbastecimento { get; set; } //Código do abastecimento
        public int codEstacao { get; set; } //Código da estação
        public string enderecoPicking { get; set; } //Endereço do Picking
        public string codProduto { get; set; } //Código do Produto
        public string descProduto { get; set; } //Descrição do Produto
        public string descCategoria { get; set; } //Categoria do Produto

        //Informações para abastecimento
        public int qtdAbastecer { get; set; } //Quantidade para abastecer
        
        //Endereço que servirá para o abastecimento
        public string enderecoPulmao { get; set; } //Endereço do Pulmão 
        public int qtdPulmao { get; set; } //Quantidade a abastecer do Pulmão 
        public string unidadePulmao { get; set; } //Tipo de abastecimento (unidade do pulmão)
        public DateTime vencimentoPulmao { get; set; } //Validade do Pulmão 
        public string lotePulmao { get; set; } //lote do Pulmão 
      
    }
}
