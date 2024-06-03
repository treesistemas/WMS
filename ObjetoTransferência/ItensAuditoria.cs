using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class ItensAuditoria
    {
        public int codAuditoria { get; set; }//código do apartamento
        public int codItemAuditoria { get; set; }//código do id do item na auditoria
        public DateTime? dataAuditoria { get; set; }//data da auditoria        
        public int codApartamento { get; set; }//código do apartamento 
        public string endereco { get; set; }//endereço 
        public string tipoEndereco { get; set; }//tipo endereço 
        public string tipoSeparacao { get; set; }//tipo endereço de separação 
        public int idProduto { get; set; }//id do produto    
        public string codProduto { get; set; }//código do produto    
        public string descProduto { get; set; }//descrição do produto
        public int fatorPulmao { get; set; }//fator do pulmão              
        public int estoque { get; set; }//estoque        
        public string unidadeEstoque { get; set; }//unidade do estoque         
        public int estoqueAuditado { get; set; }//Estoque auditado  
        public int estoqueFalta { get; set; }//falta no estoque
        public int estoqueSobra { get; set; } //sobra estoque
        public int estoqueProblema { get; set; }//% problema no estoque
        public string auditor { get; set; }//auditor do endereço         
        public string lote { get; set; }//lote        
        public DateTime? vencimento { get; set; }//vencimento 
        public string statusAuditoria { get; set; }//status da auditoria  

        //Resultado da auditoria
        public int qtdEntrada { get; set; }//Quantidade Entrada
        public int qtdPulmao { get; set; }//Quantidade Pulmão
        public int qtdPicking { get; set; }//Quantidade Picking
        public int qtdVolume { get; set; }//Quantidade Picking
        public int qtdSeparacao { get; set; }//Quantidade Separacao



    }
}
