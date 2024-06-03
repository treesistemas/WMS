using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;
using ObjetoTransferência;
using System.Data;

namespace Negocios
{
    public class PesqSeparadorNegocios
    {
        //Instância a conexão
        Conexao conexao = new Conexao();

        //Método pesquisa Separador
        public SeparadorCollection PesqSeparador(string nmSeparador, int codigo)
        {
            try
            {
                //Instância a coleção
                SeparadorCollection separadorCollection = new SeparadorCollection();
                //Instância a classe
                Separador separador = new Separador();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@nmSeparador", "%" + nmSeparador + "%");
                //String de consulta
                string select = "Select isn_separador, sepnm_separador from t_separador where sepnm_separador like @nmSeparador "+
                    "or isn_separador = @codigo order by sepnm_separador";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {                    
                    //Adiciona os valores encontrados
                    separador.codSeparador = Convert.ToInt32(linha["isn_separador"]);
                    separador.nmSeparador = Convert.ToString(linha["sepnm_separador"]);
                    //Adiciona os cadastros encontrados a coleção
                    separadorCollection.Add(separador);
                }
                //Retorna a coleção de cadastro encontrada
                return separadorCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Nenhum separador encontrado. \nDetalhes:" + ex.Message);
            }
        }

    }
}

