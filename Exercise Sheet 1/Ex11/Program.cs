using System;

namespace Ex11
{
    class Program
    {
        static void Main(string[] args)
        {
            string palavra = Console.ReadLine(), pal_inv = "";

            for (int i = palavra.Length - 1; i >= 0; i--)
                pal_inv += palavra[i];
            
            Console.Write(pal_inv);

            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
