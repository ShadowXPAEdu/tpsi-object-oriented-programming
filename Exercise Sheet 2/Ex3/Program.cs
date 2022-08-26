using System;
using System.Collections.Generic;

namespace Ex3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numeros = new List<int>();
            List<int> estrelas = new List<int>();
            List<int> numSol = new List<int>() { 4, 17, 24, 27, 31 };
            List<int> estSol = new List<int>() { 10, 11 };
            Boolean ynT = true;
            String yn;

            ReceberAposta(numeros, estrelas);

            OrdenaAposta(numeros, estrelas);

            do
            {
                ImprimeAposta(numeros, estrelas);
                Console.Write("\n\nDeseja alterar algum número? (y/n)\n");
                yn = Console.ReadLine();

                switch (yn)
                {
                    case "y":
                    case "Y":
                        AlteraNumero(numeros, estrelas);
                        break;
                    case "n":
                    case "N":
                        ynT = false;
                        break;
                    default:
                        Console.Write("\nLetra inválida\n");
                        break;
                }

            } while (ynT);

            VerificaAposta(numeros, estrelas, numSol, estSol);

            Console.ReadKey();

        }

        private static void ReceberAposta(List<int> num, List<int> est)
        {

            AlterarNum(num);
            AlterarEst(est);

        }

        private static void AlterarNum(List<int> num)
        {
            int test;
            while (num.Count < 5)
            {
                do
                {
                    Console.Write("\nIntroduza um número:\n > ");
                    while (int.TryParse(Console.ReadLine(), out test) == false)
                    {
                        Console.Write("\nNão é um número!\n\n");
                        Console.Write("\nIntroduza um número:\n > ");
                    }

                    if (test < 1 || test > 50)
                    {
                        Console.Write("\nNúmero fora dos limites! [1 ~ 50]\n\n");
                    }
                    else
                    {
                        if (num.Contains(test))
                        {
                            Console.Write("\nNúmero já existe!\n\n");
                        }
                        else
                        {
                            num.Add(test);
                            break;
                        }
                    }
                } while (true);
            }
        }

        private static void AlterarEst(List<int> est)
        {
            int test;
            while (est.Count < 2)
            {
                do
                {
                    Console.Write("\nIntroduza uma estrela:\n > ");
                    while (int.TryParse(Console.ReadLine(), out test) == false)
                    {
                        Console.Write("\nNão é um número!\n\n");
                        Console.Write("\nIntroduza uma estrela:\n > ");
                    }

                    if (test < 1 || test > 12)
                    {
                        Console.Write("\nEstrela fora dos limites! [1 ~ 12]\n\n");
                    }
                    else
                    {
                        if (est.Contains(test))
                        {
                            Console.Write("\nEstrela já existe!\n\n");
                        }
                        else
                        {
                            est.Add(test);
                            break;
                        }
                    }
                } while (true);
            }
        }

        private static void OrdenaAposta(List<int> num, List<int> est)
        {
            num.Sort();
            est.Sort();
        }

        private static void ImprimeAposta(List<int> num, List<int> est)
        {
            Console.Write("\n\nNúmeros: ");
            for (int i = 0; i < num.Count; i++)
            {
                Console.Write(String.Format("\t{0}", num[i]));
            }

            Console.Write("\n\nEstrelas: ");
            for (int i = 0; i < est.Count; i++)
            {
                Console.Write(String.Format("\t{0}", est[i]));
            }
        }

        private static void AlteraNumero(List<int> num, List<int> est)
        {
            int test, testNum, newNum;
            bool alterando = true;
            do
            {
                Console.Write("\n\n[1] - Alterar Número\n[2] - Alterar Estrela:\n > ");
                while (int.TryParse(Console.ReadLine(), out test) == false)
                {
                    Console.Write("\nNão é um número!\n\n");
                    Console.Write("\n\n[1] - Alterar Número\n[2] - Alterar Estrela:\n > ");
                }

                switch (test)
                {
                    case 1:
                        Console.Write("\n\nQual número pretende alterar:\n > ");
                        while (int.TryParse(Console.ReadLine(), out testNum) == false)
                        {
                            Console.Write("\nNão é um número!\n\n");
                            Console.Write("\n\nQual número pretende alterar:\n > ");
                        }

                        if (num.Contains(testNum))
                        {
                            do
                            {
                                Console.Write("\n\nIntroduza o número para alterar:\n > ");
                                while (int.TryParse(Console.ReadLine(), out newNum) == false)
                                {
                                    Console.Write("\nNão é um número!\n\n");
                                    Console.Write("\n\nIntroduza o número para alterar:\n > ");
                                }

                                if (num.Contains(newNum))
                                {
                                    Console.Write("\nImpossível alterar para esse valor tente novamente!\n\n");
                                }
                                else
                                {
                                    num.Remove(testNum);
                                    num.Add(newNum);
                                    Console.Write("\nNúmero alterado!\n\n");
                                    alterando = false;
                                }
                            } while (newNum < 1 || newNum > 50);
                        }
                        else
                        {
                            Console.Write("\nNúmero inexistente!\n\n");
                        }
                        break;
                    case 2:
                        Console.Write("\n\nQual estrela pretende alterar:\n > ");
                        while (int.TryParse(Console.ReadLine(), out testNum) == false)
                        {
                            Console.Write("\nNão é um número!\n\n");
                            Console.Write("\n\nQual estrela pretende alterar:\n > ");
                        }

                        if (est.Contains(testNum))
                        {
                            do
                            {
                                Console.Write("\n\nIntroduza a estrela para alterar:\n > ");
                                while (int.TryParse(Console.ReadLine(), out newNum) == false)
                                {
                                    Console.Write("\nNão é um número!\n\n");
                                    Console.Write("\n\nIntroduza a estrela para alterar:\n > ");
                                }

                                if (est.Contains(newNum))
                                {
                                    Console.Write("\nImpossível alterar para esse valor tente novamente!\n\n");
                                }
                                else
                                {
                                    est.Remove(testNum);
                                    est.Add(newNum);
                                    Console.Write("\nEstrela alterada!\n\n");
                                    alterando = false;
                                }
                            } while (newNum < 1 || newNum > 12);
                        }
                        else
                        {
                            Console.Write("\nEstrela inexistente!\n\n");
                        }
                        break;
                    default:
                        Console.Write("\nOpção inválida!\n\n");
                        break;
                }
            } while (alterando);
        }

        private static void VerificaAposta(List<int> num, List<int> est, List<int> nSol, List<int> eSol)
        {
            Boolean aposta = true;

            if (num.Count < 5 || est.Count < 2)
            {
                aposta = false;
            }

            foreach (int i in num)
            {
                if (!nSol.Contains(i))
                {
                    aposta = false;
                    break;
                }
            }

            foreach (int i in est)
            {
                if (!eSol.Contains(i))
                {
                    aposta = false;
                    break;
                }
            }

            if (aposta)
            {
                Console.Write("\n\nParabéns ganhou!");
            }
            else
            {
                Console.Write("\n\nPerdeu...");
            }
        }
    }
}
