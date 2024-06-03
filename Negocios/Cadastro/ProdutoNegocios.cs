using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class ProdutoNegocios : EstruturaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa os produtos
        public ProdutoCollection PesqProduto(string codProduto, string descProduto, string codFornecedor, string codBarra, string descCategoria, bool status, string empresa)
        {
            try
            {
                //Instância a coleção
                ProdutoCollection produtoCollection = new ProdutoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@descProduto", descProduto);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@descCategoria", descCategoria);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select p.prod_id, p.prod_codigo, p.prod_descricao, f.forn_codigo, f.forn_nome, c.cat_codigo, c.cat_descricao, p.prod_tipo_armazenamento, " +
                "p.prod_vida_util, p.prod_tolerancia, p.prod_niv_maximo, p.prod_multiplo, p.prod_tipo_palete, p.prod_lastro_p, p.prod_altura_p, " +
                "p.prod_lastro_m, p.prod_altura_m, p.prod_lastro_g, p.prod_altura_g, p.prod_lastro_b, p.prod_altura_b, " +
                "p.prod_fator_compra, uc.uni_unidade as uni_compra,  p.prod_fator_pulmao, " +
                "(select uni_unidade from wms_unidade where uni_codigo = p.uni_codigo_pulmao) as uni_pulmao, " +
                "p.prod_fator_picking, " +
                "(select uni_unidade from wms_unidade where uni_codigo = p.uni_codigo_picking) as uni_separacao, " +
                "p.prod_peso, p.prod_status, p.prod_separacao_flowrack, p.prod_audita_flowrack, p.prod_controla_validade, " +
                "p.prod_palete_blocado, p.prod_palete_padrao, p.prod_peso_variavel, prod_conferir_caixa " +
                "from wms_produto p " +
                "inner join wms_fornecedor f " +
                "on p.forn_codigo = f.forn_codigo and f.conf_codigo = p.conf_codigo " +
                "left join wms_categoria c " +
                "on c.cat_codigo = p.cat_codigo " +
                "left join wms_unidade uc " +
                "on uc.uni_codigo = p.uni_codigo_compra ";


                if (codProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_codigo = @codProduto " +
                        "and p.conf_codigo = (select conf_codigo from wms_configuracao " +
                        "where conf_sigla = @empresa) order by p.prod_codigo";
                }
                else if (descProduto != string.Empty)
                {
                    select += "where prod_status = @status and " +
                        "prod_descricao like '%" + @descProduto + "%' " +
                        "and p.conf_codigo = (select conf_codigo from wms_configuracao " +
                        "where conf_sigla = @empresa) order by p.prod_codigo";
                }
                else if (codFornecedor != string.Empty)
                {
                    select += "where prod_status = @status  and f.forn_codigo = @codFornecedor " +
                        "and p.conf_codigo = (select conf_codigo from wms_configuracao " +
                        "where conf_sigla = @empresa) order by p.prod_codigo";
                }
                else if (codBarra != string.Empty)
                {
                    select += " inner join wms_barra b " +
                        "on b.prod_id = p.prod_id and p.conf_codigo = p.conf_codigo " +
                        "where prod_status = @status and bar_numero = @codBarra " +
                        "and p.conf_codigo = (select conf_codigo from wms_configuracao " +
                        "where conf_sigla = @empresa) order by p.prod_codigo";
                }
                else if (descCategoria != string.Empty)
                {
                    select += "where prod_status = @status and cat_descricao = @descCategoria and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
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

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        produto.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        produto.nomeFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["cat_codigo"] != DBNull.Value)
                    {
                        produto.codCategoria = Convert.ToInt32(linha["cat_codigo"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        produto.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["prod_tipo_armazenamento"] != DBNull.Value)
                    {
                        produto.tipoArmazenagem = Convert.ToString(linha["prod_tipo_armazenamento"]);
                    }

                    if (linha["prod_vida_util"] != DBNull.Value)
                    {
                        produto.shelfLife = Convert.ToInt32(linha["prod_vida_util"]);
                    }

                    if (linha["prod_tolerancia"] != DBNull.Value)
                    {
                        produto.tolerancia = Convert.ToInt32(linha["prod_tolerancia"]);
                    }

                    if (linha["prod_niv_maximo"] != DBNull.Value)
                    {
                        produto.nivelMaximo = Convert.ToInt32(linha["prod_niv_maximo"]);
                    }

                    if (linha["prod_tipo_palete"] != DBNull.Value)
                    {
                        produto.tipoPalete = Convert.ToString(linha["prod_tipo_palete"]);
                    }

                    if (linha["prod_lastro_p"] != DBNull.Value)
                    {
                        produto.lastroPequeno = Convert.ToInt32(linha["prod_lastro_p"]);
                    }

                    if (linha["prod_altura_p"] != DBNull.Value)
                    {
                        produto.alturaPequeno = Convert.ToInt32(linha["prod_altura_p"]);
                    }

                    if (linha["prod_lastro_m"] != DBNull.Value)
                    {
                        produto.lastroMedio = Convert.ToInt32(linha["prod_lastro_m"]);
                    }

                    if (linha["prod_altura_m"] != DBNull.Value)
                    {
                        produto.alturaMedio = Convert.ToInt32(linha["prod_altura_m"]);
                    }

                    if (linha["prod_lastro_g"] != DBNull.Value)
                    {
                        produto.lastroGrande = Convert.ToInt32(linha["prod_lastro_g"]);
                    }

                    if (linha["prod_altura_g"] != DBNull.Value)
                    {
                        produto.alturaGrande = Convert.ToInt32(linha["prod_altura_g"]);
                    }

                    if (linha["prod_lastro_b"] != DBNull.Value)
                    {
                        produto.lastroBlocado = Convert.ToInt32(linha["prod_lastro_b"]);
                    }
                    if (linha["prod_altura_b"] != DBNull.Value)
                    {
                        produto.alturaBlocado = Convert.ToInt32(linha["prod_altura_b"]);
                    }

                    if (linha["prod_fator_compra"] != DBNull.Value)
                    {
                        produto.fatorCompra = Convert.ToInt32(linha["prod_fator_compra"]);
                    }

                    if (linha["uni_compra"] != DBNull.Value)
                    {
                        produto.undCompra = Convert.ToString(linha["uni_compra"]);
                    }
                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        produto.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        produto.undPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["prod_fator_picking"] != DBNull.Value)
                    {
                        produto.fatorPicking = Convert.ToInt32(linha["prod_fator_picking"]);
                    }
                    if (linha["uni_separacao"] != DBNull.Value)
                    {
                        produto.undPicking = Convert.ToString(linha["uni_separacao"]);
                    }

                    if (linha["prod_multiplo"] != DBNull.Value)
                    {
                        produto.multiploProduto = Convert.ToInt32(linha["prod_multiplo"]);
                    }

                    if (linha["prod_peso"] != DBNull.Value)
                    {
                        produto.pesoProduto = Convert.ToDouble(linha["prod_peso"]);
                    }

                    if (linha["prod_status"] != DBNull.Value)
                    {
                        produto.status = Convert.ToBoolean(linha["prod_status"]);
                    }
                    if (linha["prod_separacao_flowrack"] != DBNull.Value)
                    {
                        produto.separacaoFlowrack = Convert.ToBoolean(linha["prod_separacao_flowrack"]);
                    }
                    if (linha["prod_audita_flowrack"] != DBNull.Value)
                    {
                        produto.auditaFlowrack = Convert.ToBoolean(linha["prod_audita_flowrack"]);
                    }

                    if (linha["prod_controla_validade"] != DBNull.Value)
                    {
                        produto.controlaValidade = Convert.ToBoolean(linha["prod_controla_validade"]);
                    }
                    if (linha["prod_palete_blocado"] != DBNull.Value)
                    {
                        produto.paleteBlocado = Convert.ToBoolean(linha["prod_palete_blocado"]);
                    }
                    
                    if (linha["prod_palete_padrao"] != DBNull.Value)
                    {
                        produto.paletePadrao = Convert.ToBoolean(linha["prod_palete_padrao"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        produto.pesoVariavel = Convert.ToBoolean(linha["prod_peso_variavel"]);
                    }

                    if (linha["prod_conferir_caixa"] != DBNull.Value)
                    {
                        produto.conferirCaixa = Convert.ToBoolean(linha["prod_conferir_caixa"]);
                    }                   


                    //Adiciona os cadastros encontrados a coleção
                    produtoCollection.Add(produto);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao efetuar a pesquisa de produto. \nDetalhes:" + ex.Message);
            }

        }

        public void Alterar(string empresa, Produto produto)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@idProduto", produto.idProduto);
                conexao.AdicionarParamentros("@codCategoria", produto.codCategoria);
                conexao.AdicionarParamentros("@tipoArmazenagem", produto.tipoArmazenagem);
                conexao.AdicionarParamentros("@tipoPalete", produto.tipoPalete);
                conexao.AdicionarParamentros("@shelfLife", produto.shelfLife);
                conexao.AdicionarParamentros("@tolerancia", produto.tolerancia);
                conexao.AdicionarParamentros("@nivelMaximo", produto.nivelMaximo);
                conexao.AdicionarParamentros("@lastroPequeno", produto.lastroPequeno);
                conexao.AdicionarParamentros("@alturaPequeno", produto.alturaPequeno);
                conexao.AdicionarParamentros("@lastroMedio", produto.lastroMedio);
                conexao.AdicionarParamentros("@alturaMedio", produto.alturaMedio);
                conexao.AdicionarParamentros("@lastroGrande", produto.lastroGrande);
                conexao.AdicionarParamentros("@alturaGrande", produto.alturaGrande);
                conexao.AdicionarParamentros("@lastroBlocado", produto.lastroBlocado);
                conexao.AdicionarParamentros("@alturaBlocado", produto.alturaBlocado);
                conexao.AdicionarParamentros("@codUndCompra", produto.codUndCompra);
                conexao.AdicionarParamentros("@fatorPulmao", produto.fatorPulmao);
                conexao.AdicionarParamentros("@codUndPulmao", produto.codUndPulmao);
                conexao.AdicionarParamentros("@fatorPicking", produto.fatorPicking);
                conexao.AdicionarParamentros("@codUndPicking", produto.codUndPicking);
                conexao.AdicionarParamentros("@auditaFlowrack", produto.auditaFlowrack);
                conexao.AdicionarParamentros("@controlaValidade", produto.controlaValidade);
                conexao.AdicionarParamentros("@separacaoFlowrack", produto.separacaoFlowrack);
                conexao.AdicionarParamentros("@paletePadrao", produto.paletePadrao);
                conexao.AdicionarParamentros("@paleteBlocado", produto.paleteBlocado);
                conexao.AdicionarParamentros("@pesoVariavel", produto.pesoVariavel);
                conexao.AdicionarParamentros("@conferirCaixa", produto.conferirCaixa);


                //String de atualização
                string update = "update wms_produto set cat_codigo = @codCategoria, prod_tipo_armazenamento = @tipoArmazenagem, prod_tipo_palete = @tipoPalete, prod_vida_util = @shelfLife, prod_tolerancia = @tolerancia, " +
                        "prod_niv_maximo = @nivelMaximo, prod_lastro_p = @lastroPequeno, prod_altura_p = @alturaPequeno, " +
                        "prod_lastro_m = @lastroMedio, prod_altura_m = @alturaMedio, prod_lastro_g = @lastroGrande, prod_altura_g = @alturaGrande, prod_lastro_b = @lastroBlocado, " +
                        "prod_altura_b = @alturaBlocado, uni_codigo_compra = @codUndCompra,  " +
                        "prod_fator_pulmao = @fatorPulmao, uni_codigo_pulmao = @codUndPulmao, prod_fator_picking = @fatorPicking, uni_codigo_picking = @codUndPicking, " +
                        "prod_audita_flowrack = @auditaFlowrack, prod_separacao_flowrack = @separacaoFlowrack, prod_controla_validade = @controlaValidade, " +
                        "prod_palete_blocado = @paleteBlocado, prod_palete_padrao = @paletePadrao, prod_peso_variavel = @pesoVariavel, prod_conferir_caixa = @conferirCaixa " +
                        "where prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar os dados logísticos dos produtos. \nDetalhes:" + ex.Message);
            }
        }


        public void InserirRastreamento(Produto produto, int codUsuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@idProduto", produto.idProduto);
                conexao.AdicionarParamentros("@codCategoria", produto.codCategoria);
                conexao.AdicionarParamentros("@tipoArmazenagem", produto.tipoArmazenagem);
                conexao.AdicionarParamentros("@tipoPalete", produto.tipoPalete);
                conexao.AdicionarParamentros("@shelfLife", produto.shelfLife);
                conexao.AdicionarParamentros("@tolerancia", produto.tolerancia);
                conexao.AdicionarParamentros("@nivelMaximo", produto.nivelMaximo);
                conexao.AdicionarParamentros("@lastroPequeno", produto.lastroPequeno);
                conexao.AdicionarParamentros("@alturaPequeno", produto.alturaPequeno);
                conexao.AdicionarParamentros("@lastroMedio", produto.lastroMedio);
                conexao.AdicionarParamentros("@alturaMedio", produto.alturaMedio);
                conexao.AdicionarParamentros("@lastroGrande", produto.lastroGrande);
                conexao.AdicionarParamentros("@alturaGrande", produto.alturaGrande);
                conexao.AdicionarParamentros("@lastroBlocado", produto.lastroBlocado);
                conexao.AdicionarParamentros("@alturaBlocado", produto.alturaBlocado);
                conexao.AdicionarParamentros("@fatorCompra", produto.fatorCompra);
                conexao.AdicionarParamentros("@codUndCompra", produto.codUndCompra);
                conexao.AdicionarParamentros("@fatorPulmao", produto.fatorPulmao);
                conexao.AdicionarParamentros("@codUndPulmao", produto.codUndPulmao);
                conexao.AdicionarParamentros("@fatorPicking", produto.fatorPicking);
                conexao.AdicionarParamentros("@codUndPicking", produto.codUndPicking);
                conexao.AdicionarParamentros("@multiplo", produto.multiploProduto);
                conexao.AdicionarParamentros("@status", produto.status);
                conexao.AdicionarParamentros("@auditaFlowrack", produto.auditaFlowrack);
                conexao.AdicionarParamentros("@controlaValidade", produto.controlaValidade);
                conexao.AdicionarParamentros("@separacaoFlowrack", produto.separacaoFlowrack);
                conexao.AdicionarParamentros("@paletePadrao", produto.paletePadrao);
                conexao.AdicionarParamentros("@paleteBlocado", produto.paleteBlocado);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);


                //String de atualização
                string update = "insert into wms_rastreamento_cadastro (rast_codigo, prod_id, cat_codigo, prod_tipo_armazenamento, prod_tipo_palete, prod_vida_util, prod_tolerancia, " +
                        "prod_niv_maximo, prod_lastro_p, prod_altura_p, prod_lastro_m, prod_altura_m, prod_lastro_g, prod_altura_g, prod_lastro_b, prod_altura_b, " +
                        "prod_fator_compra, uni_codigo_compra, prod_fator_pulmao, uni_codigo_pulmao, prod_fator_picking, uni_codigo_picking, prod_multiplo, " +
                        "prod_status, prod_audita_flowrack, prod_separacao_flowrack, prod_controla_validade, " +
                        "prod_palete_blocado, prod_palete_padrao, rast_data, usu_codigo, conf_codigo) " +

                "select gen_id(gen_wms_rast_cadastro, 1), @idProduto, @codCategoria, @tipoArmazenagem, @tipoPalete, @shelfLife, @tolerancia, " +
                "@nivelMaximo, @lastroPequeno, @alturaPequeno, @lastroMedio, @alturaMedio, @lastroGrande, @alturaGrande, @lastroBlocado, @alturaBlocado, " +
                "@fatorCompra, @codUndCompra, @fatorPulmao, @codUndPulmao, @fatorPicking, @codUndPicking, @multiplo, " +
                "@status, @auditaFlowrack, @separacaoFlowrack, @controlaValidade, " +
                "@paleteBlocado, @paletePadrao, current_timestamp, @codUsuario, (select conf_codigo from wms_configuracao where conf_sigla = @empresa) from RDB$DATABASE ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir as alterações no rastreamento de cadastros. \nDetalhes:" + ex.Message);
            }
        }


        public BarraCollection PesqCodBarra(bool status, string empresa)
        {
            try
            {
                //Instância a coleção
                BarraCollection barraCollection = new BarraCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select b.prod_id, b.bar_codigo, b.bar_numero, b.bar_multiplicador, b.bar_altura, b.bar_largura, b.bar_comprimento, " +
                                "b.bar_cubagem, b.bar_peso, bar_tipo from wms_barra b " +
                                "inner join wms_produto p " +
                                "on b.prod_id = p.prod_id " +
                                "where prod_status = @status " +
                                "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "order by p.prod_codigo";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Barra barra = new Barra();
                    //Adiciona os valores encontrados
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        barra.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["bar_codigo"] != DBNull.Value)
                    {
                        barra.codBarra = Convert.ToInt32(linha["bar_codigo"]);
                    }

                    if (linha["bar_numero"] != DBNull.Value)
                    {
                        barra.numeroBarra = Convert.ToString(linha["bar_numero"]);
                    }

                    if (linha["bar_multiplicador"] != DBNull.Value)
                    {
                        barra.multiplicador = Convert.ToInt32(linha["bar_multiplicador"]);
                    }

                    if (linha["bar_altura"] != DBNull.Value)
                    {
                        barra.altura = Convert.ToDouble(linha["bar_altura"]);
                    }

                    if (linha["bar_largura"] != DBNull.Value)
                    {
                        barra.largura = Convert.ToDouble(linha["bar_largura"]);
                    }

                    if (linha["bar_comprimento"] != DBNull.Value)
                    {
                        barra.comprimento = Convert.ToDouble(linha["bar_comprimento"]);
                    }

                    if (linha["bar_cubagem"] != DBNull.Value)
                    {
                        barra.cubagem = Convert.ToDouble(linha["bar_cubagem"]);
                    }

                    if (linha["bar_peso"] != DBNull.Value)
                    {
                        barra.peso = Convert.ToDouble(linha["bar_peso"]);
                    }

                    if (linha["bar_tipo"] != DBNull.Value)
                    {
                        barra.tipo = Convert.ToString(linha["bar_tipo"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    barraCollection.Add(barra);
                }
                //Retorna a coleção de cadastro encontrada
                return barraCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o código de barra do produto. \nDetalhes:" + ex.Message);
            }
        }

        //Salva o código de barra
        public void SalvarCodBarra(int idProduto, string numeroBarra, int multiplicador,  double altura, double largura, double comprimento, double cubagem, double peso, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@numeroBarra", numeroBarra);
                conexao.AdicionarParamentros("@multiplicador", multiplicador);
                conexao.AdicionarParamentros("@altura", altura);
                conexao.AdicionarParamentros("@largura", largura);
                conexao.AdicionarParamentros("@comprimento", comprimento);
                conexao.AdicionarParamentros("@cubagem", cubagem);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de atualização
                string update = "insert into wms_barra (bar_codigo, prod_id, bar_numero, bar_multiplicador, bar_altura, bar_largura, " +
                                "bar_comprimento, bar_cubagem, bar_peso, bar_tipo, conf_codigo) " +
                                "Values "+
                                "(gen_id(gen_wms_barra,1), @idProduto, @numeroBarra, @multiplicador, @altura, @largura, @comprimento, @cubagem, @peso, " +
                                $"'INTERNO' , (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro salvar os dados logísticos do código de barra. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza o código de barra
        public void AlterarCodBarra(int idProduto, int codBarra, double altura, double largura, double comprimento, double cubagem, double peso,string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@altura", altura);
                conexao.AdicionarParamentros("@largura", largura);
                conexao.AdicionarParamentros("@comprimento", comprimento);
                conexao.AdicionarParamentros("@cubagem", cubagem);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de atualização
                string update = "update wms_barra set bar_altura = @altura, bar_largura = @largura, bar_comprimento = @comprimento, " +
                                "bar_cubagem = @cubagem, bar_peso = @peso " +
                                "where prod_id = @idProduto and bar_codigo = @codBarra and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro salvar os dados logísticos do código de barra. \nDetalhes:" + ex.Message);
            }
        }

        //dELETA o código de barra
        public void DeletarCodBarra(int idProduto, string numeroBarra)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@numeroBarra", numeroBarra);

                //String de atualização
                string update = "delete from wms_barra where prod_id = @idProduto and bar_numero = @numeroBarra";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro excluir o código de barra. \nDetalhes:" + ex.Message);
            }
        }


        //Ajuda - Pesquisa de produto
        public ProdutoCollection PesqProduto(string empresa, string descProduto, string codigo)
        {
            try
            {
                //Instância a coleção
                ProdutoCollection produtoCollection = new ProdutoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@descProduto", "%" + descProduto + "%");
                conexao.AdicionarParamentros("@codigo", codigo);
                //String de consulta
                string select = "select prod_id, prod_codigo, prod_descricao, prod_separacao_flowrack from wms_produto " +
                                 "where prod_status = 'True' and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                if (!codigo.Equals(""))
                {
                    select += "and prod_codigo = @codigo ";
                }
                else
                {
                    select += "and prod_descricao like @descProduto order by prod_descricao";
                }

                //Instância um datatable 
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
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

                    if (linha["prod_separacao_flowrack"] != DBNull.Value)
                    {
                        produto.separacaoFlowrack = Convert.ToBoolean(linha["prod_separacao_flowrack"]);
                    }

                    produtoCollection.Add(produto);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto! \nDetalhes: " + ex.Message);
            }
        }

        public string PesqCaminho()
        {
            try
            {
                string caminho = null;
                //String de consulta
                string select = "select conf_img_produto from wms_configuracao where conf_codigo = 1";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["conf_img_produto"] != DBNull.Value)
                    {
                        caminho = Convert.ToString(linha["conf_img_produto"]);
                    }
                }
                //Retorna o caminho
                return caminho;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o caminho da imagem. \nDetalhes:" + ex.Message);
            }
        }

    }
}
