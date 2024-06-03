using System;


namespace ObjetoTransferencia
{
    public class ItensAbastecimento
    {

        public int? codAbastecimento { get; set; } //Código do abastecimento
        public int? codManifesto { get; set; } //Código do abastecimento
        public int? codEstacao { get; set; } //Código da Estação
        public string descEstacao { get; set; } //Descrição da Estação
        public int codApartamentoPicking { get; set; } //Código do Picking
        public string enderecoPicking { get; set; } //Endereço do Picking
        public int idProduto { get; set; } //ID do produto
        public string codProduto { get; set; } //Código do Produto
        public string descProduto { get; set; } //Descrição do Produto
        public int qtdCaixaProduto { get; set; } //Quantidade da caixa do produto      
        public int qtdPicking { get; set; } //Estoque do picking
        public string unidadePicking { get; set; } //Tipo de estoque (unidade do picking)

        //Informações para abastecimento
        public int qtdAbastecer { get; set; } //Quantidade para abastecer
        public int capacidadePicking { get; set; } //Capacidade do picking
        public int abastecimentoPicking { get; set; } //Abastecimento do picking

        //Endereço que servirá para o abastecimento
        public int codApartamentoPulmao { get; set; } //Código do Pulmão 
        public string enderecoPulmao { get; set; } //Endereço do Pulmão 
        public int? qtdPulmao { get; set; } //Quantidade a abastecer do Pulmão 
        public string unidadePulmao { get; set; } //Tipo de abastecimento (unidade do pulmão)
        public DateTime? vencimentoPulmao { get; set; } //Validade do Pulmão 
        public string lotePulmao { get; set; } //lote do Pulmão 
        public string observacao { get; set; } //Observação do apartamento
        public string tipoAnalise { get; set; } //Observação do apartamento


    }
}
