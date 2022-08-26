using System;

namespace Ex7
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            bool flag = false;

            do
            {
                Console.Write("Introduza um número de 1 a 1000:\n > ");
                while (int.TryParse(Console.ReadLine(), out num) == false)
                {
                    Console.WriteLine("\nTem de ser um número! Tente novamente.\n");
                    Console.Write("Introduza um número de 1 a 1000:\n > ");
                }
                Console.WriteLine();
            } while (num < 1 || num > 1000);

            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                Console.Write(String.Format("O número {0} é primo", num));
            }
            else
            {
                Console.Write(String.Format("O número {0} não é primo", num));
            }

            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
