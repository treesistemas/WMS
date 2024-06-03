using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjetoTransferencia;
using Negocios;
using Dados;
using System.Data;


namespace Negocios.Movimentacao
{
    public class DeParaNegocios
    {

        //Conexão com o banco de dados do WMS
        Conexao conexao = new Conexao();

        //Pesquisa o produto
        public DePara PesqProduto(string codProduto, string tipo, string empresa)
        {
            try
            {
                //Instância a coleção
                DePara depara = new DePara();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select est_codigo, s.prod_id, s.apa_codigo, prod_codigo ||'-'|| prod_descricao as produto, a.apa_endereco "+
                                "from wms_separacao s "+
                                "inner join wms_produto p "+
                                "on p.prod_id = s.prod_id "+
                                "inner join wms_apartamento a "+
                                "on a.apa_codigo = s.apa_codigo "+
                                "where sep_tipo = @tipo and prod_codigo = @codProduto and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        depara.idEstacao1 = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        depara.idProduto1 = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["produto"] != DBNull.Value)
                    {
                        depara.descPorduto1 = Convert.ToString(linha["produto"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        depara.idEndereco1 = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        depara.endProduto1 = Convert.ToString(linha["apa_endereco"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return depara;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o endereco
        public DePara PesqEndereco(string endereco, string tipo, string empresa)
        {
            try
            {
                //Instância a coleção
                DePara depara = new DePara();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@endereco", endereco);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select est_codigo, s.prod_id, s.apa_codigo, prod_codigo ||'-'|| prod_descricao as produto, a.apa_endereco " +
                                "from wms_produto p " +
                                "inner join wms_separacao s " +
                                "on p.prod_id = s.prod_id " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "where sep_tipo = @tipo and apa_endereco = @endereco and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        depara.idEstacao2 = Convert.ToInt32(linha["est_codigo"]);
                    }
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        depara.idProduto2 = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["produto"] != DBNull.Value)
                    {
                        depara.descPorduto2 = Convert.ToString(linha["produto"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        depara.idEndereco2 = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        depara.endProduto2 = Convert.ToString(linha["apa_endereco"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return depara;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereco. \nDetalhes:" + ex.Message);
            }
        }


        //Atualiza os bairros alterados no erp
        public void AtualizarEndereco(DePara dePara, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto1", dePara.idProduto1);
                conexao.AdicionarParamentros("@idEndereco1", dePara.idEndereco1);
                conexao.AdicionarParamentros("@idEstacao1", dePara.idEstacao1);
                conexao.AdicionarParamentros("@idProduto2", dePara.idProduto2);
                conexao.AdicionarParamentros("@idEndereco2", dePara.idEndereco2);
                conexao.AdicionarParamentros("@idEstacao2", dePara.idEstacao2);
                conexao.AdicionarParamentros("@tipo", dePara.tipo);
                conexao.AdicionarParamentros("@empresa", empresa);

                //Zera o código do endereço
                //String de update
                string updateEndereco = "update wms_separacao set apa_codigo = null where sep_tipo = @tipo and prod_id = @idProduto2 and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                conexao.ExecuteManipulacao(CommandType.Text, updateEndereco);

                //String de update
                string update = "update wms_separacao set apa_codigo = @idEndereco2, est_codigo = @idEstacao2 where sep_tipo = @tipo and prod_id = @idProduto1 and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //String de update
                string update1 = update = "update wms_separacao set apa_codigo = @idEndereco1, est_codigo = @idEstacao1 where sep_tipo = @tipo and prod_id = @idProduto2 and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update1);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao efetuar o de para! \nDetalhes:" + ex.Message);
            }
        }

        //Registra no rastreamento
        
         
        public void InserirRastreamento(string empresa, string operacao, int codUsuario, int idProduto,
            int? codApartamentoOrigem, int? quantidadeOrigem, string vencimentoOrigem, double? pesoOrigem, string loteOrigem,
            int? codApartamentoDestino, int? quantidadeDestino, string vencimentoDestino, double? pesoDestino, string loteDestino, string tipo)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@operacao", operacao);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codApartamentoOrigem", codApartamentoOrigem);
                conexao.AdicionarParamentros("@quantidadeOrigem", quantidadeOrigem);
                conexao.AdicionarParamentros("@pesoOrigem", pesoOrigem);
                conexao.AdicionarParamentros("@vencimentoOrigem", vencimentoOrigem);
                conexao.AdicionarParamentros("@loteOrigem", loteOrigem);
                conexao.AdicionarParamentros("@codApartamentoDestino", codApartamentoDestino);
                conexao.AdicionarParamentros("@quantidadeDestino", quantidadeDestino);
                conexao.AdicionarParamentros("@pesoDestino", pesoDestino);
                conexao.AdicionarParamentros("@vencimentoDestino", vencimentoDestino);
                conexao.AdicionarParamentros("@loteDestino", loteDestino);
                conexao.AdicionarParamentros("@tipo", tipo);

                //String de insert - insere o endereço  
                string insert = "insert into wms_rastreamento_armazenagem (rast_codigo, rast_operacao, rast_data, usu_codigo, prod_id," +
                "apa_codigo_origem, arm_quantidade_origem, arm_peso_origem, arm_vencimento_origem, arm_lote_origem, " +
                "apa_codigo_destino, arm_quantidade_destino, arm_peso_destino, arm_vencimento_destino, arm_lote_destino, arm_tipo, conf_codigo)" +
                "values" +
                "(gen_id(gen_wms_rast_armazenagem, 1), @operacao, current_timestamp, @codUsuario, @idProduto, " +
                "@codApartamentoOrigem, @quantidadeOrigem, @pesoOrigem, @vencimentoOrigem, @loteOrigem," +
                "@codApartamentoDestino, @quantidadeDestino, @pesoDestino, @vencimentoDestino, @loteDestino, @tipo, (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";


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

