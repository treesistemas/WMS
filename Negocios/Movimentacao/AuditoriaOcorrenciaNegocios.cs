using System;
using System.Data;
using Dados;
using ObjetoTransferencia;
using ObjetoTransferencia.Relatorio;


namespace Negocios
{
    public class AuditoriaOcorrenciaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a ocorrencia
        public ItensOcorrenciaCollection PesqOcorrencia(string codPedido, string notaFiscal, string status, string auditoria, string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensOcorrenciaCollection itensCollection = new ItensOcorrenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@notaFiscal", notaFiscal);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@auditoria", auditoria);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta - Pesquisa o bastecimento
                string select = "select po.poco_codigo, u.usu_login, ioco_codigo, po.poco_data_ocorrencia, io.ped_codigo, t.oco_descricao, po.poco_status, po.poco_observacao, " +
                                "io.prod_id, prod_codigo, prod_descricao, ioco_qtd_avaria, ioco_qtd_falta, ioco_qtd_troca, ioco_qtd_devolucao, ioco_qtd_critica, ioco_data_critica, ioco_erp, ioco_wms, ioco_motivo, ioco_data_auditoria, usu_codigo_erro, u1.usu_login as usu_login1,  " +
                                "ioco_observacao, ioco_foto,  m.mot_apelido, po.poco_reentrega, poco_cliente_aguardo " +
                                "from wms_item_ocorrencia io " +
                                "inner join wms_pedido_ocorrencia po " +
                                "on po.poco_codigo = io.poco_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = io.prod_id " +
                                "left join wms_tipo_ocorrencia t " +
                                "on t.oco_codigo = po.oco_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = po.usu_codigo_ocorrencia " +
                                "left join wms_usuario u1 " +
                                "on u1.usu_codigo = io.usu_codigo_erro " +
                                "left join wms_motorista m " +
                                "on m.mot_codigo = po.mot_codigo_ocorrencia " +
                                "where io.ioco_qtd_falta > 0 ";

                
                if (!codPedido.Equals(string.Empty))
                {
                    select += "and io.ped_codigo = @codPedido and not poco_status = 'CANCELADA' " +
                        "or io.ioco_qtd_troca > 0 and io.ped_codigo = @codPedido and not poco_status = 'CANCELADA' " +
                        "or io.ioco_qtd_devolucao > 0 and io.ped_codigo = @codPedido and not poco_status = 'CANCELADA' " +
                        "or io.ioco_qtd_critica > 0 and io.ped_codigo = @codPedido and not poco_status = 'CANCELADA' " +
                        "or io.ioco_qtd_avaria > 0 and io.ped_codigo = @codPedido  and not poco_status = 'CANCELADA' ";
                }

                else if (!notaFiscal.Equals(string.Empty))
                {
                    select += "and io.ped_codigo = (select ped_codigo from wms_pedido where ped_nota_fiscal = @notaFiscal) and not poco_status = 'CANCELADA' " +
                              "or io.ioco_qtd_troca > 0 and io.ped_codigo = (select ped_codigo from wms_pedido where ped_nota_fiscal = @notaFiscal) and not poco_status = 'CANCELADA' " +
                              "or io.ioco_qtd_devolucao > 0 and io.ped_codigo = (select ped_codigo from wms_pedido where ped_nota_fiscal = @notaFiscal) and not poco_status = 'CANCELADA' " +
                              "or io.ioco_qtd_critica > 0 and io.ped_codigo = (select ped_codigo from wms_pedido where ped_nota_fiscal = @notaFiscal) and not poco_status = 'CANCELADA' " +
                              "or io.ioco_qtd_avaria > 0 and io.ped_codigo = (select ped_codigo from wms_pedido where ped_nota_fiscal = @notaFiscal) and not poco_status = 'CANCELADA' ";
                }

                else if ((auditoria.Equals(string.Empty) || auditoria.Equals("TODOS")) && (status.Equals(string.Empty) || status.Equals("TODOS")))
                {
                    select += "and po.poco_data_ocorrencia between @dataInicial and @dataFinal and not poco_status = 'CANCELADA' " +
                              "or io.ioco_qtd_troca > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and not poco_status = 'CANCELADA' " +
                               "or io.ioco_qtd_devolucao > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and not poco_status = 'CANCELADA' " +
                              "or io.ioco_qtd_critica > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and not poco_status = 'CANCELADA' " +
                              "or io.ioco_qtd_avaria > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and not poco_status = 'CANCELADA' ";
                }

