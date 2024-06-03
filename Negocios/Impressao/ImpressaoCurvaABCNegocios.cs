using System;
using System.Data;
using ObjetoTransferencia.Impressao;
using Dados;
using ObjetoTransferencia;
using System.Linq;

namespace Negocios.Impressao
{
    public class ImpressaoCurvaABCNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a curva abc no picking de caixa
        public ImpressaoCurvaAbcCollection PesqCaixaCurvaABC(int regiao, int rua, string dataInicial, string dataFinal, int dias)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adicionar Parâmetros
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //Instância a camada de objetos
                ImpressaoCurvaAbcCollection curvaAbcCollection = new ImpressaoCurvaAbcCollection();
                ///*Curva abc*/
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                                "a.apa_endereco, prod_codigo, prod_descricao, p.prod_fator_pulmao, u.uni_unidade, (p.prod_altura_m * p.prod_lastro_m) as palete, " +
                                "s.sep_capacidade, " +
                                "(select coalesce(sum(i.iped_quantidade), 0) " +
                                "from wms_item_pedido i " +
                                "where i.prod_id = s.prod_id and " +
                                "i.iped_data_conferencia between @dataInicial and @dataFinal)/prod_fator_pulmao as quantidade_vendida_cxa, s.est_codigo " +
                                "from wms_produto p " +
                                "inner join wms_separacao s " +
                                "on p.prod_id = s.prod_id " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao rg " +
                                "on rg.reg_codigo = r.reg_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "where s.sep_tipo = 'CAIXA' and rg.reg_numero = @regiao and r.rua_numero = @rua " +
                                "order by 9 desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    ImpressaoCurvaAbc curvaAbc = new ImpressaoCurvaAbc();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        curvaAbc.empresa = Convert.ToString(linha["empresa"]);

                        //Tipo
                        curvaAbc.tipo = "CAIXA";
                        //DataInicial
                        curvaAbc.dataInicial = Convert.ToDateTime(dataInicial);
                        //DataFinal
                        curvaAbc.dataFinal = Convert.ToDateTime(dataFinal);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        curvaAbc.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        curvaAbc.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        curvaAbc.fator = Convert.ToString(linha["prod_fator_pulmao"]) + " " + Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["palete"] != DBNull.Value)
                    {
                        curvaAbc.palete = Convert.ToInt32(linha["palete"]);
                    }

                    if (linha["sep_capacidade"] != DBNull.Value)
                    {
                        curvaAbc.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                    }

                    if (linha["quantidade_vendida_cxa"] != DBNull.Value)
                    {
                        curvaAbc.quantidade = Convert.ToInt32(linha["quantidade_vendida_cxa"]);

                        curvaAbc.valor = (Convert.ToDouble(curvaAbc.quantidade) / Convert.ToDouble(curvaAbc.capacidade)) * 100;

                        if (curvaAbc.valor >= 80)
                        {
                            curvaAbc.curva = "A";
                        }
                        else if (curvaAbc.valor >= 30)
                        {
                            curvaAbc.curva = "B";
                        }
                        else if (curvaAbc.valor < 29.99)
                        {
                            curvaAbc.curva = "C";
                        }
                    }


                    curvaAbcCollection.Add(curvaAbc);
                }

                //Retorna o valor encontrado
                return curvaAbcCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório da curva abc. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a curva abc no picking de flow rack
        public ImpressaoCurvaAbcCollection PesqFlowCurvaABC(int codEstacao, string dataInicial, string dataFinal, int dias)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adicionar Parâmetros
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //Instância a camada de objetos
                ImpressaoCurvaAbcCollection curvaAbcCollection = new ImpressaoCurvaAbcCollection();
                ///*Curva abc*/
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                                "s.est_codigo, a.apa_endereco, prod_codigo, prod_descricao, p.prod_fator_pulmao, u.uni_unidade, (p.prod_altura_m * p.prod_lastro_m) as palete, " +
                                "s.sep_capacidade, " +
                                "(select coalesce(sum(i.iflow_qtd_conferida), 0) "+
                                "from wms_rastreamento_flowrack i "+
                                "where i.prod_id = s.prod_id and i.iflow_data_conferencia between @dataInicial and @dataFinal)/prod_fator_pulmao as quantidade_vendida_cxa, s.est_codigo " +
                                "from wms_produto p " +
                                "inner join wms_separacao s " +
                                "on p.prod_id = s.prod_id " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao rg " +
                                "on rg.reg_codigo = r.reg_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "where s.sep_tipo = 'FLOWRACK' and s.est_codigo = @codEstacao " +
                                "order by 9 desc";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    ImpressaoCurvaAbc curvaAbc = new ImpressaoCurvaAbc();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        curvaAbc.empresa = Convert.ToString(linha["empresa"]);

                        //Tipo
                        curvaAbc.tipo = "FLOW RACK";
                        //DataInicial
                        curvaAbc.dataInicial = Convert.ToDateTime(dataInicial);
                        //DataFinal
                        curvaAbc.dataFinal = Convert.ToDateTime(dataFinal);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        curvaAbc.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        curvaAbc.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        curvaAbc.fator = Convert.ToString(linha["prod_fator_pulmao"]) + " " + Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        curvaAbc.estacao = Convert.ToString(linha["est_codigo"]);
                    }

                    if (linha["sep_capacidade"] != DBNull.Value)
                    {
                        curvaAbc.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                    }

                    if (linha["quantidade_vendida_cxa"] != DBNull.Value)
                    {
                        curvaAbc.quantidade = Convert.ToInt32(linha["quantidade_vendida_cxa"]);

                        curvaAbc.valor = (Convert.ToDouble(curvaAbc.quantidade) / Convert.ToDouble(curvaAbc.capacidade)) * 100;

                        if (curvaAbc.valor >= 80)
                        {
                            curvaAbc.curva = "A";
                        }
                        else if (curvaAbc.valor >= 30)
                        {
                            curvaAbc.curva = "B";
                        }
                        else if (curvaAbc.valor < 29.99)
                        {
                            curvaAbc.curva = "C";
                        }
                    }


                    curvaAbcCollection.Add(curvaAbc);
                }

                //Retorna o valor encontrado
                return curvaAbcCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório da curva abc. \nDetalhes:" + ex.Message);
            }
        }

    }
}
