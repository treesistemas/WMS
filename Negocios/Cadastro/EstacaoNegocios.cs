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
    public class EstacaoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa um novo id
        public Estacao PesqId()
        {
            try
            {
                //Instância o objeto
                Estacao estacao = new Estacao();
                //String de consulta
                string select = "select gen_id(gen_wms_estacao,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona a linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona o valor encontrado
                    estacao.codEstacao = Convert.ToInt32(linha["id"]);
                }
                //Retorna o id encontrado
                return estacao;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa estações
        public EstacaoCollection PesqEstacao(bool status, string empresa)
        {
            try
            {
                //Instância a coleção
                EstacaoCollection estacaoCollection = new EstacaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select est_codigo, est_descricao, est_tipo, est_nivel, est_volume_independente, est_status, u.usu_codigo, usu_login " +
                    "from wms_estacao s " +
                    "left join wms_usuario u " +
                    "on u.usu_codigo = s.usu_codigo " +
                    "where est_status = @status and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by est_tipo, est_codigo";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Estacao estacao = new Estacao();
                    //Adiciona os valores encontrados
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        estacao.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }
                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        estacao.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }
                    if (linha["est_tipo"] != DBNull.Value)
                    {
                        estacao.tipo = Convert.ToString(linha["est_tipo"]);
                    }

                    if (linha["est_nivel"] != DBNull.Value)
                    {
                        estacao.nivel = Convert.ToInt32(linha["est_nivel"]);
                    }

                    if (linha["est_volume_independente"] != DBNull.Value)
                    {
                        estacao.volumeIndependente = Convert.ToBoolean(linha["est_volume_independente"]);
                    }

                    if (linha["est_status"] != DBNull.Value)
                    {
                        estacao.status = Convert.ToBoolean(linha["est_status"]);
                    }
                    if (linha["usu_codigo"] != DBNull.Value)
                    {
                        estacao.codUsuario = Convert.ToInt32(linha["usu_codigo"]);
                    }
                    if (linha["usu_login"] != DBNull.Value)
                    {
                        estacao.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    estacaoCollection.Add(estacao);
                }
                //Retorna a coleção de cadastro encontrada
                return estacaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as estações. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os blocos associado a estação
        public EstacaoCollection PesqBlocoEstacao(int codEstacao)
        {
            try
            {
                //Instância a coleção
                EstacaoCollection estacaoCollection = new EstacaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                //String de consulta
                string select = "select re.reg_numero, ru.rua_numero , bl.bloc_codigo, bl.bloc_numero, est_codigo from wms_bloco bl " +
                "inner join wms_rua ru " +
                "on bl.rua_codigo = ru.rua_codigo " +
                "inner join wms_regiao re " +
                "on re.reg_codigo = ru.reg_codigo " +
                 "where est_codigo = @codEstacao";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Estacao estacao = new Estacao();
                    //Adiciona os valores encontrados
                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        estacao.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }
                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        estacao.rua = Convert.ToInt32(linha["rua_numero"]);
                    }
                    if (linha["bloc_codigo"] != DBNull.Value)
                    {
                        estacao.codBloco = Convert.ToInt32(linha["bloc_codigo"]);
                    }
                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        estacao.bloco = Convert.ToInt32(linha["bloc_numero"]);
                    }
                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        estacao.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    estacaoCollection.Add(estacao);
                }
                //Retorna a coleção de cadastro encontrada
                return estacaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os blocos da estação. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o endereço do produto no FlowRack
        public EnderecoCollection PesqProdutoEstacao(string empresa)
        {
            try
            {
                //Instância oa coleção de objêtos
                EnderecoCollection enderecoCollection = new EnderecoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parâmetro
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select s.sep_codigo, a.apa_codigo, a.apa_endereco, e.est_codigo, p.prod_id, prod_codigo, prod_descricao, " +
                "sep_estoque, sep_validade, sep_lote, sep_peso, prod_independente_flowrack, " +
                "rg.reg_numero, r.rua_numero, b.bloc_numero, n.niv_numero, a.apa_numero, " +
                "c.conf_empresa as nome_empresa, c.conf_sigla " +
                "from wms_produto p " +
                "inner join wms_separacao s " +
                "on s.prod_id = p.prod_id and p.conf_codigo = s.conf_codigo " +
                "inner join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "inner join wms_estacao e " +
                "on e.est_codigo = s.est_codigo and e.conf_codigo = s.conf_codigo " +
                "inner join wms_nivel n " +
                "on n.niv_codigo = a.niv_codigo " +
                "inner join wms_bloco b " +
                "on b.bloc_codigo = n.bloc_codigo " +
                "inner join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "inner join wms_regiao rg " +
                "on rg.reg_codigo = r.reg_codigo " +
                "inner join wms_configuracao c " +
                "on c.conf_codigo = s.conf_codigo "+
                "where sep_tipo = 'FLOWRACK' and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto endereço
                    EnderecoPicking enderecoProduto = new EnderecoPicking();

                    if (linha["nome_empresa"] != DBNull.Value)
                    {
                        enderecoProduto.nomeEmpresa = Convert.ToString(linha["nome_empresa"]);
                    }

                    if (linha["conf_sigla"] != DBNull.Value)
                    {
                        enderecoProduto.siglaEmpresa = Convert.ToString(linha["conf_sigla"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        enderecoProduto.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        enderecoProduto.numeroRua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        enderecoProduto.numeroBloco = Convert.ToInt32(linha["bloc_numero"]);
                    }

                    if (linha["niv_numero"] != DBNull.Value)
                    {
                        enderecoProduto.numeroNivel = Convert.ToInt32(linha["niv_numero"]);
                    }

                    if (linha["apa_numero"] != DBNull.Value)
                    {
                        enderecoProduto.numeroApartamento = Convert.ToInt32(linha["apa_numero"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        enderecoProduto.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["sep_codigo"] != DBNull.Value)
                    {
                        enderecoProduto.codSeparacao = Convert.ToInt32(linha["sep_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        enderecoProduto.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        enderecoProduto.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        enderecoProduto.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        enderecoProduto.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        enderecoProduto.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_independente_flowrack"] != DBNull.Value)
                    {
                        enderecoProduto.volumeIdependente = Convert.ToString(linha["prod_independente_flowrack"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        enderecoProduto.estoque = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        enderecoProduto.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_peso"] != DBNull.Value)
                    {
                        enderecoProduto.peso = Convert.ToDouble(linha["sep_peso"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        enderecoProduto.lote = Convert.ToString(linha["sep_lote"]);
                    }


                    //Adiciona o objêto a coleção
                    enderecoCollection.Add(enderecoProduto);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto da estação \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o endereço do produto para endereçar
        public EnderecoPicking PesqProdutoFlowrack(string codProduto, string tipo)
        {
            try
            {
                EnderecoPicking enderecoProduto = new EnderecoPicking();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@codProduto", codProduto);

                //String de consulta
                string select = "select s.sep_codigo, a.apa_codigo, a.apa_endereco, apa_status, p.prod_id, prod_codigo, prod_descricao, prod_separacao_flowrack, s.sep_estoque, s.sep_validade, " +
                "s.sep_capacidade, s.sep_abastecimento, sep_peso, sep_tipo, sep_lote, s.est_codigo, e.est_descricao, e.est_tipo, a.apa_disponivel " +
                "from wms_produto p " +
                "left join wms_separacao s " +
                "on s.prod_id = p.prod_id " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_estacao e " +
                "on e.est_codigo = s.est_codigo " +
                "where prod_codigo = @codProduto";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        enderecoProduto.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        enderecoProduto.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        enderecoProduto.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_separacao_flowrack"] != DBNull.Value)
                    {
                        enderecoProduto.separacaoFlowrack = Convert.ToBoolean(linha["prod_separacao_flowrack"]);
                    }

                    if (linha["est_tipo"] != DBNull.Value)
                    {
                        enderecoProduto.tipoEstacao = Convert.ToString(linha["est_tipo"]);
                    }

                    if (linha["sep_tipo"].Equals(tipo))
                    {
                        enderecoProduto.tipoEstacao = Convert.ToString(linha["sep_tipo"]);

                        if (linha["sep_codigo"] != DBNull.Value)
                        {
                            enderecoProduto.codSeparacao = Convert.ToInt32(linha["sep_codigo"]);
                        }

                        if (linha["apa_codigo"] != DBNull.Value)
                        {
                            enderecoProduto.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                        }

                        if (linha["apa_endereco"] != DBNull.Value)
                        {
                            enderecoProduto.endereco = Convert.ToString(linha["apa_endereco"]);
                        }

                        if (linha["sep_estoque"] != DBNull.Value)
                        {
                            enderecoProduto.estoque = Convert.ToInt32(linha["sep_estoque"]);
                        }

                        if (linha["sep_validade"] != DBNull.Value)
                        {
                            enderecoProduto.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                        }

                        if (linha["sep_peso"] != DBNull.Value)
                        {
                            enderecoProduto.peso = Convert.ToDouble(linha["sep_peso"]);
                        }

                        if (linha["sep_lote"] != DBNull.Value)
                        {
                            enderecoProduto.lote = Convert.ToString(linha["sep_lote"]);
                        }

                        if (linha["sep_capacidade"] != DBNull.Value)
                        {
                            enderecoProduto.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                        }

                        if (linha["sep_abastecimento"] != DBNull.Value)
                        {
                            enderecoProduto.abastecimento = Convert.ToInt32(linha["sep_abastecimento"]);
                        }

                        if (linha["est_codigo"] != DBNull.Value)
                        {
                            enderecoProduto.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                        }

                        if (linha["est_descricao"] != DBNull.Value)
                        {
                            enderecoProduto.descEstacao = Convert.ToString(linha["est_descricao"]);
                        }

                        if (linha["est_descricao"] != DBNull.Value)
                        {
                            enderecoProduto.descEstacao = Convert.ToString(linha["est_descricao"]);
                        }

                        if (linha["apa_status"] != DBNull.Value)
                        {
                            enderecoProduto.apDisponibilidade = Convert.ToString(linha["apa_status"]);
                        }

                        if (linha["apa_disponivel"] != DBNull.Value)
                        {
                            enderecoProduto.apDisponibilidade = Convert.ToString(linha["apa_disponivel"]);
                        }
                    }

                }

                return enderecoProduto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto \nDetalhes: " + ex.Message);
            }
        }

        //Salvar cadastro
        public void SalvarEstacao(int codEstacao, string descEstacao, string nivel, bool volumeIndependene, bool status, string tipo, string codUsuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@descEstacao", descEstacao);
                conexao.AdicionarParamentros("@nivel", nivel);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@volumeIndependene", volumeIndependene);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);

                //String de insert
                string insert = "insert into wms_estacao (est_codigo, est_descricao, est_nivel, est_tipo, est_volume_independente, est_status, usu_codigo, conf_codigo) " +
                        "values (@codEstacao, @descEstacao, @nivel, @tipo, @volumeIndependene, @status, @codUsuario, (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Método Alterar cadastro
        public void AlterarEstacao(int codEstacao, string descEstacao, string nivel, bool volumeIndependene, bool status, string tipo, string codUsuario)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@descEstacao", descEstacao);
                conexao.AdicionarParamentros("@nivel", nivel);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@volumeIndependene", volumeIndependene);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);

                //String de atualização
                string update = "update wms_estacao set est_descricao = @descEstacao, est_nivel = @nivel, est_tipo = @tipo, est_volume_independente = @volumeIndependene, est_status = @status, usu_codigo = @codUsuario " +
                    "where est_codigo = @codEstacao";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }
        }

        //Altera = Associa o bloco a estação
        public void AssociaBlocoEstacao(int codEstacao, int codRegiao, int codRua, int blocoInicial, int blocoFinal)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@codRegiao", codRegiao);
                conexao.AdicionarParamentros("@codRua", codRua);
                conexao.AdicionarParamentros("@blocoInicial", blocoInicial);
                conexao.AdicionarParamentros("@blocoFinal", blocoFinal);

                //String de atualização
                string update = "update wms_bloco set est_codigo = @codEstacao where bloc_codigo in " +
                "(select bl.bloc_codigo from wms_bloco bl " +
                "inner join wms_rua ru " +
                "on bl.rua_codigo = ru.rua_codigo " +
                "inner join wms_regiao re " +
                "on re.reg_codigo = ru.reg_codigo " +
                "where re.reg_codigo = @codRegiao  and ru.rua_codigo = @codRua and bl.bloc_numero between @blocoInicial and @blocoFinal)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao associar o bloco na estação. \nDetalhes:" + ex.Message);
            }
        }

        //Deleta a estação do bloco
        public void DesassociaBlocoEstacao(int codBloco)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codBloco", codBloco);
                //String de atualização
                string update = "update wms_bloco set est_codigo = null where bloc_codigo = @codBloco";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao desassociar o bloco da estação. \nDetalhes:" + ex.Message);
            }
        }

        //Altera = Seleciona o produto para separação de Flowrack
        public void AlterarSepProdutoFlowrack(string codproduto)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codigo", codproduto);
                //String de atualização
                string update = "update wms_produto set prod_separacao_flowrack = 'True' " +
                    "where prod_codigo = @codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao selecionar o produto para o flowrack. \nDetalhes:" + ex.Message);
            }
        }

        //Altera = Seleciona o produto para separação de Flowrack
        public void AlterarVolumeIndependente(string codproduto, bool independete)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codigo", codproduto);
                conexao.AdicionarParamentros("@independete", independete);
                //String de atualização
                string update = "update wms_produto set prod_independente_flowrack = @independete " +
                    "where prod_codigo = @codigo";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao autorzar o produto para gerar volume independente. \nDetalhes:" + ex.Message);
            }
        }


    }
}