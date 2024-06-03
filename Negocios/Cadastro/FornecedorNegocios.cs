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
    public class FornecedorNegocios
    {
        //instância a classe
        Conexao conexao = new Conexao();

        public FornecedorCollection pesqFornecedor(string empresa, string codigo, string nome, bool status)
        {
            try
            {
                //Instância a coleção
                FornecedorCollection fornecedorCollection = new FornecedorCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@fornecedor", nome);
                conexao.AdicionarParamentros("@status", status);

                //String de consulta
                string select = "select f.forn_codigo, f.forn_nome, f.forn_endereco, e.est_uf, c.cid_nome, b.bar_nome, f.forn_fone, f.forn_email, f.forn_status " +
                                "from wms_fornecedor f " +
                                "left join wms_bairro b " +
                                "on b.bar_codigo = f.bar_codigo " +
                                "left join wms_cidade c " +
                                "on c.cid_codigo = b.cid_codigo " +
                                "left join wms_estado e " +
                                "on e.est_codigo = c.est_codigo " +
                                "where forn_status = @status and f.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                //forn_parceiro = 'True' and
                if (!codigo.Equals(""))
                {
                    select += "and forn_codigo = @codigo";
                }
                else if (!nome.Equals(""))
                {
                    select += "and forn_nome like '%"+@nome+"%'order by forn_codigo";

                }

                //Instância um datatable 
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Fornecedor fornecedor = new Fornecedor();
                    //Adiciona os valores encontrados
                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        fornecedor.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        fornecedor.nomeFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["forn_endereco"] != DBNull.Value)
                    {
                        fornecedor.enderecoFornecedor = Convert.ToString(linha["forn_endereco"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        fornecedor.ufFornecedor = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        fornecedor.cidadeFornecedor = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        fornecedor.bairroFornecedor = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["forn_fone"] != DBNull.Value)
                    {
                        fornecedor.foneFornecedor = Convert.ToString(linha["forn_fone"]);
                    }

                    if (linha["forn_email"] != DBNull.Value)
                    {
                        fornecedor.emailFornecedor = Convert.ToString(linha["forn_email"]);
                    }

                    if (linha["forn_status"] != DBNull.Value)
                    {
                        fornecedor.statusFornecedor = Convert.ToBoolean(linha["forn_status"]);
                    }

                    fornecedorCollection.Add(fornecedor);
                }
                //Retorna a coleção de cadastro encontrada
                return fornecedorCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o fornecedor! \nDetalhes: " + ex.Message);
            }
        }

        public FornecedorCollection pesqFornecedor(string empresa, string nmfornecedor, int codigo)
        {
            try
            {
                //Instância a coleção
                FornecedorCollection pesqFornecedorCollection = new FornecedorCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@fornecedor", "%" + nmfornecedor + "%");
                conexao.AdicionarParamentros("@codigo", codigo);
                //String de consulta
                string select = "select forn_codigo, forn_nome from wms_fornecedor ";


                if (nmfornecedor != String.Empty)
                {
                    select += "where forn_status = 'True' and forn_nome like @fornecedor and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) order by forn_codigo";
                }
                else if (codigo != 0)
                {
                    select += "where forn_status = 'True' and forn_codigo = @codigo and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                //Instância um datatable 
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Fornecedor fornecedor = new Fornecedor();
                    //Adiciona os valores encontrados
                    fornecedor.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    fornecedor.nomeFornecedor = Convert.ToString(linha["forn_nome"]);

                    pesqFornecedorCollection.Add(fornecedor);
                }
                //Retorna a coleção de cadastro encontrada
                return pesqFornecedorCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o fornecedor! \nDetalhes: " + ex.Message);
            }
        }

        public RepresentanteCollection pesqRepresentante(string empresa, string codigo, string nome, bool status)
        {
            try
            {
                //Instância a coleção
                RepresentanteCollection representanteCollection = new RepresentanteCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@fornecedor", nome);
                conexao.AdicionarParamentros("@status", status);
                //String de consulta
                string select = "select r.rep_codigo, r.rep_nome, r.rep_celular, r.rep_email, r.forn_codigo from wms_forn_representante r " +
                                "inner join wms_fornecedor f " +
                                "on f.forn_codigo = r.forn_codigo " +
                                "where forn_parceiro = 'True' and forn_status = @status and f.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                if (!codigo.Equals(""))
                {
                    select += "and f.forn_codigo = @codigo";
                }
                else if (!nome.Equals(""))
                {
                    select += "and forn_nome like '%" + @nome + "%'order by forn_codigo";

                }

                //Instância um datatable 
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    Representante representante = new Representante();
                    //Adiciona os valores encontrados
                    if (linha["rep_codigo"] != DBNull.Value)
                    {
                        representante.codRepresentante = Convert.ToInt32(linha["rep_codigo"]);
                    }

                    if (linha["rep_nome"] != DBNull.Value)
                    {
                        representante.nomeRepresentante = Convert.ToString(linha["rep_nome"]);
                    }

                    if (linha["rep_celular"] != DBNull.Value)
                    {
                        representante.foneRepresentante = Convert.ToString(linha["rep_celular"]);
                    }

                    if (linha["rep_email"] != DBNull.Value)
                    {
                        representante.emailRepresentante = Convert.ToString(linha["rep_email"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        representante.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    representanteCollection.Add(representante);
                }
                //Retorna a coleção de cadastro encontrada
                return representanteCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os representantes! \nDetalhes: " + ex.Message);
            }
        }

    }
}
