using System;
using System.Data;
using ObjetoTransferencia.Relatorio;
using Dados;
using System.Linq;

namespace Negocios
{
    public class ImpressaoRendimentoExpedicaoNegocios
    {
        Conexao conexao = new Conexao();

        //Pesquisa o rendimento dos separadores
        public RendimentoExpedicaoCollection PesqRendimentoConferente(string dataInicial, string dataFinal)
        {
            try
            {
                RendimentoExpedicaoCollection rendimentoColection = new RendimentoExpedicaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataInicial);
                conexao.AdicionarParamentros("@dataFinal", dataFinal);

                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                                "'['|| p.usu_codigo_conferente || '] ' || u.usu_login as usu_login, count(p.ped_codigo) as count_pedido, " +
                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select count(i.prod_id) from wms_item_pedido i " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where trunc(iped_quantidade / po.prod_fator_pulmao) > 0 and pp.usu_codigo_conferente = p.usu_codigo_conferente and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select count(i.prod_id) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end " +
                                ") as count_produto, " +

                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select sum(trunc(iped_quantidade / po.prod_fator_pulmao)) from wms_item_pedido i " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where trunc(iped_quantidade / po.prod_fator_pulmao) > 0 and pp.usu_codigo_conferente = p.usu_codigo_conferente and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select sum(iped_quantidade) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end " +
                                ") as soma_item, " +

                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select count(distinct(r.iflow_barra)) from wms_rastreamento_flowrack r " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = r.ped_codigo " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "0 " +
                                "end " +
                                ") as count_volume, " +

                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +

                                "(select sum(trunc(I.iped_qtd_falta_conferencia/ po.prod_fator_pulmao)) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and i.iped_qtd_falta_conferencia > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select sum(iped_qtd_falta_conferencia) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and i.iped_qtd_falta_conferencia > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end " +
                                ")  as item_falta, " +
                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +

                                "(select sum(trunc(iped_qtd_sobra_conferencia/ po.prod_fator_pulmao)) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and i.iped_qtd_sobra_conferencia > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select sum(iped_qtd_sobra_conferencia) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and i.iped_qtd_sobra_conferencia > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +

                                "end " +
                                ") as item_sobra, " +
                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select sum(trunc(iped_qtd_troca_conferencia/ po.prod_fator_pulmao)) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and i.iped_qtd_troca_conferencia > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select sum(iped_qtd_troca_conferencia) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_conferente = p.usu_codigo_conferente and i.iped_qtd_troca_conferencia > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end " +
                                ")  as item_troca, " +
                                "sum(ped_peso) as soma_peso, sum (cast(ped_fim_separacao as time) - cast(ped_inicio_separacao as time)) as segundos " +
                                "from wms_pedido p " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = p.usu_codigo_conferente " +
                                "where p.ped_fim_conferencia between @dataInicial and @dataFinal " +
                                "group by usu_login,  p.usu_codigo_conferente";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    RendimentoExpedicao rendimento = new RendimentoExpedicao();

                    //Adiciona os valores encontrados
                   if (linha["empresa"] != DBNull.Value)
                    {
                        rendimento.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        rendimento.login = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["count_pedido"] != DBNull.Value)
                    {
                        rendimento.qtdPedido = Convert.ToInt32(linha["count_pedido"]);
                    }

                    if (linha["count_produto"] != DBNull.Value)
                    {
                        rendimento.qtdAcesso = Convert.ToInt32(linha["count_produto"]);
                    }

                    if (linha["soma_item"] != DBNull.Value)
                    {
                        rendimento.qtdItem = Convert.ToInt32(linha["soma_item"]);
                    }

                    if (linha["count_volume"] != DBNull.Value)
                    {
                        rendimento.qtdVolume = Convert.ToInt32(linha["count_volume"]);
                    }

                    if (linha["item_falta"] != DBNull.Value)
                    {
                        rendimento.qtdFalta = Convert.ToInt32(linha["item_falta"]);
                    }

                    if (linha["item_sobra"] != DBNull.Value)
                    {
                        rendimento.qtdSobra = Convert.ToInt32(linha["item_sobra"]);
                    }

                    if (linha["item_troca"] != DBNull.Value)
                    {
                        rendimento.qtdTroca = Convert.ToInt32(linha["item_troca"]);
                    }

                    if (linha["soma_peso"] != DBNull.Value)
                    {
                        rendimento.peso = Convert.ToInt32(linha["soma_peso"]);
                    }

                    if (linha["segundos"] != DBNull.Value)
                    {
                        rendimento.tempo = TimeSpan.FromSeconds(Convert.ToInt32(linha["segundos"]));
                    }

                    if (linha["segundos"] != DBNull.Value)
                    {
                        rendimento.segundos = Convert.ToDouble(linha["segundos"]);
                    }

                    if (linha["count_produto"] != DBNull.Value && linha["count_volume"] != DBNull.Value)
                    {
                        rendimento.qtdTotal = rendimento.qtdAcesso + rendimento.qtdVolume;
                    }

                    rendimento.dataInicial = Convert.ToDateTime(dataInicial);
                    rendimento.dataFinal = Convert.ToDateTime(dataFinal);


                    rendimentoColection.Add(rendimento);
                }

