using System;
using Dados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class EmpilhadorNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Método salvar cadastro
        public void Salvar(int codUsuario, int idRegiao, int idRuaInicial, int idRuaFinal, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idUsuario", codUsuario);
                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                conexao.AdicionarParamentros("@idRuaInicial", idRuaInicial);
                conexao.AdicionarParamentros("@idRuaFinal", idRuaFinal);
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de insert
                string insert = "insert into wms_empilhador (usu_codigo, reg_codigo, rua_codigo_inicial, rua_codigo_final, conf_codigo) " +
                        "values (@idUsuario, @idRegiao, @idRuaInicial, @idRuaFinal,  (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método alterar cadastro
        public void Alterar(int codEmpilhador, int idRegiao, int idRuaInicial, int idRuaFinal, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idEmpilhador", codEmpilhador);
                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                conexao.AdicionarParamentros("@idRuaInicial", idRuaInicial);
                conexao.AdicionarParamentros("@idRuaFinal", idRuaFinal);
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de insert
                string update = "update wms_empilhador set reg_codigo = @idRegiao , rua_codigo_inicial = @idRuaInicial, rua_codigo_final = @idRuaFinal " +
                        "where emp_codigo = @idEmpilhador and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }
        }


        //Método Remover
        public void Remover(string empresa, int codEmpilhador)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codEmpilhador", codEmpilhador);
                //String de exclusão
                string delete = "delete from wms_empilhador where emp_codigo = @codEmpilhador and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao remover os endereços. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa responsabilidades do empilhador
        public EmpilhadorCollection PesqResponsabilidadeEmpilhador(string empresa)
        {
            try
            {
                //Instância a coleção
                EmpilhadorCollection empilhadorCollection = new EmpilhadorCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select emp_codigo, usu_codigo, r.reg_numero, ri.rua_numero as rua_inicial, rf.rua_numero as rua_final from wms_empilhador e " +
                                "inner join wms_regiao r " +
                                "on r.reg_codigo = e.reg_codigo " +
                                "inner join wms_rua ri " +
                                "on ri.rua_codigo = e.rua_codigo_inicial " +
                                "inner join wms_rua rf " +
                                "on rf.rua_codigo = e.rua_codigo_final " +
                                "and e.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by r.reg_numero, ri.rua_numero, rf.rua_numero";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Empilhador empilhador = new Empilhador();
                    //Adiciona os valores encontrados
                    if (linha["emp_codigo"] != DBNull.Value)
                    {
                        empilhador.codEmpilhador = Convert.ToInt32(linha["emp_codigo"]);
                    }
                    if (linha["usu_codigo"] != DBNull.Value)
                     {
                         empilhador.codUsuario = Convert.ToInt32(linha["usu_codigo"]);
                     }
                     if (linha["reg_numero"] != DBNull.Value)
                     {
                         empilhador.regiao = Convert.ToInt32(linha["reg_numero"]);
                     }
                     if (linha["rua_inicial"] != DBNull.Value)
                     {
                         empilhador.ruaInicial = Convert.ToInt32(linha["rua_inicial"]);
                     }
                     if (linha["rua_final"] != DBNull.Value)
                     {
                         empilhador.ruafinal = Convert.ToInt32(linha["rua_final"]);
                     }

                    //Adiciona os cadastros encontrados a coleção
                    empilhadorCollection.Add(empilhador);
                }
                //Retorna a coleção
                return empilhadorCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as informações. \nDetalhes:" + ex.Message);
            }
        }
    }
}
