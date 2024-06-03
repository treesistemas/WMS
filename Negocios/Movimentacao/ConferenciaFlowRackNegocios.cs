using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class ConferenciaFlowRackNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa as restrições do cliente
        public PedidoCollection PesqPedidosNaoProcessados(int codEstacao)
        {
            try
            {
                //Instância uma coleção de objêtos
                PedidoCollection pedidoCollection = new PedidoCollection();

                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codEstacao", codEstacao);

                //Pesquisa os itens
                string select = "select distinct(r.rota_numero), ip.ped_codigo, iflow_numero, ip.iflow_barra, c.cli_codigo ||'-'|| c.cli_nome as cli_nome, c.cli_endereco, c.cli_numero, b.bar_nome, ci.cid_nome " +
                                "from wms_item_flowrack ip " +
                                "inner join wms_pedido p " +
                                "on ip.ped_codigo = p.ped_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade ci " +
                                "on ci.cid_codigo = b.cid_codigo " +
                                "inner join wms_rota r " +
                                "on r.rota_codigo = c.rota_codigo " +
                                "inner join wms_estacao e " +
                                "on e.est_codigo = ip.est_codigo " +
                                "where e.est_tipo = 'Fixa' and ip.est_codigo = @codEstacao and ip.iflow_numero = '01' and ip.iflow_criado is null and iflow_nivel = 'PRINT' " +
                                "or e.est_tipo = 'Fixa' and ip.est_codigo = @codEstacao and iflow_nivel = 'True' ";

                //"where ip.est_codigo = @codEstacao and ip.iflow_numero = '01' and ip.iflow_criado is null and iflow_nivel = 'PRINT' " +
                //"or ip.est_codigo = @codEstacao and iflow_nivel = 'True'";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
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

                    if (linha["iflow_numero"] != DBNull.Value)
                    {
                        pedido.numeroVolume = Convert.ToString(linha["iflow_numero"]);
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

                //Retorna a coleção de cadastro encontrada
                return pedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao imprimir a etiqueta. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa em qual estação o volume deverá está
        public ItensFlowRack PesqEstacaoVolume(string codVolume)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codVolume", codVolume);

                //Pesquisa os itens
                string select = "select " +
                                "(select first 1 est_codigo from wms_item_flowrack i " +
                                "where iflow_barra = @codVolume and iflow_data_conferencia is null order by est_codigo asc) as est_codigo, " +
                                "(select min(iflow_numero) from wms_item_flowrack where ped_codigo = (select distinct(ped_codigo) from wms_item_flowrack where iflow_barra = @codVolume) and iflow_qtd_conferida is null) as ped_volume, " +
                                "(select count(prod_id) from wms_item_flowrack where ped_codigo = (select distinct(ped_codigo) from wms_item_flowrack where iflow_barra = @codVolume) and not iflow_qtd_conferida is null) as item_pedido, " +
                                "(select min(est_codigo) from wms_item_flowrack where  ped_codigo = (select distinct(ped_codigo) from wms_item_flowrack where iflow_barra = @codVolume) and iflow_qtd_conferida is null) as est_atual " +
                                "from RDB$DATABASE";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                ItensFlowRack itensFlowRack = new ItensFlowRack();    

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["ped_volume"] != DBNull.Value)
                    {
                        itensFlowRack.numeroVolume = Convert.ToInt32(linha["ped_volume"]);
                    }

                    if (linha["est_atual"] != DBNull.Value)
                    {
                        itensFlowRack.codEstacaoAtual = Convert.ToInt32(linha["est_atual"]);
                    }

                    if (linha["item_pedido"] != DBNull.Value)
                    {
                        itensFlowRack.qtdConferidaProduto = Convert.ToInt32(linha["item_pedido"]);
                    }
                }

                //Retorna a coleção de cadastro encontrada
                return itensFlowRack;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a estação do volume. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa as restrições do cliente
        public Cliente PesqRestricoesCliente(int codPedido)
        {
            try
            {
                //Instância a classe
                Cliente cliente = new Cliente();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);

                //Pesquisa os itens
                string select = "select c.cli_validade, c.cli_validade_dias, c.cli_caixa_fechada from wms_pedido p " +
                                "inner join wms_cliente c  " +
                                "on p.cli_id = c.cli_id " +
                                "where ped_codigo = @codPedido";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
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

                }
                //Retorna a coleção de cadastro encontrada
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as restrições do cliente. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens para conferencia Flow rack
        public ItensFlowRackCollection PesqItemPedido(string codBarraVolume, int codEstacao)
        {
            try
            {
                //Instância a camada de objêto - Coleção
                ItensFlowRackCollection itensFlowRackCollection = new ItensFlowRackCollection();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codBarraVolume", codBarraVolume);
                conexao.AdicionarParamentros("@codEstacao", codEstacao);

                //Pesquisa os itens
                string select = "select s.est_codigo, b.bloc_numero, a.apa_numero, i.ped_codigo, a.apa_endereco, iflow_codigo, iflow_numero, p.prod_id, p.prod_codigo, " +
                                "p.prod_descricao, i.iflow_quantidade, i.iflow_qtd_conferida, i.iflow_corte, iflow_data_conferencia, iflow_audita, sep_validade," +
                                "(select cat_descricao from wms_categoria " +
                                "where cat_codigo = p.cat_codigo) as categoria, " +
                                "(select bar_peso from wms_barra " +
                                "where bar_multiplicador = 1 and prod_id = i.prod_id) as peso, " +
                                "(select bar_cubagem* i.iflow_quantidade from wms_barra " +
                                "where bar_multiplicador = 1 and prod_id = i.prod_id) as cubagem, " +
                                "(select coalesce(pp.ped_nota_fiscal, 0) from wms_pedido pp where ped_codigo = i.ped_codigo) as ped_nota_fiscal " +
                                "from wms_item_flowrack i " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_separacao s " +
                                "on s.prod_id = i.prod_id " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "where s.sep_tipo = 'FLOWRACK'  and i.iflow_barra = @codBarraVolume and s.est_codigo = @codEstacao " +
                                "order by a.apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ItensFlowRack itensFlowRack = new ItensFlowRack();
                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        itensFlowRack.bloco = Convert.ToInt32(linha["bloc_numero"]);
                    }

                    if (linha["apa_numero"] != DBNull.Value)
                    {
                        itensFlowRack.apartamento = Convert.ToInt32(linha["apa_numero"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itensFlowRack.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["iflow_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.idProdutoVolume = Convert.ToInt32(linha["iflow_codigo"]);
                    }

                    if (linha["iflow_numero"] != DBNull.Value)
                    {
                        itensFlowRack.numeroVolume = Convert.ToInt32(linha["iflow_numero"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itensFlowRack.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iflow_quantidade"] != DBNull.Value)
                    {
                        itensFlowRack.qtdProduto = Convert.ToInt32(linha["iflow_quantidade"]);
                    }

                    if (linha["iflow_qtd_conferida"] != DBNull.Value)
                    {
                        itensFlowRack.qtdConferidaProduto = Convert.ToInt32(linha["iflow_qtd_conferida"]);
                    }

                    if (linha["iflow_corte"] != DBNull.Value)
                    {
                        itensFlowRack.qtdCorteProduto = Convert.ToInt32(linha["iflow_corte"]);
                    }

                    if (linha["iflow_data_conferencia"] != DBNull.Value)
                    {
                        itensFlowRack.dataConferencia = Convert.ToDateTime(linha["iflow_data_conferencia"]);
                    }

                    if (linha["categoria"] != DBNull.Value)
                    {
                        itensFlowRack.descCategoria = Convert.ToString(linha["categoria"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        itensFlowRack.pesoProduto = Convert.ToDouble(linha["peso"]);
                    }

                    if (linha["cubagem"] != DBNull.Value)
                    {
                        itensFlowRack.cubagemProduto = Convert.ToDouble(linha["cubagem"]);
                    }

                    if (linha["iflow_audita"] != DBNull.Value)
                    {
                        itensFlowRack.auditaProduto = Convert.ToString(linha["iflow_audita"]);
                    }

                    if (linha["sep_Validade"] != DBNull.Value)
                    {
                        itensFlowRack.validadeProduto = Convert.ToDateTime(linha["sep_Validade"]).Date;
                    }

                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        itensFlowRack.notaFiscal = Convert.ToInt64(linha["ped_nota_fiscal"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    itensFlowRackCollection.Add(itensFlowRack);
                }
                //Retorna a coleção de cadastro encontrada
                return itensFlowRackCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o(s) item(s) para conferencia. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o item pelo códido de barra Flow rack
        public Barra PesqCodigoBarra(string codBarraItem, string empresa)
        {
            try
            {
                //Instância a classe
                Barra barra = new Barra();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codBarraItem", codBarraItem);
                conexao.AdicionarParamentros("@empresa", empresa);
                //Pesquisa os itens
                string select = "select prod_id, ba.bar_multiplicador from wms_barra ba where ba.bar_numero = @codBarraItem " +
                                "and ba.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";//Modificado

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        barra.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["bar_multiplicador"] != DBNull.Value)
                    {
                        barra.multiplicador = Convert.ToInt32(linha["bar_multiplicador"]);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return barra;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao pesquisar o código de barra. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a restricao de categoria
        public string PesqRestricaoCategoria(string categoria1, string categoria2)
        {
            try
            {
                //Instância a variável que vai receber o resultado
                string status = null;

                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@categoria1", categoria1);
                conexao.AdicionarParamentros("@categoria2", categoria2);

                //Pesquisa os itens
                string select = "select rest_status from wms_categoria_restricao cr " +
                                "where cat_codigo = (select cat_codigo from wms_categoria where cat_descricao = @categoria1) " +
                                "and cat_codigo_rest = (select cat_codigo from wms_categoria where cat_descricao = @categoria2) ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {

                    //Adiciona os valores encontrados
                    if (linha["rest_status"] != DBNull.Value)
                    {
                        status = Convert.ToString(linha["rest_status"]);
                    }

                }
                //Retorna o resultado
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao pesquisar as restrições dos produtos. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a próxima estacao
        public Estacao PesqProximaEstacao(int codPedido, int estacaoAtual)
        {
            try
            {
                //Instância um objêto
                Estacao estacao = new Estacao();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@estacaoAtual", estacaoAtual);

                //Pesquisa os itens
                string select = "select first 1 distinct(i.est_codigo) as est_codigo, est_nivel from wms_item_flowrack i " +
                                "inner join wms_estacao e " +
                                "on e.est_codigo = i.est_codigo " +
                                "where not e.est_tipo = 'Movel' and i.ped_codigo = @codPedido and i.est_codigo > @estacaoAtual";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        estacao.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["est_nivel"] != DBNull.Value)
                    {
                        estacao.nivel = Convert.ToInt32(linha["est_nivel"]);
                    }

                }
                //Retorna o objêto encontrado
                return estacao;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao pesquisar a próxima estação. \nDetalhes:" + ex.Message);
            }
        }

        //Analisa o pedido para da sugestão de caixa 
        public ItensFlowRackCollection AnalisaItensSugetao(string codBarraVolume, int codEstacao)
        {
            try
            {
                //Instância a camada de objêto - Coleção
                ItensFlowRackCollection itensFlowRackCollection = new ItensFlowRackCollection();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@codBarraVolume", codBarraVolume);

                //Pesquisa os itens
                string select = "select i.iflow_codigo, i.est_codigo, p.prod_codigo, p.prod_descricao, i.iflow_quantidade, " +
                                "(select cat_descricao from wms_categoria " +
                                "where cat_codigo = p.cat_codigo) as categoria, " +
                                "(select bar_peso from wms_barra " +
                                "where bar_multiplicador = 1 and prod_id = i.prod_id) as peso, " +
                                "(select bar_cubagem* i.iflow_quantidade from wms_barra " +
                                "where bar_multiplicador = 1 and prod_id = i.prod_id) as cubagem " +
                                "from wms_item_flowrack i " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "where i.iflow_barra = @codBarraVolume  and est_codigo >= @codEstacao " +
                                "order by i.est_codigo";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    ItensFlowRack itensFlowRack = new ItensFlowRack();

                    //Adiciona os valores encontrados
                    if (linha["iflow_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.idProdutoVolume = Convert.ToInt32(linha["iflow_codigo"]);
                    }

                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensFlowRack.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensFlowRack.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iflow_quantidade"] != DBNull.Value)
                    {
                        itensFlowRack.qtdProduto = Convert.ToInt32(linha["iflow_quantidade"]);
                    }

                    if (linha["categoria"] != DBNull.Value)
                    {
                        itensFlowRack.descCategoria = Convert.ToString(linha["categoria"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        itensFlowRack.pesoProduto = Convert.ToDouble(linha["peso"]);
                    }

                    if (linha["cubagem"] != DBNull.Value)
                    {
                        itensFlowRack.cubagemProduto = Convert.ToDouble(linha["cubagem"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    itensFlowRackCollection.Add(itensFlowRack);
                }
                //Retorna a coleção de cadastro encontrada
                return itensFlowRackCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu analisar o(s)  a sugetão de caixa. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a caixa 
        public CaixaCollection PesqCaixa(double cubagemInicial, double peso)
        {
            try
            {
                //Instância a camada de objêto - Coleção
                CaixaCollection caixaCollection = new CaixaCollection();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@cubagemInicial", (cubagemInicial - (cubagemInicial / 100) * 2)); //2% a menos do valor da cubagem
                conexao.AdicionarParamentros("@cubagemFinal", (cubagemInicial + (cubagemInicial / 100) * 10)); //10% a mais do valor da cubagem
                conexao.AdicionarParamentros("@peso", (peso));

                //Pesquisa os itens
                string select = "select caixa_codigo, caixa_descricao, caixa_cubagem, caixa_peso from wms_caixa where caixa_cubagem between @cubagemInicial and @cubagemFinal and not caixa_peso < @peso";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
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
                throw new Exception("Ocorreu pesquisar ao caixa. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o estoque
        public int PesqEstoque(int idProduto)
        {
            try
            {
                //Instância objêto
                int estoque = 0;

                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);

                //Pesquisa os itens
                string select = "select est_quantidade from wms_estoque e where prod_id = @idProduto";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["est_quantidade"] != DBNull.Value)
                    {
                        estoque = Convert.ToInt32(linha["est_quantidade"]);
                    }
                }

                //Retorna a coleção de cadastro encontrada
                return estoque;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o estoque! \nDetalhes:" + ex.Message);
            }
        }


        //Atualiza o inicio de conferencia e a data de criação do volume 01
        public void AtualizaInicioConferencia(string codBarraVolume, int codPedido)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codBarraVolume", codBarraVolume);
                conexao.AdicionarParamentros("@codPedido", codPedido);


                //String de atualização do volume 01
                string update = "update wms_item_flowrack set iflow_criado = current_time " +
                                "where iflow_barra = @codBarraVolume and iflow_criado is null";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //String de atualização do volume 01
                string update1 = "update wms_pedido ped set ped.ped_inicio_flow_rack = current_time " +
                                 "where ped.ped_codigo = @codPedido  and ped_inicio_flow_rack is null";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update1);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao resgistrar o início de conferencia do pedido. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza o fim de conferencia 
        public void AtualizaFimConferencia(int codPedido)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);

                //String de atualização do volume 01
                string update = "update wms_pedido ped set ped.ped_fim_flow_rack = current_time " +
                                 "where ped.ped_codigo = @codPedido  and ped_fim_flow_rack is null";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao resgistrar o fim de conferencia do pedido. \nDetalhes:" + ex.Message);
            }
        }

        //Salva o código da caixa nos itens analisados
        public void SalvarSugestaoItem(int idProdutoVolume, int codCaixa, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProdutoVolume", idProdutoVolume);
                conexao.AdicionarParamentros("@codCaixa", codCaixa);
                conexao.AdicionarParamentros("@mepresa", empresa);


                //String de atualização
                string update = "update wms_item_flowrack set caixa_codigo = @codCaixa " +
                    "where iflow_codigo = @idProdutoVolume and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao resgistrar a caixa nos itens do volume. \nDetalhes:" + ex.Message);
            }
        }

        //Salva a conferência do itens
        public void SalvarConferencia(ItensFlowRack conferenciaFlowRack, int codConferente, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codEstacao", conferenciaFlowRack.codEstacao);
                conexao.AdicionarParamentros("@idProdutoVolume", conferenciaFlowRack.idProdutoVolume);
                conexao.AdicionarParamentros("@codPedido", conferenciaFlowRack.codPedido);
                conexao.AdicionarParamentros("@numeroVolume", conferenciaFlowRack.numeroVolume);
                conexao.AdicionarParamentros("@idProduto", conferenciaFlowRack.idProduto);
                conexao.AdicionarParamentros("@qtdProduto", conferenciaFlowRack.qtdProduto);
                conexao.AdicionarParamentros("@qtdConferidaProduto", conferenciaFlowRack.qtdProduto - conferenciaFlowRack.qtdCorteProduto);
                conexao.AdicionarParamentros("@qtdCorteProduto", conferenciaFlowRack.qtdCorteProduto);
                conexao.AdicionarParamentros("@auditaProduto", conferenciaFlowRack.auditaProduto);
                conexao.AdicionarParamentros("@validadeProduto", conferenciaFlowRack.validadeProduto);
                conexao.AdicionarParamentros("@codConferente", codConferente);
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de atualização - Atualiza a conferencia do pedido
                string update = "update wms_item_flowrack i set i.iflow_qtd_conferida = @qtdConferidaProduto, i.iflow_corte = @qtdCorteProduto, " +
                                "i.iflow_data_conferencia = current_time, usu_codigo_conferente = @codConferente where iflow_codigo = @idProdutoVolume" +
                                "and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //String de atualização - Atualiza a conferencia do pedido
                string updatePicking = "update wms_separacao set sep_estoque = (sep_estoque - @qtdConferidaProduto) where sep_tipo = 'FLOWRACK' and prod_id = @idProduto";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updatePicking);

                //Registra a conferência no rastreamento de flow rack
                string insert = "insert into wms_rastreamento_flowrack (rast_codigo, iflow_codigo, est_codigo, ped_codigo, " +
                                "iflow_numero, iflow_barra, iflow_criado, caixa_codigo, prod_id, iflow_quantidade, " +
                                "iflow_qtd_conferida, iflow_corte, iflow_audita, iflow_data_conferencia, usu_codigo_conferente, conf_codigo) " +

                                "select gen_id(gen_wms_rastreamento_flowrack, 1), iflow_codigo, est_codigo, ped_codigo, " +
                                "iflow_numero, iflow_barra, iflow_criado, caixa_codigo, prod_id, iflow_quantidade, " +
                                "iflow_qtd_conferida, iflow_corte, iflow_audita, iflow_data_conferencia, usu_codigo_conferente " +
                                "from wms_item_flowrack where iflow_codigo = @idProdutoVolume, " +
                                "(select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                //Registra o corte na tabela de corte
                if (conferenciaFlowRack.qtdCorteProduto > 0)
                {
                    //Registra a conferência no rastreamento de flow rack
                    string insertCorte = "insert into wms_corte_produto (corte_codigo, ped_codigo, prod_id, corte_data, corte_quantidade, usu_codigo) " +
                                    "Values " +
                                    "(gen_id(gen_wms_corte, 1), @codPedido, @idProduto, current_time, @qtdCorteProduto, @codConferente)";

                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, insertCorte);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro registrar a conferência. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa um novo volume
        public Pedido PesqNovoVolume(string codVolume)
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
                string select = "select distinct(r.rota_numero), ip.ped_codigo, ip.iflow_barra,  c.cli_codigo ||''|| c.cli_nome as cli_nome, c.cli_endereco, c.cli_numero, b.bar_nome, ci.cid_nome, " +
                                "(select count(prod_id) from wms_item_flowrack " +
                                "where not iflow_data_conferencia is null and iflow_barra = @codVolume) as volume " +
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

        //Gera um novo Volume
        public void GerarNovoVolume(string codVolume, string codVolumeGerado, string numeroVolumeGerado)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codVolume", codVolume);
                conexao.AdicionarParamentros("@codVolumeGerado", codVolumeGerado);
                conexao.AdicionarParamentros("@numeroVolumeGerado", numeroVolumeGerado);

                //Gera um novo volume
                string update = "update wms_item_flowrack set iflow_barra = @codVolumeGerado, iflow_numero = @numeroVolumeGerado, iflow_criado = current_timestamp " +
                                "where iflow_data_conferencia is null and ped_codigo = " +
                                "(select first 1 ped_codigo from wms_item_flowrack where iflow_barra = @codVolume)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro gerar um novo volume. \nDetalhes:" + ex.Message);
            }
        }


        //Atualiza a data do volume 01 e  o nível do pedido
        public void AtualizaDataVolume(int codPedido)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);

                //String de atualização do volume 01
                string update = "update wms_item_flowrack i set i.iflow_criado = current_timestamp, iflow_nivel = 'False' " +
                    " where ped_codigo = @codPedido and i.iflow_numero = '01'";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //String de atualiza o nível da estação para false depois da impressão
                string update1 = "update wms_item_flowrack set iflow_nivel = 'False' " +
                    "where ped_codigo = @codPedido";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update1);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao resgistrar a data do primeiro volume. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza nivel do pedido
        public void AtualizaNivel(string barraVolume, int codEstacao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@barraVolume", barraVolume);
                conexao.AdicionarParamentros("@codEstacao", codEstacao);

                //String de atualização do volume 01
                string update = "update wms_item_flowrack set iflow_nivel = 'True' " +
                                 "where iflow_barra = @barraVolume and est_codigo = @codEstacao ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu ao resgistrar o nível da estação no proxímo volume. \nDetalhes:" + ex.Message);
            }
        }


    }
}
