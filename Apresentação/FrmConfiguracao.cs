using Aspose.Cells;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Microsoft.Office.Interop.Excel;
using Negocios;
using ObjetoTransferencia;
using System;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmConfiguracao : Form
    {
        //Controle a opção de salvar ou alterar da impressora
        bool opcaoImpressora = true;

        public FrmConfiguracao()
        {
            InitializeComponent();
        }

        private void FrmConfiguracao_Load(object sender, EventArgs e)
        {
            //Exibe as configurações ao abrir o frame
            PesqConfiguracao();
        }

        //Selected
        private void tabConfiguracao_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabParametros)
            {
                //Pesquisa os parâmetros do sistema
                PesqParametros();
            }

            if (e.TabPage == tabImpressora)
            {
                //Pesquisa as impressoras do sistema
                PesqImpressora();
            }


        }


        #region Eventos Formulario

        //KeyPres
        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo logo
                txtLogo.Focus();
            }
        }

        private void txtLogo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Foca no campo producao
            txtImagemProduto.Focus();
        }

        private void txtImagemProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Foca no botão imagem
            btnSalvarFormulario.Focus();
        }

        //CellContent
        private void gridItensConfiguracao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(gridParametro.Rows[e.RowIndex].Cells[0].Value) == true)
            {
                //altera o status da restrição
                gridParametro.Rows[e.RowIndex].Cells[0].Value = false;
                //Altera o status dos itens da configuração
                AlterarRestricao(Convert.ToInt32(gridParametro.Rows[e.RowIndex].Cells[1].Value), false);
            }
            else
            {   //altera o status da restrição
                gridParametro.Rows[e.RowIndex].Cells[0].Value = true;
                //Altera o status dos itens da configuração
                AlterarRestricao(Convert.ToInt32(gridParametro.Rows[e.RowIndex].Cells[1].Value), true);
            }
        }

        private void btnLogo_Click(object sender, EventArgs e)
        {
            // Instancia a classe.
            using (FolderBrowserDialog dirDialog = new FolderBrowserDialog())
            {
                // Mostra a janela de escolha do directorio
                DialogResult res = dirDialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    // Como o utilizador carregou no OK, o directorio escolhido pode ser acedido da seguinte forma:
                    txtLogo.Text = dirDialog.SelectedPath;

                    //permitir ao utilizador criar diretórios pela janela
                    dirDialog.ShowNewFolderButton = true;

                    //Pode ainda decidir qual o directorio escolhido quando a janela e aberta. Para isso utilize a propriedade 
                    //FolderBrowserDialog.RootFolder;
                }
                else
                {
                    // Caso o utilizador tenha cancelado
                    // ...
                }
            }
        }

        private void btnImagem_Click(object sender, EventArgs e)
        {
            // Instancia a classe.
            using (FolderBrowserDialog dirDialog = new FolderBrowserDialog())
            {
                // Mostra a janela de escolha do directorio
                DialogResult res = dirDialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    // Como o utilizador carregou no OK, o directorio escolhido pode ser acedido da seguinte forma:
                    txtImagemProduto.Text = dirDialog.SelectedPath;

                    //permitir ao utilizador criar diretórios pela janela
                    dirDialog.ShowNewFolderButton = true;

                    //Pode ainda decidir qual o directorio escolhido quando a janela e aberta. Para isso utilize a propriedade 
                    //FolderBrowserDialog.RootFolder;
                }
                else
                {
                    // Caso o utilizador tenha cancelado
                    // ...
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //Atualiza a configuração
            AlterarConfiguracao();
        }

        #endregion


        #region Evento impressora

        //Key press
        private void txtIPImpressora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNomeImpressora.Focus();
            }

        }

        private void txtNomeImpressora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtDescricaoImpressora.Focus();
            }
        }

        private void txtDescricaoImpressora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbEstacaoImpressora.Focus();
            }
        }

        private void cmbEstacaoImpressora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSalvarImpressora.Focus();
            }
        }

        //Key up
        private void gridImpressora_KeyUp(object sender, KeyEventArgs e)
        {
            DesabilitarCamposImpressora();
        }

        //Key Dow
        private void gridImpressora_KeyDown(object sender, KeyEventArgs e)
        {
            DesabilitarCamposImpressora();
        }

        //Mouse Double click
        private void gridImpressora_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridImpressora.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridImpressora.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtIPImpressora.Text = gridImpressora.Rows[indice].Cells[1].Value.ToString();
                    //Seta o valor da descrição
                    txtNomeImpressora.Text = gridImpressora.Rows[indice].Cells[2].Value.ToString();
                    //Seta o valor da descrição
                    txtDescricaoImpressora.Text = gridImpressora.Rows[indice].Cells[3].Value.ToString();

                    if (!Convert.ToString(gridImpressora.Rows[indice].Cells[4].Value).Equals(string.Empty))
                    {
                        //Seta o valor da descrição
                        cmbEstacaoImpressora.Text = gridImpressora.Rows[indice].Cells[4].Value.ToString();
                    }
                    else
                    {
                        cmbEstacaoImpressora.Text = string.Empty;
                    }

                    //Habilita os campos
                    txtIPImpressora.Enabled = true;
                    txtNomeImpressora.Enabled = true;
                    txtDescricaoImpressora.Enabled = true;
                    cmbEstacaoImpressora.Enabled = true;

                    //Controle para alterar
                    opcaoImpressora = false;
                }
            }
            else
            {
                //Limpa os campos da impressora
                DesabilitarCamposImpressora();
            }

        }

        //Click
        //Novo registro
        private void btnNovaImpressora_Click(object sender, EventArgs e)
        {
            //Habilita os campos
            HabilitarCamposImpressora();
        }

        //Insere a impressora
        private void btnSalvarImpressora_Click(object sender, EventArgs e)
        {
            if (opcaoImpressora == true)
            {
                InserirImpressora();
            }
            else
            {
                AtualizarImpressora();
            }
        }

        //Cancela o registro
        private void btnCancelarImpressora_Click(object sender, EventArgs e)
        {
            //Limpa os campos da impressora
            DesabilitarCamposImpressora();

        }


        #endregion



        private void txtPesoEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Foca no campo produto por endereço
            //txtProdutoEndereco.Focus();
        }

        private void txtPedidoSeparador_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Foca no campo itens por separador
            txtItensSeparador.Focus();
        }

        private void txtItensSeparador_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Foca no campo altura do palete
            txtAlturaPalete.Focus();
        }

        private void txtAlturaPalete_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Foca no botão salvar
            btnSalvarFormulario.Focus();
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            //fecha o frame
            Close();
        }

        //Pesquisa as configurções
        private void PesqConfiguracao()
        {
            try
            {
                //Instância a camada de negocios
                ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();
                //Instância a camada de objetos
                Configuracao configuracao = new Configuracao();
                //Passa o resultado da consulta para o objeto
                configuracao = configuracaoNegocios.PesqConfiguracao();

                if (configuracao.codConfiguracao > 0)
                {
                    txtEmpresa.Text = configuracao.empresa;
                    txtLogo.Text = configuracao.logoEmpresa;
                    txtImagemProduto.Text = configuracao.imagemProduto;
                    txtPesoEndereco.Text = string.Format(@"{0:N}", configuracao.pesoEndereco);
                    //txtProdutoEndereco.Value = configuracao.produtoEndereco;
                    txtPedidoSeparador.Value = configuracao.pedidoSeparador;
                    txtItensSeparador.Value = configuracao.itensSeparador;
                    txtAlturaPalete.Text = string.Format(@"{0:N}", configuracao.alturaPalete);

                    //Pesquisa as restrições
                    //PesqItensConfiguracao();
                }
                else
                {
                    MessageBox.Show("Nenhuma configurção encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens de configurações
        private void PesqItensConfiguracao()
        {
            try
            {
                //Instância a camada de negocios
                ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();
                //Instância a camada de objetos 
                ItensConfiguracaoCollection itensConfiguracaoCollection = new ItensConfiguracaoCollection();
                //A coleção recebe o resultado da consulta
                itensConfiguracaoCollection = configuracaoNegocios.PesqItensConfiguracao();
                //Limpa o grid
                gridParametro.Rows.Clear();

                if (itensConfiguracaoCollection.Count > 0)
                {
                    //Grid Recebe o resultado da coleção
                    itensConfiguracaoCollection.ForEach(n => gridParametro.Rows.Add(n.status, n.codItem, n.descricao, n.menu));

                    for (int indice = 0; indice < gridParametro.RowCount; indice++)
                    {
                        //Função menu
                        if (Convert.ToInt32(gridParametro.Rows[indice].Cells[3].Value) > 0)
                        {
                            gridParametro.Rows[indice].DefaultCellStyle.BackColor = Color.MediumSeaGreen;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Nenhuma restrição encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa as configurações
        private void PesqParametros()
        {
            try
            {
                //Instância a camada de negocios
                ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();
                //Instância a camada de objetos
                ParametroCollection parametroCollection = new ParametroCollection();
                //Passa o resultado da consulta para o objeto
                parametroCollection = configuracaoNegocios.PesqParametro();

                if (parametroCollection.Count > gridParametro.Rows.Count)
                {
                    //Limpa o grid
                    gridParametro.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    parametroCollection.ForEach(n => gridParametro.Rows.Add(n.codEmpresa, n.codItem, n.descParametro, n.status, n.valor, n.valor_II));

                    //Altera a cor da coluna
                    for (int i = 0; gridParametro.Rows.Count > i; i++)
                    {
                        if (Convert.ToString(gridParametro.Rows[i].Cells[3].Value).Equals("SIM"))
                        {
                            gridParametro.Rows[i].Cells[3].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            gridParametro.Rows[i].Cells[3].Style.ForeColor = Color.OrangeRed;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Pesquisa as configurações
        private void PesqImpressora()
        {
            try
            {
                //Instância a camada de negocios
                ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();
                //Instância a camada de objetos
                ImpressoraCollection impressoraCollection = new ImpressoraCollection();
                //Passa o resultado da consulta para o objeto
                impressoraCollection = configuracaoNegocios.PesqImpressora();

                if (impressoraCollection.Count > gridImpressora.Rows.Count)
                {
                    //Limpa o grid
                    gridImpressora.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    impressoraCollection.ForEach(n => gridImpressora.Rows.Add(n.codigo, n.IP, n.nome, n.descricao, n.estacao));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #region Insert e Update

        //Inserir impressora
        private void InserirImpressora()
        {
            try
            {
                //Instância a camada de negocios
                ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();
                //Instânia a camada de objetos
                Impressora i = new Impressora();
                //Preenche o objeto
                i.IP = txtIPImpressora.Text;
                i.nome = txtNomeImpressora.Text;
                i.descricao = txtDescricaoImpressora.Text;

                if (!(cmbEstacaoImpressora.Text.Equals("") || cmbEstacaoImpressora.Text.Equals(string.Empty)))
                {
                    i.estacao = Convert.ToInt32(cmbEstacaoImpressora.Text);
                }

                //Passa os valores para a camada de negocios
                configuracaoNegocios.InserirImpressora(i);

                //Pesquisa as impressoras do sistema para inserir ao grid
                PesqImpressora();

                //Limpa os campos da impressora
                DesabilitarCamposImpressora();

                MessageBox.Show("Impressora registrada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Atualiza a impressora
        private void AtualizarImpressora()
        {
            try
            {
                if (gridImpressora.Rows.Count > 0)
                {
                    //Instância a camada de negocios
                    ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();
                    //Instânia a camada de objetos
                    Impressora i = new Impressora();

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridImpressora.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Preenche o objeto
                    i.codigo = Convert.ToInt32(gridImpressora.Rows[indice].Cells[0].Value);
                    i.IP = txtIPImpressora.Text;
                    i.nome = txtNomeImpressora.Text;
                    i.descricao = txtDescricaoImpressora.Text;

                    if (!(cmbEstacaoImpressora.Text.Equals("") || cmbEstacaoImpressora.Text.Equals(string.Empty)))
                    {
                        i.estacao = Convert.ToInt32(cmbEstacaoImpressora.Text);
                    }
                    else
                    {
                        i.estacao = null;
                    }

                    //Passa os valores para a camada de negocios
                    configuracaoNegocios.AtualizarImpressora(i);

                    //Altera a descrição no grid                      
                    gridImpressora.Rows[indice].Cells[1].Value = txtIPImpressora.Text;
                    //Atera o controle de auditoria
                    gridImpressora.Rows[indice].Cells[2].Value = txtNomeImpressora.Text;
                    //altera o controle de validade
                    gridImpressora.Rows[indice].Cells[3].Value = txtDescricaoImpressora.Text;
                    //altera o controle de lote
                    gridImpressora.Rows[indice].Cells[4].Value = cmbEstacaoImpressora.Text;

                    //Limpa os campos da impressora
                    DesabilitarCamposImpressora();

                    MessageBox.Show("Impressora atualizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Altera a configuração
        private void AlterarConfiguracao()
        {
            try
            {
                //Instância a camada de negocios
                ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();
                //Instânia a camada de objetos
                Configuracao configuracao = new Configuracao();
                //Preenche o objeto
                configuracao.empresa = txtEmpresa.Text;
                configuracao.logoEmpresa = txtLogo.Text;
                configuracao.imagemProduto = txtImagemProduto.Text;
                configuracao.pesoEndereco = Convert.ToDouble(txtPesoEndereco.Text);
                //configuracao.produtoEndereco = Convert.ToInt32(txtProdutoEndereco.Value);
                configuracao.pedidoSeparador = Convert.ToInt32(txtPedidoSeparador.Value);
                configuracao.itensSeparador = Convert.ToInt32(txtItensSeparador.Value);
                configuracao.alturaPalete = Convert.ToDouble(txtAlturaPalete.Text);

                //Passa os valores para a camada de negocios
                configuracaoNegocios.AlterarConfiguracao(configuracao);

                MessageBox.Show("Alteração realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Altera o status dos itens da configuração
        private void AlterarRestricao(int codigo, bool status)
        {
            try
            {
                //Instância a camada de negocios
                ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();
                //Passa os valores para a camada de negocios
                configuracaoNegocios.AlterarRestricao(codigo, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void HabilitarCamposImpressora()
        {
            //Limpa os campos
            txtIPImpressora.Text = string.Empty;
            txtNomeImpressora.Text = string.Empty;
            txtDescricaoImpressora.Text = string.Empty;
            cmbEstacaoImpressora.Text = string.Empty;
            cmbEstacaoImpressora.Items.Clear();

            //Desabilita os campos
            txtIPImpressora.Enabled = true;
            txtNomeImpressora.Enabled = true;
            txtDescricaoImpressora.Enabled = true;
            cmbEstacaoImpressora.Enabled = true;

            //Foco
            txtIPImpressora.Focus();

            //Controle para registrar a impressora
            opcaoImpressora = true;
        }

        private void DesabilitarCamposImpressora()
        {
            //Limpa os campos
            txtIPImpressora.Text = string.Empty;
            txtNomeImpressora.Text = string.Empty;
            txtDescricaoImpressora.Text = string.Empty;
            cmbEstacaoImpressora.Text = string.Empty;
            cmbEstacaoImpressora.Items.Clear();

            //Desabilita os campos
            txtIPImpressora.Enabled = false;
            txtNomeImpressora.Enabled = false;
            txtDescricaoImpressora.Enabled = false;
            cmbEstacaoImpressora.Enabled = false;

            //Controle para registrar a impressora
            opcaoImpressora = true;
        }

        //Altera o status dos itens da configuração
        //Altera o status dos itens da configuração
        private void AtualizarParametro()
        {
            try
            {
                //Instância a camada de negocios
                ConfiguracaoNegocios configuracaoNegocios = new ConfiguracaoNegocios();

                string status = null;

                foreach (DataGridViewRow row in gridParametro.SelectedRows)
                {
                    if(Convert.ToString(row.Cells[3].Value).Equals("SIM"))
                    {
                        status = "True";
                    }
                    else
                    {
                        status = "False";
                    }


                    //Passa os valores para a camada de negocios
                    configuracaoNegocios.AtualizarStatus(status, Convert.ToDouble(row.Cells[4].Value),
                        Convert.ToInt32(row.Cells[5].Value), Convert.ToInt32(row.Cells[1].Value), Convert.ToInt32(row.Cells[0].Value));
                }

                MessageBox.Show("Parâmetros atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnSalvarParametro_Click(object sender, EventArgs e)
        {
            AtualizarParametro();
        }
    }
}
