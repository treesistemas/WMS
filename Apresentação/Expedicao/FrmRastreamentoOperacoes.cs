using DocumentFormat.OpenXml.Wordprocessing;
using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wms.Relatorio.DataSet;

namespace Wms
{
    public partial class FrmRastreamentoOperacoes : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmRastreamentoOperacoes()
        {
            InitializeComponent();
        }

        private void FrmRastreamentoOperacoes_Load(object sender, EventArgs e)
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

        private void cmbOperacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtProduto.Focus();
            }
        }

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtProduto.Text.Length == 0)
                {
                    //Foca no campo 
                    txtPedido.Focus();
                }
                else
                {
                    //Instância o negocios
                    ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                    //Instância a coleção
                    ProdutoCollection produtoCollection = new ProdutoCollection();
                    //A coleção recebe o resultado da consulta
                    produtoCollection = produtoNegocios.PesqProduto(cmbEmpresa.Text, "", txtProduto.Text);

                    if (produtoCollection.Count > 0)
                    {
                        //Pesquisa
                        lblDescProduto.Text = produtoCollection[0].descProduto;
                    }
                    else
                    {
                        MessageBox.Show("Produto não encontrado", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCodUsuario.Focus();
            }
        }

        private void txtCodUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCodUsuario.Text.Length == 0)
                {
                    //Foca no campo 
                    txtLote.Focus();
                }
                else
                {
                    //Instância o negocios
                    UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                    //Instância a coleçãO
                    UsuarioCollection usuarioCollection = new UsuarioCollection();
                    //A coleção recebe o resultado da consulta
                    usuarioCollection = usuarioNegocios.PesqUsuario(txtCodUsuario.Text, "", "", null);

                    if (usuarioCollection.Count > 0)
                    {
                        //Pesquisa
                        lblLoginUsuario.Text = usuarioCollection[0].login;
                    }
                    else
                    {
                        MessageBox.Show("Usuario não encontrado", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEndereco.Focus();
            }
        }

        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus();
            }
        }

        private void txtProduto_KeyDown(object sender, KeyEventArgs e)
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
                    txtProduto.Text = frame.codProduto;
                    lblDescProduto.Text = frame.descProduto;
                }
            }
        }

        private void txtCodUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqUsuario frame = new FrmPesqUsuario();
                //Nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtCodUsuario.Text = Convert.ToString(frame.codUsuario);
                    lblDescProduto.Text = frame.nmUsuario;
                }
            }
        }

        private void txtProduto_TextChanged(object sender, EventArgs e)
        {
            if (txtProduto.Text.Equals(""))
            {
                lblDescProduto.Text = "-";
            }
        }

        private void txtCodUsuario_TextChanged(object sender, EventArgs e)
        {
            if (txtCodUsuario.Text.Equals(""))
            {
                lblLoginUsuario.Text = "-";
            }
        }

        private void cmbOperacao_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            gridOperacoes.Rows.Clear();
            if (cmbOperacao.Text.Equals("ARMAZENAGEM"))
            {
                Col1.Visible = true; //Exibe armazenagem
                Col2.Visible = true; //Exibe data
                Col3.Visible = true; //Exibe usuário
                Col4.Visible = false; //Oculta coluna
                Col5.Visible = false; //Oculta coluna
                Col6.Visible = false; //Oculta coluna
                Col7.Visible = true; //Exibe a coluna nota cega
                Col8.Visible = false; //Oculta coluna              
                Col9.Visible = true; //Exibe a coluna quantidade de entrada
                Col10.Visible = true; //Exibe produto
                Col11.Visible = true; //Exibe endereço
                Col12.Visible = true; //Exibe quantidade
                Col13.Visible = false; //Oculta coluna
                Col14.Visible = true; //Exibe o vencimento
                Col15.Visible = true; //Exibe peso
                Col16.Visible = true; //Exibe lote
                Col17.Visible = false; //Oculta coluna

                Col7.HeaderText = "Nota Cega"; //Atera o texto da coluna
                Col9.HeaderText = "Entrada"; //Atera o texto da coluna
                Col11.HeaderText = "Destino"; //Atera o texto da coluna
                Col12.HeaderText = "Armazenado"; //Atera o texto da coluna
                //Pesquisa a armazenagem do produto
                PesqArmazenagem();

                
                
                
                Col9.Visible = true; //Oculta pedido
                
                
                

            }

            if (cmbOperacao.Text.Equals("TRANSFERÊNCIA"))
            {
                Col4.Visible = true; //Oculta manifesto
                Col5.Visible = true; //Oculta veículo
                Col6.Visible = true; //Oculta pedido
                Col7.Visible = true; //Exibe a coluna 
                Col8.Visible = true; //Exibe endereço de origem
                Col9.Visible = true; //Exibe a coluna
                Col13.Visible = true; //Exibe a coluna
                Col14.Visible = true; //Exibe a coluna
                Col15.Visible = true; //Oculta veículo

                Col4.HeaderText = "Produto"; //Atera o texto da coluna
                Col4.Width = 250; //Define o tamanho da coluna
                Col5.HeaderText = "Origem"; //Atera o texto da coluna
                Col5.Width = 100; //Define o tamanho da coluna
                Col6.HeaderText = "Estoque"; //Atera o texto da coluna
                Col6.Width = 100; //Define o tamanho da coluna
                Col7.HeaderText = "Vencimento"; //Atera o texto da coluna
                Col7.Width = 100; //Define o tamanho da coluna
                Col8.HeaderText = "Lote"; //Atera o texto da coluna
                Col8.Width = 100; //Define o tamanho da coluna
                Col9.HeaderText = "Transferido"; //Atera o texto da coluna
                Col9.Width = 100; //Define o tamanho da coluna
                Col10.HeaderText = "Origem"; //Atera o texto da coluna
                Col10.Width = 100; //Define o tamanho da coluna                                   
                Col11.HeaderText = "Estoque"; //Atera o texto da coluna
                Col11.Width = 100; //Define o tamanho da coluna
                Col12.HeaderText = "Vencimento"; //Atera o texto da coluna
                Col12.Width = 100; //Define o tamanho da coluna
                Col13.HeaderText = "Peso"; //Atera o texto da coluna
                Col13.Width = 100; //Define o tamanho da coluna
                Col14.HeaderText = "Lote"; //Atera o texto da coluna
                Col14.Width = 100; //Define o tamanho da coluna
                Col15.HeaderText = "Total"; //Atera o texto da coluna
                Col15.Width = 100; //Define o tamanho da coluna

                Col16.Visible = false; //Oculta veículo
                Col17.Visible = false; //Oculta pedido

                //Pesquisa a armazenagem do produto
                PesqTranferencia();
            }

            if (cmbOperacao.Text.Equals("ABASTECIMENTO"))
            {
                Col4.Visible = false; //Oculta manifesto
                Col5.Visible = false; //Oculta veículo
                Col6.Visible = false; //Oculta pedido                
                Col9.Visible = false; //Oculta a coluna

                Col7.Visible = true; //Exibe a coluna nota cega
                Col8.Visible = true; //Exibe endereço de origem

                Col7.HeaderText = "O.A"; //Atera o texto da coluna
                Col8.HeaderText = "Origem"; //Atera o texto da coluna
                Col11.HeaderText = "Destino"; //Atera o texto da coluna

                Col12.HeaderText = "Quantidade"; //Atera o texto da coluna
                                                 //Pesquisa o abastecimento do produto
                PesqAbastecimento();
            }

            if (cmbOperacao.Text.Equals("CONFERÊNCIA FLOW RACK"))
            {
                Col0.HeaderText = "Nº"; Col0.Width = 60; //Atera o texto da coluna   
                Col1.HeaderText = "Operação"; Col1.Width = 100;//Atera o texto da coluna
                Col2.HeaderText = "Data"; Col2.Width = 140;//Atera o texto da coluna
                Col3.HeaderText = "Estação"; Col3.Width = 80;//Atera o texto da coluna
                Col4.HeaderText = "Conferênte"; Col4.Width = 100;//Atera o texto da coluna
                Col5.HeaderText = "Volume"; Col5.Width = 80;//Atera o texto da coluna
                Col6.HeaderText = "Nº Volume"; Col6.Width = 80;//Atera o texto da coluna
                Col7.HeaderText = "Produto"; Col7.Width = 290;//Atera o texto da coluna
                Col8.HeaderText = "Quantidade"; Col8.Width = 80;//Atera o texto da coluna
                Col9.HeaderText = "Conferido"; Col9.Width = 100;//Atera o texto da coluna
                Col10.HeaderText = "Corte"; Col10.Width = 60;//Atera o texto da coluna
                Col11.HeaderText = "Endereçamento"; Col11.Width = 100;//Atera o texto da coluna
                Col12.HeaderText = "Usuário"; Col12.Width = 100;//Atera o texto da coluna
                Col13.HeaderText = "Auditoria"; Col13.Width = 140;//Atera o texto da coluna
                Col14.HeaderText = "Auditor"; Col14.Width = 100;//Atera o texto da coluna

                Col15.Visible = true; //Oculta a coluna
                Col16.Visible = false; //Oculta a coluna               
                Col17.Visible = false; //Oculta a coluna


                //Exibe o grid de volume
                gridVolume.Visible = false;
                //Exibe o texto 
                lblTotalVolumes.Visible = false;
                //Exibe a qtd de volume
                lblQtdVolumes.Visible = false;
            
                //Pesquisa a conferência do produto no flowrack
                PesqConferenciaProdutoFlowRack();
            }

            if (cmbOperacao.Text.Equals("CONFERÊNCIA"))
            {
                Col1.Visible = true; //exibe operação
                Col2.Visible = true; //exibe pedido
                Col3.Visible = true; //exibe produto
                Col4.Visible = true; //exibe quantidade
                Col5.Visible = true; //exibe conferido
                Col6.Visible = true; //exibe corte
                Col7.Visible = true; //Exibe o vencimento
                Col8.Visible = true; //Exibe o lote
                Col9.Visible = true; //exibe peso
                Col10.Visible = true; //exibe data conferencia
                Col11.Visible = true; //exibe conferente
                Col12.Visible = true; //exibe manifesto
                Col13.Visible = true; //exibe veículo
                Col14.Visible = false; //Oculta coluna
                Col15.Visible = false; //Oculta coluna
                Col16.Visible = false; //Oculta coluna
                Col17.Visible = false; //Oculta coluna

                Col0.HeaderText = "Nº"; Col0.Width = 60; //Atera o texto da coluna   
                Col1.HeaderText = "Operação"; Col1.Width = 80;//Atera o texto da coluna
                Col2.HeaderText = "Pedido"; Col2.Width = 100;//Atera o texto da coluna
                Col3.HeaderText = "Produto"; Col3.Width = 250;//Atera o texto da coluna
                Col4.HeaderText = "Quantidade"; Col4.Width = 80;//Atera o texto da coluna
                Col5.HeaderText = "Conferido"; Col5.Width = 80;//Atera o texto da coluna
                Col6.HeaderText = "Corte"; Col6.Width = 60;//Atera o texto da coluna
                Col7.HeaderText = "Vencimento"; Col7.Width = 100;//Atera o texto da coluna
                Col8.HeaderText = "Lote"; Col8.Width = 100;//Atera o texto da coluna
                Col9.HeaderText = "Peso"; Col9.Width = 80;//Atera o texto da coluna
                Col10.HeaderText = "Data Conferencia"; Col10.Width = 120;//Atera o texto da coluna
                Col11.HeaderText = "Conferente"; Col11.Width = 100;//Atera o texto da coluna
                Col12.HeaderText = "Manifesto"; Col12.Width = 100;//Atera o texto da coluna
                Col13.HeaderText = "Veículo"; Col13.Width = 80;//Atera o texto da coluna

                //Exibe o grid de volume
                gridVolume.Visible = true;
                //Exibe o texto 
                lblTotalVolumes.Visible = true;
                //Exibe a qtd de volume
                lblQtdVolumes.Visible = true;

                //Pesquisa a conferencia de pedido
                PesqConferenciaPedido();                
            }

            if (cmbOperacao.Text.Equals("CORTE"))
            {
                Col0.HeaderText = "Nº"; Col0.Width = 60; //Atera o texto da coluna   
                Col1.HeaderText = "Operação"; Col1.Width = 70;//Atera o texto da coluna
                Col2.HeaderText = "Data"; Col2.Width = 140;//Atera o texto da coluna
                Col3.HeaderText = "Usuário"; Col3.Width = 100;//Atera o texto da coluna
                Col4.HeaderText = "Perfíl"; Col4.Width = 120;//Atera o texto da coluna
                Col5.HeaderText = "Turno"; Col5.Width = 80;//Atera o texto da coluna
                Col6.HeaderText = "Pedido"; Col6.Width = 80;//Atera o texto da coluna
                Col7.HeaderText = "Produto"; Col7.Width = 300;//Atera o texto da coluna
                Col8.HeaderText = "Quantidade"; Col8.Width = 100;//Atera o texto da coluna
                Col9.HeaderText = "Conferência"; Col9.Width = 100;//Atera o texto da coluna
                Col10.HeaderText = "Corte"; Col10.Width = 100;//Atera o texto da coluna

                Col8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Col9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Col10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                Col11.Visible = false; //Oculta a coluna
                Col12.Visible = false; //Oculta a coluna
                Col13.Visible = false; //Oculta a coluna
                Col13.Visible = false; //Oculta a coluna
                Col14.Visible = false; //Oculta a coluna               
                Col15.Visible = false; //Oculta a coluna
                Col16.Visible = false; //Oculta a coluna

                //Exibe o grid de volume
                gridVolume.Visible = false;
                //Exibe o texto 
                lblTotalVolumes.Visible = false;
                //Exibe a qtd de volume
                lblQtdVolumes.Visible = false;

                //Pesquisa o corte
                PesqCorte();
            }

            if (cmbOperacao.Text.Equals("EXCLUSÃO"))
            {
                Col4.Visible = false; //Oculta manifesto
                Col5.Visible = false; //Oculta veículo
                Col6.Visible = false; //Oculta pedido                
                Col9.Visible = false; //Oculta a coluna
                Col7.Visible = false; //Oculta a coluna nota cega

                Col8.Visible = false; //Oculta endereço de origem
                Col17.Visible = true; //Exibe a coluna

                Col11.HeaderText = "Origem"; //Atera o texto da coluna
                Col12.HeaderText = "Quantidade"; //Atera o texto da coluna
                Col17.HeaderText = "Tipo"; //Atera o texto da coluna
                //Pesquisa as operações do endereçamento
                PesqEnderecamento("EXCLUSÃO");
            }

            if (cmbOperacao.Text.Equals("INCLUSÃO"))
            {
                Col4.Visible = false; //Oculta manifesto
                Col5.Visible = false; //Oculta veículo
                Col6.Visible = false; //Oculta pedido                
                Col9.Visible = false; //Oculta a coluna
                Col7.Visible = false; //Oculta a coluna nota cega

                Col8.Visible = false; //Oculta endereço de origem
                Col17.Visible = true; //Exibe a coluna

                Col11.HeaderText = "Origem"; //Atera o texto da coluna
                Col12.HeaderText = "Quantidade"; //Atera o texto da coluna
                Col17.HeaderText = "Tipo"; //Atera o texto da coluna
                //Pesquisa as operações do endereçamento
                PesqEnderecamento("INCLUSÃO");
            }
        }

        //Pesquisa a armazenagem do produto
        private void PesqArmazenagem()
        {
            try
            {
                if (txtProduto.Text.Equals("") && dtmDataInicial.Value.ToShortDateString().Equals("") && dtmDataFinal.Value.ToShortDateString().Equals(""))
                {
                    MessageBox.Show("Por favor, preencha um dos campos de pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoOperacoesNegocios rastreamentoNegocios = new RastreamentoOperacoesNegocios();
                    //Instância a coleção 
                    ItensNotaEntradaCollection rastreamentoCollection = new ItensNotaEntradaCollection();
                    //A coleção recebe o resultado da consulta
                    rastreamentoCollection = rastreamentoNegocios.PesqArmazenagem(dtmDataInicial.Value.ToShortDateString(), dtmDataFinal.Value.ToShortDateString(), txtProduto.Text, txtCodUsuario.Text, cmbEmpresa.Text);
                    //Limpa o grid
                    gridOperacoes.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    rastreamentoCollection.ForEach(n => gridOperacoes.Rows.Add(gridOperacoes.Rows.Count + 1, n.tipoArmazenamento, n.dataArmazenamento, n.loginUsuario, "", "", "",
                    n.codNotaCega, "", (n.quantidadeNota / n.fatorPulmao) + " " + n.undPulmao, n.codProduto + " - " + n.descProduto, n.descApartamento,
                    (n.quantidadeArmazenada / n.fatorPulmao) + " " + n.undPulmao, "",
                    string.Format("{0:d}", n.validadeProduto), string.Format("{0:n}", n.pesoProduto), n.loteProduto));

                    if (rastreamentoCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum rastreamento encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Exibe a quantidade de rastreamento 
                        lblQtd.Text = gridOperacoes.Rows.Count.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        

        //Pesquisa a transferencia do produto
        private void PesqTranferencia()
        {
            try
            {
                if (txtProduto.Text.Equals("") && dtmDataInicial.Value.ToShortDateString().Equals("") && dtmDataFinal.Value.ToShortDateString().Equals(""))
                {
                    MessageBox.Show("Por favor, preencha um dos campos de pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoOperacoesNegocios rastreamentoNegocios = new RastreamentoOperacoesNegocios();
                    //Instância a coleção 
                    EnderecoPulmaoCollection rastreamentoCollection = new EnderecoPulmaoCollection();
                    //A coleção recebe o resultado da consulta
                    rastreamentoCollection = rastreamentoNegocios.PesqTransferencia(dtmDataInicial.Value.ToShortDateString(), dtmDataFinal.Value.ToShortDateString(), txtProduto.Text, txtCodUsuario.Text, cmbEmpresa.Text);
                    //Limpa o grid
                    gridOperacoes.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    rastreamentoCollection.ForEach(n => gridOperacoes.Rows.Add(gridOperacoes.Rows.Count + 1, n.tipoOperacao, n.dataOperacao, n.loginUsuario,
                    n.codProduto + " - " + n.descProduto,
                    n.descEndereco1, n.qtdCaixaOrigem + " " + n.undCaixaOrigem + " " + n.qtdUnidadeOrigem + " " + n.undUnidadeOrigem, string.Format("{0:d}", n.vencimentoProduto1), n.loteProduto1,
                    n.qtdCxaTranferidaOrigem + " " + n.undCaixaOrigem + " " + n.qtdUndTranferidaOrigem + " " + n.undUnidadeOrigem,

                    n.descApartamento2, n.qtdCaixaDestino + " " + n.undCaixaDestino + " " + n.qtdUnidadeDestino + " " + n.undUnidadeDestino,
                    string.Format("{0:d}", n.vencimentoProduto2), string.Format("{0:n}", n.pesoProduto2), n.loteProduto2,
                    (n.qtdCaixaDestino + n.qtdCxaTranferidaOrigem) + " " + n.undCaixaDestino + " " + (n.qtdUnidadeDestino + n.qtdUndTranferidaOrigem) + " " + n.undUnidadeDestino));

                    if (rastreamentoCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum rastreamento encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Exibe a quantidade de rastreamento 
                        lblQtd.Text = gridOperacoes.Rows.Count.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa o abastecimento do produto
        private void PesqAbastecimento()
        {
            try
            {
                if (txtProduto.Text.Equals("") && dtmDataInicial.Value.ToShortDateString().Equals("") && dtmDataFinal.Value.ToShortDateString().Equals(""))
                {
                    MessageBox.Show("Por favor, preencha um dos campos de pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoOperacoesNegocios rastreamentoNegocios = new RastreamentoOperacoesNegocios();
                    //Instância a coleção 
                    EnderecoPulmaoCollection rastreamentoCollection = new EnderecoPulmaoCollection();
                    //A coleção recebe o resultado da consulta
                    rastreamentoCollection = rastreamentoNegocios.PesqAbastecimento(dtmDataInicial.Value.ToShortDateString(), dtmDataFinal.Value.ToShortDateString(), txtProduto.Text, txtCodUsuario.Text, cmbEmpresa.Text);
                    //Limpa o grid
                    gridOperacoes.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    rastreamentoCollection.ForEach(n => gridOperacoes.Rows.Add(gridOperacoes.Rows.Count + 1, n.tipoOperacao, n.dataOperacao, n.loginUsuario, "", "", "", n.codAbastecimento,
                    n.descEndereco1, n.qtdCaixaOrigem + " " + n.undCaixaOrigem + " " + n.qtdUnidadeOrigem + " " + n.undUnidadeOrigem, n.codProduto + " - " + n.descProduto,
                    n.descApartamento2, n.qtdCaixaDestino + " " + n.undCaixaDestino + " " + n.qtdUnidadeDestino + " " + n.undUnidadeDestino,
                    string.Format("{0:d}", n.vencimentoProduto2), string.Format("{0:n}", n.pesoProduto2), n.loteProduto2));

                    if (rastreamentoCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum rastreamento encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Exibe a quantidade de rastreamento 
                        lblQtd.Text = gridOperacoes.Rows.Count.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa o abastecimento do produto
        private void PesqEnderecamento(string operacao)
        {
            try
            {
                if (txtProduto.Text.Equals("") && dtmDataInicial.Value.ToShortDateString().Equals("") && dtmDataFinal.Value.ToShortDateString().Equals(""))
                {
                    MessageBox.Show("Por favor, preencha um dos campos de pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoOperacoesNegocios rastreamentoNegocios = new RastreamentoOperacoesNegocios();
                    //Instância a coleção 
                    EnderecoPulmaoCollection rastreamentoCollection = new EnderecoPulmaoCollection();
                    //A coleção recebe o resultado da consulta
                    rastreamentoCollection = rastreamentoNegocios.PesqEnderecamento(operacao, cmbEmpresa.Text, dtmDataInicial.Value.ToShortDateString(), dtmDataFinal.Value.ToShortDateString(), txtProduto.Text, txtCodUsuario.Text);
                    //Limpa o grid
                    gridOperacoes.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    rastreamentoCollection.ForEach(n => gridOperacoes.Rows.Add(gridOperacoes.Rows.Count + 1, n.tipoOperacao, n.dataOperacao, n.loginUsuario, "", "", "", "",
                    n.descEndereco1, n.qtdCaixaOrigem + " " + n.undCaixaOrigem + " " + n.qtdUnidadeOrigem + " " + n.undUnidadeOrigem, n.codProduto + " - " + n.descProduto,
                    n.descApartamento2, n.qtdCaixaDestino + " " + n.undCaixaDestino + " " + n.qtdUnidadeDestino + " " + n.undUnidadeDestino, "",
                    string.Format("{0:d}", n.vencimentoProduto2), string.Format("{0:n}", n.pesoProduto2), n.loteProduto2, n.tipoApartamento1));

                    if (rastreamentoCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum rastreamento encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Exibe a quantidade de rastreamento 
                        lblQtd.Text = gridOperacoes.Rows.Count.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa a conferência do produto no flowrack
        private void PesqConferenciaProdutoFlowRack()
        {
            try
            {
                if (txtProduto.Text == string.Empty && txtPedido.Text == string.Empty && txtCodUsuario.Text == string.Empty)
                {
                    MessageBox.Show("Por favor, Digite o código do produto ou do pedido para efetuar a pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dtmDataInicial.Value.ToShortDateString().Equals("") && dtmDataFinal.Value.ToShortDateString().Equals(""))
                {
                    MessageBox.Show("Por favor, preencha um dos campos de pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoOperacoesNegocios rastreamentoNegocios = new RastreamentoOperacoesNegocios();
                    //Instância a camada de objêto 
                    ItensFlowRackCollection itemCollection = new ItensFlowRackCollection();
                    //A coleção recebe o resultado da consulta
                    itemCollection = rastreamentoNegocios.PesqConferenciaFlowRackProduto(dtmDataInicial.Value.ToShortDateString(), dtmDataFinal.Value.ToShortDateString(), txtProduto.Text, txtPedido.Text, txtCodUsuario.Text, cmbEmpresa.Text);
                    //Limpa o grid
                    gridOperacoes.Rows.Clear();

                    if (itemCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum rastreamento encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Grid Recebe o resultado da coleção
                        itemCollection.ForEach(n => gridOperacoes.Rows.Add(gridOperacoes.Rows.Count + 1, "CONFERÊNCIA", n.dataConferencia, n.descEstacao, n.nomeUsuario,
                        n.barraVolume, n.numeroVolume, n.codProduto, n.qtdProduto, n.qtdConferidaProduto, n.qtdCorteProduto, n.endereco, n.nomeEndereco, n.dataAuditoria, n.nomeAuditor));

                        //Exibe a quantidade de rastreamento 
                        lblQtd.Text = gridOperacoes.Rows.Count.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa a conferência do produto no flowrack
        private void PesqCorte()
        {
            try
            {
                if (dtmDataInicial.Value.ToShortDateString().Equals("") && dtmDataFinal.Value.ToShortDateString().Equals(""))
                {
                    MessageBox.Show("Por favor, preencha um dos campos de pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoOperacoesNegocios rastreamentoNegocios = new RastreamentoOperacoesNegocios();
                    //Instância a camada de objêto 
                    ItensFlowRackCollection itemCollection = new ItensFlowRackCollection();
                    //A coleção recebe o resultado da consulta
                    itemCollection = rastreamentoNegocios.PesqCorteProduto(dtmDataInicial.Value.ToShortDateString(), dtmDataFinal.Value.ToShortDateString(), txtProduto.Text, txtPedido.Text, txtCodUsuario.Text, cmbEmpresa.Text);
                    //Limpa o grid
                    gridOperacoes.Rows.Clear();

                    if (itemCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum rastreamento encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Grid Recebe o resultado da coleção
                        itemCollection.ForEach(n => gridOperacoes.Rows.Add(gridOperacoes.Rows.Count + 1, "CORTE", n.dataConferencia, n.nomeUsuario, n.perfilUsuario, n.turnoUsuario,
                        n.codPedido, n.codProduto, n.qtdProduto + " " + n.uniProduto, n.qtdConferidaProduto + " " + n.uniProduto, n.qtdCorteProduto + " " + n.uniProduto));

                        //Exibe a quantidade de rastreamento 
                        lblQtd.Text = gridOperacoes.Rows.Count.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Pedido

        //Pesquisa a conferencia do volume do flowrack
        private void PesqConferenciaVolume()
        {
            try
            {
                if (txtPedido.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, digite o código do pedido!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoOperacoesNegocios rastreamentoNegocios = new RastreamentoOperacoesNegocios();
                    //Instância a coleção 
                    ItensFlowRackCollection volumeCollection = new ItensFlowRackCollection();
                    //A coleção recebe o resultado da consulta
                    volumeCollection = rastreamentoNegocios.PesqConferenciaVolume(txtPedido.Text, cmbEmpresa.Text);
                    //Limpa o grid
                    gridOperacoes.Rows.Clear();
                    //Limpa o grid
                    gridVolume.Rows.Clear();

                    if (volumeCollection.Count > 0)
                    {
                        //Grid Recebe o resultado da coleção
                        volumeCollection.ForEach(n => gridVolume.Rows.Add(n.codPedido, n.numeroVolume, n.endereco, n.dataConferencia, n.nomeUsuario));
                        //Exibe a quantidade de rastreamento 
                        lblQtd.Text = gridOperacoes.Rows.Count.ToString();
                        lblQtdVolumes.Text = gridVolume.Rows.Count.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa a conferencia do volume do flowrack
        private void PesqConferenciaPedido()
        {
            try
            {
                if (txtPedido.Text.Equals("") && txtProduto.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, digite o código do pedido ou do produto!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoOperacoesNegocios rastreamentoNegocios = new RastreamentoOperacoesNegocios();
                    //Instância a coleção 
                    ItensPedidoCollection itemCollection = new ItensPedidoCollection();
                    //A coleção recebe o resultado da consulta
                    itemCollection = rastreamentoNegocios.PesqConferenciaPedido(txtPedido.Text, txtProduto.Text, dtmDataInicial.Value.ToShortDateString(), dtmDataFinal.Value.ToShortDateString(), cmbEmpresa.Text);

                    if (!txtPedido.Text.Equals(string.Empty))
                    {
                        //Pesquisa a conferencia de volume
                        PesqConferenciaVolume();
                    }

                    if (itemCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum rastreamento encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ToolTip toolTip = new ToolTip();

                        //Grid Recebe o resultado da coleção
                        itemCollection.ForEach(n => gridOperacoes.Rows.Add((gridOperacoes.Rows.Count + 1), n.codItem, n.codPedido, n.codProduto + " - " + n.descProduto,
                        n.qtdProduto, n.qtdConferida, n.qtdCorte, n.vencimentoProduto, n.loteProduto, n.pesoProduto, n.dataConferencia, n.loginUsuario, n.codManifesto, n.maniPlaca));
                        //Exibe a quantidade de rastreamento 
                        lblQtd.Text = gridOperacoes.Rows.Count.ToString();
                        lblQtdVolumes.Text = gridVolume.Rows.Count.ToString();

                        int linha = 0;
                        //Adicionando o tootip
                        foreach (ItensPedido i in itemCollection)
                        {
                            if (Convert.ToInt32(i.codItem) == Convert.ToInt32(gridOperacoes.Rows[linha].Cells[1].Value))
                            {
                                //Verifica se existe flowrack
                                if (gridVolume.Rows.Count == 0)
                                {
                                    gridOperacoes.Rows[linha].Cells[4].ToolTipText = i.qtdCaixaProduto + " " + i.uniCaixa + "  " + i.qtdUnidadeProduto + " " + i.uniUnidade;
                                }
                                else //Se não existir
                                {
                                    gridOperacoes.Rows[linha].Cells[4].ToolTipText = i.qtdCaixaProduto + " " + i.uniCaixa;
                                }
                            }

                            linha++;
                        }

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
