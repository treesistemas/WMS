using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class CategoriaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa um novo id da categoria
        public Categoria PesqId()
        {
            try
            {
                //Instância a coleção
                Categoria categoria = new Categoria();
                //String de consulta
                string select = "select gen_id(gen_wms_categoria,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    categoria.codCategoria = Convert.ToInt32(linha["id"]);
                }
                //Retorna a coleção de cadastro encontrada
                return categoria;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa as categorias
        public CategoriaCollection PesqCategoria()
        {
            try
            {
                //Instância a coleção
                CategoriaCollection categoriaCollection = new CategoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select cat_codigo, cat_descricao, cat_audita, cat_validade, cat_lote from wms_categoria order by cat_codigo";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Categoria categoria = new Categoria();
                    //Adiciona os valores encontrados
                    if (linha["cat_codigo"] != DBNull.Value)
                    {
                        categoria.codCategoria = Convert.ToInt32(linha["cat_codigo"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        categoria.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["cat_audita"] != DBNull.Value)
                    {
                        categoria.auditaFlow = Convert.ToBoolean(linha["cat_audita"]);
                    }
                    else
                    {
                        categoria.auditaFlow = false;
                    }

                    if (linha["cat_validade"] != DBNull.Value)
                    {
                        categoria.controlaValidade = Convert.ToBoolean(linha["cat_validade"]);
                    }
                    else
                    {
                        categoria.controlaValidade = false;
                    }

                    if (linha["cat_lote"] != DBNull.Value)
                    {
                        categoria.controlaLote = Convert.ToBoolean(linha["cat_lote"]);
                    }
                    else
                    {
                        categoria.controlaLote = false;
                    }


                    //Adiciona os cadastros encontrados a coleção
                    categoriaCollection.Add(categoria);
                }
                //Retorna a coleção de cadastro encontrada
                return categoriaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as categorias. \nDetalhes:" + ex.Message);
            }
        }


        //Método salvar cadastro
        public void Salvar(int codCategoria, string descCategoria, bool auditaFlow, bool controlaValidade, bool controlaLote)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idCategoria", codCategoria);
                conexao.AdicionarParamentros("@descricao", descCategoria);
                conexao.AdicionarParamentros("@auditaFlow", auditaFlow);
                conexao.AdicionarParamentros("@controlaValidade", controlaValidade);
                conexao.AdicionarParamentros("@controlaLote", controlaLote);

                //String de insert
                string insert = "insert into wms_categoria (cat_codigo, cat_descricao, cat_audita, cat_validade, cat_lote) " +
                        "values (@idCategoria, @descricao, @auditaFlow, @controlaValidade, @controlaLote)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);           
            }
        }

        //Método Alterar cadastro
        public void Alterar(int codCategoria, string descCategoria, bool auditaFlow, bool controlaValidade, bool controlaLote)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idCategoria", codCategoria);
                conexao.AdicionarParamentros("@descricao", descCategoria);
                conexao.AdicionarParamentros("@auditaFlow", auditaFlow);
                conexao.AdicionarParamentros("@controlaValidade", controlaValidade);
                conexao.AdicionarParamentros("@controlaLote", controlaLote);

                //String de atualização
                string update = "update wms_categoria set cat_descricao = @descricao, cat_audita = @auditaFlow, cat_validade = @controlaValidade, cat_lote = @controlaLote " +
                    "where cat_codigo = @idCategoria";
                
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