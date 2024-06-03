using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Produto
    {        
        public int idProduto { get; set; }//id do produto       
        public string codProduto { get; set; }  //código do produto
        public string descProduto { get; set; }//descrição do produto        
        public int codFornecedor { get; set; }//código do fornecedor       
        public string nomeFornecedor { get; set; } //descrição do fornecedor       
        public int shelfLife { get; set; } //vida útil em dias       
        public int tolerancia { get; set; } //tolerância em dias
        public DateTime dataTolerancia { get; set; } //validade dentro da tolerância
        public int nivelMaximo { get; set; }//nível máximo        
        public string tipoArmazenagem { get; set; }//tipo de armazenagem        
        public string tipoPalete { get; set; }//tipo de palete        
        public int codCategoria { get; set; }//código da categoria       
        public string descCategoria { get; set; } //descricao da categoria        
        public int multiploProduto { get; set; }//multiplo de venda
        public double pesoProduto { get; set; }//peso do produto atualizado por uma trigger        
        public int lastroPequeno { get; set; }//lastro do palete pequeno        
        public int alturaPequeno { get; set; }//altura do palete pequeno
        public int totalPequeno { get; set; } //total do palete pequeno
        public int lastroMedio { get; set; }//lastro do palete médio          
        public int alturaMedio { get; set; }//altura do palete medio
        public int totalMedio { get; set; }//total do palete medio        
        public int lastroGrande { get; set; }//lastro do palete grande        
        public int alturaGrande { get; set; }//altura do palete grande
        public int totalGrande { get; set; }//total do palete grande        
        public int lastroBlocado { get; set; }//lastro do palete blocado
        public int alturaBlocado { get; set; }//altura do palete blocado
        public int totalBlocado { get; set; }//total do palete blocado        
        public int fatorCompra { get; set; }//Fator de compra        
        public int codUndCompra { get; set; }//código da unidade de compra         
        public string undCompra { get; set; }//descrição da unidade de compra         
        public int fatorPulmao { get; set; }//Fator de armazenagem        
        public int codUndPulmao { get; set; }//código da unidade de armazenagem        
        public string undPulmao { get; set; }//descrição da unidade do pulmao        
        public int fatorPicking { get; set; }//Fator de picking        
        public int codUndPicking { get; set; }//código da unidade de separacao      
        public string undPicking { get; set; } //descrição da unidade de separacao         
        public bool status { get; set; }//status (ativo ou inativo)        
        public bool auditaFlowrack { get; set; }//audita flowrack        
        public bool separacaoFlowrack { get; set; }//separação flowrack        
        public bool paletePadrao { get; set; }//peso padrão       
        public bool paleteBlocado { get; set; } //palete blocado       
        public bool controlaValidade { get; set; } //palete fechado
        public bool pesoVariavel { get; set; } //palete fechado
        public bool conferirCaixa { get; set; } //ConferirCaixa


        public string loginUsuario { get; set; } //Login do uusuário - RASTREAMENTO DE CADASTRO DO PRODUTO
        public DateTime dataAlteracao { get; set; } //Data de alteração - RASTREAMENTO DE CADASTRO DO PRODUTO


    }
}
