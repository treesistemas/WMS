using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

//Namspace que contém as classes que manipulam os dados (Base de dados FireBird)
namespace Dados
{
    public class Conexao
    {
        //Criar a conexão
        FbConnection fbConnection;

        //String de conexão BAT
        //private string strcon = "User=SYSDBA;Password=gd@!2020;Pooling=False;Database=E:/WMS/GD01/WMS.FDB;Datasource=192.168.10.15;Port=3050;dialect=3;";

        //String de conexão GD02
        //private string strcon = "User=SYSDBA;Password=gd@!2020;Pooling=False;Database=E:/WMS/WMS.FDB;Datasource=192.168.10.15;Port=3050;dialect=3;";

        //String de conexão GD
        //private string strcon = "User=SYSDBA;Password=masterkey;Pooling=False;Database=C:/WMS - BANCO TREE SISTEMAS/WMS.FDB;Datasource=192.168.10.14;Port=3050;dialect=3;";


        //String de conexão PRODUÇÃO DONIZETE
        //private string strcon = @"User=SYSDBA;Password=dztm#1989@fo;Pooling=False;Database=D:\PRODUCAO\WMSDONIZETE.FDB;Datasource=192.168.1.7;Port=3050;dialect=3;";

        //String de conexão PRODUÇÃO DANSUL
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=e:\DANSUL\PRODUCAO\WMS.FDB;Datasource=192.168.1.13;Port=3050;dialect=3;";

        //CAMINHO DE PRODUÇÃO DONIZETE FORQUILHA
        //string strcon = "User=SYSDBA;Password=masterkey;Pooling=False;Database=C:/Users/WMS/Desktop/DataBase/Producao/WMS.FDB;Datasource=10.0.0.8;Port=3050;dialect=3;";

        //CAMINHO DE PRODUÇÃO DONIZETE BARBALHA
        //string strcon = "User=SYSDBA;Password=masterkey;Pooling=False;Database=C:/DataBase/Producao/WMSDONIZETE.FDB;Datasource=10.88.0.8;Port=3050;dialect=3;";

        //CAMINHO DE PRODUÇÃO YMBALE
        //string strcon = "User=SYSDBA;Password=masterkey;Pooling=False;Database=c:/database/producao wms/WMSYMBALE.FDB;Datasource=192.168.15.22;Port=3050;dialect=3;";


        //String de conexão CACIQUE TERESINA
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=C:\Database PI\WMSCACIQUE.FDB;Datasource=localhost;Port=3050;dialect=3;";

        //String de conexão CACIQUE MARANÃO
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=C:\Database PI\WMSCACIQUE.FDB;Datasource=localhost;Port=3050;dialect=3;";


        //String de conexão HOMOLOGAÇÃO DONIZETE
       // private string strcon = @"User=SYSDBA;Password=dztm#1989@fo;Pooling=False;Database=D:\HOMOLOGACAO\WMSDONIZETE.FDB;Datasource=192.168.1.7;Port=3050;dialect=3;";

        //String de conexão PRODUÇÃO DANSUL
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=D:\Projeto\Banco de dados\DANSUL\WMSDANSUL.FDB;Datasource=localhost;Port=3050;dialect=3;";

        //String de conexão HOMOLOGAÇÃO YMBALE  24/05/2024
       // private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=C:\DATABASE\PRODUCAO WMS\WMSYMBALE.FDB;Datasource=192.168.15.22;Port=3050;dialect=3;";


        //Desenvolvimento LOCAL DONIZETE
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=D:\Projeto\Banco de dados\DZT\WMS.FDB;Datasource=localhost;Port=3050;dialect=3;";

        //Homologação
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=D:\Projeto\Banco de dados\GD\WMS.FDB;Datasource=localhost;Port=3050;dialect=3;";

        //Desenvolvimento LOCAL DONIZETE BARBALHA
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=D:\Projeto\Banco de dados\DZTBARBALHA\WMSDONIZETE.FDB;Datasource=localhost;Port=3050;dialect=3;";

        //Desenvolvimento LOCAL YMBALE
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=D:\Projeto\Banco de dados\YMBALE\WMSYMBALE.FDB;Datasource=localhost;Port=3050;dialect=3;";


        //CAMAINHO DE DESENVOLVIMENTO DANSUL
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=D:\Projeto\Banco de dados\DANSUL\WMSDANSUL_WINTHOR.FDB;Datasource=localhost;Port=3050;dialect=3;";

