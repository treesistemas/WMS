using System;
using System.Data;
using Dados;
using ObjetoTransferencia;


namespace Negocios
{
    public class ConsultaPedidoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o pedido
        public Pedido PesqPedido(int codPedido, string empresa)
        {
            try
            {
                //Instância o objêto
                Pedido pedido = new Pedido();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //adiciona parâmetros
                conexao.AdicionarParamentros("@codigo", codPedido);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select ped_data, ped_codigo, cf.cfop_codigo, cf.cfop_descricao, tp.tipo_codigo, tp.tipo_descricao, " +
                                "pr.prazo_codigo, pr.prazo_descricao, pg.pag_codigo, pg.pag_descricao, ped.ped_total, ped.ped_peso, ped_observacao,  " +
                                "c.cli_codigo, c.cli_nome, c.cli_fantasia, c.cli_endereco, c.cli_numero, " +
                                "b.bar_nome, cid.cid_nome, e.est_uf, rota_numero, rota_descricao, rep.rep_codigo, rep.rep_nome " +
                                "from wms_pedido ped " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = ped.cli_id and c.conf_codigo = ped.conf_codigo " +
                                "left join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "left join wms_cidade cid " +
                                "on cid.cid_codigo = b.cid_codigo " +
                                "left join wms_estado e " +
                                "on e.est_codigo = cid.est_codigo " +
                                "left join wms_cfop cf " +
                                "on cf.cfop_codigo = ped.cfop_codigo and cf.conf_codigo = ped.conf_codigo " +
                                "left join wms_tipo_pedido tp " +
                                "on tp.tipo_codigo = ped.tipo_codigo and tp.conf_codigo = ped.conf_codigo " +
                                "left join wms_prazo pr " +
                                "on pr.prazo_codigo = ped.prazo_codigo and pr.conf_codigo = ped.conf_codigo " +
                                "left join wms_pagamento pg " +
                                "on pg.pag_codigo = ped.pag_codigo and pg.conf_codigo = ped.conf_codigo " +
                                "left join wms_representante rep " +
                                "on rep.rep_codigo = ped.rep_codigo and rep.conf_codigo = ped.conf_codigo " +
                                "left join wms_rota r " +
                                "on r.rota_codigo = c.rota_codigo and r.conf_codigo = c.conf_codigo " +
                                "where ped_codigo = @codigo and ped.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["ped_data"] != DBNull.Value)
                    {
                        pedido.dataPedido = Convert.ToDateTime(linha["ped_data"]).Date;
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["cfop_codigo"] != DBNull.Value)
                    {
                        pedido.cfop = Convert.ToString("[" + linha["cfop_codigo"] + "]") + " - " + Convert.ToString(linha["cfop_descricao"]);
                    }

                    if (linha["tipo_codigo"] != DBNull.Value)
                    {
                        pedido.tipoPedido = Convert.ToString("[" + linha["tipo_codigo"] + "]") + " - " + Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["prazo_codigo"] != DBNull.Value)
                    {
                        pedido.prazo = Convert.ToString("[" + linha["prazo_codigo"] + "]") + " - " + Convert.ToString(linha["prazo_descricao"]);
                    }

                    if (linha["pag_codigo"] != DBNull.Value)
                    {
                        pedido.formaPagamento = Convert.ToString("[" + linha["prazo_codigo"] + "]") + " - " + Convert.ToString(linha["pag_descricao"]);
                    }

                    if (linha["ped_total"] != DBNull.Value)
                    {
                        pedido.totalPedido = Convert.ToDouble(linha["ped_total"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        pedido.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                    if (linha["ped_observacao"] != DBNull.Value)
                    {
                        pedido.observacaoPedido = Convert.ToString(linha["ped_observacao"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        pedido.nomeCliente = Convert.ToString("[" + linha["cli_codigo"] + "]") + " - " + Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        pedido.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        pedido.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        pedido.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        pedido.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        pedido.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        pedido.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        pedido.rotaCliente = Convert.ToString("[" + linha["rota_numero"] + "]") + " - " + Convert.ToString(linha["rota_descricao"]);
                    }

                    if (linha["rep_codigo"] != DBNull.Value)
                    {
                        pedido.representante = Convert.ToString("[" + linha["rep_codigo"] + "]") + " - " + Convert.ToString(linha["rep_nome"]);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as informações do pedido. \n Detalhes:" + ex.Message);
            }
        }

        //Pesquisa o pedido
        public ItensPedidoCollection PesqItem(int codPedido, string empresa)
        {
            try
            {
                //Instância o objêto
                ItensPedidoCollection itemPedidoCollection = new ItensPedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //adiciona parâmetros
                conexao.AdicionarParamentros("@codigo", codPedido);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select p.ped_codigo, " +
                "(select a.apa_ordem from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'CAIXA' and prod_id = pp.prod_id and s.conf_codigo = pp.conf_codigo order by apa_ordem) as ordem_separacao, " +
                "(select a.apa_endereco from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where sep_tipo = 'CAIXA' and prod_id = pp.prod_id and s.conf_codigo = pp.conf_codigo) as apa_endereco_cxa, " +
                "(select a.apa_endereco from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo  " +
                "where sep_tipo = 'FLOWRACK' and prod_id = pp.prod_id and s.conf_codigo = pp.conf_codigo) as apa_endereco_flow, " +
                "prod_codigo, prod_descricao, ip.iped_quantidade, " +
                /*Qauntida de grandeza - quantidade reservada*/
                "trunc(iped_quantidade / pp.prod_fator_compra) as qtd_fechada, " +
                "u.uni_unidade as uni_fechada, " +
                /*Quatidade em unidade - quantidade conferida no flowrack*/
                "mod(iped_quantidade, pp.prod_fator_compra) as fracionado, " +
                "u1.uni_unidade as uni_fracionada, iped_valor, iped_peso " +
                "from wms_item_pedido ip " +
                "inner join wms_pedido p " +
                "on p.ped_codigo = ip.ped_codigo and p.conf_codigo = ip.conf_codigo " +
                "inner join wms_produto pp " +
                "on pp.prod_id = ip.prod_id and pp.conf_codigo = ip.conf_codigo " +
                "left join wms_unidade u " +
                "on u.uni_codigo = pp.uni_codigo_pulmao " +
                "left join wms_unidade u1 " +
                "on u1.uni_codigo = pp.uni_codigo_picking " +
               "where ip.ped_codigo = @codigo and ip.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                "order by ordem_separacao";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItensPedido itemPedido = new ItensPedido();

                    if (linha["apa_endereco_cxa"] != DBNull.Value)
                    {
                        itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco_cxa"]);
                    }
                    else if (linha["apa_endereco_flow"] != DBNull.Value && linha["apa_endereco_cxa"] == DBNull.Value)
                    {
                        itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco_flow"]);
                    }
                    else
                    {
                        itemPedido.enderecoProduto = "Sem Picking";
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itemPedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itemPedido.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itemPedido.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iped_quantidade"] != DBNull.Value)
                    {
                        itemPedido.qtdProduto = Convert.ToInt32(linha["iped_quantidade"]);        
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (linha["iped_valor"] != DBNull.Value)
                    {
                        itemPedido.valorTotal = Convert.ToDouble(linha["iped_valor"]);

                        itemPedido.valorUnitario = Convert.ToDouble(linha["iped_valor"]) / Convert.ToInt32(linha["iped_quantidade"]);
                    }

                    if (linha["iped_peso"] != DBNull.Value)
                    {
                        itemPedido.pesoTotal = Convert.ToDouble(linha["iped_peso"]);
                    }

                    if (linha["qtd_fechada"] != DBNull.Value)
                    {
                        itemPedido.qtdCaixaProduto = Convert.ToString(linha["qtd_fechada"]);
                    }

                    if (linha["uni_fechada"] != DBNull.Value)
                    {
                        itemPedido.uniCaixa = Convert.ToString(linha["uni_fechada"]);
                    }

                    if (linha["fracionado"] != DBNull.Value)
                    {
                        itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]);
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    //Adiona o objêto a coleção
                    itemPedidoCollection.Add(itemPedido);

                }
                //Retorna a coleção de cadastro encontrada
                return itemPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens do pedido. \nDetalhes:" + ex.Message);
            }
        }
    }
}
