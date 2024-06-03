using Dados;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Expedicao
{
    public class RoteirizadorNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o pedido
        public PedidoCollection PesqPedido(string dataInicial, string dataFinal, string horario, int codRota, string status)
        {
            try
            {
                //Instância a camada de objetos
                PedidoCollection pedidoCollection = new PedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona prâmetros
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " " + horario);
                conexao.AdicionarParamentros("@codRota", codRota);
                conexao.AdicionarParamentros("@status", status);

                //String de consulta
                string select = "select coalesce(ped_nota_fiscal,0) as ped_nota_fiscal, 1 as ped_nota_serie, coalesce(p.ped_data_nota_fiscal, '01/01/2000') as ped_data_nota_fiscal, " +
                                "p.ped_data, ped_codigo, p.ped_total, t.tipo_descricao, pg.pag_descricao, 1 as ped_status, 'S' as ped_forma_carga, ped_peso, coalesce(ped_cubagem, 0) as ped_cubagem, " +
                                "ped_observacao, 0 as ped_valor_substituicao, c.cli_codigo, (select conf_codigo from wms_configuracao) as cod_filial, " +
                                "ped_codigo, ped_codigo, p.rep_codigo, 'N' as restricao_vei, 'S' as bonif_entrega, null as praca_codigo, null as praca_descricao, r.rota_numero, r.rota_descricao, " +
                                "p.ped_itens, mani_codigo, 'Entrega' as tipo_entrega, 'S' as endereco_padrao " +
                                "from wms_pedido p " +
                                "inner join wms_pagamento pg " +
                                "on p.pag_codigo = pg.pag_codigo " +
                                "inner join wms_tipo_pedido t " +
                                "on t.tipo_codigo = p.tipo_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "left join wms_rota r " +
                                "on r.rota_codigo = c.rota_codigo " +
                                "where ped_data between @dataInicial and @dataFinal ";

                if (status.Equals("CARTEIRA"))
                {
                    select += "and ped_nota_fiscal is null ";
                }

                if (status.Equals("FATURADOS"))
                {
                    select += "and not ped_nota_fiscal is null ";
                }

                if (codRota > 0)
                {
                    select += "and r.rota_numero = @codRota ";
                }

                select += "and ped_excluido is null and ped_data_devolucao is null " +
                                "and not p.tipo_codigo in (select tipo_codigo from wms_tipo_pedido v " +
                                "where tipo_descricao like '%DEVOLUCAO%' or tipo_descricao like '%TROCA%' " +
                                "or tipo_descricao like '%AVARIA%' or tipo_descricao like '%BAIXA%' " +
                                "or tipo_descricao like '%SUPERMERCADO%' or tipo_descricao like '%TRANSFERENCIA%' " +
                                "or tipo_descricao like '%DEV%' or tipo_descricao like '%COMPRAS%' " +
                                "or tipo_descricao like '%CONSUMO%' or tipo_descricao like '%CORRECAO%' " +
                                "or tipo_descricao like '%CONTA%' " +
                                "or tipo_descricao like '%CONSIGNACAO%') " +
                                "order by ped_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Pedido pedido = new Pedido();
                    //Adiciona os valores encontrados
                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        pedido.notaFiscal = Convert.ToInt32(linha["ped_nota_fiscal"]);
                    }

                    if (linha["ped_nota_serie"] != DBNull.Value)
                    {
                        pedido.serieNotaFiscal = Convert.ToInt32(linha["ped_nota_serie"]);
                    }

                    if (linha["ped_data_nota_fiscal"] != DBNull.Value)
                    {
                        pedido.dataFaturamento = Convert.ToDateTime(linha["ped_data_nota_fiscal"]);
                    }

                    if (linha["ped_data"] != DBNull.Value)
                    {
                        pedido.dataPedido = Convert.ToDateTime(linha["ped_data"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        pedido.tipoPedido = Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["ped_total"] != DBNull.Value)
                    {
                        pedido.totalPedido = Convert.ToDouble(linha["ped_total"]);
                    }

                    if (linha["pag_descricao"] != DBNull.Value)
                    {
                        pedido.formaPagamento = Convert.ToString(linha["pag_descricao"]);
                    }

                    if (linha["ped_status"] != DBNull.Value)
                    {
                        pedido.pedStatus = Convert.ToString(linha["ped_status"]);

                    }


                    if (linha["ped_forma_carga"] != DBNull.Value)
                    {
                        pedido.pedFormaCarga = Convert.ToString(linha["ped_forma_carga"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        pedido.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                    if (linha["ped_cubagem"] != DBNull.Value)
                    {
                        pedido.cubagemPedido = Convert.ToDouble(linha["ped_cubagem"]);
                    }

                    if (linha["ped_observacao"] != DBNull.Value)
                    {
                        pedido.observacaoPedido = Convert.ToString(linha["ped_observacao"]);
                    }

                    if (linha["ped_valor_substituicao"] != DBNull.Value)
                    {
                        pedido.valorSubstituicaoPedido = Convert.ToDouble(linha["ped_valor_substituicao"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        pedido.codCliente = Convert.ToInt32(linha["cli_codigo"]);
                    }

                    if (linha["cod_filial"] != DBNull.Value)
                    {
                        pedido.codFilial = Convert.ToInt32(linha["cod_filial"]);
                    }

                    if (linha["rep_codigo"] != DBNull.Value)
                    {
                        pedido.representante = Convert.ToString(linha["rep_codigo"]);
                    }

                    if (linha["restricao_vei"] != DBNull.Value)
                    {
                        pedido.restricaoveiculoPedido = Convert.ToString(linha["restricao_vei"]);
                    }

                    if (linha["bonif_entrega"] != DBNull.Value)
                    {
                        pedido.bonificacaoEntrega = Convert.ToString(linha["bonif_entrega"]);
                    }

                    if (linha["praca_codigo"] != DBNull.Value)
                    {
                        pedido.codPracaCliente = Convert.ToInt32(linha["praca_codigo"]);
                    }

                    if (linha["praca_descricao"] != DBNull.Value)
                    {
                        pedido.descPracaCliente = Convert.ToString(linha["praca_descricao"]);
                    }

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        pedido.codRotaCliente = Convert.ToInt32(linha["rota_numero"]);
                    }

                    if (linha["rota_descricao"] != DBNull.Value)
                    {
                        pedido.rotaCliente = Convert.ToString(linha["rota_descricao"]);
                    }

                    if (linha["ped_itens"] != DBNull.Value)
                    {
                        pedido.qtdItensPedido = Convert.ToInt32(linha["ped_itens"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        pedido.manifestoPedido = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["tipo_entrega"] != DBNull.Value)
                    {
                        pedido.tipoServico = Convert.ToString(linha["tipo_entrega"]);
                    }

                    if (linha["endereco_padrao"] != DBNull.Value)
                    {
                        pedido.enderecoPadraoCliente = Convert.ToString(linha["endereco_padrao"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    pedidoCollection.Add(pedido);
                }
                //Retorna a coleção de cadastro encontrada
                return pedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os pedidos. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o pedido
        public ClienteCollection PesqCliente(string dataInicial, string dataFinal, string horario, int codRota, string status)
        {
            try
            {
                //Instância a camada de objetos
                ClienteCollection clienteCollection = new ClienteCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona prâmetros
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " " + horario);
                conexao.AdicionarParamentros("@codRota", codRota);
                conexao.AdicionarParamentros("@status", status);

                //String de consulta
                string select = "select distinct(c.cli_codigo) as cli_codigo,  c.cli_fantasia, c.cli_nome, c.cli_cnpj, c.cli_cpf, c.cli_endereco, b.bar_nome, e.est_uf, ci.cid_nome, c.cli_cep, " +
                                "c.cli_data_cadastro, c.cli_fone, c.cli_celular, " +
                                "cli_email, a.ati_codigo, a.ati_descricao, (select conf_codigo from wms_configuracao where conf_cnpj is null) as cod_filial, " +
                                "null as praca_codigo, null as praca_descricao, r.rota_numero, r.rota_descricao, c.cli_numero, c.cli_latitude, c.cli_longitude " +
                                "from wms_pedido p " +
                                "inner join wms_pagamento pg " +
                                "on p.pag_codigo = pg.pag_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade ci " +
                                "on ci.cid_codigo = b.cid_codigo " +
                                "inner join wms_estado e " +
                                "on e.est_codigo = ci.est_codigo " +
                                "left join wms_atividade a " +
                                "on a.ati_codigo = c.ati_codigo " +
                                "left join wms_rota r " +
                                "on r.rota_codigo = c.rota_codigo " +
                                "where ped_data between @dataInicial and @dataFinal ";

                if (status.Equals("CARTEIRA"))
                {
                    select += "and ped_nota_fiscal is null ";
                }

                if (status.Equals("FATURADOS"))
                {
                    select += "and not ped_nota_fiscal is null ";
                }

                if (codRota > 0)
                {
                    select += "and r.rota_numero = @codRota ";
                }

                select += "and ped_excluido is null and ped_data_devolucao is null " +
                                "and not p.tipo_codigo in (select tipo_codigo from wms_tipo_pedido v " +
                                "where tipo_descricao like '%DEVOLUCAO%' or tipo_descricao like '%TROCA%' " +
                                "or tipo_descricao like '%AVARIA%' or tipo_descricao like '%BAIXA%' " +
                                "or tipo_descricao like '%SUPERMERCADO%' or tipo_descricao like '%TRANSFERENCIA%' " +
                                "or tipo_descricao like '%DEV%' or tipo_descricao like '%COMPRAS%' " +
                                "or tipo_descricao like '%CONSUMO%' or tipo_descricao like '%CORRECAO%' " +
                                "or tipo_descricao like '%CONTA%' " +
                                "or tipo_descricao like '%CONSIGNACAO%') " +
                                "order by c.cli_id ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Cliente cliente = new Cliente();
                    //Adiciona os valores encontrados
                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        cliente.codCliente = Convert.ToInt32(linha["cli_codigo"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        cliente.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        cliente.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_cnpj"] != DBNull.Value)
                    {
                        cliente.cnpjCliente = Convert.ToString(linha["cli_cnpj"]);
                    }
                    else if (linha["cli_cpf"] != DBNull.Value)
                    {
                        cliente.cnpjCliente = Convert.ToString(linha["cli_cpf"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        cliente.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        cliente.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        cliente.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        cliente.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cli_cep"] != DBNull.Value)
                    {
                        cliente.cepCliente = Convert.ToString(linha["cli_cep"]);
                    }

                    if (linha["cli_data_cadastro"] != DBNull.Value)
                    {
                        cliente.dataCadastroCliente = Convert.ToDateTime(linha["cli_data_cadastro"]);
                    }

                    if (linha["cli_fone"] != DBNull.Value)
                    {
                        cliente.foneCliente = Convert.ToString(linha["cli_fone"]);
                    }

                    if (linha["cli_celular"] != DBNull.Value)
                    {
                        cliente.celularCliente = Convert.ToString(linha["cli_celular"]);
                    }

                    if (linha["cli_email"] != DBNull.Value)
                    {
                        cliente.emailCliente = Convert.ToString(linha["cli_email"]);
                    }

                    if (linha["ati_codigo"] != DBNull.Value)
                    {
                        cliente.codAtividadeCliente = Convert.ToInt32(linha["ati_codigo"]);
                    }

                    if (linha["ati_descricao"] != DBNull.Value)
                    {
                        cliente.descAtividadeCliente = Convert.ToString(linha["ati_descricao"]);
                    }

                    if (linha["cod_filial"] != DBNull.Value)
                    {
                        cliente.codFilial = Convert.ToInt32(linha["cod_filial"]);
                    }

                    if (linha["praca_codigo"] != DBNull.Value)
                    {
                        cliente.codPracaCliente = Convert.ToInt32(linha["praca_codigo"]);
                    }

                    if (linha["praca_descricao"] != DBNull.Value)
                    {
                        cliente.descPracaCliente = Convert.ToString(linha["praca_descricao"]);
                    }

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        cliente.codRotaCliente = Convert.ToInt32(linha["rota_numero"]);
                    }

                    if (linha["rota_descricao"] != DBNull.Value)
                    {
                        cliente.rotaCliente = Convert.ToString(linha["rota_descricao"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        cliente.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["cli_latitude"] != DBNull.Value)
                    {
                        cliente.latitudeCliente = Convert.ToString(linha["cli_latitude"]);
                    }

                    if (linha["cli_longitude"] != DBNull.Value)
                    {
                        cliente.longitudeCliente = Convert.ToString(linha["cli_longitude"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    clienteCollection.Add(cliente);
                }
                //Retorna a coleção de cadastro encontrada
                return clienteCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os clientes. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza a roteirização
        public void ImportarArquivo(int codPedido, int manifesto, int sequencia)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@manifesto", manifesto);
                conexao.AdicionarParamentros("@sequencia", sequencia);

                //String de atualização
                string update = "update wms_pedido set mani_codigo_rout = @manifesto, ped_sequencia_rout = @sequencia, ped_gera_manifesto = 'True' " +
                                "where ped_codigo = @codPedido";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao importar o arquivo. \nDetalhes:" + ex.Message);
            }
        }

    }
}
