using Dados;
using ObjetoTransferencia;
using System;
using System.Data;

namespace Negocios
{
    public class ConferenciaCegaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa as restrições da conferencia na configuração do sistema
        public bool PesqRestricao(string restricao, string empresa)
        {
            try
            {
                //Instância a variável
                bool ultrapassarConferencia = false;
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@restricao", restricao);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select iconf_status from wms_itens_configuracao where iconf_descricao = @restricao " +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["iconf_status"] != DBNull.Value)
                    {
                        ultrapassarConferencia = Convert.ToBoolean(linha["iconf_status"]);
                    }
                }

                //Retorna os valores encontrado
                return ultrapassarConferencia;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar às restrições da conferência cega. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a nota cega liberar para conferir
        public NotaEntradaCollection PesqNotaCegaLiberada()
        {
            try
            {
                //Instância a camada de objetos
                NotaEntradaCollection notaEntradaCollection = new NotaEntradaCollection();
                //String de consulta
                string select = "select distinct(not_numero_cega) as not_numero_cega, f.forn_nome from wms_nota_entrada n " +
                "inner join wms_fornecedor f " +
                "on n.forn_codigo = f.forn_codigo " +
                "where n.not_numero_cega is not null and n.not_conf_final is null and n.not_data_entrada between '01.06.2020 00:00:01' and '01.06.2020 23:59:59' ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    NotaEntrada notaEntrada = new NotaEntrada();
                    if (linha["not_numero_cega"] != DBNull.Value)
                    {
                        notaEntrada.codNotaCega = Convert.ToInt32(linha["not_numero_cega"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        notaEntrada.nmFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    notaEntradaCollection.Add(notaEntrada);
                }
                //Retorna os valores encontrado
                return notaEntradaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as notas cega liberadas para a conferência! \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a nota cega
        public NotaEntrada PesqNotaCega(string notaCega, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                NotaEntrada notaEntrada = new NotaEntrada();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select distinct(not_cross_docking), not_exigir_validade,  max(not_data_entrada) as data, count(not_numero_cega) as qtdNotaCega, not_numero_cega, usu_login, not_conf_inicial, not_conf_final, " +

                "(select count(distinct(prod_id)) from wms_itens_nota i " +
                "inner join wms_nota_entrada ni " +
                "on ni.not_codigo = i.not_codigo " +
                "where not_numero_cega = @notaCega) as qtdItens, " +

                "(select sum(not_peso) from wms_nota_entrada where not_numero_cega = @notaCega) as peso, " +

                "f.forn_codigo, f.forn_nome " +
                "from wms_nota_entrada n " +
                "inner join wms_usuario u " +
                "on u.usu_codigo = n.usu_cod_gerou_cega " +
                "inner join wms_fornecedor f " +
                "on f.forn_codigo = n.forn_codigo " +
                "where not_numero_cega = @notaCega and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                "group by not_cross_docking, not_exigir_validade, not_numero_cega, usu_login, not_conf_inicial, not_conf_final, f.forn_codigo, f.forn_nome ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["not_numero_cega"] != DBNull.Value)
                    {
                        notaEntrada.codNotaCega = Convert.ToInt32(linha["not_numero_cega"]);
                    }

                    if (linha["data"] != DBNull.Value)
                    {
                        notaEntrada.dataNotaCega = Convert.ToDateTime(linha["data"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        notaEntrada.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        notaEntrada.nmFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        notaEntrada.usuarioNotaCega = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["qtdNotaCega"] != DBNull.Value)
                    {
                        notaEntrada.quantidadeNota = Convert.ToInt32(linha["qtdNotaCega"]);
                    }

                    if (linha["qtdItens"] != DBNull.Value)
                    {
                        notaEntrada.quantidadeItens = Convert.ToInt32(linha["qtdItens"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        notaEntrada.pesoNota = Convert.ToDouble(linha["peso"]);
                    }

                    if (linha["not_exigir_validade"] != DBNull.Value)
                    {
                        notaEntrada.exigirValidade = Convert.ToBoolean(linha["not_exigir_validade"]);
                    }

                    if (linha["not_cross_docking"] != DBNull.Value)
                    {
                        notaEntrada.crossDocking = Convert.ToBoolean(linha["not_cross_docking"]);
                    }

                    if (linha["not_conf_inicial"] != DBNull.Value)
                    {
                        notaEntrada.inicioConferencia = Convert.ToDateTime(linha["not_conf_inicial"]);
                    }

                    if (linha["not_conf_final"] != DBNull.Value)
                    {
                        notaEntrada.fimConferencia = Convert.ToDateTime(linha["not_conf_final"]);
                    }

                }
                //Retorna os valores encontrado
                return notaEntrada;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a nota cega. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a nota cega
        public ItensNotaEntradaCollection PesqItensNotaCega(string notaCega, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                ItensNotaEntradaCollection itensNotaEntradaCollection = new ItensNotaEntradaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select not_numero_cega, p.prod_id, prod_codigo, prod_descricao, sum(i.inot_quantidade) as quantidade, i.inot_quantidade_conf, " +
                "inot_falta, inot_avaria, inot_lote, inot_validade,  p.prod_fator_compra, p.prod_vida_util, p.prod_tolerancia, cat_descricao, c.cat_validade, c.cat_lote, inot_palete_associado " +
                "from wms_itens_nota i " +
                "inner join wms_nota_entrada n " +
                "on n.not_codigo = i.not_codigo " +
                "inner join wms_produto p " +
                "on p.prod_id = i.prod_id " +
                "left join wms_categoria c " +
                "on c.cat_codigo = p.cat_codigo " +
                "where not_numero_cega = @notaCega " +
                "and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                "group by not_numero_cega, p.prod_id, prod_codigo, prod_descricao, i.inot_data_conferencia, prod_fator_compra, i.inot_quantidade_conf," +
                "inot_falta, inot_avaria, inot_lote, inot_validade,  p.prod_fator_compra, p.prod_vida_util, p.prod_tolerancia, cat_descricao, c.cat_validade, c.cat_lote, inot_palete_associado";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    ItensNotaEntrada itensNotaEntrada = new ItensNotaEntrada();

                    if (linha["not_numero_cega"] != DBNull.Value)
                    {
                        itensNotaEntrada.codNotaCega = Convert.ToInt32(linha["not_numero_cega"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itensNotaEntrada.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensNotaEntrada.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensNotaEntrada.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_compra"] != DBNull.Value)
                    {
                        itensNotaEntrada.fatorPulmao = Convert.ToInt32(linha["prod_fator_compra"]);
                    }

                    if (linha["quantidade"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeNota = Convert.ToInt32(linha["quantidade"]);
                    }

                    if (linha["inot_quantidade_conf"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeConferida = Convert.ToInt32(linha["inot_quantidade_conf"]);
                    }

                    if (linha["inot_falta"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeFalta = Convert.ToInt32(linha["inot_falta"]);
                    }

                    if (linha["inot_avaria"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeAvariada = Convert.ToInt32(linha["inot_avaria"]);
                    }

                    if (linha["inot_lote"] != DBNull.Value)
                    {
                        itensNotaEntrada.loteProduto = Convert.ToString(linha["inot_lote"]);
                    }

                    if (linha["inot_validade"] != DBNull.Value)
                    {
                        itensNotaEntrada.validadeProduto = Convert.ToDateTime(linha["inot_validade"]);
                    }

                    if (linha["prod_vida_util"] != DBNull.Value)
                    {
                        itensNotaEntrada.vidaProduto = Convert.ToInt32(linha["prod_vida_util"]);
                    }

                    if (linha["prod_tolerancia"] != DBNull.Value)
                    {
                        itensNotaEntrada.toleranciaProduto = Convert.ToInt32(linha["prod_tolerancia"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        itensNotaEntrada.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["cat_validade"] != DBNull.Value)
                    {
                        itensNotaEntrada.controlaVencimentoCategoria = Convert.ToBoolean(linha["cat_validade"]);
                    }

                    if (linha["cat_lote"] != DBNull.Value)
                    {
                        itensNotaEntrada.controlaLoteCategoria = Convert.ToBoolean(linha["cat_lote"]);
                    }

                    if (linha["inot_palete_associado"] != DBNull.Value)
                    {
                        itensNotaEntrada.paleteAssociado = Convert.ToInt32(linha["inot_palete_associado"]);
                    }



                    //Adiciona o objêto a coleção
                    itensNotaEntradaCollection.Add(itensNotaEntrada);
                }
                //Retorna os valores encontrado
                return itensNotaEntradaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens da nota cega. \nDetalhes:" + ex.Message);
            }
        }

        //Adicionar data inicial de conferencia
        public void AdicionarInicioConferencia(string notaCega, int codUsuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update - Remove o espelho da nota
                string update = "update wms_nota_entrada set not_conf_inicial = current_timestamp , usu_cod_conf = @codUsuario where not_numero_cega = @notaCega " +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar o início da conferência. Detalhes:\n" + ex.Message);
            }
        }

        //Adicionar data final de conferencia
        public void AdicionarFimConferencia(string notaCega, int codUsuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update - Remove o espelho da nota
                string update = "update wms_nota_entrada set not_conf_final = current_timestamp , usu_cod_conf = @codUsuario where not_numero_cega = @notaCega " +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar o fim da conferência. Detalhes:\n" + ex.Message);
            }
        }

        //Adicionar a quantidade
        public void AdicionarQuantidade(string notaCega, int idProduto, int quantidade, DateTime vencimento)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@quantidade", quantidade);
                conexao.AdicionarParamentros("@vencimento", vencimento);

                //String de update - Remove o espelho da nota
                string update = "update wms_itens_nota set inot_quantidade_conf = @quantidade, inot_validade = @vencimento " +
                    "where prod_id = @idProduto and not_codigo in " +
                    "(select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega )";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a falta. Detalhes:\n" + ex.Message);
            }
        }

        //Adicionar o vencimento
        public void AdicionarVencimento(string notaCega, int idProduto, DateTime vencimento, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@vencimento", vencimento);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update - Remove o espelho da nota
                string update = "update wms_itens_nota set inot_validade = @vencimento where prod_id = @idProduto and not_codigo in " +
                                "(select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega ) " +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar o lote. Detalhes:\n" + ex.Message);
            }
        }

        //Adicionar o lote
        public void AdicionarLote(string notaCega, int idProduto, string lote, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update - Remove o espelho da nota
                string update = "update wms_itens_nota set inot_lote = @lote where prod_id = @idProduto and not_codigo in " +
                    "(select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega) and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar o lote. Detalhes:\n" + ex.Message);
            }
        }

        //Adicionar a falta
        public void AdicionarFalta(string notaCega, int idProduto, int falta, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@falta", falta);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update - Remove o espelho da nota
                string update = "update wms_itens_nota set inot_falta = @falta where prod_id = @idProduto and not_codigo in " +
                    "(select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega )" +
                    "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a falta. Detalhes:\n" + ex.Message);
            }
        }

        //Adicionar avaria
        public void AdicionarAvaria(string notaCega, int idProduto, int avaria, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@avaria", avaria);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update - Remove o espelho da nota
                string update = "update wms_itens_nota set inot_avaria = @avaria where prod_id = @idProduto and not_codigo in " +
                    "(select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega ) " +
                    "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a avaria. Detalhes:\n" + ex.Message);
            }
        }

        //Adicionar produto ao palete
        public void AdicionarProdutoPalete(string notaCega, int idProduto, int palete, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@palete", palete);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update - Remove o espelho da nota
                string update = "update wms_itens_nota set inot_palete_associado = @palete where prod_id = @idProduto and not_codigo in " +
                    "(select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega ) " +
                    "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao associar o produto ao palete! \nDetalhes:" + ex.Message);
            }
        }

    }
}
