using System;
using System.Data;
using System.Reflection;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class VencimentoProdutoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o vencimento no pulmão
        public ConsultaEstoqueCollection PesqFornecedor(string codFornecedor, string dataIncial, string dataFinal)
        {
            try
            {
                //Instância a classe
                ConsultaEstoqueCollection consultaEstoqueCollection = new ConsultaEstoqueCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);

                /*Relatório de vencimento de produto Análitico*/
                string select = "select " +
                                "(select c.conf_empresa from wms_configuracao c where conf_codigo = 1) as nome_empresa, " +
                                "f.forn_codigo, forn_nome from wms_fornecedor f " +
                                "where f.forn_codigo = @codFornecedor ";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ConsultaEstoque produto = new ConsultaEstoque();
                    //Adiciona os valores encontrados
                    if (linha["nome_empresa"] != DBNull.Value)
                    {
                        produto.nomeEmpresa = Convert.ToString(linha["nome_empresa"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        produto.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);

                        produto.nomeFornecedor = Convert.ToString(linha["forn_codigo"]) + " - " + Convert.ToString(linha["forn_nome"]);

                        produto.descProduto = "TODOS";

                        produto.dataInicial = Convert.ToDateTime(dataIncial);

                        produto.dataFinal = Convert.ToDateTime(dataFinal);
                    }

                    consultaEstoqueCollection.Add(produto);
                }
                //Retorna a coleção de cadastro encontrada
                return consultaEstoqueCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar por fornecedor. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o vencimento no pulmão
        public ConsultaEstoqueCollection PesqProduto(string codProduto, string dataIncial, string dataFinal)
        {
            try
            {
                //Instância a classe
                ConsultaEstoqueCollection consultaEstoqueCollection = new ConsultaEstoqueCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codProduto", codProduto);

                /*Relatório de vencimento de produto Análitico*/
                string select = "select distinct(f.forn_codigo), forn_nome, p.prod_codigo, p.prod_descricao, " +
                                "(select c.conf_empresa from wms_configuracao c where conf_codigo = 1) as nome_empresa from wms_fornecedor f " +
                                "left join wms_produto p " +
                                "on p.forn_codigo = f.forn_codigo " +
                                "where prod_codigo = @codProduto";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ConsultaEstoque produto = new ConsultaEstoque();
                    //Adiciona os valores encontrados
                    if (linha["nome_empresa"] != DBNull.Value)
                    {
                        produto.nomeEmpresa = Convert.ToString(linha["nome_empresa"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        produto.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);

                        produto.nomeFornecedor = Convert.ToString(linha["forn_codigo"]) + " - " + Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);

                        produto.vencimento = DateTime.Now;

                        produto.dataInicial = Convert.ToDateTime(dataIncial);

                        produto.dataFinal = Convert.ToDateTime(dataFinal);
                    }

                    consultaEstoqueCollection.Add(produto);
                }
                //Retorna a coleção de cadastro encontrada
                return consultaEstoqueCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar por produto. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o vencimento do picking
        public ConsultaEstoqueCollection PesqVencimentoSintetico(string codFornecedor, string codProduto, string dataIncial, string dataFinal)
        {
            try
            {
                //Instância a coleção
                ConsultaEstoqueCollection produtoCollection = new ConsultaEstoqueCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                /*Relatório de vencimento de produto Análitico*/
                string select = "select prod_codigo, prod_descricao, prod_estoque, uni_unidade, prod_vencimento, prod_preco, forn_codigo, forn_nome from wms_venc_prod_sintetico " +
                                "where prod_vencimento between @dataInicial and @dataFinal " +
                                "order by forn_codigo, prod_vencimento, prod_estoque ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ConsultaEstoque enderecoPulmao = new ConsultaEstoque();
                    //Adiciona os valores encontrados

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.nomeFornecedor = Convert.ToString(linha["forn_codigo"]) +" - "+ Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        enderecoPulmao.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_estoque"] != DBNull.Value)
                    {
                        enderecoPulmao.estoque = Convert.ToDouble(linha["prod_estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        enderecoPulmao.unidadePicking = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["prod_vencimento"] != DBNull.Value)
                    {
                        enderecoPulmao.vencimento = Convert.ToDateTime(linha["prod_vencimento"]);
                    }

                    if (linha["prod_preco"] != DBNull.Value)
                    {
                        enderecoPulmao.preco = Convert.ToDouble(linha["prod_preco"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    produtoCollection.Add(enderecoPulmao);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os vencimentos no pulmão. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o vencimento do picking
        public ConsultaEstoqueCollection PesqVencimentoPickingSintetico(string codFornecedor, string codProduto, string dataIncial, string dataFinal)
        {
            try
            {
                //Instância a coleção
                ConsultaEstoqueCollection produtoCollection = new ConsultaEstoqueCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                /*Relatório de vencimento de produto Análitico*/
                string select = "select p.prod_codigo, p.prod_descricao, " +
                                "sum(s.sep_estoque) qtd_fracionada, " +
                                "u.uni_unidade, s.sep_validade, " +
                                "e.est_preco, sum(s.sep_estoque * e.est_preco) as est_preco_final " +
                                "from wms_separacao s " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "inner join wms_estoque e " +
                                "on e.prod_id = s.prod_id " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = p.forn_codigo " +
                                "where sep_estoque > 0 ";

                if (codFornecedor != string.Empty)
                {
                    select += " and f.forn_codigo = @codFornecedor and a.arm_vencimento between @dataInicial and @dataFinal ";
                }

                select += "order by sep_validade, p.prod_codigo, apa_ordem ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ConsultaEstoque enderecoPulmao = new ConsultaEstoque();
                    //Adiciona os valores encontrados
                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        enderecoPulmao.descEndereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["arm_qtd_caixa"] != DBNull.Value)
                    {
                        enderecoPulmao.estoque = Convert.ToDouble(linha["arm_qtd_caixa"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        enderecoPulmao.qtdCaixa = Convert.ToDouble(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        enderecoPulmao.tipoArmazenamento = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        enderecoPulmao.vencimento = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        enderecoPulmao.lote = Convert.ToString(linha["arm_lote"]);
                    }

                    if (linha["est_preco"] != DBNull.Value)
                    {
                        enderecoPulmao.preco = Convert.ToDouble(linha["est_preco"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    produtoCollection.Add(enderecoPulmao);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os vencimentos no pulmão. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o vencimento do picking
        public ConsultaEstoqueCollection PesqVencimentoPickingAnalitico(string codFornecedor, string codProduto, string dataIncial, string dataFinal)
        {
            try
            {
                //Instância a coleção
                ConsultaEstoqueCollection produtoCollection = new ConsultaEstoqueCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                /*Relatório de vencimento de produto Análitico*/
                string select = "select p.forn_codigo, ap.apa_endereco, p.prod_codigo, p.prod_descricao, " +
                                "(s.sep_estoque) as qtd_fracionado, u.uni_unidade, s.sep_validade, s.sep_lote, " +
                                "e.est_preco, (s.sep_estoque * e.est_preco) as est_preco_final " +
                                "from wms_separacao s " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "inner join wms_estoque e " +
                                "on e.prod_id = s.prod_id " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = s.apa_codigo " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = p.forn_codigo " +
                                "where sep_estoque > 0 ";

                if (codFornecedor != string.Empty)
                {
                    select += " and f.forn_codigo = @codFornecedor and s.sep_validade between @dataInicial and @dataFinal and not prod_controla_validade = 'False' ";
                }

                if (codProduto != string.Empty)
                {
                    select += " and p.prod_codigo = @codProduto and s.sep_validade between @dataInicial and @dataFinal and not prod_controla_validade = 'False' ";
                }

                if (codFornecedor == string.Empty && codProduto == string.Empty)
                {
                    select += " and s.sep_validade between @dataInicial and @dataFinal and not prod_controla_validade = 'False' ";
                }

                select += "order by sep_validade, p.prod_codigo, apa_ordem ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ConsultaEstoque enderecoPulmao = new ConsultaEstoque();
                    //Adiciona os valores encontrados
                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        enderecoPulmao.descEndereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtd_fracionado"] != DBNull.Value)
                    {
                        enderecoPulmao.estoque = Convert.ToDouble(linha["qtd_fracionado"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        enderecoPulmao.tipoArmazenamento = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        enderecoPulmao.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        enderecoPulmao.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    if (linha["est_preco"] != DBNull.Value)
                    {
                        enderecoPulmao.preco = Convert.ToDouble(linha["est_preco"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    produtoCollection.Add(enderecoPulmao);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os vencimentos no picking. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o vencimento no pulmão
        public ConsultaEstoqueCollection PesqVencimentoPulmaoAnalitico(string codFornecedor, string codProduto, string dataIncial, string dataFinal, string status)
        {
            try
            {
                //Instância a coleção
                ConsultaEstoqueCollection produtoCollection = new ConsultaEstoqueCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                /*Relatório de vencimento de produto Análitico*/
                string select = "select ap.apa_endereco, p.prod_codigo, f.forn_codigo, p.prod_descricao, " +
                                "(a.arm_quantidade/p.prod_fator_pulmao) arm_qtd_caixa, prod_fator_pulmao, u.uni_unidade, a.arm_vencimento, arm_lote, " +
                                "e.est_preco, (a.arm_quantidade * e.est_preco) as est_preco_final " +
                                "from wms_armazenagem a " +
                                "inner join wms_produto p " +
                                "on p.prod_id = a.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = uni_codigo_pulmao " +
                                "inner join wms_estoque e " +
                                "on e.prod_id = a.prod_id " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = a.apa_codigo " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = p.forn_codigo ";

                if (codFornecedor != string.Empty)
                {
                    select += "where  f.forn_codigo = @codFornecedor and a.arm_vencimento between @dataInicial and @dataFinal and not prod_controla_validade = 'False' ";// and not coalesce(arm_bloqueado, 'False') = 'True' ";
                }

                if (codProduto != string.Empty)
                {
                    select += "where p.prod_codigo = @codProduto and a.arm_vencimento between @dataInicial and @dataFinal and not prod_controla_validade = 'False' "; // and not coalesce(arm_bloqueado, 'False') = 'True' ";
                }

                if (codFornecedor == string.Empty && codProduto == string.Empty)
                {
                    select += "where a.arm_vencimento between @dataInicial and @dataFinal and not prod_controla_validade = 'False' ";/// and not coalesce(arm_bloqueado, 'False') = 'True' ";
                }

                if (!(status.Equals("") || status.Equals("TODOS")))
                {
                    select += "and arm_motivo_bloqueio = @status ";
                }

                select += "order by a.arm_vencimento, p.prod_codigo, apa_ordem ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ConsultaEstoque enderecoPulmao = new ConsultaEstoque();
                    //Adiciona os valores encontrados
                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        enderecoPulmao.descEndereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["arm_qtd_caixa"] != DBNull.Value)
                    {
                        enderecoPulmao.estoque = Convert.ToDouble(linha["arm_qtd_caixa"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        enderecoPulmao.qtdCaixa = Convert.ToDouble(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        enderecoPulmao.tipoArmazenamento = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        enderecoPulmao.vencimento = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        enderecoPulmao.lote = Convert.ToString(linha["arm_lote"]);
                    }

                    if (linha["est_preco"] != DBNull.Value)
                    {
                        enderecoPulmao.preco = Convert.ToDouble(linha["est_preco"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    produtoCollection.Add(enderecoPulmao);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os vencimentos no pulmão. \nDetalhes:" + ex.Message);
            }
        }


    }
}
