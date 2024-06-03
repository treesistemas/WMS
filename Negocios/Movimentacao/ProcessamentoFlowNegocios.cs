using System;
using System.Data;
using System.Reflection;
using Dados;
using ObjetoTransferencia;
using ObjetoTransferencia.Relatorio;

namespace Negocios
{
    public class ProcessamentoFlowNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa pedidos para processamento
        public ProcessamentoFlowCollection PesqDataPedido(string manifesto, string pedido, int rota, string dataIncial, string dataFinal,
            bool naoProcessados, bool processados, bool emProcesso, bool auditoria, bool emConferencia, bool enderecamento, string empresa)
        {
            try
            {
                //Instância a coleção
                ProcessamentoFlowCollection pedidoProcFlowCollection = new ProcessamentoFlowCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@pedido", pedido);
                conexao.AdicionarParamentros("@manifesto", manifesto);
                conexao.AdicionarParamentros("@rota", rota);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select ped_data, pd.ped_codigo, (select iconf_valor from wms_itens_configuracao where iconf_descricao = 'PROCESSAR PEDIDOS NO FLOW RACK COM VALOR ACIMA DE?') as ped_minimo," +
                    "pd.ped_total, t.tipo_descricao, r.rota_numero, mani_codigo, ped_data_envio_flow, " +
                    "min(e.est_codigo) est_inicial, max(e.est_codigo) est_final, ped_inicio_flow_rack, ped_fim_flow_rack, usu_login, " +
                    /*Pesquisa qtd de itens*/
                    "(select count(ii.prod_id) from wms_item_pedido ii inner join wms_produto pp on ii.prod_id = pp.prod_id where ped_codigo = i.ped_codigo and mod(iped_quantidade, prod_fator_pulmao) > 0) as ped_itens, " +
                    /*Pesquisa a estação atual de conferência*/
                    "(select max(est_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo) as est_atual, " +
                    /*Pesquisa a quantidade de volumes produzidos*/
                    "(select max(iflow_numero) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo) as ped_volume, " +
                    //Pesquisa os itens a serem auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' and iflow_qtd_conferida > 0) as itens_auditar, " +
                    //Pesquisa os itens auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                    "and not v.iflow_data_auditoria is null and iflow_qtd_conferida > 0) as itens_auditado, " +
                    //Pesquisa se o endereço foi endereçado
                    "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and apa_codigo is null and iflow_qtd_conferida > 0) as volume_enderecado, " +
                    //Modificado = Pesquisa os produtos proximo ao vencimento
                    "pd.ped_tabela, "+
                    "(select coalesce(cast(iconf_valor as integer), 0) from wms_itens_configuracao where iconf_descricao = 'SEPARAR PEDIDO POR PRODUTOS PRÓXIMO AO VENCIMENTO I' and iconf_status = 'True') as venc_i, " +
                    "(select coalesce(cast(iconf_valor as integer), 0) from wms_itens_configuracao where iconf_descricao = 'SEPARAR PEDIDO POR PRODUTOS PRÓXIMO AO VENCIMENTO II' and iconf_status = 'True') as venc_ii, " +
                    "(select coalesce(cast(iconf_valor as integer), 0) from wms_itens_configuracao where iconf_descricao = 'SEPARAR PEDIDO POR PRODUTOS PRÓXIMO AO VENCIMENTO III' and iconf_status = 'True') as venc_iii " +

                    "from wms_item_pedido i " +
                    "inner join wms_produto p " +
                    "on p.prod_id = i.prod_id " +
                    "inner join wms_separacao s " +
                    "on s.prod_id = p.prod_id " +
                    "inner join wms_estacao e " +
                    "on e.est_codigo = s.est_codigo " +
                    "inner join wms_pedido pd " +
                    "on pd.ped_codigo = i.ped_codigo " +
                    "inner join wms_cliente c " +
                    "on c.cli_id = pd.cli_id " +
                    "left join wms_rota r " +
                    "on r.rota_codigo = c.rota_codigo " +
                    "left join wms_usuario uu " +
                    "on uu.usu_codigo = pd.usu_codigo_flowrack " +
                    "inner join wms_tipo_pedido t " +
                    "on t.tipo_codigo = pd.tipo_codigo " +
                    "where ped_status = 'IMPORTACAO COMPLETA'  and mod(iped_quantidade, prod_fator_pulmao) > 0 " +
                    "and prod_separacao_flowrack = 'True' and ped_bloqueado = 'False' and ped_excluido is null and ped_data_devolucao is null " +
                    "and pd.cfop_codigo <> 13 " +
                    "and not pd.tipo_codigo in (select tipo_codigo from wms_tipo_pedido v " +
                    "where tipo_descricao like '%DEVOLUCAO%' or tipo_descricao like '%TROCA%' " +
                    "or tipo_descricao like '%AVARIA%' or tipo_descricao like '%BAIXA%' " +
                    "or tipo_descricao like '%SUPERMERCADO%' or tipo_descricao like '%TRANSFERENCIA%' and not tipo_descricao like '%TRANSFERENCIA SHOWROOM%' " +
                    "or tipo_descricao like '%DEV%' or tipo_descricao like '%COMPRAS%') ";
                /*and ped_fim_conferencia is null*/
                //Verifica o status - Não Processados
                if (naoProcessados == true)
                {
                    select += "and ped_data_envio_flow is null ";
                }
                //Prcessados (enviado, conferido, auditado e endereçado) = Finalizado
                else if (processados == true)
                {
                    select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                        "and " +
                        //Pesquisa os itens a serem auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) = " +
                        //Pesquisa os itens auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                        "and not v.iflow_data_auditoria is null) " +
                        "and " +
                        //Pesquisa se os volumes estão endereçados
                        "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and apa_codigo is null) = 0 ";
                }
                else if (emProcesso == true)
                {
                    if (emConferencia == false && auditoria == false && enderecamento == false)
                    {
                        //Somente os enviado
                        select += "and not ped_data_envio_flow is null and ped_inicio_flow_rack is null ";
                    }

                    //Status de conferência
                    if (emConferencia == true)
                    {
                        select += "and not ped_data_envio_flow is null and not ped_inicio_flow_rack is null and ped_fim_flow_rack is null ";
                    }
                    //Status de auditoria
                    else if (auditoria == true)
                    {
                        select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                        "and " +
                        //Pesquisa os itens a serem auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) != " +
                        //Pesquisa os itens auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                        "and not v.iflow_data_auditoria is null) ";
                    }
                    //Status de endereçamento
                    else if (enderecamento == true)
                    {
                        select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                        "and " +
                        //Pesquisa os itens a serem auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) = " +
                        //Pesquisa os itens auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                        "and not v.iflow_data_auditoria is null) " +
                        "and " +
                        //Pesquisa se os volumes estão endereçados
                        "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and apa_codigo is null) > 0 ";
                    }
                }

                if (rota > 0)
                {
                    select += "and r.rota_numero = @rota ";
                }

                if (!manifesto.Equals(""))
                {
                    select += "and pd.mani_codigo = @manifesto ";
                }
                else if (!pedido.Equals(""))
                {
                    select += "and pd.ped_codigo = @pedido ";
                }
                else
                {
                    select += "and ped_data between @dataInicial and @dataFinal ";
                }

                 select += "and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";// Alteração


                //Verifica os pedidos que tem fracionado
                select += "group by ped_data, pd.ped_codigo, pd.ped_total, t.tipo_descricao, mani_codigo, r.rota_numero, ped_data_envio_flow, usu_login, " +
                          "ped_inicio_flow_rack, ped_fim_flow_rack, i.ped_codigo, ped_tabela " +
                          "order by rota_numero, ped_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ProcessamentoFlow pedidoProcFlow = new ProcessamentoFlow();
                    //Adiciona os valores encontrados
                    if (linha["ped_data"] != DBNull.Value)
                    {
                        pedidoProcFlow.dataPedido = Convert.ToDateTime(linha["ped_data"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedidoProcFlow.codPedido = Convert.ToInt64(linha["ped_codigo"]);
                    }

                    if (linha["ped_itens"] != DBNull.Value)
                    {
                        pedidoProcFlow.itensPedido = Convert.ToInt32(linha["ped_itens"]);
                    }

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        pedidoProcFlow.rotaPedido = Convert.ToInt32(linha["rota_numero"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        pedidoProcFlow.manifestoPedido = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["ped_data_envio_flow"] != DBNull.Value)
                    {
                        pedidoProcFlow.dataEnvioProcessamento = Convert.ToDateTime(linha["ped_data_envio_flow"]);
                    }

                    if (linha["est_inicial"] != DBNull.Value)
                    {
                        pedidoProcFlow.estInicial = Convert.ToInt32(linha["est_inicial"]);
                    }

                    if (linha["est_final"] != DBNull.Value)
                    {
                        pedidoProcFlow.estFinal = Convert.ToInt32(linha["est_final"]);
                    }

                    if (linha["est_atual"] != DBNull.Value)
                    {
                        pedidoProcFlow.estAtual = "Estação: " + String.Format("{0:00}", linha["est_atual"]);
                    }

                    if (linha["ped_volume"] != DBNull.Value)
                    {
                        pedidoProcFlow.volumePedido = Convert.ToInt32(linha["ped_volume"]);
                    }

                    if (linha["ped_inicio_flow_rack"] != DBNull.Value)
                    {
                        pedidoProcFlow.dataInicialProcessamento = Convert.ToDateTime(linha["ped_inicio_flow_rack"]);
                    }

                    if (linha["ped_fim_flow_rack"] != DBNull.Value)
                    {
                        pedidoProcFlow.dataFinalProcessamento = Convert.ToDateTime(linha["ped_fim_flow_rack"]);

                        //Calcula o tempo de conferência
                        if (linha["ped_inicio_flow_rack"] != DBNull.Value && linha["ped_fim_flow_rack"] != DBNull.Value)
                        {
                            TimeSpan r = Convert.ToDateTime(linha["ped_fim_flow_rack"]).Subtract(Convert.ToDateTime(linha["ped_inicio_flow_rack"]));

                            string tempo = r.ToString(@"d\.hh\:mm\:ss");

                            pedidoProcFlow.tempoProcessamento = tempo;
                        }
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        pedidoProcFlow.usuInicioFlowRack = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["itens_auditar"] != DBNull.Value)
                    {
                        pedidoProcFlow.itensAuditar = Convert.ToInt32(linha["itens_auditar"]);
                    }

                    if (linha["itens_auditado"] != DBNull.Value)
                    {
                        pedidoProcFlow.itensAuditado = Convert.ToInt32(linha["itens_auditado"]);
                    }

                    //Volume endereçado = 0
                    if (linha["volume_enderecado"] != DBNull.Value)
                    {
                        pedidoProcFlow.volumeEnderecado = Convert.ToInt32(linha["volume_enderecado"]);
                    }

                    if (Convert.ToString(linha["tipo_descricao"]).Contains("VENDA") && Convert.ToInt32(linha["ped_total"]) >= Convert.ToInt32(linha["ped_minimo"]) 
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_i"])
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_ii"])
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_iii"]))
                    {
                        //Adiciona os cadastros encontrados a coleção
                        pedidoProcFlowCollection.Add(pedidoProcFlow);
                    }
                    else if (Convert.ToString(linha["tipo_descricao"]).Contains("BONIFICACAO")
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_i"])
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_ii"])
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_iii"]))
                    {
                        //Adiciona os cadastros encontrados a coleção
                        pedidoProcFlowCollection.Add(pedidoProcFlow);
                    }
                    else if (Convert.ToString(linha["tipo_descricao"]).Contains("TRANSFERENCIA")
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_i"])
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_ii"])
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_iii"]))
                    {
                        //Adiciona os cadastros encontrados a coleção
                        pedidoProcFlowCollection.Add(pedidoProcFlow);
                    }
                    else if (Convert.ToString(linha["tipo_descricao"]).Contains("CONSUMO")
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_i"])
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_ii"])
                        && Convert.ToString(linha["ped_tabela"]) != Convert.ToString(linha["venc_iii"]))
                    {
                        //Adiciona os cadastros encontrados a coleção
                        pedidoProcFlowCollection.Add(pedidoProcFlow);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return pedidoProcFlowCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os pedidos. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os pedidos processados
        public ProcessamentoFlowCollection PesqDataProcessamentoPedido(string manifesto, string pedido, int[] rota, string dataIncial, string dataFinal,
            bool naoProcessados, bool processados, bool emProcesso, bool auditoria, bool emConferencia, bool enderecamento, string empresa)
        {
            try
            {
                //Instância a coleção
                ProcessamentoFlowCollection pedidoProcFlowCollection = new ProcessamentoFlowCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@pedido", pedido);
                conexao.AdicionarParamentros("@manifesto", manifesto);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select  ped_data, pd.ped_codigo, r.rota_numero, mani_codigo, ped_data_envio_flow, " +
                                "ped_inicio_flow_rack, ped_fim_flow_rack, usu_login, " +
                                "(select count(ii.prod_id) from wms_item_pedido ii " +
                                "inner join wms_produto pp " +
                                "on ii.prod_id = pp.prod_id " +
                                "where ped_codigo = pd.ped_codigo " +
                                "and mod(iped_quantidade, prod_fator_pulmao) > 0) as ped_itens, " +

                                "(select min(est_codigo) from wms_item_flowrack where ped_codigo = pd.ped_codigo) as est_inicial, " +
                                "(select max(est_codigo) from wms_item_flowrack where ped_codigo = pd.ped_codigo) as est_final, " +
                                "(select max(est_codigo) from wms_item_flowrack where ped_codigo = pd.ped_codigo and not iflow_data_conferencia is null ) as est_atual, " +


                                "(select min(est_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo) as est_inicial_rastreamento, " +
                                "(select max(est_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo) as est_final_rastreamento, " +
                                "(select max(est_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and not iflow_data_conferencia is null ) as est_atual_rastreamento, " +
                                "(select max(iflow_numero) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and iflow_qtd_conferida > 0) as ped_volume, " +
                                "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and iflow_audita = 'True' and iflow_qtd_conferida > 0) as itens_auditar, " +
                                "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and iflow_audita = 'True' and not v.iflow_data_auditoria is null and iflow_qtd_conferida > 0) as itens_auditado, " +
                                "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and apa_codigo is null and iflow_qtd_conferida > 0) as volume_enderecado " +
                                "from wms_pedido pd " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = pd.cli_id " +
                                "left join wms_rota r " +
                                "on r.rota_codigo = c.rota_codigo " +
                                "left join wms_usuario uu " +
                                "on uu.usu_codigo = pd.usu_codigo_flowrack " +
                                "where ped_data_envio_flow between @dataInicial and @dataFinal " +
                                "and pd.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";


                //Não Processados
                if (naoProcessados == true)
                {
                    select += "and ped_data_envio_flow is null ";
                }
                //Prcessados (enviado, conferido, auditado e endereçado) = Finalizado
                else if (processados == true)
                {
                    select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                        "and " +
                        //Pesquisa os itens a serem auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and iflow_audita = 'True' ) = " +
                        //Pesquisa os itens auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and iflow_audita = 'True' " +
                        "and not v.iflow_data_auditoria is null) " +
                        "and " +
                        //Pesquisa se os volumes estão endereçados
                        "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and apa_codigo is null) = 0 ";
                }
                else if (emProcesso == true)
                {
                    if (emConferencia == false && auditoria == false && enderecamento == false)
                    {
                        //Somente os enviado
                        select += "and not ped_data_envio_flow is null and ped_inicio_flow_rack is null ";
                    }

                    //Status de conferência
                    if (emConferencia == true)
                    {
                        select += "and not ped_data_envio_flow is null and not ped_inicio_flow_rack is null and ped_fim_flow_rack is null ";
                    }
                    //Status de auditoria
                    else if (auditoria == true)
                    {
                        select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                        "and " +
                        //Pesquisa os itens a serem auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and iflow_audita = 'True' ) != " +
                        //Pesquisa os itens auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and iflow_audita = 'True' " +
                        "and not v.iflow_data_auditoria is null) ";
                    }
                    //Status de endereçamento
                    else if (enderecamento == true)
                    {
                        select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                        "and " +
                        //Pesquisa os itens a serem auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and iflow_audita = 'True' ) = " +
                        //Pesquisa os itens auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and iflow_audita = 'True' " +
                        "and not v.iflow_data_auditoria is null) " +
                        "and " +
                        //Pesquisa se os volumes estão endereçados
                        "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and apa_codigo is null) > 0 ";
                    }
                }

                if (!manifesto.Equals(""))
                {
                    select += "and pd.mani_codigo = @manifesto ";
                }
                else if (!pedido.Equals(""))
                {
                    select += "and pd.ped_codigo = @pedido ";
                }
                else if (rota != null && rota.Length > 0)
                {

                    select += "and r.rota_numero = " + rota[0] + " ";

                    for (int i = 1; rota.Length > i; i++)
                    {
                        if (rota[i] != 0)
                        {
                            select += "or ped_data_envio_flow between @dataInicial and @dataFinal and r.rota_numero = " + rota[i] + " ";

                            //Não Processados
                            if (naoProcessados == true)
                            {
                                select += "and ped_data_envio_flow is null ";
                            }
                            //Prcessados (enviado, conferido, auditado e endereçado) = Finalizado
                            else if (processados == true)
                            {
                                select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                                    "and " +
                                    //Pesquisa os itens a serem auditados
                                    "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and iflow_audita = 'True' ) = " +
                                    //Pesquisa os itens auditados
                                    "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and iflow_audita = 'True' " +
                                    "and not v.iflow_data_auditoria is null) " +
                                    "and " +
                                    //Pesquisa se os volumes estão endereçados
                                    "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and apa_codigo is null) = 0 ";
                            }
                            else if (emProcesso == true)
                            {
                                if (emConferencia == false && auditoria == false && enderecamento == false)
                                {
                                    //Somente os enviado
                                    select += "and not ped_data_envio_flow is null and ped_inicio_flow_rack is null ";
                                }

                                //Status de conferência
                                if (emConferencia == true)
                                {
                                    select += "and not ped_data_envio_flow is null and not ped_inicio_flow_rack is null and ped_fim_flow_rack is null ";
                                }
                                //Status de auditoria
                                else if (auditoria == true)
                                {
                                    select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                                    "and " +
                                    //Pesquisa os itens a serem auditados
                                    "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and iflow_audita = 'True' ) != " +
                                    //Pesquisa os itens auditados
                                    "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and iflow_audita = 'True' " +
                                    "and not v.iflow_data_auditoria is null) ";
                                }
                                //Status de endereçamento
                                else if (enderecamento == true)
                                {
                                    select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                                    "and " +
                                    //Pesquisa os itens a serem auditados
                                    "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = pd.ped_codigo and iflow_audita = 'True' ) = " +
                                    //Pesquisa os itens auditados
                                    "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and iflow_audita = 'True' " +
                                    "and not v.iflow_data_auditoria is null) " +
                                    "and " +
                                    //Pesquisa se os volumes estão endereçados
                                    "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = pd.ped_codigo and apa_codigo is null) > 0 ";
                                }
                            }
                        }
                    }
                }


                //Verifica os pedidos que tem fracionado
                select += "group by ped_data, pd.ped_codigo, mani_codigo, r.rota_numero, ped_data_envio_flow, " +
                          "ped_inicio_flow_rack, ped_fim_flow_rack, usu_login, pd.ped_codigo " +
                          "order by rota_numero, ped_data";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ProcessamentoFlow pedidoProcFlow = new ProcessamentoFlow();
                    //Adiciona os valores encontrados
                    if (linha["ped_data"] != DBNull.Value)
                    {
                        pedidoProcFlow.dataPedido = Convert.ToDateTime(linha["ped_data"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedidoProcFlow.codPedido = Convert.ToInt64(linha["ped_codigo"]);
                    }

                    if (linha["ped_itens"] != DBNull.Value)
                    {
                        pedidoProcFlow.itensPedido = Convert.ToInt32(linha["ped_itens"]);
                    }

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        pedidoProcFlow.rotaPedido = Convert.ToInt32(linha["rota_numero"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        pedidoProcFlow.manifestoPedido = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["ped_data_envio_flow"] != DBNull.Value)
                    {
                        pedidoProcFlow.dataEnvioProcessamento = Convert.ToDateTime(linha["ped_data_envio_flow"]);
                    }


                    if (linha["est_inicial_rastreamento"] != DBNull.Value)
                    {
                        pedidoProcFlow.estInicial = Convert.ToInt32(linha["est_inicial_rastreamento"]);
                    }
                    else if (linha["est_inicial"] != DBNull.Value)
                    {
                        pedidoProcFlow.estInicial = Convert.ToInt32(linha["est_inicial"]);
                    }

                    if (linha["est_final_rastreamento"] != DBNull.Value)
                    {
                        pedidoProcFlow.estFinal = Convert.ToInt32(linha["est_final_rastreamento"]);
                    }

                    if (linha["est_final"] != DBNull.Value)
                    {
                        pedidoProcFlow.estFinal = Convert.ToInt32(linha["est_final"]);
                    }
                    else if (linha["est_final_rastreamento"] != DBNull.Value)
                    {
                        pedidoProcFlow.estFinal = Convert.ToInt32(linha["est_final_rastreamento"]);
                    }

                    if (linha["est_atual"] != DBNull.Value)
                    {
                        pedidoProcFlow.estAtual = "Estação: " + String.Format("{0:00}", linha["est_atual"]);
                    }

                    if (linha["ped_volume"] != DBNull.Value)
                    {
                        pedidoProcFlow.volumePedido = Convert.ToInt32(linha["ped_volume"]);
                    }

                    if (linha["ped_inicio_flow_rack"] != DBNull.Value)
                    {
                        pedidoProcFlow.dataInicialProcessamento = Convert.ToDateTime(linha["ped_inicio_flow_rack"]);
                    }

                    if (linha["ped_fim_flow_rack"] != DBNull.Value)
                    {
                        pedidoProcFlow.dataFinalProcessamento = Convert.ToDateTime(linha["ped_fim_flow_rack"]);

                        //Calcula o tempo de conferência
                        if (linha["ped_inicio_flow_rack"] != DBNull.Value && linha["ped_fim_flow_rack"] != DBNull.Value)
                        {
                            TimeSpan r = Convert.ToDateTime(linha["ped_fim_flow_rack"]).Subtract(Convert.ToDateTime(linha["ped_inicio_flow_rack"]));

                            string tempo = r.ToString(@"d\.hh\:mm\:ss");

                            pedidoProcFlow.tempoProcessamento = tempo;
                        }
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        pedidoProcFlow.usuInicioFlowRack = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["itens_auditar"] != DBNull.Value)
                    {
                        pedidoProcFlow.itensAuditar = Convert.ToInt32(linha["itens_auditar"]);
                    }

                    if (linha["itens_auditado"] != DBNull.Value)
                    {
                        pedidoProcFlow.itensAuditado = Convert.ToInt32(linha["itens_auditado"]);
                    }

                    //Volume endereçado = 0
                    if (linha["volume_enderecado"] != DBNull.Value)
                    {
                        pedidoProcFlow.volumeEnderecado = Convert.ToInt32(linha["volume_enderecado"]);
                    }





                    //Adiciona os cadastros encontrados a coleção
                    pedidoProcFlowCollection.Add(pedidoProcFlow);
                }
                //Retorna a coleção de cadastro encontrada
                return pedidoProcFlowCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os pedidos processados. \nDetalhes:" + ex.Message);
            }
        }

        //Insere os itens dos pedidos nos volumes de Flow rack
        public void ProcessarPedidos(int codPedido, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codPedido", codPedido);


                //Insere os itens
                string insert = "insert into wms_item_flowrack (iflow_codigo, ped_codigo, iflow_numero, iflow_barra, est_codigo, prod_id, iflow_quantidade, iflow_audita, conf_codigo) " +
                                "select gen_id(gen_wms_flowrack, 1), ip.ped_codigo, '01', ip.ped_codigo || '01', s.est_codigo, ip.prod_id, " +
                                "mod(iped_quantidade, prod_fator_pulmao), prod_audita_flowrack, ip.conf_codigo " +
                                "from wms_item_pedido ip " +
                                "inner join wms_produto p " +
                                "on p.prod_id = ip.prod_id " +
                                "inner join wms_pedido pd " +
                                "on pd.ped_codigo = ip.ped_codigo " +
                                "inner join wms_separacao s " +
                                "on s.prod_id = ip.prod_id " +
                                "inner join wms_estacao e " +
                                "on e.est_codigo = s.est_codigo " +
                                "where p.prod_separacao_flowrack = 'True' and s.sep_tipo = 'FLOWRACK' and ped_data_devolucao is null and pd.ped_codigo = @codPedido " +
                                "and mod(iped_quantidade, prod_fator_pulmao) > 0 " +
                                "and ped_data_envio_flow is null " +
                                "and ip.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "order by e.est_tipo desc, s.est_codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                //Atuliza no pedido a estacao para impressão de etiqueta
                string update = "update wms_item_flowrack set iflow_nivel = 'PRINT' " +
                                "where ped_codigo = @codPedido and iflow_criado is null and est_codigo = (select first 1 ip.est_codigo from wms_item_flowrack ip " +
                                "inner join wms_estacao e " +
                                "on e.est_codigo = ip.est_codigo " +
                                "where ip.ped_codigo = @codPedido " +
                                "order by e.est_tipo desc, e.est_codigo) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //Atuliza o status do pedido
                string update1 = "update wms_pedido set ped_flow_rack = 'SIM', ped_data_envio_flow = current_timestamp " +
                                "where ped_codigo = @codPedido and ped_data_devolucao is null and ped_data_envio_flow is null";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update1);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao enviar o(s) pedido(s) para processamento. \nDetalhes:" + ex.Message);
            }
        }

        //Insere os itens dos pedidos nos volumes de Flow rack
        public void ReiniciarProcessamento(int codPedido, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@empresa", empresa);

                //Insere os itens
                string delete1 = "delete from wms_item_flowrack where ped_codigo = @codPedido";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete1);

                //Atuliza no pedido a estacao para impressão de etiqueta
                string delete2 = "delete from wms_rastreamento_flowrack where ped_codigo = @codPedido ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete2);

                //Atuliza o status do pedido
                string update = "update wms_pedido set ped_flow_rack = null, ped_data_envio_flow = null, ped_inicio_flow_rack = null, ped_fim_flow_rack = null " +
                                "where ped_codigo = @codPedido and ped_data_devolucao is null and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao reiniciar o processamento. \nDetalhes:" + ex.Message);
            }
        }


        //Relatório
        public RendimentoFlowRackCollection PesquisarRendimentoFlowRack(string dataInicial, string dataFinal)
        {
            try
            {
                //Instância a camada de objeto
                RendimentoFlowRackCollection rendimentoCollection = new RendimentoFlowRackCollection();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                //conexao.AdicionarParamentros("@empresa", empresa);


                //Insere os itens
                string select = "select e.est_descricao, u.usu_login, count(prod_id) as acesso, sum(r.iflow_qtd_conferida) as quantidade, count(distinct(r.iflow_barra)) as volume, " +
                    /*Quantidade de pedido*/
                    "(select count(distinct(p.ped_codigo)) from wms_pedido p " +
                    "inner join wms_rastreamento_flowrack rr " +
                    "on rr.ped_codigo = p.ped_codigo " +
                    "where p.ped_fim_flow_rack between @dataInicial and @dataFinal and rr.usu_codigo_conferente = r.usu_codigo_conferente and rr.iflow_qtd_conferida > 0) as total_pedido, " +
                    /*Quantidade de volume*/
                    "(select count(distinct(iflow_barra)) " +
                    "from wms_rastreamento_flowrack r " +
                    "inner join wms_pedido p " +
                    "on p.ped_codigo = r.ped_codigo " +
                    "where r.iflow_criado between @dataInicial and @dataFinal) as total_volume, " +
                    /*Empresa*/
                    "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa " +
                    "from wms_rastreamento_flowrack r " +
                    "inner join wms_pedido p " +
                    "on p.ped_codigo = r.ped_codigo " +
                    "inner join wms_estacao e " +
                    "on e.est_codigo = r.est_codigo " +
                    "inner join wms_usuario u " +
                    "on r.usu_codigo_conferente = u.usu_codigo " +
                    " where r.iflow_criado between @dataInicial and @dataFinal " +
                    "group by est_descricao, usu_login, r.usu_codigo_conferente";



                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    RendimentoFlowRack rendimentoFlowRack = new RendimentoFlowRack();
                    //Adiciona os valores encontrados
                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        rendimentoFlowRack.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        rendimentoFlowRack.nomeUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["total_pedido"] != DBNull.Value)
                    {
                        rendimentoFlowRack.pedido = Convert.ToInt32(linha["total_pedido"]);
                    }

                    if (linha["acesso"] != DBNull.Value)
                    {
                        rendimentoFlowRack.acesso = Convert.ToInt32(linha["acesso"]);
                    }

                    if (linha["quantidade"] != DBNull.Value)
                    {
                        rendimentoFlowRack.qtdSeparada = Convert.ToInt32(linha["quantidade"]);
                    }

                    if (linha["volume"] != DBNull.Value)
                    {
                        rendimentoFlowRack.volume = Convert.ToInt32(linha["volume"]);
                    }

                    if (linha["total_volume"] != DBNull.Value)
                    {
                        rendimentoFlowRack.totalVolume = Convert.ToInt32(linha["total_volume"]);
                    }

                    if (linha["empresa"] != DBNull.Value)
                    {
                        rendimentoFlowRack.empresa = Convert.ToString(linha["empresa"]);
                    }

                    rendimentoFlowRack.dataInicial = Convert.ToDateTime(dataInicial);

                    rendimentoFlowRack.dataFinal = Convert.ToDateTime(dataFinal);


                    //Adiciona os cadastros encontrados a coleção
                    rendimentoCollection.Add(rendimentoFlowRack);
                }
                //Retorna a coleção de cadastro encontrada
                return rendimentoCollection;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório de rendimento. \nDetalhes:" + ex.Message);
            }
        }


        /*Backup
          //Pesquisa pedidos para processamento
        public ProcessamentoFlowCollection PesqDataPedido(string manifesto, string pedido, int[] rota, string dataIncial, string dataFinal,
            bool naoProcessados, bool processados, bool emProcesso, bool auditoria, bool emConferencia, bool enderecamento)
        {
            try
            {
                //Instância a coleção
                ProcessamentoFlowCollection pedidoProcFlowCollection = new ProcessamentoFlowCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@pedido", pedido);
                conexao.AdicionarParamentros("@manifesto", manifesto);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                //String de consulta
                string select = "select ped_data, pd.ped_codigo, r.rota_numero, mani_codigo, ped_data_envio_flow, " +
                    "min(e.est_codigo) est_inicial, max(e.est_codigo) est_final, ped_inicio_flow_rack, ped_fim_flow_rack, usu_login, " +
                    //Pesquisa qtd de itens
                    "(select count(ii.prod_id) from wms_item_pedido ii inner join wms_produto pp on ii.prod_id = pp.prod_id where ped_codigo = i.ped_codigo and mod(iped_quantidade, prod_fator_pulmao) > 0) as ped_itens, " +
                    //Pesquisa a estação atual de conferência
                    "(select max(est_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo) as est_atual, " +
                    //Pesquisa a quantidade de volumes produzidos
                    "(select max(iflow_numero) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo) as ped_volume, " +
                    //Pesquisa os itens a serem auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) as itens_auditar, " +
                    //Pesquisa os itens auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                    "and not v.iflow_data_auditoria is null) as itens_auditado, " +
                    //Pesquisa se o endereço foi endereçado
                    "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and apa_codigo is null) as volume_enderecado " +
                    "from wms_item_pedido i " +
                    "inner join wms_produto p " +
                    "on p.prod_id = i.prod_id " +
                    "inner join wms_separacao s " +
                    "on s.prod_id = p.prod_id " +
                    "inner join wms_estacao e " +
                    "on e.est_codigo = s.est_codigo " +
                    "inner join wms_pedido pd " +
                    "on pd.ped_codigo = i.ped_codigo " +
                    "inner join wms_cliente c " +
                    "on c.cli_id = pd.cli_id " +
                    "left join wms_rota r " +
                    "on r.rota_codigo = c.rota_codigo " +
                    "left join wms_usuario uu "+
                    "on uu.usu_codigo = pd.usu_codigo_flowrack " +
                    "where ped_status = 'IMPORTACAO COMPLETA' and mod(iped_quantidade, prod_fator_pulmao) > 0 " +
                    "and prod_separacao_flowrack = 'True' and ped_bloqueado = 'False' and ped_excluido is null and ped_data_devolucao is null " +
                    "and pd.cfop_codigo <> 13 and pd.tipo_codigo <> 141 and pd.tipo_codigo <> 143 and pd.tipo_codigo <> 185 and pd.tipo_codigo <> 187 " +
                    "and not pd.tipo_codigo in (select tipo_codigo from wms_tipo_pedido v where tipo_descricao like '%DEVOLUCAO%' or tipo_descricao like '%TROCA%' or tipo_descricao like '%AVARIA%') ";

                //Verifica o status - Não Processados
                if (naoProcessados == true)
                {
                    select += "and ped_data_envio_flow is null ";
                }
                //Prcessados (enviado, conferido, auditado e endereçado) = Finalizado
                else if (processados == true)
                {
                    select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                        "and " +
                        //Pesquisa os itens a serem auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) = " +
                        //Pesquisa os itens auditados
                        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                        "and not v.iflow_data_auditoria is null) " +
                        "and " +
                        //Pesquisa se os volumes estão endereçados
                        "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and apa_codigo is null) = 0 ";
                }
                else if (emProcesso == true)
{
    if (emConferencia == false && auditoria == false && enderecamento == false)
    {
        //Somente os enviado
        select += "and not ped_data_envio_flow is null and ped_inicio_flow_rack is null ";
    }

    //Status de conferência
    if (emConferencia == true)
    {
        select += "and not ped_data_envio_flow is null and not ped_inicio_flow_rack is null and ped_fim_flow_rack is null ";
    }
    //Status de auditoria
    else if (auditoria == true)
    {
        select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
        "and " +
        //Pesquisa os itens a serem auditados
        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) != " +
        //Pesquisa os itens auditados
        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
        "and not v.iflow_data_auditoria is null) ";
    }
    //Status de endereçamento
    else if (enderecamento == true)
    {
        select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
        "and " +
        //Pesquisa os itens a serem auditados
        "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) = " +
        //Pesquisa os itens auditados
        "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
        "and not v.iflow_data_auditoria is null) " +
        "and " +
        //Pesquisa se os volumes estão endereçados
        "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and apa_codigo is null) > 0 ";
    }
}

if (!manifesto.Equals(""))
{
    select += "and pd.mani_codigo = @manifesto ";
}
else if (!pedido.Equals(""))
{
    select += "and pd.ped_codigo = @pedido ";
}
else if (rota != null && rota.Length > 0)
{
    select += "and ped_data between @dataInicial and @dataFinal and r.rota_numero = " + rota[0] + " ";

    for (int i = 1; rota.Length > i; i++)
    {
        if (rota[i] != 0)
        {
            select += "or ped_status = 'IMPORTACAO COMPLETA' and mod(iped_quantidade, prod_fator_pulmao) > 0 " +
                    "and prod_separacao_flowrack = 'True' and ped_bloqueado = 'False' and ped_excluido is null and ped_data_devolucao is null " +
                    "and pd.cfop_codigo <> 13 and pd.tipo_codigo <> 141 and pd.tipo_codigo <> 143 and pd.tipo_codigo <> 185 and pd.tipo_codigo <> 187 " +
                    "and not pd.tipo_codigo in (select tipo_codigo from wms_tipo_pedido v where tipo_descricao like '%DEVOLUCAO%' or tipo_descricao like '%TROCA%' or tipo_descricao like '%AVARIA%') " +
                    "and ped_data between @dataInicial and @dataFinal and r.rota_numero = " + rota[i] + " ";

            //Verifica o status - Não Processados
            if (naoProcessados == true)
            {
                select += "and ped_data_envio_flow is null ";
            }
            //Prcessados (enviado, conferido, auditado e endereçado) = Finalizado
            else if (processados == true)
            {
                select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                    "and " +
                    //Pesquisa os itens a serem auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) = " +
                    //Pesquisa os itens auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                    "and not v.iflow_data_auditoria is null) " +
                    "and " +
                    //Pesquisa se os volumes estão endereçados
                    "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and apa_codigo is null) = 0 ";
            }
            else if (emProcesso == true)
            {
                if (emConferencia == false && auditoria == false && enderecamento == false)
                {
                    //Somente os enviado
                    select += "and not ped_data_envio_flow is null and ped_inicio_flow_rack is null ";
                }

                //Status de conferência
                if (emConferencia == true)
                {
                    select += "and not ped_data_envio_flow is null and not ped_inicio_flow_rack is null and ped_fim_flow_rack is null ";
                }
                //Status de auditoria
                else if (auditoria == true)
                {
                    select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                    "and " +
                    //Pesquisa os itens a serem auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) != " +
                    //Pesquisa os itens auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                    "and not v.iflow_data_auditoria is null) ";
                }
                //Status de endereçamento
                else if (enderecamento == true)
                {
                    select += "and not ped_data_envio_flow is null and not ped_fim_flow_rack is null " +
                    "and " +
                    //Pesquisa os itens a serem auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack where ped_codigo = i.ped_codigo and iflow_audita = 'True' ) = " +
                    //Pesquisa os itens auditados
                    "(select count(ped_codigo) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and iflow_audita = 'True' " +
                    "and not v.iflow_data_auditoria is null) " +
                    "and " +
                    //Pesquisa se os volumes estão endereçados
                    "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack v where ped_codigo = i.ped_codigo and apa_codigo is null) > 0 ";
                }
            }
        }
    }
}
else
{
    select += "and ped_data between @dataInicial and @dataFinal ";
}



//Verifica os pedidos que tem fracionado
select += "group by ped_data, pd.ped_codigo, mani_codigo, r.rota_numero, ped_data_envio_flow, usu_login, " +
          "ped_inicio_flow_rack, ped_fim_flow_rack, i.ped_codigo " +
          "order by rota_numero, ped_data";

//Instância um datatable
DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

//Percorre a tabela e adiciona as linha encontrada na coleção
foreach (DataRow linha in dataTable.Rows)
{
    //Instância a classe
    ProcessamentoFlow pedidoProcFlow = new ProcessamentoFlow();
    //Adiciona os valores encontrados
    if (linha["ped_data"] != DBNull.Value)
    {
        pedidoProcFlow.dataPedido = Convert.ToDateTime(linha["ped_data"]);
    }

    if (linha["ped_codigo"] != DBNull.Value)
    {
        pedidoProcFlow.codPedido = Convert.ToInt64(linha["ped_codigo"]);
    }

    if (linha["ped_itens"] != DBNull.Value)
    {
        pedidoProcFlow.itensPedido = Convert.ToInt32(linha["ped_itens"]);
    }

    if (linha["rota_numero"] != DBNull.Value)
    {
        pedidoProcFlow.rotaPedido = Convert.ToInt32(linha["rota_numero"]);
    }

    if (linha["mani_codigo"] != DBNull.Value)
    {
        pedidoProcFlow.manifestoPedido = Convert.ToInt32(linha["mani_codigo"]);
    }

    if (linha["ped_data_envio_flow"] != DBNull.Value)
    {
        pedidoProcFlow.dataEnvioProcessamento = Convert.ToDateTime(linha["ped_data_envio_flow"]);
    }

    if (linha["est_inicial"] != DBNull.Value)
    {
        pedidoProcFlow.estInicial = Convert.ToInt32(linha["est_inicial"]);
    }

    if (linha["est_final"] != DBNull.Value)
    {
        pedidoProcFlow.estFinal = Convert.ToInt32(linha["est_final"]);
    }

    if (linha["est_atual"] != DBNull.Value)
    {
        pedidoProcFlow.estAtual = "Estação: " + String.Format("{0:00}", linha["est_atual"]);
    }

    if (linha["ped_volume"] != DBNull.Value)
    {
        pedidoProcFlow.volumePedido = Convert.ToInt32(linha["ped_volume"]);
    }

    if (linha["ped_inicio_flow_rack"] != DBNull.Value)
    {
        pedidoProcFlow.dataInicialProcessamento = Convert.ToDateTime(linha["ped_inicio_flow_rack"]);
    }

    if (linha["ped_fim_flow_rack"] != DBNull.Value)
    {
        pedidoProcFlow.dataFinalProcessamento = Convert.ToDateTime(linha["ped_fim_flow_rack"]);

        //Calcula o tempo de conferência
        if (linha["ped_inicio_flow_rack"] != DBNull.Value && linha["ped_fim_flow_rack"] != DBNull.Value)
        {
            TimeSpan r = Convert.ToDateTime(linha["ped_fim_flow_rack"]).Subtract(Convert.ToDateTime(linha["ped_inicio_flow_rack"]));

            string tempo = r.ToString(@"d\.hh\:mm\:ss");

            pedidoProcFlow.tempoProcessamento = tempo;
        }
    }

    if (linha["usu_login"] != DBNull.Value)
    {
        pedidoProcFlow.usuInicioFlowRack = Convert.ToString(linha["usu_login"]);
    }

    if (linha["itens_auditar"] != DBNull.Value)
    {
        pedidoProcFlow.itensAuditar = Convert.ToInt32(linha["itens_auditar"]);
    }

    if (linha["itens_auditado"] != DBNull.Value)
    {
        pedidoProcFlow.itensAuditado = Convert.ToInt32(linha["itens_auditado"]);
    }

    //Volume endereçado = 0
    if (linha["volume_enderecado"] != DBNull.Value)
    {
        pedidoProcFlow.volumeEnderecado = Convert.ToInt32(linha["volume_enderecado"]);
    }


    //Adiciona os cadastros encontrados a coleção
    pedidoProcFlowCollection.Add(pedidoProcFlow);
}
//Retorna a coleção de cadastro encontrada
return pedidoProcFlowCollection;
            }
            catch (Exception ex)
            {
    throw new Exception("Ocorreu um erro ao pesquisar os pedidos. \nDetalhes:" + ex.Message);
}
        }

         */

    }
}
