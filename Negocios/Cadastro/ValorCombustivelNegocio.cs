using Dados;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Cadastro
{
    public class ValorCombustivelNegocio
    {

        //Instância a classe conexão
        Conexao conexao = new Conexao();


        //Pesquisa o Combusitivel no Negocios
        public ValorCombustivelCollection PesqValorCombustivel(DateTime combData, string combStatus)
        {
            try
            {
                //Instância a coleção
                ValorCombustivelCollection valorcombustivelCollection = new ValorCombustivelCollection();

                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@comb_data", combData);
                conexao.AdicionarParamentros("@comb_status", combStatus);

                //String de consulta
                string select = "select " +
                                    "comb_codigo, " +
                                    "comb_valor, " +
                                    "comb_data, " +
                                    "comb_status " +
                                "from wms_combustivel " +
                                "where comb_status = @comb_status and comb_data >= @comb_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ValorCombustivel combustivel = new ValorCombustivel();


                    //Adiciona os valores encontrados
                    if (linha["comb_codigo"] != DBNull.Value)
                    {
                        combustivel.combCodigo = Convert.ToInt32(linha["comb_codigo"]);
                    }

                    if (linha["comb_valor"] != DBNull.Value)
                    {
                        combustivel.combValor = (double)Convert.ToDecimal(linha["comb_valor"]);
                    }
                    
                    if (linha["comb_data"] != DBNull.Value)
                    {
                        combustivel.combData = Convert.ToDateTime(linha["comb_data"]);
                    }

                    if (linha["comb_status"] != DBNull.Value)
                    {
                        combustivel.combStatus = Convert.ToString(linha["comb_status"]);
                    }


                    //Adiciona os cadastros encontrados a coleção
                    valorcombustivelCollection.Add(combustivel);
                }
                //Retorna a coleção de cadastro encontrada
                return valorcombustivelCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o Valor do Combustivel. \nDetalhes:" + ex.Message);
            }
        }


        //Método salvar na Tabela
        public void Salvar(ValorCombustivel valorCombustivel)
        {
            try
            {
                //Limpar
                conexao.LimparParametros();

                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@comb_valor", valorCombustivel.combValor);
                conexao.AdicionarParamentros("@comb_data", valorCombustivel.combData);
                conexao.AdicionarParamentros("@comb_status", valorCombustivel.combStatus);

                //String de insert
                string insert = "insert into wms_combustivel " +
                                            "(comb_valor, " +
                                            "comb_data, " +
                                            "comb_status) " +
                                     "values(@comb_valor, " +
                                            "@comb_data, " +
                                            "@comb_status)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                PesqValorCombustivel(DateTime.Now, "True");

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar no cadastro. \nDetalhes:" + ex.Message);
            }
        }


        //Método Alterar cadastro
        public void Alterar(ValorCombustivel valorCombustivel)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();

                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@comb_codigo", valorCombustivel.combCodigo);
                conexao.AdicionarParamentros("@comb_valor", valorCombustivel.combValor);
                conexao.AdicionarParamentros("@comb_data", valorCombustivel.combData);
                conexao.AdicionarParamentros("@comb_status", valorCombustivel.combStatus);

                //String de atualização
                string update = "update " +
                                    "wms_combustivel " +
                                    "comb_valor = @comb_valor, " +
                                    "comb_data = @comb_data, " +
                                "where comb_codigo = @comb_codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

    }



}

