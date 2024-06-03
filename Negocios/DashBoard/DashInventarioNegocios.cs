using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class DashInventarioNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o inventario
        public Inventarios PesqInventario()
        {
            try
            {
                //String de consulta Verifica rank
                string select = "select i.inv_codigo, i.inv_descricao, i.inv_tipo, i.usu_codigo_inicial, i.int_rotativo, i.inv_auditoria, u.usu_login " +
                                "from wms_inventario i " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = i.usu_codigo_inicial " +
                                "where i.inv_status = 'ABERTO' and i.inv_data_final is null";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância o objêto
                Inventarios inventario = new Inventarios();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["inv_codigo"] != DBNull.Value)
                    {
                        inventario.codInventario = Convert.ToInt32(linha["inv_codigo"]);
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        inventario.descInventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["inv_tipo"] != DBNull.Value)
                    {
                        inventario.tipoInventario = Convert.ToString(linha["inv_tipo"]);
                    }

                    if (linha["int_rotativo"] != DBNull.Value)
                    {
                        inventario.descRotativo = Convert.ToString(linha["int_rotativo"]);
                    }

                    if (linha["inv_auditoria"] != DBNull.Value)
                    {
                        inventario.tipoAuditoria = Convert.ToString(linha["inv_auditoria"]);
                    }

                    if (linha["usu_codigo_inicial"] != DBNull.Value)
                    {
                        inventario.codUsuarioInicial = Convert.ToInt32(linha["usu_codigo_inicial"]);
                    }
                    
                    if (linha["usu_login"] != DBNull.Value)
                    {
                        inventario.usuarioInicial = Convert.ToString(linha["usu_login"]);
                    }

                }

                //Retorna os dados
                return inventario;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o inventário. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o progresso
        public double[] PesqProgresso(int codInventario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //String de consulta Verifica a quantidade de endereços
                string select = "select count(apa_codigo) as total_endereco, " +
                                /*Quantidade do pulmão*/
                                "(select count(i.apa_codigo) as total_endereco from wms_item_inventario i " +
                                "inner join wms_apartamento ap " +
                                "on i.apa_codigo = ap.apa_codigo " +
                                "where inv_codigo = @codInventario and apa_tipo = 'Pulmao' and not iinv_cont_final is null) as total_pulmao, " +
                                /*Quantidade do picking*/
                                "(select count(i.apa_codigo) as total_endereco from wms_item_inventario i " +
                                "inner join wms_apartamento ap " +
                                "on i.apa_codigo = ap.apa_codigo " +
                                "where inv_codigo = @codInventario and apa_tipo = 'Separacao' and not iinv_cont_final is null) as total_picking, " +
                                "(select count(apa_codigo) AS CONTAGEM from wms_item_inventario where not iinv_cont_final is null and inv_codigo = @codInventario) as total_finalizados " +
                                "from wms_item_inventario where inv_codigo = @codInventario";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância um array de inteiro
                double[] dados = new double[4];

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["total_endereco"] != DBNull.Value)
                    {
                        dados[0] = Convert.ToDouble(linha["total_endereco"]);
                    }

                    if (linha["total_pulmao"] != DBNull.Value)
                    {
                        dados[1] = Convert.ToDouble(linha["total_pulmao"]);
                    }

                    if (linha["total_picking"] != DBNull.Value)
                    {
                        dados[2] = Convert.ToDouble(linha["total_picking"]);
                    }

                    if (linha["total_finalizados"] != DBNull.Value)
                    {
                        dados[3] = Convert.ToDouble(linha["total_finalizados"]);
                    }

                }
                //Retorna a array
                return dados;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a quantidade de endereços. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a acuricidade
        public double[] PesqAcuricidade(int codInventario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@codInventario", codInventario);

                //String de consulta Verifica a acuricidade do pulmão
                string select = "select count(i.apa_codigo) as erro_pulmao, " +
                                //Verifica os erros do picking
                                "(select count(i.apa_codigo) from wms_item_inventario i " +
                                "inner join wms_apartamento ap " +
                                "on i.apa_codigo = ap.apa_codigo " +
                                "where inv_codigo = @codInventario and apa_tipo = 'Separacao' " +
                                "and iinv_cont_primeira <> iinv_cont_segunda and not iinv_cont_segunda is null) as erro_picking " +
                                "from wms_item_inventario i " +
                                "inner join wms_apartamento ap " +
                                "on i.apa_codigo = ap.apa_codigo " +
                                "where inv_codigo = @codInventario and apa_tipo = 'Pulmao' " +
                                "and iinv_cont_primeira <> iinv_cont_segunda and not iinv_cont_segunda is null ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância um array de inteiro
                double[] dados = new double[2];

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["erro_pulmao"] != DBNull.Value)
                    {
                        dados[0] = Convert.ToDouble(linha["erro_pulmao"]);
                    }

                    if (linha["erro_picking"] != DBNull.Value)
                    {
                        dados[1] = Convert.ToDouble(linha["erro_picking"]);
                    }
                }
                //Retorna a array
                return dados;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a acuricidade. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa as contagens
        public double[] PesqContagens(int codInventario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //String de consulta Verifica as contagens
                string select = "select count(apa_codigo) as cont_correta, " +
                                /*Conta a contagem errada*/
                                "(select count(apa_codigo) " +
                                "from wms_item_inventario where inv_codigo = @codInventario " +
                                "and iinv_cont_primeira <> iinv_cont_segunda and not iinv_cont_segunda is null) as cont_errada, " +
                                /*Quantidade do pulmão*/
                                "(select count(i.apa_codigo) as total_endereco from wms_item_inventario i " +
                                "inner join wms_apartamento ap " +
                                "on i.apa_codigo = ap.apa_codigo " +
                                "where inv_codigo = @codInventario and apa_tipo = 'Pulmao' and not iinv_cont_segunda is null) as cont_pulmao, " +
                                /*Quantidade do picking*/
                                "(select count(i.apa_codigo) as total_endereco from wms_item_inventario i " +
                                "inner join wms_apartamento ap " +
                                "on i.apa_codigo = ap.apa_codigo " +
                                "where inv_codigo = @codInventario and apa_tipo = 'Separacao' and not iinv_cont_segunda is null) as cont_picking " +
                                "from wms_item_inventario where inv_codigo = @codInventario and iinv_cont_primeira = iinv_cont_segunda ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância um array de inteiro
                double[] dados = new double[4];

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["cont_pulmao"] != DBNull.Value)
                    {
                        dados[0] = Convert.ToDouble(linha["cont_pulmao"]);
                    }

                    if (linha["cont_picking"] != DBNull.Value)
                    {
                        dados[1] = Convert.ToDouble(linha["cont_picking"]);
                    }

                    if (linha["cont_correta"] != DBNull.Value)
                    {
                        dados[2] = Convert.ToDouble(linha["cont_correta"]);
                    }

                    if (linha["cont_errada"] != DBNull.Value)
                    {
                        dados[3] = Convert.ToDouble(linha["cont_errada"]);
                    }

                }
                //Retorna a array
                return dados;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as contagens. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de produtos
        public double[] PesqProdutos(int codInventario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //String de consulta Verifica a quantidade de produtod
                string select = "select count(distinct(prod_id)) qtd_produto, " +
                                /*Verifica quantos produto estão pendentes*/
                                "(select count(distinct(prod_id)) from wms_item_inventario ii " +
                                "where inv_codigo = @codInventario and ii.iinv_cont_final is null) as qtd_pendente " +
                                "from wms_item_inventario where inv_codigo = @codInventario";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância um array de inteiro
                double[] dados = new double[2];

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["qtd_produto"] != DBNull.Value)
                    {
                        dados[0] = Convert.ToDouble(linha["qtd_produto"]);
                    }

                    if (linha["qtd_pendente"] != DBNull.Value)
                    {
                        dados[1] = Convert.ToDouble(linha["qtd_pendente"]);
                    }

                }
                //Retorna a array
                return dados;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a quantidade de produtos. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o ranking de usuario
        public ItemInventarioCollection PesqRankingUsuario(int codInventario, int codUsuarioAbertura)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@codUsuarioAbertura", codUsuarioAbertura);
                //String de consulta Verifica rank
                string select = "select first 10 u.usu_login, sum(contagem) as contagem "+
                                "from("+
                                "select usu_codigo_primeira as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_primeira is null " +
                                "group by usu_codigo_primeira " +
                                "union " +
                                "select usu_codigo_segunda  as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_segunda is null " +
                                "group by usu_codigo_segunda " +
                                "union " +
                                "select usu_codigo_terceira  as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_terceira is null " +
                                "group by usu_codigo_terceira " +
                                "union " +
                                "select usu_codigo_quarta  as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_quarta is null " +
                                "group by usu_codigo_quarta " +
                                "union " +
                                "select usu_codigo_quinta  as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_quinta is null " +
                                "group by usu_codigo_quinta " +
                                "union " +
                                "select usu_codigo_sesta  as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_sesta is null " +
                                "group by usu_codigo_sesta " +
                                "union " +
                                "select usu_codigo_setima as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_setima is null " +
                                "group by usu_codigo_setima " +
                                "union " +
                                "select usu_codigo_oitava as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_oitava is null " +
                                "group by usu_codigo_oitava " +
                                "union " +
                                "select usu_codigo_nona as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_nona is null " +
                                "group by usu_codigo_nona " +
                                "union " +
                                "select usu_codigo_decima as usu_codigo_contagem, count(apa_codigo) as contagem from wms_item_inventario " +
                                "where inv_codigo = @codInventario and not iinv_cont_decima is null " +
                                "group by usu_codigo_decima) " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = usu_codigo_contagem " +
                                "where usu_codigo_contagem <> (select usu_codigo_inicial from wms_inventario where inv_codigo = @codInventario) " +
                                "group by usu_login " +
                                "order by contagem desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância uma coleção de objêto
                ItemInventarioCollection itensCollection = new ItemInventarioCollection();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItemInventario itens = new ItemInventario();

                    //Adiciona os valores encontrados
                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itens.usuarioTerceira = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["contagem"] != DBNull.Value)
                    {
                        itens.contTerceira = Convert.ToInt32(linha["contagem"]);
                    }

                    itensCollection.Add(itens);
                }
                //Retorna os dados
                return itensCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a rank. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o contagem
        public ItemInventarioCollection PesqRankingContagem(int codInventario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@codInventario", codInventario);
                
                //String de consulta Verifica rank
                string select = "select 'CONTAGEM' as desc_contagem, count(apa_codigo) + (select count(apa_codigo) from wms_separacao) AS contagem from wms_armazenagem " +
                                "UNION "+
                                "select 'CONTAGEM 1'  as desc_contagem, count(apa_codigo) AS contagem from wms_item_inventario where not iinv_cont_primeira is null and inv_codigo = @codInventario " +
                                "UNION "+
                                "select 'CONTAGEM 2'  as desc_contagem, count(apa_codigo) AS contagem from wms_item_inventario where not iinv_cont_segunda is null and inv_codigo = @codInventario " +
                                "union "+
                                "select 'FINALIZADOS'  as desc_contagem, count(apa_codigo) AS contagem from wms_item_inventario where not iinv_cont_final is null and inv_codigo = @codInventario ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância uma coleção de objêto
                ItemInventarioCollection itensCollection = new ItemInventarioCollection();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItemInventario itens = new ItemInventario();

                    //Adiciona os valores encontrados
                    if (linha["desc_contagem"] != DBNull.Value)
                    {
                        itens.usuarioTerceira = Convert.ToString(linha["desc_contagem"]);
                    }

                    if (linha["contagem"] != DBNull.Value)
                    {
                        itens.contTerceira = Convert.ToInt32(linha["contagem"]);
                    }

                    itensCollection.Add(itens);
                }
                //Retorna os dados
                return itensCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a contagens. \nDetalhes:" + ex.Message);
            }
        }

    }
}
