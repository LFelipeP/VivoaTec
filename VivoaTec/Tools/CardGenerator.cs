using System;

namespace VivoaTec.Tools
{
    public class CardGenerator
    {

         public static string CreateCard()
        {
            Random rnd = new Random();
            string cartao = "";
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    cartao = cartao + (rnd.Next(9));
                }
                cartao = cartao + " ";
            }
            cartao = cartao.TrimEnd();
            Console.WriteLine(cartao);
            return cartao;
        }
    }
}
