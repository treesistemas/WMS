using Dados;
using ObjetoTransferencia;
using System;
using System.Data;

namespace Negocios
{
    public class LoginNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa login
        public EmpresaCollection PesqEmpresa()
        {
            try
            {
                //Instância o objêto
                EmpresaCollection empresaCollection = new EmpresaCollection();

                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select conf_codigo, conf_empresa, conf_sigla, (SELECT count(conf_codigo) as qtd_empresa from wms_configuracao) as qtd_empresa from wms_configuracao group by conf_codigo, conf_empresa, conf_sigla";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Empresa empresa = new Empresa();

                    if (linha["conf_codigo"] != DBNull.Value)
                    {
                        empresa.codEmpresa = Convert.ToInt32(linha["conf_codigo"]);
                    }

                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        empresa.nomeEmpresa = Convert.ToString(linha["conf_empresa"]);
                    }

                    if (linha["conf_sigla"] != DBNull.Value)
                    {
                        empresa.siglaEmpresa = Convert.ToString(linha["conf_sigla"]);
                    }

                    if (linha["qtd_empresa"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(linha["qtd_empresa"]) > 1)
                        {
                            empresa.multiEmpresa = true;
                        }
                        else
                        {
                            empresa.multiEmpresa = false;
                        }
                    }

                    empresaCollection.Add(empresa);

                }

                //Retorna o objêto
                return empresaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as empresas. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa login
        public Login PesqLogin(string login, string senha, string empresa, bool multempresa)
        {
            try
            {
                //Instância o objêto
                Login log = new Login();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@login", login);
                conexao.AdicionarParamentros("@senha", senha);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@multempresa", multempresa);

                //String de consulta
                string select = "select usu_codigo, usu_login, usu_senha, u.perf_codigo, perf_descricao, usu_dt_expiracao, usu_foto, " +
                                "(select est_codigo from wms_estacao where usu_codigo = u.usu_codigo and conf_codigo = u.conf_codigo) as est_codigo, " +
                                "(select est_nivel from wms_estacao where usu_codigo = u.usu_codigo and conf_codigo = u.conf_codigo) as est_nivel, " +
                                "(select conf_empresa from wms_configuracao where conf_sigla = @empresa) as empresa, " +
                                "(select iconf_status from wms_itens_configuracao where iconf_descricao = 'EXIBIR A SEQUENCIA DO PEDIDO DO ROTEIRIZADOR') as controla_sequencia, " +
                                "(select iconf_status from wms_itens_configuracao where iconf_descricao = 'IMPRESSORA ARGOX 214') as imp_argox, " +
                                "(select iconf_status from wms_itens_configuracao where iconf_descricao = 'IMPRESSORA ARGOX 214 PLUS') as imp_argox_plus, " +
                                "(select iconf_status from wms_itens_configuracao where iconf_descricao = 'IMPRESSORA ZEBRA') as imp_zebra " +
                                "from wms_usuario u " +
                                "left join wms_perfil p " +
                                "on p.perf_codigo = u.perf_codigo " +
                                "where usu_login = @login and usu_senha = @senha ";

                if(multempresa == true)
                {
                    select += "and u.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }
                
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["usu_codigo"] != DBNull.Value)
                    {
                        log.codUsuario = Convert.ToInt32(linha["usu_codigo"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        log.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["usu_senha"] != DBNull.Value)
                    {
                        log.senhaUsuario = Convert.ToString(linha["usu_senha"]);
                    }

                    if (linha["perf_codigo"] != DBNull.Value)
                    {
                        log.codPerfil = Convert.ToInt32(linha["perf_codigo"]);
                    }

                    if (linha["perf_descricao"] != DBNull.Value)
                    {
                        log.descPerfil = Convert.ToString(linha["perf_descricao"]);
                    }

                    if (linha["usu_dt_expiracao"] != DBNull.Value)
                    {
                        log.dataExpiracao = Convert.ToDateTime(linha["usu_dt_expiracao"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        log.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["est_nivel"] != DBNull.Value)
                    {
                        log.nivelEstacao = Convert.ToInt32(linha["est_nivel"]);
                    }

                    if (linha["empresa"] != DBNull.Value)
                    {
                        log.nomeEmpresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["controla_sequencia"] != DBNull.Value)
                    {
                        log.controlaSequenciaCarregamento = Convert.ToString(linha["controla_sequencia"]);
                    }

                    if (linha["imp_argox"] != DBNull.Value && linha["imp_argox_plus"] != DBNull.Value && linha["imp_zebra"] != DBNull.Value)
                    {
                        if (Convert.ToBoolean(linha["imp_argox"]) == true)
                        {
                            log.impressora = "ARGOX 214";
                        }

                        if (Convert.ToBoolean(linha["imp_argox_plus"]) == true)
                        {
                            log.impressora = "ARGOX 214 PLUS";
                        }

                        if (Convert.ToBoolean(linha["imp_zebra"]) == true)
                        {
                            log.impressora = "ZEBRA";
                        }
                    }

                    if (linha["usu_foto"] != DBNull.Value)
                    {
                        log.foto = (byte[])(linha["usu_foto"]);
                    }
                    else
                    {
                        log.foto = null;
                    }
                }

                //Retorna o objêto
                return log;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o login. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa acesso
        public AcessoCollection PesqAcessos(int codUsuario)
        {
            try
            {
                //Instância o objêto
                AcessoCollection funcaoCollection = new AcessoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codUsuario", codUsuario);

                //String de consulta
                string select = "select u.fun_descricao, uf.fun_ler, uf.fun_escrever, uf.fun_editar, uf.fun_excluir from wms_usuxfun uf " +
                                "inner join wms_funcao u  " +
                                "on u.fun_codigo = uf.fun_codigo  " +
                                "where usu_codigo = @codUsuario";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Acesso funcao = new Acesso();

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

                    //Adiciona os dados
                    funcaoCollection.Add(funcao);
                }
                //Retorna o objêto
                return funcaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os acessos do usuário. \nDetalhes:" + ex.Message);
            }
        }


    }
}
