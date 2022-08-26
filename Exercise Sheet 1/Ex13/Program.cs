using System;

namespace Ex13
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[10];
            int test;

            for (int i = 0; i < 10; i++)
            {
                Console.Write(String.Format("Introduza o {0}º número:\n > ", i + 1));
                while (int.TryParse(Console.ReadLine(), out array[i]) == false)
                {
                    Console.Write(String.Format("\nErro! Não é um número...\n\nIntroduza o {0}º número:\n > ", i + 1));
                }
            }

            Console.Write("Introduza um número:\n > ");
            while (int.TryParse(Console.ReadLine(), out test) == false)
            {
                Console.Write("\nErro! Não é um número...\n\nIntroduza um número:\n > ");
            }

            Array.Sort(array);

            if (test > array[0] && test < array[9])
            {
                Console.Write("\n\nO número está entre os 10 primeiros números!");
            }
            else
            {
                Console.Write("\n\nO número não está entre os 10 primeiros números!");
            }

            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
