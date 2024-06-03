using System;
using System.Data;
using ObjetoTransferencia;
using Dados;

namespace Negocios
{
    public class ImpressaoPendenciaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a pendencia
        public PedidoCollection PesqPedido(string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //Instância a camada de objetos
                PedidoCollection pedidoCollection = new PedidoCollection();
                //String de consulta
                string select = "select gen_id(gen_wms_caixa,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Pedido pedido = new Pedido();
                    //Adiciona os valores encontrados
                    pedido.codPedido = Convert.ToInt32(linha["id"]);


                    pedidoCollection.Add(pedido);
                }
               
                //Retorna o valor encontrado
                return pedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar às pendência. \nDetalhes:" + ex.Message);
            }
        }

    }
}
