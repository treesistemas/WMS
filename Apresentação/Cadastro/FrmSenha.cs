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

namespace Wms
{
    public partial class FrmSenha : Form
    {
        //Perfíl do usuário
        public int idUsuario;
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmSenha()
        {
            InitializeComponent();

        }

        private void lnkInformacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Para alterar a senha, você precisa digitar \nletras maiúsculas, minúsculas, números \ne caracteres especiais. ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtNovaSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                //Foca no campo confirmar senha
                txtConfirmarSenha.Focus();
            }
        }

        private void txtConfirmarSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão salvar
                btnSalvar.Focus();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {           
                //Altera senha
                Alterar();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o form
            Close();
        }

        private void Alterar()
        {
            try
            {
                if (txtNovaSenha.Text.Length == 0)
                {
                    MessageBox.Show("Digite uma nova senha! ", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                else if (txtNovaSenha.Text.Equals(txtConfirmarSenha.Text))
                {
                    //Instância o objeto
                    SenhaMd5Negocios senhaMd5Negocios = new SenhaMd5Negocios();
                    //Instância o objeto
                    SenhaMd5 senhaMd5 = new SenhaMd5();

                    senhaMd5 = senhaMd5Negocios.PesqSenha(FrmMenu.idUsuario);

                    string senha = senhaMd5Negocios.md5(txtNovaSenha.Text);

                    if (senha.Equals(senhaMd5.senha))
                    {
                        MessageBox.Show("Senha digita não pode ser igual a senha atual! ", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LimpaCampos();
                    }
                    else if (senha.Equals(senhaMd5.senhaAntiga))
                    {
                        MessageBox.Show("Senha digita não pode ser igual a senha anterior! ", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LimpaCampos();
                    }
                    else
                    {
                        int pontuacao = senhaMd5Negocios.GeraPontosSenha(txtNovaSenha.Text);

                        if (pontuacao < 50)
                        {
                            MessageBox.Show("Senha inaceitável! ", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LimpaCampos();
                        }
                        else if (pontuacao < 60)
                        {
                            MessageBox.Show("Senha Fraca! ", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LimpaCampos();
                        }
                        else 
                        {
                            //MessageBox.Show("SENHA: "+senha);

                            //Instância o objeto
                            UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                            //Salva a nova senha
                            
                            usuarioNegocios.ResetaSenha(idUsuario, senha);
                            MessageBox.Show("Senha alterada com sucesso! ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Senha digitada incorreta! ", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpaCampos();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao alterar a senha. \nDetalhes: "+ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpaCampos()
        {
            //Limpa o campo nova senha
            txtNovaSenha.Clear();
            //Limpa o campo confirmar senha
            txtConfirmarSenha.Clear();

            //Foca no campo nova senha
            txtNovaSenha.Focus();


        }
    }
}
