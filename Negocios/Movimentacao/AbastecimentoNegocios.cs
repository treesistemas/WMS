using System;
using System.Data;
using System.Reflection;
using Dados;
using ObjetoTransferencia;
using ObjetoTransferencia.Relatorio;

namespace Negocios
{
    public class AbastecimentoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a ordem de abastecimento
        public AbastecimentoCollection PesqAbastecimento(string codAbastecimento, string modoAbastecimento, string tipoAbastecimento, string statusAbastecimento, string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                AbastecimentoCollection abastecimentoCollection = new AbastecimentoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);
                conexao.AdicionarParamentros("@modoAbastecimento", modoAbastecimento);
                conexao.AdicionarParamentros("@tipoAbastecimento", tipoAbastecimento);
                conexao.AdicionarParamentros("@statusAbastecimento", statusAbastecimento);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta - Pesquisa o bastecimento
                string select = "select a.aba_data_inicial, a.aba_codigo, a.aba_modo, a.aba_tipo, a.aba_status, u.usu_login, " +
                                "r.reg_numero, ru.rua_numero, a.rua_lado, c.cat_descricao, f.forn_codigo, f.forn_nome, " +
                                "e.usu_login as emp_login, rep.usu_login as repos_login, mani_codigo " +
                                "from wms_abastecimento a " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = a.usu_codigo_inicial " +
                                "left join wms_categoria c " +
                                "on c.cat_codigo = a.cat_codigo " +
                                "left join wms_fornecedor f " +
                                "on f.forn_codigo = a.forn_codigo " +
                                "left join wms_regiao r " +
                                "on r.reg_codigo = a.reg_codigo " +
                                "left join wms_rua ru " +
                                "on ru.rua_codigo = a.rua_codigo " +
                                "left join wms_usuario e " +
                                "on e.usu_codigo = a.usu_codigo_empilhador " +
                                "left join wms_usuario rep " +
                                "on rep.usu_codigo = a.usu_codigo_repositor ";

                if (!codAbastecimento.Equals(""))
                {
                    select += "where a.aba_codigo = @codAbastecimento ";
                }
                else
                {
                    select += "where a.aba_data_inicial between @dataInicial and @dataFinal ";

                    if (!(modoAbastecimento.Equals("") || modoAbastecimento.Equals("Selecione") || modoAbastecimento.Equals("Todos")))
                    {
                        select += "and aba_modo = @modoAbastecimento ";
                    }

                    if (!(tipoAbastecimento.Equals("") || tipoAbastecimento.Equals("Selecione") || tipoAbastecimento.Equals("Todos")))
                    {
                        select += "and aba_tipo = @tipoAbastecimento ";
                    }

                    if (!(statusAbastecimento.Equals("") || statusAbastecimento.Equals("SELECIONE") || statusAbastecimento.Equals("TODOS")))
                    {
                        select += "and aba_status = @statusAbastecimento ";
                    }
                }

