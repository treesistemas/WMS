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
    public partial class FrmVeiculoTipo : Form
    {
        //Array com id
        private int[] veiculo;

        //Controle para salvar e alterar (false = altera)
        bool opcao = false;

        //Instância a coleção
        VeiculoTipoCollection veiculoTipoCollection = new VeiculoTipoCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmVeiculoTipo()
        {
            InitializeComponent();
        }

        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtAltura.Focus();
            }
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtLargura.Focus();
            }
        }

        private void txtLargura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtComprimento.Focus();
            }
        }

        private void txtComprimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtPeso.Focus();
            }
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                btnSalvar.Focus();
            }
        }

        private void cmbTipoVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //exibe os dados nos campos
            DadosCampos();
        }

        private void txtAltura_TextChanged(object sender, EventArgs e)
        {
            //Calcula a cubagem
            CalcularCubagem();
        }

        private void txtLargura_TextChanged(object sender, EventArgs e)
        {
            //Calcula a cubagem
            CalcularCubagem();
        }

        private void txtComprimento_TextChanged(object sender, EventArgs e)
        {
            //Calcula a cubagem
            CalcularCubagem();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o tipo veículo
            PesqTipoVeiculo();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
           /* if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {*/
                //Pesquisa o id
                PesqId();
            //}
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (opcao == true)
            {
               /* if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {*/
                    //Salva o cadastro
                    Salvar();
                //}
            }
            else
            {
               /* if (acesso[0].editarFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {*/
                    //Alterar o cadastro
                    Alterar();
                //}
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }

        //Pesquisa o ultimo id
        private void PesqId()
        {
            try
            {
                //Limpa todos os campos
                LimparCampos();
                //Instância o negocios
                VeiculoTipoNegocios veiculoTipoNegocios = new VeiculoTipoNegocios();
                //Instância a tipo de veiculo
                VeiculoTipo veiculoTipo = new VeiculoTipo();
                //Recebe o id
                veiculoTipo = veiculoTipoNegocios.PesqId();
                //Seta o novo id
                txtCodigo.Text = veiculoTipo.codTipoVeiculo.ToString();
                //Controle para salvar 
                opcao = true;
                //Habilita componentes
                //Habilita();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Salvar()
        {
            try
            {
                if (txtDescricao.Text == "" || txtAltura.Text == "" || txtLargura.Text == "" || txtComprimento.Text == "" || txtPeso.Text.Equals(""))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Convert.ToDouble(txtAltura.Text).Equals(0) || Convert.ToDouble(txtLargura.Text).Equals(0) || Convert.ToDouble(txtComprimento.Text).Equals(0) || Convert.ToDouble(txtPeso.Text).Equals(0))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto negocios
                    VeiculoTipoNegocios veiculoTipoNegocios = new VeiculoTipoNegocios();
                    //Instacia o objeto de tranferência
                    VeiculoTipo veiculoTipo = new VeiculoTipo();

                    veiculoTipo.codTipoVeiculo = Convert.ToInt32(txtCodigo.Text);
                    veiculoTipo.descTipoVeiculo = txtDescricao.Text;
                    veiculoTipo.altura = Convert.ToDouble(txtAltura.Text);
                    veiculoTipo.largura = Convert.ToDouble(txtLargura.Text);
                    veiculoTipo.comprimento = Convert.ToDouble(txtComprimento.Text);
                    veiculoTipo.cubagem = Convert.ToDouble(txtCubagem.Text);
                    veiculoTipo.peso = Convert.ToDouble(txtPeso.Text);

                    // Passa o objeto para a camada de negocios
                    veiculoTipoNegocios.Salvar(veiculoTipo);

                    //Pesquisa os tipos de veículos
                    PesqTipoVeiculo();

                    //Seleciona a linha      
                    cmbTipoVeiculo.SelectedItem = txtDescricao.Text;
                    //Foca no campo tipo de veículo
                    cmbTipoVeiculo.Focus();

                    //controle para alterar
                    opcao = false;

                    MessageBox.Show("Cadastro realizado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro realizar o cadastro! \nDetalhes: " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Alterar()
        {
            try
            {
                if (txtCodigo.Text == "")
                {
                    MessageBox.Show("Por favor, selecione um tipo de veículo!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else if (txtDescricao.Text == "" || txtAltura.Text == "" || txtLargura.Text == "" || txtComprimento.Text == "" || txtPeso.Text.Equals(""))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Convert.ToDouble(txtAltura.Text).Equals(0) || Convert.ToDouble(txtLargura.Text).Equals(0) || Convert.ToDouble(txtComprimento.Text).Equals(0) || Convert.ToDouble(txtPeso.Text).Equals(0))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto negocios
                    VeiculoTipoNegocios veiculoTipoNegocios = new VeiculoTipoNegocios();
                    //Instacia o objeto de tranferência
                    VeiculoTipo veiculoTipo = new VeiculoTipo();

                    veiculoTipo.codTipoVeiculo = Convert.ToInt32(txtCodigo.Text);
                    veiculoTipo.descTipoVeiculo = txtDescricao.Text;
                    veiculoTipo.altura = Convert.ToDouble(txtAltura.Text);
                    veiculoTipo.largura = Convert.ToDouble(txtLargura.Text);
                    veiculoTipo.comprimento = Convert.ToDouble(txtComprimento.Text);
                    veiculoTipo.cubagem = Convert.ToDouble(txtCubagem.Text);
                    veiculoTipo.peso = Convert.ToDouble(txtPeso.Text);

                    // Passa o objeto para a camada de negocios
                    veiculoTipoNegocios.Alterar(veiculoTipo);

                    //Pesquisa os tipos de veículos
                    PesqTipoVeiculo();

                    //Seleciona a linha      
                    cmbTipoVeiculo.SelectedItem = txtDescricao.Text;
                    //Foca no campo tipo de veículo
                    cmbTipoVeiculo.Focus();


                    MessageBox.Show("Cadastro alterado com sucesso! ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    //Qtd de itens encontrado
                    lblQtd.Text = cmbTipoVeiculo.Items.Count.ToString();

                    //Exibe os dados
                    DadosCampos();

                    //Define o tamanho do array para o combobox
                    veiculo = new int[veiculoTipoCollection.Count];

                    for (int i = 0; i < veiculoTipoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        veiculo[i] = veiculoTipoCollection[i].codTipoVeiculo;
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum tipo de veículo encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DadosCampos()
        {
            try
            {
                if (!cmbTipoVeiculo.Text.Equals(""))
                {
                    //Localiza o tipo de veiculo na coleção
                    List<VeiculoTipo> veiculo = veiculoTipoCollection.FindAll(delegate (VeiculoTipo n) { return n.descTipoVeiculo == cmbTipoVeiculo.Text; });

                    //Seta o valor do código
                    txtCodigo.Text = Convert.ToString(veiculo[0].codTipoVeiculo);
                    //Seta o valor da descrição
                    txtDescricao.Text = Convert.ToString(veiculo[0].descTipoVeiculo);
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

        private void LimparCampos()
        {
            //Limpa o valor do código
            txtCodigo.Clear();
            //Limpa o valor da descrição
            txtDescricao.Clear();
            //Limpa o valor da altura
            txtAltura.Text = "0";
            //Limpa o valor da largura
            txtLargura.Text = "0";
            //Limpa o valor do comprimento
            txtComprimento.Text = "0";
            //Limpa o valor da cubagem
            txtCubagem.Text = "0";
            //Limpa o valor do peso
            txtPeso.Text = "0";

            //Foca no campo descrição
            txtDescricao.Focus();
        }

        //Calcula a cubagem
        private void CalcularCubagem()
        {
            try
            {
                if (txtAltura.Text != "0" && txtLargura.Text != "0" && txtComprimento.Text != "0")
                {
                    if (txtAltura.Text != "" && txtLargura.Text != "" && txtComprimento.Text != "")
                    {
                        double r = (Convert.ToDouble(txtAltura.Text) * Convert.ToDouble(txtComprimento.Text)) * Convert.ToDouble(txtLargura.Text);

                        txtCubagem.Text = string.Format(@"{0:N}", r);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, verifique os valores informados! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

        }

    }
}