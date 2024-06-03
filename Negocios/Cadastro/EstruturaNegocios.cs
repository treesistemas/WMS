using System;
using System.Data;

using Dados;
using ObjetoTransferencia;

namespace Negocios
{
    public class EstruturaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa o id da região
        public Estrutura pesqIdRegiao()
        {
            try
            {
                //Instância a coleção
                Estrutura gerarEndereco = new Estrutura();
                //String de consulta
                string select = "select gen_id(gen_wms_regiao,1) as id, max(reg_numero) as numero from wms_regiao";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    //Código da região
                    gerarEndereco.codRegiao = Convert.ToInt32(linha["id"]);
                    //Número d região
                    if (linha["numero"] != DBNull.Value)
                    {
                        gerarEndereco.numeroRegiao = Convert.ToInt32(linha["numero"]) + 1; 
                    }
                    else
                    {
                        gerarEndereco.numeroRegiao = 1;
                    }
                }

                //Retorna o id
                return gerarEndereco;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar uma nova região. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o id da rua
        public Estrutura PesqIdRua(int idRegiao)
        {
            try
            {
                //Instância a coleção
                Estrutura gerarEndereco = new Estrutura();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                //String de consulta
                string select = "select gen_id(gen_wms_rua,1) as id, max(rua_numero) as numero from wms_rua where reg_codigo = @idRegiao";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    gerarEndereco.codRua = Convert.ToInt32(linha["id"]);

                    if (linha["numero"] == DBNull.Value)
                    {
                        gerarEndereco.numeroRua = 1;
                    }
                    else
                    {
                        gerarEndereco.numeroRua = Convert.ToInt32(linha["numero"]) + 1;
                    }

                }

                //Retorna o id
                return gerarEndereco;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar uma nova rua. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o id do bloco
        public Estrutura PesqIdBloco(int idRua)
        {
            try
            {
                //Instância a coleção
                Estrutura gerarEndereco = new Estrutura();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idRua", idRua);
                //String de consulta
                string select = "select gen_id(gen_wms_bloco,1) as id, max(bloc_numero) as numero from wms_bloco where rua_codigo = @idRua";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    gerarEndereco.codBloco = Convert.ToInt32(linha["id"]);

                    if (linha["numero"] == DBNull.Value)
                    {
                        gerarEndereco.numeroBloco = 1;
                    }
                    else
                    {
                        gerarEndereco.numeroBloco = Convert.ToInt32(linha["numero"]) + 1;
                    }
                }

                //Retorna o id
                return gerarEndereco;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo bloco. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o id do nível
        public Estrutura PesqIdNivel(int idBloco)
        {
            try
            {
                //Instância a coleção
                Estrutura gerarEndereco = new Estrutura();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idBloco", idBloco);
                //String de consulta
                string select = "select gen_id(gen_wms_nivel,1) as id, max(niv_numero) as numero from wms_nivel where bloc_codigo = @idBloco";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    gerarEndereco.codNivel = Convert.ToInt32(linha["id"]);

                    if (linha["numero"] == DBNull.Value)
                    {
                        //Nível de separação
                        gerarEndereco.numeroNivel = 0;
                    }
                    else
                    {
                        gerarEndereco.numeroNivel = Convert.ToInt32(linha["numero"]) + 1;
                    }
                }

                //Retorna o id
                return gerarEndereco;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo nível. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o id do apartamento
        public Estrutura PesqIdApartamento(int idBloco, int idNivel, int nivel)
        {
            try
            {
                //Instância a coleção
                Estrutura gerarEndereco = new Estrutura();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idBloco", idBloco);
                conexao.AdicionarParamentros("@idNivel", idNivel);
                //String de consulta
                string select = null;

                if (nivel.Equals(0) || nivel.Equals(1))
                {
                    select = "select gen_id(gen_wms_apartamento,1) as id, max(apa_numero) as numero from wms_apartamento where niv_codigo = @idNivel";
                }
                else
                {
                    select = "select gen_id(gen_wms_apartamento,1) as id, max(apa_numero) as numero from wms_apartamento " +
                    "where apa_tipo = 'Pulmao' and niv_codigo =  " +
                    "(select max(niv_codigo) from wms_nivel n " +
                    "inner join wms_bloco b  " +
                    "on b.bloc_codigo = n.bloc_codigo  " +
                    "where n.bloc_codigo = @idBloco and n.niv_codigo != @idNivel )";
                }




                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    gerarEndereco.codApartamento = Convert.ToInt32(linha["id"]);

                    if (linha["numero"] == DBNull.Value)
                    {
                        //Nível de separação
                        gerarEndereco.numeroApartamento = 1;
                    }
                    else
                    {
                        gerarEndereco.numeroApartamento = Convert.ToInt32(linha["numero"]) + 1;
                    }
                }

                //Retorna o id
                return gerarEndereco;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo apartamento. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa região (Preenche os ComboBox e a TreeView)
        public EstruturaCollection PesqRegiao()
        {
            try
            {
                //Instância a coleção
                EstruturaCollection enderecoCollection = new EstruturaCollection();
                //Limpa a conexão
                conexao.LimparParametros();
                //String de consulta
                string select = string.Empty;
                    
                    select = "select reg_codigo, reg_numero, reg_tipo from wms_regiao order by reg_numero ";
                  
                    
                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe endereço
                    Estrutura endereco = new Estrutura();
                    //Adiciona o id do endereco
                    if (linha["reg_codigo"] != DBNull.Value)
                    {
                        endereco.codRegiao = Convert.ToInt32(linha["reg_codigo"]);
                    }
                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        //Adiciona o endereço
                        endereco.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }
                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        //Adiciona o tipo de região
                        endereco.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    //Adiciona a disponibilidade do endereço
                    enderecoCollection.Add(endereco);

                }
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a região. \nDetalhes: " + ex.Message);
            }
        }

        public EstruturaCollection PesqRegiao(string empresa)
        {
            try
            {
                //Instância a coleção
                EstruturaCollection enderecoCollection = new EstruturaCollection();
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@empresa", empresa);
                string select = string.Empty;
                
                if (empresa.Length > 0)
                {
                    select = "select reg_codigo, reg_numero, reg_tipo from wms_regiao " +
                        "where conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                        "order by reg_numero";
                }
                else
                {
                    select = "select reg_codigo, reg_numero, reg_tipo from wms_regiao order by reg_numero ";
                }

                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe endereço
                    Estrutura endereco = new Estrutura();
                    //Adiciona o id do endereco
                    if (linha["reg_codigo"] != DBNull.Value)
                    {
                        endereco.codRegiao = Convert.ToInt32(linha["reg_codigo"]);
                    }
                    if (linha["reg_numero"] != DBNull.Value)
                    {
                        //Adiciona o endereço
                        endereco.numeroRegiao = Convert.ToInt32(linha["reg_numero"]);
                    }
                    if (linha["reg_tipo"] != DBNull.Value)
                    {
                        //Adiciona o tipo de região
                        endereco.tipoRegiao = Convert.ToString(linha["reg_tipo"]);
                    }

                    //Adiciona a disponibilidade do endereço
                    enderecoCollection.Add(endereco);

                }
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a região. \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa rua (Preenche os ComboBox e a TreeView)
        public EstruturaCollection PesqRua(int idRegiao)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Instância a coleção
                EstruturaCollection enderecoCollection = new EstruturaCollection();

                conexao.AdicionarParamentros("@idRegiao", idRegiao);

                //String de consulta
                string select = "select rua_codigo, rua_numero from wms_rua where reg_codigo = @idRegiao order by rua_numero";
                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe endereço
                    Estrutura endereco = new Estrutura();

                    if (linha["rua_codigo"] != DBNull.Value)
                    {
                        //Adiciona o id da rua
                        endereco.codRua = Convert.ToInt32(linha["rua_codigo"]);
                    }
                    if (linha["rua_codigo"] != DBNull.Value)
                    {
                        //Adiciona o número da rua
                        endereco.numeroRua = Convert.ToInt32(linha["rua_numero"]);
                    }


                    enderecoCollection.Add(endereco);

                }
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as ruas da região. \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa bloco (Preenche os ComboBox e a TreeView)
        public EstruturaCollection PesqBloco(int idRua)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Instância a coleção
                EstruturaCollection enderecoCollection = new EstruturaCollection();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idRua", idRua);

                //String de consulta
                string select = "select bloc_codigo, bloc_numero from wms_bloco where rua_codigo = @idRua order by bloc_numero";
                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe endereço
                    Estrutura endereco = new Estrutura();

                    if (linha["bloc_codigo"] != DBNull.Value)
                    {
                        //Adiciona o id do bloco
                        endereco.codBloco = Convert.ToInt32(linha["bloc_codigo"]);
                    }

                    if (linha["bloc_numero"] != DBNull.Value)
                    {
                        //Adiciona o numero do bloco
                        endereco.numeroBloco = Convert.ToInt32(linha["bloc_numero"]);
                    }

                    enderecoCollection.Add(endereco);

                }
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os blocos da rua. \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa Nivel (Preenche os ComboBox e a TreeView)
        public EstruturaCollection PesqNivel(int idBloco)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Instância a coleção
                EstruturaCollection enderecoCollection = new EstruturaCollection();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idBloco", idBloco);

                //String de consulta
                string select = "select niv_codigo, niv_numero from wms_nivel where bloc_codigo = @idBloco order by niv_numero";
                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe endereço
                    Estrutura endereco = new Estrutura();

                    if (linha["niv_codigo"] != DBNull.Value)
                    {
                        //Adiciona o id do nível
                        endereco.codNivel = Convert.ToInt32(linha["niv_codigo"]);
                    }
                    if (linha["niv_numero"] != DBNull.Value)
                    {
                        //Adiciona o número do nível
                        endereco.numeroNivel = Convert.ToInt32(linha["niv_numero"]);
                    }

                    enderecoCollection.Add(endereco);
                }
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os  níveis do bloco. \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa Apartamento (Preenche os ComboBox e a TreeView)
        public EstruturaCollection PesqApartamento(int idNivel)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Instância a coleção
                EstruturaCollection enderecoCollection = new EstruturaCollection();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idNivel", idNivel);

                //String de consulta
                string select = "select apa_codigo, apa_numero from wms_apartamento where niv_codigo = @idNivel order by apa_numero";
                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe endereço
                    Estrutura endereco = new Estrutura();

                    if (linha["apa_codigo"] != DBNull.Value)
                    {
                        //Adiciona o id do apartamento
                        endereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    }

                    if (linha["apa_numero"] != DBNull.Value)
                    {
                        //Adiciona o número do apartamento
                        endereco.numeroApartamento = Convert.ToInt32(linha["apa_numero"]);
                    }

                    enderecoCollection.Add(endereco);

                }
                return enderecoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os apartamentos \nDetalhes: " + ex.Message);
            }
        }

        //Pesquisa Apartamento (Preenche os ComboBox e a TreeView)
        public Estrutura PesqApartamentoInformacao(int idBloco, int idNivel, int idApartamento)
        {
            try
            {
                //Limpa a conexão
                conexao.LimparParametros();
                //Instância a coleção
                Estrutura gerarEndereco = new Estrutura();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@bloco", idBloco);
                conexao.AdicionarParamentros("@nivel", idNivel);
                conexao.AdicionarParamentros("@apartamento", idApartamento);

                //String de consulta
                string select = "select apa_codigo , apa_endereco, apa_tipo, b.bloc_lado, apa_disponivel, apa_status, apa_tamanho_palete " +
                                "from wms_apartamento ap " +
                                "inner join wms_nivel n " +
                                "on n.niv_codigo = ap.niv_codigo " +
                                "inner join wms_bloco b " +
                                "on b.bloc_codigo = n.bloc_codigo " +
                                "where b.bloc_codigo = @bloco and n.niv_codigo = @nivel and apa_codigo = @apartamento";
                //executa o select
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona o id do aprtamento
                    gerarEndereco.codApartamento = Convert.ToInt32(linha["apa_codigo"]);
                    //Adiciona o endereço
                    gerarEndereco.descApartamento = Convert.ToString(linha["apa_endereco"]);
                    //Adiciona o pallet
                    gerarEndereco.tamanhoApartamento = Convert.ToString(linha["apa_tamanho_palete"]);

                    //Adiciona o tipo endereço
                    if (linha["apa_tipo"].Equals("Picking"))
                    {
                        //Adiciona o tipo endereço
                        gerarEndereco.tipoApartamento = Convert.ToString(linha["apa_tipo"]);

                        if (linha["apa_tamanho_palete"].Equals("PB"))
                        {
                            //Adiciona o tipo endereço
                            gerarEndereco.tipoApartamento = "Blocado";
                        }
                    }
                    else if (linha["apa_tipo"].Equals("Pulmao"))
                    {
                        //Adiciona o tipo endereço
                        gerarEndereco.tipoApartamento = Convert.ToString(linha["apa_tipo"]);

                        if (linha["apa_tamanho_palete"].Equals("PB"))
                        {
                            //Adiciona o tipo endereço
                            gerarEndereco.tipoApartamento = "Blocado";
                        }
                    }

                    //Adiciona o lado do endereço
                    if (linha["bloc_lado"] != DBNull.Value)
                    {
                        //Adiciona o lado do endereço
                        gerarEndereco.ladoBloco = Convert.ToString(linha["bloc_lado"]);
                    }

                    //Adiciona o status do endereço vago ou ocupado
                    if (linha["apa_status"] != DBNull.Value)
                    {
                        //Adiciona status do endereço
                        gerarEndereco.statusApartamento = Convert.ToString(linha["apa_status"]);
                    }

                    //Adiciona a disposição
                    if (linha["apa_disponivel"] != DBNull.Value)
                    {
                        //Adiciona status do endereço
                        gerarEndereco.disposicaoApartamento = Convert.ToString(linha["apa_disponivel"]);
                    }

                }
                return gerarEndereco;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Pesquisa a quantidade de regiões, ruas, blocos e apartamentos cadastrados
        public Estrutura PesqInformacao()
        {
            try
            {
                //Instância a coleção
                Estrutura gerarEndereco = new Estrutura();
                //Consulta a qtd de região
                string select = "select count(reg_codigo) as regiao from wms_regiao";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre o datatable
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdRegiao = Convert.ToInt32(linha["regiao"]);
                }

                //Consulta a qtd de rua
                select = "select count(rua_codigo) as rua from wms_rua ";

                //Instância um datatable
                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre o datatable
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdRua = Convert.ToInt32(linha["rua"]);
                }

                //Consulta a qtd de bloco
                select = "select count(bloc_codigo) as bloco from wms_bloco ";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdBloco = Convert.ToInt32(linha["bloco"]);
                }
                //Consulta qtd de apartamento
                select = "select count(apa_codigo) as apartamento from wms_apartamento";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdApartamento = Convert.ToInt32(linha["apartamento"]);
                }

                //Consulta qtd de pulmao
                select = "select count(apa_codigo) as qtdAereo from wms_apartamento where apa_tipo = 'Pulmao' and apa_tamanho_palete != 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPulmao = Convert.ToInt32(linha["qtdAereo"]);
                }

                //Consulta qtd de pulmao indisponivel
                select = "select count(apa_codigo) as indisponivel from wms_apartamento where apa_tipo = 'Pulmao' and apa_disponivel = 'Nao' and apa_tamanho_palete != 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPulmaoIndisponivel = Convert.ToInt32(linha["indisponivel"]);
                }

                //Consulta qtd de pulmao disponivel
                select = "select count(apa_codigo) as disponivel from wms_apartamento where apa_tipo = 'Pulmao' and apa_disponivel = 'Sim'and apa_tamanho_palete != 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPulmaoDisponivel = Convert.ToInt32(linha["disponivel"]);
                }

                //Consulta qtd de pulmao ocupado
                select = "select count(apa_codigo) as ocupado from wms_apartamento where apa_tipo = 'Pulmao' and apa_disponivel = 'Sim' and apa_status = 'Ocupado' and apa_tamanho_palete != 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPulmaoOcupado = Convert.ToInt32(linha["ocupado"]);
                }

                //Consulta qtd de pulmao vago
                select = "select count(apa_codigo) as vago from wms_apartamento where apa_tipo = 'Pulmao' and apa_disponivel = 'Sim' and apa_status = 'Vago' and apa_tamanho_palete != 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPulmaoVago = Convert.ToInt32(linha["vago"]);
                }

                //Consulta qtd de blocados
                select = "select count(apa_codigo) as qtdBlocado from wms_apartamento where apa_tipo = 'Pulmao' and apa_tamanho_palete = 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdBlocados = Convert.ToInt32(linha["qtdBlocado"]);
                }

                //Consulta qtd de blocado indisponivel
                select = "select count(apa_codigo) as indisponivel from wms_apartamento where apa_tipo = 'Pulmao' and apa_disponivel = 'Nao' and apa_tamanho_palete = 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdBlocadosIndisponivel = Convert.ToInt32(linha["indisponivel"]);
                }

                //Consulta qtd de blocado disponivel
                select = "select count(apa_codigo) as disponivel from wms_apartamento where apa_tipo = 'Pulmao' and apa_disponivel = 'Sim' and apa_tamanho_palete = 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdBlocadosDisponivel = Convert.ToInt32(linha["disponivel"]);
                }

                //Consulta qtd de blocado ocupado
                select = "select count(apa_codigo) as ocupado from wms_apartamento where apa_tipo = 'Pulmao' and apa_disponivel = 'Sim' and apa_status = 'Ocupado' and apa_tamanho_palete = 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdBlocadosOcupado = Convert.ToInt32(linha["ocupado"]);
                }

                //Consulta qtd de blocado vago
                select = "select count(apa_codigo) as vago from wms_apartamento where apa_tipo = 'Pulmao' and apa_disponivel = 'Sim' and apa_status = 'Vago' and apa_tamanho_palete = 'PB'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdBlocadosVago = Convert.ToInt32(linha["vago"]);
                }

                //Consulta qtd de picking
                select = "select count(apa_codigo) as qtdPicking from wms_apartamento where apa_tipo = 'Picking'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPicking = Convert.ToInt32(linha["qtdPicking"]);
                }

                //Consulta qtd de picking indisponivel
                select = "select count(apa_codigo) as indisponivel from wms_apartamento where apa_tipo = 'Picking' and apa_disponivel = 'Nao'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPickingIndisponivel = Convert.ToInt32(linha["indisponivel"]);
                }

                //Consulta qtd de picking disponivel
                select = "select count(apa_codigo) as disponivel from wms_apartamento where apa_tipo = 'Picking' and apa_disponivel = 'Sim'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPickingDisponivel = Convert.ToInt32(linha["disponivel"]);
                }

                //Consulta qtd de picking ocupado
                select = "select count(apa_codigo) as ocupado from wms_apartamento where apa_tipo = 'Picking' and apa_disponivel = 'Sim' and apa_status = 'Ocupado'";
                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPickingOcupado = Convert.ToInt32(linha["ocupado"]);
                }

                //Consulta qtd de Picking vago
                select = "select count(apa_codigo) as vago from wms_apartamento where apa_tipo = 'Picking' and apa_disponivel = 'Sim' and apa_status = 'Vago'";

                dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada
                foreach (DataRow linha in dataTable.Rows)
                {
                    gerarEndereco.qtdPickingVago = Convert.ToInt32(linha["vago"]);
                }

                //Retorna as informações
                return gerarEndereco;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar as informações. \nDetalhes:" + ex.Message);
            }
        }

        //Gera uma nova região
        public void GerarRegiao(int idRegiao, int nrRegiao, string tipoRegiao)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                conexao.AdicionarParamentros("@numeroRegiao", nrRegiao);
                conexao.AdicionarParamentros("@tipoRegiao", tipoRegiao);

                //String de insert
                string insert = "insert into wms_regiao (reg_codigo, reg_numero, reg_tipo) " +
                        "values (@idRegiao, @numeroRegiao, @tipoRegiao)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar uma nova região. \nDetalhes:" + ex.Message);
            }
        }
        
        //Gera uma nova rua
        public void GerarRua(int idRegiao, int idRua, int nrRua)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idRegiao", idRegiao);
                conexao.AdicionarParamentros("@idRua", idRua);
                conexao.AdicionarParamentros("@nrRua", nrRua);

                //String de insert
                string insert = "insert into wms_rua (reg_codigo, rua_codigo, rua_numero) " +
                        "values (@idRegiao, @idRua, @nrRua)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar uma nova rua. \nDetalhes:" + ex.Message);
            }
        }
       
        /*Gera um novo bloco*/
        public void GerarBloco(int idRua, int idBloco, int nrBloco, string ladoBloco)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idBloco", idBloco);
                conexao.AdicionarParamentros("@idRua", idRua);
                conexao.AdicionarParamentros("@nrBloco", nrBloco);
                conexao.AdicionarParamentros("@ldBloco", ladoBloco);

                //String de insert
                string insert = "insert into wms_bloco " +
                        "(bloc_codigo,rua_codigo,bloc_numero, bloc_lado) " +
                        "values" +
                        "(@idBloco, @idRua, @nrBloco, @ldBloco)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar um novo bloco. \nDetalhes:" + ex.Message);
            }
        }

        /*Gera um novo nível*/
        public void GerarNivel(int idBloco, int idNivel, int nrNivel, string tpNivel)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idBloco", idBloco);
                conexao.AdicionarParamentros("@idNivel", idNivel);
                conexao.AdicionarParamentros("@nrNivel", nrNivel);
                conexao.AdicionarParamentros("@tpNivel", tpNivel);

                //String de insert
                string insert = "insert into wms_nivel (bloc_codigo, niv_codigo, niv_numero, niv_tipo) " +
                        "values (@idBloco, @idNivel, @nrNivel, @tpNivel)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar um novo nível. \nDetalhes:" + ex.Message);
            }
        }

        /*Gera um novo apartamento*/
        public void GerarApartamento(Estrutura gerarEdereco)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idNivel", gerarEdereco.codNivel);
                conexao.AdicionarParamentros("@regiao", gerarEdereco.numeroRegiao);
                conexao.AdicionarParamentros("@rua", gerarEdereco.numeroRua);
                conexao.AdicionarParamentros("@bloco", gerarEdereco.numeroBloco);
                conexao.AdicionarParamentros("@nivel", gerarEdereco.numeroNivel);
                conexao.AdicionarParamentros("@apartamento", gerarEdereco.numeroApartamento);
                conexao.AdicionarParamentros("@endereco", gerarEdereco.descApartamento);
                conexao.AdicionarParamentros("@ordemEndereco", gerarEdereco.ordemApartamento);
                conexao.AdicionarParamentros("@status", gerarEdereco.statusApartamento);
                conexao.AdicionarParamentros("@tipo", gerarEdereco.tipoApartamento);
                conexao.AdicionarParamentros("@disposicao", gerarEdereco.disposicaoApartamento);
                conexao.AdicionarParamentros("@lado", gerarEdereco.ladoBloco);
                conexao.AdicionarParamentros("@tamanho", gerarEdereco.tamanhoApartamento);

                //String de insert
                string insert = "insert into wms_apartamento (niv_codigo, apa_numero, apa_endereco, " +
                "apa_ordem, apa_status, apa_tipo, apa_disponivel, apa_tamanho_palete) " +
                "values (@idNivel, @apartamento, @endereco, @ordemEndereco,  @status, " +
                " @tipo, @disposicao, @tamanho)";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar um novo apartamento. \nDetalhes:" + ex.Message);
            }
        }
        
        //Atualiza o endereço
        public void AtualizarEndereco(int idApartamento,  string status, string disposicao, string pallet)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@idApartamento", idApartamento);
                conexao.AdicionarParamentros("@pallet", pallet);
                conexao.AdicionarParamentros("@status", status);
                conexao.AdicionarParamentros("@disposicao", disposicao);

                //String de atualização
                string update = "update wms_apartamento set apa_status = @status, apa_disponivel = @disposicao, apa_tamanho_palete = @pallet " +
                    "where apa_codigo = @idApartamento";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o endereço. \nDetalhes:" + ex.Message);
            }
        }
    

    }
}
