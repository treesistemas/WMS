using System;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class DashFlowRackNegocios
    {
        //Instância o objeto
        Conexao conexao = new Conexao();

        //Pesquisa a quantidade de clientes
        public DashFlowRack PesqCliente(string data, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                DashFlowRack dash = new DashFlowRack();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa );

                //String de consulta
                string select = "select count(distinct(cli_id)) as dados from wms_pedido where ped_codigo in " +
                                "(select distinct(ped_codigo) from wms_rastreamento_flowrack r where r.iflow_criado between @dataInicial and @dataFinal) " +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return dash;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o total de clientes. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de pedidos
        public DashFlowRack PesqPedido(string data, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                DashFlowRack dash = new DashFlowRack();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select count(distinct(ped_codigo)) as dados from wms_rastreamento_flowrack r where r.iflow_criado between @dataInicial and @dataFinal and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return dash;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a quantidade de pedidos. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de pedidos
        public DashFlowRack PesqAbastecimento(string data, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                DashFlowRack dash = new DashFlowRack();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select count(s.prod_id) as dados from wms_separacao s " +
                                "inner join wms_produto p " +
                                "on s.prod_id = p.prod_id " +
                                "where sep_tipo = 'FLOWRACK' and p.prod_fator_pulmao* s.sep_abastecimento < s.sep_estoque " +
                                "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return dash;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a quantidade de itens para o abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de pedidos
        public DashFlowRack PesqMediaItens(string data, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                DashFlowRack dash = new DashFlowRack();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select " +
                                "(select count(prod_id) from wms_rastreamento_flowrack r where r.iflow_criado between @dataInicial and @dataFinal) / " +
                                "count(distinct(ped_codigo)) as dados " +
                                "from wms_rastreamento_flowrack r where r.iflow_criado between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return dash;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a média de itens. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de pedidos
        public DashFlowRackCollection PesqRendimentoConferente1(string data, string empresa)
        {
            try
            {
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select first 1 usu_login as dados, u.usu_foto as dados2, count(prod_id) as dados3 from wms_rastreamento_flowrack r "+
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo_conferente " +
                                "where r.iflow_criado between @dataInicial and @dataFinal " + //REMOVER O CÓDIGO DO USUARIO ERRO DO WMS DO CLIENTE
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" + 
                                "group by usu_login, usu_foto " +
                                "order by count(prod_id) desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.foto = (byte[])(linha["dados2"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["dados3"] != DBNull.Value)
                    {
                        dash.texto2 = Convert.ToString(linha["dados3"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o primeiro conferente. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de pedidos
        public DashFlowRackCollection PesqRendimentoConferente2(string data, string empresa)
        {
            try
            {
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select first 1 skip 1 usu_login as dados, u.usu_foto as dados2, count(prod_id) as dados3 from wms_rastreamento_flowrack r " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo_conferente " +
                                "where r.iflow_criado between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" + //REMOVER O CÓDIGO DO USUARIO ERRO DO WMS DO CLIENTE
                                "group by usu_login, usu_foto " +
                                "order by count(prod_id) desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.foto = (byte[])(linha["dados2"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["dados3"] != DBNull.Value)
                    {
                        dash.texto2 = Convert.ToString(linha["dados3"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o segundo conferente. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de pedidos
        public DashFlowRackCollection PesqRendimentoEnderecamento(string data, string empresa)
        {
            try
            {
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select first 1 count(distinct(r.iflow_barra)) as dados3, u.usu_login as dados, usu_foto as dados2 from wms_rastreamento_flowrack r "+
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo_apa " +
                                "where r.iflow_criado between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                                "group by usu_login, usu_foto " +
                                "order by count(distinct(r.iflow_barra)) desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.foto = (byte[])(linha["dados2"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["dados3"] != DBNull.Value)
                    {
                        dash.texto2 = Convert.ToString(linha["dados3"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar rendimento do endereçamento. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de pedidos
        public DashFlowRackCollection PesqConferenteMes(string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select first 1 usu_login as dados, u.usu_foto as dados2, count(prod_id) as dados3 from wms_rastreamento_flowrack r " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo_conferente " +
                                "where r.iflow_criado between @dataInicial and @dataFinal and r.usu_codigo_conferente <> 68 and r.usu_codigo_conferente <> 260 " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                                "group by usu_login, usu_foto " +
                                "order by count(prod_id) desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.foto = (byte[])(linha["dados2"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["dados3"] != DBNull.Value)
                    {
                        dash.texto2 = Convert.ToString(linha["dados3"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o primeiro conferente. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o ranking das estações
        public DashFlowRackCollection PesqProdutividadeEstacao(string data, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select e.est_codigo, e.est_descricao as dados1, count(r.prod_id) as dados2 from wms_rastreamento_flowrack r " +
                                "inner join wms_estacao e " +
                                "on e.est_codigo = r.est_codigo " +
                                "where r.iflow_criado between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                                "group by e.est_codigo, est_descricao " +
                                "order by e.est_codigo ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados1"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados1"]);
                    }

                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados2"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o ranking das estações. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o ranking das categorias
        public DashFlowRackCollection PesqRankingCategorias(string data, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select c.cat_descricao as dados1, count(r.prod_id) as dados2 from wms_rastreamento_flowrack r " +
                                "inner join wms_produto p " +
                                "on p.prod_id = r.prod_id " +
                                "inner join wms_categoria c " +
                                "on c.cat_codigo = p.cat_codigo " +
                                "where r.iflow_criado between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                                "group by cat_descricao " +
                                "order by count(r.prod_id) asc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados1"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados1"]);
                    }

                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados2"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o ranking das estações. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o ranking das categorias
        public DashFlowRackCollection PesqEquilibrioEstacao(string data, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select e.est_descricao as dados1,  est_tipo as dados2, count(s.est_codigo) as dados3 from wms_separacao s " +
                                "inner join wms_estacao e " +
                                "on e.est_codigo = s.est_codigo " +
                                "where not s.est_codigo is null and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "group by e.est_codigo, est_descricao, est_tipo order by e.est_codigo desc ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados1"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados1"]);
                    }

                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.texto2 = Convert.ToString(linha["dados2"]);
                    }

                    if (linha["dados3"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados3"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a quantidade de itens de cada estação. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de item por hora
        public DashFlowRackCollection PesqItemHora(string data, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select distinct(extract(hour from iflow_criado)) as dados1, count(prod_id) as dados2  from wms_rastreamento_flowrack r " +
                                "where r.iflow_criado between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                                "group by extract(hour from iflow_criado) order by extract(hour from iflow_criado)  desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados1"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados1"]);
                    }

                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados2"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens por hora. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o código de barra do produto (usado na confêrencia)
        public DashFlowRack PesqVolumes(string data, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                DashFlowRack dash = new DashFlowRack();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select count(distinct(r.iflow_barra)) as dados from wms_rastreamento_flowrack r where r.iflow_criado between @dataInicial and @dataFinal" +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return dash;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o total de volumes. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o código de barra do produto (usado na confêrencia)
        public DashFlowRack PesqVolumesAuditados(string data, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                DashFlowRack dash = new DashFlowRack();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@dataFinal", empresa);

                //String de consulta
                string select = "select count(distinct(iflow_barra)) as dados from wms_rastreamento_flowrack r "+
                                "where r.iflow_criado between @dataInicial and @dataFinal and iflow_audita = 'True' " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";



                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return dash;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os volumes auditados. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o código de barra do produto (usado na confêrencia)
        public DashFlowRack PesqVolumesEnderecados(string data, string empresa)
        {
            try
            {
                //Instância a camada de objêtos
                DashFlowRack dash = new DashFlowRack();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select count(distinct(r.iflow_barra)) as dados from wms_rastreamento_flowrack r " +
                    "where r.iflow_criado between @dataInicial and @dataFinal and not r.apa_codigo is null " +
                    "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["dados"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return dash;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os volumes endereçados. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o ranking das categorias
        public DashFlowRackCollection PesqRankingProdutos(string data, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                DashFlowRackCollection dashCollection = new DashFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", data + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", data + " 23:59:59");
                conexao.AdicionarParamentros("@dataFinal", empresa);

                //String de consulta
                string select = "select first 10 p.prod_descricao as dados1, sum(r.iflow_quantidade) as dados2, r.est_codigo as dados3 from wms_rastreamento_flowrack r " +
                                "inner join wms_produto p " +
                                "on p.prod_id = r.prod_id " +
                                "where r.iflow_criado between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                                "group by prod_descricao, est_codigo " +
                                "order by sum(r.iflow_quantidade) desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashFlowRack dash = new DashFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["dados1"] != DBNull.Value)
                    {
                        dash.texto = Convert.ToString(linha["dados1"]);
                    }

                    if (linha["dados2"] != DBNull.Value)
                    {
                        dash.valor = Convert.ToInt32(linha["dados2"]);
                    }

                    if (linha["dados3"] != DBNull.Value)
                    {
                        dash.texto2 = Convert.ToString(linha["dados3"]);
                    }

                    dashCollection.Add(dash);
                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os produtos top 10. \nDetalhes:" + ex.Message);
            }
        }

    }
}
