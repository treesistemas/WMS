using System;

namespace ObjetoTransferencia.Impressao
{
    public class ImpressaoCurvaAbc
    {
        public string empresa {  get; set; } //Nome da empresa
        public string tipo { get; set; } //Tipo de endereço (caixa ou flowrack
        public DateTime dataInicial { get; set; } //data inicial de pesquisa
        public DateTime dataFinal { get; set; } //data Final de Pesquisa
        public string endereco { get; set; } //Endereço do produto
        public string produto { get; set; } //Produto
        public string fator { get; set; } //Fator = quantidade de unidade em uma caixa
        public int capacidade { get; set; } //Capacidade do produto no endereço ou seja quantas caixa cabem no endereço
        public int palete { get; set; } //Palete M do produto
        public int quantidade { get; set; } //Quantidade vendida no período selecionado
        public string estacao {  get; set; } //Estação que o produto pertence no flowrack      
        public string curva { get; set; } //CURVA A B C
        public double valor { get; set; } //Valor da Capacidade

    }
}
