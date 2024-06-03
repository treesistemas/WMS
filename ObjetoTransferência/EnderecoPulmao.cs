using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class EnderecoPulmao
    {
        public string nomeEmpresa { get; set; }//Descrição da empresa        
        public string tipoOperacao { get; set; }//tipo de operação (rastremento)
        public DateTime? dataOperacao { get; set; }//Data da operação  

        public int notaCega { get; set; }//número da nota cega        
        public int idProduto { get; set; }//id do produto 

        public string codProduto { get; set; }//código do produto
        public string descProduto { get; set; }//descrição do produto

        public int fatorPulmao { get; set; }//fator do pulmão  
        public int fatorPicking { get; set; }//fator do pulmão 
        public double pesoCxa { get; set; } //peso da caixa        
        public int codApartamento1 { get; set; }//id endereço pulmão        
        public string descEndereco1 { get; set; }//endereço        
        public double qtdCaixaOrigem { get; set; }//estoque
        public double qtdCxaTranferidaOrigem { get; set; }//estoque
        public double qtdUndTranferidaOrigem { get; set; }//estoque
        public double qtdUnidadeOrigem { get; set; }//estoque   
        public string undCaixaOrigem { get; set; }//unidade do estoque
        public string undUnidadeOrigem { get; set; }//unidade do estoque
        public string loteProduto1 { get; set; }//lote        
        public double pesoProduto1 { get; set; } //peso      
        public DateTime? vencimentoProduto1 { get; set; }//validade  
        public string tipoApartamento1 { get; set; }//tipo         
        public string tamanhoApartamento1 { get; set; }//Tamanho do endereço

        public int codApartamento2 { get; set; }//id endereço pulmão        
        public string descApartamento2 { get; set; }//endereço        
        public int qtdCaixaDestino { get; set; }//estoque        
        public double qtdUnidadeDestino { get; set; }//estoque   
        public string undCaixaDestino { get; set; }//unidade do estoque
        public string undUnidadeDestino { get; set; }//unidade do estoque
        public string loteProduto2 { get; set; }//lote        
        public double pesoProduto2 { get; set; } //peso      
        public DateTime? vencimentoProduto2 { get; set; }//validade  
        public string tipoApartamento2 { get; set; }//tipo         
        public string tamanhoApartamento2 { get; set; }//Tamanho do endereço


        public string reserva { get; set; }//reserva true ou false  
        public int estoqueReservado { get; set; }//Estoque reservado  
        public string bloqueado { get; set; }//bloqueado
        public string motivoBloqueio { get; set; } //Motivo
        public int codAbastecimento { get; set; }//código do abstecimento
        public int reservaAbastecimento { get; set; }//reserva abastecimento              
        public DateTime? dataEntrada { get; set; }//entrada        
        public string impresso { get; set; }//impresso        
        public DateTime? dataMaxTolerancia { get; set; }//próximo vencimento        
        public DateTime? dataMimTolerancia { get; set; }//próximo vencido
        public int qtdProdutoPalete { get; set; } //quantidade de produto no palete
        public int? codUsuario { get; set; } //usuario que armazenou
        public string loginUsuario { get; set; }//login do usuário
        public int alturaM { get; set; } //quantidade da altura do palete
        public int lastroM { get; set; } //quantidade do lastro do palete

    }
}
