using System;
using Dados;
using ObjetoTransferencia;
using System.Data;
using ObjetoTransferencia.Impressao;
using System.Reflection;

namespace Negocios




{
    public class EnderecamentoNegocios
    {
        //Instância o objeto
        Conexao conexao = new Conexao();

        //Pesquisa o endereço do produto para endereçar
        public EnderecoPicking PesqProduto(string codProduto, string tipo, string empresa)
        {
            try
            {
                EnderecoPicking enderecoProduto = new EnderecoPicking();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select s.sep_codigo, a.apa_codigo, a.apa_endereco, apa_status, p.prod_id, prod_codigo, prod_status, prod_descricao, s.sep_estoque, uni_unidade, s.sep_validade, " +
                "s.sep_capacidade, s.sep_abastecimento, sep_peso, sep_tipo, sep_lote, a.apa_disponivel " +
                "from wms_produto p " +
                "left join wms_unidade u " +
                "on u.uni_codigo = p.uni_codigo_picking " +
                "left join wms_separacao s " +
                "on s.prod_id = p.prod_id " +
                "left join wms_apartamento a " +
                "on a.apa_codigo = s.apa_codigo " +
                "where prod_codigo = @codProduto " +
                "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["prod_id"] != DBNull.Value)
                    {
                        enderecoProduto.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        enderecoProduto.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        enderecoProduto.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_status"] != DBNull.Value)
                    {
                        enderecoProduto.statusProduto = Convert.ToBoolean(linha["prod_status"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        enderecoProduto.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (Convert.ToString(linha["sep_tipo"]).Equals(tipo))
                    {

                        if (linha["sep_codigo"] != DBNull.Value)
                        {
                            enderecoProduto.codSeparacao = Convert.ToInt32(linha["sep_codigo"]);
                        }

                        if (linha["apa_codigo"] != DBNull.Value)
                        {
                            enderecoProduto.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                        }

                        if (linha["apa_endereco"] != DBNull.Value)
                        {
                            enderecoProduto.endereco = Convert.ToString(linha["apa_endereco"]);
                        }

                        if (linha["sep_estoque"] != DBNull.Value)
                        {
                            enderecoProduto.estoque = Convert.ToInt32(linha["sep_estoque"]);
                        }

                        if (linha["sep_validade"] != DBNull.Value)
                        {
                            enderecoProduto.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                        }

                        if (linha["sep_peso"] != DBNull.Value)
                        {
                            enderecoProduto.peso = Convert.ToDouble(linha["sep_peso"]);
                        }

                        if (linha["sep_lote"] != DBNull.Value)
                        {
                            enderecoProduto.lote = Convert.ToString(linha["sep_lote"]);
                        }

                        if (linha["sep_capacidade"] != DBNull.Value)
                        {
                            enderecoProduto.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                        }

                        if (linha["sep_abastecimento"] != DBNull.Value)
                        {
                            enderecoProduto.abastecimento = Convert.ToInt32(linha["sep_abastecimento"]);
                        }

                        if (linha["apa_status"] != DBNull.Value)
                        {
                            enderecoProduto.apDisponibilidade = Convert.ToString(linha["apa_status"]);
                        }

                        if (linha["apa_disponivel"] != DBNull.Value)
                        {
                            enderecoProduto.apDisponibilidade = Convert.ToString(linha["apa_disponivel"]);
                        }

                        break;
                    }
                }

                return enderecoProduto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o produto \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o endereço completo
        public EnderecoPicking PesqEndereco(string descEndereco)
        {
            try
            {
                EnderecoPicking endereco = new EnderecoPicking();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@endereco", descEndereco);

                //String de consulta
                string select = "select b.bloc_codigo, a.apa_codigo, p.prod_id, p.prod_codigo, prod_descricao, " +
                    "a.apa_tipo, sep_estoque, sep_validade, sep_peso, sep_lote, sep_tipo, b.est_codigo, " +
                    "apa_tamanho_palete, apa_status, apa_disponivel, apa_ordem " +
                    "from wms_apartamento a " +
                    "inner join wms_nivel n " +
                    "on n.niv_codigo = a.niv_codigo " +
                    "inner join wms_bloco b " +
                    "on b.bloc_codigo = n.bloc_codigo " +
                    "left join wms_separacao s " +
                    "on s.apa_codigo = a.apa_codigo " +
                    "left join wms_produto p " +
                    "on p.prod_id = s.prod_id " +
                    "where a.apa_endereco = @endereco " +
                    "order by apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados  
                    if (linha["bloc_codigo"] != DBNull.Value)
                    {
                        endereco.codBloco = Convert.ToInt32(linha["bloc_codigo"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_tipo"] != DBNull.Value)
                    {
                        endereco.tipoEndereco = Convert.ToString(linha["apa_tipo"]);
                    }

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        endereco.paleteEndereco = Convert.ToString(linha["apa_tamanho_palete"]);
                    }

                    if (linha["apa_status"] != DBNull.Value)
                    {
                        endereco.apStatus = Convert.ToString(linha["apa_status"]);
                    }

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        endereco.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["apa_disponivel"] != DBNull.Value)
                    {
                        endereco.apDisponibilidade = Convert.ToString(linha["apa_disponivel"]);
                    }

                    if (linha["apa_ordem"] != DBNull.Value)
                    {
                        endereco.ordemEndereco = Convert.ToString(linha["apa_ordem"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        endereco.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_peso"] != DBNull.Value)
                    {
                        endereco.peso = Convert.ToDouble(linha["sep_peso"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        endereco.tipoPicking = Convert.ToString(linha["sep_tipo"]);
                    }
                }

                return endereco;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereco \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o endereço por regiao, rua, bloco, nivel
        public EnderecoPickingCollection PesqEnderecoPicking(string empresa, int numeroRegiao, int numeroRua, string numeroBloco, string numeroNivel, string status, string disponibilidade, string lado)
        {
            try
            {
                //Instânicia a camada de objêto
                EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);
                conexao.AdicionarParamentros("@lado", lado);

                //String de consulta
                string select = "select b.est_codigo, a.apa_codigo, apa_endereco, apa_tamanho_palete, apa_status, apa_disponivel, " +
                                "p.prod_id, prod_codigo, prod_descricao, coalesce(s.sep_estoque, 0) as sep_estoque, uni_unidade, s.sep_validade, s.sep_peso, " +
                                "s.sep_lote, s.sep_capacidade, s.sep_abastecimento, sep_tipo, " +
                                "re.reg_numero, ru.rua_numero, b.bloc_numero, n.niv_numero, a.apa_numero, apa_ordem, " +
                                "(select conf_empresa from wms_configuracao where conf_sigla = @empresa ) as conf_empresa," +
                                "(case WHEN (s.sep_tipo = 'FLOWRACK') THEN " +
                                "(select first 1 a1.apa_endereco from wms_apartamento a1 " +
                                "inner join wms_separacao s1 " +
                                "on s1.apa_codigo = a1.apa_codigo " +
                                "where s1.prod_id = s.prod_id and s1.sep_tipo = 'CAIXA') " +
                                "end) as apa_enereco_cxa " +
                                "from wms_apartamento a " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo and re.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "left join wms_separacao s " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "left join wms_produto p " +
                                "on p.prod_id = s.prod_id and s.conf_codigo = p.conf_codigo " +
                                "left join wms_unidade u " +
                                "on u.uni_codigo = p.uni_codigo_picking  " +
                                "where apa_tipo = 'Separacao' and " +
                                "re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua " +
                              
                                //"and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "and re.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                if (!numeroBloco.Equals(""))
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                if (!numeroNivel.Equals(""))
                {
                    select += " and niv_numero = @numeroNivel ";
                }

                if (status.Equals("VAGO"))
                {
                    select += " and a.apa_status = 'Vago' ";
                }
                else if (status.Equals("OCUPADO"))
                {
                    select += " and a.apa_status = 'Ocupado' ";
                }

                if (disponibilidade.Equals("SIM"))
                {
                    select += " and a.apa_disponivel = 'Sim' ";
                }
                else if (disponibilidade.Equals("NÃO"))
                {
                    select += " and a.apa_disponivel = 'Nao' ";
                }

                if (!(lado.Equals("") || lado.Equals("Todos")))
                {
                    select += " and b.bloc_lado =  @lado ";
                }

                select += " order by apa_ordem ";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    EnderecoPicking endereco = new EnderecoPicking();

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        endereco.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["apa_enereco_cxa"] != DBNull.Value)
                    {
                        endereco.enderecoExtra = Convert.ToString(linha["apa_enereco_cxa"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        endereco.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        endereco.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_peso"] != DBNull.Value)
                    {
                        endereco.peso = Convert.ToDouble(linha["sep_peso"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    if (linha["sep_capacidade"] != DBNull.Value)
                    {
                        endereco.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                    }

                    if (linha["sep_abastecimento"] != DBNull.Value)
                    {
                        endereco.abastecimento = Convert.ToInt32(linha["sep_abastecimento"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        endereco.tipoEndereco = Convert.ToString(linha["sep_tipo"]);
                    }

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        endereco.paleteEndereco = Convert.ToString(linha["apa_tamanho_palete"]);
                    }

                    if (linha["apa_status"] != DBNull.Value)
                    {
                        endereco.apStatus = Convert.ToString(linha["apa_status"]);
                    }

                    if (linha["apa_disponivel"] != DBNull.Value)
                    {
                        endereco.apDisponibilidade = Convert.ToString(linha["apa_disponivel"]);
                    }

                    if (linha["sep_abastecimento"] != DBNull.Value)
                    {
                        endereco.abastecimento = Convert.ToInt32(linha["sep_abastecimento"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        endereco.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        endereco.numeroRua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        endereco.numeroBloco = Convert.ToInt32(linha["bloc_numero"]);
                    }

                    if (linha["niv_numero"] != DBNull.Value)
                    {
                        endereco.numeroNivel = Convert.ToInt32(linha["niv_numero"]);
                    }

                    if (linha["apa_numero"] != DBNull.Value)
                    {
                        endereco.numeroApartamento = Convert.ToInt32(linha["apa_numero"]);
                    }

                    if (linha["apa_ordem"] != DBNull.Value)
                    {
                        endereco.ordemEndereco = Convert.ToString(linha["apa_ordem"]);
                    }

                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        endereco.nomeEmpresa = Convert.ToString(linha["conf_empresa"]);
                    }



                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereco \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa o endereço por regiao, rua, bloco, nivel
        public EnderecoPickingCollection PesqEnderecoPulmao(string empresa, int numeroRegiao, int numeroRua, string numeroBloco, string numeroNivel, string status, string disponibilidade, string lado)
        {
            try
            {
                //Instânicia a camada de objêto
                EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);
                conexao.AdicionarParamentros("@lado", lado);

                //String de consulta
                string select = "select b.est_codigo, a.apa_codigo, apa_endereco, apa_tamanho_palete, apa_status, apa_disponivel, " +
                "p.prod_id, prod_codigo, prod_descricao, trunc(s.arm_quantidade/ p.prod_fator_pulmao) as arm_quantidade, uni_unidade, s.arm_vencimento, s.arm_peso, " +
                 "s.arm_lote, " +
                 "re.reg_numero, ru.rua_numero, b.bloc_numero, n.niv_numero, a.apa_numero, apa_ordem,  " +
                 "(select conf_empresa from wms_configuracao where conf_sigla = @empresa ) as conf_empresa " +
                               "from wms_apartamento a " +
                               "inner join wms_nivel n " +
                               "on n.niv_codigo = a.niv_codigo " +
                               "inner join wms_bloco b " +
                               "on b.bloc_codigo = n.bloc_codigo " +
                               "inner join wms_rua ru " +
                               "on ru.rua_codigo = b.rua_codigo " +
                               "inner join wms_regiao re " +
                               "on re.reg_codigo = ru.reg_codigo and re.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                               "left join wms_armazenagem s " +
                               "on s.apa_codigo = a.apa_codigo " +
                               "left join wms_produto p " +
                               "on p.prod_id = s.prod_id and s.conf_codigo = p.conf_codigo " +
                               "left join wms_unidade u " +
                               "on u.uni_codigo = p.uni_codigo_pulmao " +
                               "where apa_tipo = 'Pulmao' and re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua " +
                               "and re.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                if (!numeroBloco.Equals(""))
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                if (!numeroNivel.Equals(""))
                {
                    select += " and niv_numero = @numeroNivel ";
                }

                if (status.Equals("VAGO"))
                {
                    select += " and a.apa_status = 'Vago' ";
                }
                else if (status.Equals("OCUPADO"))
                {
                    select += " and a.apa_status = 'Ocupado' ";
                }

                if (disponibilidade.Equals("SIM"))
                {
                    select += " and a.apa_disponivel = 'Sim' ";
                }
                else if (disponibilidade.Equals("NÃO"))
                {
                    select += " and a.apa_disponivel = 'Nao' ";
                }

                if (!(lado.Equals("") || lado.Equals("Todos")))
                {
                    select += " and b.bloc_lado =  @lado ";
                }

                select += " order by apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    EnderecoPicking endereco = new EnderecoPicking();

                    if (linha["est_codigo"] != DBNull.Value)
                    {
                        endereco.codEstacao = Convert.ToInt32(linha["est_codigo"]);
                    }

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        endereco.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        endereco.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["arm_quantidade"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToInt32(linha["arm_quantidade"]);
                    }

                    if (linha["uni_unidade"] != DBNull.Value)
                    {
                        endereco.unidadeEstoque = Convert.ToString(linha["uni_unidade"]);
                    }

                    if (linha["arm_vencimento"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToDateTime(linha["arm_vencimento"]);
                    }

                    if (linha["arm_peso"] != DBNull.Value)
                    {
                        endereco.peso = Convert.ToDouble(linha["arm_peso"]);
                    }

                    if (linha["arm_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["arm_lote"]);
                    }

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        endereco.paleteEndereco = Convert.ToString(linha["apa_tamanho_palete"]);
                    }

                    if (linha["apa_status"] != DBNull.Value)
                    {
                        endereco.apStatus = Convert.ToString(linha["apa_status"]);
                    }

                    if (linha["apa_disponivel"] != DBNull.Value)
                    {
                        endereco.apDisponibilidade = Convert.ToString(linha["apa_disponivel"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        endereco.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        endereco.numeroRua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        endereco.numeroBloco = Convert.ToInt32(linha["bloc_numero"]);
                    }

                    if (linha["niv_numero"] != DBNull.Value)
                    {
                        endereco.numeroNivel = Convert.ToInt32(linha["niv_numero"]);
                    }

                    if (linha["apa_numero"] != DBNull.Value)
                    {
                        endereco.numeroApartamento = Convert.ToInt32(linha["apa_numero"]);
                    }

                    if (linha["apa_ordem"] != DBNull.Value)
                    {
                        endereco.ordemEndereco = Convert.ToString(linha["apa_ordem"]);
                    }

                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        endereco.nomeEmpresa = Convert.ToString(linha["conf_empresa"]);
                    }



                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereco \nDetalhes: " + ex.Message);
            }
        }

        public EnderecoPickingCollection PesqEnderecoVolume(int numeroRegiao, int numeroRua, string numeroBloco, string numeroNivel, string status, string disponibilidade, string lado)
        {
            try
            {
                //Instânicia a camada de objêto
                EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);
                conexao.AdicionarParamentros("@lado", lado);

                //String de consulta
                string select = "select a.apa_codigo, apa_endereco, apa_tamanho_palete, apa_status, apa_disponivel, "+
                "re.reg_numero, ru.rua_numero, b.bloc_numero, n.niv_numero, a.apa_numero, apa_ordem, "+
                "(select conf_empresa from wms_configuracao where conf_codigo = 1 ) as conf_empresa " +
                               "from wms_apartamento a " +
                               "inner join wms_nivel n " +
                               "on n.niv_codigo = a.niv_codigo " +
                               "inner join wms_bloco b " +
                               "on b.bloc_codigo = n.bloc_codigo " +
                               "inner join wms_rua ru " +
                               "on ru.rua_codigo = b.rua_codigo " +
                               "inner join wms_regiao re " +
                               "on re.reg_codigo = ru.reg_codigo " +
                               "where apa_tipo = 'Volume' and re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua ";

                if (!numeroBloco.Equals(""))
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                if (!numeroNivel.Equals(""))
                {
                    select += " and niv_numero = @numeroNivel ";
                }

                if (status.Equals("VAGO"))
                {
                    select += " and a.apa_status = 'Vago' ";
                }
                else if (status.Equals("OCUPADO"))
                {
                    select += " and a.apa_status = 'Ocupado' ";
                }

                if (disponibilidade.Equals("SIM"))
                {
                    select += " and a.apa_disponivel = 'Sim' ";
                }
                else if (disponibilidade.Equals("NÃO"))
                {
                    select += " and a.apa_disponivel = 'Nao' ";
                }

                if (!(lado.Equals("") || lado.Equals("Todos")))
                {
                    select += " and b.bloc_lado =  @lado ";
                }

                select += " order by apa_ordem";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    EnderecoPicking endereco = new EnderecoPicking();

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }                    

                    if (linha["apa_tamanho_palete"] != DBNull.Value)
                    {
                        endereco.paleteEndereco = Convert.ToString(linha["apa_tamanho_palete"]);
                    }

                    if (linha["apa_status"] != DBNull.Value)
                    {
                        endereco.apStatus = Convert.ToString(linha["apa_status"]);
                    }

                    if (linha["apa_disponivel"] != DBNull.Value)
                    {
                        endereco.apDisponibilidade = Convert.ToString(linha["apa_disponivel"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        endereco.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        endereco.numeroRua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        endereco.numeroBloco = Convert.ToInt32(linha["bloc_numero"]);
                    }

                    if (linha["niv_numero"] != DBNull.Value)
                    {
                        endereco.numeroNivel = Convert.ToInt32(linha["niv_numero"]);
                    }

                    if (linha["apa_numero"] != DBNull.Value)
                    {
                        endereco.numeroApartamento = Convert.ToInt32(linha["apa_numero"]);
                    }

                    if (linha["apa_ordem"] != DBNull.Value)
                    {
                        endereco.ordemEndereco = Convert.ToString(linha["apa_ordem"]);
                    }

                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        endereco.nomeEmpresa = Convert.ToString(linha["conf_empresa"]);
                    }

                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereco \nDetalhes: " + ex.Message);
            }
        }


        //Pesquisa a quantidade de blocos para o Depara
        public EnderecoPickingCollection PesqQuantidadeBlocoDePara(int numeroRegiao, int numeroRua, int? numeroBloco, int numeroNivel)
        {
            try
            {
                //Instânicia a camada de objêto
                EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);

                //String de consulta
                string select = "select max(bloc_numero) as cont_bloco from wms_apartamento a " +
                               "inner join wms_nivel n " +
                               "on n.niv_codigo = a.niv_codigo " +
                               "inner join wms_bloco b " +
                               "on b.bloc_codigo = n.bloc_codigo " +
                               "inner join wms_rua ru " +
                               "on ru.rua_codigo = b.rua_codigo " +
                               "inner join wms_regiao re " +
                               "on re.reg_codigo = ru.reg_codigo " +
                               "where re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua and niv_numero = @numeroNivel " +
                               "and a.apa_disponivel = 'Sim' ";

                if (numeroBloco > 0)
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    EnderecoPicking endereco = new EnderecoPicking();

                    if (linha["cont_bloco"] != DBNull.Value)
                    {
                        endereco.numeroBloco = Convert.ToInt32(linha["cont_bloco"]);
                    }

                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os blocos para o Depara \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa a quantidade de apartamentos para o Depara
        public EnderecoPickingCollection PesqQuantidadeApartamentoDePara(int numeroRegiao, int numeroRua, int? numeroBloco, int numeroNivel)
        {
            try
            {
                //Instânicia a camada de objêto
                EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);

                //String de consulta
                string select = "select bloc_numero, count(apa_codigo) as cont_apa from wms_apartamento a " +
                               "inner join wms_nivel n " +
                               "on n.niv_codigo = a.niv_codigo " +
                               "inner join wms_bloco b " +
                               "on b.bloc_codigo = n.bloc_codigo " +
                               "inner join wms_rua ru " +
                               "on ru.rua_codigo = b.rua_codigo " +
                               "inner join wms_regiao re " +
                               "on re.reg_codigo = ru.reg_codigo " +
                               "where re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua and niv_numero = @numeroNivel " +
                               "and a.apa_disponivel = 'Sim' ";

                if (numeroBloco > 0)
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                select += " group by bloc_numero";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    EnderecoPicking endereco = new EnderecoPicking();

                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        endereco.numeroBloco = Convert.ToInt32(linha["bloc_numero"]);
                    }

                    if (linha["cont_apa"] != DBNull.Value)
                    {
                        endereco.numeroApartamento = Convert.ToInt32(linha["cont_apa"]);
                    }

                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os apartamentos para o Depara \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa a quantidade de apartamentos para o Depara
        public EnderecoPickingCollection PesqProdutoDePara(int numeroRegiao, int numeroRua, int? numeroBloco, int numeroNivel)
        {
            try
            {
                //Instânicia a camada de objêto
                EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);

                //String de consulta
                string select = "select b.bloc_numero, a.apa_numero, s.apa_codigo, p.prod_codigo, prod_descricao, s.sep_estoque, s.sep_validade, s.sep_peso, s.sep_lote, " +
                                "s.sep_capacidade, s.sep_abastecimento, s.sep_tipo from wms_separacao s " +
                                "inner join  wms_apartamento a " +
                                "on s.apa_codigo = a.apa_codigo " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = a.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "inner join wms_rua ru " +
                                "on ru.rua_codigo = b.rua_codigo " +
                                "inner join wms_regiao re " +
                                "on re.reg_codigo = ru.reg_codigo " +
                                "inner join wms_produto p " +
                               "on p.prod_id = s.prod_id " +
                                "where re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua and niv_numero = @numeroNivel " +
                                "and sep_tipo = 'CAIXA' ";

                if (numeroBloco > 0)
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    EnderecoPicking endereco = new EnderecoPicking();

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        endereco.numeroBloco = Convert.ToInt32(linha["bloc_numero"]);
                    }

                    if (linha["apa_numero"] != DBNull.Value)
                    {
                        endereco.numeroApartamento = Convert.ToInt32(linha["apa_numero"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        endereco.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["sep_estoque"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToInt32(linha["sep_estoque"]);
                    }

                    if (linha["sep_validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToDateTime(linha["sep_validade"]);
                    }

                    if (linha["sep_peso"] != DBNull.Value)
                    {
                        endereco.peso = Convert.ToDouble(linha["sep_peso"]);
                    }

                    if (linha["sep_lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["sep_lote"]);
                    }

                    if (linha["sep_capacidade"] != DBNull.Value)
                    {
                        endereco.capacidade = Convert.ToInt32(linha["sep_capacidade"]);
                    }

                    if (linha["sep_abastecimento"] != DBNull.Value)
                    {
                        endereco.abastecimento = Convert.ToInt32(linha["sep_abastecimento"]);
                    }

                    if (linha["sep_tipo"] != DBNull.Value)
                    {
                        endereco.tipoEndereco = Convert.ToString(linha["sep_tipo"]);
                    }

                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os produtos para o Depara \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa a quantidade de apartamentos para o Depara
        public int PesqApartamentoDePara(int numeroRegiao, int numeroRua, int numeroBloco, int numeroNivel, int numeroApartamento)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);
                conexao.AdicionarParamentros("@numeroApartamento", numeroApartamento);

                //String de consulta
                string select = "select apa_codigo from wms_apartamento a " +
                               "inner join wms_nivel n " +
                               "on n.niv_codigo = a.niv_codigo " +
                               "inner join wms_bloco b " +
                               "on b.bloc_codigo = n.bloc_codigo " +
                               "inner join wms_rua ru " +
                               "on ru.rua_codigo = b.rua_codigo " +
                               "inner join wms_regiao re " +
                               "on re.reg_codigo = ru.reg_codigo " +
                               "where re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua and b.bloc_numero = @numeroBloco " +
                               "and niv_numero = @numeroNivel and apa_numero = @numeroApartamento";

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                int codApartamento = 0;

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }
                }

                return codApartamento;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o código do apartamento para o endereçamento do Depara \nDetalhes: " + ex.Message);
            }
        }


        //Endereça o produto na área de separação e altera o status do endereço
        public void EnderecarProdutoSeparacao(int? codEstacao, int codEndereco, string codProduto, int? estoque, DateTime? validade, int? capacidade, int? abastecimento, string lote, string tipo, int codUsuario, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codEstacao", codEstacao);
                conexao.AdicionarParamentros("@codEndereco", codEndereco);
                conexao.AdicionarParamentros("@codProduto", codProduto);
                conexao.AdicionarParamentros("@estoque", estoque);
                conexao.AdicionarParamentros("@validade", validade);
                conexao.AdicionarParamentros("@capacidade", capacidade);
                conexao.AdicionarParamentros("@abastecimento", abastecimento);
                conexao.AdicionarParamentros("@lote", lote);
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de insert
                string insert = "insert into wms_separacao (sep_codigo, est_codigo, apa_codigo, prod_id, sep_estoque, " +
                    "sep_validade, sep_capacidade, sep_abastecimento, sep_peso, sep_lote, sep_tipo, usu_codigo, conf_codigo) " +

                    "select gen_id(gen_wms_separacao, 1), @codEstacao, @codEndereco, prod_id, @estoque, @validade, @capacidade, " +
                    "@abastecimento, @estoque *(p.prod_peso/p.prod_fator_compra ), @lote, @tipo, @codUsuario, " +
                    "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                    "from wms_produto p " +
                    "where prod_codigo = @codProduto ";


                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                //String de atualização
                string update = "update wms_apartamento set apa_status = 'Ocupado' " +
                    "where apa_codigo = @codEndereco";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao endereçar o produto. \nDetalhes:" + ex.Message);
            }
        }

        //Atualiza a disponibilidade do endereço
        public void AtualizarDisponibilidadeEndereco(int codigo, string disponibilidade, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codigo", codigo);
                conexao.AdicionarParamentros("@disponibilidade", disponibilidade);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de atualização
                string update = "update wms_apartamento set apa_disponivel = 'Sim' " +
                    "where apa_codigo = @codigo and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao disponibilizar o endereço. \nDetalhes:" + ex.Message);
            }
        }

        //Deleta o endereço de separação e atualiza o status
        public void DeletaEnderecoSeparacao(int codEndereco, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                conexao.AdicionarParamentros("@codEndereco", codEndereco);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de atualização
                string delete = "delete from wms_armazenagem where apa_codigo = @codEndereco and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, delete);

                //String de atualização do status
                string update = "update wms_apartamento set apa_status = 'Vago' " +
                    "where apa_codigo = @codEndereco or conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);


            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao deletar o endereço. \nDetalhes:" + ex.Message);
            }
        }


        //Registra no rastreamento
        public void InserirRastreamento(string empresa, string operacao, int codUsuario, int idProduto,
            int? codApartamentoOrigem, int? quantidadeOrigem, string vencimentoOrigem, double? pesoOrigem, string loteOrigem,
            int? codApartamentoDestino, int? quantidadeDestino, string vencimentoDestino, double? pesoDestino, string loteDestino, string tipo)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@operacao", operacao);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@idProduto", idProduto);
                conexao.AdicionarParamentros("@codApartamentoOrigem", codApartamentoOrigem);
                conexao.AdicionarParamentros("@quantidadeOrigem", quantidadeOrigem);
                conexao.AdicionarParamentros("@pesoOrigem", pesoOrigem);
                conexao.AdicionarParamentros("@vencimentoOrigem", vencimentoOrigem);
                conexao.AdicionarParamentros("@loteOrigem", loteOrigem);
                conexao.AdicionarParamentros("@codApartamentoDestino", codApartamentoDestino);
                conexao.AdicionarParamentros("@quantidadeDestino", quantidadeDestino);
                conexao.AdicionarParamentros("@pesoDestino", pesoDestino);
                conexao.AdicionarParamentros("@vencimentoDestino", vencimentoDestino);
                conexao.AdicionarParamentros("@loteDestino", loteDestino);
                conexao.AdicionarParamentros("@tipo", tipo);

                //String de insert - insere o endereço  
                string insert = "insert into wms_rastreamento_armazenagem (rast_codigo, rast_operacao, rast_data, usu_codigo, prod_id," +
                "apa_codigo_origem, arm_quantidade_origem, arm_peso_origem, arm_vencimento_origem, arm_lote_origem, " +
                "apa_codigo_destino, arm_quantidade_destino, arm_peso_destino, arm_vencimento_destino, arm_lote_destino, arm_tipo, conf_codigo)" +
                "values" +
                "(gen_id(gen_wms_rast_armazenagem, 1), @operacao, current_timestamp, @codUsuario, @idProduto, " +
                "@codApartamentoOrigem, @quantidadeOrigem, @pesoOrigem, @vencimentoOrigem, @loteOrigem," +
                "@codApartamentoDestino, @quantidadeDestino, @pesoDestino, @vencimentoDestino, @loteDestino, @tipo, (select conf_codigo from wms_configuracao where conf_sigla = @empresa))";


                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a operação no rastreamento de operações! \nDetalhes: " + ex.Message);
            }
        }


        //Pesquisa o endereço por regiao, rua, bloco, nivel
        public ObjetoTransferencia.Impressao.EnderecoCollection PesqRelatorioEndereco( string empresa, string tipoEndereco, int numeroRegiao, int numeroRua, string numeroBloco, string numeroNivel, string status, string disponibilidade, string lado)
        {
            try
            {
                //Instânicia a camada de objêto
                ObjetoTransferencia.Impressao.EnderecoCollection enderecoCollection = new ObjetoTransferencia.Impressao.EnderecoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);
                conexao.AdicionarParamentros("@lado", lado);

                string select = null;

                //String de consulta
                if (tipoEndereco.Equals("TODOS") || tipoEndereco.Equals("PICKING"))
                {
                    select = "select ai.apa_codigo, apa_endereco, re.reg_numero, re.reg_tipo, ru.rua_numero, b.bloc_numero, " +
                                "n.niv_numero, replace(replace(b.bloc_lado, 'Par', 'PAR'), 'Impar', 'IMPAR') as bloc_lado, apa_ordem, " +
                                "p.prod_id, prod_codigo, prod_descricao, coalesce(s.sep_estoque, 0) as quantidade, " +
                                "trunc(s.sep_estoque / p.prod_fator_pulmao) as qtd_pulmao , u.uni_unidade as uni_pulmao, " +
                                "mod(s.sep_estoque, p.prod_fator_pulmao) as qtd_picking, uu.uni_unidade as uni_picking, " +
                                "s.sep_validade as validade, s.sep_peso as peso, s.sep_lote as lote, " +
                                "(case sep_tipo " +
                                "when 'CAIXA' then 'CAIXA' " +
                                "when 'FLOWRACK' then 'FLOWRACK' " +
                                "else " +
                                "'PICKING' " +
                                "end) as tipo_endereco, prod_peso_variavel, sep_bloqueado as bloqueado, " +
                                "sep_motivo_bloqueio as motivo_bloqueio, " +
                                "(select conf_empresa from wms_configuracao where conf_empresa = @empresa ) as conf_empresa " +
                               "from wms_apartamento ai " +
                               "inner join wms_nivel n " +
                               "on n.niv_codigo = a.niv_codigo " +
                               "inner join wms_bloco b " +
                               "on b.bloc_codigo = n.bloc_codigo " +
                               "inner join wms_rua ru " +
                               "on ru.rua_codigo = b.rua_codigo " +
                               "inner join wms_regiao re " +
                               "on re.reg_codigo = ru.reg_codigo " +
                               "left join wms_separacao s " +
                               "on s.apa_codigo = ai.apa_codigo " +
                               "left join wms_produto p " +
                               "on p.prod_id = s.prod_id " +
                               "left join wms_unidade u " +
                               "on u.uni_codigo = p.uni_codigo_pulmao " +
                               "left join wms_unidade uu " +
                                "on uu.uni_codigo = p.uni_codigo_picking " +
                               "where apa_tipo = 'Separacao' and re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua  ";

                    if (!numeroBloco.Equals(""))
                    {
                        select += " and b.bloc_numero = @numeroBloco ";
                    }

                    if (!numeroNivel.Equals(""))
                    {
                        select += " and niv_numero = @numeroNivel ";
                    }

                    if (status.Equals("VAGO"))
                    {
                        select += " and ai.apa_status = 'Vago' ";
                    }
                    else if (status.Equals("OCUPADO"))
                    {
                        select += " and ai.apa_status = 'Ocupado' ";
                    }

                    if (disponibilidade.Equals("SIM"))
                    {
                        select += " and ai.apa_disponivel = 'Sim' ";
                    }
                    else if (disponibilidade.Equals("NÃO"))
                    {
                        select += " and ai.apa_disponivel = 'Nao' ";
                    }

                    if (!lado.Equals(""))
                    {

                        if (!lado.Equals("Todos"))
                        {
                            select += " and b.bloc_lado =  @lado ";
                        }
                    }
                }

                if (tipoEndereco.Equals("TODOS"))
                {
                    select += "union ";
                }
                //******************************************
                if (tipoEndereco.Equals("TODOS") || tipoEndereco.Equals("PULMAO"))
                {
                    select += "select a.apa_codigo, apa_endereco, re.reg_numero, re.reg_tipo, ru.rua_numero, b.bloc_numero, " +
                                   "n.niv_numero, replace(replace(b.bloc_lado, 'Par', 'PAR'), 'Impar', 'IMPAR') as bloc_lado, apa_ordem, " +
                                   "p.prod_id, prod_codigo, prod_descricao, arm_quantidade as quantidade, " +
                                    "trunc(arm_quantidade / p.prod_fator_pulmao) as qtd_pulmao , u.uni_unidade as uni_pulmao, " +
                                    "mod(arm_quantidade, p.prod_fator_pulmao) as qtd_picking, uu.uni_unidade as uni_picking, " +
                                   "s.arm_vencimento as validade, s.arm_peso as peso, s.arm_lote as lote, replace(a.apa_tipo, 'Pulmao', 'PULMAO') as tipo_endereco, " +
                                   "prod_peso_variavel, arm_bloqueado as bloqueado,  arm_motivo_bloqueio as motivo_bloqueio, " +
                                   "(select conf_empresa from wms_configuracao where conf_empresa = @empresa) as conf_empresa " +
                                   "from wms_apartamento a " +
                                   "inner join wms_nivel n " +
                                   "on n.niv_codigo = a.niv_codigo " +
                                   "inner join wms_bloco b " +
                                   "on b.bloc_codigo = n.bloc_codigo " +
                                   "inner join wms_rua ru " +
                                   "on ru.rua_codigo = b.rua_codigo " +
                                   "inner join wms_regiao re " +
                                   "on re.reg_codigo = ru.reg_codigo " +
                                   "left join wms_armazenagem s " +
                                   "on s.apa_codigo = a.apa_codigo " +
                                   "left join wms_produto p " +
                                   "on p.prod_id = s.prod_id " +
                                   "left join wms_unidade u " +
                                   "on u.uni_codigo = p.uni_codigo_pulmao " +
                                   "left join wms_unidade uu " +
                                   "on uu.uni_codigo = p.uni_codigo_picking " +
                                   "where apa_tipo = 'Pulmao' and re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua ";
                }

                if (!numeroBloco.Equals(""))
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                if (!numeroNivel.Equals(""))
                {
                    select += " and niv_numero = @numeroNivel ";
                }

                if (status.Equals("VAGO"))
                {
                    select += " and a.apa_status = 'Vago' ";
                }
                else if (status.Equals("OCUPADO"))
                {
                    select += " and a.apa_status = 'Ocupado' ";
                }

                if (disponibilidade.Equals("SIM"))
                {
                    select += " and a.apa_disponivel = 'Sim' ";
                }
                else if (disponibilidade.Equals("NÃO"))
                {
                    select += " and a.apa_disponivel = 'Nao' ";
                }

                if (!lado.Equals(""))
                {

                    if (!lado.Equals("Todos"))
                    {
                        select += " and b.bloc_lado =  @lado ";
                    }
                }

                select += " order by apa_ordem";


                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    Endereco endereco = new Endereco();

                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        endereco.empresa = Convert.ToString(linha["conf_empresa"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        endereco.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        endereco.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        endereco.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (!numeroBloco.Equals(""))
                    {
                        endereco.Bloco = Convert.ToInt32(numeroBloco);
                    }
                    else
                    {
                        endereco.Bloco = 0;
                    }

                    if (!lado.Equals(""))
                    {
                        endereco.lado = Convert.ToString(lado.ToUpper());
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (!tipoEndereco.Equals(""))
                    {
                        endereco.tipoEndereco = Convert.ToString(tipoEndereco.ToUpper());
                    }


                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.produto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        endereco.pesoVariavel = Convert.ToString(linha["prod_peso_variavel"]);
                    }
                    else
                    {
                        endereco.pesoVariavel = "False";
                    }


                    if (linha["quantidade"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToDouble(linha["quantidade"]);
                    }

                    if (linha["qtd_pulmao"] != DBNull.Value)
                    {
                        endereco.estoqueCaixa = Convert.ToDouble(linha["qtd_pulmao"]);
                    }

                    if (linha["qtd_picking"] != DBNull.Value)
                    {
                        endereco.estoqueUnidade = Convert.ToDouble(linha["qtd_picking"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        endereco.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        endereco.unidadePicking = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToString(linha["validade"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        endereco.peso = string.Format("{0:n}", linha["peso"]);
                    }

                    if (linha["lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["lote"]);
                    }

                    if (linha["bloqueado"] != DBNull.Value)
                    {
                        endereco.bloqueio = Convert.ToString(linha["bloqueado"]);
                    }
                    /*
                    if (linha["motivo_bloqueio"] != DBNull.Value)
                    {
                        endereco.motivoBloqueio = Convert.ToString(linha["motivo_bloqueio"]);
                    }*/







                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereco \nDetalhes: " + ex.Message);
            }
        }

        public ObjetoTransferencia.Impressao.EnderecoCollection PesqRelatorioEnderecoPicking(string empresa, string tipoEndereco, int numeroRegiao, int numeroRua, string numeroBloco, string numeroNivel, string status, string disponibilidade, string lado)
        {
            try
            {
                //Instânicia a camada de objêto
                ObjetoTransferencia.Impressao.EnderecoCollection enderecoCollection = new ObjetoTransferencia.Impressao.EnderecoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);
                conexao.AdicionarParamentros("@lado", lado);

                string select = null;

                //String de consulta
                
                    select = "select a.apa_codigo, apa_endereco, re.reg_numero, re.reg_tipo, ru.rua_numero, b.bloc_numero, " +
                                "n.niv_numero, replace(replace(b.bloc_lado, 'Par', 'PAR'), 'Impar', 'IMPAR') as bloc_lado, apa_ordem, " +
                                "p.prod_id, prod_codigo, prod_descricao, coalesce(s.sep_estoque, 0) as quantidade, " +
                                "trunc(s.sep_estoque / p.prod_fator_pulmao) as qtd_pulmao , u.uni_unidade as uni_pulmao, " +
                                "mod(s.sep_estoque, p.prod_fator_pulmao) as qtd_picking, uu.uni_unidade as uni_picking, " +
                                "s.sep_validade as validade, s.sep_peso as peso, s.sep_lote as lote, " +
                                "(case sep_tipo " +
                                "when 'CAIXA' then 'CAIXA' " +
                                "when 'FLOWRACK' then 'FLOWRACK' " +
                                "else " +
                                "'PICKING' " +
                                "end) as tipo_endereco, prod_peso_variavel, sep_bloqueado as bloqueado, " +
                                "sep_motivo_bloqueio as motivo_bloqueio, " +
                                "(select conf_empresa from wms_configuracao where conf_empresa = @empresa ) as conf_empresa " +
                               "from wms_apartamento a " +
                               "inner join wms_nivel n " +
                               "on n.niv_codigo = a.niv_codigo " +
                               "inner join wms_bloco b " +
                               "on b.bloc_codigo = n.bloc_codigo " +
                               "inner join wms_rua ru " +
                               "on ru.rua_codigo = b.rua_codigo " +
                               "inner join wms_regiao re " +
                               "on re.reg_codigo = ru.reg_codigo " +
                               "left join wms_separacao s " +
                               "on s.apa_codigo = a.apa_codigo " +
                               "left join wms_produto p " +
                               "on p.prod_id = s.prod_id and p.conf_codigo = s.conf_codigo " +
                               "left join wms_unidade u " +
                               "on u.uni_codigo = p.uni_codigo_pulmao " +
                               "left join wms_unidade uu " +
                                "on uu.uni_codigo = p.uni_codigo_picking " +
                               "where apa_tipo = 'Separacao' and re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua  " +
                               "and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                    if (!numeroBloco.Equals(""))
                    {
                        select += " and b.bloc_numero = @numeroBloco ";
                    }

                    if (!numeroNivel.Equals(""))
                    {
                        select += " and niv_numero = @numeroNivel ";
                    }

                    if (status.Equals("VAGO"))
                    {
                        select += " and a.apa_status = 'Vago' ";
                    }
                    else if (status.Equals("OCUPADO"))
                    {
                        select += " and a.apa_status = 'Ocupado' ";
                    }

                    if (disponibilidade.Equals("SIM"))
                    {
                        select += " and a.apa_disponivel = 'Sim' ";
                    }
                    else if (disponibilidade.Equals("NÃO"))
                    {
                        select += " and a.apa_disponivel = 'Nao' ";
                    }

                    if (!lado.Equals(""))
                    {

                        if (!lado.Equals("Todos"))
                        {
                            select += " and b.bloc_lado =  @lado ";
                        }
                    }
                

           

                if (!numeroBloco.Equals(""))
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                if (!numeroNivel.Equals(""))
                {
                    select += " and niv_numero = @numeroNivel ";
                }

                if (status.Equals("VAGO"))
                {
                    select += " and a.apa_status = 'Vago' ";
                }
                else if (status.Equals("OCUPADO"))
                {
                    select += " and a.apa_status = 'Ocupado' ";
                }

                if (disponibilidade.Equals("SIM"))
                {
                    select += " and a.apa_disponivel = 'Sim' ";
                }
                else if (disponibilidade.Equals("NÃO"))
                {
                    select += " and a.apa_disponivel = 'Nao' ";
                }

                if (!lado.Equals(""))
                {

                    if (!lado.Equals("Todos"))
                    {
                        select += " and b.bloc_lado =  @lado ";
                    }
                }

                select += " order by apa_ordem";


                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    Endereco endereco = new Endereco();

                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        endereco.empresa = Convert.ToString(linha["conf_empresa"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        endereco.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        endereco.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        endereco.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (!numeroBloco.Equals(""))
                    {
                        endereco.Bloco = Convert.ToInt32(numeroBloco);
                    }
                    else
                    {
                        endereco.Bloco = 0;
                    }

                    if (!lado.Equals(""))
                    {
                        endereco.lado = Convert.ToString(lado.ToUpper());
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (!tipoEndereco.Equals(""))
                    {
                        endereco.tipoEndereco = Convert.ToString(tipoEndereco.ToUpper());
                    }


                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.produto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        endereco.pesoVariavel = Convert.ToString(linha["prod_peso_variavel"]);
                    }
                    else
                    {
                        endereco.pesoVariavel = "False";
                    }


                    if (linha["quantidade"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToDouble(linha["quantidade"]);
                    }

                    if (linha["qtd_pulmao"] != DBNull.Value)
                    {
                        endereco.estoqueCaixa = Convert.ToDouble(linha["qtd_pulmao"]);
                    }

                    if (linha["qtd_picking"] != DBNull.Value)
                    {
                        endereco.estoqueUnidade = Convert.ToDouble(linha["qtd_picking"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        endereco.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        endereco.unidadePicking = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToString(linha["validade"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        endereco.peso = string.Format("{0:n}", linha["peso"]);
                    }

                    if (linha["lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["lote"]);
                    }

                    if (linha["bloqueado"] != DBNull.Value)
                    {
                        endereco.bloqueio = Convert.ToString(linha["bloqueado"]);
                    }
                    /*
                    if (linha["motivo_bloqueio"] != DBNull.Value)
                    {
                        endereco.motivoBloqueio = Convert.ToString(linha["motivo_bloqueio"]);
                    }*/







                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereco \nDetalhes: " + ex.Message);
            }
        }

        public ObjetoTransferencia.Impressao.EnderecoCollection PesqRelatorioEnderecoPulmao(string empresa, string tipoEndereco, int numeroRegiao, int numeroRua, string numeroBloco, string numeroNivel, string status, string disponibilidade, string lado)
        {
            try
            {
                //Instânicia a camada de objêto
                ObjetoTransferencia.Impressao.EnderecoCollection enderecoCollection = new ObjetoTransferencia.Impressao.EnderecoCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //Adiciona parêmaetros
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@numeroRegiao", numeroRegiao);
                conexao.AdicionarParamentros("@numeroRua", numeroRua);
                conexao.AdicionarParamentros("@numeroBloco", numeroBloco);
                conexao.AdicionarParamentros("@numeroNivel", numeroNivel);
                conexao.AdicionarParamentros("@lado", lado);

                string select = null;

                //******************************************

                select += "select a.apa_codigo, apa_endereco, re.reg_numero, re.reg_tipo, ru.rua_numero, b.bloc_numero, " +
                               "n.niv_numero, replace(replace(b.bloc_lado, 'Par', 'PAR'), 'Impar', 'IMPAR') as bloc_lado, apa_ordem, " +
                               "p.prod_id, prod_codigo, prod_descricao, arm_quantidade as quantidade, " +
                                "trunc(arm_quantidade / p.prod_fator_pulmao) as qtd_pulmao , u.uni_unidade as uni_pulmao, " +
                                "mod(arm_quantidade, p.prod_fator_pulmao) as qtd_picking, uu.uni_unidade as uni_picking, " +
                               "s.arm_vencimento as validade, s.arm_peso as peso, s.arm_lote as lote, replace(a.apa_tipo, 'Pulmao', 'PULMAO') as tipo_endereco, " +
                               "prod_peso_variavel, arm_bloqueado as bloqueado,  arm_motivo_bloqueio as motivo_bloqueio, " +
                               "(select conf_empresa from wms_configuracao where conf_empresa = @empresa) as conf_empresa " +
                               "from wms_apartamento a " +
                               "inner join wms_nivel n " +
                               "on n.niv_codigo = a.niv_codigo " +
                               "inner join wms_bloco b " +
                               "on b.bloc_codigo = n.bloc_codigo " +
                               "inner join wms_rua ru " +
                               "on ru.rua_codigo = b.rua_codigo " +
                               "inner join wms_regiao re " +
                               "on re.reg_codigo = ru.reg_codigo " +
                               "left join wms_armazenagem s " +
                               "on s.apa_codigo = a.apa_codigo " +
                               "left join wms_produto p " +
                               "on p.prod_id = s.prod_id and p.conf_codigo = s.conf_codigo " +
                               "left join wms_unidade u " +
                               "on u.uni_codigo = p.uni_codigo_pulmao " +
                               "left join wms_unidade uu " +
                               "on uu.uni_codigo = p.uni_codigo_picking " +
                               "where apa_tipo = 'Pulmao' and re.reg_numero = @numeroRegiao and ru.rua_numero = @numeroRua ";// +
                                   //"and s.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                

                if (!numeroBloco.Equals(""))
                {
                    select += " and b.bloc_numero = @numeroBloco ";
                }

                if (!numeroNivel.Equals(""))
                {
                    select += " and niv_numero = @numeroNivel ";
                }

                if (status.Equals("VAGO"))
                {
                    select += " and a.apa_status = 'Vago' ";
                }
                else if (status.Equals("OCUPADO"))
                {
                    select += " and a.apa_status = 'Ocupado' ";
                }

                if (disponibilidade.Equals("SIM"))
                {
                    select += " and a.apa_disponivel = 'Sim' ";
                }
                else if (disponibilidade.Equals("NÃO"))
                {
                    select += " and a.apa_disponivel = 'Nao' ";
                }

                if (!lado.Equals(""))
                {

                    if (!lado.Equals("Todos"))
                    {
                        select += " and b.bloc_lado =  @lado ";
                    }
                }

                select += " order by apa_ordem";


                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os dados
                    Endereco endereco = new Endereco();

                    if (linha["conf_empresa"] != DBNull.Value)
                    {
                        endereco.empresa = Convert.ToString(linha["conf_empresa"]);
                    }

                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        endereco.regiao = Convert.ToInt32(linha["reg_numero"]);
                    }

                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        endereco.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    if (linha["rua_numero"] != DBNull.Value)
                    {
                        endereco.rua = Convert.ToInt32(linha["rua_numero"]);
                    }

                    if (!numeroBloco.Equals(""))
                    {
                        endereco.Bloco = Convert.ToInt32(numeroBloco);
                    }
                    else
                    {
                        endereco.Bloco = 0;
                    }

                    if (!lado.Equals(""))
                    {
                        endereco.lado = Convert.ToString(lado.ToUpper());
                    }

                    if (linha["apa_endereco"] != DBNull.Value)
                    {
                        endereco.endereco = Convert.ToString(linha["apa_endereco"]);
                    }

                    if (!tipoEndereco.Equals(""))
                    {
                        endereco.tipoEndereco = Convert.ToString(tipoEndereco.ToUpper());
                    }


                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        endereco.produto = Convert.ToString(linha["prod_codigo"] + " - " + linha["prod_descricao"]);
                    }

                    if (linha["prod_peso_variavel"] != DBNull.Value)
                    {
                        endereco.pesoVariavel = Convert.ToString(linha["prod_peso_variavel"]);
                    }
                    else
                    {
                        endereco.pesoVariavel = "False";
                    }


                    if (linha["quantidade"] != DBNull.Value)
                    {
                        endereco.estoque = Convert.ToDouble(linha["quantidade"]);
                    }

                    if (linha["qtd_pulmao"] != DBNull.Value)
                    {
                        endereco.estoqueCaixa = Convert.ToDouble(linha["qtd_pulmao"]);
                    }

                    if (linha["qtd_picking"] != DBNull.Value)
                    {
                        endereco.estoqueUnidade = Convert.ToDouble(linha["qtd_picking"]);
                    }

                    if (linha["uni_pulmao"] != DBNull.Value)
                    {
                        endereco.unidadePulmao = Convert.ToString(linha["uni_pulmao"]);
                    }

                    if (linha["uni_picking"] != DBNull.Value)
                    {
                        endereco.unidadePicking = Convert.ToString(linha["uni_picking"]);
                    }

                    if (linha["validade"] != DBNull.Value)
                    {
                        endereco.vencimento = Convert.ToString(linha["validade"]);
                    }

                    if (linha["peso"] != DBNull.Value)
                    {
                        endereco.peso = string.Format("{0:n}", linha["peso"]);
                    }

                    if (linha["lote"] != DBNull.Value)
                    {
                        endereco.lote = Convert.ToString(linha["lote"]);
                    }

                    if (linha["bloqueado"] != DBNull.Value)
                    {
                        endereco.bloqueio = Convert.ToString(linha["bloqueado"]);
                    }
                    /*
                    if (linha["motivo_bloqueio"] != DBNull.Value)
                    {
                        endereco.motivoBloqueio = Convert.ToString(linha["motivo_bloqueio"]);
                    }*/







                    enderecoCollection.Add(endereco);
                }

                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o endereco \nDetalhes: " + ex.Message);
            }
        }




    }
}
