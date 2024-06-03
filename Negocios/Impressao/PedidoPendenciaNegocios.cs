using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class PedidoPendenciaNegocios
    {
        //Instância o objeto
        Conexao conexao = new Conexao();

        //Pesquisa as pendências
        public OcorrenciaCollection PesqPendencia(string dataInicial, string dataFinal)
        {
            try
            {
                //Instância a camada de objêtos
                OcorrenciaCollection pendenciaCollection = new OcorrenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //String de consulta
                string select = "select " +
                                "(select cf.conf_empresa from wms_configuracao cf where cf.conf_codigo = 1 ) as nome_empresa, " +
                                "r.rota_numero, r.rota_descricao, po.poco_codigo, po.ped_codigo, po.poco_data_ocorrencia,  po.oco_codigo, t.oco_descricao, c.cli_codigo, c.cli_nome, " +
                                "c.cli_fantasia, ped_nota_fiscal, m.mot_apelido, u.usu_login, ci.cid_nome, b.bar_nome, c.cli_endereco, c.cli_numero, poco_observacao " +
                                "from wms_pedido_ocorrencia po " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = po.ped_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_rota r " +
                                "on r.rota_codigo = c.rota_codigo " +
                                "inner join wms_tipo_ocorrencia t " +
                                "on t.oco_codigo = po.oco_codigo " +
                                "inner join wms_motorista m " +
                                "on m.mot_codigo = po.mot_codigo_ocorrencia " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = po.usu_codigo_ocorrencia " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade ci " +
                                "on ci.cid_codigo = b.cid_codigo " +
                                "where po.poco_cliente_aguardo = 'True' and poco_status = 'ROTEIRIZACAO' and po.poco_data_ocorrencia between @dataInicial and @dataFinal ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Ocorrencia pendencia = new Ocorrencia();

                    //Adiciona os valores encontrados
                    if (linha["nome_empresa"] != DBNull.Value)
                    {
                        pendencia.nomeEmpresa = Convert.ToString(linha["nome_empresa"]);
                    }

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        pendencia.rotaOcorrencia = "[" + Convert.ToString(linha["rota_numero"]) + "] " + Convert.ToString(linha["rota_descricao"]);
                    }

                    if (linha["poco_codigo"] != DBNull.Value)
                    {
                        pendencia.codOcorrencia = Convert.ToInt32(linha["poco_codigo"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pendencia.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["poco_data_ocorrencia"] != DBNull.Value)
                    {
                        pendencia.dataOcorrencia = Convert.ToDateTime(linha["poco_data_ocorrencia"]);
                    }

                    if (linha["oco_descricao"] != DBNull.Value)
                    {
                        pendencia.descTipoOcorrencia = "[" + Convert.ToInt32(linha["oco_codigo"]) + "] " + Convert.ToString(linha["oco_descricao"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        pendencia.nomeCliente = "[" + Convert.ToInt32(linha["cli_codigo"]) + "] " + Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        pendencia.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        pendencia.notaFiscal = Convert.ToInt32(linha["ped_nota_fiscal"]);
                    }

                    if (linha["mot_apelido"] != DBNull.Value)
                    {
                        pendencia.motoristaOcorrencia = Convert.ToString(linha["mot_apelido"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        pendencia.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        pendencia.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        pendencia.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        pendencia.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        pendencia.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["poco_observacao"] != DBNull.Value)
                    {
                        pendencia.obsOcorrencia = Convert.ToString(linha["poco_observacao"]);
                    }

                    pendenciaCollection.Add(pendencia);
                }

                //Retorna a coleção de cadastro encontrada
                return pendenciaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar às pendências. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa as pendências
        public ItemPendenciaCollection PesqItemPendencia(string dataInicial, string dataFinal)
        {
            try
            {
                //Instância a camada de objêtos
                ItemPendenciaCollection itensPendenciaCollection = new ItemPendenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //String de consulta
                string select = "select " +
                                 "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'CAIXA' and prod_id = i.prod_id) as apa_endereco_cxa, " +
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = i.prod_id) as apa_endereco_flow, " +
                                "po.poco_codigo, pr.prod_codigo, prod_descricao, i.ioco_qtd_avaria, i.ioco_qtd_falta, " +
                                "i.ioco_qtd_troca, i.ioco_qtd_critica, u.uni_unidade, prod_fator_pulmao, PROD_PESO_VARIAVEL from wms_pedido_ocorrencia po " +
                                "inner join wms_item_ocorrencia i " +
                                "on po.poco_codigo = i.poco_codigo " +
                                "inner join wms_produto pr " +
                                "on pr.prod_id = i.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = pr.uni_codigo_picking " +
                                "where po.poco_cliente_aguardo = 'True' and poco_status = 'ROTEIRIZACAO' and not i.ioco_qtd_avaria is null and po.poco_data_ocorrencia between @dataInicial and @dataFinal " +
                                "or po.poco_cliente_aguardo = 'True'  and poco_status = 'ROTEIRIZACAO' and not i.ioco_qtd_falta is null and po.poco_data_ocorrencia between @dataInicial and @dataFinal " +
                                "or po.poco_cliente_aguardo = 'True'  and poco_status = 'ROTEIRIZACAO' and not i.ioco_qtd_troca is null and po.poco_data_ocorrencia between @dataInicial and @dataFinal " +
                                "or po.poco_cliente_aguardo = 'True'  and poco_status = 'ROTEIRIZACAO' and not i.ioco_qtd_critica is null and po.poco_data_ocorrencia between @dataInicial and @dataFinal ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItemPendencia itensPendencia = new ItemPendencia();

                    //Adiciona os valores encontrados
                    if (linha["poco_codigo"] != DBNull.Value)
                    {
                        itensPendencia.codOcorrencia = Convert.ToInt32(linha["poco_codigo"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensPendencia.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["ioco_qtd_avaria"] != DBNull.Value)
                    {
                        itensPendencia.qtdAvariaProduto = Convert.ToDouble(linha["ioco_qtd_avaria"]);
                    }

                    if (linha["ioco_qtd_troca"] != DBNull.Value)
                    {
                        itensPendencia.qtdTrocaProduto = Convert.ToDouble(linha["ioco_qtd_troca"]);
                    }

                    if (linha["ioco_qtd_critica"] != DBNull.Value)
                    {
                        itensPendencia.qtdCriticaProduto = Convert.ToDouble(linha["ioco_qtd_critica"]);
                    }

                    if (linha["ioco_qtd_falta"] != DBNull.Value)
                    {
                        itensPendencia.qtdFaltaProduto = Convert.ToDouble(linha["ioco_qtd_falta"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itensPendencia.unidadeFracionada = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["PROD_PESO_VARIAVEL"] != DBNull.Value)
                    {
                        itensPendencia.controlaPeso = Convert.ToString(linha["PROD_PESO_VARIAVEL"]);
                    }
                    else
                    {
                        itensPendencia.controlaPeso = "False";
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        //Verifica se a quantidade a separar é igual o fator da caixa
                        if (itensPendencia.qtdAvariaProduto + itensPendencia.qtdTrocaProduto +
                            itensPendencia.qtdCriticaProduto + itensPendencia.qtdFaltaProduto >= Convert.ToInt32(linha["prod_fator_pulmao"]))
                        {
                            if (linha["apa_endereco_cxa"] != DBNull.Value)
                            {
                                itensPendencia.endereco = Convert.ToString(linha["apa_endereco_cxa"]);
                            }
                            else if (linha["apa_endereco_flow"] != DBNull.Value)
                            {
                                itensPendencia.endereco = Convert.ToString(linha["apa_endereco_flow"]);
                            }
                            else
                            {
                                itensPendencia.endereco = "SEM PICKING";
                            }
                        }
                        else
                        {
                            if (linha["apa_endereco_flow"] != DBNull.Value)
                            {
                                itensPendencia.endereco = Convert.ToString(linha["apa_endereco_flow"]);
                            }
                            else if (linha["apa_endereco_cxa"] != DBNull.Value)
                            {
                                itensPendencia.endereco = Convert.ToString(linha["apa_endereco_cxa"]);
                            }
                            else
                            {
                                itensPendencia.endereco = "SEM PICKING";
                            }
                        }
                    }



                    itensPendenciaCollection.Add(itensPendencia);
                }
                //Retorna a coleção de cadastro encontrada
                return itensPendenciaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens das pendências. \nDetalhes:" + ex.Message);
            }
        }

    }
}
