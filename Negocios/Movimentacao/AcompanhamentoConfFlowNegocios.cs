using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class AcompanhamentoConfFlowNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa os itens no rastreamento
        public ItensFlowRackCollection PesqItensRastreamento(int codPedido)
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
                string select = "select r.ped_codigo, e.est_descricao, r.iflow_numero, " +
                                /*Separação de flow rack*/
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = r.prod_id) as apa_endereco_flow, " +
                                /*Separação de caixa*/
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'CAIXA' and prod_id = r.prod_id) as apa_endereco_cxa, " +
                                "prod_codigo, prod_descricao, r.iflow_quantidade, r.iflow_qtd_conferida, r.iflow_corte, r.iflow_data_conferencia, usu_login " +
                                "from wms_rastreamento_flowrack r " +
                                "left join wms_item_flowrack i " +
                                "on i.iflow_codigo = r.iflow_codigo " +
                                "inner join wms_produto pd " +
                                "on pd.prod_id = r.prod_id " +
                                "inner join wms_estacao e " +
                                "on e.est_codigo = r.est_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo_conferente " +
                                "where r.ped_codigo = @codPedido " +
                                "order by iflow_data_conferencia ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    ItensFlowRack itensFlowRack = new ItensFlowRack();
                    //Adiciona os valores encontrados                   

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    /*if (linha["ped_data_envio_flow"] != DBNull.Value)
                    {
                        itensFlowRack.dataProcessamento = Convert.ToDateTime(linha["ped_data_envio_flow"]);
                    }*/

                    if (linha["apa_endereco_flow"] != DBNull.Value)
                    {
                        itensFlowRack.endereco = Convert.ToString(linha["apa_endereco_flow"]);
                    }
                    else if (linha["apa_endereco_cxa"] != DBNull.Value)
                    {
                        itensFlowRack.endereco = Convert.ToString(linha["apa_endereco_cxa"]);
                    }
                    else
                    {
                        itensFlowRack.endereco = "Sem Picking";
                    }

                    if (linha["iflow_numero"] != DBNull.Value)
                    {
                        itensFlowRack.numeroVolume = Convert.ToInt32(linha["iflow_numero"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iflow_quantidade"] != DBNull.Value)
                    {
                        itensFlowRack.qtdProduto = Convert.ToInt32(linha["iflow_quantidade"]);
                    }

                    if (linha["iflow_qtd_conferida"] != DBNull.Value)
                    {
                        itensFlowRack.qtdConferidaProduto = Convert.ToInt32(linha["iflow_qtd_conferida"]);
                    }

                    /*if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itensFlowRack.uniProduto = Convert.ToString(linha["uni_unidade"]);
                    }*/                    

                    if (linha["iflow_corte"] != DBNull.Value)
                    {
                        itensFlowRack.qtdCorteProduto = Convert.ToInt32(linha["iflow_corte"]);
                    }

                    if (linha["iflow_data_conferencia"] != DBNull.Value)
                    {
                        itensFlowRack.dataConferencia = Convert.ToDateTime(linha["iflow_data_conferencia"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itensFlowRack.nomeUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    //Adiciona o objêto a coleção
                    itensFlowRackCollection.Add(itensFlowRack);
                }
                //Retorna a coleção de cadastro encontrada
                return itensFlowRackCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o pedido. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens 
        public ItensFlowRackCollection PesqItensPedido(int codPedido)
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
                string select = "select ip.ped_codigo, ped_data_envio_flow, " +
                                /*Decrição da estação*/
                                "(select est_descricao from wms_estacao where est_codigo = s.est_codigo) as est_descricao, " +
                                /*Ordem de endereço por flow rack*/
                                "(select a.apa_ordem from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = ip.prod_id order by apa_ordem) as ordem_separacao, " +
                                /*Separação de flow rack*/
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = ip.prod_id) as apa_endereco_flow, " +
                                /*Separação de caixa*/
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'CAIXA' and prod_id = ip.prod_id) as apa_endereco_cxa, " +
                                /*Número de volume*/
                                "(select r.iflow_numero from wms_rastreamento_flowrack r where ped_codigo = pd.ped_codigo and prod_id = ip.prod_id) as numero_volume, " +
                                "p.prod_codigo, p.prod_descricao, mod(iped_quantidade, prod_fator_pulmao) as qtd_produto, " +
                                /*Quantidade conferida*/
                                "(select r.iflow_qtd_conferida from wms_rastreamento_flowrack r where ped_codigo = pd.ped_codigo and prod_id = ip.prod_id) as qtd_conferida, uni_unidade, " +
                                /*corte conferida*/
                                "(select r.iflow_corte from wms_rastreamento_flowrack r where ped_codigo = pd.ped_codigo and prod_id = ip.prod_id) as corte_conferida, uni_unidade, " +
                                /*Data conferida*/
                                "(select r.iflow_data_conferencia from wms_rastreamento_flowrack r where ped_codigo = pd.ped_codigo and prod_id = ip.prod_id) as data_conferencia " +
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

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    /*if (linha["ped_data_envio_flow"] != DBNull.Value)
                    {
                        itensFlowRack.dataProcessamento = Convert.ToDateTime(linha["ped_data_envio_flow"]);
                    }*/

                    if (linha["apa_endereco_flow"] != DBNull.Value)
                    {
                        itensFlowRack.endereco = Convert.ToString(linha["apa_endereco_flow"]);

                        itensFlowRack.tipoEndereco = "FLOWRACK";
                    }
                    else if (linha["apa_endereco_cxa"] != DBNull.Value)
                    {
                        itensFlowRack.endereco = Convert.ToString(linha["apa_endereco_cxa"]);
                        itensFlowRack.tipoEndereco = "CAIXA";
                    }
                    else
                    {
                        itensFlowRack.endereco = "Sem Picking";

                        itensFlowRack.tipoEndereco = string.Empty;
                    }

                   /* if (linha["iflow_numero"] != DBNull.Value)
                    {
                        itensFlowRack.numeroVolume = Convert.ToInt32(linha["iflow_numero"]);
                    }*/

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    /*if (linha["iflow_quantidade"] != DBNull.Value)
                    {
                        itensFlowRack.qtdProduto = Convert.ToInt32(linha["iflow_quantidade"]);
                    }*/

                    /*if (linha["iflow_qtd_conferida"] != DBNull.Value)
                    {
                        itensFlowRack.qtdConferidaProduto = Convert.ToInt32(linha["iflow_qtd_conferida"]);
                    }*/

                    /*if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itensFlowRack.uniProduto = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["iflow_corte"] != DBNull.Value)
                    {
                        itensFlowRack.qtdCorteProduto = Convert.ToInt32(linha["iflow_corte"]);
                    }

                    if (linha["iflow_data_conferencia"] != DBNull.Value)
                    {
                        itensFlowRack.dataConferencia = Convert.ToDateTime(linha["iflow_data_conferencia"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itensFlowRack.nomeUsuario = Convert.ToString(linha["usu_login"]);
                    }*/

                    //Adiciona o objêto a coleção
                    itensFlowRackCollection.Add(itensFlowRack);
                }
                //Retorna a coleção de cadastro encontrada
                return itensFlowRackCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o pedido. \nDetalhes:" + ex.Message);
            }
        }



    }
}
