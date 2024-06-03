using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class RastreamentoProdutoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa os produtos
        public ProdutoCollection PesqProduto(string codProduto, string empresa, string dataInicial, string dataFinal)
        {
            try
            {
                //Instância a coleção
                ProdutoCollection produtoCollection = new ProdutoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //String de consulta
                string select = "select r.rast_data, u.usu_login, p.prod_codigo, p.prod_descricao, c.cat_codigo, c.cat_descricao, p.prod_tipo_armazenamento, " +
                "p.prod_vida_util, p.prod_tolerancia, p.prod_niv_maximo, p.prod_multiplo, p.prod_tipo_palete, p.prod_lastro_p, p.prod_altura_p, " +
                "p.prod_lastro_m, p.prod_altura_m, p.prod_lastro_g, p.prod_altura_g, p.prod_lastro_b, p.prod_altura_b, " +
                "p.prod_fator_compra, uc.uni_unidade as uni_compra,  p.prod_fator_pulmao, " +
                "(select uni_unidade from wms_unidade where uni_codigo = p.uni_codigo_pulmao) as uni_pulmao, " +
                "p.prod_fator_picking, " +
                "(select uni_unidade from wms_unidade where uni_codigo = p.uni_codigo_picking) as uni_separacao, " +
                "p.prod_peso, p.prod_status, p.prod_separacao_flowrack, p.prod_audita_flowrack, p.prod_controla_validade, " +
                "p.prod_palete_blocado, p.prod_palete_padrao " +
                "from wms_rastreamento_cadastro r " +
                "inner join wms_produto p " +
                "on p.prod_id = r.prod_id " +
                "left join wms_categoria c " +
                "on c.cat_codigo = p.cat_codigo " +
                "inner join wms_unidade uc " +
                "on uc.uni_codigo = p.uni_codigo_compra " +
                "inner join wms_usuario u " +
                "on u.usu_codigo = r.usu_codigo ";

                if (!codProduto.Equals(""))
                {
                    select += "where prod_codigo = @codProduto and rast_data between @dataInicial and @dataFinal ";
                }
                else
                {
                    select += "where rast_data between @dataInicial and @dataFinal ";                   
                }

                select += "and r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by rast_data";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Produto produto = new Produto();
                    //Adiciona os valores encontrados
                    if (linha["rast_data"] != DBNull.Value)
                    {
                        produto.dataAlteracao = Convert.ToDateTime(linha["rast_data"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        produto.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        produto.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["cat_codigo"] != DBNull.Value)
                    {
                        produto.codCategoria = Convert.ToInt32(linha["cat_codigo"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        produto.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["prod_tipo_armazenamento"] != DBNull.Value)
                    {
                        produto.tipoArmazenagem = Convert.ToString(linha["prod_tipo_armazenamento"]);
                    }

                    if (linha["prod_vida_util"] != DBNull.Value)
                    {
                        produto.shelfLife = Convert.ToInt32(linha["prod_vida_util"]);
                    }

                    if (linha["prod_tolerancia"] != DBNull.Value)
                    {
                        produto.tolerancia = Convert.ToInt32(linha["prod_tolerancia"]);
                    }

                    if (linha["prod_niv_maximo"] != DBNull.Value)
                    {
                        produto.nivelMaximo = Convert.ToInt32(linha["prod_niv_maximo"]);
                    }

                    if (linha["prod_tipo_palete"] != DBNull.Value)
                    {
                        produto.tipoPalete = Convert.ToString(linha["prod_tipo_palete"]);
                    }

                    if (linha["prod_lastro_p"] != DBNull.Value)
                    {
                        produto.lastroPequeno = Convert.ToInt32(linha["prod_lastro_p"]);
                    }

                    if (linha["prod_altura_p"] != DBNull.Value)
                    {
                        produto.alturaPequeno = Convert.ToInt32(linha["prod_altura_p"]);
                    }

                    if (linha["prod_lastro_m"] != DBNull.Value)
                    {
                        produto.lastroMedio = Convert.ToInt32(linha["prod_lastro_m"]);
                    }

                    if (linha["prod_altura_m"] != DBNull.Value)
                    {
                        produto.alturaMedio = Convert.ToInt32(linha["prod_altura_m"]);
                    }

                    if (linha["prod_lastro_g"] != DBNull.Value)
                    {
                        produto.lastroGrande = Convert.ToInt32(linha["prod_lastro_g"]);
                    }

                    if (linha["prod_altura_g"] != DBNull.Value)
                    {
                        produto.alturaGrande = Convert.ToInt32(linha["prod_altura_g"]);
                    }

                    if (linha["prod_lastro_b"] != DBNull.Value)
                    {
                        produto.lastroBlocado = Convert.ToInt32(linha["prod_lastro_b"]);
                    }
                    if (linha["prod_altura_b"] != DBNull.Value)
                    {
                        produto.alturaBlocado = Convert.ToInt32(linha["prod_altura_b"]);
                    }

                    if (linha["prod_fator_compra"] != DBNull.Value)
                    {
                        produto.fatorCompra = Convert.ToInt32(linha["prod_fator_compra"]);
                    }

                    if (linha["uni_compra"] != DBNull.Value)
                    {
                        produto.undCompra = Convert.ToString(linha["uni_compra"]);
                    }
                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        produto.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        produto.undPulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["prod_fator_picking"] != DBNull.Value)
                    {
                        produto.fatorPicking = Convert.ToInt32(linha["prod_fator_picking"]);
                    }
                    if (linha["uni_separacao"] != DBNull.Value)
                    {
                        produto.undPicking = Convert.ToString(linha["uni_separacao"]);
                    }

                    if (linha["prod_multiplo"] != DBNull.Value)
                    {
                        produto.multiploProduto = Convert.ToInt32(linha["prod_multiplo"]);
                    }

                    if (linha["prod_peso"] != DBNull.Value)
                    {
                        produto.pesoProduto = Convert.ToDouble(linha["prod_peso"]);
                    }

                    if (linha["prod_status"] != DBNull.Value)
                    {
                        produto.status = Convert.ToBoolean(linha["prod_status"]);
                    }
                    if (linha["prod_separacao_flowrack"] != DBNull.Value)
                    {
                        produto.separacaoFlowrack = Convert.ToBoolean(linha["prod_separacao_flowrack"]);
                    }
                    if (linha["prod_audita_flowrack"] != DBNull.Value)
                    {
                        produto.auditaFlowrack = Convert.ToBoolean(linha["prod_audita_flowrack"]);
                    }

                    if (linha["prod_controla_validade"] != DBNull.Value)
                    {
                        produto.controlaValidade = Convert.ToBoolean(linha["prod_controla_validade"]);
                    }
                    if (linha["prod_palete_blocado"] != DBNull.Value)
                    {
                        produto.paleteBlocado = Convert.ToBoolean(linha["prod_palete_blocado"]);
                    }
                    if (linha["prod_palete_padrao"] != DBNull.Value)
                    {
                        produto.paletePadrao = Convert.ToBoolean(linha["prod_palete_padrao"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    produtoCollection.Add(produto);
                }
                //Retorna a coleção de cadastro encontrada
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao efetuar a pesquisa de produto. \nDetalhes:" + ex.Message);
            }

        }

    }
}
