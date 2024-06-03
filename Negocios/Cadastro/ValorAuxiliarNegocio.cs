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

    public class ValorAuxiliarNegocio
    {

        //Instância a classe conexão
        Conexao conexao = new Conexao();


        //Pesquisa o auxiliar no Negocios
        public ValorAuxiliarCollection PesqValorAuxiliar(DateTime auxData, string auxStatus)
        {
            try
            {
                //Instância a coleção
                ValorAuxiliarCollection valorauxiliarCollection = new ValorAuxiliarCollection();

                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@aux_data", auxData);
                conexao.AdicionarParamentros("@aux_status", auxStatus);

                //String de consulta
                string select = "select " +
                                    "aux_codigo, " +
                                    "aux_salario, " +
                                    "aux_alimentacao, " +
                                    "aux_data, " +
                                    "aux_status " +
                                "from wms_aux_espedicao " +
                                "where aux_status = @aux_status and aux_data >= @aux_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ValorAuxiliar auxiliar = new ValorAuxiliar();


                    //Adiciona os valores encontrados
                    if (linha["aux_codigo"] != DBNull.Value)
                    {
                        auxiliar.auxCodigo = Convert.ToInt32(linha["aux_codigo"]);
                    }

                    if (linha["aux_salario"] != DBNull.Value)
                    {
                        auxiliar.auxSalario = (double)Convert.ToDecimal(linha["aux_salario"]);
                    }

                    if (linha["aux_alimentacao"] != DBNull.Value)
                    {
                        auxiliar.auxAlimentacao = (double)Convert.ToDecimal(linha["aux_alimentacao"]);
                    }

                    if (linha["aux_data"] != DBNull.Value)
                    {
                        auxiliar.auxData = Convert.ToDateTime(linha["aux_data"]);
                    }

                    if (linha["aux_status"] != DBNull.Value)
                    {
                        auxiliar.auxStatus = Convert.ToString(linha["aux_status"]);
                    }


                    //Adiciona os cadastros encontrados a coleção
                    valorauxiliarCollection.Add(auxiliar);
                }
                //Retorna a coleção de cadastro encontrada
                return valorauxiliarCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o Auxiliar de Espedição. \nDetalhes:" + ex.Message);
            }
        }


        //Método salvar na Tabela
        public void Salvar(ValorAuxiliar valorAuxiliar)
        {
            try
            {
                //Limpar
                conexao.LimparParametros();

                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@aux_salario", valorAuxiliar.auxSalario);
                conexao.AdicionarParamentros("@aux_alimentacao", valorAuxiliar.auxAlimentacao);
                conexao.AdicionarParamentros("@aux_data", valorAuxiliar.auxData);
                conexao.AdicionarParamentros("@aux_status", valorAuxiliar.auxStatus);

                //String de insert
                string insert = "insert into wms_aux_espedicao " +
                                            "(aux_salario, " +
                                            "aux_alimentacao, " +
                                            "aux_data, " +
                                            "aux_status) " +
                                     "values(@aux_salario, " +
                                            "@aux_alimentacao, " +
                                            "@aux_data, " +
                                            "@aux_status)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                PesqValorAuxiliar(DateTime.Now, "True");

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }


        //Método Alterar cadastro
        public void Alterar(ValorAuxiliar valorAuxiliar)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();

                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@aux_codigo", valorAuxiliar.auxCodigo);
                conexao.AdicionarParamentros("@aux_salario", valorAuxiliar.auxSalario);
                conexao.AdicionarParamentros("@aux_alimentacao", valorAuxiliar.auxAlimentacao);
                conexao.AdicionarParamentros("@aux_data", valorAuxiliar.auxData);
                conexao.AdicionarParamentros("@aux_status", valorAuxiliar.auxStatus);

                //String de atualização
                string update = "update " +
                                    "wms_aux_espedicao " +
                                    "aux_salario = @aux_salario, " +
                                    "aux_alimentacao = @aux_alimentacao, " +
                                    "aux_data = @aux_data, " +
                                "where aux_codigo = @aux_codigo";

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
