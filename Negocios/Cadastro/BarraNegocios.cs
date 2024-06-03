using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class BarraNegocios
    {
        //Instância o objeto
        Conexao conexao = new Conexao();

        //Pesquisa o código de barra do produto (usado na confêrencia)
        public Barra PesqCodBarra(string codBarra)
        {
            try
            {
                //Instância a camada de objêtos
                Barra barra = new Barra();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codBarra", codBarra);

                //String de consulta
                string select = "select prod_id, bar_multiplicador from wms_barra b "+
                                "where bar_numero = @codBarra";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                   
                    //Adiciona os valores encontrados
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        barra.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["bar_multiplicador"] != DBNull.Value)
                    {
                        barra.multiplicador = Convert.ToInt32(linha["bar_multiplicador"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return barra;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o código de barra do produto. \nDetalhes:" + ex.Message);
            }
        }



    }
}