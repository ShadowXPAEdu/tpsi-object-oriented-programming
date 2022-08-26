using System;
using System.Collections.Generic;

namespace Ex4
{
    class Program
    {
        static void Main(string[] args)
        {
            // String[] matriculas = new String[0];
            List<String> matriculas = new List<String>();
            Boolean loop = true;
            int num;

            do
            {
                Console.Write("Introduza um número:\n[1] - Adicionar Viatura\n[2] - Verificar se está no campus\n[3] - Apagar viatura\n[4] - Mostrar número de viaturas no campus\n[0] - Sair\n\n > ");
                while (int.TryParse(Console.ReadLine(), out num) == false)
                {
                    Console.Write("\nTem de ser um número!\n");
                    Console.Write("Introduza um número:\n[1] - Adicionar Viatura\n[2] - Verificar se está no campus\n[3] - Apagar viatura\n[4] - Mostrar número de viaturas no campus\n[0] - Sair\n\n > ");
                }

                switch (num)
                {
                    case 1:
                        // Adicionar Viaturas
                        if (matriculas.Count < 100)
                        {
                            //Array.Resize(ref matriculas, matriculas.Length + 1);
                            AddCar(matriculas);
                        }
                        else
                        {
                            Console.Write("\nOcupação máxima. Não é possivel adicionar mais viaturas!\n\n");
                        }
                        break;
                    case 2:
                        // Comparar Viaturas
                        CompareCar(matriculas);
                        break;
                    case 3:
                        // Apagar Viaturas
                        DeleteCar(matriculas);
                        break;
                    case 4:
                        ShowNumCar(matriculas);
                        break;
                    case 0:
                        loop = false;
                        break;
                    default:
                        Console.Write("\nNúmero Errado\n\n");
                        break;
                }

            } while (loop);

            Console.Write("\n\n\n\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void ShowNumCar(List<String> x)
        {
            Console.Write(String.Format("\nNúmero de viaturas:\n{0}\n\n", x.Count));
        }

        private static void AddCar(List<String> x)
        {
            Console.Write("\nIntroduza a matricula adicionar:\n > ");
            String comp = Console.ReadLine();
            if (x.Contains(comp))
            {
                Console.Write("\nA viatura já se encontra no campus!\n\n");
            }
            else
            {
                x.Add(comp);
            }
        }

        private static void CompareCar(List<String> x)
        {
            Console.Write("\nIntroduza uma matricula para verificar:\n > ");
            String comp = Console.ReadLine();

            if (x.Contains(comp))
            {
                Console.Write("\nA viatura está no campus!\n\n");
            }
            else
            {
                Console.Write("\nA viatura não está no campus!\n\n");
            }
        }

        private static void DeleteCar(List<String> x)
        {
            Console.Write("\nIntroduza a matricula para apagar:\n > ");
            String comp = Console.ReadLine();
            if (x.Contains(comp))
            {
                x.Remove(comp);
                Console.Write("\nA viatura foi removida!\n\n");
            }
            else
            {
                Console.Write("\nA viatura não está no campus!\n\n");
            }
        }
    }
}
