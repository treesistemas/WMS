using Dados;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Expedicao
{
    public class DinamicaInteriorNegocio
    {

        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa por data 
        public DinamicaInteriorCollection PesqDinamicaInterior(string dataInicial, string dataFinal)
        {
            try
            {
                DinamicaInteriorCollection dinamicaInteriorCollection = new DinamicaInteriorCollection();
                //Instância uma coleção de objêtos
                //Limpa os parâmetros
                conexao.LimparParametros();

                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");


                //String de consulta - saida manifesto
                string select = "select  mani_data, r.rota_descricao, " +
                                         "p.mani_codigo, " +
                                         "count(ped_codigo) AS pediCodigo," +
                                         "count(distinct(p.cli_id)) AS Cliente," +
                                         "sum(ped_peso) AS Peso," +
                                         "sum(ped_total) AS Valor," +
                                         "mot.mot_apelido AS Apelido," +
                                         "v.vei_placa AS Placa," +
                                "(select first 1 cid.cid_nome from wms_pedido p " +
                                        "inner join wms_cliente c on c.cli_id = p.cli_id " +
                                        "inner join wms_bairro b on b.bar_codigo = c.bar_codigo " +
                                        "inner join wms_cidade cid on cid.cid_codigo = b.cid_codigo " +
                                "where mani_codigo = m.mani_codigo " +
                                        "group by cid.cid_nome " +
                                        "order by count(cid.cid_codigo) desc) as Cidade " +
                                "from wms_pedido p " +
                                        "inner join wms_cliente c on c.cli_id = p.cli_id " +
                                        "inner join wms_rota r on r.rota_codigo = c.rota_codigo " +
                                        "inner join wms_manifesto m   on  p.mani_codigo = m.mani_codigo " +
                                        "inner join wms_motorista mot on  mot.mot_codigo = m.mot_codigo " +
                                        "left  join wms_veiculo   v   on  v.vei_codigo =   m.vei_codigo " +
                                        "where r.tipo_codigo = 3 and mani_data between @dataInicial and @dataFinal " +
                                        "group by m.mani_codigo, mani_data, p.mani_codigo, mot.mot_apelido, v.vei_placa, rota_descricao ";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)

                {
                    //Instância a camada de objêtos
                    DinamicaInterior dinamicaInterior = new DinamicaInterior();


                    //Percorre a tabela e preenche as linhas da gride
                    if (linha["mani_data"] != DBNull.Value)
                    {
                        dinamicaInterior.maniData = Convert.ToDateTime(linha["mani_data"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        dinamicaInterior.maniCodigo = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["Placa"] != DBNull.Value)
                    {
                        dinamicaInterior.Placa = Convert.ToString(linha["Placa"]);
                    }

                    if (linha["Apelido"] != DBNull.Value)
                    {
                        dinamicaInterior.Apelido = Convert.ToString(linha["Apelido"]);
                    }

                    if (linha["Cidade"] != DBNull.Value)
                    {
                        dinamicaInterior.Cidade = Convert.ToString(linha["Cidade"]);
                    }

                    if (linha["Peso"] != DBNull.Value)
                    {
                        dinamicaInterior.Peso = (double)Convert.ToDouble(linha["Peso"]);
                    }

                    if (linha["Valor"] != DBNull.Value)
                    {
                        dinamicaInterior.Valor = (double)Convert.ToDecimal(linha["Valor"]);
                    }

                    if (linha["Cliente"] != DBNull.Value)
                    {
                        dinamicaInterior.Cliente = Convert.ToInt32(linha["Cliente"]);
                    }
 
                    if (linha["pediCodigo"] != DBNull.Value)
                    {
                        dinamicaInterior.pediCodigo = Convert.ToInt32(linha["pediCodigo"]);
                    }

                    //Adiciona os valores encontrados a coleção
                    dinamicaInteriorCollection.Add(dinamicaInterior);
                }

                //Retorna a coleção de cadastro encontrada
                return dinamicaInteriorCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as Dinamica Interior. \nDetalhes:" + ex.Message);
            }

        }


    }
}
