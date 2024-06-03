using System;
using System.Data;
using Dados;
using ObjetoTransferencia;


namespace Negocios
{
    public class OcorrenciaNegocios
    {
        //Instância a classe conexão
        Conexao conexao = new Conexao();

        //Pesquisa as categorias
        public TipoOcorrenciaCollection PesqTipoOcorrencia()
        {
            try
            {
                //Instância a coleção
                TipoOcorrenciaCollection tipoOcorrenciaCollection = new TipoOcorrenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //String de consulta
                string select = "select oco_codigo, oco_descricao, oco_area from wms_tipo_ocorrencia where oco_ativo = 'True' order by oco_descricao";
                //Instância um datatable  
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a classe
                    TipoOcorrencia tipoOcorrencia = new TipoOcorrencia();
                    //Adiciona os valores encontrados
                    if (linha["oco_codigo"] != DBNull.Value)
                    {
                        tipoOcorrencia.codigo = Convert.ToInt32(linha["oco_codigo"]);
                    }

                    if (linha["oco_descricao"] != DBNull.Value)
                    {
                        tipoOcorrencia.descricao = Convert.ToString(linha["oco_descricao"]);
                    }

                    if (linha["oco_area"] != DBNull.Value)
                    {
                        tipoOcorrencia.area = Convert.ToString(linha["oco_area"]);
                    }

                    //Adiciona os cadastros encontrados a coleção
                    tipoOcorrenciaCollection.Add(tipoOcorrencia);
                }
                //Retorna a coleção de cadastro encontrada
                return tipoOcorrenciaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os tipos de ocorrência. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa manifesto
        public OcorrenciaCollection PesqManifesto(string codManifesto, string empresa)
        {
            try
            {
                //Instância o objêto
                OcorrenciaCollection ocorrenciaCollection = new OcorrenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select p.ped_codigo, p.ped_data_nota_fiscal, p.ped_nota_fiscal, tp.tipo_descricao, pg.pag_descricao, p.ped_total, p.ped_peso, " +
                                "m.mani_codigo, mot.mot_codigo, mot.mot_nome, mot.mot_apelido, mot.mot_celular, v.vei_placa, p.ped_observacao, " +
                                "c.cli_codigo, c.cli_nome, c.cli_fantasia, c.cli_email, e.est_uf, cid.cid_nome, b.bar_nome, c.cli_endereco, c.cli_numero, " +
                                "r.rep_nome, r.rep_celular, sup_nome " +
                                "from wms_pedido p " +
                                "inner join wms_tipo_pedido tp " +
                                "on tp.tipo_codigo = p.tipo_codigo " +
                                "inner join wms_pagamento pg " +
                                "on pg.pag_codigo = p.pag_codigo " +
                                "left join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "left join wms_motorista mot " +
                                "on mot.mot_codigo = m.mot_codigo " +
                                "left join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade cid " +
                                "on cid.cid_codigo = b.cid_codigo " +
                                "inner join wms_estado e " +
                                "on e.est_codigo = cid.est_codigo " +
                                "left join wms_representante r " +
                                "on r.rep_codigo = p.rep_codigo " +
                                "left join wms_supervisor s " +
                                "on s.equi_codigo = r.equi_codigo " +
                                "where p.mani_codigo = @codManifesto " +
                                "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Ocorrencia ocorrencia = new Ocorrencia();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["ped_data_nota_fiscal"] != DBNull.Value)
                    {
                        ocorrencia.dataFaturamento = Convert.ToDateTime(linha["ped_data_nota_fiscal"]);
                    }

                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        ocorrencia.notaFiscal = Convert.ToInt32(linha["ped_nota_fiscal"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        ocorrencia.tipoPedido = Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["pag_descricao"] != DBNull.Value)
                    {
                        ocorrencia.pagamentoPedido = Convert.ToString(linha["pag_descricao"]);
                    }

                    if (linha["ped_total"] != DBNull.Value)
                    {
                        ocorrencia.totalPedido = Convert.ToDouble(linha["ped_total"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        ocorrencia.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        ocorrencia.manifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["mot_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codMotorista = Convert.ToInt32(linha["mot_codigo"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    if (linha["mot_apelido"] != DBNull.Value)
                    {
                        ocorrencia.motorista = Convert.ToString(linha["mot_apelido"]);
                    }

                    if (linha["mot_celular"] != DBNull.Value)
                    {
                        ocorrencia.celularMotorista = Convert.ToString(linha["mot_celular"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        ocorrencia.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["ped_observacao"] != DBNull.Value)
                    {
                        ocorrencia.obsPedido = Convert.ToString(linha["ped_observacao"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codCliente = Convert.ToString(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        ocorrencia.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["cli_email"] != DBNull.Value)
                    {
                        ocorrencia.emailCliente = Convert.ToString(linha["cli_email"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        ocorrencia.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        ocorrencia.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        ocorrencia.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        ocorrencia.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        ocorrencia.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["rep_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeRepresentante = Convert.ToString(linha["rep_nome"]);
                    }

                    if (linha["rep_celular"] != DBNull.Value)
                    {
                        ocorrencia.celularRepresentante = Convert.ToString(linha["rep_celular"]);
                    }

                    if (linha["sup_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeSupervisor = Convert.ToString(linha["sup_nome"]);
                    }

                    ocorrenciaCollection.Add(ocorrencia);

                }
                //Retorna a coleção de cadastro encontrada
                return ocorrenciaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro o manifesto. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa os pedidos
        public Ocorrencia PesqPedido(string codPedido, string notaPedido, string empresa)
        {
            try
            {
                //Instância o objêto
                Ocorrencia ocorrencia = new Ocorrencia();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@notaPedido", notaPedido);
                conexao.AdicionarParamentros("@empresa", empresa);
                //String de consulta
                string select = "select p.ped_codigo, p.ped_data_nota_fiscal, p.ped_nota_fiscal, tp.tipo_descricao, pg.pag_descricao, p.ped_total, p.ped_peso, " +
                                "m.mani_codigo, mot.mot_codigo, mot.mot_nome, mot.mot_apelido, mot.mot_celular, v.vei_placa, p.ped_observacao, " +
                                "c.cli_codigo, c.cli_nome, c.cli_fantasia, c.cli_email, e.est_uf, cid.cid_nome, b.bar_nome, c.cli_endereco, c.cli_numero, p.ped_implantador, " +
                                "r.rep_nome, r.rep_celular, sup_nome " +
                                "from wms_pedido p " +
                                "inner join wms_tipo_pedido tp " +
                                "on tp.tipo_codigo = p.tipo_codigo " +
                                "inner join wms_pagamento pg " +
                                "on pg.pag_codigo = p.pag_codigo " +
                                "left join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "left join wms_motorista mot " +
                                "on mot.mot_codigo = m.mot_codigo " +
                                "left join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade cid " +
                                "on cid.cid_codigo = b.cid_codigo " +
                                "inner join wms_estado e " +
                                "on e.est_codigo = cid.est_codigo " +
                                "left join wms_representante r " +
                                "on r.rep_codigo = p.rep_codigo " +
                                "left join wms_supervisor s " +
                                "on s.equi_codigo = r.equi_codigo ";

                if (!codPedido.Equals(string.Empty))
                {
                    select += "where ped_codigo = @codPedido and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }

                if (!notaPedido.Equals(string.Empty))
                {
                    select += "where p.ped_nota_fiscal = @notaPedido and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";
                }


                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["ped_data_nota_fiscal"] != DBNull.Value)
                    {
                        ocorrencia.dataFaturamento = Convert.ToDateTime(linha["ped_data_nota_fiscal"]);
                    }

                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        ocorrencia.notaFiscal = Convert.ToInt32(linha["ped_nota_fiscal"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        ocorrencia.tipoPedido = Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["pag_descricao"] != DBNull.Value)
                    {
                        ocorrencia.pagamentoPedido = Convert.ToString(linha["pag_descricao"]);
                    }

                    if (linha["ped_total"] != DBNull.Value)
                    {
                        ocorrencia.totalPedido = Convert.ToDouble(linha["ped_total"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        ocorrencia.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        ocorrencia.manifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["mot_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codMotorista = Convert.ToInt32(linha["mot_codigo"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    if (linha["mot_apelido"] != DBNull.Value)
                    {
                        ocorrencia.motorista = Convert.ToString(linha["mot_apelido"]);
                    }

                    if (linha["mot_celular"] != DBNull.Value)
                    {
                        ocorrencia.celularMotorista = Convert.ToString(linha["mot_celular"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        ocorrencia.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["ped_observacao"] != DBNull.Value)
                    {
                        ocorrencia.obsPedido = Convert.ToString(linha["ped_observacao"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codCliente = Convert.ToString(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        ocorrencia.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["cli_email"] != DBNull.Value)
                    {
                        ocorrencia.emailCliente = Convert.ToString(linha["cli_email"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        ocorrencia.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        ocorrencia.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        ocorrencia.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        ocorrencia.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        ocorrencia.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["rep_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeRepresentante = Convert.ToString(linha["rep_nome"]);
                    }

                    if (linha["rep_celular"] != DBNull.Value)
                    {
                        ocorrencia.celularRepresentante = Convert.ToString(linha["rep_celular"]);
                    }

                    if (linha["sup_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeSupervisor = Convert.ToString(linha["sup_nome"]);
                    }

                    if (linha["ped_implantador"] != DBNull.Value)
                    {
                        ocorrencia.pedImplantador = Convert.ToString(linha["ped_implantador"]);
                    }
                }
                //Retorna a coleção de cadastro encontrada
                return ocorrencia;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o pedido. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o pedido
        public ItensPedidoCollection PesqItem(int codPedido, string empresa)
        {
            try
            {
                //Instância o objêto
                ItensPedidoCollection itemPedidoCollection = new ItensPedidoCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //adiciona parâmetros
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de consulta
                string select = "select p.ped_codigo, pp.prod_id, prod_codigo, prod_descricao, ip.iped_quantidade, u1.uni_unidade as uni_fracionada, iped_valor, iped_peso " +
                "from wms_item_pedido ip " +
                "inner join wms_pedido p " +
                "on p.ped_codigo = ip.ped_codigo " +
                "inner join wms_produto pp " +
                "on pp.prod_id = ip.prod_id " +
                "left join wms_unidade u " +
                "on u.uni_codigo = pp.uni_codigo_pulmao " +
                "left join wms_unidade u1 " +
                "on u1.uni_codigo = pp.uni_codigo_picking " +
                "where ip.ped_codigo = @codPedido " +
                "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa)";

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItensPedido itemPedido = new ItensPedido();

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itemPedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itemPedido.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itemPedido.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itemPedido.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["iped_quantidade"] != DBNull.Value)
                    {
                        itemPedido.qtdProduto = Convert.ToInt32(linha["iped_quantidade"]);
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.uniUnidade = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (linha["iped_valor"] != DBNull.Value)
                    {
                        itemPedido.valorTotal = Convert.ToDouble(linha["iped_valor"]);

                        itemPedido.valorUnitario = Convert.ToDouble(linha["iped_valor"]) / Convert.ToInt32(linha["iped_quantidade"]);
                    }

                    if (linha["iped_peso"] != DBNull.Value)
                    {
                        itemPedido.pesoTotal = Convert.ToDouble(linha["iped_peso"]);
                    }

                    //Adiona o objêto a coleção
                    itemPedidoCollection.Add(itemPedido);

                }
                //Retorna a coleção de cadastro encontrada
                return itemPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens do pedido. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa as ocorrência
        public OcorrenciaCollection PesqOcorrencia(string codOcorrencia, string codPedido, string notaPedido, string codManifesto, string codRepresentante,
                                                   string codCliente, string codUsuario, string codMotorista, int Tipo, string Status, string empresa, string dataInicial, string dataFinal)
        {
            try
            {
                //Instância o objêto
                OcorrenciaCollection ocorrenciaCollection = new OcorrenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codOcorrencia", codOcorrencia);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@notaPedido", notaPedido);
                conexao.AdicionarParamentros("@codRepresentante", codRepresentante);
                conexao.AdicionarParamentros("@codCliente", codCliente);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codMotorista", codMotorista);
                conexao.AdicionarParamentros("@Tipo", Tipo);
                conexao.AdicionarParamentros("@Status", Status);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");


                //String de consulta
                string select = "select po.poco_codigo, p.ped_codigo, p.ped_data_nota_fiscal, p.ped_nota_fiscal, tp.tipo_descricao, pg.pag_descricao, p.ped_total, p.ped_peso, " +
                                "po.mani_codigo, po.mot_codigo, mot.mot_nome, mot.mot_apelido, mot.mot_celular, v.vei_placa, p.ped_observacao, " +
                                "c.cli_codigo, c.cli_nome, c.cli_fantasia, c.cli_email, e.est_uf, cid.cid_nome, b.bar_nome, c.cli_endereco, c.cli_numero, " +
                                "r.rep_codigo, r.rep_nome, r.rep_celular, sup_nome, po.oco_codigo, o.oco_descricao, po.poco_area_ocorrencia, po.mani_codigo_ocorrencia, " +
                                "po.mot_codigo_ocorrencia, mot1.mot_apelido as motorista_ocorrencia, " +
                                "po.poco_devolucao, po.poco_reentrega, po.poco_programacao_reentrega, poco_cliente_aguardo, po.poco_observacao, po.poco_status, poco_data_ocorrencia, usu_login, ped_implantador " +
                                "from wms_pedido p " +
                                "inner join wms_pedido_ocorrencia po " +
                                "on po.ped_codigo = p.ped_codigo " +
                                "inner join wms_tipo_pedido tp " +
                                "on tp.tipo_codigo = p.tipo_codigo " +
                                "inner join wms_pagamento pg " +
                                "on pg.pag_codigo = p.pag_codigo " +
                                "left join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "left join wms_motorista mot " +
                                "on po.mot_codigo = mot.mot_codigo " +
                                "left join wms_motorista mot1 " +
                                "on mot1.mot_codigo = po.mot_codigo_ocorrencia " +
                                "left join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade cid " +
                                "on cid.cid_codigo = b.cid_codigo " +
                                "inner join wms_estado e " +
                                "on e.est_codigo = cid.est_codigo " +
                                "left join wms_representante r " +
                                "on r.rep_codigo = p.rep_codigo " +
                                "left join wms_supervisor s " +
                                "on s.equi_codigo = r.equi_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = po.usu_codigo_ocorrencia " +
                                "left join wms_tipo_ocorrencia o " +
                                "on o.oco_codigo = po.oco_codigo ";

                if (!codOcorrencia.Equals(string.Empty))
                {
                    select += "where po.poco_codigo = @codOcorrencia"; //modificado
                }

                else if (!codManifesto.Equals(string.Empty))
                {
                    select += "where p.mani_codigo = @codManifesto"; //modificado
                }

                else if (!notaPedido.Equals(string.Empty))
                {
                    select += "where p.ped_nota_fiscal = @notaPedido"; //modificado
                }

                else if (!codPedido.Equals(string.Empty))
                {
                    select += "where po.ped_codigo = @codPedido"; //modificado
                }

                else if (!codRepresentante.Equals(string.Empty))
                {
                    select += "where r.rep_codigo = @codRepresentante and po.poco_data_ocorrencia between @dataInicial and @dataFinal"; //modificado
                }

                else if (!codCliente.Equals(string.Empty))
                {
                    select += "where c.cli_codigo = @codCliente and po.poco_data_ocorrencia between @dataInicial and @dataFinal "; //modificado
                }

                else if (!codUsuario.Equals(string.Empty))
                {
                    select += "where po.usu_codigo_ocorrencia = @codUsuario and po.poco_data_ocorrencia between @dataInicial and @dataFinal"; //modificado
                }

                else if (!codMotorista.Equals(string.Empty))
                {
                    select += "where po.mot_codigo_ocorrencia = @codMotorista and po.poco_data_ocorrencia between @dataInicial and @dataFinal"; //modificado
                }

                else if (codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) && (Tipo.Equals(string.Empty) || Tipo == 0) && (Status.Equals(string.Empty) || Status.Equals("TODOS")))
                {
                    select += "where po.poco_data_ocorrencia between @dataInicial and @dataFinal "; //modificado
                }

                else if (codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) && !(Tipo.Equals(string.Empty) || Tipo == 0) && !(Status.Equals(string.Empty) || Status.Equals("TODOS")))
                {
                    select += "where po.oco_codigo = @Tipo and po.poco_status = @Status and po.poco_data_ocorrencia between @dataInicial and @dataFinal";
                              //modificado
                }

                else if (codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) && !(Tipo.Equals(string.Empty) || Tipo == 0))
                {
                    select += "where po.oco_codigo = @Tipo and po.poco_data_ocorrencia between @dataInicial and @dataFinal"; //modificado
                }

                else if (codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) && !(Status.Equals(string.Empty) || Status.Equals("TODOS")))
                {
                    if(Status.Equals("REENTREGA"))
                    {
                        select += "where po.poco_reentrega = 'True' and po.poco_data_ocorrencia between @dataInicial and @dataFinal " +
                                  "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) "; //modificado
                    }
                    else
                    {
                        select += "where po.poco_status = @Status and po.poco_data_ocorrencia between @dataInicial and @dataFinal " +
                            "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) "; //modificado
                    }
                    
                }

                else if ((!dataInicial.Equals(string.Empty)) && codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) &&
                        Tipo.Equals(string.Empty) && Status.Equals(string.Empty))
                {
                    select += "where po.poco_data_ocorrencia between @dataInicial and @dataFinal " +
                              "and p.conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) "; //modificado
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Ocorrencia ocorrencia = new Ocorrencia();

                    if (linha["poco_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codOcorrencia = Convert.ToInt32(linha["poco_codigo"]);
                    }

                    if (linha["ped_data_nota_fiscal"] != DBNull.Value)
                    {
                        ocorrencia.dataFaturamento = Convert.ToDateTime(linha["ped_data_nota_fiscal"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }        

                    if (linha["ped_data_nota_fiscal"] != DBNull.Value)
                    {
                        ocorrencia.dataFaturamento = Convert.ToDateTime(linha["ped_data_nota_fiscal"]);
                    }

                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        ocorrencia.notaFiscal = Convert.ToInt32(linha["ped_nota_fiscal"]);
                    }

                    if (linha["tipo_descricao"] != DBNull.Value)
                    {
                        ocorrencia.tipoPedido = Convert.ToString(linha["tipo_descricao"]);
                    }

                    if (linha["pag_descricao"] != DBNull.Value)
                    {
                        ocorrencia.pagamentoPedido = Convert.ToString(linha["pag_descricao"]);
                    }

                    if (linha["ped_total"] != DBNull.Value)
                    {
                        ocorrencia.totalPedido = Convert.ToDouble(linha["ped_total"]);
                    }

                    if (linha["ped_peso"] != DBNull.Value)
                    {
                        ocorrencia.pesoPedido = Convert.ToDouble(linha["ped_peso"]);
                    }

                    if (linha["mani_codigo"] != DBNull.Value)
                    {
                        ocorrencia.manifesto = Convert.ToInt32(linha["mani_codigo"]);
                    }

                    if (linha["mot_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codMotorista = Convert.ToInt32(linha["mot_codigo"]);
                    }

                    if (linha["mot_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeMotorista = Convert.ToString(linha["mot_nome"]);
                    }

                    if (linha["mot_apelido"] != DBNull.Value)
                    {
                        ocorrencia.motorista = Convert.ToString(linha["mot_apelido"]);
                    }

                    if (linha["mot_celular"] != DBNull.Value)
                    {
                        ocorrencia.celularMotorista = Convert.ToString(linha["mot_celular"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        ocorrencia.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }

                    if (linha["ped_observacao"] != DBNull.Value)
                    {
                        ocorrencia.obsPedido = Convert.ToString(linha["ped_observacao"]);
                    }

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codCliente = Convert.ToString(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        ocorrencia.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["cli_email"] != DBNull.Value)
                    {
                        ocorrencia.emailCliente = Convert.ToString(linha["cli_email"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        ocorrencia.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        ocorrencia.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        ocorrencia.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["cli_endereco"] != DBNull.Value)
                    {
                        ocorrencia.enderecoCliente = Convert.ToString(linha["cli_endereco"]);
                    }

                    if (linha["cli_numero"] != DBNull.Value)
                    {
                        ocorrencia.numeroCliente = Convert.ToString(linha["cli_numero"]);
                    }

                    if (linha["rep_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeRepresentante = Convert.ToString(linha["rep_nome"]);
                    }

                    if (linha["rep_celular"] != DBNull.Value)
                    {
                        ocorrencia.celularRepresentante = Convert.ToString(linha["rep_celular"]);
                    }

                    if (linha["sup_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeSupervisor = Convert.ToString(linha["sup_nome"]);
                    }

                    if (linha["oco_descricao"] != DBNull.Value)
                    {
                        ocorrencia.descTipoOcorrencia = Convert.ToString(linha["oco_descricao"]);
                    }

                    if (linha["poco_area_ocorrencia"] != DBNull.Value)
                    {
                        ocorrencia.areaOcorrencia = Convert.ToString(linha["poco_area_ocorrencia"]);
                    }

                    if (linha["mani_codigo_ocorrencia"] != DBNull.Value)
                    {
                        ocorrencia.manifestoOcorrencia = Convert.ToInt32(linha["mani_codigo_ocorrencia"]);
                    }

                    if (linha["poco_devolucao"] != DBNull.Value)
                    {
                        ocorrencia.gerarDevolucao = Convert.ToBoolean(linha["poco_devolucao"]);
                    }

                    if (linha["poco_reentrega"] != DBNull.Value)
                    {
                        ocorrencia.gerarReentrega = Convert.ToBoolean(linha["poco_reentrega"]);
                    }

                    if (linha["poco_cliente_aguardo"] != DBNull.Value)
                    {
                        ocorrencia.gerarClienteAguardo = Convert.ToBoolean(linha["poco_cliente_aguardo"]);
                    }
                    

                    if (linha["mot_codigo_ocorrencia"] != DBNull.Value)
                    {
                        ocorrencia.codMotoristaOcorrencia = Convert.ToInt32(linha["mot_codigo_ocorrencia"]);
                    }

                    if (linha["motorista_ocorrencia"] != DBNull.Value)
                    {
                        ocorrencia.motoristaOcorrencia = Convert.ToString(linha["motorista_ocorrencia"]);
                    }

                    if (linha["poco_observacao"] != DBNull.Value)
                    {
                        ocorrencia.obsOcorrencia = Convert.ToString(linha["poco_observacao"]);
                    }

                    if (linha["poco_status"] != DBNull.Value)
                    {
                        ocorrencia.statusOcorrencia = Convert.ToString(linha["poco_status"]);

                        if (Convert.ToString(linha["poco_status"]).Equals("PENDENTE"))
                        {
                            ocorrencia.gerarPendencia = true;
                        }
                        else
                        {
                            ocorrencia.gerarPendencia = false;
                        }
                    }

                    if (linha["poco_data_ocorrencia"] != DBNull.Value)
                    {
                        ocorrencia.dataOcorrencia = Convert.ToDateTime(linha["poco_data_ocorrencia"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        ocorrencia.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    if (linha["ped_implantador"] != DBNull.Value)
                    {
                        ocorrencia.pedImplantador = Convert.ToString(linha["ped_implantador"]);
                    }

                    if (linha["poco_programacao_reentrega"] != DBNull.Value)
                    {
                        ocorrencia.dataReentrega = Convert.ToDateTime(linha["poco_programacao_reentrega"]);
                    }

                    

                    ocorrenciaCollection.Add(ocorrencia);
                }
                //Retorna a coleção de cadastro encontrada
                return ocorrenciaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar o pedido. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa o pedido
        public ItensOcorrenciaCollection PesqItemOcorrencia(string codOcorrencia, string codPedido, string notaPedido, string codManifesto, string codRepresentante,
                                                   string codCliente, string codUsuario, string codMotorista, int Tipo, string Status, string empresa, string dataInicial, string dataFinal)
        {
            try
            {
                //Instância o objêto
                ItensOcorrenciaCollection itemPedidoCollection = new ItensOcorrenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //adiciona parâmetros
                conexao.AdicionarParamentros("@codOcorrencia", codOcorrencia);
                conexao.AdicionarParamentros("@codManifesto", codManifesto);
                conexao.AdicionarParamentros("@codPedido", codPedido);
                conexao.AdicionarParamentros("@notaPedido", notaPedido);
                conexao.AdicionarParamentros("@codRepresentante", codRepresentante);
                conexao.AdicionarParamentros("@codCliente", codCliente);
                conexao.AdicionarParamentros("@codUsuario", codUsuario);
                conexao.AdicionarParamentros("@codMotorista", codMotorista);
                conexao.AdicionarParamentros("@Tipo", Tipo);
                conexao.AdicionarParamentros("@Status", Status);
                conexao.AdicionarParamentros("@empresa", empresa);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");

                //String de consulta
                string select = "select p.ped_codigo, po.poco_codigo, pp.prod_id, prod_codigo, prod_descricao, io.prod_quantidade, " +
                                "u1.uni_unidade as uni_fracionada, io.ioco_qtd_avaria, io.ioco_qtd_falta, io.ioco_qtd_troca, " +
                                "io.ioco_qtd_critica, io.ioco_data_critica, ioco_qtd_devolucao, io.ioco_valor " +
                                "from wms_item_ocorrencia io " +
                                "inner join wms_pedido_ocorrencia po " +
                                "on po.poco_codigo = io.poco_codigo " +
                                "inner join wms_pedido p " +
                                "on p.ped_codigo = po.ped_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_produto pp " +
                                "on pp.prod_id = io.prod_id " +
                                "left join wms_unidade u1 " +
                                "on u1.uni_codigo = pp.uni_codigo_picking ";

                if (!codOcorrencia.Equals(string.Empty))
                {
                    select += "where po.poco_codigo = @codOcorrencia";
                }

                else if (!codManifesto.Equals(string.Empty))
                {
                    select += "where p.mani_codigo = @codManifesto";
                }

                else if (!notaPedido.Equals(string.Empty))
                {
                    select += "where p.ped_nota_fiscal = @notaPedido";
                }

                else if (!codPedido.Equals(string.Empty))
                {
                    select += "where po.ped_codigo = @codPedido";
                }

                else if (!codRepresentante.Equals(string.Empty))
                {
                    select += "where p.rep_codigo = @codRepresentante";
                }

                else if (!codCliente.Equals(string.Empty))
                {
                    select += "where c.cli_codigo = @codCliente";
                }

                else if (!codUsuario.Equals(string.Empty))
                {
                    select += "where po.usu_codigo_ocorrencia  = @codUsuario";
                }

                else if (!codMotorista.Equals(string.Empty))
                {
                    select += "where po.mot_codigo_ocorrencia  = @codMotorista";
                }

                else if (codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) && !(Tipo.Equals(string.Empty) || Tipo == 0) && !(Status.Equals(string.Empty) || Status.Equals("TODOS")))
                {
                    select += "where po.oco_codigo = @Tipo and po.poco_status = @Status and po.poco_data_ocorrencia between @dataInicial and @dataFinal";
                }

                else if (codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) && !(Tipo.Equals(string.Empty) || Tipo == 0))
                {
                    select += "where po.oco_codigo = @Tipo and po.poco_data_ocorrencia between @dataInicial and @dataFinal";
                }

                else if (codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) && !(Status.Equals(string.Empty) || Status.Equals("TODOS")))
                {
                    select += "where po.poco_status = @Status and po.poco_data_ocorrencia between @dataInicial and @dataFinal";
                }

                else if ((!dataInicial.Equals(string.Empty)) && codOcorrencia.Equals(string.Empty) && codManifesto.Equals(string.Empty) &&
                        notaPedido.Equals(string.Empty) && codPedido.Equals(string.Empty) && codRepresentante.Equals(string.Empty) &&
                        codCliente.Equals(string.Empty) && codUsuario.Equals(string.Empty) && codMotorista.Equals(string.Empty) &&
                        Tipo.Equals(string.Empty) && Status.Equals(string.Empty))
                {
                    select += "where po.poco_data_ocorrencia between @dataInicial and @dataFinal";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância a camada de objêto
                    ItensOcorrencia itemPedido = new ItensOcorrencia();

                    if (linha["poco_codigo"] != DBNull.Value)
                    {
                        itemPedido.codItemOcorrencia = Convert.ToInt32(linha["poco_codigo"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        itemPedido.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["prod_id"] != DBNull.Value)
                    {
                        itemPedido.idProduto = Convert.ToInt32(linha["prod_id"]);
                    }

                    if (linha["prod_codigo"] != DBNull.Value)
                    {
                        itemPedido.codProduto = Convert.ToString(linha["prod_codigo"]);
                    }

                    if (linha["prod_descricao"] != DBNull.Value)
                    {
                        itemPedido.descProduto = Convert.ToString(linha["prod_descricao"]);
                    }

                    if (linha["prod_quantidade"] != DBNull.Value)
                    {
                        itemPedido.qtdProduto = Convert.ToInt32(linha["prod_quantidade"]);
                    }

                    if (linha["uni_fracionada"] != DBNull.Value)
                    {
                        itemPedido.fatorProduto = Convert.ToString(linha["uni_fracionada"]);
                    }

                    if (linha["ioco_qtd_avaria"] != DBNull.Value)
                    {
                        itemPedido.qtdAvariaProduto = Convert.ToInt32(linha["ioco_qtd_avaria"]);
                    }

                    if (linha["ioco_qtd_falta"] != DBNull.Value)
                    {
                        itemPedido.qtdFaltaProduto = Convert.ToInt32(linha["ioco_qtd_falta"]);
                    }

                    if (linha["ioco_qtd_troca"] != DBNull.Value)
                    {
                        itemPedido.qtdTrocaProduto = Convert.ToInt32(linha["ioco_qtd_troca"]);
                    }

                    if (linha["ioco_qtd_critica"] != DBNull.Value) 
                    {
                        itemPedido.qtdCriticaProduto = Convert.ToInt32(linha["ioco_qtd_critica"]);
                    }

                    if (linha["ioco_data_critica"] != DBNull.Value)
                    {
                        itemPedido.DataCriticaProduto = Convert.ToDateTime(linha["ioco_data_critica"]).Date;
                    }

                    if (linha["ioco_qtd_devolucao"] != DBNull.Value)
                    {
                        itemPedido.qtdDevolucao = Convert.ToInt32(linha["ioco_qtd_devolucao"]);
                    }

                    /*
                    if (linha["ioco_valor"] != DBNull.Value)
                    {
                        itemPedido.valorProduto = Convert.ToDouble(linha["ioco_valor"]);
                    }
                    */

                    if (linha["ioco_valor"] != DBNull.Value)
                    {
                        itemPedido.valorProduto = Convert.ToDouble(linha["ioco_valor"]);

                        itemPedido.valorUnitario = Convert.ToDouble(linha["ioco_valor"]) / Convert.ToInt32(linha["prod_quantidade"]);
                    }
                    

                    //Adiona o objêto a coleção
                    itemPedidoCollection.Add(itemPedido);

                }
                //Retorna a coleção de cadastro encontrada
                return itemPedidoCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar os itens da ocorrência do pedido. \nDetalhes:" + ex.Message);
            }
        }

        //Pesquisa um novo id
        public int PesqId()
        {
            try
            {
                //Instância a coleção
                int novoId = 0;
                //String de consulta
                string select = "select gen_id(gen_wms_pedido_ocorrencia,1) as id from RDB$DATABASE";
                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    novoId = Convert.ToInt32(linha["id"]);
                }
                //Retorna a coleção de cadastro encontrada
                return novoId;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar um novo registro. \nDetalhes:" + ex.Message);
            }
        }

        //Método salvar a ocorrência
        public void SalvarOcorrencia(Ocorrencia ocorrencia, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codOcorrencia", ocorrencia.codOcorrencia);
                conexao.AdicionarParamentros("@dataOcorrencia", ocorrencia.dataOcorrencia);
                conexao.AdicionarParamentros("@areaOcorrencia", ocorrencia.areaOcorrencia);
                conexao.AdicionarParamentros("@codPedido", ocorrencia.codPedido);
                conexao.AdicionarParamentros("@manifesto", ocorrencia.manifesto);
                conexao.AdicionarParamentros("@manifestoOcorrencia", ocorrencia.manifestoOcorrencia);
                conexao.AdicionarParamentros("@codMotorista", ocorrencia.codMotorista);
                conexao.AdicionarParamentros("@codMotoristaOcorrencia", ocorrencia.codMotoristaOcorrencia);
                conexao.AdicionarParamentros("@codTipoOcorrencia", ocorrencia.codTipoOcorrencia);
                conexao.AdicionarParamentros("@gerarClienteAguardo", ocorrencia.gerarClienteAguardo);
                conexao.AdicionarParamentros("@gerarDevolucao", ocorrencia.gerarDevolucao);
                conexao.AdicionarParamentros("@gerarReentrega", ocorrencia.gerarReentrega);
                conexao.AdicionarParamentros("@dataReentrega", ocorrencia.dataReentrega);
                conexao.AdicionarParamentros("@obsOcorrencia", ocorrencia.obsOcorrencia);
                conexao.AdicionarParamentros("@codUsuarioOcorrencia", ocorrencia.codUsuarioOcorrencia);
                conexao.AdicionarParamentros("@statusOcorrencia", ocorrencia.statusOcorrencia);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de insert - Registra a ocorrência
                string insert = "insert into wms_pedido_ocorrencia (poco_codigo, poco_data_ocorrencia, poco_area_ocorrencia, ped_codigo, mani_codigo, mani_codigo_Ocorrencia, " +
                                "mot_codigo, mot_codigo_Ocorrencia, oco_codigo, poco_devolucao, poco_cliente_aguardo, poco_reentrega, poco_programacao_reentrega, poco_observacao, usu_codigo_ocorrencia, " +
                                "poco_status, conf_codigo) " +
                                "values " +
                                "(@codOcorrencia, @dataOcorrencia, " +
                                "@areaOcorrencia, @codPedido, " +
                                "@manifesto, @manifestoOcorrencia, " +
                                "@codMotorista, @codMotoristaOcorrencia, " +
                                "@codTipoOcorrencia, @gerarDevolucao, " +
                                "@gerarClienteAguardo, @gerarReentrega, " +
                                "@dataReentrega, @obsOcorrencia, " +
                                "@codUsuarioOcorrencia, " +
                                "@statusOcorrencia, " +
                                "(select conf_codigo from wms_configuracao where conf_sigla = @empresa)) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

                //String de insert - Registra os itens da ocorrência
                string insert1 = "insert into wms_item_ocorrencia (poco_codigo, ioco_codigo, ped_codigo, prod_id, prod_quantidade, ioco_valor, conf_codigo) " +
                                "select @codOcorrencia, gen_id(gen_wms_item_ocorrencia, 1), ped_codigo, prod_id, iped_quantidade, iped_valor, " +
                                "(select conf_codigo from wms_configuracao where conf_sigla = @empresa) " +
                                "from wms_item_pedido where ped_codigo = @codPedido";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert1);



            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao registrar a ocorrência. \nDetalhes:" + ex.Message);
            }
        }

        //Método Alterar cadastro
        public void AlterarOcorrencia(Ocorrencia ocorrencia, string empresa)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codOcorrencia", ocorrencia.codOcorrencia);
                conexao.AdicionarParamentros("@areaOcorrencia", ocorrencia.areaOcorrencia);
                conexao.AdicionarParamentros("@manifestoOcorrencia", ocorrencia.manifestoOcorrencia);
                conexao.AdicionarParamentros("@codMotoristaOcorrencia", ocorrencia.codMotoristaOcorrencia);
                conexao.AdicionarParamentros("@codTipoOcorrencia", ocorrencia.codTipoOcorrencia);
                conexao.AdicionarParamentros("@gerarClienteAguardo", ocorrencia.gerarClienteAguardo);
                conexao.AdicionarParamentros("@gerarDevolucao", ocorrencia.gerarDevolucao);
                conexao.AdicionarParamentros("@gerarReentrega", ocorrencia.gerarReentrega);
                conexao.AdicionarParamentros("@dataReentrega", ocorrencia.dataReentrega);
                conexao.AdicionarParamentros("@obsOcorrencia", ocorrencia.obsOcorrencia);
                conexao.AdicionarParamentros("@statusOcorrencia", ocorrencia.statusOcorrencia);
                conexao.AdicionarParamentros("@tempoOcorrencia", ocorrencia.tempoOcorrencia);
                conexao.AdicionarParamentros("@empresa", empresa);

                //String de insert
                string update = "update wms_pedido_ocorrencia set oco_codigo = @codTipoOcorrencia, poco_area_ocorrencia = @areaOcorrencia, mani_codigo_Ocorrencia = @manifestoOcorrencia, " +
                                "mot_codigo_Ocorrencia = @codMotoristaOcorrencia,  poco_devolucao = @gerarDevolucao,  poco_cliente_aguardo = @gerarClienteAguardo, " +
                                "poco_reentrega = @gerarReentrega, poco_programacao_reentrega = @dataReentrega, poco_observacao = @obsOcorrencia, " +
                                "poco_status = @statusOcorrencia, poco_tempo = @tempoOcorrencia " +
                                "where poco_codigo = @codOcorrencia and conf_codigo = (select conf_codigo from wms_configuracao where conf_sigla = @empresa) ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, update);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar a ocorrência. \nDetalhes:" + ex.Message);
            }
        }

        //Método Alterar cadastro
        public void AlterarItem(ItensOcorrencia item)
        {
            try
            {
                //Limpar Paramêtros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@codOcorrencia", item.codItemOcorrencia);
                conexao.AdicionarParamentros("@idProduto", item.idProduto);
                conexao.AdicionarParamentros("@qtdAvariaProduto", item.qtdAvariaProduto);
                conexao.AdicionarParamentros("@qtdFaltaProduto", item.qtdFaltaProduto);
                conexao.AdicionarParamentros("@qtdTrocaProduto", item.qtdTrocaProduto);
                conexao.AdicionarParamentros("@qtdCriticaProduto", item.qtdCriticaProduto);
                conexao.AdicionarParamentros("@DataCriticaProduto", item.DataCriticaProduto);
                conexao.AdicionarParamentros("@qtdDevolucao", item.qtdDevolucao);


                //String de insert
                string insert = "update wms_item_ocorrencia set ioco_qtd_avaria = @qtdAvariaProduto, ioco_qtd_falta = @qtdFaltaProduto, ioco_qtd_troca = @qtdTrocaProduto, " +
                                "ioco_qtd_critica = @qtdCriticaProduto, ioco_data_critica = @DataCriticaProduto, ioco_qtd_devolucao = @qtdDevolucao " +
                                "where poco_codigo = @codOcorrencia and prod_id = @idProduto ";

                //Executa o script no banco
                conexao.ExecuteManipulacao(CommandType.Text, insert);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao alterar os itens da ocorrência. \nDetalhes:" + ex.Message);
            }
        }

        //Relatório
        public OcorrenciaCollection ImpressaoOcorrencia(string tipo, string dataInicial, string dataFinal)
        {
            try
            {
                //Instância o objêto
                OcorrenciaCollection ocorrenciaCollection = new OcorrenciaCollection();
                //Limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os parâmetros
                conexao.AdicionarParamentros("@tipo", tipo);
                conexao.AdicionarParamentros("@dataInicial", dataInicial + " 00:00:01");
                conexao.AdicionarParamentros("@dataFinal", dataFinal + " 23:59:59");
                
                //String de consulta

                string select = "select (select conf_empresa from wms_configuracao where conf_codigo = 1) as nome_empresa, " +
                                "poco_data_ocorrencia, po.poco_codigo, p.ped_codigo, p.ped_nota_fiscal, pg.pag_descricao, p.ped_total, " +
                                "mot1.mot_apelido, v.vei_placa, " +
                                "c.cli_codigo, c.cli_nome, c.cli_fantasia, e.est_uf, cid.cid_nome, b.bar_nome, " +
                                "r.rep_codigo, r.rep_nome, sup_nome, po.oco_codigo, o.oco_descricao, po.poco_area_ocorrencia, po.mani_codigo_ocorrencia, " +
                                "mot1.mot_apelido as motorista_ocorrencia, " +
                                "po.poco_devolucao, po.poco_reentrega, po.poco_observacao, po.poco_status, u.usu_login " +
                                "from wms_pedido p " +
                                "inner join wms_pedido_ocorrencia po " +
                                "on po.ped_codigo = p.ped_codigo " +
                                "inner join wms_tipo_pedido tp " +
                                "on tp.tipo_codigo = p.tipo_codigo " +
                                "inner join wms_pagamento pg " +
                                "on pg.pag_codigo = p.pag_codigo " +
                                "left join wms_manifesto m " +
                                "on m.mani_codigo = p.mani_codigo " +
                                "left join wms_motorista mot1 " +
                                "on mot1.mot_codigo = po.mot_codigo_ocorrencia " +
                                "left join wms_veiculo v " +
                                "on v.vei_codigo = m.vei_codigo " +
                                "inner join wms_cliente c " +
                                "on c.cli_id = p.cli_id " +
                                "inner join wms_bairro b " +
                                "on b.bar_codigo = c.bar_codigo " +
                                "inner join wms_cidade cid " +
                                "on cid.cid_codigo = b.cid_codigo " +
                                "inner join wms_estado e " +
                                "on e.est_codigo = cid.est_codigo " +
                                "left join wms_representante r " +
                                "on r.rep_codigo = p.rep_codigo " +
                                "left join wms_supervisor s " +
                                "on s.equi_codigo = r.equi_codigo " +
                                "left join wms_usuario u " +
                                "on u.usu_codigo = po.usu_codigo_ocorrencia " +
                                "left join wms_tipo_ocorrencia o " +
                                "on o.oco_codigo = po.oco_codigo ";

                if (tipo.Equals("DEVOLUÇÃO"))
                {
                    select += "where poco_devolucao = 'True' and poco_data_ocorrencia between @dataInicial and @dataFinal";
                }

                if (tipo.Equals("PROGRAMADOS"))
                {
                    select += "where poco_reentrega = 'True' and poco_programacao_reentrega between @dataInicial and @dataFinal";
                }

                if (tipo.Equals("REENTREGA"))
                {
                    select += "where poco_reentrega = 'True' and poco_data_ocorrencia between @dataInicial and @dataFinal";
                }

                //Instância um datatable
                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Instância o objêto
                    Ocorrencia ocorrencia = new Ocorrencia();

                    if (linha["nome_empresa"] != DBNull.Value)
                    {
                        ocorrencia.nomeEmpresa = Convert.ToString(linha["nome_empresa"]);
                    }

                    if (linha["poco_data_ocorrencia"] != DBNull.Value)
                    {
                        ocorrencia.dataOcorrencia = Convert.ToDateTime(linha["poco_data_ocorrencia"]);
                    }

                    if (linha["poco_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codOcorrencia = Convert.ToInt32(linha["poco_codigo"]);
                    }

                    if (linha["mani_codigo_ocorrencia"] != DBNull.Value)
                    {
                        ocorrencia.manifestoOcorrencia = Convert.ToInt32(linha["mani_codigo_ocorrencia"]);
                    }                    

                    if (linha["oco_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codTipoOcorrencia = Convert.ToInt32(linha["oco_codigo"]);
                    }

                    if (linha["oco_descricao"] != DBNull.Value)
                    {
                        ocorrencia.descTipoOcorrencia = Convert.ToString(linha["oco_descricao"]);
                    }

                    if (linha["poco_area_ocorrencia"] != DBNull.Value)
                    {
                        ocorrencia.areaOcorrencia = Convert.ToString(linha["poco_area_ocorrencia"]);
                    }

                    if (linha["poco_devolucao"] != DBNull.Value)
                    {
                        ocorrencia.gerarDevolucao = Convert.ToBoolean(linha["poco_devolucao"]);
                    }

                    if (linha["poco_reentrega"] != DBNull.Value)
                    {
                        ocorrencia.gerarReentrega = Convert.ToBoolean(linha["poco_reentrega"]);
                    }

                    if (linha["poco_observacao"] != DBNull.Value)
                    {
                        ocorrencia.obsOcorrencia = Convert.ToString(linha["poco_observacao"]);
                    }

                    if (linha["poco_status"] != DBNull.Value)
                    {
                        ocorrencia.statusOcorrencia = Convert.ToString(linha["poco_status"]);
                    }

                    if (linha["ped_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codPedido = Convert.ToInt32(linha["ped_codigo"]);
                    }

                    if (linha["ped_nota_fiscal"] != DBNull.Value)
                    {
                        ocorrencia.notaFiscal = Convert.ToInt32(linha["ped_nota_fiscal"]);
                    }

                    if (linha["pag_descricao"] != DBNull.Value)
                    {
                        ocorrencia.pagamentoPedido = Convert.ToString(linha["pag_descricao"]);
                    }

                    if (linha["ped_total"] != DBNull.Value)
                    {
                        ocorrencia.totalPedido = Convert.ToDouble(linha["ped_total"]);
                    }

                    if (linha["mot_apelido"] != DBNull.Value)
                    {
                        ocorrencia.motorista = Convert.ToString(linha["mot_apelido"]);
                    }

                    if (linha["vei_placa"] != DBNull.Value)
                    {
                        ocorrencia.veiculoPedido = Convert.ToString(linha["vei_placa"]);
                    }                    

                    if (linha["cli_codigo"] != DBNull.Value)
                    {
                        ocorrencia.codCliente = Convert.ToString(linha["cli_codigo"]);
                    }

                    if (linha["cli_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeCliente = Convert.ToString(linha["cli_nome"]);
                    }

                    if (linha["cli_fantasia"] != DBNull.Value)
                    {
                        ocorrencia.fantasiaCliente = Convert.ToString(linha["cli_fantasia"]);
                    }

                    if (linha["est_uf"] != DBNull.Value)
                    {
                        ocorrencia.ufCliente = Convert.ToString(linha["est_uf"]);
                    }

                    if (linha["cid_nome"] != DBNull.Value)
                    {
                        ocorrencia.cidadeCliente = Convert.ToString(linha["cid_nome"]);
                    }

                    if (linha["bar_nome"] != DBNull.Value)
                    {
                        ocorrencia.bairroCliente = Convert.ToString(linha["bar_nome"]);
                    }

                    if (linha["rep_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeRepresentante = Convert.ToString(linha["rep_nome"]);
                    }

                    if (linha["sup_nome"] != DBNull.Value)
                    {
                        ocorrencia.nomeSupervisor = Convert.ToString(linha["sup_nome"]);
                    }

                    if (linha["usu_login"] != DBNull.Value)
                    {
                        ocorrencia.loginUsuario = Convert.ToString(linha["usu_login"]);
                    }

                    //Controla o período no relatório
                    ocorrencia.dataFaturamento = Convert.ToDateTime(dataInicial);

                    ocorrencia.dataReentrega = Convert.ToDateTime(dataFinal);

                    ocorrenciaCollection.Add(ocorrencia);
                }
                //Retorna a coleção de cadastro encontrada
                return ocorrenciaCollection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao gerar o relatório. \nDetalhes:" + ex.Message);
            }
        }


    }

}

