using System;
using System.Data;
using System.Reflection;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class RastreamentoOperacoesNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a armazenagem
        public ItensNotaEntradaCollection PesqArmazenagem(string dataIncial, string dataFinal, string CodProduto, string codUsuario, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                ItensNotaEntradaCollection itensNotaEntradaCollection = new ItensNotaEntradaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@CodProduto", CodProduto);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select rast_operacao, r.rast_data, u.usu_login, r.not_nota_cega, a.apa_endereco, p.prod_codigo, p.prod_descricao, p.prod_fator_pulmao, " +
                                "r.arm_quantidade_origem, u1.uni_unidade, r.arm_quantidade_destino, r.arm_vencimento_destino, r.arm_peso_destino, r.arm_lote_destino " +
                                "from wms_rastreamento_armazenagem r " +
                                "inner join wms_produto p " +
                                "on p.prod_id = r.prod_id and p.conf_codigo = r.conf_codigo " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = r.apa_codigo_destino " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "where rast_operacao = 'ARMAZENAGEM' and r.rast_data between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                if (!CodProduto.Equals(""))
                {
                    select += "and prod_codigo = @CodProduto ";
                }

                if (!codUsuario.Equals(""))
                {
                    select += "and r.usu_codigo = @codUsuario ";
                }

                select += "order by not_nota_cega, prod_codigo, rast_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    ItensNotaEntrada itensNotaEntrada = new ItensNotaEntrada();


                    if (linha["rast_operacao"] != DBNull.Value)
                    {
                        itensNotaEntrada.tipoArmazenamento = Convert.ToString(linha["rast_operacao"]);
                    }

                    if (linha["rast_data"] != DBNull.Value)
                    {
                        itensNotaEntrada.dataArmazenamento = Convert.ToDateTime(linha["rast_data"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itensNotaEntrada.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["not_nota_cega"] != DBNull.Value)
                    {
                        itensNotaEntrada.codNotaCega = Convert.ToInt32(linha["not_nota_cega"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itensNotaEntrada.descApartamento = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensNotaEntrada.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensNotaEntrada.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itensNotaEntrada.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);

                    }
                    else
                    {
                        itensNotaEntrada.fatorPulmao = 1;
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itensNotaEntrada.undPulmao = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["arm_quantidade_origem"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeNota = Convert.ToInt32(linha["arm_quantidade_origem"]);
                    }

                    if (linha["arm_quantidade_destino"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeArmazenada = Convert.ToInt32(linha["arm_quantidade_destino"]);
                    }

                    if (linha["arm_vencimento_destino"] != DBNull.Value)
                    {
                        itensNotaEntrada.validadeProduto = Convert.ToDateTime(linha["arm_vencimento_destino"]);
                    }

                    if (linha["arm_peso_destino"] != DBNull.Value)
                    {
                        itensNotaEntrada.pesoProduto = Convert.ToDouble(linha["arm_peso_destino"]);
                    }

                    if (linha["arm_lote_destino"] != DBNull.Value)
                    {
                        itensNotaEntrada.loteProduto = Convert.ToString(linha["arm_lote_destino"]);
                    }

                    //Adiciona o objêto a coleção
                    itensNotaEntradaCollection.Add(itensNotaEntrada);
                }
                //Retorna os valores encontrado
                return itensNotaEntradaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rastreamento de armazenagem. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a transferência
        public EnderecoPulmaoCollection PesqTransferencia(string dataIncial, string dataFinal, string CodProduto, string codUsuario, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                EnderecoPulmaoCollection itensTransferidosCollection = new EnderecoPulmaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@CodProduto", CodProduto);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select rast_operacao, r.rast_data, u.usu_login, " +
                                "a.apa_endereco as apartamento1, a.apa_tipo as tipo1, a1.apa_endereco as apartamento2, a1.apa_tipo as tipo2, p.prod_codigo, p.prod_descricao, " +
                                "p.prod_fator_pulmao, u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking, " +
                                "trunc(r.arm_quantidade_origem/p.prod_fator_pulmao) as qtd_cxa_origem, mod(r.arm_quantidade_origem, p.prod_fator_pulmao) as qtd_und_origem, " +
                                "trunc(r.arm_qtd_transferida/p.prod_fator_pulmao) as qtd_cxa_transferida, mod(r.arm_qtd_transferida, p.prod_fator_pulmao) as qtd_und_transferida, " +
                                "trunc(r.arm_quantidade_destino/p.prod_fator_pulmao)as qtd_cxa_destino, mod(arm_quantidade_destino, p.prod_fator_pulmao) as qtd_und_destino, " +
                                "r.arm_vencimento_origem, r.arm_vencimento_destino, r.arm_peso_origem, r.arm_peso_destino, r.arm_lote_origem, r.arm_lote_destino " +
                                "from wms_rastreamento_armazenagem r " +
                                "inner join wms_produto p " +
                                "on p.prod_id = r.prod_id and p.conf_codigo = r.conf_codigo " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = r.apa_codigo_origem " +
                                "inner join wms_apartamento a1 " +
                                "on a1.apa_codigo = r.apa_codigo_destino " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking " +
                                "where rast_operacao = 'TRANSFERÊNCIA' and r.rast_data between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                if (!CodProduto.Equals(""))
                {
                    select += "and prod_codigo = @CodProduto ";
                }

                if (!codUsuario.Equals(""))
                {
                    select += "and r.usu_codigo = @codUsuario ";
                }

                select += "order by not_nota_cega, prod_codigo, rast_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    EnderecoPulmao itensTranferencia = new EnderecoPulmao();


                    if (linha["rast_operacao"] != DBNull.Value)
                    {
                        itensTranferencia.tipoOperacao = Convert.ToString(linha["rast_operacao"]);
                    }

                    if (linha["rast_data"] != DBNull.Value)
                    {
                        itensTranferencia.dataOperacao = Convert.ToDateTime(linha["rast_data"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itensTranferencia.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["apartamento1"] != DBNull.Value)
                    {
                        itensTranferencia.descEndereco1 = Convert.ToString(linha["apartamento1"]);
                    }

                    if (linha["qtd_cxa_origem"] != DBNull.Value)
                    {
                        itensTranferencia.qtdCaixaOrigem = Convert.ToInt32(linha["qtd_cxa_origem"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itensTranferencia.undCaixaOrigem = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["qtd_und_origem"] != DBNull.Value)
                    {
                        itensTranferencia.qtdUnidadeOrigem = Convert.ToInt32(linha["qtd_und_origem"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        itensTranferencia.undUnidadeOrigem = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["qtd_cxa_transferida"] != DBNull.Value)
                    {
                        itensTranferencia.qtdCxaTranferidaOrigem = Convert.ToInt32(linha["qtd_cxa_transferida"]);
                    }

                    if (linha["qtd_und_transferida"] != DBNull.Value)
                    {
                        itensTranferencia.qtdUndTranferidaOrigem = Convert.ToInt32(linha["qtd_und_transferida"]);
                    }


                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensTranferencia.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensTranferencia.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itensTranferencia.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itensTranferencia.fatorPulmao = 1;
                    }

                    if (linha["apartamento2"] != DBNull.Value)
                    {
                        itensTranferencia.descApartamento2 = Convert.ToString(linha["apartamento2"]);
                    }

                    if (linha["qtd_cxa_destino"] != DBNull.Value)
                    {
                        itensTranferencia.qtdCaixaDestino = Convert.ToInt32(linha["qtd_cxa_destino"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itensTranferencia.undCaixaDestino = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["qtd_und_destino"] != DBNull.Value)
                    {
                        itensTranferencia.qtdUnidadeDestino = Convert.ToInt32(linha["qtd_und_destino"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        itensTranferencia.undUnidadeDestino = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["arm_vencimento_origem"] != DBNull.Value)
                    {
                        itensTranferencia.vencimentoProduto1 = Convert.ToDateTime(linha["arm_vencimento_origem"]);
                    }

                    if (linha["arm_vencimento_destino"] != DBNull.Value)
                    {
                        itensTranferencia.vencimentoProduto2 = Convert.ToDateTime(linha["arm_vencimento_destino"]);
                    }

                    if (linha["arm_peso_destino"] != DBNull.Value)
                    {
                        itensTranferencia.pesoProduto2 = Convert.ToDouble(linha["arm_peso_destino"]);
                    }

                    if (linha["arm_lote_origem"] != DBNull.Value)
                    {
                        itensTranferencia.loteProduto1 = Convert.ToString(linha["arm_lote_origem"]);
                    }

                    if (linha["arm_lote_destino"] != DBNull.Value)
                    {
                        itensTranferencia.loteProduto2 = Convert.ToString(linha["arm_lote_destino"]);
                    }

                    //Adiciona o objêto a coleção
                    itensTransferidosCollection.Add(itensTranferencia);
                }
                //Retorna os valores encontrado
                return itensTransferidosCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rastreamento de transferência de estoque. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o abastecimento
        public EnderecoPulmaoCollection PesqAbastecimento(string dataIncial, string dataFinal, string CodProduto, string codUsuario, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                EnderecoPulmaoCollection itensTransferidosCollection = new EnderecoPulmaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@CodProduto", CodProduto);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select rast_operacao, r.rast_data, u.usu_login, aba_codigo, " +
                                "a.apa_endereco as apartamento1, a.apa_tipo as tipo1, a1.apa_endereco as apartamento2, a1.apa_tipo as tipo2, p.prod_codigo, p.prod_descricao, " +
                                "p.prod_fator_pulmao, u1.uni_unidade as uni_pulmao, u2.uni_unidade as uni_picking, " +
                                "r.arm_quantidade_destino, r.arm_vencimento_destino,r.arm_peso_destino, r.arm_lote_destino " +
                                "from wms_rastreamento_armazenagem r " +
                                "inner join wms_produto p " +
                                "on p.prod_id = r.prod_id and p.conf_codigo = r.conf_codigo " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = r.apa_codigo_origem " +
                                "inner join wms_apartamento a1 " +
                                "on a1.apa_codigo = r.apa_codigo_destino " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking " +
                                "where rast_operacao = 'ABASTECIMENTO' and r.rast_data between @dataInicial and @dataFinal " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                if (!CodProduto.Equals(""))
                {
                    select += "and prod_codigo = @CodProduto ";
                }

                if (!codUsuario.Equals(""))
                {
                    select += "and r.usu_codigo = @codUsuario ";
                }

                select += "order by not_nota_cega, prod_codigo, rast_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    EnderecoPulmao itensTranferencia = new EnderecoPulmao();


                    if (linha["rast_operacao"] != DBNull.Value)
                    {
                        itensTranferencia.tipoOperacao = Convert.ToString(linha["rast_operacao"]);
                    }

                    if (linha["rast_data"] != DBNull.Value)
                    {
                        itensTranferencia.dataOperacao = Convert.ToDateTime(linha["rast_data"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itensTranferencia.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["aba_codigo"] != DBNull.Value)
                    {
                        itensTranferencia.codAbastecimento = Convert.ToInt32(linha["aba_codigo"]);
                    }

                    if (linha["apartamento1"] != DBNull.Value)
                    {
                        itensTranferencia.descEndereco1 = Convert.ToString(linha["apartamento1"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itensTranferencia.undCaixaOrigem = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensTranferencia.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensTranferencia.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itensTranferencia.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itensTranferencia.fatorPulmao = 1;
                    }

                    if (linha["apartamento2"] != DBNull.Value)
                    {
                        itensTranferencia.descApartamento2 = Convert.ToString(linha["apartamento2"]);
                    }

                    if (linha["arm_quantidade_destino"] != DBNull.Value)
                    {
                        itensTranferencia.qtdCaixaDestino = Convert.ToInt32(linha["arm_quantidade_destino"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        itensTranferencia.undCaixaDestino = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["arm_vencimento_destino"] != DBNull.Value)
                    {
                        itensTranferencia.vencimentoProduto2 = Convert.ToDateTime(linha["arm_vencimento_destino"]);
                    }

                    if (linha["arm_peso_destino"] != DBNull.Value)
                    {
                        itensTranferencia.pesoProduto2 = Convert.ToDouble(linha["arm_peso_destino"]);
                    }

                    if (linha["arm_lote_destino"] != DBNull.Value)
                    {
                        itensTranferencia.loteProduto2 = Convert.ToString(linha["arm_lote_destino"]);
                    }

                    //Adiciona o objêto a coleção
                    itensTransferidosCollection.Add(itensTranferencia);
                }
                //Retorna os valores encontrado
                return itensTransferidosCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rastreamento do abastecimento de mercadoria. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o endereçamento
        public EnderecoPulmaoCollection PesqEnderecamento(string operacao, string empresa, string dataIncial, string dataFinal, string CodProduto, string codUsuario)
        {
            try
            {
                //Instância a camada de objetos
                EnderecoPulmaoCollection itensTransferidosCollection = new EnderecoPulmaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@operacao", operacao);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@CodProduto", CodProduto);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //String de consulta
                string select = "select rast_operacao, r.rast_data, u.usu_login, p.prod_codigo, p.prod_descricao, p.prod_fator_pulmao, " +
                                "a.apa_endereco as apartamento1, a.apa_tipo as tipo1, r.arm_quantidade_origem, r.arm_vencimento_origem, r.arm_peso_origem, r.arm_lote_origem, " +
                                "u2.uni_unidade as uni_picking, arm_tipo, " +
                                "a1.apa_endereco as apartamento2, a1.apa_tipo as tipo2, r.arm_quantidade_destino, r.arm_vencimento_destino, r.arm_peso_destino, r.arm_lote_destino " +
                                "from wms_rastreamento_armazenagem r " +
                                "inner join wms_produto p " +
                                "on p.prod_id = r.prod_id and p.conf_codigo = r.conf_codigo " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = r.apa_codigo_origem " +
                                "left join wms_apartamento a1 " +
                                "on a1.apa_codigo = r.apa_codigo_destino " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking " +
                                "where rast_operacao = @operacao and r.rast_data between @dataInicial and @dataFinal ";

                if (!CodProduto.Equals(""))
                {
                    select += "and prod_codigo = @CodProduto ";
                }

                if (!codUsuario.Equals(""))
                {
                    select += "and r.usu_codigo = @codUsuario ";
                }

                select += "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by not_nota_cega, prod_codigo, rast_data ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    EnderecoPulmao itensTranferencia = new EnderecoPulmao();


                    if (linha["rast_operacao"] != DBNull.Value)
                    {
                        itensTranferencia.tipoOperacao = Convert.ToString(linha["rast_operacao"]);
                    }

                    if (linha["rast_data"] != DBNull.Value)
                    {
                        itensTranferencia.dataOperacao = Convert.ToDateTime(linha["rast_data"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itensTranferencia.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensTranferencia.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensTranferencia.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itensTranferencia.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itensTranferencia.fatorPulmao = 1;
                    }


                    if (linha["apartamento1"] != DBNull.Value)
                    {
                        itensTranferencia.descEndereco1 = Convert.ToString(linha["apartamento1"]);
                    }

                    if (linha["arm_quantidade_origem"] != DBNull.Value)
                    {
                        itensTranferencia.qtdCaixaOrigem = Convert.ToInt32(linha["arm_quantidade_origem"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        itensTranferencia.undUnidadeOrigem = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["arm_vencimento_origem"] != DBNull.Value)
                    {
                        itensTranferencia.vencimentoProduto1 = Convert.ToDateTime(linha["arm_vencimento_origem"]);
                    }

                    if (linha["arm_peso_origem"] != DBNull.Value)
                    {
                        itensTranferencia.pesoProduto1 = Convert.ToDouble(linha["arm_peso_origem"]);
                    }

                    if (linha["arm_lote_origem"] != DBNull.Value)
                    {
                        itensTranferencia.loteProduto1 = Convert.ToString(linha["arm_lote_origem"]);
                    }

                    if (linha["arm_tipo"] != DBNull.Value)
                    {
                        itensTranferencia.tipoApartamento1 = Convert.ToString(linha["arm_tipo"]);
                    }


                    if (linha["apartamento2"] != DBNull.Value)
                    {
                        itensTranferencia.descApartamento2 = Convert.ToString(linha["apartamento2"]);
                    }

                    if (linha["arm_quantidade_destino"] != DBNull.Value)
                    {
                        itensTranferencia.qtdCaixaDestino = Convert.ToInt32(linha["arm_quantidade_destino"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        itensTranferencia.undCaixaDestino = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["arm_vencimento_destino"] != DBNull.Value)
                    {
                        itensTranferencia.vencimentoProduto2 = Convert.ToDateTime(linha["arm_vencimento_destino"]);
                    }

                    if (linha["arm_peso_destino"] != DBNull.Value)
                    {
                        itensTranferencia.pesoProduto2 = Convert.ToDouble(linha["arm_peso_destino"]);
                    }

                    if (linha["arm_lote_destino"] != DBNull.Value)
                    {
                        itensTranferencia.loteProduto2 = Convert.ToString(linha["arm_lote_destino"]);
                    }

                    //Adiciona o objêto a coleção
                    itensTransferidosCollection.Add(itensTranferencia);
                }
                //Retorna os valores encontrado
                return itensTransferidosCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rastreamento de endereçamento do produto. \nDetalhes:" + ex.Message);
            }
        }



        //Pedido

        //Pesquisa a conferencia de volume
        public ItensFlowRackCollection PesqConferenciaVolume(string codPedido, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                ItensFlowRackCollection volumeCollection = new ItensFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select distinct(p.ped_codigo) as ped_codigo, r.iflow_numero, a.apa_endereco, r.iflow_conferencia_volume, u.usu_login from wms_pedido p " +
                                "left join wms_rastreamento_flowrack r " +
                                "on r.ped_codigo = p.ped_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = p.usu_codigo_conferente " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = r.apa_codigo " +
                                "where p.ped_codigo = @codPedido and iflow_numero > 0 " +
                                "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    ItensFlowRack volume = new ItensFlowRack();


                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        volume.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["iflow_numero"] != DBNull.Value)
                    {
                        volume.numeroVolume = Convert.ToInt32(linha["iflow_numero"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        volume.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["iflow_conferencia_volume"] != DBNull.Value)
                    {
                        volume.dataConferencia = Convert.ToDateTime(linha["iflow_conferencia_volume"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        volume.nomeUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    //Adiciona o objêto a coleção
                    volumeCollection.Add(volume);
                }
                //Retorna os valores encontrado
                return volumeCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rastreamento de conferencia do volume. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a conferencia de volume
        public ItensPedidoCollection PesqConferenciaPedido(string codPedido, string codProduto, string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                ItensPedidoCollection itemPedidoCollection = new ItensPedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select ip.iped_codigo, us.usu_login, p.mani_codigo, v.vei_placa, p.ped_codigo, " +
                "prod_codigo, prod_descricao, ip.iped_quantidade, ip.iped_qtd_conferencia, ip.iped_data_conferencia, ip.iped_qtd_corte_conferencia, ip.iped_validade, ip.iped_lote, ip.iped_peso, " +
                /*Qauntida de grandeza - quantidade reservada*/
                "trunc(iped_quantidade / pp.prod_fator_pulmao) as qtd_fechada, " +
                "(select sum(res_quantidade) from wms_reserva r where ped_codigo = ip.ped_codigo and prod_id = ip.prod_id) as grandeza, " +
                "u.uni_unidade as uni_fechada, " +
                /*Quatidade em unidade - quantidade conferida no flowrack*/
                "mod(iped_quantidade, pp.prod_fator_pulmao) as fracionado, " +
                "(select distinct(iflow_qtd_conferida) from wms_rastreamento_flowrack where ped_codigo = ip.ped_codigo and prod_id = ip.prod_id) as flow, " +
                "u1.uni_unidade as uni_fracionada " +
                "from wms_item_pedido ip " +
                "inner join wms_pedido p " +
                "on p.ped_codigo = ip.ped_codigo " +
                "inner join wms_produto pp " +
                "on pp.prod_id = ip.prod_id and pp.conf_codigo = ip.conf_codigo " +
                "left join wms_unidade u " +
                "on u.uni_codigo = pp.uni_codigo_pulmao " +
                "left join wms_unidade u1 " +
                "on u1.uni_codigo = pp.uni_codigo_picking " +
                "left join wms_manifesto m " +
                "on m.mani_codigo = p.mani_codigo " +
                "left join wms_usuario us " +
                "on us.usu_codigo = p.usu_codigo_conferente " +
                "left join wms_veiculo v " +
                "on v.vei_codigo = m.vei_codigo ";

                if (!codPedido.Equals(string.Empty))
                {
                    select += "where p.ped_codigo = @codPedido and ip.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }

                if (!codProduto.Equals(string.Empty))
                {
                    select += "where not ped_fim_conferencia is null and pp.prod_codigo = @codProduto and ped_fim_conferencia between @dataInicial and @dataFinal " +
                              "and ip.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    ItensPedido itemPedido = new ItensPedido();

                    //Controla a exibição do item no mapa de separação
                    int qtdFechada = 0, qtdFracionada = 0;

                    if (linha["iped_codigo"] != DBNull.Value)
                    {
                        itemPedido.codItem = Convert.ToInt64(linha["iped_codigo"]);
                    }
                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itemPedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        itemPedido.codManifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        itemPedido.maniPlaca = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itemPedido.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itemPedido.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iped_quantidade"] != DBNull.Value)
                    {
                        itemPedido.qtdProduto = Convert.ToInt32(linha["iped_quantidade"]);
                    }

                    if (linha["iped_qtd_conferencia"] != DBNull.Value)
                    {
                        itemPedido.qtdConferida = Convert.ToInt32(linha["iped_qtd_conferencia"]);
                    }

                    if (linha["iped_data_conferencia"] != DBNull.Value)
                    {
                        itemPedido.dataConferencia = Convert.ToDateTime(linha["iped_data_conferencia"]);
                    }

                    if (linha["iped_qtd_corte_conferencia"] != DBNull.Value)
                    {
                        itemPedido.qtdCorte = Convert.ToInt32(linha["iped_qtd_corte_conferencia"]);
                    }

                    if (linha["iped_validade"] != DBNull.Value)
                    {
                        itemPedido.vencimentoProduto = Convert.ToDateTime(linha["iped_validade"]);
                    }

                    if (linha["iped_lote"] != DBNull.Value)
                    {
                        itemPedido.loteProduto = Convert.ToString(linha["iped_lote"]);
                    }

                    if (linha["iped_peso"] != DBNull.Value)
                    {
                        itemPedido.pesoProduto = Convert.ToDouble(linha["iped_peso"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        itemPedido.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }


                    if (linha["qtd_fechada"] != DBNull.Value)
                    {
                        itemPedido.qtdCaixaProduto = Convert.ToString(linha["qtd_fechada"]);
                        //Recebe a quantidade do item
                        qtdFechada = Convert.ToInt32(linha["qtd_fechada"]);
                    }

                    if (linha["uni_fechada"] != DBNull.Value)
                    {
                        itemPedido.uniCaixa = Convert.ToString(linha["uni_fechada"]);
                    }

                    if (linha["fracionado"] != DBNull.Value)
                    {
                        itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]);

                        qtdFracionada = Convert.ToInt32(linha["fracionado"]);

                        //Verifica se existe flow rack
                        if (linha["flow"] != DBNull.Value)
                        {
                            //Subtrai a quantidade do flow rack
                            itemPedido.qtdUnidadeProduto = Convert.ToInt32(linha["fracionado"]) - Convert.ToInt32(linha["flow"]);

                            qtdFracionada = Convert.ToInt32(linha["fracionado"]) - Convert.ToInt32(linha["flow"]);
                        }
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (qtdFechada + qtdFracionada > 0)
                    {
                        //Adiona o objêto a coleção
                        itemPedidoCollection.Add(itemPedido);
                    }

                }
                //Retorna a coleção de cadastro encontrada
                return itemPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rastreamento de conferencia do volume. \nDetalhes:" + ex.Message);
            }
        }


        //Produto
        //Pesquisa a conferencia do produto
        public ItensFlowRackCollection PesqConferenciaFlowRackProduto(string dataIncial, string dataFinal, string codProduto, string codPedido, string codUsuario, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                ItensFlowRackCollection itemCollection = new ItensFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select iflow_data_conferencia, ped_codigo, iflow_barra, iflow_numero, p.prod_codigo, p.prod_descricao, r.iflow_quantidade, r.iflow_qtd_conferida, r.iflow_corte," +
                                "u.usu_login as usu_conferente, iflow_audita, iflow_data_auditoria, e.est_descricao, ui.usu_login as usu_auditor, a.apa_endereco, uii.usu_login as usu_enderecamento " +
                                "from wms_rastreamento_flowrack r " +
                                "inner join wms_produto p " +
                                "on p.prod_id = r.prod_id and p.conf_codigo = r.conf_codigo " +
                                "inner join wms_estacao e " +
                                "on e.est_codigo = r.est_codigo " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = r.usu_codigo_conferente " +
                                "left join wms_usuario ui " +
                                "on ui.usu_codigo = r.usu_codigo_auditor " +
                                "left join wms_usuario uii " +
                                "on uii.usu_codigo = r.usu_codigo_apa " +
                                "left join wms_apartamento a " +
                                "on a.apa_codigo = r.apa_codigo ";
                                

                if (codProduto != string.Empty)
                {
                    select += "where iflow_data_conferencia between @dataInicial and @dataFinal and p.prod_codigo = @codProduto " +
                              "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }
                else if (codPedido != string.Empty)
                {
                    select += "where r.ped_codigo = @codPedido and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }
                else if (codUsuario != string.Empty)
                {
                    select += "where iflow_data_conferencia between @dataInicial and @dataFinal  and r.usu_codigo_conferente = @codUsuario " +
                              "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }
                else
                {
                    select += "where iflow_data_conferencia between @dataInicial and @dataFinal and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }

                select += "order by iflow_data_conferencia";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    ItensFlowRack item = new ItensFlowRack();

                    if (linha["iflow_data_conferencia"] != DBNull.Value)
                    {
                        item.dataConferencia = Convert.ToDateTime(linha["iflow_data_conferencia"]);
                    }

                    if (linha["est_descricao"] != DBNull.Value)
                    {
                        item.descEstacao = Convert.ToString(linha["est_descricao"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        item.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["iflow_barra"] != DBNull.Value)
                    {
                        item.barraVolume = Convert.ToString(linha["iflow_barra"]);
                    }

                    if (linha["iflow_numero"] != DBNull.Value)
                    {
                        item.numeroVolume = Convert.ToInt32(linha["iflow_numero"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.codProduto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                    }

                    if (linha["iflow_quantidade"] != DBNull.Value)
                    {
                        item.qtdProduto = Convert.ToInt32(linha["iflow_quantidade"]);
                    }

                    if (linha["iflow_qtd_conferida"] != DBNull.Value)
                    {
                        item.qtdConferidaProduto = Convert.ToInt32(linha["iflow_qtd_conferida"]);
                    }

                    if (linha["iflow_corte"] != DBNull.Value)
                    {
                        item.qtdCorteProduto = Convert.ToInt32(linha["iflow_corte"]);
                    }

                    if (linha["usu_conferente"] != DBNull.Value)
                    {
                        item.nomeUsuario = Convert.ToString(linha["usu_conferente"]);
                    }

                    if (linha["iflow_data_auditoria"] != DBNull.Value)
                    {
                        item.dataAuditoria = Convert.ToDateTime(linha["iflow_data_auditoria"]);
                    }

                    if (linha["usu_auditor"] != DBNull.Value)
                    {
                        item.nomeAuditor = Convert.ToString(linha["usu_auditor"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["usu_enderecamento"] != DBNull.Value)
                    {
                        item.nomeEndereco = Convert.ToString(linha["usu_enderecamento"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rastreamento de conferencia do item no flow rack. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a conferencia do produto
        public ItensFlowRackCollection PesqCorteProduto(string dataIncial, string dataFinal, string codProduto, string codPedido, string codUsuario, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                ItensFlowRackCollection itemCollection = new ItensFlowRackCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@dataInicial", dataIncial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select corte_data, u.usu_login, f.perf_descricao, u.usu_turno, ped_codigo, " +
                                "p.prod_codigo, prod_descricao, " +
                                "COALESCE((select iped_quantidade from wms_item_pedido where ped_codigo = c.ped_codigo and prod_id = c.prod_id), 0) as iped_quantidade, " +
                                "COALESCE((select iped_qtd_conferencia from wms_item_pedido where ped_codigo = c.ped_codigo and prod_id = c.prod_id), 0) as iped_qtd_conferida, " +
                                "c.corte_quantidade, uni_unidade  from wms_corte_produto c " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = c.usu_codigo " +
                                "inner join wms_perfil f " +
                                "on f.perf_codigo = u.perf_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = c.prod_id and p.conf_codigo = c.conf_codigo " +
                                "inner join wms_unidade n " +
                                "on n.uni_codigo = p.uni_codigo_picking " +
                                "where corte_data between @dataInicial and @dataFinal " +
                                "and c.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                if (codProduto != string.Empty)
                {
                    select += "and p.prod_codigo = @codProduto ";
                }

                if (codPedido != string.Empty)
                {
                    select += "and c.ped_codigo = @codPedido ";
                }

                select += "order by corte_data";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    ItensFlowRack item = new ItensFlowRack();

                    if (linha["corte_data"] != DBNull.Value)
                    {
                        item.dataConferencia = Convert.ToDateTime(linha["corte_data"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        item.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        item.codProduto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                    }

                    if (linha["iped_quantidade"] != DBNull.Value)
                    {
                        item.qtdProduto = Convert.ToInt32(linha["iped_quantidade"]) + Convert.ToInt32(linha["corte_quantidade"]);
                    }

                    if (linha["iped_qtd_conferida"] != DBNull.Value)
                    {
                        item.qtdConferidaProduto = Convert.ToInt32(linha["iped_qtd_conferida"]);
                    }

                    if (linha["corte_quantidade"] != DBNull.Value)
                    {
                        item.qtdCorteProduto = Convert.ToInt32(linha["corte_quantidade"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        item.uniProduto = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        item.nomeUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["perf_descricao"] != DBNull.Value)
                    {
                        item.perfilUsuario = Convert.ToString(linha["perf_descricao"]);
                    }

                    if (linha["usu_turno"] != DBNull.Value)
                    {
                        item.turnoUsuario = Convert.ToString(linha["usu_turno"]);
                    }

                    itemCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return itemCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o rastreamento de corte do item \nDetalhes:" + ex.Message);
            }
        }

    }
}
