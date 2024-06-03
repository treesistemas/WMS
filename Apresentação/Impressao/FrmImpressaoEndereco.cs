using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms.Impressao
{
    public partial class FrmImpressaoEndereco : Form
    {
        //Array com id
        private int[] regiao;
        private int[] rua;

        public FrmImpressaoEndereco()
        {
            InitializeComponent();
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {

        }

        //Pesquisa região
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
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao("");
                //Preenche o combobox região
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

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

    }
}
