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
    public partial class FrmRotas : Form
    {
        //Array com id(Combobox)
        private int[] codRota;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        //Coleção de Empresa
        public List<Empresa> empresaCollection;

        public FrmRotas()
        {
            InitializeComponent();
        }

        private void FrmRotas_Load(object sender, EventArgs e)
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

        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no tipo de rota
                cmbTipo.Focus();
            }
        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no salvar
                btnSalvar.Focus();
            }
        }

        private void gridRotas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridRotas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridRotas.Rows.Count > 0)
                {
                    //Habilita todos os campos
                    Habilita();                  
                }
            }
        }

        private void gridRotas_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (cmbTipo.Items.Count == 0)
            {
                //Pesquisa os tipos de rotas
                PesqTipoRotas();
            }

            //Pesquisa as rotas
            PesqRotas();
        }
        


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acesso[0].escreverFuncao == false && acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Alterar o cadastro
                Alterar();
            }

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o form
            Close();
        }

        //Pesquisa o tipo de rota
        private void PesqTipoRotas()
        {
            try
            {
                //Instância a camada de negocios
                TipoRotaNegocios tipoRotaNegocios = new TipoRotaNegocios();
                //Instância a coleção de objêtos
                TipoRotaCollection tipoRotaCollection = new TipoRotaCollection();

                //Limpa o combobox regiao
                cmbTipo.Items.Clear();

                //Preenche a coleção com apesquisa
                tipoRotaCollection = tipoRotaNegocios.PesqTipo();
                //Preenche o combobox região
                tipoRotaCollection.ForEach(n => cmbTipo.Items.Add(n.descTipoRota));
                
                //Define o tamanho do array para o combobox
                codRota = new int[tipoRotaCollection.Count];

                for (int i = 0; i < tipoRotaCollection.Count; i++)
                {
                    //Preenche o array combobox
                    codRota[i] = tipoRotaCollection[i].codTipoRota;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Alterar o cadastro
        private void Alterar()
        {
            try
            {
                if (cmbTipo.Text.Equals("") || cmbTipo.Text.Equals("SELECIONE"))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo
                    cmbTipo.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    RotaNegocios rotaNegocios = new RotaNegocios();
                    //Passa para a camada de negocios
                    rotaNegocios.Alterar(cmbEmpresa.Text, Convert.ToInt32(txtCodigo.Text), codRota[cmbTipo.SelectedIndex]);

                    //Instância as linha do grid
                    DataGridViewRow linha = gridRotas.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    //Insere a unidade no grid                      
                    gridRotas.Rows[indice].Cells[2].Value = cmbTipo.Text;
                    //Foca na tabela
                    gridRotas.Focus();
                    //Desabilita todos os campos
                    Desabilita();

                    MessageBox.Show("Cadastro alterado com sucesso! ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa rotas
        private void PesqRotas()
        {
            try
            {
                //Instância a camada de negocios
                RotaNegocios rotaNegocios = new RotaNegocios();
                //Instância a coleção de objêto
                RotaCollection rotaCollection = new RotaCollection();
                //A coleção recebe o resultado da consulta
                rotaCollection = rotaNegocios.PesqRotas(cmbEmpresa.Text);
                //Limpa o grid
                gridRotas.Rows.Clear();
                //Grid Recebe o resultado da coleção
                rotaCollection.ForEach(n => gridRotas.Rows.Add(n.codRota, n.descRota, n.descTipoRota));

                if (rotaCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();
                    //Qtd encontrada
                    lblQtd.Text = gridRotas.RowCount.ToString();
                    //Seleciona a primeira linha do grid
                    gridRotas.CurrentCell = gridRotas.Rows[0].Cells[1];
                    //Foca no grid
                    gridRotas.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma rota encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (gridRotas.Rows.Count > 0)
                {
                    //Instância as linha do grid
                    DataGridViewRow linha = gridRotas.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = Convert.ToString(gridRotas.Rows[indice].Cells[0].Value);
                    //Seta a descrição
                    txtDescricao.Text = Convert.ToString(gridRotas.Rows[indice].Cells[1].Value);
                    //Seleciona a descrição do tipo de rota
                    cmbTipo.Text = Convert.ToString(gridRotas.Rows[indice].Cells[2].Value);
                    //Desabilita todos os campos
                    Desabilita();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Habilita()
        {
            //Habilita o tipo de rota
            cmbTipo.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;

            //Foca no campo tipo de rota
            cmbTipo.Focus();
        }

        private void Desabilita()
        {
            //Desabilita o tipo de rota
            cmbTipo.Enabled = false;
            //Desabilita o botão
            btnSalvar.Enabled = false;

        }

        
    }
}
