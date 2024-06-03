using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class ConfiguracaoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa as configurações do sistema
        public Configuracao PesqConfiguracao()
        {
            try
            {
                //Instância a camada de objetos
                Configuracao configuracao = new Configuracao();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select conf_codigo, conf_empresa, conf_logo, conf_producao, conf_homologacao, conf_img_produto, " +
                    "conf_peso_endereco, conf_produto_endereco, conf_pedido_separador, conf_itens_separador, conf_altura_palete " +
                    "from wms_configuracao where conf_codigo = 1 ";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["conf_codigo"] != DBNull.Value)
                    {
                        configuracao.codConfiguracao = Convert.ToInt32(linha["conf_codigo"]);
                    }
                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        configuracao.empresa = Convert.ToString(linha["conf_empresa"]);
                    }

                    if (linha["conf_logo"] != DBNull.Value)
                    {
                        configuracao.logoEmpresa = Convert.ToString(linha["conf_logo"]);
                    }

                    if (linha["conf_producao"] != DBNull.Value)
                    {
                        configuracao.caminhoProducao = Convert.ToString(linha["conf_producao"]);
                    }

                    if (linha["conf_homologacao"] != DBNull.Value)
                    {
                        configuracao.caminhoHomologacao = Convert.ToString(linha["conf_homologacao"]);
                    }

                    if (linha["conf_img_produto"] != DBNull.Value)
                    {
                        configuracao.imagemProduto = Convert.ToString(linha["conf_img_produto"]);
                    }

                    if (linha["conf_peso_endereco"] != DBNull.Value)
                    {
                        configuracao.pesoEndereco = Convert.ToDouble(linha["conf_peso_endereco"]);
                    }

                    if (linha["conf_produto_endereco"] != DBNull.Value)
                    {
                        configuracao.produtoEndereco = Convert.ToInt32(linha["conf_produto_endereco"]);
                    }

                    if (linha["conf_pedido_separador"] != DBNull.Value)
                    {
                        configuracao.pedidoSeparador = Convert.ToInt32(linha["conf_pedido_separador"]);
                    }

                    if (linha["conf_itens_separador"] != DBNull.Value)
                    {
                        configuracao.itensSeparador = Convert.ToInt32(linha["conf_itens_separador"]);
                    }

                    if (linha["conf_altura_palete"] != DBNull.Value)
                    {
                        configuracao.alturaPalete = Convert.ToDouble(linha["conf_altura_palete"]);
                    }


                }
                //Retorna o resultado
                return configuracao;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as configurações do sistema \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens de configuração
        public ItensConfiguracaoCollection PesqItensConfiguracao()
        {
            try
            {
                //Instância a camada de objetos
                ItensConfiguracaoCollection itensConfiguracaoCollection = new ItensConfiguracaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select iconf_codigo, iconf_descricao, iconf_menu, iconf_status from wms_itens_configuracao where conf_codigo = 1 order by iconf_ordem";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    ItensConfiguracao itensConfiguracao = new ItensConfiguracao();

                    //Adiciona os valores encontrados
                    if (linha["iconf_codigo"] != DBNull.Value)
                    {
                        itensConfiguracao.codItem = Convert.ToInt32(linha["iconf_codigo"]);
                    }
                    if (linha["iconf_descricao"] != DBNull.Value)
                    {
                        itensConfiguracao.descricao = Convert.ToString(linha["iconf_descricao"]);
                    }

                    if (linha["iconf_menu"] != DBNull.Value)
                    {
                        itensConfiguracao.menu = Convert.ToInt32(linha["iconf_menu"]);
                    }

                    if (linha["iconf_status"] != DBNull.Value)
                    {
                        itensConfiguracao.status = Convert.ToBoolean(linha["iconf_status"]);
                    }

                    //Preenche a coleção
                    itensConfiguracaoCollection.Add(itensConfiguracao);
                }
                //Retorna o resultado
                return itensConfiguracaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens da configuração do sistema \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa os parâmetros do sistema
        public ParametroCollection PesqParametro()
        {
            try
            {
                //Instância a camada de objetos
                ParametroCollection parametroCollection = new ParametroCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select i.conf_codigo, i.iconf_codigo, i.iconf_descricao, i.iconf_status, i.iconf_valor, i.iconf_valor_II  from wms_itens_configuracao i";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Parametro parametro = new Parametro();

                    //Adiciona os valores encontrados
                    if (linha["conf_codigo"] != DBNull.Value)
                    {
                        parametro.codEmpresa = Convert.ToInt32(linha["conf_codigo"]);
                    }
                    if (linha["iconf_codigo"] != DBNull.Value)
                    {
                        parametro.codItem = Convert.ToInt32(linha["iconf_codigo"]);
                    }

                    if (linha["iconf_descricao"] != DBNull.Value)
                    {
                        parametro.descParametro = Convert.ToString(linha["iconf_descricao"]);
                    }

                    if (linha["iconf_status"] != DBNull.Value)
                    {
                        if(Convert.ToBoolean(linha["iconf_status"]) == true)
                        {
                            parametro.status = "SIM";
                        }
                        else
                        {
                            parametro.status = "NÃO";
                        }

                        
                    }
                    
                     
                    if (linha["iconf_valor"] != DBNull.Value)
                    {
                        parametro.valor = Convert.ToDouble(linha["iconf_valor"]);
                    }
                    
                    if (linha["iconf_valor_II"] != DBNull.Value)
                    {
                        parametro.valor_II = Convert.ToInt32(linha["iconf_valor_II"]);
                    }
                    

                    parametroCollection.Add(parametro);
                }
                //Retorna o resultado
                return parametroCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os parâmetros \nDetalhes:" + ex.Message);
            }
        }



        //Pesquisa as configurações do sistema
        public ImpressoraCollection PesqImpressora()
        {
            try
            {
                //Instância a camada de objetos
                ImpressoraCollection impressoraCollection = new ImpressoraCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select imp_codigo, imp_ip, imp_impressora, imp_descricao, est_codigo from wms_impressora order by imp_codigo";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objetos
                    Impressora impressora = new Impressora();

                    //Adiciona os valores encontrados
                    if (linha["imp_codigo"] != DBNull.Value)
                    {
                        impressora.codigo = Convert.ToInt32(linha["imp_codigo"]);
                    }
                    if (linha["imp_ip"] != DBNull.Value)
                    {
                        impressora.IP = Convert.ToString(linha["imp_ip"]);
                    }

                    if (linha["imp_impressora"] != DBNull.Value)
                    {
                        impressora.nome = Convert.ToString(linha["imp_impressora"]);
                    }

                    if (linha["imp_descricao"] != DBNull.Value)
                    {
                        impressora.descricao = Convert.ToString(linha["imp_descricao"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        impressora.estacao = Convert.ToInt32(linha["est_codigo"]);
                    }
                    
                    impressoraCollection.Add(impressora);
                }
                //Retorna o resultado
                return impressoraCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as impressoras \nDetalhes:" + ex.Message);
            }
        }

        //Inseri a impressora
        public void InserirImpressora(Impressora i)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@IP", i.IP);
                conexao.AdicionarParamentros("@nome", i.nome);
                conexao.AdicionarParamentros("@descricao", i.descricao);
                conexao.AdicionarParamentros("@estacao", i.estacao);
 
                //String de atualização
                string update = "insert into wms_impressora (imp_codigo, imp_ip, imp_impressora, imp_descricao, est_codigo)" +
                    "values" +
                    "(gen_id(gen_wms_impressora, 1), @IP, @nome, @descricao, @estacao)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a impressora. \nDetalhes:" + ex.Message);
            }
        }

        //Inseri a impressora
        public void AtualizarImpressora(Impressora i)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", i.codigo);
                conexao.AdicionarParamentros("@IP", i.IP);
                conexao.AdicionarParamentros("@nome", i.nome);
                conexao.AdicionarParamentros("@descricao", i.descricao);
                conexao.AdicionarParamentros("@estacao", i.estacao);

                //String de atualização
                string update = "update wms_impressora set imp_ip = @IP, imp_impressora = @nome, imp_descricao = @descricao, " +
                                "est_codigo = @estacao " +
                                "where imp_codigo = @codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar a impressora. \nDetalhes:" + ex.Message);
            }
        }




        //Atualiza as informações da configuração
        public void AlterarConfiguracao(Configuracao configuracao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", configuracao.empresa);
                conexao.AdicionarParamentros("@logo", configuracao.logoEmpresa);
                conexao.AdicionarParamentros("@producao", configuracao.caminhoProducao);
                conexao.AdicionarParamentros("@homologacao", configuracao.caminhoHomologacao);
                conexao.AdicionarParamentros("@imagem", configuracao.imagemProduto);
                conexao.AdicionarParamentros("@pesoEndereco", configuracao.pesoEndereco);
                conexao.AdicionarParamentros("@produtoEndereco", configuracao.produtoEndereco);
                conexao.AdicionarParamentros("@pedidoSeparador", configuracao.pedidoSeparador);
                conexao.AdicionarParamentros("@itensSeparador", configuracao.itensSeparador);
                conexao.AdicionarParamentros("@alturaPalete", configuracao.alturaPalete);

                //String de atualização
                string update = "update wms_configuracao set conf_empresa = @empresa, conf_logo = @logo, conf_producao = @producao, " +
                    "conf_homologacao = @homologacao, conf_img_produto = @imagem, " +
                    "conf_peso_endereco = @pesoEndereco, conf_produto_endereco = @produtoEndereco, conf_pedido_separador = @pedidoSeparador, " +
                    "conf_itens_separador = @itensSeparador, conf_altura_palete = @alturaPalete where conf_codigo = 1";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar a configuração. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza as informações da configuração
        public void AtualizarStatus(string status, double valor, int valor_II, int codItem, int codEmpresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codEmpresa", codEmpresa);
                conexao.AdicionarParamentros("@codItem", codItem);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@valor", valor);
                conexao.AdicionarParamentros("@valor_II", valor_II);

                //String de atualização
                string update = "update wms_itens_configuracao set iconf_status = @status, iconf_valor = @valor, iconf_valor_II = @valor_II " +
                    "where iconf_codigo = @codItem and conf_codigo = @codEmpresa ";
                                

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o status. \nDetalhes:" + ex.Message);
            }
        }


        //Atualiza o status dos itens da configuração
        public void AlterarRestricao(int codItem, bool status)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codigo", codItem);
                conexao.AdicionarParamentros("@status", status);

                //String de atualização
                string update = "update wms_itens_configuracao set iconf_status = @status where iconf_codigo = @codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o status da restrição. \nDetalhes:" + ex.Message);
            }
        }

    }
}
