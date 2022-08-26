using System;

namespace Ex10
{
    class Program
    {
        static void Main(string[] args)
        {
            string palavra = Console.ReadLine(), pal_inv = "";
            
            for (int i = palavra.Length - 1; i >= 0; i--)
                pal_inv += palavra[i];

            if (pal_inv == palavra)
                Console.Write("A palavra é um palíndromo.");
            else
                Console.Write("A palavra não é um palíndromo.");

            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
