using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class AcompanhamentoEstacaoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        public AcompanhamentoEstacaoCollection PesqDados(string dataIncial, string dataFinal)
        {
            try
            {
                //Instância a coleção
                AcompanhamentoEstacaoCollection acompanhamentoCollection = new AcompanhamentoEstacaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //String de consulta
                string select = "select s.est_codigo, est_descricao, u.usu_login, count(s.prod_id) as cont_produto, " +
                                /*Pesquisa a quantidade de categorias da estação*/
                                "(select count(distinct(p.cat_codigo)) from wms_separacao s1 " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s1.prod_id " +
                                "where est_codigo = s.est_codigo) as cont_categoria, " +
                                /*Pesquisa a quantidade de itens no abastecimento */
                                "(select count(prod_id) from wms_item_abastecimento where est_codigo = s.est_codigo and iaba_status = 'True') as cont_abastecimento, " +
                                /*Pesquisa a quantidade de pedidos enviados para processamento*/
                                "(select count(distinct(p.ped_codigo)) from wms_item_pedido ip " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = ip.ped_codigo " +
                                "inner join wms_separacao s1 " +
                                "on s1.prod_id = ip.prod_id " +
                                "where s1.est_codigo = s.est_codigo and ped_data_envio_flow between @dataInicial and @dataFinal) as ped_envidos, " +
                                /*Pesquisa a quantidade de pedidos enviados para processamento e não conferido*/
                                "(select count(distinct(ped.ped_codigo)) from wms_item_pedido ip " +
                                "inner join wms_pedido ped " +
                                "on ped.ped_codigo = ip.ped_codigo " +
                                "inner join wms_separacao s1 " +
                                "on s1.prod_id = ip.prod_id " +
                                "where s1.est_codigo = s.est_codigo and not ped.ped_fim_flow_rack is null and ped_data_envio_flow between @dataInicial and @dataFinal) as ped_conferidos, " +
                                /*Pesquisa a quantidade de itens enviados para processamento*/
                                "(select count(ip.prod_id) from wms_item_pedido ip " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = ip.ped_codigo " +
                                "inner join wms_separacao s1 " +
                                "on s1.prod_id = ip.prod_id " +
                                "where s1.est_codigo = s.est_codigo and ped_data_envio_flow between @dataInicial and @dataFinal) as itens_enviados, " +
                                /*Pesquisa a quantidade de itens enviados para processamento e conferidos*/
                                "(select count(rt.prod_id) " +
                                "from wms_rastreamento_flowrack rt " +
                                "inner join wms_pedido ped " +
                                "on ped.ped_codigo = rt.ped_codigo " +
                                "where rt.est_codigo = s.est_codigo and ped_data_envio_flow between @dataInicial and @dataFinal) as itens_conferidos, " +
                                /*Pesquisa a quantidade de volumes processados*/
                                "(select count(distinct(rt.iflow_barra)) from wms_pedido ped " +
                                "inner join wms_rastreamento_flowrack rt " +
                                "on ped.ped_codigo = rt.ped_codigo " +
                                "where rt.est_codigo = s.est_codigo and ped_data_envio_flow between @dataInicial and @dataFinal) as cont_volumes, " +
                                /*Pesquisa a quantidade de falta na auditoria*/
                                "(select count(rt.prod_id) " +
                                "from wms_rastreamento_flowrack  rt " +
                                "inner join wms_pedido ped " +
                                "on ped.ped_codigo = rt.ped_codigo " +
                                "where rt.est_codigo = s.est_codigo and not rt.iflow_qtd_falta_auditoria is null " +
                                "and ped_data_envio_flow between @dataInicial and @dataFinal) as falta_auditoria, " +
                                /*Pesquisa a quantidade de sobra na auditoria*/
                                "(select count(rt.prod_id) " +
                                "from wms_rastreamento_flowrack  rt " +
                                "inner join wms_pedido ped " +
                                "on ped.ped_codigo = rt.ped_codigo " +
                                "where rt.est_codigo = s.est_codigo and not rt.iflow_qtd_sobra_auditoria is null " +
                                "and ped_data_envio_flow between @dataInicial and @dataFinal) as sobra_auditoria, " +
                                /*Pesquisa a quantidade de troca na auditoria*/
                                "(select count(rt.prod_id) " +
                                "from wms_rastreamento_flowrack  rt " +
                                "inner join wms_pedido ped " +
                                "on ped.ped_codigo = rt.ped_codigo " +
                                "where rt.est_codigo = s.est_codigo and not rt.iflow_qtd_troca_auditoria is null " +
                                "and ped_data_envio_flow between @dataInicial and @dataFinal) as troca_auditoria, " +
                                /*Pesquisa o pedido que está em conferência na estação*/
                                "(select max(distinct(if.ped_codigo)) " +
                                "from wms_item_flowrack  if " +
                                "inner join wms_pedido ped " +
                                "on ped.ped_codigo = if.ped_codigo " +
                                "where if.est_codigo = s.est_codigo and if.iflow_data_conferencia is null and not if.iflow_criado is null " +
                                "and ped_data_envio_flow between @dataInicial and @dataFinal) as pedido_em_conferencia " +
                                "from wms_separacao s " +
                                "inner join wms_estacao e " +
                                "on s.est_codigo = e.est_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = e.usu_codigo " +
                                "group by s.est_codigo, est_descricao, usu_login ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    AcompanhamentoEstacao acompanhamentoEstacao = new AcompanhamentoEstacao();
                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.usuEstacao = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["cont_produto"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.qtdProdutos = Convert.ToInt32(linha["cont_produto"]);
                    }

                    if (linha["cont_categoria"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.qtdCategoria = Convert.ToInt32(linha["cont_categoria"]);
                    }

                    if (linha["cont_abastecimento"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.emAbastecimento = Convert.ToInt32(linha["cont_abastecimento"]);
                    }

                    if (linha["ped_envidos"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.pedidosEnviados = Convert.ToInt32(linha["ped_envidos"]);
                    }

                    if (linha["ped_conferidos"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.pedidosConferidos = Convert.ToInt32(linha["ped_conferidos"]);
                    }

                    if (linha["itens_enviados"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.produtosEnviados = Convert.ToInt32(linha["itens_enviados"]);
                    }

                    if (linha["itens_conferidos"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.produtosConferidos = Convert.ToInt32(linha["itens_conferidos"]);
                    }

                    if (linha["cont_volumes"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.volumesConferidos = Convert.ToInt32(linha["cont_volumes"]);
                    }

                    if (linha["pedido_em_conferencia"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.pedidoEmConferencia = Convert.ToInt32(linha["pedido_em_conferencia"]);
                    }                    

                    if (linha["falta_auditoria"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.faltaAuditoria = Convert.ToInt32(linha["falta_auditoria"]);
                    }

                    if (linha["sobra_auditoria"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.sobraAuditoria = Convert.ToInt32(linha["sobra_auditoria"]);
                    }

                    if (linha["troca_auditoria"] != DBNull.Value)
                    {
                        acompanhamentoEstacao.trocaAuditoria = Convert.ToInt32(linha["troca_auditoria"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    acompanhamentoCollection.Add(acompanhamentoEstacao);
                }
                //Retorna a coleção de cadastro encontrada
                return acompanhamentoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os dados. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o tempo de conferência de cada estação
        public int PesqTempoConferencia(int codEstacao, string dataIncial, string dataFinal)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //String de consulta
                string select = "select rt.est_codigo, rt.ped_codigo, " +
                                "DATEDIFF( second FROM CAST(MIN(rt.iflow_data_conferencia) AS TIMESTAMP) TO " +
                                "CAST(MAX(rt.iflow_data_conferencia) AS TIMESTAMP)) as tempo_segundo " +
                                "from wms_rastreamento_flowrack rt " +
                                "inner join wms_pedido ped " +
                                "on rt.ped_codigo = ped.ped_codigo " +
                                "where est_codigo = @codEstacao and ped_data_envio_flow between @dataInicial and @dataFinal " +
                                "group by rt.est_codigo, rt.ped_codigo ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                int segundo = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["tempo_segundo"] != DBNull.Value)
                    {
                        segundo += Convert.ToInt32(linha["tempo_segundo"]);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return segundo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o tempo de conferência da estação. \nDetalhes:" + ex.Message);
            }
        }
    }



}

