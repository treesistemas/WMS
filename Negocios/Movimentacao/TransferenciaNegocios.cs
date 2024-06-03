using System;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class TransferenciaNegocios
    {
        //Instância o objeto conexão
        Conexao conexao = new Conexao();

        //Pesquisa o Produto
        public Produto PesqProduto(string codProduto, string empresa)
        {
            try
            {
                //Instância o objeto transferência
                Produto produto = new Produto();
                //Limpa parametros da conexão
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                string select = "select prod_id, prod_codigo, prod_descricao, cat_descricao, " +
                                "prod_tipo_palete, u.uni_unidade, u1.uni_unidade as uni_picking, prod_tipo_armazenamento, p.prod_peso, p.prod_niv_maximo, " +
                                "p.prod_altura_p* p.prod_lastro_p as palete_pequeno, p.prod_altura_m* p.prod_lastro_m as palete_medio, " +
                                "p.prod_altura_g* p.prod_lastro_g as palete_grande, p.prod_altura_b* p.prod_lastro_b as palete_blocado,  " +
                                "current_date + cast(p.prod_vida_util - (prod_vida_util * cast(p.prod_tolerancia as double precision) / 100) as integer) as tolerancia_vencimento, " +
                                "p.prod_fator_pulmao, p.prod_fator_compra, p.prod_controla_validade, prod_separacao_flowrack, prod_palete_blocado, p.prod_status " +
                                "from wms_produto p " +
                                "left join wms_categoria c " +
                                "on c.cat_codigo = p.cat_codigo " +
                                "left join wms_unidade u " +
                                "on p.uni_codigo_pulmao = u.uni_codigo "+
                                "left join wms_unidade u1 " +
                                "on p.uni_codigo_picking = u1.uni_codigo " +
                                "where prod_codigo = @codProduto and p.conf_codigo = " +
                                "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona o id do produto
                        produto.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        //Adiciona o código do produto
                        produto.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        //Adiciona a descrição do produto
                        produto.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        //Adiciona a categoria do produto
                        produto.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["prod_tipo_palete"] != DBNull.Value)
                    {
                        //Adiciona o tipo de pallet usado pelo produto
                        produto.tipoPalete = Convert.ToString(linha["prod_tipo_palete"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        //Adiciona a unidade do pulmão
                        produto.undPulmao = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        //Adiciona a unidade do pulmão
                        produto.undPicking = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["prod_tipo_armazenamento"] != DBNull.Value)
                    {
                        //Adiciona o tipo de armazenamento do produto
                        produto.tipoArmazenagem = Convert.ToString(linha["prod_tipo_armazenamento"]);
                    }

                    if (linha["prod_peso"] != DBNull.Value)
                    {
                        //Adiciona o peso do produto
                        produto.pesoProduto = Convert.ToDouble(linha["prod_peso"]);
                    }

                    if (linha["prod_niv_maximo"] != DBNull.Value)
                    {
                        //Adiciona o nível máximo do produto
                        produto.nivelMaximo = Convert.ToInt32(linha["prod_niv_maximo"]);
                    }

                    if (linha["palete_pequeno"] != DBNull.Value)
                    {
                        //Adiciona o total do pallet PP
                        produto.totalPequeno = Convert.ToInt32(linha["palete_pequeno"]);
                    }


                    if (linha["palete_medio"] != DBNull.Value)
                    {
                        //Adiciona o total do pallet PM
                        produto.totalMedio = Convert.ToInt32(linha["palete_medio"]);
                    }

                    if (linha["palete_grande"] != DBNull.Value)
                    {
                        //Adiciona o total do pallet PG
                        produto.totalGrande = Convert.ToInt32(linha["palete_grande"]);
                    }

                    if (linha["palete_blocado"] != DBNull.Value)
                    {
                        //Adiciona o total do pallet PG
                        produto.totalBlocado = Convert.ToInt32(linha["palete_blocado"]);
                    }

                    if (linha["tolerancia_vencimento"] != DBNull.Value)
                    {
                        //Adiciona a tolerância do produto
                        produto.dataTolerancia = Convert.ToDateTime(linha["tolerancia_vencimento"]);

                    }
                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        //Adiciona o fator de armazenamento
                        produto.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);

                    }

                    if (linha["prod_controla_validade"] != DBNull.Value)
                    {
                        //Adiciona o status de controle de validade
                        produto.controlaValidade = Convert.ToBoolean(linha["prod_controla_validade"]);
                    }

                    if (linha["prod_separacao_flowrack"] != DBNull.Value)
                    {
                        //Adiciona o status de controle de validade
                        produto.separacaoFlowrack = Convert.ToBoolean(linha["prod_separacao_flowrack"]);
                    }

                    if (linha["prod_palete_blocado"] != DBNull.Value)
                    {
                        //Adiciona o status de controle do palete blocado
                        produto.paletePadrao = Convert.ToBoolean(linha["prod_palete_blocado"]);
                    }

                    if (linha["prod_status"] != DBNull.Value)
                    {
                        //Adiciona o status do produto
                        produto.status = Convert.ToBoolean(linha["prod_status"]);
                    }
                }

                return produto;
            }

            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto! \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o picking do produto
        public EnderecoPickingCollection PesqPicking(int idProduto, string empresa)
        {
            try
            {
                //Instância a coleção de objêto
                EnderecoPickingCollection enderecoPickingCollection = new EnderecoPickingCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adicionar parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select s.prod_id, ap.apa_codigo, apa_endereco, s.sep_estoque, u.uni_unidade, s.sep_validade, s.sep_peso, s.sep_lote, " +
                                "s.sep_capacidade, s.sep_abastecimento, s.sep_tipo, prod_fator_pulmao, ap.apa_tamanho_palete " +
                                "from wms_separacao s " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = s.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u " +
                                "on p.uni_codigo_picking = u.uni_codigo " +
                                "where s.prod_id = @idProduto " +
                                "and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "order by sep_tipo asc";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    EnderecoPicking enderecoPicking = new EnderecoPicking();

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona o id do produto
                        enderecoPicking.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona o código do endereço
                        enderecoPicking.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        //Adiciona endereço
                        enderecoPicking.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        //Adiciona a quantidade
                        enderecoPicking.estoque = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        //Adiciona a unidade
                        enderecoPicking.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        //Adiciona o vencimento
                        enderecoPicking.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_peso"] != DBNull.Value)
                    {
                        //Adiciona o peso
                        enderecoPicking.peso = Convert.ToDouble(linha["sep_peso"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        //Adiciona o lote
                        enderecoPicking.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    if (linha["sep_capacidade"] != DBNull.Value)
                    {
                        //Adiciona a capacidade
                        enderecoPicking.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                    }

                    if (linha["sep_abastecimento"] != DBNull.Value)
                    {
                        //Adiciona o abastecimento
                        enderecoPicking.abastecimento = Convert.ToInt32(linha["sep_abastecimento"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        //Adiciona o tipo do endereco
                        enderecoPicking.tipoEndereco = Convert.ToString(linha["sep_tipo"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        //Adiciona o abastecimento
                        enderecoPicking.quantidadeCaixa = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        //Adiciona o tipo do endereco
                        enderecoPicking.tamanhoEndereco = Convert.ToString(linha["apa_tamanho_palete"]);
                    }



                    enderecoPickingCollection.Add(enderecoPicking);
                }

                return enderecoPickingCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereço de picking! \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o estoque do produto no pulmão
        public EnderecoPulmaoCollection PesqPulmao(int idProduto, string empresa)
        {
            try
            {
                EnderecoPulmaoCollection enderecoPulmaoCollection = new EnderecoPulmaoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select arm_data_armazenagem, a.apa_codigo, a.prod_id, p.prod_fator_pulmao, ap.apa_endereco, apa_tamanho_palete, a.arm_quantidade/p.prod_fator_pulmao as estoque, " +
                                "u.uni_unidade, a.arm_vencimento, a.arm_peso, a.arm_lote, not_nota_cega, arm_impresso,  a.usu_codigo, "+
                                "coalesce(r.res_quantidade, 0) + coalesce(iaba_estoque_pulmao, 0) as estoque_reservado " +
                                "from wms_armazenagem a " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = a.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.conf_codigo = a.conf_codigo  and p.prod_id = a.prod_id " +
                                "left join wms_unidade u " +
                                "on p.uni_codigo_pulmao = u.uni_codigo " +
                                "left join wms_reserva r " +
                                "on r.prod_id = p.prod_id and r.apa_codigo = a.apa_codigo " +
                                "left join wms_item_abastecimento ab " +
                                "on ab.prod_id = p.prod_id  and ab.iaba_status = 'PENDENTE' and ab.iaba_tipo_analise = 'ANÁLISE DO PULMÃO' " +
                                "where p.prod_id = @idProduto " +
                                "and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "order by arm_vencimento, arm_quantidade, ap.apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto transferência
                    EnderecoPulmao enderecoPulmao = new EnderecoPulmao();

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona o id do produto
                        enderecoPulmao.codApartamento1 = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        //Adiciona o código do produto
                        enderecoPulmao.descEndereco1 = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona o id do produto
                        enderecoPulmao.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }                    

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        //Adiciona a descrição do produto
                        enderecoPulmao.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["estoque"] != DBNull.Value)
                    {
                        //Adiciona a descrição do produto
                        enderecoPulmao.qtdCaixaOrigem = Convert.ToInt32(linha["estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        //Adiciona a unidade do estoque
                        enderecoPulmao.undCaixaDestino = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        //Adiciona a categoria do produto
                        enderecoPulmao.vencimentoProduto1 = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_peso"] != DBNull.Value)
                    {
                        //Adiciona o tipo de pallet usado pelo produto
                        enderecoPulmao.pesoProduto1 = Convert.ToDouble(linha["arm_peso"]);
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        //Adiciona o tipo de armazenamento do produto
                        enderecoPulmao.loteProduto1 = Convert.ToString(linha["arm_lote"]);
                    }

                    if (linha["estoque_reservado"] != DBNull.Value)
                    {
                        //Adiciona o peso do produto
                        enderecoPulmao.estoqueReservado = Convert.ToInt32(linha["estoque_reservado"]);
                    }

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        //Adiciona o tipo do endereco
                        enderecoPulmao.tamanhoApartamento1 = Convert.ToString(linha["apa_tamanho_palete"]);
                    }

                    if (linha["not_nota_cega"] != DBNull.Value)
                    {
                        //Adiciona o código do usuário de armazenagem
                        enderecoPulmao.notaCega = Convert.ToInt32(linha["not_nota_cega"]);
                    }

                    if (linha["arm_data_armazenagem"] != DBNull.Value)
                    {
                        //Adiciona o código do usuário de armazenagem
                        enderecoPulmao.dataEntrada = Convert.ToDateTime(linha["arm_data_armazenagem"]);
                    }

                    if (linha["arm_impresso"] != DBNull.Value)
                    {
                        //Adiciona o status da impressão
                        enderecoPulmao.impresso = Convert.ToString(linha["arm_impresso"]);
                    }

                    if (linha["usu_codigo"] != DBNull.Value)
                    {
                        //Adiciona o código do usuário de armazenagem
                        enderecoPulmao.codUsuario = Convert.ToInt32(linha["usu_codigo"]);
                    }

                    enderecoPulmaoCollection.Add(enderecoPulmao);
                }

                return enderecoPulmaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o pulmão do produto! \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa novo endereço
        public Estrutura PesqNovoEndereco(string endereco)
        {
            try
            {
                //Instância o objeto estrutura
                Estrutura estrutura = new Estrutura();
                //Limpa a parametros da conexão
                conexao.LimparParametros();
                //Adiciona os parâmetros 
                conexao.AdicionarParamentros("@endereco", endereco);
                //String de consulta
                string select = "select apa_codigo, apa_endereco, a.apa_disponivel, a.apa_tipo, apa_status, rg.reg_numero, " +
                                "reg_tipo, apa_tamanho_palete, " +
                                "n.niv_numero " +
                                "from wms_apartamento a " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao rg " +
                                "on rg.reg_codigo = r.reg_codigo " +
                                "where a.apa_endereco = @endereco ";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona o id do endereco
                        estrutura.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }
                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        //Adiciona o endereço
                        estrutura.descApartamento = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["apa_disponivel"] != DBNull.Value)
                    {
                        //Adiciona a disponibilidade do endeço
                        estrutura.disposicaoApartamento = Convert.ToString(linha["apa_disponivel"]);
                    }

                    if (linha["apa_tipo"] != DBNull.Value)
                    {
                        //Adiciona o tipo de apartamento
                        estrutura.tipoApartamento = Convert.ToString(linha["apa_tipo"]);
                    }

                    if (linha["apa_status"] != DBNull.Value)
                    {
                        //Adiciona o status do endereço
                        estrutura.statusApartamento = Convert.ToString(linha["apa_status"]);
                    }

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        //Adiciona o tipo de pallet
                        estrutura.tamanhoApartamento = Convert.ToString(linha["apa_tamanho_palete"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        //Adiciona a região do endereco
                        estrutura.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        //Adiciona o tipo de região
                        estrutura.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    if (linha["niv_numero"] != DBNull.Value)
                    {
                        //Adiciona o nivel do endereço
                        estrutura.numeroNivel = Convert.ToInt32(linha["niv_numero"]);
                    }

                }
                return estrutura;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereço! \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa os produtos no novo endereço
        public EnderecoPulmaoCollection PesqProdutoNovoEndereco(int codApartamento, string empresa)
        {
            try
            {
                //Instância uma coleção de objêto 
                EnderecoPulmaoCollection enderecoPulmaoCollection = new EnderecoPulmaoCollection();
                //Limpa a parametros da conexão
                conexao.LimparParametros();
                //Adiciona os parâmetros 
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select apa_codigo, prod_id, a.arm_quantidade, a.arm_vencimento, a.arm_peso, a.arm_impresso, " +
                    "a.not_nota_cega, a.usu_codigo, " +
                    //Pesquisa nas configurações quantidade de produto no palete permitido
                    "(select iconf_valor from wms_itens_configuracao where iconf_status = 'True' and iconf_descricao = 'QUANTIDADE MÁXIMA DE PRODUTO NO PALETE') as qtd_palete " +
                    "from wms_armazenagem a where a.apa_codigo =  @codApartamento and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    EnderecoPulmao enderecoPulmao = new EnderecoPulmao();

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona o id do endereco
                        enderecoPulmao.codApartamento1 = Convert.ToInt32(linha["apa_codigo"]);
                    }
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona o endereço
                        enderecoPulmao.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["arm_quantidade"] != DBNull.Value)
                    {
                        //Adiciona a disponibilidade do endeço
                        enderecoPulmao.qtdCaixaOrigem = Convert.ToInt32(linha["arm_quantidade"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        //Adiciona o tipo de apartamento
                        enderecoPulmao.vencimentoProduto1 = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_peso"] != DBNull.Value)
                    {
                        //Adiciona o status do endereço
                        enderecoPulmao.pesoProduto1 = Convert.ToDouble(linha["arm_peso"]);
                    }

                    if (linha["arm_impresso"] != DBNull.Value)
                    {
                        //Adiciona o tipo de pallet
                        enderecoPulmao.impresso = Convert.ToString(linha["arm_impresso"]);
                    }

                    if (linha["not_nota_cega"] != DBNull.Value)
                    {
                        //Adiciona a região do endereco
                        enderecoPulmao.notaCega = Convert.ToInt32(linha["not_nota_cega"]);
                    }

                    if (linha["qtd_palete"] != DBNull.Value)
                    {
                        //Adiciona a região do endereco
                        enderecoPulmao.qtdProdutoPalete = Convert.ToInt32(linha["qtd_palete"]);
                    }

                    if (linha["usu_codigo"] != DBNull.Value)
                    {
                        //Adiciona a região do endereco
                        enderecoPulmao.codUsuario = Convert.ToInt32(linha["usu_codigo"]);
                    }


                    enderecoPulmaoCollection.Add(enderecoPulmao);

                }
                return enderecoPulmaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os produtos do novo endereço! \nDetalhes: " + ex.Message);
            }
        }


        //Tranferencia de picking para picking
        public void TransferirPickingPicking(int idProduto, int codPickingTranfestir, int codPickingReceber, int estoque, double peso, string vencimento, string lote, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codPickingTranfestir", codPickingTranfestir);
                conexao.AdicionarParamentros("@codPickingReceber", codPickingReceber);
                conexao.AdicionarParamentros("@estoque", estoque);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@validade", vencimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de atualização
                string update = "update wms_separacao s set s.sep_estoque = coalesce(sep_estoque, 0) - @estoque, s.sep_peso = sep_peso - @peso " +
                                "where apa_codigo = @codPickingTranfestir and prod_id = @idProduto " +
                                "and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                string update1 = "update wms_separacao s set s.sep_estoque = coalesce(sep_estoque, 0) + @estoque, s.sep_peso = sep_peso + @peso, " +
                                "s.sep_validade = @validade, s.sep_lote = @lote " +
                                "where apa_codigo = @codPickingReceber and prod_id = @idProduto " +
                                "and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update1);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao transferir o produto de picking para picking! \nDetalhes: " + ex.Message);
            }
        }

        //Tranferencia de pulmão para pulmão
        public void TransferirPulmaoPulmao(string existe, int idProduto, int codPulmaoTranfestir, int codPumlmaoReceber, 
             int estoque, double peso, DateTime? vencimento, string lote, string impressao, int notaCega, DateTime? dataArmazenagem, int? codUsarioArmazenagem, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codPulmaoTranfestir", codPulmaoTranfestir);
                conexao.AdicionarParamentros("@codPumlmaoReceber", codPumlmaoReceber);
                conexao.AdicionarParamentros("@estoque", estoque);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@validade", vencimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@impressao", impressao);
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@dataArmazenagem", dataArmazenagem);
                conexao.AdicionarParamentros("@codUsarioArmazenagem", codUsarioArmazenagem);
                conexao.AdicionarParamentros("@empresa", empresa);

                //Atualiza o apartamento
                string update = "update wms_armazenagem s set s.arm_quantidade = arm_quantidade - @estoque, s.arm_peso = arm_peso - @peso " +
                                "where apa_codigo = @codPulmaoTranfestir and prod_id = @idProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //Deleta se o estoque ficar zero
                string delete = "delete from wms_armazenagem where apa_codigo = @codPulmaoTranfestir and prod_id = @idProduto and arm_quantidade = 0 ";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete);

                string updateStatus = "update wms_apartamento set apa_status = 'Vago' where apa_codigo = @codPulmaoTranfestir and " +
                                      "(select count(prod_id) from wms_armazenagem where apa_codigo = @codPulmaoTranfestir) = 0 " +
                                      "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updateStatus);

                if (existe.Equals("SIM"))
                {
                    string update1 = "update wms_armazenagem s set s.arm_quantidade = arm_quantidade + @estoque, s.arm_peso = arm_peso + @peso, " +
                                    "s.arm_vencimento = @validade, s.arm_lote = @lote, arm_impresso = @impressao, not_nota_cega = @notaCega, usu_codigo = @codUsarioArmazenagem " +
                                    "where apa_codigo = @codPumlmaoReceber and prod_id = @idProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, update1);
                }
                else if (existe.Equals("NÃO"))
                {
                    string insert = "insert into wms_armazenagem (arm_codigo, arm_data_armazenagem, usu_codigo, not_nota_cega, " +
                                    "apa_codigo, prod_id, arm_quantidade, arm_peso, arm_vencimento, arm_lote, arm_impresso, conf_codigo) "+
                        
                        "select gen_id(gen_wms_armazenagem, 1), @dataArmazenagem, @codUsarioArmazenagem, @notaCega, @codPumlmaoReceber, @idProduto, " +
                        "@estoque, @peso, @validade, @lote, @impressao,  (select conf_codigo from wms_configuracao where conf_sigla = @empresa) from RDB$DATABASE";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, insert);

                    updateStatus = "update wms_apartamento set apa_status = 'Ocupado' where apa_codigo = @codPumlmaoReceber " +
                                    "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, updateStatus);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao transferir o produto de pulmão para pulmão! \nDetalhes: " + ex.Message);
            }
        }


        //Tranferencia de Pulmão para picking
        public void TransferirPulmaoPicking(int idProduto, int codPulmao, int codPicking, int estoque, double peso, string vencimento, string lote, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codPulmao", codPulmao);
                conexao.AdicionarParamentros("@codPicking", codPicking);
                conexao.AdicionarParamentros("@estoque", estoque);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@validade", vencimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@empresa", empresa);


                //Atualiza o pulmão
                string update = "update wms_armazenagem s set s.arm_quantidade = arm_quantidade - @estoque, s.arm_peso = arm_peso - @peso " +
                                "where apa_codigo = @codPulmao and prod_id = @idProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //Deleta se o estoque ficar zero
                string delete = "delete from wms_armazenagem where apa_codigo = @codPulmao and prod_id = @idProduto and arm_quantidade = 0 and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";               
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete);

                //Atualiza o status
                string updateStatus = "update wms_apartamento set apa_status = 'Vago' where apa_codigo = @codPulmao and " +
                                      "(select count(prod_id) from wms_armazenagem where apa_codigo = @codPulmao) = 0 and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updateStatus);

                //Atualiza o picking
                string update1 = "update wms_separacao s set s.sep_estoque = coalesce(sep_estoque, 0) + @estoque, s.sep_peso = sep_peso + @peso, " +
                                "s.sep_validade = @validade, s.sep_lote = @lote " +
                                "where apa_codigo = @codPicking and prod_id = @idProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update1);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao transferir o produto de pulmão para picking! \nDetalhes: " + ex.Message);
            }
        }

        //Tranferencia de Pulmão para picking
        public void TransferirPickingPulmao(string existe, int idProduto, int codPicking, int codPulmao, int estoque, DateTime? vencimento, double peso, string lote, int codUsuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codPulmao", codPulmao);
                conexao.AdicionarParamentros("@codPicking", codPicking);
                conexao.AdicionarParamentros("@estoque", estoque);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@validade", vencimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de atualização
                string update = "update wms_separacao s set s.sep_estoque = sep_estoque - @estoque, s.sep_peso = sep_peso - @peso " +
                                "where apa_codigo = @codPicking and prod_id = @idProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);               

                if (existe.Equals("SIM"))
                {
                    string update1 = "update wms_armazenagem s set s.arm_quantidade = arm_quantidade + @estoque, s.arm_peso = arm_peso + @peso, " +
                                    "s.arm_vencimento = @validade, s.arm_lote = @lote " +
                                    "where apa_codigo = @codPulmao and prod_id = @idProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, update1);
                }
                else if (existe.Equals("NÃO"))
                {
                    string insert = "insert into wms_armazenagem (arm_codigo, arm_data_armazenagem, usu_codigo, " +
                        "apa_codigo, prod_id, arm_quantidade, arm_peso, arm_vencimento, arm_lote, arm_impresso, conf_codigo)" +

                        "select gen_id(gen_wms_armazenagem, 1), current_timestamp, @codUsuario, @codPulmao, @idProduto," +
                        "@estoque, @peso, @validade, @lote, 'Nao', (select conf_codigo from wms_configuracao where conf_sigla = @empresa) from RDB$DATABASE";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, insert);

                    string updateStatus = "update wms_apartamento set apa_status = 'Ocupado' where apa_codigo = @codPulmao and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, updateStatus);
                }



            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao transferir o produto de picking para pulmão! \nDetalhes: " + ex.Message);
            }
        }

        //Registra no rastreamento
        public void InserirRastreamento(int codUsuario, int idProduto, int codApartamentoOrigem, int quantidadeOrigem, string vencimentoOrigem, double pesoOrigem, string loteOrigem,
            int codApartamentoDestino, int quantidadeDestino, string vencimentoDestino, double pesoDestino,   string loteDestino, string empresa)
        {
            try
            { 
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codApartamentoOrigem", codApartamentoOrigem);
                conexao.AdicionarParamentros("@codApartamentoDestino", codApartamentoDestino);
                conexao.AdicionarParamentros("@quantidadeOrigem", quantidadeOrigem);
                conexao.AdicionarParamentros("@quantidadeDestino", quantidadeDestino);
                conexao.AdicionarParamentros("@pesoOrigem", pesoOrigem);
                conexao.AdicionarParamentros("@pesoDestino", pesoDestino);
                conexao.AdicionarParamentros("@vencimentoOrigem", vencimentoOrigem);
                conexao.AdicionarParamentros("@vencimentoDestino", vencimentoDestino);
                conexao.AdicionarParamentros("@loteOrigem", loteOrigem);
                conexao.AdicionarParamentros("@loteDestino", loteDestino);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de insert - insere o endereço  
                string insert = "insert into wms_rastreamento_armazenagem (rast_codigo, rast_operacao, rast_data, usu_codigo," +
                "apa_codigo_origem, apa_codigo_destino, prod_id, arm_quantidade_origem, arm_quantidade_destino, arm_peso_origem, arm_peso_destino," +
                "arm_vencimento_origem, arm_vencimento_destino, arm_lote_origem, arm_lote_destino, conf_codigo)" +
                "values " +
                "(gen_id(gen_wms_rast_armazenagem, 1), 'TRANSFERÊNCIA', current_timestamp, @codUsuario," +
                "@codApartamentoOrigem, @codApartamentoDestino, @idProduto, @quantidadeOrigem, @quantidadeDestino, @pesoOrigem, @pesoDestino," +
                "@vencimentoOrigem, @vencimentoDestino, @loteOrigem, @loteDestino, (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";


                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a operação no rastreamento de operações! \nDetalhes: " + ex.Message);
            }
        }



    }
}
