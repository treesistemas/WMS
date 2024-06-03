using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmCaixa : Form
    {
        //Controle para salvar e alterar (false = altera)
        bool opcao = false;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmCaixa()
        {
            InitializeComponent();
        }

        //KeyPress
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

        //Changed
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

        //KeyUp
        private void gridCaixa_KeyUp(object sender, KeyEventArgs e)
        {
            //exibe os dados nos campos
            DadosCampos();
        }

        //Mouse e Doubleclick
        private void gridCaixa_MouseClick(object sender, MouseEventArgs e)
        {
            //exibe os dados nos campos
            DadosCampos();
        }

        private void gridCaixa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (perfilUsuario == "ADMINISTRADOR" || acesso[0].editarFuncao == true)
                {
                    if (gridCaixa.Rows.Count > 0)
                    {
                        //Controle para alterar  
                        opcao = false;
                        //Habilita todos os campos
                        HabilitarCampos();
                    }
                }
            }

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa as caixas
            PesqCaixa();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Pesquisa o id
                PesqId();
            }
            else if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Pesquisa o id
                PesqId();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (opcao == true)
            {
                if (perfilUsuario == "ADMINISTRADOR")
                {
                    //Salva o cadastro
                    Salvar();
                }
                else if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Salva o cadastro
                    Salvar();
                }
            }
            else
            {
                if (perfilUsuario == "ADMINISTRADOR")
                {
                    //Alterar o cadastro
                    Alterar();
                }
                else if (acesso[0].editarFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Alterar o cadastro
                    Alterar();
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }

        //Pesquisa o ultimo id da categoria
        private void PesqId()
        {
            try
            {
                //Limpa todos os campos
                LimparCampos();
                //Instância a camada de negocios
                CaixaNegocios caixaNegocios = new CaixaNegocios();
                //Instância a camada de objetos 
                Caixa caixa = new Caixa();
                //Recebe o id
                caixa = caixaNegocios.PesqId();
                //Seta o novo id
                txtCodigo.Text = caixa.codCaixa.ToString();
                //Controle para salvar 
                opcao = true;
                //Habilita os campos
                HabilitarCampos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa as caixas
        private void PesqCaixa()
        {
            try
            {
                //Instância a camada de negocios
                CaixaNegocios caixaNegocios = new CaixaNegocios();
                //Instância a camada de objetos 
                CaixaCollection caixaCollection = new CaixaCollection();
                //A coleção recebe o resultado da consulta
                caixaCollection = caixaNegocios.PesqCaixa();
                //Limpa o grid
                gridCaixa.Rows.Clear();
                //Grid Recebe o resultado da coleção
                caixaCollection.ForEach(n => gridCaixa.Rows.Add(n.codCaixa, n.descCaixa, n.altura, n.largura, n.comprimento, n.peso));

                if (caixaCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();

                    //Qtd de categoria encontrada
                    lblQtd.Text = gridCaixa.RowCount.ToString();

                    //Seleciona a primeira linha do grid
                    gridCaixa.CurrentCell = gridCaixa.Rows[0].Cells[1];
                    //Foca no grid
                    gridCaixa.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum modelo de caixa encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (gridCaixa.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridCaixa.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridCaixa.Rows[indice].Cells[0].Value.ToString();
                    //Seta o valor da descrição
                    txtDescricao.Text = gridCaixa.Rows[indice].Cells[1].Value.ToString();
                    //Seta o valor da altura
                    txtAltura.Text = string.Format(@"{0:N}", gridCaixa.Rows[indice].Cells[2].Value);
                    //Seta o valor da largura
                    txtLargura.Text = string.Format(@"{0:N}", gridCaixa.Rows[indice].Cells[3].Value);
                    //Seta o valor do comprimento
                    txtComprimento.Text = string.Format(@"{0:N}", gridCaixa.Rows[indice].Cells[4].Value);
                    //Seta o valor do peso
                    txtPeso.Text = string.Format(@"{0:0.000}", gridCaixa.Rows[indice].Cells[5].Value);

                    //Desabilita todos os campos
                    DesabilitaCampos();
                    //Controle para alterar
                    opcao = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    //Instância a camada de negocios
                    CaixaNegocios caixaNegocios = new CaixaNegocios();
                    //Instacia a camada objeto
                    Caixa caixa = new Caixa();

                    caixa.codCaixa = Convert.ToInt32(txtCodigo.Text);
                    caixa.descCaixa = txtDescricao.Text;
                    caixa.altura = Convert.ToDouble(string.Format(@"{0:N}", txtAltura.Text));
                    caixa.largura = Convert.ToDouble(string.Format(@"{0:N}", txtLargura.Text));
                    caixa.comprimento = Convert.ToDouble(string.Format(@"{0:N}", txtComprimento.Text));
                    caixa.cubagem = Convert.ToDouble(string.Format(@"{0:0.000}", txtCubagem.Text));
                    caixa.peso = Convert.ToDouble(string.Format(@"{0:0.000}", txtPeso.Text));

                    // Passa o objeto para a camada de negocios
                    caixaNegocios.Salvar(caixa);

                    //Insere o cadastro no grid
                    gridCaixa.Rows.Add(txtCodigo.Text, txtDescricao.Text, txtAltura.Text, txtLargura.Text, txtComprimento.Text, txtPeso.Text);

                    //Recebe a qtd de linha no grid 
                    int linha = Convert.ToInt32(gridCaixa.RowCount.ToString()) - 1;

                    //Qtd de caixa encontrada
                    lblQtd.Text = gridCaixa.RowCount.ToString();
                    //Seleciona a linha      
                    gridCaixa.CurrentCell = gridCaixa.Rows[linha].Cells[1];
                    //Desabilita todos os campos
                    DesabilitaCampos();
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
                    //Instância a camada de negocios
                    CaixaNegocios caixaNegocios = new CaixaNegocios();
                    //Instacia a camada objeto
                    Caixa caixa = new Caixa();

                    caixa.codCaixa = Convert.ToInt32(txtCodigo.Text);
                    caixa.descCaixa = txtDescricao.Text;
                    caixa.altura = Convert.ToDouble(txtAltura.Text);
                    caixa.largura = Convert.ToDouble(txtLargura.Text);
                    caixa.comprimento = Convert.ToDouble(txtComprimento.Text);
                    caixa.cubagem = Convert.ToDouble(txtCubagem.Text);
                    caixa.peso = Convert.ToDouble(txtPeso.Text);

                    //Passa o objeto para a camada de negocios
                    caixaNegocios.Alterar(caixa);

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridCaixa.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Altera a descrição no grid                      
                    gridCaixa.Rows[indice].Cells[1].Value = txtDescricao.Text;
                    //Atera o controle de auditoria
                    gridCaixa.Rows[indice].Cells[2].Value = txtAltura.Text;
                    //altera o controle de validade
                    gridCaixa.Rows[indice].Cells[3].Value = txtLargura.Text;
                    //altera o controle de lote
                    gridCaixa.Rows[indice].Cells[4].Value = txtComprimento.Text;
                    //altera o controle de lote
                    gridCaixa.Rows[indice].Cells[5].Value = txtPeso.Text;

                    //Foca na tabela
                    gridCaixa.Focus();
                    //Desabilita todos os campos
                    DesabilitaCampos();


                    MessageBox.Show("Cadastro alterado com sucesso! ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Limpa todos os campos
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

        //Habilita os campos
        private void HabilitarCampos()
        {
            //Habilita o campo da descrição
            txtDescricao.Enabled = true;
            //Habilita o campo da altura
            txtAltura.Enabled = true;
            //Habilita o campo da largura
            txtLargura.Enabled = true;
            //Habilita o campo do comprimento
            txtComprimento.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;
            //Desabilita o botão novo
            btnNovo.Enabled = false;

            //Foca no campo descrição
            txtDescricao.Focus();
            //Seleciona o campo descrição
            txtDescricao.SelectAll();
        }

        //Desabilita os campos
        private void DesabilitaCampos()
        {
            //Desabilita o campo da descrição
            txtDescricao.Enabled = false;
            //Desabilita o campo da altura
            txtAltura.Enabled = false;
            //Desabilita o campo da largura
            txtLargura.Enabled = false;
            //Desabilita o campo do comprimento
            txtComprimento.Enabled = false;
            //desabilita o botão
            btnSalvar.Enabled = false;
            //desabilita o botão novo
            btnNovo.Enabled = true;

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
                        double r = (Convert.ToDouble(txtAltura.Text) * Convert.ToDouble(txtLargura.Text) * Convert.ToDouble(txtComprimento.Text));

                        //Rsultado das dimensões da caixa
                        txtCubagem.Text = string.Format(@"{0:0.00000}", r);

                        //300 fator de densidade da caixa
                        txtPeso.Text = string.Format(@"{0:0.000}", r * 300);
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