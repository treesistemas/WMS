using Dados;
using ObjetoTransferencia.Impressao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Impressao
{
    public class WmsxErpNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o estoque wms vs picking
        public WmsxErpCollection PesqWmsxErp(string tipo, int regiao, int rua, string lado, string usuario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adicionar parâmetros
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@lado", lado);

                //Instância a camada de objetos
                WmsxErpCollection produtoCollection = new WmsxErpCollection();
                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "p.prod_codigo, p.prod_descricao, s.sep_estoque as picking, coalesce(sum(a.arm_quantidade), 0) as pulmao, " +
                                "(select coalesce(sum(i.iped_quantidade),0) from wms_item_pedido i " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "where p.ped_nota_fiscal is null and not p.ped_fim_conferencia is null and i.prod_id = s.prod_id) as conferidos, " +
                                "coalesce(e.est_quantidade, 0) as ERP, uni_unidade " +
                                "from wms_separacao s " +
                                "inner join  wms_produto p " +
                                "on s.prod_id = p.prod_id " +
                                "inner join wms_estoque e " +
                                "on p.prod_id = e.prod_id " +
                                "left join wms_armazenagem a " +
                                "on a.prod_id = p.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking ";


                if (regiao > 0)
                {
                    select += "inner join wms_apartamento ap " +
                     "on ap.apa_codigo = s.apa_codigo " +
                     "inner join wms_nivel n " +
                     "on n.niv_codigo = ap.niv_codigo " +
                     "inner join wms_bloco b " +
                     "on b.bloc_codigo = n.bloc_codigo " +
                     "inner join wms_rua r " +
                     "on r.rua_codigo = b.rua_codigo " +
                     "inner join wms_regiao rg " +
                     "on rg.reg_codigo = r.reg_codigo " +
                     "where rg.reg_numero = @regiao ";

                    if (rua > 0)
                    {
                        select += "and r.rua_numero = @rua ";
                    }

                    if (!lado.Equals("TODOS"))
                    {
                        select += "and b.bloc_lado = @lado ";
                    }
                }

                select += "group by s.prod_id, p.prod_codigo, p.prod_descricao, sep_estoque, est_quantidade, uni_unidade";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    WmsxErp produto = new WmsxErp();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        produto.empresa = Convert.ToString(linha["empresa"]);

                        produto.usuario = usuario;

                        produto.regiao = regiao;

                        produto.rua = rua;

                        produto.lado = lado;
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["picking"] != DBNull.Value)
                    {
                        produto.estoquePicking = Convert.ToInt32(linha["picking"]);
                    }

                    if (linha["pulmao"] != DBNull.Value)
                    {
                        produto.estoquePulmao = Convert.ToInt32(linha["pulmao"]);
                    }

                    if (linha["conferidos"] != DBNull.Value)
                    {
                        produto.estoqueEmconferencia = Convert.ToInt32(linha["conferidos"]);
                    }

                    if (linha["erp"] != DBNull.Value)
                    {
                        produto.estoqueErp = Convert.ToInt32(linha["erp"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        produto.unidade = Convert.ToString(linha["uni_unidade"]);
                    }

                    produtoCollection.Add(produto);
                }

                //Retorna o valor encontrado
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro o relatório de estoque sem picking. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o estoque wms vs picking com o flowrack
        public WmsxErpCollection PesqWmsComFlowRackxErp(string tipo, int regiao, int rua, string lado, String codProduto, string usuario)
        {
            try
            {
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adicionar parâmetros
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@produto", codProduto);
                conexao.AdicionarParamentros("@lado", lado);

                //Instância a camada de objetos
                WmsxErpCollection produtoCollection = new WmsxErpCollection();
                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao where conf_codigo = 1) as empresa, " +
                                "apa_endereco, apa_ordem, p.prod_codigo, p.prod_descricao, s.sep_estoque as picking, " +
                                "coalesce((select ss.sep_estoque from wms_separacao ss where ss.sep_tipo = 'FLOWRACK' and ss.prod_id = s.prod_id),0) as flowrack, " +
                                "coalesce((select sum(r.iflow_qtd_conferida) as qtdFlowRack from wms_rastreamento_flowrack r " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = r.ped_codigo " +
                                "where p.ped_fim_conferencia is null and r.prod_id = s.prod_id),0) as volume, " +
                                "coalesce((select sum(inot_quantidade_conf) from wms_itens_nota ii " +
                                "inner join wms_nota_entrada nn " +
                                "on nn.not_codigo = ii.not_codigo " +
                                "where inot_quantidade_conf > coalesce(inot_armazenagem, 0) and ii.prod_id = s.prod_id),0) as entrada, " +
                                "coalesce(sum(a.arm_quantidade), 0) as pulmao, " +
                                "(select coalesce(sum(i.iped_quantidade), 0) from wms_item_pedido i " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = i.ped_codigo " +
                                "where p.ped_nota_fiscal is null and not p.ped_fim_conferencia is null and i.prod_id = s.prod_id) as conferidos, " +
                                "(select coalesce(sum(iped_quantidade), 0) from wms_item_pedido as i3 " +
                                "inner join wms_pedido p3 " +
                                "on p3.ped_codigo = i3.ped_codigo " +
                                "inner join wms_tipo_pedido ip " +
                                "on ip.tipo_codigo = p3.tipo_codigo " +
                                "where ip.tipo_descricao like '%DEV%' and p3.ped_data between DATEADD(-0 DAY TO CURRENT_DATE) || ' 00:00:01' and current_date || ' 23:59:59' and i3.prod_id = s.prod_id) as devolucao, " +
                                "(select coalesce(sum(iped_quantidade), 0) from wms_item_pedido as i3 " +
                                "inner join wms_pedido p3 " +
                                "on p3.ped_codigo = i3.ped_codigo " +
                                "inner join wms_tipo_pedido ip " +
                                "on ip.tipo_codigo = p3.tipo_codigo " +
                                "where p3.ped_status = 'EXCLUIDO' and p3.ped_data between DATEADD(-0 DAY TO CURRENT_DATE) || ' 00:00:00' and current_date || ' 23:59:59' and i3.prod_id = s.prod_id) as cancelado, " +
                                "coalesce(e.est_quantidade, 0) as ERP, uni_unidade " +
                                "from wms_separacao s " +
                                "inner join  wms_produto p " +
                                "on s.prod_id = p.prod_id " +
                                "inner join wms_estoque e " +
                                "on p.prod_id = e.prod_id " +
                                "left join wms_armazenagem a " +
                                "on a.prod_id = p.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = s.apa_codigo ";


                if (tipo.Equals("TODOS"))
                {
                    select += "where sep_tipo = 'CAIXA' ";
                }
                else
                {
                    if (!(codProduto == null || codProduto.Equals("") || codProduto == string.Empty))
                    {
                        select += "where sep_tipo = 'CAIXA' and prod_codigo = @produto ";
                    }

                    if (regiao > 0 && (codProduto == null || codProduto.Equals("") || codProduto == string.Empty))
                    {
                        select += " inner join wms_nivel n " +
                         "on n.niv_codigo = ap.niv_codigo " +
                         "inner join wms_bloco b " +
                         "on b.bloc_codigo = n.bloc_codigo " +
                         "inner join wms_rua r " +
                         "on r.rua_codigo = b.rua_codigo " +
                         "inner join wms_regiao rg " +
                         "on rg.reg_codigo = r.reg_codigo " +
                         "where rg.reg_numero = @regiao ";

                        if (rua > 0)
                        {
                            select += "and r.rua_numero = @rua ";
                        }

                        if (!lado.Equals("TODOS"))
                        {
                            select += "and b.bloc_lado = @lado ";
                        }
                    }
                }

                select += "group by apa_endereco, apa_ordem, s.prod_id, p.prod_codigo, p.prod_descricao, sep_estoque, est_quantidade, uni_unidade " +
                          "order by apa_ordem ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    WmsxErp produto = new WmsxErp();

                    if (linha["empresa"] != DBNull.Value)
                    {
                        produto.empresa = Convert.ToString(linha["empresa"]);

                        produto.usuario = usuario;

                        produto.regiao = regiao;

                        produto.rua = rua;

                        produto.lado = lado;
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        produto.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        produto.produto = Convert.ToString(linha["prod_codigo"]) + " - " + Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["picking"] != DBNull.Value)
                    {
                        produto.estoquePicking = Convert.ToInt32(linha["picking"]);
                    }

                    if (linha["flowrack"] != DBNull.Value)
                    {
                        produto.estoquePickingFlowRack = Convert.ToInt32(linha["flowrack"]);
                    }

                    if (linha["volume"] != DBNull.Value)
                    {
                        produto.estoqueVolume = Convert.ToInt32(linha["volume"]);
                    }

                    if (linha["pulmao"] != DBNull.Value)
                    {
                        produto.estoquePulmao = Convert.ToInt32(linha["pulmao"]);
                    }

                    if (linha["conferidos"] != DBNull.Value)
                    {
                        produto.estoqueEmconferencia = Convert.ToInt32(linha["conferidos"]);
                    }

                    if (linha["devolucao"] != DBNull.Value)
                    {
                        produto.estoqueDevolucao = Convert.ToInt32(linha["devolucao"]);
                    }

                    if (linha["cancelado"] != DBNull.Value)
                    {
                        produto.estoqueCancelados = Convert.ToInt32(linha["cancelado"]);
                    }

                    if (linha["erp"] != DBNull.Value)
                    {
                        produto.estoqueErp = Convert.ToInt32(linha["erp"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        produto.unidade = Convert.ToString(linha["uni_unidade"]);
                    }

                    produtoCollection.Add(produto);
                }

                //Retorna o valor encontrado
                return produtoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro o relatório de estoque sem picking. \nDetalhes:" + ex.Message);
            }
        }

    }
}
