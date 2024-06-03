using Dados;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Negocios.Expedicao
{



    public class SaidaManifestoNegocios
    {


        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa por data 
        public SaidaManifestoCollection PesqManisfesto(string empresa ,string dataInicial, string dataFinal)
        {
            try
            {
                SaidaManifestoCollection saidaManifestoCollection = new SaidaManifestoCollection();
                //Instância uma coleção de objêtos
                //Limpa os parâmetros
                conexao.LimparParametros();

                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");


                //String de consulta - saida manifesto
                string select = "select mani_data, mani_portatil, " +
                                        "p.mani_codigo, r.tipo_codigo, " +
                                        "(select count(ped_codigo) from wms_pedido where mani_codigo = p.mani_codigo) AS pedido, " +
                                        "count(distinct(p.cli_id)) as Cliente, " +
                                        "(select count(distinct(cli_id)) from wms_pedido where not ped_data_fim_entrega is null and mani_codigo = p.mani_codigo) AS cli_efetuado, " +
                                        "sum(ped_peso) as Peso, " +
                                        "sum(ped_total) as Valor, " +
                                        "mot.mot_apelido AS Apelido, " +
                                        "(select count(ped_codigo) from wms_pedido where not ped_data_fim_entrega is null and mani_codigo = p.mani_codigo) AS Efetuado, " +
                                        "v.vei_placa AS Placa, " +
                                        "(select first 1 cid.cid_nome from wms_pedido p " +
                                        "inner join wms_cliente c on c.cli_id = p.cli_id " +
                                        "inner join wms_bairro b on b.bar_codigo = c.bar_codigo " +
                                        "inner join wms_cidade cid on cid.cid_codigo = b.cid_codigo " +
                                "where  mani_codigo = m.mani_codigo group by cid.cid_nome order by count(cid.cid_codigo) desc) as Cidade " +
                                "from wms_pedido p  " +
                                "inner join wms_cliente c "+
                                "on c.cli_id = p.cli_id "+
                                "inner join wms_rota r "+ 
                                "on r.rota_codigo = c.rota_codigo " +
                                "inner join wms_manifesto m   on  p.mani_codigo = m.mani_codigo " +
                                "inner join wms_motorista mot on  mot.mot_codigo = m.mot_codigo " +
                                "left  join wms_veiculo   v   on  v.vei_codigo =   m.vei_codigo " +
                                "where mani_data between @dataInicial and @dataFinal " +
                                "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "group by m.mani_codigo, mani_data, mani_portatil, p.mani_codigo, mot.mot_apelido, v.vei_placa, r.tipo_codigo ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)

                {
                    //Instância a camada de objêtos
                    SaidaManifestoModel saidaManifesto = new SaidaManifestoModel();
                    //Adiciona os valores encontrados


                    //Percorre a tabela e preenche as linhas da gride
                    if (linha["mani_data"] != DBNull.Value)
                    {
                        saidaManifesto.ManiData = Convert.ToDateTime(linha["mani_data"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        saidaManifesto.Manicodigo = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["mani_portatil"] != DBNull.Value)
                    {
                        saidaManifesto.Portatil = Convert.ToString(linha["mani_portatil"]);
                    }

                    if (linha["pedido"] != DBNull.Value)
                    {
                        saidaManifesto.PedTotal = Convert.ToInt32(linha["pedido"]);
                    }

                    if (linha["Efetuado"] != DBNull.Value)
                    {
                        saidaManifesto.EfPedido = Convert.ToInt32(linha["Efetuado"]);

                        if (saidaManifesto.EfPedido > 0 && saidaManifesto.PedTotal > 0)
                        {
                           saidaManifesto.porPedido = (Convert.ToDouble(saidaManifesto.EfPedido) / Convert.ToDouble(saidaManifesto.PedTotal)) * 100;
                        }
                    }

                    if (linha["Peso"] != DBNull.Value)
                    {
                        saidaManifesto.PesoPedido = (double)Convert.ToDouble(linha["Peso"]);
                    }

                    if (linha["Valor"] != DBNull.Value)
                    {
                        saidaManifesto.Valor = (double)Convert.ToDecimal(linha["Valor"]);
                    }

                    if (linha["Cliente"] != DBNull.Value)
                    {
                        saidaManifesto.QtCliente = Convert.ToInt32(linha["Cliente"]);
                    }

                    if (linha["cli_efetuado"] != DBNull.Value)
                    {
                        saidaManifesto.EfCliente = Convert.ToInt32(linha["cli_efetuado"]);

                        if (saidaManifesto.EfCliente > 0 && saidaManifesto.QtCliente > 0)
                        {
                            saidaManifesto.porCliente = (Convert.ToDouble(saidaManifesto.EfCliente) / Convert.ToDouble(saidaManifesto.QtCliente)) * 100;
                        }
                    }

                    if (linha["Placa"] != DBNull.Value)
                    {
                        saidaManifesto.Veiculo = Convert.ToString(linha["Placa"]);
                    }
                

                    if (linha["Apelido"] != DBNull.Value)
                    {
                        saidaManifesto.Motorista = Convert.ToString(linha["Apelido"]);
                    }

                    if (linha["Cidade"] != DBNull.Value)
                    {
                        saidaManifesto.Regiao = Convert.ToString(linha["Cidade"]);
                    }

                    if (linha["tipo_codigo"] != DBNull.Value)
                    {
                        saidaManifesto.tipoRota = Convert.ToInt32(linha["tipo_codigo"]);
                    }

                   


                    //Adiciona os valores encontrados a coleção
                    saidaManifestoCollection.Add(saidaManifesto);
                }

                //Retorna a coleção de cadastro encontrada
                return saidaManifestoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os manifestos. \nDetalhes:" + ex.Message);
            }

        }



    }

}
