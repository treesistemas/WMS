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
using Wms.Relatorio;

namespace Wms.Impressao
{
    public partial class FrmImpressaoCurvaABC : Form
    {
        //Array com id
        private int[] regiao;
        private int[] rua;

        public FrmImpressaoCurvaABC()
        {
            InitializeComponent();
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text.Equals("FLOWRACK"))
            {
                cmbEstacao.Enabled = true; //Habilita o campo estação
                cmbRegiao.Enabled = false; //Desabilita o campo região
                cmbRua.Enabled = false; //Desabilita o campo rua

            }

            if (cmbTipo.Text.Equals("CAIXA"))
            {
                cmbEstacao.Enabled = false; //Desabilita o campo estação
                cmbRegiao.Enabled = true; //Habilita o campo região
                cmbRua.Enabled = true; //Habilita o campo rua
            }
        }

        private void cmbEstacao_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua(cmbRua, cmbRegiao, regiao);
        }

        private void cmbRua_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua(cmbRua, cmbRegiao, regiao);
        }

        private void cmbRegiao_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRegiao.Items.Count == 0)
            {
                PesqRegiao(cmbRegiao);
            }
        }

        private void cmbEstacao_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbEstacao.Items.Count == 0)
            {
                PesqEstacao();
            }
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

        //Pesquisa as estações
        private void PesqEstacao() 
        {
            try
            {
                //Instância o objêto
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                //Instância a coleção 
                EstacaoCollection estacaoCollection = new EstacaoCollection();
                //A coleção recebe o resultado da consulta
                estacaoCollection = abastecimentoNegocios.PesqEstacao();    

                //Preenche o combobox região
                estacaoCollection.ForEach(n => cmbEstacao.Items.Add(n.codEstacao));
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            

            if (cmbTipo.Text.Equals("FLOWRACK"))
            {
                //Gera o relatório de devolução
                FrmCurvaABC frame = new FrmCurvaABC();
                frame.GerarRelatorioCurvaAbcFlowRack(Convert.ToInt32(cmbEstacao.Text), dtmInicial.Text, dtmFinal.Text);
            }
            else if (cmbTipo.Text.Equals("CAIXA"))
            {
                FrmCurvaABC frame = new FrmCurvaABC();
                frame.GerarRelatorioCurvaAbcCaixa(Convert.ToInt32(cmbRegiao.Text), Convert.ToInt32(cmbRua.Text),  dtmInicial.Text, dtmFinal.Text);
            }
            else
            {
                MessageBox.Show("Por favor, selecione um tipo de relatório!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();

        }  
    }
}
