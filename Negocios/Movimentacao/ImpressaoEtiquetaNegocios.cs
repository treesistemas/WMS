using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class ImpressaoEtiquetaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o volume de flowrack
        public Pedido PesqVolumePedido(string codPedido)
        {
            try
            {
                //Instância um objêto
                Pedido pedido = new Pedido();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);

                //Pesquisa o pedido
                string select = "select distinct(r.rota_numero), " +
                               "p.ped_codigo, ped_fim_conferencia, ped_volume, " +
                               "c.cli_codigo ||''|| c.cli_nome as cli_nome, c.cli_endereco, c.cli_numero, b.bar_nome, ci.cid_nome " +
                               "from wms_pedido p " +
                               "inner join wms_cliente c " +
                               "on c.cli_id = p.cli_id " +
                               "inner join wms_bairro b " +
                               "on b.bar_codigo = c.bar_codigo " +
                               "inner join wms_cidade ci " +
                               "on ci.cid_codigo = b.cid_codigo " +
                               "inner join wms_rota r on r.rota_codigo = c.rota_codigo " +
                               "where p.ped_codigo = @codPedido ";

                //Executa o script no banco
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        pedido.rotaCliente = Convert.ToString(linha["rota_numero"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["ped_fim_conferencia"] != DBNull.Value)
                    {
                        pedido.fimConferencia = Convert.ToDateTime(linha["ped_fim_conferencia"]);
                    }

                    if (linha["ped_volume"] != DBNull.Value)
                    {
                        pedido.numeroVolume = Convert.ToString(linha["ped_volume"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        pedido.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        pedido.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        pedido.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        pedido.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        pedido.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }
                }

                //Retorna o objêto encontrada
                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro pesquisar um novo volume. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o volume dos pedidos
        public Pedido PesqVolumeFlowRack(string codVolume)
        {
            try
            {
                //Instância um objêto
                Pedido pedido = new Pedido();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codVolume", codVolume);

                //Pesquisa se existe volume dentro do volume conferido (O VOLUME SÓ POSSUI ITEM SE ESTIVIR CONFERIDO)
                string select = "select distinct(r.rota_numero), " +
                                "(select max(est_codigo) from wms_item_flowrack where iflow_barra = @codVolume) as est_codigo, " +
                                "ip.ped_codigo, ip.iflow_barra,  c.cli_codigo ||''|| c.cli_nome as cli_nome, c.cli_endereco, c.cli_numero, b.bar_nome, ci.cid_nome, " +
                                "iflow_numero as volume " +
                                "from wms_item_flowrack ip " +
                                "inner join wms_pedido p " +
                                "on ip.ped_codigo = p.ped_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade ci " +
                                "on ci.cid_codigo = b.cid_codigo " +
                                "inner join wms_rota r on r.rota_codigo = c.rota_codigo " +
                                "where iflow_barra = @codVolume ";

                //Executa o script no banco
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados

                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        pedido.rotaCliente = Convert.ToString(linha["rota_numero"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        pedido.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["volume"] != DBNull.Value)
                    {
                        pedido.numeroVolume = Convert.ToString(linha["volume"]);
                    }

                    if (linha["iflow_barra"] != DBNull.Value)
                    {
                        pedido.barraVolume = Convert.ToString(linha["iflow_barra"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        pedido.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        pedido.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        pedido.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        pedido.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        pedido.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }
                }

                //Retorna o objêto encontrada
                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro pesquisar um novo volume. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o volume não impresso do usuário
        public PedidoCollection PesqVolumeUsuario(int codUsuario)
        {
            try
            {               
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codUsuario", codUsuario);

                //Instância uma coleção de objêto
                PedidoCollection pedidoCollection = new PedidoCollection();

                //Pesquisa se existe volume dentro do volume conferido (O VOLUME SÓ POSSUI ITEM SE ESTIVIR CONFERIDO)
                string select = "select distinct(r.rota_numero), " +
                                "(select max(est_codigo) from wms_item_flowrack where iflow_barra = ip.iflow_barra) as est_codigo, " +
                                "ip.ped_codigo, ip.iflow_barra,  c.cli_codigo ||''|| c.cli_nome as cli_nome, c.cli_endereco, c.cli_numero, b.bar_nome, ci.cid_nome, " +
                                "iflow_numero as volume " +
                                "from wms_item_flowrack ip " +
                                "inner join wms_pedido p " +
                                "on ip.ped_codigo = p.ped_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade ci " +
                                "on ci.cid_codigo = b.cid_codigo " +
                                "inner join wms_rota r on r.rota_codigo = c.rota_codigo " +
                                "where iflow_criado between DATEADD( - 3 DAY TO CURRENT_DATE ) || ' 00:00:00' and current_date ||' 23:59:59' and ip.usu_codigo_conferente = @codUsuario and iflow_impresso is null and iflow_numero <> '01' ";

                //Executa o script no banco
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {                    
                    //Instância um objêto
                    Pedido pedido = new Pedido();

                    //Adiciona os valores encontrados
                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        pedido.rotaCliente = Convert.ToString(linha["rota_numero"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        pedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        pedido.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["volume"] != DBNull.Value)
                    {
                        pedido.numeroVolume = Convert.ToString(linha["volume"]);
                    }

                    if (linha["iflow_barra"] != DBNull.Value)
                    {
                        pedido.barraVolume = Convert.ToString(linha["iflow_barra"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        pedido.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        pedido.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        pedido.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        pedido.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        pedido.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    pedidoCollection.Add(pedido);
                }

                //Retorna o objêto encontrada
                return pedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro pesquisar os volumes para impressão. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza sttaus
        public void AtualizarSatatus(string codVolume)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codVolume", codVolume);                

                //String de atualização
                string update = "update wms_item_flowrack set iflow_impresso = 'True' where iflow_barra = @codVolume";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o status da impressão. \nDetalhes:" + ex.Message);
            }
        }

    }
}
