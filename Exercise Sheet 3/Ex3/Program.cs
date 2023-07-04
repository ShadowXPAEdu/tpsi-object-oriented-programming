using System;
using System.Collections.Generic;

namespace Ex3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool numInválido;
            List<int> numeros = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                numInválido = true;
                do
                {
                    try
                    {
                        Console.Write(String.Format("Introduza o {0}º número:\n > ", (i+1)));
                        numeros.Add(int.Parse(Console.ReadLine()));
                        numInválido = false;
                    }
                    catch (FormatException erro)
                    {
                        Console.Write(String.Format("\nErro! Tente novamente\n\nMensagem: {0}\n\n", erro.Message));
                    }
                    catch (Exception erro)
                    {
                        Console.Write("\nOccureu um erro....\n" + erro + "\n\n");
                    }
                } while (numInválido);
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write(String.Format("\n{0}º número:\t{1}", (i+1), numeros[i]));
            }

            Console.Write("\n\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