                //Retorna o valor encontrado
                return rendimentoColection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar às pendência. \nDetalhes:" + ex.Message);
            }
        }


        //Pesquisa o rendimento dos separadores
        public RendimentoExpedicaoCollection PesqRendimentoSeparacao(string dataInicial, string dataFinal)
        {
            try
            {
                RendimentoExpedicaoCollection rendimentoColection = new RendimentoExpedicaoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataInicial);
                conexao.AdicionarParamentros("@dataFinal", dataFinal);

                //String de consulta
                string select = "select (select conf_empresa from wms_configuracao ) as empresa, " +
                                "p.usu_codigo_separador || ' - ' || u.usu_login as usu_login, count(p.ped_codigo) as count_pedido, " +
                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select count(i.prod_id) from wms_item_pedido i " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where trunc(iped_quantidade / po.prod_fator_pulmao) > 0 and pp.usu_codigo_separador = p.usu_codigo_separador and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select count(i.prod_id) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end) as count_produto, " +

                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select sum(trunc(iped_quantidade / po.prod_fator_pulmao)) from wms_item_pedido i " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where trunc(iped_quantidade / po.prod_fator_pulmao) > 0 and pp.usu_codigo_separador = p.usu_codigo_separador and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select sum(iped_quantidade) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end) as soma_item, " +

                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select count(distinct(r.iflow_barra)) from wms_rastreamento_flowrack r " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = r.ped_codigo " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "0 " +
                                "end) as count_volume, " +

                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select sum(trunc(iped_qtd_falta_separacao/ po.prod_fator_pulmao)) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and i.iped_qtd_falta_separacao > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select sum(iped_qtd_falta_separacao) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and i.iped_qtd_falta_separacao > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end)  as item_falta, " +
                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select sum(trunc(iped_qtd_sobra_separacao/ po.prod_fator_pulmao)) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and i.iped_qtd_sobra_separacao > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select sum(iped_qtd_sobra_separacao) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and i.iped_qtd_sobra_separacao > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end) as item_sobra, " +
                                "(case when (select iconf_status from wms_itens_configuracao where iconf_descricao = 'UTILIZA FLOW RACK NAS OPERAÇÕES') = 'True' then " +
                                "(select sum(trunc(iped_qtd_troca_separacao/ po.prod_fator_pulmao)) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "inner join wms_produto po " +
                                "on po.prod_id = i.prod_id " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and i.iped_qtd_troca_separacao > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "else " +
                                "(select sum(iped_qtd_troca_separacao) from wms_item_pedido i " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = i.ped_codigo " +
                                "where pp.usu_codigo_separador = p.usu_codigo_separador and i.iped_qtd_troca_separacao > 0 and pp.ped_fim_conferencia between @dataInicial and @dataFinal) " +
                                "end)  as item_troca, " +

                                "sum(ped_peso) as soma_peso, sum (cast(ped_fim_separacao as time) - cast(ped_inicio_separacao as time)) as segundos " +
                                "from wms_pedido p " +
                                "inner join wms_usuario u " +
                                "on u.usu_codigo = p.usu_codigo_separador " +
                                "where p.ped_fim_conferencia between @dataInicial and @dataFinal " +
                                "group by usu_login,  p.usu_codigo_separador";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objeto
                    RendimentoExpedicao rendimento = new RendimentoExpedicao();

                    //Adiciona os valores encontrados
                    if (linha["empresa"] != DBNull.Value)
                    {
                        rendimento.empresa = Convert.ToString(linha["empresa"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        rendimento.login = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["count_pedido"] != DBNull.Value)
                    {
                        rendimento.qtdPedido = Convert.ToInt32(linha["count_pedido"]);
                    }

                    if (linha["count_produto"] != DBNull.Value)
                    {
                        rendimento.qtdAcesso = Convert.ToInt32(linha["count_produto"]);
                    }

                    if (linha["soma_item"] != DBNull.Value)
                    {
                        rendimento.qtdItem = Convert.ToInt32(linha["soma_item"]);
                    }

                    if (linha["count_volume"] != DBNull.Value)
                    {
                        rendimento.qtdVolume = Convert.ToInt32(linha["count_volume"]);
                    }

                    if (linha["item_falta"] != DBNull.Value)
                    {
                        rendimento.qtdFalta = Convert.ToInt32(linha["item_falta"]);
                    }

                    if (linha["item_sobra"] != DBNull.Value)
                    {
                        rendimento.qtdSobra = Convert.ToInt32(linha["item_sobra"]);
                    }

                    if (linha["item_troca"] != DBNull.Value)
                    {
                        rendimento.qtdTroca = Convert.ToInt32(linha["item_troca"]);
                    }

                    if (linha["soma_peso"] != DBNull.Value)
                    {
                        rendimento.peso = Convert.ToInt32(linha["soma_peso"]);
                    }

                    if (linha["segundos"] != DBNull.Value)
                    {   
                        rendimento.tempo = TimeSpan.FromSeconds(Convert.ToInt32(linha["segundos"]));
                    }

                    if (linha["segundos"] != DBNull.Value)
                    {
                        rendimento.segundos = Convert.ToDouble(linha["segundos"]);
                    }

                    if(linha["count_produto"] != DBNull.Value && linha["count_volume"] != DBNull.Value)
                    {
                        rendimento.qtdTotal = rendimento.qtdAcesso + rendimento.qtdVolume;
                    }

                    rendimento.dataInicial = Convert.ToDateTime(dataInicial);
                    rendimento.dataFinal = Convert.ToDateTime(dataFinal);

                    rendimentoColection.Add(rendimento);
                }

                //Retorna o valor encontrado
                return rendimentoColection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar às pendência. \nDetalhes:" + ex.Message);
            }
        }

    }
}

