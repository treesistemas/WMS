using Dados;
using System.Data;
using ObjetoTransferencia;
using System;



namespace Negocios
{
    public class ReservaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a estacao
        public EstacaoCollection PesqEstacao()
        {
            try
            {
                //Instância a camada de objêto
                EstacaoCollection estacaoCollection = new EstacaoCollection();
                conexao.LimparParametros();

                //String de consulta
                string select = "select est_codigo, est_descricao from wms_estacao order by est_codigo";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    Estacao estacao = new Estacao();

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        estacao.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        estacao.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    //Adiona o objêto a coleção
                    estacaoCollection.Add(estacao);
                }
                //Retorna a coleção de cadastro encontrada
                return estacaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as estações. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa os itens do pedido
        public ReservaCollection PesqReserva(string codPedido, string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                //Instância a camada de objêto
                ReservaCollection reservaCollection = new ReservaCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select rs.res_codigo, " +
                                /*Pesquisa o picking de caixa*/
                                "(select a.apa_codigo from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'CAIXA' and prod_id = rs.prod_id) as apa_codigo_cxa, " +
                                /*Endereço do picking*/
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'CAIXA' and prod_id = rs.prod_id) as endereco_cxa, " +
                                /*Pesquisa o picking de flow rack*/
                                "(select a.apa_codigo from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = rs.prod_id) as apa_cod_flow, " +
                                /*Endereço do flow rack*/
                                "(select a.apa_endereco from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = 'FLOWRACK' and prod_id = rs.prod_id) as endereco_flow,  " +
                                "rs.apa_codigo, ap.apa_endereco, rs.prod_id, prod_codigo, prod_descricao, rs.res_quantidade as estoque_pulmao, uni.uni_unidade, " +
                                "rs.res_validade, rs.res_peso, rs.res_lote, rs.mani_codigo, rs.ped_codigo, u.usu_login, rs.res_data, " +
                                "rs.res_tipo " +
                                "from wms_reserva rs " +
                                "inner join wms_produto p " +
                                "on p.prod_id = rs.prod_id " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = rs.apa_codigo " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = rs.usu_codigo " +
                                "left join wms_unidade uni " +
                                "on uni.uni_codigo = p.uni_codigo_pulmao ";

                if(!(codPedido.Equals("") || codPedido.Equals(string.Empty)))
                {
                    select += "where rs.ped_codigo = @codPedido " +
                                "order by ap.apa_ordem ";
                }
                else
                {
                    select += "where rs.res_data between @dataInicial and @dataFinal " +
                                "and rs.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)  order by ap.apa_ordem ";
                }
                                

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    Reserva reserva = new Reserva();

                    if (linha["res_codigo"] != DBNull.Value)
                    {
                        reserva.codReserva = Convert.ToInt32(linha["res_codigo"]);
                    }

                    if (linha["apa_codigo_cxa"] != DBNull.Value)
                    {
                        reserva.codPicking = Convert.ToInt32(linha["apa_codigo_cxa"]);
                        reserva.enderecoPicking = Convert.ToString(linha["endereco_cxa"]);
                    }
                    else if (linha["apa_cod_flow"] != DBNull.Value && linha["apa_codigo_cxa"] == DBNull.Value)
                    {
                        reserva.codPicking = Convert.ToInt32(linha["apa_cod_flow"]);
                        reserva.enderecoPicking = Convert.ToString(linha["endereco_flow"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        reserva.codPulmao = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        reserva.enderecoPulmao = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        reserva.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        reserva.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        reserva.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["estoque_pulmao"] != DBNull.Value)
                    {
                        reserva.qtdReservada = Convert.ToInt32(linha["estoque_pulmao"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        reserva.unidadePulmao = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["res_validade"] != DBNull.Value)
                    {
                        reserva.dataVencimento = Convert.ToDateTime(linha["res_validade"]).Date;
                    }

                    if (linha["res_peso"] != DBNull.Value)
                    {
                        reserva.pesoProduto = Convert.ToDouble(linha["res_peso"]);
                    }

                    if (linha["res_lote"] != DBNull.Value)
                    {
                        reserva.loteProduto = Convert.ToString(linha["res_lote"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        reserva.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        reserva.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                        reserva.status = "PENDENTE";
                    }

                    if (linha["res_tipo"] != DBNull.Value)
                    {
                        reserva.tipoReserva = Convert.ToString(linha["res_tipo"]);
                    }

                    if (linha["res_data"] != DBNull.Value)
                    {
                        reserva.dataReserva = Convert.ToDateTime(linha["res_data"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        reserva.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }



                    //Adiona o objêto a coleção
                    reservaCollection.Add(reserva);
                }
                //Retorna a coleção de cadastro encontrada
                return reservaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens reservados. \nDetalhes:" + ex.Message);
            }
        }

        public ReservaCollection PesqAbastecimento(string codAbastecimento, string status, string dataInicial, string dataFinal, int codEstacao, string empresa)
        {
            try
            {
                //Instância a camada de objêto
                ReservaCollection reservaCollection = new ReservaCollection();
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select i.aba_codigo, iaba_codigo, i.iaba_data, i.apa_codigo_picking, ap1.apa_endereco as endereco_picking, " +
                                "i.apa_codigo_pulmao, ap.apa_endereco as endereco_pulmao, i.prod_id, prod_codigo, prod_descricao, prod_fator_pulmao, " +
                                "i.iaba_estoque_pulmao/p.prod_fator_pulmao as estoque_pulmao, uni.uni_unidade, " +
                                "i.iaba_vencimento_pulmao, (p.prod_peso * i.iaba_estoque_pulmao/p.prod_fator_pulmao) peso_reservado, i.iaba_lote_pulmao, iaba_tipo_analise, u.usu_login, i.iaba_status " +
                                "from wms_item_abastecimento i " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "inner join wms_apartamento ap1 " +
                                "on ap1.apa_codigo = i.apa_codigo_picking " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = i.apa_codigo_pulmao " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = i.usu_codigo " +
                                "left join wms_unidade uni " +
                                "on uni.uni_codigo = p.uni_codigo_pulmao ";



                if (!(codAbastecimento.Equals(null) || codAbastecimento.Equals("")))
                {
                    select += "where aba_codigo = @codAbastecimento ";

                    if (!(status.Equals("SELECIONE") || status.Equals("")))
                    {
                        select += "and iaba_status = @status ";
                    }

                    if(codEstacao > 0)
                    {
                        select += "and est_codigo = @codEstacao ";
                    }
                }
                else
                {
                    select += "where iaba_data between @dataInicial and @dataFinal ";

                    if (!(status.Equals("SELECIONE") || status.Equals("")))
                    {
                        select += "and iaba_status = @status ";
                    }

                }

                select += "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by aba_codigo, iaba_status desc, ap.apa_ordem ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    Reserva reserva = new Reserva();

                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        reserva.codAbastecimento = Convert.ToString(linha["aba_codigo"]);
                    }
                    else
                    {
                        reserva.codAbastecimento = "SISTEMA";
                    }

                    if (linha["iaba_codigo"] != DBNull.Value)
                    {
                        reserva.codReserva = Convert.ToInt32(linha["iaba_codigo"]);
                    }

                    if (linha["apa_codigo_picking"] != DBNull.Value)
                    {
                        reserva.codPicking = Convert.ToInt32(linha["apa_codigo_picking"]);
                    }

                    if (linha["endereco_picking"] != DBNull.Value)
                    {
                        reserva.enderecoPicking = Convert.ToString(linha["endereco_picking"]);
                    }

                    if (linha["apa_codigo_pulmao"] != DBNull.Value)
                    {
                        reserva.codPulmao = Convert.ToInt32(linha["apa_codigo_pulmao"]);
                    }

                    if (linha["endereco_pulmao"] != DBNull.Value)
                    {
                        reserva.enderecoPulmao = Convert.ToString(linha["endereco_pulmao"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        reserva.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        reserva.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        reserva.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        reserva.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        reserva.fatorPulmao = 1;
                    }

                    if (linha["estoque_pulmao"] != DBNull.Value)
                    {
                        reserva.qtdReservada = Convert.ToInt32(linha["estoque_pulmao"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        reserva.unidadePulmao = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["iaba_vencimento_pulmao"] != DBNull.Value)
                    {
                        reserva.dataVencimento = Convert.ToDateTime(linha["iaba_vencimento_pulmao"]).Date;
                    }

                    if (linha["peso_reservado"] != DBNull.Value)
                    {
                        reserva.pesoProduto = Convert.ToDouble(linha["peso_reservado"]);
                    }

                    if (linha["iaba_lote_pulmao"] != DBNull.Value)
                    {
                        reserva.loteProduto = Convert.ToString(linha["iaba_lote_pulmao"]);
                    }

                    if (linha["iaba_data"] != DBNull.Value)
                    {
                        reserva.dataReserva = Convert.ToDateTime(linha["iaba_data"]).Date;

                        reserva.tipoReserva = "ABASTECIMENTO";
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        reserva.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["iaba_tipo_analise"] != DBNull.Value)
                    {
                        reserva.tipoAnalise = Convert.ToString(linha["iaba_tipo_analise"]);
                    }

                    if (linha["iaba_status"] != DBNull.Value)
                    {
                        reserva.status = Convert.ToString(linha["iaba_status"]);
                    }



                    //Adiona o objêto a coleção
                    reservaCollection.Add(reserva);
                }
                //Retorna a coleção de cadastro encontrada
                return reservaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens do abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Libera as reserva
        public void LiberarReserva(int codReserva, int codPulmao, int idProduto, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codReserva", codReserva);
                conexao.AdicionarParamentros("@codPulmao", codPulmao);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                // deleta a reserva
                string delete = "delete from wms_reserva where res_codigo = @codReserva and prod_id = @idProduto ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete);

                //Libera o produto reservado
                string update = "update wms_armazenagem set arm_reserva = 'False', arm_bloqueado = 'False', arm_motivo_bloqueio = null where apa_codigo = @codPulmao and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Cancela os itens de abastecimento
        public void TransferirAbastecimento(string codOrdem, int codOAItem, int codPicking, int codPulmao,
            int idProduto, int qtdProduto, string vencimentoProduto, double pesoProduto, string loteProduto, string tipoAnalise, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codOrdem", codOrdem);
                conexao.AdicionarParamentros("@codOAItem", codOAItem);
                conexao.AdicionarParamentros("@codPicking", codPicking);
                conexao.AdicionarParamentros("@codPulmao", codPulmao);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@qtdProduto", qtdProduto);
                conexao.AdicionarParamentros("@vencimentoProduto", vencimentoProduto);
                conexao.AdicionarParamentros("@pesoProduto", pesoProduto);
                conexao.AdicionarParamentros("@loteProduto", loteProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                if (tipoAnalise.Equals("ANÁLISE DO PULMÃO"))
                {
                    //Diminui a qtd do  do produto armazenado
                    string updateAnalisePulmao = "update wms_armazenagem set arm_quantidade = arm_quantidade - @qtdProduto, arm_peso = arm_peso - @pesoProduto," +
                        "  arm_reserva = 'False' where apa_codigo = @codPulmao and prod_id = @idProduto (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, updateAnalisePulmao);

                    //Atualiza apartamento se o produto se a qtd for zero
                    string update = "update wms_apartamento set apa_status = 'Vago' where apa_codigo = @codPulmao and (select count(prod_id) from wms_armazenagem where apa_codigo = @codPulmao) = 0 (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, update);

                    //Deleta o produto se a qtd for zero
                    string delete = "delete from wms_armazenagem where apa_codigo = @codPulmao and prod_id = @idProduto and arm_quantidade = 0 ";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, delete);
                }
                else if (tipoAnalise.Equals("ANÁLISE DE PICKING"))
                {
                    //Libera o picking do produto
                    string updateAnalisePicking = "update wms_separacao set sep_estoque = coalesce(sep_estoque, 0) - @qtdProduto, sep_peso = sep_peso - @pesoProduto, (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                    "sep_bloqueado = 'False' where apa_codigo = @codPulmao and prod_id = @idProduto";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, updateAnalisePicking);
                }

                //Libera o picking do produto
                string updatePicking = "update wms_separacao set sep_estoque = coalesce(sep_estoque, 0) + @qtdProduto, sep_peso = sep_peso + @pesoProduto, " +
                "sep_validade = @vencimentoProduto, sep_lote = @loteProduto, sep_bloqueado = 'False' where apa_codigo = @codPicking and prod_id = @idProduto (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updatePicking);

                //Finaliza o abastecimento
                string updateItem = "update wms_item_abastecimento set iaba_status = 'FINALIZADO' where iaba_codigo = @codOAItem and prod_id = @idProduto (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updateItem);

                //Atualiza o status da ordem de abastecimento
                string updateOrdem = "update wms_abastecimento set aba_status = 'INICIADA' where aba_codigo = @codOrdem (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and not aba_status = 'INICIADA' ";
                conexao.ExecuteManipulacao(CommandType.Text, updateOrdem);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Registra no rastreamento
        public void InserirRastreamento(int codUsuario, string codAbastecimento, int idProduto, int codApartamentoOrigem,
            int codApartamentoDestino, int quantidadeDestino, string vencimentoDestino, double pesoDestino, string loteDestino, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codAbastecimento", codAbastecimento);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codApartamentoOrigem", codApartamentoOrigem);
                conexao.AdicionarParamentros("@codApartamentoDestino", codApartamentoDestino);                
                conexao.AdicionarParamentros("@quantidadeDestino", quantidadeDestino);                
                conexao.AdicionarParamentros("@pesoDestino", pesoDestino);                
                conexao.AdicionarParamentros("@vencimentoDestino", vencimentoDestino);
                conexao.AdicionarParamentros("@loteDestino", loteDestino);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de insert - insere o endereço  
                string insert = "insert into wms_rastreamento_armazenagem (rast_codigo, rast_operacao, rast_data, usu_codigo, aba_codigo," +
                "apa_codigo_origem, apa_codigo_destino, prod_id, arm_quantidade_destino, arm_peso_destino," +
                "arm_vencimento_destino, arm_lote_destino, conf_codigo)" +
                "values" +
                "(gen_id(gen_wms_rast_armazenagem, 1), 'ABASTECIMENTO', current_timestamp, @codUsuario, @codAbastecimento, " +
                "@codApartamentoOrigem, @codApartamentoDestino, @idProduto, @quantidadeDestino, @pesoDestino," +
                "@vencimentoDestino, @loteDestino, (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";


                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a operação no rastreamento de operações! \nDetalhes: " + ex.Message);
            }
        }



        //Cancela os itens de abastecimento
        public void CancelarAbastecimento(int codOAItem, int codPicking, int codPulmao, int idProduto, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codOAItem", codOAItem);
                conexao.AdicionarParamentros("@codPicking", codPicking);
                conexao.AdicionarParamentros("@codPulmao", codPulmao);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //Libera o produto armazenado
                string update = "update wms_armazenagem set arm_reserva = 'False' where apa_codigo = @codPulmao and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //Libera o picking do produto
                string updatePicking = "update wms_separacao set sep_bloqueado = 'False' where apa_codigo = @codPicking and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updatePicking);

                //Cancela o abastecimento
                string updateItem = "update wms_item_abastecimento set iaba_status = 'CANCELADO' where iaba_codigo = @codOAItem and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updateItem);






            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do abastecimento. \nDetalhes:" + ex.Message);
            }
        }

        //Cancela os itens de abastecimento
        public void AtualizarStatusAbastecimento(string codOrdem, int codUsuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codOrdem", codOrdem);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@empresa", empresa);

                string select = "select count(prod_id) as count_pendente from wms_item_abastecimento where aba_codigo = @codOrdem and iaba_status = 'PENDENTE'";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if(Convert.ToInt32(linha["count_pendente"]) == 0)
                    {
                        //Atualiza o status da ordem de abastecimento
                        string updateOrdem = "update wms_abastecimento set aba_status = 'FINALIZADO', usu_codigo_final = @codUsuario, " +
                                             "aba_data_final = current_timestamp  where aba_codigo = @codOrdem and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                        //Executa o script no banco
                        conexao.ExecuteManipulacao(CommandType.Text, updateOrdem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o status da ordem. \nDetalhes:" + ex.Message);
            }
        }

    }
}
