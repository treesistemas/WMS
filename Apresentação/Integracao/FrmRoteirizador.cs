using Negocios;
using Negocios.Expedicao;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wms.Relatorio.DataSet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Wms.Integracao
{
    public partial class FrmRoteirizador : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        //Instância o objêto Pedido
        PedidoCollection pedidoCollection = new PedidoCollection();
        //Instância o objêto
        ClienteCollection clienteCollection = new ClienteCollection();
        //Caminho do arquivo 
        string arquivoCSV = "";

        public FrmRoteirizador()
        {
            InitializeComponent();
        }

        //Altera a cor da guia do tabControl
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush bgBrush; //Cor do fundo
            Brush foreColorBrush; //Cor Fundo
            Font font;



            if (e.Index == tabControl.SelectedIndex)
            {
                //Muda aparência do TabControl selecionado
                font = new Font(e.Font, FontStyle.Regular | FontStyle.Regular);
                bgBrush = new System.Drawing.SolidBrush(Color.White);
                foreColorBrush = new SolidBrush(Color.DimGray);

                if (e.Index == 0)
                {
                    foreColorBrush = Brushes.Green;
                }

                if (e.Index == 1)
                {
                    foreColorBrush = Brushes.RoyalBlue;
                }

            }
            else
            {
                //Muda aparência do TabControl selecionado
                font = new Font(e.Font, FontStyle.Regular);
                bgBrush = new System.Drawing.SolidBrush(Color.White);
                foreColorBrush = new SolidBrush(Color.DimGray);
            }


            //Alinhamento do texto
            var tabName = tabControl.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            //Preencher tab selecionada
            e.Graphics.FillRectangle(bgBrush, e.Bounds);

            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width + 3, r.Height - 3);



            e.Graphics.DrawString(tabName, font, foreColorBrush, r, sf);

            sf.Dispose(); //Libera os recursos

            if (e.Index == tabControl.SelectedIndex)
            {
                bgBrush.Dispose();
            }
            else
            {
                bgBrush.Dispose();
                foreColorBrush.Dispose();
            }


        }

        private void txtCodRota_TextChanged(object sender, EventArgs e)
        {
            if (txtCodRota.Text.Equals(""))
            {
                //Limpa o campo
                lblRota.Text = "-";
            }
        }

        private void cmbRoteirizador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtHorario.Focus();
            }
        }

        private void txtHorario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmInicial.Focus();
            }
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
                txtCodRota.Focus();
            }
        }

        private void txtCodRota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !txtCodRota.Text.Equals(string.Empty))
            {
                //Pesquisa a rota
                PesqRota();
            }
            else
            {
                btnPesquisar.Focus();
            }
        }

        private void txtCodRota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqRota frame = new FrmPesqRota();

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtCodRota.Text = frame.codRota;
                    lblRota.Text = frame.descRota;
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string status = null;

            //Verifica o status
            if (rbtTodos.Checked == true)
            {
                status = "TODOS";
            }
            else if (rbtCarteira.Checked == true)
            {
                status = "CARTEIRA";
            }
            else if (rbtFaturados.Checked == true)
            {
                status = "FATURADOS";
            }

            if (!(cmbRoteirizador.Text.Equals("SELECIONE") || cmbRoteirizador.Text.Equals(string.Empty)))
            {
                //Pesquisa os pedido
                PesqPedido(status);
            }
            else
            {
                MessageBox.Show("Por favor, selecione um roteirizador!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //Exporta o arquivo
            ExportarPedidoFusion();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            if (cmbRoteirizador1.Text.Equals("SELECIONE") || cmbRoteirizador1.Text.Equals(string.Empty))
            {
                MessageBox.Show("Selecione o roteirizador!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Instancia a classe.
                using (OpenFileDialog dirDialog = new OpenFileDialog())
                {
                    // Mostra a janela de escolha do directorio
                    DialogResult res = dirDialog.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        //O utilizador carregou no OK
                        txtArquivo.Text = dirDialog.FileName.ToString();
                        //Lê o arquivo
                        LerArquivo();
                    }
                }
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            //Importar o arquivo
            ImportarArquivo();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSair1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Pesquisa a rota
        private void PesqRota()
        {
            try
            {
                //Instância o negocios
                RotaNegocios rotaNegocios = new RotaNegocios();
                //Instância a coleção
                RotaCollection rotaCollection = new RotaCollection();
                //A coleção recebe o resultado da consulta
                rotaCollection = rotaNegocios.PesqRota("", Convert.ToInt32(txtCodRota.Text), null);

                if (rotaCollection.Count > 0)
                {
                    lblRota.Text = rotaCollection[0].descRota.ToString();

                    if (gridRota.Rows.Count == 0)
                    {
                        gridRota.Rows.Add(gridRota.Rows.Count + 1, rotaCollection[0].codRota, rotaCollection[0].descRota);

                        //Limpa os campos
                        txtCodRota.Clear();
                        lblRota.Text = "-";

                    }
                    else
                    {
                        //Verifica se o produto já foi digitado
                        for (int i = 0; gridRota.Rows.Count > i; i++)
                        {
                            if (Convert.ToString(gridRota.Rows[i].Cells[1].Value).Equals(txtCodRota.Text))
                            {
                                txtCodRota.Focus();
                                MessageBox.Show("Produto já digitado!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            else if (gridRota.Rows.Count - 1 == i)
                            {
                                //Adiciona
                                gridRota.Rows.Add(gridRota.Rows.Count + 1, rotaCollection[0].codRota, rotaCollection[0].descRota);

                                //Limpa os campos
                                txtCodRota.Clear();
                                lblRota.Text = "-";
                                txtCodRota.Focus();
                                break;
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Rota não encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os pedidos
        private void PesqPedido(string status)
        {
            try
            {
                //Limpa a coleção de objeto
                pedidoCollection.Clear();
                //Instância o objeto
                RoteirizadorNegocios routeirizadorNegocios = new RoteirizadorNegocios();
                //Instância o objêto Pedido
                PedidoCollection pedidoRotaCollection = new PedidoCollection();

                //Instância o tamanho um array 
                int[] codigoRota = new int[gridRota.Rows.Count];
                //Qtd de Rota Selecionada
                int qtdRotaSelecionada = 0;

                //Verifica se existe alguma rota selacionada
                for (int i = 0; gridRota.Rows.Count > i; i++)
                {

                    if (Convert.ToBoolean(gridRota.Rows[i].Cells[0].Value) == true)
                    {
                        //Seta no array os códigos das rotas
                        codigoRota[i] = Convert.ToInt32(gridRota.Rows[i].Cells[1].Value);
                        qtdRotaSelecionada++;
                    }
                }

                if (qtdRotaSelecionada > 0)
                {
                    for (int i = 0; gridRota.Rows.Count > i; i++)
                    {
                        if (Convert.ToBoolean(gridRota.Rows[i].Cells[0].Value) == true)
                        {
                            //Pesquisa - A coleção recebe o resultado da consulta
                            pedidoCollection = routeirizadorNegocios.PesqPedido(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), txtHorario.Text, Convert.ToInt32(gridRota.Rows[i].Cells[1].Value), status);

                            //Concatena o objêto encontrado
                            pedidoRotaCollection.AddRange(pedidoCollection);
                        }
                    }
                }
                else
                {
                    //Pesquisa
                    pedidoCollection = routeirizadorNegocios.PesqPedido(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), txtHorario.Text, 0, status);
                }


                if (qtdRotaSelecionada == 0)
                {
                    //Limpa o grid
                    gridPedido.Rows.Clear();
                    //Percorre a coleção
                    pedidoCollection.ForEach(n => gridPedido.Rows.Add(gridPedido.Rows.Count + 1, n.dataPedido, n.codPedido, n.tipoPedido, string.Format(@"{0:N}", n.totalPedido),
                    n.qtdItensPedido, string.Format(@"{0:N}", n.pesoPedido), string.Format(@"{0:N}", n.cubagemPedido), n.codRotaCliente + " - " + n.rotaCliente));
                }
                else
                {
                    //Limpa o grid
                    gridPedido.Rows.Clear();
                    //Limpa a coleção
                    pedidoCollection.Clear();
                    //Concatena o objêto encontrado
                    pedidoCollection.AddRange(pedidoRotaCollection);
                    //Percorre a coleção
                    pedidoCollection.ForEach(n => gridPedido.Rows.Add(gridPedido.Rows.Count + 1, n.dataPedido, n.codPedido, n.tipoPedido, string.Format(@"{0:N}", n.totalPedido),
                    n.qtdItensPedido, string.Format(@"{0:N}", n.pesoPedido), string.Format(@"{0:N}", n.cubagemPedido), n.codRotaCliente + " - " + n.rotaCliente));
                }

                if (gridPedido.Rows.Count > 0)
                {
                    //Seleciona a primeira linha
                    gridPedido.CurrentCell = gridPedido.Rows[0].Cells[1];
                    //Foca no grid
                    gridPedido.Focus();

                    //Informações dos pedidos encontrados
                    lblQtdPedido.Text = "Pedido: " + gridPedido.Rows.Count.ToString();

                    //Pesquisa os cliente
                    PesqCliente(status);
                }
                else
                {
                    MessageBox.Show("Nenhum pedido encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa os cliente
        private void PesqCliente(string status)
        {
            try
            {
                //Limpa a coleção de objeto
                clienteCollection.Clear();
                //Instância o objeto
                RoteirizadorNegocios routeirizadorNegocios = new RoteirizadorNegocios();
                //Instância o objêto
                ClienteCollection clienteRotaCollection = new ClienteCollection();

                //Instância o tamanho do array
                int[] codigoRota = new int[gridRota.Rows.Count];
                //Qtd de Rota Selecionada
                int qtdRotaSelecionada = 0;

                //Verifica se existe alguma rota selacionada
                for (int i = 0; gridRota.Rows.Count > i; i++)
                {
                    if (Convert.ToBoolean(gridRota.Rows[i].Cells[0].Value) == true)
                    {
                        //Seta no array os códigos das rotas
                        codigoRota[i] = Convert.ToInt32(gridRota.Rows[i].Cells[1].Value);
                        qtdRotaSelecionada++;
                    }
                }

                if (qtdRotaSelecionada > 0)
                {
                    for (int i = 0; gridRota.Rows.Count > i; i++)
                    {
                        if (Convert.ToBoolean(gridRota.Rows[i].Cells[0].Value) == true)
                        {
                            //Pesquisa - A coleção recebe o resultado da consulta
                            clienteCollection = routeirizadorNegocios.PesqCliente(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), txtHorario.Text, Convert.ToInt32(gridRota.Rows[i].Cells[1].Value), status);

                            //Concatena o objêto encontrado
                            clienteRotaCollection.AddRange(clienteCollection);
                        }
                    }
                }
                else
                {
                    //Pesquisa
                    clienteCollection = routeirizadorNegocios.PesqCliente(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), txtHorario.Text, 0, status);
                }

                if (qtdRotaSelecionada != 0)
                {
                    //Limpa a coleção
                    clienteCollection.Clear();
                    //Concatena o objêto encontrado
                    clienteCollection.AddRange(clienteRotaCollection);
                }

                if (pedidoCollection.Count > 0 && clienteCollection.Count == 0)
                {
                    MessageBox.Show("Há algo errado, nenhum cliente encontrado para exportação!", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Exportar Arquivo de pedido formato Fusion
        private void ExportarPedidoFusion()
        {
            try
            {
                //Caminho da área de trabalho
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Verifica se a pasta wms existe
                if (Directory.Exists(desktop))
                {
                    //Cria a pasta
                    DirectoryInfo di = Directory.CreateDirectory(desktop + "\\WMS ARQUIVO");
                }

                //Verifica se a pasta Fusion existe
                if (Directory.Exists(desktop))
                {
                    //Cria a pasta
                    DirectoryInfo di = Directory.CreateDirectory(desktop + "\\WMS ARQUIVO\\Fusion");
                }

                //Nome do arquivo
                string nomeArquivo = "FUSPEDIDO" + DateTime.Now.ToString().Replace(" ", "").Replace("/", "").Replace(":", "") + ".txt";

                //Combina o caminho do arquivo com o nome
                string filePath = Path.Combine(desktop + "\\WMS ARQUIVO\\Fusion\\", nomeArquivo);
                //Chama o refresh (Limpa o arquivo)
                var arquivo = new FileInfo(filePath);

                //Cria o arquivo
                using (StreamWriter Line = arquivo.CreateText())
                {
                    //Implementação do cabeçalho no TXT.
                    //Line.WriteLine("A;B;C;D;E;F;G;H;I;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;AA;AB;AC;AD;AE;AF;AG;AH;AI;AJ;");

                    foreach (var pedido in pedidoCollection)
                    {
                        Line.WriteLine(
                            pedido.notaFiscal + ";" + //A = Número da Nota Fiscal                       
                            pedido.serieNotaFiscal + ";" + //B = Série da Nota Fiscal 
                            pedido.dataFaturamento.ToString("yyyy-MM-dd") + ";" + //C = Data de emissão ou Data prevista de Faturamento ou ainda Data Prevista de Entrega
                            pedido.dataPedido.ToString("yyyy-MM-dd") + ";" + //D = Data de Gravação/Inserção da Entrega
                            pedido.codPedido + ";" + //E = Código do Pedido/Entrega (chave pedido)
                            pedido.totalPedido + ";" + //F = Valor da Entrega em Reais (R$)
                            pedido.formaPagamento + ";" + //G = Forma de Pagamento / Tipo Pedido /Bonificação
                            pedido.pedStatus + ";" + //H = tatus da Entrega: 1-Aprovado / 2-Em Separação / 3 - Separado e Conferido / 4 - Faturado /9 - Cancelado / B - Bloqueado / 10 - Reentrega/11 - Saldo de Pedido
                            pedido.pedFormaCarga + ";" + //I = S ou N: Indicação explicita se a entrega pode entrar na formação de cargas
                            pedido.pesoPedido + ";" + //J = Peso da Entrega em KG
                            pedido.cubagemPedido + ";" + //K = Cubagem da Entrega em M3 (metros cúbicos)
                            pedido.observacaoPedido + ";" + //L = Observação da Entrega 
                            pedido.valorSubstituicaoPedido + ";" + //M = Valor da Substituição Tributária
                            pedido.codCliente + ";" + //N -> Código do Cliente (Código ERP)
                            pedido.codFilial + ";" + //O -> Código da Filial de Faturamento
                            pedido.codFilial + ";" + //P -> Código da Filial de Logística
                            pedido.codPedido + ";" + //Q -> Código alternativo do Pedido/Entrega
                            pedido.codPedido + ";" + //R ->Código alternativo do Pedido/Entrega 2
                            /*pedido.representante +*/ ";" + //S ->  Código do Vendedor
                            pedido.restricaoveiculoPedido + ";" + //T -> Código da Restrição de Transporte
                            pedido.bonificacaoEntrega + ";" + //U -> S ou N: Entrega é uma bonificação
                            pedido.codPracaCliente + ";" + //V -> Código da Praça/Setor (ID)
                            pedido.descPracaCliente + ";" + //W -> Descrição da Rota
                            pedido.codRotaCliente + ";" + //X -> Código da Rota (ID)
                            pedido.rotaCliente + ";" + //Y -> Descrição da Rota 
                            pedido.qtdItensPedido + ";" + //Z -> Quantidade de Itens do pedido
                            pedido.manifestoPedido + ";" + //AA -> Código da Carga do ERP
                            pedido.tipoServico + ";" + //AB -> Tipo Entrega/Servico
                            pedido.enderecoPadraoCliente + ";" + //AC -> Utilizar o Endereço do cadastro (S/N)
                            ";" + //AD -> Código do Endereço do ERP
                            ";" + //AE -> Endereço alternativo do cliente
                            ";" + //AF -> Número do Endereço Alternativo do Cliente
                            ";" + //AG -> Bairro do Endereço Alternativo do Cliente
                            ";" + //AH -> Cidade do Endereço Alternativo do Cliente
                            ";" + //AI -> UF do Endereço Alternativo do Cliente
                            ";"); //AJ -> CEP do Endereço Alternativo do Cliente
                    }

                    //Fecha o arquivo
                    Line.Close();

                    //Exporta os cliente
                    ExportarClienteFusion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Exportar Arquivo de cliente formato Fusion
        private void ExportarClienteFusion()
        {
            try
            {
                //Caminho da área de trabalho
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Nome do arquivo
                string nomeArquivo = "FUSCLIENTE" + DateTime.Now.ToString().Replace(" ", "").Replace("/", "").Replace(":", "") + ".txt";

                //Combina o caminho do arquivo com o nome
                string filePath = Path.Combine(desktop + "\\WMS Arquivo\\Fusion\\", nomeArquivo);
                //Chama o refresh (Limpa o arquivo)
                var arquivo = new FileInfo(filePath);

                //Cria o arquivo
                using (StreamWriter Line = arquivo.CreateText())
                {
                    //Implementação do cabeçalho no TXT.
                    //Line.WriteLine("A;B;C;D;E;F;G;H;I;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;AA;AB;AC;AD;AE;AF;");

                    foreach (var cliente in clienteCollection)
                    {
                        Line.WriteLine(
                        ";" + //A -> Forma Padrão de Pagamento
                        ";" + //B -> S ou N: Calcula Substituição Tributária de ICMS
                        ";" + //C -> Código do Cliente Principal ou Rede de Lojas
                        cliente.codCliente + ";" + //D -> Código do ERP, WMS ou TMS          
                        cliente.fantasiaCliente + ";" + //E -> Fantasia
                        cliente.nomeCliente + ";" + //F -> Razão Social
                        cliente.cnpjCliente + ";" + //G -> CNPJ ou CPF (Somente Números) 
                        cliente.enderecoCliente + ";" + //H -> Endereço de Entrega
                        cliente.bairroCliente + ";" + //I -> Bairro
                        cliente.ufCliente + ";" +//J -> Estado
                        cliente.cidadeCliente + ";" + //K -> Cidade 
                        cliente.cepCliente + ";" + //L -> CEP
                        ";" + //M -> Data de Cadastro (yyyy-mm-dd)
                        ";" + //N -> Data da Última Compra (yyyy-mm-dd)
                        cliente.foneCliente + ";" + //O -> Telefone 1
                        ";" + //P -> Telefone 2
                        cliente.celularCliente + ";" + //Q -> Celular 
                        cliente.emailCliente + ";" + //R -> Email
                        ";" + //S -> Status de Crédito
                        ";" + //T -> Não utilizado. Deixar em branco
                        cliente.codAtividadeCliente + ";" + //U -> Código do Segmento 
                        cliente.descAtividadeCliente + ";" + //V -> Descrição do Segmento  
                        cliente.codFilial + ";" + //W -> Código da Filial de Faturamento 
                        cliente.codPracaCliente + ";" + //X -> Código da Praça/Setor 
                        cliente.descPracaCliente + ";" + //Y -> Descrição da Praça/Setor 
                        cliente.codRotaCliente + ";" + //Z -> Código da Rota que a Praça pertence
                        cliente.rotaCliente + ";" + //AA -> Descrição da Rota 
                        cliente.numeroCliente + ";" + //AB -> Número do endereço 
                        ";" + //AC -> Valor comprado nos últimos 90 dias
                        ";" + //AD -> Referência para Entrega
                        cliente.latitudeCliente + ";" + //AE -> Latitude
                        cliente.longitudeCliente + ";"); //AF -> Longitude
                    }

                    //Fecha o arquivo
                    Line.Close();

                    MessageBox.Show("Arquivos exportados com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Lê o arquivo
        private void LerArquivo()
        {
            try
            {
                //Verifica o arquivo
                var lines = File.ReadAllLines(txtArquivo.Text);

                string[] coluna = null;

                gridImportar.Rows.Clear();

                foreach (var line in lines)
                {
                    coluna = line.Split(';');

                    if (!coluna[0].Equals("C"))
                    {

                        gridImportar.Rows.Add((gridImportar.Rows.Count + 1), coluna[0].Replace("E", "ENTREGA"), coluna[1], coluna[4], coluna[8].ToUpper(), coluna[2], coluna[3]);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Importar o arquivo
        private void ImportarArquivo()
        {
            try
            {
                if(gridImportar.Rows.Count == 0)
                {
                    MessageBox.Show("Importe o arquivo", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Instância o objeto
                    RoteirizadorNegocios routeirizadorNegocios = new RoteirizadorNegocios();

                    //Define um valor para o progressbar
                    progressBar.Maximum = (gridImportar.Rows.Count);

                    for (int i = 0; gridImportar.Rows.Count > i; i++)
                    {
                        routeirizadorNegocios.ImportarArquivo(
                            Convert.ToInt32(gridImportar.Rows[i].Cells[3].Value), //Código do pedido
                            Convert.ToInt32(gridImportar.Rows[i].Cells[2].Value), //Código do manifesto
                            Convert.ToInt32(gridImportar.Rows[i].Cells[5].Value)); //Sequencia

                        //Incrementa o arquivo
                        progressBar.Value++;
                    }

                    MessageBox.Show("Arquivo importado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Limpa o campo
                    txtArquivo.Clear();
                    //Zera o progressbar
                    progressBar.Value = 0;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
    }
}
