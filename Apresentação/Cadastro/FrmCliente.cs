using System;
using System.Windows.Forms;
using ObjetoTransferencia;
using Negocios;
using System.Collections.Generic;

namespace Wms
{
    public partial class FrmCliente : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public List<Empresa> empresaCollection;

        public FrmCliente()
        {
            InitializeComponent();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            if (empresaCollection.Count > 0)
            {
                //Preenche o combobox região
                empresaCollection.ForEach(n => cmbEmpresa.Items.Add(n.siglaEmpresa));
                //Seleciona a primeira empresa
                cmbEmpresa.SelectedIndex = 0;

                //Verifica se existe mais de uma empresa
                if (empresaCollection[0].multiEmpresa == false)
                {
                    cmbEmpresa.Enabled = false;

                }
                else
                {
                    cmbEmpresa.Enabled = true;
                }
            }
        }

        private void txtPesqCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo cnpj
                txtPesqCnpj.Focus();
            }
        }

        private void txtCodClienteUnificar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa o clienet
                UnificarCliente();
            }
        }

        private void txtPesqCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo razão social
                txtPesqRazaoSocial.Focus();
            }
        }

        private void txtPesqRazaoSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão
                btnPesquisar.Focus();
            }
        }

        private void chkControlaValidade_CheckedChanged(object sender, EventArgs e)
        {
            if (chkControlaValidade.Checked == true)
            {
                //exibe o label e o campo dias
                lblDiasValidade.Visible = true;
                nmrDiasValidade.Visible = true;
            }
            else
            {
                //esconde o label e o campo dias
                lblDiasValidade.Visible = false;
                nmrDiasValidade.Visible = false;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o cliente
            PesqCliente();
        }

        private void gridCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Exibe os dados no campos
            DadosCampos();
        }

        private void gridCliente_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados no campos
            DadosCampos();
        }

        private void gridCliente_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (perfilUsuario == "ADMINISTRADOR" || acesso[0].editarFuncao == true)
                {
                    if (gridCliente.Rows.Count > 0)
                    {
                        //Habilita os campos
                        Habilita();
                    }
                }
            }
        }

        private void mniRemoverUnificacao_Click(object sender, EventArgs e)
        {
            //Remove a unificação
            RemoverUnificacao();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Alterar às obsevações
                Alterar();
            }
            else if (acesso[0].escreverFuncao == false && acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Alterar às obsevações
                Alterar();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //se meu checkbox estiver ativado
            if (chkAgendamento.Enabled == true)
            {
                if (MessageBox.Show("Você deseja realmente cancelar essa operação?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //desabilitar os campos 
                    Desabilita();
                    //Exibir novamente os dados nos campos
                    DadosCampos();
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //sai da tela
            Close();
        }


        //Pesquisa o cliente
        private void PesqCliente()
        {
            try
            {
                if (txtPesqCodigo.Text == string.Empty && txtPesqRazaoSocial.Text == string.Empty && System.Text.RegularExpressions.Regex.Replace(txtPesqCnpj.Text, "[^0-9]", "") == string.Empty)
                {
                    MessageBox.Show("Preencha pelo menos um campo para executar a pesquisa de cliente!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //foca no campo código
                    txtPesqCodigo.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    ClienteNegocios clienteNegocios = new ClienteNegocios();
                    //Instância o objeto coleção
                    ClienteCollection clienteCollection = new ClienteCollection();
                    //A coleção recebe o resultado da consulta
                    clienteCollection = clienteNegocios.PesqCliente(txtPesqCodigo.Text, System.Text.RegularExpressions.Regex.Replace(txtPesqCnpj.Text, "[^0-9]", ""), txtPesqRazaoSocial.Text, chkPesqAtivo.Checked,cmbEmpresa.Text);
                    //Limpa o grid
                    gridCliente.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    clienteCollection.ForEach(n => gridCliente.Rows.Add(n.idCliente, n.codCliente, n.nomeCliente, n.fantasiaCliente, n.cnpjCliente, n.enderecoCliente,
                        n.numeroCliente, n.bairroCliente, n.cidadeCliente, n.ufCliente, n.rotaCliente, n.seqEntregaCliente, n.foneCliente, n.celularCliente,
                        n.emailCliente, n.agendamentoCliente, n.auditoriaPedido, n.caixaFechadaCliente, n.compartilhado, n.validadeCliente, n.diasValidadeCliente,
                        n.paletizadoCliente, n.naoDividirCarga, n.observacaoCliente, n.statusCliente));

                    if (clienteCollection.Count > 0)
                    {
                        //Seta os dados nos campos
                        DadosCampos();

                        //Qtd de categoria encontrada
                        lblQtd.Text = gridCliente.RowCount.ToString();

                        //Seleciona a primeira linha do grid
                        gridCliente.CurrentCell = gridCliente.Rows[0].Cells[1];
                        //Foca no grid
                        gridCliente.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum cliente encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Exibe os dados nos campos
        private void DadosCampos()
        {
            try
            {
                if (gridCliente.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridCliente.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o código
                    txtCodigo.Text = Convert.ToInt32(gridCliente.Rows[indice].Cells[1].Value).ToString();
                    //Seta a razão social
                    txtRazaoSocial.Text = Convert.ToString(gridCliente.Rows[indice].Cells[2].Value);
                    //Seta a fantasia
                    txtFantasia.Text = Convert.ToString(gridCliente.Rows[indice].Cells[3].Value);
                    //Seta o cnpj
                    txtCnpj.Text = Convert.ToString(gridCliente.Rows[indice].Cells[4].Value);
                    //Seta o endereço
                    txtEndereco.Text = Convert.ToString(gridCliente.Rows[indice].Cells[5].Value);
                    //Seta o número
                    txtNumero.Text = Convert.ToString(gridCliente.Rows[indice].Cells[6].Value);
                    //Seta o bairro
                    txtBairro.Text = Convert.ToString(gridCliente.Rows[indice].Cells[7].Value);
                    //Seta a cidade
                    txtCidade.Text = Convert.ToString(gridCliente.Rows[indice].Cells[8].Value);
                    //Seta a uf do cliente
                    txtUF.Text = Convert.ToString(gridCliente.Rows[indice].Cells[9].Value);
                    //Seta a rota do cliente
                    txtRota.Text = Convert.ToString(gridCliente.Rows[indice].Cells[10].Value);
                    //Seta a sequencia de entrega do cliente
                    txtSequencia.Text = Convert.ToString(gridCliente.Rows[indice].Cells[11].Value);
                    //Seta o fone
                    txtFone.Text = Convert.ToString(gridCliente.Rows[indice].Cells[12].Value);
                    //Seta o celular
                    txtCelular.Text = Convert.ToString(gridCliente.Rows[indice].Cells[13].Value);
                    //Seta o email
                    txtEmail.Text = Convert.ToString(gridCliente.Rows[indice].Cells[14].Value);
                    //Seta o agendamento                      
                    chkAgendamento.Checked = Convert.ToBoolean(gridCliente.Rows[indice].Cells[15].Value);
                    //Seta a auditoria                     
                    chkAuditar.Checked = Convert.ToBoolean(gridCliente.Rows[indice].Cells[16].Value);
                    //Seta a caixa fechada
                    chkCaixaFechada.Checked = Convert.ToBoolean(gridCliente.Rows[indice].Cells[17].Value);
                    //Seta o compartilhado                    
                    chkCompartilhada.Checked = Convert.ToBoolean(gridCliente.Rows[indice].Cells[18].Value);
                    //Seta o controle de validade
                    chkControlaValidade.Checked = Convert.ToBoolean(gridCliente.Rows[indice].Cells[19].Value);
                    //Seta o dias de validade
                    nmrDiasValidade.Value = Convert.ToInt32(gridCliente.Rows[indice].Cells[20].Value);
                    //Seta o paletizado
                    chkPaletizado.Checked = Convert.ToBoolean(gridCliente.Rows[indice].Cells[21].Value);
                    //Seta o não dividir carga                     
                    chkNaoDividirCarga.Checked = Convert.ToBoolean(gridCliente.Rows[indice].Cells[22].Value);
                    //Seta a observação
                    txtObservacao.Text = Convert.ToString(gridCliente.Rows[indice].Cells[23].Value);
                    //Status do cliente
                    chkAtivo.Checked = Convert.ToBoolean(gridCliente.Rows[indice].Cells[24].Value);

                    //Desabilita todos os campos
                    Desabilita();

                    //Instância o objeto coleção
                    ClienteCollection clienteUnificadoCollection = new ClienteCollection();
                    ClienteNegocios clienteNegocios = new ClienteNegocios();
                    //Limpa o grid
                    gridClienteUnificar.Rows.Clear();
                    //Pesquisa os cliente unificados
                    clienteUnificadoCollection = clienteNegocios.PesqClienteUnificado(Convert.ToInt32(gridCliente.Rows[gridCliente.CurrentCell.RowIndex].Cells[0].Value));

                    if (clienteUnificadoCollection.Count > 0)
                    {
                        clienteUnificadoCollection.ForEach(n => gridClienteUnificar.Rows.Add(n.idCliente, n.codCliente, n.nomeCliente, n.cnpjCliente));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n " + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Salva as observações
        private void Alterar()
        {
            try
            {
                if ((txtCodigo.Text == ""))
                {
                    //Mensagem
                    MessageBox.Show("Por favor realize uma pesquisa!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo código
                    txtPesqCodigo.Focus();
                }
                //verifica se os campos estão ativos
                else if (chkAgendamento.Enabled == false)
                {
                    //Mensagem
                    MessageBox.Show("Nenhuma alteração realizada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (chkControlaValidade.Checked == true && nmrDiasValidade.Value < 30)
                {
                    MessageBox.Show("O controle de validade não pode ser menor que 30 dias", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo código
                    nmrDiasValidade.Focus();
                }
                else
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridCliente.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Instância o objeto negocios
                    ClienteNegocios clienteNegocios = new ClienteNegocios();
                    //Passa a categoria para a camada de negocios
                    clienteNegocios.Alterar(cmbEmpresa.Text, (int)gridCliente.Rows[indice].Cells[0].Value, chkAgendamento.Checked, chkAuditar.Checked, chkControlaValidade.Checked, (int)nmrDiasValidade.Value, chkCaixaFechada.Checked, chkCompartilhada.Checked, chkPaletizado.Checked, chkNaoDividirCarga.Checked, txtObservacao.Text);

                    //Altera o agendamento no grid                      
                    gridCliente.Rows[indice].Cells[15].Value = chkAgendamento.Checked;
                    //Altera o auditar no grid                      
                    gridCliente.Rows[indice].Cells[16].Value = chkAuditar.Checked;
                    //altera a caixa fechada
                    gridCliente.Rows[indice].Cells[17].Value = chkCaixaFechada.Checked;
                    //Altera o agendamento no grid                      
                    gridCliente.Rows[indice].Cells[18].Value = chkCompartilhada.Checked;
                    //Atera o controle de validade
                    gridCliente.Rows[indice].Cells[19].Value = chkControlaValidade.Checked;
                    //altera o dias de validade
                    gridCliente.Rows[indice].Cells[20].Value = nmrDiasValidade.Value;
                    //altera o paletizado
                    gridCliente.Rows[indice].Cells[21].Value = chkPaletizado.Checked;
                    //Altera o não dive carga no grid                      
                    gridCliente.Rows[indice].Cells[22].Value = chkNaoDividirCarga.Checked;
                    //altera a observação
                    gridCliente.Rows[indice].Cells[23].Value = txtObservacao.Text;

                    //Foca no grid
                    gridCliente.Focus();
                    //Desabilita todos os campos
                    Desabilita();

                    MessageBox.Show("Observações salva com sucesso! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa e unifica o cliente
        private void UnificarCliente()
        {
            try
            {
                if (txtCodClienteUnificar.Text == string.Empty)
                {
                    MessageBox.Show("Digite o código do cliente!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //foca no campo código
                    txtCodClienteUnificar.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    ClienteNegocios clienteNegocios = new ClienteNegocios();
                    //Instância o objeto coleção
                    ClienteCollection clienteCollection = new ClienteCollection();
                    //A coleção recebe o resultado da consulta
                    clienteCollection = clienteNegocios.PesqCliente(txtCodClienteUnificar.Text, string.Empty, string.Empty, true, string.Empty);


                    if (clienteCollection.Count > 0)
                    {
                        //Instância as linha da tabela
                        DataGridViewRow linha = gridCliente.CurrentRow;
                        //Recebe o indice   
                        int indice = linha.Index;

                        if (Convert.ToInt32(gridCliente.Rows[indice].Cells[0].Value) == clienteCollection[0].idCliente)
                        {
                            MessageBox.Show("Operação não realizada: Não pode inserir o mesmo cliente!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //Seleciona o texto do campo
                            txtCodClienteUnificar.SelectAll();
                        }
                        else
                        {
                            bool clienteDigitado = false;

                            for (int i = 0; gridClienteUnificar.Rows.Count > i; i++)
                            {
                                if (Convert.ToInt32(gridClienteUnificar.Rows[i].Cells[0].Value) == clienteCollection[0].idCliente)
                                {
                                    clienteDigitado = true;
                                }
                            }

                            if (clienteDigitado == true)
                            {
                                MessageBox.Show("Cliente já digitado!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //Seleciona o texto do campo
                                txtCodClienteUnificar.SelectAll();
                            }
                            else
                            {
                                //Grid Recebe o resultado da coleção
                                clienteCollection.ForEach(n => gridClienteUnificar.Rows.Add(n.idCliente, n.codCliente, n.nomeCliente, n.cnpjCliente));

                                //Unifica o cliente
                                clienteNegocios.AtualizarUnificacao(clienteCollection[0].idCliente, Convert.ToInt32(gridCliente.Rows[indice].Cells[0].Value));

                                //Seleciona a primeira linha do grid
                                gridClienteUnificar.CurrentCell = gridClienteUnificar.Rows[0].Cells[1];
                                //Limpa o campo
                                txtCodClienteUnificar.Text = string.Empty;
                                //Foca no grid
                                txtCodClienteUnificar.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhum cliente encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Remove a unificação
        private void RemoverUnificacao()
        {
            try
            {
                if (gridClienteUnificar.SelectedRows.Count == 0)
                {
                    //Mensagem
                    MessageBox.Show("Por favor selecione um cliente para remoção!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo código
                    txtCodClienteUnificar.Focus();
                }
                else
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridClienteUnificar.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Instância o objeto negocios
                    ClienteNegocios clienteNegocios = new ClienteNegocios();
                    //Atualiza a unificação
                    clienteNegocios.AtualizarUnificacao(Convert.ToInt32(gridClienteUnificar.Rows[indice].Cells[0].Value), null);
                    //Remove a linha do grid
                    gridClienteUnificar.Rows.RemoveAt(indice);

                    MessageBox.Show("Unificação removida com sucesso! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Habilita()
        {
            //Habilita o checkbox agendamento
            chkAgendamento.Enabled = true;
            //Habita o checkbox auditar
            chkAuditar.Enabled = true;
            //Habilita o checkbox caixa fechada
            chkCaixaFechada.Enabled = true;
            //Habilita o checkbox controle de validade
            chkControlaValidade.Enabled = true;
            //Habita o checkbox compartilhado
            chkCompartilhada.Enabled = true;
            //Habilita o campo dias de validade
            nmrDiasValidade.Enabled = true;
            //Habilita o checkbox paletizado
            chkPaletizado.Enabled = true;
            //Habita o checkbox não dividir carga
            chkNaoDividirCarga.Enabled = true;
            //Habilita o campo observação
            txtObservacao.Enabled = true;
            //Habilita o campo
            txtCodClienteUnificar.Enabled = true;
            //Habilita o campo
            gridClienteUnificar.Enabled = true;
        }

        private void Desabilita()
        {
            //desabilita o checkbox agendamento
            chkAgendamento.Enabled = false;
            //desabilita o checkbox auditar
            chkAuditar.Enabled = false;
            //desabilita o checkbox caixa fechada
            chkCaixaFechada.Enabled = false;
            //desabilita o checkbox controle de validade
            chkControlaValidade.Enabled = false;
            //desabilita o checkbox compartilhado
            chkCompartilhada.Enabled = false;
            //desabilita o campo dias de validade
            nmrDiasValidade.Enabled = false;
            //desabilita o checkbox paletizado
            chkPaletizado.Enabled = false;
            //desabilita o checkbox não dividir carga
            chkNaoDividirCarga.Enabled = false;
            //desabilita o campo observação
            txtObservacao.Enabled = false;
            //desabilita o campo
            txtCodClienteUnificar.Enabled = false;
            //desabilita o campo
            gridClienteUnificar.Enabled = false;
        }

        
    }
}
