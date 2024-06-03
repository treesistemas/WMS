using System;
using System.Data;
using Dados;
using ObjetoTransferencia;
using ObjetoTransferencia.Relatorio;

namespace Negocios.Relatorio
{
    public class PedidoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();
        //Pesquisa os pedidos
        public MapaSeparacaoCollection PesqPedidoManifesto(string empresa, int codManifesto, int codPedido, string controlaSequenciaCarregamento, bool naoConferido, bool naoImpresso, string ordem)
        {
            try
            {
                //Instância a camada de objêto
                MapaSeparacaoCollection pedidoCollection = new MapaSeparacaoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@ordem", ordem);

                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao where conf_sigla = @empresa) as empresa, p.tipo_codigo, " +
                "(select cast(iconf_valor as integer) from wms_itens_configuracao " +
                "where iconf_descricao = 'SEPARAR PRODUTOS PRÓXIMO AO VENCIMENTO POR TIPO DE PEDIDO' and iconf_status = 'True') as tipo_vencimento, " +
                "p.mani_codigo, mani_letra, " +
                /*Pesquisa a qtd de pedido no manifesto*/
                "(select count(ped_codigo) from wms_pedido where mani_codigo = p.mani_codigo and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as cont_pedido, " +
                /*Soma o peso total do pedido no manifesto*/
                "(select sum(ped_peso) from wms_pedido where mani_codigo = p.mani_codigo and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as soma_peso, " +
                /*Pesquisa a cubagem do pedido*/
                "(select sum(bar_cubagem * i.iped_quantidade) from wms_item_pedido i " +
                "inner join wms_barra b " +
                "on i.prod_id = b.prod_id and i.conf_codigo = b.conf_codigo " +
                "where bar_multiplicador = 1 and ped_codigo = p.ped_codigo and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as ped_cubagem, " +
                /*Quantidade de caixa fechada*/
                "(select sum(trunc(iped_quantidade/pp.prod_fator_pulmao)) from wms_item_pedido i " +
                "inner join wms_produto pp " +
                "on i.prod_id = pp.prod_id and i.conf_codigo = pp.conf_codigo " +
                "where i.ped_codigo = p.ped_codigo and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa))  as qtd_fechada, " +
                /*Quatidade em unidade*/
                "(select sum(mod(iped_quantidade, pp.prod_fator_pulmao)) from wms_item_pedido i " +
                "inner join wms_produto pp " +
                "on i.prod_id = pp.prod_id and i.conf_codigo = pp.conf_codigo " +
                "where i.ped_codigo = p.ped_codigo and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as qtd_fracionada, " +
                /*Quantidade de volumes do flowrack*/
                "(select max(iflow_numero) from wms_rastreamento_flowrack where ped_codigo = p.ped_codigo and iflow_qtd_conferida > 0 and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as qtd_volumes, " +
                "(select iconf_status from wms_itens_configuracao where iconf_descricao = 'EXIBIR A SEQUENCIA DO PEDIDO DO ROTEIRIZADOR') as controla_sequencia, " +
                "(select iconf_status from wms_itens_configuracao where iconf_descricao = 'SEGUIR 100% A ROREIRIZAÇÃO POR SOFTWARE DE TERCEIRO') as roteirizador_sequencia, " +
                "v.vei_placa, r.rota_numero || ' - ' || r.rota_descricao as rota, p.ped_data, p.ped_codigo, " +
                "c.cli_codigo || ' - ' || c.cli_nome as cliente, cli_fantasia, cid.cid_nome, b.bar_nome, c.cli_endereco, c.cli_numero, rp.rep_numero, rp.rep_nome, rp.rep_celular, " +
                "pg.pag_descricao, pz.prazo_descricao, " +
                "p.ped_data, c.cli_compartilhada, c.cli_validade, c.cli_validade_dias, c.cli_paletizado, " +
                "c.cli_caixa_fechada, c.cli_nao_dividir_carga, c.cli_observacao, " +
                "p.ped_flow_rack, p.ped_peso, p.ped_observacao, ped_impresso, ped_sequencia_rota, cli_sequencia, mot_nome " +
                "from wms_pedido p " +
                "left join wms_manifesto m " +
                "on m.mani_codigo = p.mani_codigo " +
                "left join wms_cliente c " +
                "on c.cli_id = p.cli_id " +
                "left join wms_bairro b " +
                "on b.bar_codigo = c.bar_codigo " +
                "left join wms_cidade cid " +
                "on cid.cid_codigo = b.cid_codigo " +
                "left join wms_rota r " +
                "on r.rota_codigo = c.rota_codigo " +
                "left join wms_veiculo v " +
                "on v.vei_codigo = m.vei_codigo " +
                "left join wms_motorista mo " +
                "on mo.mot_codigo = m.mot_codigo " +
                "inner join wms_pagamento pg " +
                "on pg.pag_codigo = p.pag_codigo " +
                "inner join wms_prazo pz " +
                "on pz.prazo_codigo = p.prazo_codigo " +
                "inner join wms_representante rp " +
                "on rp.rep_codigo = p.rep_codigo ";

                if (codPedido > 0)
                {
                    select += "where p.ped_codigo = @codPedido and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }


                if (controlaSequenciaCarregamento == "True")
                {
                    //GD E FORQUILHA
                    if (codManifesto > 0)
                    {
                        select += "where p.mani_codigo = @codManifesto and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                        if (naoConferido == true)
                        {
                            select += "and ped_fim_conferencia is null ";
                        }

                        if (naoImpresso == true)
                        {
                            select += "and ped_impresso is null ";
                        }

                        if (ordem.Equals("asc"))
                        {
                            select += "order by p.ped_sequencia_rota asc, cid.cid_sequencia desc, c.cli_sequencia desc"; //Asc
                        }

                        if (ordem.Equals("desc"))
                        {
                            select += "order by p.ped_sequencia_rota desc, cid.cid_sequencia desc, c.cli_sequencia desc"; //desc
                        }
                    }
                }
                //VERIFICAR NA DONIZETE
                else if (codManifesto > 0)
                {
                    select += "where p.mani_codigo = @codManifesto and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                    if (naoConferido == true)
                    {
                        select += "and ped_fim_conferencia is null ";
                    }

                    if (naoImpresso == true)
                    {
                        select += "and ped_impresso is null ";
                    }

                    if (ordem.Equals("asc"))
                    {
                        //select += "order by cid.cid_sequencia asc, p.ped_sequencia_rota desc, c.cli_sequencia desc"; //asc
                        select += "order by " +
                                  "CASE WHEN (select iconf_status from wms_itens_configuracao where iconf_descricao = 'SEGUIR 100% A ROREIRIZAÇÃO POR SOFTWARE DE TERCEIRO') = 'True' THEN p.ped_sequencia_rota " +
                                  "end asc, " +
                                  "CASE WHEN (select iconf_status from wms_itens_configuracao where iconf_descricao = 'SEGUIR 100% A ROREIRIZAÇÃO POR SOFTWARE DE TERCEIRO') = 'False' THEN cid.cid_sequencia " +
                                  "end asc, " +
                                  "p.ped_sequencia_rota desc, c.cli_sequencia desc";
                    }

                    if (ordem.Equals("desc"))
                    {
                        //select += "order by cid.cid_sequencia desc, p.ped_sequencia_rota desc, c.cli_sequencia desc"; //Desc
                        select += "order by " +
                                  "CASE WHEN (select iconf_status from wms_itens_configuracao where iconf_descricao = 'SEGUIR 100% A ROREIRIZAÇÃO POR SOFTWARE DE TERCEIRO') = 'True' THEN p.ped_sequencia_rota " +
                                  "end desc, " +
                                  "CASE WHEN (select iconf_status from wms_itens_configuracao where iconf_descricao = 'SEGUIR 100% A ROREIRIZAÇÃO POR SOFTWARE DE TERCEIRO') = 'False' THEN cid.cid_sequencia " +
                                  "end desc, " +
                                  "p.ped_sequencia_rota desc, c.cli_sequencia desc";
                    }


                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
                int i = 1;
                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    MapaSeparacao pedido = new MapaSeparacao();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        pedido.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        pedido.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["cont_pedido"] != DBNull.Value)
                    {
                        pedido.qtdPedidoManifesto = Convert.ToInt32(linha["cont_pedido"]);
                    }

                    if (linha["soma_peso"] != DBNull.Value)
                    {
                        pedido.PesoTotalManifesto = Convert.ToDouble(linha["soma_peso"]);
                    }

                    if (Convert.ToString(linha["controla_sequencia"]).Equals("True"))
                    {
                        if (linha["ped_sequencia_rota"] != DBNull.Value)
                        {
                            pedido.sequenciaPedido = Convert.ToInt32(linha["ped_sequencia_rota"]);
                        }
                        else
                        {
                            pedido.sequenciaPedido = i; //Controla a sequencia do pedido
                        }
                    }
                    else
                    {
                        pedido.sequenciaPedido = i; //Controla a sequencia do pedido
                    }

                    if (Convert.ToString(linha["roteirizador_sequencia"]).Equals("True"))
                    {

                        pedido.sequenciaPedido = i; //Controla a sequencia do pedido
                    }




                    if (linha["mani_letra"] != DBNull.Value)
                    {
                        pedido.letraPedido = Convert.ToString("| " + linha["mani_letra"] + " |");
                    }

                    if (linha["rota"] != DBNull.Value)
                    {
                        pedido.rotaPedido = Convert.ToString(linha["rota"]) + "   Sequência: " + Convert.ToString(linha["cli_sequencia"]);
                    }

                    if (linha["rep_numero"] != DBNull.Value)
                    {
                        pedido.pedNomeRepresentante = Convert.ToString(linha["rep_numero"]) + " - " + Convert.ToString(linha["rep_nome"]) + "/" + Convert.ToString(linha["rep_celular"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        pedido.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        pedido.nomeMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    if (linha["cliente"] != DBNull.Value)
                    {
                        pedido.nomeCliente = Convert.ToString(linha["cliente"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        pedido.FantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        pedido.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        pedido.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        pedido.pedEndereco = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        pedido.pedEndNumero = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["pag_descricao"] != DBNull.Value)
                    {
                        pedido.pedPagamento = Convert.ToString(linha["pag_descricao"]);
                    }

                    if (linha["prazo_descricao"] != DBNull.Value)
                    {
                        pedido.pedPrazo = Convert.ToString(linha["prazo_descricao"]);
                    }

                    if (linha["tipo_codigo"] != DBNull.Value)
                    {
                        pedido.tipoPedido = Convert.ToInt32(linha["tipo_codigo"]);
                    }

                    if (linha["tipo_vencimento"] != DBNull.Value)
                    {
                        pedido.tipoPedidoVencimento = Convert.ToInt32(linha["tipo_vencimento"]);
                    }

                    if (linha["ped_data"] != DBNull.Value)
                    {
                        pedido.dataPedido = Convert.ToDateTime(linha["ped_data"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["qtd_fechada"] != DBNull.Value)
                    {
                        pedido.qtdGrandeza = Convert.ToInt32(linha["qtd_fechada"]);
                    }

                    if (linha["qtd_Volumes"] != DBNull.Value)
                    {
                        pedido.qtdFlowRack = Convert.ToInt32(linha["qtd_Volumes"]);
                    }

                    if (linha["qtd_fracionada"] != DBNull.Value)
                    {
                        pedido.qtdFracionada = Convert.ToInt32(linha["qtd_fracionada"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        pedido.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                    if (linha["ped_cubagem"] != DBNull.Value)
                    {
                        pedido.cubagemPedido = Convert.ToDouble(linha["ped_cubagem"]);
                    }

                    if (linha["cli_compartilhada"] != DBNull.Value)
                    {
                        pedido.rotaCompartilhada = Convert.ToString(linha["cli_compartilhada"]);
                    }

                    if (linha["cli_paletizado"] != DBNull.Value)
                    {
                        pedido.exigePaletizacaoCliente = Convert.ToString(linha["cli_paletizado"]);
                    }

                    if (linha["cli_validade"] != DBNull.Value)
                    {
                        pedido.exigeDataCliente = Convert.ToString(linha["cli_validade"]);
                    }

                    if (linha["cli_validade_dias"] != DBNull.Value)
                    {
                        pedido.dataExigidaCliente = Convert.ToDateTime(DateTime.Now).AddDays(Convert.ToInt32(linha["cli_validade_dias"]));
                    }
                    else
                    {
                        pedido.dataExigidaCliente = Convert.ToDateTime(DateTime.Now).AddDays(0);
                    }

                    if (linha["cli_caixa_fechada"] != DBNull.Value)
                    {
                        pedido.exigeCxaFechadaCliente = Convert.ToString(linha["cli_caixa_fechada"]);
                    }

                    if (linha["ped_flow_rack"] != DBNull.Value)
                    {
                        pedido.flowRackPedido = Convert.ToString(linha["ped_flow_rack"]);
                    }

                    if (linha["cli_nao_dividir_carga"] != DBNull.Value)
                    {
                        pedido.naoAceitaDividirCargaCliente = Convert.ToString(linha["cli_nao_dividir_carga"]);
                    }

                    if (linha["cli_observacao"] != DBNull.Value)
                    {
                        pedido.observacaoCliente = Convert.ToString(linha["cli_observacao"]);
                    }

                    if (linha["ped_observacao"] != DBNull.Value)
                    {
                        pedido.observacaoPedido = Convert.ToString(linha["ped_observacao"]);
                    }

                    if (linha["ped_impresso"] != DBNull.Value)
                    {
                        pedido.impressaoMapa = Convert.ToString(linha["ped_impresso"]);
                    }



                    //Adiona o objêto a coleção
                    pedidoCollection.Add(pedido);

                    //Soma + 1 n contagem
                    i++;
                }
                //Retorna a coleção de cadastro encontrada
                return pedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório por manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens do pedido a pedido
        public ItemMapaSeparacaoCollection PesqItemPedidoManifesto(string empresa, int codManifesto, int codPedido, bool naoConferido, bool naoImpresso)
        {
            try
            {
                //Instância a camada de objêto
                ItemMapaSeparacaoCollection itemPedidoCollection = new ItemMapaSeparacaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);

                //String de consulta
                string select = "select p.ped_codigo, p.tipo_codigo, coalesce(ped_tabela, 0) as ped_tabela, " +
                "(select cast(iconf_valor as integer) from wms_itens_configuracao " +
                "where iconf_descricao = 'SEPARAR PEDIDO POR PRODUTOS PRÓXIMO AO VENCIMENTO I' and iconf_status = 'True') as tipo_vencimento_I, " +
                "(select cast(iconf_valor as integer) from wms_itens_configuracao " +
                "where iconf_descricao = 'SEPARAR PEDIDO POR PRODUTOS PRÓXIMO AO VENCIMENTO II' and iconf_status = 'True') as tipo_vencimento_II, " +
                "(select cast(iconf_valor as integer) from wms_itens_configuracao " +
                "where iconf_descricao = 'SEPARAR PEDIDO POR PRODUTOS PRÓXIMO AO VENCIMENTO III' and iconf_status = 'True') as tipo_vencimento_III, " +
                "(select a.apa_endereco from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'CAIXA' and prod_id = pp.prod_id and s.conf_codigo = ip.conf_codigo) as apa_endereco_cxa, " +
                "(select a.apa_endereco from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'VENCIMENTO I' and prod_id = pp.prod_id and s.conf_codigo = ip.conf_codigo) as apa_endereco_venc_I, " +
                "(select a.apa_endereco from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'VENCIMENTO II' and prod_id = pp.prod_id and s.conf_codigo = ip.conf_codigo) as apa_endereco_venc_II, " +
                "(select a.apa_endereco from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'VENCIMENTO III' and prod_id = pp.prod_id and s.conf_codigo = ip.conf_codigo) as apa_endereco_venc_III, " +
                "(select a.apa_endereco from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'FLOWRACK' and prod_id = pp.prod_id and s.conf_codigo = ip.conf_codigo) as apa_endereco_flow, " +
                "prod_codigo, prod_descricao, ip.iped_quantidade, " +
                /*Qauntida de grandeza - quantidade reservada*/
                "trunc(iped_quantidade / pp.prod_fator_pulmao) as qtd_fechada, " +
                "(select sum(res_quantidade) from wms_reserva r where ped_codigo = ip.ped_codigo and prod_id = ip.prod_id and conf_codigo = ip.conf_codigo) as grandeza, " +
                "u.uni_unidade as uni_fechada, " +
                /*Quatidade em unidade - quantidade conferida no flowrack*/
                "mod(iped_quantidade, pp.prod_fator_pulmao) as fracionado, " +
                "(select distinct(iflow_qtd_conferida) from wms_rastreamento_flowrack where ped_codigo = ip.ped_codigo and prod_id = ip.prod_id and conf_codigo = ip.conf_codigo) as flow, " +
                "u1.uni_unidade as uni_fracionada, " +
                /*Ordem de separacao*/
                "(select a.apa_ordem from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'CAIXA' and prod_id = pp.prod_id and s.conf_codigo = ip.conf_codigo order by apa_ordem) as ordem_separacao_caixa, " +
                 "(select a.apa_ordem from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'FLOWRACK' and prod_id = pp.prod_id and s.conf_codigo = ip.conf_codigo order by apa_ordem) as ordem_separacao_flow " +
                "from wms_item_pedido ip " +
                "inner join wms_pedido p " +
                "on p.ped_codigo = ip.ped_codigo " +
                "inner join wms_produto pp " +
                "on pp.prod_id = ip.prod_id and pp.conf_codigo = ip.conf_codigo " +
                "left join wms_unidade u " +
                "on u.uni_codigo = pp.uni_codigo_pulmao " +
                "left join wms_unidade u1 " +
                "on u1.uni_codigo = pp.uni_codigo_picking ";

                if (codPedido > 0)
                {
                    select += "where p.ped_codigo = @codPedido and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                if (codManifesto > 0)
                {
                    select += "where p.mani_codigo = @codManifesto and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                if (naoConferido == true)
                {
                    select += "and ped_fim_conferencia is null ";
                }

                if (naoImpresso == true)
                {
                    select += "and ped_impresso is null ";
                }

                select += "order by ordem_separacao_caixa ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
                int i = 1;
                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItemMapaSeparacao itemPedido = new ItemMapaSeparacao();
                    //Controla a exibição do item no mapa de separação
                    int qtdFechada = 0, qtdFracionada = 0;

                    if (linha["tipo_vencimento_I"] != DBNull.Value && Convert.ToInt32(linha["ped_tabela"]) == Convert.ToInt32(linha["tipo_vencimento_I"]))
                    {
                        if (linha["apa_endereco_venc_I"] != DBNull.Value)
                        {
                            itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco_venc_I"]);
                        }
                        else
                        {
                            itemPedido.enderecoProduto = "SEM PICKING";
                        }
                    }
                    else if (linha["tipo_vencimento_II"] != DBNull.Value && Convert.ToInt32(linha["ped_tabela"]) == Convert.ToInt32(linha["tipo_vencimento_II"]))
                    {
                        if (linha["apa_endereco_venc_II"] != DBNull.Value)
                        {
                            itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco_venc_II"]);
                        }
                        else
                        {
                            itemPedido.enderecoProduto = "SEM PICKING";
                        }
                    }                    
                    else if (linha["tipo_vencimento_III"] != DBNull.Value && Convert.ToInt32(linha["ped_tabela"]) == Convert.ToInt32(linha["tipo_vencimento_III"]))
                    {
                        if (linha["apa_endereco_venc_III"] != DBNull.Value)
                        {
                            itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco_venc_III"]);
                        }
                        else
                        {
                            itemPedido.enderecoProduto = "SEM PICKING";
                        }
                    }
                    else if (linha["apa_endereco_cxa"] != DBNull.Value)
                    {
                        itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco_cxa"]);

                        itemPedido.ordem = Convert.ToString(linha["ordem_separacao_caixa"]);
                    }
                    else if (linha["apa_endereco_flow"] != DBNull.Value && linha["apa_endereco_cxa"] == DBNull.Value)
                    {
                        itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco_flow"]);

                        itemPedido.ordem = Convert.ToString(linha["ordem_separacao_flow"]);
                    }
                    else
                    {
                        itemPedido.enderecoProduto = "SEM PICKING";
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itemPedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    //s.sep_tipo, iped_qtd_flow 

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itemPedido.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itemPedido.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtd_fechada"] != DBNull.Value)
                    {
                        itemPedido.qtdCaixaProduto = Convert.ToInt32(linha["qtd_fechada"]);
                        //Recebe a quantidade do item
                        qtdFechada = Convert.ToInt32(linha["qtd_fechada"]);

                        //Verifica se existe grandezas reservada
                        if (linha["grandeza"] != DBNull.Value)
                        {
                            //Subtrai a grandeza na quantidade
                            itemPedido.qtdCaixaProduto = Convert.ToInt32(linha["qtd_fechada"]) - Convert.ToInt32(linha["grandeza"]);

                            //Recebe a quantidade do item
                            qtdFechada = Convert.ToInt32(linha["qtd_fechada"]) - Convert.ToInt32(linha["grandeza"]);
                        }
                    }

                    if (linha["uni_fechada"] != DBNull.Value)
                    {
                        itemPedido.uniCaixa = Convert.ToString(linha["uni_fechada"]);
                    }

                    if (linha["fracionado"] != DBNull.Value)
                    {
                        itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]);

                        qtdFracionada = Convert.ToInt32(linha["fracionado"]);

                        //Verifica se existe flow rack
                        if (linha["flow"] != DBNull.Value)
                        {
                            //Subtrai a quantidade do flow rack
                            itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]) - Convert.ToInt32(linha["flow"]);

                            qtdFracionada = Convert.ToInt32(linha["fracionado"]) - Convert.ToInt32(linha["flow"]);
                        }
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (qtdFechada + qtdFracionada > 0)
                    {
                        //Adiona o objêto a coleção
                        itemPedidoCollection.Add(itemPedido);
                    }
                    //Soma + 1 n contagem
                    i++;
                }
                //Retorna a coleção de cadastro encontrada
                return itemPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do pedido no relatório por manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os volumes de flow rack
        public FlowRackMapaSeparacaoCollection PesqVolumeFlowRack(string empresa, int codManifesto, int codPedido)
        {
            try
            {
                //Instância a camada de objêto
                FlowRackMapaSeparacaoCollection flowRackMapaSeparacaoCollection = new FlowRackMapaSeparacaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);

                /*Pesquisa o flow rack*/
                string select = "select distinct(a.apa_endereco) as endereco, i.ped_codigo, " +
                                /*Verifica quantos volumes existem sem endereço*/
                                "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack " +
                                "where ped_codigo = i.ped_codigo and apa_codigo is null and iflow_qtd_conferida > 0 and conf_codigo = i.conf_codigo) as flow_sem_endereco, " +
                                /*Verifica quantos volumes existem no endereço*/
                                "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack " +
                                "where ped_codigo = i.ped_codigo and apa_codigo = i.apa_codigo and iflow_qtd_conferida > 0 and conf_codigo = i.conf_codigo) as qtd_flow_endereco, " +
                                /*Verifica quantos volumes existem*/
                                "(select count(distinct(iflow_numero)) from wms_rastreamento_flowrack " +
                                "where ped_codigo = i.ped_codigo and iflow_qtd_conferida > 0 and conf_codigo = i.conf_codigo) as cont_flow " +
                                "from wms_rastreamento_flowrack i " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = i.apa_codigo " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo ";



                if (codPedido > 0)
                {
                    select += "where p.ped_codigo = @codPedido and i.iflow_qtd_conferida >  0 and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                              "group by apa_endereco, i.ped_codigo, i.apa_codigo, i.conf_codigo, apa_ordem " +
                              "order by apa_ordem ";
                }

                if (codManifesto > 0)
                {
                    select += "where mani_codigo = @codManifesto and i.iflow_qtd_conferida >  0 and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                              "group by apa_endereco, i.ped_codigo, i.apa_codigo, i.conf_codigo, apa_ordem " +
                              "order by apa_ordem ";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    FlowRackMapaSeparacao flowRackMapaSeparacao = new FlowRackMapaSeparacao();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        flowRackMapaSeparacao.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }


                    if (linha["qtd_flow_endereco"] != DBNull.Value)
                    {
                        flowRackMapaSeparacao.qtdFlowEndereco = Convert.ToInt32(linha["qtd_flow_endereco"]);

                    }

                    if (linha["cont_flow"] != DBNull.Value)
                    {
                        flowRackMapaSeparacao.totalVolume = Convert.ToInt32(linha["cont_flow"]);

                    }

                    //Fica por útimo para manter o controle dos volumes não endereçados
                    if (linha["endereco"] != DBNull.Value)
                    {
                        flowRackMapaSeparacao.enderecoVolume = Convert.ToString(linha["endereco"]);
                    }
                    else
                    {
                        //Quantidade de volume sem endereço
                        flowRackMapaSeparacao.qtdFlowEndereco = Convert.ToInt32(linha["flow_sem_endereco"]);
                        //Informa que está sem endereço
                        flowRackMapaSeparacao.enderecoVolume = "NÃO ENDEREÇADO(S)";
                    }

                    //Adiona o objêto a coleção
                    flowRackMapaSeparacaoCollection.Add(flowRackMapaSeparacao);

                }
                //Retorna a coleção de cadastro encontrada
                return flowRackMapaSeparacaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os volumes do flow rack. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa as grandezas
        public GrandezaMapaSeparacaoCollection PesqGrandeza(string empresa, int codManifesto, int codPedido)
        {
            try
            {
                //Instância a camada de objêto
                GrandezaMapaSeparacaoCollection granzezasColletion = new GrandezaMapaSeparacaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);

                /*Pesquisa as grandezas*/
                string select = "select rs.ped_codigo, a.apa_endereco, prod_codigo || ' - ' ||prod_descricao as desc_produto, rs.res_quantidade, u.uni_unidade, rs.res_validade, rs.res_peso, rs.res_lote " +
                                "from wms_reserva rs " +
                                "inner join wms_produto p " +
                                "on p.prod_id = rs.prod_id and p.conf_codigo = rs.conf_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = rs.apa_codigo " +
                                "inner join wms_pedido pd " +
                                "on pd.ped_codigo = rs.ped_codigo ";


                if (codPedido > 0)
                {
                    select += "where pd.ped_codigo = @codPedido and pd.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                              "order by a.apa_ordem ";
                }

                if (codManifesto > 0)
                {
                    select += "where pd.mani_codigo = @codManifesto and pd.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                              "order by a.apa_ordem ";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    GrandezaMapaSeparacao grandezaMapaSeparacao = new GrandezaMapaSeparacao();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        grandezaMapaSeparacao.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        grandezaMapaSeparacao.enderecoProduto = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["desc_produto"] != DBNull.Value)
                    {
                        grandezaMapaSeparacao.descProduto = Convert.ToString(linha["desc_produto"]);

                    }

                    if (linha["res_quantidade"] != DBNull.Value)
                    {
                        grandezaMapaSeparacao.qtdProduto = Convert.ToInt32(linha["res_quantidade"]);

                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        grandezaMapaSeparacao.unidadeProduto = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["res_validade"] != DBNull.Value)
                    {
                        grandezaMapaSeparacao.validadeProduto = Convert.ToDateTime(linha["res_validade"]).Date;

                    }

                    if (linha["res_peso"] != DBNull.Value)
                    {
                        grandezaMapaSeparacao.pesoProduto = Convert.ToInt32(linha["res_peso"]);

                    }

                    if (linha["res_lote"] != DBNull.Value)
                    {
                        grandezaMapaSeparacao.loteProduto = Convert.ToString(linha["res_lote"]);

                    }

                    //Adiona o objêto a coleção
                    granzezasColletion.Add(grandezaMapaSeparacao);

                }
                //Retorna a coleção de cadastro encontrada
                return granzezasColletion;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as grandezas. \nDetalhes:" + ex.Message);
            }
        }



        //Pesquisa os itens do pedido a pedido
        public ItemMapaSeparacaoCollection PesqItemResumoManifesto(int codManifesto)
        {
            try
            {
                //Instância a camada de objêto
                ItemMapaSeparacaoCollection itemPedidoCollection = new ItemMapaSeparacaoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select apa_endereco, prod_codigo, prod_descricao, sum(ip.iped_quantidade), " +
                "trunc(sum(ip.iped_quantidade) / pp.prod_fator_pulmao) as qtd_fechada, u.uni_unidade as uni_fechado" +
                "mod(sum(ip.iped_quantidade), pp.prod_fator_pulmao) as qtd_fracionado, u1.uni_unidade as uni_fraciondada " +
                "sum(res_quantidade) as qtd_reservada " +
                "from wms_item_pedido ip " +
                "inner join wms_pedido p " +
                "on p.ped_codigo = ip.ped_codigo " +
                "inner join wms_produto pp " +
                "on pp.prod_id = ip.prod_id " +
                "left join wms_unidade u " +
                "on u.uni_codigo = pp.uni_codigo_pulmao " +
                "left join wms_unidade u1 " +
                "on u1.uni_codigo = pp.uni_codigo_picking " +
                "left join  wms_separacao s " +
                "on s.prod_id = ip.prod_id " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_reserva r " +
                "on r.ped_codigo = p.ped_codigo " +
                "where p.mani_codigo = @codManifesto and sep_tipo = 'CAIXA' " +
                "group by apa_endereco, prod_codigo, prod_descricao, prod_fator_pulmao, " +
                "u.uni_unidade, u1.uni_unidade, apa_ordem " +
                "order by apa_ordem ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
                int i = 1;
                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItemMapaSeparacao itemPedido = new ItemMapaSeparacao();
                    //Controla a exibição do item no mapa de separação
                    int qtdFechada = 0, qtdFracionada = 0;

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco"]);
                    }
                    else
                    {
                        itemPedido.enderecoProduto = "SEM PICKING";
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itemPedido.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itemPedido.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtd_fechada"] != DBNull.Value)
                    {
                        itemPedido.qtdCaixaProduto = Convert.ToInt32(linha["qtd_fechada"]);
                        //Recebe a quantidade do item
                        qtdFechada = Convert.ToInt32(linha["qtd_fechada"]);

                        //Verifica se existe grandezas reservada
                        if (linha["qtd_reservada"] != DBNull.Value)
                        {
                            //Subtrai a grandeza na quantidade
                            itemPedido.qtdCaixaProduto = Convert.ToInt32(linha["qtd_fechada"]) - Convert.ToInt32(linha["qtd_reservada"]);

                            //Recebe a quantidade do item
                            qtdFechada = Convert.ToInt32(linha["qtd_fechada"]) - Convert.ToInt32(linha["qtd_reservada"]);
                        }
                    }

                    if (linha["uni_fechada"] != DBNull.Value)
                    {
                        itemPedido.uniCaixa = Convert.ToString(linha["uni_fechada"]);
                    }

                    if (linha["qtd_fracionado"] != DBNull.Value)
                    {
                        itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["qtd_fracionado"]);
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (qtdFechada + qtdFracionada > 0)
                    {
                        //Adiona o objêto a coleção
                        itemPedidoCollection.Add(itemPedido);
                    }
                    //Soma + 1 n contagem
                    i++;
                }
                //Retorna a coleção de cadastro encontrada
                return itemPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens por resumo de manifesto. \nDetalhes:" + ex.Message);
            }
        }



        //Sequencia os pedidos para o carregamento
        public void SequenciaPedido(string codPedido, int sequencia)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@sequencia", sequencia);

                //Atualiza a sequencia de pedidos
                string update = "update wms_pedido  ped_seq_carregamento = @sequencia " +
                                "where ped_codigo = @codPedido";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, update);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a sequência de carregamento. \nDetalhes:" + ex.Message);
            }
        }


    }
}
