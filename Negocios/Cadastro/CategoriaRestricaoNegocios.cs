using System;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class CategoriaRestricaoNegocios
    {
        //Instância o objeto
        Conexao conexao = new Conexao();

        public CategoriaRestricaoCollection PesqListaCategoria()
        {
            try
            {
                //Instância a coleção
                CategoriaRestricaoCollection restricaoCollection = new CategoriaRestricaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select rest_codigo, r.cat_codigo, r.cat_codigo_rest, cat_descricao, rest_status from wms_categoria_restricao r " +
                    "inner join wms_categoria c " +
                    "on r.cat_codigo_rest = c.cat_codigo " +
                    "order by rest_codigo";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    CategoriaRestricao restricao = new CategoriaRestricao();
                    //Adiciona os valores encontrados
                    restricao.codCategoria1 = Convert.ToInt32(linha["cat_codigo"]);
                    restricao.codCategoria2 = Convert.ToInt32(linha["cat_codigo_rest"]);
                    restricao.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    restricao.status = Convert.ToChar(linha["rest_status"]);

                    //Adiciona os cadastros encontrados a coleção
                    restricaoCollection.Add(restricao);
                }
                //Retorna a coleção de cadastro encontrada
                return restricaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a lista de categorias. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa Nivel
        public EstruturaCollection PesqNivel(int idRegiao, int idRua)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Instância a coleção
                EstruturaCollection enderecoCollection = new EstruturaCollection();

                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                conexao.AdicionarParamentros("@idRua", idRua);

                //String de consulta
                string select = "select distinct(niv_numero) from wms_nivel where reg_codigo = " +
                       "@idRegiao and rua_codigo = @idRua order by niv_codigo";
                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe endereço
                    Estrutura endereco = new Estrutura();
                    //Adiciona o endereço
                    endereco.numeroNivel = Convert.ToInt32(linha["niv_numero"]);

                    enderecoCollection.Add(endereco);

                }
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os  níveis. \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa Nivel
        public EstruturaCollection PesqEndereco(int idRegiao, int idRua, string lado)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Instância a coleção
                EstruturaCollection enderecoCollection = new EstruturaCollection();
                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                conexao.AdicionarParamentros("@idRua", idRua);
                conexao.AdicionarParamentros("@lado", lado);
                //String de consulta
                string select = "select r.reg_codigo, r.reg_numero, a.rua_codigo, a.rua_numero, e.rua_lado, c.cat_codigo, c.cat_descricao from wms_categoria_endereco e " +
                                "inner join wms_regiao r " +
                                "on r.reg_codigo = e.reg_codigo " +
                                "inner join wms_rua a " +
                                "on a.rua_codigo = e.rua_codigo " +
                                "inner join wms_categoria c " +
                                "on c.cat_codigo = e.cat_codigo " +
                                "where e.reg_codigo = @idRegiao and e.rua_codigo = @idRua ";

                if (!lado.Equals("Todos"))
                {
                  select = select + "and e.rua_lado = @lado ";
                }
                
                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe endereço
                    Estrutura endereco = new Estrutura();
                    //Adiciona o endereço
                    endereco.codRegiao = Convert.ToInt32(linha["reg_codigo"]);
                    endereco.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    endereco.codRua = Convert.ToInt32(linha["rua_codigo"]);
                    endereco.numeroRua = Convert.ToInt32(linha["rua_numero"]);
                    endereco.ladoBloco = Convert.ToString(linha["rua_lado"]);
                    endereco.idCategoria = Convert.ToInt32(linha["cat_codigo"]);
                    endereco.categoria = Convert.ToString(linha["cat_descricao"]);
                    
                    enderecoCollection.Add(endereco);

                }
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os  endereços. \nDetalhes: " + ex.Message);
            }
        }

        //Atualizar Restrição
        public void Atualizar(int idCategoria, int idRestricao, char status)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idCategoria", idCategoria);
                conexao.AdicionarParamentros("@idRestricao", idRestricao);
                conexao.AdicionarParamentros("@status", status);

                //String de atualização
                string update = "update wms_categoria_restricao set rest_status = @status " +
                    "where cat_codigo = @idCategoria and cat_codigo_rest = @idRestricao";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar a restrição! \nDetalhes:" + ex.Message);
            }
        }

        //Atualizar endereço com a categoria
        public void AssociarEndereco(int idRegiao, int idRua, int idCategoria, string lado)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                conexao.AdicionarParamentros("@idRua", idRua);
                conexao.AdicionarParamentros("@idCategoria", idCategoria);
                conexao.AdicionarParamentros("@lado", lado);

                //String de atualização
                string insert = "insert into wms_categoria_endereco (reg_codigo, rua_codigo, rua_lado, cat_codigo) " +
                    "values " +
                    "(@idRegiao, @idRua, @lado, @idCategoria) ";
                    
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao associar a categoria ao endereco. \nDetalhes:" + ex.Message);
            }
        }

        //Atualizar endereço com a categoria
        public void RemoverEndereco(int idRegiao, int idRua, string lado, int idCategoria)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                conexao.AdicionarParamentros("@idRua", idRua);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@idCategoria", idCategoria);

                //String de atualização
                string delete = "delete from wms_categoria_endereco where reg_codigo = @idRegiao and rua_codigo = @idRua and rua_lado = @lado and cat_codigo = @idCategoria";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao remover a categoria do endereco. \nDetalhes:" + ex.Message);
            }
        }
    }
}
