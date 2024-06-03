using System;
using System.Data;
using ObjetoTransferencia.Impressao;
using Dados;
using System.Reflection;
using ObjetoTransferencia;

namespace Negocios.Impressao
{
    public class ProdutoFlowRackNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o produto
        public ItensFlowRack PesqProdutoFlowRack(string codProduto, string data, bool pesqData, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                ItensFlowRack itemFlowRack = new ItensFlowRack();

                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@data", data);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select r.prod_id, prod_codigo, prod_descricao, sum(r.iflow_qtd_conferida) as qtdFlowRack from wms_rastreamento_flowrack r " +
                                "inner join wms_item_pedido i " +
                                "on i.ped_codigo = r.ped_codigo and i.prod_id = r.prod_id " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto pd " +
                                "on pd.prod_id = r.prod_id " +
                                "where p.ped_fim_conferencia is null and r.prod_id = (select prod_id from wms_produto where prod_codigo = @codProduto)" +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                                if(pesqData == true)
                {
                    select += "and iflow_data_conferencia <= @data ";
                }

                select += "group by r.prod_id, prod_codigo, prod_descricao";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itemFlowRack.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itemFlowRack.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtdFlowRack"] != DBNull.Value)
                    {
                        itemFlowRack.qtdProduto = Convert.ToInt32(linha["qtdFlowRack"]);
                    }
                }

                //Retorna o valor encontrado
                return itemFlowRack;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao pesquisar o produto no flowrack. \nDetalhes:" + ex.Message);
            }
        }

    }
}
