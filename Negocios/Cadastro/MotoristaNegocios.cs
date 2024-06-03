using System;
using System.Data;
using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class MotoristaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o motorista no ERP Negocios
        public MotoristaCollection PesqMotorista(string empresa, string codigo, string apelido, bool status)
        {
            try
            {
                //Instância a coleção
                MotoristaCollection motoristaCollection = new MotoristaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@apelido", apelido);
                conexao.AdicionarParamentros("@status", status);
                //String de consulta
                string select = "select mot_codigo, mot_nome, mot_apelido, mot_cnh, mot_validade, mot_celular, mot_status " +
                    "from wms_motorista where mot_status = @status ";

                if(!codigo.Equals(""))
                {
                    select += "and mot_codigo = @codigo ";
                }
                else if(!apelido.Equals(""))
                {
                    select += $"and mot_apelido like '{apelido}%'";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Motorista motorista = new Motorista();
                    //Adiciona os valores encontrados
                    if (linha["mot_codigo"] != DBNull.Value)
                    {
                        motorista.codMotorista = Convert.ToInt32(linha["mot_codigo"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        motorista.nomeMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    if (linha["mot_apelido"] != DBNull.Value)
                    {
                        motorista.apelidoMotorista = Convert.ToString(linha["mot_apelido"]);
                    }

                    if (linha["mot_cnh"] != DBNull.Value)
                    {
                        motorista.CNHMotorista = Convert.ToString(linha["mot_cnh"]);
                    }

                    if (linha["mot_validade"] != DBNull.Value)
                    {
                        motorista.validadeCNH = Convert.ToDateTime(linha["mot_validade"]);
                    }

                    if (linha["mot_celular"] != DBNull.Value)
                    {
                        motorista.celularMotorista = Convert.ToString(linha["mot_celular"]);
                    }

                    if (linha["mot_status"] != DBNull.Value)
                    {
                        motorista.statusMotorista = Convert.ToBoolean(linha["mot_status"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    motoristaCollection.Add(motorista);
                }
                //Retorna a coleção de cadastro encontrada
                return motoristaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o motorista. \nDetalhes:" + ex.Message);
            }
        }



    }
}
