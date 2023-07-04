using System;

namespace Ex17
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 26;
            char[] abc = new char[N] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            int[] abcindex = new int[N];
            string caracts;

            for (int i = 0; i < N; i++)
                abcindex[i] = 0;

            Console.Write("Introduza uma cadeia de caracter:\n > ");
            caracts = Console.ReadLine();

            for (int i = 0; i < caracts.Length; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (caracts[i] == abc[j])
                    {
                        abcindex[Array.IndexOf(abc, caracts[i])]++;
                    }
                }
            }

            for (int i = 0; i < N; i++)
            {
                Console.Write(String.Format("\n\t{0} : {1}", abc[i], abcindex[i]));
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
