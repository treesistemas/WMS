
namespace ObjetoTransferencia
{
    public class Estrutura
    {
       
        
       
        //código da categoria
        public int idCategoria { get; set; }
        //categoria
        public string categoria { get; set; }
       
       public string descProduto { get; set; }
        public double estoqueProduto { get; set; }
        public double paleteProduto { get; set; }


        public int codRegiao { get; set; }//código da região       
        public int numeroRegiao{ get; set; } //região do endereço       
        public string tipoRegiao { get; set; } //tipo de região
        
        public int codRua { get; set; } //código do rua     
        public int numeroRua { get; set; } //código do endereço

        public int codBloco { get; set; }//código do bloco        
        public int numeroBloco { get; set; }//bloco do endereço        
        public string ladoBloco { get; set; }//lado endereco

        public int codNivel { get; set; } //código do nivel       
        public int numeroNivel { get; set; } //nivel do endereco
        
        public int codApartamento { get; set; } //código do apartamento       
        public int numeroApartamento { get; set; } //número do apartamento        
        public string descApartamento { get; set; }//descrição do apartamento
        public string tipoApartamento { get; set; } //tipo do apartamento       
        public string statusApartamento { get; set; } //status apartamento        
        public string disposicaoApartamento { get; set; }//disposição do apartamento        
        public string tamanhoApartamento { get; set; }//tamanho do apartamento        
        public string ordemApartamento { get; set; }//ordem do apartamento

        //qtd de região
        public int qtdRegiao { get; set; }
        //qtd de rua
        public int qtdRua { get; set; }
        //qtd de bloco
        public int qtdBloco { get; set; }
        //qtd de apartamento
        public int qtdApartamento { get; set; }

        //qtd de aereo
        public int qtdPulmao { get; set; }
        //qtd de aereo indisponivel
        public int qtdPulmaoIndisponivel { get; set; }
        //qtd de aereo disponivel
        public int qtdPulmaoDisponivel { get; set; }
        //qtd de aereo ocupado
        public int qtdPulmaoOcupado { get; set; }
        //qtd de aereo vago
        public int qtdPulmaoVago { get; set; }
        public int qtdPulmaoMaximo { get; set; } //Quantidade máxima de produto em um palete

        //qtd de blocados
        public int qtdBlocados { get; set; }
        //qtd de blocados indisponivel
        public int qtdBlocadosIndisponivel { get; set; }
        //qtd de blocados disponivel
        public int qtdBlocadosDisponivel { get; set; }
        //qtd de blocados ocupado
        public int qtdBlocadosOcupado { get; set; }
        //qtd de blocados vago
        public int qtdBlocadosVago { get; set; }

        //qtd de picking
        public int qtdPicking { get; set; }
        //qtd de picking indisponivel
        public int qtdPickingIndisponivel { get; set; }
        //qtd de picking disponivel
        public int qtdPickingDisponivel { get; set; }
        //qtd de picking ocupado
        public int qtdPickingOcupado { get; set; }
        //qtd de picking vago
        public int qtdPickingVago { get; set; }



    }
}
