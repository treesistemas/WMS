using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmMotorista : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmMotorista()
        {
            InitializeComponent();
        }

        private void FrmMotorista_Load(object sender, EventArgs e)
        {
            if (empresaCollection != null)
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
                //foca no campo apelido
                txtPesqApelido.Focus();
            }
        }

        private void txtPesqApelido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no botão de pesquisa
                btnPesquisar.Focus();
            }
        }

        private void gridMotorista_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void gridMotorista_MouseClick(object sender, MouseEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o motorista
            PesqMotorista();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o form
            Close();
        }

        //Pesquisa o motorista
        private void PesqMotorista()
        {
            try
            {
                //Instância a camada de negocios
                MotoristaNegocios motoristaNegocios = new MotoristaNegocios();
                //Instância a camada de objeto
                MotoristaCollection motoristaCollection = new MotoristaCollection();
                //A coleção recebe o resultado da consulta
                motoristaCollection = motoristaNegocios.PesqMotorista(cmbEmpresa.Text, txtPesqCodigo.Text, txtPesqApelido.Text, chkPesqAtivo.Checked);
                //Limpa o grid
                gridMotorista.Rows.Clear();
                //Grid Recebe o resultado da coleção
                motoristaCollection.ForEach(n => gridMotorista.Rows.Add(n.codMotorista, n.nomeMotorista, n.apelidoMotorista, n.CNHMotorista, n.validadeCNH, n.celularMotorista, n.statusMotorista));

                if (motoristaCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();

                    //Qtd de categoria encontrada
                    lblQtd.Text = gridMotorista.RowCount.ToString();

                    //Seleciona a primeira linha do grid
                    gridMotorista.CurrentCell = gridMotorista.Rows[0].Cells[1];
                    //Foca no grid
                    gridMotorista.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma motorista encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (gridMotorista.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridMotorista.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridMotorista.Rows[indice].Cells[0].Value.ToString();
                    //Seta o nome do motorista
                    txtNome.Text = Convert.ToString(gridMotorista.Rows[indice].Cells[1].Value);
                    // Seta o apelido do motorista 
                    txtApelido.Text = Convert.ToString(gridMotorista.Rows[indice].Cells[2].Value);
                    //Seta a CNH
                    txtCNH.Text = Convert.ToString(gridMotorista.Rows[indice].Cells[3].Value);
                    //Seta a validade da CNH
                    txtValidade.Text = string.Format(@"00/00 /0000", gridMotorista.Rows[indice].Cells[4].Value);
                    //Seta o celular motorista 
                    txtCelular.Text = Convert.ToString(gridMotorista.Rows[indice].Cells[5].Value);
                    //Seta o status do motorista 
                    chkAtivo.Checked = Convert.ToBoolean(gridMotorista.Rows[indice].Cells[6].Value);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

       
    }
}
