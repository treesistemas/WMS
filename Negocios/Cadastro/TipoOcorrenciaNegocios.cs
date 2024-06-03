using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class TipoOcorrenciaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa um novo id
        public int PesqId()
        {
            try
            {
                //String de consulta
                string select = "select gen_id(gen_wms_tipo_ocorrencia,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                int codigo = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    codigo = Convert.ToInt32(linha["id"]);
                }
                //Retorna a coleção de cadastro encontrada
                return codigo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método salvar cadastro
        public void Salvar(int codigo, string descricao, string area, bool ativo)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@descricao", descricao);
                conexao.AdicionarParamentros("@area", area);
                conexao.AdicionarParamentros("@ativo", ativo);

                //String de insert
                string insert = "insert into wms_tipo_ocorrencia (oco_codigo, oco_descricao, oco_area, oco_ativo) " +
                        "values (@codigo, @descricao, @area, @ativo)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método Alterar cadastro
        public void Alterar(int codigo, string descricao, string area, bool ativo)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@descricao", descricao);
                conexao.AdicionarParamentros("@area", area);
                conexao.AdicionarParamentros("@ativo", ativo);

                //String de atualização
                string update = "update wms_tipo_ocorrencia set oco_descricao = @descricao, oco_area = @area, oco_ativo = @ativo " +
                    "where oco_codigo = @codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa as categorias
        public TipoOcorrenciaCollection PesqTipoOcorrencia()
        {
            try
            {
                //Instância a coleção
                TipoOcorrenciaCollection tipoOcorrenciaCollection = new TipoOcorrenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select oco_codigo, oco_descricao, oco_area, oco_ativo from wms_tipo_ocorrencia order by oco_codigo";
                //Instância um datatable  
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    TipoOcorrencia tipoOcorrencia = new TipoOcorrencia();
                    //Adiciona os valores encontrados
                    if (linha["oco_codigo"] != DBNull.Value)
                    {
                        tipoOcorrencia.codigo = Convert.ToInt32(linha["oco_codigo"]);
                    }

                    if (linha["oco_descricao"] != DBNull.Value)
                    {
                        tipoOcorrencia.descricao = Convert.ToString(linha["oco_descricao"]);
                    }

                    if (linha["oco_area"] != DBNull.Value)
                    {
                        tipoOcorrencia.area = Convert.ToString(linha["oco_area"]);
                    }

                    if (linha["oco_ativo"] != DBNull.Value)
                    {
                        tipoOcorrencia.ativo = Convert.ToBoolean(linha["oco_ativo"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    tipoOcorrenciaCollection.Add(tipoOcorrencia);
                }
                //Retorna a coleção de cadastro encontrada
                return tipoOcorrenciaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os tipos de ocorrência. \nDetalhes:" + ex.Message);
            }
        }
    }
}
