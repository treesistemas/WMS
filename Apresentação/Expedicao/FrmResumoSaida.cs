using Negocios;
using Negocios.Expedicao;
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


namespace Wms.Expedicao
{
    public partial class FrmResumoSaida : Form
    {

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;
        //Instância a coleção de objêto
        SaidaManifestoCollection saidaManifestosColletion = new SaidaManifestoCollection();

        public FrmResumoSaida()
        {
            InitializeComponent();
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a janela da Gride
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

            SaidaManifesto(); //Pesquisa aba saida manifesto

            // AbaDinamica(); //Pesquisa aba saida dinamica

            // DinamicaInterior(); //Pesquisa aba Dinamica Interior

            //AnaliseInterior(); //Pesuisa aba Analise Interiio


        }


        //Pesquisa e busca as infromações da aba saida manifesto.
        private void SaidaManifesto()
        {
            try
            {
                //Instância a camada de negocios
                SaidaManifestoNegocios manifestoNegocios = new SaidaManifestoNegocios();
 
                //A coleção recebe o resultado da consulta
                saidaManifestosColletion = manifestoNegocios.PesqManisfesto(cmbEmpresa.Text ,dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());

                if(saidaManifestosColletion.Count > 0)
                {
                    DadosGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DadosGrid()
        {
            try
            {

                 //Localizando as rotas por tipo
                List<SaidaManifestoModel> manifestoColection = null;

                if (rbtTodos.Checked == true)
                {
                    //Localizando as rotas por tipo
                    manifestoColection = saidaManifestosColletion;
                }

                if (rbtInterior.Checked == true)
                {
                    //Localizando as rotas por tipo
                    manifestoColection = saidaManifestosColletion.FindAll(delegate (SaidaManifestoModel n) { return n.tipoRota == 3; });

                }

                if (rbtCapital.Checked == true)
                {
                    //Localizando as rotas por tipo
                    manifestoColection = saidaManifestosColletion.FindAll(delegate (SaidaManifestoModel n) { return n.tipoRota == 1; });

                }

                if (rbtCompartilhada.Checked == true)
                {
                    //Localizando as rotas por tipo
                    manifestoColection = saidaManifestosColletion.FindAll(delegate (SaidaManifestoModel n) { return n.tipoRota == 2; });

                }

                //Limpa o grid
                dataGridSaidaManifesto.Rows.Clear();

                if (saidaManifestosColletion.Count > 0)
                {

                    //Grid Recebe o resultado da coleção
                    manifestoColection.ForEach(n => dataGridSaidaManifesto.Rows.Add((dataGridSaidaManifesto.Rows.Count + 1),
                                                                                           n.ManiData,
                                                                                           n.Manicodigo,
                                                                                           n.QtCliente,
                                                                                           n.EfCliente,
                                                                                           string.Format("{0:n}", n.porCliente),
                                                                                           n.PedTotal,
                                                                                           n.EfPedido,
                                                                                           string.Format("{0:n}", n.porPedido),
                                                                                           string.Format("{0:n}", n.PesoPedido),
                                                                                           string.Format("{0:n}", n.Valor),
                                                                                           n.Veiculo,
                                                                                           n.Motorista,
                                                                                           n.Regiao,



                                                                                           n.Motcodigo,
                                                                                           n.Veicodigo,
                                                                                           n.PedStatus,

                                                                                           n.Valor,


                                                                                           n.Portatil
                                                                                           ));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Pesquisa e busca as informações da aba Dinamica.
        /*
        
        private void AbaDinamica()
        {
            try
            {
                //Instância a camada de negocios
                SaidaManifestoNegocios manifestoNegocios = new SaidaManifestoNegocios();
                //Instância a coleção de objêto
                SaidaManifestoCollection saidaManifestosColletion = new SaidaManifestoCollection();


                //A coleção recebe o resultado da consulta
                saidaManifestosColletion = manifestoNegocios.PesqManisfesto(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());


                //Limpa o grid
                dataGridView2.Rows.Clear();

                //Grid Recebe o resultado da coleção   
                saidaManifestosColletion.ForEach(n => dataGridView2.Rows.Add((dataGridView2.Rows.Count + 1),
                                                                                       n.Manicodigo,
                                                                                       n.Veiculo,
                                                                                       n.Motorista,
                                                                                       n.Regiao,
                                                                                       n.Motcodigo,
                                                                                       n.Veicodigo,
                                                                                       //n.ManiData,
                                                                                       string.Format("{0:n}", n.PesoPedido),
                                                                                       string.Format("{0:n}", n.Valor),
                                                                                       n.QtCliente,
                                                                                       n.PedTotal,
                                                                                       n.PedStatus
                                                                                       ));
                if (saidaManifestosColletion.Count > 0)
                {


                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        */


        //Pesquisa e busca as informações da aba Analise Interior.
        /*
        private void AnaliseInterior()
           
        {
            try
            {
                //Instância a camada de negocios
                AnaliseInteriorNegocio analiseInteriorNegocio = new AnaliseInteriorNegocio();
                //Instância a coleção de objêto
                AnaliseInteriorCollection analiseInteriorColletion = new AnaliseInteriorCollection();


                //A coleção recebe o resultado da consulta
                analiseInteriorColletion = analiseInteriorNegocio.PesqAnaliseInterior(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());


                //Limpa o grid
                dataGridView1.Rows.Clear();

                //Grid Recebe o resultado da coleção   
                analiseInteriorColletion.ForEach(n => dataGridView1.Rows.Add((dataGridView1.Rows.Count + 1),
                                                                                       n.maniData,
                                                                                       n.maniCodigo,
                                                                                       n.pediCodigo,
                                                                                       string.Format("{0:n}", n.Peso),
                                                                                       string.Format("{0:n}", n.Valor),
                                                                                       n.Placa,
                                                                                       n.Apelido,
                                                                                       n.Cidade,
                                                                                       n.Cliente
                                                                                       ));
                if (analiseInteriorColletion.Count > 0)
                {


                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */


        //Pesquisa e busca as informações da aba Dinamica Interior.
        /*
        private void DinamicaInterior()
        {
            try
            {
                //Instância a camada de negocios
                DinamicaInteriorNegocio dinamicaInteriorNegocio = new DinamicaInteriorNegocio();

                //Instância a coleção de objêto
                DinamicaInteriorCollection dinamicaInteriorCollection = new DinamicaInteriorCollection();


                //A coleção recebe o resultado da consulta
                dinamicaInteriorCollection = dinamicaInteriorNegocio.PesqDinamicaInterior(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());


                //Limpa o grid
                gridDinamicaInterior.Rows.Clear();

                //Grid Recebe o resultado da coleção   
                dinamicaInteriorCollection.ForEach(n => gridDinamicaInterior.Rows.Add((gridDinamicaInterior.Rows.Count + 1),
                                                                                       n.maniData,
                                                                                       n.maniCodigo,
                                                                                       n.Placa,
                                                                                       n.Apelido,
                                                                                       n.Cidade,
                                                                                       string.Format("{0:n}", n.Peso),
                                                                                       string.Format("{0:n}", n.Valor),
                                                                                       n.Cliente,
                                                                                       n.pediCodigo
                                                                                       ));
                if (dinamicaInteriorCollection.Count > 0)
                {


                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        */



        private void dtmInicial_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtmFinal_ValueChanged(object sender, EventArgs e)
        {

        }


        private void rbtCapital_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtCapital.Checked == true)
            {
                DadosGrid();
            }
        }

        private void rbtCompartilhada_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtCompartilhada.Checked == true)
            {
                DadosGrid();
            }
        }

        private void rbtInterior_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtInterior.Checked == true)
            {
                DadosGrid();
            }
        }

        private void rbtTodos_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtTodos.Checked == true)
            {
                DadosGrid();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void FrmResumoSaida_Load(object sender, EventArgs e)
        {
            FrmResumoSaida frm = new FrmResumoSaida();

            frm.WindowState = FormWindowState.Maximized;

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

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void portatilToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
