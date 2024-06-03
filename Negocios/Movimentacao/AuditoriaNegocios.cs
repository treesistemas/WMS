using Dados;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class AuditoriaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa um novo código de auditoria
        public int PesqId()
        {
            try
            {
                //String de consulta
                string select = "select gen_id(gen_wms_auditoria_produto,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Variável código
                int codigo = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    codigo = Convert.ToInt32(linha["id"]);
                }
                //Retorna a coleção de cadastro encontrada
                return codigo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um nova auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a auditoria
        public AuditoriaCollection PesqAuditoria(string codAuditoria, string tipoAuditoria, string statusAuditoria, string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                //Instância uma coleção de objêto
                AuditoriaCollection auditoriaCollection = new AuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@tipoAuditoria", tipoAuditoria);
                conexao.AdicionarParamentros("@statusAuditoria", statusAuditoria);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                conexao.AdicionarParamentros("@empresa", empresa);


                //String de consulta
                string select = "select aud_codigo, aud_data, aud_tipo, aud_armazenagem, reg_numero, rua_numero_inicial, rua_numero_final, " +
                                "rua_lado, aud_relatorio, aud_exige_contagem, aud_exige_validade, aud_exige_lote, aud_exige_barra, " +
                                "usu_login, aud_status, " +
                                //Verifica a qtd de endereços da auditoria
                                "(select count(aud_codigo) from wms_item_auditoria where aud_codigo = a.aud_codigo) as qtd_endereco, " +
                                //Verifica a média de problemas da auditoria
                                "(select sum(iaud_problema)/count(aud_codigo) from wms_item_auditoria where aud_codigo = a.aud_codigo and iaud_status = 'FINALIZADO') as aud_problema " +
                                "from wms_auditoria_produto a " +
                                "inner join wms_usuario u " +
                                "on a.usu_codigo = u.usu_codigo ";

                if (!codAuditoria.Equals(""))
                {
                    select += "where a.aud_codigo = @codAuditoria ";
                }
                else
                {
                    select += "where a.aud_data between @dataInicial and @dataFinal ";

                    if (!(tipoAuditoria.Equals("") || tipoAuditoria.Equals("TODOS")))
                    {
                        select += "and aud_tipo = @tipoAuditoria ";
                    }

                    if (!(statusAuditoria.Equals("") || statusAuditoria.Equals("TODOS")))
                    {
                        select += "and aud_status = @statusAuditoria ";
                    }
                    select += "and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                //Ordenação pelo código
                select += " order by a.aud_codigo";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);



                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Auditoria auditoria = new Auditoria();

                    if (linha["aud_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.codAuditoria = Convert.ToInt32(linha["aud_codigo"]);
                    }

                    if (linha["aud_data"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.dataAuditoria = Convert.ToDateTime(linha["aud_data"]);
                    }

                    if (linha["aud_tipo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.tipoAuditoria = Convert.ToString(linha["aud_tipo"]);
                    }

                    if (linha["aud_armazenagem"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.tipoArmazenamento = Convert.ToString(linha["aud_armazenagem"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero_inicial"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.ruaIncial = Convert.ToInt32(linha["rua_numero_inicial"]);
                    }

                    if (linha["rua_numero_final"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.ruaFinal = Convert.ToInt32(linha["rua_numero_final"]);
                    }

                    if (linha["rua_lado"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.ladoRua = Convert.ToString(linha["rua_lado"]);
                    }

                    if (linha["aud_relatorio"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.tipoRelatorio = Convert.ToBoolean(linha["aud_relatorio"]);
                    }

                    if (linha["aud_exige_contagem"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.exigeContagem = Convert.ToBoolean(linha["aud_exige_contagem"]);
                    }

                    if (linha["aud_exige_validade"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.exigemVencimento = Convert.ToBoolean(linha["aud_exige_validade"]);
                    }

                    if (linha["aud_exige_lote"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.exigeLote = Convert.ToBoolean(linha["aud_exige_lote"]);
                    }

                    if (linha["aud_exige_barra"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.exigeBarra = Convert.ToBoolean(linha["aud_exige_barra"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.responsavel = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["qtd_Endereco"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.qtdEndereco = Convert.ToInt32(linha["qtd_Endereco"]);
                    }

                    if (linha["aud_problema"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.problemas = Convert.ToInt32(linha["aud_problema"]);
                    }

                    if (linha["aud_status"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        auditoria.status = Convert.ToString(linha["aud_status"]);
                    }
                    auditoriaCollection.Add(auditoria);
                }
                //Retorna a coleção de cadastro encontrada
                return auditoriaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa a auditoria
        public ItensAuditoriaCollection PesqItensAuditoria(string empresa,string codAuditoria, string tipoAuditoria, string statusAuditoria, string dataInicial, string dataFinal)
        {
            try
            {
                //Instância uma coleção de objêto
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@tipoAuditoria", tipoAuditoria);
                conexao.AdicionarParamentros("@statusAuditoria", statusAuditoria);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:00");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //String de consulta
                string select = "select ia.aud_codigo, iaud_codigo, ia.apa_codigo, ap.apa_endereco, iaud_tipo_endereco, s.sep_tipo, " +
                                "ia.prod_id, prod_codigo, prod_descricao, p.prod_fator_pulmao, uni.uni_unidade as uni_pulmao, uni1.uni_unidade as uni_picking, " +
                                "iaud_estoque, iaud_estoque_auditado, iaud_estoque_falta, iaud_estoque_sobra, " +
                                "iaud_vencimento, iaud_lote, iaud_problema, iaud_data, usu_login, iaud_status " +
                                "from wms_item_auditoria ia " +
                                "left join wms_auditoria_produto a " +
                                "on a.aud_codigo = ia.aud_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = ia.prod_id " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = ia.apa_codigo " +
                                "left join wms_separacao s " +
                                "on s.apa_codigo = ia.apa_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = ia.usu_codigo_auditor " +
                                "left join wms_unidade uni " +
                                "on uni.uni_codigo = p.uni_codigo_pulmao " +
                                 "left join wms_unidade uni1 " +
                                "on uni1.uni_codigo = p.uni_codigo_picking ";

                if (!codAuditoria.Equals(""))
                {
                    select += "where a.aud_codigo = @codAuditoria ";
                }
                else
                {
                    select += "where a.aud_data between @dataInicial and @dataFinal ";

                    if (!(tipoAuditoria.Equals("") || tipoAuditoria.Equals("TODOS")))
                    {
                        select += "and aud_tipo = @tipoAuditoria ";
                    }

                    if (!(statusAuditoria.Equals("") || statusAuditoria.Equals("TODOS")))
                    {
                        select += "and aud_status = @statusAuditoria ";
                    }
                        select += "and ia.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                }

                //Ordenação pelo código
                select += "order by a.aud_codigo";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAuditoria itensAuditoria = new ItensAuditoria();

                    if (linha["aud_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.codAuditoria = Convert.ToInt32(linha["aud_codigo"]);
                    }

                    if (linha["iaud_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.codItemAuditoria = Convert.ToInt32(linha["iaud_codigo"]);
                    }

                    if (linha["iaud_data"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.dataAuditoria = Convert.ToDateTime(linha["iaud_data"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["iaud_tipo_endereco"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.tipoEndereco = Convert.ToString(linha["iaud_tipo_endereco"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.tipoSeparacao = Convert.ToString(linha["sep_tipo"]);

                        if (linha["uni_picking"] != DBNull.Value)
                        {
                            //Adiciona os valores encontrados
                            itensAuditoria.unidadeEstoque = Convert.ToString(linha["uni_picking"]);
                        }
                    }
                    else
                    {
                        itensAuditoria.tipoSeparacao = "Pulmão";

                        if (linha["uni_pulmao"] != DBNull.Value)
                        {
                            //Adiciona os valores encontrados
                            itensAuditoria.unidadeEstoque = Convert.ToString(linha["uni_pulmao"]);
                        }
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itensAuditoria.fatorPulmao = 1;
                    }

                    if (linha["iaud_estoque"] != DBNull.Value)
                    {
                        if (itensAuditoria.tipoEndereco.Equals("Pulmão"))
                        {
                            //Adiciona os valores encontrados
                            itensAuditoria.estoque = Convert.ToInt32(linha["iaud_estoque"]) / itensAuditoria.fatorPulmao;
                        }
                        else
                        {
                            //Adiciona os valores encontrados
                            itensAuditoria.estoque = Convert.ToInt32(linha["iaud_estoque"]);
                        }
                    }

                    if (linha["iaud_estoque_auditado"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.estoqueAuditado = Convert.ToInt32(linha["iaud_estoque_auditado"]);
                    }

                    if (linha["iaud_estoque_falta"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.estoqueFalta = Convert.ToInt32(linha["iaud_estoque_falta"]);
                    }

                    if (linha["iaud_estoque_sobra"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.estoqueSobra = Convert.ToInt32(linha["iaud_estoque_sobra"]);
                    }

                    if (linha["iaud_vencimento"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.vencimento = Convert.ToDateTime(linha["iaud_vencimento"]);
                    }

                    if (linha["iaud_lote"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.lote = Convert.ToString(linha["iaud_lote"]);
                    }

                    if (linha["iaud_problema"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.estoqueProblema = Convert.ToInt32(linha["iaud_problema"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.auditor = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["iaud_status"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        itensAuditoria.statusAuditoria = Convert.ToString(linha["iaud_status"]);
                    }

                    itensAuditoriaCollection.Add(itensAuditoria);
                }
                //Retorna a coleção de cadastro encontrada
                return itensAuditoriaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens da auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Analisa os itens para auditoria
        public ItensAuditoriaCollection PesqItensPulmao(string empresa,string tipo, string numeroRegiao, string ruaInicial, string ruaFinal, string tipoArmazenamento, string lado, int[] idProduto)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@ruaInicial", ruaInicial);
                conexao.AdicionarParamentros("@ruaFinal", ruaFinal);
                conexao.AdicionarParamentros("@tipoArmazenamento", tipoArmazenamento);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@idProduto", idProduto);


                //String de consulta
                string select = "select a.apa_codigo, ap.apa_endereco, a.prod_id, prod_codigo, prod_descricao, p.prod_fator_pulmao, " +
                                "a.arm_quantidade, u.uni_unidade, a.arm_vencimento, a.arm_lote from wms_armazenagem a " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = a.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = a.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_pulmao " +
                                "where a.prod_id in (select prod_id from wms_separacao s " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = s.apa_codigo ";


                if (tipo.Equals("ENDERECO"))
                {
                    if (!numeroRegiao.Equals("Selec..."))
                    {
                        select += "inner join wms_nivel n " +
                                   "on n.niv_codigo = ap.niv_codigo " +
                                   "inner join wms_bloco b " +
                                   "on b.bloc_codigo = n.bloc_codigo " +
                                   "inner join wms_rua r " +
                                   "on r.rua_codigo = b.rua_codigo " +
                                   "inner join wms_regiao re " +
                                   "on re.reg_codigo = r.reg_codigo " +
                                   "where re.reg_numero = @numeroRegiao " +
                                   "and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";
                    }

                    if (!(ruaInicial.Equals("Selec...") && ruaFinal.Equals("Selec...")))
                    {
                        if (!lado.Equals("TODOS"))
                        {
                            select += "and r.rua_numero between @ruaInicial  and @ruaFinal and bloc_lado = @lado) ";
                        }
                        else
                        {
                            select += "and r.rua_numero between @ruaInicial  and @ruaFinal) ";
                        }
                    }
                }
                else if (tipo.Equals("PRODUTO"))
                {
                    if (idProduto != null && idProduto.Length > 0)
                    {
                        select += "where a.prod_id = " + idProduto[0] + " ";

                        for (int i = 1; idProduto.Length > i; i++)
                        {
                            if (idProduto[i] != 0)
                            {
                                select += "or a.prod_id = " + idProduto[i] + " ";
                            }
                        }
                    }
                }

                select += "order by ap.apa_ordem";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAuditoria itens = new ItensAuditoria();

                    //Adiciona os valores encontrados
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        itens.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itens.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itens.fatorPulmao = 1;
                    }

                    if (linha["arm_quantidade"] != DBNull.Value)
                    {
                        itens.estoque = Convert.ToInt32(linha["arm_quantidade"]) / itens.fatorPulmao;
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itens.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                    }
                    else
                    {
                        itens.unidadeEstoque = "FALTA DADOS LOGÍSTICOS";
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        itens.vencimento = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        itens.lote = Convert.ToString(linha["arm_lote"]);
                    }

                    //Adiciona o objêto a coleção
                    itensAuditoriaCollection.Add(itens);
                }


                //Retorna a coleção de cadastro encontrada
                return itensAuditoriaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao analisar os itens para a auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Analisa os itens para auditoria
        public ItensAuditoriaCollection PesqItensPickingFlow(string empresa, string tipo, string numeroRegiao, string ruaInicial, string ruaFinal, string tipoArmazenamento, string lado, int[] idProduto)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@ruaInicial", ruaInicial);
                conexao.AdicionarParamentros("@ruaFinal", ruaFinal);
                conexao.AdicionarParamentros("@tipoArmazenamento", tipoArmazenamento);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@idProduto", idProduto);


                //String de consulta
                string select = "select s.apa_codigo, ap.apa_endereco, sep_tipo, s.prod_id, prod_codigo, prod_descricao, p.prod_fator_picking, " +
                                "s.sep_estoque, u.uni_unidade, s.sep_validade, s.sep_lote from wms_separacao s " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = s.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking " +
                                "where s.prod_id in (select prod_id from wms_separacao ss " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = ss.apa_codigo ";



                if (tipo.Equals("ENDERECO"))
                {
                    if (!numeroRegiao.Equals("Selec..."))
                    {
                        select += "inner join wms_nivel n " +
                                   "on n.niv_codigo = ap.niv_codigo " +
                                   "inner join wms_bloco b " +
                                   "on b.bloc_codigo = n.bloc_codigo " +
                                   "inner join wms_rua r " +
                                   "on r.rua_codigo = b.rua_codigo " +
                                   "inner join wms_regiao re " +
                                   "on re.reg_codigo = r.reg_codigo " +
                                   "where p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and re.reg_numero = @numeroRegiao ";
                    }

                    if (!(ruaInicial.Equals("Selec...") && ruaFinal.Equals("Selec...")))
                    {
                        if (!lado.Equals("TODOS"))
                        {
                            select += "and r.rua_numero between @ruaInicial  and @ruaFinal and bloc_lado = @lado) ";
                        }
                        else
                        {
                            select += "and r.rua_numero between @ruaInicial  and @ruaFinal)";
                        }

                    }

                }
                else if (tipo.Equals("PRODUTO"))
                {
                    if (idProduto != null && idProduto.Length > 0)
                    {
                        select += "where p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and s.prod_id = " + idProduto[0] + " ";

                        for (int i = 1; idProduto.Length > i; i++)
                        {
                            if (idProduto[i] != 0)
                            {
                                select += "or conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and s.prod_id = " + idProduto[i] + " ";
                            }
                        }
                    }
                }

                select += "order by ap.apa_ordem, s.sep_tipo";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAuditoria itens = new ItensAuditoria();

                    //Adiciona os valores encontrados
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        itens.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        itens.tipoEndereco = Convert.ToString(linha["sep_tipo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_picking"] != DBNull.Value)
                    {
                        itens.fatorPulmao = Convert.ToInt32(linha["prod_fator_picking"]);
                    }
                    else
                    {
                        itens.fatorPulmao = 1;
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        itens.estoque = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itens.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                    }
                    else
                    {
                        itens.unidadeEstoque = "FALTA DADOS LOGÍSTICOS";
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        itens.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        itens.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    //Adiciona o objêto a coleção
                    itensAuditoriaCollection.Add(itens);
                }


                //Retorna a coleção de cadastro encontrada
                return itensAuditoriaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao analisar os itens do picking para a auditoria. \nDetalhes:" + ex.Message);
            }
        }


        //Analisa os itens para auditoria
        public ItensAuditoriaCollection PesqItensPicking(string empresa, string tipo, string numeroRegiao, string ruaInicial, string ruaFinal, string tipoArmazenamento, string lado, int[] idProduto)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@ruaInicial", ruaInicial);
                conexao.AdicionarParamentros("@ruaFinal", ruaFinal);
                conexao.AdicionarParamentros("@tipoArmazenamento", tipoArmazenamento);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@idProduto", idProduto);


                //String de consulta
                string select = "select s.apa_codigo, ap.apa_endereco, sep_tipo, s.prod_id, prod_codigo, prod_descricao, p.prod_fator_picking, " +
                                "s.sep_estoque, u.uni_unidade, s.sep_validade, s.sep_lote from wms_separacao s " +
                                "inner join wms_produto p " +
                                "on p.prod_id = s.prod_id " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = s.apa_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking ";


                if (tipo.Equals("ENDERECO"))
                {
                    if (!numeroRegiao.Equals("Selec..."))
                    {
                        select += "inner join wms_nivel n " +
                                   "on n.niv_codigo = ap.niv_codigo " +
                                   "inner join wms_bloco b " +
                                   "on b.bloc_codigo = n.bloc_codigo " +
                                   "inner join wms_rua r " +
                                   "on r.rua_codigo = b.rua_codigo " +
                                   "inner join wms_regiao re " +
                                   "on re.reg_codigo = r.reg_codigo " +
                                   "where p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and re.reg_numero = @numeroRegiao ";
                    }

                    if (!(ruaInicial.Equals("Selec...") && ruaFinal.Equals("Selec...")))
                    {

                        select += "and r.rua_numero between @ruaInicial  and @ruaFinal ";

                        if (!lado.Equals("TODOS"))
                        {
                            select += "and bloc_lado = @lado ";
                        }
                    }

                }
                else if (tipo.Equals("PRODUTO"))
                {
                    if (idProduto != null && idProduto.Length > 0)
                    {
                        select += "where p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and s.prod_id = " + idProduto[0] + " ";

                        for (int i = 1; idProduto.Length > i; i++)
                        {
                            if (idProduto[i] != 0)
                            {
                                select += "or p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and s.prod_id = " + idProduto[i] + " ";
                            }
                        }
                    }
                }

                select += "order by ap.apa_ordem, s.sep_tipo";


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAuditoria itens = new ItensAuditoria();

                    //Adiciona os valores encontrados
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        itens.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        itens.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        itens.tipoEndereco = Convert.ToString(linha["sep_tipo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itens.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itens.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itens.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_fator_picking"] != DBNull.Value)
                    {
                        itens.fatorPulmao = Convert.ToInt32(linha["prod_fator_picking"]);
                    }
                    else
                    {
                        itens.fatorPulmao = 1;
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        itens.estoque = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itens.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                    }
                    else
                    {
                        itens.unidadeEstoque = "FALTA DADOS LOGÍSTICOS";
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        itens.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        itens.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    //Adiciona o objêto a coleção
                    itensAuditoriaCollection.Add(itens);
                }


                //Retorna a coleção de cadastro encontrada
                return itensAuditoriaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao analisar os itens do picking para a auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o picking de caixa da auditoria
        public ItensAuditoriaCollection PesqPickingCaixaAuditoria(string empresa, int codAuditoria)
        {
            try
            {
                //Instância uma coleção de objêto
                ItensAuditoriaCollection resultadoCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);


                //String de consulta
                string select = "select i.aud_codigo, i.iaud_data, a.apa_codigo, apa_endereco, i.prod_id, prod_codigo, prod_descricao, " +
                                "(select sep_estoque from wms_separacao where prod_id = I.prod_id and sep_tipo = 'CAIXA') as qtd_picking, i.iaud_estoque_auditado, " +
                                "i.iaud_vencimento, i.iaud_lote, u.usu_login, iaud_status " +
                                "from wms_item_auditoria i " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = i.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = i.usu_codigo_auditor " +
                                "where i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and aud_codigo = @codAuditoria and iaud_tipo_endereco = 'CAIXA'";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);



                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAuditoria item = new ItensAuditoria();

                    if (linha["aud_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codAuditoria = Convert.ToInt32(linha["aud_codigo"]);
                    }

                    if (linha["iaud_data"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.dataAuditoria = Convert.ToDateTime(linha["iaud_data"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtd_picking"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdPulmao = Convert.ToInt32(linha["qtd_picking"]);
                    }

                    if (linha["iaud_estoque_auditado"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdPicking = Convert.ToInt32(linha["iaud_estoque_auditado"]);
                    }

                    if (linha["iaud_vencimento"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.vencimento = Convert.ToDateTime(linha["iaud_vencimento"]);
                    }

                    if (linha["iaud_lote"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.lote = Convert.ToString(linha["iaud_lote"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.auditor = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["iaud_status"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.statusAuditoria = Convert.ToString(linha["iaud_status"]);
                    }

                    resultadoCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return resultadoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o resultado do picking de caixa da auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o picking de flow rack auditoria
        public ItensAuditoriaCollection PesqPickingFlowRackAuditoria(string empresa, int codAuditoria)
        {
            try
            {
                //Instância uma coleção de objêto
                ItensAuditoriaCollection resultadoCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);

                //String de consulta
                string select = "select i.aud_codigo, i.iaud_data, a.apa_codigo, apa_endereco, i.prod_id, prod_codigo, prod_descricao, " +
                                "(select sep_estoque from wms_separacao where prod_id = I.prod_id and sep_tipo = 'FLOWRACK') as qtd_picking, i.iaud_estoque_auditado, " +
                                "i.iaud_vencimento, i.iaud_lote, u.usu_login, iaud_status " +
                                "from wms_item_auditoria i " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = i.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = i.usu_codigo_auditor " +
                                "where i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and aud_codigo = @codAuditoria and iaud_tipo_endereco = 'FLOWRACK'";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);



                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAuditoria item = new ItensAuditoria();

                    if (linha["aud_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codAuditoria = Convert.ToInt32(linha["aud_codigo"]);
                    }

                    if (linha["iaud_data"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.dataAuditoria = Convert.ToDateTime(linha["iaud_data"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtd_picking"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdPulmao = Convert.ToInt32(linha["qtd_picking"]);
                    }

                    if (linha["iaud_estoque_auditado"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdPicking = Convert.ToInt32(linha["iaud_estoque_auditado"]);
                    }

                    if (linha["iaud_vencimento"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.vencimento = Convert.ToDateTime(linha["iaud_vencimento"]);
                    }

                    if (linha["iaud_lote"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.lote = Convert.ToString(linha["iaud_lote"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.auditor = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["iaud_status"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.statusAuditoria = Convert.ToString(linha["iaud_status"]);
                    }

                    resultadoCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return resultadoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o resultado do picking de flow rack da auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o picking de flow rack auditoria
        public ItensAuditoriaCollection PesqPulmaoAuditoria(string empresa, int codAuditoria)
        {
            try
            {
                //Instância uma coleção de objêto
                ItensAuditoriaCollection resultadoCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);


                //String de consulta
                string select = "select i.aud_codigo, i.iaud_data, a.apa_codigo, apa_endereco, i.prod_id, prod_codigo, prod_descricao, " +
                                "(select arm_quantidade from wms_armazenagem where prod_id = i.prod_id and apa_codigo = a.apa_codigo ) as qtd_pulmao, i.iaud_estoque_auditado, " +
                                "i.iaud_vencimento, i.iaud_lote, u.usu_login, iaud_status " +
                                "from wms_item_auditoria i " +
                                "inner join wms_apartamento a " +
                                "on a.apa_codigo = i.apa_codigo " +
                                "inner join wms_produto p " +
                                "on p.prod_id = i.prod_id " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = i.usu_codigo_auditor " +
                                "where i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and aud_codigo = @codAuditoria and iaud_tipo_endereco = 'PULMAO'";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);



                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAuditoria item = new ItensAuditoria();

                    if (linha["aud_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codAuditoria = Convert.ToInt32(linha["aud_codigo"]);
                    }

                    if (linha["iaud_data"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.dataAuditoria = Convert.ToDateTime(linha["iaud_data"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["qtd_pulmao"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdPulmao = Convert.ToInt32(linha["qtd_pulmao"]);
                    }

                    if (linha["iaud_estoque_auditado"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdPicking = Convert.ToInt32(linha["iaud_estoque_auditado"]);
                    }

                    if (linha["iaud_vencimento"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.vencimento = Convert.ToDateTime(linha["iaud_vencimento"]);
                    }

                    if (linha["iaud_lote"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.lote = Convert.ToString(linha["iaud_lote"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.auditor = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["iaud_status"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.statusAuditoria = Convert.ToString(linha["iaud_status"]);
                    }

                    resultadoCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return resultadoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o resultado do pulmão da auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o resultado da auditoria
        public ItensAuditoriaCollection PesqResultadoAuditoria(string empresa, int codAuditoria)
        {
            try
            {
                //Instância uma coleção de objêto
                ItensAuditoriaCollection resultadoCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);


                //String de consulta
                string select = "select a.aud_codigo, p.prod_id, prod_codigo, prod_descricao, " +
                                /*Verifica a entrada dos produtos que não foram armazenados*/
                                "coalesce((select sum(inot_quantidade_conf) from wms_itens_nota ii " +
                                "inner join wms_nota_entrada nn " +
                                "on nn.not_codigo = ii.not_codigo " +
                                "where inot_quantidade_conf > coalesce(inot_armazenagem, 0) and ii.prod_id = p.prod_id),0) as prod_entrada, " +
                                /*Verifica a armazenagem do produto*/
                                "coalesce((select sum(ar.arm_quantidade) from wms_armazenagem ar where ar.prod_id = p.prod_id), 0) as qtd_Pulmao_armazenado, " +
                                "coalesce((select sum(iaud_estoque_auditado) from wms_item_auditoria where aud_codigo = @codAuditoria and prod_id = p.prod_id and iaud_tipo_endereco = 'PULMAO'), 0) as qtd_pulmao_auditado, " +
                                /*Verifica produtos que foram separados e não faturados*/
                                "coalesce((select sum(ip.iped_quantidade) from wms_pedido pd " +
                                "inner join wms_item_pedido ip " +
                                "on ip.ped_codigo = pd.ped_codigo " +
                                "where ped_nota_fiscal is null and not ped_fim_conferencia is null and prod_id = p.prod_id), 0) as qtd_separacao, " +
                                /*Verifica a quantidade dentro dos volumes*/
                                "coalesce((select sum(r.iflow_qtd_conferida) as qtdFlowRack from wms_rastreamento_flowrack r " +
                                "inner join wms_pedido pp " +
                                "on pp.ped_codigo = r.ped_codigo " +
                                "where pp.ped_fim_conferencia is null and r.prod_id = p.prod_id),0) as qtd_volume, " +
                                "(select sum(iaud_estoque_auditado) from wms_item_auditoria where aud_codigo = @codAuditoria and prod_id = p.prod_id and iaud_tipo_endereco = 'CAIXA') as qtd_picking, " +
                                "(select sum(iaud_estoque_auditado) from wms_item_auditoria where aud_codigo = @codAuditoria and prod_id = p.prod_id and iaud_tipo_endereco = 'FLOWRACK') as qtd_flowrack, " +
                                "(select sum(iaud_estoque_auditado) from wms_item_auditoria where aud_codigo = @codAuditoria and prod_id = p.prod_id and iaud_tipo_endereco = 'PULMAO') as qtd_pulmao, " +
                                "e.est_quantidade, iaud_finalizada " +
                                "from wms_item_auditoria a " +
                                "inner join wms_produto p " +
                                "on p.prod_id = a.prod_id " +
                                "inner join wms_estoque e " +
                                "on e.prod_id = a.prod_id " +
                                "where a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and a.aud_codigo = @codAuditoria " +
                                "group by a.aud_codigo, p.prod_id, prod_codigo, prod_descricao, e.est_quantidade, iaud_finalizada ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);



                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    ItensAuditoria item = new ItensAuditoria();

                    if (linha["aud_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codAuditoria = Convert.ToInt32(linha["aud_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_entrada"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdEntrada = Convert.ToInt32(linha["prod_entrada"]);
                    }

                    if (linha["qtd_pulmao_auditado"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdPulmao = Convert.ToInt32(linha["qtd_pulmao_auditado"]);
                    }

                    if (linha["qtd_picking"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdPicking = Convert.ToInt32(linha["qtd_picking"]);
                    }

                    if (linha["qtd_separacao"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdSeparacao = Convert.ToInt32(linha["qtd_separacao"]);
                    }

                    if (linha["qtd_volume"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.qtdVolume = Convert.ToInt32(linha["qtd_volume"]);
                    }

                    if (linha["est_quantidade"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.estoque = Convert.ToInt32(linha["est_quantidade"]);
                    }

                    if (linha["iaud_finalizada"] != DBNull.Value)
                    {
                        //Adiciona os valores encontrados
                        item.statusAuditoria = "FINALIZADA";
                    }

                    resultadoCollection.Add(item);
                }
                //Retorna a coleção de cadastro encontrada
                return resultadoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o resultado da auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Gera a auditoria
        public void GeraAuditoria( int codAuditoria, string tipo, string numeroRegiao, string ruaInicial, string ruaFinal, string tipoArmazenamento, string lado,
            bool tipoRelatorio, bool exigeContagem, bool exigeValidade, bool exigeLote, bool exigeBarra, int codUsuario, string empresa)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@ruaInicial", ruaInicial);
                conexao.AdicionarParamentros("@ruaFinal", ruaFinal);
                conexao.AdicionarParamentros("@tipoArmazenamento", tipoArmazenamento);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@tipoRelatorio", tipoRelatorio);
                conexao.AdicionarParamentros("@exigeContagem", exigeContagem);
                conexao.AdicionarParamentros("@exigeValidade", exigeValidade);
                conexao.AdicionarParamentros("@exigeLote", exigeLote);
                conexao.AdicionarParamentros("@exigeBarra", exigeBarra);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                
                //Insere a no banco
                string insert = "insert into wms_auditoria_produto (aud_codigo, aud_data, aud_tipo, aud_armazenagem, reg_numero, rua_numero_inicial, rua_numero_final, " +
                                "rua_lado, aud_relatorio, aud_exige_contagem, aud_exige_validade, aud_exige_lote, aud_exige_barra, usu_codigo, aud_status, conf_codigo) " +

                                "select @codAuditoria, current_timestamp, @tipo, @tipoArmazenamento, @numeroRegiao, @ruaInicial, @ruaFinal, " +
                                "@lado, @tipoRelatorio, @exigeContagem, @exigeValidade, @exigeLote, @exigeBarra, @codUsuario, 'PENDENTE', (select conf_codigo from wms_configuracao where conf_sigla = @empresa) from RDB$DATABASE ";



                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar a auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Gera os itens do pulmão
        public void GerarItensAuditoriaPulmao(string empresa,int codAuditoria, string tipo, string numeroRegiao, string ruaInicial, string ruaFinal, string tipoArmazenamento, string lado, int[] idProduto, int? codAuditor, bool auditarPulmao)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@ruaInicial", ruaInicial);
                conexao.AdicionarParamentros("@ruaFinal", ruaFinal);
                conexao.AdicionarParamentros("@tipoArmazenamento", tipoArmazenamento);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codAuditor", codAuditor);

                //Insere a no banco
                string insert = null;

                if (auditarPulmao == false)
                {
                    //Insere a no banco
                    insert = "insert into wms_item_auditoria(aud_codigo, iaud_codigo, apa_codigo, prod_id, iaud_estoque, iaud_estoque_auditado, iaud_vencimento, iaud_lote, " +
                                    "iaud_tipo_endereco, iaud_status, usu_codigo_auditor, conf_codigo) " +

                                    "select @codAuditoria, gen_id(gen_wms_item_auditoria, 1), a.apa_codigo, a.prod_id, a.arm_quantidade, a.arm_quantidade, a.arm_vencimento, a.arm_lote, 'PULMAO', 'AUDITADO', @codAuditor, " +
                                    "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                    "from wms_armazenagem a " +
                                    "inner join wms_apartamento ap " +
                                    "on ap.apa_codigo = a.apa_codigo " +
                                    "where prod_id in (select prod_id from wms_separacao s " +
                                    "inner join wms_apartamento ap " +
                                    "on ap.apa_codigo = s.apa_codigo ";
                }

                if (auditarPulmao == true)
                {
                    //Insere a no banco
                    insert = "insert into wms_item_auditoria(aud_codigo, iaud_codigo, apa_codigo, prod_id, iaud_estoque, iaud_vencimento, iaud_lote, " +
                                    "iaud_tipo_endereco, iaud_status, usu_codigo_auditor, conf_codigo) " +

                                    "select @codAuditoria, gen_id(gen_wms_item_auditoria, 1), a.apa_codigo, a.prod_id, a.arm_quantidade, a.arm_vencimento, a.arm_lote, 'PULMAO', 'PENDENTE', @codAuditor, " +
                                    "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                    "from wms_armazenagem a " +
                                    "inner join wms_apartamento ap " +
                                    "on ap.apa_codigo = a.apa_codigo "+
                                    "where prod_id in (select prod_id from wms_separacao s " +
                                    "inner join wms_apartamento ap " +
                                    "on ap.apa_codigo = s.apa_codigo ";
                }



                if (tipo.Equals("ENDERECO"))
                {
                    if (!numeroRegiao.Equals("Selec..."))
                    {
                        insert += "inner join wms_nivel n " +
                                   "on n.niv_codigo = ap.niv_codigo " +
                                   "inner join wms_bloco b " +
                                   "on b.bloc_codigo = n.bloc_codigo " +
                                   "inner join wms_rua r " +
                                   "on r.rua_codigo = b.rua_codigo " +
                                   "inner join wms_regiao re " +
                                   "on re.reg_codigo = r.reg_codigo " +
                                   "where a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and re.reg_numero = @numeroRegiao ";
                    }

                    if (!(ruaInicial.Equals("Selec...") && ruaFinal.Equals("Selec...")))
                    {
                        if (!lado.Equals("TODOS"))
                        {
                            insert += "and r.rua_numero between @ruaInicial  and @ruaFinal and bloc_lado = @lado) ";
                        }
                        else
                        {
                            insert += "and r.rua_numero between @ruaInicial  and @ruaFinal) ";
                        }
                    }

                    insert += "order by ap.apa_ordem";
                }
                else if (tipo.Equals("PRODUTO"))
                {
                    if (idProduto != null && idProduto.Length > 0)
                    {
                        insert += "where a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and a.prod_id = " + idProduto[0] + " ";

                        for (int i = 1; idProduto.Length > i; i++)
                        {
                            if (idProduto[i] != 0)
                            {
                                insert += "or a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and a.prod_id = " + idProduto[i] + " ";
                            }
                        }
                    }

                    insert += "order by ap.apa_ordem";
                }

                

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do pulmão para a auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Gera os itens do picking
        public void GerarItensAuditoriaPicking(string empresa , int codAuditoria, string tipo, string numeroRegiao, string ruaInicial, string ruaFinal, string tipoArmazenamento, string lado, int[] idProduto, int? codAuditor, bool auditarPicking)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@ruaInicial", ruaInicial);
                conexao.AdicionarParamentros("@ruaFinal", ruaFinal);
                conexao.AdicionarParamentros("@tipoArmazenamento", tipoArmazenamento);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codAuditor", codAuditor);


                //Insere a no banco
                string insert = null;

                if (auditarPicking == false)
                {
                    //Insere a no banco
                    insert = "insert into wms_item_auditoria(aud_codigo, iaud_codigo, apa_codigo, prod_id, iaud_estoque, iaud_estoque_auditado, iaud_vencimento, iaud_lote, " +
                                "iaud_tipo_endereco, iaud_status, usu_codigo_auditor, conf_codigo) " +

                                "select @codAuditoria, gen_id(gen_wms_item_auditoria, 1), a.apa_codigo, a.prod_id, sep_estoque, sep_estoque, sep_validade, sep_lote, sep_tipo, 'AUDITADO', @codAuditor, " +
                                "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "from wms_separacao a " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = a.apa_codigo ";
                }

                if (auditarPicking == true)
                {
                    //Insere a no banco
                    insert = "insert into wms_item_auditoria(aud_codigo, iaud_codigo, apa_codigo, prod_id, iaud_estoque, iaud_vencimento, iaud_lote, " +
                                "iaud_tipo_endereco, iaud_status, usu_codigo_auditor, conf_codigo) " +

                                "select @codAuditoria, gen_id(gen_wms_item_auditoria, 1), a.apa_codigo, a.prod_id, sep_estoque, sep_validade, sep_lote, sep_tipo, 'PENDENTE', @codAuditor, " +
                                "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "from wms_separacao a " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = a.apa_codigo ";
                }

                if (tipo.Equals("ENDERECO"))
                {
                    if (!numeroRegiao.Equals("Selec..."))
                    {
                        insert += "inner join wms_nivel n " +
                                   "on n.niv_codigo = ap.niv_codigo " +
                                   "inner join wms_bloco b " +
                                   "on b.bloc_codigo = n.bloc_codigo " +
                                   "inner join wms_rua r " +
                                   "on r.rua_codigo = b.rua_codigo " +
                                   "inner join wms_regiao re " +
                                   "on re.reg_codigo = r.reg_codigo " +
                                   "where a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and sep_tipo = @tipo and re.reg_numero = @numeroRegiao ";
                    }

                    if (!(ruaInicial.Equals("Selec...") && ruaFinal.Equals("Selec...")))
                    {
                        insert += "and r.rua_numero between @ruaInicial  and @ruaFinal ";

                        if (!lado.Equals("TODOS"))
                        {
                            insert += "and bloc_lado = @lado ";
                        }
                    }

                    insert += "order by ap.apa_ordem";
                }
                else if (tipo.Equals("PRODUTO"))
                {
                    if (idProduto != null && idProduto.Length > 0)
                    {
                        insert += "where a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and a.prod_id = " + idProduto[0] + " ";

                        for (int i = 1; idProduto.Length > i; i++)
                        {
                            if (idProduto[i] != 0)
                            {
                                insert += "or a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and a.prod_id = " + idProduto[i] + " ";
                            }
                        }
                    }

                    insert += "order by ap.apa_ordem";
                }

                

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do picking para auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Gera os itens do picking
        public void GerarItensAuditoriaPickingFlow(string empresa,int codAuditoria, string tipo, string numeroRegiao, string ruaInicial, string ruaFinal, string tipoArmazenamento, string lado, int[] idProduto, int? codAuditor, bool auditarPicking)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@ruaInicial", ruaInicial);
                conexao.AdicionarParamentros("@ruaFinal", ruaFinal);
                conexao.AdicionarParamentros("@tipoArmazenamento", tipoArmazenamento);
                conexao.AdicionarParamentros("@lado", lado);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codAuditor", codAuditor);


                //Insere a no banco
                string insert = null;

                if (auditarPicking == false)
                {
                    //Insere a no banco
                    insert = "insert into wms_item_auditoria(aud_codigo, iaud_codigo, apa_codigo, prod_id, iaud_estoque, iaud_estoque_auditado, iaud_vencimento, iaud_lote, " +
                                 "iaud_tipo_endereco, iaud_status, usu_codigo_auditor, conf_codigo) " +

                                 "select @codAuditoria, gen_id(gen_wms_item_auditoria, 1), a.apa_codigo, a.prod_id, sep_estoque, sep_estoque, sep_validade, sep_lote, sep_tipo, 'AUDITADO', @codAuditor, " +
                                 "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                 "from wms_separacao a " +
                                 "inner join wms_apartamento ap " +
                                 "on ap.apa_codigo = a.apa_codigo " +
                                 "where prod_id in (select prod_id from wms_separacao s " +
                                 "inner join wms_apartamento ap " +
                                 "on ap.apa_codigo = s.apa_codigo ";
                }

                if (auditarPicking == true)
                {
                    //Insere a no banco
                    insert = "insert into wms_item_auditoria(aud_codigo, iaud_codigo, apa_codigo, prod_id, iaud_estoque, iaud_vencimento, iaud_lote, " +
                                 "iaud_tipo_endereco, iaud_status, usu_codigo_auditor, conf_codigo) " +

                                 "select @codAuditoria, gen_id(gen_wms_item_auditoria, 1), a.apa_codigo, a.prod_id, sep_estoque, sep_validade, sep_lote, sep_tipo, 'PENDENTE', @codAuditor, " +
                                 "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                 "from wms_separacao a " +
                                 "inner join wms_apartamento ap " +
                                 "on ap.apa_codigo = a.apa_codigo " +
                                 "where prod_id in (select prod_id from wms_separacao s " +
                                 "inner join wms_apartamento ap " +
                                 "on ap.apa_codigo = s.apa_codigo ";
                }

                if (tipo.Equals("ENDERECO"))
                {
                    if (!numeroRegiao.Equals("Selec..."))
                    {
                        insert += "inner join wms_nivel n " +
                                   "on n.niv_codigo = ap.niv_codigo " +
                                   "inner join wms_bloco b " +
                                   "on b.bloc_codigo = n.bloc_codigo " +
                                   "inner join wms_rua r " +
                                   "on r.rua_codigo = b.rua_codigo " +
                                   "inner join wms_regiao re " +
                                   "on re.reg_codigo = r.reg_codigo " +
                                   "where a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and re.reg_numero = @numeroRegiao ";
                    }

                    if (!(ruaInicial.Equals("Selec...") && ruaFinal.Equals("Selec...")))
                    {
                        if (!lado.Equals("TODOS"))
                        {
                            insert += "and bloc_lado = @lado order by ap.apa_ordem) ";
                        }
                        else
                        {
                            insert += "and r.rua_numero between @ruaInicial and @ruaFinal order by ap.apa_ordem) ";
                        }
                    }
                }
                else if (tipo.Equals("PRODUTO"))
                {
                    if (idProduto != null && idProduto.Length > 0)
                    {
                        insert += "where a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) and a.prod_id = " + idProduto[0] + " ";

                        for (int i = 1; idProduto.Length > i; i++)
                        {
                            if (idProduto[i] != 0)
                            {
                                insert += "or a.prod_id = " + idProduto[i] + " ";
                            }
                        }
                    }

                    insert += "order by ap.apa_ordem";
                }

                

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar os itens do picking para auditoria. \nDetalhes:" + ex.Message);
            }
        }

        //Atualizar Picking
        public void CancelarAuditoria(string empresa, int codAuditoria)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);

                //Insere a no banco
                string update1 = "update wms_auditoria_produto set aud_status = 'CANCELADA' "+
                                       "where aud_codigo = @codAuditoria and aud_status = 'PENDENTE' and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                       "or aud_codigo =  @codAuditoria and aud_status = 'INICIADA' and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Instância um datatable
                conexao.ExecutarConsulta(CommandType.Text, update1);

                //Atualiza o status da auditoria
                string update2 = "update wms_item_auditoria set iaud_status = 'CANCELADA' "+
                                             "where aud_codigo = @codAuditoria and iaud_status = 'PENDENTE' and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Instância um datatable
                conexao.ExecutarConsulta(CommandType.Text, update2);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao cancelar a auditoria. \nDetalhes:" + ex.Message);
            }
        }


        //Atualizar Picking
        public void AtualizarPicking(string empresa, int codAuditoria, int codApartamento, int idProduto, int quantidade, DateTime vencimento)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@quantidade", quantidade);
                conexao.AdicionarParamentros("@vencimento", vencimento);


                //Insere a no banco
                string updatePicking = "update wms_separacao e set e.sep_estoque = @quantidade, e.sep_validade = @vencimento where prod_id = @idProduto and apa_codigo = @codApartamento and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                conexao.ExecutarConsulta(CommandType.Text, updatePicking);

                //Atualiza o status da auditoria
                string updateItemAuditoria = "update wms_item_auditoria i set i.iaud_status = 'FINALIZADO', iaud_finalizada = current_timestamp where iaud_status = 'AUDITADO' AND prod_id = @idProduto and aud_codigo = @codAuditoria and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Instância um datatable
                conexao.ExecutarConsulta(CommandType.Text, updateItemAuditoria);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os picking. \nDetalhes:" + ex.Message);
            }
        }

        //Atualizar Picking
        public void RecusarAuditoria(string empresa, int codAuditoria, int idProduto)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@idProduto", idProduto);

                //Atualiza o status da auditoria
                string updateItemAuditoria = "update wms_item_auditoria i set i.iaud_status = 'PENDENTE', iaud_estoque_auditado = 0 where iaud_status = 'AUDITADO' AND prod_id = @idProduto and aud_codigo = @codAuditoria and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Instância um datatable
                conexao.ExecutarConsulta(CommandType.Text, updateItemAuditoria);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os picking. \nDetalhes:" + ex.Message);
            }
        }

        //Atualizar Picking
        public void AtualizarPulmao(string empresa, int codAuditoria, int codApartamento, int idProduto, int quantidade, DateTime vencimento)
        {
            try
            {
                //Instância uma coleção de objêtos
                ItensAuditoriaCollection itensAuditoriaCollection = new ItensAuditoriaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codAuditoria", codAuditoria);
                conexao.AdicionarParamentros("@codApartamento", codApartamento);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@quantidade", quantidade);
                conexao.AdicionarParamentros("@vencimento", vencimento);


                //Insere a no banco
                string updatePicking = "update wms_armazenagem a set a.arm_quantidade = @quantidade, a.arm_vencimento = @vencimento where prod_id = @idProduto and apa_codigo = @codApartamento and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                conexao.ExecutarConsulta(CommandType.Text, updatePicking);

                //Atualiza o status da auditoria
                string updateItemAuditoria = "update wms_item_auditoria i set i.iaud_status = 'FINALIZADO' where iaud_status = 'AUDITADO' AND prod_id = @idProduto and apa_codigo = @codApartamento and aud_codigo = @codAuditoria and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Instância um datatable
                conexao.ExecutarConsulta(CommandType.Text, updateItemAuditoria);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os endereços de pulmão. \nDetalhes:" + ex.Message);
            }
        }

        public void InserirRastreamento(string empresa, int codUsuario, int idProduto, int codApartamentoOrigem, int quantidadeOrigem, DateTime vencimentoOrigem)//, double pesoOrigem, string loteOrigem,
                                                                                                                                                 //int codApartamentoDestino, int quantidadeDestino, string vencimentoDestino, double pesoDestino, string loteDestino)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codApartamentoOrigem", codApartamentoOrigem);
                // conexao.AdicionarParamentros("@codApartamentoDestino", codApartamentoDestino);
                conexao.AdicionarParamentros("@quantidadeOrigem", quantidadeOrigem);
                //conexao.AdicionarParamentros("@quantidadeDestino", quantidadeDestino);
                // conexao.AdicionarParamentros("@pesoOrigem", pesoOrigem);
                //conexao.AdicionarParamentros("@pesoDestino", pesoDestino);
                conexao.AdicionarParamentros("@vencimentoOrigem", vencimentoOrigem);
                //conexao.AdicionarParamentros("@vencimentoDestino", vencimentoDestino);
                //conexao.AdicionarParamentros("@loteOrigem", loteOrigem);
                //conexao.AdicionarParamentros("@loteDestino", loteDestino);

                //String de insert - insere o endereço  
                string insert = "insert into wms_rastreamento_armazenagem (rast_codigo, rast_operacao, rast_data, usu_codigo, " +
                "apa_codigo_origem, prod_id, arm_quantidade_origem, arm_vencimento_origem, conf_codigo)" +
                "values" +
                "(gen_id(gen_wms_rast_armazenagem, 1), 'AUDITORIA', current_timestamp, @codUsuario, " +
                "@codApartamentoOrigem, @idProduto, @quantidadeOrigem, @vencimentoOrigem, (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";


                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a operação no rastreamento de operações! \nDetalhes: " + ex.Message);
            }
        }


    }
}
