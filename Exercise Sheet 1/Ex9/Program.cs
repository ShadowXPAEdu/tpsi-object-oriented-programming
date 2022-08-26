using System;

namespace Ex9
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;

            Console.Write("Introduza um número (a):\n > ");
            while (int.TryParse(Console.ReadLine(), out a) == false)
            {
                Console.WriteLine("\nTem de ser um número! Tente novamente.\n");
                Console.Write("Introduza um número (a):\n > ");
            }
            Console.WriteLine();

            Console.Write("Introduza um número (b):\n > ");
            while (int.TryParse(Console.ReadLine(), out b) == false)
            {
                Console.WriteLine("\nTem de ser um número! Tente novamente.\n");
                Console.Write("Introduza um número (b):\n > ");
            }
            Console.WriteLine();

            TrocaValores(ref a, ref b);

            Console.Write(String.Format("Valor 1: {0}\nValor 2: {1}", a, b));

            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey();
        }
        static void TrocaValores(ref int x, ref int y)
        {
            int temp;

            temp = x;
            x = y;
            y = temp;
        }
    }
}
