using System;
using System.Security.Policy;

namespace ObjetoTransferencia
{
    public class EnderecoPicking
    {
        public string nomeEmpresa { get; set; }//Nome da empresa
        public string siglaEmpresa { get; set; }//Nome da empresa      
        public int codEstacao { get; set; } //código estacao        
        public string descEstacao { get; set; }//descrição da estacão        
        public string tipoEstacao { get; set; }//tipo da estacão        
        public int codSeparacao { get; set; }//id endereço da separacao        
        public int idProduto { get; set; }//id produto        
        public int quantidadeCaixa { get; set; }//fator do pulmão        
        public double pesoCxa { get; set; }//peso da caixa       
        public string codProduto { get; set; } //código do produto        
        public string descProduto { get; set; }//descrição do produto        
        public bool statusProduto { get; set; }//status do Produto        
        public bool separacaoFlowrack { get; set; }//separacao do produto        
        public int codRegiao { get; set; }//Código do regiao        
        public int numeroRegiao { get; set; }//número do regiao       
        public int codRua { get; set; } //Código do rua        
        public int numeroRua { get; set; }//número do rua        
        public int codBloco { get; set; }//Código do bloco        
        public int numeroBloco { get; set; }//número do bloco       
        public int codNivel { get; set; } //código do nível        
        public int numeroNivel { get; set; }//número do nível        
        public int codApartamento { get; set; }//código do apartamento        
        public int numeroApartamento { get; set; }//número do Apartamento        
        public string endereco { get; set; }//descrição do endereço       
        public string enderecoExtra { get; set; } //descrição do endereço        
        public int? estoque { get; set; }//qtd armazenado       
        public string unidadeEstoque { get; set; } //unidade do estoque       
        public string unidadeCapacidade { get; set; } //qtd reservada        
        public string tipoEndereco { get; set; }//Tipo do endereço
        public string tipoPicking { get; set; }//Tipo do picking
        public string tamanhoEndereco { get; set; }//Tamanho do endereço        
        public DateTime? vencimento { get; set; }//data de validade do endereço       
        public double peso { get; set; } //peso do endereço        
        public string lote { get; set; }//lote do endereço        
        public int? capacidade { get; set; }//capacidade do endereço        
        public int? abastecimento { get; set; }//abastecimento do endereço        
        public string paleteEndereco { get; set; }//tanho do palete do endereço        
        public string apStatus { get; set; }//status do endereço        
        public string apDisponibilidade { get; set; }//disponibilidade do endereço        
        public string ordemEndereco { get; set; }//Ordem dos endereços
        public string numeroBarra { get; set; }//numero de barra

        public string volumeIdependente { get; set; }//Gera volume independente



    }
}
