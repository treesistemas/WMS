using Dados;
using System;
using ObjetoTransferencia;
using System.Data;
using System.ComponentModel.Design;

namespace Negocios
{
    public class ConferenciaManifestoNegocios
    {
        //Chama a camada de dados
        Conexao conexao = new Conexao();

        //Pesquisa o manifesto por resumo
        public Manifesto PesqManifesto(int codManifesto, bool reentrega, string empresa)
        {
            try
            {
                //Instância a camada de objêto
                Manifesto manifesto = new Manifesto();
                //Limpa os parâmetro
                conexao.LimparParametros();
                //Adicionar os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string script = "select first 1 m.mani_codigo, v.vei_placa, ped_inicio_conferencia, ped_fim_conferencia, u.usu_codigo, usu_login, " +
                                "(select count(ped_codigo) from wms_pedido where mani_codigo = p.mani_codigo ) as cont_pedido, " +
                                "(select sum(ped_peso) from wms_pedido where mani_codigo = p.mani_codigo ) as soma_peso " +
                                "from wms_pedido p " +
                                "inner join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "left join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo " +
                                 "left join wms_usuario u " +
                                "on p.usu_codigo_separador = u.usu_codigo ";

                if (reentrega == false)
                {
                    script += "where p.mani_codigo = @codManifesto ";
                }
                else
                {
                    script += "where p.ped_codigo in (select ped_codigo from wms_pedido_ocorrencia where poco_reentrega = 'True' and poco_conferencia is null and mani_codigo_ocorrencia = @codManifesto) " +
                               "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        manifesto.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["ped_inicio_conferencia"] != DBNull.Value)
                    {
                        manifesto.inicioConferencia = Convert.ToDateTime(linha["ped_inicio_conferencia"]);
                    }


                    if (linha["ped_fim_conferencia"] != DBNull.Value)
                    {
                        manifesto.fimConferencia = Convert.ToDateTime(linha["ped_fim_conferencia"]);
                    }

                    if (linha["usu_codigo"] != DBNull.Value)
                    {
                        manifesto.codSeparador = Convert.ToInt32(linha["usu_codigo"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        manifesto.loginSeparador = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["cont_pedido"] != DBNull.Value)
                    {
                        manifesto.pedidoManifesto = Convert.ToInt32(linha["cont_pedido"]);
                    }

                    if (linha["soma_peso"] != DBNull.Value)
                    {
                        manifesto.pesoManifesto = Convert.ToDouble(linha["soma_peso"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        manifesto.veiculoManifesto = Convert.ToString(linha["vei_placa"]);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return manifesto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório por resumo de manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens do pedido a pedido
        public ItensPedidoCollection PesqItensManifesto(int codManifesto, bool reentrega, string empresa)
        {
            try
            {
                //Instância a camada de objêto
                ItensPedidoCollection itemPedidoCollection = new ItensPedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string script = "select m.mani_codigo, a.apa_endereco, pp.prod_id, pp.prod_codigo, pp.prod_descricao, pp.prod_fator_pulmao, pp.prod_peso_variavel, " +
                                "sum(i.iped_quantidade) as iped_quantidade, sum(i.IPED_QTD_CONFERENCIA) as iped_qtd_conferencia, " +
                                "sum(i.IPED_QTD_CORTE_CONFERENCIA) as iped_qtd_corte_conferencia, " +
                                "i.iped_validade, i.iped_lote, i.iped_data_conferencia, uu.uni_unidade as uni_fracionada, u.uni_unidade as uni_fechada " +
                                "from wms_item_pedido i " +
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
                                "on uu.uni_codigo = pp.uni_codigo_picking ";

                if (reentrega == false)
                {
                    script += "where p.mani_codigo = @codManifesto ";
                }
                else
                {
                    script += "where p.ped_codigo in (select ped_codigo from wms_pedido_ocorrencia where poco_reentrega = 'True' and poco_conferencia is null and mani_codigo_ocorrencia = @codManifesto) " +
                              "and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }


                script += "group by m.mani_codigo, a.apa_endereco, pp.prod_id, pp.prod_codigo, pp.prod_descricao, pp.prod_fator_pulmao, prod_peso_variavel, " +
                 "i.iped_data_conferencia, u.uni_unidade, uu.uni_unidade, i.iped_validade, i.iped_lote, apa_ordem " +
                 "order by a.apa_ordem "; //Modificado


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItensPedido itemPedido = new ItensPedido();

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

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itemPedido.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itemPedido.descProduto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        itemPedido.pesoVariavel = Convert.ToBoolean(linha["prod_peso_variavel"]);
                    }

                    if (linha["iped_quantidade"] != DBNull.Value)
                    {
                        if (linha["prod_peso_variavel"].Equals("True"))
                        {
                            itemPedido.qtdProduto = Convert.ToDouble(linha["iped_quantidade"]);
                        }
                        else
                        {
                            itemPedido.qtdProduto = Convert.ToInt32(linha["iped_quantidade"]);
                        }
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (linha["iped_qtd_conferencia"] != DBNull.Value)
                    {
                        if (linha["prod_peso_variavel"].Equals("True"))
                        {
                            itemPedido.qtdConferida = Convert.ToDouble(linha["iped_qtd_conferencia"]);
                        }
                        else
                        {
                            itemPedido.qtdConferida = Convert.ToInt32(linha["iped_qtd_conferencia"]);
                        }
                    }

                    if (linha["iped_qtd_corte_conferencia"] != DBNull.Value)
                    {
                        if (linha["prod_peso_variavel"].Equals("True"))
                        {
                            itemPedido.qtdCorte = Convert.ToDouble(linha["iped_qtd_corte_conferencia"]);
                        }
                        else
                        {
                            itemPedido.qtdCorte = Convert.ToInt32(linha["iped_qtd_corte_conferencia"]);
                        }
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itemPedido.qtdCaixaProduto = Convert.ToString(linha["prod_fator_pulmao"]);

                        if (linha["prod_peso_variavel"].Equals("True"))
                        {
                            if (itemPedido.qtdConferida > 0)
                            {
                                itemPedido.qtdCaixaConferida = Convert.ToDouble(itemPedido.qtdConferida / 1);
                            }
                        }
                        else
                        {
                            if (itemPedido.qtdConferida > 0)
                            {
                                itemPedido.qtdCaixaConferida = Math.Truncate(itemPedido.qtdConferida / Convert.ToInt32(linha["prod_fator_pulmao"]));

                                itemPedido.qtdUnidadeConferida = (itemPedido.qtdConferida % Convert.ToInt32(linha["prod_fator_pulmao"]));
                            }
                        }
                    }

                    if (linha["uni_fechada"] != DBNull.Value)
                    {
                        itemPedido.uniCaixa = Convert.ToString(linha["uni_fechada"]);
                    }

                    if (linha["iped_validade"] != DBNull.Value)
                    {
                        itemPedido.vencimentoProduto = Convert.ToDateTime(linha["iped_validade"]);
                    }

                    if (linha["iped_lote"] != DBNull.Value)
                    {
                        itemPedido.loteProduto = Convert.ToString(linha["iped_lote"]);
                    }

                    if (linha["iped_data_conferencia"] != DBNull.Value)
                    {
                        itemPedido.dataConferencia = Convert.ToDateTime(linha["iped_data_conferencia"]);
                    }


                    //Adiona o objêto a coleção
                    itemPedidoCollection.Add(itemPedido);

                }
                //Retorna a coleção de cadastro encontrada
                return itemPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens do manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o produto pelo código de barra
        public Barra PesqProduto(string codBarra, string empresa)
        {
            try
            {
                //Instância a camada de objêto
                Barra barra = new Barra();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string script = "select b.prod_id, b.bar_multiplicador, b.bar_peso from wms_barra b " +
                                "where b.bar_numero = @codBarra and b.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        barra.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["bar_multiplicador"] != DBNull.Value)
                    {
                        barra.multiplicador = Convert.ToInt32(linha["bar_multiplicador"]);
                    }

                    if (linha["bar_peso"] != DBNull.Value)
                    {
                        barra.peso = Convert.ToDouble(linha["bar_peso"]);
                    }
                }

                //Retorna o objêto
                return barra;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o produto pelo código de barra
        public EnderecoPicking PesqEndereco(int idProduto, string empresa)
        {
            try
            {
                //Instância a camada de objêto
                EnderecoPicking endereco = new EnderecoPicking();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string script = "select s.prod_id, s.sep_validade, s.sep_lote from wms_separacao s where prod_id = @idProduto " +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["sep_lote"]);
                    }
                }

                //Retorna o objêto
                return endereco;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar às informações do endereço. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o produto pelo código de barra
        public ItensPedidoCollection PesqPedidoPesoVariavel(int codManifesto, int idProduto)
        {
            try
            {
                //Instância a camada de objêto
                ItensPedidoCollection itensPedidoCollection = new ItensPedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@idProduto", idProduto);
               

                //String de consulta
                string script = "select mani_codigo, p.ped_codigo, i.prod_id, i.iped_quantidade, vol_volume, i.iped_qtd_conferencia, i.iped_qtd_corte_conferencia from wms_pedido p " +
                                "inner join wms_item_pedido i " +
                                "on i.ped_codigo = p.ped_codigo " +
                                "inner join wms_produto s " +
                                "on s.prod_id = i.prod_id " +
                                "where mani_codigo = @codManifesto and i.prod_id = @idProduto and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItensPedido itensPedido = new ItensPedido();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itensPedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itensPedido.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["iped_quantidade"] != DBNull.Value)
                    {
                        itensPedido.qtdProduto = Convert.ToDouble(linha["iped_quantidade"]);
                    }

                    if (linha["vol_volume"] != DBNull.Value)
                    {
                        itensPedido.volume = Convert.ToInt32(linha["vol_volume"]);
                    }

                    if (linha["iped_qtd_conferencia"] != DBNull.Value)
                    {
                        itensPedido.qtdConferida = Convert.ToDouble(linha["iped_qtd_conferencia"]);
                    }

                    if (linha["iped_qtd_corte_conferencia"] != DBNull.Value)
                    {
                        itensPedido.qtdCorte = Convert.ToDouble(linha["iped_qtd_corte_conferencia"]);
                    }

                    //Adiciona a coleção
                    itensPedidoCollection.Add(itensPedido);
                }

                //Retorna o objêto
                return itensPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os pedidos. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o produto pelo código de barra
        public ItensPedidoCollection PesqPedidoItemCorte(int codManifesto, int idProduto)
        {
            try
            {
                //Instância a camada de objêto
                ItensPedidoCollection itensPedidoCollection = new ItensPedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@idProduto", idProduto);

                //String de consulta
                string script = "select mani_codigo, p.ped_codigo, i.prod_id, i.iped_quantidade, i.iped_qtd_conferencia, i.iped_qtd_corte_conferencia from wms_pedido p " +
                                "inner join wms_item_pedido i " +
                                "on i.ped_codigo = p.ped_codigo " +
                                "inner join wms_produto s " +
                                "on s.prod_id = i.prod_id " +
                                "where mani_codigo = @codManifesto and i.prod_id = @idProduto";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItensPedido itensPedido = new ItensPedido();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itensPedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itensPedido.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["iped_quantidade"] != DBNull.Value)
                    {
                        itensPedido.qtdProduto = Convert.ToDouble(linha["iped_quantidade"]);
                    }

                    if (linha["iped_qtd_conferencia"] != DBNull.Value)
                    {
                        itensPedido.qtdConferida = Convert.ToDouble(linha["iped_qtd_conferencia"]);
                    }

                    if (linha["iped_qtd_corte_conferencia"] != DBNull.Value)
                    {
                        itensPedido.qtdCorte = Convert.ToDouble(linha["iped_qtd_corte_conferencia"]);
                    }

                    //Adiciona a coleção
                    itensPedidoCollection.Add(itensPedido);
                }

                //Retorna o objêto
                return itensPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens do corte. \nDetalhes:" + ex.Message);
            }
        }


        //Resgistra o inicio de conferencia
        public void RegistraInicioConferencia(int codUsuario, int codManifesto)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros                
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string script = "update wms_pedido set ped_inicio_conferencia = current_timestamp," +
                    "usu_codigo_conferente = @codUsuario where mani_codigo = @codManifesto";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar o inicio de conferencia. \nDetalhes:" + ex.Message);
            }
        }

        //Resgistra o separador
        public void RegistraSeparador(int codSeparador, int codManifesto)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros                
                conexao.AdicionarParamentros("@codSeparador", codSeparador);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);

                //String de consulta
                string script = "update wms_pedido set usu_codigo_separador = @codSeparador where mani_codigo = @codManifesto";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar o separador. \nDetalhes:" + ex.Message);
            }
        }


        //Salva a conferência do itens
        public void RegistrarConferenciaItem(int idProduto, int qtdConferida, DateTime vencimento, string lote, int codManifesto, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@qtdConferida", qtdConferida);
                conexao.AdicionarParamentros("@vencimento", vencimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de atualização - Atualiza a conferencia do item
                string update = "update wms_item_pedido set iped_qtd_conferencia = iped_quantidade, iped_validade = @vencimento, iped_lote = @lote and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa), " + //modifcado
                                "iped_data_conferencia = current_timestamp where prod_id = @idProduto and ped_codigo in (select ped_codigo from wms_pedido where mani_codigo = @codManifesto)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //String de atualização - Atualiza a quantidade do picking de separação
                string update1 = "update wms_separacao set sep_estoque = (sep_estoque - @qtdConferida) where prod_id = @idProduto and sep_tipo = 'CAIXA'";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update1);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro registrar a conferência. \nDetalhes:" + ex.Message);
            }
        }

        //Registra o corte
        public void RegistrarCorteItem(int codPedido, int idProduto, int qtdCorte, int codConferente, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@qtdCorte", qtdCorte);
                conexao.AdicionarParamentros("@codConferente", codConferente);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de atualização - Atualiza a conferencia do pedido
                string update = "update wms_item_pedido set iped_qtd_corte_conferencia = @qtdCorte where ped_codigo = @codPedido and prod_id = @idProduto" +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //Registra o corte
                string insert = "insert into wms_corte_produto (corte_codigo, ped_codigo, prod_id, corte_data, corte_quantidade, usu_codigo, conf_codigo) " +
                                "Values " +
                                "(gen_id(gen_wms_corte, 1), @codPedido, @idProduto, current_time, @qtdCorte, @codConferente)" +
                                "(select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro registrar o corte do item. \nDetalhes:" + ex.Message);
            }
        }


        //Resgistra o fim de conferencia
        public void RegistraFimConferencia(int codManifesto, bool reentrega, int codConferente, int codSeparador, string empresa)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros                
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codConferente", codConferente);
                conexao.AdicionarParamentros("@codSeparador", codSeparador);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string script = null;

                if (reentrega == false)
                {
                    //String de consulta
                    script = "update wms_pedido set ped_fim_conferencia = current_timestamp where mani_codigo = @codManifesto " +
                        "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }
                else
                {
                    //String de consulta
                    script = "update wms_pedido_ocorrencia set poco_conferencia = current_timestamp, usu_codigo_conferente = @codConferente," +
                             "usu_codigo_separador = @codSeparador where mani_codigo = @codManifesto and poco_reentrega = 'True' and poco_conferencia is null" +
                             "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                }
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, script);
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar o fim de conferencia. \nDetalhes:" + ex.Message);
            }
        }


    }
}
