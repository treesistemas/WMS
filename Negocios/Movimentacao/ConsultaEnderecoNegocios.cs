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
    public class ConsultaEnderecoNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o endereço
        public ConsultaEnderecoCollection PesqEndereco(string tipo, string status, string regiao, string rua, string bloco, string nivel, string apartamento, string lado)
        {
            try
            {
                //Instância o objêto
                ConsultaEnderecoCollection consultaEnderecoCollection = new ConsultaEnderecoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmentros
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@regiao", regiao);
                conexao.AdicionarParamentros("@rua", rua);
                conexao.AdicionarParamentros("@bloco", bloco);
                conexao.AdicionarParamentros("@nivel", nivel);
                conexao.AdicionarParamentros("@apartamento", apartamento);
                conexao.AdicionarParamentros("@lado", lado);

                //String de consulta
                string select = "select prtmntddscndrc, codprtmntd, prtmntdrgd, prtmntdrd, prtmntdblcd, prtmntdnvld, prtmntdnr, prtmntdld, prtmntdstts, prtmntdrsrvd, prtmntdtmnh, ctgrdsc, prtmntddspsc  from briwprtmntd apto " +
                                "left outer join briwctgr cat " +
                                "on cat.codctgr = apto.codctgr " +
                                "where prtmntdrgd = @regiao and prtmntdrd = @rua ";

                if (!(tipo.Equals("All")))
                {
                    select += "and prtmntdtp = @tipo ";
                }

                if (status.Equals("Unavailable"))
                {
                    select += "and prtmntddspsc = 1 ";
                }
                else if (!(status.Equals("Todos")))
                {
                    select += "and prtmntdstts = @status and prtmntddspsc = 0 ";
                }

                if (!(bloco.Equals("Selecione")))
                {
                    select += "and prtmntdblcd = @bloco ";
                }

                if (!(nivel.Equals("Selecione")))
                {
                    select += "and prtmntdnvld = @nivel ";
                }

                if (!(nivel.Equals("Selecione")) && !(apartamento.Equals("Selecione")))
                {
                    select += "and prtmntdnr = @apartamento ";
                }

                if (!(lado.Equals("All")))
                {
                    select += "and prtmntdld = @lado";
                }

               


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    ConsultaEndereco consultaEndereco = new ConsultaEndereco();
                    //Adiciona os valores encontrados
                    consultaEndereco.endereco = Convert.ToString(linha["prtmntddscndrc"]);
                    consultaEndereco.codEndereco = Convert.ToInt32(linha["codprtmntd"]);
                    consultaEndereco.regiao = Convert.ToInt32(linha["prtmntdrgd"]);
                    consultaEndereco.rua = Convert.ToInt32(linha["prtmntdrd"]);
                    consultaEndereco.bloco = Convert.ToInt32(linha["prtmntdblcd"]);
                    consultaEndereco.nivel = Convert.ToInt32(linha["prtmntdnvld"]);
                    consultaEndereco.apartamento = Convert.ToInt32(linha["prtmntdnr"]);
                    consultaEndereco.lado = Convert.ToString(linha["prtmntdld"]);
                    consultaEndereco.tamanho = Convert.ToString(linha["prtmntdtmnh"]);

                    if (linha["prtmntdstts"].Equals("Vacant"))
                    {
                        consultaEndereco.status = "Vago";
                    }
                    else
                    {
                        consultaEndereco.status = "Ocupado";
                    }

                    if (Convert.ToInt32(linha["prtmntddspsc"]) == 0)
                    {
                        consultaEndereco.disposicao = "Sim";
                    }
                    else
                    {
                        consultaEndereco.disposicao = "Não";
                    }

                    if (linha["prtmntdrsrvd"].Equals("N"))
                    {
                        consultaEndereco.reserva = "Não";
                    }
                    else
                    {
                        consultaEndereco.reserva = "Sim";
                    }

                    if (linha["ctgrdsc"] == DBNull.Value)
                    {
                        consultaEndereco.categoria= "Não associada";
                    }
                    else
                    {
                        consultaEndereco.categoria = Convert.ToString(linha["ctgrdsc"]); 
                    }

                    consultaEnderecoCollection.Add(consultaEndereco);
                }

                return consultaEnderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereço. \nDetalhes: " + ex.Message);
            }
        }
    }
}
