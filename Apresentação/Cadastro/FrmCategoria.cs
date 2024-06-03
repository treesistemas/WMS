using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmCategoria : Form
    {
        //Opção para salvar ou alterar
        bool opcao = false;
        //perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmCategoria()
        {
            InitializeComponent();
        }

        //KeyPress
        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo salvar
                btnSalvar.Focus();
            }
        }

        //KeyUp
        private void gridCategoria_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        //Mouse click e Double click
        private void gridCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridCategoria_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (perfilUsuario == "ADMINISTRADOR" || acesso[0].editarFuncao == true)
                {
                    if (gridCategoria.Rows.Count > 0)
                    {
                        //Controle para alterar  
                        opcao = false;
                        //Habilita todos os campos
                        Habilita();
                    }
                }
            }
        }

        //Click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa as categorias
            PesqCategoria();
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
                    Salvar(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, chkAudita.Checked, chkValidade.Checked, chkLote.Checked);

                }
                else if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Salva o cadastro
                    Salvar(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, chkAudita.Checked, chkValidade.Checked, chkLote.Checked);
                }
            }
            else
            {
                if (perfilUsuario == "ADMINISTRADOR")
                {
                    //Alterar o cadastro
                    Alterar(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, chkAudita.Checked, chkValidade.Checked, chkLote.Checked);
                }
                else if (acesso[0].editarFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Alterar o cadastro
                    Alterar(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, chkAudita.Checked, chkValidade.Checked, chkLote.Checked);
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a tela
            Close();
        }

        private void lnkInformacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("A categoria é um grupo de produto. ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Pesquisa o ultimo id da categoria
        private void PesqId()
        {
            try
            {
                //Limpa todos os campos
                LimpaCampos();
                //Instância a categoria negocios
                CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                //Instância a coleção de categoria
                Categoria categoria = new Categoria();
                //Recebe o id
                categoria = categoriaNegocios.PesqId();
                //Seta o novo id
                txtCodigo.Text = categoria.codCategoria.ToString();
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
        private void PesqCategoria()
        {
            try
            {
                //Instância a categoria negocios
                CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                //Instância a coleção de categoria
                CategoriaCollection categoriaCollection = new CategoriaCollection();
                //A coleção recebe o resultado da consulta
                categoriaCollection = categoriaNegocios.PesqCategoria();
                //Limpa o grid
                gridCategoria.Rows.Clear();
                //Grid Recebe o resultado da coleção
                categoriaCollection.ForEach(n => gridCategoria.Rows.Add(n.codCategoria, n.descCategoria, n.auditaFlow, n.controlaValidade, n.controlaLote));

                //Substitui o true por sim e false por não
                for (int i = 0; categoriaCollection.Count > i; i++)
                {
                    alterarDadosGrid(i);
                }

                if (categoriaCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();

                    //Qtd de categoria encontrada
                    lblQtd.Text = gridCategoria.RowCount.ToString();

                    //Seleciona a primeira linha do grid
                    gridCategoria.CurrentCell = gridCategoria.Rows[0].Cells[1];
                    //Foca no grid
                    gridCategoria.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma categoria encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (gridCategoria.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridCategoria.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridCategoria.Rows[indice].Cells[0].Value.ToString();
                    //Seta o valor da descrição
                    txtDescricao.Text = gridCategoria.Rows[indice].Cells[1].Value.ToString();

                    if (gridCategoria.Rows[indice].Cells[2].Value.ToString() == "SIM")
                    {
                        chkAudita.Checked = true;
                    }
                    else
                    {
                        chkAudita.Checked = false;
                    }

                    if (gridCategoria.Rows[indice].Cells[3].Value.ToString() == "SIM")
                    {
                        chkValidade.Checked = true;
                    }
                    else
                    {
                        chkValidade.Checked = false;
                    }

                    if (gridCategoria.Rows[indice].Cells[4].Value.ToString() == "SIM")
                    {
                        chkLote.Checked = true;
                    }
                    else
                    {
                        chkLote.Checked = false;
                    }

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

        //Salva a categoria
        private void Salvar(int idCategoria, string descCategoria, bool auditaFlow, bool controlaValidade, bool controlaLote)
        {
            try
            {
                if ((idCategoria == 0) || (descCategoria == ""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo descrição
                    txtDescricao.Focus();
                }
                else
                {
                    //Instância o objeto CategoriaNegocios
                    CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                    // Passa a categoria para a camada de negocios
                    categoriaNegocios.Salvar(idCategoria, descCategoria, auditaFlow, controlaValidade, controlaLote);

                    //Insere o cadastro no grid
                    gridCategoria.Rows.Add(txtCodigo.Text, txtDescricao.Text, auditaFlow, controlaValidade, controlaLote);

                    //Recebe a qtd de linha no grid 
                    int linha = Convert.ToInt32(gridCategoria.RowCount.ToString()) - 1;
                    //Atera true por sim e false por não
                    alterarDadosGrid(linha);

                    //Qtd de categoria encontrada
                    lblQtd.Text = gridCategoria.RowCount.ToString();
                    //Seleciona a linha      
                    gridCategoria.CurrentCell = gridCategoria.Rows[linha].Cells[1];
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

        //Altera a categoria
        private void Alterar(int idCategoria, string descCategoria, bool auditaFlow, bool controlaValidade, bool controlaLote)
        {
            try
            {
                if ((idCategoria == 0) || (descCategoria == ""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo descrição
                    txtDescricao.Focus();
                }
                else
                {
                    //Instância o objeto CategoriaNegocios
                    CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridCategoria.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    //Passa a categoria para a camada de negocios
                    categoriaNegocios.Alterar(idCategoria, descCategoria, auditaFlow, controlaValidade, controlaLote);

                    //Altera a descrição no grid                      
                    gridCategoria.Rows[indice].Cells[1].Value = txtDescricao.Text;
                    //Atera o controle de auditoria
                    gridCategoria.Rows[indice].Cells[2].Value = auditaFlow;
                    //altera o controle de validade
                    gridCategoria.Rows[indice].Cells[3].Value = controlaValidade;
                    //altera o controle de lote
                    gridCategoria.Rows[indice].Cells[4].Value = controlaLote;

                    //Altera true por sim e false por não
                    alterarDadosGrid(indice);

                    //Foca na tabela
                    gridCategoria.Focus();
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

        //Altera algumas informações no grid
        private void alterarDadosGrid(int linha)
        {
            if (gridCategoria.Rows[linha].Cells[2].Value.ToString() == "True")
            {
                gridCategoria.Rows[linha].Cells[2].Value = "SIM";
            }
            else
            {
                gridCategoria.Rows[linha].Cells[2].Value = "NÃO";
            }

            if (gridCategoria.Rows[linha].Cells[3].Value.ToString() == "True")
            {
                gridCategoria.Rows[linha].Cells[3].Value = "SIM";
            }
            else
            {
                gridCategoria.Rows[linha].Cells[3].Value = "NÃO";
            }

            if (gridCategoria.Rows[linha].Cells[4].Value.ToString() == "True")
            {
                gridCategoria.Rows[linha].Cells[4].Value = "SIM";
            }
            else
            {
                gridCategoria.Rows[linha].Cells[4].Value = "NÃO";
            }

        }

        //Limpa os campos
        private void LimpaCampos()
        {
            //Limpa o campo código
            txtCodigo.Clear();
            //Limpa o campo descrição
            txtDescricao.Clear();
            //Desmarca o checkbox audita flow rack
            chkAudita.Checked = false;
            //Desmarca o checkbox controla validade
            chkValidade.Checked = false;
            //Desmarca o checkbox controla lote
            chkLote.Checked = false;
        }

        //Habilita os campos
        private void Habilita()
        {
            //Habilita a descrição
            txtDescricao.Enabled = true;
            //Habilita o checkbox audita flow rack
            chkAudita.Enabled = true;
            //Habilita o checkbox controla validade
            chkValidade.Enabled = true;
            //Habilita o checkbox controla lote
            chkLote.Enabled = true;
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
            //desabilita o checkbox audita flow rack
            chkAudita.Enabled = false;
            //desabilita o checkbox controla validade
            chkValidade.Enabled = false;
            //desabilita o checkbox controla lote
            chkLote.Enabled = false;
            //desabilita o botão
            btnSalvar.Enabled = false;
            //desabilita o botão novo
            btnNovo.Enabled = true;
        }

    }
}