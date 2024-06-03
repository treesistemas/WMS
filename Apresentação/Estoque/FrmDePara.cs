using Negocios.Movimentacao;
using ObjetoTransferencia;
using ObjetoTransferencia.Impressao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wms.Relatorio.DataSet;

namespace Wms.Movimentacao
{
    public partial class FrmDePara : Form
    {
        public List<Empresa> empresaCollection;

        public FrmDePara()
        {
            InitializeComponent();
        }

        private void FrmDePara_Load(object sender, EventArgs e)
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

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEndereco.Focus();
            }
        }

        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnConfirmar.Focus();
            }
        }

        private void mniRemover_Click(object sender, EventArgs e)
        {
            if (gridEndereco.Rows.Count > 0)
            {
                gridEndereco.Rows.RemoveAt(gridEndereco.CurrentRow.Index);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            PesqProduto();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            gridEndereco.Rows.Clear();
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            //Transfere
            Transferir();
        }

        private void PesqProduto()
        {
            try
            {
                if (cmbTipo.Text.Equals("") || cmbTipo.Text.Equals("SELECIONE"))
                {
                    MessageBox.Show("Preencha todos os campos");
                }
                else if (txtCodigo.Text.Equals("") || txtEndereco.Text.Equals(""))
                {
                    MessageBox.Show("Preencha todos os campos");
                }
                else
                {
                    //Instância o camada de negocios
                    DeParaNegocios negocios = new DeParaNegocios();
                    //Instância o objêtos
                    DePara objeto1 = new DePara();
                    //Instância o objêtos
                    DePara objeto2 = new DePara();
                    //Pesquisa o produto
                    objeto1 = negocios.PesqProduto(txtCodigo.Text, cmbTipo.Text, cmbEmpresa.Text);
                    //Pesquisa o endereço
                    objeto2 = negocios.PesqEndereco(txtEndereco.Text, cmbTipo.Text, cmbEmpresa.Text);

                    gridEndereco.Rows.Add(gridEndereco.Rows.Count + 1, objeto1.idProduto1, objeto1.descPorduto1, objeto1.idEndereco1, objeto1.endProduto1, objeto1.idEstacao1,
                        objeto2.idEndereco2, objeto2.endProduto2, objeto2.idProduto2, objeto2.descPorduto2, objeto2.idEstacao2);

                    //Limpa os campos
                    txtCodigo.Text = string.Empty;
                    txtEndereco.Text = string.Empty;
                    txtCodigo.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void Transferir()
        {
            try
            {
                if (cmbTipo.Text.Equals("") || cmbTipo.Text.Equals("SELECIONE"))
                {
                    MessageBox.Show("Selecione um tipo de endereço", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o camada de negocios
                    DeParaNegocios negocios = new DeParaNegocios();

                    for (int i = 0; gridEndereco.Rows.Count > i; i++)
                    {
                        //Instância o objêtos
                        DePara objeto1 = new DePara();

                        objeto1.idProduto1 = Convert.ToInt32(gridEndereco.Rows[i].Cells[1].Value);
                        objeto1.idEndereco1 = Convert.ToInt32(gridEndereco.Rows[i].Cells[3].Value);
                        objeto1.idEstacao1 = Convert.ToInt32(gridEndereco.Rows[i].Cells[5].Value);

                        objeto1.idProduto2 = Convert.ToInt32(gridEndereco.Rows[i].Cells[8].Value);
                        objeto1.idEndereco2 = Convert.ToInt32(gridEndereco.Rows[i].Cells[6].Value);
                        objeto1.idEstacao2 = Convert.ToInt32(gridEndereco.Rows[i].Cells[10].Value);
                        objeto1.tipo = cmbTipo.Text;

                        //Atualiza os endereco
                        negocios.AtualizarEndereco(objeto1, cmbEmpresa.Text);

                        //Inserir Rastreamento
                        //Registra a exclusão no rastreamento
                        /*negocios.InserirRastreamento(cmbEmpresa.Text, "TRANSFERÊNCIA", codUsuario, idProduto, //Código do produto
                                                                         null, null, null, null, null,
                                                                         Convert.ToInt32(negocios.codApartamento), //Código do endereço
                                                                         Convert.ToInt32(0), //Quantidade do produto
                                                                         Convert.ToString(lblValidade.Text), //Vencimento do produto
                                                                         Convert.ToDouble(0), null, "FLOWRACK"); //peso do produto*/

                    }

                    




                    //Limpa todos os campos
                    gridEndereco.Rows.Clear();

                    MessageBox.Show("Operação realizada com sucesso!");

                    txtCodigo.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }

        }
    }
}
