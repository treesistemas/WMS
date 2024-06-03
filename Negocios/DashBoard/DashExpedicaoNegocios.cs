using System;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios.DashBoard
{
    public class DashExpedicaoNegocios
    {
        //Instância o objeto
        Conexao conexao = new Conexao();

        //Pesquisa os pedidos
        public DashExpedicao PesqPedido(string dataIncial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataIncial.Replace("/","."));
                conexao.AdicionarParamentros("@dataFinal", dataFinal.Replace("/", "."));
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select " +
                                /*Quantidade de manifesto*/
                                "(select count(mani_codigo) from wms_manifesto m  " +
                                "where m.mani_data between @dataInicial and @dataFinal) as mani_quantidade, " +
                                /*Quantidade de pedido*/
                                "count(p.mani_codigo) as ped_quantidade, " +
                                /*Quantidade de pedido conferidos*/
                                "(select count(p.mani_codigo) from wms_pedido p " +
                                "inner join wms_manifesto m  " +
                                "on m.mani_codigo = p.mani_codigo  " +
                                "where not p.ped_fim_conferencia is null and m.mani_data between @dataInicial and @dataFinal) as ped_conferido,  " +
                                "sum(ped_peso) as ped_peso " +
                                "from wms_pedido p  " +
                                "inner join wms_manifesto m  " +
                                "on m.mani_codigo = p.mani_codigo  " +
                                "where m.mani_data between @dataInicial and @dataFinal and m.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Instância a camada de objêtos
                DashExpedicao dash = new DashExpedicao();
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["mani_quantidade"] != DBNull.Value)
                    {
                        dash.valor1 = Convert.ToInt32(linha["mani_quantidade"]);
                    }

                    if (linha["ped_quantidade"] != DBNull.Value)
                    {
                        dash.valor2 = Convert.ToInt32(linha["ped_quantidade"]);
                    }

                    if (linha["ped_conferido"] != DBNull.Value)
                    {
                        dash.valor3 = Convert.ToInt32(linha["ped_conferido"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        dash.valor4 = Convert.ToInt32(linha["ped_peso"]);
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

        //Pesquisa o ranking de empilhador
        public DashExpedicaoCollection PesqEmpilhador(string dataIncial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataIncial.Replace("/", "."));
                conexao.AdicionarParamentros("@dataFinal", dataFinal.Replace("/", "."));
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select u.usu_login, usu_foto, count(r.rast_codigo) as arm_tranferencia, " +
                                "sum(r.arm_quantidade_destino/p.prod_fator_pulmao) arm_volume  " +
                                "from wms_rastreamento_armazenagem r " +
                                "inner join wms_usuario u " +
                                "on r.usu_codigo = u.usu_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = r.prod_id " +
                                "where rast_operacao = 'TRANSFERÊNCIA' and rast_data between @dataInicial and @dataFinal " +
                                "group by usu_login, usu_foto  " +
                                "order by count(r.rast_codigo) asc";

                //Instância a camada de objêtos
                DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashExpedicao dash = new DashExpedicao();

                    //Adiciona os valores encontrados
                    if (linha["usu_login"] != DBNull.Value)
                    {
                        dash.texto1 = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["arm_tranferencia"] != DBNull.Value)
                    {
                        dash.valor1 = Convert.ToInt32(linha["arm_tranferencia"]);
                    }

                    if (linha["arm_volume"] != DBNull.Value)
                    {
                        dash.valor2 = Convert.ToInt32(linha["arm_volume"]);
                    }

                    if (linha["usu_foto"] != DBNull.Value)
                    {
                        dash.foto = (byte[])(linha["usu_foto"]);
                    }

                    dashCollection.Add(dash);

                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rancking de empilhador. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o ranking de conferencia
        public DashExpedicaoCollection PesqConferencia(string dataIncial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataIncial.Replace("/", "."));
                conexao.AdicionarParamentros("@dataFinal", dataFinal.Replace("/", "."));
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select u.usu_login, usu_foto, count(distinct(p.ped_codigo)) as ped_quantidade, count(distinct(iflow_barra)) + " +
                                "(select sum(iif(trunc(iped_quantidade/pp.prod_fator_pulmao) > 0 , 1, 0)) from wms_pedido pd " +
                                "inner join  wms_item_pedido i " +
                                "on pd.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto pp " +
                                "on i.prod_id = pp.prod_id " +
                                "inner join wms_manifesto mm " +
                                "on mm.mani_codigo = pd.mani_codigo " +
                                "where pd.usu_codigo_conferente = p.usu_codigo_conferente and mm.mani_data between @dataInicial and @dataFinal)  as ped_volume " +
                                "from wms_pedido p " +
                                "inner join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = p.usu_codigo_conferente " +
                                "left join wms_rastreamento_flowrack r " +
                                "on r.ped_codigo = p.ped_codigo " +
                                "where m.mani_data between @dataInicial and @dataFinal " +
                                "group by  p.usu_codigo_conferente, usu_login, usu_foto " +
                                "order by sum(ped_volume) desc";

                //Instância a camada de objêtos
                DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashExpedicao dash = new DashExpedicao();

                    //Adiciona os valores encontrados
                    if (linha["usu_login"] != DBNull.Value)
                    {
                        dash.texto1 = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["ped_quantidade"] != DBNull.Value)
                    {
                        dash.valor1 = Convert.ToInt32(linha["ped_quantidade"]);
                    }

                    if (linha["ped_volume"] != DBNull.Value)
                    {
                        dash.valor2 = Convert.ToInt32(linha["ped_volume"]);
                    }

                    if (linha["usu_foto"] != DBNull.Value)
                    {
                        dash.foto = (byte[])(linha["usu_foto"]);
                    }

                    dashCollection.Add(dash);

                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rancking de conferência. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o ranking da separacao
        public DashExpedicaoCollection PesqSeparacao(string dataIncial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataIncial.Replace("/", "."));
                conexao.AdicionarParamentros("@dataFinal", dataFinal.Replace("/", "."));
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select u.usu_login, usu_foto, count(distinct(p.ped_codigo)) as ped_quantidade, count(distinct(iflow_barra)) + "+
                                "(select sum(iif(trunc(iped_quantidade/pp.prod_fator_pulmao) > 0 , 1, 0)) from wms_pedido pd "+
                                "inner join  wms_item_pedido i " +
                                "on pd.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto pp " +
                                "on i.prod_id = pp.prod_id " +
                                "inner join wms_manifesto mm " +
                                "on mm.mani_codigo = pd.mani_codigo " +
                                "where pd.usu_codigo_separador = p.usu_codigo_separador and mm.mani_data between @dataInicial and @dataFinal)  as ped_volume " +
                                "from wms_pedido p " +
                                "inner join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = p.usu_codigo_separador " +
                                "left join wms_rastreamento_flowrack r " +
                                "on r.ped_codigo = p.ped_codigo " +
                                "where m.mani_data between @dataInicial and @dataFinal " +
                                "group by  p.usu_codigo_separador, usu_login, usu_foto " +
                                "order by sum(ped_volume) asc";

                //Instância a camada de objêtos
                DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashExpedicao dash = new DashExpedicao();

                    //Adiciona os valores encontrados
                    if (linha["usu_login"] != DBNull.Value)
                    {
                        dash.texto1 = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["ped_quantidade"] != DBNull.Value)
                    {
                        dash.valor1 = Convert.ToInt32(linha["ped_quantidade"]);
                    }

                    if (linha["ped_volume"] != DBNull.Value)
                    {
                        dash.valor2 = Convert.ToInt32(linha["ped_volume"]);
                    }

                    if (linha["usu_foto"] != DBNull.Value)
                    {
                        dash.foto = (byte[])(linha["usu_foto"]);
                    }

                    dashCollection.Add(dash);

                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rancking de conferência. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de itens conferidos por hora
        public DashExpedicaoCollection PesqItemHoraConferencia(string dataIncial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataIncial.Replace("/", "."));
                conexao.AdicionarParamentros("@dataFinal", dataFinal.Replace("/", "."));
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select distinct(extract(hour from iped_data_conferencia)) as hora_conferencia, count(prod_id) as prod_count " +
                                "from wms_item_pedido i " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "inner join wms_manifesto m  " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "where m.mani_data between @dataInicial and @dataFinal and not extract(hour from iped_data_conferencia) is null " +
                                "group by extract(hour from iped_data_conferencia) " +
                                "order by extract(hour from iped_data_conferencia) asc";

                //Instância a camada de objêtos
                DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashExpedicao dash = new DashExpedicao();

                    //Adiciona os valores encontrados
                    if (linha["hora_conferencia"] != DBNull.Value)
                    {
                        dash.texto1 = Convert.ToString(linha["hora_conferencia"]);
                    }

                    if (linha["prod_count"] != DBNull.Value)
                    {
                        dash.valor1 = Convert.ToInt32(linha["prod_count"]);
                    }

                    dashCollection.Add(dash);

                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rancking de conferência. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de itens separados por hora
        public DashExpedicaoCollection PesqItemHoraSeparacao(string dataIncial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataIncial.Replace("/", "."));
                conexao.AdicionarParamentros("@dataFinal", dataFinal.Replace("/", "."));
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select distinct(extract(hour from iped_data_separacao)) as hora_separacao, count(prod_id) as prod_count " +
                                "from wms_item_pedido i " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "inner join wms_manifesto m  " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "where m.mani_data between @dataInicial and @dataFinal and not extract(hour from iped_data_separacao) is null " +
                                "group by extract(hour from iped_data_separacao) " +
                                "order by extract(hour from iped_data_separacao) asc";

                //Instância a camada de objêtos
                DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashExpedicao dash = new DashExpedicao();

                    //Adiciona os valores encontrados
                    if (linha["hora_separacao"] != DBNull.Value)
                    {
                        dash.texto1 = Convert.ToString(linha["hora_separacao"]);
                    }

                    if (linha["prod_count"] != DBNull.Value)
                    {
                        dash.valor1 = Convert.ToInt32(linha["prod_count"]);
                    }

                    dashCollection.Add(dash);

                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rancking de separação. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a quantidade de itens abasteciso por hora
        public DashExpedicaoCollection PesqItemHoraAbastecimento(string dataIncial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataIncial.Replace("/", "."));
                conexao.AdicionarParamentros("@dataFinal", dataFinal.Replace("/", "."));
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select distinct(extract(hour from rast_data)) as rast_data, count(prod_id) as prod_count "+
                                "from wms_rastreamento_armazenagem r "+
                                "where rast_operacao = 'TRANSFERÊNCIA' and r.rast_data between @dataInicial and @dataFinal " +
                                "and not extract(hour from rast_data) is null " +
                                "group by extract(hour from rast_data) " +
                                "order by extract(hour from rast_data) asc";

                //Instância a camada de objêtos
                DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashExpedicao dash = new DashExpedicao();

                    //Adiciona os valores encontrados
                    if (linha["rast_data"] != DBNull.Value)
                    {
                        dash.texto1 = Convert.ToString(linha["rast_data"]);
                    }

                    if (linha["prod_count"] != DBNull.Value)
                    {
                        dash.valor1 = Convert.ToInt32(linha["prod_count"]);
                    }

                    dashCollection.Add(dash);

                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rancking de abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o ranking de categorias
        public DashExpedicaoCollection PesqRankingCategoria(string dataIncial, string dataFinal, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataIncial.Replace("/", "."));
                conexao.AdicionarParamentros("@dataFinal", dataFinal.Replace("/", "."));
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select first 5 cat_descricao, sum(i.iped_quantidade) as iped_quantidade from wms_item_pedido i "+
                                "inner join wms_pedido p "+
                                "on p.ped_codigo = i.ped_codigo "+
                                "inner join wms_manifesto m "+
                                "on m.mani_codigo = p.mani_codigo "+
                                "inner join wms_produto pp  "+
                                "on pp.prod_id = i.prod_id  "+
                                "inner join wms_categoria c  "+
                                "on c.cat_codigo = pp.cat_codigo  "+
                                "where m.mani_data between @dataInicial and @dataFinal " +
                                "group by cat_descricao "+
                                "order by sum(i.iped_quantidade) desc";

                //Instância a camada de objêtos
                DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêtos
                    DashExpedicao dash = new DashExpedicao();

                    //Adiciona os valores encontrados
                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        dash.texto1 = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["iped_quantidade"] != DBNull.Value)
                    {
                        dash.valor1 = Convert.ToInt32(linha["iped_quantidade"]);
                    }

                    dashCollection.Add(dash);

                }
                //Retorna a coleção de cadastro encontrada
                return dashCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rancking de categorias. \nDetalhes:" + ex.Message);
            }
        }


    }
}
