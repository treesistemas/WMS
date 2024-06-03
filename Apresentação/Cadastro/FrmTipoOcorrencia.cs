using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmTipoOcorrencia : Form
    {
        //Opção para salvar ou alterar
        bool opcao = false;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmTipoOcorrencia()
        {
            InitializeComponent();
        }

        //KeyPress
        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo salvar
                cmbArea.Focus();
            }
        }

        private void cmbArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo salvar
                btnSalvar.Focus();
            }
        }

        //KeyUp
        private void gridTipos_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        //Mouse click e Double click
        private void gridTipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridTipos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridTipos.Rows.Count > 0)
                {
                    //Controle para alterar  
                    opcao = false;
                    //Habilita todos os campos
                    Habilita();
                }
            }
        }

        //Click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa as categorias
            PesqTiposOcorrencia();
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
            //Fecha a tela
            Close();
        }


        //Pesquisa o ultimo id da categoria
        private void PesqId()
        {
            try
            {
                //Limpa todos os campos
                LimpaCampos();
                //Instância a camada de negocios
                TipoOcorrenciaNegocios tipoOcorrenciaNegocios = new TipoOcorrenciaNegocios();
                //Recebe o id
                txtCodigo.Text = Convert.ToString(tipoOcorrenciaNegocios.PesqId());
                //Controle para salvar 
                opcao = true;
                //Habilita componentes
                Habilita();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa as categorias
        private void PesqTiposOcorrencia()
        {
            try
            {
                //Instância a camada de negocios
                TipoOcorrenciaNegocios tipoOcorrenciaNegocios = new TipoOcorrenciaNegocios();
                //Instância a coleção de objêto
                TipoOcorrenciaCollection tipoOcorrenciaCollection = new TipoOcorrenciaCollection();
                //A coleção recebe o resultado da consulta
                tipoOcorrenciaCollection = tipoOcorrenciaNegocios.PesqTipoOcorrencia();
                //Limpa o grid
                gridTipos.Rows.Clear();
                //Grid Recebe o resultado da coleção
                tipoOcorrenciaCollection.ForEach(n => gridTipos.Rows.Add(n.codigo, n.descricao, n.area, n.ativo));

                if (tipoOcorrenciaCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();

                    //Qtd de categoria encontrada
                    lblQtd.Text = gridTipos.RowCount.ToString();

                    //Seleciona a primeira linha do grid
                    gridTipos.CurrentCell = gridTipos.Rows[0].Cells[1];
                    //Foca no grid
                    gridTipos.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum tipo de ocorrência cadastrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (gridTipos.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridTipos.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridTipos.Rows[indice].Cells[0].Value.ToString();
                    //Seta o valor da descrição
                    txtDescricao.Text = Convert.ToString(gridTipos.Rows[indice].Cells[1].Value);
                    //Seta o valor da descrição
                    cmbArea.SelectedItem = Convert.ToString(gridTipos.Rows[indice].Cells[2].Value);
                    //Seta o valor da descrição
                    chkAtivo.Checked = Convert.ToBoolean(gridTipos.Rows[indice].Cells[3].Value);

                    //Desabilita todos os campos
                    Desabilita();
                    //Controle para alterar
                    opcao = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //Salva o tipo de ocorrência
        private void Salvar()
        {
            try
            {
                if ((txtCodigo.Text.Equals("")) || (txtDescricao.Text.Equals("")) || (cmbArea.Text.Equals("")) || (cmbArea.Text.Equals("SELECIONE")))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo descrição
                    txtDescricao.Focus();
                }
                else
                {
                    //Instância a camada de objêto
                    TipoOcorrenciaNegocios tipoOcorrenciaNegocios = new TipoOcorrenciaNegocios();
                    // Passa as informações para a camada de negocios
                    tipoOcorrenciaNegocios.Salvar(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, cmbArea.Text, chkAtivo.Checked);

                    //Insere o cadastro no grid
                    gridTipos.Rows.Add(txtCodigo.Text, txtDescricao.Text, cmbArea.Text, chkAtivo.Checked);

                    //Recebe a qtd de linha no grid 
                    int linha = Convert.ToInt32(gridTipos.RowCount.ToString()) - 1;

                    //Qtd de categoria encontrada
                    lblQtd.Text = gridTipos.RowCount.ToString();
                    //Seleciona a linha      
                    gridTipos.CurrentCell = gridTipos.Rows[linha].Cells[1];
                    //Desabilita todos os campos
                    Desabilita();
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

        //Altera o tipo de ocorrência
        private void Alterar()
        {
            try
            {
                if ((txtCodigo.Text.Equals("")) || (txtDescricao.Text.Equals("")) || (cmbArea.Text.Equals("")) || (cmbArea.Text.Equals("SELECIONE")))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo descrição
                    txtDescricao.Focus();
                }
                else
                {
                    //Instância a camada de objêto
                    TipoOcorrenciaNegocios tipoOcorrenciaNegocios = new TipoOcorrenciaNegocios();
                    
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridTipos.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    // Passa as informações para a camada de negocios
                    tipoOcorrenciaNegocios.Alterar(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, cmbArea.Text, chkAtivo.Checked);

                    //Altera a descrição no grid                      
                    gridTipos.Rows[indice].Cells[1].Value = txtDescricao.Text;
                    //Atera a área
                    gridTipos.Rows[indice].Cells[2].Value = cmbArea.Text;
                    gridTipos.Rows[indice].Cells[3].Value = chkAtivo.Checked;
                    //Foca na tabela
                    gridTipos.Focus();
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

        //Limpa os campos
        private void LimpaCampos()
        {
            //Limpa o campo código
            txtCodigo.Clear();
            //Limpa o campo descrição
            txtDescricao.Clear();
            //Limpa o campo área
            cmbArea.Text = "SELECIONE";
            //Limpa
            chkAtivo.Checked = true;

        }

        //Habilita os campos
        private void Habilita()
        {
            //Habilita a descrição
            txtDescricao.Enabled = true;
            //Habilita o campo área
            cmbArea.Enabled = true;
            //Habilita o campo ativo
            chkAtivo.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;
            //Desabilita o botão novo
            btnNovo.Enabled = false;

            //Foca no campo descrição
            txtDescricao.Focus();

            
        }

        //Desabilita os campos
        private void Desabilita()
        {
            //desabilita a descrição
            txtDescricao.Enabled = false;
            //desabilita o campo área
            cmbArea.Enabled = false;
            //Habilita o campo ativo
            chkAtivo.Enabled = false;
            //desabilita o botão
            btnSalvar.Enabled = false;
            //desabilita o botão novo
            btnNovo.Enabled = true;
        }
    }
}
