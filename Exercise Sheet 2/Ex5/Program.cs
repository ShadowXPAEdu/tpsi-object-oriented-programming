using System;
using System.Collections.Generic;

namespace Ex5
{
    class Program
    {
        struct EuroMilhoes
        {
            public List<int> numeros, estrelas;
            public String cidade;
            public int identificador;
        }
        static void Main(string[] args)
        {
            EuroMilhoes EM = new EuroMilhoes
            {
                numeros = new List<int>(),
                estrelas = new List<int>()
            };

            List<int> numSol = new List<int>() { 4, 17, 24, 27, 31 };
            List<int> estSol = new List<int>() { 10, 11 };

            Boolean ynT = true;
            String yn;

            ReceberAposta(ref EM);

            OrdenaAposta(EM);

            do
            {
                ImprimeAposta(EM);
                Console.Write("\n\nDeseja alterar algum número? (y/n)\n");
                yn = Console.ReadLine();

                switch (yn)
                {
                    case "y":
                    case "Y":
                        AlteraNumero(EM);
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

            VerificaAposta(EM, numSol, estSol);

            Console.ReadKey();


        }

        private static void ReceberAposta(ref EuroMilhoes EM)
        {

            AlterarNum(EM);
            AlterarEst(EM);
            Console.Write("\nIntroduza a cidade:\n > ");
            EM.cidade = Console.ReadLine();
            Console.Write("\nIntroduza o identificador:\n > ");
            while (int.TryParse(Console.ReadLine(), out EM.identificador) == false)
            {
                Console.Write("\nNão é um número!\n\n");
                Console.Write("\nIntroduza o identificador:\n > ");
            }
        }

        private static void AlterarNum(EuroMilhoes EM)
        {
            int test;
            while (EM.numeros.Count < 5)
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
                        if (EM.numeros.Contains(test))
                        {
                            Console.Write("\nNúmero já existe!\n\n");
                        }
                        else
                        {
                            EM.numeros.Add(test);
                            break;
                        }
                    }
                } while (true);
            }
        }

        private static void AlterarEst(EuroMilhoes EM)
        {
            int test;
            while (EM.estrelas.Count < 2)
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
                        if (EM.estrelas.Contains(test))
                        {
                            Console.Write("\nEstrela já existe!\n\n");
                        }
                        else
                        {
                            EM.estrelas.Add(test);
                            break;
                        }
                    }
                } while (true);
            }
        }

        private static void OrdenaAposta(EuroMilhoes EM)
        {
            EM.numeros.Sort();
            EM.estrelas.Sort();
        }

        private static void ImprimeAposta(EuroMilhoes EM)
        {
            Console.Write("\n\nNúmeros: ");
            for (int i = 0; i < EM.numeros.Count; i++)
            {
                Console.Write(String.Format("\t{0}", EM.numeros[i]));
            }

            Console.Write("\n\nEstrelas: ");
            for (int i = 0; i < EM.estrelas.Count; i++)
            {
                Console.Write(String.Format("\t{0}", EM.estrelas[i]));
            }
        }

        private static void AlteraNumero(EuroMilhoes EM)
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

                        if (EM.numeros.Contains(testNum))
                        {
                            do
                            {
                                Console.Write("\n\nIntroduza o número para alterar:\n > ");
                                while (int.TryParse(Console.ReadLine(), out newNum) == false)
                                {
                                    Console.Write("\nNão é um número!\n\n");
                                    Console.Write("\n\nIntroduza o número para alterar:\n > ");
                                }

                                if (EM.numeros.Contains(newNum))
                                {
                                    Console.Write("\nImpossível alterar para esse valor tente novamente!\n\n");
                                }
                                else
                                {
                                    EM.numeros.Remove(testNum);
                                    EM.numeros.Add(newNum);
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

                        if (EM.estrelas.Contains(testNum))
                        {
                            do
                            {
                                Console.Write("\n\nIntroduza a estrela para alterar:\n > ");
                                while (int.TryParse(Console.ReadLine(), out newNum) == false)
                                {
                                    Console.Write("\nNão é um número!\n\n");
                                    Console.Write("\n\nIntroduza a estrela para alterar:\n > ");
                                }

                                if (EM.estrelas.Contains(newNum))
                                {
                                    Console.Write("\nImpossível alterar para esse valor tente novamente!\n\n");
                                }
                                else
                                {
                                    EM.estrelas.Remove(testNum);
                                    EM.estrelas.Add(newNum);
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

        private static void VerificaAposta(EuroMilhoes EM, List<int> nSol, List<int> eSol)
        {
            Boolean aposta = true;

            if (EM.numeros.Count < 5 || EM.estrelas.Count < 2)
            {
                aposta = false;
            }

            foreach (int i in EM.numeros)
            {
                if (!nSol.Contains(i))
                {
                    aposta = false;
                    break;
                }
            }

            foreach (int i in EM.estrelas)
            {
                if (!eSol.Contains(i))
                {
                    aposta = false;
                    break;
                }
            }

            if (aposta)
            {
                Console.Write(String.Format("\n\nParabéns ganhou!\nCidade: {0}\nIdentificador: {1}", EM.cidade, EM.identificador));
            }
            else
            {
                Console.Write("\n\nPerdeu...");
            }
        }
    }
}
