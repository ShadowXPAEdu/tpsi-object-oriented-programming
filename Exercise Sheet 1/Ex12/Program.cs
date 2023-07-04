using System;

namespace Ex12
{
    class Program
    {
        static void Main(string[] args)
        {
            ApresentarTabuada();

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private static void ApresentarTabuada()
        {
            Console.WriteLine("----------------------------------------------------");
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Console.WriteLine(String.Format("\t{0}\tx\t{1}\t=\t{2}", i, j, (i * j)));
                }
                Console.WriteLine("----------------------------------------------------");
            }
        }
    }
}