                else if ((auditoria.Equals(string.Empty) || auditoria.Equals("TODOS")) && (!status.Equals(string.Empty) || status.Equals("TODOS")))
                {
                    select += "and po.poco_data_ocorrencia between @dataInicial and @dataFinal and poco_status = @status and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                               "or io.ioco_qtd_troca > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and poco_status = @status and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                               "or io.ioco_qtd_devolucao > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and poco_status = @status and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                               "or io.ioco_qtd_critica > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and poco_status = @status and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                               "or io.ioco_qtd_avaria > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and poco_status = @status and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                else if ((!auditoria.Equals(string.Empty) || auditoria.Equals("TODOS")) && (status.Equals(string.Empty) || status.Equals("TODOS")))
                {
                    select += "and po.poco_data_ocorrencia between @dataInicial and @dataFinal and ioco_motivo = @auditoria and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                               "or io.ioco_qtd_troca > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and ioco_motivo = @auditoria and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or io.ioco_qtd_devolucao > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and ioco_motivo = @auditoria and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                               "or io.ioco_qtd_critica > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and ioco_motivo = @auditoria and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                               "or io.ioco_qtd_avaria > 0 and po.poco_data_ocorrencia between @dataInicial and @dataFinal and ioco_motivo = @auditoria and io.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                //Ordenação pelo código
                select += "order by poco_data_ocorrencia";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (!Convert.ToString(linha["oco_descricao"]).Equals("RECOLHIMENTO"))
                    {
                        //Instância a camada de objêtos
                        ItensOcorrencia itens = new ItensOcorrencia();
                        //Adiciona os valores encontrados
                        if (linha["usu_login"] != DBNull.Value)
                        {
                            itens.nomeAuditor = Convert.ToString(linha["usu_login"]);
                        }

                        if (linha["poco_codigo"] != DBNull.Value)
                        {
                            itens.codOcorrencia = Convert.ToInt32(linha["poco_codigo"]);
                        }

                        if (linha["ioco_codigo"] != DBNull.Value)
                        {
                            itens.codItemOcorrencia = Convert.ToInt32(linha["ioco_codigo"]);
                        }

                        if (linha["poco_data_ocorrencia"] != DBNull.Value)
                        {
                            itens.dataOcorrencia = Convert.ToDateTime(linha["poco_data_ocorrencia"]);
                        }

                        if (linha["ioco_data_auditoria"] != DBNull.Value)
                        {
                            itens.dataAuditoria = Convert.ToDateTime(linha["ioco_data_auditoria"]);
                        }                        

                        if (linha["ped_codigo"] != DBNull.Value)
                        {
                            itens.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                        }

                        if (linha["oco_descricao"] != DBNull.Value)
                        {
                            itens.descricaoOcorrencia = Convert.ToString(linha["oco_descricao"]);
                        }

                        if (linha["poco_status"] != DBNull.Value)
                        {
                            itens.statusOcorrencia = Convert.ToString(linha["poco_status"]);
                        }

                        if (linha["poco_observacao"] != DBNull.Value)
                        {
                            itens.obsOcorrencia = Convert.ToString(linha["poco_observacao"]);
                        }

                        if (linha["prod_id"] != DBNull.Value)
                        {
                            itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                        }

                        if (linha["prod_codigo"] != DBNull.Value)
                        {
                            itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                        }

                        if (linha["prod_descricao"] != DBNull.Value)
                        {
                            itens.descProduto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                        }

                        if (linha["ioco_qtd_falta"] != DBNull.Value)
                        {
                            itens.qtdFaltaProduto = Convert.ToInt32(linha["ioco_qtd_falta"]);
                        }

                        if (linha["ioco_qtd_troca"] != DBNull.Value)
                        {
                            itens.qtdTrocaProduto = Convert.ToInt32(linha["ioco_qtd_troca"]);
                        }

                        if (linha["ioco_qtd_avaria"] != DBNull.Value)
                        {
                            itens.qtdAvariaProduto = Convert.ToInt32(linha["ioco_qtd_avaria"]);
                        }


                        if (linha["ioco_qtd_devolucao"] != DBNull.Value)
                        {
                            itens.qtdDevolucao = Convert.ToInt32(linha["ioco_qtd_devolucao"]);
                        }

                        if (linha["ioco_qtd_critica"] != DBNull.Value)
                        {
                            itens.qtdCriticaProduto = Convert.ToInt32(linha["ioco_qtd_critica"]);
                        }

                        if (linha["ioco_data_critica"] != DBNull.Value)
                        {
                            itens.DataCriticaProduto = Convert.ToDateTime(linha["ioco_data_critica"]);
                        }                        

                        if (linha["ioco_erp"] != DBNull.Value)
                        {
                            itens.qtdErp = Convert.ToInt32(linha["ioco_erp"]);
                        }

                        if (linha["ioco_wms"] != DBNull.Value)
                        {
                            itens.qtdWMS = Convert.ToInt32(linha["ioco_wms"]);
                        }

                        if (linha["ioco_motivo"] != DBNull.Value)
                        {
                            itens.descMotivo = Convert.ToString(linha["ioco_motivo"]);
                        }

                        if (linha["ioco_observacao"] != DBNull.Value)
                        {
                            itens.obsAuditoria = Convert.ToString(linha["ioco_observacao"]);
                        }

                        if (linha["ioco_foto"] != DBNull.Value)
                        {
                            itens.fotoAuditoria = (byte[])(linha["ioco_foto"]);
                        }

                        if (linha["usu_codigo_erro"] != DBNull.Value)
                        {
                            itens.codUsuarioErro = Convert.ToInt32(linha["usu_codigo_erro"]);
                        }

                        if (linha["usu_login1"] != DBNull.Value)
                        {
                            itens.nomeUsuarioErro = Convert.ToString(linha["usu_login1"]);
                        }

                        if (linha["mot_apelido"] != DBNull.Value)
                        {
                            itens.apelidoMotorista = Convert.ToString(linha["mot_apelido"]);
                        }

                        if (linha["poco_reentrega"] != DBNull.Value)
                        {
                            itens.reentregaOcorrencia = Convert.ToString(linha["poco_reentrega"]);
                        }

                        if (linha["poco_cliente_aguardo"] != DBNull.Value)
                        {
                            itens.clienteAguardo = Convert.ToString(linha["poco_cliente_aguardo"]);
                        }
                        
                        //Adiciona os valores encontrados a coleção
                        itensCollection.Add(itens);
                    }
                }

                //Retorna a coleção de cadastro encontrada
                return itensCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as ocorrências \nDetalhes:" + ex.Message);
            }

        }

        //Pesquisa a ocorrencia
        public int PesqEstoque(string codProduto)
        {
            try
            {
                //Instância uma variável
                int estoque = 0;
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codProduto", codProduto);


                //String de consulta - Pesquisa o bastecimento
                string select = "select est_quantidade from wms_estoque where prod_id = (select prod_id from wms_produto where prod_codigo = @codProduto)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    if (linha["est_quantidade"] != DBNull.Value)
                    {
                        estoque = Convert.ToInt32(linha["est_quantidade"]);
                    }
                }

                //Retorna a coleção de cadastro encontrada
                return estoque;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o estoque \nDetalhes:" + ex.Message);
            }

        }


        //Altera a ocorrencia
        public void AlterarOcorrencia(int codOcorrencia, int codOcorrenciaItem, int codUsuario, string codUsuarioErro, string motivo, int estoqueErp, int estoqueWms, string observacao, byte[] foto, string status, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codOcorrencia", codOcorrencia);
                conexao.AdicionarParamentros("@codOcorrenciaItem", codOcorrenciaItem);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codUsuarioErro", codUsuarioErro);
                conexao.AdicionarParamentros("@motivo", motivo);
                conexao.AdicionarParamentros("@estoqueErp", estoqueErp);
                conexao.AdicionarParamentros("@estoqueWms", estoqueWms);
                conexao.AdicionarParamentros("@observacao", observacao);
                conexao.AdicionarParamentros("@foto", foto);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de atualização
                string update = "update wms_item_ocorrencia set usu_codigo = @codUsuario, ioco_motivo = @motivo, ioco_erp = @estoqueErp," +
                    "ioco_wms = @estoqueWms, ioco_observacao = @observacao, ioco_foto = @foto, ioco_data_auditoria = current_timestamp," +
                    "usu_codigo_erro = @codUsuarioErro " +
                    "where ioco_codigo = @codOcorrenciaItem and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //String de atualização
                string update1 = "update wms_pedido_ocorrencia set poco_status = @status " +
                    "where poco_codigo = @codOcorrencia";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update1);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar a ocorrência. \nDetalhes:" + ex.Message);
            }
        }

    }
}
