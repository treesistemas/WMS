using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class DigitacaoInventarioNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o inventario aberto
        public Inventarios PesqInventario()
        {
            try
            {
                //String de consulta
                string select = "select inv_codigo, inv_descricao, usu_login, inv_data_inicial, inv_tipo, " +
                                "int_rotativo, inv_auditoria, inv_importar_erp, inv_cont_picking, inv_cont_picking_flow, " +
                                "inv_cont_pulmao, inv_cont_avaria, inv_cont_volume_flow, inv_cont_vencimento, inv_cont_lote from wms_inventario i " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = i.usu_codigo_inicial " +
                                "where inv_data_final is null and inv_status = 'ABERTO'";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância a camada de objetos
                Inventarios inventario = new Inventarios();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["inv_codigo"] != DBNull.Value)
                    {
                        inventario.codInventario = Convert.ToInt32(linha["inv_codigo"]);
                    }

                    if (linha["inv_descricao"] != DBNull.Value)
                    {
                        inventario.descInventario = Convert.ToString(linha["inv_descricao"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        inventario.usuarioInicial = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["inv_tipo"] != DBNull.Value)
                    {
                        inventario.tipoInventario = Convert.ToString(linha["inv_tipo"]);
                    }

                    if (linha["inv_data_inicial"] != DBNull.Value)
                    {
                        inventario.dataInicial = Convert.ToDateTime(linha["inv_data_inicial"]);
                    }

                    if (linha["int_rotativo"] != DBNull.Value)
                    {
                        inventario.descRotativo = Convert.ToString(linha["int_rotativo"]);
                    }

                    if (linha["inv_auditoria"] != DBNull.Value)
                    {
                        inventario.tipoAuditoria = Convert.ToString(linha["inv_auditoria"]);
                    }

                    if (linha["inv_importar_erp"] != DBNull.Value)
                    {
                        inventario.importarERP = Convert.ToBoolean(linha["inv_importar_erp"]);
                    }

                    if (linha["inv_cont_vencimento"] != DBNull.Value)
                    {
                        inventario.contVencimento = Convert.ToBoolean(linha["inv_cont_vencimento"]);
                    }

                    if (linha["inv_cont_lote"] != DBNull.Value)
                    {
                        inventario.contLote = Convert.ToBoolean(linha["inv_cont_lote"]);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return inventario;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o invetário. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o endereco 
        public Apartamento PesqEndereco(string codApartamento)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codApartamento", codApartamento);

                //String de consulta
                string select = "select apa_endereco, apa_disponivel from wms_apartamento where apa_codigo = @codApartamento";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância a camada de objetos
                Apartamento apartamento = new Apartamento();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        apartamento.descApartamento = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["apa_disponivel"] != DBNull.Value)
                    {
                        apartamento.disposicaoApartamento = Convert.ToString(linha["apa_disponivel"]);
                    }

                }
                //Retorna os dados
                return apartamento;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereço digitado! \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o produto 
        public ItemInventario PesqProduto(int codInventario,string empresa ,string codApartamento, string codProduto)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@codProduto", codProduto);

                //String de consulta
                string select = "select prod_id, prod_codigo, prod_descricao, prod_peso, prod_fator_pulmao, u.uni_unidade as uni_pulmao, u1.uni_unidade as uni_picking, " +
                                "(select iinv_codigo from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cod_item_inventario, " +
                                //Código do produto no armazenamento
                                "(select arm_codigo from wms_armazenagem where apa_codigo = @codApartamento and prod_id = p.prod_id) as cod_operacao_Pulmao, " +
                                //Venvimento do produto já armazenado 
                                "(select arm_vencimento from wms_armazenagem where apa_codigo = @codApartamento and prod_id = p.prod_id) as vencimento_pulmao, " +
                                 //Código do produto na separação
                                 "(select sep_codigo from wms_separacao where apa_codigo = @codApartamento and prod_id = p.prod_id) as cod_operacao_Picking, " +
                                "(select iinv_cont_primeira from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario ) as cont_primeira, " +
                                "(select iinv_cont_segunda from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario ) as cont_segunda, " +
                                "(select iinv_cont_terceira from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_terceira, " +
                                "(select iinv_cont_quarta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_quarta, " +
                                "(select iinv_cont_quinta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_quinta, " +
                                "(select iinv_cont_sesta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_sesta, " +
                                "(select iinv_cont_setima from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_setima, " +
                                "(select iinv_cont_oitava from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_oitava, " +
                                "(select iinv_cont_nona from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_nona, " +
                                "(select iinv_cont_decima from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_decima, " +
                                "(select iinv_cont_final from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as cont_final, " +
                                "(select iinv_venc_primeira from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario ) as venc_primeira, " +
                                "(select iinv_venc_segunda from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario ) as venc_segunda, " +
                                "(select iinv_venc_terceira from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_terceira, " +
                                "(select iinv_venc_quarta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_quarta, " +
                                "(select iinv_venc_quinta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_quinta, " +
                                "(select iinv_venc_sesta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_sesta, " +
                                "(select iinv_venc_setima from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_setima, " +
                                "(select iinv_venc_oitava from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_oitava, " +
                                "(select iinv_venc_nona from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_nona, " +
                                "(select iinv_venc_decima from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_decima, " +
                                "(select iinv_venc_final from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as venc_final, " +
                                "(select iinv_lote_primeira from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario ) as lote_primeira, " +
                                "(select iinv_lote_segunda from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario ) as lote_segunda, " +
                                "(select iinv_lote_terceira from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_terceira, " +
                                "(select iinv_lote_quarta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_quarta, " +
                                "(select iinv_lote_quinta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_quinta, " +
                                "(select iinv_lote_sesta from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_sesta, " +
                                "(select iinv_lote_setima from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_setima, " +
                                "(select iinv_lote_oitava from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_oitava, " +
                                "(select iinv_lote_nona from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_nona, " +
                                "(select iinv_lote_decima from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_decima, " +
                                "(select iinv_lote_final from wms_item_inventario where apa_codigo = @codApartamento and prod_id = p.prod_id and inv_codigo = @codInventario) as lote_final, " +
                                "(select apa_tipo from wms_apartamento where apa_codigo = @codApartamento) as apa_tipo, " +
                                "(select prod_id from wms_separacao where apa_codigo = @codApartamento) as prod_picking " +
                                "from wms_produto p " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_picking " +
                                "where prod_codigo = @codProduto and c.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância a camada de objetos
                ItemInventario itensInventario = new ItemInventario();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["apa_tipo"] != DBNull.Value)
                    {
                        itensInventario.tipoApartemento = Convert.ToString(linha["apa_tipo"]);
                    }

                    if (linha["prod_picking"] != DBNull.Value)
                    {
                        itensInventario.idProdutoPicking = Convert.ToInt32(linha["prod_picking"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itensInventario.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensInventario.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensInventario.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["vencimento_pulmao"] != DBNull.Value)
                    {
                        itensInventario.vencimentoPulmao = Convert.ToDateTime(linha["vencimento_pulmao"]);
                    }

                    if (linha["prod_peso"] != DBNull.Value)
                    {
                        itensInventario.pesoProduto = Convert.ToDouble(linha["prod_peso"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itensInventario.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_Picking"] != DBNull.Value)
                    {
                        itensInventario.unidadePicking = Convert.ToString(linha["uni_Picking"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itensInventario.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["cod_item_inventario"] != DBNull.Value)
                    {
                        itensInventario.codItensInventario = Convert.ToInt32(linha["cod_item_inventario"]);
                    }

                    if (linha["cod_operacao_Picking"] != DBNull.Value)
                    {
                        itensInventario.codOperacao = Convert.ToInt32(linha["cod_operacao_Picking"]);
                    }
                    else if (linha["cod_operacao_Pulmao"] != DBNull.Value)
                    {
                        itensInventario.codOperacao = Convert.ToInt32(linha["cod_operacao_Pulmao"]);
                    }

                    if (linha["cont_primeira"] != DBNull.Value)
                    {
                        itensInventario.contPrimeira = Convert.ToInt32(linha["cont_primeira"]);
                    }

                    if (linha["cont_segunda"] != DBNull.Value)
                    {
                        itensInventario.contSegunda = Convert.ToInt32(linha["cont_segunda"]);
                    }

                    if (linha["cont_terceira"] != DBNull.Value)
                    {
                        itensInventario.contTerceira = Convert.ToInt32(linha["cont_terceira"]);
                    }

                    if (linha["cont_quarta"] != DBNull.Value)
                    {
                        itensInventario.contQuarta = Convert.ToInt32(linha["cont_quarta"]);
                    }

                    if (linha["cont_quinta"] != DBNull.Value)
                    {
                        itensInventario.contQuinta = Convert.ToInt32(linha["cont_quinta"]);
                    }

                    if (linha["cont_sesta"] != DBNull.Value)
                    {
                        itensInventario.contSesta = Convert.ToInt32(linha["cont_sesta"]);
                    }

                    if (linha["cont_setima"] != DBNull.Value)
                    {
                        itensInventario.contSetima = Convert.ToInt32(linha["cont_setima"]);
                    }

                    if (linha["cont_oitava"] != DBNull.Value)
                    {
                        itensInventario.contOitava = Convert.ToInt32(linha["cont_oitava"]);
                    }

                    if (linha["cont_nona"] != DBNull.Value)
                    {
                        itensInventario.contNona = Convert.ToInt32(linha["cont_nona"]);
                    }

                    if (linha["cont_decima"] != DBNull.Value)
                    {
                        itensInventario.contDecima = Convert.ToInt32(linha["cont_decima"]);
                    }

                    if (linha["cont_final"] != DBNull.Value)
                    {
                        itensInventario.contagemFinal = Convert.ToInt32(linha["cont_final"]);
                    }

                    if (linha["venc_primeira"] != DBNull.Value)
                    {
                        itensInventario.vencPrimeira = Convert.ToDateTime(linha["venc_primeira"]);
                    }

                    if (linha["venc_segunda"] != DBNull.Value)
                    {
                        itensInventario.vencSegunda = Convert.ToDateTime(linha["venc_segunda"]);
                    }

                    if (linha["venc_terceira"] != DBNull.Value)
                    {
                        itensInventario.vencTerceira = Convert.ToDateTime(linha["venc_terceira"]);
                    }

                    if (linha["venc_quarta"] != DBNull.Value)
                    {
                        itensInventario.vencQuarta = Convert.ToDateTime(linha["venc_quarta"]);
                    }

                    if (linha["venc_quinta"] != DBNull.Value)
                    {
                        itensInventario.vencQuinta = Convert.ToDateTime(linha["venc_quinta"]);
                    }

                    if (linha["venc_sesta"] != DBNull.Value)
                    {
                        itensInventario.vencSesta = Convert.ToDateTime(linha["venc_sesta"]);
                    }

                    if (linha["venc_setima"] != DBNull.Value)
                    {
                        itensInventario.vencSetima = Convert.ToDateTime(linha["venc_setima"]);
                    }

                    if (linha["venc_oitava"] != DBNull.Value)
                    {
                        itensInventario.vencOitava = Convert.ToDateTime(linha["venc_oitava"]);
                    }

                    if (linha["venc_nona"] != DBNull.Value)
                    {
                        itensInventario.vencNona = Convert.ToDateTime(linha["venc_nona"]);
                    }

                    if (linha["venc_decima"] != DBNull.Value)
                    {
                        itensInventario.vencDecima = Convert.ToDateTime(linha["venc_decima"]);
                    }

                    if (linha["venc_final"] != DBNull.Value)
                    {
                        itensInventario.vencFinal = Convert.ToDateTime(linha["venc_final"]);
                    }

                    if (linha["lote_primeira"] != DBNull.Value)
                    {
                        itensInventario.lotePrimeiro = Convert.ToString(linha["lote_primeira"]);
                    }

                    if (linha["lote_segunda"] != DBNull.Value)
                    {
                        itensInventario.loteSegundo = Convert.ToString(linha["lote_segunda"]);
                    }

                    if (linha["lote_terceira"] != DBNull.Value)
                    {
                        itensInventario.loteTerceiro = Convert.ToString(linha["lote_terceira"]);
                    }

                    if (linha["lote_quarta"] != DBNull.Value)
                    {
                        itensInventario.loteQuarto = Convert.ToString(linha["lote_quarta"]);
                    }

                    if (linha["lote_quinta"] != DBNull.Value)
                    {
                        itensInventario.loteQuinto = Convert.ToString(linha["lote_quinta"]);
                    }

                    if (linha["lote_sesta"] != DBNull.Value)
                    {
                        itensInventario.loteSesto = Convert.ToString(linha["lote_sesta"]);
                    }

                    if (linha["lote_setima"] != DBNull.Value)
                    {
                        itensInventario.loteSetimo = Convert.ToString(linha["lote_setima"]);
                    }

                    if (linha["lote_oitava"] != DBNull.Value)
                    {
                        itensInventario.loteOitavo = Convert.ToString(linha["lote_oitava"]);
                    }

                    if (linha["lote_nona"] != DBNull.Value)
                    {
                        itensInventario.loteNono = Convert.ToString(linha["lote_nona"]);
                    }

                    if (linha["lote_decima"] != DBNull.Value)
                    {
                        itensInventario.loteDecimo = Convert.ToString(linha["lote_decima"]);
                    }

                    if (linha["lote_final"] != DBNull.Value)
                    {
                        itensInventario.loteFinal = Convert.ToString(linha["lote_final"]);
                    }

                }
                //Retorna os dados
                return itensInventario;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto! \nDetalhes:" + ex.Message);
            }
        }

        /*Insere a contagem
        public void InserirCotagem(int codInventario, int codOperacao, int codApartamento, int idProduto, int contagem, string vencimento, double peso, string lote, int codAuditor, int codUsuario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@codOperacao", codOperacao);
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@contagem", contagem);
                conexao.AdicionarParamentros("@vencimento", vencimento);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@codAuditor", codAuditor);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);


                //insert da primeira contagem
                string insert = "insert into wms_item_inventario (inv_codigo, iinv_codigo, iinv_operacao, apa_codigo, prod_id, iinv_cont_primeira, " +
                         "iinv_venc_primeira, iinv_peso_primeira, iinv_lote_primeira, usu_codigo_primeira, usu_digitador_primeira) " +
                        "select @codInventario, gen_id(gen_wms_item_inventario, 1), @codOperacao,  @codApartamento, @idProduto, @contagem, " +
                        "@vencimento, @peso, @lote, @codAuditor, @codUsuario from RDB$DATABASE ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir a primeira contagem. \nDetalhes:" + ex.Message);
            }
        }

        //Insere a 2 ou a 3 contagem
        public void AtualizarCotagem(int codInventario, int codItemInventario, int contagem, string vencimento, double peso, string lote, int codAuditor, int codUsuario, bool segunda, bool terceira)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@codItemInventario", codItemInventario);
                conexao.AdicionarParamentros("@contagem", contagem);
                conexao.AdicionarParamentros("@vencimento", vencimento);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@codAuditor", codAuditor);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);

                //string de atualização
                string update = null;

                if (segunda == true)
                {
                    //atualiza a segunda contagem
                    update = "update wms_item_inventario set iinv_cont_segunda = @contagem, " +
                             "iinv_venc_segunda = @vencimento, iinv_peso_segunda = @peso, iinv_lote_segunda = @lote, " +
                             "usu_codigo_segunda = @codAuditor, usu_digitador_segunda = @codUsuario " +
                             "where inv_codigo = @codInventario and iinv_codigo = @codItemInventario";
                }

                if (terceira == true)
                {
                    //atualiza a segunda contagem
                    update = "update wms_item_inventario set iinv_cont_terceira = @contagem, " +
                             "iinv_venc_terceira = @vencimento, iinv_peso_terceira = @peso, iinv_lote_terceira = @lote, " +
                             "usu_codigo_terceira = @codAuditor, usu_digitador_terceira = @codUsuario " +
                             "where inv_codigo = @codInventario and iinv_codigo = @codItemInventario";
                }

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir a segunda ou terceira contagem. \nDetalhes:" + ex.Message);
            }
        }*/

        //Registra a contagem do picking
        public void EnviarContagem(ItemInventario n, int codInventario, string contagem, int codUsuario, bool contagemFinal, bool vencimentoFinal, bool loteFinal)
        {
            try
            {
                // Limpa todos os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@codOperacao", n.codOperacao);
                conexao.AdicionarParamentros("@codApartamento", n.codApartamento);
                conexao.AdicionarParamentros("@idProduto", n.idProduto);
                conexao.AdicionarParamentros("@qtdConferidaProduto", n.contagemFinal);
                conexao.AdicionarParamentros("@validadeProduto", n.vencFinal);
                conexao.AdicionarParamentros("@pesoProduto", n.pesoFinal);
                conexao.AdicionarParamentros("@loteProduto", n.loteFinal);
                conexao.AdicionarParamentros("@dataConferencia", n.dataContagem);
                conexao.AdicionarParamentros("@codAuditor", n.usuContFinal);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);

                //Registra a primeira contagem
                if (contagem.Equals("1ª"))
                {
                    //String de insert
                    string insert = "insert into wms_item_inventario (inv_codigo, iinv_codigo, iinv_operacao, apa_codigo, prod_id, iinv_cont_primeira, iinv_venc_primeira, " +
                                    "iinv_peso_primeira, iinv_lote_primeira, usu_codigo_primeira, usu_digitador_primeira, iinv_tempo_primeira) " +

                                    "select @codInventario, gen_id(gen_wms_item_inventario, 1), @codOperacao, @codApartamento, @idProduto, @qtdConferidaProduto, @validadeProduto, " +
                                    "@pesoProduto, @loteProduto, @codAuditor, @codUsuario, @dataConferencia from rdb$database " +
                                    "WHERE NOT EXISTS (select 1 from wms_item_inventario where apa_codigo = @codApartamento and inv_codigo = @codInventario)";

                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, insert);
                }

                //Registra a primeira contagem
                if (contagem.Equals("2ª"))
                {
                    //String de update
                    string update = "update wms_item_inventario set iinv_cont_segunda = @qtdConferidaProduto, iinv_venc_segunda = @validadeProduto, " +
                                    "iinv_peso_segunda = @pesoProduto, iinv_lote_segunda = @loteProduto, usu_digitador_segunda = @codUsuario, usu_codigo_segunda = @codAuditor, iinv_tempo_segunda = @dataConferencia " +
                                    "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";

                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, update);
                }

                //Registra a primeira contagem
                if (contagem.Equals("3ª+"))
                {
                    string update = null;

                    if (n.contTerceira == null)
                    {
                        //String de update
                        update = "update wms_item_inventario set iinv_cont_terceira = @qtdConferidaProduto, iinv_venc_terceira = @validadeProduto, " +
                                        "iinv_peso_terceira = @pesoProduto, iinv_lote_terceira = @loteProduto, usu_digitador_terceira = @codUsuario, usu_codigo_terceira = @codAuditor, iinv_tempo_terceira = @dataConferencia " +
                                        "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    }
                    else if (n.contQuarta == null)
                    {
                        //String de update
                        update = "update wms_item_inventario set iinv_cont_quarta = @qtdConferidaProduto, iinv_venc_quarta = @validadeProduto, " +
                                        "iinv_peso_quarta = @pesoProduto, iinv_lote_quarta = @loteProduto, usu_digitador_quarta = @codUsuario, usu_codigo_quarta = @codAuditor, iinv_tempo_quarta = @dataConferencia " +
                                        "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    }
                    else if (n.contQuinta == null)
                    {
                        //String de update
                        update = "update wms_item_inventario set iinv_cont_quinta = @qtdConferidaProduto, iinv_venc_quinta = @validadeProduto, " +
                                        "iinv_peso_quinta = @pesoProduto, iinv_lote_quinta = @loteProduto, usu_digitador_quinta = @codUsuario, usu_codigo_quinta = @codAuditor, iinv_tempo_quinta = @dataConferencia " +
                                        "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    }
                    else if (n.contSesta == null)
                    {
                        //String de update
                        update = "update wms_item_inventario set iinv_cont_sesta = @qtdConferidaProduto, iinv_venc_sesta = @validadeProduto, " +
                                        "iinv_peso_sesta = @pesoProduto, iinv_lote_sesta = @loteProduto, usu_digitador_sesta = @codUsuario, usu_codigo_sesta = @codAuditor, iinv_tempo_sesta = @dataConferencia " +
                                        "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    }
                    else if (n.contSetima == null)
                    {
                        //String de update
                        update = "update wms_item_inventario set iinv_cont_setima = @qtdConferidaProduto, iinv_venc_setima = @validadeProduto, " +
                                        "iinv_peso_setima = @pesoProduto, iinv_lote_setima = @loteProduto, usu_digitador_setima = @codUsuario, usu_codigo_setima = @codAuditor, iinv_tempo_setima = @dataConferencia " +
                                        "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    }
                    else if (n.contOitava == null)
                    {
                        //String de update
                        update = "update wms_item_inventario set iinv_cont_oitava = @qtdConferidaProduto, iinv_venc_oitava = @validadeProduto, " +
                                        "iinv_peso_oitava = @pesoProduto, iinv_lote_oitava = @loteProduto, usu_digitador_oitava = @codUsuario, usu_codigo_oitava = @codAuditor, iinv_tempo_oitava = @dataConferencia " +
                                        "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    }
                    else if (n.contNona == null)
                    {
                        //String de update
                        update = "update wms_item_inventario set iinv_cont_nona = @qtdConferidaProduto, iinv_venc_nona = @validadeProduto, " +
                                        "iinv_peso_nona = @pesoProduto, iinv_lote_nona = @loteProduto, usu_digitador_nona = @codUsuario, usu_codigo_nona = @codAuditor, iinv_tempo_nona = @dataConferencia " +
                                        "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    }
                    else if (n.contDecima == null)
                    {
                        //String de update
                        update = "update wms_item_inventario set iinv_cont_decima = @qtdConferidaProduto, iinv_venc_decima = @validadeProduto, " +
                                        "iinv_peso_decima = @pesoProduto, iinv_lote_decima = @loteProduto, usu_digitador_decima = @codUsuario, usu_codigo_decima = @codAuditor, iinv_tempo_decima = @dataConferencia " +
                                        "where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    }

                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, update);                    
                }

                if (contagemFinal == true)
                {
                    //String de update
                    string cont = "update wms_item_inventario set iinv_cont_final = @qtdConferidaProduto, iinv_peso_final = @pesoProduto where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, cont);
                }

                if (vencimentoFinal == true)
                {
                    //String de update
                    string venc = "update wms_item_inventario set iinv_venc_final = @validadeProduto where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, venc);
                }

                if (loteFinal == true)
                {
                    //String de update
                    string lote = "update wms_item_inventario set iinv_lote_final = @loteProduto where inv_codigo = @codInventario and apa_codigo = @codApartamento and prod_id = @idProduto ";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, lote);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro registrar a contagem de picking. \nDetalhes:" + ex.Message);
            }
        }



    }
}
