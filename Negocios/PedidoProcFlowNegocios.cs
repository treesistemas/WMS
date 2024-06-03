using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class PedidoProcFlowNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa as categorias
        public PedidoProcFlowCollection PesqPedido()
        {
            try
            {
                //Instância a coleção
                PedidoProcFlowCollection pedidoProcFlowCollection = new PedidoProcFlowCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select ped_data, pd.ped_codigo, r.rota_numero, mani_codigo, ped_data_envio_flow, " +
                    "min(e.est_codigo) est_inicial, max(e.est_codigo) est_final, ped_inicio_flow_rack, ped_fim_flow_rack, " +
                    /*Pesquisa a quantidade de item no pedido*/
                    "(select count(ped_codigo) from wms_item_pedido ii " +
                    "inner join wms_produto pp " +
                    "on pp.prod_id = ii.prod_id " +
                    "where ped_codigo = i.ped_codigo and mod(iped_quantidade, prod_fator_pulmao) > 1 and prod_separacao_flowrack = 'True') as ped_itens, " +
                    /*Pesquisa a estação atual de conferência*/
                    "(select max(est_codigo) from wms_rastreamento_volumes where ped_codigo = i.ped_codigo) as est_atual, " +
                    /*Pesquisa a quantidade de volumes produzidos*/
                    "(select max(ivol_numero) from wms_rastreamento_volumes where ped_codigo = i.ped_codigo) as ped_volume, " +
                    //Pesquisa os itens a serem auditados
                    "(select count(ped_codigo) from wms_rastreamento_volumes where ped_codigo = i.ped_codigo and ivol_audita = 'True' ) as itens_auditar, " +
                    //Pesquisa os itens auditados
                    "(select count(ped_codigo) from wms_rastreamento_volumes v where ped_codigo = i.ped_codigo and ivol_audita = 'True' " +
                    "and not v.ivol_data_auditoria is null) as itens_auditado, " +
                    //Pesquisa se o endereço foi endereçado
                    "(select count(distinct(ivol_numero)) from wms_rastreamento_volumes v where ped_codigo = i.ped_codigo and apa_codigo is null) as volume_enderecado " +
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
                    "where pd.ped_data between '24.11.2020 08:00:00' and '24.11.2020 09:00:00' " +
                    "and mod(iped_quantidade, prod_fator_pulmao) > 1 and p.prod_separacao_flowrack = 'True' " +
                    "group by ped_data, pd.ped_codigo, mani_codigo, r.rota_numero, ped_data_envio_flow, " +
                    "ped_inicio_flow_rack, ped_fim_flow_rack, i.ped_codigo " +
                    "order by rota_numero, ped_data";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    PedidoProcFlow pedidoProcFlow = new PedidoProcFlow();
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

        //Insere os itens dos pedidos nos volumes de Flow rack
        public void ProcessarPedidos(int codPedido)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);

                //Insere os itens
                string insert = "insert into wms_item_volume (ivol_codigo, ped_codigo, ivol_numero, ivol_barra, est_codigo, prod_id, ivol_quantidade, ivol_audita) " +
                                "select gen_id(gen_wms_volume, 1), ip.ped_codigo, '01', ip.ped_codigo || '01', s.est_codigo, ip.prod_id, " +
                                "mod(iped_quantidade, prod_fator_pulmao), prod_audita_flowrack from wms_item_pedido ip " +
                                "inner join wms_produto p " +
                                "on p.prod_id = ip.prod_id " +
                                "inner join wms_pedido pd " +
                                "on pd.ped_codigo = ip.ped_codigo " +
                                "inner join wms_separacao s " +
                                "on s.prod_id = ip.prod_id " +
                                "where p.prod_separacao_flowrack = 'True' and s.sep_tipo = 'FLOWRACK' and ped_data_devolucao is null and pd.ped_codigo = @codPedido " +
                                "and mod(iped_quantidade, prod_fator_pulmao) > 0 " +
                                "and ped_data_envio_flow is null " +
                                "order by s.est_codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                //Atuliza o status do pedido
                string update = "update wms_pedido set ped_flow_rack = 'SIM', ped_data_envio_flow = current_timestamp " +
                                "where ped_codigo = @codPedido and ped_data_devolucao is null and ped_data_envio_flow is null";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao enviar o(s) pedido(s) para processamento. \nDetalhes:" + ex.Message);
            }
        }

    }
}
