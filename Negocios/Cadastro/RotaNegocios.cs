using System;
using Dados;
using System.Data;
using ObjetoTransferencia;

namespace Negocios
{
    public class RotaNegocios
    {//instância a classe
        Conexao conexao = new Conexao();

        //Usado no frame de pesquisa rota
        public RotaCollection PesqRota(string descRota, int codigoRota, string empresa)
        {
            try
            {
                //Instância a coleção
                RotaCollection rotaCollection = new RotaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@descRota", "%" + descRota + "%");
                conexao.AdicionarParamentros("@codigoRota", codigoRota);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select rota_numero, rota_descricao from wms_rota r ";

                if(!descRota.Equals(""))
                {
                    select += "where rota_descricao like @descRota and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }
                else if(codigoRota > 0)
                {
                    select += "where rota_numero = @codigoRota and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }

                select += "order by rota_numero ";

                


                //Instância um datatable 
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Rota rota = new Rota();

                    //Adiciona os valores encontrados
                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        rota.codRota = Convert.ToInt32(linha["rota_numero"]);
                    }

                    if (linha["rota_descricao"] != DBNull.Value)
                    {
                        rota.descRota = Convert.ToString(linha["rota_descricao"]);
                    }

                    rotaCollection.Add(rota);
                }
                //Retorna a coleção de cadastro encontrada
                return rotaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a rota! \nDetalhes: " + ex.Message);
            }

        }

        //Pesquisa 
        public RotaCollection PesqRotas(string empresa)
        {
            try
            {
                //Instância a coleção
                RotaCollection rotaCollection = new RotaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select r.rota_numero, r.rota_descricao, tr.tipo_descricao from wms_rota r " +
                                "left join wms_tipo_rota tr " +
                                "on tr.tipo_codigo = r.tipo_codigo " +
                                "where r.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by rota_numero";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Rota rota = new Rota();

                    //Adiciona os valores encontrados
                    if (linha["rota_numero"] != DBNull.Value)
                    {
                        rota.codRota = Convert.ToInt32(linha["rota_numero"]);
                    }

                    if (linha["rota_descricao"] != DBNull.Value)
                    {
                        rota.descRota = Convert.ToString(linha["rota_descricao"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        rota.descTipoRota = Convert.ToString(linha["tipo_descricao"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    rotaCollection.Add(rota);
                }
                //Retorna a coleção de cadastro encontrada
                return rotaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as rotas \nDetalhes:" + ex.Message);
            }
        }

        //Alterar cadastro
        public void Alterar(string empresa,int codigo, int codTipoRota)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@codTipoRota", codTipoRota);

                //String de atualização
                string update = "update wms_rota set tipo_codigo = @codTipoRota where rota_numero = @codigo and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar o cadastro. \nDetalhes:" + ex.Message);
            }

        }


    }
}
