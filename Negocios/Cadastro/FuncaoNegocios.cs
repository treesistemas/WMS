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
    public class FuncaoNegocios
    {
        //instância o objeto
        Conexao conexao = new Conexao();

        public AcessoCollection PesqFuncao()
        {
            try
            {
                //Instância o objeto
                AcessoCollection funcaoCollection = new AcessoCollection();
                //String de pesquisa
                string select = "select fun_codigo, fun_descricao, fun_menu, fun_submenu, fun_submenu_item from wms_funcao order by fun_ordem ";
                //instância o datatable e executa a pesquisa
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Acesso funcao = new Acesso();
                    //Adiciona os valores encontrados
                    funcao.codFuncao = Convert.ToInt32(linha["fun_codigo"]);
                    funcao.descFuncao = Convert.ToString(linha["fun_descricao"]);
                    funcao.paiFuncao = Convert.ToString(linha["fun_menu"]);
                    funcao.filhoFuncao = Convert.ToString(linha["fun_submenu"]);
                    funcao.itemFilhoFuncao = Convert.ToString(linha["fun_submenu_item"]);

                    funcaoCollection.Add(funcao);

                }

                return funcaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as funções \nDetalhes: " + ex.Message);
            }
        }
        //Pesquisa a função do perfíl
        public AcessoCollection PesqAcessoFuncao()
        {
            try
            {
                //Instância o objeto
                AcessoCollection funcaoCollection = new AcessoCollection();
                //String de pesquisa
                string select = "select perfxfunc_codigo, perf_codigo, pf.fun_codigo, fun_descricao, fun_ler, fun_escrever, fun_editar, fun_excluir, fun_menu, fun_submenu,  fun_submenu_item from wms_perfxfun pf " +
                                "inner join wms_funcao f " +
                                "on f.fun_codigo = pf.fun_codigo " +
                                "order by fun_ordem";


                //instância o datatable e executa a pesquisa
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Acesso funcao = new Acesso();
                    //Adiciona os valores encontrados

                    if (linha["perfxfunc_codigo"] != DBNull.Value)
                    {
                        funcao.codAcesso = Convert.ToInt32(linha["perfxfunc_codigo"]);
                    }

                    if (linha["perf_codigo"] != DBNull.Value)
                    {
                        funcao.codPerfil = Convert.ToInt32(linha["perf_codigo"]);
                    }

                    if (linha["fun_codigo"] != DBNull.Value)
                    {
                        funcao.codFuncao = Convert.ToInt32(linha["fun_codigo"]);
                    }

                    if (linha["fun_descricao"] != DBNull.Value)
                    {
                        funcao.descFuncao = Convert.ToString(linha["fun_descricao"]);
                    }

                    if (linha["fun_ler"] != DBNull.Value)
                    {
                        funcao.lerFuncao = Convert.ToBoolean(linha["fun_ler"]);
                    }

                    if (linha["fun_escrever"] != DBNull.Value)
                    {
                        funcao.escreverFuncao = Convert.ToBoolean(linha["fun_escrever"]);
                    }

                    if (linha["fun_editar"] != DBNull.Value)
                    {
                        funcao.editarFuncao = Convert.ToBoolean(linha["fun_editar"]);
                    }

                    if (linha["fun_excluir"] != DBNull.Value)
                    {
                        funcao.excluirFuncao = Convert.ToBoolean(linha["fun_excluir"]);
                    }

                    if (linha["fun_menu"] != DBNull.Value)
                    {
                        funcao.paiFuncao = Convert.ToString(linha["fun_menu"]);
                    }

                    if (linha["fun_submenu"] != DBNull.Value)
                    {
                        funcao.filhoFuncao = Convert.ToString(linha["fun_submenu"]);
                    }

                    if (linha["fun_submenu_item"] != DBNull.Value)
                    {
                        funcao.itemFilhoFuncao = Convert.ToString(linha["fun_submenu_item"]);
                    }


                    funcaoCollection.Add(funcao);

                }

                return funcaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o acesso! \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa a função e acesso do usuário
        public AcessoCollection PesqAcessoUsuario()
        {
            try
            {
                //Instância o objeto
                AcessoCollection funcaoCollection = new AcessoCollection();
                //String de pesquisa
                string select = "select uf.usuxfunc_codigo, usu_codigo, perf_codigo, f.fun_codigo, f.fun_descricao, fun_ler, fun_escrever, fun_editar, fun_excluir, f.fun_menu, f.fun_submenu, f.fun_submenu_item  from wms_usuxfun uf "+
                "inner join wms_funcao f "+
                "on f.fun_codigo = uf.fun_codigo " +
                "order by fun_ordem";

                 
                //instância o datatable e executa a pesquisa
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Acesso funcao = new Acesso();
                    //Adiciona os valores encontrados
                    funcao.codAcesso = Convert.ToInt32(linha["usuxfunc_codigo"]);
                    funcao.codUsuario = Convert.ToInt32(linha["usu_codigo"]);
                    funcao.codPerfil = Convert.ToInt32(linha["perf_codigo"]);
                    funcao.codFuncao = Convert.ToInt32(linha["fun_codigo"]);
                    funcao.descFuncao = Convert.ToString(linha["fun_descricao"]);
                    funcao.lerFuncao = Convert.ToBoolean(linha["fun_ler"]);
                    funcao.escreverFuncao = Convert.ToBoolean(linha["fun_escrever"]);
                    funcao.editarFuncao = Convert.ToBoolean(linha["fun_editar"]);
                    funcao.excluirFuncao = Convert.ToBoolean(linha["fun_excluir"]);
                    funcao.paiFuncao = Convert.ToString(linha["fun_menu"]);
                    funcao.filhoFuncao = Convert.ToString(linha["fun_submenu"]);
                    funcao.itemFilhoFuncao = Convert.ToString(linha["fun_submenu_item"]);

                    funcaoCollection.Add(funcao);

                }

                return funcaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o acesso! \nDetalhes: " + ex.Message);
            }
        }

        public void AtualizaPermissao(int idPerfil, int idFuncao, bool ler, bool escrever, bool editar, bool excluir)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idPerfil", idPerfil);
                conexao.AdicionarParamentros("@idFuncao", idFuncao);
                conexao.AdicionarParamentros("@ler", ler);
                conexao.AdicionarParamentros("@escrever", escrever);
                conexao.AdicionarParamentros("@editar", editar);
                conexao.AdicionarParamentros("@excluir", excluir);
                //String de update
                string update = "update wms_perfxfun set fun_ler = @ler , fun_escrever = @escrever , fun_editar = @editar, fun_excluir = @excluir where perf_codigo = @idPerfil and fun_codigo = @idFuncao";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar as permissões. \nDetalhes:" + ex.Message);
            }
        }

        public void AtualizaPermissaoUsuario(int idUsuario, int idFuncao, bool ler, bool escrever, bool editar, bool excluir)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idUsuario", idUsuario);
                conexao.AdicionarParamentros("@idFuncao", idFuncao);
                conexao.AdicionarParamentros("@ler", ler);
                conexao.AdicionarParamentros("@escrever", escrever);
                conexao.AdicionarParamentros("@editar", editar);
                conexao.AdicionarParamentros("@excluir", excluir);
                //String de update
                string update = "update wms_usuxfun set fun_ler = @ler , fun_escrever = @escrever , fun_editar = @editar, fun_excluir = @excluir where usu_codigo = @idUsuario and fun_codigo = @idFuncao";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar as permissões. \nDetalhes:" + ex.Message);
            }
        }


    }
}
