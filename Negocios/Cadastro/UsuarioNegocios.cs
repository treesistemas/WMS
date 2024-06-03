using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;
using ObjetoTransferencia;
using System.Data;
using System.IO;

namespace Negocios
{
    public class UsuarioNegocios
    {
        //instância o objeto
        Conexao conexao = new Conexao();
        //Pesquisa o novo id do usuário
        public Usuario PesqId()
        {
            try
            {
                //Instância o objeto
                Usuario usuario = new Usuario();
                //String de pesquisa
                string select = "select gen_id(gen_wms_usuario,1) as id from RDB$DATABASE";
                //instância o datatable e executa a pesquisa
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    usuario.codUsuario = Convert.ToInt32(linha["id"]);
                }

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo usuário \nDetalhes: " + ex.Message);
            }
        }

        public void Salvar(Usuario usuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idUsuario", usuario.codUsuario);
                conexao.AdicionarParamentros("@nome", usuario.nome);
                conexao.AdicionarParamentros("@login", usuario.login);
                conexao.AdicionarParamentros("@senha", usuario.senha);
                conexao.AdicionarParamentros("@turno", usuario.turno);
                conexao.AdicionarParamentros("@status", usuario.status);
                conexao.AdicionarParamentros("@controlaSenha", usuario.controlaSenha);
                conexao.AdicionarParamentros("@idPerfil", usuario.codPerfil);
                conexao.AdicionarParamentros("@email", usuario.email);
                conexao.AdicionarParamentros("@dias", usuario.diasExpirar);
                conexao.AdicionarParamentros("@dataExpirar", usuario.dataExpiracao);
                conexao.AdicionarParamentros("@foto", usuario.foto);
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de insert
                string insert = "insert into wms_usuario (usu_codigo, usu_nome, usu_login, usu_senha, usu_turno, usu_status, usu_control_senha, perf_codigo, usu_email, usu_dias_expirar, usu_dt_expiracao, usu_foto, conf_codigo) " +
                        "values (@idUsuario, @nome, @login, @senha,  @turno, @status, @controlaSenha, @idPerfil, @email, @dias, @dataExpirar, @foto, (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        public void Alterar(Usuario usuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idUsuario", usuario.codUsuario);
                conexao.AdicionarParamentros("@nome", usuario.nome);
                conexao.AdicionarParamentros("@login", usuario.login);
                conexao.AdicionarParamentros("@turno", usuario.turno);
                conexao.AdicionarParamentros("@status", usuario.status);
                conexao.AdicionarParamentros("@controlaSenha", usuario.controlaSenha);
                conexao.AdicionarParamentros("@idPerfil", usuario.codPerfil);
                conexao.AdicionarParamentros("@email", usuario.email);
                conexao.AdicionarParamentros("@dias", usuario.diasExpirar);
                conexao.AdicionarParamentros("@dataExpirar", usuario.dataExpiracao);
                conexao.AdicionarParamentros("@foto", usuario.foto);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de update
                string update = "update wms_usuario set usu_nome = @nome, usu_login = @login, usu_turno = @turno, usu_status = @status, usu_control_senha = @controlaSenha, perf_codigo = @idPerfil, usu_email = @email, usu_dias_expirar = @dias, " +
                        "usu_dt_expiracao = @dataExpirar, usu_foto = @foto where usu_codigo = @idUsuario and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        public void ResetaSenha(int idUsuario, string senha)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idUsuario", idUsuario);
                conexao.AdicionarParamentros("@senha", senha);

                //String de update
                string update = "update wms_usuario set usu_senha_anterior = usu_senha, usu_senha = @senha where usu_codigo = @idUsuario";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o usuário
        public UsuarioCollection PesqUsuario(string empresa)
        {
            try
            {
                //Limpar parâmetros
                conexao.LimparParametros();
                //Adicionar
                conexao.AdicionarParamentros("@empresa", empresa);

                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //String de pesquisa
                string select = "select usu_codigo, usu_nome, usu_login, usu_senha, usu_status, usu_control_senha, usu_turno, usu.perf_codigo, perf_descricao, usu_email, usu_dias_expirar, usu_dt_expiracao, usu_foto " +
                    "from wms_usuario usu " +
                    "left outer join wms_perfil prf " +
                    "on usu.perf_codigo = prf.perf_codigo " +
                    "where usu.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                    "order by usu_codigo";


                //instância o datatable e executa a pesquisa
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Usuario usuario = new Usuario();
                    //Adiciona os valores encontrados
                    if (linha["usu_codigo"] != DBNull.Value)
                    {
                        usuario.codUsuario = Convert.ToInt32(linha["usu_codigo"]);
                    }
                    if (linha["usu_nome"] != DBNull.Value)
                    {
                        usuario.nome = Convert.ToString(linha["usu_nome"]);
                    }
                    if (linha["usu_login"] != DBNull.Value)
                    {
                        usuario.login = Convert.ToString(linha["usu_login"]);
                    }
                    if (linha["usu_senha"] != DBNull.Value)
                    {
                        usuario.senha = Convert.ToString(linha["usu_senha"]);
                    }
                    if (linha["usu_turno"] != DBNull.Value)
                    {
                        usuario.turno = Convert.ToString(linha["usu_turno"]);
                    }
                    if (linha["usu_status"] != DBNull.Value)
                    {
                        usuario.status = Convert.ToBoolean(linha["usu_status"]);
                    }
                    else
                    {
                        usuario.status = true;
                    }
                    if (linha["usu_control_senha"] != DBNull.Value)
                    {
                        usuario.controlaSenha = Convert.ToBoolean(linha["usu_control_senha"]);
                    }
                    else
                    {
                        usuario.controlaSenha = false;
                    }
                    if (linha["perf_codigo"] != DBNull.Value)
                    {
                        usuario.codPerfil = Convert.ToInt32(linha["perf_codigo"]);
                    }
                    if (linha["perf_descricao"] != DBNull.Value)
                    {
                        usuario.perfil = Convert.ToString(linha["perf_descricao"]);
                    }
                    if (linha["usu_email"] != DBNull.Value)
                    {
                        usuario.email = Convert.ToString(linha["usu_email"]);
                    }
                    if (linha["usu_dias_expirar"] != DBNull.Value)
                    {
                        usuario.diasExpirar = Convert.ToInt32(linha["usu_dias_expirar"]);
                    }
                    if (linha["usu_dt_expiracao"] != DBNull.Value)
                    {
                        usuario.dataExpiracao = Convert.ToDateTime(linha["usu_dt_expiracao"]);
                    }

                    if (linha["usu_foto"] != DBNull.Value)
                    {
                        usuario.foto = (byte[])(linha["usu_foto"]);
                    }
                    else
                    {
                        usuario.foto = null;
                    }

                    usuarioCollection.Add(usuario);
                }

                return usuarioCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os usuários! \nDetalhes: " + ex.Message);
            }

        }

        //Pesquisa do frame de pesquisa do usuário
        public UsuarioCollection PesqUsuario(string codigo, string login, string perfil, string empresa)
        {
            try
            {
                //Instância o objeto coleção
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@login", "%"+login+"%");
                conexao.AdicionarParamentros("@perfil", perfil);
                //String de pesquisa
                string select = "select usu_codigo, usu_login, perf_descricao from wms_usuario usu " +
                    "left outer join wms_perfil prf " +
                    "on usu.perf_codigo = prf.perf_codigo " +
                    "where usu_status = 'True' and usu.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                if (!perfil.Equals(""))
                {
                    select += "and perf_descricao = @perfil ";
                }

                if (!login.Equals(""))
                {
                    select += "and usu_login like @login ";
                }

                if (!codigo.Equals(""))
                {
                    select += "and usu_codigo = @codigo";
                }

                //instância o datatable e executa a pesquisa
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Usuario usuario = new Usuario();
                    //Adiciona os valores encontrados
                    if (linha["usu_codigo"] != DBNull.Value)
                    {
                        usuario.codUsuario = Convert.ToInt32(linha["usu_codigo"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        usuario.login = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["perf_descricao"] != DBNull.Value)
                    {
                        usuario.perfil = Convert.ToString(linha["perf_descricao"]);
                    }

                    usuarioCollection.Add(usuario);
                }

                return usuarioCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os usuários! \nDetalhes: " + ex.Message);
            }

        }
    }
}
