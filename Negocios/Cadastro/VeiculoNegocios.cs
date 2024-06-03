using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class VeiculoNegocios
    {
        //Instância a conexão
        Conexao conexao = new Conexao();
        public VeiculoCollection PesqVeiculos(bool status, string placa, string proprietario, string empresa)
        {
            try
            {
                //Instância a coleção
                VeiculoCollection veiculoCollection = new VeiculoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiona os parâmentos
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@placa", placa);
                conexao.AdicionarParamentros("@proprietario", proprietario);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select vei_codigo, vei_placa, vei_ano, vei_proprietario, tipo_descricao, ras_numero, " +
                                "tipo_altura, tipo_largura, tipo_comprimento, tipo_cubagem, tipo_peso, vei_status from wms_veiculo v " +
                                "left outer join wms_veiculo_tipo t " +
                                "on t.tipo_codigo = v.tipo_codigo " +
                                "left outer join wms_rastreador r " +
                                "on r.ras_codigo = v.ras_codigo " +
                                "where v.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and vei_status = @status ";

                if (!placa.Equals(""))
                {
                    select += "and vei_placa = @placa ";

                }
                else if (!proprietario.Equals(""))
                {
                    select += "and vei_proprietario = @proprietario ";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância uma nova unidade
                    Veiculo veiculo = new Veiculo();

                    //Adiciona os valores encontrados
                    if (linha["vei_codigo"] != DBNull.Value)
                    {
                        veiculo.codVeiculo = Convert.ToInt32(linha["vei_codigo"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        veiculo.placaVeiculo = Convert.ToString(linha["vei_placa"]);
                    }
                    if (linha["vei_ano"] != DBNull.Value)
                    {
                        veiculo.anoVeiculo = Convert.ToInt32(linha["vei_ano"]);
                    }

                    if (linha["vei_status"] != DBNull.Value)
                    {
                        veiculo.statusVeiculo = Convert.ToBoolean(linha["vei_status"]);
                    }

                    if (linha["vei_proprietario"] != DBNull.Value)
                    {
                        veiculo.proprietarioVeiculo = Convert.ToString(linha["vei_proprietario"]);
                    }

                    if (linha["ras_numero"] != DBNull.Value)
                    {
                        veiculo.numeroRastreador = Convert.ToInt32(linha["ras_numero"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        veiculo.descTipo = Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["tipo_altura"] != DBNull.Value)
                    {
                        veiculo.alturaVeiculo = Convert.ToDouble(linha["tipo_altura"]);
                    }

                    if (linha["tipo_largura"] != DBNull.Value)
                    {
                        veiculo.larguraVeiculo = Convert.ToDouble(linha["tipo_largura"]);
                    }

                    if (linha["tipo_comprimento"] != DBNull.Value)
                    {
                        veiculo.comprimentoVeiculo = Convert.ToDouble(linha["tipo_comprimento"]);
                    }

                    if (linha["tipo_cubagem"] != DBNull.Value)
                    {
                        veiculo.cubagemVeiculo = Convert.ToDouble(linha["tipo_cubagem"]);
                    }

                    if (linha["tipo_peso"] != DBNull.Value)
                    {
                        veiculo.pesoVeiculo = Convert.ToDouble(linha["tipo_peso"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    veiculoCollection.Add(veiculo);
                }
                //Retorna a coleção de cadastro encontrada
                return veiculoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os veículos \nDetalhes:" + ex.Message);
            }
        }

        //Método alterar cadastro
        public void Alterar(string empresa, Veiculo veiculo)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codigo", veiculo.codVeiculo);
                conexao.AdicionarParamentros("@proprietario", veiculo.proprietarioVeiculo);
                conexao.AdicionarParamentros("@tipo", veiculo.codTipo);

                if (veiculo.codRastreador == 0)
                {
                    conexao.AdicionarParamentros("@rastreador", null);
                }
                else
                {
                    conexao.AdicionarParamentros("@rastreador", veiculo.codRastreador);
                }

                //String de atualização
                string update = "update wms_veiculo set vei_proprietario = @proprietario, ras_codigo = @rastreador, tipo_codigo = @tipo " +
                                "where vei_codigo = @codigo and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

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

