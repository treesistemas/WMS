using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class ItemInventario
    {
        public int codInventario { get; set; } //código do inventário
        public int codItensInventario { get; set; } //código dos itens no inventário
        public int codOperacao { get; set; } //código da operação (código do item no pulmão ou no picking)
        public int codApartamento { get; set; } //código do apartamento
        public string descApartemento { get; set; } //descrição do apartamento
        public string tipoApartemento { get; set; } //tipo do apartamento
        public int idProdutoPicking { get; set; } //controla o produto no picking
        public int idProduto { get; set; } //id do produto
        public string codProduto { get; set; } //código do produto
        public string descProduto { get; set; } //descrição do produto
        public DateTime? vencimentoPulmao { get; set; } //vencimento do armazenamento
        public int fatorPulmao { get; set; } //fator de armazenamento
        public string unidadePulmao { get; set; } //unidade de armazenamento
        public string unidadePicking { get; set; } //unidade de picking
        public int estoqueProdutoAtual { get; set; } //estoque do produto atual
        public int? contPrimeira { get; set; } //primeira contagem
        public int? contSegunda { get; set; } //segunda contagem
        public int? contTerceira { get; set; } //terceira contagem
        public double? contQuarta { get; set; } //Quarta contagem,
        public double? contQuinta { get; set; } //Quinta contagem,
        public double? contSesta { get; set; } //Sesta contagem,
        public double? contSetima { get; set; } //Setima contagem,
        public double? contOitava { get; set; } //Oitava contagem,
        public double? contNona { get; set; } //Nona contagem,
        public double? contDecima { get; set; } //Décima contagem,
        public double? contagemFinal { get; set; } //Contagem final,

        public DateTime? vencPrimeira { get; set; } //vencimento da primeira contagem
        public DateTime? vencSegunda { get; set; } //vencimento da segunda contagem
        public DateTime? vencTerceira { get; set; } //vencimento da terceira contagem
        public DateTime? vencQuarta { get; set; } //terceiro vencimento
        public DateTime? vencQuinta { get; set; } //terceiro vencimento
        public DateTime? vencSesta { get; set; } //terceiro vencimento
        public DateTime? vencSetima { get; set; } //terceiro vencimento
        public DateTime? vencOitava { get; set; } //terceiro vencimento
        public DateTime? vencNona { get; set; } //terceiro vencimento
        public DateTime? vencDecima { get; set; } //terceiro vencimento
        public DateTime? vencFinal { get; set; }//vencimento final,

        public double pesoProduto { get; set; } //peso do produto
        public double pesoPrimeiro { get; set; }//primeiro peso
        public double pesoSegundo { get; set; }//segundo peso
        public double pesoTerceiro { get; set; }//terceiro peso
        public double pesoQuarto { get; set; }//quarto peso
        public double pesoQuinto { get; set; }//quinto peso
        public double pesoSesto { get; set; }//sesto peso
        public double pesoSetimo { get; set; }//setimo peso
        public double pesoOitavo { get; set; }//oitavo peso
        public double pesoNono { get; set; }//nono peso
        public double pesoDecimo { get; set; }//decimo peso
        public double pesoFinal { get; set; }//peso final


        public string lotePrimeiro { get; set; } //lote da primeira contagem
        public string loteSegundo { get; set; } //lote da segunda contagem
        public string loteTerceiro { get; set; } //lote da terceira contagem 
        public string loteQuarto { get; set; }//quarto lote,
        public string loteQuinto { get; set; }//quinto lote,
        public string loteSesto { get; set; }//sesto lote,
        public string loteSetimo { get; set; }//setimo lote,
        public string loteOitavo { get; set; }//oitavo lote,
        public string loteNono { get; set; }//nono lote,
        public string loteDecimo { get; set; }//decimo lote,
        public string loteFinal { get; set; }//lote Final


        public int codUsuario { get; set; } //código do usuário da primeira contagem
        public int usuContPrimeira { get; set; }//Usuário da primeira contagem,
        public int usuContSegunda { get; set; }//Usuário da sequnda contagem,
        public int usuContTerceira { get; set; }//Usuário da terceira contagem,
        public int usuContQuarta { get; set; }//Usuário da quarta contagem,
        public int usuContQuinta { get; set; }//Usuário da quinta contagem,
        public int usuContSesta { get; set; }//Usuário da sesta contagem,
        public int usuContSetima { get; set; }//Usuário da setima contagem,
        public int usuContOitava { get; set; }//Usuário da oitava contagem,
        public int usuContNona { get; set; }//Usuário da nona contagem,
        public int usuContDecima { get; set; }//Usuário da decima contagem,
        public int usuContFinal { get; set; }//Usuário da contagem final

        public DateTime? dataContagem { get; set; }//vencimento final

        public string usuarioTerceira { get; set; }


    }
}
