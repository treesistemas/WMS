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
using Wms.Impressao;
using Wms.Relatorio;

namespace Wms
{
    public partial class FrmOcorrencia : Form
    {
        string emailCliente; //Email do cliente

        public string nomeEmpresa; //Nome da empresa
        public int codUsuario; //Código do usuário
        public string loginUsuario; //Login do usuário
        private int codMotorista; //Código do motorista
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;
        //Controla o salvar ou alterar o cadastro
        Boolean opcao = false;

        //Array com id(Combobox)
        private int[] codTipoOcorrencia;
        //Instância a coleção de objêtos
        TipoOcorrenciaCollection tipoOcorrenciaCollection = new TipoOcorrenciaCollection();

        //Instância a camada de objêto
        ItensOcorrenciaCollection itensOcorrenciaCollection = new ItensOcorrenciaCollection();

        string tempoAlerta = "00:30:00";
        string tempoExcedido = "00:35:00";

        public FrmOcorrencia()
        {
            InitializeComponent();
        }

        private void FrmOcorrencia_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            // Enable timer.  
            timer1.Enabled = true;
            timer1.Start();

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
        private void txtPesqCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPesqManifesto.Focus();
            }
        }

        private void txtPesqManifesto_KeyPress(object sender, KeyPressEventArgs e)
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
                txtPesqPedido.Focus();
            }
        }

        private void txtPesqPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPesqRepresentante.Focus();
            }
        }

        private void txtPesqRepresentante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPesqCliente.Focus();
            }
        }

        private void txtPesqCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPesqMonitor.Focus();
            }
        }

        private void txtPesqMonitor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPesqMotorista.Focus();
            }
        }

        private void txtPesqMotorista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbPesqTipo.Focus();
            }
        }

        private void cmbPesqTipo_KeyPress(object sender, KeyPressEventArgs e)
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

        //Changed
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Items.Count == 0)
            {
                PesqTipoOcorrencia();
            }

            if (cmbTipo.Items.Count > 0)
            {
                //Localizando os tipos
                List<TipoOcorrencia> tipo = tipoOcorrenciaCollection.FindAll(delegate (TipoOcorrencia n) { return n.descricao == cmbTipo.Text; });

                if (tipo != null)
                {
                    //Exibe a area
                    txtArea.Text = tipo[0].area;
                }
            }
        }

        private void chkGerarReentrega_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGerarReentrega.Checked == true)
            {
                lblProgramacao.Visible = true; //Exibel o label de programação
                dtmProgramacao.Visible = true; //Exibe o campo de programação
            }
            else
            {
                lblProgramacao.Visible = false; //Oculta o label de programação
                dtmProgramacao.Visible = false; //Oculta o campo de programação
            }
        }

        private void chkCancelar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCancelar.Checked == true)
            {
                if (chkPendente.Checked == true || chkDevolucao.Checked == true || chkGerarReentrega.Checked == true)
                {
                    //Marca o botão novamente
                    chkCancelar.Checked = false;
                    MessageBox.Show("Não é possível cancelar a ocorrência com mais de um status!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtPesqCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqCliente frame = new FrmPesqCliente();

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe o código
                    txtPesqCliente.Text = Convert.ToString(frame.codCliente);
                    //Receme o login
                    lblPesqCliente.Text = frame.nmCliente;
                }
            }
        }

        private void txtPesqMonitor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqUsuario frame = new FrmPesqUsuario();
                frame.perfilUsuario = "";
                //Nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe o código
                    txtPesqMonitor.Text = Convert.ToString(frame.codUsuario);
                    //Receme o login
                    lblMonitor.Text = frame.nmUsuario;
                }
            }
        }

        private void txtPesqMotorista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqMotorista frame = new FrmPesqMotorista();

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe o código
                    txtPesqMotorista.Text = Convert.ToString(frame.codMotorista);
                    //Receme o login
                    lblPesqMotorista.Text = frame.apelidoMotorista;
                }
            }
        }

        private void txtMotoristaReentrega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqMotorista frame = new FrmPesqMotorista();

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe o código
                    txtMultiMotorista.Text = Convert.ToString(frame.codMotorista);
                    //Receme o login
                    lblMultiPesqMotorista.Text = frame.apelidoMotorista;
                }
            }
        }

        private void txtCodMotorista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqMotorista frame = new FrmPesqMotorista();

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe o código
                    txtCodMotorista.Text = Convert.ToString(frame.codMotorista);
                    //Receme o login
                    lblMotorista.Text = frame.apelidoMotorista;
                }
            }
        }

        private void txtPesqMonitor_TextChanged(object sender, EventArgs e)
        {
            if (txtPesqMonitor.Text.Length == 0)
            {
                lblMonitor.Text = "-";
            }
        }

        private void txtPesqMotorista_TextChanged(object sender, EventArgs e)
        {
            if (txtPesqMotorista.Text.Length == 0)
            {
                lblPesqMotorista.Text = "-";
            }
        }

        private void txtMotoristaReentrega_TextChanged(object sender, EventArgs e)
        {
            if (txtMultiMotorista.Text.Length == 0)
            {
                lblMultiPesqMotorista.Text = "-";
            }
        }

        //KeyPress
        private void txtCodPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPedido.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Digite o código do pedido ou o número da nota fiscal!", "Wms - Informação", MessageBoxButtons.OK);
                }
                else
                {
                    PesqPedido(txtPedido.Text, "");//Pesquisa as informações do pedido
                }
            }
        }

        private void txtNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtNotaFiscal.Text.Equals(string.Empty))
                {
                    txtPedido.Focus();
                }
                else
                {
                    PesqPedido("", txtNotaFiscal.Text);//Pesquisa as informações do pedido
                }
            }
        }

        private void txtManifestoOcorrencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                PesqManifesto();//Pesquisa o manifesto

            }
        }

        //Click
        private void cmbPesqTipo_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbPesqTipo.Items.Count == 0)
            {
                PesqTipoOcorrencia();
            }
        }

        private void cmbTipo_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbPesqTipo.Items.Count == 0)
            {
                PesqTipoOcorrencia();
            }
        }

        private void cmbMultiTipo_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbMultiTipo.Items.Count == 0)
            {
                PesqTipoOcorrencia();
            }
        }

        private void cmbMultiTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMultiTipo.Items.Count == 0)
            {
                PesqTipoOcorrencia();
            }
        }

        private void gridOcorrencia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
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

        private void gridReentrega_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Verifica se a coluna zero foi clicada
            if (gridMultiOcorrencia.Columns[e.ColumnIndex].Index == 0)
            {
                //Verifica se a coluna do checkbox foi clicada
                if (Convert.ToBoolean(gridMultiOcorrencia.Rows[e.RowIndex].Cells[0].Value) == true)
                {
                    gridMultiOcorrencia.Rows[e.RowIndex].Cells[0].Value = false;

                }
                else
                {
                    gridMultiOcorrencia.Rows[e.RowIndex].Cells[0].Value = true;
                }
            }
        }

        //Double Click
        private void gridOcorrencia_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                //Verifica o retorno da pesquisa
                if (gridOcorrencia.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridOcorrencia.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    if (Convert.ToString(gridOcorrencia.Rows[indice].Cells[4].Value).Equals("ROTEIRIZACAO") ||
                        Convert.ToString(gridOcorrencia.Rows[indice].Cells[4].Value).Equals("EXPEDICAO") ||
                        Convert.ToString(gridOcorrencia.Rows[indice].Cells[4].Value).Equals("FINALIZADA"))
                    {
                        //Mensagem
                        MessageBox.Show("Ocorrência não pode ser alterada", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        //Habilita os campos
                        Habilita();
                        //controle para alterar
                        opcao = false;
                    }
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!(cmbPesqTipo.Text.Equals("TODOS") || cmbPesqTipo.Text.Equals(string.Empty)))
            {
                PesqOcorrencia(txtPesqCodigo.Text, txtPesqPedido.Text, txtPesqNotaFiscal.Text, txtPesqManifesto.Text, txtPesqRepresentante.Text, txtPesqCliente.Text,
                                            txtPesqMonitor.Text, txtPesqMotorista.Text, codTipoOcorrencia[cmbPesqTipo.SelectedIndex], cmbPesqStatus.Text, cmbEmpresa.Text, dtmInicial.Text, dtmFinal.Text);
            }
            else
            {
                PesqOcorrencia(txtPesqCodigo.Text, txtPesqPedido.Text, txtPesqNotaFiscal.Text, txtPesqManifesto.Text, txtPesqRepresentante.Text, txtPesqCliente.Text,
                                           txtPesqMonitor.Text, txtPesqMotorista.Text, 0, cmbPesqStatus.Text, cmbEmpresa.Text, dtmInicial.Text, dtmFinal.Text);

            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Pesquisa um novo id
            PesqId();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tbpOcorrencia)
            {
                if (opcao == true)
                {
                    //Registra a ocorrencia
                    SalvarOcorrencia();
                }
                else if (opcao == false)
                {
                    //Altera a ocorrência
                    AlterarOcorrencia();
                }

            }
            else if (tabControl1.SelectedTab == tbpReentrega)
            {
                //Registra as reentregas
                SalvarMultiOcorrencia();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Limpa todos os campos
            LimparCampos();
        }

        private void mniRelatorio_Click(object sender, EventArgs e)
        {
            FrmImpressaoOcorrencia frame = new FrmImpressaoOcorrencia();
            frame.ShowDialog();
        }


        //Pesquisa os tipos de ocorrência
        private void PesqTipoOcorrencia()
        {
            try
            {
                //Limpa o combobox rua 
                cmbTipo.Items.Clear();
                //Adiciona o texto
                cmbPesqTipo.Text = "TODOS";

                tipoOcorrenciaCollection.Clear();
                //Instância a camada de negocios
                OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();

                //Pesquisa as ocorrencia
                tipoOcorrenciaCollection = ocorrenciaNegocios.PesqTipoOcorrencia();
                //Preenche o combobox da ocorrência
                tipoOcorrenciaCollection.ForEach(n => cmbTipo.Items.Add(n.descricao));
                //Preenche o combobox da reentrega
                tipoOcorrenciaCollection.ForEach(n => cmbMultiTipo.Items.Add(n.descricao));
                //Preenche o combobox de pesquisa
                tipoOcorrenciaCollection.ForEach(n => cmbPesqTipo.Items.Add(n.descricao));

                codTipoOcorrencia = new int[tipoOcorrenciaCollection.Count];

                for (int i = 0; i < tipoOcorrenciaCollection.Count; i++)
                {
                    //Preenche o array
                    codTipoOcorrencia[i] = tipoOcorrenciaCollection[i].codigo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa um novo id
        private void PesqId()
        {
            try
            {
                if (tabControl1.SelectedTab == tbpOcorrencia)
                {
                    //Limpa todos os campos
                    LimparCampos();
                    //Instância a categoria negocios
                    OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();
                    //Seta o novo id
                    lblCodigo.Text = Convert.ToString(ocorrenciaNegocios.PesqId());
                    //Controle para salvar 
                    opcao = true;
                    //Habilita componentes
                    Habilita();

                    //Foca no campo
                    txtNotaFiscal.Focus();
                }
                else
                {
                    //Limpa todos os campos
                    LimparCampos();
                    //Habilita componentes
                    Habilita();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa os pedido por manifesto
        private void PesqManifesto()
        {
            try
            {
                //Instância a camada de negocios
                OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();
                //Instância a camada de objêto
                OcorrenciaCollection ocorrenciaCollection = new OcorrenciaCollection();
                //A coleção recebe o resultado da consulta
                ocorrenciaCollection = ocorrenciaNegocios.PesqManifesto(txtMultiManifesto.Text, cmbEmpresa.Text);

                //Limpa o grid
                gridMultiOcorrencia.Rows.Clear();

                if (ocorrenciaCollection.Count > 0)
                {
                    //Grid Recebe o resultado da coleção
                    ocorrenciaCollection.ForEach(n => gridMultiOcorrencia.Rows.Add(false, n.notaFiscal, n.codPedido, n.codCliente + " - " + n.nomeCliente, n.fantasiaCliente, n.enderecoCliente + "," + n.numeroCliente, n.cidadeCliente, n.pagamentoPedido, n.nomeSupervisor, n.nomeRepresentante, n.motorista, string.Format(@"{0:N}", n.totalPedido)));
                    //Foca no grid
                    gridMultiOcorrencia.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum pedido encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o pedido para abrir a ocorrência
        private void PesqPedido(string pedido, string nota)
        {
            try
            {
                //Instância a camada de negocios
                OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();
                //Instância a camada de objêto
                Ocorrencia ocorrencia = new Ocorrencia();
                //A coleção recebe o resultado da consulta
                ocorrencia = ocorrenciaNegocios.PesqPedido(pedido, nota, cmbEmpresa.Text);

                if (ocorrencia.codPedido > 0)
                {
                    lblDataFaturamento.Text = Convert.ToString(ocorrencia.dataFaturamento);  //Data da nota fiscal
                    lblPedido.Text = Convert.ToString(ocorrencia.codPedido);  //Número da nota Pedido
                    lblNotaFiscal.Text = Convert.ToString(ocorrencia.notaFiscal);  //Número da nota Fiscal
                    lblTipoPedido.Text = Convert.ToString(ocorrencia.tipoPedido);  //Tipo de pedido
                    lblTotaPedido.Text = string.Format(@"{0:N}", ocorrencia.totalPedido);  //Valor total
                    lblPagamento.Text = Convert.ToString(ocorrencia.pagamentoPedido);  //tipo Pagamento 
                    lblManifesto.Text = Convert.ToString(ocorrencia.manifesto); //Manifesto
                    lblVeiculo.Text = Convert.ToString(ocorrencia.veiculoPedido);  //Placa
                    codMotorista = Convert.ToInt32(ocorrencia.codMotorista); //Código do motorista
                    lblNomeMotorista.Text = Convert.ToString(ocorrencia.codMotorista + "-" + ocorrencia.nomeMotorista + " (" + ocorrencia.celularMotorista + ") ");  //Motorista
                    lblObservacao.Text = Convert.ToString(ocorrencia.obsPedido);  //Observação do pedido
                    lblNomeCliente.Text = Convert.ToString(ocorrencia.codCliente + " - " + ocorrencia.nomeCliente);  //Nome do cliente 
                    lblFantasia.Text = Convert.ToString(ocorrencia.fantasiaCliente);  //Nome fantasia do cliente

                    lblUF.Text = Convert.ToString(ocorrencia.ufCliente);  //UF do cliente
                    lblCidade.Text = Convert.ToString(ocorrencia.cidadeCliente);  //cidade do cliente
                    lblBairro.Text = Convert.ToString(ocorrencia.bairroCliente);  //Bairro do cliente
                    lblEndereco.Text = Convert.ToString(ocorrencia.enderecoCliente + ", " + ocorrencia.numeroCliente);  //Endereço do cliente
                    lblRepresentante.Text = Convert.ToString(ocorrencia.nomeRepresentante + " ( " + ocorrencia.celularRepresentante + " )");  //Nome do representante
                    lblSupervisor.Text = Convert.ToString(ocorrencia.nomeSupervisor + " ( " + ocorrencia.celularSupervisor + " )");  //Nome do representante

                    emailCliente = Convert.ToString(ocorrencia.emailCliente);  //Variável global - Email do cliente

                    lblImplantador.Text = Convert.ToString(ocorrencia.pedImplantador);  //Nome do implantador


                    //Pesquisa os itens do pedido
                    PesqItens(ocorrencia.codPedido);
                }
                else
                {
                    MessageBox.Show("Nenhuma pedido encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens do pedido
        private void PesqItens(int codPedido)
        {
            try
            {
                //Instância a camada de negocios
                OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();
                //Instância a camada de objêto
                ItensPedidoCollection itemPedidoCollection = new ItensPedidoCollection();
                //O objêto recebe o resultado da consulta
                itemPedidoCollection = ocorrenciaNegocios.PesqItem(codPedido, cmbEmpresa.Text);
                //Limpa o grid
                gridItens.Rows.Clear();

                if (itemPedidoCollection.Count > 0)
                {
                    //Grid Recebe o resultado da coleção
                    itemPedidoCollection.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.idProduto, n.codProduto + " - " + n.descProduto, n.qtdProduto, n.uniUnidade, "", "", "", "", "", "", string.Format(@"{0:N}", n.valorTotal/ n.qtdProduto), string.Format(@"{0:N}", n.valorTotal)));

                    //Seleciona a primeira linha do grid
                    gridItens.CurrentCell = gridItens.Rows[0].Cells[0];
                    //Foca no grid
                    gridItens.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum item encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa a ocorrência
        private void PesqOcorrencia(string codOcorrencia, string codPedido, string codNota, string codManifesto, string codRepresentante, string codCliente,
                                    string codUsuario, string codMotorista, int tipo, string status, string empresa, string dataIncial, string dataFinal)
        {
            try
            {
                //Instância a camada de negocios
                OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();
                //Instância a camada de objêto
                OcorrenciaCollection ocorrenciaOcollection = new OcorrenciaCollection();

                //Pesquisa a ocorrência
                ocorrenciaOcollection = ocorrenciaNegocios.PesqOcorrencia(codOcorrencia, codPedido, codNota, codManifesto, codRepresentante, codCliente,
                                                                          codUsuario, codMotorista, tipo, status, empresa, dataIncial, dataFinal);
                //Pesquisa os itens da ocorrência
                itensOcorrenciaCollection = ocorrenciaNegocios.PesqItemOcorrencia(codOcorrencia, codPedido, codNota, codManifesto, codRepresentante, codCliente,
                                                                          codUsuario, codMotorista, tipo, status, empresa, dataIncial, dataFinal);
                //Limpa o grid
                gridOcorrencia.Rows.Clear();
                //Seta a quantidade de ocorrência
                lblQtd.Text = Convert.ToString(ocorrenciaOcollection.Count);

                //Verifica o retorno da pesquisa
                if (ocorrenciaOcollection.Count > 0)
                {
                    //Grid Recebe o resultado da coleção
                    ocorrenciaOcollection.ForEach(n => gridOcorrencia.Rows.Add(gridOcorrencia.Rows.Count + 1, n.dataOcorrencia, n.loginUsuario, n.notaFiscal, n.codPedido, n.statusOcorrencia,
                        n.codOcorrencia, n.dataFaturamento, string.Format(@"{0:N}", n.totalPedido), n.tipoPedido, n.pagamentoPedido, n.codCliente + " - " + n.nomeCliente, n.fantasiaCliente,
                        n.enderecoCliente + ", " + n.numeroCliente, n.cidadeCliente, n.bairroCliente, n.ufCliente,
                        n.manifesto, n.veiculoPedido, n.codMotorista, n.codMotorista + " - " + n.nomeMotorista + " (" + n.celularMotorista + ")",
                        n.nomeRepresentante + " (" + n.celularRepresentante + ")", n.nomeSupervisor, n.obsPedido,
                        n.descTipoOcorrencia, n.areaOcorrencia, n.manifestoOcorrencia, n.codMotoristaOcorrencia, n.motoristaOcorrencia,
                        n.gerarPendencia, n.gerarDevolucao, n.gerarReentrega, n.obsOcorrencia, "", "", "", "", n.pedImplantador, n.gerarClienteAguardo, n.dataReentrega));


                    //Seleciona a primeira linha do grid
                    gridOcorrencia.CurrentCell = gridOcorrencia.Rows[0].Cells[0];
                    //Foca no grid
                    gridOcorrencia.Focus();
                    //Exibe os dados nos campos
                    DadosCampos();

                    //Altera as cores do grid
                    CorGrid();
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

                    txtNotaFiscal.Text = string.Empty; //Limpa o campo da nota fiscal
                    txtPedido.Text = string.Empty; // Limpa o campo do pedido
                    lblNotaFiscal.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[3].Value);  //Número da nota Fiscal
                    lblPedido.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[4].Value);  //Número da nota Pedido
                    lblCodigo.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[6].Value);  //Código da acorrencia
                    lblDataFaturamento.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[7].Value);  //Data da nota fiscal
                    lblTotaPedido.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[8].Value);  //Valor total
                    lblTipoPedido.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[9].Value);  //Tipo de pedido
                    lblPagamento.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[10].Value);  //tipo Pagamento 
                    lblNomeCliente.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[11].Value);  //Nome do cliente 
                    lblFantasia.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[12].Value);  //Nome fantasia do cliente
                    lblEndereco.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[13].Value);  //Endereço do cliente
                    lblCidade.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[14].Value);  //cidade do cliente
                    lblBairro.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[15].Value);  //Bairro do cliente
                    lblUF.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[16].Value);  //UF do cliente
                    lblManifesto.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[17].Value); //Manifesto
                    lblVeiculo.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[18].Value);  //Placa
                    codMotorista = Convert.ToInt32(gridOcorrencia.Rows[indice].Cells[19].Value); //Código do motorista
                    lblNomeMotorista.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[20].Value);  //Motorista              
                    lblRepresentante.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[21].Value);  //Nome do representante
                    lblSupervisor.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[22].Value);  //Nome do representante
                    lblObservacao.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[23].Value);  //Observação do pedido

                    cmbTipo.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[24].Value);  //Tipo de Ocorrencia
                    txtArea.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[25].Value);  //Area da ocorrencia
                    txtManifesto.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[26].Value);  //Manifesto da ocorrencia
                    txtCodMotorista.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[27].Value);  //Código do motorista
                    lblMotorista.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[28].Value);  //Motorista da ocorrencia

                    chkPendente.Checked = Convert.ToBoolean(gridOcorrencia.Rows[indice].Cells[29].Value);  //
                    chkClienteAguardo.Checked = Convert.ToBoolean(gridOcorrencia.Rows[indice].Cells[38].Value);  //
                    chkDevolucao.Checked = Convert.ToBoolean(gridOcorrencia.Rows[indice].Cells[30].Value);
                    chkGerarReentrega.Checked = Convert.ToBoolean(gridOcorrencia.Rows[indice].Cells[31].Value);
                    txtObservacao.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[32].Value);  //Observação da ocorrência

                    //Verifica os status
                    if (Convert.ToString(gridOcorrencia.Rows[indice].Cells[5].Value).Equals("CANCELADA"))
                    {
                        chkCancelar.Checked = true;
                    }
                    else
                    {
                        chkCancelar.Checked = false;
                    }

                    if (chkGerarReentrega.Checked == true)
                    {
                        dtmProgramacao.Value = Convert.ToDateTime(gridOcorrencia.Rows[indice].Cells[39].Value);
                    }

                    lblImplantador.Text = Convert.ToString(gridOcorrencia.Rows[indice].Cells[37].Value);  //Usuário que implantou

                    //Limpa o grid
                    gridItens.Rows.Clear();
                    //Localizando os empilhadores e ordena por turno
                    List<ItensOcorrencia> itensOcorrencia = itensOcorrenciaCollection.FindAll(delegate (ItensOcorrencia n) { return n.codItemOcorrencia == Convert.ToInt32(gridOcorrencia.Rows[indice].Cells[6].Value); });

                    //grid recebe o resultado da coleção
                    itensOcorrencia.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.idProduto, n.codProduto + " - " + n.descProduto, n.qtdProduto, n.fatorProduto,
                                            n.qtdAvariaProduto, n.qtdFaltaProduto, n.qtdTrocaProduto, n.qtdCriticaProduto, n.DataCriticaProduto, n.qtdDevolucao, string.Format(@"{0:N}", n.valorUnitario), string.Format(@"{0:N}", n.valorProduto)));


                    //Desabilita os campos
                    Desabilita();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Registra a ocorrência
        private void SalvarOcorrencia()
        {
            try
            {
                if (cmbTipo.Text.Equals("SELECIONE") || txtArea.Text.Equals("SELECIONE"))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                else if (lblManifesto.Text.Equals(string.Empty))
                {
                    //Mensagem
                    MessageBox.Show("Pedido não associado a nenhum manifesto!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância a camada de negocios
                    OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();
                    //Instância a camada de objêto
                    Ocorrencia ocorrencia = new Ocorrencia();
                    ocorrencia.codOcorrencia = Convert.ToInt32(lblCodigo.Text);
                    ocorrencia.dataOcorrencia = DateTime.Now;
                    ocorrencia.areaOcorrencia = txtArea.Text;
                    ocorrencia.codPedido = Convert.ToInt32(lblPedido.Text);

                    ocorrencia.nomeCliente = Convert.ToString(lblNomeCliente.Text);
                    ocorrencia.fantasiaCliente = Convert.ToString(lblFantasia.Text);
                    ocorrencia.enderecoCliente = Convert.ToString(lblEndereco.Text);
                    ocorrencia.cidadeCliente = Convert.ToString(lblCidade.Text);
                    ocorrencia.pagamentoPedido = Convert.ToString(lblPagamento.Text);
                    ocorrencia.totalPedido = Convert.ToDouble(lblTotaPedido.Text);
                    ocorrencia.nomeSupervisor = Convert.ToString(lblSupervisor.Text);
                    ocorrencia.nomeRepresentante = Convert.ToString(lblRepresentante.Text);
                    ocorrencia.motorista = Convert.ToString(lblNomeMotorista.Text);
                    ocorrencia.manifesto = Convert.ToInt32(lblManifesto.Text);

                    //Verifica se o manifesto da ocorrencia está vazio
                    if (txtManifesto.Text == string.Empty)
                    {
                        ocorrencia.manifestoOcorrencia = Convert.ToInt32(lblManifesto.Text);
                    }
                    else
                    {
                        ocorrencia.manifestoOcorrencia = Convert.ToInt32(txtManifesto.Text);
                    }


                    ocorrencia.codMotorista = Convert.ToInt32(codMotorista);

                    //Verifica se o manifesto da ocorrencia está vazio
                    if (txtCodMotorista.Text == string.Empty)
                    {
                        ocorrencia.codMotoristaOcorrencia = Convert.ToInt32(codMotorista);
                    }
                    else
                    {
                        ocorrencia.codMotoristaOcorrencia = Convert.ToInt32(txtCodMotorista.Text);
                    }


                    ocorrencia.codTipoOcorrencia = codTipoOcorrencia[cmbTipo.SelectedIndex];
                    ocorrencia.descTipoOcorrencia = Convert.ToString(cmbTipo.Text);
                    ocorrencia.gerarClienteAguardo = chkClienteAguardo.Checked;
                    ocorrencia.gerarDevolucao = chkDevolucao.Checked;
                    ocorrencia.gerarReentrega = chkGerarReentrega.Checked;
                    ocorrencia.dataReentrega = dtmProgramacao.Value;

                    ocorrencia.obsOcorrencia = txtObservacao.Text;
                    ocorrencia.codUsuarioOcorrencia = codUsuario;


                    if (chkGerarReentrega.Checked == true)
                    {
                        ocorrencia.statusOcorrencia = "ROTEIRIZACAO";
                    }
                    else if (chkDevolucao.Checked == true)
                    {
                        ocorrencia.statusOcorrencia = "DEVOLUCAO";
                    }
                    else if (chkPendente.Checked == true)
                    {
                        ocorrencia.statusOcorrencia = "PENDENTE";
                    }
                    else
                    {
                        ocorrencia.statusOcorrencia = "FINALIZADA";
                    }


                    if (gridItens.Rows.Count > 0)
                    {
                        //Verifica se há itens para auditoria
                        for (int i = 0; gridItens.Rows.Count > i; i++)
                        {
                            if (!gridItens.Rows[i].Cells[5].Value.Equals(string.Empty))
                            {
                                ocorrencia.statusOcorrencia = "AUDITORIA";
                            }

                            if (!gridItens.Rows[i].Cells[6].Value.Equals(string.Empty))
                            {
                                ocorrencia.statusOcorrencia = "AUDITORIA";
                            }

                            if (!gridItens.Rows[i].Cells[7].Value.Equals(string.Empty))
                            {
                                ocorrencia.statusOcorrencia = "AUDITORIA";
                            }

                            if (!gridItens.Rows[i].Cells[8].Value.Equals(string.Empty))
                            {
                                ocorrencia.statusOcorrencia = "AUDITORIA";
                            }

                        }
                    }

                    if (chkCancelar.Checked == true)
                    {
                        ocorrencia.statusOcorrencia = "CANCELADA";
                    }

                    // Passa o objeto para a camada de negocios
                    ocorrenciaNegocios.SalvarOcorrencia(ocorrencia, cmbEmpresa.Text);
                    //controle para alterar
                    opcao = false;
                    //Registra os itens da ocorrência
                    AlterarItensOcorrencia();
                    //Gera a ocorrência em arquivo txt
                    GerarOcorrenciaTxt(ocorrencia);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro realizar o registro! \nDetalhes: " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Registra as reentregas
        private void SalvarMultiOcorrencia()
        {
            try
            {
                //MessageBox.Show(""+ codTipoOcorrencia[cmbMultiTipo.SelectedIndex] +" QTD "+ codTipoOcorrencia.Count());

                if (codTipoOcorrencia[cmbMultiTipo.SelectedIndex] == 0)
                {
                    //Mensagem
                    MessageBox.Show("Selecione o tipo de ocorrência", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (cmbMultiTipo.Text.Equals("SELECIONE") || cmbMultiArea.Text.Equals("SELECIONE"))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (txtMultiMotorista.Text.Equals(string.Empty))
                {
                    //Mensagem
                    MessageBox.Show("Preencha o campo motorista!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //Instância a camada de negocios
                    OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();

                    OcorrenciaCollection ocorrenciaCollection = new OcorrenciaCollection();

                    for (int i = 0; gridMultiOcorrencia.Rows.Count > i; i++)
                    {
                        if (Convert.ToBoolean(gridMultiOcorrencia.Rows[i].Cells[0].Value) == true)
                        {
                            //Instância a camada de objêto
                            Ocorrencia ocorrencia = new Ocorrencia();
                            ocorrencia.codOcorrencia = Convert.ToInt32(ocorrenciaNegocios.PesqId());
                            ocorrencia.dataOcorrencia = DateTime.Now;
                            ocorrencia.manifesto = Convert.ToInt32(txtMultiManifesto.Text);
                            ocorrencia.manifestoOcorrencia = Convert.ToInt32(txtMultiManifesto.Text);
                            ocorrencia.codTipoOcorrencia = codTipoOcorrencia[cmbMultiTipo.SelectedIndex];
                            ocorrencia.areaOcorrencia = cmbMultiArea.Text;
                            ocorrencia.codMotorista = Convert.ToInt32(txtMultiMotorista.Text);
                            ocorrencia.codMotoristaOcorrencia = Convert.ToInt32(txtMultiMotorista.Text);// null;                           

                            if (chkMultiReentrega.Checked == true)
                            {
                                ocorrencia.gerarReentrega = true;
                                ocorrencia.dataReentrega = dtmMultiProgramacao.Value;
                            }

                            ocorrencia.obsOcorrencia = txtMultiObservacao.Text;

                            ocorrencia.codPedido = Convert.ToInt32(gridMultiOcorrencia.Rows[i].Cells[2].Value);
                            ocorrencia.nomeCliente = Convert.ToString(gridMultiOcorrencia.Rows[i].Cells[3].Value);
                            ocorrencia.fantasiaCliente = Convert.ToString(gridMultiOcorrencia.Rows[i].Cells[4].Value);
                            ocorrencia.enderecoCliente = Convert.ToString(gridMultiOcorrencia.Rows[i].Cells[5].Value);
                            ocorrencia.cidadeCliente = Convert.ToString(gridMultiOcorrencia.Rows[i].Cells[6].Value);
                            ocorrencia.pagamentoPedido = Convert.ToString(gridMultiOcorrencia.Rows[i].Cells[7].Value);
                            ocorrencia.totalPedido = Convert.ToDouble(gridMultiOcorrencia.Rows[i].Cells[11].Value);
                            ocorrencia.nomeSupervisor = Convert.ToString(gridMultiOcorrencia.Rows[i].Cells[8].Value);
                            ocorrencia.nomeRepresentante = Convert.ToString(gridMultiOcorrencia.Rows[i].Cells[9].Value);
                            ocorrencia.motorista = Convert.ToString(gridMultiOcorrencia.Rows[i].Cells[10].Value);
                            ocorrencia.descTipoOcorrencia = Convert.ToString(cmbMultiTipo.Text);
                            ocorrencia.obsOcorrencia = txtMultiObservacao.Text;
                            ocorrencia.codUsuarioOcorrencia = codUsuario;

                            //Status do comercial
                            if (cmbMultiArea.Text.Equals("COMERCIAL"))
                            {
                                if (chkMultiReentrega.Checked == true)
                                {
                                    ocorrencia.statusOcorrencia = "ROTEIRIZACAO";
                                }
                                else
                                {
                                    if (chkMultiPendente.Checked == true)
                                    {
                                        ocorrencia.statusOcorrencia = "PENDENTE";
                                    }
                                    else
                                    {
                                        ocorrencia.statusOcorrencia = "FINALIZADA";
                                    }
                                }
                            }//Status da logistica
                            else if (cmbMultiArea.Text.Equals("LOGISTICA"))
                            {
                                if (chkMultiReentrega.Checked == true)
                                {
                                    ocorrencia.statusOcorrencia = "ROTEIRIZACAO";
                                }
                                else if (chkMultiReentrega.Checked == false)
                                {
                                    if (chkMultiPendente.Checked == true)
                                    {
                                        ocorrencia.statusOcorrencia = "PENDENTE";
                                    }
                                    else
                                    {
                                        ocorrencia.statusOcorrencia = "FINALIZADA";
                                    }
                                }
                            }

                            if (chkMultiDevolucao.Checked == true)
                            {
                                ocorrencia.statusOcorrencia = "DEVOLUCAO";
                            }

                            // Passa o objeto para a camada de negocios
                            ocorrenciaNegocios.SalvarOcorrencia(ocorrencia, cmbEmpresa.Text);


                            //Adiciona para o objêto para a coleção
                            ocorrenciaCollection.Add(ocorrencia);


                        }

                        //Gera arquivo txt
                        GerarOcorrenciaReentregaTxt(ocorrenciaCollection);

                        //Desabilita
                        Desabilita();


                    }

                    MessageBox.Show("Reentrega(s) registradas com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro realizar o registro! \nDetalhes: " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Alterar a ocorrência
        private void AlterarOcorrencia()
        {
            try
            {
                if (cmbTipo.Text.Equals("SELECIONE") || txtArea.Text.Equals("SELECIONE"))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Pesquisa os tipos de ocorrencia
                    PesqTipoOcorrencia();
                    //Seleciona o item
                    cmbTipo.SelectedItem = cmbTipo.Text;

                    //Instância a camada de negocios
                    OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();
                    //Instância a camada de objêto
                    Ocorrencia ocorrencia = new Ocorrencia();
                    ocorrencia.codTipoOcorrencia = codTipoOcorrencia[cmbTipo.SelectedIndex];
                    ocorrencia.codOcorrencia = Convert.ToInt32(lblCodigo.Text);
                    ocorrencia.areaOcorrencia = txtArea.Text;

                    ocorrencia.gerarClienteAguardo = chkClienteAguardo.Checked;
                    ocorrencia.gerarDevolucao = chkDevolucao.Checked;
                    ocorrencia.gerarReentrega = chkGerarReentrega.Checked;
                    ocorrencia.dataReentrega = dtmProgramacao.Value;
                    ocorrencia.obsOcorrencia = txtObservacao.Text;
                    ocorrencia.tempoOcorrencia = Convert.ToString(gridOcorrencia.Rows[gridOcorrencia.CurrentRow.Index].Cells[34].Value);

                    //Verifica se o manifesto da ocorrencia está vazio
                    if (txtManifesto.Text == string.Empty)
                    {
                        ocorrencia.manifestoOcorrencia = Convert.ToInt32(lblManifesto.Text);
                    }
                    else
                    {
                        ocorrencia.manifestoOcorrencia = Convert.ToInt32(txtManifesto.Text);
                    }


                    ocorrencia.codMotorista = Convert.ToInt32(codMotorista);

                    //Verifica se o manifesto da ocorrencia está vazio
                    if (txtCodMotorista.Text == string.Empty)
                    {
                        ocorrencia.codMotoristaOcorrencia = Convert.ToInt32(codMotorista);
                    }
                    else
                    {
                        ocorrencia.codMotoristaOcorrencia = Convert.ToInt32(txtCodMotorista.Text);
                    }


                    if (chkGerarReentrega.Checked == true)
                    {
                        ocorrencia.statusOcorrencia = "ROTEIRIZACAO";
                    }
                    else if (chkDevolucao.Checked == true)
                    {
                        ocorrencia.statusOcorrencia = "DEVOLUCAO";
                    }
                    else if (chkPendente.Checked == true)
                    {
                        ocorrencia.statusOcorrencia = "PENDENTE";
                    }
                    else
                    {
                        ocorrencia.statusOcorrencia = "FINALIZADA";
                    }

                    if (gridItens.Rows.Count > 0)
                    {
                        //Verifica se há itens para auditoria
                        for (int i = 0; gridItens.Rows.Count > i; i++)
                        {
                            if (!Convert.ToString(gridItens.Rows[i].Cells[5].Value).Equals(string.Empty)) //Avaria
                            {
                                if (Convert.ToInt32(gridItens.Rows[i].Cells[5].Value) > 0) //Avaria
                                {
                                    ocorrencia.statusOcorrencia = "AUDITORIA";
                                }
                            }

                            if (!Convert.ToString(gridItens.Rows[i].Cells[6].Value).Equals(string.Empty)) //Avaria
                            {
                                if (Convert.ToInt32(gridItens.Rows[i].Cells[6].Value) > 0)
                                {
                                    ocorrencia.statusOcorrencia = "AUDITORIA";
                                }
                            }

                            if (!Convert.ToString(gridItens.Rows[i].Cells[7].Value).Equals(string.Empty)) //Avaria
                            {
                                if (Convert.ToInt32(gridItens.Rows[i].Cells[7].Value) > 0)
                                {
                                    ocorrencia.statusOcorrencia = "AUDITORIA";
                                }
                            }

                            if (!Convert.ToString(gridItens.Rows[i].Cells[8].Value).Equals(string.Empty)) //Avaria
                            {
                                if (Convert.ToInt32(gridItens.Rows[i].Cells[8].Value) > 0)
                                {
                                    ocorrencia.statusOcorrencia = "AUDITORIA";
                                }
                            }

                        }
                    }

                    if (chkCancelar.Checked == true)
                    {
                        ocorrencia.statusOcorrencia = "CANCELADA";
                    }

                    // Passa o objeto para a camada de negocios
                    ocorrenciaNegocios.AlterarOcorrencia(ocorrencia, cmbEmpresa.Text);
                    //Alterar os itens da ocorrência
                    AlterarItensOcorrencia();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro realizar o registro! \nDetalhes: " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Alterar os itens da ocorrência
        private void AlterarItensOcorrencia()
        {
            try
            {
                //Instância a camada de negocios
                OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();


                //Percorre  grid para salvar os itens
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    //Instância a camada de objêto
                    ItensOcorrencia item = new ItensOcorrencia();
                    //Código da ocorrência
                    item.codItemOcorrencia = Convert.ToInt32(lblCodigo.Text); //valor do produto
                    item.codPedido = Convert.ToInt32(lblPedido.Text);
                    item.idProduto = Convert.ToInt32(gridItens.Rows[i].Cells[1].Value); //Id do produto
                    item.qtdProduto = Convert.ToInt32(gridItens.Rows[i].Cells[3].Value); //quantidade do produto

                    //Verifica se a coluna de falta está vázia
                    if (Convert.ToString(gridItens.Rows[i].Cells[5].Value) != string.Empty)
                    {
                        if (Convert.ToInt32(gridItens.Rows[i].Cells[5].Value) > Convert.ToInt32(gridItens.Rows[i].Cells[3].Value))
                        {
                            MessageBox.Show("A quantidade avariada do produto " + gridItens.Rows[i].Cells[2].Value + " não pode ser maior que a quantidade no pedido", "WMS - Informação");
                            break;
                        }
                        else
                        {
                            item.qtdAvariaProduto = Convert.ToInt32(gridItens.Rows[i].Cells[5].Value); //avaria do produto
                        }
                    }

                    //Verifica se a coluna de falta está vázia
                    if (Convert.ToString(gridItens.Rows[i].Cells[6].Value) != string.Empty)
                    {
                        //Verifica se a quantidade digitada é maior que a quantidade do pedido
                        if (Convert.ToInt32(gridItens.Rows[i].Cells[6].Value) > Convert.ToInt32(gridItens.Rows[i].Cells[3].Value))
                        {
                            MessageBox.Show("A quantidade de falta do produto " + gridItens.Rows[i].Cells[2].Value + " não pode ser maior que a quantidade no pedido", "WMS - Informação");
                            break;
                        }
                        else
                        {
                            item.qtdFaltaProduto = Convert.ToInt32(gridItens.Rows[i].Cells[6].Value); //falta do produto
                        }
                    }

                    //Verifica se a coluna de troca está vázia
                    if (Convert.ToString(gridItens.Rows[i].Cells[7].Value) != string.Empty)
                    {
                        if (Convert.ToInt32(gridItens.Rows[i].Cells[7].Value) > Convert.ToInt32(gridItens.Rows[i].Cells[3].Value))
                        {
                            MessageBox.Show("A quantidade em desacordo do produto " + gridItens.Rows[i].Cells[2].Value + " não pode ser maior que a quantidade no pedido", "WMS - Informação");
                            break;
                        }
                        else
                        {
                            item.qtdTrocaProduto = Convert.ToInt32(gridItens.Rows[i].Cells[7].Value); //troca do produto
                        }
                    }

                    //Verifica se a coluna de data crítica está vázia
                    if (Convert.ToString(gridItens.Rows[i].Cells[8].Value) != string.Empty)
                    {
                        if (Convert.ToInt32(gridItens.Rows[i].Cells[8].Value) > Convert.ToInt32(gridItens.Rows[i].Cells[3].Value))
                        {
                            MessageBox.Show("A quantidade com data crítica do produto " + gridItens.Rows[i].Cells[2].Value + " não pode ser maior que a quantidade no pedido", "WMS - Informação");
                            break;
                        }
                        else
                        {
                            item.qtdCriticaProduto = Convert.ToInt32(gridItens.Rows[i].Cells[8].Value); //quantidade critica do produto
                        }

                        //Verifica se a coluna de data crítica está vázia
                        if (Convert.ToString(gridItens.Rows[i].Cells[9].Value).Equals(string.Empty) && Convert.ToInt32(gridItens.Rows[i].Cells[8].Value) > 0)
                        {
                            MessageBox.Show("Digite a data crítica do produto " + gridItens.Rows[i].Cells[2].Value + "!", "WMS - Informação");
                            break;
                        }
                        else
                        {
                            item.DataCriticaProduto = Convert.ToDateTime(gridItens.Rows[i].Cells[9].Value); //data do produto
                        }

                    }

                    //Verifica se a coluna de troca está vázia
                    if (Convert.ToString(gridItens.Rows[i].Cells[10].Value) != string.Empty)
                    {
                        if (Convert.ToInt32(gridItens.Rows[i].Cells[10].Value) > Convert.ToInt32(gridItens.Rows[i].Cells[3].Value))
                        {
                            MessageBox.Show("A quantidade  de devolução do produto " + gridItens.Rows[i].Cells[2].Value + " não pode ser maior que a quantidade no pedido", "WMS - Informação");
                            break;
                        }
                        else
                        {
                            item.qtdDevolucao = Convert.ToInt32(gridItens.Rows[i].Cells[10].Value); //troca do produto
                        }
                    }

                    item.valorProduto = Convert.ToDouble(gridItens.Rows[i].Cells[11].Value); //valor do produto

                    //Salva os itens da ocorrência
                    ocorrenciaNegocios.AlterarItem(item);

                    if ((gridItens.Rows.Count - 1) == i)
                    {
                        //Desabilita todos os campos
                        Desabilita();
                        //controle para alterar
                        opcao = false;

                        MessageBox.Show("Ocorrencia registrada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro realizar o registro de itens da ocorrência! \nDetalhes: " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Gerar a ocorrencia em arquivo txt
        private void GerarOcorrenciaTxt(Ocorrencia ocorrencia)
        {
            try
            {
                //Caminho da área de trabalho
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/OCORRÊNCIAS";

                //Verifica se a pasta existe
                if (!Directory.Exists(desktop))
                {
                    //Cria a pasta
                    DirectoryInfo di = Directory.CreateDirectory(desktop);
                }

                //Nome do arquivo
                string nomeArquivo = ocorrencia.codPedido + "" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".txt";

                //Combina o caminho do arquivo com o nome do arquivo
                string filePath = Path.Combine(desktop, nomeArquivo);
                //Chama o refresh (Limpa o arquivo)
                //var arquivo = new FileInfo(filePath);

                //declarando a variavel do tipo StreamWriter para abrir ou criar um arquivo para escrita
                StreamWriter linha; //arquivo.CreateText();
                                    //utilizando o metodo para criar um arquivo texto e associando o caminho e nome ao metodo
                linha = File.CreateText(filePath);

                // primeira linha é o cabecalho
                //escrevendo o titulo
                linha.WriteLine("--------------------------------------------------------------------------------------");
                linha.WriteLine("*" + ocorrencia.areaOcorrencia + " OCORRÊNCIA " + ocorrencia.codOcorrencia + "*");
                linha.WriteLine("--------------------------------------------------------------------------------------");
                linha.WriteLine(); //pulando linha sem escrita
                linha.WriteLine("*TIPO DE OCORRENCIA: " + ocorrencia.descTipoOcorrencia + "*");
                linha.WriteLine(); //pulando linha sem escrita
                                   //escrevendo conteúdo               
                linha.WriteLine("*PEDIDO:* " + ocorrencia.codPedido);
                linha.WriteLine("*CLIENTE:* " + ocorrencia.nomeCliente);
                linha.WriteLine("*FANTASIA:* " + ocorrencia.fantasiaCliente);
                linha.WriteLine("*ENDERECO:* " + ocorrencia.enderecoCliente + ocorrencia.numeroCliente);
                linha.WriteLine("*CIDADE:* " + ocorrencia.cidadeCliente);
                linha.WriteLine("*FORMA DE PAGAMENTO:* " + ocorrencia.pagamentoPedido);
                linha.WriteLine("*VALOR:* " + ocorrencia.totalPedido);
                linha.WriteLine(); //pulando linha sem escrita
                linha.WriteLine("*SUPERVISOR:* " + ocorrencia.nomeSupervisor);
                linha.WriteLine("*CONSULTOR:* " + ocorrencia.nomeRepresentante);
                linha.WriteLine();  //pulando linha sem escrita
                linha.WriteLine("*MOTORISTA:* " + ocorrencia.motorista);
                linha.WriteLine("*RESPONSAVEL:* " + loginUsuario);
                linha.WriteLine();  //pulando linha sem escrita

                int contFalta = 0, contAvaria = 0, contTroca = 0, contData = 0;

                //Escreve os produto que faltaram
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    if (Convert.ToString(gridItens.Rows[i].Cells[6].Value) != string.Empty)
                    {
                        contFalta++; //Controla o cabeçalho Falta

                        if (contFalta == 1)
                        {
                            linha.WriteLine("FALTA");
                        }

                        linha.WriteLine(gridItens.Rows[i].Cells[2].Value + "  " + gridItens.Rows[i].Cells[6].Value + " " + gridItens.Rows[i].Cells[4].Value);
                    }
                }

                linha.WriteLine();  //pulando linha sem escrita

                //Escreve os produto que foi avariado
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    if (Convert.ToString(gridItens.Rows[i].Cells[5].Value) != string.Empty)
                    {
                        contAvaria++; //Controla o cabeçalho de avaria

                        if (contAvaria == 1)
                        {
                            linha.WriteLine("AVARIA");
                        }

                        linha.WriteLine(gridItens.Rows[i].Cells[2].Value + "  " + gridItens.Rows[i].Cells[5].Value + " " + gridItens.Rows[i].Cells[4].Value);
                    }
                }

                linha.WriteLine();  //pulando linha sem escrita

                //Escreve os produto que foi trocado
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    if (Convert.ToString(gridItens.Rows[i].Cells[7].Value) != string.Empty)
                    {
                        contTroca++; //Controla o cabeçalho troca

                        if (contTroca == 1)
                        {
                            linha.WriteLine("DESACORDO COM A NOTA");
                        }

                        linha.WriteLine(gridItens.Rows[i].Cells[2].Value + "  " + gridItens.Rows[i].Cells[7].Value + " " + gridItens.Rows[i].Cells[4].Value);
                    }
                }

                linha.WriteLine();  //pulando linha sem escrita

                //Escreve os produto que foi com data critica
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    if (Convert.ToString(gridItens.Rows[i].Cells[8].Value) != string.Empty)
                    {
                        contData++; //Controla o cabeçalho de data crítica

                        if (contData == 1)
                        {
                            linha.WriteLine("DATA CRITICA");
                        }

                        linha.WriteLine(Convert.ToString(gridItens.Rows[i].Cells[2].Value) + "  " + gridItens.Rows[i].Cells[8].Value + " " + gridItens.Rows[i].Cells[4].Value + " " + gridItens.Rows[i].Cells[9].Value);
                    }
                }

                //Escreve os produto que faltaram
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    if (Convert.ToString(gridItens.Rows[i].Cells[10].Value) != string.Empty)
                    {
                        contFalta++; //Controla o cabeçalho Falta

                        if (contFalta == 1)
                        {
                            linha.WriteLine("DEVOLUÇÃO");
                        }

                        linha.WriteLine(gridItens.Rows[i].Cells[2].Value + "  " + gridItens.Rows[i].Cells[10].Value + " " + gridItens.Rows[i].Cells[4].Value);
                    }
                }

                linha.WriteLine(txtObservacao.Text);

                linha.WriteLine();  //pulando linha sem escrita
                linha.WriteLine(nomeEmpresa);

                //Fecha o arquivo
                linha.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("O correu um erro ao gerar o arquivo! \n" + ex);
            }
        }

        //Gerar a ocorrencia em arquivo txt
        private void GerarOcorrenciaReentregaTxt(OcorrenciaCollection ocorrenciaCollection)
        {
            try
            {
                //Caminho da área de trabalho
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/OCORRÊNCIAS";

                //Verifica se a pasta existe
                if (!Directory.Exists(desktop))
                {
                    //Cria a pasta
                    DirectoryInfo di = Directory.CreateDirectory(desktop);
                }

                //Nome do arquivo
                string nomeArquivo = "MultiOcorrencia" + txtMultiManifesto.Text + "" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".txt";

                //Combina o caminho do arquivo com o nome do arquivo
                string filePath = Path.Combine(desktop, nomeArquivo);
                //Chama o refresh (Limpa o arquivo)
                //var arquivo = new FileInfo(filePath);

                //declarando a variavel do tipo StreamWriter para abrir ou criar um arquivo para escrita
                StreamWriter linha; //arquivo.CreateText();
                                    //utilizando o metodo para criar um arquivo texto e associando o caminho e nome ao metodo
                linha = File.CreateText(filePath);

                foreach (Ocorrencia ocorrencia in ocorrenciaCollection)
                {
                    // primeira linha é o cabecalho
                    //escrevendo o titulo
                    linha.WriteLine("--------------------------------------------------------------------------------------");
                    linha.WriteLine("*" + ocorrencia.areaOcorrencia + " OCORRÊNCIA " + ocorrencia.codOcorrencia + "*");
                    linha.WriteLine("--------------------------------------------------------------------------------------");
                    linha.WriteLine(); //pulando linha sem escrita
                    linha.WriteLine("*TIPO DE OCORRENCIA: " + ocorrencia.descTipoOcorrencia + "*");
                    linha.WriteLine(); //pulando linha sem escrita
                                       //escrevendo conteúdo               
                    linha.WriteLine("*PEDIDO:* " + ocorrencia.codPedido);
                    linha.WriteLine("*CLIENTE:* " + ocorrencia.nomeCliente);
                    linha.WriteLine("*FANTASIA:* " + ocorrencia.fantasiaCliente);
                    linha.WriteLine("*ENDERECO:* " + ocorrencia.enderecoCliente + ocorrencia.numeroCliente);
                    linha.WriteLine("*CIDADE:* " + ocorrencia.cidadeCliente);
                    linha.WriteLine("*FORMA DE PAGAMENTO:* " + ocorrencia.pagamentoPedido);
                    linha.WriteLine("*VALOR:* " + ocorrencia.totalPedido);
                    linha.WriteLine(); //pulando linha sem escrita
                    linha.WriteLine("*SUPERVISOR:* " + ocorrencia.nomeSupervisor);
                    linha.WriteLine("*CONSULTOR:* " + ocorrencia.nomeRepresentante);
                    linha.WriteLine();  //pulando linha sem escrita
                    linha.WriteLine("*MOTORISTA:* " + ocorrencia.motorista);
                    linha.WriteLine("*RESPONSAVEL:* " + loginUsuario);
                    linha.WriteLine();  //pulando linha sem escrita

                    linha.WriteLine(txtMultiObservacao.Text);

                    linha.WriteLine();  //pulando linha sem escrita
                }

                linha.WriteLine(nomeEmpresa);
                //Fecha o arquivo
                linha.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("O correu um erro ao gerar o arquivo! \n" + ex);
            }
        }

        private void CorGrid()
        {
            try
            {
                //Verifica o retorno da pesquisa
                if (gridOcorrencia.Rows.Count > 0)
                {
                    for (int i = 0; gridOcorrencia.Rows.Count > i; i++)
                    {
                        //Altera o status
                        if (Convert.ToString(gridOcorrencia.Rows[i].Cells[5].Value).Equals(string.Empty) || Convert.ToBoolean(gridOcorrencia.Rows[i].Cells[29].Value) == true)
                        {
                            gridOcorrencia.Rows[i].Cells[5].Value = "PENDENTE";
                        }

                        if (Convert.ToBoolean(gridOcorrencia.Rows[i].Cells[30].Value) == true)
                        {
                            gridOcorrencia.Rows[i].Cells[5].Value = "DEVOLUCAO";
                        }

                        if (Convert.ToBoolean(gridOcorrencia.Rows[i].Cells[31].Value) == true)
                        {
                            gridOcorrencia.Rows[i].Cells[5].Value = "REENTREGA";
                        }

                        if (!Convert.ToString(gridOcorrencia.Rows[i].Cells[5].Value).Equals(string.Empty))
                        {
                            if (gridOcorrencia.Rows[i].Cells[5].Value.Equals("PENDENTE"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            }

                            if (gridOcorrencia.Rows[i].Cells[5].Value.Equals("DEVOLUCAO"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.DimGray;
                            }

                            if (gridOcorrencia.Rows[i].Cells[5].Value.Equals("REENTREGA"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.SteelBlue;
                            }

                            if (gridOcorrencia.Rows[i].Cells[5].Value.Equals("CANCELADA"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }

                            if (gridOcorrencia.Rows[i].Cells[5].Value.Equals("FINALIZADA"))
                            {
                                gridOcorrencia.Rows[i].DefaultCellStyle.BackColor = Color.SeaGreen;
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

        //Limpa e habilita os campos
        private void LimparCampos()
        {
            if (tabControl1.SelectedTab == tbpOcorrencia)
            {
                lblCodigo.Text = "-";
                txtNotaFiscal.Text = string.Empty; //Limpa o campo nota fiscal
                txtPedido.Text = string.Empty; //Limpa o campo pedido
                lblDataFaturamento.Text = "-";  //Limpa o campo Data da nota fiscal
                lblPedido.Text = "-";  //Limpa o campo Número da nota Pedido
                lblNotaFiscal.Text = "-";  //Limpa o campo Número da nota Fiscal
                lblTipoPedido.Text = "-";  //Limpa o campo Tipo de pedido
                lblTotaPedido.Text = "-";  //Limpa o campo Valor total
                lblPagamento.Text = "-";  //Limpa o campo tipo Pagamento 
                lblManifesto.Text = "-"; //Limpa o campo Manifesto
                lblVeiculo.Text = "-";  //Limpa o campo Placa
                lblNomeMotorista.Text = "-";  //Limpa o campo Motorista
                txtManifesto.Text = string.Empty;  //Limpa o campo manifesto do pedido
                lblObservacao.Text = string.Empty;  //Limpa o campo Observação do pedido
                lblNomeCliente.Text = "-";  //Limpa o campo Nome do cliente 
                lblFantasia.Text = "-";  //Limpa o campo Nome fantasia do cliente
                gridItens.Rows.Clear(); //Limpa o grid de itens
                dtmProgramacao.Value = DateTime.Now; //Seta a data Padrão

                cmbTipo.Text = "SELECIONE"; //Seta o texto no campo tipo
                txtArea.Text = "SELECIONE"; //Seta o texto no campo area
                txtManifesto.Text = string.Empty; //Limpa o campo manifesto
                txtCodMotorista.Text = string.Empty; //Limpa o campo motorista
                lblMotorista.Text = "-"; //Limpa o campo nome do motorista
                chkPendente.Checked = false; //Desmarca o campo gerar reentrega
                chkDevolucao.Checked = false; //Desmarca o campo gerar reentrega
                chkCancelar.Checked = false; //Desmarca o campo cancelar
                chkGerarReentrega.Checked = false; //Desmarca o campo gerar reentrega
                txtObservacao.Text = string.Empty; //Limpa o campo observação

                lblUF.Text = string.Empty;  //Limpa o campo UF do cliente
                lblCidade.Text = string.Empty;  //Limpa o campo cidade do cliente
                lblBairro.Text = string.Empty;  //Limpa o campo Bairro do cliente
                lblEndereco.Text = string.Empty;  //Limpa o campo Endereço do cliente
                lblRepresentante.Text = string.Empty;  //Limpa o campo Nome do representante
                lblSupervisor.Text = string.Empty;  //Limpa o campo Nome do representante
                emailCliente = string.Empty;  //Limpa o campo Variável global - Email do cliente
            }
            else if (tabControl1.SelectedTab == tbpReentrega)
            {
                txtMultiManifesto.Text = string.Empty; //Limpa o campo de manifesto
                cmbMultiTipo.Text = "SELECIONE"; //Seta o texto no campo tipo
                cmbMultiArea.Text = "SELECIONE"; //Seta o texto no campo area
                txtMultiMotorista.Text = string.Empty; // Limpa o campo de motorista
                dtmMultiProgramacao.Value = DateTime.Now; //Seta a data Padrão
                txtMultiObservacao.Text = string.Empty; // Limpa o campo de observação
                gridMultiOcorrencia.Rows.Clear(); //Limpa o grid
            }
        }

        //Habilitas os campos
        private void Habilita()
        {
            if (tabControl1.SelectedTab == tbpOcorrencia)
            {
                txtNotaFiscal.Enabled = true; //Habilita o campo nota fiscal
                txtPedido.Enabled = true; //Habilita o campo pedido
                cmbTipo.Enabled = true; //Habilita o campo tipo
                txtCodMotorista.Enabled = true; //Habilita o campo motorista
                chkPendente.Enabled = true; //Habilita o campo pendente
                chkClienteAguardo.Enabled = true; //Habilita o campo cliente aguardo
                chkDevolucao.Enabled = true; //Habilita o campo devolução
                chkGerarReentrega.Enabled = true; //Habilita o campo gerar reentrega
                chkCancelar.Enabled = true; //Habilita o campo cancelar
                txtManifesto.Enabled = true;  //Habilita o campo manifesto do pedido
                txtObservacao.Enabled = true; //Habilita o campo observação
                dtmProgramacao.Enabled = true; //Habilita a data Padrão
                gridItens.Enabled = true; //Habilita o grid de itens
            }
            else if (tabControl1.SelectedTab == tbpReentrega)
            {
                txtMultiManifesto.Enabled = true; //Habilita o campo de manifesto
                cmbMultiTipo.Enabled = true; //Habilita o campo tipo
                cmbMultiArea.Enabled = true; //Habilita o campo area
                txtMultiMotorista.Enabled = true; // Habilita o campo de motorista
                dtmMultiProgramacao.Enabled = true; //Habilita a data Padrão
                txtMultiObservacao.Enabled = true; // Habilita o campo de observação
                gridMultiOcorrencia.Enabled = true; //Habilita o grid

                chkMultiPendente.Enabled = true; //Habilita o campo pendente
                chkMultiDevolucao.Enabled = true; //Habilita o campo devolução
                chkMultiReentrega.Enabled = true; //Habilita o campo gerar reentrega
            }
        }

        //Desabilitas os campos
        private void Desabilita()
        {
            txtNotaFiscal.Enabled = false; //Desabilita o campo nota fiscal
            txtPedido.Enabled = false; //Desabilita o campo pedido
            cmbTipo.Enabled = false; //Desabilita o campo tipo
            txtArea.Enabled = false; //Desabilita o campo area
            txtCodMotorista.Enabled = false; //Desabilita o campo motorista
            chkPendente.Enabled = false; //Desabilita o campo pendente
            chkClienteAguardo.Enabled = false; //Desabilita o campo cliente aguardo
            chkDevolucao.Enabled = false; //Desabilita o campo devolução
            chkGerarReentrega.Enabled = false; //Desabilita o campo gerar reentrega
            chkCancelar.Enabled = false; //Desabilita o campo cancelar
            txtManifesto.Enabled = false;  //Desabilita o campo manifesto do pedido
            txtObservacao.Enabled = false; //Desabilita o campo observação
            dtmProgramacao.Enabled = false; //Desabilita a data Padrão
            gridItens.Enabled = false; //Desabilita o grid de itens

            txtMultiManifesto.Enabled = false; //Desabilita o campo de manifesto
            cmbMultiTipo.Enabled = false; //Desabilita o campo tipo
            cmbMultiArea.Enabled = false; //Desabilita o campo area
            txtMultiMotorista.Enabled = false;  // Desabilita o campo de motorista
            dtmMultiProgramacao.Enabled = false; //Desabilita a data Padrão
            txtMultiObservacao.Enabled = false; // Desabilita o campo de observação
            gridMultiOcorrencia.Enabled = false; //Desabilita o grid

            chkMultiPendente.Enabled = false; //Desabilita o campo pendente
            chkMultiDevolucao.Enabled = false; //Desabilita o campo devolução
            chkMultiReentrega.Enabled = false; //Desabilita o campo gerar reentrega

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //Verifica o retorno da pesquisa
            if (gridOcorrencia.Rows.Count > 0)
            {
                string mensagem = "";

                for (int i = 0; gridOcorrencia.Rows.Count > i; i++)
                {
                    //Verificando o status
                    if (Convert.ToString(gridOcorrencia.Rows[i].Cells[5].Value).Equals(string.Empty) || Convert.ToString(gridOcorrencia.Rows[i].Cells[5].Value).Equals("PENDENTE"))
                    {
                        TimeSpan tempo = new TimeSpan(7, 36, 10);
                        tempo = DateTime.Now.Subtract(Convert.ToDateTime(gridOcorrencia.Rows[i].Cells[1].Value)); //Verificando a data da ocorrência

                        gridOcorrencia.Rows[i].Cells[34].Value = tempo.ToString(@"dd\.hh\:mm\:ss");


                        //Verifica o tempo limite e se o controle da mensagem está vazio
                        if (tempo > TimeSpan.Parse(tempoAlerta) && gridOcorrencia.Rows[i].Cells[35].Value == null || tempo > TimeSpan.Parse(tempoAlerta) && Convert.ToString(gridOcorrencia.Rows[i].Cells[35].Value).Equals(string.Empty))
                        {
                            //Recebe os pedidos
                            mensagem = mensagem + "\nPedido: " + gridOcorrencia.Rows[i].Cells[4].Value + " Tempo: " + gridOcorrencia.Rows[i].Cells[34].Value + " Supervisor: " + gridOcorrencia.Rows[i].Cells[22].Value + "\n";

                            //Seta false para para a mensagem
                            gridOcorrencia.Rows[i].Cells[35].Value = false;

                        }

                        //Verifica o tempo limite e se o controle da mensagem está vazio
                        if (tempo > TimeSpan.Parse(tempoExcedido) && gridOcorrencia.Rows[i].Cells[36].Value == null)
                        {
                            //Recebe os pedidos
                            mensagem = mensagem + "\nPedido: " + gridOcorrencia.Rows[i].Cells[4].Value + " Tempo: " + gridOcorrencia.Rows[i].Cells[34].Value + " Supervisor: " + gridOcorrencia.Rows[i].Cells[22].Value + "\n";

                            //Seta false para para a mensagem
                            gridOcorrencia.Rows[i].Cells[36].Value = false;
                        }
                    }
                }

                //MessageBox.Show("" + mensagem + "   " + mensagem.Length);

                if (mensagem.Length > 0)
                {
                    MessageBox.Show("Existem ocorrências pendentes!" + mensagem, "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


    }

}
