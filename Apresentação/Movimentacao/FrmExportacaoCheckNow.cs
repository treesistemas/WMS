using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmExportacaoCheckNow : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmExportacaoCheckNow()
        {
            InitializeComponent();
        }

        private void txtManifestoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (chkExportar.Checked == true)
                {
                    //Pesquisa os manifestos no grid
                    PesqManifestos();
                }
                else
                {
                    txtManifestoFinal.Focus();
                }
            }
        }

        private void txtManifestoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnExportar.Focus();
            }
        }

        private void chkExportar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExportar.Checked == true)
            {
                lblManifestoInicial.Text = "Manifesto";
                //Oculta o label manifesto final
                lblManifestoFinal.Visible = false;
                //Oculta o campo manifesto final
                txtManifestoFinal.Visible = false;
                //Habilita o grid de manifestos
                gridManifesto.Enabled = true;
                //Foca no campo
                txtManifestoInicial.Focus();
            }
            else
            {
                lblManifestoInicial.Text = "Manifesto inícial";
                //Exibe o label manifesto final
                lblManifestoFinal.Visible = true;
                //Exibe o campo manifesto final
                txtManifestoFinal.Visible = true;
                //Desabilita o grid de manifestos
                gridManifesto.Enabled = false;
                //Foca no campo
                txtManifestoInicial.Focus();
            }
        }

        private void mniRemoverManifesto_Click(object sender, EventArgs e)
        {
            //Remove manifesto
            gridManifesto.Rows.RemoveAt(gridManifesto.CurrentRow.Index);
            //Váriavel que controla a quantidade de manifesto no grid
            int count = 1;

            for (int i = 0; gridManifesto.Rows.Count > i; i++)
            {
                //Refa a contagem de manifesto
                gridManifesto.Rows[i].Cells[0].Value = count;
                //Soma mais um
                count++;
            }

            //Recebe a contagem de manifestos
            lblManifestos.Text = Convert.ToString(gridManifesto.Rows.Count);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (chkExportar.Checked == true)
            {
                //Exportar os Manifestos do grid
                ExportarManifestos();
            }
            else
            {
                //Exporta os manifestos
                ExportarSequenciaManifesto();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha TELA
            Dispose();
        }

        //Pesquisa os manifestos no grid
        private void PesqManifestos()
        {
            try
            {

                //Instância a camada de negocios
                ExportarCheckNowNegocios exportarCheckNowNegocios = new ExportarCheckNowNegocios();
                //Instância o objeto
                Manifesto manifesto = new Manifesto();

                manifesto = exportarCheckNowNegocios.PesqManifestos(Convert.ToInt32(txtManifestoInicial.Text));

                if (manifesto.codManifesto != 0)
                {
                    //Verifica se o manifesto já foi incluido

                    bool adiciona = true;

                    for (int i = 0; gridManifesto.Rows.Count > i; i++)
                    {
                        if (Convert.ToInt32(txtManifestoInicial.Text) == Convert.ToInt32(gridManifesto.Rows[i].Cells[1].Value))
                        {
                            adiciona = false;

                            MessageBox.Show("Manifesto já digitado!", "WMS - Informação", MessageBoxButtons.OK);
                            break;
                        }
                    }

                    if (adiciona == true)
                    {
                        //Adiciona o manifesto ao grid
                        gridManifesto.Rows.Add(gridManifesto.Rows.Count + 1, manifesto.codManifesto, manifesto.pedidoManifesto);
                        //Limpa o campo
                        txtManifestoInicial.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum manifesto encontrado!", "WMS - Informação", MessageBoxButtons.OK);
                }

                //Recebe a contagem de manifestos
                lblManifestos.Text = Convert.ToString(gridManifesto.Rows.Count);
                //Foca no campo 
                txtManifestoInicial.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os manifestos
        private void ExportarSequenciaManifesto()
        {
            try
            {
                //Caminho da área de trabalho
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Verifica se a pasta existe
                if (Directory.Exists(desktop))
                {
                    //Cria a pastea
                    DirectoryInfo di = Directory.CreateDirectory(desktop + "\\WMS ARQUIVO");
                }

                //Verifica se a pasta existe
                if (Directory.Exists(desktop))
                {
                    //Cria a pastea
                    DirectoryInfo di = Directory.CreateDirectory(desktop + "\\WMS ARQUIVO\\Pathfind");
                }

                //Verifica se a pasta existe
                if (Directory.Exists(desktop))
                {
                    //Cria a pastea
                    DirectoryInfo di = Directory.CreateDirectory(desktop + "\\WMS ARQUIVO\\Pathfind\\Chek Now");
                }

                //Nome do arquivo
                string nomeArquivo = "CHECKNOW" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".txt";

                //Combina o caminho do arquivo com o nome
                string filePath = Path.Combine(desktop + "\\WMS ARQUIVO\\Pathfind\\Check Now\\", nomeArquivo);
                //Chama o refresh (Limpa o arquivo)
                var arquivo = new FileInfo(filePath);

                //Instância a camada de negocios
                ExportarCheckNowNegocios exportarCheckNowNegocios = new ExportarCheckNowNegocios();
                //Instância a coleção de objeto
                ExportarCheckNowCollection checkNowCollection = new ExportarCheckNowCollection();

                int manifestoInicial = 0, manifestoFinal = 0;

                if (!txtManifestoInicial.Text.Equals(""))
                {
                    manifestoInicial = Convert.ToInt32(txtManifestoInicial.Text);
                }

                if (!txtManifestoFinal.Text.Equals(""))
                {
                    manifestoFinal = Convert.ToInt32(txtManifestoFinal.Text);
                }

                //Recebe os dados
                checkNowCollection = exportarCheckNowNegocios.PesqManifestos(manifestoInicial, manifestoFinal);

                if (checkNowCollection.Count > 0)
                {
                    double peso = 0, valor = 0;

                    //Cria o arquivo
                    using (StreamWriter Line = arquivo.CreateText())
                    {
                        //Implementação do cabeçalho no TXT.
                        Line.Write("TIPO DE ROTA;DATA MANIFESTO;PLACA;DATA INICIAL ROTA;DATA FINAL ROTA;DISTANCIA PLANEJADA;PLACA;DESCRICAO ROTA;MOTORISTA;TARA;SEQUENCIA ENTREGA;CODIGO CLIENTE;" +
                            "CODIGO CLIENTE;DESCRICAO DO CLIENTE;LATITUDE;LONGITUDE;ENDERECO;NUMERTO DO ENDERECO;COMPLEMENTO;CEP;CIDADE;BAIRRO;UF;TELEFONE;RAIO;" +
                            "DATA CHEGADA PLANEJADA;DATA SAIDA PLANEJADA;TEMPO VIAGEM;TEMPO ENTREGA;PEDIDO;PESO;VALOR;CUBAGEM;CODIGO RCA;NOME RCA;TELEFONE RCA;FORMA DE PAGAMENTO;CODIGO MOTORISTA;MANIFESTO\n");

                        foreach (ExportarCheckNow now in checkNowCollection.OrderBy(x => x.codigoManifesto))
                        {
                            Line.Write(now.regiaoRota + ";" +
                                        now.dataManifesto.ToString("yyyy-MM-dd") + ";" +
                                        now.plavaVeiculo + ";" +
                                        now.dataManifesto.ToString("yyyy-MM-dd HH:mm:ss") + ";" +//now.dataIncialConferencia + ";" +
                                        now.dataManifesto.ToString("yyyy-MM-dd HH:mm:ss") + ";" +//now.dataFinalConferencia + ";" +
                                        "00.00;" +               //now.distanciaCliente + ";" +
                                        now.plavaVeiculo.Replace("-", "") + ";" +
                                        now.descRota + ";" +
                                        now.nomeMotorista + ";" +
                                        now.capacidadeVeiculo + ";" +
                                        now.sequenciaEntrega + ";" +
                                        now.codCliente + ";" +
                                         now.codCliente + ";" +
                                        now.nomeCliente + ";" +
                                        now.latitudeCliente + ";" +
                                        now.longitudeCliente + ";" +
                                        now.enderecoCliente + ";" +
                                        now.numeroCliente + ";" +
                                        now.complementoCliente + ";" +
                                        now.cepCliente + ";" +
                                        now.cidadeCliente + ";" +
                                        now.bairroCliente + ";" +
                                        now.UFCliente + ";" +
                                        now.telefoneCliente + ";" +
                                        "50;" +                  //now.raioCliente + ";" +
                                        ";" +                   //now.dataChegaCliente + ";" +
                                        ";" +                  //now.tempoViagem + ";" +
                                        ";" +                 //now.tempoEntrega + ";" +
                                        ";" +                //now.codPedido + ";" +
                                        now.codPedido + ";" +//now.numeroNotaFiscal + ";" +
                                        now.pesoPedido + ";" +
                                        now.valorPedido + ";" +
                                        now.cubagemPedido + ";" +
                                        now.codigoRepresentante + ";" +
                                        now.nomeRepresentante + ";" +
                                        now.celularRepresentante + ";" +
                                        now.formaPagamentoPedido + ";" +
                                        now.codigoMotorista + ";" +
                                        now.codigoManifesto + ";\n");

                            //Soma o peso de todos os pedidos
                            peso += now.pesoPedido;
                            //Soma o valor de todos os pedidos
                            valor += now.valorPedido;
                        }

                        //Fecha o arquivo
                        Line.Close();

                        //Exibe os dados
                        if (checkNowCollection.Count() > 0)
                        {
                            lblManifestos.Text = Convert.ToString(checkNowCollection[0].qtdManifesto);
                        }
                        lblPedidos.Text = Convert.ToString(checkNowCollection.Count()); //Quantidade de pedidos
                        lblPeso.Text = string.Format("{0:n}", peso);
                        lblTotal.Text = string.Format("{0:c}", valor);

                        if (MessageBox.Show("Exportação realizada com sucesso! \nDeseja gerar uma nova exportação?", "WMS - Informação", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            //Limpa os campos
                            txtManifestoInicial.Clear();
                            txtManifestoFinal.Clear();
                            lblManifestos.Text = "0";
                            lblPedidos.Text = "0";
                            lblPeso.Text = "0";
                            lblTotal.Text = "0";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum manifesto encontrado!", "WMS - Informação", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Exportar os manifestos do grid
        private void ExportarManifestos()
        {
            try
            {
                //Caminho da área de trabalho
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Verifica se a pasta wms existe
                if (Directory.Exists(desktop))
                {
                    //Cria a pasta
                    DirectoryInfo di = Directory.CreateDirectory(desktop + "WMS\\");
                }

                //Verifica se a pasta Fusion existe
                if (Directory.Exists(desktop))
                {
                    //Cria a pasta
                    DirectoryInfo di = Directory.CreateDirectory(desktop + "WMS\\Pathifind");
                }

                //Nome do arquivo
                string nomeArquivo = "CHECKNOW" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".txt";

                //Combina o caminho do arquivo com o nome
                string filePath = Path.Combine(desktop + "\\WMS\\Pathifind\\", nomeArquivo);
                //Chama o refresh (Limpa o arquivo)
                var arquivo = new FileInfo(filePath);

                //Instância a camada de negocios
                ExportarCheckNowNegocios exportarCheckNowNegocios = new ExportarCheckNowNegocios();
                //Instância a coleção de objeto
                ExportarCheckNowCollection checkNowCollection = new ExportarCheckNowCollection();
                //Recebe o código do manifesto que tem mais pedido
                int manifesto = Convert.ToInt32(gridManifesto.Rows[0].Cells[1].Value), 
                //Recebe a quantidade de pedido
                pedido = Convert.ToInt32(gridManifesto.Rows[0].Cells[2].Value); 
                


                //Pesquisa os manifestos para exportação
                for (int i = 0; gridManifesto.Rows.Count > i; i++)
                {
                    //Instância a coleção de objeto
                    ExportarCheckNowCollection manifestosGrid = new ExportarCheckNowCollection();
                    //Recebe os dados
                    manifestosGrid = exportarCheckNowNegocios.PesqManifestos(Convert.ToInt32(gridManifesto.Rows[i].Cells[1].Value), 0);
                    //Adiciona a nova lista
                    checkNowCollection.AddRange(manifestosGrid);
                    
                    //Verifica se existe um manifesto com maior quantidade de pedido
                    if (Convert.ToInt32(gridManifesto.Rows[i].Cells[2].Value) > pedido)
                    {
                        //Recebe a quantidade de pedido
                        pedido = Convert.ToInt32(gridManifesto.Rows[i].Cells[2].Value);
                        //Recebe o manifesto
                        manifesto = Convert.ToInt32(gridManifesto.Rows[i].Cells[1].Value);
                    }
                }

                //Verifica se existe dados para exportar
                if (checkNowCollection.Count > 0)
                {
                    double peso = 0, valor = 0;

                    //Cria o arquivo
                    using (StreamWriter Line = arquivo.CreateText())
                    {
                        //Implementação do cabeçalho no TXT.
                        Line.Write("TIPO DE ROTA;DATA MANIFESTO;PLACA;DATA INICIAL ROTA;DATA FINAL ROTA;DISTANCIA PLANEJADA;PLACA;DESCRICAO ROTA;MOTORISTA;TARA;SEQUENCIA ENTREGA;CODIGO CLIENTE;" +
                            "CODIGO CLIENTE;DESCRICAO DO CLIENTE;LATITUDE;LONGITUDE;ENDERECO;NUMERTO DO ENDERECO;COMPLEMENTO;CEP;CIDADE;BAIRRO;UF;TELEFONE;RAIO;" +
                            "DATA CHEGADA PLANEJADA;DATA SAIDA PLANEJADA;TEMPO VIAGEM;TEMPO ENTREGA;PEDIDO;PESO;VALOR;CUBAGEM;CODIGO RCA;NOME RCA;TELEFONE RCA;FORMA DE PAGAMENTO;CODIGO MOTORISTA;MANIFESTO\n");

                        foreach (ExportarCheckNow now in checkNowCollection)
                        {
                            Line.Write(now.regiaoRota + ";" +
                                        now.dataManifesto.ToString("yyyy-MM-dd") + ";" +
                                        now.plavaVeiculo + ";" +
                                        now.dataManifesto.ToString("yyyy-MM-dd HH:mm:ss") + ";" +//now.dataIncialConferencia + ";" +
                                        now.dataManifesto.ToString("yyyy-MM-dd HH:mm:ss") + ";" +//now.dataFinalConferencia + ";" +
                                        "00.00;" +               //now.distanciaCliente + ";" +
                                        now.plavaVeiculo.Replace("-", "") + ";" +
                                        now.descRota + ";" +
                                        now.nomeMotorista + ";" +
                                        now.capacidadeVeiculo + ";" +
                                        now.sequenciaEntrega + ";" +
                                        now.codCliente + ";" +
                                         now.codCliente + ";" +
                                        now.nomeCliente + ";" +
                                        now.latitudeCliente + ";" +
                                        now.longitudeCliente + ";" +
                                        now.enderecoCliente + ";" +
                                        now.numeroCliente + ";" +
                                        now.complementoCliente + ";" +
                                        now.cepCliente + ";" +
                                        now.cidadeCliente + ";" +
                                        now.bairroCliente + ";" +
                                        now.UFCliente + ";" +
                                        now.telefoneCliente + ";" +
                                        "50;" +                  //now.raioCliente + ";" +
                                        ";" +                   //now.dataChegaCliente + ";" +
                                        ";" +                  //now.tempoViagem + ";" +
                                        ";" +                 //now.tempoEntrega + ";" +
                                        ";" +                //now.codPedido + ";" +
                                        now.codPedido + ";" +//now.numeroNotaFiscal + ";" +
                                        now.pesoPedido + ";" +
                                        now.valorPedido + ";" +
                                        now.cubagemPedido + ";" +
                                        now.codigoRepresentante + ";" +
                                        now.nomeRepresentante + ";" +
                                        now.celularRepresentante + ";" +
                                        now.formaPagamentoPedido + ";" +
                                        now.codigoMotorista + ";" +
                                        manifesto + ";\n");

                            //Soma o peso de todos os pedidos
                            peso += now.pesoPedido;
                            //Soma o valor de todos os pedidos
                            valor += now.valorPedido;
                        }

                        //Fecha o arquivo
                        Line.Close();

                        //Exibe os dados
                        if (checkNowCollection.Count() > 0)
                        {
                            lblManifestos.Text = Convert.ToString(checkNowCollection[0].qtdManifesto);
                        }

                        //Recebe a contagem de manifestos
                        lblManifestos.Text = Convert.ToString(gridManifesto.Rows.Count);

                        lblPedidos.Text = Convert.ToString(checkNowCollection.Count()); //Quantidade de pedidos
                        lblPeso.Text = string.Format("{0:n}", peso);
                        lblTotal.Text = string.Format("{0:c}", valor);

                        if (MessageBox.Show("Exportação realizada com sucesso! \nDeseja gerar uma nova exportação?", "WMS - Informação", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            //Limpa os campos
                            txtManifestoInicial.Clear();
                            txtManifestoFinal.Clear();
                            lblManifestos.Text = "0";
                            lblPedidos.Text = "0";
                            lblPeso.Text = "0";
                            lblTotal.Text = "0";
                            gridManifesto.Rows.Clear();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum manifesto encontrado!", "WMS - Informação", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
