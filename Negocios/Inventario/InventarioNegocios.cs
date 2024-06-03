using System;
using System.Data;
using System.Reflection;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class InventarioNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa um novo id
        public int PesqCodigo()
        {
            try
            {
                //String de consulta
                string select = "select gen_id(gen_wms_inventario,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Variável
                int codInventario = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    codInventario = Convert.ToInt32(linha["id"]);
                }
                //Retorna o valor encontrado
                return codInventario;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo código. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa informações da região
        public Estrutura PesqRegiaoInformacao(int numeroRegiao)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adicionar parâmetros
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);

                //String de consulta
                string select = "select re.reg_codigo, reg_numero, reg_tipo, " +
                                /*Pesquisa o pulmão*/
                                "(select count(s.apa_codigo) from wms_armazenagem s " +
                                "inner join wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on a.niv_codigo = n.niv_codigo " +
                                "inner join  wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "where r.reg_codigo = re.reg_codigo) as pulmao, " +
                                /*Pesquisa o picking*/
                                "(select count(s.apa_codigo) from wms_separacao s " +
                                "inner join wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on a.niv_codigo = n.niv_codigo " +
                                "inner join  wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "where r.reg_codigo = re.reg_codigo) as picking " +
                                "from wms_regiao re " +
                                "where reg_numero = @numeroRegiao";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância um objêto
                Estrutura estrutura = new Estrutura();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["reg_codigo"] != DBNull.Value)
                    {
                        estrutura.codRegiao = Convert.ToInt32(linha["reg_codigo"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        estrutura.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        estrutura.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    if (linha["pulmao"] != DBNull.Value)
                    {
                        estrutura.qtdPulmao = Convert.ToInt32(linha["pulmao"]);
                    }

                    if (linha["picking"] != DBNull.Value)
                    {
                        estrutura.qtdPicking = Convert.ToInt32(linha["picking"]);
                    }
                }

                //Retorna os valores encontrado
                return estrutura;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as informações da região. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o inventario aberto
        public Inventarios PesqInventario(string empresa)
        {
            try
            {

                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select inv_codigo, inv_descricao, usu_login, inv_data_inicial, inv_tipo, " +
                                "int_rotativo, inv_auditoria, inv_importar_erp, inv_cont_picking, inv_cont_picking_flow, " +
                                "inv_cont_pulmao, inv_cont_avaria, inv_cont_volume_flow, inv_cont_vencimento, inv_cont_lote from wms_inventario i " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = i.usu_codigo_inicial " +
                                "where inv_data_final is null and inv_status = 'ABERTO' " +
                                "and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
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

                    if (linha["inv_cont_picking"] != DBNull.Value)
                    {
                        inventario.contPicking = Convert.ToBoolean(linha["inv_cont_picking"]);
                    }

                    if (linha["inv_cont_picking_flow"] != DBNull.Value)
                    {
                        inventario.contPickingFlow = Convert.ToBoolean(linha["inv_cont_picking_flow"]);
                    }

                    if (linha["inv_cont_pulmao"] != DBNull.Value)
                    {
                        inventario.contPulmao = Convert.ToBoolean(linha["inv_cont_pulmao"]);
                    }

                    if (linha["inv_cont_avaria"] != DBNull.Value)
                    {
                        inventario.contAvaria = Convert.ToBoolean(linha["inv_cont_avaria"]);
                    }

                    if (linha["inv_cont_volume_flow"] != DBNull.Value)
                    {
                        inventario.contVolumeFlow = Convert.ToBoolean(linha["inv_cont_volume_flow"]);
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

        //Pesquisa o itens do inventario aberto
        public ProdutoCollection PesqItensInventario(int codInventario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //String de consulta
                string select = "select i.prod_id, prod_codigo, prod_descricao from wms_inventario_historico i " +
                                "inner join wms_produto p " +
                                "on i.prod_id = p.prod_id " +
                                "where inv_codigo = @codInventario";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância uma coleção de objetos
                ProdutoCollection produtoCollection = new ProdutoCollection();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Produto produto = new Produto();

                    //Adiciona os valores encontrados
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        produto.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        produto.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    produtoCollection.Add(produto);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens do inventário. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o fornecedor do inventario aberto
        public FornecedorCollection PesqFornecedorInventario(int codInventario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona o parâmetro
                conexao.AdicionarParamentros("@codInventario", codInventario);
                //String de consulta
                string select = "select distinct(f.forn_codigo), f.forn_nome from wms_inventario_historico i " +
                                "inner join wms_produto p " +
                                "on i.prod_id = p.prod_id " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = p.forn_codigo " +
                                "where inv_codigo = @codInventario";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância uma coleção de objetos
                FornecedorCollection fornecedorCollection = new FornecedorCollection();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Fornecedor fornecedor = new Fornecedor();

                    //Adiciona os valores encontrados
                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        fornecedor.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        fornecedor.nomeFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    fornecedorCollection.Add(fornecedor);
                }
                //Retorna a coleção de cadastro encontrada
                return fornecedorCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os fornecedores no inventário. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa informações da região
        public EstruturaCollection PesqEnderecoInventario(int numeroRegiao)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adicionar parâmetros
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);

                //String de consulta
                string select = "select distinct(rg.reg_codigo), reg_numero, reg_tipo, " +
                                /*Pesquisa o pulmao*/
                                "(select count(ii.apa_codigo) from wms_item_inventario ii " +
                                "inner join wms_apartamento app " +
                                "on app.apa_codigo = ii.apa_codigo " +
                                "where app.apa_tipo = 'Pulmao' and ii.inv_codigo = i.inv_codigo ) as pulmao, " +
                                /*Pesquisa o picking*/
                                "(select count(ii.apa_codigo) from wms_item_inventario ii " +
                                "inner join wms_apartamento app " +
                                "on app.apa_codigo = ii.apa_codigo " +
                                "where app.apa_tipo = 'Picking' and ii.inv_codigo = i.inv_codigo ) as picking " +
                                "from wms_item_inventario i " +
                                "inner join wms_apartamento ap " +
                                "on i.apa_codigo = ap.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on ap.niv_codigo = n.niv_codigo " +
                                "inner join  wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao rg " +
                                "on rg.reg_codigo = r.reg_codigo " +
                                "where inv_codigo = @numeroRegiao";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Inst^ncia uma coleção de objêto
                EstruturaCollection estruturaCollection = new EstruturaCollection();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância um objêto
                    Estrutura estrutura = new Estrutura();

                    //Adiciona os valores encontrados
                    if (linha["reg_codigo"] != DBNull.Value)
                    {
                        estrutura.codRegiao = Convert.ToInt32(linha["reg_codigo"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        estrutura.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        estrutura.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    if (linha["pulmao"] != DBNull.Value)
                    {
                        estrutura.qtdPulmao = Convert.ToInt32(linha["pulmao"]);
                    }

                    if (linha["picking"] != DBNull.Value)
                    {
                        estrutura.qtdPicking = Convert.ToInt32(linha["picking"]);
                    }

                    //Adciona o objêto
                    estruturaCollection.Add(estrutura);

                }

                //Retorna os valores encontrado
                return estruturaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os endereços no invetário. \nDetalhes:" + ex.Message);
            }
        }

        //Gera o inventário
        public void GeraInventario(Inventarios inventario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", inventario.codInventario);
                //conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@descInventario", inventario.descInventario);
                conexao.AdicionarParamentros("@codUsuarioInicial", inventario.codUsuarioInicial);
                conexao.AdicionarParamentros("@tipoInventario", inventario.tipoInventario);
                conexao.AdicionarParamentros("@descRotativo", inventario.descRotativo);
                conexao.AdicionarParamentros("@tipoAuditoria", inventario.tipoAuditoria);
                conexao.AdicionarParamentros("@importarERP", inventario.importarERP);
                conexao.AdicionarParamentros("@contPicking", inventario.contPicking);
                conexao.AdicionarParamentros("@contPickingFlow", inventario.contPickingFlow);
                conexao.AdicionarParamentros("@contPulmao", inventario.contPulmao);
                conexao.AdicionarParamentros("@contAvaria", inventario.contAvaria);
                conexao.AdicionarParamentros("@contVolumeFlow", inventario.contVolumeFlow);
                conexao.AdicionarParamentros("@contVencimento", inventario.contVencimento);
                conexao.AdicionarParamentros("@contLote", inventario.contLote);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de insert
                string insert = "insert into wms_inventario (inv_codigo, inv_descricao, usu_codigo_inicial, inv_data_inicial, inv_tipo, " +
                                "int_rotativo, inv_auditoria, inv_importar_erp, inv_cont_picking, inv_cont_picking_flow, " +
                                "inv_cont_pulmao, inv_cont_avaria, inv_cont_volume_flow, inv_cont_vencimento, inv_cont_lote, inv_status, conf_codigo) " +
                                "values " +
                                "(@codInventario, @descInventario, @codUsuarioInicial, current_timestamp, @tipoInventario, " +
                                "@descRotativo, @tipoAuditoria, @importarERP, @contPicking, @contPickingFlow," +
                                " @contPulmao, @contAvaria, @contVolumeFlow, @contVencimento, @contLote, 'ABERTO',(select conf_codigo from wms_configuracao where conf_sigla = @empresa))";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o inventário. \nDetalhes:" + ex.Message);
            }
        }

        //Gera os itens do pulmão
        public void GeraItensPulmaoInventario(int codInventario, int codUsuario, int[] idProduto, int[] codFornecedor, int[] codRegiao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@codUsuarioInicial", codUsuario);

                //String de insert
                string insert = "insert into wms_item_inventario(inv_codigo, iinv_codigo, iinv_operacao, iinv_tempo_primeira, apa_codigo, prod_id, iinv_cont_primeira, iinv_venc_primeira, " +
                                "iinv_peso_primeira, iinv_lote_primeira, usu_codigo_primeira) " +

                                "select @codInventario, gen_id(gen_wms_item_inventario, 1), arm_codigo, current_timestamp, a.apa_codigo, prod_id, arm_quantidade, arm_vencimento, arm_peso, arm_lote, @codUsuarioInicial from wms_armazenagem a ";


                if (idProduto != null && idProduto.Length > 0)
                {
                    insert += "where prod_id = " + idProduto[0] + " ";

                    for (int i = 1; idProduto.Length > i; i++)
                    {
                        if (idProduto[i] != 0)
                        {
                            insert += "or prod_id = " + idProduto[i] + " ";
                        }
                    }
                }

                if (codFornecedor != null && codFornecedor.Length > 0)
                {
                    insert += "where prod_id in (select prod_id from wms_produto where forn_codigo = " + codFornecedor[0] + ") ";

                    for (int i = 1; codFornecedor.Length > i; i++)
                    {
                        if (codFornecedor[i] != 0)
                        {
                            insert += "or prod_id in (select prod_id from wms_produto where forn_codigo = " + codFornecedor[i] + ") ";
                        }
                    }
                }

                if (codRegiao != null && codRegiao.Length > 0)
                {
                    insert += "inner join wms_apartamento ap " +
                              "on a.apa_codigo = ap.apa_codigo " +
                              "inner join wms_nivel n " +
                              "on ap.niv_codigo = n.niv_codigo " +
                              "inner join  wms_bloco b " +
                              "on b.bloc_codigo = n.bloc_codigo  " +
                              "inner join wms_rua r " +
                              "on r.rua_codigo = b.rua_codigo " +
                              "where r.reg_codigo =  " + codRegiao[0] + " ";

                    for (int i = 1; codRegiao.Length > i; i++)
                    {
                        if (codRegiao[i] != 0)
                        {
                            insert += "or r.reg_codigo = " + codRegiao[i] + " ";
                        }
                    }
                }

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do inventário. \nDetalhes:" + ex.Message);
            }
        }

        //Gera os itens do picking
        public void GeraItensPickingInventario(int codInventario, int codUsuario, bool contPicking, bool contPickingFlow, int[] idProduto, int[] codFornecedor, int[] codRegiao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@codUsuarioInicial", codUsuario);


                //String de insert
                string insert = "insert into wms_item_inventario(inv_codigo, iinv_codigo, iinv_operacao, iinv_tempo_primeira, apa_codigo, prod_id, iinv_cont_primeira, iinv_venc_primeira, " +
                                "iinv_peso_primeira, iinv_lote_primeira, usu_codigo_primeira) " +

                                "select @codInventario, gen_id(gen_wms_item_inventario, 1), sep_codigo, current_timestamp, s.apa_codigo, prod_id, coalesce(sep_estoque,0), s.sep_validade, " +
                                "s.sep_peso, s.sep_lote, @codUsuarioInicial from wms_separacao s ";


                if (contPicking == true && contPickingFlow == false)
                {
                    insert += "where sep_tipo = 'CAIXA'";
                }

                if (contPicking == false && contPickingFlow == true)
                {
                    insert += "where sep_tipo = 'FLOWRACK' ";
                }

                if (idProduto != null && idProduto.Length > 0)
                {
                    if (contPicking == true && contPickingFlow == false)
                    {
                        insert += "and prod_id = " + idProduto[0] + " ";
                    }
                    else if (contPicking == false && contPickingFlow == true)
                    {
                        insert += "and prod_id = " + idProduto[0] + " ";
                    }
                    else
                    {
                        insert += "where prod_id = " + idProduto[0] + " ";
                    }

                    for (int i = 1; idProduto.Length > i; i++)
                    {
                        if (idProduto[i] != 0)
                        {
                            insert += "or prod_id = " + idProduto[i] + " ";
                        }
                    }
                }

                if (codFornecedor != null && codFornecedor.Length > 0)
                {
                    insert += "where prod_id in (select prod_id from wms_produto where forn_codigo = " + codFornecedor[0] + ") ";

                    for (int i = 1; codFornecedor.Length > i; i++)
                    {
                        if (codFornecedor[i] != 0)
                        {
                            insert += "or prod_id in (select prod_id from wms_produto where forn_codigo = " + codFornecedor[i] + ") ";
                        }
                    }
                }

                if (codRegiao != null && codRegiao.Length > 0)
                {
                    insert += "inner join wms_apartamento ap " +
                              "on s.apa_codigo = ap.apa_codigo " +
                              "inner join wms_nivel n " +
                              "on ap.niv_codigo = n.niv_codigo " +
                              "inner join  wms_bloco b " +
                              "on b.bloc_codigo = n.bloc_codigo  " +
                              "inner join wms_rua r " +
                              "on r.rua_codigo = b.rua_codigo " +
                              "where r.reg_codigo =  " + codRegiao[0] + " ";

                    for (int i = 1; codRegiao.Length > i; i++)
                    {
                        if (codRegiao[i] != 0)
                        {
                            insert += "or r.reg_codigo = " + codRegiao[i] + " ";
                        }
                    }
                }

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens de separação do inventário. \nDetalhes:" + ex.Message);
            }
        }

        //Gera um histórico do inventário com produtos da area de separação - Picking
        public void GeraHistoricoItensPicking(int codInventario, int[] idProduto, int[] codFornecedor, int[] codRegiao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);

                //String de insert LEMBRE-SE O DISTINCT SEMPRE VEM NA FRENTE
                string insert = "insert into wms_inventario_historico (prod_id, inv_codigo, prod_estoque, prod_avaria, prod_custo, prod_venda) " +

                                "select distinct(e.prod_id), @codInventario, e.est_quantidade, e.est_avaria, " +
                                "e.est_preco_custo, e.est_preco from wms_estoque e " +
                                "inner join wms_separacao s " +
                                "on s.prod_id = e.prod_id ";

                if (idProduto != null && idProduto.Length > 0)
                {
                    insert += "where e.prod_id = " + idProduto[0] + " ";

                    for (int i = 1; idProduto.Length > i; i++)
                    {
                        if (idProduto[i] != 0)
                        {
                            insert += "or e.prod_id = " + idProduto[i] + " ";
                        }
                    }
                }

                if (codFornecedor != null && codFornecedor.Length > 0)
                {
                    insert += "where e.prod_id in (select prod_id from wms_produto where forn_codigo = " + codFornecedor[0] + " ) ";

                    for (int i = 1; codFornecedor.Length > i; i++)
                    {
                        if (codFornecedor[i] != 0)
                        {
                            insert += "or e.prod_id in (select prod_id from wms_produto where forn_codigo = " + codFornecedor[i] + " ) ";
                        }
                    }
                }

                if (codRegiao != null && codRegiao.Length > 0)
                {
                    insert += "inner join wms_apartamento ap " +
                              "on s.apa_codigo = ap.apa_codigo " +
                              "inner join wms_nivel n " +
                              "on ap.niv_codigo = n.niv_codigo " +
                              "inner join  wms_bloco b " +
                              "on b.bloc_codigo = n.bloc_codigo  " +
                              "inner join wms_rua r " +
                              "on r.rua_codigo = b.rua_codigo " +
                              "where r.reg_codigo =  " + codRegiao[0] + " ";

                    for (int i = 1; codRegiao.Length > i; i++)
                    {
                        if (codRegiao[i] != 0)
                        {
                            insert += "or r.reg_codigo = " + codRegiao[i] + " ";
                        }
                    }
                }



                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um histórico para o inventário. \nDetalhes:" + ex.Message);
            }
        }

        //Gera um histórico do inventário com produtos do pulmão que não existe no Picking
        public void GeraHistoricoItensPulmao(int codInventario, int[] idProduto, int[] codFornecedor, int[] codRegiao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);

                //String de insert LEMBRE-SE O DISTINCT SEMPRE VEM NA FRENTE
                string insert = "insert into wms_inventario_historico (prod_id, inv_codigo, prod_estoque, prod_avaria, prod_flowrack, prod_custo, prod_venda) " +

                                "select distinct(a.prod_id), @codInventario, e.est_quantidade, e.est_avaria, " +
                                /*Pesquisa o produto que tem no picking*/
                                "(select sum(i.iflow_qtd_conferida) from wms_item_flowrack i " +
                                "where not i.iflow_data_conferencia is null and i.prod_id = e.prod_id) qtd_flow, " +
                                "e.est_preco_custo, e.est_preco " +
                                "from wms_armazenagem a " +
                                "inner join wms_estoque e " +
                                "on e.prod_id = a.prod_id " +
                                "where a.prod_id not in (select prod_id from wms_separacao) ";

                if (idProduto != null && idProduto.Length > 0)
                {
                    insert += "where e.prod_id = " + idProduto[0] + " ";

                    for (int i = 1; idProduto.Length > i; i++)
                    {
                        if (idProduto[i] != 0)
                        {
                            insert += "or e.prod_id = " + idProduto[i] + " ";
                        }
                    }
                }

                if (codFornecedor != null && codFornecedor.Length > 0)
                {
                    insert += "where e.prod_id in (select prod_id from wms_produto where forn_codigo = " + codFornecedor[0] + " ) ";

                    for (int i = 1; codFornecedor.Length > i; i++)
                    {
                        if (codFornecedor[i] != 0)
                        {
                            insert += "or e.prod_id in (select prod_id from wms_produto where forn_codigo = " + codFornecedor[i] + " ) ";
                        }
                    }
                }

                if (codRegiao != null && codRegiao.Length > 0)
                {
                    insert += "inner join wms_apartamento ap " +
                              "on s.apa_codigo = ap.apa_codigo " +
                              "inner join wms_nivel n " +
                              "on ap.niv_codigo = n.niv_codigo " +
                              "inner join  wms_bloco b " +
                              "on b.bloc_codigo = n.bloc_codigo  " +
                              "inner join wms_rua r " +
                              "on r.rua_codigo = b.rua_codigo " +
                              "where r.reg_codigo =  " + codRegiao[0] + " ";

                    for (int i = 1; codRegiao.Length > i; i++)
                    {
                        if (codRegiao[i] != 0)
                        {
                            insert += "or r.reg_codigo = " + codRegiao[i] + " ";
                        }
                    }
                }



                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um histórico com o pulmão do inventário. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa as divergência na contagem no inventario
        public int[] AnalisarContagemInventario(int codInventario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);

                //String de consulta
                string select = "select  " +
                    //Verifica o pulmão
                    "(select count(a.apa_codigo) from wms_armazenagem a " +
                    "where apa_codigo not in " +
                    "(select i.apa_codigo from wms_item_inventario i " +
                    "inner join wms_apartamento ap " +
                    "on ap.apa_codigo = i.apa_codigo and a.prod_id = i.prod_id " +
                    "where apa_tipo = 'Pulmao' and inv_codigo = @codInventario )) as contagem_um_pulmao, " +
                    //Verifica o picking
                    "(select count(s.apa_codigo) from wms_separacao s " +
                    "where s.apa_codigo not in " +
                    "(select i.apa_codigo from wms_item_inventario i " +
                    "inner join wms_apartamento ap " +
                    "on ap.apa_codigo = i.apa_codigo " +
                    "where apa_tipo = 'Separacao' and inv_codigo = @codInventario)) as contagem_um_picking," +

                    "(select count(apa_codigo) from wms_item_inventario where inv_codigo = @codInventario and iinv_cont_segunda is null) as contagem_dois," +
                    "(select count(apa_codigo) from wms_item_inventario where inv_codigo = @codInventario and iinv_venc_segunda is null) as vencimento_dois," +
                    "(select count(apa_codigo) from wms_item_inventario where inv_codigo = @codInventario and iinv_lote_segunda is null) as lote_dois," +
                    "(select count(apa_codigo) from wms_item_inventario where inv_codigo = @codInventario and iinv_cont_final is null) as contagem_tres," +
                    "(select count(apa_codigo) from wms_item_inventario where inv_codigo = @codInventario and iinv_venc_final is null) as vencimento_tres," +
                    "(select count(apa_codigo) from wms_item_inventario where inv_codigo = @codInventario and iinv_lote_final is null) as lote_tres " +
                    "from RDB$DATABASE ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância um array
                int[] dadosContagem = new int[8];

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["contagem_um_picking"] != DBNull.Value)
                    {
                        dadosContagem[0] = Convert.ToInt32(linha["contagem_um_picking"]);
                    }

                    if (linha["contagem_um_pulmao"] != DBNull.Value)
                    {
                        dadosContagem[1] = Convert.ToInt32(linha["contagem_um_pulmao"]);
                    }

                    if (linha["contagem_dois"] != DBNull.Value)
                    {
                        dadosContagem[2] = Convert.ToInt32(linha["contagem_dois"]);
                    }

                    if (linha["contagem_tres"] != DBNull.Value)
                    {
                        dadosContagem[3] = Convert.ToInt32(linha["contagem_tres"]);
                    }

                    if (linha["vencimento_dois"] != DBNull.Value)
                    {
                        dadosContagem[4] = Convert.ToInt32(linha["vencimento_dois"]);
                    }

                    if (linha["vencimento_tres"] != DBNull.Value)
                    {
                        dadosContagem[5] = Convert.ToInt32(linha["vencimento_tres"]);
                    }

                    if (linha["lote_dois"] != DBNull.Value)
                    {
                        dadosContagem[6] = Convert.ToInt32(linha["lote_dois"]);
                    }

                    if (linha["lote_tres"] != DBNull.Value)
                    {
                        dadosContagem[7] = Convert.ToInt32(linha["lote_tres"]);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return dadosContagem;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao verificar as divergência do invetário. \nDetalhes:" + ex.Message);
            }
        }


        //Atualizar inventário
        public void AtualizaInventario(int codInventario, bool importarERP, bool contVencimento, bool contLote)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@importarERP", importarERP);
                conexao.AdicionarParamentros("@contVencimento", contVencimento);
                conexao.AdicionarParamentros("@contLote", contLote);

                //String de atualização
                string update = "update wms_inventario set inv_importar_erp = @importarERP, inv_cont_vencimento = @contVencimento, " +
                    "inv_cont_lote = @contLote where inv_codigo = @codInventario";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o inventário. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza o picking do inventário
        public void AtualizaPicking(int codInventario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);

                //String de atualização
                string update = "MERGE INTO  " +
                                "wms_separacao AS S " +
                                "USING " +
                                "(SELECT apa_codigo, iinv_cont_final, iinv_venc_final, iinv_peso_final, iinv_lote_final FROM wms_item_inventario " +
                                "WHERE inv_codigo = @codInventario) AS I " +
                                "ON(I.apa_codigo = S.apa_codigo) " +
                                "WHEN MATCHED THEN " +
                                "UPDATE SET S.sep_estoque = I.iinv_cont_final, S.sep_validade = I.iinv_venc_final, " +
                                "S.sep_peso = I.iinv_peso_final, S.sep_lote = I.iinv_lote_final";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os pickings. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza o pulmão do inventário
        public void AtualizaPulmao(int codInventario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);

                //String de atualização
                string update = "MERGE INTO  " +
                                "wms_armazenagem AS A  " +
                                "USING  " +
                                "(SELECT I.iinv_operacao, I.apa_codigo, I.prod_id, iinv_cont_final, iinv_venc_final, iinv_peso_final, iinv_lote_final FROM wms_item_inventario AS I " +
                                "INNER JOIN wms_armazenagem A " +
                                "ON(I.apa_codigo = A.apa_codigo) " +
                                "WHERE inv_codigo = @codInventario AND A.prod_id = I.prod_id) AS I " +
                                "ON ( A.arm_codigo = I.iinv_operacao)  " +
                                "WHEN MATCHED THEN " +
                                "UPDATE SET A.arm_quantidade = I.iinv_cont_final, A.arm_vencimento = I.iinv_venc_final, " +
                                "A.arm_peso = I.iinv_peso_final, A.arm_lote = I.iinv_lote_final";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //Insere todos os produtos que não existia 
                string insert = "insert into wms_armazenagem (arm_codigo, arm_data_armazenagem, apa_codigo, prod_id, arm_quantidade, arm_vencimento, arm_peso, arm_lote, arm_impresso, usu_codigo) " +

                                "select gen_id(gen_wms_armazenagem, 1), current_timestamp, i.apa_codigo, i.prod_id, iinv_cont_final, iinv_venc_final, " +
                                "iinv_peso_final, iinv_lote_final, 'Sim', usu_codigo_final from wms_item_inventario i " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = i.apa_codigo " +
                                "where apa_tipo = 'Pulmao' and inv_codigo = @codInventario and i.iinv_operacao = 0 and iinv_cont_final > 0 ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                //Deleta todos os produtos com estoque zero
                string delete = "delete from wms_armazenagem where arm_quantidade = 0 ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os endereços do pulmão. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza o historico so inventário com a contagem do flowrack
        public void AtualizaHistorico(int codInventario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);

                //String de atualização
                string update = "update wms_inventario_historico i set " +
                    "prod_flowrack = (select sum(r.iflow_qtd_conferida) from wms_rastreamento_flowrack r where r.prod_id = i.prod_id and r.inv_codigo = @codInventario)," +
                    "prod_contagem = (select sum(iinv_cont_final) from wms_item_inventario ii where ii.prod_id = i.prod_id and i.inv_codigo = @codInventario)," +
                    "prod_saldo = (coalesce((select sum(r.iflow_qtd_conferida) from wms_rastreamento_flowrack r where r.prod_id = i.prod_id and r.inv_codigo = @codInventario),0) + coalesce((select sum(iinv_cont_final) from wms_item_inventario ii where ii.prod_id = i.prod_id and i.inv_codigo = @codInventario),0)) - prod_estoque ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o histórico do inventário. \nDetalhes:" + ex.Message);
            }
        }


        //fechar inventário
        public void FecharInventario(int codInventario, int codUsuario, string status)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codInventario", codInventario);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@status", status);

                //String de atualização
                string update = "update wms_inventario set inv_status = @status, inv_data_final = current_timestamp, " +
                    "usu_codigo_final = @codUsuario where inv_codigo = @codInventario";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao cancelar o inventário. \nDetalhes:" + ex.Message);
            }
        }
    }
}

