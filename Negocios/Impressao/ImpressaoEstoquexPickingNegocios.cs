using System;
using System.Data;
using ObjetoTransferencia.Impressao;
using Dados;

namespace Negocios.Impressao
{
    public class ImpressaoEstoquexPickingNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a pendencia
        public EstoquexPickingCollection PesqEstoqueSemPicking(string categoria)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adicionar Parâmetros
                conexao.AdicionarParamentros("@categoria", categoria);

                //Instância a camada de objetos
                EstoquexPickingCollection produtoCollection = new EstoquexPickingCollection();
                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                                "e.est_data_compra, p.prod_codigo, p.prod_descricao, c.cat_descricao, " +
                                "p.prod_fator_pulmao, u.uni_unidade, p.prod_tipo_palete, p.prod_tipo_armazenamento from wms_estoque e " +
                                "inner join wms_produto p " +
                                "on p.prod_id = e.prod_id " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = p.forn_codigo " +
                                "left join wms_categoria c " +
                                "on c.cat_codigo = p.cat_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "where p.prod_status = 'True' " +
                                "and e.est_quantidade > 0 and cat_descricao != @categoria and e.prod_id not in (select prod_id from wms_separacao )";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    EstoquexPicking produto = new EstoquexPicking();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        produto.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        produto.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        produto.qtdCaixa = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        produto.unidade = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["prod_tipo_palete"] != DBNull.Value)
                    {
                        produto.tipoPalete = Convert.ToString(linha["prod_tipo_palete"]);
                    }

                    if (linha["prod_tipo_armazenamento"] != DBNull.Value)
                    {
                        produto.tipoArmazenagem = Convert.ToString(linha["prod_tipo_armazenamento"]);
                    }

                    produtoCollection.Add(produto);
                }

                //Retorna o valor encontrado
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro o relatório de estoque sem picking. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a pendencia
        public EstoquexPickingCollection PesqPickingSemEstoque(string tipo)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@tipo", tipo);
                

                //Instância a camada de objetos
                EstoquexPickingCollection produtoCollection = new EstoquexPickingCollection();
                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                                "a.apa_endereco, p.prod_codigo, p.prod_descricao, s.sep_estoque, uni_unidade, s.sep_validade from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "where s.prod_id in (select prod_id from wms_estoque e where e.est_quantidade <= 0) ";

                if (!tipo.Equals("TODOS"))
                {
                    select += "and s.sep_tipo = @tipo ";
                }

                select += "order by a.apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    EstoquexPicking produto = new EstoquexPicking();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        produto.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        produto.descApartamento = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        produto.qtdCaixa = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        produto.unidade = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        produto.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    produtoCollection.Add(produto);
                }

                //Retorna o valor encontrado
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro o relatório de picking sem estoque. \nDetalhes:" + ex.Message);
            }
        }

    }
}
