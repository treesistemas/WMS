using Dados;
using System;
using ObjetoTransferencia;
using System.Data;
using ObjetoTransferencia.Relatorio;

namespace Negocios
{
    public class ManifestoNegocios
    {
        //Chama a camada de dados
        Conexao conexao = new Conexao();

        //Pesquisa os conferentes para o gridview
        public string[] PesqConferente()
        {
            try
            {

                //String de consulta
                string select = "select usu_login from wms_usuario u " +
                "inner join wms_perfil p " +
                "on p.perf_codigo = u.perf_codigo where p.perf_descricao = 'CONFERENTE' order by usu_login";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância um array
                string[] arrayConferente = new string[dataTable.Rows.Count];

                //Controla o index do array
                int i = 0;
                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["usu_login"] != DBNull.Value)
                    {
                        arrayConferente[i] = Convert.ToString(linha["usu_login"]);
                    }

                    i++;


                }
                //Retorna a coleção de cadastro encontrada
                return arrayConferente;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os conferentes. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa manifesto
        public ManifestoCollection PesqManifesto(string codManifesto, string empresa, DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                //Instância a camada de objetos
                ManifestoCollection manifestoCollection = new ManifestoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@dataInicial", dataInicial);
                conexao.AdicionarParamentros("@datafinal", dataFinal);

                //String de consulta
                string select = "select m.mani_data, m.mani_codigo, v.vei_placa, t.tipo_descricao, " +
                /*Tipo de rota*/
                "(select  first 1 tp.tipo_descricao from wms_pedido p " +
                "inner join wms_cliente c " +
                "on p.cli_id = c.cli_id " +
                "inner join wms_rota r " +
                "on r.rota_codigo = c.rota_codigo " +
                "left join wms_tipo_rota tp " +
                "on tp.tipo_codigo = r.tipo_codigo " +
                "where mani_codigo = m.mani_codigo) as tipo_rota, " +
                /*Qtd de pedidos*/
                "(select count(ped_codigo) from wms_pedido where mani_codigo = m.mani_codigo) as pedido, " +
                /*Qtd de pedidos pendentes*/
                "(select count(ped_codigo) from wms_pedido where mani_codigo = m.mani_codigo and ped_fim_conferencia is null ) as pedido_pendente, " +
                /*Quantidade de itens*/
                "(select count(ip.ped_codigo) from wms_pedido p " +
                "inner join wms_item_pedido ip " +
                "on ip.ped_codigo = p.ped_codigo " +
                "where mani_codigo = m.mani_codigo) as itens, " +
                /*Peso da rota*/
                "(select sum(ped_peso) from wms_pedido where mani_codigo = m.mani_codigo) as peso, " +
                /*Status de impressão*/
                "(select first 1 ped_impresso from wms_pedido where mani_codigo = m.mani_codigo order by ped_impresso ) as impresso, " +
                /*Pedido bloqueado*/
                "(select count(ped_codigo) from wms_pedido where ped_bloqueado = 'True' and mani_codigo = m.mani_codigo) as pedido_bloqueado, " +
                /*Pedido não finaizados no flow rack*/
                "(select count(ped_codigo) from wms_pedido where ped_flow_rack = 'SIM' and ped_fim_flow_rack is null and mani_codigo = m.mani_codigo) as pendente_flow, " +
                /*Soma a cubagem do pedido*/
                "(select sum(ped_cubagem) from wms_pedido where mani_codigo = m.mani_codigo) as cubagem, " +
                /*Cubagem caixa
                "(select sum(trunc(iped_quantidade/prod_fator_pulmao) * b.bar_cubagem) + " +
                //Cubagem fracionada
                "(select sum(mod(iped_quantidade,  prod_fator_pulmao) * b.bar_cubagem) " +
                "from wms_pedido  pd " +
                "inner join wms_item_pedido ip " +
                "on ip.ped_codigo = pd.ped_codigo " +
                "inner join wms_produto p " +
                "on p.prod_id = ip.prod_id " +
                "left join wms_barra b " +
                "on b.prod_id = ip.prod_id " +
                "where bar_multiplicador = 1 and mani_codigo = m.mani_codigo) " +
                "from wms_pedido  pd " +
                "inner join wms_item_pedido ip " +
                "on ip.ped_codigo = pd.ped_codigo " +
                "inner join wms_produto p " +
                "on p.prod_id = ip.prod_id " +
                "left join wms_barra b " +
                "on b.prod_id = ip.prod_id " +
                "where bar_multiplicador = prod_fator_pulmao and mani_codigo = m.mani_codigo) as cubagem, " + */
                "u.usu_login, tipo_cubagem, " +
                /*Inicio de conferencia*/
                "(select min(ped_inicio_conferencia) from wms_pedido where mani_codigo = m.mani_codigo) as inicio_conferencia, " +
                /*fim de conferencia*/
                "(select max(ped_fim_conferencia) from wms_pedido where mani_codigo = m.mani_codigo) as fim_conferencia, " +
                /*Notas fiscais não faturadas*/
                "(select count(ped_codigo) from wms_pedido where mani_codigo = m.mani_codigo and ped_nota_fiscal is null) as nf_faturar " +
                "from wms_manifesto m " +
                "left join wms_veiculo v " +
                "on v.vei_codigo = m.vei_codigo " +
                "left join wms_veiculo_tipo t " +
                "on v.tipo_codigo = t.tipo_codigo " +
                "left join wms_usuario u " +
                "on u.usu_codigo = m.usu_codigo_conferente ";


                if (!codManifesto.Equals(""))
                {
                    select += "where m.mani_codigo = @codManifesto";
                }
                else
                {
                    select += "where m.mani_data between @dataInicial and @datafinal and m.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by mani_codigo";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Manifesto manifesto = new Manifesto();
                    //Adiciona os valores encontrados
                    if (linha["mani_data"] != DBNull.Value)
                    {
                        manifesto.dataManifesto = Convert.ToDateTime(linha["mani_data"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        manifesto.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        manifesto.veiculoManifesto = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["tipo_cubagem"] != DBNull.Value)
                    {
                        manifesto.cubagemVeiculo = Convert.ToDouble(linha["tipo_cubagem"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        manifesto.tipoVeiculo = Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["tipo_rota"] != DBNull.Value)
                    {
                        manifesto.tipoRota = Convert.ToString(linha["tipo_rota"]);
                    }

                    if (linha["pedido"] != DBNull.Value)
                    {
                        manifesto.pedidoManifesto = Convert.ToInt32(linha["pedido"]);
                    }

                    if (linha["pedido_pendente"] != DBNull.Value)
                    {
                        manifesto.pedPendenteManifesto = Convert.ToInt32(linha["pedido_pendente"]);
                    }

                    if (linha["pedido_bloqueado"] != DBNull.Value)
                    {
                        manifesto.pedidoBloqueado = Convert.ToInt32(linha["pedido_bloqueado"]);
                    }

                    if (linha["pendente_flow"] != DBNull.Value)
                    {
                        manifesto.pendenteFlow = Convert.ToInt32(linha["pendente_flow"]);
                    }

                    if (linha["itens"] != DBNull.Value)
                    {
                        manifesto.itensManifesto = Convert.ToInt32(linha["itens"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        manifesto.pesoManifesto = Convert.ToDouble(linha["peso"]);
                    }

                    if (linha["cubagem"] != DBNull.Value)
                    {
                        manifesto.cubagemManifesto = Convert.ToDouble(linha["cubagem"]);
                    }

                    if (linha["impresso"] != DBNull.Value)
                    {
                        manifesto.impressoManifesto = Convert.ToString(linha["impresso"]);
                    }
                    else
                    {
                        manifesto.impressoManifesto = "NÃO";
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        manifesto.conferenteManifesto = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["inicio_conferencia"] != DBNull.Value)
                    {
                        manifesto.inicioConferencia = Convert.ToDateTime(linha["inicio_conferencia"]);
                    }

                    if (linha["fim_conferencia"] != DBNull.Value)
                    {
                        manifesto.fimConferencia = Convert.ToDateTime(linha["fim_conferencia"]);
                    }

                    if (linha["nf_faturar"] != DBNull.Value)
                    {
                        manifesto.NFFaturar = Convert.ToInt32(linha["nf_faturar"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    manifestoCollection.Add(manifesto);
                }
                //Retorna a coleção de cadastro encontrada
                return manifestoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os manifestos. \nDetalhes:" + ex.Message);
            }
        }

        //Analisa o manifesto para separação multipedidos
        public Manifesto AnalisarManifesto(string codManifesto)
        {
            try
            {
                //Instância a camada de objetos
                Manifesto manifesto = new Manifesto();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select count(ped_codigo) as count_pedido, " +
                                /*Quantidade de itens*/
                                "(select count(iif(trunc(iped_quantidade/pp.prod_fator_pulmao) > 0 , 1, 0)) as grandeza from wms_pedido p " +
                                "inner join  wms_item_pedido i " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = i.prod_id " +
                                "where mani_codigo = @codManifesto) as count_itens, " +
                                /*Quantidade de volumes de grandeza*/
                                "(select sum(trunc(iped_quantidade/pp.prod_fator_compra)) as grandeza from wms_pedido p " +
                                "inner join  wms_item_pedido i " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = i.prod_id " +
                                "where mani_codigo = @codManifesto) + " +
                                /*Quantidade de volumes de flowrack*/
                                "(select count(distinct(f.iflow_barra)) as flowrack from wms_rastreamento_flowrack f " +
                                "inner join wms_pedido p " +
                                "on f.ped_codigo = p.ped_codigo " +
                                "where mani_codigo = @codManifesto) as count_volumes, " +
                                /*Peso da rota*/
                                "sum(ped_peso) as soma_peso " +
                                "from wms_pedido where mani_codigo = @codManifesto ";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["count_pedido"] != DBNull.Value)
                    {
                        manifesto.pedidoManifesto = Convert.ToInt32(linha["count_pedido"]);
                    }

                    if (linha["count_itens"] != DBNull.Value)
                    {
                        manifesto.itensManifesto = Convert.ToInt32(linha["count_itens"]);
                    }

                    if (linha["count_volumes"] != DBNull.Value)
                    {
                        manifesto.volumesManifesto = Convert.ToInt32(linha["count_volumes"]);
                    }

                    if (linha["soma_peso"] != DBNull.Value)
                    {
                        manifesto.pesoManifesto = Convert.ToDouble(linha["soma_peso"]);
                    }

                }
                //Retorna objêto
                return manifesto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao analisar o manifesto. \nDetalhes:" + ex.Message);
            }
        }

        public ProdutoCollection VerificaProduto(int codManifesto)
        {
            try
            {
                //Instância a camada de objêto
                ProdutoCollection produtoCollection = new ProdutoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                //conexao.AdicionarParamentros("@codPedido", codPedido);

                //String de consulta
                string select = "select distinct(prod_codigo), prod_descricao, prod_fator_pulmao, prod_lastro_m, prod_altura_m from wms_pedido p " +
                                "inner join wms_item_pedido ip  " +
                                "on ip.ped_codigo = p.ped_codigo  " +
                                "inner join wms_produto po  " +
                                "on po.prod_id = ip.prod_id  ";


                /*if (codPedido > 0)
                {
                    select += "where mani_codigo = @codPedido and prod_fator_pulmao is null or prod_fator_pulmao = 0  " +
                              "or mani_codigo = @codPedido and prod_lastro_m is null or prod_lastro_m = 0  " +
                              "or mani_codigo = @codPedido and prod_altura_m is null or prod_altura_m = 0 ";
                }*/

                //GD E FORQUILHA
                if (codManifesto > 0)
                {
                    select += "where mani_codigo = @codManifesto and prod_fator_pulmao = 0 " +
                              "or mani_codigo = @codManifesto and prod_altura_m = 0 " +
                              "or mani_codigo = @codManifesto and prod_altura_m = 0 ";
                    // "or mani_codigo = @codManifesto and prod_lastro_m is null or prod_lastro_m = 0  " +
                    // "or mani_codigo = @codManifesto and prod_altura_m is null or prod_altura_m = 0 ";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    Produto produto = new Produto();

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        produto.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        produto.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["prod_lastro_m"] != DBNull.Value)
                    {
                        produto.lastroMedio = Convert.ToInt32(linha["prod_lastro_m"]);
                    }

                    if (linha["prod_altura_m"] != DBNull.Value)
                    {
                        produto.alturaMedio = Convert.ToInt32(linha["prod_altura_m"]);
                    }

                    //Adiona o objêto a coleção
                    produtoCollection.Add(produto);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar produtos com dados logísticos em falta. \nDetalhes:" + ex.Message);
            }
        }


        //Sequencia os pedidos para a separação
        public void SequenciaSeparacao(string codManifesto, int sequencia, int qtdPedidos)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@sequencia", sequencia);

                //Atualiza a sequencia de pedidos
                string update = "update wms_pedido p set ped_multipedido = 'True', ped_separacao = @sequencia " +
                                "where mani_codigo = @codManifesto and ped_multipedido is null " +
                                "order by p.ped_sequencia_rota " +
                                "ROWS 1 TO " + qtdPedidos;

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, update);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a sequência para separação multipedidos. \nDetalhes:" + ex.Message);
            }
        }

        //Resgistra a marcação de rota
        public void RegistraMarcacao(string empresa, int codManifesto, string marcacao)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@marcacao", marcacao);

                //String de consulta
                string select = "update wms_manifesto set mani_letra = @marcacao " +
                                "where mani_codigo = @codManifesto and mani_letra is null and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a marcação no manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Insere a grandeza do pedido
        public void PesquisaGrandeza(string empresa, int codUsuario, int codManifesto, int codPedido)
        {
            try
            {

                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);

                /*Pesquisa a quantidade de produto que necessita de reserva*/
                string select = "select p.mani_codigo, p.ped_codigo, ip.ped_codigo, p.tipo_codigo, ped_tabela, " +
                                "coalesce((select cast(iconf_valor as integer) from wms_itens_configuracao where iconf_descricao = 'SEPARAR PEDIDO POR PRODUTOS PRÓXIMO AO VENCIMENTO I' and iconf_status = 'True'), 0)  as tipo_vencimento, " +
                                "ip.prod_id, " +
                                "ip.iped_quantidade/ pp.prod_fator_pulmao as qtd_separar, pp.prod_lastro_m * pp.prod_altura_m as qtd_reservar, " +
                                "(ip.iped_quantidade/pp.prod_fator_pulmao)/(pp.prod_lastro_m * pp.prod_altura_m) as qtd_palete, " +
                                "(select count(r.prod_id) from wms_reserva r where r.ped_codigo = p.ped_codigo and r.prod_id = ip.prod_id) qtd_palete_reservado, "+
                                /*Pesquisa a qtd de palete armazenada desse produto*/
                                "(select count(prod_id) from wms_armazenagem a " +
                                "where prod_id = ip.prod_id and (arm_quantidade/ pp.prod_fator_pulmao) >= (pp.prod_lastro_m * pp.prod_altura_m)) as qtd_palete_armazenado " +
                                "from wms_item_pedido ip " +
                                "inner join wms_pedido p " +
                                "on ip.ped_codigo = p.ped_codigo " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = ip.prod_id and pp.conf_codigo = ip.conf_codigo " +
                                "where p.ped_impresso is null and (ip.iped_quantidade/ pp.prod_fator_pulmao) >= (pp.prod_lastro_m * pp.prod_altura_m) ";

                if (codPedido > 0)
                {
                    select += "and p.ped_codigo = @codPedido ";
                }
                else
                {
                    select += "and p.mani_codigo = @codManifesto ";
                }

                //Executa o script no banco
                //conexao.ExecuteManipulacao(CommandType.Text, select);

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                int idProduto = 0, qtdReservar = 0, qtdPalete = 0, qtdPaleteReservar = 0, tipoVencimento = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItemMapaSeparacao itemPedido = new ItemMapaSeparacao();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        //tem que declarar para evitar erro
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        //tem que declarar para evitar erro
                    }


                    if (linha["prod_id"] != DBNull.Value)
                    {
                        idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["qtd_reservar"] != DBNull.Value)
                    {
                        qtdReservar = Convert.ToInt32(linha["qtd_reservar"]);
                    }

                    if (linha["qtd_palete"] != DBNull.Value)
                    {
                        qtdPalete = Convert.ToInt32(linha["qtd_palete"]);
                    }

                    if (linha["qtd_palete_armazenado"] != DBNull.Value)
                    {
                        qtdPaleteReservar = Convert.ToInt32(linha["qtd_palete_armazenado"]);

                        //Verifica se o produto existe na armazenagem
                        if (qtdPaleteReservar > Convert.ToInt32(linha["qtd_palete_reservado"]))
                        {
                            //Zera o tipo de pedido
                            tipoVencimento = 0;

                            //Tipo de pedido que controla o vencimento do produto no pedido
                            if (linha["ped_tabela"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(linha["ped_tabela"]) == Convert.ToInt32(linha["tipo_vencimento"]))
                                {
                                    tipoVencimento = Convert.ToInt32(linha["tipo_vencimento"]);
                                }
                            }

                            ReservaGrandeza(empresa, codUsuario, Convert.ToInt32(linha["mani_codigo"]), Convert.ToInt32(linha["ped_codigo"]), tipoVencimento,
                            Convert.ToInt32(linha["prod_id"]), Convert.ToInt32(linha["qtd_reservar"]), Convert.ToInt32(linha["qtd_palete"]));
                                   
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as grandezas. \nDetalhes:" + ex.Message);
            }
        }

        //Insere a grandeza do pedido
        public void ReservaGrandeza(string empresa, int codUsuario, int? codManifesto, int codPedido, int tipoTabela, int idProduto, int qtdReservar, int qtdPalete)
        {
            try
            {

                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@qtdReservar", qtdReservar);
                conexao.AdicionarParamentros("@qtdPalete", qtdPalete);
                conexao.AdicionarParamentros("@tipoTabela", tipoTabela);


                //Insere o endereço na tabela de reserva
                string insert = "insert into wms_reserva (res_codigo, res_data, usu_codigo, mani_codigo, ped_codigo, apa_codigo, prod_id, res_quantidade, " +
                                 "res_validade, res_peso, res_lote, res_tipo, conf_codigo) " +

                                "select first @qtdPalete gen_id(gen_wms_reserva,1), current_timestamp, @codUsuario, @codManifesto, @codPedido, a.apa_codigo, a.prod_id, @qtdReservar, " +
                                "arm_vencimento, (arm_quantidade * arm_peso) as peso, arm_lote, 'SEPARAÇÃO', (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "from wms_armazenagem a " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = a.prod_id ";

                if (tipoTabela == 0)
                {
                   insert += "where a.arm_reserva is null and a.arm_bloqueado is null " +
                    "and (arm_quantidade / pp.prod_fator_pulmao) >= (pp.prod_lastro_m * pp.prod_altura_m) " +
                    "and a.prod_id = @idProduto " +
                    "order by arm_vencimento ";
                }

                if (tipoTabela > 0)
                {
                    insert += "where a.arm_reserva is null and a.arm_bloqueado = 'True' "+
                                "and(arm_quantidade / pp.prod_fator_pulmao) >= (pp.prod_lastro_m * pp.prod_altura_m) "+
                                "and a.prod_id = @idProduto and arm_motivo_bloqueio = 'PRODUTO VENCIDO O PROX.' " +
                                "or a.arm_reserva = 'False' and a.arm_bloqueado = 'True' "+
                                "and(arm_quantidade / pp.prod_fator_pulmao) >= (pp.prod_lastro_m * pp.prod_altura_m) "+
                                "and a.prod_id = @idProduto and arm_motivo_bloqueio = 'PRODUTO VENCIDO O PROX.' " +
                                "order by arm_vencimento";
                }


                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                //Atualiza o status do endereço como reservado
                string update = "update wms_armazenagem set arm_reserva = 'True' " +
                    "where prod_id = @idProduto and apa_codigo in (select apa_codigo from wms_reserva where ped_codigo = @codPedido) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao reservar as grandezas. \nDetalhes:" + ex.Message);
            }
        }

        //Resgistra a marcação de rota
        public void RegistraImpressao(string empresa, int codUsuario, int codManifesto, int codPedido)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros 
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);

                //String de consulta
                string select = "update wms_pedido set ped_impresso = 'SIM', ped_data_impressao = current_timestamp," +
                    "usu_codigo_impresso = @codUsuario ";

                if (codPedido > 0)
                {
                    select += "where ped_codigo = @codPedido and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                if (codManifesto > 0)
                {
                    select += "where mani_codigo = @codManifesto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a impressão do mapa de separação. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza o código do conferente no manifesto
        public void VinculaConferente(string empresa, string nmConferente, int codManifesto)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@nmConferente", nmConferente);

                //String de consulta
                string select = "update wms_manifesto set usu_codigo_conferente = (select usu_codigo from wms_usuario where usu_login = @nmConferente) " +
                    "where mani_codigo = @codManifesto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao vincular o conferente ao manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o manifesto por resumo
        public MapaSeparacaoCollection PesqResumoManifesto(int codManifesto, bool naoConferido, bool naoImpresso)
        {
            try
            {
                //Instância a camada de objêto
                MapaSeparacaoCollection pedidoCollection = new MapaSeparacaoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select first 1 " +
                                "distinct(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "r.rota_numero || ' - ' || r.rota_descricao as rota, " +
                                "m.mani_codigo, mani_letra, mm.mot_nome, v.vei_placa, " +
                                "(select count(ped_codigo) from wms_pedido where mani_codigo = p.mani_codigo ";


                if (naoConferido.Equals(true))
                {
                    select += "and ped_fim_conferencia is null ";
                }

                if (naoImpresso.Equals(true))
                {
                    select += "and ped_impresso is null ";
                }

                select += ") as cont_pedido, " +
                                "(select sum(ped_peso) from wms_pedido where mani_codigo = p.mani_codigo ";

                if (naoConferido.Equals(true))
                {
                    select += "and ped_fim_conferencia is null ";
                }

                if (naoImpresso.Equals(true))
                {
                    select += "and ped_impresso is null ";
                }

                select += ") as soma_peso " +
                "from wms_pedido p " +
                "inner join wms_cliente c " +
                "on c.cli_id = p.cli_id " +
                "inner join wms_rota r " +
                "on c.rota_codigo = r.rota_codigo " +
                "inner join wms_manifesto m " +
                "on m.mani_codigo = p.mani_codigo " +
                "left join wms_motorista mm " +
                "on mm.mot_codigo = m.mot_codigo " +
                "left join wms_veiculo v " +
                "on v.vei_codigo = m.vei_codigo " +
                "where p.mani_codigo = @codManifesto ";

                if (naoConferido.Equals(true))
                {
                    select += "and ped_fim_conferencia is null ";
                }

                if (naoImpresso.Equals(true))
                {
                    select += "and ped_impresso is null ";
                }



                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

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

                    if (linha["mani_letra"] != DBNull.Value)
                    {
                        pedido.letraPedido = Convert.ToString("| " + linha["mani_letra"] + " |");
                    }

                    if (linha["rota"] != DBNull.Value)
                    {
                        pedido.rotaPedido = Convert.ToString(linha["rota"]);
                    }


                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        pedido.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        pedido.nomeMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    //Adiona o objêto a coleção
                    pedidoCollection.Add(pedido);

                }
                //Retorna a coleção de cadastro encontrada
                return pedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório por resumo de manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens do pedido a pedido
        public ItemMapaSeparacaoCollection PesqItemResumoManifesto(int codManifesto, bool naoConferido, bool naoImpresso)
        {
            try
            {
                //Instância a camada de objêto
                ItemMapaSeparacaoCollection itemPedidoCollection = new ItemMapaSeparacaoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select m.mani_codigo, a.apa_endereco, pp.prod_codigo, pp.prod_descricao, pp.prod_fator_pulmao, " +
                                "sum(i.iped_quantidade) as quantidade, " +
                                "trunc(sum(iped_quantidade) / pp.prod_fator_pulmao) as qtd_fechada, u.uni_unidade as uni_fechada, " +
                                "mod(sum(iped_quantidade), pp.prod_fator_pulmao) as fracionado, uu.uni_unidade as uni_fracionada, " +
                                "coalesce(i.iped_lote, '') as iped_lote from wms_item_pedido i " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "inner join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "left join wms_motorista mm " +
                                "on mm.mot_codigo = m.mot_codigo " +
                                "left join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = i.prod_id " +
                                "left join wms_separacao s " +
                                "on s.prod_id = pp.prod_id " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = pp.uni_codigo_pulmao " +
                                "left join wms_unidade uu " +
                                "on uu.uni_codigo = pp.uni_codigo_picking " +
                                "where p.mani_codigo = @codManifesto ";

                if (naoConferido.Equals(true))
                {
                    select += "and ped_fim_conferencia is null ";
                }

                if (naoConferido.Equals(true))
                {
                    select += "and ped_impresso is null ";
                }

                select += "group by m.mani_codigo, a.apa_endereco, pp.prod_codigo, pp.prod_descricao, pp.prod_fator_pulmao, " +
                                "u.uni_unidade, uu.uni_unidade, coalesce(i.iped_lote, ''), apa_ordem " +
                                "order by a.apa_ordem ";




                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
                int i = 1;
                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItemMapaSeparacao itemPedido = new ItemMapaSeparacao();
                    //Controla a exibição do item no mapa de separação

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco"]);
                    }
                    else
                    {
                        itemPedido.enderecoProduto = "SEM PICKING";
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        itemPedido.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
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
                        //qtdFechada = Convert.ToInt32(linha["qtd_fechada"]);

                        //Verifica se existe grandezas reservada
                        /*if (linha["grandeza"] != DBNull.Value)
                        {
                            //Subtrai a grandeza na quantidade
                            itemPedido.qtdCaixaProduto = Convert.ToInt32(linha["qtd_fechada"]) - Convert.ToInt32(linha["grandeza"]);

                            //Recebe a quantidade do item
                            qtdFechada = Convert.ToInt32(linha["qtd_fechada"]) - Convert.ToInt32(linha["grandeza"]);
                        }*/
                    }

                    if (linha["uni_fechada"] != DBNull.Value)
                    {
                        itemPedido.uniCaixa = Convert.ToString(linha["uni_fechada"]);
                    }

                    if (linha["fracionado"] != DBNull.Value)
                    {
                        itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]);

                        /*qtdFracionada = Convert.ToInt32(linha["fracionado"]);

                        //Verifica se existe flow rack
                        if (linha["flow"] != DBNull.Value)
                        {
                            //Subtrai a quantidade do flow rack
                            itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]) - Convert.ToInt32(linha["flow"]);

                            qtdFracionada = Convert.ToInt32(linha["fracionado"]) - Convert.ToInt32(linha["flow"]);
                        }*/
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (linha["iped_lote"] != DBNull.Value)
                    {
                        itemPedido.lote = Convert.ToString(linha["iped_lote"]);
                    }

                    //if (qtdFechada + qtdFracionada > 0)
                    //{
                    //Adiona o objêto a coleção
                    itemPedidoCollection.Add(itemPedido);
                    //}
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

        //Pesquisa reentrega por resumo de manifesto
        public MapaSeparacaoCollection PesqReentregaResumoManifesto(int codManifesto)
        {
            try
            {
                //Instância a camada de objêto
                MapaSeparacaoCollection pedidoCollection = new MapaSeparacaoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select first 1 " +
                                "distinct(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "r.rota_numero || ' - ' || r.rota_descricao as rota, " +
                                "m.mani_codigo, mani_letra, mm.mot_nome, v.vei_placa, " +
                                "(select count(ped_codigo) from wms_pedido pp where mani_codigo = p.mani_codigo and pp.ped_codigo in (select ped_codigo from wms_pedido_ocorrencia po where mani_codigo = @codManifesto)) as cont_pedido, " +
                                "(select sum(ped_peso) from wms_pedido pp where mani_codigo = p.mani_codigo and pp.ped_codigo in (select ped_codigo from wms_pedido_ocorrencia po where mani_codigo = @codManifesto)) as soma_peso " +
                "from wms_pedido p " +
                "inner join wms_cliente c " +
                "on c.cli_id = p.cli_id " +
                "inner join wms_rota r " +
                "on c.rota_codigo = r.rota_codigo " +
                "inner join wms_manifesto m " +
                "on m.mani_codigo = p.mani_codigo " +
                "left join wms_motorista mm " +
                "on mm.mot_codigo = m.mot_codigo " +
                "left join wms_veiculo v " +
                "on v.vei_codigo = m.vei_codigo " +
                "where p.mani_codigo = @codManifesto and p.ped_codigo in (select ped_codigo from wms_pedido_ocorrencia po where mani_codigo = @codManifesto)";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

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

                    if (linha["mani_letra"] != DBNull.Value)
                    {
                        pedido.letraPedido = Convert.ToString("| " + linha["mani_letra"] + " |");
                    }

                    if (linha["rota"] != DBNull.Value)
                    {
                        pedido.rotaPedido = Convert.ToString(linha["rota"]);
                    }


                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        pedido.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        pedido.nomeMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    //Adiona o objêto a coleção
                    pedidoCollection.Add(pedido);

                }
                //Retorna a coleção de cadastro encontrada
                return pedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório por resumo de manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens do pedido a pedido
        public ItemMapaSeparacaoCollection PesqReentregaItemResumoManifesto(int codManifesto)
        {
            try
            {
                //Instância a camada de objêto
                ItemMapaSeparacaoCollection itemPedidoCollection = new ItemMapaSeparacaoCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "select m.mani_codigo, a.apa_endereco, pp.prod_codigo, pp.prod_descricao, pp.prod_fator_pulmao, " +
                                "sum(i.iped_quantidade) as quantidade, " +
                                "trunc(sum(iped_quantidade) / pp.prod_fator_pulmao) as qtd_fechada, u.uni_unidade as uni_fechada, " +
                                "mod(sum(iped_quantidade), pp.prod_fator_pulmao) as fracionado, uu.uni_unidade as uni_fracionada, " +
                                "coalesce(i.iped_lote, '') as iped_lote from wms_item_pedido i " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "inner join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "left join wms_motorista mm " +
                                "on mm.mot_codigo = m.mot_codigo " +
                                "left join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = i.prod_id " +
                                "left join wms_separacao s " +
                                "on s.prod_id = pp.prod_id " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = pp.uni_codigo_pulmao " +
                                "left join wms_unidade uu " +
                                "on uu.uni_codigo = pp.uni_codigo_picking " +
                                "where p.mani_codigo = @codManifesto  and p.ped_codigo in (select ped_codigo from wms_pedido_ocorrencia po where mani_codigo = @codManifesto) ";

                select += "group by m.mani_codigo, a.apa_endereco, pp.prod_codigo, pp.prod_descricao, pp.prod_fator_pulmao, " +
                                "u.uni_unidade, uu.uni_unidade, coalesce(i.iped_lote, ''), apa_ordem " +
                                "order by a.apa_ordem ";




                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
                int i = 1;
                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItemMapaSeparacao itemPedido = new ItemMapaSeparacao();
                    //Controla a exibição do item no mapa de separação

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itemPedido.enderecoProduto = Convert.ToString(linha["apa_endereco"]);
                    }
                    else
                    {
                        itemPedido.enderecoProduto = "SEM PICKING";
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        itemPedido.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
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
                        //qtdFechada = Convert.ToInt32(linha["qtd_fechada"]);

                        //Verifica se existe grandezas reservada
                        /*if (linha["grandeza"] != DBNull.Value)
                        {
                            //Subtrai a grandeza na quantidade
                            itemPedido.qtdCaixaProduto = Convert.ToInt32(linha["qtd_fechada"]) - Convert.ToInt32(linha["grandeza"]);

                            //Recebe a quantidade do item
                            qtdFechada = Convert.ToInt32(linha["qtd_fechada"]) - Convert.ToInt32(linha["grandeza"]);
                        }*/
                    }

                    if (linha["uni_fechada"] != DBNull.Value)
                    {
                        itemPedido.uniCaixa = Convert.ToString(linha["uni_fechada"]);
                    }

                    if (linha["fracionado"] != DBNull.Value)
                    {
                        itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]);

                        /*qtdFracionada = Convert.ToInt32(linha["fracionado"]);

                        //Verifica se existe flow rack
                        if (linha["flow"] != DBNull.Value)
                        {
                            //Subtrai a quantidade do flow rack
                            itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]) - Convert.ToInt32(linha["flow"]);

                            qtdFracionada = Convert.ToInt32(linha["fracionado"]) - Convert.ToInt32(linha["flow"]);
                        }*/
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (linha["iped_lote"] != DBNull.Value)
                    {
                        itemPedido.lote = Convert.ToString(linha["iped_lote"]);
                    }

                    //if (qtdFechada + qtdFracionada > 0)
                    //{
                    //Adiona o objêto a coleção
                    itemPedidoCollection.Add(itemPedido);
                    //}
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



        //Remove os pedidos bloqueados do manifesto
        public void RemoverBloqueado(int codManifesto)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "update wms_pedido set mani_codigo = null where mani_codigo = @codManifesto and ped_bloqueado = 'True'";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao remover os pedidos bloqueados. \nDetalhes:" + ex.Message);
            }
        }

        //Remove os pedidos bloqueados do manifesto
        public void RemoverNaoFinalizadoFlow(int codManifesto)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string select = "update wms_pedido set mani_codigo = null where mani_codigo = @codManifesto and ped_flow_rack = 'SIM' and ped_fim_flow_rack is null";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao remover os pedidos não finalizados no flow rack. \nDetalhes:" + ex.Message);
            }
        }


    }
}
