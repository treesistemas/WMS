using System;
using System.Data;
using ObjetoTransferencia.Impressao;
using Dados;

namespace Negocios.Impressao
{
    public class ImpressaoPickingNegativoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a pendencia
        public EnderecoCollection PesqPickingNegativo(int regiao, int rua)
        {
            try
            {
                //Instância a camada de objetos
                EnderecoCollection enderecoCollection = new EnderecoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adicionar parâmetros
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);

                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                                "a.apa_endereco, p.prod_codigo, p.prod_descricao, " +
                                "trunc(s.sep_estoque / p.prod_fator_pulmao) as qtd_caixa, u1.uni_unidade as uni_caixa, " +
                                "mod(s.sep_estoque, p.prod_fator_pulmao) as qtd_unidade, u2.uni_unidade as uni_unidade, " +
                                "s.sep_validade, s.sep_peso, s.sep_lote, p.prod_peso_variavel, s.sep_tipo from wms_separacao s " +
                                "inner join wms_produto p " +
                                "on s.prod_id = p.prod_id " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = s.apa_codigo " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = p.uni_codigo_pulmao " +
                                "left join wms_unidade u2 " +
                                "on u2.uni_codigo = p.uni_codigo_picking " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua r " +
                                "on r.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao rg " +
                                "on rg.reg_codigo = r.reg_codigo " +
                                "where s.sep_estoque < 0 ";

                if (regiao > 0)
                {
                    select += "and rg.reg_numero = @regiao ";

                    if (rua > 0)
                    {
                        select += "and r.rua_numero = @rua ";
                    }
                }

                select += "order by a.apa_ordem";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    Endereco endereco = new Endereco();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        endereco.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);

                        endereco.regiao = regiao;
                        endereco.rua = rua;
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtd_caixa"] != DBNull.Value)
                    {
                        endereco.estoqueCaixa = Convert.ToDouble(linha["qtd_caixa"]);
                    }

                    if (linha["uni_caixa"] != DBNull.Value)
                    {
                        endereco.unidadePulmao = Convert.ToString(linha["uni_caixa"]);
                    }

                    if (linha["qtd_unidade"] != DBNull.Value)
                    {
                        endereco.estoqueUnidade = Convert.ToDouble(linha["qtd_unidade"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        endereco.unidadePicking = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToString(linha["sep_validade"]);
                    }

                    if (linha["sep_peso"] != DBNull.Value)
                    {
                        endereco.peso = Convert.ToString(linha["sep_peso"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        endereco.pesoVariavel = Convert.ToString(linha["prod_peso_variavel"]);
                    }
                    else
                    {
                        endereco.pesoVariavel = "False";
                    }

                    enderecoCollection.Add(endereco);
                }

                //Retorna o valor encontrado
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro o relatório de estoque sem picking. \nDetalhes:" + ex.Message);
            }
        }

    }
}
