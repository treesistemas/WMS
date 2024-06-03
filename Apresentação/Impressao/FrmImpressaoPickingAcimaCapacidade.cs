using Negocios;
using ObjetoTransferencia;
using System;
using System.Windows.Forms;

namespace Wms.Impressao
{
    public partial class FrmImpressaoPickingAcimaCapacidade : Form
    {
        //Array com id
        private int[] regiao;
        private int[] rua;

        public FrmImpressaoPickingAcimaCapacidade()
        {
            InitializeComponent();
        }

        private void cmbTipo_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbTipo.Text.Equals("ENDERECO"))
            {
                cmbRegiao.Enabled = true;
                cmbRua.Enabled = true;
            }
            else
            {
                cmbRegiao.Enabled = false;
                cmbRua.Enabled = false;
            }
        }

        private void cmbRegiao_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRegiao.Items.Count == 0)
            {
                PesqRegiao(cmbRegiao);
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text.Equals("ENDERECO"))
            {
                cmbRegiao.Enabled = true;
                cmbRua.Enabled = true;
            }
            else
            {
                cmbRegiao.Enabled = false;
                cmbRua.Enabled = false;
            }
        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua(cmbRua, cmbRegiao, regiao);
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            Relatorio.FrmAcimaCapacidade frame = new Relatorio.FrmAcimaCapacidade();

            int regiao = 0;
            int rua = 0;

            if (!cmbTipo.Text.Equals("TODOS"))
            {
                if (cmbRegiao.Text.Equals("SELEC") || cmbRegiao.Text.Equals(string.Empty))
                {
                    regiao = 0;
                }
                else
                {
                    regiao = Convert.ToInt32(cmbRegiao.Text);
                }

                if (cmbRua.Text.Equals("SELEC") || cmbRua.Text.Equals(string.Empty))
                {
                    rua = 0;
                }
                else
                {
                    rua = Convert.ToInt32(cmbRua.Text);
                }
            }

            frame.GerarRelatorio(regiao, rua);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Pesquisa região
        private void PesqRegiao(ComboBox cmbRegiao)
        {
            try
            {
                cmbRegiao.Text = "SELEC";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Limpa o combobox regiao
                cmbRegiao.Items.Clear();
                //Preenche a coleção com apesquisa
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao("");
                //Preenche o combobox região
                gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(n.numeroRegiao));

                if (cmbRegiao.Name == "cmbRegiao")
                {


                    //Define o tamanho do array para o combobox
                    regiao = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        regiao[i] = gerarEnderecoCollection[i].codRegiao;
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
                cmbRua.Text = "SELEC";

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

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

    }
}
