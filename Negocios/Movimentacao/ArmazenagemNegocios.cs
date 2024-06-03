using Dados;
using ObjetoTransferencia;
using System;
using System.Data;

namespace Negocios
{
    public class ArmazenagemNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa a nota cega
        public NotaEntrada PesqNotaCega(string notaCega, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                NotaEntrada notaEntrada = new NotaEntrada();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select distinct(not_cross_docking), not_exigir_validade,  max(not_data_entrada) as data, count(not_numero_cega) as qtdNotaCega, not_numero_cega, u.usu_login, not_conf_inicial, not_conf_final, u1.usu_login as conferente, " +
                /*Pesquisa a quantidade de itens*/
                "(select count(distinct(prod_id)) from wms_itens_nota i " +
                "inner join wms_nota_entrada ni " +
                "on ni.not_codigo = i.not_codigo " +
                "where not_numero_cega = @notaCega) as qtdItens, " +
                /*Pesquisa o peso da nota*/
                "(select sum(not_peso) from wms_nota_entrada where not_numero_cega = @notaCega) as peso, " +
                "f.forn_codigo, f.forn_nome " +
                "from wms_nota_entrada n " +
                "inner join wms_usuario u " +
                "on u.usu_codigo = n.usu_cod_gerou_cega " +
                "left join wms_usuario u1 " +
                "on u1.usu_codigo = n.usu_cod_conf " +
                "inner join wms_fornecedor f " +
                "on f.forn_codigo = n.forn_codigo " +
                "where not_numero_cega = @notaCega and n.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " + //modificado
                "group by not_cross_docking, not_exigir_validade, not_numero_cega, u.usu_login, not_conf_inicial, not_conf_final, conferente, f.forn_codigo, f.forn_nome ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["not_numero_cega"] != DBNull.Value)
                    {
                        notaEntrada.codNotaCega = Convert.ToInt32(linha["not_numero_cega"]);
                    }

                    if (linha["data"] != DBNull.Value)
                    {
                        notaEntrada.dataNotaCega = Convert.ToDateTime(linha["data"]);
                    }

                    if (linha["forn_codigo"] != DBNull.Value)
                    {
                        notaEntrada.codFornecedor = Convert.ToInt32(linha["forn_codigo"]);
                    }

                    if (linha["forn_nome"] != DBNull.Value)
                    {
                        notaEntrada.nmFornecedor = Convert.ToString(linha["forn_nome"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        notaEntrada.usuarioNotaCega = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["qtdNotaCega"] != DBNull.Value)
                    {
                        notaEntrada.quantidadeNota = Convert.ToInt32(linha["qtdNotaCega"]);
                    }

                    if (linha["qtdItens"] != DBNull.Value)
                    {
                        notaEntrada.quantidadeItens = Convert.ToInt32(linha["qtdItens"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        notaEntrada.pesoNota = Convert.ToDouble(linha["peso"]);
                    }

                    if (linha["not_exigir_validade"] != DBNull.Value)
                    {
                        notaEntrada.exigirValidade = Convert.ToBoolean(linha["not_exigir_validade"]);
                    }

                    if (linha["not_cross_docking"] != DBNull.Value)
                    {
                        notaEntrada.crossDocking = Convert.ToBoolean(linha["not_cross_docking"]);
                    }

                    if (linha["conferente"] != DBNull.Value)
                    {
                        notaEntrada.conferente = Convert.ToString(linha["conferente"]);
                    }

                    if (linha["not_conf_inicial"] != DBNull.Value)
                    {
                        notaEntrada.inicioConferencia = Convert.ToDateTime(linha["not_conf_inicial"]);
                    }

                    if (linha["not_conf_final"] != DBNull.Value)
                    {
                        notaEntrada.fimConferencia = Convert.ToDateTime(linha["not_conf_final"]);
                    }

                }
                //Retorna os valores encontrado
                return notaEntrada;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a nota cega. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os itens da nota cega
        public ItensNotaEntradaCollection PesqProdutoNotaCega(string notaCega, string empresa)
        {
            try
            {
                //Instância a camada de objetos
                ItensNotaEntradaCollection itensNotaEntradaCollection = new ItensNotaEntradaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select " +
                /*Verifica o código do endereo de grandeza*/
                "(select s.apa_codigo from  wms_separacao s " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where s.sep_tipo = 'CAIXA' and s.prod_id = p.prod_id and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as apa_grandeza, " +
                /*Verifica o endereo de grandeza*/
                "(select a.apa_endereco from wms_separacao s " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where s.sep_tipo = 'CAIXA' and s.prod_id = p.prod_id and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as endereco_grandeza, " +
                /*Verifica a região da grandeza*/
                "(select reg_numero from wms_separacao s " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where s.sep_tipo = 'CAIXA' and s.prod_id = p.prod_id and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as regiao_grandeza, " +
                /*Verifica a rua de grandeza*/
                "(select rua_numero from wms_separacao s " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where s.sep_tipo = 'CAIXA' and s.prod_id = p.prod_id and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as rua_grandeza,  " +
                /*Verifica o código do endereço do flowrack*/
                "(select s.apa_codigo from  wms_separacao s " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where s.sep_tipo = 'FLOWRACK' and s.prod_id = p.prod_id and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as apa_flowrack, " +
                /*Verifica o endereo do flowrack*/
                "(select a.apa_endereco from wms_separacao s " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where s.sep_tipo = 'FLOWRACK' and s.prod_id = p.prod_id and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as endereco_flowrack, " +
                /*Verifica a região do flowrack*/
                "(select reg_numero from wms_separacao s " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where s.sep_tipo = 'FLOWRACK' and s.prod_id = p.prod_id and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as regiao_flowrack,  " +
                /*Verifica a rua do flowrack*/
                "(select rua_numero from wms_separacao s " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where s.sep_tipo = 'FLOWRACK' and s.prod_id = p.prod_id and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)) as rua_flowrack, " +
                /*---*/
                "n.not_liberar_armaz_fim, p.prod_id, prod_codigo, prod_descricao, sum(i.inot_quantidade) as inot_quantidade, sum(inot_quantidade_conf) as inot_quantidade_conf, sum(coalesce(i.inot_falta + i.inot_avaria, 0)) as falta_conferencia, inot_armazenagem, coalesce(inot_lote, '') as inot_lote, inot_validade, inot_palete_associado, " +
                "p.prod_tipo_palete, p.prod_niv_maximo, p.prod_palete_blocado, " +
                "p.prod_lastro_p, p.prod_altura_p, p.prod_lastro_m, p.prod_altura_m, p.prod_lastro_g, p.prod_altura_g, p.prod_lastro_b, p.prod_altura_b, " +
                "p.prod_fator_pulmao, p.prod_vida_util, p.prod_tolerancia, cat_descricao, c.cat_lote, prod_controla_validade, prod_peso, prod_peso_variavel, prod_tipo_armazenamento, uni_unidade " +
                "from wms_itens_nota i " +
                "inner join wms_nota_entrada n " +
                "on n.not_codigo = i.not_codigo and i.conf_codigo = n.conf_codigo " +
                "inner join wms_produto p " +
                "on p.prod_id = i.prod_id and p.conf_codigo = i.conf_codigo " +
                "left join wms_categoria c " +
                "on c.cat_codigo = p.cat_codigo " +
                "left join wms_unidade u " +
                "on u.uni_codigo = uni_codigo_pulmao " +
                "where not_numero_cega = @notaCega " +
                "and i.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " + //aqui
                "group by n.not_liberar_armaz_fim, p.prod_id, prod_codigo, prod_descricao, inot_armazenagem, inot_lote, inot_validade, inot_palete_associado, " +
                "p.prod_tipo_palete, p.prod_niv_maximo, p.prod_palete_blocado, " +
                "p.prod_lastro_p, p.prod_altura_p, p.prod_lastro_m, p.prod_altura_m, p.prod_lastro_g, p.prod_altura_g, p.prod_lastro_b, p.prod_altura_b, " +
                 "p.prod_fator_pulmao, p.prod_vida_util, p.prod_tolerancia, cat_descricao, c.cat_lote, prod_controla_validade, prod_peso, prod_peso_variavel, prod_tipo_armazenamento, uni_unidade ";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto 
                    ItensNotaEntrada itensNotaEntrada = new ItensNotaEntrada();

                    //Verifica se o produto possui endereço de grandeza
                    if (linha["endereco_grandeza"] != DBNull.Value)
                    {
                        itensNotaEntrada.descApartamento = Convert.ToString(linha["endereco_grandeza"]);

                        if (linha["apa_grandeza"] != DBNull.Value)
                        {
                            itensNotaEntrada.codApartamento = Convert.ToInt32(linha["apa_grandeza"]);
                        }

                        if (linha["regiao_grandeza"] != DBNull.Value)
                        {
                            itensNotaEntrada.numeroRegiao = Convert.ToInt32(linha["regiao_grandeza"]);
                        }

                        if (linha["rua_grandeza"] != DBNull.Value)
                        {
                            itensNotaEntrada.numeroRua = Convert.ToInt32(linha["rua_grandeza"]);
                        }
                    }
                    //Verifica se o produto possui endereço de flowrack
                    else if (linha["endereco_flowrack"] != DBNull.Value)
                    {
                        itensNotaEntrada.descApartamento = Convert.ToString(linha["endereco_flowrack"]);

                        if (linha["apa_flowrack"] != DBNull.Value)
                        {
                            itensNotaEntrada.codApartamento = Convert.ToInt32(linha["apa_flowrack"]);
                        }

                        if (linha["regiao_flowrack"] != DBNull.Value)
                        {
                            itensNotaEntrada.numeroRegiao = Convert.ToInt32(linha["regiao_flowrack"]);
                        }

                        if (linha["rua_flowrack"] != DBNull.Value)
                        {
                            itensNotaEntrada.numeroRua = Convert.ToInt32(linha["rua_flowrack"]);
                        }
                    }
                    else
                    {
                        itensNotaEntrada.descApartamento = "SEM PICKING";
                    }

                    if (linha["prod_tipo_armazenamento"] != DBNull.Value)
                    {
                        itensNotaEntrada.tipoArmazenamento = Convert.ToString(linha["prod_tipo_armazenamento"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        itensNotaEntrada.undPulmao = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itensNotaEntrada.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itensNotaEntrada.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itensNotaEntrada.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        itensNotaEntrada.pesoVariavel = Convert.ToBoolean(linha["prod_peso_variavel"]);
                    }


                    //Verifica a opção de amazenar somente ao finalizar a conferência
                    if (linha["not_liberar_armaz_fim"].Equals("True"))
                    {
                        if (linha["inot_quantidade"] != DBNull.Value)
                        {
                            itensNotaEntrada.quantidadeConferida = Convert.ToDouble(linha["inot_quantidade"]);
                        }
                    }
                    else if (linha["not_liberar_armaz_fim"].Equals("False"))
                    {
                        if (linha["inot_quantidade"] != DBNull.Value)
                        {
                            itensNotaEntrada.quantidadeConferida = Convert.ToDouble(linha["inot_quantidade"]) ;

                            //Verifica se houve falta e avaria na conferência
                            if (linha["falta_conferencia"] != DBNull.Value)
                            {
                                itensNotaEntrada.quantidadeConferida = Convert.ToDouble(linha["inot_quantidade"]) - Convert.ToInt32(linha["falta_conferencia"]);
                            }
                        }

                    }

                    if (linha["inot_armazenagem"] != DBNull.Value)
                    {
                        itensNotaEntrada.quantidadeArmazenada = Convert.ToInt32(linha["inot_armazenagem"]);
                    }

                    if (linha["inot_validade"] != DBNull.Value)
                    {
                        itensNotaEntrada.validadeProduto = Convert.ToDateTime(linha["inot_validade"]);
                    }

                    if (linha["inot_lote"] != DBNull.Value)
                    {
                        itensNotaEntrada.loteProduto = Convert.ToString(linha["inot_lote"]);
                    }

                    if (linha["inot_palete_associado"] != DBNull.Value)
                    {
                        itensNotaEntrada.paleteAssociado = Convert.ToInt32(linha["inot_palete_associado"]);
                    }

                    if (linha["prod_tipo_palete"] != DBNull.Value)
                    {
                        itensNotaEntrada.tipoPalete = Convert.ToString(linha["prod_tipo_palete"]);
                    }

                    if (linha["prod_niv_maximo"] != DBNull.Value)
                    {
                        itensNotaEntrada.NivelMaximo = Convert.ToInt32(linha["prod_niv_maximo"]);
                    }

                    if (linha["prod_palete_blocado"] != DBNull.Value)
                    {
                        itensNotaEntrada.aceitaBlocado = Convert.ToBoolean(linha["prod_palete_blocado"]);
                    }

                    if (linha["prod_lastro_p"] != DBNull.Value)
                    {
                        itensNotaEntrada.lastroPequeno = Convert.ToInt32(linha["prod_lastro_p"]);
                    }

                    if (linha["prod_altura_p"] != DBNull.Value)
                    {
                        itensNotaEntrada.alturaPequeno = Convert.ToInt32(linha["prod_altura_p"]);
                    }

                    if (linha["prod_lastro_m"] != DBNull.Value)
                    {
                        itensNotaEntrada.lastroMedio = Convert.ToInt32(linha["prod_lastro_m"]);
                    }

                    if (linha["prod_altura_m"] != DBNull.Value)
                    {
                        itensNotaEntrada.alturaMedio = Convert.ToInt32(linha["prod_altura_m"]);
                    }

                    if (linha["prod_lastro_g"] != DBNull.Value)
                    {
                        itensNotaEntrada.lastroGrande = Convert.ToInt32(linha["prod_lastro_g"]);
                    }

                    if (linha["prod_altura_g"] != DBNull.Value)
                    {
                        itensNotaEntrada.alturaGrande = Convert.ToInt32(linha["prod_altura_g"]);
                    }

                    if (linha["prod_lastro_b"] != DBNull.Value)
                    {
                        itensNotaEntrada.lastroBlocado = Convert.ToInt32(linha["prod_lastro_b"]);
                    }

                    if (linha["prod_altura_b"] != DBNull.Value)
                    {
                        itensNotaEntrada.alturaBlocado = Convert.ToInt32(linha["prod_altura_b"]);
                    }

                    if (linha["prod_fator_pulmao"] != DBNull.Value)
                    {
                        itensNotaEntrada.fatorPulmao = Convert.ToInt32(linha["prod_fator_pulmao"]);
                    }
                    else
                    {
                        itensNotaEntrada.fatorPulmao = 1;
                    }

                    if (linha["prod_vida_util"] != DBNull.Value)
                    {
                        itensNotaEntrada.vidaProduto = Convert.ToInt32(linha["prod_vida_util"]);
                    }

                    if (linha["prod_tolerancia"] != DBNull.Value)
                    {
                        itensNotaEntrada.toleranciaProduto = Convert.ToInt32(linha["prod_tolerancia"]);
                    }

                    if (linha["cat_descricao"] != DBNull.Value)
                    {
                        itensNotaEntrada.descCategoria = Convert.ToString(linha["cat_descricao"]);
                    }

                    if (linha["prod_controla_validade"] != DBNull.Value)
                    {
                        itensNotaEntrada.controlaVencimentoProduto = Convert.ToBoolean(linha["prod_controla_validade"]);

                    }

                    if (linha["cat_lote"] != DBNull.Value)
                    {
                        itensNotaEntrada.controlaLoteCategoria = Convert.ToBoolean(linha["cat_lote"]);
                    }

                    if (linha["prod_peso"] != DBNull.Value)
                    {
                        itensNotaEntrada.pesoProduto = Convert.ToDouble(linha["prod_peso"]);
                    }





                    //Adiciona o objêto a coleção
                    itensNotaEntradaCollection.Add(itensNotaEntrada);
                }
                //Retorna os valores encontrado
                return itensNotaEntradaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens da nota cega. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o endereço
        public string[] PesqEnderecoVago(string descEndereco)
        {
            try
            {               
                //Limpa a conexão
                conexao.LimparParametros();
                //adiciona o parâmetro
                conexao.AdicionarParamentros("@descEndereco", descEndereco+"%");

                //String de consulta
                string select = "select a.apa_codigo, apa_endereco from wms_apartamento a "+
                                "where apa_tipo = 'Pulmao' and apa_disponivel = 'Sim' and apa_status = 'Vago' and apa_endereco like @descEndereco " +
                                "and (select count(prod_id) from wms_armazenagem where apa_codigo = a.apa_codigo) = 0 " +
                                "order by apa_ordem ";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                string[] endereco = new string[dataTable.Rows.Count];

                int count = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    
                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco[count] = Convert.ToString(linha["apa_endereco"]);
                    }

                    count++;
                }

                return endereco;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereço digitado! \nDetalhes: " + ex.Message);
            }
        }


        //Pesquisa o endereço
       //public EstruturaCollection PesqEndereco(string descEndereco, string empresa)
        public EstruturaCollection PesqEndereco(string descEndereco)
        {
            try
            {
                
                //Limpa a conexão
                conexao.LimparParametros();
                //adiciona o parâmetro
                conexao.AdicionarParamentros("@descEndereco", descEndereco);
                //conexao.AdicionarParamentros("@empresa", empresa);
                //Instância a coleção de objêto
                EstruturaCollection enderecoCollection = new EstruturaCollection();

                //String de consulta
                string select = "select rg.reg_tipo, a.apa_codigo, a.apa_endereco, prod_codigo, prod_descricao, n.niv_numero, apa_tipo, apa_tamanho_palete, " +
                    "(p.prod_lastro_m * p.prod_altura_m) as total_palete, (arm_quantidade / p.prod_fator_pulmao) as arm_estoque, " +
                    "apa_disponivel, " +
                "(select count(prod_id) from wms_armazenagem ar where ar.apa_codigo = a.apa_codigo) as quantidade_produto, " +
                "(select iconf_valor from wms_itens_configuracao ic where ic.iconf_descricao = 'QUANTIDADE MÁXIMA DE PRODUTO NO PALETE' and iconf_status = 'True') as quantitade_maxima " +
                "from wms_apartamento a " +
                "inner join wms_nivel n  " +
                "on n.niv_codigo = a.niv_codigo " +
                "inner join wms_bloco b " +
                "on b.bloc_codigo = n.bloc_codigo " +
                "inner join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "inner join wms_regiao rg " +
                "on rg.reg_codigo = r.reg_codigo " +
                "left join wms_armazenagem ar " +
                "on ar.apa_codigo = a.apa_codigo " +
                "left join wms_produto p " +
                "on p.prod_id = ar.prod_id " +
                "where apa_endereco = @descEndereco";
                //"and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto endereço
                    Estrutura endereco = new Estrutura();

                    //Preenche o objêto
                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        endereco.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.descApartamento = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.descProduto = Convert.ToString(linha["prod_codigo"] + " - "+ linha["prod_descricao"]);
                    }

                    if (linha["total_palete"] != DBNull.Value)
                    {
                        endereco.paleteProduto = Convert.ToDouble(linha["total_palete"]);
                    }

                    if (linha["arm_estoque"] != DBNull.Value)
                    {
                        endereco.estoqueProduto = Convert.ToDouble(linha["arm_estoque"]);
                    }

                    if (linha["niv_numero"] != DBNull.Value)
                    {
                        endereco.numeroNivel = Convert.ToInt32(linha["niv_numero"]);
                    }

                    if (linha["apa_tipo"] != DBNull.Value)
                    {
                        endereco.tipoApartamento = Convert.ToString(linha["apa_tipo"]);
                    }

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        endereco.tamanhoApartamento = Convert.ToString(linha["apa_tamanho_palete"]);
                    }

                    if (linha["apa_disponivel"] != DBNull.Value)
                    {
                        endereco.disposicaoApartamento = Convert.ToString(linha["apa_disponivel"]);
                    }

                    if (linha["quantidade_produto"] != DBNull.Value)
                    {
                        endereco.qtdPulmaoOcupado = Convert.ToInt32(linha["quantidade_produto"]);
                    }

                    if (linha["quantitade_maxima"] != DBNull.Value)
                    {
                        endereco.qtdPulmaoMaximo = Convert.ToInt32(linha["quantitade_maxima"]);
                    }

                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereço digitado! \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o endereço do produto armazenado
        public EnderecoPulmaoCollection PesqProdutoArmazenado(string notaCega, string empresa)
        {
            try
            {
                //Instância a camada de objêto
                EnderecoPulmaoCollection enderecoPulmaoCollection = new EnderecoPulmaoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //adiciona o parâmetro
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select am.prod_id, apa_endereco, arm_quantidade from wms_armazenagem am " +
                "inner join wms_apartamento ap " +
                "on ap.apa_codigo = am.apa_codigo " +
                "where not_nota_cega = @notaCega and am.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    EnderecoPulmao enderecoPulmao = new EnderecoPulmao();

                    //Preenche o objêto
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        enderecoPulmao.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        enderecoPulmao.descEndereco1 = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["arm_quantidade"] != DBNull.Value)
                    {
                        enderecoPulmao.qtdCaixaOrigem = Convert.ToInt32(linha["arm_quantidade"]);
                    }

                    enderecoPulmaoCollection.Add(enderecoPulmao);
                }

                return enderecoPulmaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os endereços dos produtos armazenados! \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o endereço para o armazenamento automático
        public EstruturaCollection PesqEnderecoAutomatico(int numeroRegiao, int numeroRua, int nivel)
        {
            try
            {
                //Instância o objêto endereço
                EstruturaCollection enderecoCollection = new EstruturaCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //adiciona o parâmetro
                conexao.AdicionarParamentros("@regiao", numeroRegiao);
                conexao.AdicionarParamentros("@rua", numeroRua);
                conexao.AdicionarParamentros("@nivel", nivel);

                //String de consulta
                string select = "select a.apa_codigo, a.apa_endereco, a.apa_tamanho_palete, reg_tipo, niv_numero from wms_apartamento a " +
                "left join wms_nivel ni " +
                "on ni.niv_codigo = a.niv_codigo " +
                "left join wms_bloco b " +
                "on b.bloc_codigo = ni.bloc_codigo " +
                "left join wms_rua r " +
                "on r.rua_codigo = b.rua_codigo " +
                "left join wms_regiao re " +
                "on re.reg_codigo = r.reg_codigo " +
                "where reg_numero = @regiao and rua_numero = @rua and niv_numero <= @nivel and apa_tipo = 'Pulmao' and apa_status = 'Vago' " +
                "and apa_disponivel = 'Sim'  " +
                "order by a.apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto endereço
                    Estrutura endereco = new Estrutura();

                    //Preenche o objêto
                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        endereco.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.descApartamento = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["niv_numero"] != DBNull.Value)
                    {
                        endereco.numeroNivel = Convert.ToInt32(linha["niv_numero"]);
                    }

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        endereco.tamanhoApartamento = Convert.ToString(linha["apa_tamanho_palete"]);
                    }

                    enderecoCollection.Add(endereco);

                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereço para o armazenamento automático! \nDetalhes: " + ex.Message);
            }
        }

        //Insere o produto na área de armazenagem e atualiza a quantidade armazenada na nota de entrada
        public void ArmazenarProduto(bool controlaEndereco, int codUsuario, string notaCega, string empresa, int codEndereco, int idProduto, double quantidade, double peso, DateTime vencimento, string lote, double totalArmazenado)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codEndereco", codEndereco);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@quantidade", quantidade);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@vencimento", vencimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@totalArmazenado", totalArmazenado);


                //String de update - atualiza a quantidade armazenada
                string update = "update wms_itens_nota set inot_armazenagem = @totalArmazenado where prod_id = @idProduto and not_codigo in (select not_codigo from wms_nota_entrada where not_numero_cega = @notaCega) " +
                                "and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

                //String de atualização
                string updateStatus = "update wms_apartamento set apa_status = 'Ocupado' " +
                    "where apa_codigo = @codEndereco and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, updateStatus);

                if (controlaEndereco == false)
                {
                    //String de insert - insere o endereço
                    string insert = "insert into wms_armazenagem (arm_codigo, arm_data_armazenagem, usu_codigo, not_nota_cega, apa_codigo, prod_id, arm_quantidade, " +
                    "arm_peso, arm_vencimento, arm_lote, conf_codigo)" +
                    "values" +
                    "(gen_id(gen_wms_armazenagem, 1), current_timestamp, @codUsuario, @notaCega, @codEndereco, @idProduto, @quantidade, @peso, @vencimento, @lote," +
                    "(select conf_codigo from wms_configuracao where conf_sigla = @empresa))";

                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, insert);
                }
                else
                {
                    //String de update - atualiza o novo endereço
                    string updateEndereco = "update wms_armazenagem set arm_quantidade = @quantidade, arm_data_armazenagem = current_timestamp, usu_codigo = @codUsuario, not_nota_cega = @notaCega " +
                        "where apa_codigo = @codEndereco and prod_id = @idProduto and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                    //Executa o script no banco
                    conexao.ExecuteManipulacao(CommandType.Text, updateEndereco);
                }

                

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao endereçar o produto! Detalhes:\n" + ex.Message);
            }


        }

        //Insere a operação no rastreamento
        public void InserirRastreamento(int codUsuario, string notaCega, string empresa, int codEndereco, int idProduto, double quantidade_nota, double quantidade, double peso, DateTime vencimento, string lote)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@codEndereco", codEndereco);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@quantidade", quantidade);
                conexao.AdicionarParamentros("@peso", peso);
                conexao.AdicionarParamentros("@vencimento", vencimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@quantidadeNota", quantidade_nota);




                //String de insert - insere o endereço
                string insert = "insert into wms_rastreamento_armazenagem (rast_codigo, rast_operacao, rast_data, usu_codigo, not_nota_cega," +
                "apa_codigo_destino, prod_id, arm_quantidade_origem, arm_quantidade_destino, arm_peso_destino, arm_vencimento_destino, arm_lote_destino, conf_codigo)" +
                "values" +
                "(gen_id(gen_wms_rast_armazenagem, 1), 'ARMAZENAGEM',current_timestamp, @codUsuario, @notaCega, " +
                "@codEndereco, @idProduto, @quantidadeNota, @quantidade, @peso, @vencimento, @lote, (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao resgistrar no rastreamento de armazenagem! Detalhes:\n" + ex.Message);
            }


        }

        //Pesquisa os produtos armazenados para impressão
        public EnderecoPulmaoCollection PesqProdutoImpressao(string empresa, string notaCega, int idProduto)
        {
            try
            {
                //Instância a camada de objêto
                EnderecoPulmaoCollection enderecoPulmaoCollection = new EnderecoPulmaoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //adiciona o parâmetro
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select ap.apa_codigo, ap.apa_endereco, prod_codigo, prod_descricao, a.arm_data_armazenagem, a.arm_quantidade/prod_fator_pulmao, a.arm_lote, a.arm_vencimento, a.arm_peso, not_nota_cega, " +
                                "(select c.conf_empresa from wms_configuracao c) as emp_nome" +
                                "from wms_armazenagem a " +
                                "inner join wms_produto p " +
                                "on p.prod_id = a.prod_id " +
                                "inner join wms_apartamento ap " +
                                "on ap.apa_codigo = a.apa_codigo " +
                                "where a.not_nota_cega = @notaCega and prod_id = @idProduto and a.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)  " +
                                "order by ap.apa_ordem ";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    EnderecoPulmao enderecoPulmao = new EnderecoPulmao();

                    //Preenche o objêto
                    if (linha["emp_nome"] != DBNull.Value)
                    {
                        enderecoPulmao.nomeEmpresa = Convert.ToString(linha["emp_nome"]);
                    }

                    if (linha["not_nota_cega"] != DBNull.Value)
                    {
                        enderecoPulmao.notaCega = Convert.ToInt32(linha["not_nota_cega"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.codApartamento1 = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        enderecoPulmao.descEndereco1 = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        enderecoPulmao.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        enderecoPulmao.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["arm_data_armazenagem"] != DBNull.Value)
                    {
                        enderecoPulmao.dataEntrada = Convert.ToDateTime(linha["arm_data_armazenagem"]);
                    }

                    if (linha["arm_quantidade"] != DBNull.Value)
                    {
                        enderecoPulmao.qtdCaixaOrigem = Convert.ToInt32(linha["arm_quantidade"]);
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        enderecoPulmao.loteProduto1 = Convert.ToString(linha["arm_lote"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        enderecoPulmao.vencimentoProduto1 = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_peso"] != DBNull.Value)
                    {
                        enderecoPulmao.pesoProduto1 = Convert.ToDouble(linha["arm_peso"]);
                    }

                    enderecoPulmaoCollection.Add(enderecoPulmao);
                }

                return enderecoPulmaoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto para impressão! \nDetalhes: " + ex.Message);
            }
        }

        //Insere a operação no rastreamento
        public void AtualizaImpressaoEtiqueta(string empresa, int idProduto, string notaCega)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@notaCega", notaCega);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de insert - insere o endereço
                string insert = "update wms_armazenagem set arm_impresso = 'Sim' where prod_id = @idProduto and not_nota_cega = @notaCega and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao resgistrar no rastreamento de armazenagem! Detalhes:\n" + ex.Message);
            }


        }


    }
}
