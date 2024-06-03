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
    public partial class FrmPortatil : Form
    {

        public int codUsuario;
        //Perfíl do usuário
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;
        public string impressora;


        PortatilCollection portatilColletion = new PortatilCollection();
        public FrmPortatil()
        {
            InitializeComponent();
        }

        private void FrmPortatil_Load(object sender, EventArgs e)
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            
                //Pesquisa
                PesqPortatil();
           

        }

        private void PesqPortatil()
        {
            try
            {
                //Instância a camada de negocios
                PortatilNegocio portatilNegocios = new PortatilNegocio();

                //A coleção recebe o resultado da consulta
                portatilColletion = portatilNegocios.PesqContPortatil(cmbEmpresa.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());

                if (portatilColletion.Count > 0)
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
                List<PortatilModel> manifestoColection = null;
                manifestoColection = portatilColletion;
               

                //Limpa o grid
                dataGridSaidaPortatil.Rows.Clear();

                if (portatilColletion.Count > 0)
                {

                    //Grid Recebe o resultado da coleção
                    manifestoColection.ForEach(n => dataGridSaidaPortatil.Rows.Add((dataGridSaidaPortatil.Rows.Count + 1),
                                                                                           n.Portatil,
                                                                                           n.ManiDataInserido,
                                                                                           n.Manicodigo,
                                                                                           n.Nmconferente,
                                                                                           n.ManiData));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }

    }


}

