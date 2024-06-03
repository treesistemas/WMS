using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class CaixaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa um novo id
        public Caixa PesqId()
        {
            try
            {
                //Instância a camada de objetos
                Caixa caixa = new Caixa();
                //String de consulta
                string select = "select gen_id(gen_wms_caixa,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    caixa.codCaixa = Convert.ToInt32(linha["id"]);
                }
                //Retorna o valor encontrado
                return caixa;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os modelos de caixa
        public CaixaCollection PesqCaixa()
        {
            try
            {
                //Instância a camada de objetos
                CaixaCollection caixaCollection = new CaixaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select caixa_codigo, caixa_descricao, caixa_largura, caixa_altura, caixa_comprimento, caixa_cubagem, caixa_peso from wms_caixa order by caixa_codigo ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Caixa caixa = new Caixa();
                    //Adiciona os valores encontrados
                    if (linha["caixa_codigo"] != DBNull.Value)
                    {
                        caixa.codCaixa = Convert.ToInt32(linha["caixa_codigo"]);
                    }

                    if (linha["caixa_descricao"] != DBNull.Value)
                    {
                        caixa.descCaixa = Convert.ToString(linha["caixa_descricao"]);
                    }

                    if (linha["caixa_altura"] != DBNull.Value)
                    {
                        caixa.altura = Convert.ToDouble(linha["caixa_altura"]);
                    }

                    if (linha["caixa_largura"] != DBNull.Value)
                    {
                        caixa.largura = Convert.ToDouble(linha["caixa_largura"]);
                    }

                    if (linha["caixa_comprimento"] != DBNull.Value)
                    {
                        caixa.comprimento = Convert.ToDouble(linha["caixa_comprimento"]);
                    }

                    if (linha["caixa_cubagem"] != DBNull.Value)
                    {
                        caixa.cubagem = Convert.ToDouble(linha["caixa_cubagem"]);
                    }

                    if (linha["caixa_peso"] != DBNull.Value)
                    {
                        caixa.peso = Convert.ToDouble(linha["caixa_peso"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    caixaCollection.Add(caixa);
                }
                //Retorna a coleção de cadastro encontrada
                return caixaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os modelos de caixas. \nDetalhes:" + ex.Message);
            }
        }

        //Método salvar cadastro
        public void Salvar(Caixa caixa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", caixa.codCaixa);
                conexao.AdicionarParamentros("@descricao", caixa.descCaixa);
                conexao.AdicionarParamentros("@altura", caixa.altura);
                conexao.AdicionarParamentros("@largura", caixa.largura);
                conexao.AdicionarParamentros("@comprimento", caixa.comprimento);
                conexao.AdicionarParamentros("@cubagem", caixa.cubagem);
                conexao.AdicionarParamentros("@peso", caixa.peso);

                //String de insert
                string insert = "insert into wms_caixa (caixa_codigo, caixa_descricao, caixa_altura, caixa_largura, caixa_comprimento, caixa_cubagem, caixa_peso) " +
                        "values (@codigo, @descricao, @altura, @largura, @comprimento, @cubagem, @peso)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método Alterar cadastro
        public void Alterar(Caixa caixa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", caixa.codCaixa);
                conexao.AdicionarParamentros("@descricao", caixa.descCaixa);
                conexao.AdicionarParamentros("@altura", caixa.altura);
                conexao.AdicionarParamentros("@largura", caixa.largura);
                conexao.AdicionarParamentros("@comprimento", caixa.comprimento);
                conexao.AdicionarParamentros("@cubagem", caixa.cubagem);
                conexao.AdicionarParamentros("@peso", caixa.peso);

                //String de atualização
                string update = "update wms_caixa set caixa_descricao = @descricao, caixa_altura = @altura, caixa_largura = @largura, caixa_comprimento = @comprimento, " +
                    "caixa_cubagem = @cubagem, caixa_peso = @peso  where caixa_codigo = @codigo";

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

