using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Reserva
    {
        public int codReserva { get; set; } //Código da reserva
        public int codPicking { get; set; } //Código do picking
        public string enderecoPicking { get; set; } //Endereço do picking
        public int codPulmao { get; set; } //Código do pulmão reservado
        public string enderecoPulmao { get; set; } //Endereço do pulmão
        public int idProduto { get; set; } //ID produto
        public string codProduto { get; set; } //Código do Produto
        public string descProduto { get; set; } //descrição do Produto
        public int fatorPulmao { get; set; } //quantidade de armazenagem da caixa
        public int qtdReservada { get; set; } //Quantidade reservada
        public string unidadePulmao { get; set; } //Fator do pulmão do produto
        public DateTime dataVencimento { get; set; } //Vencimento do Produto
        public double pesoProduto { get; set; } //Pedso do Produto
        public string loteProduto { get; set; } //Lote Produto
        public string loginUsuario { get; set; } //Usuario Reservou
        public DateTime dataReserva { get; set; } //Data Reserva
        public string tipoReserva { get; set; } //Tipo de Reserva
        public string codAbastecimento { get; set; } //Código do abastecimento
        public int codManifesto { get; set; } //Código do Manifesto
        public int codPedido { get; set; } //Código Pedido
        public string tipoAnalise { get; set; } //Tipo de análise do abastecimento
        public string status { get; set; } //Status


    }
}
