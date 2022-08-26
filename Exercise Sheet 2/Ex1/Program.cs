using System;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numeros = new int[5];
            int[] estrelas = new int[2];
            int[] numSol = { 3, 9, 14, 22, 49 };
            int[] estSol = { 5, 10 };
            bool aposta;

            ReceberAposta(numeros, estrelas);
            aposta = VerificaAposta(numeros, estrelas, numSol, estSol);

            for (int i = 0; i < 5; i++)
            {
                Console.Write(String.Format("{0}, ", numeros[i]));
            }
            for (int i = 0; i < 2; i++)
            {
                Console.Write(String.Format("{0}, ", estrelas[i]));
            }

            if (aposta)
            {
                Console.Write(String.Format("Venceu!"));
            }
            else
            {
                Console.Write(String.Format("Perdeu!"));
            }

            Console.ReadKey();
        }

        private static void ReceberAposta(int[] num, int[] est)
        {
            for (int i = 0; i < num.Length;)
            {
                if (i == 0)
                {
                    do
                    {
                        Console.Write(String.Format("Introduza o {0}º número:\n > ", i + 1));
                        while (int.TryParse(Console.ReadLine(), out num[i]) == false)
                        {
                            Console.Write(String.Format("\nTem de ser um número!\n"));
                            Console.Write(String.Format("Introduza o {0}º número:\n > ", i + 1));
                        }
                    } while (num[i] < 1 || num[i] > 45);

                    i++;
                }
                else
                {
                    do
                    {
                        Console.Write(String.Format("Introduza o {0}º número:\n > ", i + 1));
                        while (int.TryParse(Console.ReadLine(), out num[i]) == false)
                        {
                            Console.Write(String.Format("\nTem de ser um número!\n\n"));
                            Console.Write(String.Format("Introduza o {0}º número:\n > ", i + 1));
                        }

                        if (num[i] <= num[i - 1])
                        {
                            Console.Write(String.Format("\nNúmero tem de ser maior que o anterior!\n\n"));
                        }
                    } while (num[i] <= num[i - 1] || num[i] > 45 + i);

                    i++;
                }
            }

            for (int i = 0; i < est.Length;)
            {
                if (i == 0)
                {
                    do
                    {
                        Console.Write(String.Format("Introduza a {0}º estrela:\n > ", i + 1));
                        while (int.TryParse(Console.ReadLine(), out est[i]) == false)
                        {
                            Console.Write(String.Format("\nTem de ser um número!\n\n"));
                            Console.Write(String.Format("Introduza a {0}º estrela:\n > ", i + 1));
                        }
                    } while (est[i] < 1 || est[i] > 11);

                    i++;
                }
                else
                {
                    do
                    {
                        Console.Write(String.Format("Introduza a {0}º estrela:\n > ", i + 1));
                        while (int.TryParse(Console.ReadLine(), out est[i]) == false)
                        {
                            Console.Write(String.Format("\nTem de ser um número!\n"));
                            Console.Write(String.Format("Introduza a {0}º estrela:\n > ", i + 1));
                        }

                        if (est[i] <= est[i - 1])
                        {
                            Console.Write(String.Format("\nNúmero tem de ser maior que o anterior!\n\n"));
                        }
                    } while (est[i] <= est[i - 1] || est[i] > 12);

                    i++;
                }
            }
        }

        private static bool VerificaAposta(int[] num, int[] est, int[] nSol, int[] eSol)
        {
            for (int i = 0; i < num.Length; i++)
                if (Array.IndexOf(nSol, num[i]) < 0)
                    return false;

            for (int i = 0; i < est.Length; i++)
                if (Array.IndexOf(eSol, est[i]) < 0)
                    return false;

            return true;
        }
    }
}
