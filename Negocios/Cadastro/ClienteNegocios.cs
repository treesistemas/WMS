using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class ClienteNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa as categorias
        public ClienteCollection PesqCliente(string codigo, string cnpj, string razaoSocial, bool status, string empresa)
        {
            try
            {
                //Instância a coleção
                ClienteCollection clienteCollection = new ClienteCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //adiciona parâmetros
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@cnpj", cnpj);
                conexao.AdicionarParamentros("@razaoSocial", razaoSocial);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select distinct(cli_id), cli_codigo, cli_nome, cli_fantasia, cli_cnpj, b.bar_nome, ci.cid_nome, e.est_uf, cli_endereco, cli_numero, " +
                                "cli_cep, r.rota_numero, r.rota_descricao, c.cli_sequencia, cli_fone, cli_celular, cli_email, cli_agendamento, cli_auditar, cli_validade, cli_validade_dias, cli_caixa_fechada, " +
                                "cli_compartilhada, cli_nao_dividir_carga, cli_paletizado, cli_observacao, cli_status from wms_cliente c " +
                                "left join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo "+
                                "left join wms_cidade ci " +
                                "on ci.cid_codigo = b.cid_codigo " +
                                "left join wms_estado e " +
                                "on e.est_codigo = ci.est_codigo "+
                                "left join wms_rota r "+
                                "on r.rota_codigo = b.rota_codigo ";

                if(codigo != string.Empty)
                {
                    select += "where cli_status = @status and cli_codigo = @codigo and c.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by cli_codigo";
                }
                else if (razaoSocial != string.Empty)
                {
                    select += $"where cli_status = @status and cli_nome like '{razaoSocial}%' and c.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by cli_codigo";
                }
                else if (cnpj != string.Empty)
                {
                    select += "where cli_status = @status and cli_cnpj = @cnpj and c.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by cli_codigo";
                }


                //select += "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";



                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Cliente cliente = new Cliente();
                    //Adiciona os valores encontrados
                    if (linha["cli_id"] != DBNull.Value)
                    {
                        cliente.idCliente = Convert.ToInt32(linha["cli_id"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        cliente.codCliente = Convert.ToInt32(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        cliente.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        cliente.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["cli_cnpj"] != DBNull.Value)
                    {
                        cliente.cnpjCliente = Convert.ToString(linha["cli_cnpj"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        cliente.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        cliente.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        cliente.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        cliente.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        cliente.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        cliente.rotaCliente = Convert.ToString(linha["rota_numero"]) + " - " + Convert.ToString(linha["rota_descricao"]);
                    }

                    if (linha["cli_sequencia"] != DBNull.Value)
                    {
                        cliente.seqEntregaCliente = Convert.ToInt32(linha["cli_sequencia"]);
                    }                   

                    if (linha["cli_celular"] != DBNull.Value)
                    {
                        cliente.celularCliente = Convert.ToString(linha["cli_celular"]);
                    }

                    //Aqui ele trata as informações de cliente que só tem um campo de telefone
                    if (linha["cli_fone"] != DBNull.Value)
                    {
                        //se for telefone
                        if (Convert.ToString(linha["cli_fone"]).Length == 10)
                        {
                            cliente.foneCliente = Convert.ToString(linha["cli_fone"]);
                        }
                        //se for celular
                        else if (Convert.ToString(linha["cli_fone"]).Length == 11)
                        {
                            cliente.celularCliente = Convert.ToString(linha["cli_fone"]);
                        }
                    }

                    if (linha["cli_email"] != DBNull.Value)
                    {
                        cliente.emailCliente = Convert.ToString(linha["cli_email"]);
                    }

                    if (linha["cli_agendamento"] != DBNull.Value)
                    {
                        cliente.agendamentoCliente = Convert.ToBoolean(linha["cli_agendamento"]);
                    }

                    if (linha["cli_auditar"] != DBNull.Value)
                    {
                        cliente.auditoriaPedido = Convert.ToBoolean(linha["cli_auditar"]);
                    }

                    if (linha["cli_validade"] != DBNull.Value)
                    {
                        cliente.validadeCliente = Convert.ToBoolean(linha["cli_validade"]);
                    }

                    if (linha["cli_validade_dias"] != DBNull.Value)
                    {
                        cliente.diasValidadeCliente = Convert.ToInt32(linha["cli_validade_dias"]);
                    }

                    if (linha["cli_caixa_fechada"] != DBNull.Value)
                    {
                        cliente.caixaFechadaCliente = Convert.ToBoolean(linha["cli_caixa_fechada"]);
                    }

                    if (linha["cli_compartilhada"] != DBNull.Value)
                    {
                        cliente.compartilhado = Convert.ToBoolean(linha["cli_compartilhada"]);
                    }

                    if (linha["cli_paletizado"] != DBNull.Value)
                    {
                        cliente.paletizadoCliente = Convert.ToBoolean(linha["cli_paletizado"]);
                    }

                    if (linha["cli_nao_dividir_carga"] != DBNull.Value)
                    {
                        cliente.naoDividirCarga = Convert.ToBoolean(linha["cli_nao_dividir_carga"]);
                    }

                    if (linha["cli_observacao"] != DBNull.Value)
                    {
                        cliente.observacaoCliente = Convert.ToString(linha["cli_observacao"]);
                    }

                    if (linha["cli_status"] != DBNull.Value)
                    {
                        cliente.statusCliente = Convert.ToBoolean(linha["cli_status"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    clienteCollection.Add(cliente);
                }
                //Retorna a coleção de cadastro encontrada
                return clienteCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o cliente. \n Detalhes:" + ex.Message);
            }
        }

        //Pesquisa as categorias
        public ClienteCollection PesqClienteUnificado(int idCliente)
        {
            try
            {
                //Instância a coleção
                ClienteCollection clienteCollection = new ClienteCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //adiciona parâmetros
                conexao.AdicionarParamentros("@idCliente", idCliente);
                
                //String de consulta
                string select = "select cli_id, cli_codigo, cli_nome, cli_cnpj from wms_cliente where cli_id_unificar = @idCliente ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Cliente cliente = new Cliente();
                    //Adiciona os valores encontrados
                    if (linha["cli_id"] != DBNull.Value)
                    {
                        cliente.idCliente = Convert.ToInt32(linha["cli_id"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        cliente.codCliente = Convert.ToInt32(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        cliente.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_cnpj"] != DBNull.Value)
                    {
                        cliente.cnpjCliente = Convert.ToString(linha["cli_cnpj"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    clienteCollection.Add(cliente);
                }
                //Retorna a coleção de cadastro encontrada
                return clienteCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os clientes unificados. \n Detalhes:" + ex.Message);
            }
        }


        //Alterar cadastro
        public void Alterar(string empresa, int idCliente, bool agendamento, bool auditar, bool validade, int diasValidade, bool caixaFechada, bool compartilhado, bool paletizado, bool naoDividirCarga, string observacao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@idCliente", idCliente);
                conexao.AdicionarParamentros("@agendamento", agendamento);
                conexao.AdicionarParamentros("@auditar", auditar);
                conexao.AdicionarParamentros("@validade", validade);
                conexao.AdicionarParamentros("@diasValidade", diasValidade);
                conexao.AdicionarParamentros("@caixaFechada", caixaFechada);
                conexao.AdicionarParamentros("@compartilhado", compartilhado);
                conexao.AdicionarParamentros("@paletizado", paletizado);
                conexao.AdicionarParamentros("@naoDividirCarga", naoDividirCarga);
                conexao.AdicionarParamentros("@observacao", observacao);

                //String de atualização
                string update = "update wms_cliente set cli_agendamento = @agendamento, cli_auditar = @auditar, cli_validade = @validade, cli_validade_dias = @diasValidade, cli_caixa_fechada = @caixaFechada, " +
                    "cli_compartilhada = @compartilhado, cli_paletizado = @paletizado, cli_nao_dividir_carga = @naoDividirCarga, cli_observacao = @observacao " +
                    "where cli_id = @idCliente and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";


                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar as observações. \nDetalhes:" + ex.Message);
            }
        }

        //Alterar cadastro
        public void AtualizarUnificacao(int idCliente, int? idClienteUnificar)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idCliente", idCliente);
                conexao.AdicionarParamentros("@idClienteUnificar", idClienteUnificar);

                //String de atualização
                string update = "update wms_cliente set cli_id_unificar = @idClienteUnificar where cli_id = @idCliente ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar a unificação. \nDetalhes:" + ex.Message);
            }
        }



    }
}

