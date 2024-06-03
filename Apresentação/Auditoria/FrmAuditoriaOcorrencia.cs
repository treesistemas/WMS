using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmAuditoriaOcorrencia : Form
    {
        //Código do usuário
        public int codUsuario;
        //Controla a foto
        private long tamanhoArquivoImagem = 0;
        //Vertor de imagens
        private byte[] vetorImagens;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmAuditoriaOcorrencia()
        {
            InitializeComponent();
        }

        private void FrmAuditoriaOcorrencia_Load(object sender, EventArgs e)
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

        //KeyPress
        private void txtPesqPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPesqNotaFiscal.Focus();
            }
        }

        private void txtPesqNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbPesqStatus.Focus();
            }
        }

        private void cmbPesqStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbPesqAuditoria.Focus();
            }
        }

        private void cmbPesqAuditoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmInicial.Focus();
            }
        }

        private void dtmInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmFinal.Focus();
            }
        }

        private void dtmFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus();
            }
        }

        private void txtCodUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqUsuario frame = new FrmPesqUsuario();
                //Perfil do usuário
                frame.perfilUsuario = "";
                //Nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe o código
                    txtCodUsuario.Text = Convert.ToString(frame.codUsuario);
                    //Receme o login
                    lblUsuario.Text = frame.nmUsuario;
                }
            }
        }

        private void txtCodUsuario_TextChanged(object sender, EventArgs e)
        {
            if (txtCodUsuario.Text.Length == 0)
            {
                lblUsuario.Text = "-";
            }
        }

        private void txtWms_TextChanged(object sender, EventArgs e)
        {
            //Calcula a diferença
            CalcularDiferença();
        }

        private void gridOcorrencia_Click(object sender, EventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void gridOcorrencia_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Habilitar();
            }
        }

        private void gridOcorrencia_KeyDown(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void gridOcorrencia_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void chkEstoque_Click(object sender, EventArgs e)
        {
            if (chkEstoque.Checked == true)
            {
                txtWms.Enabled = true;
                //Pesquisa o estoque do produto
                PesquisarEstoque();
            }
            else
            {
                txtWms.Enabled = false;
            }
        }

        private void adicionarFoto_Click(object sender, EventArgs e)
        {
            CarregaImagem();
        }

        private void excluirFoto_Click(object sender, EventArgs e)
        {
            //Limpa a imagem
            picFoto.Image = null;
            //Limpa o vetor
            vetorImagens = null;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa a ocorrencia
            PesquisarOcorrencia();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar(); //Atualiza a ocorrência
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PesquisarOcorrencia()
        {
            try
            {
                //Instância a camada de negocios
                AuditoriaOcorrenciaNegocios ocorrenciaNegocios = new AuditoriaOcorrenciaNegocios();
                //Instância a camada de objêto
                ItensOcorrenciaCollection ocorrenciaOcollection = new ItensOcorrenciaCollection();

                //Pesquisa a ocorrência
                ocorrenciaOcollection = ocorrenciaNegocios.PesqOcorrencia(txtPesqPedido.Text, txtPesqNotaFiscal.Text, cmbPesqStatus.Text,  cmbPesqAuditoria.Text, dtmInicial.Text, dtmFinal.Text, cmbEmpresa.Text);
                //Limpa o grid
                gridOcorrencia.Rows.Clear();
                //Seta a quantidade de ocorrência
                lblQtdOcorrencia.Text = Convert.ToString(ocorrenciaOcollection.Count);

                //Verifica o retorno da pesquisa
                if (ocorrenciaOcollection.Count > 0)
                {
                    //Grid Recebe o resultado da coleção
                    ocorrenciaOcollection.ForEach(n => gridOcorrencia.Rows.Add(gridOcorrencia.Rows.Count + 1, n.codOcorrencia, n.codItemOcorrencia, n.dataOcorrencia, n.dataAuditoria, n.codPedido,
                        n.descricaoOcorrencia, n.nomeMonitor, n.descMotivo, n.codProduto, n.descProduto, n.qtdAvariaProduto, n.qtdFaltaProduto, n.qtdTrocaProduto, n.qtdCriticaProduto, n.DataCriticaProduto,
                        n.qtdErp, n.qtdWMS, n.obsAuditoria, n.obsOcorrencia, n.obsAuditoria, n.fotoAuditoria, n.statusOcorrencia, n.codUsuarioErro, n.nomeUsuarioErro, n.clienteAguardo, n.apelidoMotorista, n.qtdDevolucao));


                    //Seleciona a primeira linha do grid
                    gridOcorrencia.CurrentCell = gridOcorrencia.Rows[0].Cells[0];
                    //Foca no grid
                    gridOcorrencia.Focus();
                    //Exibe os dados nos campos
                    DadosCampos();
                }
                else
                {
                    MessageBox.Show("Nenhuma ocorrência encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesquisarEstoque()
        {
            try
            {
                //Instância a camada de negocios
                AuditoriaOcorrenciaNegocios ocorrenciaNegocios = new AuditoriaOcorrenciaNegocios();
                //Instância as linha da tabela
                DataGridViewRow linha = gridOcorrencia.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;
                //Pesquisa o estoque
                int estoque = ocorrenciaNegocios.PesqEstoque(Convert.ToString(gridOcorrencia.Rows[indice].Cells[9].Value));

                //Seta a quantidade
                txtErp.Text = Convert.ToString(estoque);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Exibe os dados do grid
        private void DadosCampos()
        {
            try
            {
                //Verifica o retorno da pesquisa
                if (gridOcorrencia.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridOcorrencia.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    lblData.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[3].Value);  //data ocorrência                    
                    txtData.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[4].Value);  //data da auditoria                    
                    lblPedido.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[5].Value);  //código do pedido
                    lblOcorrencia.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[6].Value);  //descrição da ocorrência
                    lblMonitor.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[7].Value);  //descrição da ocorrência

                    //Verifica se existe motivo na auditoria
                    if (Convert.ToString(gridOcorrencia.Rows[indice].Cells[8].Value).Equals(""))
                    {
                        cmbArea.Text = "SELECIONE";
                    }
                    else
                    {
                        cmbArea.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[8].Value);  //descrição da auditoria
                    }

                    if (Convert.ToString(gridOcorrencia.Rows[indice].Cells[16].Value).Equals(string.Empty))
                    {
                        chkEstoque.Checked = false;
                    }
                    else
                    {
                        chkEstoque.Checked = true;
                    }

                    lblProduto.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[10].Value);  //descrição do produto
                    lblAvaria.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[11].Value);  //Avaria do produto
                    lblFalta.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[12].Value);  //Falta do produto
                    lblTroca.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[13].Value);  //Troca do produto
                    lblDevolucao.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[27].Value);  //Quantidade de devolução
                    lblCritica.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[14].Value);  //Critica do produto
                    lblDataCritica.Text = string.Format("{0: dd/MM/yyyy}", gridOcorrencia.Rows[indice].Cells[15].Value);  //Data crítica do produto
                    txtErp.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[16].Value);  //Estoque do erp 
                    txtWms.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[17].Value);  //estoque do wms

                    //lblObservacao.Text = string.Format(Convert.ToString(gridOcorrencia.Rows[indice].Cells[19].Value), Environment.NewLine);
                    txtObservacao.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[19].Value);  //observação docorrência                     
                    txtObservacaoAuditoria.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[20].Value);  //observação da auditoria  
                    lblStatus.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[22].Value);  //status da auditoria
                    lblMotorista.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[26].Value);  //status da auditoria

                    //Controla o nome do usuário que errou
                    if (Convert.ToString(gridOcorrencia.Rows[indice].Cells[23].Value).Equals("0"))
                    {
                        txtCodUsuario.Text = "";
                        lblUsuario.Text = "-";
                    }
                    else
                    {
                        txtCodUsuario.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[23].Value);  //código do usuário que errou
                        lblUsuario.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[24].Value);  //nome do usuário que errou
                    }

                    //Verifica se o campo ERP não está preenchido
                    if (Convert.ToString(gridOcorrencia.Rows[indice].Cells[16].Value).Equals(""))
                    {
                        //Limpa o campo
                        txtDiferenca.Text = string.Empty;
                    }

                    //verifica se o cliente aguarda a mercadoria
                    if (Convert.ToString(gridOcorrencia.Rows[indice].Cells[25].Value).Equals("True"))
                    {
                        lblClienteAguardo.Visible = true;
                    }
                    else
                    {
                        lblClienteAguardo.Visible = false;
                    }

                    //seta a foto
                    vetorImagens = (byte[])gridOcorrencia.Rows[indice].Cells[21].Value;

                    if (vetorImagens == null)
                    {
                        //Limpa a imagem
                        picFoto.Image = null;
                    }
                    else
                    {
                        //exibe a imagem
                        string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                        FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                        fs.Write(vetorImagens, 0, vetorImagens.Length);
                        fs.Flush();
                        fs.Close();
                        picFoto.Image = Image.FromFile(strNomeArquivo);
                    }

                    //Desabilita os campos
                    Desabilita();
                    //Exibe as cores no grid
                    CorGrid();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Exibe as cores no grid
        private void CorGrid()
        {
            try
            {
                //Verifica o retorno da pesquisa
                if (gridOcorrencia.Rows.Count > 0)
                {
                    for (int i = 0; gridOcorrencia.Rows.Count > i; i++)
                    {
                        if (!Convert.ToString(gridOcorrencia.Rows[i].Cells[22].Value).Equals(string.Empty))
                        {
                            if (gridOcorrencia.Rows[i].Cells[22].Value.Equals("PENDENTE"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            }

                            if (gridOcorrencia.Rows[i].Cells[22].Value.Equals("DEVOLUCAO"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.DimGray;
                            }

                            if (gridOcorrencia.Rows[i].Cells[22].Value.Equals("REENTREGA"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.SteelBlue;
                            }

                            if (gridOcorrencia.Rows[i].Cells[22].Value.Equals("CANCELADA"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }

                            if (gridOcorrencia.Rows[i].Cells[22].Value.Equals("FINALIZADA"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.SeaGreen;
                            }

                            if (gridOcorrencia.Rows[i].Cells[22].Value.Equals("ROTEIRIZACAO"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao alterar as cores no grid! \n" + ex);
            }
        }

        //Salva a ocorrência
        private void Salvar()
        {
            try
            {
                if (cmbArea.Text.Equals("SELECIONE") || cmbArea.Text.Equals("") || chkEstoque.Checked == true && txtWms.Text.Equals(string.Empty))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância a camada de negocios
                    AuditoriaOcorrenciaNegocios ocorrenciaNegocios = new AuditoriaOcorrenciaNegocios();

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridOcorrencia.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    if (chkEstoque.Checked == true && txtWms.Text.Equals(string.Empty))
                    {
                        if (txtErp.Text.Equals(string.Empty) && txtWms.Text.Equals(string.Empty) && txtDiferenca.Text.Equals(string.Empty))
                        {
                            //Mensagem
                            MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (txtErp.Text.Equals(string.Empty))
                        {
                            txtErp.Text = "0";
                        }

                        if (txtWms.Text.Equals(string.Empty))
                        {
                            txtWms.Text = "0";
                        }

                        if (txtDiferenca.Text.Equals(string.Empty))
                        {
                            txtDiferenca.Text = "0";
                        }

                        string status = null;

                        //Verifica se o cliente está no aguardo
                        if (Convert.ToString(gridOcorrencia.Rows[indice].Cells[25].Value).Equals("True"))
                        {
                            status = "ROTEIRIZACAO";
                        }
                        else
                        {
                            status = "FINALIZADA";
                        }

                        string codUsuarioErro = null;

                        if (!txtCodUsuario.Text.Equals(""))
                        {
                            codUsuarioErro = txtCodUsuario.Text;
                        }

                        ocorrenciaNegocios.AlterarOcorrencia(Convert.ToInt32(gridOcorrencia.Rows[indice].Cells[1].Value), Convert.ToInt32(gridOcorrencia.Rows[indice].Cells[2].Value), codUsuario, codUsuarioErro, cmbArea.Text, Convert.ToInt32(txtErp.Text), Convert.ToInt32(txtWms.Text), txtObservacaoAuditoria.Text, vetorImagens, status, cmbEmpresa.Text);

                        //Seta as informações no grid
                        gridOcorrencia.Rows[indice].Cells[4].Value = DateTime.Now; //Adiciona data de auditoria
                        gridOcorrencia.Rows[indice].Cells[8].Value = cmbArea.SelectedItem; //Adiciona data de auditoria
                        gridOcorrencia.Rows[indice].Cells[16].Value = txtErp.Text; //Adiciona o estoque do erp
                        gridOcorrencia.Rows[indice].Cells[17].Value = txtWms.Text; //Adiciona o estoque do wms
                        gridOcorrencia.Rows[indice].Cells[20].Value = txtObservacaoAuditoria.Text; //Adiciona a observação
                        gridOcorrencia.Rows[indice].Cells[21].Value = vetorImagens; //Adiciona a imagem
                        gridOcorrencia.Rows[indice].Cells[22].Value = status; //Adiciona o status
                        gridOcorrencia.Rows[indice].Cells[23].Value = txtCodUsuario.Text; //Adiciona o código do usuário
                        gridOcorrencia.Rows[indice].Cells[24].Value = lblUsuario.Text; //Adiciona o login do usuário

                        MessageBox.Show("Auditoria realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro realizar o registro! \nDetalhes: " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Habilita os campos
        private void Habilitar()
        {
            cmbArea.Enabled = true; //Habilita o campo área
            picFoto.Enabled = true;
            chkEstoque.Enabled = true; //Habilita o campo estoque
            txtCodUsuario.Enabled = true;
            txtObservacaoAuditoria.Enabled = true; //Habilita o campo observação
            btnSalvar.Enabled = true; //Habilita o campo salvar

            if (txtData.Text.Equals(""))
            {
                txtData.Text = DateTime.Now.ToString();
            }
        }

        //Desabilita os campos
        private void Desabilita()
        {
            cmbArea.Enabled = false; //Desabilita o campo área
            picFoto.Enabled = false;
            chkEstoque.Enabled = false; //Desabilita o campo estoque
            txtWms.Enabled = false; //Desabilita o campo estoque
            txtCodUsuario.Enabled = false;
            txtObservacaoAuditoria.Enabled = false; //Desabilita o campo observação
            btnSalvar.Enabled = false; //Desabilita o botão salvar
        }

        protected void CarregaImagem()
        {
            try
            {
                this.openFileDialog1.ShowDialog(this);
                string strFn = this.openFileDialog1.FileName;

                if (string.IsNullOrEmpty(strFn))
                    return;

                this.picFoto.Image = Image.FromFile(strFn);
                FileInfo arqImagem = new FileInfo(strFn);
                tamanhoArquivoImagem = arqImagem.Length;
                FileStream fs = new FileStream(strFn, FileMode.Open, FileAccess.Read, FileShare.Read);
                vetorImagens = new byte[Convert.ToInt32(this.tamanhoArquivoImagem)];
                int iBytesRead = fs.Read(vetorImagens, 0, Convert.ToInt32(this.tamanhoArquivoImagem));
                fs.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void FrmAuditoriaOcorrencia_Resize(object sender, EventArgs e)
        {
            /*  if (this.WindowState == FormWindowState.Maximized)
              {
                  picFoto.Location = new Point(400,32);
                  picFoto.Size = new Size(300, 200);
              }
              else if (this.WindowState == FormWindowState.Normal)
              {
                  picFoto.Location = new Point(349, 332);
                  picFoto.Size = new Size(159, 148);
              }*/
        }

        private void CalcularDiferença()
        {
            try
            {
                if (txtWms.Text != "0")
                {
                    if (txtErp.Text != "" && txtWms.Text != "")
                    {
                        int r = (Convert.ToInt32(txtWms.Text) - Convert.ToInt32(txtErp.Text));

                        txtDiferenca.Text = Convert.ToString(r);
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
