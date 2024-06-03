using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class ExportarCheckNowNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa os pedidos
        public ExportarCheckNowCollection PesqManifestos(int codManifestoInicial, int codManifestoFinal)
        {
            try
            {
                //Instância a coleção
                ExportarCheckNowCollection exportarCheckNowCollection = new ExportarCheckNowCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifestoInicial", codManifestoInicial);
                conexao.AdicionarParamentros("@codManifestoFinal", codManifestoFinal);
                //String de consulta
                string select = "select cid_regiao, m.mani_data, m.mani_codigo, v.vei_placa, p.ped_inicio_conferencia, p.ped_fim_conferencia, '00.00', " +
                                "r.rota_descricao, mo.mot_nome, vei_tara, '', p.ped_sequencia_rota, c.cli_sequencia, " +
                                "c.cli_codigo, c.cli_nome, cli_latitude, cli_longitude, c.cli_endereco, c.cli_numero, " +
                                "cli_complemento, c.cli_cep, e.est_uf, ci.cid_nome, b.bar_nome, c.cli_celular, " +
                                "'' as cli_distancia, '' as cli_raio, '' as cli_chegada, '' as cli_saida, '' as cli_tempo_entrega, " +
                                "p.ped_codigo, p.ped_nota_fiscal, p.ped_total, p.ped_peso, p.ped_cubagem, rr.rep_codigo, rr.rep_nome, rr.rep_celular, " +
                                "pg.pag_descricao, mo.mot_codigo, " +
                                "(select count(mani_codigo) from wms_manifesto  where mani_codigo >= @codManifestoInicial and mani_codigo <= @codManifestoFinal) as mani_count " +
                                "from wms_pedido p " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade ci " +
                                "on ci.cid_codigo = b.cid_codigo " +
                                "inner join wms_estado e " +
                                "on e.est_codigo = ci.est_codigo " +
                                "inner join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "inner join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo " +
                                "inner join wms_rota r " +
                                "on r.rota_codigo = c.rota_codigo " +
                                "inner join wms_motorista mo " +
                                "on mo.mot_codigo = m.mot_codigo " +
                                "inner join wms_representante rr " +
                                "on rr.rep_codigo = p.rep_codigo " +
                                "inner join wms_pagamento pg " +
                                "on pg.pag_codigo = p.pag_codigo ";

                if(codManifestoFinal > 0)
                {
                    select += "where ped_excluido is null and p.mani_codigo >= @codManifestoInicial and p.mani_codigo <= @codManifestoFinal " +
                                "order by ci.cid_sequencia desc, ped_sequencia_rota asc, c.cli_sequencia asc ";
                }
                else
                {
                    select += "where ped_excluido is null and p.mani_codigo = @codManifestoInicial " +
                              "order by ci.cid_sequencia desc, ped_sequencia_rota asc, c.cli_sequencia asc ";
                }
                               

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                int countSequencia = 1;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ExportarCheckNow checkNow = new ExportarCheckNow();

                    //Adiciona os valores encontrados
                    if (linha["cid_regiao"] != DBNull.Value)
                    {
                        checkNow.regiaoRota = Convert.ToString(linha["cid_regiao"]); 
                    }

                    if (linha["mani_data"] != DBNull.Value)
                    {
                        checkNow.dataManifesto = Convert.ToDateTime(linha["mani_data"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        checkNow.plavaVeiculo = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["ped_inicio_conferencia"] != DBNull.Value)
                    {
                        checkNow.dataIncialConferencia = Convert.ToDateTime(linha["ped_inicio_conferencia"]);
                    }

                    if (linha["ped_fim_conferencia"] != DBNull.Value)
                    {
                        checkNow.dataFinalConferencia = Convert.ToDateTime(linha["ped_fim_conferencia"]);
                    }                   

                    if (linha["cli_distancia"] != DBNull.Value)
                    {
                        checkNow.distanciaCliente = Convert.ToString(linha["cli_distancia"]);
                    }

                    if (linha["rota_descricao"] != DBNull.Value)
                    {
                        checkNow.descRota = Convert.ToString(linha["rota_descricao"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        checkNow.nomeMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    if (linha["vei_tara"] != DBNull.Value)
                    {
                        checkNow.capacidadeVeiculo = Convert.ToDouble(linha["vei_tara"]);
                    }

                    if (Convert.ToInt32(linha["ped_sequencia_rota"]) == 0)
                    {
                        checkNow.sequenciaEntrega = countSequencia++; //Convert.ToInt32(linha["cli_sequencia"]);                        
                    }
                    else
                    {
                        checkNow.sequenciaEntrega = Convert.ToInt32(linha["ped_sequencia_rota"]);
                    }


                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        checkNow.codCliente = Convert.ToString(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        checkNow.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_latitude"] != DBNull.Value)
                    {
                        checkNow.latitudeCliente = Convert.ToString(linha["cli_latitude"]);
                    }

                    if (linha["cli_longitude"] != DBNull.Value)
                    {
                        checkNow.longitudeCliente = Convert.ToString(linha["cli_longitude"]);
                    }

                     if (linha["cli_endereco"] != DBNull.Value)
                     {
                         checkNow.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                     }

                     if (linha["cli_numero"] != DBNull.Value)
                     {
                         checkNow.numeroCliente = Convert.ToString(linha["cli_numero"]);
                     }

                     if (linha["cli_complemento"] != DBNull.Value)
                     {
                         checkNow.complementoCliente = Convert.ToString(linha["cli_complemento"]);
                     }

                     if (linha["cli_cep"] != DBNull.Value)
                     {
                         checkNow.cepCliente = Convert.ToString(linha["cli_cep"]);
                     }

                     if (linha["est_uf"] != DBNull.Value)
                     {
                         checkNow.UFCliente = Convert.ToString(linha["est_uf"]);
                     }

                     if (linha["cid_nome"] != DBNull.Value)
                     {
                         checkNow.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                     }

                     if (linha["bar_nome"] != DBNull.Value)
                     {
                         checkNow.bairroCliente = Convert.ToString(linha["bar_nome"]);
                     }

                     if (linha["cli_celular"] != DBNull.Value)
                     {
                         checkNow.telefoneCliente = Convert.ToString(linha["cli_celular"]);
                     }

                    /* if (linha["cli_raio"] != DBNull.Value)
                     {
                         checkNow.raioCliente = Convert.ToInt32(linha["cli_raio"]);
                     }

                     if (linha["cli_chegada"] != DBNull.Value)
                     {
                         checkNow.dataChegaCliente = Convert.ToDateTime(linha["cli_chegada"]);
                     }

                     if (linha["cli_saida"] != DBNull.Value)
                     {
                         checkNow.dataSaidaCliente = Convert.ToDateTime(linha["cli_saida"]);
                     }

                     if (linha["cli_tempo_entrega"] != DBNull.Value)
                     {
                         checkNow.tempoViagem = Convert.ToDateTime(linha["cli_tempo_entrega"]);
                     }

                     if (linha["cli_tempo_entrega"] != DBNull.Value)
                     {
                         checkNow.tempoEntrega = Convert.ToDateTime(linha["cli_tempo_entrega"]);
                     }*/

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        checkNow.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        checkNow.numeroNotaFiscal = Convert.ToInt32(linha["ped_nota_fiscal"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        checkNow.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                    if (linha["ped_total"] != DBNull.Value)
                    {
                        checkNow.valorPedido = Convert.ToDouble(linha["ped_total"]);
                    }

                    if (linha["ped_cubagem"] != DBNull.Value)
                    {
                        checkNow.cubagemPedido = Convert.ToDouble(linha["ped_cubagem"]);
                    }

                    if (linha["rep_codigo"] != DBNull.Value)
                    {
                        checkNow.codigoRepresentante = Convert.ToInt32(linha["rep_codigo"]);
                    }

                    if (linha["rep_nome"] != DBNull.Value)
                    {
                        checkNow.nomeRepresentante = Convert.ToString(linha["rep_nome"]);
                    }

                    if (linha["rep_celular"] != DBNull.Value)
                    {
                        checkNow.celularRepresentante = Convert.ToString(linha["rep_celular"]);
                    }

                    if (linha["pag_descricao"] != DBNull.Value)
                    {
                        checkNow.formaPagamentoPedido = Convert.ToString(linha["pag_descricao"]);
                    }

                    if (linha["mot_codigo"] != DBNull.Value)
                    {
                        checkNow.codigoMotorista = Convert.ToInt32(linha["mot_codigo"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        checkNow.codigoManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["mani_count"] != DBNull.Value)
                    {
                        checkNow.qtdManifesto = Convert.ToInt32(linha["mani_count"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    exportarCheckNowCollection.Add(checkNow);
                }
                //Retorna a coleção de cadastro encontrada
                return exportarCheckNowCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a exportação. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os pedidos
        public Manifesto PesqManifestos(int codManifesto)
        {
            try
            {
                //Instância a classe
                Manifesto manifesto = new Manifesto();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                //String de consulta
                string select = "select mani_codigo, count(ped_codigo) as ped_codigo from wms_pedido " +
                    "where mani_codigo = @codManifesto " +
                    "group by mani_codigo";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        manifesto.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        manifesto.pedidoManifesto = Convert.ToInt32(linha["ped_codigo"]);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return manifesto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o manifesto. \nDetalhes:" + ex.Message);
            }
        }

    }
}
