using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class VeiculoTipoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa um novo id da categoria
        public VeiculoTipo PesqId()
        {
            try
            {
                //Instância a coleção
                VeiculoTipo veiculoTipo = new VeiculoTipo();
                //String de consulta
                string select = "select gen_id(gen_wms_veiculo_tipo,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    veiculoTipo.codTipoVeiculo = Convert.ToInt32(linha["id"]);
                }
                //Retorna a coleção de cadastro encontrada
                return veiculoTipo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os tipos de veiculos
        public VeiculoTipoCollection PesqTipoVeiculo()
        {
            try
            {
                //Instância a coleção
                VeiculoTipoCollection veiculoTipoCollection = new VeiculoTipoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select tipo_codigo, tipo_descricao, tipo_largura, tipo_altura, tipo_comprimento, tipo_cubagem, tipo_peso from wms_veiculo_tipo order by tipo_descricao, tipo_codigo ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    VeiculoTipo veiculoTipo = new VeiculoTipo();
                    //Adiciona os valores encontrados
                    if (linha["tipo_codigo"] != DBNull.Value)
                    {
                        veiculoTipo.codTipoVeiculo = Convert.ToInt32(linha["tipo_codigo"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        veiculoTipo.descTipoVeiculo = Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["tipo_altura"] != DBNull.Value)
                    {
                        veiculoTipo.altura = Convert.ToDouble(linha["tipo_altura"]);
                    }

                    if (linha["tipo_largura"] != DBNull.Value)
                    {
                        veiculoTipo.largura = Convert.ToDouble(linha["tipo_largura"]);
                    }                   

                    if (linha["tipo_comprimento"] != DBNull.Value)
                    {
                        veiculoTipo.comprimento = Convert.ToDouble(linha["tipo_comprimento"]);
                    }

                    if (linha["tipo_cubagem"] != DBNull.Value)
                    {
                        veiculoTipo.cubagem = Convert.ToDouble(linha["tipo_cubagem"]);
                    }

                    if (linha["tipo_peso"] != DBNull.Value)
                    {
                        veiculoTipo.peso = Convert.ToDouble(linha["tipo_peso"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    veiculoTipoCollection.Add(veiculoTipo);
                }
                //Retorna a coleção de cadastro encontrada
                return veiculoTipoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os tipos de veículos. \nDetalhes:" + ex.Message);
            }
        }

        //Método salvar cadastro
        public void Salvar(VeiculoTipo tipo)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codTipo", tipo.codTipoVeiculo);
                conexao.AdicionarParamentros("@descricao", tipo.descTipoVeiculo);
                conexao.AdicionarParamentros("@altura", tipo.altura);
                conexao.AdicionarParamentros("@largura", tipo.largura);
                conexao.AdicionarParamentros("@comprimento", tipo.comprimento);
                conexao.AdicionarParamentros("@cubagem", tipo.cubagem);
                conexao.AdicionarParamentros("@peso", tipo.peso);

                //String de insert
                string insert = "insert into wms_veiculo_tipo (tipo_codigo, tipo_descricao, tipo_altura, tipo_largura, tipo_comprimento, tipo_cubagem, tipo_peso) " +
                        "values (@codTipo, @descricao, @altura, @largura, @comprimento, @cubagem, @peso)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método Alterar cadastro
        public void Alterar(VeiculoTipo tipo)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codTipo", tipo.codTipoVeiculo);
                conexao.AdicionarParamentros("@descricao", tipo.descTipoVeiculo);
                conexao.AdicionarParamentros("@altura", tipo.altura);
                conexao.AdicionarParamentros("@largura", tipo.largura);
                conexao.AdicionarParamentros("@comprimento", tipo.comprimento);
                conexao.AdicionarParamentros("@cubagem", tipo.cubagem);
                conexao.AdicionarParamentros("@peso", tipo.peso);

                //String de atualização
                string update = "update wms_veiculo_tipo set tipo_descricao = @descricao, tipo_altura = @altura, tipo_largura = @largura, tipo_comprimento = @comprimento, " +
                    "tipo_cubagem = @cubagem, tipo_peso = @peso  where tipo_codigo = @codTipo";


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