        //String de conexão HOMOLOGÇÃO SÁBADO GD
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=e:\HOMOLOGACAO\WMS.FDB;Datasource=192.168.1.13;Port=3050;dialect=3;";

        //String de conexão HOMOLOGAÇÃO YMBALE 
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=e:\HOMOLOGACAO\WMSYMBALE.FDB;Datasource=192.168.1.13;Port=3050;dialect=3;";

        //String de conexão HOMOLOGAÇÃO DONIZETE/BANCO TESTE FINAL
        private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=E:\HOMOLOGACAO02\WMSDONIZETE.FDB;Datasource=192.168.1.13;Port=3050;dialect=3;";

        //String de conexão HOMOLOGAÇÃO DONIZETE/BANCO TESTE FINAL
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=C:\base\WMS.FDB;Datasource=localhost;Port=3050;dialect=3;";

        //String de conexão HOMOLOGAÇÃO CACIQUE
        //private string strcon = "User=SYSDBA;Password=masterkey;Pooling=False;Database=E:/HOMOLOGACAO02/WMSCACIQUE.FDB;Datasource=192.168.1.13;Port=3050;dialect=3;";


        //String de conexão local
        //private string strcon = @"User=SYSDBA;Password=masterkey;Pooling=False;Database=C:\base\WMSYMBALE.FDB;Datasource=localhost;dialect=3;";


        //Pooling=False; Retira o tempo de conexão

        //Cria a conexão
        private FbConnection CriarConexao()
        {
            return new FbConnection(strcon);
        }

        //Parâmetros que vão para o banco
        private FbParameterCollection fbParameterCollection = new FbCommand().Parameters;

        //Limpa os parâmetros
        public void LimparParametros()
        {
            fbParameterCollection.Clear();
        }

        //Adiciona os parâmetros
        public void AdicionarParamentros(string nomeParametro, object valorParametro)
        {
            fbParameterCollection.Add(new FbParameter(nomeParametro, valorParametro));
        }

        //Persistência - Inserir, Alterar, Excluir 
        public object ExecuteManipulacao(CommandType commandType, string sql)
        {
            try
            {
                //Criar a conexão
                fbConnection = CriarConexao();
                //Abrir a conexão
                fbConnection.Open();
                //Criar o comando que vai levar a informação para o banco
                FbCommand fbCommand = fbConnection.CreateCommand();
                //Colocando os parâmetro e valores dentro do camando (trafegar na conexão)
                fbCommand.CommandType = commandType;
                fbCommand.CommandText = sql;
                //Tempo em segundo de conexão aberta (1 hora)
                fbCommand.CommandTimeout = 3600;

                //Adiciona os parâmetros no comando
                foreach (FbParameter fbParameter in fbParameterCollection)
                {
                    fbCommand.Parameters.Add(new FbParameter(fbParameter.ParameterName, fbParameter.Value));
                }

                //Executar o comando, ou seja, mandar o comando ir até o banco de dados.
                return fbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Fecha a conxão com o banco
                fbConnection.Close();
            }
        }

        //Persistência - Consultar
        public DataTable ExecutarConsulta(CommandType commandType, string sql)
        {
            try
            {
                //Criar a conexão
                fbConnection = CriarConexao();
                //Abrir a conexão
                fbConnection.Open();
                //Criar o comando que vali levar a informação para o banco
                FbCommand fbCommand = fbConnection.CreateCommand();
                //Colocando os parâmetro e valores dentro do camando (tafegar na conexão)
                fbCommand.CommandType = commandType;
                fbCommand.CommandText = sql;
                //Tempo em segundo de conexão aberta (1 hora)
                fbCommand.CommandTimeout = 3600;

                //Adiciona os parâmetros no comando
                foreach (FbParameter fbParameter in fbParameterCollection)
                {
                    fbCommand.Parameters.Add(new FbParameter(fbParameter.ParameterName, fbParameter.Value));
                }

                //Criar um adaptador
                FbDataAdapter fbDataAdapter = new FbDataAdapter(fbCommand);
                //DataTable (Recebe os dados que vem da base de dados)
                DataTable dataTable = new DataTable();
                //Preenche a datatable
                fbDataAdapter.Fill(dataTable);

                //Retorna o datatable preenchido
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Fecha a conxão com o banco
                fbConnection.Close();
            }
        }

    }
}
