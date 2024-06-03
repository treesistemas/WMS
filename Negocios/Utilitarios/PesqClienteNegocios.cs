using System;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class PesqClienteNegocios
    {
        //instância a classe
        Conexao conexao = new Conexao();

        public PesqClienteCollection pesqCliente(string nmCliente, int codigoCliente)
        {
            try
            {
                //Instância a coleção
                PesqClienteCollection pesqClienteCollection = new PesqClienteCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@cliente", "%" + nmCliente + "%");
                conexao.AdicionarParamentros("@codigo", codigoCliente);
                //String de consulta
                string select = "select cli_codigo, cli_nome from wms_cliente where cli_status = 'True' ";

                if (!nmCliente.Equals("null"))
                {
                    select += "and cli_nome like @cliente order by cli_nome ";
                }
                else if (codigoCliente > 0)
                {
                    select += "and cli_codigo = @codigo order by cli_nome ";
                }

                //Instância um datatable 
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    PesqCliente pesqCliente = new PesqCliente();
                    //Adiciona os valores encontrados
                    pesqCliente.codCliente = Convert.ToInt32(linha["cli_codigo"]);
                    pesqCliente.nmCliente = Convert.ToString(linha["cli_nome"]);

                    pesqClienteCollection.Add(pesqCliente);
                }
                //Retorna a coleção de cadastro encontrada
                return pesqClienteCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o cliente! \nDetalhes: " + ex.Message);
            }

        }
    }
}
