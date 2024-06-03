using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class PedidoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();
        //Pesquisa um novo id da categoria
        public PedidoCollection PesqPedidoManifesto(int codManifesto, string letra)
        {
            try
            {
                //Instância a camada de objêto
                PedidoCollection pedidoCollection = new PedidoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                "p.mani_codigo, mani_letra, v.vei_placa, r.rota_numero || ' - ' || r.rota_descricao as rota, p.ped_codigo, " +
                "p.ped_data, c.cli_compartilhada, c.cli_validade, c.cli_validade_dias, c.cli_paletizado, " +
                "c.cli_caixa_fechada, c.cli_nao_dividir_carga, c.cli_observacao, " +
                "p.ped_flow_rack, p.ped_peso, p.ped_observacao " +
                "from wms_pedido p " +
                "left join wms_manifesto m " +
                "on m.mani_codigo = p.mani_codigo " +
                "left join wms_cliente c " +
                "on c.cli_id = p.cli_id " +
                "left join wms_rota r " +
                "on r.rota_codigo = c.rota_codigo " +
                "left join wms_veiculo v " +
                "on v.vei_codigo = m.vei_codigo " +
                "where p.mani_codigo = @codManifesto";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
                int i = 1;
                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    Pedido pedido = new Pedido();

                    //pedido.sequenciaPedido = i;
                    //pedido.letraManifesto = "XTV";

                    if (linha["empresa"] != DBNull.Value)
                    {
                        pedido.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["rota"] != DBNull.Value)
                    {
                    //    pedido.rotaPedido = Convert.ToString(linha["rota"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                      //  pedido.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["ped_data"] != DBNull.Value)
                    {
                        pedido.dataPedido = Convert.ToDateTime(linha["ped_data"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        pedido.manifestoPedido = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        //pedido.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                   // pedido.cubagemPedido = 0.00;

                    if (linha["cli_compartilhada"] != DBNull.Value)
                    {
                      //  pedido.rotaCompartilhada = Convert.ToString(linha["cli_compartilhada"]);
                    }
                    else
                    {
                       // pedido.rotaCompartilhada = "NÃO";
                    }

                    if (linha["cli_paletizado"] != DBNull.Value)
                    {
                       // pedido.exigePaletizacaoCliente = Convert.ToString(linha["cli_paletizado"]);
                    }
                    else
                    {
                       // pedido.exigePaletizacaoCliente = "NÃO";
                    }

                    if (linha["cli_validade"] != DBNull.Value)
                    {
                       // pedido.exigeDataCliente = Convert.ToString(linha["cli_validade"]);
                    }
                    else
                    {
                       // pedido.exigeDataCliente = "NÃO";
                    }

                    if (linha["cli_validade_dias"] != DBNull.Value)
                    {
                       // pedido.dataExigidaCliente = Convert.ToDateTime(DateTime.Now).AddDays(Convert.ToInt32(linha["cli_validade_dias"]));
                    }
                    else
                    {
                       // pedido.dataExigidaCliente = Convert.ToDateTime(DateTime.Now).AddDays(0);
                    }

                    if (linha["cli_caixa_fechada"] != DBNull.Value)
                    {
                       // pedido.exigeCxaFechadaCliente = Convert.ToString(linha["cli_caixa_fechada"]);
                    }
                    else
                    {
                       // pedido.exigeCxaFechadaCliente = "NÃO";
                    }

                    if (linha["ped_flow_rack"] != DBNull.Value)
                    {
                       // pedido.flowRackPedido = Convert.ToString(linha["ped_flow_rack"]);
                    }
                    else
                    {
                       // pedido.flowRackPedido = "NÃO";
                    }

                    //pedido.naoAceitaDividirCargaCliente = "SIM";

                    if (linha["cli_observacao"] != DBNull.Value)
                    {
                       // pedido.observacaoCliente = Convert.ToString(linha["cli_observacao"]);
                    }

                    if (linha["ped_observacao"] != DBNull.Value)
                    {
                       // pedido.observacaoPedido = Convert.ToString(linha["ped_observacao"]);
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



        /*public RelatorioPedidoCollection PesqPedidoManifesto(int codManifesto)
        {
            try
            {
                //Instância a camada de objêto
                RelatorioPedidoCollection pedidoCollection = new RelatorioPedidoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select e.est_uf, cid.cid_nome, cid_sequencia, b.bar_nome, c.cli_endereco,  " +
                    "c.cli_numero, c.cli_sequencia,  c.cli_codigo, c.cli_nome, c.cli_fantasia, v.vei_placa, " +
                    "p.mani_codigo, p.ped_codigo, p.ped_data, " +
                    "(select conf_empresa from wms_configuracao ) as empresa " +
                    "from wms_pedido p  " +
                    "left join wms_cliente c " +
                    "on c.cli_id = p.cli_id " +
                    "left join wms_bairro b " +
                    "on b.bar_codigo = c.bar_codigo " +
                    "left join wms_cidade cid " +
                    "on cid.cid_codigo = b.cid_codigo " +
                    "left join wms_estado e  " +
                    "on e.est_codigo = cid.est_codigo " +
                    "left join wms_manifesto m " +
                    "on m.mani_codigo = p.mani_codigo " +
                    "left join wms_veiculo v  " +
                    "on v.vei_codigo = m.vei_codigo " +
                    "where p.mani_codigo = @codManifesto " +
                    "order by est_uf, cid_nome, cid_sequencia desc, bar_nome, cli_sequencia desc";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    RelatorioPedido pedido = new RelatorioPedido();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        pedido.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        pedido.codCliente = Convert.ToString(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        pedido.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        pedido.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        pedido.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        pedido.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        pedido.bairro = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        pedido.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        pedido.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["ped_data"] != DBNull.Value)
                    {
                        pedido.dataPedido = Convert.ToDateTime(linha["ped_data"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        pedido.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        pedido.placaVeiculo = Convert.ToString(linha["vei_placa"]);
                    }

                    pedidoCollection.Add(pedido);
                }
                //Retorna a coleção de cadastro encontrada
                return pedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório por manifesto. \nDetalhes:" + ex.Message);
            }
        }
        */

        public PedidoCollection PesqItemPedido(int codManifesto, bool controlaChocolate)
        {
            try
            {
                //Instância a camada de objêto
                PedidoCollection pedidoCollection = new PedidoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                "p.mani_codigo, mani_letra, v.vei_placa, r.rota_numero || ' - ' || r.rota_descricao as rota, p.ped_codigo, " +
                "p.ped_data, c.cli_compartilhada, c.cli_validade, c.cli_validade_dias, c.cli_paletizado, " +
                "c.cli_caixa_fechada, c.cli_nao_dividir_carga, c.cli_observacao, " +
                "p.ped_flow_rack, p.ped_peso, p.ped_observacao " +
                "from wms_pedido p " +
                "left join wms_manifesto m " +
                "on m.mani_codigo = p.mani_codigo " +
                "left join wms_cliente c " +
                "on c.cli_id = p.cli_id " +
                "left join wms_rota r " +
                "on r.rota_codigo = c.rota_codigo " +
                "left join wms_veiculo v " +
                "on v.vei_codigo = m.vei_codigo " +
                "where p.mani_codigo = @codManifesto";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
                int i = 1;
                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    Pedido pedido = new Pedido();

                    //pedido.sequenciaPedido = i;
                    //pedido.letraManifesto = "XTV";

                    if (linha["empresa"] != DBNull.Value)
                    {
                        pedido.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["rota"] != DBNull.Value)
                    {
                        //    pedido.rotaPedido = Convert.ToString(linha["rota"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        //  pedido.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["ped_data"] != DBNull.Value)
                    {
                        pedido.dataPedido = Convert.ToDateTime(linha["ped_data"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        pedido.manifestoPedido = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        //pedido.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                    // pedido.cubagemPedido = 0.00;

                    if (linha["cli_compartilhada"] != DBNull.Value)
                    {
                        //  pedido.rotaCompartilhada = Convert.ToString(linha["cli_compartilhada"]);
                    }
                    else
                    {
                        // pedido.rotaCompartilhada = "NÃO";
                    }

                    if (linha["cli_paletizado"] != DBNull.Value)
                    {
                        // pedido.exigePaletizacaoCliente = Convert.ToString(linha["cli_paletizado"]);
                    }
                    else
                    {
                        // pedido.exigePaletizacaoCliente = "NÃO";
                    }

                    if (linha["cli_validade"] != DBNull.Value)
                    {
                        // pedido.exigeDataCliente = Convert.ToString(linha["cli_validade"]);
                    }
                    else
                    {
                        // pedido.exigeDataCliente = "NÃO";
                    }

                    if (linha["cli_validade_dias"] != DBNull.Value)
                    {
                        // pedido.dataExigidaCliente = Convert.ToDateTime(DateTime.Now).AddDays(Convert.ToInt32(linha["cli_validade_dias"]));
                    }
                    else
                    {
                        // pedido.dataExigidaCliente = Convert.ToDateTime(DateTime.Now).AddDays(0);
                    }

                    if (linha["cli_caixa_fechada"] != DBNull.Value)
                    {
                        // pedido.exigeCxaFechadaCliente = Convert.ToString(linha["cli_caixa_fechada"]);
                    }
                    else
                    {
                        // pedido.exigeCxaFechadaCliente = "NÃO";
                    }

                    if (linha["ped_flow_rack"] != DBNull.Value)
                    {
                        // pedido.flowRackPedido = Convert.ToString(linha["ped_flow_rack"]);
                    }
                    else
                    {
                        // pedido.flowRackPedido = "NÃO";
                    }

                    //pedido.naoAceitaDividirCargaCliente = "SIM";

                    if (linha["cli_observacao"] != DBNull.Value)
                    {
                        // pedido.observacaoCliente = Convert.ToString(linha["cli_observacao"]);
                    }

                    if (linha["ped_observacao"] != DBNull.Value)
                    {
                        // pedido.observacaoPedido = Convert.ToString(linha["ped_observacao"]);
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


        //Pesquisa o pedido para a conferência automática
        public Pedido PesqPedido(string codPedido, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@empresa", empresa);

                //Instância o objêto
                Pedido pedido = new Pedido();

                //String de consulta
                string select = "select cli_codigo, cli_nome, ped_fim_conferencia from wms_pedido p " +
                    "inner join wms_cliente c " +
                    "on c.cli_id = p.cli_id " +
                    "where ped_codigo = @codPedido and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        pedido.nomeCliente = linha["cli_codigo"] + " - " + linha["cli_nome"];
                    }

                    if (linha["ped_fim_conferencia"] != DBNull.Value)
                    {
                        pedido.fimConferencia = Convert.ToDateTime(linha["ped_fim_conferencia"]);
                    }
                }

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o pedido. Detalhes:\n" + ex.Message);
            }
        }

        //Atualiza o status do pedido
        public void StatusPedido(int codPedido, string motivo, int codUsuario, DateTime? dataInicial, DateTime? dataFinal, string processo, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@motivo", motivo);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@dataInicial", dataInicial);
                conexao.AdicionarParamentros("@dataFinal", dataFinal);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de atualização - Anula conferência
                string update = "update wms_pedido set ped_inicio_conferencia = @dataInicial, ped_fim_conferencia = @dataFinal, " +
                                "usu_codigo_conferente = @codUsuario, ped_motivo = 'CONF. AUTOMATICA: ' || @motivo where ped_codigo = @codPedido " +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                if (processo.Equals("desconferido"))
                {
                    //String de atualização - Zera a conferencia do item
                    string updateSeparacao = "update wms_separacao s set sep_estoque = sep_estoque - (select iped_qtd_conferencia from wms_item_pedido i where i.ped_codigo = @codPedido and i.prod_id = s.prod_id) " +
                        "where prod_id in (select prod_id from wms_item_pedido i where i.ped_codigo = @codPedido ) and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and sep_tipo = 'CAIXA'";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, updateSeparacao);

                    //String de atualização - Zera a conferencia do item
                    string updateItem = "update wms_item_pedido i set i.iped_qtd_conferencia = null, i.iped_data_conferencia = null where ped_codigo = @codPedido " +
                                        "and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and not iped_qtd_conferencia is null ";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, updateItem);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o status da conferência do pedido! \nDetalhes:" + ex.Message);
            }
        }

    }
}
