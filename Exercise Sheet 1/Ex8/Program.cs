using System;

namespace Ex8
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b, x = 0;

            Console.Write("Introduza um número (a):\n > ");
            while (int.TryParse(Console.ReadLine(), out a) == false)
            {
                Console.WriteLine("\nTem de ser um número! Tente novamente.\n");
                Console.Write("Introduza um número (a):\n > ");
            }
            Console.WriteLine();

            do
            {
                Console.Write("Introduza um número (b) maior que (a):\n > ");
                while (int.TryParse(Console.ReadLine(), out b) == false)
                {
                    Console.WriteLine("\nTem de ser um número! Tente novamente.\n");
                    Console.Write("Introduza um número (b) maior que (a):\n > ");
                }
                Console.WriteLine();

                if (a > b)
                {
                    Console.Write(String.Format("O número (a = {0}) tem de ser menor que (b = {1})!\n\n", a, b));
                }
            } while (a > b);

            Console.Write(String.Format("Números primos: "));

            for (int i = a; i <= b; i++)
            {
                for (int j = 1; j <= i; j++)
                    if (i % j == 0)
                        x++;

                if (x == 2)
                    Console.Write(String.Format("{0}, ", i));
                x = 0;
            }

            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
