using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;
using Utilitarios;
using Wms.Relatorio;

namespace Wms
{
    public partial class FrmEnderecamentoPicking : Form
    {
        //Array com id
        private int[] regiao;
        private int[] regiaoDe;
        private int[] regiaoPara;
        private int[] rua;
        private int[] ruaDe;
        private int[] ruaPara;
        private int[] bloco;
        private int[] nivel;

        public int codUsuario;

        //Recebe o id do apartamento atual do produto pesquisado
        private int codApartamento;
        private int idProduto; //Recebe o id do produto

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmEnderecamentoPicking()
        {
            InitializeComponent();
        }

        private void FrmEnderecamentoPicking_Load(object sender, EventArgs e)
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
                //cmbEmpresa.Enabled = false;
            }
        }

        //KeyPress
        private void cmbRegiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbRua.Focus();
            }
        }

        private void cmbRua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbBloco.Focus();
            }
        }

        private void cmbBloco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbNivel.Focus();
            }
        }

        private void cmbNivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbStatus.Focus();
            }
        }

        private void cmbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDisponibilidade.Focus();
            }
        }

        private void cmbDisponibilidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus();
            }
        }

        private void txtPesqCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtPesqCodigo.Text.Equals(""))
                {
                    //Pesquisa o produto, ele estando endereçado não
                    PesqProduto();
                }
            }
        }

        private void txtCapacidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo abastecimento
                txtAbastecimento.Focus();
            }
        }

        private void txtAbastecimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo novo endereço
                txtNovoEndereco.Focus();
            }
        }

        //Changed
        private void chkVencimento_CheckedChanged(object sender, EventArgs e)
        {
            //Limpa os campos
            LimparCampos();
        }

        private void txtPesqCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtPesqCodigo.Text.Length == 0)
            {
                LimparCampos();
            }
        }

        private void txtNovoEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão endereçar produto
                btnEnderecar.Focus();
            }
        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //Pesquisa a rua
            PesqRua(cmbRua, cmbRegiao, regiao);
        }

        private void cmbRegiaoDe_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua(cmbRuaDe, cmbRegiaoDe, regiaoDe);
        }

        private void cmbRegiaoPara_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua(cmbRuaPara, cmbRegiaoPara, regiaoPara);
        }

        private void cmbRua_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa o bloco
            PesqBloco(cmbBloco, cmbRua, rua);
        }

        private void cmbRuaDe_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa o bloco
            PesqBloco(cmbBlocoDe, cmbRuaDe, ruaDe);
        }

        private void cmbRuaPara_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa o bloco
            PesqBloco(cmbBlocoPara, cmbRuaPara, ruaPara);
        }

        private void cmbBloco_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa o bloco
            PesqNivel(bloco);
            cmbStatus.Text = "Selecione";
            cmbDisponibilidade.Text = "Selecione";
        }

        //KeyDown
        private void txtPesqCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqProduto frame = new FrmPesqProduto();
                //Adiciona o nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtPesqCodigo.Text = Convert.ToString(frame.codProduto);
                    lblDescProduto.Text = frame.descProduto;
                    PesqProduto();
                }
            }
        }

        //DoubleClick
        private void gridEndereco_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                txtNovoEndereco.Clear();
            }
            if (e.Clicks == 2)
            {
                if (gridEndereco.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEndereco.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtNovoEndereco.Text = gridEndereco.Rows[indice].Cells[1].Value.ToString();
                }
            }
        }

        //Click
        private void cmbRegiao_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRegiao.Items.Count == 0)
            {
                //PesqRegiao(cmbRegiao);
                //PesqRegiaoCmb(cmbRegiao);
            }
        }

        private void cmbRegiaoDe_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRegiaoDe.Items.Count == 0)
            {
                //PesqRegiao(cmbRegiaoDe);
            }
        }

        private void cmbRegiaoPara_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRegiaoPara.Items.Count == 0)
            {
                //PesqRegiao(cmbRegiaoPara);
            }
        }

        private void gridEndereco_MouseClick(object sender, MouseEventArgs e)
        {
            //Limpa o campo endereco
            txtNovoEndereco.Clear();
        }

        //Exclusão de produto no endereço
        private void mniExcluir_Click(object sender, EventArgs e)
        {

            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Exclui o produto 
                ExcuirProduto();
            }
            else if (acesso[0].excluirFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Exclui o produto 
                ExcuirProduto();
            }

        }

        private void mniEtiqueta_Click(object sender, EventArgs e)
        {
            //Imprimi a etiqueta
            ImprimirEtiqueta();
        }

        private void mniEtiquetaFlowRack_Click(object sender, EventArgs e)
        {

            //Imprime a etiqueta
            ImpressaoEtiquetaEndereco();
        }

        private void mniImprimirEtiquetaColuna_Click(object sender, EventArgs e)
        {
            //Imprime a etiqueta
            ImpressaoEtiquetaColuna();

        }

        private void mniRelatorioEnderecos_Click(object sender, EventArgs e)
        {
            FrmEndereco frame = new FrmEndereco();

            frame.GerarRelatorio(cmbEmpresa.Text, cmbEndereco.Text, cmbRegiao.Text, cmbRua.Text, cmbBloco.Text, cmbNivel.Text, cmbStatus.Text, cmbDisponibilidade.Text, cmbLado.Text);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o endereço
            PesqPorEndereco();
        }

        private void btnEnderecar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Endereça o produto 
                EnderecarProduto();
            }
            else if (acesso[0].escreverFuncao == false && acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Endereça o produto 
                EnderecarProduto();
            }
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                Thread analisa = new Thread(DePara);
                analisa.Start();
            }
            else if (acesso[0].escreverFuncao == false && acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                Thread analisa = new Thread(DePara);
                analisa.Start();
            }

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Pesquisa região
        /*
        private void PesqRegiao(ComboBox cmbRegiao)
        {
            try
            {
                cmbRegiao.Text = "Selecione";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Limpa o combobox regiao
                cmbRegiao.Items.Clear();
                //Preenche a coleção com apesquisa
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao(cmbEmpresa.Text);
                if(gerarEnderecoCollection.Any())
                //Preenche o combobox região
                    //gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(new ListItem($"{n.numeroRegiao}", n.codRegiao.ToString())));
                    gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(n.numeroRegiao));

                if (cmbRegiao.Name == "cmbRegiao")
                {
                    //Exibe o texto
                    cmbBloco.Text = "Selecione";
                    cmbStatus.Text = "Selecione";
                    cmbDisponibilidade.Text = "Selecione";

                    //Define o tamanho do array para o combobox
                    regiao = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        regiao[i] = gerarEnderecoCollection[i].codRegiao;
                    }

                }

                if (cmbRegiao.Name == "cmbRegiaoDe")
                {
                    //Exibe o texto
                    cmbRuaDe.Text = "Selecione";
                    cmbBlocoDe.Text = "Selecione";

                    //Define o tamanho do array para o combobox
                    regiaoDe = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        regiaoDe[i] = gerarEnderecoCollection[i].codRegiao;
                    }

                }

                if (cmbRegiao.Name == "cmbRegiaoPara")
                {
                    //Exibe o texto
                    cmbRuaPara.Text = "Selecione";
                    cmbBlocoPara.Text = "Selecione";

                    //Define o tamanho do array para o combobox
                    regiaoPara = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        regiaoPara[i] = gerarEnderecoCollection[i].codRegiao;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        */

        private void PesqRegiaoCmb(string empresa)
        {
            try
            {
                cmbRegiao.Text = "Selecione";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Limpa o combobox regiao
                cmbRegiao.Items.Clear();
                //Preenche a coleção com apesquisa
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao(cmbEmpresa.Text);
                if (gerarEnderecoCollection.Any())
                    //Preenche o combobox região
                    //gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(new ListItem($"{n.numeroRegiao}", n.codRegiao.ToString())));
                    gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(n.numeroRegiao));

                if (cmbRegiao.Name == "cmbRegiao")
                {
                    //Exibe o texto
                    cmbBloco.Text = "Selecione";
                    cmbStatus.Text = "Selecione";
                    cmbDisponibilidade.Text = "Selecione";

                    //Define o tamanho do array para o combobox
                    regiao = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        regiao[i] = gerarEnderecoCollection[i].codRegiao;
                    }

                }

                if (cmbRegiao.Name == "cmbRegiaoDe")
                {
                    //Exibe o texto
                    cmbRuaDe.Text = "Selecione";
                    cmbBlocoDe.Text = "Selecione";

                    //Define o tamanho do array para o combobox
                    regiaoDe = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        regiaoDe[i] = gerarEnderecoCollection[i].codRegiao;
                    }

                }

                if (cmbRegiao.Name == "cmbRegiaoPara")
                {
                    //Exibe o texto
                    cmbRuaPara.Text = "Selecione";
                    cmbBlocoPara.Text = "Selecione";

                    //Define o tamanho do array para o combobox
                    regiaoPara = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        regiaoPara[i] = gerarEnderecoCollection[i].codRegiao;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa rua combobox
        private void PesqRua(ComboBox cmbRua, ComboBox cmbRegiao, int[] idRegiao)
        {
            try
            {
                //Limpa o combobox rua inícial
                cmbRua.Items.Clear();
                //Adiciona o texto
                cmbRua.Text = "Selecione";
                //Limpa o combobox rua
                cmbBloco.Items.Clear();
                //Adiciona o texto
                cmbBloco.Text = "Selecione";
                //Limpa o combobox rua
                cmbNivel.Items.Clear();
                //Adiciona o texto
                cmbNivel.Text = "Selecione";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa as ruas da região selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(idRegiao[cmbRegiao.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbRua.Items.Add(n.numeroRua));
                //Define o tamanho do array


                if (cmbRua.Name == "cmbRua")
                {
                    rua = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array
                        rua[i] = gerarEnderecoCollection[i].codRua;
                    }
                }

                if (cmbRua.Name == "cmbRuaDe")
                {
                    ruaDe = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array
                        ruaDe[i] = gerarEnderecoCollection[i].codRua;
                    }
                }

                if (cmbRua.Name == "cmbRuaPara")
                {
                    ruaPara = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array
                        ruaPara[i] = gerarEnderecoCollection[i].codRua;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o Bloco
        private void PesqBloco(ComboBox cmbBloco, ComboBox cmbRua, int[] idRua)
        {
            try
            {
                //Limpa o combobox rua
                cmbBloco.Items.Clear();
                //Adiciona o texto
                cmbBloco.Text = "Selecione";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa os bloco da rua selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqBloco(idRua[cmbRua.SelectedIndex]);
                //Preenche o combobox bloco
                gerarEnderecoCollection.ForEach(n => cmbBloco.Items.Add(n.numeroBloco));

                bloco = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    bloco[i] = gerarEnderecoCollection[i].codBloco;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o Bloco
        private void PesqNivel(int[] idBloco)
        {
            try
            {
                //Limpa o combobox rua
                cmbNivel.Items.Clear();
                //Adiciona o texto
                cmbNivel.Text = "Selecione";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa os bloco da rua selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqNivel(idBloco[cmbBloco.SelectedIndex]);

                //Preenche o combobox bloco
                gerarEnderecoCollection.ForEach(n => cmbNivel.Items.Add(n.numeroNivel));

                nivel = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    nivel[i] = gerarEnderecoCollection[i].codNivel;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa por endereço (GridView)
        private void PesqPorEndereco()
        {
            try
            {
                gridEndereco.Rows.Clear();
                if (cmbRegiao.Text.Equals("Selecione") || cmbRegiao.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, selecione uma região! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cmbRua.Text.Equals("Selecione") || cmbRua.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, selecione uma rua! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string numeroBloco, numeroNivel;

                if (cmbBloco.Text.Equals("Selecione"))
                {
                    numeroBloco = "";
                }
                else
                {
                    numeroBloco = Convert.ToString(cmbBloco.Text);
                }

                if (cmbNivel.Text.Equals("Selecione"))
                {
                    numeroNivel = "";
                }
                else
                {
                    numeroNivel = Convert.ToString(cmbNivel.Text);
                }

                int numeroRegiao = Convert.ToInt32(cmbRegiao.Text);
                int numeroRua = Convert.ToInt32(cmbRua.Text);
                string lado = cmbLado.Text;
                string status = cmbStatus.Text;
                string disponibilidade = cmbDisponibilidade.Text;
                string empresa = cmbEmpresa.Text;

                //Instância a camada de negocios
                EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();
                //Instância a camada de objêto
                EnderecoPickingCollection enderecoCollection = new EnderecoPickingCollection();

                if (cmbEndereco.Text.Equals("PICKING"))
                {
                    //Pesquisa e passa os dados encontrados
                    enderecoCollection = enderecoNegocios.PesqEnderecoPicking(cmbEmpresa.Text, numeroRegiao, numeroRua, numeroBloco, numeroNivel, status, disponibilidade, lado);
                }

                else if (cmbEndereco.Text.Equals("PULMAO"))
                {
                    //Pesquisa e passa os dados encontrados
                    enderecoCollection = enderecoNegocios.PesqEnderecoPulmao(cmbEmpresa.Text, numeroRegiao, numeroRua, numeroBloco, numeroNivel, status, disponibilidade, lado);
                }
                else if (cmbEndereco.Text.Equals("VOLUME"))
                {
                    //Pesquisa e passa os dados encontrados
                    enderecoCollection = enderecoNegocios.PesqEnderecoVolume(numeroRegiao, numeroRua, numeroBloco, numeroNivel, status, disponibilidade, lado);
                }
                else
                {
                    //Instância a camada de objêto
                    EnderecoPickingCollection enderecoPickingCollection = new EnderecoPickingCollection();
                    //Instância a camada de objêto
                    EnderecoPickingCollection enderecoPulmaoCollection = new EnderecoPickingCollection();
                    //Instância a camada de objêto
                    EnderecoPickingCollection enderecoOrdemCollection = new EnderecoPickingCollection();

                    //Pesquisa e passa os dados encontrados
                    enderecoPickingCollection = enderecoNegocios.PesqEnderecoPicking(cmbEmpresa.Text, numeroRegiao, numeroRua, numeroBloco, numeroNivel, status, disponibilidade, lado);
                   
                    //Pesquisa e passa os dados encontrados
                    enderecoPulmaoCollection = enderecoNegocios.PesqEnderecoPulmao(cmbEmpresa.Text, numeroRegiao, numeroRua, numeroBloco, numeroNivel, status, disponibilidade, lado);
 
                    //Junta os endereços de picking e pulmao
                    enderecoPickingCollection.AddRange(enderecoPulmaoCollection);
                    //Passa para o objêto de ordenação
                    enderecoOrdemCollection.AddRange(enderecoPickingCollection);
                    //Ordena os endereco
                    List<EnderecoPicking> enderecoOrdenado = enderecoOrdemCollection.OrderBy(x => x.ordemEndereco).ToList();
                    //Passa para o objêto final
                    //enderecoCollection.AddRange(enderecoOrdenado);
                    enderecoCollection.AddRange(enderecoOrdemCollection);

                }


                //Limpa o grid
                gridEndereco.Rows.Clear();

                if (enderecoCollection.Count > 0)
                {

                    //Preenche o grid
                    enderecoCollection.ForEach(n => gridEndereco.Rows.Add(n.codApartamento, n.endereco, n.idProduto, n.codProduto, n.codProduto + " " + n.descProduto,
                        string.Format("{0:g}", n.estoque), n.unidadeEstoque, string.Format("{0:d}", n.vencimento), string.Format(@"{0:0.000}", n.peso), n.lote, n.capacidade,
                        n.abastecimento, n.paleteEndereco, n.apStatus, n.apDisponibilidade,
                        n.numeroRegiao, n.numeroRua, n.numeroBloco, n.numeroNivel, n.numeroApartamento, n.nomeEmpresa, n.enderecoExtra, n.tipoEndereco));

                    //Exibe a quantidade de endereços encontrados
                    lblQtdEndereco.Text = enderecoCollection.Count.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }


        //Pesquisa o produto endereçado ou para ser endereçado (TextBox)
        private void PesqProduto()
        {
            try
            {
                //Instância a camada de negocios
                EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();
                //Instância a coleção
                EnderecoPicking enderecoProduto = new EnderecoPicking();

                if (rbtVencimentoI.Checked == true)
                {
                    //O objeto recebe o resultado da consulta
                    enderecoProduto = enderecoNegocios.PesqProduto(txtPesqCodigo.Text, "VENCIMENTO I", cmbEmpresa.Text);
                }
                else if (rbtVencimentoII.Checked == true)
                {
                    //O objeto recebe o resultado da consulta
                    enderecoProduto = enderecoNegocios.PesqProduto(txtPesqCodigo.Text, "VENCIMENTO II", cmbEmpresa.Text);
                }
                else if (rbtVencimentoIII.Checked == true)
                {
                    //O objeto recebe o resultado da consulta
                    enderecoProduto = enderecoNegocios.PesqProduto(txtPesqCodigo.Text, "VENCIMENTO III", cmbEmpresa.Text);
                }
                else
                {
                    //O objeto recebe o resultado da consulta
                    enderecoProduto = enderecoNegocios.PesqProduto(txtPesqCodigo.Text, "CAIXA", cmbEmpresa.Text);
                }

                //Limpa os campos
                LimparCampos();

                if (enderecoProduto.idProduto > 0)
                {
                    if (enderecoProduto.statusProduto == true)
                    {
                        this.codApartamento = enderecoProduto.codApartamento;

                        idProduto = enderecoProduto.idProduto;
                        txtPesqCodigo.Text = enderecoProduto.codProduto;
                        lblDescProduto.Text = enderecoProduto.descProduto;
                        lblQuantidade.Text = Convert.ToString(enderecoProduto.estoque);
                        lblUnidade.Text = Convert.ToString(enderecoProduto.estoque) + " " + enderecoProduto.unidadeEstoque;
                        lblValidade.Text = string.Format("{0:dd/MM/yyyy}", enderecoProduto.vencimento);
                        lblEndereco.Text = enderecoProduto.endereco;
                        lblLote.Text = enderecoProduto.lote;
                        txtCapacidade.Text = Convert.ToString(enderecoProduto.capacidade);
                        txtAbastecimento.Text = Convert.ToString(enderecoProduto.abastecimento);
                        //Foca no campo 
                        txtCapacidade.Focus();
                    }
                    else
                    {
                        MessageBox.Show("O SKU " + enderecoProduto.codProduto + " - " + enderecoProduto.descProduto + " se encontra inativo! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum SKU encontrado! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Executa o Depara
        private void DePara()
        {
            try
            {
                Invoke((MethodInvoker)delegate ()
                {
                    if (cmbRegiaoDe.Text.Equals("Selecione") || cmbRegiaoDe.Text.Equals(""))
                    {
                        MessageBox.Show("Por favor, selecione uma região dos produtos a serem transferidos! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (cmbRuaDe.Text.Equals("Selecione") || cmbRuaDe.Text.Equals(""))
                    {
                        MessageBox.Show("Por favor, selecione uma rua dos produtos a serem transferidos! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (cmbRegiaoPara.Text.Equals("Selecione") || cmbRegiaoPara.Text.Equals(""))
                    {
                        MessageBox.Show("Por favor, selecione uma região dos produtos para receber às transferências! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (cmbRuaPara.Text.Equals("Selecione") || cmbRuaPara.Text.Equals(""))
                    {
                        MessageBox.Show("Por favor, selecione uma rua dos produtos para receber às transferências! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    int? numeroBlocoDe;
                    int? numeroBlocoPara;

                    if (cmbBlocoDe.Text.Equals("Selecione") || cmbBlocoDe.Text.Equals(""))
                    {
                        numeroBlocoDe = null;
                    }
                    else
                    {
                        numeroBlocoDe = Convert.ToInt32(cmbBlocoDe.Text);
                    }

                    if (cmbBlocoPara.Text.Equals("Selecione") || cmbBlocoPara.Text.Equals(""))
                    {
                        numeroBlocoPara = null;
                    }
                    else
                    {
                        numeroBlocoPara = Convert.ToInt32(cmbBlocoPara.Text);
                    }

                    int numeroRegiaoDe = Convert.ToInt32(cmbRegiaoDe.Text);
                    int numeroRuaDe = Convert.ToInt32(cmbRuaDe.Text);
                    int numeroRegiaoPara = Convert.ToInt32(cmbRegiaoPara.Text);
                    int numeroRuaPara = Convert.ToInt32(cmbRuaPara.Text);

                    //Instância a camada de negocios
                    EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();
                    //Instância a camada de objêto De
                    EnderecoPickingCollection enderecoDe = new EnderecoPickingCollection();
                    //Instância a camada de objêto Para
                    EnderecoPickingCollection enderecoPara = new EnderecoPickingCollection();

                    //Exibe a mensagem
                    MsgProcesso("Pesquisando endereços");
                    DesabilitarDepara();

                    //Pesquisa e passa os dados encontrados
                    enderecoDe = enderecoNegocios.PesqQuantidadeBlocoDePara(numeroRegiaoDe, numeroRuaDe, 0, 0);
                    //Pesquisa e passa os dados encontrados
                    enderecoPara = enderecoNegocios.PesqQuantidadeBlocoDePara(numeroRegiaoPara, numeroRuaPara, 0, 0);

                    if (enderecoDe[0].numeroBloco != enderecoPara[0].numeroBloco)
                    {
                        MessageBox.Show("Não é possível executar o depara! A quantidade de blocos precisam ser iguais. " + enderecoDe[0].numeroBloco + " -  " + enderecoPara[0].numeroBloco, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        //Limpa as listas
                        enderecoDe.Clear();
                        enderecoPara.Clear();
                        //Pesquisa a quantidade de apartamento em cada bloco
                        enderecoDe = enderecoNegocios.PesqQuantidadeApartamentoDePara(numeroRegiaoDe, numeroRuaDe, numeroBlocoDe, 0);
                        //Pesquisa a quantidade de apartamento em cada bloco
                        enderecoPara = enderecoNegocios.PesqQuantidadeApartamentoDePara(numeroRegiaoPara, numeroRuaPara, numeroBlocoPara, 0);

                        //Variável responsável por controlar a transferência
                        bool tranfere = true;

                        //Verifica se os blocos são idênticos na quantidade de apartamentos
                        for (int i = 0; enderecoDe.Count > i; i++)
                        {
                            if (enderecoDe[i].numeroBloco == enderecoPara[i].numeroBloco)
                            {
                                if (enderecoDe[i].numeroApartamento != enderecoPara[i].numeroApartamento)
                                {
                                    MessageBox.Show("Não é possível executar o depara! O bloco " + enderecoDe[i].numeroBloco + " da região " + cmbRegiaoDe.Text + " rua " + cmbRuaDe.Text +
                                    " possui mais apartamentos que o bloco " + enderecoPara[i].numeroBloco + " da região " + cmbRegiaoPara.Text + " rua " + cmbRuaPara.Text + " a ser tranferido.", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tranfere = false;

                                    break;
                                }
                            }
                        }

                        if (tranfere == true)
                        {
                            //Exibe a mensagem
                            MsgProcesso("Pesquisando produtos");
                            //Limpa as listas
                            enderecoDe.Clear();
                            enderecoPara.Clear();
                            //Pesquisa os produtos a serem transferidos
                            enderecoDe = enderecoNegocios.PesqProdutoDePara(numeroRegiaoDe, numeroRuaDe, 0, 0);
                            //Pesquisa os produtos a serem transferidos
                            enderecoPara = enderecoNegocios.PesqProdutoDePara(numeroRegiaoPara, numeroRuaPara, 0, 0);

                            //Deleta os endereços de
                            foreach (EnderecoPicking endereco in enderecoDe)
                            {
                                enderecoNegocios.DeletaEnderecoSeparacao(endereco.codApartamento, cmbEmpresa.Text);
                            }

                            //Deleta os endereços Para
                            foreach (EnderecoPicking endereco in enderecoPara)
                            {
                                enderecoNegocios.DeletaEnderecoSeparacao(endereco.codApartamento, cmbEmpresa.Text);
                            }

                            //Variável responsável pelo código do apartamento
                            int codApartamento = 0;

                            //Exibe a mensagem
                            MsgProcesso("Iniciando tranferências");

                            //Endereça o produto no Para
                            for (int i = 0; enderecoDe.Count > i; i++)
                            {
                                //Pesquisa o apartamento 
                                codApartamento = enderecoNegocios.PesqApartamentoDePara(numeroRegiaoPara, numeroRuaPara, enderecoDe[i].numeroBloco, 0, enderecoDe[i].numeroApartamento);
                                //Endereça o produto
                                enderecoNegocios.EnderecarProdutoSeparacao(null, codApartamento, enderecoDe[i].codProduto, enderecoDe[i].estoque, enderecoDe[i].vencimento, enderecoDe[i].capacidade, enderecoDe[i].abastecimento, enderecoDe[i].lote, "CAIXA", codUsuario, cmbEmpresa.Text);

                                //Exibe a mensagem
                                MsgProcesso("Trânsferindo : " + enderecoDe[i].codProduto + " - " + enderecoDe[i].descProduto);
                            }

                            //Endereça o produto no Para
                            for (int i = 0; enderecoPara.Count > i; i++)
                            {
                                //Pesquisa o apartamento 
                                codApartamento = enderecoNegocios.PesqApartamentoDePara(numeroRegiaoDe, numeroRuaDe, enderecoPara[i].numeroBloco, 0, enderecoPara[i].numeroApartamento);
                                //Endereça o produto
                                enderecoNegocios.EnderecarProdutoSeparacao(null, codApartamento, enderecoPara[i].codProduto, enderecoPara[i].estoque, enderecoPara[i].vencimento, enderecoPara[i].capacidade, enderecoPara[i].abastecimento, enderecoPara[i].lote, "CAIXA", codUsuario, cmbEmpresa.Text);

                                //Exibe a mensagem
                                MsgProcesso("Trânsferindo : " + enderecoDe[i].codProduto + " - " + enderecoDe[i].descProduto);
                            }

                            MessageBox.Show("Transferência realizada com sucesso! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            HabilitarDepara();
                        }
                    }

                });
            }
            catch (Exception ex)
            {
                HabilitarDepara();
                MessageBox.Show("" + ex);
            }
        }

        //Endereça o produto
        private void EnderecarProduto()
        {
            try
            {
                if (txtNovoEndereco.Text.Equals(lblEndereco.Text))
                {
                    MessageBox.Show("O endereço digitado não pode ser o mesmo endereço atual! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo novo endereço
                    txtNovoEndereco.Focus();
                }
                else if (txtPesqCodigo.Text.Equals("") || txtCapacidade.Text.Equals("") || txtAbastecimento.Text.Equals("") || txtPesqCodigo.Text.Equals("0") || txtCapacidade.Text.Equals("0") || txtAbastecimento.Text.Equals("0"))
                {
                    MessageBox.Show("Por favor, preencha às informações do Produto! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo código do produto
                    txtPesqCodigo.Focus();
                }
                else if (txtNovoEndereco.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, digite ou selecione um endereço! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo novo endereço
                    txtNovoEndereco.Focus();
                }
                else if (Convert.ToInt32(txtCapacidade.Text) < Convert.ToInt32(txtAbastecimento.Text))
                {
                    MessageBox.Show("O campo abastecimento não pode ser maior que o campo capacidade! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo abastecimento
                    txtAbastecimento.Focus();
                }
                else
                {
                    //Instância a camada de negocios
                    EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();
                    //Instância o objêto 
                    EnderecoPicking endereco = new EnderecoPicking();
                    //O objeto recebe o resultado da consulta
                    endereco = enderecoNegocios.PesqEndereco(txtNovoEndereco.Text);

                    if (endereco.codApartamento > 0)
                    {
                        if (endereco.codEstacao > 0)
                        {
                            MessageBox.Show("O endereço digitado pertece a separação de flow rack! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //Foca no campo novo endereço
                            txtNovoEndereco.Focus();
                        }
                        else
                        {
                            //Verifica a disponibildade do endereço
                            if (!endereco.apDisponibilidade.Equals("Sim"))
                            {
                                if (MessageBox.Show("O endereço digitado encontra-se indisponível, deseja disponibilizá-lo?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //Atualiza a disponibilidade do endereço
                                    enderecoNegocios.AtualizarDisponibilidadeEndereco(endereco.codApartamento, "Sim", cmbEmpresa.Text);

                                    MessageBox.Show("O endereço foi disponibilizado com sucesso! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    //Chama novamente o método
                                    EnderecarProduto();
                                }
                            }//Verifica se o endereço é da área de separação
                            else if (!endereco.tipoEndereco.Equals("Separacao"))
                            {
                                MessageBox.Show("Por favor, digite um endereço de separação! " + endereco.tipoEndereco, "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            //Endereça se vago
                            else if (endereco.apStatus.Equals("Vago"))
                            {

                                //Se o produto já estiver endereçado
                                if (lblEndereco.Text != string.Empty)
                                {
                                    //Deleta o endereço atual do produto 
                                    enderecoNegocios.DeletaEnderecoSeparacao(this.codApartamento, cmbEmpresa.Text);

                                    //Registra a exclusão no rastreamento
                                    enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "EXCLUSÃO", codUsuario,
                                    idProduto, //Código do produto
                                    null, null, null, null, null,
                                    this.codApartamento, //Código do endereço
                                    Convert.ToInt32(endereco.estoque), //Quantidade do produto
                                    Convert.ToString(lblValidade.Text), //Vencimento do produto
                                    Convert.ToDouble(endereco.peso), //peso do produto
                                    Convert.ToString(lblLote.Text), //Lote do produto
                                    endereco.tipoPicking); //Tipo de endereço
                                }

                                //Se a validade estiver vazia
                                if (lblValidade.Text == string.Empty)
                                {
                                    //Data do dia
                                    DateTime dataDia = DateTime.Today;
                                    lblValidade.Text = Convert.ToString(dataDia.ToString("d"));
                                }

                                //Verifica se o picking é de vencimento
                                if (rbtVencimentoI.Checked == true)
                                {
                                    //Endereça o produto para separação de caixa
                                    enderecoNegocios.EnderecarProdutoSeparacao(null, endereco.codApartamento, txtPesqCodigo.Text, 0, Convert.ToDateTime(lblValidade.Text), Convert.ToInt32(txtCapacidade.Text), Convert.ToInt32(txtAbastecimento.Text), lblLote.Text, "VENCIMENTO I", codUsuario, cmbEmpresa.Text);

                                    //Registra a exclusão no rastreamento
                                    enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "INSERÇÃO", codUsuario,
                                    idProduto, //Código do produto
                                    null, null, null, null, null,
                                    Convert.ToInt32(endereco.codApartamento), //Código do endereço
                                    Convert.ToInt32(0), //Quantidade do produto
                                    Convert.ToString(lblValidade.Text), //Vencimento do produto
                                    Convert.ToDouble(0), //peso do produto
                                    Convert.ToString(0), "VENCIMENTO I"); //Lote do produto


                                }
                                //Verifica se o picking é de vencimento
                                else if (rbtVencimentoII.Checked == true)
                                {
                                    //Endereça o produto para separação de caixa
                                    enderecoNegocios.EnderecarProdutoSeparacao(null, endereco.codApartamento, txtPesqCodigo.Text, 0, Convert.ToDateTime(lblValidade.Text), Convert.ToInt32(txtCapacidade.Text), Convert.ToInt32(txtAbastecimento.Text), lblLote.Text, "VENCIMENTO II", codUsuario, cmbEmpresa.Text);

                                    //Registra a exclusão no rastreamento
                                    enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "INSERÇÃO", codUsuario,
                                    idProduto, //Código do produto
                                    null, null, null, null, null,
                                    Convert.ToInt32(endereco.codApartamento), //Código do endereço
                                    Convert.ToInt32(0), //Quantidade do produto
                                    Convert.ToString(lblValidade.Text), //Vencimento do produto
                                    Convert.ToDouble(0), //peso do produto
                                    Convert.ToString(0), "VENCIMENTO II"); //Lote do produto


                                }
                                //Verifica se o picking é de vencimento
                                else if (rbtVencimentoIII.Checked == true)
                                {
                                    //Endereça o produto para separação de caixa
                                    enderecoNegocios.EnderecarProdutoSeparacao(null, endereco.codApartamento, txtPesqCodigo.Text, 0, Convert.ToDateTime(lblValidade.Text), Convert.ToInt32(txtCapacidade.Text), Convert.ToInt32(txtAbastecimento.Text), lblLote.Text, "VENCIMENTO III", codUsuario, cmbEmpresa.Text);

                                    //Registra a exclusão no rastreamento
                                    enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "INSERÇÃO", codUsuario,
                                    idProduto, //Código do produto
                                    null, null, null, null, null,
                                    Convert.ToInt32(endereco.codApartamento), //Código do endereço
                                    Convert.ToInt32(0), //Quantidade do produto
                                    Convert.ToString(lblValidade.Text), //Vencimento do produto
                                    Convert.ToDouble(0), //peso do produto
                                    Convert.ToString(0), "VENCIMENTO III"); //Lote do produto


                                }
                                //Verifica se o picking é de caixa
                                else
                                {
                                    //Endereça o produto para separação de caixa
                                    enderecoNegocios.EnderecarProdutoSeparacao(null, endereco.codApartamento, txtPesqCodigo.Text, 0, Convert.ToDateTime(lblValidade.Text), Convert.ToInt32(txtCapacidade.Text), Convert.ToInt32(txtAbastecimento.Text), lblLote.Text, "CAIXA", codUsuario , cmbEmpresa.Text);

                                    //Registra a exclusão no rastreamento
                                    enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "INSERÇÃO", codUsuario,
                                    idProduto, //Código do produto
                                    null, null, null, null, null,
                                    Convert.ToInt32(endereco.codApartamento), //Código do endereço
                                    Convert.ToInt32(0), //Quantidade do produto
                                    Convert.ToString(lblValidade.Text), //Vencimento do produto
                                    Convert.ToDouble(0), //peso do produto
                                    Convert.ToString(0), "CAIXA"); //Lote do produto

                                }

                                if (cmbBloco.Items.Count > 0)
                                {
                                    //Limpa o grid se a pesquisa por endereço for executada
                                    gridEndereco.Rows.Clear();
                                }

                                //Exibe no grid produto
                                gridEndereco.Rows.Add(endereco.codApartamento, txtNovoEndereco.Text, "", txtPesqCodigo.Text, txtPesqCodigo.Text + " -" + lblDescProduto.Text, lblQtdEndereco.Text, lblUnidade.Text, lblValidade.Text, "0,00", lblLote.Text, txtCapacidade.Text, txtAbastecimento.Text, endereco.paleteEndereco, "Ocupado", "Sim");

                                //Limpa os campos do produto
                                LimparCampos();

                                MessageBox.Show("Produto endereçado com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }//Transfere de um endereço para outro                   
                            else if (endereco.apStatus.Equals("Ocupado"))
                            {
                                if (MessageBox.Show("O endereço digitado encontra-se ocupado com o produto " + endereco.codProduto + "-" + endereco.descProduto + ", deseja transferir assim mesmo? Este produto ficará desendereçado!", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //Deleta o endereço com o produto ocupado
                                    enderecoNegocios.DeletaEnderecoSeparacao(Convert.ToInt32(endereco.codApartamento), cmbEmpresa.Text);

                                    //Registra a exclusão no rastreamento
                                    enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "EXCLUSÃO", codUsuario,
                                    endereco.idProduto, //Código do produto
                                    null, null, null, null, null,
                                    Convert.ToInt32(endereco.codApartamento), //Código do endereço
                                    Convert.ToInt32(endereco.estoque), //Quantidade do produto
                                    Convert.ToString(endereco.vencimento), //Vencimento do produto
                                    Convert.ToDouble(endereco.peso), //peso do produto
                                    Convert.ToString(endereco.lote), //Lote do produto
                                    endereco.tipoPicking); //Tipo de endereço

                                    //Cria o novo endereço, chamando novamente o método
                                    EnderecarProduto();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Endereço não encontrado! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Excluí o produto do endereço
        private void ExcuirProduto()
        {
            try
            {
                if (gridEndereco.Rows.Count > 0)
                {
                    if (MessageBox.Show("Deseja realmente excluir o produto do endereço?", "WMS - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Instância as linha da tabela
                        DataGridViewRow linha = gridEndereco.CurrentRow;
                        //Recebe o indice   
                        int indice = linha.Index;

                        //Instância a camada de negocios
                        EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();

                        if (gridEndereco.Rows[indice].Cells[2].Value.ToString() == "0")
                        {
                            MessageBox.Show("Não nada pra excluir", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            enderecoNegocios.DeletaEnderecoSeparacao(Convert.ToInt32(gridEndereco.Rows[indice].Cells[0].Value), cmbEmpresa.Text);
                            //Registra a exclusão no rastreamento
                            enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "EXCLUSÃO", codUsuario,
                            Convert.ToInt32(gridEndereco.Rows[indice].Cells[2].Value), //Código do produto
                            null, null, null, null, null,
                            Convert.ToInt32(gridEndereco.Rows[indice].Cells[0].Value), //Código do endereço
                            Convert.ToInt32(gridEndereco.Rows[indice].Cells[5].Value), //Quantidade do produto
                            Convert.ToString(gridEndereco.Rows[indice].Cells[7].Value), //Vencimento do produto
                            Convert.ToDouble(gridEndereco.Rows[indice].Cells[8].Value), //peso do produto
                            Convert.ToString(gridEndereco.Rows[indice].Cells[9].Value), //Lote do produto
                            Convert.ToString(gridEndereco.Rows[indice].Cells[22].Value)); //Tipo de picking


                            gridEndereco.Rows[indice].Cells[2].Value = "";
                            gridEndereco.Rows[indice].Cells[3].Value = "";
                            gridEndereco.Rows[indice].Cells[4].Value = "";
                            gridEndereco.Rows[indice].Cells[5].Value = "";
                            gridEndereco.Rows[indice].Cells[6].Value = "";
                            gridEndereco.Rows[indice].Cells[7].Value = "";
                            gridEndereco.Rows[indice].Cells[8].Value = "";
                            gridEndereco.Rows[indice].Cells[9].Value = "";
                            gridEndereco.Rows[indice].Cells[10].Value = "";
                            gridEndereco.Rows[indice].Cells[11].Value = "";
                            gridEndereco.Rows[indice].Cells[22].Value = "";
                            gridEndereco.Rows[indice].Cells[13].Value = "Vago";
                            indice = 0;
                            MessageBox.Show("Exclusão realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Imprimi a etiqueta
        private void ImprimirEtiqueta()
        {
            try
            {
                //Pega o caminho da etiqueta prn
                string etiqueta = AppDomain.CurrentDomain.BaseDirectory + "Separacao_50x80.prn";

                //Caminho do novo arquivo atualizado
                string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + "Separacao_50x80.txt";

                // Abre o arquivo para escrita
                StreamReader streamReader;
                streamReader = File.OpenText(etiqueta);

                string contents = streamReader.ReadToEnd();

                streamReader.Close();

                string conteudo = contents;

                StreamWriter streamWriter = File.CreateText(NovaEtiqueta);

                // Atualizo as variaveis do arquivo
                // streamWriter.WriteLine("<STX>L");
                conteudo = conteudo.Replace("SKU", "014055 - PEDIGREE 20KG  FILHOTE RAÇAS MED. E GRANDE");
                conteudo = conteudo.Replace("ENDERECO", "1.9.3.0.2");
                streamWriter.Write(conteudo);
                //Cancela pausa entre etiquetas


                //streamWriter.WriteLine("j"); //Cancela pausa entre etiquetas

                // streamWriter.WriteLine("m"); //Unidade em milímetros

                //streamWriter.WriteLine("L"); //Entra em modo de formato de etiqueta

                //streamWriter.WriteLine("PC"); //Velocidade de Impressão (C=63,5mm/s)

                //streamWriter.WriteLine("D11"); //Tamanho padrão para Pixel

                //streamWriter.WriteLine("H14"); //Fixa a temperatura para 14



                streamWriter.WriteLine("E"); //Fim do modo de formatação e imprime

                streamWriter.WriteLine("/222"); //Avanço para corte da etiqueta
                streamWriter.Close();

                PrintDialog pd = new PrintDialog();

                pd.PrinterSettings = new PrinterSettings();

                if (pd.PrinterSettings.IsValid)
                {
                    ArgoxPPLA.RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, NovaEtiqueta);

                    //File.Delete(AppDomain.CurrentDomain.BaseDirectory + NovaEtiqueta);
                }
                else
                {
                    MessageBox.Show("Não foi encontrada nenhuma impressora instalada no seu computador!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na geração da etiqueta: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Imprime a etiqueta
        private void ImpressaoEtiquetaEndereco()
        {
            try
            {
                //Pega o caminho da etiqueta prn
                string etiqueta = AppDomain.CurrentDomain.BaseDirectory + "ETIQUETA_ENDERECO_40x100.prn";

                //Caminho do novo arquivo atualizado
                string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + "ETIQUETA_ENDERECO_40x100.txt";

                foreach (DataGridViewRow row in gridEndereco.SelectedRows)
                {
                    // Abre o arquivo para escrita
                    StreamReader streamReader;
                    streamReader = File.OpenText(etiqueta);

                    string contents = streamReader.ReadToEnd();

                    streamReader.Close();

                    string conteudo = contents;

                    StreamWriter streamWriter = File.CreateText(NovaEtiqueta);

                    // Atualiza as variaveis do arquivo
                    //streamWriter.WriteLine("<STX>L");
                    conteudo = conteudo.Replace("P5", "" + row.Cells[0].Value); //Código do Apartamento
                    conteudo = conteudo.Replace("P1", "" + row.Cells[15].Value); //Regiao
                    conteudo = conteudo.Replace("P2", "" + row.Cells[16].Value); //Rua
                    conteudo = conteudo.Replace("P3", "" + row.Cells[17].Value); // BLOCO
                    conteudo = conteudo.Replace("P7", "" + row.Cells[18].Value); // NIVEL
                    conteudo = conteudo.Replace("P4", "" + row.Cells[19].Value); //Apartamento
                    conteudo = conteudo.Replace("EMPRESA", "" + row.Cells[20].Value); //Nome da Empresa
                    conteudo = conteudo.Replace("P6", "" + row.Cells[21].Value); //Apartamento
                    streamWriter.Write(conteudo);

                    streamWriter.WriteLine("E"); //Fim do modo de formatação e imprime

                    streamWriter.WriteLine("/222"); //Avanço para corte da etiqueta


                    streamWriter.Close();

                    PrintDialog pd = new PrintDialog();

                    pd.PrinterSettings = new PrinterSettings();

                    if (pd.PrinterSettings.IsValid)
                    {
                        ArgoxPPLA.RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, NovaEtiqueta);

                        //File.Delete(AppDomain.CurrentDomain.BaseDirectory + NovaEtiqueta);
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrada nenhuma impressora instalada no seu computador!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na geração da etiqueta: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Imprime a etiqueta
        private void ImpressaoEtiquetaColuna()
        {
            try
            {
                if (!cmbEndereco.Text.Equals("PULMAO"))
                {
                    MessageBox.Show("Por favor selecione o tipo de endereço Pulmão!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Criando uma lista de listas de strings
                    List<List<string>> listaUnificada = new List<List<string>>();

                    // Iterando sobre as linhas selecionadas no grid
                    for (int i = 0; i < gridEndereco.SelectedRows.Count; i += 2)
                    {
                        DataGridViewRow rowPar = gridEndereco.SelectedRows[i];
                        DataGridViewRow rowImpar = null;

                        // Verificando se há uma próxima linha ímpar
                        if (i + 1 < gridEndereco.SelectedRows.Count)
                        {
                            rowImpar = gridEndereco.SelectedRows[i + 1];
                        }

                        // Verificando se as duas linhas são válidas
                        if (rowPar != null && rowImpar != null)
                        {
                            // Criando uma nova lista de strings para armazenar os valores da linha
                            List<string> conteudo = new List<string>();

                            // Adicionando os valores das células à lista de conteúdo da linha (par)
                            conteudo.Add(rowPar.Cells[0].Value.ToString()); // Código do Apartamento
                            conteudo.Add(rowPar.Cells[16].Value.ToString()); // Rua
                            conteudo.Add(rowPar.Cells[17].Value.ToString()); // BLOCO
                            conteudo.Add(rowPar.Cells[19].Value.ToString()); // Apartamento

                            // Adicionando os valores das células à lista de conteúdo da linha (ímpar)
                            conteudo.Add(rowImpar.Cells[0].Value.ToString()); // Código do Apartamento
                            conteudo.Add(rowImpar.Cells[16].Value.ToString()); // Rua
                            conteudo.Add(rowImpar.Cells[17].Value.ToString()); // BLOCO
                            conteudo.Add(rowImpar.Cells[19].Value.ToString()); // Apartamento
                            conteudo.Add(rowImpar.Cells[20].Value.ToString()); // Nome da Empresa

                            // Adicionando a lista de conteúdo da linha à lista principal
                            listaUnificada.Add(conteudo);
                        }

                    }

                    // Percorrendo toda a lista
                    foreach (var item in listaUnificada.OrderBy(x => x[2]).ThenBy(x => x[6]))
                    {
                        //Pega o caminho da etiqueta prn
                        string etiqueta = AppDomain.CurrentDomain.BaseDirectory + "END_LOG 40x100.prn";

                        //Caminho do novo arquivo atualizado
                        string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + "END_LOG 40x100.txt";

                        // Abre o arquivo para escrita
                        StreamReader streamReader;
                        streamReader = File.OpenText(etiqueta);

                        string contents = streamReader.ReadToEnd();

                        streamReader.Close();

                        string linha = contents;

                        StreamWriter streamWriter = File.CreateText(NovaEtiqueta);


                        // Atualiza as variaveis do arquivo
                        //streamWriter.WriteLine("<STX>L");
                        linha = linha.Replace("BR2", "" + item[0]); //Código do Apartamento
                        linha = linha.Replace("R2", "" + item[1]); //Rua
                        linha = linha.Replace("B2", "" + item[2]); // BLOCO
                        linha = linha.Replace("P2", "" + item[3]); //Apartamento
                        linha = linha.Replace("BR1", "" + item[4]); //Código do Apartamento
                        linha = linha.Replace("R1", "" + item[5]); //Rua
                        linha = linha.Replace("B1", "" + item[6]); // BLOCO
                        linha = linha.Replace("P1", "" + item[7]); //Apartamento                            
                        linha = linha.Replace("EMPRESA", "" + item[8]); //Nome da Empresa

                        streamWriter.Write(linha);

                        streamWriter.WriteLine("E"); //Fim do modo de formatação e imprime

                        streamWriter.WriteLine("/222"); //Avanço para corte da etiqueta


                        streamWriter.Close();

                        PrintDialog pd = new PrintDialog();

                        pd.PrinterSettings = new PrinterSettings();

                        if (pd.PrinterSettings.IsValid)
                        {
                            ArgoxPPLA.RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, NovaEtiqueta);

                            //File.Delete(AppDomain.CurrentDomain.BaseDirectory + NovaEtiqueta);
                        }
                        else
                        {
                            MessageBox.Show("Não foi encontrada nenhuma impressora instalada no seu computador!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }




                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na geração da etiqueta: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //Exibe as mensagens do processo
        private void MsgProcesso(string texto)
        {
            Invoke((MethodInvoker)delegate ()
            {
                lblProcesso.Text = texto;

                lblProcesso.Visible = true;
            });
        }

        //Limpa os campos do produto
        private void LimparCampos()
        {
            //Limpa o campo do produto
            txtPesqCodigo.Clear();
            lblDescProduto.Text = "DIGITE O SKU ACIMA";
            lblQuantidade.Text = "0";
            lblUnidade.Text = "-";
            lblValidade.Text = "-";
            lblEndereco.Text = "-";
            txtCapacidade.Clear();
            txtAbastecimento.Clear();
            txtNovoEndereco.Clear();
            //Foca no campo código do produto
            txtPesqCodigo.Focus();
        }

        //habilatar os campos depara
        private void HabilitarDepara()
        {
            cmbRegiaoDe.Enabled = true;
            cmbRuaDe.Enabled = true;
            cmbBlocoDe.Enabled = true;

            cmbRegiaoPara.Enabled = true;
            cmbRuaPara.Enabled = true;
            cmbBlocoPara.Enabled = true;

            lblProcesso.Visible = false;
            btnTransferir.Enabled = true;
        }

        //Desabilita os campos depara
        private void DesabilitarDepara()
        {
            cmbRegiaoDe.Enabled = false;
            cmbRuaDe.Enabled = false;
            cmbBlocoDe.Enabled = false;

            cmbRegiaoPara.Enabled = false;
            cmbRuaPara.Enabled = false;
            cmbBlocoPara.Enabled = false;

            btnTransferir.Enabled = false;
        }

        private void rbtVencimentoI_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtVencimentoI.Checked == true)
            {
                LimparCampos();
            }
        }

        private void rbtVencimentoII_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtVencimentoII.Checked == true)
            {
                LimparCampos();
            }
        }

        private void rbtVencimentoIII_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtVencimentoIII.Checked == true)
            {
                LimparCampos();
            }
        }

        private void cmbRegiao_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbRegiao.Text != "Selecione")
            {
                string ee = cmbRegiao.SelectedItem.ToString();

                //codRegiao = int.Parse(cmbRegiao.SelectedValue);
                // if (codRegiao > 0)
                // ListarRuas(codRegiao, tipoPesquisa);
            }

            string item = cmbRegiao.GetItemText(cmbRegiao.SelectedItem);

            string valor = cmbRegiao.GetItemText(cmbRegiao.Items);

            //MessageBox.Show($"codigo:{valor} Texto:{item}");
        }

        private void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmpresa.Text != string.Empty)
                PesqRegiaoCmb(cmbEmpresa.Text);
        }
    }
}
