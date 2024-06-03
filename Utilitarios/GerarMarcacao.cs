using System;

namespace Utilitarios
{
    public class GerarMarcacao
    {
        //Gerar marcação para manifesto ou pedido
        public string gerar()
        {
            Random rnd = new Random();
            string valor = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                if (rnd.Next(0, 2) == 1) // Caso seja 1, sorteia letras  
                {
                    valor += Convert.ToChar(rnd.Next(65, 91));
                }
                else
                {
                    valor += Convert.ToChar(rnd.Next(48, 58));
                }
            }

            return valor;
        }
    }
}
