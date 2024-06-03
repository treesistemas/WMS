using System;
using Dados;
using ObjetoTransferencia;
using ObjetoTransferencia.Impressao;
using System.Data;
using System.Reflection;

namespace Negocios
{
    public class NotaEntradaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();
        //Pesquisa de nota e espelho
        public NotaEntradaCollection PesqNotaEntrada(string nota, string notaCega, string codFornecedor, string dataInicial, string dataFinal, bool comNotaCega, bool semNotaCega, bool conferido, bool naoConferido, string empresa)
        {
            try
            {
                //Instância a coleção
                NotaEntradaCollection gerarNotaCegaCollection = new NotaEntradaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@nota", nota);
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select not_data_entrada, not_numero, f.forn_codigo, f.forn_nome, n.not_peso, " +
                                "not_numero_cega, not_data_cega, n.not_conf_inicial, n.not_conf_final, u.usu_login as conferente, unc.usu_login as usuario_gerou, " +
                                "not_liberar_estoque_fim, not_liberar_armaz_fim, not_armaz_com_picking, not_exigir_validade, not_conf_relatorio, not_cross_docking " +
                                "from wms_nota_entrada n " +
                                "join wms_fornecedor f " +
                                "on f.forn_codigo = n.forn_codigo and f.conf_codigo = n.conf_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = n.usu_cod_conf "+
                                "left join wms_usuario unc "+
                                "on unc.usu_codigo = n.usu_cod_gerou_cega";

                if (!nota.Equals(""))
                {
                    select = select + " where not_numero = @nota and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }
                else if (!notaCega.Equals(""))
                {
                    select = select + " where not_numero_cega = @notaCega and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }
                else if (nota.Equals("") && notaCega.Equals(""))
                {
                    select = select + " where n.not_data_entrada between @dataInicial and @dataFinal and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                    if (!codFornecedor.Equals(""))
                    {
                        select = select + " and n.forn_codigo = @codFornecedor and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                    }
                }

                if (comNotaCega == true)
                {
                    select = select + " and not_numero_cega is not null";
                }

                if (semNotaCega == true)
                {
                    select = select + " and not_numero_cega is null";
                }

                if (conferido == true)
                {
                    select = select + " and not_conf_final is not null";
                }

                if (naoConferido == true)
                {
                    select = select + " and not_conf_final is null";
                }

                //Ordenação
                select = select + " order by not_data_entrada, not_numero_cega, not_numero, forn_codigo ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    NotaEntrada notaEntrada = new NotaEntrada();
                    //Adiciona os valores encontrados
                    if (linha["not_data_entrada"] != DBNull.Value)
                    {
                        notaEntrada.dataNota = Convert.ToDateTime(linha["not_data_entrada"]);
                    }

                    if (linha["not_numero"] != DBNull.Value)
                    {
                        notaEntrada.nota = Convert.ToInt32(linha["not_numero"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        notaEntrada.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        notaEntrada.nmFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["not_peso"] != DBNull.Value)
                    {
                        notaEntrada.pesoNota = Convert.ToDouble(linha["not_peso"]);
                    }

                    if (linha["not_numero_cega"] != DBNull.Value)
                    {
                        notaEntrada.codNotaCega = Convert.ToInt32(linha["not_numero_cega"]);
                    }

                    if (linha["not_data_cega"] != DBNull.Value)
                    {
                        notaEntrada.dataNotaCega = Convert.ToDateTime(linha["not_data_cega"]);
                    }

                    if (linha["usuario_gerou"] != DBNull.Value)
                    {
                        notaEntrada.usuarioNotaCega = Convert.ToString(linha["usuario_gerou"]);
                    }

                    if (linha["not_conf_inicial"] != DBNull.Value)
                    {
                        notaEntrada.inicioConferencia = Convert.ToDateTime(linha["not_conf_inicial"]);
                    }

                    if (linha["not_conf_inicial"] != DBNull.Value && linha["not_conf_final"] != DBNull.Value)
                    {
                        notaEntrada.fimConferencia = Convert.ToDateTime(linha["not_conf_final"]);

                         TimeSpan r = Convert.ToDateTime(linha["not_conf_final"]).Subtract(Convert.ToDateTime(linha["not_conf_inicial"]));

                        string tempo = r.ToString(@"d\.hh\:mm\:ss");

                        notaEntrada.tempoConferencia = tempo;
                    }

                    if (linha["conferente"] != DBNull.Value)
                    {
                        notaEntrada.conferente = Convert.ToString(linha["conferente"]);
                    }

                    if (linha["not_liberar_estoque_fim"] != DBNull.Value)
                    {
                        notaEntrada.liberarEstoque = Convert.ToBoolean(linha["not_liberar_estoque_fim"]);
                    }

                    if (linha["not_liberar_armaz_fim"] != DBNull.Value)
                    {
                        notaEntrada.liberarArmazenagem = Convert.ToBoolean(linha["not_liberar_armaz_fim"]);
                    }

                    if (linha["not_armaz_com_picking"] != DBNull.Value)
                    {
                        notaEntrada.armzenagemPicking = Convert.ToBoolean(linha["not_armaz_com_picking"]);
                    }

                    if (linha["not_exigir_validade"] != DBNull.Value)
                    {
                        notaEntrada.exigirValidade = Convert.ToBoolean(linha["not_exigir_validade"]);
                    }

                    if (linha["not_conf_relatorio"] != DBNull.Value)
                    {
                        notaEntrada.tipoRelatorio = Convert.ToBoolean(linha["not_conf_relatorio"]);
                    }

                    if (linha["not_cross_docking"] != DBNull.Value)
                    {
                        notaEntrada.crossDocking = Convert.ToBoolean(linha["not_cross_docking"]);
                    }

                    gerarNotaCegaCollection.Add(notaEntrada);
                }
                //Retorna a coleção de cadastro encontrada
                return gerarNotaCegaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a notas de entrada! \nDetalhes:" + ex.Message);
            }

        }
        //Pesquisa pelo fornecedor e data

        //Pesquisa o itens da nota de entrada
        public ItensNotaEntradaCollection PesqItensNota(string nota, string notaCega, string codFornecedor, string dataInicial, string dataFinal, bool comNotaCega, bool semNotaCega, bool conferido, bool naoConferido, string empresa)
        {
            try
            {
                //Instância a coleção
                ItensNotaEntradaCollection itensNotaEntradaCollection = new ItensNotaEntradaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@nota", nota);
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select not_numero, prod_codigo, prod_descricao, i.inot_quantidade from wms_itens_nota i " +
                                "inner join wms_nota_entrada n "+
                                "on n.not_codigo = i.not_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id  and p.conf_codigo = i.conf_codigo " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = n.forn_codigo and f.conf_codigo = n.conf_codigo ";



                if (!nota.Equals(""))
                {
                    select = select + " where not_numero = @nota and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }
                else if (!notaCega.Equals(""))
                {
                    select = select + " where not_numero_cega = @notaCega and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }
                else if (nota.Equals("") && notaCega.Equals(""))
                {
                    select = select + " where n.not_data_entrada between @dataInicial and @dataFinal " +
                                        "and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                    if (!codFornecedor.Equals(""))
                    {
                        select = select + " and n.forn_codigo = @codFornecedor and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                    }
                }

                if (comNotaCega == true)
                {
                    select = select + " and not_numero_cega is not null";
                }

                if (semNotaCega == true)
                {
                    select = select + " and not_numero_cega is null";
                }

                if (conferido == true)
                {
                    select = select + " and not_conf_final is not null";
                }

                if (naoConferido == true)
                {
                    select = select + " and not_conf_final is null";
                }

                //Ordenação
                select = select + " order by not_numero_cega, not_numero, n.forn_codigo ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ItensNotaEntrada itensNotaEntrada = new ItensNotaEntrada();
                    //Adiciona os valores encontrado
                    if (linha["not_numero"] != DBNull.Value)
                    {
                        itensNotaEntrada.codNota = Convert.ToInt32(linha["not_numero"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensNotaEntrada.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensNotaEntrada.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["inot_quantidade"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeNota = Convert.ToInt32(linha["inot_quantidade"]);
                    }

                    itensNotaEntradaCollection.Add(itensNotaEntrada);
                }
                //Retorna a coleção de cadastro encontrada
                return itensNotaEntradaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens da nota de entrada! \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o itens da nota de entrada
        public ItensNotaEntradaCollection PesqItensNotaCega(string nota, string notaCega, string codFornecedor, string dataInicial, string dataFinal, bool comNotaCega, bool semNotaCega, bool conferido, bool naoConferido, string empresa)
        {
            try
            {
                //Instância a coleção
                ItensNotaEntradaCollection itensNotaEntradaCollection = new ItensNotaEntradaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@nota", nota);
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@codFornecedor", codFornecedor);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select not_numero_cega, prod_codigo, prod_descricao, sum(i.inot_quantidade) as quantidade, i.inot_quantidade_conf, " +
                                "inot_falta, inot_avaria, inot_lote, inot_validade, sum(i.inot_quantidade * p.prod_peso) as peso from wms_itens_nota i " +
                                "inner join wms_nota_entrada n " +
                                "on n.not_codigo = i.not_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "join wms_fornecedor f " +
                                "on f.forn_codigo = n.forn_codigo ";

                if (!nota.Equals(""))
                {
                    select = select + " where not_numero = @nota ";
                }
                else if (!notaCega.Equals(""))
                {
                    select = select + " where not_numero_cega = @notaCega ";
                }
                else if (nota.Equals("") && notaCega.Equals(""))
                {
                    select = select + " where n.not_data_entrada between @dataInicial and @dataFinal and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                    if (!codFornecedor.Equals(""))
                    {
                        select = select + " and n.forn_codigo = @codFornecedor";
                    }
                }

                if (comNotaCega == true)
                {
                    select = select + " and not_numero_cega is not null";
                }

                if (semNotaCega == true)
                {
                    select = select + " and not_numero_cega is null";
                }

                if (conferido == true)
                {
                    select = select + " and not_conf_final is not null";
                }

                if (naoConferido == true)
                {
                    select = select + " and not_conf_final is null";
                }

                //Ordenação
                select = select + " group by not_numero_cega, prod_codigo, prod_descricao, i.inot_data_conferencia, i.inot_quantidade_conf, " +
                                    "inot_falta, inot_avaria, inot_lote, inot_validade";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ItensNotaEntrada itensNotaEntrada = new ItensNotaEntrada();
                    //Adiciona os valores encontrado
                    if (linha["not_numero_cega"] != DBNull.Value)
                    {
                        itensNotaEntrada.codNotaCega = Convert.ToInt32(linha["not_numero_cega"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensNotaEntrada.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensNotaEntrada.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["quantidade"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeNota = Convert.ToInt32(linha["quantidade"]);
                    }

                    if (linha["inot_quantidade_conf"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeConferida = Convert.ToInt32(linha["inot_quantidade_conf"]);
                    }

                    if (linha["inot_avaria"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeAvariada = Convert.ToInt32(linha["inot_avaria"]);
                    }

                    if (linha["inot_falta"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeFalta = Convert.ToInt32(linha["inot_falta"]);
                    }

                    if (linha["inot_lote"] != DBNull.Value)
                    {
                        itensNotaEntrada.loteProduto = Convert.ToString(linha["inot_lote"]);
                    }

                    if (linha["inot_validade"] != DBNull.Value)
                    {
                        itensNotaEntrada.validadeProduto = Convert.ToDateTime(linha["inot_validade"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        itensNotaEntrada.pesoProduto = Convert.ToDouble(linha["peso"]);
                    }

                    itensNotaEntradaCollection.Add(itensNotaEntrada);
                }
                //Retorna a coleção de cadastro encontrada
                return itensNotaEntradaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens da nota cega! \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa um código para a nota cega
        public int PesqIdNotaCega()
        {
            try
            {
                string select = "select gen_id(gen_wms_nota_entrada_cega,1) as id FROM RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                int codNotaCega = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    codNotaCega = Convert.ToInt32(linha["id"]);
                }
                //Retorna a coleção de cadastro encontrada
                return codNotaCega;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o número da nota cega Detalhes:\n" + ex.Message);
            }
        }

        //Método criar nota cega
        public void GerarNotaCega(NotaEntrada n, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@nota", n.nota);
                conexao.AdicionarParamentros("@data", n.dataNota);
                conexao.AdicionarParamentros("@codUsuario", n.codUsuarioNotaCega);
                conexao.AdicionarParamentros("@codFornecedor", n.codFornecedor);
                conexao.AdicionarParamentros("@codNotaCega", n.codNotaCega);
                conexao.AdicionarParamentros("@liberarEstoque", n.liberarEstoque);
                conexao.AdicionarParamentros("@liberarArmazenagem", n.liberarArmazenagem);
                conexao.AdicionarParamentros("@ArmzenagemPicking", n.armzenagemPicking);
                conexao.AdicionarParamentros("@ExigirValidade", n.exigirValidade);
                conexao.AdicionarParamentros("@tipoRelatorio", n.tipoRelatorio);
                conexao.AdicionarParamentros("@crossDocking", n.crossDocking);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de update - Atualiza o nota de entrada com o numero da nota Cega
                string update = "update wms_nota_entrada set not_numero_cega = @codNotaCega,  " +
                            "not_liberar_estoque_fim = @liberarEstoque , not_liberar_armaz_fim = @liberarArmazenagem , " +
                            "not_armaz_com_picking = @ArmzenagemPicking, not_exigir_validade = @ExigirValidade, not_conf_relatorio = @tipoRelatorio, " +
                            "not_cross_docking = @crossDocking, usu_cod_gerou_cega = @codUsuario, not_data_cega = current_timestamp " +
                            "where not_numero = @nota and not_data_entrada = @data and forn_codigo = @codFornecedor and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);


            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar a nota cega. Detalhes:\n" + ex.Message);
            }
        }

        //Adicionar nota a nota cega
        public void AdicionarNotaNotaCega(int nota, int notaCega, int codUsuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@nota", nota);
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update - Remove o espelho da nota
                string update = "update wms_nota_entrada set not_numero_cega = @notaCega , usu_cod_gerou_cega = @codUsuario where not_numero = @nota" +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar a nota a nota cega. Detalhes:\n" + ex.Message);
            }
        }

        //Remove o número da nota cega da nota de entrad
        public void RemoverNota(int nota, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@nota", nota);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de update - Remove o espelho da nota
                string update = "update wms_nota_entrada set not_numero_cega = null, usu_cod_gerou_cega = null  " +
                                "where not_numero = @nota and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao remover a nota cega. Detalhes:\n" + ex.Message);
            }
        }

        //Atualizar controles deentrada
        public void AtualizarControle(int codNotaCega, int codUsuario, bool liberarEstoque, bool liberarArmazenagem, bool armzenagemPicking, bool exigirValidade, bool tipoRelatorio, bool crossDocking, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codNotaCega", codNotaCega);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@liberarEstoque", liberarEstoque);
                conexao.AdicionarParamentros("@liberarArmazenagem", liberarArmazenagem);
                conexao.AdicionarParamentros("@ArmzenagemPicking", armzenagemPicking);
                conexao.AdicionarParamentros("@ExigirValidade", exigirValidade);
                conexao.AdicionarParamentros("@tipoRelatorio", tipoRelatorio);
                conexao.AdicionarParamentros("@crossDocking", crossDocking);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de update - Atualiza o nota de entrada com o numero da nota Cega
                string update = "update wms_nota_entrada set not_liberar_estoque_fim = @liberarEstoque , not_liberar_armaz_fim = @liberarArmazenagem, " +
                            "not_armaz_com_picking = @ArmzenagemPicking, not_exigir_validade = @ExigirValidade, not_conf_relatorio = @tipoRelatorio, " +
                            "not_cross_docking = @crossDocking, usu_cod_gerou_cega = @codUsuario " +
                            "where not_numero_cega = @codNotaCega and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);


            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os controles de entrada. Detalhes:\n" + ex.Message);
            }
        }

        //Pesquisa o itens da nota de entrada
        public ItensNotaEntradaCollection PesqItensSemDadosLogisticos(string notaCega, string empresa)
        {
            try
            {
                //Instância a coleção
                ItensNotaEntradaCollection itensNotaEntradaCollection = new ItensNotaEntradaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de consulta
                string select = "select distinct(p.prod_codigo) as prod_codigo, p.prod_descricao, " +
                                "cat_codigo, prod_vida_util, prod_tolerancia, prod_lastro_p, " +
                                "prod_lastro_m, prod_lastro_g, prod_altura_p, prod_altura_m, prod_altura_g " +
                                "from wms_itens_nota i " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "inner join wms_nota_entrada n " +
                                "on n.not_codigo = i.not_codigo " +
                                "where n.not_numero_cega = @notaCega and p.prod_fator_pulmao is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.cat_codigo is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.prod_vida_util is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.prod_tolerancia is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.prod_lastro_p is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.prod_lastro_m is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.prod_lastro_g is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.prod_altura_p is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.prod_altura_m is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "or n.not_numero_cega = @notaCega and p.prod_altura_g is null and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ItensNotaEntrada itensNotaEntrada = new ItensNotaEntrada();
                    //Adiciona os valores encontrado
                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensNotaEntrada.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensNotaEntrada.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }
                    
                    itensNotaEntradaCollection.Add(itensNotaEntrada);
                }
                //Retorna a coleção de cadastro encontrada
                return itensNotaEntradaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens sem dados logisticos! \nDetalhes:" + ex.Message);
            }
        }


        /*Relatório*/

        //Pesquisa de nota e espelho
        public NotaCegaCollection PesqRelNotaCega(int codNotaCega)
        {
            try
            {
                //Instância o objêto
                NotaCegaCollection notaCollection = new NotaCegaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", codNotaCega);

                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "min(not_data_cega) as not_data_cega, u.usu_login, not_numero_cega, f.forn_codigo, f.forn_nome, list(not_numero, ', ') as not_codigo, " +
                                "count(not_codigo) as count_nota, sum(not_peso) as not_peso, " +
                                "(select count(prod_id) from wms_itens_nota where not_codigo in ( select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega)) as total_itens, " +
                                "(select sum(trunc(COALESCE(i.inot_quantidade, 0)/ p.prod_fator_pulmao)) from wms_itens_nota i " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "where not_codigo in ( select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega)) as total_volumes, " +
                                "uu.usu_login as usu_conferente, min(n.not_conf_inicial) as not_conf_inicial, max(n.not_conf_final) as not_conf_final " +
                                "from wms_nota_entrada n " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = n.forn_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = n.usu_cod_gerou_cega " +
                                "left join wms_usuario uu " +
                                "on uu.usu_codigo = n.usu_cod_conf " +
                                "where n.not_numero_cega = @notaCega " +
                                "group by  u.usu_login, not_numero_cega, f.forn_codigo, f.forn_nome, uu.usu_login";

               
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a coleção
                    NotaCega notaCega = new NotaCega();

                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        notaCega.empresa = Convert.ToString(linha["empresa"]);
                    }

                     if (linha["not_data_cega"] != DBNull.Value)
                     {
                         notaCega.data = Convert.ToDateTime(linha["not_data_cega"]);
                     }

                     if (linha["usu_login"] != DBNull.Value)
                     {
                         notaCega.usuNotaCega = Convert.ToString(linha["usu_login"]);
                     }

                     if (linha["not_numero_cega"] != DBNull.Value)
                     {
                         notaCega.notaCega = Convert.ToInt32(linha["not_numero_cega"]);
                     }

                     if (linha["forn_codigo"] != DBNull.Value)
                     {
                         notaCega.fornecedor = Convert.ToString(linha["forn_codigo"] +" - " +  linha["forn_nome"]);
                     }

                     if (linha["not_codigo"] != DBNull.Value)
                     {
                         notaCega.notaEntrada = Convert.ToString(linha["not_codigo"]);
                     }



                     if (linha["count_nota"] != DBNull.Value)
                     {
                         notaCega.qtdNota = Convert.ToInt32(linha["count_nota"]);
                     }

                     if (linha["total_itens"] != DBNull.Value)
                     {
                         notaCega.qtdItens = Convert.ToInt32(linha["total_itens"]);
                     }

                     if (linha["total_volumes"] != DBNull.Value)
                     {
                         notaCega.qtdVolumes = Convert.ToInt32(linha["total_volumes"]);
                     }

                     if (linha["not_peso"] != DBNull.Value)
                     {
                         notaCega.totalPeso = Convert.ToDouble(linha["not_peso"]);
                     }

                     if (linha["usu_conferente"] != DBNull.Value)
                     {
                         notaCega.usuConferente = Convert.ToString(linha["usu_conferente"]);
                     }

                    if (linha["not_conf_inicial"] != DBNull.Value)
                     {
                         notaCega.dataInicial = Convert.ToString(linha["not_conf_inicial"]);
                     }

                     if (linha["not_conf_final"] != DBNull.Value)
                     {
                         notaCega.dataFinal = Convert.ToString(linha["not_conf_final"]);
                     }

                    notaCollection.Add(notaCega);
                }
                //Retorna a coleção de cadastro encontrada
                return notaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o relatório da nota cega! \nDetalhes:" + ex.Message);
            }

        }

        //Pesquisa de nota e espelho   PesqRelItemNotaCega
        public ItemNotaCegaCollection PesqRelItemNotaCega(int codNotaCega, string tipo, string empresa)
        {
            try
            {
                //Instância a coleção
                ItemNotaCegaCollection itensCollection = new ItemNotaCegaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", codNotaCega);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select not_numero_cega, i.prod_id, prod_codigo, prod_descricao, max(prod_fator_pulmao) as prod_fator_pulmao, " +
                "(sum(i.inot_quantidade) * max(prod_fator_pulmao)) as inot_quantidade, " +
                "(sum(i.inot_quantidade_conf) + sum(i.inot_falta) + sum(i.inot_avaria)) as inot_quantidade_conf, " +
                "trunc((sum(i.inot_quantidade) * max(prod_fator_pulmao)) / max(prod_fator_pulmao)) as qtd_entrada_fechado, " +
                "mod((sum(i.inot_quantidade) * max(prod_fator_pulmao)), max(p.prod_fator_pulmao)) as qtd_entrada_fracionado, " +
                "trunc((sum(i.inot_quantidade_conf) + sum(i.inot_falta) + sum(i.inot_avaria)) / max(prod_fator_pulmao)) as qtd_conferida_fechado, uc.uni_unidade as uni_caixa, " +
                "mod((sum(i.inot_quantidade_conf) + sum(i.inot_falta) + sum(i.inot_avaria)), max(p.prod_fator_pulmao)) as qtd_conferida_fracionado, uf.uni_unidade as uni_fracionado, " +
                "sum(i.inot_falta) as inot_falta, sum(i.inot_avaria) as inot_avaria, " +
                "max(i.inot_validade) as inot_validade, max(inot_lote) as inot_lote,  " +
                "p.prod_controla_validade, PROD_PESO_VARIAVEL " +
                "from wms_nota_entrada n " +
                "inner join wms_itens_nota i " +
                "on i.not_codigo = n.not_codigo " +
                "inner join wms_produto p " +
                "on p.prod_id = i.prod_id " +
                "left join wms_unidade uc " +
                "on uc.uni_codigo = p.uni_codigo_pulmao " +
                "left join wms_unidade uf " +
                "on uf.uni_codigo = p.uni_codigo_picking " +
                "where n.not_numero_cega = @notaCega " +
                "and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                "group by not_numero_cega, i.prod_id, prod_codigo,  p.prod_descricao, " +
                "p.prod_controla_validade, uc.uni_unidade, uf.uni_unidade, PROD_PESO_VARIAVEL ";

                if (tipo.Equals("Divergência"))
                {
                    select += "having (sum(i.inot_quantidade) * max(prod_fator_pulmao)) <> COALESCE((sum(i.inot_quantidade_conf) + sum(i.inot_falta) + sum(i.inot_avaria)),0) ";
                }


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    ItemNotaCega item = new ItemNotaCega();

                    //Adiciona os valores encontrados
                    if (linha["not_numero_cega"] != DBNull.Value)
                    {
                        item.codNotaCega = Convert.ToInt32(linha["not_numero_cega"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.descProduto = Convert.ToString(linha["prod_codigo"] +" - "+ linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        item.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }                    

                    if (linha["inot_quantidade"] != DBNull.Value)
                    {
                        item.quantidadeNota = Convert.ToInt32(linha["inot_quantidade"]);
                    }

                    if (linha["qtd_entrada_fechado"] != DBNull.Value)
                    {
                        item.quantidadeNotaCaixa = Convert.ToInt32(linha["qtd_entrada_fechado"]);
                    }

                    if (linha["qtd_entrada_fracionado"] != DBNull.Value)
                    {
                        item.quantidadeNotaUnidade = Convert.ToInt32(linha["qtd_entrada_fracionado"]);
                    }

                    if (linha["inot_quantidade_conf"] != DBNull.Value)
                    {
                        item.quantidadeConferida = Convert.ToInt32(linha["inot_quantidade_conf"]);
                    }

                    if (linha["qtd_conferida_fechado"] != DBNull.Value)
                    {
                        item.quantidadeConferidaCaixa = Convert.ToInt32(linha["qtd_conferida_fechado"]);
                    }

                    if (linha["qtd_conferida_fracionado"] != DBNull.Value)
                    {
                        item.quantidadeConferidaUnidade = Convert.ToInt32(linha["qtd_conferida_fracionado"]);
                    }

                    if (linha["inot_falta"] != DBNull.Value)
                    {
                        item.quantidadeFalta = Convert.ToInt32(linha["inot_falta"]);
                    }

                    if (linha["inot_avaria"] != DBNull.Value)
                    {
                        item.quantidadeAvariada = Convert.ToInt32(linha["inot_avaria"]);
                    }

                    if (linha["uni_caixa"] != DBNull.Value)
                    {
                        item.undPulmao = Convert.ToString(linha["uni_caixa"]);
                    }

                    if (linha["uni_fracionado"] != DBNull.Value)
                    {
                        item.undPicking = Convert.ToString(linha["uni_fracionado"]);
                    }

                    if (linha["inot_validade"] != DBNull.Value)
                    {
                        item.validadeProduto = Convert.ToString(linha["inot_validade"]);
                    }

                    if (linha["inot_lote"] != DBNull.Value)
                    {
                        item.loteProduto = Convert.ToString(linha["inot_lote"]);
                    }

                    if (linha["PROD_PESO_VARIAVEL"] != DBNull.Value)
                    {
                        item.PesoVariavel = Convert.ToString(linha["PROD_PESO_VARIAVEL"]);
                    }
                    else
                    {
                        item.PesoVariavel = "False";
                    }

                    itensCollection.Add(item);

                }
                //Retorna a coleção de cadastro encontrada
                return itensCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens da divergência! \nDetalhes:" + ex.Message);
            }

        }

        //Pesquisa de nota e espelho
        public PaletizacaoNotaCegaCollection PesqRelPaletizacaoNotaCega(int codNotaCega, string empresa)
        {
            try
            {
                //Instância o objêto
                PaletizacaoNotaCegaCollection notaCollection = new PaletizacaoNotaCegaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", codNotaCega);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select distinct(i.prod_id) as prod_id, prod_codigo, prod_descricao, prod_cod_fornecedor, prod_fator_pulmao, uc.uni_unidade, " +
                                "replace(p.prod_controla_validade, 'True', 'Sim') as prod_controla_validade, p.prod_vida_util, prod_tolerancia, " +
                                "cast(prod_vida_util * (cast(p.prod_tolerancia as double precision) / 100) as integer) as prod_dias,  dateadd(prod_vida_util day to current_date) prod_data_tolerancia, " +
                                "p.prod_lastro_p, p.prod_altura_p, " +
                                "p.prod_lastro_m, p.prod_altura_m, " +
                                "p.prod_lastro_g, p.prod_altura_g, " +
                                "p.prod_lastro_b, p.prod_altura_b, " +
                                "(select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "not_numero_cega, u.usu_login, f.forn_codigo, f.forn_nome " +
                                "from wms_nota_entrada n " +
                                "inner join wms_itens_nota i " +
                                "on i.not_codigo = n.not_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_unidade uc " +
                                "on uc.uni_codigo = p.uni_codigo_pulmao " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = n.forn_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = n.usu_cod_gerou_cega " +
                                "where n.not_numero_cega = @notaCega " +
                                "and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a coleção
                    PaletizacaoNotaCega notaCega = new PaletizacaoNotaCega();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        notaCega.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["not_numero_cega"] != DBNull.Value)
                    {
                        notaCega.notaCega = Convert.ToInt32(linha["not_numero_cega"]);
                    }                   

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        notaCega.descProduto = Convert.ToString(linha["prod_codigo"] + " - "+ linha["prod_descricao"]);
                    }

                    if (linha["prod_cod_fornecedor"] != DBNull.Value)
                    {
                        notaCega.codProdutoFornecedor = Convert.ToString(linha["prod_cod_fornecedor"]);
                    }
                    
                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        notaCega.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        notaCega.unidadePulmao = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        notaCega.fornecedor = Convert.ToString(linha["forn_codigo"] + " - " + linha["forn_nome"]);
                    }

                    if (linha["prod_controla_validade"] != DBNull.Value)
                    {
                        notaCega.controlaVencimento = Convert.ToString(linha["prod_controla_validade"]);
                    }

                    if (linha["prod_vida_util"] != DBNull.Value)
                    {
                        notaCega.vidaUtil = Convert.ToInt32(linha["prod_vida_util"]);
                    }

                    if (linha["prod_tolerancia"] != DBNull.Value)
                    {
                        notaCega.tolerancia = Convert.ToInt32(linha["prod_tolerancia"]);
                    }

                    if (linha["prod_dias"] != DBNull.Value)
                    {
                        notaCega.diasMaxima = Convert.ToInt32(linha["prod_dias"]);
                    }

                    if (linha["prod_data_tolerancia"] != DBNull.Value)
                    {
                        notaCega.dataMaxima = Convert.ToDateTime(linha["prod_data_tolerancia"]);
                    }                    

                    if (linha["prod_lastro_p"] != DBNull.Value)
                    {
                        notaCega.lastroP = Convert.ToInt32(linha["prod_lastro_p"]);
                    }

                    if (linha["prod_altura_p"] != DBNull.Value)
                    {
                        notaCega.alturaP = Convert.ToInt32(linha["prod_altura_p"]);
                    }

                    if (linha["prod_lastro_m"] != DBNull.Value)
                    {
                        notaCega.lastroM = Convert.ToInt32(linha["prod_lastro_m"]);
                    }

                    if (linha["prod_altura_m"] != DBNull.Value)
                    {
                        notaCega.alturaM = Convert.ToInt32(linha["prod_altura_m"]);
                    }

                    if (linha["prod_lastro_g"] != DBNull.Value)
                    {
                        notaCega.lastroG = Convert.ToInt32(linha["prod_lastro_g"]);
                    }

                    if (linha["prod_altura_g"] != DBNull.Value)
                    {
                        notaCega.alturaG = Convert.ToInt32(linha["prod_altura_g"]);
                    }

                    if (linha["prod_lastro_b"] != DBNull.Value)
                    {
                        notaCega.lastroB = Convert.ToInt32(linha["prod_lastro_b"]);
                    }

                    if (linha["prod_altura_b"] != DBNull.Value)
                    {
                        notaCega.alturaB = Convert.ToInt32(linha["prod_altura_b"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        notaCega.usuario = Convert.ToString(linha["usu_login"]);
                    }

                    notaCollection.Add(notaCega);
                }
                //Retorna a coleção de cadastro encontrada
                return notaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o relatório de paletização da nota cega! \nDetalhes:" + ex.Message);
            }

        }





    }
}