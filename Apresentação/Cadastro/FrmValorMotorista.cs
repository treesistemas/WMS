using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Negocios;
using Negocios.Cadastro;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Wms.Cadastro
{
    public partial class FrmValorMotorista : Form
    {



        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        
        public FrmValorMotorista()
        {
            InitializeComponent();
        }

        private void gridMotorista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        #region  Codigo motorista
        //PESQUISAR OS DADOS DO MOTORISTA
        private void PesqValorMotorista()
        {
            string ativo;

            try
            {

                if (chkPesqAtivo.Checked)
                    ativo = "Ativo";
                else ativo = "Nao";


                //Instância a camada de negocios
                ValorMotoristaNegocio valormotoristaNegocio = new ValorMotoristaNegocio();

                //Instância a camada de objeto
                ValorMotoristaCollection valormotoristaCollection = new ValorMotoristaCollection();

                //A coleção recebe o resultado da consulta                                                
                valormotoristaCollection = valormotoristaNegocio.PesqValorMotorista(Convert.ToDateTime(textPesqValData.Text), ativo);

                //Limpa o grid
                gridMotorista.Rows.Clear();

                //Grid Recebe o resultado da coleção
                valormotoristaCollection.ForEach(n => gridMotorista.Rows.Add(n.valCodigo,
                                                                             n.valSalario,
                                                                             n.valAlimentacao,
                                                                             n.valAlimentacao,
                                                                             n.valData,
                                                                             n.valStatus
                                                                             ));

                if (valormotoristaCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCampos();

                    //Qtd de categoria encontrada
                    //lblQtd.Text = gridMotorista.RowCount.ToString();

                    //Seleciona a primeira linha do grid
                    // gridMotorista.CurrentCell = gridMotorista.Rows[0].Cells[1];
                    //Foca no grid
                    gridMotorista.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma informação encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        //LIMPA OS CAMPOS DO FORMULARIO MOTORISTA
        private void LimparCampos()
        {
            txtValorAlimentacao.Text = string.Empty;
            txtValorSalario.Text = string.Empty;
            chkAtivo.Checked = true;
            txtValorData.Value = DateTime.Now;

        }

        //SETA OS DADOS NOS CAMPOS DO MOTORISTA
        private void DadosCampos()
        {
            try
            {
                if (gridMotorista.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridMotorista.CurrentRow;

                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    textPesqCodigo.Text = gridMotorista.Rows[indice].Cells[0].Value.ToString();
                    //Seta o valor do salario
                    textPesqValorSalario.Text = gridMotorista.Rows[indice].Cells[1].Value.ToString();
                    //Seta o valor da alimentação
                    textPesqValorAlimentacao.Text = gridMotorista.Rows[indice].Cells[2].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //SALVA OS DADOS DO MOTORISTA NA TABELA
        private void SalvarMotorista()
        {
            try
            {

                //string ativo = string.Empty;
                string ativo;

                ValorMotoristaNegocio negocio = new ValorMotoristaNegocio();


                ValorMotorista motorista = new ValorMotorista();
                // motorista.valCodigo = Convert.ToInt32(txtCodigo.Text);
                motorista.valSalario = Convert.ToDouble(txtValorSalario.Text);
                motorista.valAlimentacao = Convert.ToDouble(txtValorAlimentacao.Text);
                motorista.valData = DateTime.Now;

                if (chkAtivo.Checked)
                    ativo = "Ativo";
                else ativo = "Nao";

                motorista.valStatus = ativo;


                //A coleção recebe o resultado da consulta                                                
                negocio.Salvar(motorista);

                LimparCampos();

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error:{ex.Message}");
            }


        }
        #endregion



        #region Codigo Auxiliar espedicao

        //SALVA OS DADOS NA TABELA WMS_AUX_ESPEDICAO
        private void SalvarValorAuxiliarEspedicao()
        {

            //string ativo = string.Empty;
            string ativo;

            ValorAuxiliarNegocio auxiliarnegocio = new ValorAuxiliarNegocio();


            ValorAuxiliar valorauxiliar = new ValorAuxiliar();
            // motorista.valCodigo = Convert.ToInt32(txtCodigo.Text);
            valorauxiliar.auxSalario = Convert.ToDouble(textFormSalarioAuxiliar.Text);
            valorauxiliar.auxAlimentacao = Convert.ToDouble(textFormalimetacaoAuxiliar.Text);
            valorauxiliar.auxData = DateTime.Now;

            if (checkBoxFormAuxiliar.Checked)
                ativo = "Ativo";
            else ativo = "Nao";

            valorauxiliar.auxStatus = ativo;


            //A coleção recebe o resultado da consulta                                                
            auxiliarnegocio.Salvar(valorauxiliar);

            LimpaCamposAuxiliar();

        }

        //LIMPA OS CAMPOR DO FORM AUXILIAR
        private void LimpaCamposAuxiliar()
        {

            textFormSalarioAuxiliar.Text = string.Empty;
            textFormalimetacaoAuxiliar.Text = string.Empty;
            checkBoxFormAuxiliar.Checked = true;
            txtDataAuxiliar.Value = DateTime.Now;

        }

        //SETA OS DADOS AUXLIAR ESPEDICAO
        private void DadosAuxiliarEspedicao()
        {
            try
            {
                if (gridAuxiliar.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridAuxiliar.CurrentRow;

                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtPesqCodigoAuxiliar.Text = gridAuxiliar.Rows[indice].Cells[0].Value.ToString();
                    //Seta o valor do salario
                    textPesqSalarioAuxiliar.Text = gridAuxiliar.Rows[indice].Cells[1].Value.ToString();
                    //Seta o valor da alimentação
                    textPesqAlimentacaoAuxiliar.Text = gridAuxiliar.Rows[indice].Cells[2].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //PESQUISAR OS DADOS E LISTA NA GRID AUXILIAR ESPEDIÇÃO
        private void PesqValorAuxiliar()
        {
            string ativo;

            try
            {

                if (checkPesqAuxiliar.Checked)
                    ativo = "Ativo";
                else ativo = "Nao";


                //Instância a camada de negocios
                ValorAuxiliarNegocio valorauxiliarNegocio = new ValorAuxiliarNegocio();

                //Instância a camada de objeto
                ValorAuxiliarCollection valorauxiliarCollection = new ValorAuxiliarCollection();

                //A coleção recebe o resultado da consulta                                                
                valorauxiliarCollection = valorauxiliarNegocio.PesqValorAuxiliar(Convert.ToDateTime(txtPesqDataAuxiliar.Text), ativo);

                //Limpa o grid
                gridAuxiliar.Rows.Clear();

                //Grid Recebe o resultado da coleção
                valorauxiliarCollection.ForEach(n => gridAuxiliar.Rows.Add(n.auxCodigo,
                                                                             n.auxSalario,
                                                                             n.auxAlimentacao,
                                                                             n.auxData,
                                                                             n.auxStatus
                                                                             ));

                if (valorauxiliarCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosAuxiliarEspedicao();


                    //Foca no grid
                    gridAuxiliar.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma informação encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region COMBUSTIVEL
        //SALVA OS DADOS NA TABELA WMS_COMBUSTIVEL
        private void SalvarValorCombustivel()
        {

            //string ativo = string.Empty;
            string ativo;

            ValorCombustivelNegocio combustivelnegocio = new ValorCombustivelNegocio();


            ValorCombustivel valorcombustivel = new ValorCombustivel();
            // motorista.valCodigo = Convert.ToInt32(txtCodigo.Text);
            valorcombustivel.combValor = Convert.ToDouble(textFormValorCombustivel.Text);
            valorcombustivel.combData = DateTime.Now;

            if (checkFormCombustivel.Checked)
                ativo = "Ativo";
            else ativo = "Nao";

            valorcombustivel.combStatus = ativo;


            //A coleção recebe o resultado da consulta                                                
            combustivelnegocio.Salvar(valorcombustivel);

            LimpaCamposCombustivel();

        }

        //LIMPA OS CAMPOR DO FORM COMBUSTIVEL
        private void LimpaCamposCombustivel()
        {

            textFormValorCombustivel.Text = string.Empty;
            checkFormCombustivel.Checked = true;
            dateTimePicker2.Value = DateTime.Now;

        }

        //SETA OS DADOS VALOR COMBUSTIVEL
        private void DadosCombustivel()
        {
            try
            {
                if (gridCombustivel.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridCombustivel.CurrentRow;

                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    textPesqCodCombustivel.Text = gridCombustivel.Rows[indice].Cells[0].Value.ToString();
                    //Seta o valor do combustivel
                    textPesqValorCombustive.Text = gridCombustivel.Rows[indice].Cells[1].Value.ToString();
                    //Seta o valor da alimentação
                    //textPesqAlimentacaoAuxiliar.Text = gridCombustivel.Rows[indice].Cells[2].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //PESQUISAR OS DADOS E LISTA NA GRID COMBUSTIVEL
        private void PesqValorCombustivel()
        {
            string ativo;

            try
            {

                if (checkCombustivel.Checked)
                    ativo = "Ativo";
                else ativo = "Nao";


                //Instância a camada de negocios
                ValorCombustivelNegocio valorcombustivelNegocio = new ValorCombustivelNegocio();

                //Instância a camada de objeto
                ValorCombustivelCollection valorcombustivelCollection = new ValorCombustivelCollection();

                //A coleção recebe o resultado da consulta                                                
                valorcombustivelCollection = valorcombustivelNegocio.PesqValorCombustivel(Convert.ToDateTime(txtPesqDataCombustivel.Text), ativo);
                

                //Limpa o grid
                gridCombustivel.Rows.Clear();

                //Grid Recebe o resultado da coleção
                valorcombustivelCollection.ForEach(n => gridCombustivel.Rows.Add(n.combCodigo,
                                                                                 n.combValor,
                                                                                 n.combData,
                                                                                 n.combStatus
                                                                                 ));

                if (valorcombustivelCollection.Count > 0)
                {
                    //Seta os dados nos campos
                    DadosCombustivel();


                    //Foca no grid
                    gridCombustivel.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma informação encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion



        #region BOTAO
        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o form
            Close();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                SalvarMotorista();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                SalvarValorAuxiliarEspedicao();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                SalvarValorCombustivel();

            }

        }

        private void btnPesqAuxiliar_Click(object sender, EventArgs e)
        {
            //Pesquisa o motorista
            PesqValorAuxiliar();
        }

        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {

            //Pesquisa o Auxiliar
            PesqValorMotorista();
        }

        private void btnPesqCombustivel_Click(object sender, EventArgs e)
        {
            //Pesquisa o Combustivel
            PesqValorCombustivel();
        }
        #endregion

        private void btNovo_Click(object sender, EventArgs e)
        {
            groupBoxMotorista.Enabled = true;

            groupBoxCombustivel.Enabled = true;
            btSalvar.Enabled = true;
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }


        private void textPesqValData_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmValorMotorista_Load(object sender, EventArgs e)
        {
            textPesqValData.Value = DateTime.Now;
            groupBoxMotorista.Enabled = false;

            groupBoxCombustivel.Enabled = false;
            btSalvar.Enabled = false;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void gridMotorista_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        //Metodo para clicar e selecionar os dados na gride motorista
        private void gridMotorista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Instância as linha da tabela
            DataGridViewRow linha = gridMotorista.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;
            SelecionarGridMotorista(indice);

        }

        //Metodo para clicar e selecionar os dados na gride Motorista
        private void SelecionarGridMotorista(int indice)
        {

            textPesqCodigo.Text = gridMotorista.Rows[indice].Cells[0].Value.ToString();
            textPesqValorSalario.Text = gridMotorista.Rows[indice].Cells[1].Value.ToString();
            textPesqValorAlimentacao.Text = gridMotorista.Rows[indice].Cells[2].Value.ToString();
            textPesqValData.Value = Convert.ToDateTime(gridMotorista.Rows[indice].Cells[4].Value.ToString());
            string ativo = gridMotorista.Rows[indice].Cells[5].Value.ToString();
            if (ativo == "ativo")

                chkPesqAtivo.Checked = true;
            else
                chkPesqAtivo.Checked = false;


        }

        private void gridMotorista_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            
        }

        //Metodo para clicar e selecionar os dados na gride Auxiliar
        private void gridAuxiliar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Instância as linha da tabela
            DataGridViewRow linha = gridAuxiliar.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;
            SelecionarGridAuxiliar(indice);

        }

        //Metodo para clicar e selecionar os dados na gride Auxiliar
        private void SelecionarGridAuxiliar(int indice)
        {

            txtPesqCodigoAuxiliar.Text = gridAuxiliar.Rows[indice].Cells[0].Value.ToString();
            textPesqSalarioAuxiliar.Text = gridAuxiliar.Rows[indice].Cells[1].Value.ToString();
            textPesqAlimentacaoAuxiliar.Text = gridAuxiliar.Rows[indice].Cells[2].Value.ToString();
            txtPesqDataAuxiliar.Value = Convert.ToDateTime(gridAuxiliar.Rows[indice].Cells[3].Value.ToString());
            string ativo = gridAuxiliar.Rows[indice].Cells[4].Value.ToString();
            if (ativo == "Ativo")

                checkPesqAuxiliar.Checked = true;
            else
                checkPesqAuxiliar.Checked = false;


        }

        //Metodo para clicar e selecionar os dados na gride Combustivel
        private void gridCombustivel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Instância as linha da tabela
            DataGridViewRow linha = gridCombustivel.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;
            SelecionarGridCombustivel(indice);

        }


        //Metodo para clicar e selecionar os dados na gride Combustivel
        private void SelecionarGridCombustivel(int indice)
        {

            textPesqCodCombustivel.Text = gridCombustivel.Rows[indice].Cells[0].Value.ToString();
            textPesqValorCombustive.Text = gridCombustivel.Rows[indice].Cells[1].Value.ToString();
            //textPesqAlimentacaoAuxiliar.Text = gridCombustivel.Rows[indice].Cells[2].Value.ToString();
            txtPesqDataCombustivel.Value = Convert.ToDateTime(gridCombustivel.Rows[indice].Cells[2].Value.ToString());
            string ativo = gridCombustivel.Rows[indice].Cells[3].Value.ToString();
            if (ativo == "Ativo")

                checkCombustivel.Checked = true;
            else
                checkCombustivel.Checked = false;


        }
    }


}