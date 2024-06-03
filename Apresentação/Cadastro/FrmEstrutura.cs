using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmEstrutura : Form
    {
        //Array com id(Combobox)
        private int[] regiao;
        private int[] rua;
        private int[] bloco;
        private int[] nivel;
        private int[] apartamento;
        //TreeView
        private int[] treeRegiao;
        private int[] treeRua;
        private int[] treeBloco;
        private int[] treeNivel;
        private int[] treeApartamento;
        private int indexRegiao;
        private int indexRua;
        private int indexBloco;
        private int indexNivel;

        //Controla o progressbar
        System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmEstrutura()
        {
            InitializeComponent();
           
        }

        //Keypress
        private void nmrNovaRegiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no campo nova rua
                nmrNovaRua.Focus();
            }
        }

        private void nmrNovaRua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no campo novo bloco
                nmrNovoBloco.Focus();
            }
        }

        private void nmrNovoBloco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no campo novo nivel
                nmrNovoNivel.Focus();
            }
        }

        private void nmrNovoNivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no campo novo apartamento
                nmrNovoApartamento.Focus();
            }
        }

        private void nmrNovoApartamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no botão de gerar estrutura
                btnGerarEndereco.Focus();
            }
        }

        private void txtBloco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Adiciona o bloco
                AdicionarBloco();
            }
        }

        //Changed
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

        private void nmrNovaRegiao_ValueChanged(object sender, EventArgs e)
        {
            //Habilita e desabilita os campos
            NovaRegiao();
        }

        private void nmrNovaRua_ValueChanged(object sender, EventArgs e)
        {
            //Habilita e desabilita os campos
            NovaRua();
        }

        private void nmrNovoBloco_ValueChanged(object sender, EventArgs e)
        {
            //Habilita e desabilita os campos
            NovoBloco();
        }

        private void nmrNovoNivel_ValueChanged(object sender, EventArgs e)
        {
            //Habilita e desabilita os campos
            NovoNivel();
        }

        private void nmrNovoApartamento_ValueChanged(object sender, EventArgs e)
        {
            //Habilita e desabilita os campos
            NovoApartamento();
        }

        //Mouse click e double click
        private void treeEndereco_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                //Pesquisa endereço no treeview endereço
                PesqTreeEndereco();

                if (treeEndereco.SelectedNode.Text.Substring(0, 3) == "Apa")
                {
                    //Exibe os dados do apartamento
                    ExibirDados();
                }
            }
        }

        //Click
        private void btnPesqEstrutura_Click(object sender, EventArgs e)
        {
            //Pesquisa a região
            PesqRegiao();
            //Pesquisa os endereços
            PesqInformacaoEndereco();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (acesso[0].excluirFuncao == false && !perfilUsuario.Equals("ADMINISTRADOR"))
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Remove o bloco
                RemoverBloco();
            }
        }

        private void btGerarEndereco_Click(object sender, EventArgs e)
        {
             //if (acesso[0].escreverFuncao == false && !perfilUsuario.Equals("ADMINISTRADOR"))
            if (perfilUsuario != "ADMINISTRADOR")
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (nmrNovaRegiao.Value > 0)
                {
                    //Gera Estrutura
                    Thread estrutura = new Thread(GerarEstrutura);
                    estrutura.Start();
                }
                else if (cmbRegiao.Text != "Selecione" && nmrNovaRua.Value != 0 || cmbRegiao.Text != "Selecione" && cmbRua.Text != "Selecione")
                {
                    //Gera endereço
                    Thread estrutura = new Thread(GerarEndereco);
                    estrutura.Start();

                    //Pesquisa os endereços
                    PesqInformacaoEndereco();
                }
                else
                {
                    MessageBox.Show("Por favor, preencha às informações para continuar!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btAtualizar_Click(object sender, EventArgs e)
        {
            if (acesso[0].editarFuncao == false && !perfilUsuario.Equals("ADMINISTRADOR"))
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Atualiza endereço
                AtualizarEndereco();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o formulário
            Close();
        }

        //Pesquisa região e preenche o combobox e a treeview
        private void PesqRegiao()
        {
            try
            {
                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();

                //Limpa o combobox regiao
                cmbRegiao.Items.Clear();
                //Limpa a treeview
                treeEndereco.Nodes[0].Nodes.Clear();

                //Preenche a coleção com apesquisa
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao("");
                //Preenche o combobox região
                gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(n.numeroRegiao));
                //Preenche a treeView
                gerarEnderecoCollection.ForEach(n => treeEndereco.Nodes[0].Nodes.Add("Região " + n.numeroRegiao + " (" + n.tipoRegiao + ")"));

                //Define o tamanho do array para o combobox
                regiao = new int[gerarEnderecoCollection.Count];
                //Define o tamanho do array para a treeView
                treeRegiao = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array combobox
                    regiao[i] = gerarEnderecoCollection[i].codRegiao;
                    //Preenche o array treeview
                    treeRegiao[i] = gerarEnderecoCollection[i].codRegiao;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa as ruas e preenche o combobox e a treeview
        private void PesqRua()
        {
            try
            {
                //Limpa o combobox rua 
                cmbRua.Items.Clear();
                //Adiciona o texto
                cmbRua.Text = "Selecione";
                cmbBloco.Text = "Selecione";
                cmbNivel.Text = "Selecione";
                cmbApartamento.Text = "Selecione";
                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa as ruas da região selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(regiao[cmbRegiao.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbRua.Items.Add(n.numeroRua));

                rua = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    rua[i] = gerarEnderecoCollection[i].codRua;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o bloco e preenche o combobox e a treeview
        private void PesqBloco()
        {
            try
            {
                //Limpa o combobox rua
                cmbBloco.Items.Clear();
                //Adiciona o texto
                cmbBloco.Text = "Selecione";
                cmbNivel.Text = "Selecione";
                cmbApartamento.Text = "Selecione";
                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa os bloco da rua selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqBloco(rua[cmbRua.SelectedIndex]);
                //Preenche o combobox rua
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

        //Pesquisa o nivel no combobox e a treeview
        private void PesqNivel()
        {
            try
            {
                //Limpa o combobox bloco
                cmbNivel.Items.Clear();
                //Adiciona o texto
                cmbNivel.Text = "Selecione";
                cmbApartamento.Text = "Selecione";
                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa os niveis dos blocos selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqNivel(bloco[cmbBloco.SelectedIndex]);
                //Preenche o combobox nivel
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

        //Pesquisa o apartamento no combobox e no treeview
        private void PesqApartamento()
        {
            try
            {
                //Limpa o combobox apartamento
                cmbApartamento.Items.Clear();
                //Adiciona o texto
                cmbNivel.Text = "Selecione";
                cmbApartamento.Text = "Selecione";
                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa os apartamento do nível selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqApartamento(nivel[cmbNivel.SelectedIndex]);
                //Preenche o combobox apartamento
                gerarEnderecoCollection.ForEach(n => cmbApartamento.Items.Add(n.numeroApartamento));

                apartamento = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    apartamento[i] = gerarEnderecoCollection[i].codApartamento;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa rua no treeview
        private void PesqRuaTree(int idRegiao, int indexRegiao)
        {

            //Limpa as ruas
            treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes.Clear();
            //Instância a coleção
            EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
            //Instância o negocios
            EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
            //Pesquisa as ruas
            gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(idRegiao);
            //Preenche a treeView
            gerarEnderecoCollection.ForEach(n => treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes.Add("Rua " + n.numeroRua));
            //Define o tamanho do array
            treeRua = new int[gerarEnderecoCollection.Count];

            //Preenche as ruas da região
            for (int i = 0; i < gerarEnderecoCollection.Count; i++)
            {
                //Preenche o array de ruas
                treeRua[i] = gerarEnderecoCollection[i].codRua;
            }

            //Expande a tree
            treeEndereco.Nodes[0].Nodes[indexRegiao].ExpandAll();
        }

        //Pesquisa o bloco no treeview
        private void PesqBlocoTree(int idRegiao, int indexRegiao, int idRua, int indexRua)
        {
            //Limpa os blocos
            treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes.Clear();
            //Instância a coleção
            EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
            //Instância o negocios
            EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
            //Pesquisa a Bloco
            gerarEnderecoCollection = gerarEnderecoNegocios.PesqBloco(idRua);
            //Preenche a treeView
            gerarEnderecoCollection.ForEach(n => treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes.Add("Bloco " + n.numeroBloco));
            //Define o tamanho do array
            treeBloco = new int[gerarEnderecoCollection.Count];

            //Preenche os blocos
            for (int i = 0; i < gerarEnderecoCollection.Count; i++)
            {
                //Preenche o array de blocos
                treeBloco[i] = gerarEnderecoCollection[i].codBloco;
            }
            //Expande os blocos na treeview
            treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].ExpandAll();
        }

        //Pesuisa o nível no treeview
        private void PesqNivelTree(int idRegiao, int indexRegiao, int idRua, int indexRua, int idBloco, int indexBloco)
        {
            //Limpa os níveis
            treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes.Clear();
            //Instância a coleção
            EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
            //Instância o negocios
            EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
            //Pesquisa a nível
            gerarEnderecoCollection = gerarEnderecoNegocios.PesqNivel(idBloco);
            //Preenche a treeView
            gerarEnderecoCollection.ForEach(n => treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes.Add("Nível " + n.numeroNivel));
            //Define o tamanho do array
            treeNivel = new int[gerarEnderecoCollection.Count];

            //Preenche os níveis
            for (int i = 0; i < gerarEnderecoCollection.Count; i++)
            {
                //Preenche o array
                treeNivel[i] = gerarEnderecoCollection[i].codNivel;
            }
            //Expande os níveis
            treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].ExpandAll();
        }

        //Pesquisa o apartamento no treeview
        private void PesqApartamentoTree(int idRegiao, int indexRegiao, int idRua, int indexRua, int idBloco, int indexBloco, int idNivel, int indexNivel)
        {
            //Limpa os apartamentos
            treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes.Clear();
            //Instância a coleção
            EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
            //Instância o negocios
            EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
            //Pesquisa o apartamento
            gerarEnderecoCollection = gerarEnderecoNegocios.PesqApartamento(idNivel);
            //Preenche a treeView
            gerarEnderecoCollection.ForEach(n => treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes.Add("Apartamento " + n.numeroApartamento));
            //Define o tamanho do array
            treeApartamento = new int[gerarEnderecoCollection.Count];

            //Preenche os apartamentos
            for (int i = 0; i < gerarEnderecoCollection.Count; i++)
            {
                //Preenche o array de apartamento
                treeApartamento[i] = gerarEnderecoCollection[i].codApartamento;
            }

            //Expande o apartamento
            treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].ExpandAll();

            //Preenche as informações dos apartamentos
            for (int i = 0; i < gerarEnderecoCollection.Count; i++)
            {
                PesqApartamentoInformacao(indexRegiao, treeRua[indexRua], indexRua, treeBloco[indexBloco], indexBloco, treeNivel[indexNivel], indexNivel, treeApartamento[i], i);
            }
        }

        //Gera um nova estrutura
        private void GerarEstrutura()
        {
            try
            {
                int intervalo = Convert.ToInt32(Convert.ToInt32((nmrNovaRegiao.Value * nmrNovaRua.Value) * ((nmrNovoBloco.Value * nmrNovoApartamento.Value))) + Convert.ToInt32(nmrNovaRegiao.Value * nmrNovaRua.Value) * (((nmrNovoBloco.Value - lstBlocoDiferenciado.Items.Count) * (nmrNovoNivel.Value - 1)) * nmrQtdAptoNivelAereo.Value) + Convert.ToInt32((nmrNovaRegiao.Value * nmrNovaRua.Value) * ((lstBlocoDiferenciado.Items.Count * (nmrNovoNivel.Value - 1)) * nmrApartamentoDiferenciado.Value)));
                //Define um valor para o progressbar
                Progressbar(intervalo);

                //Intanância o objeto
                Estrutura gerarEndereco = new Estrutura();
                //instância o objeto
                EstruturaNegocios gerarNegocios = new EstruturaNegocios();

                int idRegiao = 0;
                int idRua = 0;
                int idBloco = 0;
                int idNivel = 0;

                int nmrRegiao = 0;
                int nmrRua = 0;
                int nmrBloco = 0;
                int nmrNivel = 0;
                string lado = null;
                string tipo = null;

                /*Verifica a quantidade de regiões a serem geradas*/
                for (int i = 0; nmrNovaRegiao.Value > i; i++)
                {
                    //Pesquisa o numero da nova região
                    gerarEndereco = gerarNegocios.pesqIdRegiao();

                    //Seta o id da região
                    idRegiao = gerarEndereco.codRegiao;
                    //Seta a regiao
                    nmrRegiao = gerarEndereco.numeroRegiao;

                    //Garante que o progressbar seja executado da thread que foi iniciado
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Gera a nova região
                        gerarNegocios.GerarRegiao(gerarEndereco.codRegiao, gerarEndereco.numeroRegiao, cmbTipoRegiao.Text);

                    });

                    for (int ii = 0; nmrNovaRua.Value > ii; ii++)
                    {
                        //Pesquisa uma nova rua
                        gerarEndereco = gerarNegocios.PesqIdRua(idRegiao);
                        //Gera uma nova rua
                        gerarNegocios.GerarRua(idRegiao, gerarEndereco.codRua, gerarEndereco.numeroRua);

                        //Seta o id da rua
                        idRua = gerarEndereco.codRua;
                        //Seta a rua
                        nmrRua = gerarEndereco.numeroRua;

                        for (int iii = 0; nmrNovoBloco.Value > iii; iii++)
                        {
                            //Pesquisa um novo bloco
                            gerarEndereco = gerarNegocios.PesqIdBloco(idRua);
                            //Seta o id do bloco
                            idBloco = gerarEndereco.codBloco;
                            //Seta o bloco
                            nmrBloco = gerarEndereco.numeroBloco;
                            //Gera as informações do bloco
                            lado = gerarInfoBloco(nmrBloco);
                            //Gera uma novo bloco
                            gerarNegocios.GerarBloco(idRua, gerarEndereco.codBloco, gerarEndereco.numeroBloco, lado);

                            for (int iv = 0; nmrNovoNivel.Value > iv; iv++)
                            {
                                //Pesquisa um novo nível
                                gerarEndereco = gerarNegocios.PesqIdNivel(idBloco);

                                //Seta o id do nível
                                idNivel = gerarEndereco.codNivel;
                                //Seta o nivel
                                nmrNivel = gerarEndereco.numeroNivel;
                                //Gera as informações do nível
                                tipo = gerarInfoNivel(nmrNivel);

                                //Gera uma novo nível
                                gerarNegocios.GerarNivel(idBloco, gerarEndereco.codNivel, gerarEndereco.numeroNivel, tipo);

                                //Pesquisa o bloco se foi digitado
                                int index = lstBlocoDiferenciado.FindStringExact(nmrBloco.ToString());

                                //Cria aparatamentos novos para separacao
                                if (nmrNivel.Equals(0))
                                {
                                    //Pesquisa o novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //controla a quantidade de apartamento
                                    int apartamento = 0;

                                    for (int v = 0; nmrNovoApartamento.Value > v; v++)
                                    {
                                        if (apartamento.Equals(0))
                                        {
                                            //Inicia a numeração
                                            apartamento = gerarEndereco.numeroApartamento;
                                        }
                                        else
                                        {
                                            //Continua a numerção
                                            apartamento++;
                                        }

                                        //Garante que o progressbar seja executado da thread que foi iniciado
                                        Invoke((MethodInvoker)delegate ()
                                        {
                                            //Gera as informações do apartamento
                                            gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                        });

                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);
                                    }
                                }
                                //Cria apartamentos novos para o pulmao com blocos diferenciados
                                else if (index >= 0)
                                {
                                    //Pesquisa um novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //Controla a numeração do apartamento
                                    int apartamento = 0;
                                    //Recebe o número inicial do apartamento
                                    apartamento = gerarEndereco.numeroApartamento;

                                    for (int v = 0; nmrApartamentoDiferenciado.Value > v; v++)
                                    {
                                        //Garante que o progressbar seja executado da thread que foi iniciado
                                        Invoke((MethodInvoker)delegate ()
                                        {
                                            //Gera as informações do apartamento
                                            gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());

                                        });

                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);
                                        //incrementa a numeração do apartamento
                                        apartamento++;
                                    }
                                }
                                else //Cria apartamentos novos para o pulmão
                                {
                                    //Pesquisa o novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //controla a quantidade de apartamento
                                    int apartamento = 0;

                                    for (int v = 0; nmrQtdAptoNivelAereo.Value > v; v++)
                                    {
                                        if (apartamento.Equals(0))
                                        {
                                            //Inicia a numeração
                                            apartamento = gerarEndereco.numeroApartamento;
                                        }
                                        else
                                        {
                                            //Continua a numerção
                                            apartamento++;
                                        }

                                        //Garante que o progressbar seja executado da thread que foi iniciado
                                        Invoke((MethodInvoker)delegate ()
                                        {
                                            //Gera a informação do apartamento
                                            gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());

                                        });

                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);

                                    }
                                }
                            }
                        }
                    }
                }

                //Garante que o progressbar seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Limpa todos os campos
                    LimparCampos();
                    //Pesquisa a região
                    PesqRegiao();
                    //Pesquisa os endereços
                    PesqInformacaoEndereco();
                    //Esconde o texto do processo
                    lblProcesso.Visible = false;
                });

                MessageBox.Show("Endereços gerados com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Gera endereço
        private void GerarEndereco()
        {
            try
            {
                //Intanância o objeto
                Estrutura gerarEndereco = new Estrutura();
                //instância o objeto
                EstruturaNegocios gerarNegocios = new EstruturaNegocios();

                int idRegiao = 0;
                int idRua = 0;
                int idBloco = 0;
                int idNivel = 0;
                int nmrRegiao = 0;
                int nmrRua = 0;
                int nmrBloco = 0;
                int nmrNivel = 0;
                string lado = null;
                string tipo = null;

                //Se a região for habilitada
                if (cmbRegiao.Enabled == true && cmbRua.Enabled == false && cmbBloco.Enabled == false && cmbNivel.Enabled == false && cmbApartamento.Enabled == false)
                {
                    //Soma a qtd de endereço
                    int intervalo = Convert.ToInt32(Convert.ToInt32((1 * nmrNovaRua.Value) * ((nmrNovoBloco.Value * nmrNovoApartamento.Value))) + Convert.ToInt32(1 * nmrNovaRua.Value) * (((nmrNovoBloco.Value - lstBlocoDiferenciado.Items.Count) * (nmrNovoNivel.Value - 1)) * nmrQtdAptoNivelAereo.Value) + Convert.ToInt32((1 * nmrNovaRua.Value) * ((lstBlocoDiferenciado.Items.Count * (nmrNovoNivel.Value - 1)) * nmrApartamentoDiferenciado.Value)));

                    if (intervalo > 0)
                    {
                        //Define um valor para o progressbar
                        Progressbar(intervalo);
                    }

                    for (int i = 0; nmrNovaRua.Value > i; i++)
                    {
                        //Garante que o progressbar seja executado da thread que foi iniciado
                        Invoke((MethodInvoker)delegate ()
                        {
                            //Pesquisa uma nova rua
                            gerarEndereco = gerarNegocios.PesqIdRua(regiao[cmbRegiao.SelectedIndex]);
                            //Gera uma nova rua
                            gerarNegocios.GerarRua(regiao[cmbRegiao.SelectedIndex], gerarEndereco.codRua, gerarEndereco.numeroRua);

                            //Seta o id da região
                            idRegiao = regiao[cmbRegiao.SelectedIndex];
                            //seta a região
                            nmrRegiao = Convert.ToInt32(cmbRegiao.SelectedItem);
                        });

                        //Seta o id da rua
                        idRua = gerarEndereco.codRua;
                        //seta a rua
                        nmrRua = gerarEndereco.numeroRua;

                        for (int iii = 0; nmrNovoBloco.Value > iii; iii++)
                        {
                            //Pesquisa um novo bloco
                            gerarEndereco = gerarNegocios.PesqIdBloco(idRua);
                            //Seta o id do bloco
                            idBloco = gerarEndereco.codBloco;
                            //Seta o bloco
                            nmrBloco = gerarEndereco.numeroBloco;
                            //Gera as informações do bloco
                            lado = gerarInfoBloco(nmrBloco);
                            //Gera uma novo bloco
                            gerarNegocios.GerarBloco(idRua, gerarEndereco.codBloco, gerarEndereco.numeroBloco, lado);

                            for (int iv = 0; nmrNovoNivel.Value > iv; iv++)
                            {
                                //Pesquisa um novo nível
                                gerarEndereco = gerarNegocios.PesqIdNivel(idBloco);
                                //Seta o id do nível
                                idNivel = gerarEndereco.codNivel;
                                //Seta a nivel
                                nmrNivel = gerarEndereco.numeroNivel;
                                //Gera as informações do nível
                                tipo = gerarInfoNivel(nmrNivel);
                                //Gera uma novo nível
                                gerarNegocios.GerarNivel(idBloco, gerarEndereco.codNivel, gerarEndereco.numeroNivel, tipo);

                                //Pesquisa o bloco se foi digitado
                                int index = lstBlocoDiferenciado.FindStringExact(nmrBloco.ToString());

                                //Cria aparatamentos novos para separacao
                                if (nmrNivel.Equals(0))
                                {
                                    //Pesquisa o novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //controla a quantidade de apartamento
                                    int apartamento = 0;

                                    for (int v = 0; nmrNovoApartamento.Value > v; v++)
                                    {
                                        if (apartamento.Equals(0))
                                        {
                                            //Inicia a numeração
                                            apartamento = gerarEndereco.numeroApartamento;
                                        }
                                        else
                                        {
                                            //Continua a numerção
                                            apartamento++;
                                        }
                                        //Garante que o progressbar seja executado da thread que foi iniciado
                                        Invoke((MethodInvoker)delegate ()
                                        {
                                            //Gera as informações do apartamento
                                            gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                        });

                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);
                                    }
                                }
                                //Cria apartamentos novos para o pulmao com blocos diferenciados
                                else if (index >= 0)
                                {
                                    //Pesquisa um novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //Controla a numeração do apartamento
                                    int apartamento = 0;
                                    //Recebe o número inicial do apartamento
                                    apartamento = gerarEndereco.numeroApartamento;

                                    for (int v = 0; nmrApartamentoDiferenciado.Value > v; v++)
                                    {
                                        //Garante que o progressbar seja executado da thread que foi iniciado
                                        Invoke((MethodInvoker)delegate ()
                                        {
                                            //Gera as informações do apartamento
                                            gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                        });

                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);
                                        //incrementa a numeração do apartamento
                                        apartamento++;
                                    }
                                }
                                else //Cria apartamentos novos para o pulmão
                                {
                                    //Pesquisa o novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //controla a quantidade de apartamento
                                    int apartamento = 0;

                                    for (int v = 0; nmrQtdAptoNivelAereo.Value > v; v++)
                                    {
                                        if (apartamento.Equals(0))
                                        {
                                            //Inicia a numeração
                                            apartamento = gerarEndereco.numeroApartamento;
                                        }
                                        else
                                        {
                                            //Continua a numerção
                                            apartamento++;
                                        }

                                        //Garante que o progressbar seja executado da thread que foi iniciado
                                        Invoke((MethodInvoker)delegate ()
                                        {
                                            //Gera a informação do apartamento
                                            gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                        });

                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);

                                    }
                                }
                            }
                        }
                    }
                }

                //Garante que o progressbar seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {

                    //Se a rua for habilitada
                    if (cmbRegiao.Enabled == true && cmbRua.Enabled == true && cmbBloco.Enabled == false && cmbNivel.Enabled == false && cmbApartamento.Enabled == false)
                    {
                        //Soma a qtd de endereço
                        int intervalo = Convert.ToInt32(Convert.ToInt32((1 * 1) * ((nmrNovoBloco.Value * nmrNovoApartamento.Value))) + Convert.ToInt32(1 * 1) * (((nmrNovoBloco.Value - lstBlocoDiferenciado.Items.Count) * (nmrNovoNivel.Value - 1)) * nmrQtdAptoNivelAereo.Value) + Convert.ToInt32((1 * 1) * ((lstBlocoDiferenciado.Items.Count * (nmrNovoNivel.Value - 1)) * nmrApartamentoDiferenciado.Value)));

                        if (intervalo > 0)
                        {
                            //Define um valor para o progressbar
                            Progressbar(intervalo);
                        }

                        //Seta o id da região
                        idRegiao = regiao[cmbRegiao.SelectedIndex];
                        //seta a região
                        nmrRegiao = Convert.ToInt32(cmbRegiao.SelectedItem);
                        //Seta o id da rua
                        idRua = rua[cmbRua.SelectedIndex];
                        //seta a rua
                        nmrRua = Convert.ToInt32(cmbRua.SelectedItem);


                        for (int i = 0; nmrNovoBloco.Value > i; i++)
                        {
                            //Pesquisa um novo bloco
                            gerarEndereco = gerarNegocios.PesqIdBloco(rua[cmbRua.SelectedIndex]);
                            //Seta o id do bloco
                            idBloco = gerarEndereco.codBloco;
                            //Seta o bloco
                            nmrBloco = gerarEndereco.numeroBloco;
                            //Gera as informações do bloco
                            lado = gerarInfoBloco(nmrBloco);
                            //Gera uma novo bloco
                            gerarNegocios.GerarBloco(idRua, gerarEndereco.codBloco, gerarEndereco.numeroBloco, lado);

                            for (int iv = 0; nmrNovoNivel.Value > iv; iv++)
                            {
                                //Pesquisa um novo nível
                                gerarEndereco = gerarNegocios.PesqIdNivel(idBloco);
                                //Seta o id do nível
                                idNivel = gerarEndereco.codNivel;
                                //Seta a nivel
                                nmrNivel = gerarEndereco.numeroNivel;
                                //Gera as informações do nível
                                tipo = gerarInfoNivel(nmrNivel);
                                //Gera uma novo nível
                                gerarNegocios.GerarNivel(idBloco, gerarEndereco.codNivel, gerarEndereco.numeroNivel, tipo);

                                //Pesquisa o bloco se foi digitado
                                int index = lstBlocoDiferenciado.FindStringExact(nmrBloco.ToString());

                                //Cria aparatamentos novos para separacao
                                if (nmrNivel.Equals(0))
                                {
                                    //Pesquisa o novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //controla a quantidade de apartamento
                                    int apartamento = 0;

                                    for (int v = 0; nmrNovoApartamento.Value > v; v++)
                                    {
                                        if (apartamento.Equals(0))
                                        {
                                            //Inicia a numeração
                                            apartamento = gerarEndereco.numeroApartamento;
                                        }
                                        else
                                        {
                                            //Continua a numerção
                                            apartamento++;
                                        }
                                        //Gera as informações do apartamento
                                        gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);
                                    }
                                }
                                //Cria apartamentos novos para o pulmao com blocos diferenciados
                                else if (index >= 0)
                                {
                                    //Pesquisa um novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //Controla a numeração do apartamento
                                    int apartamento = 0;
                                    //Recebe o número inicial do apartamento
                                    apartamento = gerarEndereco.numeroApartamento;

                                    for (int v = 0; nmrApartamentoDiferenciado.Value > v; v++)
                                    {
                                        //Gera as informações do apartamento
                                        gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);
                                        //incrementa a numeração do apartamento
                                        apartamento++;
                                    }
                                }
                                else //Cria apartamentos novos para o pulmão
                                {
                                    //Pesquisa o novo apartamento
                                    gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                    //controla a quantidade de apartamento
                                    int apartamento = 0;

                                    for (int v = 0; nmrQtdAptoNivelAereo.Value > v; v++)
                                    {
                                        if (apartamento.Equals(0))
                                        {
                                            //Inicia a numeração
                                            apartamento = gerarEndereco.numeroApartamento;
                                        }
                                        else
                                        {
                                            //Continua a numerção
                                            apartamento++;
                                        }
                                        //Gera a informação do apartamento
                                        gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                        //Gera uma novo apartamento
                                        gerarNegocios.GerarApartamento(gerarEndereco);
                                    }
                                }
                            }
                        }
                    }

                    //Se o bloco for habilitado
                    if (cmbRegiao.Enabled == true && cmbRua.Enabled == true && cmbBloco.Enabled == true && cmbNivel.Enabled == false && cmbApartamento.Enabled == false)
                    {

                        //Soma a qtd de endereço
                        int intervalo = Convert.ToInt32((1 * 1) * ((1 * (nmrNovoNivel.Value - 1)) * nmrNovoApartamento.Value));

                        if (intervalo > 0)
                        {
                            //Define um valor para o progressbar
                            Progressbar(intervalo);
                        }

                        //Seta o id da região
                        idRegiao = regiao[cmbRegiao.SelectedIndex];
                        //seta a região
                        nmrRegiao = Convert.ToInt32(cmbRegiao.SelectedItem);
                        //Seta o id da rua
                        idRua = rua[cmbRua.SelectedIndex];
                        //seta a rua
                        nmrRua = Convert.ToInt32(cmbRua.SelectedItem);
                        //Seta o id da bloco
                        idBloco = bloco[cmbBloco.SelectedIndex];
                        //seta o bloco
                        nmrBloco = Convert.ToInt32(cmbBloco.SelectedItem);

                        for (int i = 0; nmrNovoNivel.Value > i; i++)
                        {
                            //Pesquisa um novo nível
                            gerarEndereco = gerarNegocios.PesqIdNivel(idBloco);
                            //Seta o id do nível
                            idNivel = gerarEndereco.codNivel;
                            //Seta a nivel
                            nmrNivel = gerarEndereco.numeroNivel;
                            //Gera as informações do nível
                            tipo = gerarInfoNivel(nmrNivel);
                            //Gera uma novo nível
                            gerarNegocios.GerarNivel(idBloco, gerarEndereco.codNivel, gerarEndereco.numeroNivel, tipo);

                            if (nmrNivel.Equals(0))
                            {
                                //Pesquisa o novo apartamento
                                gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                //controla a quantidade de apartamento
                                int apartamento = 0;

                                for (int v = 0; nmrNovoApartamento.Value > v; v++)
                                {
                                    if (apartamento.Equals(0))
                                    {
                                        //Inicia a numeração
                                        apartamento = gerarEndereco.numeroApartamento;
                                    }
                                    else
                                    {
                                        //Continua a numerção
                                        apartamento++;
                                    }
                                    //Gera as informações do apartamento
                                    gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                    //Gera uma novo apartamento
                                    gerarNegocios.GerarApartamento(gerarEndereco);
                                }
                            }
                            else //Cria apartamentos novos para o pulmão
                            {
                                //Pesquisa o novo apartamento
                                gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                                //controla a quantidade de apartamento
                                int apartamento = 0;

                                for (int v = 0; nmrQtdAptoNivelAereo.Value > v; v++)
                                {
                                    if (apartamento.Equals(0))
                                    {
                                        //Inicia a numeração
                                        apartamento = gerarEndereco.numeroApartamento;
                                    }
                                    else
                                    {
                                        //Continua a numerção
                                        apartamento++;
                                    }
                                    //Gera a informação do apartamento
                                    gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, apartamento, cmbPallet.SelectedItem.ToString());
                                    //Gera uma novo apartamento
                                    gerarNegocios.GerarApartamento(gerarEndereco);
                                }
                            }
                        }
                    }

                    //Se o nível for habilitado
                    if (cmbRegiao.Enabled == true && cmbRua.Enabled == true && cmbBloco.Enabled == true && cmbNivel.Enabled == true && cmbApartamento.Enabled == false)
                    {
                        //Soma a qtd de endereço
                        int intervalo = Convert.ToInt32((1 * 1) * ((1 * 1) * nmrNovoApartamento.Value));

                        if (intervalo > 0)
                        {
                            //Define um valor para o progressbar
                            Progressbar(intervalo);
                        }

                        //Seta o id da região
                        idRegiao = regiao[cmbRegiao.SelectedIndex];
                        //seta a região
                        nmrRegiao = Convert.ToInt32(cmbRegiao.SelectedItem);
                        //Seta o id da rua
                        idRua = rua[cmbRua.SelectedIndex];
                        //seta a rua
                        nmrRua = Convert.ToInt32(cmbRua.SelectedItem);
                        //Seta o id da bloco
                        idBloco = bloco[cmbBloco.SelectedIndex];
                        //seta o bloco
                        nmrBloco = Convert.ToInt32(cmbBloco.SelectedItem);
                        //Seta o id do nível
                        idNivel = nivel[cmbNivel.SelectedIndex];
                        //seta o id do nível
                        nmrNivel = Convert.ToInt32(cmbNivel.SelectedItem);

                        for (int i = 0; nmrNovoApartamento.Value > i; i++)
                        {

                            if (cmbNivel.SelectedItem.Equals(0))
                            {
                                //Pesquisa um novo apartamento
                                gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                            }
                            else if (cmbNivel.SelectedItem.Equals(1))
                            {
                                //Pesquisa um novo apartamento
                                gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, nmrNivel);
                            }
                            else
                            {
                                //Pesquisa um novo apartamento (Nível 1 = para que possa pesquisar o último apartamento)
                                gerarEndereco = gerarNegocios.PesqIdApartamento(idBloco, idNivel, 1);

                            }
                            //Gera as informações do apartamento
                            gerarEndereco = gerarInfoApartamento(idRegiao, nmrRegiao, idRua, nmrRua, idBloco, nmrBloco, idNivel, nmrNivel, gerarEndereco.codApartamento, gerarEndereco.numeroApartamento, cmbPallet.SelectedItem.ToString());
                            //Gera uma novo apartamento
                            gerarNegocios.GerarApartamento(gerarEndereco);
                        }
                    }

                    LimparCampos();
                    lblProcesso.Visible = false;

                    MessageBox.Show("Endereços gerados com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os endereços no treview
        private void PesqTreeEndereco()
        {
            try
            {
                if (treeEndereco.SelectedNode.Text.Substring(0, 3) == "Reg")
                {
                    //recebe o index da região selecionada
                    indexRegiao = treeEndereco.SelectedNode.Index;
                    //Pesquisa ruas
                    PesqRuaTree(treeRegiao[indexRegiao], indexRegiao);
                }
                else if (treeEndereco.SelectedNode.Text.Substring(0, 3) == "Rua")
                {
                    //recebe o index da rua selecionada
                    indexRua = treeEndereco.SelectedNode.Index;
                    //Pesquisa blocos
                    PesqBlocoTree(treeRegiao[indexRegiao], indexRegiao, treeRua[indexRua], indexRua);
                }
                else if (treeEndereco.SelectedNode.Text.Substring(0, 3) == "Blo")
                {
                    //recebe o index do bloco selecionado
                    indexBloco = treeEndereco.SelectedNode.Index;
                    //Pesquisa nível
                    PesqNivelTree(treeRegiao[indexRegiao], indexRegiao, treeRua[indexRua], indexRua, treeBloco[indexBloco], indexBloco);
                }
                else if (treeEndereco.SelectedNode.Text.Substring(0, 3) == "Nív")
                {
                    //recebe o index do nivel selecionado
                    indexNivel = treeEndereco.SelectedNode.Index;
                    //Pesquisa apartamento
                    PesqApartamentoTree(treeRegiao[indexRegiao], indexRegiao, treeRua[indexRua], indexRua, treeBloco[indexBloco], indexBloco, treeNivel[indexNivel], indexNivel);
                }

                //Desabilita campos
                cmbStatus.Enabled = false;
                cmbDisponivel.Enabled = false;
                cmbStatusPallet.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Informação do apartamento no treeview
        private void PesqApartamentoInformacao(int indexRegiao, int idRua, int indexRua, int idBloco, int indexBloco, int idNivel, int indexNivel, int idApartamento, int indexApartamento)
        {
            try
            {
                //Limpa as informações do apartamento
                treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes[indexApartamento].Nodes.Clear();
                //Instância o objeto
                Estrutura gerarEndereco = new Estrutura();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa a nível
                gerarEndereco = gerarEnderecoNegocios.PesqApartamentoInformacao(idBloco, idNivel, idApartamento);


                //Adiciona o código          
                treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes[indexApartamento].Nodes.Add("Código: " + gerarEndereco.codApartamento).ForeColor = System.Drawing.Color.DarkOrange;
                //Adiciona o endereço           
                treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes[indexApartamento].Nodes.Add("Endereço: " + gerarEndereco.descApartamento);
                //Adiciona o tipo 
                treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes[indexApartamento].Nodes.Add("Tipo:    " + gerarEndereco.tipoApartamento);
                //Adiciona o tamanho do pallet 
                treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes[indexApartamento].Nodes.Add("Palete:  " + gerarEndereco.tamanhoApartamento);
                //Adiciona o lado do endereço
                treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes[indexApartamento].Nodes.Add("Lado:   " + gerarEndereco.ladoBloco);
                //Adiciona o status do endereço
                treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes[indexApartamento].Nodes.Add("Status: " + gerarEndereco.statusApartamento).ForeColor = System.Drawing.Color.Green;
                //Adiciona a disposição do endereço
                treeEndereco.Nodes[0].Nodes[indexRegiao].Nodes[indexRua].Nodes[indexBloco].Nodes[indexNivel].Nodes[indexApartamento].Nodes.Add("Disposição: " + gerarEndereco.disposicaoApartamento).ForeColor = System.Drawing.Color.Red;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa as informações das quantidade de endereços
        private void PesqInformacaoEndereco()
        {
            try
            {
                //Instância o objeto
                Estrutura gerarEndereco = new Estrutura();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa as informações
                gerarEndereco = gerarEnderecoNegocios.PesqInformacao();
                //Seta as informações
                lblQtdRegiao.Text = gerarEndereco.qtdRegiao.ToString();
                lblQtdRua.Text = gerarEndereco.qtdRua.ToString();
                lblQtdBloco.Text = gerarEndereco.qtdBloco.ToString();
                lblQtdApartamento.Text = gerarEndereco.qtdApartamento.ToString();
                lblQtdPulmao.Text = gerarEndereco.qtdPulmao.ToString();
                lblQtdPulmaoIndisponivel.Text = gerarEndereco.qtdPulmaoIndisponivel.ToString();
                lblQtdPulmaoDisponivel.Text = gerarEndereco.qtdPulmaoDisponivel.ToString();
                lblQtdPulmaoOcupado.Text = gerarEndereco.qtdPulmaoOcupado.ToString();
                lblQtdPulmaoVago.Text = gerarEndereco.qtdPulmaoVago.ToString();
                lblQtdBlocados.Text = gerarEndereco.qtdBlocados.ToString();
                lblQtdBlocadosIndisponivel.Text = gerarEndereco.qtdBlocadosIndisponivel.ToString();
                lblQtdBlocadosDisponivel.Text = gerarEndereco.qtdBlocadosDisponivel.ToString();
                lblQtdBlocadosOcupado.Text = gerarEndereco.qtdBlocadosOcupado.ToString();
                lblQtdBlocadosVago.Text = gerarEndereco.qtdBlocadosVago.ToString();
                lblQtdPicking.Text = gerarEndereco.qtdPicking.ToString();
                lblQtdPickingIndisponivel.Text = gerarEndereco.qtdPickingIndisponivel.ToString();
                lblQtdPickingDisponivel.Text = gerarEndereco.qtdPickingDisponivel.ToString();
                lblQtdPickingOcupado.Text = gerarEndereco.qtdPickingOcupado.ToString();
                lblQtdPickingVago.Text = gerarEndereco.qtdPickingVago.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }

        }

        //Exibe as informações na treeview
        private void ExibirDados()
        {
            try
            {
                //Quantidade de caracteres na palavra (retirando a palavra código)
                int palavra = treeEndereco.SelectedNode.FirstNode.Text.Length;
                //Recebe o código do endereço
                txtCodigo.Text = treeEndereco.SelectedNode.FirstNode.Text.Substring(8, (palavra - 8));
                //Quantidade de caracteres na palavra (retirando a palavra endereço)
                palavra = treeEndereco.SelectedNode.FirstNode.NextNode.Text.Length;
                //Recebe o endereço selecionado
                txtEnderecoSelecionado.Text = treeEndereco.SelectedNode.FirstNode.NextNode.Text.Substring(10, (palavra - 10));
                //Quantidade de caracteres na palavra (retirando a apalavra tipo)
                palavra = treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.Text.Length;
                //Recebe o tipo de endereço
                cmbTipo.SelectedItem = treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.Text.Substring(9, (palavra - 9)).ToString();
                //Quantidade de caracteres na palavra (retirando a apalavra pallet)
                palavra = treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.Text.Length;
                //Recebe o tamanho do pallet do endereço
                cmbStatusPallet.SelectedItem = treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.Text.Substring(9, (palavra - 9));
                //Quantidade de caracteres na palavra (retirando a apalavra status)
                palavra = treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.NextNode.NextNode.Text.Length;
                //Recebe o status do endereço
                cmbStatus.SelectedItem = treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.NextNode.NextNode.Text.Substring(8, (palavra - 8));
                //Quantidade de caracteres na palavra (retirando a palavra disposição)
                palavra = treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.NextNode.NextNode.NextNode.Text.Length;
                //Recebe a disponibilidade do endereço
                cmbDisponivel.Text = treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.NextNode.NextNode.NextNode.Text.Substring(12, (palavra - 12));

                //Habilita campos
                cmbStatus.Enabled = true;
                cmbDisponivel.Enabled = true;
                cmbStatusPallet.Enabled = true;
                btnAtualizar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados do apartamento! \n" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Gera as informações do tipo de bloco
        private string gerarInfoBloco(int nmrBloco)
        {
            string ladoBloco = string.Empty;

            if (nmrBloco % 2 == 0)
            {
                ladoBloco = "Par";
            }
            else
            {
                ladoBloco = "Impar";
            }

            ProcessoProgressBar("Gerando Bloco:" + nmrBloco);

            return ladoBloco;
        }

        //Gera as informações do tipo de nível
        private string gerarInfoNivel(int nmrNivel)
        {

            string tipoNivel = string.Empty;

            if (nmrNivel.Equals(0))
            {
                tipoNivel = "Separacao";
            }
            else
            {
                tipoNivel = "Pulmao";
            }

            ProcessoProgressBar("Gerando Nível:" + nmrNivel);

            return tipoNivel;
        }

        //Gerar as inofrmações do tipo de apartamento
        private Estrutura gerarInfoApartamento(int idRegiao, int nmrRegiao, int idRua, int nmrRua, int idBloco, int nmrBloco, int idNivel, int nmrNivel, int idApartamento, int nmrApartamento, string palete)
        {
            Estrutura gerarEndereco = new Estrutura();

            //Verifica o lado do apartamento (Impar ou Par)
            if (gerarEndereco.numeroBloco % 2 == 0)
            {
                gerarEndereco.ladoBloco = "Par";
            }
            else
            {
                gerarEndereco.ladoBloco = "Impar";
            }

            if (nmrNivel.Equals(0))
            {
                gerarEndereco.tipoApartamento = "Separacao";
            }
            else
            {
                gerarEndereco.tipoApartamento = "Pulmao";
            }
            gerarEndereco.codRegiao = idRegiao;
            gerarEndereco.numeroRegiao = nmrRegiao;
            gerarEndereco.codRua = idRua;
            gerarEndereco.numeroRua = nmrRua;
            gerarEndereco.codBloco = idBloco;
            gerarEndereco.numeroBloco = nmrBloco;
            gerarEndereco.codNivel = idNivel;
            gerarEndereco.numeroNivel = nmrNivel;
            gerarEndereco.codApartamento = idApartamento;
            gerarEndereco.numeroApartamento = nmrApartamento;
            gerarEndereco.descApartamento = gerarEndereco.numeroRegiao + "." + gerarEndereco.numeroRua + "." + gerarEndereco.numeroBloco + "." + gerarEndereco.numeroNivel + "." + gerarEndereco.numeroApartamento;
            gerarEndereco.ordemApartamento = String.Format("{0:000}", gerarEndereco.numeroRegiao) + "." + String.Format("{0:000}", gerarEndereco.numeroRua) + "." + String.Format("{0:000}", gerarEndereco.numeroBloco) + "." + String.Format("{0:000}", gerarEndereco.numeroNivel) + "." + String.Format("{0:000}", gerarEndereco.numeroApartamento);
            gerarEndereco.statusApartamento = "Vago";
            gerarEndereco.disposicaoApartamento = "Sim";
            gerarEndereco.tamanhoApartamento = palete;

            //incrementa o progressbar
            IncrementarProgressBar();
            //Exibe o processo na tela
            ProcessoProgressBar("Gerando Endereço:" + gerarEndereco.descApartamento);

            return gerarEndereco;
        }

        //Adiciona o bloco no lista para controle da quantidade de apartamentos
        private void AdicionarBloco()
        {
            try
            {
                if ((Convert.ToInt32(txtBloco.Text) == 0))
                {
                    MessageBox.Show("Bloco não aceito.", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Seleciona o conteúdo
                    txtBloco.SelectAll();
                }
                else
                {
                    //Pesquisa o bloco se foi digitado
                    int index = lstBlocoDiferenciado.FindStringExact(txtBloco.Text);

                    if (index >= 0)
                    {
                        MessageBox.Show("Bloco já digitado.", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Limpa o campo 
                        txtBloco.Clear();
                    }
                    else
                    {
                        //Adiciona o bloco
                        lstBlocoDiferenciado.Items.Add(Convert.ToInt32(txtBloco.Text));
                        //Limpa o campo 
                        txtBloco.Clear();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Digite o número do bloco.", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Remove o bloco da lista para controle da quantidade de apartamentos
        private void RemoverBloco()
        {
            try
            {
                int i = 0;

                i = lstBlocoDiferenciado.SelectedIndex;

                //Adiciona o bloco
                lstBlocoDiferenciado.Items.RemoveAt(i);

            }
            catch (Exception)
            {
                MessageBox.Show("Selecione um bloco para ser removido.", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Atualiza o status, tipo e tamanho do endereço 
        private void AtualizarEndereco()
        {
            try
            {


                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Atualiza
                gerarEnderecoNegocios.AtualizarEndereco(Convert.ToInt32(txtCodigo.Text), cmbStatus.Text, cmbDisponivel.Text, cmbStatusPallet.Text);

                //Seta o tamanho do pallet do endereço
                treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.Text = "Palete:  " + cmbStatusPallet.Text;


                if (Convert.ToInt32(treeEndereco.SelectedNode.Parent.Text.Substring(5, 7 - 5)) == 0)
                {
                    //Seta o tipo de endereço
                    treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.Text = "Tipo:    Picking";
                    //Seleciona o item
                    cmbTipo.SelectedItem = "Picking";
                }
                else
                {
                    if (cmbStatusPallet.Text.Equals("PB"))
                    {
                        //Seta o tipo de endereço
                        treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.Text = "Tipo:    Blocado";
                        //Seleciona o item
                        cmbTipo.SelectedItem = "Blocado";
                    }
                    else
                    {
                        //Seta o tipo de endereço
                        treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.Text = "Tipo:    Pulmao";
                        //Seleciona o item
                        cmbTipo.SelectedItem = "Pulmao";
                    }
                }


                //Seta o status do endereço
                treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.NextNode.NextNode.Text = "Status: " + cmbStatus.Text;
                //Seta o disponibilidade do endereço
                treeEndereco.SelectedNode.FirstNode.NextNode.NextNode.NextNode.NextNode.NextNode.NextNode.Text = "Disponível:  " + cmbDisponivel.Text;

                cmbStatus.Enabled = false;
                cmbDisponivel.Enabled = false;
                cmbStatusPallet.Enabled = false;
                btnAtualizar.Enabled = false;

                //Pesquisa as informações do endereço
                PesqInformacaoEndereco();

                MessageBox.Show("Endereço atualizado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione um apartamento.", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Starta o progressbar
        private void Progressbar(int valor)
        {
            //Garante que o progressbar seja executado da thread que foi iniciado
            Invoke((MethodInvoker)delegate ()
            {
                //Define um valor para o progressbar
                progressBar1.Maximum = (valor);
                //Inicie o cronômetro.
                time.Start();

                //Exibe o texto do processo
                lblProcesso.Visible = true;
            });
        }

        //Incrementa o progressbar
        private void IncrementarProgressBar()
        {
            Invoke((MethodInvoker)delegate ()
            {
                //Incrementar o valor da ProgressBar um valor de uma de cada vez.
                progressBar1.Increment(1);

                //Determinar se ter concluído, comparando o valor da propriedade Value para o value.if máxima (progressBar1.Value == progressBar1.Maximum)
                //Pare o cronômetro.
                time.Stop();
            });
        }

        //Exibe o texto do processo do progressbar
        private void ProcessoProgressBar(string processo)
        {
            Invoke((MethodInvoker)delegate ()
            {
                //Exibe o texto no processo
                lblProcesso.Text = processo;
            });
        }

        //Habilita e desabilita os campo da nova região
        private void NovaRegiao()
        {
            if (nmrNovaRegiao.Value > 0)
            {
                //Desabilita
                cmbRegiao.Enabled = false;
                cmbRua.Enabled = false;
                cmbBloco.Enabled = false;
                cmbNivel.Enabled = false;
                cmbApartamento.Enabled = false;
                cmbTipoRegiao.Enabled = true;
            }
            else
            {
                //Habilita
                cmbRegiao.Enabled = true;
                cmbRua.Enabled = true;
                cmbBloco.Enabled = true;
                cmbNivel.Enabled = true;
                cmbApartamento.Enabled = true;
                cmbTipoRegiao.Enabled = false;

                //Zera
                nmrNovaRua.Value = 0;
                nmrNovoBloco.Value = 0;
                nmrNovoNivel.Value = 0;
                nmrNovoApartamento.Value = 0;

            }
        }

        //Habilita e desabilita os campo da nova rua
        private void NovaRua()
        {
            if (nmrNovaRua.Value > 0 || nmrNovaRegiao.Value > 0)
            {
                //Desabilita
                cmbRua.Enabled = false;
                cmbBloco.Enabled = false;
                cmbNivel.Enabled = false;
                cmbApartamento.Enabled = false;
            }
            else
            {
                //Habilita
                cmbRua.Enabled = true;
                cmbBloco.Enabled = true;
                cmbNivel.Enabled = true;
                cmbApartamento.Enabled = true;
            }
        }

        //Habilita e desabilita os campo do novo bloco
        private void NovoBloco()
        {
            if (nmrNovoBloco.Value > 0 || nmrNovaRua.Value > 0 || nmrNovaRegiao.Value > 0)
            {
                //Desabilita
                cmbBloco.Enabled = false;
                cmbNivel.Enabled = false;
                cmbApartamento.Enabled = false;
                txtBloco.Enabled = true;
                nmrApartamentoDiferenciado.Enabled = true;
                lstBlocoDiferenciado.Enabled = true;
            }
            else
            {
                //Habilita
                cmbBloco.Enabled = true;
                cmbNivel.Enabled = true;
                cmbApartamento.Enabled = true;
                txtBloco.Enabled = false;
                nmrApartamentoDiferenciado.Enabled = false;
                lstBlocoDiferenciado.Enabled = false;
            }
        }

        //Habilita e desabilita os campo do novo nível
        private void NovoNivel()
        {
            if (nmrNovoNivel.Value > 0 || nmrNovoBloco.Value > 0 || nmrNovaRua.Value > 0 || nmrNovaRegiao.Value > 0)
            {
                //Desabilita
                cmbNivel.Enabled = false;
                cmbApartamento.Enabled = false;
                nmrQtdAptoNivelAereo.Enabled = true;
                cmbPallet.Enabled = true;
            }
            else
            {
                //Habilita
                cmbNivel.Enabled = true;
                cmbApartamento.Enabled = true;
                nmrQtdAptoNivelAereo.Enabled = false;
                cmbPallet.Enabled = false;
            }
        }

        //Habilita e desabilita o campo do novo apartamento
        private void NovoApartamento()
        {
            if (nmrNovoApartamento.Value > 0 || nmrNovoNivel.Value > 0 || nmrNovoBloco.Value > 0 || nmrNovaRua.Value > 0 || nmrNovaRegiao.Value > 0)
            {
                //Habilita
                cmbApartamento.Enabled = false;
            }
            else
            {
                //Desabilita
                cmbApartamento.Enabled = true;
            }
        }

        //Limpa os campos
        private void LimparCampos()
        {
            cmbTipoRegiao.SelectedIndex = 0; //Região
            cmbPallet.SelectedIndex = 1; //PM
            nmrNovaRegiao.Value = 0;
            nmrNovaRua.Value = 0;
            nmrNovoBloco.Value = 0;
            nmrNovoNivel.Value = 0;
            nmrNovoApartamento.Value = 0;
            nmrQtdAptoNivelAereo.Value = 2;
            txtBloco.Clear();
            nmrApartamentoDiferenciado.Value = 3;
            lstBlocoDiferenciado.Items.Clear();
            progressBar1.Value = 0;

        }

    }
}