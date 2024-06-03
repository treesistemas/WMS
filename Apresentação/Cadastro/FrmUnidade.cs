using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmUnidade : Form
    {
        //Controle para salvar e alterar
        bool opcao = false;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmUnidade()
        {
            InitializeComponent();
        }

        private void txtUnidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca na descrição
                txtDescricao.Focus();
            }
        }

        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no salvar
                btnSalvar.Focus();
            }
        }

        private void gridUnidade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridUnidade_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridUnidade.Rows.Count > 0)
                {
                    //Controle para alterar  
                    opcao = false;
                    //Habilita todos os campos
                    Habilita();
                }
            }
        }

        private void gridUnidade_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa as unidades
            PesqUnidade();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (acesso[0].escreverFuncao == false)
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
                if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Salva o cadastro
                    Salvar(Convert.ToInt32(txtCodigo.Text), txtUnidade.Text, txtDescricao.Text);
                }
            }
            else
            {
                if (acesso[0].editarFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Alterar o cadastro
                    Alterar(Convert.ToInt32(txtCodigo.Text), txtUnidade.Text, txtDescricao.Text);
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o form
            Close();
        }

        private void lnkInformacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("A unidade é o tipo de embalagem do produto!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //Pesquisa um novo id 
        private void PesqId()
        {
            try
            {
                //Limpa todos os campos
                LimpaCampos();
                //Instância o objeto negocios
                UnidadeNegocios unidadeNegocios = new UnidadeNegocios();
                //Instância a unidade
                Unidade unidade = new Unidade();
                //Recebe o resultado da consulta
                unidade = unidadeNegocios.PesqId();
                //Seta o id
                txtCodigo.Text = unidade.codUnidade.ToString();
                //Habilita componentes
                Habilita();
                //Controle para salvar 
                opcao = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Salva o cadastro
        private void Salvar(int id, string und, string dsUnidade)
        {
            try
            {
                if ((id == 0) || (und == ""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha o campo unidade!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo unidade
                    txtUnidade.Focus();
                }
                else if (dsUnidade == "")
                {
                    //Mensagem
                    MessageBox.Show("Preencha o campo descrição!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo unidade
                    txtDescricao.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    UnidadeNegocios unidadeNegocios = new UnidadeNegocios();
                    //Instância o objeto unidade
                    Unidade unidade = new Unidade();
                    //Seta o id
                    unidade.codUnidade = id;
                    //Seta a unidade
                    unidade.unidade = und;
                    //Seta a descrição
                    unidade.descUnidade = dsUnidade;
                    //Passa a unidade para a camada de negocios
                    unidadeNegocios.Salvar(unidade);

                    //Insere o cadastro no grid
                    gridUnidade.Rows.Add(id, und, dsUnidade);
                    //Recebe a qtd de linha no grid 
                    int linha = Convert.ToInt32(gridUnidade.RowCount.ToString());
                    //Seleciona a linha      
                    gridUnidade.CurrentCell = gridUnidade.Rows[linha - 1].Cells[1];
                    //Qtd de unidade encontrada
                    lblQtd.Text = gridUnidade.RowCount.ToString();
                    //Desabilita todos os campos
                    Desabilita();
                    //controle para alterar
                    opcao = false;

                    MessageBox.Show("Cadastro realizado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Alterar o cadastro
        private void Alterar(int id, string und, string dsUnidade)
        {
            try
            {
                if ((id == 0) || (und == ""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha o campo unidade!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo unidade
                    txtUnidade.Focus();
                }
                else if (dsUnidade == "")
                {
                    //Mensagem
                    MessageBox.Show("Preencha o campo descrição!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo unidade
                    txtDescricao.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    UnidadeNegocios unidadeNegocios = new UnidadeNegocios();
                    //Instância o objeto unidade
                    Unidade unidade = new Unidade();
                    //Seta o id
                    unidade.codUnidade = id;
                    //Seta a unidade
                    unidade.unidade = und;
                    //Seta a descrição
                    unidade.descUnidade = dsUnidade;
                    //Passa para a camada de negocios
                    unidadeNegocios.Alterar(unidade);

                    //Instância as linha do grid
                    DataGridViewRow linha = gridUnidade.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    //Insere a unidade no grid                      
                    gridUnidade.Rows[indice].Cells[1].Value = und;
                    gridUnidade.Rows[indice].Cells[2].Value = dsUnidade;
                    //Foca na tabela
                    gridUnidade.Focus();
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

        //Pesquisa as unidades
        private void PesqUnidade()
        {
            try
            {
                //Instância o objeto negocios
                UnidadeNegocios unidadeNegocios = new UnidadeNegocios();
                //Instância a coleção
                UnidadeCollection unidadeCollection = new UnidadeCollection();
                //A coleção recebe o resultado da consulta
                unidadeCollection = unidadeNegocios.PesqUnidade();
                //Limpa o grid
                gridUnidade.Rows.Clear();
                //Grid Recebe o resultado da coleção
                unidadeCollection.ForEach(n => gridUnidade.Rows.Add(n.codUnidade, n.unidade, n.descUnidade));

                if (unidadeCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();
                    //Qtd de unidade encontrada
                    lblQtd.Text = gridUnidade.RowCount.ToString();
                    //Seleciona a primeira linha do grid
                    gridUnidade.CurrentCell = gridUnidade.Rows[0].Cells[1];
                    //Foca no grid
                    gridUnidade.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma unidade encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (gridUnidade.Rows.Count > 0)
                {
                    //Instância as linha do grid
                    DataGridViewRow linha = gridUnidade.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridUnidade.Rows[indice].Cells[0].Value.ToString();
                    //Seta a unidade
                    txtUnidade.Text = Convert.ToString(gridUnidade.Rows[indice].Cells[1].Value);
                    //Seta a descrição
                    txtDescricao.Text = Convert.ToString(gridUnidade.Rows[indice].Cells[2].Value);

                    //Desabilita todos os campos
                    Desabilita();
                    //Controle para alterar
                    opcao = false;
                }

            }
            catch (Exception)
            {
                // MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos!, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LimpaCampos()
        {
            //Limpa o campo código
            txtCodigo.Clear();
            //Limpa o campo unidade
            txtUnidade.Clear();
            //Limpa o campo descrição
            txtDescricao.Clear();
        }

        private void Habilita()
        {
            //Habilita a unidade
            txtUnidade.Enabled = true;
            //Habilita a descrição
            txtDescricao.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;
            //Desabilita o botão novo
            btnNovo.Enabled = false;

            //Foca no campo unidade
            txtUnidade.Focus();
        }

        private void Desabilita()
        {
            //Desabilita a unidade
            txtUnidade.Enabled = false;
            //Desabilita a descrição
            txtDescricao.Enabled = false;           
            //Desabilita o botão
            btnSalvar.Enabled = false;

            //Habilita o botão novo
            btnNovo.Enabled = true;
        }
    }
}
