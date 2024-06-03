using Dados;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{

    public class ValorMotoristaNegocio
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o motorista no Negocios
        public ValorMotoristaCollection PesqValorMotorista(DateTime valData, string valStatus)
        {
            try
            {
                //Instância a coleção
                ValorMotoristaCollection valormotoristaCollection = new ValorMotoristaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@val_data", valData);
                conexao.AdicionarParamentros("@val_status", valStatus);

                //String de consulta
                string select = "select " +
                                    "val_codigo, " +
                                    "val_salario, " +
                                    "val_alimentacao, " +
                                    "val_data, " +
                                    "val_status " +
                                "from wms_valormotorista " +
                                "where val_status = @val_status and val_data >= @val_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ValorMotorista motorista = new ValorMotorista();


                    //Adiciona os valores encontrados
                    if (linha["val_codigo"] != DBNull.Value)
                    {
                        motorista.valCodigo = Convert.ToInt32(linha["val_codigo"]);
                    }

                    if (linha["val_salario"] != DBNull.Value)
                    {
                        motorista.valSalario = (double)Convert.ToDecimal(linha["val_salario"]);
                    }

                    if (linha["val_alimentacao"] != DBNull.Value)
                    {
                        motorista.valAlimentacao = (double)Convert.ToDecimal(linha["val_alimentacao"]);
                    }

                    if (linha["val_data"] != DBNull.Value)
                    {
                        motorista.valData = Convert.ToDateTime(linha["val_data"]);
                    }
    
                    if (linha["val_status"] != DBNull.Value)
                    {
                        motorista.valStatus = Convert.ToString(linha["val_status"]);
                    }

                
                    //Adiciona os cadastros encontrados a coleção
                    valormotoristaCollection.Add(motorista);
                }
                //Retorna a coleção de cadastro encontrada
                return valormotoristaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o motorista. \nDetalhes:" + ex.Message);
            }
        }



        //Método salvar na Tabela
        public void Salvar(ValorMotorista valorMotorista)
        {
            try
            {
                //Limpar
                //
                conexao.LimparParametros();
                //Adiciona os parâmetros
                //conexao.AdicionarParamentros("@val_codigo", valorMotorista.valCodigo);
                conexao.AdicionarParamentros("@val_salario", valorMotorista.valSalario);
                conexao.AdicionarParamentros("@val_alimentacao", valorMotorista.valAlimentacao);
                conexao.AdicionarParamentros("@val_data", valorMotorista.valData);
                conexao.AdicionarParamentros("@val_status", valorMotorista.valStatus);

                //String de insert
                string insert = "insert into wms_valormotorista " +
                                            "(val_salario, " +
                                            "val_alimentacao, " +
                                            "val_data, " +
                                            "val_status) " +
                                     "values(@val_salario, " +
                                            "@val_alimentacao, " +
                                            "@val_data, " +
                                            "@val_status)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                PesqValorMotorista(DateTime.Now, "True");

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }


        //Método Alterar cadastro
        public void Alterar(ValorMotorista valorMotorista)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@val_codigo", valorMotorista.valCodigo);
                conexao.AdicionarParamentros("@val_salario", valorMotorista.valSalario);
                conexao.AdicionarParamentros("@val_alimentacao", valorMotorista.valAlimentacao);
                conexao.AdicionarParamentros("@val_data", valorMotorista.valData);
                conexao.AdicionarParamentros("@val_status", valorMotorista.valStatus);

                //String de atualização
                string update = "update " +
                                    "wms_valormotorista " +
                                    "val_salario = @val_salario, " +
                                    "val_alimentacao = @val_alimentacao, " +
                                    "val_data = @val_data, " +
                                "where val_codigo = @val_codigo";

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

