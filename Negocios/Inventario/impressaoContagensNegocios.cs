using Dados;
using ObjetoTransferencia;
using ObjetoTransferencia.Impressao;
using ObjetoTransferencia.Relatorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Inventario
{
    public class impressaoContagensNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o inventario aberto
        public InventarioCollection PesqInventario()
        {
            try
            {
                //Instância a coleção
                InventarioCollection inventarioCollection = new InventarioCollection();

                //String de consulta
                string select = "select inv_codigo, inv_descricao from wms_inventario where inv_data_final is null and inv_status = 'ABERTO'";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);



                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Inventarios inventario = new Inventarios();
                    //Adiciona os valores encontrados
                    if (linha["inv_codigo"] != DBNull.Value)
                    {
                        inventario.codInventario = Convert.ToInt32(linha["inv_codigo"]);
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        inventario.descInventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    inventarioCollection.Add(inventario);
                }
                //Retorna a coleção de cadastro encontrada
                return inventarioCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o invetário. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o produto
        public Produto PesqProduto(string codProduto, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@empresa", empresa);
                //Instância a camada de objetos
                Produto produto = new Produto();

                //String de consulta
                string select = "select prod_id, prod_codigo, prod_descricao from wms_produto where prod_codigo = @codProduto " +
                                "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        produto.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }
                }

                //Retorna o valor encontrado
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao pesquisar o produto. \nDetalhes:" + ex.Message);
            }
        }

        public EstacaoCollection PesqEstacao()
        {
            try
            {
                //isntância o objeto
                EstacaoCollection estacaoCollection = new EstacaoCollection();
                //Limpa todos os parâmetros
                conexao.LimparParametros();
                //Consulta
                String select = "select est_codigo, est_descricao, est_nivel, est_tipo from wms_estacao order by est_tipo, est_codigo";

                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Estacao estacao = new Estacao();

                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        estacao.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        estacao.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    //Adiciona os objêto a coleção
                    estacaoCollection.Add(estacao);
                }

                //retorna a coleção
                return estacaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as estações. \nDetalhes: " + ex);
            }
        }

        //Pesquisa a contagem de picking
        public RelItemInventarioCollection PesqContagemPicking(int codInventario, int regiao, int rua, int bloco, string tipo, string tipoPicking, int estacao, string descEstacao, string lado)
        {
            try
            {
                //Instância a coleção
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@bloco", bloco);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@estacao", estacao);


                //String de consulta
                string select = "select " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_login, " +
                                "s.sep_tipo, reg_numero, rua_numero, bloc_numero, bloc_lado, a.apa_codigo, apa_endereco, " +
                                "prod_codigo, prod_descricao, prod_fator_pulmao, u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking " +
                                "from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking ";

                if (estacao > 0)
                {
                    select += "where s.sep_tipo = 'FLOWRACK' and s.est_codigo = @estacao ";
                }

                if (regiao > 0 && rua > 0)
                {
                    if (tipoPicking.Equals("CAIXA"))
                    {
                        select += "where s.sep_tipo = 'CAIXA' and reg_numero = @regiao and rua_numero = @rua ";
                    }

                    if (tipoPicking.Equals("FLOW RACK"))
                    {
                        select += "and reg_numero = @regiao and rua_numero = @rua ";
                    }
                }

                if (bloco > 0)
                {
                    select += "and bloc_numero = @bloco ";
                }

                if (!(lado.Equals("TODOS") || lado.Equals("SELECIONE") || lado.Equals(string.Empty)))
                {
                    select += "and bloc_lado = @lado ";
                }

                select += "order by apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    RelItemInventario item = new RelItemInventario();
                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        item.empresa = Convert.ToString(linha["empresa"]);

                        if (tipo.Equals("PICKING") && tipoPicking.Equals("FLOW RACK"))
                        {
                            item.tipo = tipo + " " + tipoPicking + "  -  " + descEstacao;
                        }
                        else if (tipo.Equals("PICKING") && tipoPicking.Equals("CAIXA"))
                        {
                            item.tipo = tipo + " " + tipoPicking;
                        }
                        else
                        {
                            item.tipo = tipo;
                        }

                        item.bloco = 0;

                        item.lado = lado.ToUpper();
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        item.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        item.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        item.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        item.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o invetário. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a contagem de picking
        public RelItemInventarioCollection PesqContagemPulmao(int codInventario, int regiao, int rua, int bloco, string tipo, string tipoPicking, int estacao, string descEstacao, string lado)
        {
            try
            {
                //Instância a coleção
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@bloco", bloco);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@estacao", estacao);


                //String de consulta
                string select = "select " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_login, " +
                                "a.apa_tipo, reg_numero, rua_numero, bloc_numero, bloc_lado, a.apa_codigo, apa_endereco, " +
                                "prod_codigo, prod_descricao, prod_fator_pulmao, u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking " +
                                "from wms_armazenagem s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking " +
                                "where reg_numero = @regiao and rua_numero = @rua ";

                if (!(lado.Equals("TODOS") || lado.Equals("SELECIONE") || lado.Equals(string.Empty)))
                {
                    select += "and bloc_lado = @lado ";
                }

                select += "order by apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    RelItemInventario item = new RelItemInventario();
                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        item.empresa = Convert.ToString(linha["empresa"]);

                        item.tipo = tipo;
                        item.bloco = 0;

                        item.lado = lado.ToUpper();
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        item.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        item.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        item.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        item.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o invetário. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa a contagem 3+
        public RelItemInventarioCollection PesqContagemPicking3(int codInventario, int regiao, int rua, int bloco, string tipo, string tipoPicking, int estacao, string descEstacao, string lado, string contagem)
        {
            try
            {
                //Instância a coleção
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@bloco", bloco);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@estacao", estacao);


                //String de consulta
                string select = "select " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_login, " +
                                "s.sep_tipo, reg_numero, rua_numero, bloc_numero, bloc_lado, a.apa_codigo, apa_endereco, " +
                                "prod_codigo, prod_descricao, prod_fator_pulmao, " +
                                 "iinv_cont_final, iinv_venc_final, iinv_lote_final," +
                                "u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking " +
                                "from wms_separacao s " +
                                "inner join wms_item_inventario i " +
                                "on i.apa_codigo = s.apa_codigo " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking ";

                //Divergência da contagem 1 e 2 do picking tipo caixa 

                if (tipoPicking.Equals("CAIXA"))
                {
                    if (regiao > 0 && rua > 0 && contagem.Equals("CONTAGEM 3"))
                    {
                        //Verifica a contagem e a data
                        select += "where s.sep_tipo = 'CAIXA' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and iinv_cont_primeira <> iinv_cont_segunda and iinv_cont_final is null " +
                                  "or s.sep_tipo = 'CAIXA' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and iinv_venc_primeira <> iinv_venc_segunda and iinv_venc_final is null " +
                                  "or s.sep_tipo = 'CAIXA' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and iinv_cont_final is null "+
                                  "or s.sep_tipo = 'CAIXA' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and iinv_venc_final is null ";

                    }
                    else if (regiao > 0 && rua > 0 && contagem.Equals("CONTAGEM 3+"))
                    {
                        //Verifica a contagem e a data
                        select += "where s.sep_tipo = 'CAIXA' and reg_numero = @regiao and rua_numero = @rua " +
                                       "and i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_terceira is null " +
                                       /*Lote 3*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_terceira is null " +
                                       /*Contagem 4*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_cont_primeira <> iinv_cont_segunda " +
                                       "and iinv_cont_primeira <> iinv_cont_terceira " +
                                       "and iinv_cont_segunda <> iinv_cont_terceira " +
                                       "and iinv_cont_quarta is null " +
                                       /*Vencimento 4*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_primeira <> iinv_venc_terceira " +
                                       "and iinv_venc_segunda <> iinv_venc_terceira " +
                                       "and iinv_venc_quarta is null " +
                                       /*Lote 4*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_primeira <> iinv_lote_terceira " +
                                       "and iinv_lote_segunda <> iinv_lote_terceira " +
                                       "and iinv_lote_quarta is null " +
                                       /*Contagem 5*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_cont_primeira <> iinv_cont_segunda " +
                                       "and iinv_cont_primeira <> iinv_cont_terceira " +
                                       "and iinv_cont_primeira <> iinv_cont_quarta " +
                                       "and iinv_cont_segunda <> iinv_cont_terceira " +
                                       "and iinv_cont_segunda <> iinv_cont_quarta " +
                                       "and iinv_cont_terceira <> iinv_cont_quarta " +
                                       "and iinv_cont_quinta is null " +
                                       /*Vencimento 5*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_primeira <> iinv_venc_terceira " +
                                       "and iinv_venc_primeira <> iinv_venc_quarta " +
                                       "and iinv_venc_segunda <> iinv_venc_terceira " +
                                       "and iinv_venc_segunda <> iinv_venc_quarta " +
                                       "and iinv_venc_terceira <> iinv_venc_quarta " +
                                       "and iinv_venc_quinta is null " +
                                       /*Lote 5*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_primeira <> iinv_lote_terceira " +
                                       "and iinv_lote_primeira <> iinv_lote_quarta " +
                                       "and iinv_lote_segunda <> iinv_lote_terceira " +
                                       "and iinv_lote_segunda <> iinv_lote_quarta " +
                                       "and iinv_lote_terceira <> iinv_lote_quarta " +
                                       "and iinv_lote_quinta is null " +
                                       /*Contagem 6*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_cont_primeira <> iinv_cont_segunda " +
                                       "and iinv_cont_primeira <> iinv_cont_terceira " +
                                       "and iinv_cont_primeira <> iinv_cont_quarta " +
                                       "and iinv_cont_primeira <> iinv_cont_quinta " +
                                       "and iinv_cont_segunda <> iinv_cont_terceira " +
                                       "and iinv_cont_segunda <> iinv_cont_quarta " +
                                       "and iinv_cont_segunda <> iinv_cont_quinta " +
                                       "and iinv_cont_terceira <> iinv_cont_quarta " +
                                       "and iinv_cont_terceira <> iinv_cont_quinta " +
                                       "and iinv_cont_quarta <> iinv_cont_quinta " +
                                       "and iinv_cont_sesta is null " +
                                       /*Vencimento 6*/
                                       "or i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_primeira <> iinv_venc_terceira " +
                                       "and iinv_venc_primeira <> iinv_venc_quarta " +
                                       "and iinv_venc_primeira <> iinv_venc_quinta " +
                                       "and iinv_venc_segunda <> iinv_venc_terceira " +
                                       "and iinv_venc_segunda <> iinv_venc_quarta " +
                                       "and iinv_venc_segunda <> iinv_venc_quinta " +
                                       "and iinv_venc_terceira <> iinv_venc_quarta " +
                                       "and iinv_venc_terceira <> iinv_venc_quinta " +
                                       "and iinv_venc_quarta <> iinv_venc_quinta " +
                                       "and iinv_venc_sesta is null " +
                                       /*Lote 6*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_primeira <> iinv_lote_terceira " +
                                       "and iinv_lote_primeira <> iinv_lote_quarta " +
                                       "and iinv_lote_primeira <> iinv_lote_quinta " +
                                       "and iinv_lote_segunda <> iinv_lote_terceira " +
                                       "and iinv_lote_segunda <> iinv_lote_quarta " +
                                       "and iinv_lote_segunda <> iinv_lote_quinta " +
                                       "and iinv_lote_terceira <> iinv_lote_quarta " +
                                       "and iinv_lote_terceira <> iinv_lote_quinta " +
                                       "and iinv_lote_quarta <> iinv_lote_quinta " +
                                       "and iinv_lote_sesta is null " +
                                       /*Contagem 7*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_cont_primeira <> iinv_cont_segunda " +
                                       "and iinv_cont_primeira <> iinv_cont_terceira " +
                                       "and iinv_cont_primeira <> iinv_cont_quarta " +
                                       "and iinv_cont_primeira <> iinv_cont_quinta " +
                                       "and iinv_cont_primeira <> iinv_cont_sesta " +
                                       "and iinv_cont_segunda <> iinv_cont_terceira " +
                                       "and iinv_cont_segunda <> iinv_cont_quarta " +
                                       "and iinv_cont_segunda <> iinv_cont_quinta " +
                                       "and iinv_cont_segunda <> iinv_cont_sesta " +
                                       "and iinv_cont_terceira <> iinv_cont_quarta " +
                                       "and iinv_cont_terceira <> iinv_cont_quinta " +
                                       "and iinv_cont_terceira <> iinv_cont_sesta " +
                                       "and iinv_cont_quarta <> iinv_cont_quinta " +
                                       "and iinv_cont_quarta <> iinv_cont_sesta " +
                                       "and iinv_cont_quinta <> iinv_cont_sesta " +
                                       "and iinv_cont_setima is null " +
                                       /*Vencimento 7*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_primeira <> iinv_venc_terceira " +
                                       "and iinv_venc_primeira <> iinv_venc_quarta " +
                                       "and iinv_venc_primeira <> iinv_venc_quinta " +
                                       "and iinv_venc_primeira <> iinv_venc_sesta " +
                                       "and iinv_venc_segunda <> iinv_venc_terceira " +
                                       "and iinv_venc_segunda <> iinv_venc_quarta " +
                                       "and iinv_venc_segunda <> iinv_venc_quinta " +
                                       "and iinv_venc_segunda <> iinv_venc_sesta " +
                                       "and iinv_venc_terceira <> iinv_venc_quarta " +
                                       "and iinv_venc_terceira <> iinv_venc_quinta " +
                                       "and iinv_venc_terceira <> iinv_venc_sesta " +
                                       "and iinv_venc_quarta <> iinv_venc_quinta " +
                                       "and iinv_venc_quarta <> iinv_venc_sesta " +
                                       "and iinv_venc_quinta <> iinv_venc_sesta " +
                                       "and iinv_venc_setima is null " +
                                       /*Lote 7*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_primeira <> iinv_lote_terceira " +
                                       "and iinv_lote_primeira <> iinv_lote_quarta " +
                                       "and iinv_lote_primeira <> iinv_lote_quinta " +
                                       "and iinv_lote_primeira <> iinv_lote_sesta " +
                                       "and iinv_lote_segunda <> iinv_lote_terceira " +
                                       "and iinv_lote_segunda <> iinv_lote_quarta " +
                                       "and iinv_lote_segunda <> iinv_lote_quinta " +
                                       "and iinv_lote_segunda <> iinv_lote_sesta " +
                                       "and iinv_lote_terceira <> iinv_lote_quarta " +
                                       "and iinv_lote_terceira <> iinv_lote_quinta " +
                                       "and iinv_lote_terceira <> iinv_lote_sesta " +
                                       "and iinv_lote_quarta <> iinv_lote_quinta " +
                                       "and iinv_lote_quarta <> iinv_lote_sesta " +
                                       "and iinv_lote_quinta <> iinv_lote_sesta " +
                                       "and iinv_lote_setima is null " +
                                       /*Contagem 8*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_cont_primeira <> iinv_cont_segunda " +
                                       "and iinv_cont_primeira <> iinv_cont_terceira " +
                                       "and iinv_cont_primeira <> iinv_cont_quarta " +
                                       "and iinv_cont_primeira <> iinv_cont_quinta " +
                                       "and iinv_cont_primeira <> iinv_cont_sesta " +
                                       "and iinv_cont_primeira <> iinv_cont_setima " +
                                       "and iinv_cont_segunda <> iinv_cont_terceira " +
                                       "and iinv_cont_segunda <> iinv_cont_quarta " +
                                       "and iinv_cont_segunda <> iinv_cont_quinta " +
                                       "and iinv_cont_segunda <> iinv_cont_sesta " +
                                       "and iinv_cont_segunda <> iinv_cont_setima " +
                                       "and iinv_cont_terceira <> iinv_cont_quarta " +
                                       "and iinv_cont_terceira <> iinv_cont_quinta " +
                                       "and iinv_cont_terceira <> iinv_cont_sesta " +
                                       "and iinv_cont_terceira <> iinv_cont_setima " +
                                       "and iinv_cont_quarta <> iinv_cont_quinta " +
                                       "and iinv_cont_quarta <> iinv_cont_sesta " +
                                       "and iinv_cont_quarta <> iinv_cont_setima " +
                                       "and iinv_cont_quinta <> iinv_cont_sesta " +
                                       "and iinv_cont_quinta <> iinv_cont_setima " +
                                       "and iinv_cont_sesta <> iinv_cont_setima " +
                                       "and iinv_cont_oitava is null " +
                                       /*Vencimento 8*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_primeira <> iinv_venc_terceira " +
                                       "and iinv_venc_primeira <> iinv_venc_quarta " +
                                       "and iinv_venc_primeira <> iinv_venc_quinta " +
                                       "and iinv_venc_primeira <> iinv_venc_sesta " +
                                       "and iinv_venc_primeira <> iinv_venc_setima " +
                                       "and iinv_venc_segunda <> iinv_venc_terceira " +
                                       "and iinv_venc_segunda <> iinv_venc_quarta " +
                                       "and iinv_venc_segunda <> iinv_venc_quinta " +
                                       "and iinv_venc_segunda <> iinv_venc_sesta " +
                                       "and iinv_venc_segunda <> iinv_venc_setima " +
                                       "and iinv_venc_terceira <> iinv_venc_quarta " +
                                       "and iinv_venc_terceira <> iinv_venc_quinta " +
                                       "and iinv_venc_terceira <> iinv_venc_sesta " +
                                       "and iinv_venc_terceira <> iinv_venc_setima " +
                                       "and iinv_venc_quarta <> iinv_venc_quinta " +
                                       "and iinv_venc_quarta <> iinv_venc_sesta " +
                                       "and iinv_venc_quarta <> iinv_venc_setima " +
                                       "and iinv_venc_quinta <> iinv_venc_sesta " +
                                       "and iinv_venc_quinta <> iinv_venc_setima " +
                                       "and iinv_venc_sesta <> iinv_venc_setima " +
                                       "and iinv_venc_oitava is null " +
                                       /*Lote 8*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_primeira <> iinv_lote_terceira " +
                                       "and iinv_lote_primeira <> iinv_lote_quarta " +
                                       "and iinv_lote_primeira <> iinv_lote_quinta " +
                                       "and iinv_lote_primeira <> iinv_lote_sesta " +
                                       "and iinv_lote_primeira <> iinv_lote_setima " +
                                       "and iinv_lote_segunda <> iinv_lote_terceira " +
                                       "and iinv_lote_segunda <> iinv_lote_quarta " +
                                       "and iinv_lote_segunda <> iinv_lote_quinta " +
                                       "and iinv_lote_segunda <> iinv_lote_sesta " +
                                       "and iinv_lote_segunda <> iinv_lote_setima " +
                                       "and iinv_lote_terceira <> iinv_lote_quarta " +
                                       "and iinv_lote_terceira <> iinv_lote_quinta " +
                                       "and iinv_lote_terceira <> iinv_lote_sesta " +
                                       "and iinv_lote_terceira <> iinv_lote_setima " +
                                       "and iinv_lote_quarta <> iinv_lote_quinta " +
                                       "and iinv_lote_quarta <> iinv_lote_sesta " +
                                       "and iinv_lote_quarta <> iinv_lote_setima " +
                                       "and iinv_lote_quinta <> iinv_lote_sesta " +
                                       "and iinv_lote_quinta <> iinv_lote_setima " +
                                       "and iinv_lote_sesta <> iinv_lote_setima " +
                                       "and iinv_lote_oitava is null " +
                                       /*Contagem 9*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_cont_primeira <> iinv_cont_segunda " +
                                       "and iinv_cont_primeira <> iinv_cont_terceira " +
                                       "and iinv_cont_primeira <> iinv_cont_quarta " +
                                       "and iinv_cont_primeira <> iinv_cont_quinta " +
                                       "and iinv_cont_primeira <> iinv_cont_sesta " +
                                       "and iinv_cont_primeira <> iinv_cont_setima " +
                                       "and iinv_cont_primeira <> iinv_cont_oitava " +
                                       "and iinv_cont_segunda <> iinv_cont_terceira " +
                                       "and iinv_cont_segunda <> iinv_cont_quarta " +
                                       "and iinv_cont_segunda <> iinv_cont_quinta " +
                                       "and iinv_cont_segunda <> iinv_cont_sesta " +
                                       "and iinv_cont_segunda <> iinv_cont_setima " +
                                       "and iinv_cont_segunda <> iinv_cont_oitava " +
                                       "and iinv_cont_terceira <> iinv_cont_quarta " +
                                       "and iinv_cont_terceira <> iinv_cont_quinta " +
                                       "and iinv_cont_terceira <> iinv_cont_sesta " +
                                       "and iinv_cont_terceira <> iinv_cont_setima " +
                                       "and iinv_cont_terceira <> iinv_cont_oitava " +
                                       "and iinv_cont_quarta <> iinv_cont_quinta " +
                                       "and iinv_cont_quarta <> iinv_cont_sesta " +
                                       "and iinv_cont_quarta <> iinv_cont_setima " +
                                       "and iinv_cont_quarta <> iinv_cont_oitava " +
                                       "and iinv_cont_quinta <> iinv_cont_sesta " +
                                       "and iinv_cont_quinta <> iinv_cont_setima " +
                                       "and iinv_cont_quinta <> iinv_cont_oitava " +
                                       "and iinv_cont_sesta <> iinv_cont_setima " +
                                       "and iinv_cont_sesta <> iinv_cont_oitava " +
                                       "and iinv_cont_setima <> iinv_cont_oitava " +
                                       "and iinv_cont_nona is null " +
                                       /*Vencimento 9*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_primeira <> iinv_venc_terceira " +
                                       "and iinv_venc_primeira <> iinv_venc_quarta " +
                                       "and iinv_venc_primeira <> iinv_venc_quinta " +
                                       "and iinv_venc_primeira <> iinv_venc_sesta " +
                                       "and iinv_venc_primeira <> iinv_venc_setima " +
                                       "and iinv_venc_primeira <> iinv_venc_oitava " +
                                       "and iinv_venc_segunda <> iinv_venc_terceira " +
                                       "and iinv_venc_segunda <> iinv_venc_quarta " +
                                       "and iinv_venc_segunda <> iinv_venc_quinta " +
                                       "and iinv_venc_segunda <> iinv_venc_sesta " +
                                       "and iinv_venc_segunda <> iinv_venc_setima " +
                                       "and iinv_venc_segunda <> iinv_venc_oitava " +
                                       "and iinv_venc_terceira <> iinv_venc_quarta " +
                                       "and iinv_venc_terceira <> iinv_venc_quinta " +
                                       "and iinv_venc_terceira <> iinv_venc_sesta " +
                                       "and iinv_venc_terceira <> iinv_venc_setima " +
                                       "and iinv_venc_terceira <> iinv_venc_oitava " +
                                       "and iinv_venc_quarta <> iinv_venc_quinta " +
                                       "and iinv_venc_quarta <> iinv_venc_sesta " +
                                       "and iinv_venc_quarta <> iinv_venc_setima " +
                                       "and iinv_venc_quarta <> iinv_venc_oitava " +
                                       "and iinv_venc_quinta <> iinv_venc_sesta " +
                                       "and iinv_venc_quinta <> iinv_venc_setima " +
                                       "and iinv_venc_quinta <> iinv_venc_oitava " +
                                       "and iinv_venc_sesta <> iinv_venc_setima " +
                                       "and iinv_venc_sesta <> iinv_venc_oitava " +
                                       "and iinv_venc_setima <> iinv_venc_oitava " +
                                       "and iinv_venc_nona is null " +
                                       /*Lote 9*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_primeira <> iinv_lote_terceira " +
                                       "and iinv_lote_primeira <> iinv_lote_quarta " +
                                       "and iinv_lote_primeira <> iinv_lote_quinta " +
                                       "and iinv_lote_primeira <> iinv_lote_sesta " +
                                       "and iinv_lote_primeira <> iinv_lote_setima " +
                                       "and iinv_lote_primeira <> iinv_lote_oitava " +
                                       "and iinv_lote_segunda <> iinv_lote_terceira " +
                                       "and iinv_lote_segunda <> iinv_lote_quarta " +
                                       "and iinv_lote_segunda <> iinv_lote_quinta " +
                                       "and iinv_lote_segunda <> iinv_lote_sesta " +
                                       "and iinv_lote_segunda <> iinv_lote_setima " +
                                       "and iinv_lote_segunda <> iinv_lote_oitava " +
                                       "and iinv_lote_terceira <> iinv_lote_quarta " +
                                       "and iinv_lote_terceira <> iinv_lote_quinta " +
                                       "and iinv_lote_terceira <> iinv_lote_sesta " +
                                       "and iinv_lote_terceira <> iinv_lote_setima " +
                                       "and iinv_lote_terceira <> iinv_lote_oitava " +
                                       "and iinv_lote_quarta <> iinv_lote_quinta " +
                                       "and iinv_lote_quarta <> iinv_lote_sesta " +
                                       "and iinv_lote_quarta <> iinv_lote_setima " +
                                       "and iinv_lote_quarta <> iinv_lote_oitava " +
                                       "and iinv_lote_quinta <> iinv_lote_sesta " +
                                       "and iinv_lote_quinta <> iinv_lote_setima " +
                                       "and iinv_lote_quinta <> iinv_lote_oitava " +
                                       "and iinv_lote_sesta <> iinv_lote_setima " +
                                       "and iinv_lote_sesta <> iinv_lote_oitava " +
                                       "and iinv_lote_setima <> iinv_lote_oitava " +
                                       "and iinv_lote_nona is null " +
                                       /*Contagem 10*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_cont_primeira <> iinv_cont_segunda " +
                                       "and iinv_cont_primeira <> iinv_cont_terceira " +
                                       "and iinv_cont_primeira <> iinv_cont_quarta " +
                                       "and iinv_cont_primeira <> iinv_cont_quinta " +
                                       "and iinv_cont_primeira <> iinv_cont_sesta " +
                                       "and iinv_cont_primeira <> iinv_cont_setima " +
                                       "and iinv_cont_primeira <> iinv_cont_oitava " +
                                       "and iinv_cont_primeira <> iinv_cont_nona " +
                                       "and iinv_cont_segunda <> iinv_cont_terceira " +
                                       "and iinv_cont_segunda <> iinv_cont_quarta " +
                                       "and iinv_cont_segunda <> iinv_cont_quinta " +
                                       "and iinv_cont_segunda <> iinv_cont_sesta " +
                                       "and iinv_cont_segunda <> iinv_cont_setima " +
                                       "and iinv_cont_segunda <> iinv_cont_oitava " +
                                       "and iinv_cont_segunda <> iinv_cont_nona " +
                                       "and iinv_cont_terceira <> iinv_cont_quarta " +
                                       "and iinv_cont_terceira <> iinv_cont_quinta " +
                                       "and iinv_cont_terceira <> iinv_cont_sesta " +
                                       "and iinv_cont_terceira <> iinv_cont_setima " +
                                       "and iinv_cont_terceira <> iinv_cont_oitava " +
                                       "and iinv_cont_terceira <> iinv_cont_nona " +
                                       "and iinv_cont_quarta <> iinv_cont_quinta " +
                                       "and iinv_cont_quarta <> iinv_cont_sesta " +
                                       "and iinv_cont_quarta <> iinv_cont_setima " +
                                       "and iinv_cont_quarta <> iinv_cont_oitava " +
                                       "and iinv_cont_quarta <> iinv_cont_nona " +
                                       "and iinv_cont_quinta <> iinv_cont_sesta " +
                                       "and iinv_cont_quinta <> iinv_cont_setima " +
                                       "and iinv_cont_quinta <> iinv_cont_oitava " +
                                       "and iinv_cont_quinta <> iinv_cont_nona " +
                                       "and iinv_cont_sesta <> iinv_cont_setima " +
                                       "and iinv_cont_sesta <> iinv_cont_oitava " +
                                       "and iinv_cont_sesta <> iinv_cont_nona " +
                                       "and iinv_cont_setima <> iinv_cont_oitava " +
                                       "and iinv_cont_setima <> iinv_cont_nona " +
                                       "and iinv_cont_oitava <> iinv_cont_nona " +
                                       "and iinv_cont_decima is null " +
                                       /*Vencimento 10*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_primeira <> iinv_venc_terceira " +
                                       "and iinv_venc_primeira <> iinv_venc_quarta " +
                                       "and iinv_venc_primeira <> iinv_venc_quinta " +
                                       "and iinv_venc_primeira <> iinv_venc_sesta " +
                                       "and iinv_venc_primeira <> iinv_venc_setima " +
                                       "and iinv_venc_primeira <> iinv_venc_oitava " +
                                       "and iinv_venc_primeira <> iinv_venc_nona " +
                                       "and iinv_venc_segunda <> iinv_venc_terceira " +
                                       "and iinv_venc_segunda <> iinv_venc_quarta " +
                                       "and iinv_venc_segunda <> iinv_venc_quinta " +
                                       "and iinv_venc_segunda <> iinv_venc_sesta " +
                                       "and iinv_venc_segunda <> iinv_venc_setima " +
                                       "and iinv_venc_segunda <> iinv_venc_oitava " +
                                       "and iinv_venc_segunda <> iinv_venc_nona " +
                                       "and iinv_venc_terceira <> iinv_venc_quarta " +
                                       "and iinv_venc_terceira <> iinv_venc_quinta " +
                                       "and iinv_venc_terceira <> iinv_venc_sesta " +
                                       "and iinv_venc_terceira <> iinv_venc_setima " +
                                       "and iinv_venc_terceira <> iinv_venc_oitava " +
                                       "and iinv_venc_terceira <> iinv_venc_nona " +
                                       "and iinv_venc_quarta <> iinv_venc_quinta " +
                                       "and iinv_venc_quarta <> iinv_venc_sesta " +
                                       "and iinv_venc_quarta <> iinv_venc_setima " +
                                       "and iinv_venc_quarta <> iinv_venc_oitava " +
                                       "and iinv_venc_quarta <> iinv_venc_nona " +
                                       "and iinv_venc_quinta <> iinv_venc_sesta " +
                                       "and iinv_venc_quinta <> iinv_venc_setima " +
                                       "and iinv_venc_quinta <> iinv_venc_oitava " +
                                       "and iinv_venc_quinta <> iinv_venc_nona " +
                                       "and iinv_venc_sesta <> iinv_venc_setima " +
                                       "and iinv_venc_sesta <> iinv_venc_oitava " +
                                       "and iinv_venc_sesta <> iinv_venc_nona " +
                                       "and iinv_venc_setima <> iinv_venc_oitava " +
                                       "and iinv_venc_setima <> iinv_venc_nona " +
                                       "and iinv_venc_oitava <> iinv_venc_nona " +
                                       "and iinv_venc_decima is null " +
                                       /*Lote 10*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_primeira <> iinv_lote_terceira " +
                                       "and iinv_lote_primeira <> iinv_lote_quarta " +
                                       "and iinv_lote_primeira <> iinv_lote_quinta " +
                                       "and iinv_lote_primeira <> iinv_lote_sesta " +
                                       "and iinv_lote_primeira <> iinv_lote_setima " +
                                       "and iinv_lote_primeira <> iinv_lote_oitava " +
                                       "and iinv_lote_primeira <> iinv_lote_nona " +
                                       "and iinv_lote_segunda <> iinv_lote_terceira " +
                                       "and iinv_lote_segunda <> iinv_lote_quarta " +
                                       "and iinv_lote_segunda <> iinv_lote_quinta " +
                                       "and iinv_lote_segunda <> iinv_lote_sesta " +
                                       "and iinv_lote_segunda <> iinv_lote_setima " +
                                       "and iinv_lote_segunda <> iinv_lote_oitava " +
                                       "and iinv_lote_segunda <> iinv_lote_nona " +
                                       "and iinv_lote_terceira <> iinv_lote_quarta " +
                                       "and iinv_lote_terceira <> iinv_lote_quinta " +
                                       "and iinv_lote_terceira <> iinv_lote_sesta " +
                                       "and iinv_lote_terceira <> iinv_lote_setima " +
                                       "and iinv_lote_terceira <> iinv_lote_oitava " +
                                       "and iinv_lote_terceira <> iinv_lote_nona " +
                                       "and iinv_lote_quarta <> iinv_lote_quinta " +
                                       "and iinv_lote_quarta <> iinv_lote_sesta " +
                                       "and iinv_lote_quarta <> iinv_lote_setima " +
                                       "and iinv_lote_quarta <> iinv_lote_oitava " +
                                       "and iinv_lote_quarta <> iinv_lote_nona " +
                                       "and iinv_lote_quinta <> iinv_lote_sesta " +
                                       "and iinv_lote_quinta <> iinv_lote_setima " +
                                       "and iinv_lote_quinta <> iinv_lote_oitava " +
                                       "and iinv_lote_quinta <> iinv_lote_nona " +
                                       "and iinv_lote_sesta <> iinv_lote_setima " +
                                       "and iinv_lote_sesta <> iinv_lote_oitava " +
                                       "and iinv_lote_sesta <> iinv_lote_nona " +
                                       "and iinv_lote_setima <> iinv_lote_oitava " +
                                       "and iinv_lote_setima <> iinv_lote_nona " +
                                       "and iinv_lote_oitava <> iinv_lote_nona " +
                                       "and iinv_lote_decima is null " +
                                       /*Verifica a contagem 10*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_cont_primeira <> iinv_cont_segunda " +
                                       "and iinv_cont_primeira <> iinv_cont_terceira " +
                                       "and iinv_cont_primeira <> iinv_cont_quarta " +
                                       "and iinv_cont_primeira <> iinv_cont_quinta " +
                                       "and iinv_cont_primeira <> iinv_cont_sesta " +
                                       "and iinv_cont_primeira <> iinv_cont_setima " +
                                       "and iinv_cont_primeira <> iinv_cont_oitava " +
                                       "and iinv_cont_primeira <> iinv_cont_nona " +
                                       "and iinv_cont_primeira <> iinv_cont_decima " +
                                       "and iinv_cont_segunda <> iinv_cont_terceira " +
                                       "and iinv_cont_segunda <> iinv_cont_quarta " +
                                       "and iinv_cont_segunda <> iinv_cont_quinta " +
                                       "and iinv_cont_segunda <> iinv_cont_sesta " +
                                       "and iinv_cont_segunda <> iinv_cont_setima " +
                                       "and iinv_cont_segunda <> iinv_cont_oitava " +
                                       "and iinv_cont_segunda <> iinv_cont_nona " +
                                       "and iinv_cont_segunda <> iinv_cont_decima " +
                                       "and iinv_cont_terceira <> iinv_cont_quarta " +
                                       "and iinv_cont_terceira <> iinv_cont_quinta " +
                                       "and iinv_cont_terceira <> iinv_cont_sesta " +
                                       "and iinv_cont_terceira <> iinv_cont_setima " +
                                       "and iinv_cont_terceira <> iinv_cont_oitava " +
                                       "and iinv_cont_terceira <> iinv_cont_nona " +
                                       "and iinv_cont_terceira <> iinv_cont_decima " +
                                       "and iinv_cont_quarta <> iinv_cont_quinta " +
                                       "and iinv_cont_quarta <> iinv_cont_sesta " +
                                       "and iinv_cont_quarta <> iinv_cont_setima " +
                                       "and iinv_cont_quarta <> iinv_cont_oitava " +
                                       "and iinv_cont_quarta <> iinv_cont_nona " +
                                       "and iinv_cont_quarta <> iinv_cont_decima " +
                                       "and iinv_cont_quinta <> iinv_cont_sesta " +
                                       "and iinv_cont_quinta <> iinv_cont_setima " +
                                       "and iinv_cont_quinta <> iinv_cont_oitava " +
                                       "and iinv_cont_quinta <> iinv_cont_nona " +
                                       "and iinv_cont_quinta <> iinv_cont_decima " +
                                       "and iinv_cont_sesta <> iinv_cont_setima " +
                                       "and iinv_cont_sesta <> iinv_cont_oitava " +
                                       "and iinv_cont_sesta <> iinv_cont_nona " +
                                       "and iinv_cont_sesta <> iinv_cont_decima " +
                                       "and iinv_cont_setima <> iinv_cont_oitava " +
                                       "and iinv_cont_setima <> iinv_cont_nona " +
                                       "and iinv_cont_setima <> iinv_cont_decima " +
                                       "and iinv_cont_oitava <> iinv_cont_nona " +
                                       "and iinv_cont_oitava <> iinv_cont_decima " +
                                       "and iinv_cont_nona <> iinv_cont_decima " +
                                       /*Verifica o vencimento 10*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_venc_primeira <> iinv_venc_segunda " +
                                       "and iinv_venc_primeira <> iinv_venc_terceira " +
                                       "and iinv_venc_primeira <> iinv_venc_quarta " +
                                       "and iinv_venc_primeira <> iinv_venc_quinta " +
                                       "and iinv_venc_primeira <> iinv_venc_sesta " +
                                       "and iinv_venc_primeira <> iinv_venc_setima " +
                                       "and iinv_venc_primeira <> iinv_venc_oitava " +
                                       "and iinv_venc_primeira <> iinv_venc_nona " +
                                       "and iinv_venc_primeira <> iinv_venc_decima " +
                                       "and iinv_venc_segunda <> iinv_venc_terceira " +
                                       "and iinv_venc_segunda <> iinv_venc_quarta " +
                                       "and iinv_venc_segunda <> iinv_venc_quinta " +
                                       "and iinv_venc_segunda <> iinv_venc_sesta " +
                                       "and iinv_venc_segunda <> iinv_venc_setima " +
                                       "and iinv_venc_segunda <> iinv_venc_oitava " +
                                       "and iinv_venc_segunda <> iinv_venc_nona " +
                                       "and iinv_venc_segunda <> iinv_venc_decima " +
                                       "and iinv_venc_terceira <> iinv_venc_quarta " +
                                       "and iinv_venc_terceira <> iinv_venc_quinta " +
                                       "and iinv_venc_terceira <> iinv_venc_sesta " +
                                       "and iinv_venc_terceira <> iinv_venc_setima " +
                                       "and iinv_venc_terceira <> iinv_venc_oitava " +
                                       "and iinv_venc_terceira <> iinv_venc_nona " +
                                       "and iinv_venc_terceira <> iinv_venc_decima " +
                                       "and iinv_venc_quarta <> iinv_venc_quinta " +
                                       "and iinv_venc_quarta <> iinv_venc_sesta " +
                                       "and iinv_venc_quarta <> iinv_venc_setima " +
                                       "and iinv_venc_quarta <> iinv_venc_oitava " +
                                       "and iinv_venc_quarta <> iinv_venc_nona " +
                                       "and iinv_venc_quarta <> iinv_venc_decima " +
                                       "and iinv_venc_quinta <> iinv_venc_sesta " +
                                       "and iinv_venc_quinta <> iinv_venc_setima " +
                                       "and iinv_venc_quinta <> iinv_venc_oitava " +
                                       "and iinv_venc_quinta <> iinv_venc_nona " +
                                       "and iinv_venc_quinta <> iinv_venc_decima " +
                                       "and iinv_venc_sesta <> iinv_venc_setima " +
                                       "and iinv_venc_sesta <> iinv_venc_oitava " +
                                       "and iinv_venc_sesta <> iinv_venc_nona " +
                                       "and iinv_venc_sesta <> iinv_venc_decima " +
                                       "and iinv_venc_setima <> iinv_venc_oitava " +
                                       "and iinv_venc_setima <> iinv_venc_nona " +
                                       "and iinv_venc_setima <> iinv_venc_decima " +
                                       "and iinv_venc_oitava <> iinv_venc_nona " +
                                       "and iinv_venc_oitava <> iinv_venc_decima " +
                                       "and iinv_venc_nona <> iinv_venc_decima " +
                                       /*Verifica o Lote 10*/
                                       "or  i.inv_codigo = @codInventario " +
                                       "and iinv_lote_primeira <> iinv_lote_segunda " +
                                       "and iinv_lote_primeira <> iinv_lote_terceira " +
                                       "and iinv_lote_primeira <> iinv_lote_quarta " +
                                       "and iinv_lote_primeira <> iinv_lote_quinta " +
                                       "and iinv_lote_primeira <> iinv_lote_sesta " +
                                       "and iinv_lote_primeira <> iinv_lote_setima " +
                                       "and iinv_lote_primeira <> iinv_lote_oitava " +
                                       "and iinv_lote_primeira <> iinv_lote_nona " +
                                       "and iinv_lote_primeira <> iinv_lote_decima " +
                                       "and iinv_lote_segunda <> iinv_lote_terceira " +
                                       "and iinv_lote_segunda <> iinv_lote_quarta " +
                                       "and iinv_lote_segunda <> iinv_lote_quinta " +
                                       "and iinv_lote_segunda <> iinv_lote_sesta " +
                                       "and iinv_lote_segunda <> iinv_lote_setima " +
                                       "and iinv_lote_segunda <> iinv_lote_oitava " +
                                       "and iinv_lote_segunda <> iinv_lote_nona " +
                                       "and iinv_lote_segunda <> iinv_lote_decima " +
                                       "and iinv_lote_terceira <> iinv_lote_quarta " +
                                       "and iinv_lote_terceira <> iinv_lote_quinta " +
                                       "and iinv_lote_terceira <> iinv_lote_sesta " +
                                       "and iinv_lote_terceira <> iinv_lote_setima " +
                                       "and iinv_lote_terceira <> iinv_lote_oitava " +
                                       "and iinv_lote_terceira <> iinv_lote_nona " +
                                       "and iinv_lote_terceira <> iinv_lote_decima " +
                                       "and iinv_lote_quarta <> iinv_lote_quinta " +
                                       "and iinv_lote_quarta <> iinv_lote_sesta " +
                                       "and iinv_lote_quarta <> iinv_lote_setima " +
                                       "and iinv_lote_quarta <> iinv_lote_oitava " +
                                       "and iinv_lote_quarta <> iinv_lote_nona " +
                                       "and iinv_lote_quarta <> iinv_lote_decima " +
                                       "and iinv_lote_quinta <> iinv_lote_sesta " +
                                       "and iinv_lote_quinta <> iinv_lote_setima " +
                                       "and iinv_lote_quinta <> iinv_lote_oitava " +
                                       "and iinv_lote_quinta <> iinv_lote_nona " +
                                       "and iinv_lote_quinta <> iinv_lote_decima " +
                                       "and iinv_lote_sesta <> iinv_lote_setima " +
                                       "and iinv_lote_sesta <> iinv_lote_oitava " +
                                       "and iinv_lote_sesta <> iinv_lote_nona " +
                                       "and iinv_lote_sesta <> iinv_lote_decima " +
                                       "and iinv_lote_setima <> iinv_lote_oitava " +
                                       "and iinv_lote_setima <> iinv_lote_nona " +
                                       "and iinv_lote_setima <> iinv_lote_decima " +
                                       "and iinv_lote_oitava <> iinv_lote_nona " +
                                       "and iinv_lote_oitava <> iinv_lote_decima " +
                                       "and iinv_lote_nona <> iinv_lote_decima ";
                    }
                }

                //Divergência da contagem 1 e 2 do picking tipo flowrack 
                if (tipoPicking.Equals("FLOW RACK"))
                {
                    //Verifica a contagem
                    if (estacao > 0)
                    {
                        select += "where s.sep_tipo = 'FLOWRACK' and s.est_codigo = @estacao and iinv_cont_primeira <> iinv_cont_segunda ";
                    }

                    if (regiao > 0 && rua > 0)
                    {
                        select += "and reg_numero = @regiao and rua_numero = @rua ";
                    }

                    if (bloco > 0)
                    {
                        select += "and bloc_numero = @bloco ";
                    }

                    if (!(lado.Equals("TODOS") || lado.Equals("SELECIONE") || lado.Equals(string.Empty)))
                    {
                        select += "and bloc_lado = @lado ";
                    }

                    //Verifica a data
                    if (estacao > 0)
                    {
                        select += "or s.sep_tipo = 'FLOWRACK' and s.est_codigo = @estacao and iinv_venc_primeira <> iinv_venc_segunda ";
                    }

                    if (regiao > 0 && rua > 0)
                    {
                        select += "and reg_numero = @regiao and rua_numero = @rua ";
                    }

                    if (bloco > 0)
                    {
                        select += "and bloc_numero = @bloco ";
                    }

                    if (!(lado.Equals("TODOS") || lado.Equals("SELECIONE") || lado.Equals(string.Empty)))
                    {
                        select += "and bloc_lado = @lado ";
                    }


                }

                select += "order by apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    RelItemInventario item = new RelItemInventario();
                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        item.empresa = Convert.ToString(linha["empresa"]);

                        if (tipo.Equals("PICKING") && tipoPicking.Equals("FLOW RACK"))
                        {
                            item.tipo = tipo + " " + tipoPicking + "  -  " + descEstacao;
                        }
                        else if (tipo.Equals("PICKING") && tipoPicking.Equals("CAIXA"))
                        {
                            item.tipo = tipo + " " + tipoPicking;
                        }
                        else
                        {
                            item.tipo = tipo;
                        }

                        item.contagem = contagem;

                        item.bloco = 0;

                        item.lado = lado.ToUpper();
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        item.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        item.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        item.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        item.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iinv_cont_final"] != DBNull.Value)
                    {
                        item.contagemFinal = Convert.ToString(linha["iinv_cont_final"]);
                    }

                    if (linha["iinv_venc_final"] != DBNull.Value)
                    {
                        item.vencimentoFinal = Convert.ToString(linha["iinv_venc_final"]);
                    }

                    if (linha["iinv_lote_final"] != DBNull.Value)
                    {
                        item.loteFinal = Convert.ToString(linha["iinv_lote_final"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o invetário. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a contagem 3+
        public RelItemInventarioCollection PesqContagemPulmao3(int codInventario, int regiao, int rua, string tipo, string tipoPicking, string lado, string contagem)
        {
            try
            {
                //Instância a coleção
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@tipo", tipo);

                //String de consulta
                string select = "select " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_login, " +
                                "a.apa_tipo, reg_numero, rua_numero, bloc_numero, bloc_lado, a.apa_codigo, apa_endereco, " +
                                "prod_codigo, prod_descricao, prod_fator_pulmao, " +
                                "iinv_cont_final, iinv_venc_final, iinv_lote_final," +
                                "u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking " +
                                "from wms_item_inventario i " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = i.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking " +
                                "where a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_segunda is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_terceira is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_quarta is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_quinta is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_sesta is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_setima is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_oitava is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_nona is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_decima is null and iinv_cont_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and iinv_cont_final is null and not iinv_venc_final is null " +

                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_segunda is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_terceira is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_quarta is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_quinta is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_sesta is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_setima is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_oitava is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_nona is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_decima is null and iinv_venc_final is null " +
                                "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua and i.inv_codigo = @codInventario and not iinv_cont_final is null and iinv_venc_final is null " +
                                "order by apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    RelItemInventario item = new RelItemInventario();
                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        item.empresa = Convert.ToString(linha["empresa"]);

                        item.tipo = tipo;

                        item.contagem = contagem;

                        item.bloco = 0;

                        item.lado = lado.ToUpper();
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        item.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        item.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        item.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        item.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["iinv_cont_final"] != DBNull.Value)
                    {
                        item.contagemFinal = Convert.ToString(linha["iinv_cont_final"]);
                    }

                    if (linha["iinv_venc_final"] != DBNull.Value)
                    {
                        item.vencimentoFinal = Convert.ToString(linha["iinv_venc_final"]);
                    }

                    if (linha["iinv_lote_final"] != DBNull.Value)
                    {
                        item.loteFinal = Convert.ToString(linha["iinv_lote_final"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o invetário. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a critica do picking
        public RelItemInventarioCollection PesqCriticaPicking(int codInventario, int regiao, int rua, int bloco, string tipo, string tipoPicking, int estacao, string descEstacao, string lado, string contagem)
        {
            try
            {
                //Instância a coleção
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@bloco", bloco);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@tipo", tipoPicking);
                conexao.AdicionarParamentros("@estacao", estacao);


                //String de consulta
                string select = "select " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_login, " +
                                "s.sep_tipo, reg_numero, rua_numero, bloc_numero, bloc_lado, a.apa_codigo, apa_endereco, " +
                                "prod_codigo, prod_descricao, prod_fator_pulmao, " +
                                "iinv_cont_primeira, iinv_cont_segunda, iinv_cont_terceira, iinv_cont_quarta, iinv_cont_quinta, iinv_cont_sesta, iinv_cont_setima, iinv_cont_oitava, iinv_cont_nona, iinv_cont_decima, iinv_cont_final, " +
                                "iinv_venc_primeira, iinv_venc_segunda, iinv_venc_terceira, iinv_venc_quarta, iinv_venc_quinta, iinv_venc_sesta, iinv_venc_setima, iinv_venc_oitava, iinv_venc_nona, iinv_venc_decima, iinv_venc_final, " +
                                "iinv_lote_primeira, iinv_lote_segunda, iinv_lote_terceira, iinv_lote_quarta, iinv_lote_quinta, iinv_lote_sesta, iinv_lote_setima, iinv_lote_oitava, iinv_lote_nona, iinv_lote_decima, iinv_lote_final, " +
                                "u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking " +
                                "from wms_separacao s " +
                                "inner join wms_item_inventario i " +
                                "on i.apa_codigo = s.apa_codigo " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking ";

                if (regiao > 0 && rua > 0)
                {
                    //Verifica a contagem
                    select += "where s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                   "and i.inv_codigo = @codInventario " +
                                   "and iinv_cont_primeira <> iinv_cont_segunda " +
                                   /*Vencimento 2*/
                                   "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                   "and i.inv_codigo = @codInventario " +
                                   "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    /*Lote 2*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    /*Contagem 3*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    /*Vencimento 3*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    /*Lote 3*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    /*Contagem 4*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    /*Vencimento 4*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    /*Lote 4*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    /*Contagem 5*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    /*Vencimento 5*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    /*Lote 5*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    /*Contagem 6*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    /*Vencimento 6*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    /*Lote 6*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    /*Contagem 7*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_primeira <> iinv_cont_setima " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_setima " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_setima " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_setima " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_setima " +
                                    "and iinv_cont_sesta <> iinv_cont_setima " +
                                    /*Vencimento 7*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_primeira <> iinv_venc_setima " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_setima " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_setima " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_setima " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_setima " +
                                    "and iinv_venc_sesta <> iinv_venc_setima " +
                                    /*Lote 7*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_primeira <> iinv_lote_setima " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_setima " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_setima " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_setima " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_setima " +
                                    "and iinv_lote_sesta <> iinv_lote_setima " +
                                    /*Contagem 8*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_primeira <> iinv_cont_setima " +
                                    "and iinv_cont_primeira <> iinv_cont_oitava " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_setima " +
                                    "and iinv_cont_segunda <> iinv_cont_oitava " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_setima " +
                                    "and iinv_cont_terceira <> iinv_cont_oitava " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_setima " +
                                    "and iinv_cont_quarta <> iinv_cont_oitava " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_setima " +
                                    "and iinv_cont_quinta <> iinv_cont_oitava " +
                                    "and iinv_cont_sesta <> iinv_cont_setima " +
                                    "and iinv_cont_sesta <> iinv_cont_oitava " +
                                    "and iinv_cont_setima <> iinv_cont_oitava " +
                                    /*Vencimento 8*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_primeira <> iinv_venc_setima " +
                                    "and iinv_venc_primeira <> iinv_venc_oitava " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_setima " +
                                    "and iinv_venc_segunda <> iinv_venc_oitava " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_setima " +
                                    "and iinv_venc_terceira <> iinv_venc_oitava " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_setima " +
                                    "and iinv_venc_quarta <> iinv_venc_oitava " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_setima " +
                                    "and iinv_venc_quinta <> iinv_venc_oitava " +
                                    "and iinv_venc_sesta <> iinv_venc_setima " +
                                    "and iinv_venc_sesta <> iinv_venc_oitava " +
                                    "and iinv_venc_setima <> iinv_venc_oitava " +
                                    /*Lote 8*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_primeira <> iinv_lote_setima " +
                                    "and iinv_lote_primeira <> iinv_lote_oitava " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_setima " +
                                    "and iinv_lote_segunda <> iinv_lote_oitava " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_setima " +
                                    "and iinv_lote_terceira <> iinv_lote_oitava " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_setima " +
                                    "and iinv_lote_quarta <> iinv_lote_oitava " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_setima " +
                                    "and iinv_lote_quinta <> iinv_lote_oitava " +
                                    "and iinv_lote_sesta <> iinv_lote_setima " +
                                    "and iinv_lote_sesta <> iinv_lote_oitava " +
                                    "and iinv_lote_setima <> iinv_lote_oitava " +
                                    /*Contagem 9*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_primeira <> iinv_cont_setima " +
                                    "and iinv_cont_primeira <> iinv_cont_oitava " +
                                    "and iinv_cont_primeira <> iinv_cont_nona " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_setima " +
                                    "and iinv_cont_segunda <> iinv_cont_oitava " +
                                    "and iinv_cont_segunda <> iinv_cont_nona " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_setima " +
                                    "and iinv_cont_terceira <> iinv_cont_oitava " +
                                    "and iinv_cont_terceira <> iinv_cont_nona " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_setima " +
                                    "and iinv_cont_quarta <> iinv_cont_oitava " +
                                    "and iinv_cont_quarta <> iinv_cont_nona " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_setima " +
                                    "and iinv_cont_quinta <> iinv_cont_oitava " +
                                    "and iinv_cont_quinta <> iinv_cont_nona " +
                                    "and iinv_cont_sesta <> iinv_cont_setima " +
                                    "and iinv_cont_sesta <> iinv_cont_oitava " +
                                    "and iinv_cont_sesta <> iinv_cont_nona " +
                                    "and iinv_cont_setima <> iinv_cont_oitava " +
                                    "and iinv_cont_setima <> iinv_cont_nona " +
                                    "and iinv_cont_oitava <> iinv_cont_nona " +
                                    /*Vencimento 9*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_primeira <> iinv_venc_setima " +
                                    "and iinv_venc_primeira <> iinv_venc_oitava " +
                                    "and iinv_venc_primeira <> iinv_venc_nona " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_setima " +
                                    "and iinv_venc_segunda <> iinv_venc_oitava " +
                                    "and iinv_venc_segunda <> iinv_venc_nona " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_setima " +
                                    "and iinv_venc_terceira <> iinv_venc_oitava " +
                                    "and iinv_venc_terceira <> iinv_venc_nona " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_setima " +
                                    "and iinv_venc_quarta <> iinv_venc_oitava " +
                                    "and iinv_venc_quarta <> iinv_venc_nona " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_setima " +
                                    "and iinv_venc_quinta <> iinv_venc_oitava " +
                                    "and iinv_venc_quinta <> iinv_venc_nona " +
                                    "and iinv_venc_sesta <> iinv_venc_setima " +
                                    "and iinv_venc_sesta <> iinv_venc_oitava " +
                                    "and iinv_venc_sesta <> iinv_venc_nona " +
                                    "and iinv_venc_setima <> iinv_venc_oitava " +
                                    "and iinv_venc_setima <> iinv_venc_nona " +
                                    "and iinv_venc_oitava <> iinv_venc_nona " +
                                    /*Lote 9*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_primeira <> iinv_lote_setima " +
                                    "and iinv_lote_primeira <> iinv_lote_oitava " +
                                    "and iinv_lote_primeira <> iinv_lote_nona " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_setima " +
                                    "and iinv_lote_segunda <> iinv_lote_oitava " +
                                    "and iinv_lote_segunda <> iinv_lote_nona " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_setima " +
                                    "and iinv_lote_terceira <> iinv_lote_oitava " +
                                    "and iinv_lote_terceira <> iinv_lote_nona " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_setima " +
                                    "and iinv_lote_quarta <> iinv_lote_oitava " +
                                    "and iinv_lote_quarta <> iinv_lote_nona " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_setima " +
                                    "and iinv_lote_quinta <> iinv_lote_oitava " +
                                    "and iinv_lote_quinta <> iinv_lote_nona " +
                                    "and iinv_lote_sesta <> iinv_lote_setima " +
                                    "and iinv_lote_sesta <> iinv_lote_oitava " +
                                    "and iinv_lote_sesta <> iinv_lote_nona " +
                                    "and iinv_lote_setima <> iinv_lote_oitava " +
                                    "and iinv_lote_setima <> iinv_lote_nona " +
                                    "and iinv_lote_oitava <> iinv_lote_nona " +
                                    /*Verifica a contagem 10*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_primeira <> iinv_cont_setima " +
                                    "and iinv_cont_primeira <> iinv_cont_oitava " +
                                    "and iinv_cont_primeira <> iinv_cont_nona " +
                                    "and iinv_cont_primeira <> iinv_cont_decima " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_setima " +
                                    "and iinv_cont_segunda <> iinv_cont_oitava " +
                                    "and iinv_cont_segunda <> iinv_cont_nona " +
                                    "and iinv_cont_segunda <> iinv_cont_decima " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_setima " +
                                    "and iinv_cont_terceira <> iinv_cont_oitava " +
                                    "and iinv_cont_terceira <> iinv_cont_nona " +
                                    "and iinv_cont_terceira <> iinv_cont_decima " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_setima " +
                                    "and iinv_cont_quarta <> iinv_cont_oitava " +
                                    "and iinv_cont_quarta <> iinv_cont_nona " +
                                    "and iinv_cont_quarta <> iinv_cont_decima " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_setima " +
                                    "and iinv_cont_quinta <> iinv_cont_oitava " +
                                    "and iinv_cont_quinta <> iinv_cont_nona " +
                                    "and iinv_cont_quinta <> iinv_cont_decima " +
                                    "and iinv_cont_sesta <> iinv_cont_setima " +
                                    "and iinv_cont_sesta <> iinv_cont_oitava " +
                                    "and iinv_cont_sesta <> iinv_cont_nona " +
                                    "and iinv_cont_sesta <> iinv_cont_decima " +
                                    "and iinv_cont_setima <> iinv_cont_oitava " +
                                    "and iinv_cont_setima <> iinv_cont_nona " +
                                    "and iinv_cont_setima <> iinv_cont_decima " +
                                    "and iinv_cont_oitava <> iinv_cont_nona " +
                                    "and iinv_cont_oitava <> iinv_cont_decima " +
                                    "and iinv_cont_nona <> iinv_cont_decima " +
                                    /*Verifica o vencimento 10*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_primeira <> iinv_venc_setima " +
                                    "and iinv_venc_primeira <> iinv_venc_oitava " +
                                    "and iinv_venc_primeira <> iinv_venc_nona " +
                                    "and iinv_venc_primeira <> iinv_venc_decima " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_setima " +
                                    "and iinv_venc_segunda <> iinv_venc_oitava " +
                                    "and iinv_venc_segunda <> iinv_venc_nona " +
                                    "and iinv_venc_segunda <> iinv_venc_decima " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_setima " +
                                    "and iinv_venc_terceira <> iinv_venc_oitava " +
                                    "and iinv_venc_terceira <> iinv_venc_nona " +
                                    "and iinv_venc_terceira <> iinv_venc_decima " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_setima " +
                                    "and iinv_venc_quarta <> iinv_venc_oitava " +
                                    "and iinv_venc_quarta <> iinv_venc_nona " +
                                    "and iinv_venc_quarta <> iinv_venc_decima " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_setima " +
                                    "and iinv_venc_quinta <> iinv_venc_oitava " +
                                    "and iinv_venc_quinta <> iinv_venc_nona " +
                                    "and iinv_venc_quinta <> iinv_venc_decima " +
                                    "and iinv_venc_sesta <> iinv_venc_setima " +
                                    "and iinv_venc_sesta <> iinv_venc_oitava " +
                                    "and iinv_venc_sesta <> iinv_venc_nona " +
                                    "and iinv_venc_sesta <> iinv_venc_decima " +
                                    "and iinv_venc_setima <> iinv_venc_oitava " +
                                    "and iinv_venc_setima <> iinv_venc_nona " +
                                    "and iinv_venc_setima <> iinv_venc_decima " +
                                    "and iinv_venc_oitava <> iinv_venc_nona " +
                                    "and iinv_venc_oitava <> iinv_venc_decima " +
                                    "and iinv_venc_nona <> iinv_venc_decima " +
                                    /*Verifica o Lote 10*/
                                    "or s.sep_tipo = @tipo and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_primeira <> iinv_lote_setima " +
                                    "and iinv_lote_primeira <> iinv_lote_oitava " +
                                    "and iinv_lote_primeira <> iinv_lote_nona " +
                                    "and iinv_lote_primeira <> iinv_lote_decima " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_setima " +
                                    "and iinv_lote_segunda <> iinv_lote_oitava " +
                                    "and iinv_lote_segunda <> iinv_lote_nona " +
                                    "and iinv_lote_segunda <> iinv_lote_decima " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_setima " +
                                    "and iinv_lote_terceira <> iinv_lote_oitava " +
                                    "and iinv_lote_terceira <> iinv_lote_nona " +
                                    "and iinv_lote_terceira <> iinv_lote_decima " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_setima " +
                                    "and iinv_lote_quarta <> iinv_lote_oitava " +
                                    "and iinv_lote_quarta <> iinv_lote_nona " +
                                    "and iinv_lote_quarta <> iinv_lote_decima " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_setima " +
                                    "and iinv_lote_quinta <> iinv_lote_oitava " +
                                    "and iinv_lote_quinta <> iinv_lote_nona " +
                                    "and iinv_lote_quinta <> iinv_lote_decima " +
                                    "and iinv_lote_sesta <> iinv_lote_setima " +
                                    "and iinv_lote_sesta <> iinv_lote_oitava " +
                                    "and iinv_lote_sesta <> iinv_lote_nona " +
                                    "and iinv_lote_sesta <> iinv_lote_decima " +
                                    "and iinv_lote_setima <> iinv_lote_oitava " +
                                    "and iinv_lote_setima <> iinv_lote_nona " +
                                    "and iinv_lote_setima <> iinv_lote_decima " +
                                    "and iinv_lote_oitava <> iinv_lote_nona " +
                                    "and iinv_lote_oitava <> iinv_lote_decima " +
                                    "and iinv_lote_nona <> iinv_lote_decima ";
                }

                select += "order by apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    RelItemInventario item = new RelItemInventario();
                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        item.empresa = Convert.ToString(linha["empresa"]);

                        if (tipo.Equals("PICKING") && tipoPicking.Equals("FLOW RACK"))
                        {
                            item.tipo = tipo + " " + tipoPicking + "  -  " + descEstacao;
                        }
                        else if (tipo.Equals("PICKING") && tipoPicking.Equals("CAIXA"))
                        {
                            item.tipo = tipo + " " + tipoPicking;
                        }
                        else
                        {
                            item.tipo = tipo;
                        }

                        item.contagem = contagem;

                        item.bloco = 0;

                        item.lado = lado.ToUpper();
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        item.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        item.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        item.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        item.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iinv_cont_primeira"] != DBNull.Value)
                    {
                        item.contagem1 = Convert.ToString(linha["iinv_cont_primeira"]);
                    }

                    if (linha["iinv_cont_segunda"] != DBNull.Value)
                    {
                        item.contagem2 = Convert.ToString(linha["iinv_cont_segunda"]);
                    }

                    if (linha["iinv_cont_terceira"] != DBNull.Value)
                    {
                        item.contagem3 = Convert.ToString(linha["iinv_cont_terceira"]);
                    }

                    if (linha["iinv_cont_quarta"] != DBNull.Value)
                    {
                        item.contagem4 = Convert.ToString(linha["iinv_cont_quarta"]);
                    }

                    if (linha["iinv_cont_quinta"] != DBNull.Value)
                    {
                        item.contagem5 = Convert.ToString(linha["iinv_cont_quinta"]);
                    }

                    if (linha["iinv_cont_sesta"] != DBNull.Value)
                    {
                        item.contagem6 = Convert.ToString(linha["iinv_cont_sesta"]);
                    }

                    if (linha["iinv_cont_setima"] != DBNull.Value)
                    {
                        item.contagem7 = Convert.ToString(linha["iinv_cont_setima"]);
                    }

                    if (linha["iinv_cont_oitava"] != DBNull.Value)
                    {
                        item.contagem8 = Convert.ToString(linha["iinv_cont_oitava"]);
                    }

                    if (linha["iinv_cont_nona"] != DBNull.Value)
                    {
                        item.contagem9 = Convert.ToString(linha["iinv_cont_nona"]);
                    }

                    if (linha["iinv_cont_decima"] != DBNull.Value)
                    {
                        item.contagem10 = Convert.ToString(linha["iinv_cont_decima"]);
                    }

                    if (linha["iinv_cont_final"] != DBNull.Value)
                    {
                        item.contagemFinal = Convert.ToString(linha["iinv_cont_Final"]);
                    }

                    if (linha["iinv_venc_primeira"] != DBNull.Value)
                    {
                        item.vencimento1 = Convert.ToString(linha["iinv_venc_primeira"]);
                    }

                    if (linha["iinv_venc_segunda"] != DBNull.Value)
                    {
                        item.vencimento2 = Convert.ToString(linha["iinv_venc_segunda"]);
                    }

                    if (linha["iinv_venc_terceira"] != DBNull.Value)
                    {
                        item.vencimento3 = Convert.ToString(linha["iinv_venc_terceira"]);
                    }

                    if (linha["iinv_venc_quarta"] != DBNull.Value)
                    {
                        item.vencimento4 = Convert.ToString(linha["iinv_venc_quarta"]);
                    }

                    if (linha["iinv_venc_quinta"] != DBNull.Value)
                    {
                        item.vencimento5 = Convert.ToString(linha["iinv_venc_quinta"]);
                    }

                    if (linha["iinv_venc_sesta"] != DBNull.Value)
                    {
                        item.vencimento6 = Convert.ToString(linha["iinv_venc_sesta"]);
                    }

                    if (linha["iinv_venc_setima"] != DBNull.Value)
                    {
                        item.vencimento7 = Convert.ToString(linha["iinv_venc_setima"]);
                    }

                    if (linha["iinv_venc_oitava"] != DBNull.Value)
                    {
                        item.vencimento8 = Convert.ToString(linha["iinv_venc_oitava"]);
                    }

                    if (linha["iinv_venc_nona"] != DBNull.Value)
                    {
                        item.vencimento9 = Convert.ToString(linha["iinv_venc_nona"]);
                    }

                    if (linha["iinv_venc_decima"] != DBNull.Value)
                    {
                        item.vencimento10 = Convert.ToString(linha["iinv_venc_decima"]);
                    }

                    if (linha["iinv_venc_final"] != DBNull.Value)
                    {
                        item.vencimentoFinal = Convert.ToString(linha["iinv_venc_final"]);
                    }

                    if (linha["iinv_lote_primeira"] != DBNull.Value)
                    {
                        item.lote1 = Convert.ToString(linha["iinv_lote_primeira"]);
                    }

                    if (linha["iinv_lote_segunda"] != DBNull.Value)
                    {
                        item.lote2 = Convert.ToString(linha["iinv_lote_segunda"]);
                    }

                    if (linha["iinv_lote_terceira"] != DBNull.Value)
                    {
                        item.lote3 = Convert.ToString(linha["iinv_lote_terceira"]);
                    }

                    if (linha["iinv_lote_quarta"] != DBNull.Value)
                    {
                        item.lote4 = Convert.ToString(linha["iinv_lote_quarta"]);
                    }

                    if (linha["iinv_lote_quinta"] != DBNull.Value)
                    {
                        item.lote5 = Convert.ToString(linha["iinv_lote_quinta"]);
                    }

                    if (linha["iinv_lote_sesta"] != DBNull.Value)
                    {
                        item.lote6 = Convert.ToString(linha["iinv_lote_sesta"]);
                    }

                    if (linha["iinv_lote_setima"] != DBNull.Value)
                    {
                        item.lote7 = Convert.ToString(linha["iinv_lote_setima"]);
                    }

                    if (linha["iinv_lote_oitava"] != DBNull.Value)
                    {
                        item.lote8 = Convert.ToString(linha["iinv_lote_oitava"]);
                    }

                    if (linha["iinv_lote_nona"] != DBNull.Value)
                    {
                        item.lote9 = Convert.ToString(linha["iinv_lote_nona"]);
                    }

                    if (linha["iinv_lote_decima"] != DBNull.Value)
                    {
                        item.lote10 = Convert.ToString(linha["iinv_lote_decima"]);
                    }

                    if (linha["iinv_lote_final"] != DBNull.Value)
                    {
                        item.loteFinal = Convert.ToString(linha["iinv_lote_final"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o invetário. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a critica do Pulmão
        public RelItemInventarioCollection PesqCriticaPulmao(int codInventario, int regiao, int rua, int bloco, string tipo, string tipoPicking, int estacao, string descEstacao, string lado, string contagem)
        {
            try
            {
                //Instância a coleção
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@bloco", bloco);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@tipo", tipoPicking);
                conexao.AdicionarParamentros("@estacao", estacao);


                //String de consulta
                string select = "select " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_login, " +
                                "a.apa_tipo, reg_numero, rua_numero, bloc_numero, bloc_lado, a.apa_codigo, apa_endereco, " +
                                "prod_codigo, prod_descricao, prod_fator_pulmao, " +
                                "iinv_cont_primeira, iinv_cont_segunda, iinv_cont_terceira, iinv_cont_quarta, iinv_cont_quinta, iinv_cont_sesta, iinv_cont_setima, iinv_cont_oitava, iinv_cont_nona, iinv_cont_decima, iinv_cont_final, " +
                                "iinv_venc_primeira, iinv_venc_segunda, iinv_venc_terceira, iinv_venc_quarta, iinv_venc_quinta, iinv_venc_sesta, iinv_venc_setima, iinv_venc_oitava, iinv_venc_nona, iinv_venc_decima, iinv_venc_final, " +
                                "iinv_lote_primeira, iinv_lote_segunda, iinv_lote_terceira, iinv_lote_quarta, iinv_lote_quinta, iinv_lote_sesta, iinv_lote_setima, iinv_lote_oitava, iinv_lote_nona, iinv_lote_decima, iinv_lote_final, " +
                                "u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking " +
                                "from wms_armazenagem s " +
                                "inner join wms_item_inventario i " +
                                "on i.apa_codigo = s.apa_codigo " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking ";

                if (regiao > 0 && rua > 0)
                {
                    //Verifica a contagem
                    select += "where a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                   "and i.inv_codigo = @codInventario " +
                                   "and iinv_cont_primeira <> iinv_cont_segunda " +
                                   /*Vencimento 2*/
                                   "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                   "and i.inv_codigo = @codInventario " +
                                   "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    /*Lote 2*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    /*Contagem 3*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    /*Vencimento 3*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    /*Lote 3*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    /*Contagem 4*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    /*Vencimento 4*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    /*Lote 4*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    /*Contagem 5*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    /*Vencimento 5*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    /*Lote 5*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    /*Contagem 6*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    /*Vencimento 6*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    /*Lote 6*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    /*Contagem 7*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_primeira <> iinv_cont_setima " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_setima " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_setima " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_setima " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_setima " +
                                    "and iinv_cont_sesta <> iinv_cont_setima " +
                                    /*Vencimento 7*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_primeira <> iinv_venc_setima " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_setima " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_setima " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_setima " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_setima " +
                                    "and iinv_venc_sesta <> iinv_venc_setima " +
                                    /*Lote 7*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_primeira <> iinv_lote_setima " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_setima " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_setima " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_setima " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_setima " +
                                    "and iinv_lote_sesta <> iinv_lote_setima " +
                                    /*Contagem 8*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_primeira <> iinv_cont_setima " +
                                    "and iinv_cont_primeira <> iinv_cont_oitava " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_setima " +
                                    "and iinv_cont_segunda <> iinv_cont_oitava " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_setima " +
                                    "and iinv_cont_terceira <> iinv_cont_oitava " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_setima " +
                                    "and iinv_cont_quarta <> iinv_cont_oitava " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_setima " +
                                    "and iinv_cont_quinta <> iinv_cont_oitava " +
                                    "and iinv_cont_sesta <> iinv_cont_setima " +
                                    "and iinv_cont_sesta <> iinv_cont_oitava " +
                                    "and iinv_cont_setima <> iinv_cont_oitava " +
                                    /*Vencimento 8*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_primeira <> iinv_venc_setima " +
                                    "and iinv_venc_primeira <> iinv_venc_oitava " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_setima " +
                                    "and iinv_venc_segunda <> iinv_venc_oitava " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_setima " +
                                    "and iinv_venc_terceira <> iinv_venc_oitava " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_setima " +
                                    "and iinv_venc_quarta <> iinv_venc_oitava " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_setima " +
                                    "and iinv_venc_quinta <> iinv_venc_oitava " +
                                    "and iinv_venc_sesta <> iinv_venc_setima " +
                                    "and iinv_venc_sesta <> iinv_venc_oitava " +
                                    "and iinv_venc_setima <> iinv_venc_oitava " +
                                    /*Lote 8*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_primeira <> iinv_lote_setima " +
                                    "and iinv_lote_primeira <> iinv_lote_oitava " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_setima " +
                                    "and iinv_lote_segunda <> iinv_lote_oitava " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_setima " +
                                    "and iinv_lote_terceira <> iinv_lote_oitava " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_setima " +
                                    "and iinv_lote_quarta <> iinv_lote_oitava " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_setima " +
                                    "and iinv_lote_quinta <> iinv_lote_oitava " +
                                    "and iinv_lote_sesta <> iinv_lote_setima " +
                                    "and iinv_lote_sesta <> iinv_lote_oitava " +
                                    "and iinv_lote_setima <> iinv_lote_oitava " +
                                    /*Contagem 9*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_primeira <> iinv_cont_setima " +
                                    "and iinv_cont_primeira <> iinv_cont_oitava " +
                                    "and iinv_cont_primeira <> iinv_cont_nona " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_setima " +
                                    "and iinv_cont_segunda <> iinv_cont_oitava " +
                                    "and iinv_cont_segunda <> iinv_cont_nona " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_setima " +
                                    "and iinv_cont_terceira <> iinv_cont_oitava " +
                                    "and iinv_cont_terceira <> iinv_cont_nona " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_setima " +
                                    "and iinv_cont_quarta <> iinv_cont_oitava " +
                                    "and iinv_cont_quarta <> iinv_cont_nona " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_setima " +
                                    "and iinv_cont_quinta <> iinv_cont_oitava " +
                                    "and iinv_cont_quinta <> iinv_cont_nona " +
                                    "and iinv_cont_sesta <> iinv_cont_setima " +
                                    "and iinv_cont_sesta <> iinv_cont_oitava " +
                                    "and iinv_cont_sesta <> iinv_cont_nona " +
                                    "and iinv_cont_setima <> iinv_cont_oitava " +
                                    "and iinv_cont_setima <> iinv_cont_nona " +
                                    "and iinv_cont_oitava <> iinv_cont_nona " +
                                    /*Vencimento 9*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_primeira <> iinv_venc_setima " +
                                    "and iinv_venc_primeira <> iinv_venc_oitava " +
                                    "and iinv_venc_primeira <> iinv_venc_nona " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_setima " +
                                    "and iinv_venc_segunda <> iinv_venc_oitava " +
                                    "and iinv_venc_segunda <> iinv_venc_nona " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_setima " +
                                    "and iinv_venc_terceira <> iinv_venc_oitava " +
                                    "and iinv_venc_terceira <> iinv_venc_nona " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_setima " +
                                    "and iinv_venc_quarta <> iinv_venc_oitava " +
                                    "and iinv_venc_quarta <> iinv_venc_nona " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_setima " +
                                    "and iinv_venc_quinta <> iinv_venc_oitava " +
                                    "and iinv_venc_quinta <> iinv_venc_nona " +
                                    "and iinv_venc_sesta <> iinv_venc_setima " +
                                    "and iinv_venc_sesta <> iinv_venc_oitava " +
                                    "and iinv_venc_sesta <> iinv_venc_nona " +
                                    "and iinv_venc_setima <> iinv_venc_oitava " +
                                    "and iinv_venc_setima <> iinv_venc_nona " +
                                    "and iinv_venc_oitava <> iinv_venc_nona " +
                                    /*Lote 9*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_primeira <> iinv_lote_setima " +
                                    "and iinv_lote_primeira <> iinv_lote_oitava " +
                                    "and iinv_lote_primeira <> iinv_lote_nona " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_setima " +
                                    "and iinv_lote_segunda <> iinv_lote_oitava " +
                                    "and iinv_lote_segunda <> iinv_lote_nona " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_setima " +
                                    "and iinv_lote_terceira <> iinv_lote_oitava " +
                                    "and iinv_lote_terceira <> iinv_lote_nona " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_setima " +
                                    "and iinv_lote_quarta <> iinv_lote_oitava " +
                                    "and iinv_lote_quarta <> iinv_lote_nona " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_setima " +
                                    "and iinv_lote_quinta <> iinv_lote_oitava " +
                                    "and iinv_lote_quinta <> iinv_lote_nona " +
                                    "and iinv_lote_sesta <> iinv_lote_setima " +
                                    "and iinv_lote_sesta <> iinv_lote_oitava " +
                                    "and iinv_lote_sesta <> iinv_lote_nona " +
                                    "and iinv_lote_setima <> iinv_lote_oitava " +
                                    "and iinv_lote_setima <> iinv_lote_nona " +
                                    "and iinv_lote_oitava <> iinv_lote_nona " +
                                    /*Verifica a contagem 10*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_cont_primeira <> iinv_cont_segunda " +
                                    "and iinv_cont_primeira <> iinv_cont_terceira " +
                                    "and iinv_cont_primeira <> iinv_cont_quarta " +
                                    "and iinv_cont_primeira <> iinv_cont_quinta " +
                                    "and iinv_cont_primeira <> iinv_cont_sesta " +
                                    "and iinv_cont_primeira <> iinv_cont_setima " +
                                    "and iinv_cont_primeira <> iinv_cont_oitava " +
                                    "and iinv_cont_primeira <> iinv_cont_nona " +
                                    "and iinv_cont_primeira <> iinv_cont_decima " +
                                    "and iinv_cont_segunda <> iinv_cont_terceira " +
                                    "and iinv_cont_segunda <> iinv_cont_quarta " +
                                    "and iinv_cont_segunda <> iinv_cont_quinta " +
                                    "and iinv_cont_segunda <> iinv_cont_sesta " +
                                    "and iinv_cont_segunda <> iinv_cont_setima " +
                                    "and iinv_cont_segunda <> iinv_cont_oitava " +
                                    "and iinv_cont_segunda <> iinv_cont_nona " +
                                    "and iinv_cont_segunda <> iinv_cont_decima " +
                                    "and iinv_cont_terceira <> iinv_cont_quarta " +
                                    "and iinv_cont_terceira <> iinv_cont_quinta " +
                                    "and iinv_cont_terceira <> iinv_cont_sesta " +
                                    "and iinv_cont_terceira <> iinv_cont_setima " +
                                    "and iinv_cont_terceira <> iinv_cont_oitava " +
                                    "and iinv_cont_terceira <> iinv_cont_nona " +
                                    "and iinv_cont_terceira <> iinv_cont_decima " +
                                    "and iinv_cont_quarta <> iinv_cont_quinta " +
                                    "and iinv_cont_quarta <> iinv_cont_sesta " +
                                    "and iinv_cont_quarta <> iinv_cont_setima " +
                                    "and iinv_cont_quarta <> iinv_cont_oitava " +
                                    "and iinv_cont_quarta <> iinv_cont_nona " +
                                    "and iinv_cont_quarta <> iinv_cont_decima " +
                                    "and iinv_cont_quinta <> iinv_cont_sesta " +
                                    "and iinv_cont_quinta <> iinv_cont_setima " +
                                    "and iinv_cont_quinta <> iinv_cont_oitava " +
                                    "and iinv_cont_quinta <> iinv_cont_nona " +
                                    "and iinv_cont_quinta <> iinv_cont_decima " +
                                    "and iinv_cont_sesta <> iinv_cont_setima " +
                                    "and iinv_cont_sesta <> iinv_cont_oitava " +
                                    "and iinv_cont_sesta <> iinv_cont_nona " +
                                    "and iinv_cont_sesta <> iinv_cont_decima " +
                                    "and iinv_cont_setima <> iinv_cont_oitava " +
                                    "and iinv_cont_setima <> iinv_cont_nona " +
                                    "and iinv_cont_setima <> iinv_cont_decima " +
                                    "and iinv_cont_oitava <> iinv_cont_nona " +
                                    "and iinv_cont_oitava <> iinv_cont_decima " +
                                    "and iinv_cont_nona <> iinv_cont_decima " +
                                    /*Verifica o vencimento 10*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_venc_primeira <> iinv_venc_segunda " +
                                    "and iinv_venc_primeira <> iinv_venc_terceira " +
                                    "and iinv_venc_primeira <> iinv_venc_quarta " +
                                    "and iinv_venc_primeira <> iinv_venc_quinta " +
                                    "and iinv_venc_primeira <> iinv_venc_sesta " +
                                    "and iinv_venc_primeira <> iinv_venc_setima " +
                                    "and iinv_venc_primeira <> iinv_venc_oitava " +
                                    "and iinv_venc_primeira <> iinv_venc_nona " +
                                    "and iinv_venc_primeira <> iinv_venc_decima " +
                                    "and iinv_venc_segunda <> iinv_venc_terceira " +
                                    "and iinv_venc_segunda <> iinv_venc_quarta " +
                                    "and iinv_venc_segunda <> iinv_venc_quinta " +
                                    "and iinv_venc_segunda <> iinv_venc_sesta " +
                                    "and iinv_venc_segunda <> iinv_venc_setima " +
                                    "and iinv_venc_segunda <> iinv_venc_oitava " +
                                    "and iinv_venc_segunda <> iinv_venc_nona " +
                                    "and iinv_venc_segunda <> iinv_venc_decima " +
                                    "and iinv_venc_terceira <> iinv_venc_quarta " +
                                    "and iinv_venc_terceira <> iinv_venc_quinta " +
                                    "and iinv_venc_terceira <> iinv_venc_sesta " +
                                    "and iinv_venc_terceira <> iinv_venc_setima " +
                                    "and iinv_venc_terceira <> iinv_venc_oitava " +
                                    "and iinv_venc_terceira <> iinv_venc_nona " +
                                    "and iinv_venc_terceira <> iinv_venc_decima " +
                                    "and iinv_venc_quarta <> iinv_venc_quinta " +
                                    "and iinv_venc_quarta <> iinv_venc_sesta " +
                                    "and iinv_venc_quarta <> iinv_venc_setima " +
                                    "and iinv_venc_quarta <> iinv_venc_oitava " +
                                    "and iinv_venc_quarta <> iinv_venc_nona " +
                                    "and iinv_venc_quarta <> iinv_venc_decima " +
                                    "and iinv_venc_quinta <> iinv_venc_sesta " +
                                    "and iinv_venc_quinta <> iinv_venc_setima " +
                                    "and iinv_venc_quinta <> iinv_venc_oitava " +
                                    "and iinv_venc_quinta <> iinv_venc_nona " +
                                    "and iinv_venc_quinta <> iinv_venc_decima " +
                                    "and iinv_venc_sesta <> iinv_venc_setima " +
                                    "and iinv_venc_sesta <> iinv_venc_oitava " +
                                    "and iinv_venc_sesta <> iinv_venc_nona " +
                                    "and iinv_venc_sesta <> iinv_venc_decima " +
                                    "and iinv_venc_setima <> iinv_venc_oitava " +
                                    "and iinv_venc_setima <> iinv_venc_nona " +
                                    "and iinv_venc_setima <> iinv_venc_decima " +
                                    "and iinv_venc_oitava <> iinv_venc_nona " +
                                    "and iinv_venc_oitava <> iinv_venc_decima " +
                                    "and iinv_venc_nona <> iinv_venc_decima " +
                                    /*Verifica o Lote 10*/
                                    "or a.apa_tipo = 'Pulmao' and reg_numero = @regiao and rua_numero = @rua " +
                                    "and i.inv_codigo = @codInventario " +
                                    "and iinv_lote_primeira <> iinv_lote_segunda " +
                                    "and iinv_lote_primeira <> iinv_lote_terceira " +
                                    "and iinv_lote_primeira <> iinv_lote_quarta " +
                                    "and iinv_lote_primeira <> iinv_lote_quinta " +
                                    "and iinv_lote_primeira <> iinv_lote_sesta " +
                                    "and iinv_lote_primeira <> iinv_lote_setima " +
                                    "and iinv_lote_primeira <> iinv_lote_oitava " +
                                    "and iinv_lote_primeira <> iinv_lote_nona " +
                                    "and iinv_lote_primeira <> iinv_lote_decima " +
                                    "and iinv_lote_segunda <> iinv_lote_terceira " +
                                    "and iinv_lote_segunda <> iinv_lote_quarta " +
                                    "and iinv_lote_segunda <> iinv_lote_quinta " +
                                    "and iinv_lote_segunda <> iinv_lote_sesta " +
                                    "and iinv_lote_segunda <> iinv_lote_setima " +
                                    "and iinv_lote_segunda <> iinv_lote_oitava " +
                                    "and iinv_lote_segunda <> iinv_lote_nona " +
                                    "and iinv_lote_segunda <> iinv_lote_decima " +
                                    "and iinv_lote_terceira <> iinv_lote_quarta " +
                                    "and iinv_lote_terceira <> iinv_lote_quinta " +
                                    "and iinv_lote_terceira <> iinv_lote_sesta " +
                                    "and iinv_lote_terceira <> iinv_lote_setima " +
                                    "and iinv_lote_terceira <> iinv_lote_oitava " +
                                    "and iinv_lote_terceira <> iinv_lote_nona " +
                                    "and iinv_lote_terceira <> iinv_lote_decima " +
                                    "and iinv_lote_quarta <> iinv_lote_quinta " +
                                    "and iinv_lote_quarta <> iinv_lote_sesta " +
                                    "and iinv_lote_quarta <> iinv_lote_setima " +
                                    "and iinv_lote_quarta <> iinv_lote_oitava " +
                                    "and iinv_lote_quarta <> iinv_lote_nona " +
                                    "and iinv_lote_quarta <> iinv_lote_decima " +
                                    "and iinv_lote_quinta <> iinv_lote_sesta " +
                                    "and iinv_lote_quinta <> iinv_lote_setima " +
                                    "and iinv_lote_quinta <> iinv_lote_oitava " +
                                    "and iinv_lote_quinta <> iinv_lote_nona " +
                                    "and iinv_lote_quinta <> iinv_lote_decima " +
                                    "and iinv_lote_sesta <> iinv_lote_setima " +
                                    "and iinv_lote_sesta <> iinv_lote_oitava " +
                                    "and iinv_lote_sesta <> iinv_lote_nona " +
                                    "and iinv_lote_sesta <> iinv_lote_decima " +
                                    "and iinv_lote_setima <> iinv_lote_oitava " +
                                    "and iinv_lote_setima <> iinv_lote_nona " +
                                    "and iinv_lote_setima <> iinv_lote_decima " +
                                    "and iinv_lote_oitava <> iinv_lote_nona " +
                                    "and iinv_lote_oitava <> iinv_lote_decima " +
                                    "and iinv_lote_nona <> iinv_lote_decima ";
                }

                select += "order by apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    RelItemInventario item = new RelItemInventario();
                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        item.empresa = Convert.ToString(linha["empresa"]);

                        item.tipo = tipo;

                        item.contagem = contagem;

                        item.bloco = 0;

                        item.lado = lado.ToUpper();
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        item.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        item.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        item.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        item.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iinv_cont_primeira"] != DBNull.Value)
                    {
                        item.contagem1 = Convert.ToString(linha["iinv_cont_primeira"]);
                    }

                    if (linha["iinv_cont_segunda"] != DBNull.Value)
                    {
                        item.contagem2 = Convert.ToString(linha["iinv_cont_segunda"]);
                    }

                    if (linha["iinv_cont_terceira"] != DBNull.Value)
                    {
                        item.contagem3 = Convert.ToString(linha["iinv_cont_terceira"]);
                    }

                    if (linha["iinv_cont_quarta"] != DBNull.Value)
                    {
                        item.contagem4 = Convert.ToString(linha["iinv_cont_quarta"]);
                    }

                    if (linha["iinv_cont_quinta"] != DBNull.Value)
                    {
                        item.contagem5 = Convert.ToString(linha["iinv_cont_quinta"]);
                    }

                    if (linha["iinv_cont_sesta"] != DBNull.Value)
                    {
                        item.contagem6 = Convert.ToString(linha["iinv_cont_sesta"]);
                    }

                    if (linha["iinv_cont_setima"] != DBNull.Value)
                    {
                        item.contagem7 = Convert.ToString(linha["iinv_cont_setima"]);
                    }

                    if (linha["iinv_cont_oitava"] != DBNull.Value)
                    {
                        item.contagem8 = Convert.ToString(linha["iinv_cont_oitava"]);
                    }

                    if (linha["iinv_cont_nona"] != DBNull.Value)
                    {
                        item.contagem9 = Convert.ToString(linha["iinv_cont_nona"]);
                    }

                    if (linha["iinv_cont_decima"] != DBNull.Value)
                    {
                        item.contagem10 = Convert.ToString(linha["iinv_cont_decima"]);
                    }

                    if (linha["iinv_cont_final"] != DBNull.Value)
                    {
                        item.contagemFinal = Convert.ToString(linha["iinv_cont_Final"]);
                    }

                    if (linha["iinv_venc_primeira"] != DBNull.Value)
                    {
                        item.vencimento1 = Convert.ToString(linha["iinv_venc_primeira"]);
                    }

                    if (linha["iinv_venc_segunda"] != DBNull.Value)
                    {
                        item.vencimento2 = Convert.ToString(linha["iinv_venc_segunda"]);
                    }

                    if (linha["iinv_venc_terceira"] != DBNull.Value)
                    {
                        item.vencimento3 = Convert.ToString(linha["iinv_venc_terceira"]);
                    }

                    if (linha["iinv_venc_quarta"] != DBNull.Value)
                    {
                        item.vencimento4 = Convert.ToString(linha["iinv_venc_quarta"]);
                    }

                    if (linha["iinv_venc_quinta"] != DBNull.Value)
                    {
                        item.vencimento5 = Convert.ToString(linha["iinv_venc_quinta"]);
                    }

                    if (linha["iinv_venc_sesta"] != DBNull.Value)
                    {
                        item.vencimento6 = Convert.ToString(linha["iinv_venc_sesta"]);
                    }

                    if (linha["iinv_venc_setima"] != DBNull.Value)
                    {
                        item.vencimento7 = Convert.ToString(linha["iinv_venc_setima"]);
                    }

                    if (linha["iinv_venc_oitava"] != DBNull.Value)
                    {
                        item.vencimento8 = Convert.ToString(linha["iinv_venc_oitava"]);
                    }

                    if (linha["iinv_venc_nona"] != DBNull.Value)
                    {
                        item.vencimento9 = Convert.ToString(linha["iinv_venc_nona"]);
                    }

                    if (linha["iinv_venc_decima"] != DBNull.Value)
                    {
                        item.vencimento10 = Convert.ToString(linha["iinv_venc_decima"]);
                    }

                    if (linha["iinv_venc_final"] != DBNull.Value)
                    {
                        item.vencimentoFinal = Convert.ToString(linha["iinv_venc_final"]);
                    }

                    if (linha["iinv_lote_primeira"] != DBNull.Value)
                    {
                        item.lote1 = Convert.ToString(linha["iinv_lote_primeira"]);
                    }

                    if (linha["iinv_lote_segunda"] != DBNull.Value)
                    {
                        item.lote2 = Convert.ToString(linha["iinv_lote_segunda"]);
                    }

                    if (linha["iinv_lote_terceira"] != DBNull.Value)
                    {
                        item.lote3 = Convert.ToString(linha["iinv_lote_terceira"]);
                    }

                    if (linha["iinv_lote_quarta"] != DBNull.Value)
                    {
                        item.lote4 = Convert.ToString(linha["iinv_lote_quarta"]);
                    }

                    if (linha["iinv_lote_quinta"] != DBNull.Value)
                    {
                        item.lote5 = Convert.ToString(linha["iinv_lote_quinta"]);
                    }

                    if (linha["iinv_lote_sesta"] != DBNull.Value)
                    {
                        item.lote6 = Convert.ToString(linha["iinv_lote_sesta"]);
                    }

                    if (linha["iinv_lote_setima"] != DBNull.Value)
                    {
                        item.lote7 = Convert.ToString(linha["iinv_lote_setima"]);
                    }

                    if (linha["iinv_lote_oitava"] != DBNull.Value)
                    {
                        item.lote8 = Convert.ToString(linha["iinv_lote_oitava"]);
                    }

                    if (linha["iinv_lote_nona"] != DBNull.Value)
                    {
                        item.lote9 = Convert.ToString(linha["iinv_lote_nona"]);
                    }

                    if (linha["iinv_lote_decima"] != DBNull.Value)
                    {
                        item.lote10 = Convert.ToString(linha["iinv_lote_decima"]);
                    }

                    if (linha["iinv_lote_final"] != DBNull.Value)
                    {
                        item.loteFinal = Convert.ToString(linha["iinv_lote_final"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }


                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a crítica da contagem do pulmão. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a contagem
        public RelItemInventarioCollection PesqSemContagemPicking(int codInventario, string tipo, string contagem)
        {
            try
            {
                //Instância a coleção
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@tipo", tipo);

                //String de consulta
                string select = "select " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_login, " +
                                "a.apa_endereco, s.sep_tipo, prod_codigo, prod_descricao, p.prod_fator_pulmao, " +
                                "u1.uni_unidade as uni_pulmao, e.est_quantidade, u2.uni_unidade as uni_picking " +
                                "from wms_separacao s " +
                                "left join wms_estoque e " +
                                "on e.prod_id = s.prod_id " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "left join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "left join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "left join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "left join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "left join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking ";

                if (contagem.Equals("1"))
                {
                    select += "where not s.apa_codigo in (select apa_codigo from wms_item_inventario where inv_codigo = @codInventario) " +
                                "order by a.apa_ordem";
                }

                if (contagem.Equals("2"))
                {
                    select += "where s.apa_codigo in (select apa_codigo from wms_item_inventario where inv_codigo = @codInventario and iinv_cont_segunda is null) " +
                                "order by a.apa_ordem";
                }




                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    RelItemInventario item = new RelItemInventario();
                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        item.empresa = Convert.ToString(linha["empresa"]);
                        item.lado = "TODOS";
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        item.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        item.tipo = Convert.ToString(linha["sep_tipo"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["est_quantidade"] != DBNull.Value)
                    {
                        item.contagem1 = Convert.ToString(linha["est_quantidade"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar endereços sem contagem. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a contagem
        public RelItemInventarioCollection PesqSemContagemPulmao(int codInventario, string tipo, string contagem)
        {
            try
            {
                //Instância a coleção
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@tipo", tipo);

                //String de consulta
                string select = "select " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_login, " +
                                "a.apa_endereco, a.apa_tipo, prod_codigo, prod_descricao, p.prod_fator_pulmao, " +
                                "u1.uni_unidade as uni_pulmao, e.est_quantidade, u2.uni_unidade as uni_picking " +
                                "from wms_armazenagem s " +
                                "left join wms_estoque e " +
                                "on e.prod_id = s.prod_id " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "left join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "left join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "left join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "left join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "left join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking ";

                if (contagem.Equals("1"))
                {
                    select += "where not s.apa_codigo in (select apa_codigo from wms_item_inventario where inv_codigo = @codInventario) " +
                                "order by a.apa_ordem";
                }

                if (contagem.Equals("2"))
                {
                    select += "where s.apa_codigo in (select apa_codigo from wms_item_inventario where inv_codigo = @codInventario and iinv_cont_segunda is null) " +
                                "order by a.apa_ordem";
                }



                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    RelItemInventario item = new RelItemInventario();
                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        item.empresa = Convert.ToString(linha["empresa"]);

                        item.tipo = tipo;
                        item.lado = "TODOS";
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        item.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["est_quantidade"] != DBNull.Value)
                    {
                        item.contagem1 = Convert.ToString(linha["est_quantidade"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar endereços sem contagem. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o relatório de endereco do produto
        public RelItemInventario PesqRelatorioEnderecoProduto(int codInventario, int idProduto)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //Instância o objeto
                RelItemInventario prod = new RelItemInventario();

                //String de consulta
                string select = "select distinct(p.prod_id), f.forn_codigo, f.forn_nome, prod_codigo, p.prod_descricao, " +
                                "coalesce((select sum(i.iinv_cont_final) from wms_item_inventario i where prod_id = @idProduto and inv_codigo = @codInventario), 0) as contagem, " +
                                "coalesce((select sum(iflow_qtd_conferida) from wms_rastreamento_flowrack i where prod_id = @idProduto and inv_codigo = @codInventario), 0) as cont_flowrack, " +
                                "uni_unidade, " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao " +
                                "from wms_produto p " +
                                "inner join wms_fornecedor f " +
                                "on p.forn_codigo = f.forn_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "where p.prod_id = @idProduto";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados                    
                    if (linha["empresa"] != DBNull.Value)
                    {
                        prod.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        prod.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        prod.responsavel = Convert.ToString(linha["forn_codigo"]) + " - " + Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        prod.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["contagem"] != DBNull.Value)
                    {
                        prod.contagem = Convert.ToString(linha["contagem"]);
                    }

                    if (linha["cont_flowrack"] != DBNull.Value)
                    {
                        prod.contFlowRack = Convert.ToInt32(linha["cont_flowrack"]);
                    }
                    

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        prod.uniPicking = Convert.ToString(linha["uni_unidade"]);
                    }
                }

                return prod;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório de endereço do produto \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o relatório de endereco do picking do produto
        public RelItemInventarioCollection PesqRelatorioEnderecoPicking(int codInventario, int idProduto)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //Instância o objeto
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();

                //String de consulta
                string select = "select i.apa_codigo, a.apa_endereco, prod_codigo, p.prod_descricao, iinv_cont_final, iinv_venc_final, " +
                                "iinv_lote_final, s.sep_tipo, prod_fator_pulmao, est_descricao, " +
                                "u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking " +
                                "from wms_item_inventario i " +
                                "inner join wms_separacao s " +
                                "on i.apa_codigo = s.apa_codigo " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_estacao e " +
                                "on e.est_codigo = s.est_codigo " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking " +
                                "where i.prod_id = @idProduto and inv_codigo = @codInventario " +
                                "order by a.apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    RelItemInventario item = new RelItemInventario();

                    //Adiciona os dados                    
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        item.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        item.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iinv_cont_final"] != DBNull.Value)
                    {
                        item.contagemFinal = Convert.ToString(linha["iinv_cont_final"]);
                    }

                    if (linha["iinv_venc_final"] != DBNull.Value)
                    {
                        item.vencimentoFinal = Convert.ToString(linha["iinv_venc_final"]);

                    }

                    if (linha["iinv_lote_final"] != DBNull.Value)
                    {
                        item.loteFinal = Convert.ToString(linha["iinv_lote_final"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        item.tipo = Convert.ToString(linha["sep_tipo"]);
                    }

                    itemCollection.Add(item);
                }

                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório de picking do produto \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o relatório de endereço do pulmão do produto 
        public RelItemInventarioCollection PesqRelatorioEnderecoPulmao(int codInventario, int idProduto)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //Instância o objeto
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();

                //String de consulta
                string select = "select i.apa_codigo, a.apa_endereco, prod_codigo, p.prod_descricao, iinv_cont_final, iinv_venc_final, " +
                                "iinv_lote_final, 'PULMAO' as sep_tipo, prod_fator_pulmao, " +
                                "u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking " +
                                "from wms_item_inventario i " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = i.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking " +
                                "where apa_tipo = 'Pulmao' and i.prod_id = @idProduto and not iinv_cont_final is null and inv_codigo = @codInventario " +
                                "order by a.apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    RelItemInventario item = new RelItemInventario();

                    //Adiciona os dados                    
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        item.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iinv_cont_final"] != DBNull.Value)
                    {
                        item.contagemFinal = Convert.ToString(linha["iinv_cont_final"]);
                    }

                    if (linha["iinv_venc_final"] != DBNull.Value)
                    {
                        item.vencimentoFinal = Convert.ToString(linha["iinv_venc_final"]);

                    }

                    if (linha["iinv_lote_final"] != DBNull.Value)
                    {
                        item.loteFinal = Convert.ToString(linha["iinv_lote_final"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        item.uniPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        item.uniPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        item.tipo = Convert.ToString(linha["sep_tipo"]);
                    }

                    itemCollection.Add(item);
                }

                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório de pulmao do produto \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o relatório de endereço do pulmão do produto 
        public RelVolumeInventarioCollection PesqRelatorioVolumeSemContagem(int codInventario)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //Instância o objeto
                RelVolumeInventarioCollection volumeCollection = new RelVolumeInventarioCollection();

                //String de consulta
                string select = "select distinct(i.iflow_barra) as iflow_barra, ped_codigo, i.iflow_audita, a.apa_endereco, u.usu_login, " +
                    "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "(select inv_descricao from wms_inventario where inv_codigo = @codInventario) as inv_descricao, " +
                                "(select usu_login from wms_inventario i inner join wms_usuario u on u.usu_codigo = i.usu_codigo_inicial where inv_codigo = @codInventario) as usu_responsavel " +
                                "from wms_rastreamento_flowrack i " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = i.apa_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = i.usu_codigo_apa " +
                                "where iflow_barra in (select iflow_barra from wms_item_flowrack) and i.inv_codigo is null " +
                                "order by a.apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    RelVolumeInventario volume = new RelVolumeInventario();
                    //Adiciona os dados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        volume.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        volume.inventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_responsavel"] != DBNull.Value)
                    {
                        volume.responsavel = Convert.ToString(linha["usu_responsavel"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        volume.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["iflow_barra"] != DBNull.Value)
                    {
                        volume.barraVolume = Convert.ToString(linha["iflow_barra"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        volume.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["iflow_audita"] != DBNull.Value)
                    {
                        if(Convert.ToString(linha["iflow_audita"]).Equals("True"))
                        {
                            volume.auditoria = "SIM";
                        }
                        else
                        {
                            volume.auditoria = "NAO";
                        }                        
                    }
                    else
                    {
                        volume.auditoria = "NAO";
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        volume.organizador = Convert.ToString(linha["usu_login"]);
                    }                  

                    volumeCollection.Add(volume);
                }

                return volumeCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório de volumes sem contagem \nDetalhes: " + ex.Message);
            }
        }


    }
}