                //Ordenação pelo código
                select += " and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by a.aba_codigo"; //Modificado

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    Abastecimento abastecimento = new Abastecimento();
                    //Adiciona os valores encontrados
                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        abastecimento.codAbastecimento = Convert.ToInt32(linha["aba_codigo"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        abastecimento.codManifesto = Convert.ToString(linha["mani_codigo"]);
                    }

                    if (linha["aba_tipo"] != DBNull.Value)
                    {
                        abastecimento.tipoAbastecimento = Convert.ToString(linha["aba_tipo"]);
                    }

                    if (linha["aba_modo"] != DBNull.Value)
                    {
                        abastecimento.modoAbastecimento = Convert.ToString(linha["aba_modo"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        abastecimento.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        abastecimento.numeroRua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["rua_lado"] != DBNull.Value)
                    {
                        abastecimento.ladoRua = Convert.ToString(linha["rua_lado"]);
                    }

                    if (linha["aba_status"] != DBNull.Value)
                    {
                        abastecimento.statusAbastecimento = Convert.ToString(linha["aba_status"]);
                    }

                    if (linha["aba_data_inicial"] != DBNull.Value)
                    {
                        abastecimento.dataAbertura = Convert.ToDateTime(linha["aba_data_inicial"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        abastecimento.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        abastecimento.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        abastecimento.nomeFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        abastecimento.loginResponsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["emp_login"] != DBNull.Value)
                    {
                        abastecimento.loginEmpilhador = Convert.ToString(linha["emp_login"]);
                    }

                    if (linha["repos_login"] != DBNull.Value)
                    {
                        abastecimento.loginRepositor = Convert.ToString(linha["repos_login"]);
                    }


                    //Adiciona os valores encontrados a coleção
                    abastecimentoCollection.Add(abastecimento);
                }

                //Retorna a coleção de cadastro encontrada
                return abastecimentoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as ordem de abastecimento. \nDetalhes:" + ex.Message);
            }

        }

        //Pesquisa os itens da ordem de abastecimento
        public ItensAbastecimentoCollection PesqItensAbastecimento(string codAbastecimento, string modoAbastecimento, string tipoAbastecimento, string statusAbastecimento, string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAbastecimentoCollection itensAbastecimentoCollection = new ItensAbastecimentoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);
                conexao.AdicionarParamentros("@modoAbastecimento", modoAbastecimento);
                conexao.AdicionarParamentros("@tipoAbastecimento", tipoAbastecimento);
                conexao.AdicionarParamentros("@statusAbastecimento", statusAbastecimento);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta - Pesquisa as ordem de abastecimento
                string select = "select a.aba_codigo, i.est_codigo, est_descricao, ap.apa_endereco, i.prod_id, prod_codigo, prod_descricao, prod_fator_pulmao, " +
                                "i.iaba_qtd_abastecer / p.prod_fator_pulmao as abastecimento, u1.uni_unidade as uni_pulmao, " +
                                "i.iaba_estoque, u.uni_unidade, ap1.apa_endereco as apa_pulmao, i.iaba_estoque_pulmao / p.prod_fator_pulmao as estoque_Pulmao, i.iaba_vencimento_pulmao, i.iaba_peso_pulmao, " +
                                "i.iaba_lote_pulmao, i.iaba_tipo_analise from wms_item_abastecimento i " +
                                "inner join wms_abastecimento a " +
                                "on i.aba_codigo = a.aba_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = i.apa_codigo_picking " +
                                "inner join wms_apartamento ap1 " +
                                "on ap1.apa_codigo = i.apa_codigo_pulmao " +
                                "left join wms_estacao est " +
                                "on est.est_codigo = i.est_codigo ";

                if (!codAbastecimento.Equals(""))
                {
                    select += "where a.aba_codigo = @codAbastecimento ";
                }
                else
                {
                    select += "where a.aba_data_inicial between @dataInicial and @dataFinal ";

                    if (!(modoAbastecimento.Equals("") || modoAbastecimento.Equals("Selecione") || modoAbastecimento.Equals("Todos")))
                    {
                        select += "and aba_modo = @modoAbastecimento ";
                    }

                    if (!(tipoAbastecimento.Equals("") || tipoAbastecimento.Equals("Selecione") || tipoAbastecimento.Equals("Todos")))
                    {
                        select += "and aba_tipo = @tipoAbastecimento ";
                    }

                    if (!(statusAbastecimento.Equals("") || statusAbastecimento.Equals("SELECIONE") || statusAbastecimento.Equals("TODOS")))
                    {
                        select += "and aba_status = @statusAbastecimento ";
                    }
                    select += "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                }

                //Ordenação pelo código
                select += " order by i.est_codigo, ap.apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAbastecimento itens = new ItensAbastecimento();

                    //Adiciona os valores encontrados
                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        itens.codAbastecimento = Convert.ToInt32(linha["aba_codigo"]);
                    }

                    /*if (linha["mani_codigo"] != DBNull.Value)
                    {
                        itens.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }*/

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        itens.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        itens.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.enderecoPicking = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itens.qtdCaixaProduto = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itens.qtdCaixaProduto = 1;
                    }

                    if (linha["abastecimento"] != DBNull.Value)
                    {
                        itens.qtdAbastecer = Convert.ToInt32(linha["abastecimento"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itens.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["iaba_estoque"] != DBNull.Value)
                    {
                        itens.qtdPicking = Convert.ToInt32(linha["iaba_estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itens.unidadePicking = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["apa_pulmao"] != DBNull.Value)
                    {
                        itens.enderecoPulmao = Convert.ToString(linha["apa_pulmao"]);
                    }

                    if (linha["estoque_Pulmao"] != DBNull.Value)
                    {
                        itens.qtdPulmao = Convert.ToInt32(linha["estoque_Pulmao"]);
                    }

                    if (linha["iaba_vencimento_pulmao"] != DBNull.Value)
                    {
                        itens.vencimentoPulmao = Convert.ToDateTime(linha["iaba_vencimento_pulmao"]);
                    }

                    if (linha["iaba_lote_pulmao"] != DBNull.Value)
                    {
                        itens.lotePulmao = Convert.ToString(linha["iaba_lote_pulmao"]);
                    }

                    if (linha["iaba_tipo_analise"] != DBNull.Value)
                    {
                        itens.tipoAnalise = Convert.ToString(linha["iaba_tipo_analise"]);
                    }

                    //Adiciona o objêto a coleção
                    itensAbastecimentoCollection.Add(itens);
                }


                //Retorna a coleção de cadastro encontrada
                return itensAbastecimentoCollection;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as o itens gerados. \nDetalhes: " + ex.Message);
            }

        }


        //Pesquisa um novo código para o abastecimento
        public int PesqCodigo()
        {
            try
            {
                //Instância a variável responsável pelo código
                int codigo = 0;

                //String de consulta
                string select = "select gen_id(gen_wms_abastecimento,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    codigo = Convert.ToInt32(linha["id"]);
                }
                //Retorna a coleção de cadastro encontrada
                return codigo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar uma novo código para o abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa as estações
        public EstacaoCollection PesqEstacao()
        {
            try
            {
                //Instância uma coleção de objêtos
                EstacaoCollection estacaoCollection = new EstacaoCollection();

                //String de consulta
                string select = "select e.est_codigo, e.est_descricao, e.est_tipo from wms_estacao e where e.est_status = 'True' order by est_tipo asc, est_codigo ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
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

                    if (linha["est_tipo"] != DBNull.Value)
                    {
                        estacao.tipo = Convert.ToString(linha["est_tipo"]);
                    }

                    //Adiciona os valores encontrados a coleção
                    estacaoCollection.Add(estacao);
                }

                //Retorna a coleção de cadastro encontrada
                return estacaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as estações. \nDetalhes:" + ex.Message);
            }

        }

        //Analisa os itens para o abastecimento
        public ItensAbastecimentoCollection PesqItens(string empresa, string tipo, string numeroRegiao, string numeroRua, string lado, string descCategoria, string codFornecedor, int[] codEstacao)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAbastecimentoCollection itensAbastecimentoCollection = new ItensAbastecimentoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@descCategoria", descCategoria);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);

                //String de consulta
                string select = "select e.est_codigo, e.est_descricao, p.prod_id, a.apa_codigo, a.apa_endereco, p.prod_codigo, p.prod_descricao, p.prod_fator_pulmao, coalesce(s.sep_estoque, 0) as sep_estoque, u.uni_unidade, " +
                                "s.sep_capacidade, s.sep_abastecimento,  (sep_capacidade -(coalesce(s.sep_estoque, 0) / prod_fator_pulmao))as abastecimento, u1.uni_unidade as uni_pulmao " +
                                "from wms_separacao s " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_estacao e " +
                                "on e.est_codigo = s.est_codigo " +
                                "inner join wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = r.reg_codigo " +
                                "left join wms_categoria c " +
                                "on c.cat_codigo = p.cat_codigo " +
                                "left join wms_fornecedor f " +
                                "on f.forn_codigo = p.forn_codigo ";


                if (tipo.Equals("CAIXA"))
                {
                    select += "where s.sep_tipo = @tipo and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                    if (!numeroRegiao.Equals("Selec..."))
                    {
                        select += "and re.reg_numero = @numeroRegiao ";
                    }

                    if (!numeroRua.Equals("Selec..."))
                    {
                        if (!numeroRua.Equals(string.Empty))
                        {
                            select += "and r.rua_numero = @numeroRua ";
                        }

                        if (!lado.Equals("Todos"))
                        {
                            select += "and bloc_lado = @lado ";
                        }
                    }

                    if (!descCategoria.Equals(string.Empty))
                    {
                        select += "and cat_descricao = @descCategoria ";
                    }

                    if (!codFornecedor.Equals(string.Empty))
                    {
                        select += "and f.forn_codigo = @codFornecedor ";
                    }

                }
                else if (tipo.Equals("FLOWRACK"))
                {
                    if (codEstacao != null && codEstacao.Length > 0)
                    {
                        select += "where s.sep_tipo = @tipo and s.est_codigo = " + codEstacao[0] + " and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                        for (int i = 1; codEstacao.Length > i; i++)
                        {
                            if (codEstacao[i] != 0)
                            {
                                select += "or s.sep_tipo = @tipo and s.est_codigo = " + codEstacao[i] + " ";
                            }
                        }
                    }
                }

                select += "order by a.apa_ordem";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAbastecimento itens = new ItensAbastecimento();

                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        itens.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        itens.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        itens.codApartamentoPicking = Convert.ToInt32(linha["apa_codigo"]);
                    }
                    

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.enderecoPicking = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itens.qtdCaixaProduto = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itens.qtdCaixaProduto = 1;
                    }

                    if (linha["sep_capacidade"] != DBNull.Value)
                    {
                        itens.capacidadePicking = Convert.ToInt32(linha["sep_capacidade"]);
                    }

                    if (linha["sep_abastecimento"] != DBNull.Value)
                    {
                        itens.abastecimentoPicking = Convert.ToInt32(linha["sep_abastecimento"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itens.unidadePicking = Convert.ToString(linha["uni_unidade"]);
                    }
                    else
                    {
                        itens.observacao = "FALTA DADOS LOGÍSTICOS";
                    }

                    if (linha["abastecimento"] != DBNull.Value)
                    {
                        itens.qtdAbastecer = Convert.ToInt32(linha["abastecimento"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itens.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        itens.qtdPicking = Convert.ToInt32(linha["sep_estoque"]);

                        if (Convert.ToInt32(linha["sep_estoque"]) < 0)
                        {
                            itens.observacao = "ERRO: ESTOQUE NEGATIVO";
                            itens.qtdAbastecer = 0; //Recebe Zero
                        }
                        else if (Convert.ToInt32(linha["sep_estoque"]) / Convert.ToInt32(linha["prod_fator_pulmao"]) > Convert.ToInt32(linha["sep_capacidade"]))
                        {
                            itens.observacao = "ESTOQUE ACIMA DA CAPACIDADE";
                            itens.qtdAbastecer = 0; //Recebe Zero
                        }
                        else if (Convert.ToInt32(linha["sep_estoque"]) / Convert.ToInt32(linha["prod_fator_pulmao"]) > Convert.ToInt32(linha["sep_abastecimento"]))
                        {
                            itens.observacao = "PRODUTO NÃO PRECISA DE ABASTECIMENTO";
                            itens.qtdAbastecer = 0; //Recebe Zero
                        }
                    }

                    //Adiciona o objêto a coleção
                    itensAbastecimentoCollection.Add(itens);
                }


                //Retorna a coleção de cadastro encontrada
                return itensAbastecimentoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens para o abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Analisa os itens para o abastecimento
        public ItensAbastecimentoCollection PesqItensManifesto(int[] codManifesto, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAbastecimentoCollection itensAbastecimentoCollection = new ItensAbastecimentoCollection();

                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select " +
                                /*Pesquisa o código do picking de grandeza*/
                                "(select a.apa_codigo from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "where s.sep_tipo = 'CAIXA' and prod_id = i.prod_id) as apa_codigo_cxa, " +
                                /*Pesquisa o picking de grandeza*/
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "where s.sep_tipo = 'CAIXA' and prod_id = i.prod_id) as apa_endereco_cxa, " +
                                /*Pesquisa o código do picking de flowrack */
                                "(select a.apa_codigo from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "where s.sep_tipo = 'FLOWRACK' and prod_id = i.prod_id) as apa_codigo_flow, " +
                                /*Pesquisa o picking de flowrack  , mod(sum( iped_quantidade),prod_fator_pulmao) as fracionado*/
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "where s.sep_tipo = 'FLOWRACK' and prod_id = i.prod_id) as apa_endereco_flow, " +
                                "i.prod_id, prod_codigo, prod_descricao, prod_fator_pulmao, " +
                                "(round(sum(iped_quantidade) -(select sum(sep_estoque) from wms_separacao s where s.prod_id = i.prod_id and sep_tipo = 'CAIXA'))/prod_fator_pulmao) as abastecimento, u.uni_unidade " +
                                //"trunc(sum( iped_quantidade)/prod_fator_pulmao) as abastecimento, u.uni_unidade " +
                                "from wms_item_pedido i " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = i.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = pp.uni_codigo_pulmao ";


                if (codManifesto != null && codManifesto.Length > 0)
                {
                    select += "where mani_codigo = " + codManifesto[0] + " and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                    for (int i = 1; codManifesto.Length > i; i++)
                    {
                        if (codManifesto[i] != 0)
                        {
                            select += "or mani_codigo = " + codManifesto[i] + " ";
                        }
                    }
                }

                select += "group by i.prod_id, prod_codigo, prod_descricao,prod_fator_pulmao, uni_unidade " +
                          "having (select sum(sep_estoque) from wms_separacao s where s.prod_id = i.prod_id and sep_tipo = 'CAIXA') > 0 and " +
                          "sum(iped_quantidade) - (select sum(sep_estoque) from wms_separacao s where s.prod_id = i.prod_id and sep_tipo = 'CAIXA') > 0 and(round(sum(iped_quantidade) - (select sum(sep_estoque) from wms_separacao s where s.prod_id = i.prod_id and sep_tipo = 'CAIXA')) / prod_fator_pulmao) > 0 and trunc(sum(iped_quantidade)/ prod_fator_pulmao) > 0 ";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância um objêto
                    ItensAbastecimento itens = new ItensAbastecimento();
                    //Adiciona os valores encontrados
                    /*if (linha["mani_codigo"] != DBNull.Value)
                    {
                        itens.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }*/

                    if (linha["apa_endereco_cxa"] != DBNull.Value)
                    {
                        itens.codApartamentoPicking = Convert.ToInt32(linha["apa_codigo_cxa"]);
                        itens.enderecoPicking = Convert.ToString(linha["apa_endereco_cxa"]);
                    }
                    else if (linha["apa_endereco_flow"] != DBNull.Value && linha["apa_endereco_cxa"] == DBNull.Value)
                    {
                        itens.codApartamentoPicking = Convert.ToInt32(linha["apa_codigo_flow"]);
                        itens.enderecoPicking = Convert.ToString(linha["apa_endereco_flow"]);
                    }
                    else
                    {
                        itens.enderecoPicking = "SEM PICKING";
                        itens.observacao = "FALTA DADOS LOGÍSTICOS";
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itens.qtdCaixaProduto = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itens.qtdCaixaProduto = 1;
                    }

                    if (linha["abastecimento"] != DBNull.Value)
                    {
                        itens.qtdAbastecer = Convert.ToInt32(linha["abastecimento"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itens.unidadePulmao = Convert.ToString(linha["uni_unidade"]);
                    }
                    else
                    {
                        itens.observacao = "FALTA DADOS LOGÍSTICOS";
                    }

                    //Adiciona o objêto 
                    itensAbastecimentoCollection.Add(itens);

                }


                //Retorna a coleção de cadastro encontrada
                return itensAbastecimentoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens para o abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Analisa os itens para o abastecimento
        public ItensAbastecimento PesqAbastecimentoPulmao(int idProduto, int pulaLinha, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                ItensAbastecimento itens = new ItensAbastecimento();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@pulaLinha", pulaLinha);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select first(1) skip @pulaLinha a.apa_codigo," +
                    "(select apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = r.reg_codigo " +
                                "where prod_id  = @idProduto and sep_tipo = 'CAIXA') as apa_endereco_caixa," +
                    " a.apa_endereco, ar.prod_id, arm_quantidade/prod_fator_pulmao as estoque, arm_vencimento, arm_lote " +
                                "from wms_armazenagem ar " +
                                "inner join wms_produto p " +
                                "on p.prod_id = ar.prod_id " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = ar.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = r.reg_codigo " +
                                "where p.prod_id = @idProduto and ar.arm_reserva is null " +
                                "or p.prod_id = @idProduto and ar.arm_reserva = 'False' " +
                                "or p.prod_id = @idProduto and ar.arm_bloqueado is null " +
                                "or p.prod_id = @idProduto and ar.arm_bloqueado = 'False' " +
                                "and ar.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "order by arm_vencimento asc ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados                    
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        itens.codApartamentoPulmao = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco_caixa"] != DBNull.Value)
                    {
                        itens.enderecoPicking = Convert.ToString(linha["apa_endereco_caixa"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.enderecoPulmao = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["estoque"] != DBNull.Value)
                    {
                        itens.qtdPulmao = Convert.ToInt32(linha["estoque"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        itens.vencimentoPulmao = Convert.ToDateTime(linha["arm_vencimento"]).Date;
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        itens.lotePulmao = Convert.ToString(linha["arm_lote"]);
                    }

                }

                //Retorna a coleção de cadastro encontrada
                return itens;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao analisar os itens para o abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Analisa os itens para o abastecimento
        public ItensAbastecimento PesquisaAbstecimentoPicking(int idProduto, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                ItensAbastecimento itens = new ItensAbastecimento();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select a.apa_codigo, a.apa_endereco, sep.prod_id, prod_codigo, prod_descricao, " +
                                "sep_estoque/prod_fator_pulmao as estoque, sep_capacidade, sep_abastecimento, " +
                                "(sep_capacidade -(sep_estoque / prod_fator_pulmao))as abastecimento, sep.sep_validade, sep_lote from wms_separacao sep " +
                                "inner join wms_produto p " +
                                "on p.prod_id = sep.prod_id " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = sep.apa_codigo " +
                                "where sep.prod_id = @idProduto and sep.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "and sep_tipo = 'CAIXA'";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados                    
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        itens.codApartamentoPulmao = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.enderecoPulmao = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["estoque"] != DBNull.Value)
                    {
                        itens.qtdPulmao = Convert.ToInt32(linha["estoque"]);
                    }

                    if (linha["sep_capacidade"] != DBNull.Value)
                    {
                        itens.capacidadePicking = Convert.ToInt32(linha["sep_capacidade"]);
                    }

                    if (linha["sep_abastecimento"] != DBNull.Value)
                    {
                        itens.abastecimentoPicking = Convert.ToInt32(linha["sep_abastecimento"]);
                    }

                    if (linha["abastecimento"] != DBNull.Value)
                    {
                        itens.qtdAbastecer = Convert.ToInt32(linha["abastecimento"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        itens.vencimentoPulmao = Convert.ToDateTime(linha["sep_validade"]).Date;
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        itens.lotePulmao = Convert.ToString(linha["sep_lote"]);
                    }

                }

                //Retorna a coleção de cadastro encontrada
                return itens;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao analisar os itens do flow rack para o abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Gera a ordem de abastecimento
        public void GerarAbastecimento(int codAbastecimento, int codUsuario, int? codCategoria, int? codFornecedor,
             int? regiao, int? rua, string lado, string tipo, string modo, int? codEmpilhador, int? codRepositor, string codManifesto, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codCategoria", codCategoria);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@modo", modo);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@codEmpilhador", codEmpilhador);
                conexao.AdicionarParamentros("@codRepositor", codRepositor);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de insert
                string insert = "insert into wms_abastecimento (aba_codigo, aba_data_inicial, usu_codigo_inicial, " +
                                "cat_codigo, forn_codigo, reg_codigo, rua_codigo, rua_lado, aba_modo, aba_tipo,  usu_codigo_empilhador, usu_codigo_repositor, mani_codigo, aba_status, conf_codigo) " +
                                "values " +
                                "(@codAbastecimento, current_timestamp, @codUsuario, @codCategoria, @codFornecedor, " +
                                " @regiao, @rua, @lado, @modo, @tipo, @codEmpilhador, @codRepositor, @codManifesto, 'NÃO INICIADA', (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o abastecimento. \nDetalhes:" + ex.Message);
            }
        }


        //Gera os itens de abastecimento
        public void GerarItens(int? codAbastecimento, int? codEstacao, int codPicking, int idProduto, int qtdAbasecimento, int estoquePicking,
             int codPulmao, int estoquePulmao, string vencimentoPulmao, string lotePulmao, string tipoAnalise)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                //conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@codPicking", codPicking);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@estoquePicking", estoquePicking);
                conexao.AdicionarParamentros("@qtdAbasecimento", qtdAbasecimento);
                //conexao.AdicionarParamentros("@vencimentoPicking", vencimentoPicking);
                //conexao.AdicionarParamentros("@lotePicking", lotePicking);
                conexao.AdicionarParamentros("@codPulmao", codPulmao);
                conexao.AdicionarParamentros("@estoquePulmao", estoquePulmao);
                conexao.AdicionarParamentros("@vencimentoPulmao", vencimentoPulmao);
                //conexao.AdicionarParamentros("@pesoPulmao", pesoPulmao);
                conexao.AdicionarParamentros("@lotePulmao", lotePulmao);
                conexao.AdicionarParamentros("@tipoAnalise", tipoAnalise);


                //String de insert
                string insert = "insert into wms_item_abastecimento (aba_codigo, iaba_codigo, iaba_data, est_codigo, apa_codigo_picking, prod_id, iaba_qtd_abastecer, iaba_estoque, " +
                                "apa_codigo_pulmao, iaba_estoque_pulmao, iaba_vencimento_pulmao, iaba_lote_pulmao, iaba_status, iaba_tipo_analise) " +

                                "Select @codAbastecimento, gen_id(gen_wms_itens_abastecimento, 1), current_timestamp, @codEstacao, @codPicking, @idProduto, @qtdAbasecimento, @estoquePicking, " +
                                "@codPulmao, @estoquePulmao, @vencimentoPulmao, @lotePulmao, 'PENDENTE', @tipoAnalise From RDB$DATABASE " +
                                "Where Not Exists " +
                                "(Select 1 From wms_item_abastecimento " +
                                "Where est_codigo = @codEstacao and prod_id = @idProduto and apa_codigo_pulmao = @codPulmao and iaba_status = 'PENDENTE') ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                string updatePulmao = "update wms_armazenagem set arm_bloqueado = 'True', arm_motivo_bloqueio = 'ABASTECIMENTO' where apa_codigo = @codPulmao and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) "; //Modificado
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updatePulmao);

                string updatePicking = "update wms_separacao set sep_bloqueado = 'True' where apa_codigo = @codPicking and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) "; //Modificado
                                                                                                                                                                                                                                            //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updatePicking);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Alterar a ordem de abastecimento
        public void AlterarOrdem(int codAbastecimento, int? codEmpilhador, int? codRepositor)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);
                conexao.AdicionarParamentros("@codEmpilhador", codEmpilhador);
                conexao.AdicionarParamentros("@codRepositor", codRepositor);

                //String de atualização
                string update = "update wms_abastecimento set usu_codigo_empilhador = @codEmpilhador, usu_codigo_repositor = @codRepositor " +
                    "where aba_codigo = @codAbastecimento";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar a ordem de abastecimento. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa a ordem de abastecimento
        public MapaAbastecimentoCollection RelatorioAbastecimentoGrandeza(int codAbastecimento)
        {
            try
            {
                //Instância uma coleção de objêtos
                MapaAbastecimentoCollection abastecimentoCollection = new MapaAbastecimentoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);



                //String de consulta - Pesquisa o bastecimento
                string select = "select a.aba_data_inicial, a.aba_codigo, a.aba_modo, a.aba_tipo, a.aba_status, u.usu_login, " +
                                "r.reg_numero, ru.rua_numero, a.rua_lado, c.cat_descricao, f.forn_codigo, f.forn_nome, " +
                                "e.usu_login as emp_login, rep.usu_login as repos_login, " +
                                "(select sum(i.iaba_estoque_pulmao / p.prod_fator_pulmao) " +
                                "from wms_item_abastecimento i " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "where aba_codigo = @codAbastecimento) as soma_volumes," +
                                "(select sum(i.iaba_peso_pulmao) " +
                                "from wms_item_abastecimento i " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "where aba_codigo = @codAbastecimento) as soma_peso," +
                                "(select co.conf_empresa from wms_configuracao co) as nome_empresa " +
                                "from wms_abastecimento a " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = a.usu_codigo_inicial " +
                                "left join wms_categoria c " +
                                "on c.cat_codigo = a.cat_codigo " +
                                "left join wms_fornecedor f " +
                                "on f.forn_codigo = a.forn_codigo " +
                                "left join wms_regiao r " +
                                "on r.reg_codigo = a.reg_codigo " +
                                "left join wms_rua ru " +
                                "on ru.rua_codigo = a.rua_codigo " +
                                "left join wms_usuario e " +
                                "on e.usu_codigo = a.usu_codigo_empilhador " +
                                "left join wms_usuario rep " +
                                "on rep.usu_codigo = a.usu_codigo_repositor " +
                                "where a.aba_codigo = @codAbastecimento ";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    MapaAbastecimento abastecimento = new MapaAbastecimento();
                    //Adiciona os valores encontrados

                    if (linha["nome_empresa"] != DBNull.Value)
                    {
                        abastecimento.nomeEmpresa = Convert.ToString(linha["nome_empresa"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        abastecimento.codAbastecimento = Convert.ToInt32(linha["aba_codigo"]);
                    }

                    if (linha["aba_tipo"] != DBNull.Value)
                    {
                        abastecimento.tipoAbastecimento = Convert.ToString(linha["aba_tipo"]).ToUpper();
                    }

                    if (linha["aba_modo"] != DBNull.Value)
                    {
                        abastecimento.modoAbastecimento = Convert.ToString(linha["aba_modo"]).ToUpper();
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        abastecimento.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        abastecimento.numeroRua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["rua_lado"] != DBNull.Value)
                    {
                        abastecimento.ladoRua = Convert.ToString(linha["rua_lado"]).ToUpper();
                    }

                    if (linha["soma_volumes"] != DBNull.Value)
                    {
                        abastecimento.totalVolumes = Convert.ToInt32(linha["soma_volumes"]);
                    }

                    if (linha["soma_peso"] != DBNull.Value)
                    {
                        abastecimento.totalPeso = Convert.ToDouble(linha["soma_peso"]);
                    }

                    if (linha["aba_status"] != DBNull.Value)
                    {
                        abastecimento.statusAbastecimento = Convert.ToString(linha["aba_status"]);
                    }

                    if (linha["aba_data_inicial"] != DBNull.Value)
                    {
                        abastecimento.dataAbertura = Convert.ToDateTime(linha["aba_data_inicial"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        abastecimento.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        abastecimento.nomeFornecedor = Convert.ToInt32(linha["forn_codigo"]) + " - " + Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        abastecimento.loginResponsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["emp_login"] != DBNull.Value)
                    {
                        abastecimento.loginEmpilhador = Convert.ToString(linha["emp_login"]);
                    }

                    if (linha["repos_login"] != DBNull.Value)
                    {
                        abastecimento.loginRepositor = Convert.ToString(linha["repos_login"]);
                    }



                    //Adiciona os valores encontrados a coleção
                    abastecimentoCollection.Add(abastecimento);
                }

                //Retorna a coleção de cadastro encontrada
                return abastecimentoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as ordem de abastecimento. \nDetalhes:" + ex.Message);
            }

        }

        //Pesquisa os itens da ordem de abastecimento no Pulmão
        public ItemMapaAbastecimentoCollection RelatorioItensAbastecimentoPulmao(int codAbastecimento)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItemMapaAbastecimentoCollection itensAbastecimentoCollection = new ItemMapaAbastecimentoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);

                //String de consulta - Pesquisa as ordem de abastecimento
                string select = "select a.aba_codigo, i.est_codigo, est_descricao, ap.apa_endereco, i.prod_id, prod_codigo, prod_descricao, cat_descricao, prod_fator_pulmao, " +
                                "i.iaba_qtd_abastecer / p.prod_fator_pulmao as abastecimento, u1.uni_unidade as uni_pulmao, " +
                                "i.iaba_estoque, u.uni_unidade, ap1.apa_endereco as apa_pulmao, i.iaba_estoque_pulmao / p.prod_fator_pulmao as estoque_Pulmao, i.iaba_vencimento_pulmao, i.iaba_peso_pulmao, " +
                                "i.iaba_lote_pulmao, i.iaba_tipo_analise from wms_item_abastecimento i " +
                                "inner join wms_abastecimento a " +
                                "on i.aba_codigo = a.aba_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = i.apa_codigo_picking " +
                                "inner join wms_apartamento ap1 " +
                                "on ap1.apa_codigo = i.apa_codigo_pulmao " +
                                "left join wms_estacao est " +
                                "on est.est_codigo = i.est_codigo " +
                                "left join wms_categoria c " +
                                "on c.cat_codigo = p.cat_codigo " +
                                "where a.aba_codigo = @codAbastecimento " +
                                "order by i.est_codigo, ap.apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItemMapaAbastecimento itens = new ItemMapaAbastecimento();

                    //Adiciona os valores encontrados
                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        itens.codAbastecimento = Convert.ToInt32(linha["aba_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.enderecoPicking = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        itens.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["abastecimento"] != DBNull.Value)
                    {
                        itens.qtdAbastecer = Convert.ToInt32(linha["abastecimento"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itens.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }


                    if (linha["apa_pulmao"] != DBNull.Value)
                    {
                        itens.enderecoPulmao = Convert.ToString(linha["apa_pulmao"]);
                    }

                    if (linha["estoque_Pulmao"] != DBNull.Value)
                    {
                        itens.qtdPulmao = Convert.ToInt32(linha["estoque_Pulmao"]);
                    }

                    if (linha["iaba_vencimento_pulmao"] != DBNull.Value)
                    {
                        itens.vencimentoPulmao = Convert.ToDateTime(linha["iaba_vencimento_pulmao"]);
                    }

                    if (linha["iaba_lote_pulmao"] != DBNull.Value)
                    {
                        itens.lotePulmao = Convert.ToString(linha["iaba_lote_pulmao"]);
                    }



                    //Adiciona o objêto a coleção
                    itensAbastecimentoCollection.Add(itens);
                }


                //Retorna a coleção de cadastro encontrada
                return itensAbastecimentoCollection;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as o itens gerados. \nDetalhes: " + ex.Message);
            }

        }

        //Pesquisa a ordem de abastecimento
        public MapaAbastecimentoCollection RelatorioAbastecimentoFlowRack(int codAbastecimento)
        {
            try
            {
                //Instância uma coleção de objêtos
                MapaAbastecimentoCollection abastecimentoCollection = new MapaAbastecimentoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);

                //String de consulta - Pesquisa o bastecimento
                string select = "select " +
                                "distinct(i.est_codigo) as est_codigo, a.aba_codigo, a.aba_modo, aba_tipo, " +
                                "count(i.est_codigo) as aba_item, " +
                                "(select sum(trunc(ii.iaba_qtd_abastecer/p1.prod_fator_pulmao)) from wms_item_abastecimento ii " +
                                "inner join wms_produto p1 " +
                                "on p1.prod_id = ii.prod_id " +
                                "where ii.aba_codigo = @codAbastecimento and ii.est_codigo = i.est_codigo) as aba_volume, " +
                                "sum(i.iaba_qtd_abastecer * b.bar_peso) as aba_peso, " +
                                "u.usu_login as aba_responsavel, u1.usu_login as aba_empilhador, u2.usu_login as aba_repositor, " +
                                "a.aba_data_inicial, a.aba_data_final, a.aba_status, " +
                                "(select co.conf_empresa from wms_configuracao co) as nome_empresa " +
                                "from wms_item_abastecimento i " +
                                "inner join wms_abastecimento a " +
                                "on a.aba_codigo = i.aba_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = a.usu_codigo_inicial " +
                                "left join wms_usuario u1 " +
                                "on u1.usu_codigo = a.usu_codigo_empilhador " +
                                "left join wms_usuario u2 " +
                                "on u2.usu_codigo = a.usu_codigo_empilhador " +
                                "left join wms_barra b " +
                                "on b.prod_id = i.prod_id " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "where a.aba_codigo = @codAbastecimento and b.bar_multiplicador = 1 " +
                                "group by i.est_codigo, a.aba_codigo, a.aba_modo, aba_tipo, u.usu_login, u1.usu_login, u2.usu_login, a.aba_data_inicial, a.aba_data_final, a.aba_status";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    MapaAbastecimento abastecimento = new MapaAbastecimento();
                    //Adiciona os valores encontrados

                    if (linha["nome_empresa"] != DBNull.Value)
                    {
                        abastecimento.nomeEmpresa = Convert.ToString(linha["nome_empresa"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        abastecimento.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        abastecimento.codAbastecimento = Convert.ToInt32(linha["aba_codigo"]);
                    }

                    if (linha["aba_tipo"] != DBNull.Value)
                    {
                        abastecimento.tipoAbastecimento = Convert.ToString(linha["aba_tipo"]).ToUpper();
                    }

                    if (linha["aba_modo"] != DBNull.Value)
                    {
                        abastecimento.modoAbastecimento = Convert.ToString(linha["aba_modo"]).ToUpper();
                    }

                    if (linha["aba_item"] != DBNull.Value)
                    {
                        abastecimento.totalItem = Convert.ToInt32(linha["aba_item"]);
                    }

                    if (linha["aba_volume"] != DBNull.Value)
                    {
                        abastecimento.totalVolumes = Convert.ToInt32(linha["aba_volume"]);
                    }

                    if (linha["aba_peso"] != DBNull.Value)
                    {
                        abastecimento.totalPeso = Convert.ToDouble(linha["aba_peso"]);
                    }

                    if (linha["aba_status"] != DBNull.Value)
                    {
                        abastecimento.statusAbastecimento = Convert.ToString(linha["aba_status"]);
                    }

                    if (linha["aba_data_inicial"] != DBNull.Value)
                    {
                        abastecimento.dataAbertura = Convert.ToDateTime(linha["aba_data_inicial"]);
                    }

                    if (linha["aba_responsavel"] != DBNull.Value)
                    {
                        abastecimento.loginResponsavel = Convert.ToString(linha["aba_responsavel"]);
                    }

                    if (linha["aba_empilhador"] != DBNull.Value)
                    {
                        abastecimento.loginEmpilhador = Convert.ToString(linha["aba_empilhador"]);
                    }

                    if (linha["aba_repositor"] != DBNull.Value)
                    {
                        abastecimento.loginRepositor = Convert.ToString(linha["aba_repositor"]);
                    }



                    //Adiciona os valores encontrados a coleção
                    abastecimentoCollection.Add(abastecimento);
                }

                //Retorna a coleção de cadastro encontrada
                return abastecimentoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as ordem de abastecimento. \nDetalhes:" + ex.Message);
            }

        }

        //Pesquisa os itens da ordem de abastecimento do picking
        public ItemMapaAbastecimentoCollection RelatorioItensAbastecimentoFlowrackGrandeza(int codAbastecimento)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItemMapaAbastecimentoCollection itensAbastecimentoCollection = new ItemMapaAbastecimentoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);

                //String de consulta - Pesquisa as ordem de abastecimento
                string select = "select a.aba_codigo, i.est_codigo, ap.apa_endereco as apa_separacao, prod_codigo, prod_descricao, " +
                                "i.iaba_qtd_abastecer / p.prod_fator_pulmao as abastecimento, u1.uni_unidade as uni_pulmao, " +
                                "ap1.apa_endereco, ap1.apa_tipo, i.iaba_vencimento_pulmao, iaba_lote_pulmao " +
                                "from wms_item_abastecimento i " +
                                "inner join wms_abastecimento a " +
                                "on i.aba_codigo = a.aba_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = i.apa_codigo_picking " +
                                "inner join wms_apartamento ap1 " +
                                "on ap1.apa_codigo = i.apa_codigo_pulmao " +
                                "where ap1.apa_tipo = 'Picking' and a.aba_codigo = @codAbastecimento " +
                                "or ap1.apa_tipo = 'Separacao' and a.aba_codigo = @codAbastecimento " +
                                "order by ap1.apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItemMapaAbastecimento itens = new ItemMapaAbastecimento();

                    //Adiciona os valores encontrados
                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        itens.codAbastecimento = Convert.ToInt32(linha["aba_codigo"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        itens.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["apa_separacao"] != DBNull.Value)
                    {
                        itens.enderecoPicking = Convert.ToString(linha["apa_separacao"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.enderecoPulmao = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["abastecimento"] != DBNull.Value)
                    {
                        itens.qtdAbastecer = Convert.ToInt32(linha["abastecimento"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itens.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["iaba_vencimento_pulmao"] != DBNull.Value)
                    {
                        itens.vencimentoPulmao = Convert.ToDateTime(linha["iaba_vencimento_pulmao"]);
                    }

                    if (linha["iaba_lote_pulmao"] != DBNull.Value)
                    {
                        itens.lotePulmao = Convert.ToString(linha["iaba_lote_pulmao"]);
                    }

                    //Adiciona o objêto a coleção
                    itensAbastecimentoCollection.Add(itens);
                }


                //Retorna a coleção de cadastro encontrada
                return itensAbastecimentoCollection;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as o itens gerados. \nDetalhes: " + ex.Message);
            }

        }

        //Pesquisa os itens da ordem de abastecimento do picking
        public ItemMapaAbastecimentoCollection RelatorioItensAbastecimentoFlowrackPulmao(int codAbastecimento)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItemMapaAbastecimentoCollection itensAbastecimentoCollection = new ItemMapaAbastecimentoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);

                //String de consulta - Pesquisa as ordem de abastecimento
                string select = "select a.aba_codigo, i.est_codigo, ap.apa_endereco as apa_separacao, prod_codigo, prod_descricao, " +
                                "i.iaba_qtd_abastecer / p.prod_fator_pulmao as abastecimento, u1.uni_unidade as uni_pulmao, " +
                                "ap1.apa_endereco, ap1.apa_tipo, i.iaba_vencimento_pulmao, iaba_lote_pulmao " +
                                "from wms_item_abastecimento i " +
                                "inner join wms_abastecimento a " +
                                "on i.aba_codigo = a.aba_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = i.apa_codigo_picking " +
                                "inner join wms_apartamento ap1 " +
                                "on ap1.apa_codigo = i.apa_codigo_pulmao " +
                                "where ap1.apa_tipo = 'Pulmao' and a.aba_codigo = @codAbastecimento " +
                                "order by ap1.apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItemMapaAbastecimento itens = new ItemMapaAbastecimento();

                    //Adiciona os valores encontrados
                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        itens.codAbastecimento = Convert.ToInt32(linha["aba_codigo"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        itens.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["apa_separacao"] != DBNull.Value)
                    {
                        itens.enderecoPicking = Convert.ToString(linha["apa_separacao"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.enderecoPulmao = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["abastecimento"] != DBNull.Value)
                    {
                        itens.qtdAbastecer = Convert.ToInt32(linha["abastecimento"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itens.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["iaba_vencimento_pulmao"] != DBNull.Value)
                    {
                        itens.vencimentoPulmao = Convert.ToDateTime(linha["iaba_vencimento_pulmao"]);
                    }

                    if (linha["iaba_lote_pulmao"] != DBNull.Value)
                    {
                        itens.lotePulmao = Convert.ToString(linha["iaba_lote_pulmao"]);
                    }

                    //Adiciona o objêto a coleção
                    itensAbastecimentoCollection.Add(itens);
                }


                //Retorna a coleção de cadastro encontrada
                return itensAbastecimentoCollection;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as o itens gerados. \nDetalhes: " + ex.Message);
            }

        }

    }
}
