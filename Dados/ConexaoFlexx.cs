using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Dados
{
    public class ConexaoFlexx
    {
        //Criar a conexão
        SqlConnection sqlConnection;

        AppSettingsReader r = new AppSettingsReader();

        //Caminho de Produção
        string caminho = "Data Source=consulta.flagcloud.com.br;Initial Catalog=Flexx01784065; User ID=SQL01784065; Password=SQL6868/Br0k3rMd$; Min Pool Size=5;Max Pool Size=250; Connect Timeout=3600";

        //Cria a conexão - Sql Server
        private SqlConnection CriarConexaoSqlServer()
        {
            //return new SqlConnection(r.GetValue("strConexao", typeof(string)).ToString());
            return new SqlConnection(caminho);
        }


        //Parâmetros que vão para o banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        //Limpa os parâmetros
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        //Adiciona os parâmetros
        public void AdicionarParamentros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Persistência - Inserir, Alterar, Excluir 
        public object ExecuteManipulacao(CommandType commandType, string sql)
        {
            try
            {       //Criar a conexão
                sqlConnection = CriarConexaoSqlServer();
                //Abrir a conexão
                sqlConnection.Open();
                //Criar o comando que vali levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando os parâmetro e valores dentro do camando (tafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = sql;
                //Tempo em segundo de conexão aberta (1 hora)
                //sqlCommand.CommandTimeout = 3600;

                //Adiciona os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Executar o comando, ou seja, mandar o comando ir até o banco de dados.
                return sqlCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Fecha a conexão
                sqlConnection.Close();
            }

        }

        //Persistência - Consultar
        public DataTable ExecutarConsulta(CommandType commandType, string sql)
        {
            try
            {
                //DataTable (Recebe os dados que vem da base de dados)
                DataTable dataTable = new DataTable();

                //Criar a conexão
                sqlConnection = CriarConexaoSqlServer();
                //Abrir a conexão
                sqlConnection.Open();
                //Criar o comando que vali levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando os parâmetro e valores dentro do camando (tafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = sql;
                //Tempo em segundo de conexão aberta (1 hora)
                sqlCommand.CommandTimeout = 3600;

                //Adiciona os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Criar um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                //Preenche a datatable
                sqlDataAdapter.Fill(dataTable);

                //Retorna o datatable preenchido
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Fecha a conexão
                sqlConnection.Close();
            }
        }
    }
}