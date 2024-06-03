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
    public class PerfilNegocios
    {
        //instância o objeto
        Conexao conexao = new Conexao();
        //Pesquisa o novo id
        public Perfil PesqId()
        {
            try
            {
                //Instância o objeto
                Perfil perfil = new Perfil();
                //String de pesquisa
                string select = "select gen_id(gen_wms_perfil,1) as id from RDB$DATABASE";
                //instância o datatable e executa a pesquisa
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    perfil.codPerfil = Convert.ToInt32(linha["id"]);
                }

                return perfil;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo perfíl \nDetalhes: " + ex.Message);
            }
        }
        //Salva o perfíl
        public void Salvar(Perfil perfil)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idPerfil", perfil.codPerfil);
                conexao.AdicionarParamentros("@descPerfil", perfil.descPerfil);
                //String de insert
                string insert = "insert into wms_perfil (perf_codigo, perf_descricao) " +
                        "values (@idPerfil, @descPerfil)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }
        //Altera o perfíl
        public void Alterar(Perfil perfil)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idPerfil", perfil.codPerfil);
                conexao.AdicionarParamentros("@descPerfil", perfil.descPerfil);
                //String de update
                string update = "update wms_perfil set perf_descricao= @descPerfil where perf_codigo = @idPerfil";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o perfíl
        public PerfilCollection PesqPerfil()
        {
            try
            {
                PerfilCollection perfilCollection = new PerfilCollection();
                //String de pesquisa
                string select = "select perf_codigo, perf_descricao from wms_perfil order by perf_codigo";
                //instância o datatable e executa a pesquisa
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Perfil perfil = new Perfil();
                    //Adiciona os valores encontrados
                    perfil.codPerfil = Convert.ToInt32(linha["perf_codigo"]);
                    perfil.descPerfil = Convert.ToString(linha["perf_descricao"]);

                    perfilCollection.Add(perfil);
                }

                return perfilCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o perfíl \nDetalhes: "+ ex.Message);
            }
            
        }
    }
}
