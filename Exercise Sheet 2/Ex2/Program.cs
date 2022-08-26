using System;

namespace Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] matriculas = new String[0];
            bool apagar = false, loop = true;
            int num;

            do
            {
                Console.Write("Introduza um número:\n[1] - Adicionar Viatura\n[2] - Verificar se está no campus\n[3] - Apagar viatura\n[0] - Sair\n\n > ");
                while (int.TryParse(Console.ReadLine(), out num) == false)
                {
                    Console.Write("\nTem de ser um número!\n");
                    Console.Write("Introduza um número:\n[1] - Adicionar Viatura\n[2] - Verificar se está no campus\n[3] - Apagar viatura\n[0] - Sair\n\n > ");
                }

                switch (num)
                {
                    case 1:
                        // Adicionar Viaturas
                        if (matriculas.Length < 100)
                        {
                            Array.Resize(ref matriculas, matriculas.Length + 1);
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
                        apagar = DeleteCar(matriculas);
                        if (apagar)
                        {
                            Array.Resize(ref matriculas, matriculas.Length - 1);
                        }
                        break;
                    case 0:
                        loop = false;
                        break;
                    default:
                        Console.Write("\nNúmero Errado\n\n");
                        break;
                }

            } while (loop);

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private static void AddCar(String[] x)
        {
            Console.Write("Introduza a matricula adicionar:\n > ");
            String comp = Console.ReadLine();
            for (int i = 0; i < x.Length; i++)
            {
                if (comp == x[i])
                {
                    Console.Write("\nA viatura já se encontra no campus!\n\n");
                    break;
                }

                if (i == x.Length - 1)
                {
                    x[i] = comp;
                    break;
                }
            }
        }

        private static void CompareCar(String[] x)
        {
            Console.Write("Introduza uma matricula para verificar:\n > ");
            String comp = Console.ReadLine();

            for (int i = 0; i < x.Length; i++)
            {
                if (comp == x[i])
                {
                    Console.Write("\nA viatura está no campus!\n\n");
                    break;
                }

                if (i == x.Length - 1)
                {
                    Console.Write("\nA viatura não está no campus!\n\n");
                    break;
                }
            }
        }

        private static bool DeleteCar(String[] x)
        {
            Console.Write("Introduza a matricula para apagar:\n > ");
            String comp = Console.ReadLine();
            for (int i = 0; i < x.Length; i++)
            {
                if (comp == x[i])
                {
                    x[i] = null;
                    Array.Sort(x);
                    Array.Reverse(x);
                    return true;
                }
            }
            return false;
        }
    }
}
