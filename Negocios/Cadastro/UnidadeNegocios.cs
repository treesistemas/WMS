using System;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class UnidadeNegocios
    {
        //Instância a conexão
        Conexao conexao = new Conexao();

        //Método pesquisa id
        public Unidade PesqId()
        {
            try
            {
                //Instância o objeto
                Unidade unidade = new Unidade();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select gen_id(gen_wms_unidade,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    unidade.codUnidade = Convert.ToInt32(linha["id"]);
                }
                
                return unidade;
            }
            catch (Exception ex)
            {
                throw new Exception("Nenhuma unidade encontrada. \nDetalhes:" + ex.Message);
            }
        }

        //Método salvar cadastro
        public void Salvar(Unidade unidade)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idUnidade", unidade.codUnidade);
                conexao.AdicionarParamentros("@unidade", unidade.unidade);
                conexao.AdicionarParamentros("@descUnidade", unidade.descUnidade);
                //String de insert
                string insert = "insert into wms_unidade (uni_codigo, uni_unidade, uni_descricao) " +
                        "values (@idUnidade, @unidade, @descUnidade)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método alterar cadastro
        public void Alterar(Unidade unidade)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idUnidade", unidade.codUnidade);
                conexao.AdicionarParamentros("@unidade", unidade.unidade);
                conexao.AdicionarParamentros("@descUnidade", unidade.descUnidade);
                //String de atualização
                string update = "update wms_unidade set uni_unidade = @unidade, uni_descricao = @descUnidade where uni_codigo = @idUnidade";
                
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }

        }

        //Método pesquisa 
        public UnidadeCollection PesqUnidade()
        {
            try
            {
                //Instância a coleção
                UnidadeCollection unidadeCollection = new UnidadeCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "Select uni_codigo, uni_unidade, uni_descricao from wms_unidade order by uni_codigo";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância uma nova unidade
                    Unidade unidade = new Unidade();
                    //Adiciona os valores encontrados
                    if (linha["uni_codigo"] != DBNull.Value)
                    {
                        unidade.codUnidade = Convert.ToInt32(linha["uni_codigo"]);
                    }
                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        unidade.unidade = Convert.ToString(linha["uni_unidade"]);
                    }
                    if (linha["uni_descricao"] != DBNull.Value)
                    {
                        unidade.descUnidade = Convert.ToString(linha["uni_descricao"]);
                    }
                    //Adiciona os cadastros encontrados a coleção
                    unidadeCollection.Add(unidade);
                }
                //Retorna a coleção de cadastro encontrada
                return unidadeCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as unidades \nDetalhes:" + ex.Message);
            }
        }

    }
}
