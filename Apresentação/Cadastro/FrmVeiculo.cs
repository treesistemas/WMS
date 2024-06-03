using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmVeiculo : Form
    {
        //Array com id
        private int[] tipoVeiculo;
        private int[] rastreador;

        //Instância a coleção
        VeiculoTipoCollection veiculoTipoCollection = new VeiculoTipoCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmVeiculo()
        {
            InitializeComponent();
        }

        private void FrmVeiculo_Load(object sender, EventArgs e)
        {
            if (empresaCollection.Count != null)
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

        private void txtPesqPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no pesquisa de proprietário
                cmbPesqProprietario.Focus();
            }
        }

        private void cmbPesqProprietario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão pesquisar
                btnPesquisar.Focus();
            }
        }

        private void cmbProprietario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo rastreador
                cmbRastreador.Focus();
            }
        }

        private void cmbRastreador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo tipo de veiculo
                cmbTipoVeiculo.Focus();
            }
        }

        private void cmbTipoVeiculo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão salvar
                btnSalvar.Focus();
            }
        }

        private void cmbTipoVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Exibe os dados do tipo de veiculo selecionado
            DadosTipoVeiculoCampos();
        }

        private void gridVeiculo_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridVeiculo_MouseClick(object sender, MouseEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridVeiculo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridVeiculo.Rows.Count > 0)
                {
                    //Habilita todos os campos
                    Habilita();
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa os veículos
            PesqVeiculos();

            if (cmbTipoVeiculo.Items.Count == 0)
            {
                //Pesquisa o tipo de veículo
                PesqTipoVeiculo();
            }

            if (cmbRastreador.Items.Count == 0)
            {
                //Pesquisa o rastreador do veículo
                PesqRastreadores();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            /*if (acesso[0].escreverFuncao == false && acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {*/
                //Alterar o cadastro
                Alterar();
            //}
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }


        //Pesquisa os rastreadores
        private void PesqRastreadores()
        {
            try
            {
                //Instância o objeto negocios
                RastreadorNegocios rastreadorNegocios = new RastreadorNegocios();
                //Instância a coleção
                RastreadorCollection rastreadorCollection = new RastreadorCollection();
                //A coleção recebe o resultado da consulta
                rastreadorCollection = rastreadorNegocios.PesqRastreador("", true);
                //Limpa o grid
                cmbRastreador.Items.Clear();
                //Grid Recebe o resultado da coleção
                rastreadorCollection.ForEach(n => cmbRastreador.Items.Add(n.numeroRastreador));

                if (rastreadorCollection.Count > 0)
                {
                    //Define o tamanho do array para o combobox
                    rastreador = new int[rastreadorCollection.Count];

                    for (int i = 0; i < rastreadorCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        rastreador[i] = rastreadorCollection[i].codRastreador;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o tipo veículo
        private void PesqTipoVeiculo()
        {
            try
            {
                //Instância o negocios
                VeiculoTipoNegocios veiculoTipoNegocios = new VeiculoTipoNegocios();

                //A coleção recebe o resultado da consulta
                veiculoTipoCollection = veiculoTipoNegocios.PesqTipoVeiculo();

                if (veiculoTipoCollection.Count > 0)
                {
                    //Limpa o combobox tipo de veículo
                    cmbTipoVeiculo.Items.Clear();
                    //Preenche o combobox veiculo
                    veiculoTipoCollection.ForEach(n => cmbTipoVeiculo.Items.Add(n.descTipoVeiculo));
                    //Seleciona o pimeiro veiculo
                    cmbTipoVeiculo.SelectedIndex = 0;
                    //Foca no campo tipo de veiculo
                    cmbTipoVeiculo.Focus();

                    //Define o tamanho do array para o combobox
                    tipoVeiculo = new int[veiculoTipoCollection.Count];

                    for (int i = 0; i < veiculoTipoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        tipoVeiculo[i] = veiculoTipoCollection[i].codTipoVeiculo;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DadosTipoVeiculoCampos()
        {
            try
            {
                if (!cmbTipoVeiculo.Text.Equals(""))
                {
                    //Localiza o tipo de veiculo na coleção
                    List<VeiculoTipo> veiculo = veiculoTipoCollection.FindAll(delegate (VeiculoTipo n) { return n.descTipoVeiculo == cmbTipoVeiculo.Text; });

                    //Seta o valor da altura
                    txtAltura.Text = string.Format(@"{0:g}", veiculo[0].altura);
                    //Seta o valor da largura
                    txtLargura.Text = string.Format(@"{0:g}", veiculo[0].largura);
                    //Seta o valor do comprimento
                    txtComprimento.Text = string.Format(@"{0:g}", veiculo[0].comprimento);
                    //Seta o valor da cubagem
                    txtCubagem.Text = string.Format(@"{0:0.000}", veiculo[0].cubagem);
                    //Seta o valor do peso
                    txtPeso.Text = string.Format(@"{0:#.00}", veiculo[0].peso);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        //Alterar o cadastro
        private void Alterar()
        {
            try
            {
                if (cmbProprietario.Enabled == false)
                {
                    MessageBox.Show("Por favor, realiaze as alterações desejadas!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (cmbProprietario.Text.Equals("") || cmbTipoVeiculo.Text.Equals(""))
                    {
                        //Mensagem
                        MessageBox.Show("Por favor, preencha todos os campos!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Foca no campo proprietário
                        cmbProprietario.Focus();

                    }
                    else
                    {
                        //Instância o objeto negocios
                        VeiculoNegocios veiculoNegocios = new VeiculoNegocios();
                        //Instância o objeto unidade
                        Veiculo veiculo = new Veiculo();
                        //Seta o código do veículo
                        veiculo.codVeiculo = Convert.ToInt32(txtCodigo.Text);
                        //Seta o proprietário do veículo
                        veiculo.proprietarioVeiculo = Convert.ToString(cmbProprietario.SelectedItem);

                        if (cmbRastreador.Text.Equals("") || cmbRastreador.Text.Equals("0"))
                        {
                            veiculo.codRastreador = 0;
                        }
                        else
                        {
                            //Seta o rastreador do veículo
                            veiculo.codRastreador = rastreador[cmbRastreador.SelectedIndex];
                        }
                        //Seta o tipo do veículo
                        veiculo.codTipo = tipoVeiculo[cmbTipoVeiculo.SelectedIndex];
                        //Passa para a camada de negocios
                        veiculoNegocios.Alterar(cmbEmpresa.Text, veiculo);

                        //Instância as linha do grid
                        DataGridViewRow linha = gridVeiculo.CurrentRow;
                        //Recebe o indice   
                        int indice = linha.Index;
                        //Insere a unidade no grid                      
                        gridVeiculo.Rows[indice].Cells[3].Value = cmbProprietario.Text;
                        gridVeiculo.Rows[indice].Cells[4].Value = cmbRastreador.Text;
                        gridVeiculo.Rows[indice].Cells[5].Value = cmbTipoVeiculo.Text;
                        //Seta a altura
                        gridVeiculo.Rows[indice].Cells[6].Value = txtAltura.Text;
                        //Seta a largura
                        gridVeiculo.Rows[indice].Cells[7].Value = txtLargura.Text;
                        //Seta a comprimento
                        gridVeiculo.Rows[indice].Cells[8].Value = txtComprimento.Text;
                        //Seta a cubagem
                        gridVeiculo.Rows[indice].Cells[9].Value = txtCubagem.Text;
                        //Seta a peso
                        gridVeiculo.Rows[indice].Cells[7].Value = txtPeso.Text;
                        //Foca na tabela
                        gridVeiculo.Focus();
                        //Desabilita todos os campos
                        Desabilita();

                        MessageBox.Show("Cadastro alterado com sucesso! ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Pesquisa os veículos
        private void PesqVeiculos()
        {
            try
            {
                //Instância o objeto negocios
                VeiculoNegocios veiculoNegocios = new VeiculoNegocios();
                //Instância a coleção
                VeiculoCollection veiculoCollection = new VeiculoCollection();
                //A coleção recebe o resultado da consulta
                veiculoCollection = veiculoNegocios.PesqVeiculos(chkPesqAtivo.Checked, txtPesqPlaca.Text, cmbPesqProprietario.Text, cmbEmpresa.Text);
                //Limpa o grid
                gridVeiculo.Rows.Clear();
                //Grid Recebe o resultado da coleção
                veiculoCollection.ForEach(n => gridVeiculo.Rows.Add(n.codVeiculo, n.placaVeiculo, n.anoVeiculo, n.proprietarioVeiculo,
                    n.numeroRastreador, n.descTipo, n.alturaVeiculo, n.larguraVeiculo, n.comprimentoVeiculo, n.cubagemVeiculo, n.pesoVeiculo, n.statusVeiculo));

                if (veiculoCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();
                    //Qtd encontrada
                    lblQtd.Text = gridVeiculo.RowCount.ToString();
                    //Seleciona a primeira linha do grid
                    gridVeiculo.CurrentCell = gridVeiculo.Rows[0].Cells[1];
                    //Foca no grid
                    gridVeiculo.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum veiculo encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Seta os dados nos campos
        private void DadosCampos()
        {
            try
            {
                if (gridVeiculo.Rows.Count > 0)
                {
                    //Instância as linha do grid
                    DataGridViewRow linha = gridVeiculo.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[0].Value);
                    //Seta a placa
                    txtPlaca.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[1].Value);
                    //Seta o ano
                    txtAno.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[2].Value);
                    //Seta o proprietário
                    cmbProprietario.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[3].Value);
                    //Seta o rastreador
                    cmbRastreador.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[4].Value);
                    //Seta o tipo do veículo
                    cmbTipoVeiculo.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[5].Value);
                    //Seta a altura
                    txtAltura.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[6].Value);
                    //Seta a largura
                    txtLargura.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[7].Value);
                    //Seta a comprimento
                    txtComprimento.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[8].Value);
                    //Seta a cubagem
                    txtCubagem.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[9].Value);
                    //Seta a peso
                    txtPeso.Text = Convert.ToString(gridVeiculo.Rows[indice].Cells[10].Value);
                    //Seta o status
                    chkAtivo.Checked = Convert.ToBoolean(gridVeiculo.Rows[indice].Cells[11].Value);

                    //Desabilita todos os campos
                    Desabilita();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n " + ex, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Habilita os campos
        private void Habilita()
        {
            //Habilita o campo proprietário
            cmbProprietario.Enabled = true;
            //Habilita o campo rastreador
            cmbRastreador.Enabled = true;
            //Habilita o campo tipo do veículo
            cmbTipoVeiculo.Enabled = true;

            //Foca no campo proprietario
            cmbProprietario.Focus();
        }

        //Desabilita os campos
        private void Desabilita()
        {
            //Desabilita o campo proprietário
            cmbProprietario.Enabled = false;
            //Desabilita o campo rastreador
            cmbRastreador.Enabled = false;
            //Desabilita o campo tipo do veículo
            cmbTipoVeiculo.Enabled = false;
        }


    }
}
