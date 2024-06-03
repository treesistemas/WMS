using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmCategoriaRestricao : Form
    {
        private int[] regiao;
        private int[] rua;

        //Instância o objeto
        CategoriaRestricaoCollection restricaoCollection = new CategoriaRestricaoCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmCategoriaRestricao()
        {
            InitializeComponent();
        }

        private void FrmRestricao_Load(object sender, EventArgs e)
        {
            //Pesquisa a região
            PesqRegiao();
        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua();
        }

        private void btnPesquisarCategoria_Click(object sender, EventArgs e)
        {
            //Pesquisa a categoria
            PesqCategoria();
            //Pesquisa todas as categorias
            PesqListaCategoria();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa endereço com restrição
            PesqEndereco();
        }

        private void gridCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            if (gridCategoria.SelectedRows.Count > 0)
            {
                //Exibe os dados
                ExibirDados();
            }
        }

        private void gridCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridCategoria.SelectedRows.Count > 0)
            {
                //Exibe os dados
                ExibirDados();
            }
        }

        private void gridCategoria_KeyUp(object sender, KeyEventArgs e)
        {
            if (gridCategoria.SelectedRows.Count > 0)
            {
                //Exibe os dados
                ExibirDados();
            }
        }

        private void gridListaCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            //Limpa a selação do grid restrição de categoria
            gridRestricaoCategoria.ClearSelection();
        }

        private void gridRestricaoCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            //Limpa a selação do grid lista de categoria
            gridListaCategoria.ClearSelection();
        }

        private void btnAdicionarRestricao_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Adiciona a categoria
                AdicionarRestricao();
            }
            else if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Adiciona a categoria
                AdicionarRestricao();
            }
        }

        private void btnRemoverCategoria_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Remove a categoria
                RemoverRestricao();
            }
            else if (acesso[0].excluirFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Remove a categoria
                RemoverRestricao();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Adicionar categoria ao endereço
                AssociarCategoriaEndereco();
            }
            else if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Adicionar categoria ao endereço
                AssociarCategoriaEndereco();
            }
        }

        private void btnRemoverEndereco_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Remover categoria do endereço
                RemoverCategoriaEndereco();
            }
            else if (acesso[0].excluirFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Remover categoria do endereço
                RemoverCategoriaEndereco();
            }
        }
       
        private void btnSair_Click(object sender, EventArgs e)
        {
            //fecha a tela
            Close();
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
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao("");
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
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Pesquisa endereço selecionado
        private void PesqEndereco()
        {
            try
            {
                if (cmbRegiao.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Selecione uma região.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbRua.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Selecione uma rua.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbLado.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Selecione um lado.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    //Instância o objeto
                    EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                    //Insância o objeto
                    CategoriaRestricaoNegocios restricaoNegocios = new CategoriaRestricaoNegocios();
                    //Limpa o grid
                    gridEndereco.Rows.Clear();
                    //Pesquisa os endereços
                    gerarEnderecoCollection = restricaoNegocios.PesqEndereco(regiao[cmbRegiao.SelectedIndex], rua[cmbRua.SelectedIndex], cmbLado.Text);
                    //Preenche o grid
                    gerarEnderecoCollection.ForEach(n => gridEndereco.Rows.Add(n.codRegiao, n.numeroRegiao, n.codRua, n.numeroRua, n.ladoBloco, n.idCategoria, n.categoria));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa as categorias
        private void PesqCategoria()
        {
            try
            {
                //Instância a categoria negocios
                CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                //Instância a coleção de categoria
                CategoriaCollection categoriaCollection = new CategoriaCollection();
                //A coleção recebe o resultado da consulta
                categoriaCollection = categoriaNegocios.PesqCategoria();
                //Limpa o grid
                gridCategoria.Rows.Clear();
                //Grid Recebe o resultado da coleção
                categoriaCollection.ForEach(n => gridCategoria.Rows.Add(n.codCategoria, n.descCategoria));

                if (gridCategoria.Rows.Count > 0)
                {
                    //Seleciona a primeira linha do grid
                    gridCategoria.CurrentCell = gridCategoria.Rows[0].Cells[1];
                    //Foca no grid
                    gridCategoria.Focus();
                    //Informa a quantidade de categorias no grid
                    lblQtd.Text = categoriaCollection.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa as categorias
        private void PesqListaCategoria()
        {
            try
            {
                //Instância o objeto
                CategoriaRestricaoNegocios restricaoNegocios = new CategoriaRestricaoNegocios();
                //A coleção recebe o resultado da consulta
                restricaoCollection = restricaoNegocios.PesqListaCategoria();
                //Exibir os dados
                ExibirDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExibirDados()
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridCategoria.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Localizando as categorias não restrita
                List<CategoriaRestricao> restricao = restricaoCollection.FindAll(delegate (CategoriaRestricao n) { return n.codCategoria1 == Convert.ToInt32(gridCategoria.Rows[indice].Cells[0].Value) && n.status == 'N'; });
                //Limpa o grid
                gridListaCategoria.Rows.Clear();
                //Adiciona no grid
                restricao.ForEach(n => gridListaCategoria.Rows.Add(n.codCategoria2, n.descCategoria));

                //Localizando as categorias restrita
                List<CategoriaRestricao> restricao1 = restricaoCollection.FindAll(delegate (CategoriaRestricao n) { return n.codCategoria1 == Convert.ToInt32(gridCategoria.Rows[indice].Cells[0].Value) && n.status == 'S'; });
                //Limpa o grid
                gridRestricaoCategoria.Rows.Clear();
                //Adiciona no grid
                restricao1.ForEach(n => gridRestricaoCategoria.Rows.Add(n.codCategoria2, n.descCategoria));

                //Limpa a selação do grid restrição de categoria
                gridRestricaoCategoria.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir as restrições. \nDetalhes" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AdicionarRestricao()
        {
            try
            {
                if (gridListaCategoria.SelectedRows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridListaCategoria.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Instância as linha da tabela
                    DataGridViewRow linhaCategoria = gridCategoria.CurrentRow;
                    //Recebe o indice   
                    int indiceCategoria = linhaCategoria.Index;
                    //Instância o objeto
                    CategoriaRestricaoNegocios restricaoNegocios = new CategoriaRestricaoNegocios();
                    //Executa a atualização
                    restricaoNegocios.Atualizar(Convert.ToInt32(gridCategoria.Rows[indiceCategoria].Cells[0].Value), Convert.ToInt32(gridListaCategoria.Rows[indice].Cells[0].Value), 'S');

                    //Atualiza a coleção
                    var colecao = restricaoCollection.Where(n => n.codCategoria2 == Convert.ToInt32(gridListaCategoria.Rows[indice].Cells[0].Value) && n.codCategoria1 == Convert.ToInt32(gridCategoria.Rows[indiceCategoria].Cells[0].Value));

                    foreach (var atualiza in colecao)
                    {
                        atualiza.status = 'S';
                    }

                    //Adiciona a categoria selecionada a restrição
                    gridRestricaoCategoria.Rows.Add(gridListaCategoria.Rows[indice].Cells[0].Value, gridListaCategoria.Rows[indice].Cells[1].Value);
                    //Remove a categoria do grid categoria
                    gridListaCategoria.Rows.RemoveAt(indice);

                    //Limpa a selação do grid restrição de categoria
                    gridRestricaoCategoria.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Selecione uma categoria para restrição.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoverRestricao()
        {
            try
            {
                if (gridRestricaoCategoria.SelectedRows.Count > 0)
                {

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridRestricaoCategoria.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Instância as linha da tabela
                    DataGridViewRow linhaCategoria = gridCategoria.CurrentRow;
                    //Recebe o indice   
                    int indiceCategoria = linhaCategoria.Index;
                    //Instância o objeto
                    CategoriaRestricaoNegocios restricaoNegocios = new CategoriaRestricaoNegocios();
                    //Executa a atualização
                    restricaoNegocios.Atualizar(Convert.ToInt32(gridCategoria.Rows[indiceCategoria].Cells[0].Value), Convert.ToInt32(gridRestricaoCategoria.Rows[indice].Cells[0].Value), 'N');

                    //Atualiza a coleção
                    var colecao = restricaoCollection.Where(n => n.codCategoria2 == Convert.ToInt32(gridRestricaoCategoria.Rows[indice].Cells[0].Value) && n.codCategoria1 == Convert.ToInt32(gridCategoria.Rows[indiceCategoria].Cells[0].Value));

                    foreach (var atualiza in colecao)
                    {
                        atualiza.status = 'N';
                    }

                    //Adiciona a categoria selecionada a lista
                    gridListaCategoria.Rows.Add(gridRestricaoCategoria.Rows[indice].Cells[0].Value, gridRestricaoCategoria.Rows[indice].Cells[1].Value);
                    //Remove a categoria do grid categoria
                    gridRestricaoCategoria.Rows.RemoveAt(indice);

                    //Limpa a selação do grid lista de categoria
                    gridListaCategoria.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Selecione uma categoria restringida.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AssociarCategoriaEndereco()
        {
            try
            {
                //Pesquisa endereço com a restrição
                PesqEndereco();
                //verifica se existe alguma categoria selecionada
                if (gridCategoria.SelectedRows.Count > 0)
                {
                    //Instância as linha da tabela categoria
                    DataGridViewRow linha = gridCategoria.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //dataGridView1.ClearSelection();

                    string pesquisaCategoria = gridCategoria.Rows[indice].Cells[1].Value.ToString();
                    bool categoriaEncontrada = false;
                    //Pesquisa se a categoria já foi associada
                    foreach (DataGridViewRow row in gridEndereco.Rows)
                    {
                        if (row.Cells[6].Value != null)
                        {
                            if (row.Cells[6].Value.ToString().Equals(pesquisaCategoria))
                            {
                                categoriaEncontrada = true;
                                break;
                            }
                        }
                    }

                    if (categoriaEncontrada.Equals(true))
                    {
                        MessageBox.Show("A categoria não pode se associada em duplicidade!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //controla se existe restrição
                        bool restricaoEncontrada = false;

                        //Percorre as restrições associada no endereço
                        for (int i = 0; gridEndereco.Rows.Count > i; i++)
                        {
                            //Localizando as categorias restrita pelo o id já associada no endereço
                            List<CategoriaRestricao> restricao = restricaoCollection.FindAll(delegate (CategoriaRestricao n) { return n.codCategoria1 == Convert.ToInt32(gridEndereco.Rows[i].Cells[5].Value) && n.status == 'S'; });

                            foreach (CategoriaRestricao c in restricao)
                            {
                                if (c.codCategoria2.Equals(gridCategoria.Rows[indice].Cells[0].Value))
                                {
                                    MessageBox.Show("A categoria selecionada " + "(" + c.descCategoria + ")" + " é retrita a categoria já associada (" + gridEndereco.Rows[i].Cells[6].Value + "), associação não permitida!");
                                    //Controla a inserção
                                    restricaoEncontrada = true;
                                    //Para de pesquisar ao encontrar a restrição
                                    return;
                                }
                            }
                        }

                        //Verifica a restriões da categoria selecionada
                        if (gridRestricaoCategoria.Rows.Count > 0 && restricaoEncontrada == false)
                        {
                            //Localizando a retrição dessas categorias
                            List<CategoriaRestricao> restricao = restricaoCollection.FindAll(delegate (CategoriaRestricao n) { return n.codCategoria1 == Convert.ToInt32(gridCategoria.Rows[indice].Cells[0].Value) && n.status == 'S'; });

                            foreach (CategoriaRestricao c in restricao)
                            {
                                for (int i = 0; gridEndereco.Rows.Count > i; i++)
                                {
                                    if (c.codCategoria2.Equals(gridEndereco.Rows[i].Cells[5].Value))
                                    {
                                        MessageBox.Show("A categoria selecionada " + "(" + gridCategoria.Rows[indice].Cells[1].Value + ")" + " é retrita a categoria já associada (" + gridEndereco.Rows[i].Cells[6].Value + "), associação não permitida!");
                                        //Controla a inserção
                                        restricaoEncontrada = true;
                                        //Para de pesquisar ao encontrar a restrição
                                        return;
                                    }
                                }
                            }
                        }

                        if (restricaoEncontrada == false)
                        {
                            //Instância o objeto
                            CategoriaRestricaoNegocios restricaoNegocios = new CategoriaRestricaoNegocios();

                            //se for igual a todos adiciona para os dois lados
                            if (cmbLado.Text.Equals("Todos"))
                            {
                                //Associa a categoria ao endereço Pares
                                restricaoNegocios.AssociarEndereco(regiao[cmbRegiao.SelectedIndex], rua[cmbRua.SelectedIndex], Convert.ToInt32(gridCategoria.Rows[indice].Cells[0].Value), "Par");
                                //Inseri no grid a categoria o endereço Pares 
                                gridEndereco.Rows.Add(regiao[cmbRegiao.SelectedIndex], cmbRegiao.Text, rua[cmbRua.SelectedIndex], cmbRua.Text, "Par", gridCategoria.Rows[indice].Cells[0].Value, gridCategoria.Rows[indice].Cells[1].Value);
                                //Associa a categoria ao endereço Impar
                                restricaoNegocios.AssociarEndereco(regiao[cmbRegiao.SelectedIndex], rua[cmbRua.SelectedIndex], Convert.ToInt32(gridCategoria.Rows[indice].Cells[0].Value), "Impar");
                                //Inseri no grid a categoria o endereço Impar 
                                gridEndereco.Rows.Add(regiao[cmbRegiao.SelectedIndex], cmbRegiao.Text, rua[cmbRua.SelectedIndex], cmbRua.Text, "Impar", gridCategoria.Rows[indice].Cells[0].Value, gridCategoria.Rows[indice].Cells[1].Value);

                            }
                            else
                            {
                                //Associa a categoria ao endereço selecionado
                                restricaoNegocios.AssociarEndereco(regiao[cmbRegiao.SelectedIndex], rua[cmbRua.SelectedIndex], Convert.ToInt32(gridCategoria.Rows[indice].Cells[0].Value), cmbLado.SelectedItem.ToString());
                                //Inseri no grid a categoria selecionada 
                                gridEndereco.Rows.Add(regiao[cmbRegiao.SelectedIndex], cmbRegiao.Text, rua[cmbRua.SelectedIndex], cmbRua.Text, cmbLado.Text, gridCategoria.Rows[indice].Cells[0].Value, gridCategoria.Rows[indice].Cells[1].Value);

                            }



                            MessageBox.Show("Categoria associada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Selecione uma categoria para associar ao endereço.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RemoverCategoriaEndereco()
        {
            try
            {
                if (gridEndereco.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Você deseja realmente remover a associação?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {


                        //Instância as linha da tabela categoria
                        DataGridViewRow linha = gridEndereco.CurrentRow;
                        //Recebe o indice   
                        int indice = linha.Index;

                        //Instância o objeto
                        CategoriaRestricaoNegocios restricaoNegocios = new CategoriaRestricaoNegocios();
                        //Executa a remoção do endereco restrito
                        restricaoNegocios.RemoverEndereco(Convert.ToInt32(gridEndereco.Rows[indice].Cells[0].Value), Convert.ToInt32(gridEndereco.Rows[indice].Cells[2].Value), gridEndereco.Rows[indice].Cells[4].Value.ToString(), Convert.ToInt32(gridEndereco.Rows[indice].Cells[5].Value));
                        //Remove a linha do grid
                        gridEndereco.Rows.Remove(linha);

                        MessageBox.Show("Categoria removida com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
