using Dados;
using ObjetoTransferencia;
using System;
using System.Data;


namespace Negocios
{
    public class TipoRotaNegocios
    {
        //Instância a conexão
        Conexao conexao = new Conexao();

        //Pesquisa id
        public int PesqId()
        {
            try
            {
                //String de consulta
                string select = "select gen_id(gen_wms_tipo_rota,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                int codigo = 0;

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    codigo = Convert.ToInt32(linha["id"]);
                }

                return codigo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro agerar um novo código. \nDetalhes:" + ex.Message);
            }
        }

        //Método pesquisa 
        public TipoRotaCollection PesqTipo()
        {
            try
            {
                //Instância a coleção
                TipoRotaCollection tipoRotaCollection = new TipoRotaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "Select tipo_codigo, tipo_descricao from wms_tipo_rota order by tipo_codigo";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    TipoRota tipoRota = new TipoRota();

                    //Adiciona os valores encontrados
                    if (linha["tipo_codigo"] != DBNull.Value)
                    {
                        tipoRota.codTipoRota = Convert.ToInt32(linha["tipo_codigo"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        tipoRota.descTipoRota = Convert.ToString(linha["tipo_descricao"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    tipoRotaCollection.Add(tipoRota);
                }
                //Retorna a coleção de cadastro encontrada
                return tipoRotaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os tipos de rotas \nDetalhes:" + ex.Message);
            }
        }

        //Salvar cadastro
        public void Salvar(int codigo, string descricao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@descricao", descricao);
  
                //String de insert
                string insert = "insert into wms_tipo_rota (tipo_codigo, tipo_descricao) " +
                        "values (@codigo, @descricao)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método alterar cadastro
        public void Alterar(int codigo, string descricao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@descricao", descricao);

                //String de atualização
                string update = "update wms_tipo_rota set tipo_descricao = @descricao where tipo_codigo = @codigo";

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
