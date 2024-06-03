using System;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class MonitorPedidoNegocios
    {
        //instância o objeto de conexão
        Conexao conexao = new Conexao();

        public MonitorPedidoCollection PesqPedido(string manifesto, string pedido, string notaFiscal, string rota, string codCliente,
                                                  string dataIncial, string dataFinal, string tipoPedido, bool conferidos, bool naoConferidos,
                                                  bool faturados, bool naoFaturados, bool impresso, bool naoImpresso, bool manifestado, bool naoManifestado,
                                                  bool bloqueio, bool ocorrencia, bool reentrega, bool agendamento, bool flowRack, bool semFlowRack, string empresa)
        {
            try
            {
                //Instância o objeto
                MonitorPedidoCollection monitorPedidoCollection = new MonitorPedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@pedido", pedido);
                conexao.AdicionarParamentros("@tipoPedido", tipoPedido);
                conexao.AdicionarParamentros("@notaFiscal", notaFiscal);
                conexao.AdicionarParamentros("@manifesto", manifesto);
                conexao.AdicionarParamentros("@rota", rota);
                conexao.AdicionarParamentros("@codCliente", codCliente);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de consulta - Pesquisa o pedido ou pedidos relacionados ao manifesto
                string select = "select p.ped_data, p.ped_codigo, tipo_descricao, p.ped_nota_fiscal, p.ped_data_nota_fiscal, p.mani_codigo, c.cli_codigo, c.cli_nome, " +
                                "ped_inicio_conferencia, ped_fim_conferencia, conf.usu_login as conferente, " +
                                "ped_inicio_separacao, ped_fim_separacao, sep.usu_login as separador, r.rota_codigo, r.rota_descricao, ped_data_impressao, " +
                                "e.est_uf, cid.cid_nome, b.bar_nome, c.cli_endereco, c.cli_numero, ped_bloqueado, ped_mot_bloqueio, usu_codigo_desbloqueio, " +
                                "ped_data_importacao, ped_implantador, ped_data_desbloqueio, ped_ocorrencia, ped_reentrega, " +
                                "mot.mot_nome || ' (' || mot.mot_apelido || ')' as mot_nome, v.vei_placa, m.mani_letra, " +
                                //Pesquisa o peso do pedido
                                "(select ped_peso from wms_pedido where ped_codigo = p.ped_codigo) as peso, " +
                                //Pesquisa a quantidade de itens do pedido
                                "(select count(prod_id) from wms_item_pedido ip " +
                                "where ip.ped_codigo = p.ped_codigo) as ped_itens " +
                                "from wms_pedido p " +
                                "left join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo and p.conf_codigo = m.conf_codigo " +
                                "left join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo and m.conf_codigo = v.conf_codigo " +
                                "left join wms_motorista mot " +
                                "on mot.mot_codigo = m.mot_codigo and m.conf_codigo = mot.conf_codigo " +
                                "inner join wms_cliente c " +
                                "on p.cli_id = c.cli_id and p.conf_codigo = c.conf_codigo " +
                                "left join wms_rota r " +
                                "on r.rota_codigo = c.rota_codigo and r.conf_codigo = c.conf_codigo " +
                                "left join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "left join wms_cidade cid " +
                                "on cid.cid_codigo = b.cid_codigo " +
                                "left join wms_estado e " +
                                "on e.est_codigo = cid.est_codigo " +
                                "left join wms_usuario conf " +
                                "on conf.usu_codigo = p.usu_codigo_conferente " +
                                "left join wms_usuario sep " +
                                "on sep.usu_codigo = p.usu_codigo_separador " +
                                " left join wms_tipo_pedido tp " +
                                "on tp.tipo_codigo = p.tipo_codigo and tp.conf_codigo = p.conf_codigo " +
                                "where ped_status = 'IMPORTACAO COMPLETA' and ped_excluido is null ";


                if (!manifesto.Equals(""))
                {
                    select += "and p.mani_codigo = @manifesto ";

                    if(tipoPedido.Equals("DEVOLUCAO"))
                    {
                        select += "and tipo_descricao like '%DEV%' ";
                    }

                    if (tipoPedido.Equals("BONIFICACAO"))
                    {
                        select += "and tipo_descricao like '%BON%' ";
                    }
                    select += "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) "; //altração
                }
                else if (!pedido.Equals(""))
                {
                    select += "and ped_codigo = @pedido and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";//alteração
                }
                else if (!notaFiscal.Equals(""))
                {
                    select += "and ped_nota_fiscal = @notaFiscal and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)"; //alteraçao
                }
                else if (!rota.Equals(""))
                {
                    select += "and r.rota_codigo = @rota and ped_data between @dataInicial and @dataFinal ";

                    if (tipoPedido.Equals("DEVOLUCAO"))
                    {
                        select += "and tipo_descricao like '%DEV%' ";
                    }

                    if (tipoPedido.Equals("BONIFICACAO"))
                    {
                        select += "and tipo_descricao like '%BON%' ";
                    }

                    if (tipoPedido.Equals("VENDA"))
                    {
                        select += "and tipo_descricao like '%VEN%' ";
                    }
                    select += "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";//alteração
                }
                else if (!codCliente.Equals(""))
                {
                    select += "and cli_codigo = @codCliente and ped_data between @dataInicial and @dataFinal ";

                    if (tipoPedido.Equals("DEVOLUCAO"))
                    {
                        select += "and tipo_descricao like '%DEV%' ";
                    }

                    if (tipoPedido.Equals("BONIFICACAO"))
                    {
                        select += "and tipo_descricao like '%BON%' ";
                    }

                    if (tipoPedido.Equals("VENDA"))
                    {
                        select += "and tipo_descricao like '%VEN%' ";
                    }
                    select += "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";//alteração
                }
                else
                {
                    select += "and ped_data between @dataInicial and @dataFinal ";

                    if (tipoPedido.Equals("DEVOLUCAO"))
                    {
                        select += "and tipo_descricao like '%DEV%' ";
                    }

                    if (tipoPedido.Equals("BONIFICACAO"))
                    {
                        select += "and tipo_descricao like '%BON%' ";
                    }

                    if (tipoPedido.Equals("VENDA"))
                    {
                        select += "and tipo_descricao like '%VEN%' ";
                    }
                    select += "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";//alteração
                }

                /*Pesquisa o tipo do pedido
                if (!tipoPedido.Equals("TODOS"))
                {
                    select += "and tp.tipo_descricao LIKE @tipoPedido "; //and tp.tipo_controle = 'True' ";
                }*/

                if (conferidos == true)
                {
                    select += "and not ped_inicio_conferencia is null and not ped_fim_conferencia is null ";
                }

                if (naoConferidos == true)
                {
                    select += "and ped_fim_conferencia is null ";
                }

                if (faturados == true)
                {
                    select += "and not ped_nota_fiscal is null ";
                }

                if (naoFaturados == true)
                {
                    select += "and ped_nota_fiscal is null ";
                }

                if (impresso == true)
                {
                    select += "and not p.ped_data_impressao is null ";
                }

                if (naoImpresso == true)
                {
                    select += "and p.ped_data_impressao is null ";
                }

                if (manifestado == true)
                {
                    select += "and not p.mani_codigo is null ";
                }

                if (naoManifestado == true)
                {
                    select += "and p.mani_codigo is null ";
                }

                if (bloqueio == true)
                {
                    select += "and not p.ped_bloqueado = 'True' ";
                }

                if (ocorrencia == true)
                {
                    select += "and ped_ocorrencia = 'True' ";
                }

                if (reentrega == true)
                {
                    select += "and ped_reentrega = 'True' ";
                }

                if (agendamento == true)
                {
                    select += "and cli_agendamento = 'True' ";
                }

                if (flowRack == true)
                {
                    select += "and ped_flow_rack = 'SIM' ";
                }

                if (semFlowRack == true)
                {
                    select += "and ped_flow_rack is null ";
                }



                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    MonitorPedido monitorPedido = new MonitorPedido();
                    //Adiciona os dados
                    if (linha["rota_codigo"] != DBNull.Value)
                    {
                        monitorPedido.rota = Convert.ToString(linha["rota_codigo"]) + " - " + Convert.ToString(linha["rota_descricao"]);
                    }

                    if (linha["ped_data"] != DBNull.Value)
                    {
                        monitorPedido.dataPedido = Convert.ToDateTime(linha["ped_data"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        monitorPedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        monitorPedido.tipoPedido = Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        monitorPedido.notaFiscal = Convert.ToInt32(linha["ped_nota_fiscal"]);
                    }

                    if (linha["ped_data_nota_fiscal"] != DBNull.Value)
                    {
                        monitorPedido.dataFaturamento = Convert.ToDateTime(linha["ped_data_nota_fiscal"]);
                    }                    

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        monitorPedido.manifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        monitorPedido.codCliente = Convert.ToInt32(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        monitorPedido.nmCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["ped_itens"] != DBNull.Value)
                    {
                        monitorPedido.itensPedido = Convert.ToInt32(linha["ped_itens"]);
                    }

                    // if (linha["ped_volume"] != DBNull.Value)
                    // {
                    //    monitorPedido.volumePedido = Convert.ToInt32(linha["ped_volume"]);
                    // }

                    if (linha["peso"] != DBNull.Value)
                    {
                        monitorPedido.pesoPedido = Convert.ToDouble(linha["peso"]);
                    }

                    if (linha["ped_data_impressao"] != DBNull.Value)
                    {
                        monitorPedido.dataImpressao = Convert.ToDateTime(linha["ped_data_impressao"]);
                    }

                    if (linha["conferente"] != DBNull.Value)
                    {
                        monitorPedido.nmConferente = Convert.ToString(linha["conferente"]);
                    }

                    if (linha["ped_inicio_conferencia"] != DBNull.Value)
                    {
                        monitorPedido.inicioConferencia = Convert.ToDateTime(linha["ped_inicio_conferencia"]);
                    }

                    if (linha["ped_fim_conferencia"] != DBNull.Value)
                    {
                        monitorPedido.fimConferencia = Convert.ToDateTime(linha["ped_fim_conferencia"]);

                        TimeSpan r = Convert.ToDateTime(linha["ped_inicio_conferencia"]).Subtract(Convert.ToDateTime(linha["ped_fim_conferencia"]));

                        string tempo = r.ToString(@"d\.hh\:mm\:ss");

                        monitorPedido.tempoConferencia = tempo;
                    }

                    if (linha["separador"] != DBNull.Value)
                    {
                        monitorPedido.nmSeparador = Convert.ToString(linha["separador"]);
                    }

                    if (linha["ped_inicio_separacao"] != DBNull.Value)
                    {
                        monitorPedido.inicioSeparacao = Convert.ToDateTime(linha["ped_inicio_separacao"]);
                    }

                    if (linha["ped_fim_separacao"] != DBNull.Value)
                    {
                        monitorPedido.fimSeparacao = Convert.ToDateTime(linha["ped_fim_separacao"]);

                        TimeSpan r = Convert.ToDateTime(linha["ped_inicio_separacao"]).Subtract(Convert.ToDateTime(linha["ped_fim_separacao"]));

                        string tempo = r.ToString(@"d\.hh\:mm\:ss");

                        monitorPedido.tempoSeparacao = tempo;
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        monitorPedido.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        monitorPedido.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        monitorPedido.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        monitorPedido.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        monitorPedido.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        monitorPedido.nmMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        monitorPedido.Placa = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["mani_letra"] != DBNull.Value)
                    {
                        monitorPedido.marcacao = Convert.ToString(linha["mani_letra"]);
                    }

                    if (linha["ped_bloqueado"] != DBNull.Value)
                    {
                        monitorPedido.pedBloqueado = Convert.ToString(linha["ped_bloqueado"]);
                    }

                    if (linha["ped_ocorrencia"] != DBNull.Value)
                    {
                        monitorPedido.pedOcorrencia = Convert.ToString(linha["ped_ocorrencia"]);
                    }

                    if (linha["ped_reentrega"] != DBNull.Value)
                    {
                        monitorPedido.pedReentrega = Convert.ToString(linha["ped_reentrega"]);
                    }

                    if (linha["ped_mot_bloqueio"] != DBNull.Value)
                    {
                        monitorPedido.pedMotivoBloqueio = Convert.ToString(linha["ped_mot_bloqueio"]);
                    }

                    if (linha["usu_codigo_desbloqueio"] != DBNull.Value)
                    {
                        monitorPedido.pedDesbloPor = Convert.ToString(linha["usu_codigo_desbloqueio"]);
                    }

                    if (linha["ped_data_desbloqueio"] != DBNull.Value)
                    {
                        monitorPedido.pedDataDesbloqueio = Convert.ToDateTime(linha["ped_data_desbloqueio"]);
                    }

                    if (linha["ped_data_importacao"] != DBNull.Value)
                    {
                        monitorPedido.pedDataImportacao = Convert.ToDateTime(linha["ped_data_importacao"]);
                    }

                    if (linha["ped_implantador"] != DBNull.Value)
                    {
                        monitorPedido.pedImplantador = Convert.ToString(linha["ped_implantador"]);
                    }

                    monitorPedidoCollection.Add(monitorPedido);
                }

                return monitorPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao efetuar a pesquisa de pedido! \nDetalhes: " + ex.Message);
            }
        }

        public MonitorPedidoCollection PesqRotaClienteData(string cliente, string rota, string dataInicial, string dataFinal, bool conferido, bool naoConferido, bool naoImpresso, string empresa)
        {
            try
            {
                //Instância o objeto
                MonitorPedidoCollection monitorPedidoCollection = new MonitorPedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@cliente", cliente);
                conexao.AdicionarParamentros("@rota", rota);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@conferido", conferido);
                conexao.AdicionarParamentros("@naoConferido", naoConferido);
                conexao.AdicionarParamentros("@naoImpresso", naoImpresso);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta - Pesquisa o pedido ou pedidos relacionados ao manifesto
                string select = "select isn_rota, isn_manifesto, peddt_pedido, isn_pedido, pedcn_pedido, clicn_cliente, clinm_cliente, pedfg_impresso_mapa, " +
                                "pedvl_peso, peddt_inicio_conferencia, peddt_fim_conferencia, usunm_usuario, " +
                                "sepnm_separador, pedvl_volume, pedqt_conf_faltas from t_pedido ped " +
                                "inner join t_cliente cli " +
                                "on cli.isn_cliente = ped.isn_cliente " +
                                "left outer join t_separador sep " +
                                "on sep.isn_separador = ped.isn_separador_wms " +
                                "left outer join wms_usuario conf " +
                                "on conf.isn_usuario = ped.isn_usuconferente_wms " +
                                "where peddt_pedido between @dataInicial and @dataFinal" +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                if (cliente != "")
                {
                    select = select + " and clicn_cliente = @cliente";
                }

                if (rota != "")
                {
                    select = select + " and isn_rota = @rota";
                }

                if (conferido == true)
                {
                    select = select + " and peddt_fim_conferencia is not null";
                }

                if (naoConferido == true)
                {
                    select = select + " and peddt_fim_conferencia is null";
                }

                if (naoImpresso == true)
                {
                    select = select + " and pedfg_impresso_mapa = 'N'";
                }

                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    MonitorPedido monitorPedido = new MonitorPedido();
                    //Adiciona os dados
                    //monitorPedido.rota = Convert.ToInt32(linha["isn_rota"]);
                    monitorPedido.dataPedido = Convert.ToDateTime(linha["peddt_pedido"]);
                    monitorPedido.codPedido = Convert.ToInt32(linha["isn_pedido"]);
                    monitorPedido.pedido = Convert.ToInt32(linha["pedcn_pedido"]);
                    monitorPedido.codCliente = Convert.ToInt32(linha["clicn_cliente"]);
                    monitorPedido.nmCliente = Convert.ToString(linha["clinm_cliente"]);
                    monitorPedido.nmConferente = Convert.ToString(linha["usunm_usuario"]);
                    monitorPedido.nmSeparador = Convert.ToString(linha["sepnm_separador"]);

                    if (Convert.ToString(linha["pedfg_impresso_mapa"]) == "N" || Convert.ToString(linha["pedfg_impresso_mapa"]) == "")
                    {
                        // monitorPedido.statusImpressao = "Não Impresso";
                    }
                    else
                    {
                        //monitorPedido.statusImpressao = "Impresso";
                    }

                    if (Convert.ToString(linha["isn_manifesto"]) != "")
                    {
                        monitorPedido.manifesto = Convert.ToInt32(linha["isn_manifesto"]);
                    }

                    if (Convert.ToString(linha["peddt_inicio_conferencia"]) != "")
                    {
                        monitorPedido.inicioConferencia = Convert.ToDateTime(linha["peddt_inicio_conferencia"]);
                        monitorPedido.statusConferencia = "Em Progresso";
                    }
                    else
                    {
                        monitorPedido.statusConferencia = "Não Iniciada";
                    }

                    if (Convert.ToString(linha["peddt_fim_conferencia"]) != "")
                    {
                        monitorPedido.fimConferencia = Convert.ToDateTime(linha["peddt_fim_conferencia"]);
                        monitorPedido.statusConferencia = "Finalizada";
                    }

                    if (Convert.ToString(linha["peddt_inicio_conferencia"]) != "" && Convert.ToString(linha["peddt_fim_conferencia"]) != "")
                    {
                        TimeSpan r = Convert.ToDateTime(linha["peddt_inicio_conferencia"]).Subtract(Convert.ToDateTime(linha["peddt_fim_conferencia"]));

                        string tempo = r.ToString(@"d\.hh\:mm\:ss");

                        monitorPedido.tempoConferencia = tempo;
                    }

                    if (Convert.ToString(linha["pedvl_peso"]) != "")
                    {
                        monitorPedido.pesoPedido = Convert.ToDouble(linha["pedvl_peso"]);
                    }

                    if (Convert.ToString(linha["pedqt_conf_faltas"]) != "")
                    {
                        monitorPedido.faltaPedido = Convert.ToInt32(linha["pedqt_conf_faltas"]);
                    }

                    if (Convert.ToString(linha["pedvl_volume"]) != "")
                    {
                        monitorPedido.volumePedido = Convert.ToInt32(linha["pedvl_volume"]);
                    }

                    monitorPedidoCollection.Add(monitorPedido);
                }

                return monitorPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao efetuar a pesquisa por data! \nDetalhes: " + ex.Message);
            }
        }


        //Método Alterar cadastro
        public void EnviarPedidoSeparacao(string empresa, int codUsuario, int codPedido)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codPedido", codPedido);

                //String de atualização
                string update = "update wms_pedido set ped_enviado_separacao = 'True', ped_envio_data_separacao = current_timestamp, "+
                                "usu_codigo_envio_separacao = @codUsuario "+
                                "where ped_codigo = @codPedido and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao enviar os pedido para separação. \nDetalhes:" + ex.Message);
            }
        }




    }
}
