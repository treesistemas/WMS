using Negocios;
using ObjetoTransferencia;
using System;
using System.Windows.Forms;


namespace Wms
{
    public partial class FrmLogin : Form
    {
        int cont = 0; //Conta as tentativas de entrada no sistema
        public string versao;
        public string empresa;
        public bool multempresa;

        //Instância o objêto
        EmpresaCollection empresaCollection = new EmpresaCollection();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //Pesquisa a empresa
            PesqEmpresa();
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Encerra a aplicação
            Application.ExitThread();
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //Foca no campo senha
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtSenha.Text.Equals(""))
                {
                    //Foca no botão acessar
                    btnAcessar.Focus();
                }
                else
                {
                    PesqUsuario(txtLogin.Text, txtSenha.Text);
                }

            }
        }

        private void btnAcessar_Click(object sender, EventArgs e)
        {
            PesqUsuario(txtLogin.Text, txtSenha.Text);
        }

        private void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbEmpresa.Text.Equals(string.Empty))
            {
                empresa = cmbEmpresa.Text;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtLogin.Clear(); //Seleciona o campo login
            txtSenha.Clear(); // Limpa o campo senha
            txtLogin.Focus(); //Foca o campo login
        }

        private void PesqEmpresa()
        {
            try
            {
                //Instância a camada de negocios
                LoginNegocios loginNegocios = new LoginNegocios();
                
                //Recebe a pesquisa
                empresaCollection = loginNegocios.PesqEmpresa();

                if (empresaCollection.Count > 0)
                {
                    //Preenche o combobox região
                    empresaCollection.ForEach(n => cmbEmpresa.Items.Add(n.siglaEmpresa));
                    //Seleciona a primeira empresa
                    cmbEmpresa.SelectedIndex = 0;
                    //Recebe a informação de multempresa
                    multempresa = empresaCollection[0].multiEmpresa;

                    //Verifica se existe mais de uma empresa
                    if (empresaCollection[0].multiEmpresa == false)
                    {
                        cmbEmpresa.Enabled = false;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
       
        private void PesqUsuario(string login, string senha)
        {
            try
            {
                //Hash na senha do usuário
                SenhaMd5Negocios convert = new SenhaMd5Negocios();
                //Hash
                string senhaMd5 = convert.md5(senha); ;

                //Instância a camada de negocios
                LoginNegocios loginNegocios = new LoginNegocios();
                //Instância o objêto
                Login log = new Login();
                //Recebe o id
                log = loginNegocios.PesqLogin(login, senhaMd5, empresa, multempresa);

                
                if (log.codUsuario > 0)
                {
                    //Fecha o frame login
                    this.Hide();
                    //Instância o frame
                    FrmMenu frame = new FrmMenu();
                    //Passa os valores
                    frame.nomeEmpresa = log.nomeEmpresa; //Nome da Empresa
                    frame.versao = versao;
                    frame.impressora = log.impressora; //Nome da Empresa
                    frame.codUsuario = log.codUsuario; //Código do Usuário
                    frame.loginUsuario = log.loginUsuario; //Login do Usuário 
                    frame.codEstacao = log.codEstacao;
                    frame.nivelEstacao = log.nivelEstacao;
                    frame.perfil = log.descPerfil; //Perfíl do Usuario
                    frame.controlaSequenciaCarregamento = log.controlaSequenciaCarregamento;
                    frame.vetorImagens = log.foto;
                    //Pesquisa os acessos
                    frame.controleAcesso = loginNegocios.PesqAcessos(log.codUsuario);
                    //Pesquisa empresas
                    frame.empresaCollection = this.empresaCollection;
                    //Exibe o frame
                    frame.Show();
                }
                else
                {
                    if (cont == 2)
                    {
                        MessageBox.Show("Desculpa, tente novamente!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //Terceira tentativa fecha o sistema
                        Application.ExitThread();
                    }
                    else
                    {
                        txtLogin.Clear(); //Seleciona o campo login
                        txtSenha.Clear(); // Limpa o campo senha
                        txtLogin.Focus(); //Foca o campo login

                        MessageBox.Show("Login ou senha incorreta!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    cont++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        
    }
}
