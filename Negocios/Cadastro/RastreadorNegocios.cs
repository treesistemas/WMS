using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class RastreadorNegocios
    {
        //Instância a conexão
        Conexao conexao = new Conexao();

        //Método pesquisa id
        public Rastreador PesqId()
        {
            try
            {
                //Instância o objeto
                Rastreador rastreador = new Rastreador();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select gen_id(gen_wms_rastreador,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    rastreador.codRastreador = Convert.ToInt32(linha["id"]);
                }

                return rastreador;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método salvar cadastro
        public void Salvar(Rastreador rastreador)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", rastreador.codRastreador);
                conexao.AdicionarParamentros("@numero", rastreador.numeroRastreador);
                conexao.AdicionarParamentros("@modo", rastreador.modoRastreador);
                conexao.AdicionarParamentros("@status", rastreador.statusRastreador);
                conexao.AdicionarParamentros("@observacao", rastreador.observacaoRastreador);
                //String de insert
                string insert = "insert into wms_rastreador (ras_codigo, ras_numero, ras_modo, ras_status, ras_observacao) " +
                        "values (@codigo, @numero, @modo, @status, @observacao)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método alterar cadastro
        public void Alterar(Rastreador rastreador)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", rastreador.codRastreador);
                conexao.AdicionarParamentros("@numero", rastreador.numeroRastreador);
                conexao.AdicionarParamentros("@modo", rastreador.modoRastreador);
                conexao.AdicionarParamentros("@status", rastreador.statusRastreador);
                conexao.AdicionarParamentros("@observacao", rastreador.observacaoRastreador);

                //String de atualização
                string update = "update wms_rastreador set ras_numero = @numero, ras_modo = @modo, ras_status =@status , ras_observacao = @observacao where ras_codigo = @codigo";

            

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }

        }

        //Método desvincula o rastreado do veículo
        public void DesvincularRastreador(int codRastreador)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", codRastreador);
                
                //String de atualização
                string update = "update wms_veiculo set ras_codigo = null where ras_codigo = @codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }

        }

        //Método pesquisa 
        public RastreadorCollection PesqRastreador(string numero, bool status)
        {
            try
            {
                //Instância a coleção
                RastreadorCollection rastreadorCollection = new RastreadorCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@numero", numero);
                conexao.AdicionarParamentros("@status", status);
                //String de consulta
                string select = "select ras_codigo, ras_numero, ras_status, ras_modo, ras_observacao from wms_rastreador where ras_status = @status ";

                if(!numero.Equals(""))
                {
                    select += "and ras_numero = @numero ";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância do objeto
                    Rastreador rastreador = new Rastreador();
                    //Adiciona os valores encontrados
                    if (linha["ras_codigo"] != DBNull.Value)
                    {
                        rastreador.codRastreador = Convert.ToInt32(linha["ras_codigo"]);
                    }

                    if (linha["ras_numero"] != DBNull.Value)
                    {
                        rastreador.numeroRastreador = Convert.ToInt32(linha["ras_numero"]);
                    }

                    if (linha["ras_modo"] != DBNull.Value)
                    {
                        rastreador.modoRastreador = Convert.ToString(linha["ras_modo"]);
                    }

                    if (linha["ras_status"] != DBNull.Value)
                    {
                        rastreador.statusRastreador = Convert.ToString(linha["ras_status"]);
                    }

                    if (linha["ras_observacao"] != DBNull.Value)
                    {
                        rastreador.observacaoRastreador = Convert.ToString(linha["ras_observacao"]);
                    }
                    //Adiciona os cadastros encontrados a coleção
                    rastreadorCollection.Add(rastreador);
                }
                //Retorna a coleção de cadastro encontrada
                return rastreadorCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os rastreadores \nDetalhes:" + ex.Message);
            }
        }

    }
}

