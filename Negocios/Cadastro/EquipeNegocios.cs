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
    public class EquipeNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa um novo id da equipe
        public int PesqId()
        {
            try
            {
                //Variável
                int codigo = 0;
                //String de consulta
                string select = "select gen_id(gencdgqp,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valor encontrado
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
        public void Salvar(int codEquipe, string descEquipe, int codUsuario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idEquipe", codEquipe);
                conexao.AdicionarParamentros("@descricao", descEquipe);
                conexao.AdicionarParamentros("@idUsuario", codUsuario);

                //String de insert
                string insert = "insert into briwqp (cdgqp, qpdsc, cdgsr) " +
                        "values (@idEquipe, @descricao, @idUsuario)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método Alterar cadastro
        public void Alterar(int codEquipe, string descEquipe, int codUsuario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idEquipe", codEquipe);
                conexao.AdicionarParamentros("@descricao", descEquipe);
                conexao.AdicionarParamentros("@idUsuario", codUsuario);

                //String de atualização
                string update = "update briwqp set qpdsc = @descricao, cdgsr = @idUsuario " +
                    "where cdgqp = @idEquipe";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método adicionar integrantes
        public void Adicionar(int codEquipe, int codUsuario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idEquipe", codEquipe);
                conexao.AdicionarParamentros("@idUsuario", codUsuario);

                //String de insert
                string insert = "insert into briwqpntgrnt (cdgqp, cdgsr) " +
                        "values (@idEquipe, @idUsuario)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar um novo integrante. \nDetalhes:" + ex.Message);
            }
        }


        //Método remover cadastro
        public void Remover(int codUsuario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idUsuario", codUsuario);
                
                //String - excluir
                string delete = "delete from briwqpntgrnt where cdgsr = @idUsuario";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao remover cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa equipe
        public EquipeCollection PesqSeparador()
        {
            try
            {
                //Instância a coleção
                EquipeCollection equipeCollection = new EquipeCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select codsr, srlgn, p.prfdsc from briwsr u "+
                                "inner join briwprf p "+
                                "on u.codprf = p.codprf "+
                                "where prfdsc = 'SEPARADOR'  and codsr not in (select cdgsr from briwqpntgrnt)";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Equipe equipe = new Equipe();
                    //Adiciona os valores encontrados
                    equipe.codUsuario = Convert.ToInt32(linha["codsr"]);
                    equipe.nmUsuario = Convert.ToString(linha["srlgn"]);
                    equipe.perfil = Convert.ToString(linha["prfdsc"]);

                    //Adiciona os cadastros encontrados a coleção
                    equipeCollection.Add(equipe);
                }
                //Retorna a coleção de cadastro encontrada
                return equipeCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as equipes. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa equipe
        public EquipeCollection PesqEquipe()
        {
            try
            {
                //Instância a coleção
                EquipeCollection equipeCollection = new EquipeCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select u.codsr, cdgqp, qpdsc, srlgn, prfdsc from briwqp e "+
                    "inner join briwsr u "+
                    "on e.cdgsr = u.codsr " +
                    "inner join briwprf p " +
                    "on p.codprf = u.codprf " +
                    "order by cdgqp";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Equipe equipe = new Equipe();
                    //Adiciona os valores encontrados
                    equipe.codUsuario = Convert.ToInt32(linha["codsr"]);
                    equipe.codEquipe = Convert.ToInt32(linha["cdgqp"]);
                    equipe.descEquipe = Convert.ToString(linha["qpdsc"]);
                    equipe.nmUsuario = Convert.ToString(linha["srlgn"]);
                    equipe.perfil = Convert.ToString(linha["prfdsc"]);

                    //Adiciona os cadastros encontrados a coleção
                    equipeCollection.Add(equipe);
                }
                //Retorna a coleção de cadastro encontrada
                return equipeCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as equipes. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa equipe
        public EquipeCollection PesqEquipeIntegrante()
        {
            try
            {
                //Instância a coleção
                EquipeCollection equipeCollection = new EquipeCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select u.codsr, cdgqp, srlgn, prfdsc from briwqpntgrnt e " +
                    "inner join briwsr u " +
                    "on e.cdgsr = u.codsr " +
                    "inner join briwprf p " +
                    "on p.codprf = u.codprf " +
                    "order by cdgqp";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Equipe equipe = new Equipe();
                    //Adiciona os valores encontrados
                    equipe.codUsuario = Convert.ToInt32(linha["codsr"]);
                    equipe.codEquipe = Convert.ToInt32(linha["cdgqp"]);
                    equipe.nmUsuario = Convert.ToString(linha["srlgn"]);
                    equipe.perfil = Convert.ToString(linha["prfdsc"]);

                    //Adiciona os cadastros encontrados a coleção
                    equipeCollection.Add(equipe);
                }
                //Retorna a coleção de cadastro encontrada
                return equipeCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os integrantes das equipes. \nDetalhes:" + ex.Message);
            }
        }
    }
}
