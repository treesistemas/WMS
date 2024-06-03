using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjetoTransferencia;
using Dados;
using System.Data;
using System.Text.RegularExpressions;

namespace Negocios
{
    public class SenhaMd5Negocios
    {
        //Instância o objeto
        Conexao conexao = new Conexao();

        public SenhaMd5 PesqSenha(int idUsuario)
        {
            try
            {
                //instância o objeto
                SenhaMd5 senhaMd5 = new SenhaMd5();
                //limpa os parâmetros
                conexao.LimparParametros();
                //Adiciona os Parâmetros
                conexao.AdicionarParamentros("@idUsuario", idUsuario);
                //String de Pesquisa - Pesquisa uma nova senha
                string select = "select usu_senha, usu_senha_anterior from wms_usuario where usu_codigo = @idUsuario";

                DataTable dataTable = conexao.ExecutarConsulta(CommandType.Text, select);

                //Percorre a tabela e adiciona as linha encontrada na coleção
                foreach (DataRow linha in dataTable.Rows)
                {
                    //Adiciona os valores encontrados
                    if (linha["usu_senha"] != DBNull.Value)
                    {
                        senhaMd5.senha = Convert.ToString(linha["usu_senha"]);
                    }

                    if (linha["usu_senha_anterior"] != DBNull.Value)
                    {
                        senhaMd5.senhaAntiga = Convert.ToString(linha["usu_senha_anterior"]);
                    }

                }


                return senhaMd5;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao pesquisar a senha! \nDetalhes: " + ex.Message);
            }
        }

        public int GeraPontosSenha(string senha)
        {
            if (senha == null) return 0;
            int pontosPorTamanho = GetPontoPorTamanho(senha);
            int pontosPorMinusculas = GetPontoPorMinusculas(senha);
            int pontosPorMaiusculas = GetPontoPorMaiusculas(senha);
            int pontosPorDigitos = GetPontoPorDigitos(senha);
            int pontosPorSimbolos = GetPontoPorSimbolos(senha);
            int pontosPorRepeticao = GetPontoPorRepeticao(senha);

            // Console.WriteLine("Ponto Tamanho: "+pontosPorTamanho + " Ponto Minuscula: "+ pontosPorMinusculas + " Ponto Maiuscula: "+pontosPorMaiusculas +
            //   " Ponto Digito: "+pontosPorDigitos +" Ponto Simbolo: "+ pontosPorSimbolos +" Ponto Por Repetição: "+ pontosPorRepeticao);

            //Console.WriteLine("Senha " + senha);

            return pontosPorTamanho + pontosPorMinusculas + pontosPorMaiusculas + pontosPorDigitos + pontosPorSimbolos - pontosPorRepeticao;

            /*GetPontoPorTamanho -Seis pontos serão atribuídos para cada caractere na senha, até um máximo de sessenta pontos.
            GetPontoPorMinusculas - Cinco pontos serão concedidos se a senha inclui uma letra minúscula. Dez pontos serão atribuídos se mais de uma letra minúscula estiver presente.
            GetPontoPorMaiusculas - Cinco pontos serão concedidos se a senha incluir uma letra maiúscula. Dez pontos serão atribuídos se mais de uma letra maiúscula estiver presente.
            GetPontoPorDigitos - Cinco pontos serão concedidos se a senha incluir um dígito numérico. Dez pontos serão atribuídos se mais de um dígito numérico estiver presente.
            GetPontoPorSimbolos - Cinco pontos serão concedidos se a senha incluir qualquer caractere diferente de uma letra ou um dígito. Isto inclui símbolos e espaços em branco. Dez pontos serão concedidos se houver dois ou mais de tais caracteres.
            GetPontoPorRepeticao - Se houver caracteres repetidos na senha será atribuido 30 pontos que será subtraida da fórmula do cálculo do total dos pontos;
             */
        }

        private int GetPontoPorTamanho(string senha)
        {
            return Math.Min(10, senha.Length) * 6;
        }

        private int GetPontoPorMinusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[a-z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorMaiusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[A-Z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorDigitos(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorSimbolos(string senha)
        {
            int rawplacar = Regex.Replace(senha, "[a-zA-Z0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorRepeticao(string senha)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(\w)*.*\1");
            bool repete = regex.IsMatch(senha);
            if (repete)
            {
                return 30;
            }
            else
            {
                return 0;
            }
        }





        public string md5(string senha)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            // Converte a cadeia de entrada para um array de bytes e calcular o hash.
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(senha));


            // Cria um novo Stringbuilder para recolher os bytes
            // E criar uma string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop através de cada byte de dados de hash
            // E formato de cada um como uma string hexadecimal.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Retorna a string hexadecimal.
            return sBuilder.ToString();
        }

    }
}
