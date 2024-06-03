using System;
using System.Data;
using Dados;
using ObjetoTransferencia;
using ObjetoTransferencia.Relatorio;

namespace Negocios
{
    public class ConsultarFlowRackNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa os itens do flow rack
        public ItensFlowRackCollection PesqItens(string codPedido)
        {
            try
            {
                //Instância a coleção
                ItensFlowRackCollection itensFlowRackCollection = new ItensFlowRackCollection();
                //Limpa o parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                //String de consulta
                string select = "select ped_data_envio_flow, " +
                                "(select est_descricao from wms_estacao where est_codigo = s.est_codigo) as est_descricao, " +
                                "(select a.apa_ordem from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = ip.prod_id order by apa_ordem) as ordem_separacao, " +
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = ip.prod_id) as endereco_flow, " +
                                "(select r.iflow_numero from wms_rastreamento_flowrack r where ped_codigo = pd.ped_codigo and prod_id = ip.prod_id) as numero_volume, " +
                                "p.prod_codigo, p.prod_descricao, " +
                                "(select r.iflow_qtd_conferida from wms_rastreamento_flowrack r where ped_codigo = pd.ped_codigo and prod_id = ip.prod_id) as qtd_conferida, uni_unidade, " +
                                "(select r.iflow_data_conferencia from wms_rastreamento_flowrack r where ped_codigo = pd.ped_codigo and prod_id = ip.prod_id) as data_conferencia, " +
                                "(select r.iflow_audita from wms_rastreamento_flowrack r where ped_codigo = pd.ped_codigo and prod_id = ip.prod_id) as auditado " +
                                "from wms_item_pedido ip " +
                                "inner join wms_produto p " +
                                "on p.prod_id = ip.prod_id " +
                                "inner join wms_pedido pd " +
                                "on pd.ped_codigo = ip.ped_codigo " +
                                "inner join wms_separacao s " +
                                "on s.prod_id = ip.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "where p.prod_separacao_flowrack = 'True' and s.sep_tipo = 'FLOWRACK' " +
                                "and ped_data_devolucao is null and pd.ped_codigo = @codPedido " +
                                "and mod(iped_quantidade, prod_fator_pulmao) > 0 " +
                                "order by s.est_codigo, ordem_separacao ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    ItensFlowRack itensFlowRack = new ItensFlowRack();
                    //Adiciona os valores encontrados
                    if (linha["ped_data_envio_flow"] != DBNull.Value)
                    {
                        itensFlowRack.dataProcessamento = Convert.ToDateTime(linha["ped_data_envio_flow"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["endereco_flow"] != DBNull.Value)
                    {
                        itensFlowRack.endereco = Convert.ToString(linha["endereco_flow"]);
                    }

                    if (linha["numero_volume"] != DBNull.Value)
                    {
                        itensFlowRack.numeroVolume = Convert.ToInt32(linha["numero_volume"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtd_conferida"] != DBNull.Value)
                    {
                        itensFlowRack.qtdConferidaProduto = Convert.ToInt32(linha["qtd_conferida"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itensFlowRack.uniProduto = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["data_conferencia"] != DBNull.Value)
                    {
                        itensFlowRack.dataConferencia = Convert.ToDateTime(linha["data_conferencia"]);
                    }

                    if (linha["auditado"] != DBNull.Value)
                    {
                        if (Convert.ToString(linha["auditado"]).Equals("False"))
                        {
                            itensFlowRack.auditaProduto = string.Empty;
                        }
                        else
                        {
                            itensFlowRack.auditaProduto = "SIM";
                        }
                    }

                    //Adiciona o objêto a coleção
                    itensFlowRackCollection.Add(itensFlowRack);
                }
                //Retorna a coleção de cadastro encontrada
                return itensFlowRackCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o itens do flow rack. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens do Flow rack para impressão
        public RelItemFlowRackCollection PesqItensFlowRack(int codPedido)
        {
            try
            {
                //Instância a coleção
                RelItemFlowRackCollection itensFlowRackCollection = new RelItemFlowRackCollection();
                //Limpa o parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao where conf_codigo = 1) as conf_empresa, " +
                                "c.cli_codigo, c.cli_nome, r.ped_codigo, iflow_numero, e.est_descricao, "+
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = r.prod_id) as endereco_flow, " +
                                "prod_codigo, prod_descricao, r.iflow_qtd_conferida, uu.uni_unidade, u.usu_login " +
                                "from wms_rastreamento_flowrack r " +
                                "inner join wms_estacao e " +
                                "on r.est_codigo = e.est_codigo " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = r.ped_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = r.prod_id " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo_conferente " +
                                "left join wms_unidade uu " +
                                "on uu.uni_codigo = pp.uni_codigo_picking " +
                                "where r.ped_codigo = @codPedido and iflow_qtd_conferida > 0 " +
                                "order by iflow_numero";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    RelItemFlowRack itensFlowRack = new RelItemFlowRack();
                    //Adiciona os valores encontrados
                    

                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        itensFlowRack.empresa = Convert.ToString(linha["conf_empresa"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.cliente = Convert.ToString(linha["cli_codigo"] +" - "+ linha["cli_nome"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.pedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["iflow_numero"] != DBNull.Value)
                    {
                        itensFlowRack.volume = Convert.ToString(linha["iflow_numero"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.estacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["endereco_flow"] != DBNull.Value)
                    {
                        itensFlowRack.endereco = Convert.ToString(linha["endereco_flow"]);
                    }                   

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.produto = Convert.ToString(linha["prod_codigo"] +" - "+ linha["prod_descricao"]);
                    }

                    if (linha["iflow_qtd_conferida"] != DBNull.Value)
                    {
                        itensFlowRack.quantidade = Convert.ToInt32(linha["iflow_qtd_conferida"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itensFlowRack.unidade = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itensFlowRack.conferente = Convert.ToString(linha["usu_login"]);
                    }

                    //Adiciona o objêto a coleção
                    itensFlowRackCollection.Add(itensFlowRack);
                }
                //Retorna a coleção de cadastro encontrada
                return itensFlowRackCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o itens do flow rack para impressão. \nDetalhes:" + ex.Message);
            }
        }

    }
}
