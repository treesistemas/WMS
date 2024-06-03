using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmRastreador : Form
    {
        //Controle para salvar e alterar (False = alterar)
        bool opcao = false;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmRastreador()
        {
            InitializeComponent();
        }

        private void txtPesqRastreador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão pesuisar
                btnPesquisar.Focus();
            }
        }

        private void txtRastreador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo modo
                cmbModo.Focus();
            }
        }

        private void txtRastreador_TextChanged(object sender, EventArgs e)
        {
            //Verifica se é numero
            VerificarNumero();
        }

        private void cmbModo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo observação
                txtObservacao.Focus();
            }
        }

        private void txtObservacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no salvar
                btnSalvar.Focus();
            }
        }

        private void gridRastreador_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridRastreador_MouseClick(object sender, MouseEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridRastreador_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridRastreador.Rows.Count > 0)
                {
                    //Controle para alterar  
                    opcao = false;
                    //Habilita todos os campos
                    Habilita();
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa os rastreadores
            PesqRastreadores();
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
            //Fecha o frame
            Close();
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
        private void Salvar()
        {
            try
            {
                if (txtRastreador.Text.Equals("") || cmbModo.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Por favor, preencha as informações necessárias!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo rastreador
                    txtRastreador.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    RastreadorNegocios rastreadorNegocios = new RastreadorNegocios();
                    //Instância o objeto
                    Rastreador rastreador = new Rastreador();

                    //Seta o código
                    rastreador.codRastreador = Convert.ToInt32(txtCodigo.Text);
                    //Seta o rastreador
                    rastreador.numeroRastreador = Convert.ToInt32(txtRastreador.Text);
                    //Seta o status
                    rastreador.statusRastreador = Convert.ToString(chkAtivo.Checked);
                    //Seta o modo
                    rastreador.modoRastreador = cmbModo.Text;
                    //Seta a observação
                    rastreador.observacaoRastreador = txtObservacao.Text;

                    //Passa para a camada de negocios
                    rastreadorNegocios.Salvar(rastreador);

                    //Insere o cadastro no grid
                    gridRastreador.Rows.Add(txtCodigo.Text, txtRastreador.Text, chkAtivo.Checked, cmbModo.Text, txtObservacao.Text);
                    //Recebe a qtd de linha no grid 
                    int linha = Convert.ToInt32(gridRastreador.RowCount.ToString());
                    //Seleciona a linha      
                    gridRastreador.CurrentCell = gridRastreador.Rows[linha - 1].Cells[1];
                    //Qtd encontrada
                    lblQtd.Text = gridRastreador.RowCount.ToString();
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
                if (txtRastreador.Text.Equals("") || cmbModo.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Por favor, preencha as informações necessárias!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo rastreador
                    txtRastreador.Focus();
                }
                else
                {
                    //Instância o objeto negocios
                    RastreadorNegocios rastreadorNegocios = new RastreadorNegocios();
                    //Instância o objeto
                    Rastreador rastreador = new Rastreador();
                    //Seta o código
                    rastreador.codRastreador = Convert.ToInt32(txtCodigo.Text);
                    //Seta o rastreador
                    rastreador.numeroRastreador = Convert.ToInt32(txtRastreador.Text);
                    //Seta o status
                    rastreador.statusRastreador = Convert.ToString(chkAtivo.Checked);
                    //Seta o modo
                    rastreador.modoRastreador = cmbModo.Text;
                    //Seta a observação
                    rastreador.observacaoRastreador = txtObservacao.Text;

                    if (cmbModo.Text.Equals("EXTRAVIADO") || cmbModo.Text.Equals("PERDIDO"))
                    {
                        if (MessageBox.Show("Se o rastreador estiver vinculado há algum veiculo \nVocê deseja remover?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            //Desvincula o rastreador do veículo
                            rastreadorNegocios.DesvincularRastreador(Convert.ToInt32(txtCodigo.Text));
                        }
                    }

                    //Passa para a camada de negocios
                    rastreadorNegocios.Alterar(rastreador);

                    //Instância as linha do grid
                    DataGridViewRow linha = gridRastreador.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    //Altera o rastreador no grid                      
                    gridRastreador.Rows[indice].Cells[1].Value = txtRastreador.Text;
                    gridRastreador.Rows[indice].Cells[2].Value = chkAtivo.Checked;
                    gridRastreador.Rows[indice].Cells[3].Value = cmbModo.Text;
                    gridRastreador.Rows[indice].Cells[4].Value = txtObservacao.Text;
                    //Foca na tabela
                    gridRastreador.Focus();
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
                rastreadorCollection = rastreadorNegocios.PesqRastreador(txtPesqRastreador.Text, chkPesqAtivo.Checked);
                //Limpa o grid
                gridRastreador.Rows.Clear();
                //Grid Recebe o resultado da coleção
                rastreadorCollection.ForEach(n => gridRastreador.Rows.Add(n.codRastreador, n.numeroRastreador, n.statusRastreador, n.modoRastreador, n.observacaoRastreador));

                if (rastreadorCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();
                    //Qtd de unidade encontrada
                    lblQtd.Text = gridRastreador.RowCount.ToString();
                    //Seleciona a primeira linha do grid
                    gridRastreador.CurrentCell = gridRastreador.Rows[0].Cells[1];
                    //Foca no grid
                    gridRastreador.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum rastreador encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (gridRastreador.Rows.Count > 0)
                {
                    //Instância as linha do grid
                    DataGridViewRow linha = gridRastreador.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridRastreador.Rows[indice].Cells[0].Value.ToString();
                    //Seta a rastreador
                    txtRastreador.Text = gridRastreador.Rows[indice].Cells[1].Value.ToString();
                    //Seta o status
                    chkAtivo.Checked = Convert.ToBoolean(gridRastreador.Rows[indice].Cells[2].Value);
                    //Seta o modo
                    cmbModo.SelectedItem = gridRastreador.Rows[indice].Cells[3].Value.ToString();
                    //Seta a observação
                    txtObservacao.Text = gridRastreador.Rows[indice].Cells[4].Value.ToString();

                    //Desabilita todos os campos
                    Desabilita();
                    //Controle para alterar
                    opcao = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void VerificarNumero()
        {
            try
            {
                if (!txtRastreador.Text.Equals(""))
                {
                    Convert.ToInt32(txtRastreador.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Este campo só aceita numeros inteiros!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void ImpressaoEtiqueta(string nome, string empresa)
        {
            try
            {
                //Web => var caminho = Server.MapPath("~/pasta") + "\\nomerelatorio.rpt"
                //DeskTop => var caminho = Path.GetFullPath("~/pasta") + "\\nomerelatorio.rpt"

                // Pego o arquivo gerado pelo Argobar
                string source = Path.GetFullPath("/Teste.prn");

                // Caminho do novo arquivo atualizado
                string destination = Path.GetFullPath("/template.prn");

                // Abro o arquivo para escrita
                StreamReader streamReader;
                streamReader = File.OpenText(source);
                string contents = streamReader.ReadToEnd();
                streamReader.Close();

                string conteudo = contents;
                StreamWriter streamWriter = File.CreateText(destination);

                // Atualizo as variaveis do arquivo

                conteudo = conteudo.Replace("%Nome%", nome);

                conteudo = conteudo.Replace("%Empresa%", empresa);

                streamWriter.Write(conteudo);
                streamWriter.Close();

                if (File.Exists(destination))
                {
                    //File.Delete(destination);

                }



                // envio o arquivo para a porta LPT1
                System.IO.File.Copy(destination, "USB001", true);
                File.Delete(destination);




            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao imprimir a etiqueta. \nDetalhes: " + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LimpaCampos()
        {
            //Limpa o campo código
            txtCodigo.Clear();
            //Limpa o campo rastreador
            txtRastreador.Clear();
            //Limpa o campo modo
            cmbModo.Text = "EM USO";
            //Ativa o checkbox
            chkAtivo.Checked = true;
            //Limpa o campo observação
            txtObservacao.Clear();
        }

        private void Habilita()
        {
            //Habilita o rastredor
            txtRastreador.Enabled = true;
            //Habilita o checkbox
            chkAtivo.Enabled = true;
            //Habilita o modo
            cmbModo.Enabled = true;
            //Habilta o campo observação
            txtObservacao.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;
            //Desabilita o botão novo
            btnNovo.Enabled = false;

            //Foca no campo rastreador
            txtRastreador.Focus();
        }

        private void Desabilita()
        {
            //Desabilita o rastredor
            txtRastreador.Enabled = false;
            //desabilita o checkbox
            chkAtivo.Enabled = false;
            //Desabilita o modo
            cmbModo.Enabled = false;
            //Desabilta o campo observação
            txtObservacao.Enabled = false;
            //Desabilita o botão
            btnSalvar.Enabled = false;

            //Habilita o botão novo
            btnNovo.Enabled = true;
        }

        private void mniEtiqueta_Click(object sender, EventArgs e)
        {
            ImpressaoEtiqueta("Leandro", "Donizete");
        }
    }
}
