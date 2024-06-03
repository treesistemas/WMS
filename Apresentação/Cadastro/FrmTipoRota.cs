using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmTipoRota : Form
    {
        //Controle para salvar e alterar
        bool opcao = false;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmTipoRota()
        {
            InitializeComponent();
        }

        //KeyPress
        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no salvar
                btnSalvar.Focus();
            }
        }

        //CellClick
        private void gridTipo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        //MouseDoubleClick
        private void gridTipo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridTipo.Rows.Count > 0)
                {
                    //Controle para alterar  
                    opcao = false;
                    //Habilita todos os campos
                    Habilita();
                }
            }
        }

        //KeyUP
        private void gridTipo_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        //Click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa
            PesqTiposRota();
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
                    Salvar();
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
                    Alterar();
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o form
            Close();
        }


        //Pesquisa um novo id 
        private void PesqId()
        {
            try
            {
                //Limpa todos os campos
                LimpaCampos();
                //Instância a camada de negocios
                TipoRotaNegocios tipoRotaNegocios = new TipoRotaNegocios();
                //Recebe o resultado da consulta
                txtCodigo.Text = Convert.ToString(tipoRotaNegocios.PesqId());
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
        private void Salvar()
        {
            try
            {
                if (txtCodigo.Text.Equals("") || txtDescricao.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo
                    txtDescricao.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    TipoRotaNegocios tipoRotaNegocios = new TipoRotaNegocios();
                    //Passa para a camada de negocios
                    tipoRotaNegocios.Salvar(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text);

                    //Insere o cadastro no grid
                    gridTipo.Rows.Add(txtCodigo.Text, txtDescricao.Text);
                    //Recebe a qtd de linha no grid 
                    int linha = Convert.ToInt32(gridTipo.RowCount.ToString());
                    //Seleciona a linha      
                    gridTipo.CurrentCell = gridTipo.Rows[linha - 1].Cells[1];
                    //Qtd encontrada
                    lblQtd.Text = gridTipo.RowCount.ToString();
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
        private void Alterar()
        {
            try
            {
                if (txtCodigo.Text.Equals("") || txtDescricao.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo
                    txtDescricao.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    TipoRotaNegocios tipoRotaNegocios = new TipoRotaNegocios();
                    //Passa para a camada de negocios
                    tipoRotaNegocios.Alterar(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text);

                    //Instância as linha do grid
                    DataGridViewRow linha = gridTipo.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    //Insere a unidade no grid                      
                    gridTipo.Rows[indice].Cells[1].Value = txtDescricao.Text;
                    //Foca na tabela
                    gridTipo.Focus();
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
        private void PesqTiposRota()
        {
            try
            {
                //Instância a camada de negocios
                TipoRotaNegocios tipoRotaNegocios = new TipoRotaNegocios();
                //Instância a coleção de objêto
                TipoRotaCollection tipoRotaCollection = new TipoRotaCollection();
                //A coleção recebe o resultado da consulta
                tipoRotaCollection = tipoRotaNegocios.PesqTipo();
                //Limpa o grid
                gridTipo.Rows.Clear();
                //Grid Recebe o resultado da coleção
                tipoRotaCollection.ForEach(n => gridTipo.Rows.Add(n.codTipoRota, n.descTipoRota));

                if (tipoRotaCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();
                    //Qtd de unidade encontrada
                    lblQtd.Text = gridTipo.RowCount.ToString();
                    //Seleciona a primeira linha do grid
                    gridTipo.CurrentCell = gridTipo.Rows[0].Cells[1];
                    //Foca no grid
                    gridTipo.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum tipo de rota encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (gridTipo.Rows.Count > 0)
                {
                    //Instância as linha do grid
                    DataGridViewRow linha = gridTipo.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridTipo.Rows[indice].Cells[0].Value.ToString();
                    //Seta a descrição
                    txtDescricao.Text = Convert.ToString(gridTipo.Rows[indice].Cells[1].Value);

                    //Desabilita todos os campos
                    Desabilita();
                    //Controle para alterar
                    opcao = false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LimpaCampos()
        {
            //Limpa o campo código
            txtCodigo.Clear();
            //Limpa o campo descrição
            txtDescricao.Clear();
        }

        private void Habilita()
        {
            //Habilita a descrição
            txtDescricao.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;
            //Desabilita o botão novo
            btnNovo.Enabled = false;

            //Foca no campo descrição
            txtDescricao.Focus();
        }

        private void Desabilita()
        {
            //Desabilita a descrição
            txtDescricao.Enabled = false;
            //Desabilita o botão
            btnSalvar.Enabled = false;

            //Habilita o botão novo
            btnNovo.Enabled = true;
        }
    }
}
