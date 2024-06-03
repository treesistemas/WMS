
using System;

namespace ObjetoTransferencia
{
    public class Abastecimento
    {
     
        public int codAbastecimento { get; set; } //Código do Abastecimento
        public string codManifesto { get; set; } //Código do manifesto
        public string tipoAbastecimento { get; set; } //Tipo de Abastecimento (Caixa ou Flow Rack)
        public string modoAbastecimento { get; set; } //Modo do Abastecimento (Corretiva ou Preventiva)
        public int numeroRegiao { get; set; } //Número da região
        public int numeroRua { get; set; } //Número da Rua
        public string ladoRua { get; set; } //Lado da Rua
        public int codEstacao { get; set; } //Código da Estação
        public string descEstacao { get; set; } //Descrição da Estação
        public string statusAbastecimento { get; set; } //Status do Abastecimento ( Iniciada, Não Iniciada, Finalizada, Cancelada ou Todos)
        public DateTime dataAbertura { get; set; } //Data de abertura do abastecimento
        public DateTime dataFechamento { get; set; } //Data de Fechamento do abastecimento   

        public string descCategoria { get; set; } //Descrição da categoria
        public int? codFornecedor { get; set; } //Código do fornecedor
        public string nomeFornecedor { get; set; } //Nome do fornecedor

        public string loginResponsavel { get; set; } //Responsável pela abertura
        public string loginEmpilhador { get; set; } //Login do empilhador
        public string loginRepositor { get; set; } //Login do repositor


    }
}
