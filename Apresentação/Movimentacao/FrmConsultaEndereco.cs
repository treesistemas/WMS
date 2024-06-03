using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmConsultaEndereco : Form
    {
        //recebem os id 
        private int[] regiao;
        private int[] rua;
        private int[] bloco;
        private int[] nivel;
        private int[] apartamento;
       
        public FrmConsultaEndereco()
        {
            InitializeComponent();
        }


        private void FrmConsultaEndereco_Load(object sender, EventArgs e)
        {
            //Pesquisa a região
            PesqRegiao();
        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa rua
            PesqRua();
        }

        private void cmbRua_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa o bloco
            PesqBloco();
        }

        private void cmbBloco_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa nível
            PesqNivel();
        }

        private void cmbNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa apartamento
            PesqApartamento();
        }

        private void txtTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text == "Aéreo") 
            {
                //Deixa o grid todos (Aéreo Blocado e Picking) invisivil
                gridTodos.Visible = false;
                //Deixa o grid picking invisivil
                gridPicking.Visible = false;
                //Deixa o grid aéreo visivil
                gridAereo.Visible = true;
                groupEndereco.Text = "Endereços Aéreo";
            }
            else if (cmbTipo.Text == "Blocado")
            {
                //Deixa o grid todos (Aéreo Blocado e Picking) invisivil
                gridTodos.Visible = false;
                //Deixa o grid picking invisivil
                gridPicking.Visible = false;
                //Deixa o grid aéreo visivil
                gridAereo.Visible = true;
                groupEndereco.Text = "Endereços Blocados";
            }
            else if (cmbTipo.Text == "Picking")
            {
                //Deixa o grid todos (Aéreo Blocado e Picking) invisivil
                gridTodos.Visible = false;
                //Deixa o grid aéreo invisivil
                gridAereo.Visible = false;
                //Deixa o grid picking visivil
                gridPicking.Visible = true;
                groupEndereco.Text = "Endereços de Picking";
            }
            else 
            {
                //Deixa o grid aéreo invisivil
                gridAereo.Visible = false;
                //Deixa o grid picking invisivil
                gridPicking.Visible = false;
                //Deixa o grid todos (Aéreo Blocado e Picking) visivil
                gridTodos.Visible = true;
                groupEndereco.Text = "Todos Endereços";
            }
        }

        private void impressãoDeEtiquetaDeAéreoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o endereço
            PesqEndereco();
        }

        //Pesquisa região
        private void PesqRegiao()
        {
            try
            {
                //Insância o objeto
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Insância o objeto
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();                
                //Preenche a coleção com apesquisa
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao();
                //Preenche o combobox região
                gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(n.numeroRegiao));
                //Define o tamanho do array
                regiao = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    regiao[i] = gerarEnderecoCollection[i].codRegiao;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Pesquisa rua
        private void PesqRua()
        {
            try
            {
                //Insância o objeto
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Insância o objeto
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();                
                //Limpa o combobox rua
                cmbRua.Items.Clear();
                //Adiciona o texto
                cmbRua.Text = "Selecione";
                cmbBloco.Text = "Selecione";
                cmbNivel.Text = "Selecione";
                cmbApartamento.Text = "Selecione";
                //Pesquisa as ruas da região selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(regiao[cmbRegiao.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbRua.Items.Add(n.numeroRua));
                //Define o tamanho do array
                rua = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    rua[i] = gerarEnderecoCollection[i].codRua;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesqBloco()
        {
            try
            {
                //Insância o objeto
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Insância o objeto
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();                
                //Limpa o combobox rua
                cmbBloco.Items.Clear();
                //Adiciona o texto
                cmbBloco.Text = "Selecione";
                cmbNivel.Text = "Selecione";
                cmbApartamento.Text = "Selecione";
                //Pesquisa os bloco da rua selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqBloco(rua[cmbRua.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbBloco.Items.Add(n.numeroBloco));
                //Define o tamanho do array
                bloco = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    bloco[i] = gerarEnderecoCollection[i].codBloco;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesqNivel()
        {
            try
            {
                //Insância o objeto
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Insância o objeto
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();                
                //Limpa o combobox bloco
                cmbNivel.Items.Clear();
                //Adiciona o texto
                cmbNivel.Text = "Selecione";
                cmbApartamento.Text = "Selecione";
                //Pesquisa os niveis dos blocos selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqNivel(bloco[cmbBloco.SelectedIndex]);
                //Preenche o combobox nivel
                gerarEnderecoCollection.ForEach(n => cmbNivel.Items.Add(n.numeroNivel));
                //Define o tamanho do array
                nivel = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    nivel[i] = gerarEnderecoCollection[i].codNivel;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesqApartamento()
        {
            try
            {
                //Insância o objeto
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Insância o objeto
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Limpa o combobox apartamento
                cmbApartamento.Items.Clear();
                //Adiciona o texto
                cmbNivel.Text = "Selecione";
                cmbApartamento.Text = "Selecione";
                //Pesquisa os apartamento do nível selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqApartamento(nivel[cmbNivel.SelectedIndex]);
                //Preenche o combobox apartamento
                gerarEnderecoCollection.ForEach(n => cmbApartamento.Items.Add(n.numeroApartamento));

                //Define o tamanho do array
                apartamento = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    apartamento[i] = gerarEnderecoCollection[i].codApartamento;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa endereço
        private void PesqEndereco()
        {
            try
            {
                //Insância o objeto
                ConsultaEnderecoCollection consultaEnderecoCollection = new ConsultaEnderecoCollection();
                //Insância o objeto
                ConsultaEnderecoNegocios consultaEnderecoNegocios = new ConsultaEnderecoNegocios();

                string tipo = null;
                string status = null;
                string lado = null;
                string regiao = null;
                string rua = null;
                string bloco = null;
                string nivel = null;
                string apartamento = null;

                if (cmbTipo.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Selecione o tipo de endereço.", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (cmbTipo.Text.Equals("Picking"))
                {
                    tipo = "Separation";
                }
                else if (cmbTipo.Text.Equals("Aéreo"))
                {
                    tipo = "Storage";
                }
                else if (cmbTipo.Text.Equals("Blocado"))
                {
                    tipo = "Blockad";
                }
                else
                {
                    tipo = "All";
                }

                if (cmbStatus.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Selecione o status do endereço.", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (cmbStatus.Text.Equals("Vago"))
                {
                    status = "Vacant";
                }
                else if (cmbStatus.Text.Equals("Ocupado"))
                {
                    status = "Busy";
                }
                else if (cmbStatus.Text.Equals("Indisponível"))
                {
                    status = "Unavailable";
                }
                else
                {
                    status = "All";
                }

                if (cmbLado.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Selecione o lado do endereço.", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (cmbLado.Text.Equals("Par"))
                {
                    lado = "Pair";
                }
                else if (cmbLado.Text.Equals("Impar"))
                {
                    lado = "Odd";
                }
                else
                {
                    lado = "All";
                }

                if (cmbRegiao.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Selecione uma região.", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
             
                }
                else if (cmbRua.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Selecione uma rua.", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
                else
                {
                    regiao = cmbRegiao.SelectedItem.ToString();
                    rua = cmbRua.SelectedItem.ToString();
                    bloco = cmbBloco.Text;
                    nivel = cmbNivel.Text;
                    apartamento = cmbApartamento.Text;                    
                   
                    //Limpa o grid
                    gridEndereco.Rows.Clear();
                    //Pesquisa e preenche a coleção
                    consultaEnderecoCollection = consultaEnderecoNegocios.PesqEndereco(tipo, status, regiao, rua, bloco, nivel, apartamento, lado);
                    //Preenche o grid
                    consultaEnderecoCollection.ForEach(n => gridEndereco.Rows.Add(n.endereco, n.codEndereco, n.regiao, n.rua, n.bloco, n.nivel, n.apartamento, n.tamanho, n.categoria, n.status, n.disposicao, n.reserva));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
