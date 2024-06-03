using System;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using Dados;
using ObjetoTransferencia;
using ObjetoTransferencia.Relatorio;

namespace Negocios
{
    public class EstoqueNegocios
    {
        //Instância o objeto
        Conexao conexao = new Conexao();

        //Pesquisa o produto
        public ConsultaEstoqueCollection PesqProduto(string codProduto, string descProduto, string codFornecedor, string codBarra, string descCategoria, bool statusProduto, string empresa)
        {
            try
            {
                //Instância o objeto
                ConsultaEstoqueCollection consultaEstoqueCollection = new ConsultaEstoqueCollection();
                //Limpa os parâmetros
                conexao.LimparParametros(); 
                //Adiciona os parâmentros
                //conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@descProduto", descProduto);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@descCategoria", descCategoria);
                conexao.AdicionarParamentros("@status", statusProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta - Pesquisa o produto
                string select = "select p.prod_id, prod_codigo, prod_descricao, p.forn_codigo, forn_nome, e.est_quantidade, e.est_vendido, " +
                "(est_quantidade - est_vendido) as est_saldo, e.est_avaria, e.est_preco, " +
                "(select sum(a.arm_quantidade) from wms_armazenagem a where a.prod_id = p.prod_id) *p.prod_fator_compra as wms, " + //" + "(select sum(s.sep_estoque) from wms_separacao s where s.prod_id = p.prod_id) as
                "p.prod_fator_compra, p.prod_multiplo, c.cat_descricao, p.prod_vida_util, " +
                "p.prod_tolerancia, p.prod_tipo_armazenamento,  p.prod_fator_picking, p.prod_niv_maximo, p.prod_tipo_palete, " +
                "p.prod_lastro_p, p.prod_altura_p, " +
                "p.prod_lastro_m, p.prod_altura_m, " +
                "p.prod_lastro_g, p.prod_altura_g, " +
                "p.prod_lastro_b, p.prod_altura_b, " +
                "p.prod_status, p.prod_audita_flowrack, p.prod_controla_validade, p.prod_palete_blocado, p.prod_separacao_flowrack, " +
                "(select bar_numero from wms_barra b where bar_multiplicador = 1 and b.prod_id = p.prod_id and b.conf_codigo = p.conf_codigo) as bar_numero " +
                "from wms_produto p " +
                "inner join wms_fornecedor f " +
                "on p.forn_codigo = f.forn_codigo and p.conf_codigo = f.conf_codigo " +
                "left join wms_estoque e " +
                "on e.prod_id = p.prod_id and p.conf_codigo = e.conf_codigo " +
                "left join wms_categoria c " +
                "on c.cat_codigo = p.cat_codigo ";

                if (codProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_codigo = @codProduto and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
                }
                else if (descProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_descricao like '%" + @descProduto + "%' and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
                }
                else if (codFornecedor != string.Empty)
                {
                    select += "where prod_status = @status  and f.forn_codigo = @codFornecedor and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
                }
                else if (codBarra != string.Empty)
                {
                    select = select + "inner join wms_barra b " +
                                      "on b.prod_id = p.prod_id " +
                                      "where bar_numero = @codBarra ";
                }
                else if (descCategoria != string.Empty)  
                {
                    select += "where prod_status = @status and cat_descricao = @descCategoria and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
                }
                else 
                {
                    select += "where prod_status = @status and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
                }


                //select += "and c.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)"; //Alteracao



                //Executa o comando
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    ConsultaEstoque consultaEstoque = new ConsultaEstoque();
                    //Adiciona os dados encontrados

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        consultaEstoque.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        consultaEstoque.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        consultaEstoque.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        consultaEstoque.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        consultaEstoque.nomeFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["est_quantidade"] != DBNull.Value)
                    {
                        consultaEstoque.estoque = Convert.ToInt32(linha["est_quantidade"]);
                    }

                    if (linha["est_vendido"] != DBNull.Value)
                    {
                        consultaEstoque.reserva = Convert.ToInt32(linha["est_vendido"]);
                    }

                    if (linha["est_saldo"] != DBNull.Value)
                    {
                        consultaEstoque.saldo = Convert.ToInt32(linha["est_saldo"]);
                    }

                    if (linha["est_avaria"] != DBNull.Value)
                    {
                        consultaEstoque.avaria = Convert.ToInt32(linha["est_avaria"]);
                    }

                    if (linha["est_preco"] != DBNull.Value)
                    {
                        consultaEstoque.preco = Convert.ToInt32(linha["est_preco"]);
                    }

                    if (linha["wms"] != DBNull.Value)
                    {
                        consultaEstoque.wms = Convert.ToInt32(linha["wms"]);
                    }

                    if (linha["prod_fator_compra"] != DBNull.Value)
                    {
                        consultaEstoque.qtdCaixa = Convert.ToInt32(linha["prod_fator_compra"]);
                    }

                    if (linha["prod_fator_picking"] != DBNull.Value)
                    {
                        consultaEstoque.unidadeVenda = Convert.ToInt32(linha["prod_fator_picking"]);
                    }

                    if (linha["bar_numero"] != DBNull.Value)
                    {
                        consultaEstoque.numeroBarra = Convert.ToString(linha["bar_numero"]);
                    }

                    if (linha["prod_multiplo"] != DBNull.Value)
                    {
                        consultaEstoque.mutiploVenda = Convert.ToInt32(linha["prod_multiplo"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        consultaEstoque.categoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["prod_tipo_armazenamento"] != DBNull.Value)
                    {
                        consultaEstoque.tipoArmazenamento = Convert.ToString(linha["prod_tipo_armazenamento"]);
                    }

                    if (linha["prod_status"] != DBNull.Value)
                    {
                        consultaEstoque.status = Convert.ToBoolean(linha["prod_status"]);
                    }

                    if (linha["prod_separacao_flowrack"] != DBNull.Value)
                    {
                        consultaEstoque.flowrack = Convert.ToBoolean(linha["prod_separacao_flowrack"]);
                    }

                    if (linha["prod_audita_flowrack"] != DBNull.Value)
                    {
                        consultaEstoque.auditaFlowrack = Convert.ToBoolean(linha["prod_audita_flowrack"]);
                    }

                    if (linha["prod_palete_blocado"] != DBNull.Value)
                    {
                        consultaEstoque.paleteBlocado = Convert.ToBoolean(linha["prod_palete_blocado"]);
                    }

                    if (linha["prod_tolerancia"] != DBNull.Value)
                    {
                        consultaEstoque.tolerancia = Convert.ToInt32(linha["prod_tolerancia"]);
                    }

                    if (linha["prod_vida_util"] != DBNull.Value)
                    {
                        consultaEstoque.diasValidade = Convert.ToInt32(linha["prod_vida_util"]);
                    }

                    if (linha["prod_niv_maximo"] != DBNull.Value)
                    {
                        consultaEstoque.nivel = Convert.ToInt32(linha["prod_niv_maximo"]);
                    }

                    if (linha["prod_controla_validade"] != DBNull.Value)
                    {
                        consultaEstoque.controlaValidade = Convert.ToBoolean(linha["prod_controla_validade"]);
                    }

                    if (linha["prod_tipo_palete"] != DBNull.Value || Convert.ToString(linha["prod_tipo_palete"]) != "")
                    {
                        consultaEstoque.tamanhoPalete = Convert.ToString(linha["prod_tipo_palete"]);

                        if (Convert.ToString(linha["prod_tipo_palete"]) == "PP")
                        {
                            if (linha["prod_lastro_p"] != DBNull.Value)
                            {
                                consultaEstoque.lastro = Convert.ToInt32(linha["prod_lastro_p"]);
                                consultaEstoque.altura = Convert.ToInt32(linha["prod_altura_p"]);
                                consultaEstoque.totalPalete = Convert.ToInt32(linha["prod_lastro_p"]) * Convert.ToInt32(linha["prod_altura_p"]);
                            }
                        }
                        else if (Convert.ToString(linha["prod_tipo_palete"]) == "PM")
                        {
                            if (linha["prod_lastro_m"] != DBNull.Value)
                            {
                                consultaEstoque.lastro = Convert.ToInt32(linha["prod_lastro_m"]);
                                consultaEstoque.altura = Convert.ToInt32(linha["prod_altura_m"]);
                                consultaEstoque.totalPalete = Convert.ToInt32(linha["prod_lastro_m"]) * Convert.ToInt32(linha["prod_altura_m"]);
                            }
                        }
                        else if (Convert.ToString(linha["prod_tipo_palete"]) == "PG")
                        {
                            if (linha["prod_lastro_g"] != DBNull.Value)
                            {
                                consultaEstoque.lastro = Convert.ToInt32(linha["prod_lastro_g"]);
                                consultaEstoque.altura = Convert.ToInt32(linha["prod_altura_g"]);
                                consultaEstoque.totalPalete = Convert.ToInt32(linha["prod_lastro_g"]) * Convert.ToInt32(linha["prod_altura_g"]);
                            }
                        }
                        else if (Convert.ToString(linha["prod_tipo_palete"]) == "PB")
                        {
                            if (linha["prod_lastro_b"] != DBNull.Value)
                            {
                                consultaEstoque.lastro = Convert.ToInt32(linha["prod_lastro_b"]);
                                consultaEstoque.altura = Convert.ToInt32(linha["prod_altura_b"]);
                                consultaEstoque.totalPalete = Convert.ToInt32(linha["prod_lastro_b"]) * Convert.ToInt32(linha["prod_altura_b"]);
                            }
                        }
                    }


                    consultaEstoqueCollection.Add(consultaEstoque);
                }

                return consultaEstoqueCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto \nDetalhes: " + ex.Message);
            }
        }


        //Pesquisa o código de barra do produto
        public BarraCollection PesqCodBarra(string codProduto, string descProduto, string codFornecedor, string codBarra, string descCategoria, bool statusProduto, string empresa)
        {
            try
            {
                //Instância a coleção
                BarraCollection barraCollection = new BarraCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@descProduto", descProduto);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@descCategoria", descCategoria);
                conexao.AdicionarParamentros("@status", statusProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select b.prod_id, b.bar_codigo, b.bar_numero, b.bar_multiplicador, b.bar_peso " +
                    "from wms_barra b " +
                    "inner join wms_produto p " +
                    "on b.prod_id = p.prod_id and p.conf_codigo = b.conf_codigo " +
                    "inner join wms_fornecedor f " +
                    "on p.forn_codigo = f.forn_codigo and p.conf_codigo = f.conf_codigo " +
                    "left join wms_categoria c " +
                    "on c.cat_codigo = p.cat_codigo ";

                if (codProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_codigo = @codProduto order by p.prod_codigo";
                }
                else if (descProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_descricao like '%" + @descProduto + "%' order by p.prod_codigo";
                }
                else if (codFornecedor != string.Empty)
                {
                    select += "where prod_status = @status  and f.forn_codigo = @codFornecedor order by p.prod_codigo";
                }
                else if (codBarra != string.Empty)
                {
                    select = select + "where bar_numero = @codBarra order by p.prod_codigo";
                }
                else if (descCategoria != string.Empty)
                {
                    select += "where prod_status = @status and cat_descricao = @descCategoria " +
                        "and b.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
                }

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

                    if (linha["bar_peso"] != DBNull.Value)
                    {
                        barra.peso = Convert.ToDouble(linha["bar_peso"]);
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

        ///PESQUISA A ÁREA DE SEPARAÇÃO DO PRODUTO
        #region Antes 
        /*public EnderecoPickingCollection PesqPicking(string codProduto, string descProduto, string codFornecedor, string codBarra, string descCategoria, bool statusProduto, string empresa)
       {
           try
           {
               EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();
               //Limpa a conexão
               conexao.LimparParametros();
               //Adiciona parêmaetros
               //conexao.AdicionarParamentros("@codBarra", codBarra);
               conexao.AdicionarParamentros("@codProduto", codProduto);
               conexao.AdicionarParamentros("@descProduto", descProduto);
               conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
               conexao.AdicionarParamentros("@codBarra", codBarra);
               conexao.AdicionarParamentros("@descCategoria", descCategoria);
               conexao.AdicionarParamentros("@status", statusProduto);
               conexao.AdicionarParamentros("@empresa", empresa);

               //String de consulta
               string select = "select s.prod_id, prod_fator_pulmao, prod_peso, s.apa_codigo, apa_endereco, coalesce(s.sep_estoque, 0) as sep_estoque, u.uni_unidade, prod_fator_pulmao, " +
               "s.sep_validade, s.sep_peso, s.sep_lote, s.sep_capacidade, u1.uni_unidade, s.sep_abastecimento, u1.uni_unidade as uni_abastecimento, s.sep_tipo, s.est_codigo, " +
               "(select b.bar_numero from wms_barra b where b.bar_multiplicador = 1 and prod_id = s.prod_id ) as bar_numero " +
               "from wms_separacao s " +
               "inner join wms_apartamento a " +
               "on a.apa_codigo = s.apa_codigo " +
               "inner join wms_produto p " +
               "on s.prod_id = p.prod_id " +
               "left join wms_unidade u " +
               "on u.uni_codigo = p.uni_codigo_picking " +
               "left join wms_unidade u1 " +
               "on u1.uni_codigo = p.uni_codigo_pulmao ";

               if (codProduto != string.Empty)
               {
                   select += "where prod_status = @status and prod_codigo = @codProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by sep_tipo";
               }
               else if (descProduto != string.Empty)
               {
                   select += "where prod_status = @status and prod_descricao like '%" + @descProduto + "%' and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by sep_tipo";
               }
               else if (codFornecedor != string.Empty)
               {
                   select += "inner join wms_fornecedor f " +
                             "on p.forn_codigo = f.forn_codigo " +
                             "where prod_status = @status  and f.forn_codigo = @codFornecedor and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by sep_tipo";
               }
               else if (codBarra != string.Empty)
               {
                   select = select + "inner join wms_barra b " +
                                     "on b.prod_id = p.prod_id " +
                                     "where bar_numero = @codBarra and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by sep_tipo";
               }
               else if (descCategoria != string.Empty)
               {
                   select += "inner join wms_categoria c " +
                       "on c.cat_codigo = p.cat_codigo " +
                       "where prod_status = @status and cat_descricao = @descCategoria " +
                       "and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
               }


               //executa o select
               DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

               //Percorre a tabela e adiciona as linha encontrada na coleção
               foreach (DataRow linha in dataTable.Rows)
               {
                   //Instância o objeto
                   EnderecoPicking endereco = new EnderecoPicking();

                   //picking.codEstacao = Convert.ToInt32(linha["isn_estacao"]);
                   if (linha["prod_id"] != DBNull.Value)
                   {
                       endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                   }

                   if (linha["prod_fator_pulmao"] != DBNull.Value)
                   {
                       endereco.quantidadeCaixa = Convert.ToInt32(linha["prod_fator_pulmao"]);
                   }
                   else
                   {
                       endereco.quantidadeCaixa = 1;
                   }

                   if (linha["prod_peso"] != DBNull.Value)
                   {
                       endereco.pesoCxa = Convert.ToDouble(linha["prod_peso"]);
                   }

                   if (linha["apa_codigo"] != DBNull.Value)
                   {
                       endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                   }

                   if (linha["apa_endereco"] != DBNull.Value)
                   {
                       endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                   }

                   if (linha["sep_estoque"] != DBNull.Value)
                   {
                       endereco.estoque = Convert.ToInt32(linha["sep_estoque"]);
                   }
                   else
                   {
                       endereco.estoque = 0;
                   }

                   if (linha["uni_unidade"] != DBNull.Value)
                   {

                       endereco.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                   }

                   if (linha["sep_validade"] != DBNull.Value)
                   {
                       endereco.vencimento = Convert.ToDateTime(linha["sep_validade"]).Date;
                   }

                   if (linha["sep_peso"] != DBNull.Value)
                   {
                       endereco.peso = Convert.ToDouble(linha["sep_peso"]);
                   }

                   if (linha["sep_lote"] != DBNull.Value)
                   {
                       endereco.lote = Convert.ToString(linha["sep_lote"]);
                   }

                   if (linha["sep_capacidade"] != DBNull.Value)
                   {
                       endereco.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                   }
                   else
                   {
                       endereco.capacidade = 1;
                   }

                   if (linha["uni_abastecimento"] != DBNull.Value)
                   {
                       endereco.unidadeCapacidade = Convert.ToString(linha["uni_abastecimento"]);
                   }

                   if (linha["sep_abastecimento"] != DBNull.Value)
                   {
                       endereco.abastecimento = Convert.ToInt32(linha["sep_abastecimento"]);
                   }

                   if (linha["sep_tipo"] != DBNull.Value)
                   {
                       endereco.tipoEndereco = Convert.ToString(linha["sep_tipo"]);
                   }

                   if (linha["sep_estoque"] != DBNull.Value && linha["sep_capacidade"] != DBNull.Value && linha["prod_fator_pulmao"] != DBNull.Value)
                   {
                       if (Convert.ToInt32(endereco.estoque) < 0)
                       {
                           endereco.apStatus = "Picking Com Problema";
                       }
                         else if (Convert.ToDouble(endereco.estoque) > 0 && Convert.ToDouble(endereco.estoque) / Convert.ToInt32(endereco.quantidadeCaixa) > Convert.ToInt32(endereco.capacidade))
                         {
                             endereco.apStatus = "Picking Acima da Capacidade";
                         }
                       else if (Convert.ToDouble(endereco.estoque) > 0 && Convert.ToDouble(endereco.estoque) / Convert.ToInt32(endereco.quantidadeCaixa) < Convert.ToInt32(endereco.capacidade))
                         {
                             endereco.apStatus = "Abastecer Picking";
                         }
                       else
                       {
                           endereco.apStatus = "Picking Normal";
                       }
                   }

                   if (linha["bar_numero"] != DBNull.Value)
                   {
                       endereco.numeroBarra = Convert.ToString(linha["bar_numero"]);
                   }

                   if (linha["est_codigo"] != DBNull.Value)
                   {
                       endereco.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                   }

                   //Adiciona os dados na coleção
                   enderecoCollection.Add(endereco);

               }

               return enderecoCollection;
           }
           catch (Exception ex)
           {
               throw new Exception("Ocorreu um erro ao pesquisar o picking do produto \nDetalhes: " + ex.Message);
           }
       }*/
        #endregion
        ///PESQUISA A ÁREA DE SEPARAÇÃO DO PRODUTO
        public EnderecoPickingCollection PesqPicking(string codProduto, string descProduto, string codFornecedor, string codBarra, string descCategoria, bool statusProduto, string empresa)
        {
            try
            {
                EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@descProduto", descProduto);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@descCategoria", descCategoria);
                conexao.AdicionarParamentros("@status", statusProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select s.prod_id, prod_fator_pulmao, prod_peso, s.apa_codigo, apa_endereco, coalesce(s.sep_estoque, 0) as sep_estoque, u.uni_unidade, prod_fator_pulmao, " +
                "s.sep_validade, s.sep_peso, s.sep_lote, s.sep_capacidade, u1.uni_unidade, s.sep_abastecimento, u1.uni_unidade as uni_abastecimento, s.sep_tipo, s.est_codigo, " +
                "(select b.bar_numero from wms_barra b where b.bar_multiplicador = 1 and prod_id = s.prod_id ) as bar_numero," +
                "(select iconf_status from wms_itens_configuracao c where iconf_descricao = 'CONTROLA PICKING POR CAIXA FECHADA' ) as picking_caixa " +
                "from wms_separacao s " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "inner join wms_produto p " +
                "on s.prod_id = p.prod_id " +
                "left join wms_unidade u " +
                "on u.uni_codigo = p.uni_codigo_picking " +
                "left join wms_unidade u1 " +
                "on u1.uni_codigo = p.uni_codigo_pulmao ";

                if (codProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_codigo = @codProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by sep_tipo";
                }
                else if (descProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_descricao like '%" + @descProduto + "%' and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by sep_tipo";
                }
                else if (codFornecedor != string.Empty)
                {
                    select += "inner join wms_fornecedor f " +
                              "on p.forn_codigo = f.forn_codigo " +
                              "where prod_status = @status  and f.forn_codigo = @codFornecedor and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by sep_tipo";
                }
                else if (codBarra != string.Empty)
                {
                    select = select + "inner join wms_barra b " +
                                      "on b.prod_id = p.prod_id " +
                                      "where bar_numero = @codBarra and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by sep_tipo";
                }
                else if (descCategoria != string.Empty)
                {
                    select += "inner join wms_categoria c " +
                        "on c.cat_codigo = p.cat_codigo " +
                        "where prod_status = @status and cat_descricao = @descCategoria " +
                        "and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by p.prod_codigo";
                }


                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    EnderecoPicking endereco = new EnderecoPicking();

                    //picking.codEstacao = Convert.ToInt32(linha["isn_estacao"]);
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        endereco.quantidadeCaixa = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        endereco.quantidadeCaixa = 1;
                    }

                    if (linha["prod_peso"] != DBNull.Value)
                    {
                        endereco.pesoCxa = Convert.ToDouble(linha["prod_peso"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToInt32(linha["sep_estoque"]);

                        if (linha["picking_caixa"] != DBNull.Value)
                        {
                            if (Convert.ToString(linha["picking_caixa"]).Equals("True"))
                            {
                                endereco.estoque = Convert.ToInt32(linha["sep_estoque"]) / endereco.quantidadeCaixa;
                            }
                        }
                    }
                    else
                    {
                        endereco.estoque = 0;
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {

                        endereco.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToDateTime(linha["sep_validade"]).Date;
                    }

                    if (linha["sep_peso"] != DBNull.Value)
                    {
                        endereco.peso = Convert.ToDouble(linha["sep_peso"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    if (linha["sep_capacidade"] != DBNull.Value)
                    {
                        endereco.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                    }
                    else
                    {
                        endereco.capacidade = 1;
                    }

                    if (linha["uni_abastecimento"] != DBNull.Value)
                    {
                        endereco.unidadeCapacidade = Convert.ToString(linha["uni_abastecimento"]);
                    }

                    if (linha["sep_abastecimento"] != DBNull.Value)
                    {
                        endereco.abastecimento = Convert.ToInt32(linha["sep_abastecimento"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        endereco.tipoEndereco = Convert.ToString(linha["sep_tipo"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value && linha["sep_capacidade"] != DBNull.Value && linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(endereco.estoque) < 0)
                        {
                            endereco.apStatus = "Picking Com Problema";
                        }
                        else if (Convert.ToDouble(endereco.estoque) > 0 && Convert.ToDouble(endereco.estoque) / Convert.ToInt32(endereco.quantidadeCaixa) > Convert.ToInt32(endereco.capacidade))
                        {
                            endereco.apStatus = "Picking Acima da Capacidade";
                        }
                        else if (Convert.ToDouble(endereco.estoque) > 0 && Convert.ToDouble(endereco.estoque) / Convert.ToInt32(endereco.quantidadeCaixa) < Convert.ToInt32(endereco.capacidade))
                        {
                            endereco.apStatus = "Abastecer Picking";
                        }
                        else
                        {
                            endereco.apStatus = "Picking Normal";
                        }
                    }

                    if (linha["bar_numero"] != DBNull.Value)
                    {
                        endereco.numeroBarra = Convert.ToString(linha["bar_numero"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        endereco.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    //Adiciona os dados na coleção
                    enderecoCollection.Add(endereco);

                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o picking do produto \nDetalhes: " + ex.Message);
            }
        }
        //Edição de estoque no Pulmão
        public EnderecoPulmaoCollection PesqPulmao(string codProduto, string descProduto, string codFornecedor, string codBarra, string descCategoria, bool statusProduto, string empresa)
        {
            try
            {
                EnderecoPulmaoCollection enderecoCollection = new EnderecoPulmaoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                //conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@descProduto", descProduto);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@codBarra", codBarra);
                conexao.AdicionarParamentros("@descCategoria", descCategoria);
                conexao.AdicionarParamentros("@status", statusProduto);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select distinct(e.apa_codigo), e.prod_id, a.apa_endereco, e.arm_quantidade, p.prod_peso, p.prod_fator_pulmao, " +
                                "(select trunc(i.iaba_qtd_abastecer/p.prod_fator_pulmao) from wms_item_abastecimento i where iaba_status = 'PENDENTE' and i.apa_codigo_pulmao = e.apa_codigo and i.prod_id = e.prod_id ) as res_abastecimento, " +
                                " r.res_quantidade, u.uni_unidade, r.res_tipo, " +
                                "e.arm_peso, e.arm_vencimento, e.arm_data_armazenagem, e.arm_lote, arm_bloqueado, arm_motivo_bloqueio, " +
                                "current_date + cast(p.prod_vida_util - (prod_vida_util * cast(p.prod_tolerancia as double precision) / 100) as integer) as maxima_tolerancia, apa_ordem, " +
                                "current_date + 30 as minima_tolerancia, e.not_nota_cega, p.prod_lastro_m, p.prod_altura_m " +
                                "from wms_armazenagem e " +
                                "inner join wms_apartamento a " +
                                "on e.apa_codigo = a.apa_codigo " +
                                "join wms_produto p " +
                                "on p.prod_id = e.prod_id " +
                                "left outer join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_reserva r " +
                                "on r.apa_codigo = e.apa_codigo ";

                if (codProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_codigo = @codProduto order by arm_vencimento, apa_ordem";
                }
                else if (descProduto != string.Empty)
                {
                    select += "where prod_status = @status and prod_descricao like '%" + @descProduto + "%' order by arm_vencimento, apa_ordem";
                }
                else if (codFornecedor != string.Empty)
                {
                    select += "inner join wms_fornecedor f " +
                              "on p.forn_codigo = f.forn_codigo " +
                              "where prod_status = @status  and f.forn_codigo = @codFornecedor order by arm_vencimento, apa_ordem";
                }
                else if (codBarra != string.Empty)
                {
                    select = select + "inner join wms_barra b " +
                                      "on b.prod_id = p.prod_id " +
                                      "where bar_numero = @codBarra order by arm_vencimento, apa_ordem";
                }
                else if (descCategoria != string.Empty)
                {
                    select += "inner join wms_categoria c " +
                        "on c.cat_codigo = p.cat_codigo " +
                        "where prod_status = @status and cat_descricao = @descCategoria " +
                        "and e.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)  order by p.prod_codigo";
                }

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre o datatable
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    EnderecoPulmao endereco = new EnderecoPulmao();
                    //Adiciona os dados encontrados  
                    if (linha["arm_data_armazenagem"] != DBNull.Value)
                    {
                        endereco.dataEntrada = Convert.ToDateTime(linha["arm_data_armazenagem"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        endereco.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        endereco.fatorPulmao = 1;
                    }

                    if (linha["prod_peso"] != DBNull.Value)
                    {
                        endereco.pesoCxa = Convert.ToDouble(linha["prod_peso"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento1 = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.descEndereco1 = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["arm_quantidade"] != DBNull.Value)
                    {
                        endereco.qtdCaixaOrigem = Convert.ToInt32(linha["arm_quantidade"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        endereco.tipoApartamento1 = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["arm_peso"] != DBNull.Value)
                    {
                        endereco.pesoProduto1 = Convert.ToDouble(linha["arm_peso"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        endereco.vencimentoProduto1 = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        endereco.loteProduto1 = Convert.ToString(linha["arm_lote"]);
                    }

                    if (linha["arm_bloqueado"] != DBNull.Value)
                    {
                        if (Convert.ToString(linha["arm_bloqueado"]).Equals("True"))
                        {
                            endereco.bloqueado = "SIM";
                        }

                        if (Convert.ToString(linha["arm_bloqueado"]).Equals("False"))
                        {
                            endereco.bloqueado = "";
                        }
                    }

                    if (linha["res_quantidade"] != DBNull.Value)
                    {
                        endereco.reserva = Convert.ToString(linha["res_quantidade"]);
                    }

                    if (linha["res_abastecimento"] != DBNull.Value)
                    {
                        endereco.reserva = Convert.ToString(linha["res_abastecimento"]);
                    }

                    if (linha["arm_motivo_bloqueio"] != DBNull.Value)
                    {
                        endereco.motivoBloqueio = Convert.ToString(linha["arm_motivo_bloqueio"]);
                    }

                    if (linha["res_tipo"] != DBNull.Value)
                    {
                        endereco.motivoBloqueio = Convert.ToString(linha["res_tipo"]);
                    }

                    if (linha["minima_tolerancia"] != DBNull.Value)
                    {
                        endereco.dataMimTolerancia = Convert.ToDateTime(linha["minima_tolerancia"]);
                    }

                    if (linha["maxima_tolerancia"] != DBNull.Value)
                    {
                        endereco.dataMaxTolerancia = Convert.ToDateTime(linha["maxima_tolerancia"]);
                    }

                    if (linha["not_nota_cega"] != DBNull.Value)
                    {
                        endereco.notaCega = Convert.ToInt32(linha["not_nota_cega"]);
                    }

                    if (linha["prod_lastro_m"] != DBNull.Value)
                    {
                        endereco.lastroM = Convert.ToInt32(linha["prod_lastro_m"]);
                    }

                    if (linha["prod_altura_m"] != DBNull.Value)
                    {
                        endereco.alturaM = Convert.ToInt32(linha["prod_altura_m"]);
                    }

                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os endereços no pulmão \nDetalhes: " + ex.Message);
            }
        }

        //Edição de estoque no Pulmão
        public void EdicaoEstoquePulmao(int codUsuario, int idProduto, int codApartamento, int estoque, DateTime validade, string lote, double peso, string empresa)
        {
            try
            {
                string sqlcommant = string.Empty;
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros               
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@estoque", estoque);
                conexao.AdicionarParamentros("@validade", validade);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de atualização
                string update = "update wms_armazenagem a set a.arm_quantidade = @estoque, a.arm_vencimento = @validade, " +
                                "a.arm_peso = @peso, a.arm_lote = @lote, usu_codigo_edicao = @codUsuario, arm_data_edicao = current_timestamp " +
                                "where prod_id = @idProduto and apa_codigo = @codApartamento and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);
                //Verificar se existe o lote do produto
                WmsLote(idProduto,estoque,lote,empresa);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao editar o estoque do produto. \nDetalhes:" + ex.Message);
            }
        }

        //Edição de estoque no Picking
        public void EdicaoEstoquePicking(int codUsuario, int idProduto, int codApartamento, int estoque, DateTime validade, int capacidade, int abastecimento, string lote, double peso, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros               
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@estoque", estoque);
                conexao.AdicionarParamentros("@validade", validade);
                conexao.AdicionarParamentros("@capacidade", capacidade);
                conexao.AdicionarParamentros("@abastecimento", abastecimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de atualização
                string update = "update wms_separacao s set s.sep_estoque = @estoque, s.sep_validade = @validade, " +
                                "s.sep_peso = @peso, s.sep_lote = @lote, s.sep_capacidade = @capacidade, s.sep_abastecimento = @abastecimento, " +
                                "usu_codigo_edicao = @codUsuario, sep_data_edicao = current_timestamp " +
                                "where prod_id = @idProduto and apa_codigo = @codApartamento and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);
                //Verificar se existe o lote do produto
                WmsLote(idProduto, estoque, lote, empresa);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao editar o estoque do produto. \nDetalhes:" + ex.Message);
            }
        }
        //Update no lote
        public void WmsLote(int idProduto, int estoque, string lote, string empresa)
        {
            try
            {
                string sqlcommant = string.Empty;
                //Verificar se existe o lote do produto
                sqlcommant = $"select first 1 lote_codigo from wms_lote where lower(lote_numero) = '{lote.ToLower()}'  " +
                             $"and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = '{empresa}') ";
                int lotecodigo = Convert.ToInt32(conexao.ExecuteManipulacao(CommandType.Text, sqlcommant));
                //se
                if (lotecodigo == 0)
                {
                    sqlcommant = string.Empty;
                    sqlcommant = $"select conf_codigo from wms_configuracao where conf_sigla = '{empresa}' "; 
                    int confcodigo = Convert.ToInt32(conexao.ExecuteManipulacao(CommandType.Text, sqlcommant));
                    //Verificar ser o codigo da empresa existe
                    if (confcodigo > 0)
                    {
                        string sql = "insert into wms_lote (prod_id,lote_numero,lote_quantidade,lote_data,conf_codigo) " +
                                      "values " +
                                      $"({idProduto},'{lote}',{estoque},'{DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")}',{confcodigo})";
                        //Executa o script no banco
                        conexao.ExecuteManipulacao(CommandType.Text, sql);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao editar o estoque do produto. \nDetalhes:" + ex.Message);
            }
        }
        //Bloqueia o estoque do endereço selecionado
        public void BloqueiaEstoque(int codUsuario, int idProduto, int codApartamento, string descMotivo, string bloqueia)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@descMotivo", descMotivo);
                conexao.AdicionarParamentros("@bloqueia", bloqueia);
                

                //String de atualização
                string update = "update wms_armazenagem a set a.arm_bloqueado = @bloqueia, a.arm_data_bloqueio = current_timestamp, " +
                "a.usu_codigo_bloqueou = @codUsuario, a.arm_motivo_bloqueio = @descMotivo " +
                "where prod_id = @idProduto and apa_codigo = @codApartamento" +
                "and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao bloquear o estoque do produto. \nDetalhes:" + ex.Message);
            }
        }

        //Ecluir estoque no pulmao
        public void DeletarEstoquePulmao(int idProduto, int codApartamento)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros               
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codApartamento", codApartamento);

                //String de atualização
                string update = "delete from wms_armazenagem where prod_id = @idProduto and apa_codigo = @codApartamento";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao editar o estoque do produto. \nDetalhes:" + ex.Message);
            }
        }

        //Ecluir estoque no pulmao
        public void InserirRastreamento(int idProduto, int codApartamento, int quantidade, DateTime vencimento, double peso, string lote, int codUsuario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                //conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@quantidade", quantidade);
                conexao.AdicionarParamentros("@vencimento", vencimento);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                

                //String de insert - insere o endereço  
                string insert = "insert into wms_rastreamento_armazenagem (rast_codigo, rast_operacao, rast_data, usu_codigo," +
                " apa_codigo_origem, apa_codigo_destino, prod_id, arm_quantidade_origem, arm_quantidade_destino, arm_peso_origem, arm_peso_destino," +
                "arm_vencimento_origem, arm_vencimento_destino, arm_lote_origem, arm_lote_destino, conf_codigo)" +
                "values" +
                "(gen_id(gen_wms_rast_armazenagem, 1), 'EXCLUSÃO', current_timestamp, @codUsuario, " +
                "@codApartamento, null, @idProduto, @quantidade, null, @peso, null," +
                "@vencimento, null, @lote, null)";

                //String de atualização
                string updateApartamento = "update wms_apartamento set apa_status = 'Vago' where apa_codigo = @codApartamento";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updateApartamento);

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao editar o estoque do produto. \nDetalhes:" + ex.Message);
            }
        }


        //Relatório

        ///PESQUISA INFORMAÇÕES SOBRE O PRODUTO
        public RelProdutoCollection PesqRelatorioProduto(string empresa, int idProduto)
        {
            try
            {
                //Instância a camada de objêto
                RelProdutoCollection produtoCollection = new RelProdutoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@idProduto", idProduto);                

                //String de consulta
                string select = "select distinct(p.prod_id), f.forn_codigo, f.forn_nome, prod_codigo, p.prod_descricao, " +
                                "coalesce((select sum(sep_estoque) from wms_separacao where prod_id = @idProduto), 0) + " +
                                "coalesce((select sum(arm_quantidade) from wms_armazenagem where prod_id = @idProduto), 0) as estoque," +
                                "uni_unidade, " +
                                "(select conf_empresa from wms_configuracao where conf_sigla = @empresa) as empresa " +
                                "from wms_produto p " +
                                "inner join wms_fornecedor f " +
                                "on p.forn_codigo = f.forn_codigo and p.conf_codigo = f.conf_codigo " +
                                "left join wms_separacao s " +
                                "on p.prod_id = s.prod_id and p.conf_codigo = s.conf_codigo " +
                                 "left join wms_armazenagem a " +
                                "on p.prod_id = s.prod_id and p.conf_codigo = a.conf_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "where p.prod_id = @idProduto and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    RelProduto prod = new RelProduto();

                    //Adiciona os dados                    
                    if (linha["empresa"] != DBNull.Value)
                    {
                        prod.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        prod.nomeFornecedor = Convert.ToString(linha["forn_codigo"]) + " - " + Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        prod.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        prod.descProduto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);

                    }

                    if (linha["estoque"] != DBNull.Value)
                    {
                        prod.estoque = Convert.ToInt32(linha["estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        prod.unidade = Convert.ToString(linha["uni_unidade"]);
                    }

                    //Adiciona os dados na coleção
                    produtoCollection.Add(prod);

                }

                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório de endereço do produto \nDetalhes: " + ex.Message);
            }
        }


        ///PESQUISA A ÁREA DE SEPARAÇÃO DO PRODUTO
        public RelEnderecoProdutoCollection PesqRelatorioPicking(string empresa, int idProduto)
        {
            try
            {
                RelEnderecoProdutoCollection enderecoCollection = new RelEnderecoProdutoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@idProduto", idProduto);

                //String de consulta
                string select = "select ap.apa_codigo, ap.apa_endereco, p.prod_id, p.prod_codigo, p.prod_descricao, coalesce(sep_estoque, 0) as sep_estoque, " +
                                "coalesce(trunc(s.sep_estoque / p.prod_fator_pulmao),0) as qtd_pulmao , u.uni_unidade as uni_pulmao, " +
                                "coalesce(mod(s.sep_estoque, p.prod_fator_pulmao), 0) as qtd_picking, uu.uni_unidade as uni_picking, " +
                                "s.sep_validade , s.sep_lote, s.sep_peso, sep_tipo, sep_bloqueado, sep_data_bloqueio, prod_peso_variavel " +
                                "from wms_separacao s " +
                                "inner join wms_produto p " +
                                "on s.prod_id = p.prod_id and p.conf_codigo = s.conf_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade uu " +
                                "on uu.uni_codigo = p.uni_codigo_picking " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = s.apa_codigo " +
                                "where p.prod_id = @idProduto and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "order by s.sep_tipo";


                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    RelEnderecoProduto endereco = new RelEnderecoProduto();

                    //Adiciona os dados                    
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["qtd_pulmao"] != DBNull.Value)
                    {
                        endereco.estoqueCaixa = Convert.ToInt32(linha["qtd_pulmao"]);
                    }

                    if (linha["qtd_picking"] != DBNull.Value)
                    {
                        endereco.estoqueUnidade = Convert.ToInt32(linha["qtd_picking"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {

                        endereco.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {

                        endereco.unidadePicking = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToDateTime(linha["sep_validade"]).Date;
                    }

                    if (linha["sep_peso"] != DBNull.Value)
                    {
                        endereco.peso = Convert.ToDouble(linha["sep_peso"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        endereco.tipoEndereco = Convert.ToString(linha["sep_tipo"]);
                    }

                    if (linha["sep_bloqueado"] != DBNull.Value)
                    {
                        if (Convert.ToString(linha["sep_bloqueado"]).Equals("True"))
                        {
                            endereco.bloqueio = "SIM";
                        }

                        if (Convert.ToString(linha["sep_bloqueado"]).Equals("False"))
                        {
                            endereco.bloqueio = "";
                        }
                    }

                    if (linha["sep_data_bloqueio"] != DBNull.Value)
                    {
                        endereco.dataBloqueio = string.Format("{0:d}", linha["sep_data_bloqueio"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        endereco.pesoVariavel = Convert.ToString(linha["prod_peso_variavel"]);
                    }
                    else
                    {
                        endereco.pesoVariavel = "False";
                    }




                    //Adiciona os dados na coleção
                    enderecoCollection.Add(endereco);

                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o relatório de picking do produto \nDetalhes: " + ex.Message);
            }
        }

        //Edição de estoque no Pulmão
        public RelEnderecoProdutoCollection PesqRelatorioPulmao(string empresa, int idProduto)
        {
            try
            {
                RelEnderecoProdutoCollection enderecoCollection = new RelEnderecoProdutoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@idProduto", idProduto);

                //String de consulta
                string select = "select a.apa_codigo, ap.apa_endereco, p.prod_id, arm_quantidade, " +
                                "trunc(arm_quantidade / p.prod_fator_pulmao) as qtd_pulmao , u.uni_unidade as uni_pulmao, " +
                                "mod(arm_quantidade, p.prod_fator_pulmao) as qtd_picking, uu.uni_unidade as uni_picking, " +
                                "arm_reserva, a.arm_vencimento, a.arm_lote, (arm_quantidade * a.arm_peso) as arm_peso, a.arm_bloqueado, a.arm_motivo_bloqueio, prod_peso_variavel " +
                                "from wms_armazenagem a " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = a.apa_codigo and ap.conf_codigo = a.conf_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = a.prod_id and p.conf_codigo = a.conf_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade uu " +
                                "on uu.uni_codigo = p.uni_codigo_picking " +
                                "where p.prod_id = @idProduto and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "order by ap.apa_ordem";


                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre o datatable
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    RelEnderecoProduto endereco = new RelEnderecoProduto();

                    //Adiciona os dados encontrados  
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codEndereco = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["arm_quantidade"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToDouble(linha["arm_quantidade"]);
                    }

                    if (linha["qtd_pulmao"] != DBNull.Value)
                    {
                        endereco.estoqueCaixa = Convert.ToDouble(linha["qtd_pulmao"]);
                    }

                    if (linha["qtd_picking"] != DBNull.Value)
                    {
                        endereco.estoqueUnidade = Convert.ToDouble(linha["qtd_picking"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        endereco.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        endereco.unidadePicking = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["arm_peso"] != DBNull.Value)
                    {
                        endereco.peso = Convert.ToDouble(linha["arm_peso"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["arm_lote"]);
                    }

                    if (linha["arm_reserva"] != DBNull.Value)
                    {
                        if (Convert.ToString(linha["arm_reserva"]).Equals("True"))
                        {
                            endereco.estoqueReservado = "SIM";
                        }

                        if (Convert.ToString(linha["arm_bloqueado"]).Equals("False"))
                        {
                            endereco.estoqueReservado = "";
                        }
                    }

                    if (linha["arm_bloqueado"] != DBNull.Value)
                    {
                        if (Convert.ToString(linha["arm_bloqueado"]).Equals("True"))
                        {
                            endereco.bloqueio = "SIM";
                        }

                        if (Convert.ToString(linha["arm_bloqueado"]).Equals("False"))
                        {
                            endereco.bloqueio = "";
                        }
                    }

                    if (linha["arm_motivo_bloqueio"] != DBNull.Value)
                    {
                        endereco.motivoBloqueio = Convert.ToString(linha["arm_motivo_bloqueio"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        endereco.pesoVariavel = Convert.ToString(linha["prod_peso_variavel"]);
                    }
                    else
                    {
                        endereco.pesoVariavel = "False";
                    }

                    enderecoCollection.Add(endereco);

                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os endereços no pulmão \nDetalhes: " + ex.Message);
            }
        }

        //Registra no rastreamento
        public void InserirRastreamento(string operacao, int codUsuario, int idProduto, int codApartamentoOrigem, int quantidadeOrigem, string vencimentoOrigem, double pesoOrigem, string loteOrigem,
            int codApartamentoDestino, int quantidadeDestino, string vencimentoDestino, double pesoDestino, string loteDestino)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@operacao", operacao);
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

                //String de insert - insere o endereço  
                string insert = "insert into wms_rastreamento_armazenagem (rast_codigo, rast_operacao, rast_data, usu_codigo," +
                " apa_codigo_origem, apa_codigo_destino, prod_id, arm_quantidade_origem, arm_quantidade_destino, arm_peso_origem, arm_peso_destino," +
                "arm_vencimento_origem, arm_vencimento_destino, arm_lote_origem, arm_lote_destino)" +
                "values" +
                "(gen_id(gen_wms_rast_armazenagem, 1), @operacao, current_timestamp, @codUsuario, " +
                "@codApartamentoOrigem, @codApartamentoDestino, @idProduto, @quantidadeOrigem, @quantidadeDestino, @pesoOrigem, @pesoDestino," +
                "@vencimentoOrigem, @vencimentoDestino, @loteOrigem, @loteDestino)";


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
