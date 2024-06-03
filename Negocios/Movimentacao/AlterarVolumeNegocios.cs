using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class AlterarVolumeNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa os itens do volume de flowrack
        public ItensFlowRackCollection PesqItensFlowRack(string codPedido, string codProduto, string empresa)
        {
            try
            {                
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@empresa", empresa);
                //Pesquisa
                string select = "select rast_codigo, ped_codigo, r.prod_id, prod_codigo, iflow_barra, iflow_numero " +
                                "from wms_rastreamento_flowrack r " +
                                "inner join wms_produto p "+
                                "on r.prod_id = p.prod_id "+
                                "where ped_codigo = @codPedido " +
                                "and iflow_barra >= (select distinct(iflow_barra) from wms_rastreamento_flowrack where ped_codigo = @codPedido " +
                                "and prod_id = (select prod_id from wms_produto where prod_codigo = @codProduto)) " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)" +
                                "order by iflow_barra ";

                //Executa o script no banco
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância uma coleção de objêto
                ItensFlowRackCollection itensCollection = new ItensFlowRackCollection();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância um objêto
                    ItensFlowRack itens = new ItensFlowRack();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itens.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["iflow_barra"] != DBNull.Value)
                    {
                        itens.barraVolume = Convert.ToString(linha["iflow_barra"]);
                    }

                    if (linha["iflow_numero"] != DBNull.Value)
                    {
                        itens.numeroVolume = Convert.ToInt32(linha["iflow_numero"]);
                    }                    

                    //Adiciona o objêto a coleção
                    itensCollection.Add(itens);
                }

                //Retorna o objêto encontrada
                return itensCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro pesquisar os itens do flow rack. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens do volume de flowrack
        public int PesqQtdItemVolume(string iflowBarra)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@iflowBarra", iflowBarra);

                //Pesquisa
                string select = "select count(iflow_barra) as quantidade from wms_rastreamento_flowrack r where iflow_barra = @iflowBarra";

                //Executa o script no banco
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância uma coleção de objêto
                int quantidade = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância um objêto
                    ItensFlowRack itens = new ItensFlowRack();

                    if (linha["quantidade"] != DBNull.Value)
                    {
                        quantidade = Convert.ToInt32(linha["quantidade"]);
                    }                   
                }

                //Retorna o objêto encontrada
                return quantidade;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro pesquisar a quantidade de item dentro do volume. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens do volume para reduzir
        public ItensFlowRackCollection PesqItensVolume(string codPedido, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@empresa", empresa);

                //Pesquisa
                string select = "select rast_codigo, ped_codigo, r.prod_id, iflow_barra, iflow_numero "+
                               "from wms_rastreamento_flowrack r "+
                               "where ped_codigo = @codPedido and iflow_numero = (select max(distinct(iflow_numero)) from wms_rastreamento_flowrack  where ped_codigo = @codPedido) " +
                               "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by iflow_barra";

                //Executa o script no banco
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Instância uma coleção de objêto
                ItensFlowRackCollection itensCollection = new ItensFlowRackCollection();

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância um objêto
                    ItensFlowRack itens = new ItensFlowRack();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itens.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["iflow_barra"] != DBNull.Value)
                    {
                        itens.barraVolume = Convert.ToString(linha["iflow_barra"]);
                    }

                    if (linha["iflow_numero"] != DBNull.Value)
                    {
                        itens.numeroVolume = Convert.ToInt32(linha["iflow_numero"]);
                    }

                    //Adiciona o objêto a coleção
                    itensCollection.Add(itens);
                }

                //Retorna o objêto encontrada
                return itensCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro pesquisar os itens do flow rack. \nDetalhes:" + ex.Message);
            }
        }


        //Altera o volume
        public void AlterarVolume(int codPedido, int idProduto, string barraVolume, string numeroVolume, string empresa)
        {
            //Limpa os parâmetros
            conexao.LimparParametros();
            //Adiciona os parâmetros
            conexao.AdicionarParamentros("@codPedido", codPedido);
            conexao.AdicionarParamentros("@idProduto", idProduto);
            conexao.AdicionarParamentros("@barraVolume", barraVolume);
            conexao.AdicionarParamentros("@numeroVolume", numeroVolume);
            conexao.AdicionarParamentros("@empresa", empresa);

            //Atualiza os volumes
            string updateFlowrack = "update wms_item_flowrack set iflow_barra = @barraVolume, iflow_numero = @numeroVolume " +
                "where ped_codigo = @codPedido and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

            //Executa
            conexao.ExecuteManipulacao(CommandType.Text, updateFlowrack);

            //Atualiza os volumes
            string updateRastreamento = "update wms_rastreamento_flowrack set iflow_barra = @barraVolume, iflow_numero = @numeroVolume " +
                "where ped_codigo = @codPedido and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

            //Executa
            conexao.ExecuteManipulacao(CommandType.Text, updateRastreamento);
        }

        //Pesquisa um novo volume
        public Pedido PesqVolume(string codVolume, string empresa)
        {
            try
            {
                //Instância um objêto
                Pedido pedido = new Pedido();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codVolume", codVolume);
                conexao.AdicionarParamentros("@empresa", empresa);

                //Pesquisa se existe volume dentro do volume conferido (O VOLUME SÓ POSSUI ITEM SE ESTIVIR CONFERIDO)
                string select = "select distinct(r.rota_numero), ip.ped_codigo, ip.iflow_barra,  c.cli_codigo ||''|| c.cli_nome as cli_nome, c.cli_endereco, c.cli_numero, b.bar_nome, ci.cid_nome, " +
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
                                "where iflow_barra = @codVolume and ip.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

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

    }
}
