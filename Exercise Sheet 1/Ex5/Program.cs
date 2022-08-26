using System;

namespace Ex5
{
    class Program
    {
        static void Main(string[] args)
        {
            String alinea, ext;

            do
            {
                Console.Write("Introduza a alínea (a, b, c):\n > ");
                alinea = Console.ReadLine();
                Console.WriteLine();
            } while (alinea != "a" && alinea != "A" && alinea != "b" && alinea != "B" && alinea != "c" && alinea != "C");

            if (alinea == "a" || alinea == "A")
            {
                ext = A();
                Console.Write(ext);
                Console.WriteLine();
            }
            else if (alinea == "b" || alinea == "B")
            {
                ext = B();
                Console.Write(ext);
                Console.WriteLine();
            }
            else
            {
                do
                {
                    ext = C();
                    if (ext != "Sair")
                    {
                        Console.Write(ext);
                    }
                    else
                    {
                        return;
                    }
                    Console.WriteLine();
                    Console.Write("\n\nPress any key to continue...");
                    Console.ReadKey();
                } while (true);
            }

            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey();

        }

        static String A()
        {
            int num;

            do
            {
                Console.Write("Introduza um número de 1 a 10:\n > ");
                while ((int.TryParse(Console.ReadLine(), out num)) == false)
                {
                    Console.WriteLine("\nTem de ser um número! Tente novamente.\n");
                }

                if (num < 1 || num > 10)
                {
                    Console.WriteLine("\nTem de ser um número entre 1 e 10! Tente novamente.\n");
                }
                else { }
            } while (num < 1 || num > 10);

            return ConverterParaExt(num);
        }
        static String B()
        {
            int num;

            Console.Write("Introduza um número:\n > ");
            while ((int.TryParse(Console.ReadLine(), out num)) == false)
            {
                Console.WriteLine("\nTem de ser um número! Tente novamente.\n");
                Console.Write("Introduza um número:\n > ");
            }

            return ConverterParaExt(num);
        }
        static String C()
        {
            int num;

            Console.Write("Introduza um número (0 para sair):\n > ");
            while ((int.TryParse(Console.ReadLine(), out num)) == false)
            {
                Console.WriteLine("\nTem de ser um número! Tente novamente.\n");
                Console.Write("Introduza um número (0 para sair):\n > ");
            }

            if (num == 0)
            {
                return "Sair";
            }
            else
            {
                return ConverterParaExt(num);
            }
        }

        static String ConverterParaExt(int x)
        {
            switch (x)
            {
                case 1:
                    return "\nUm";
                case 2:
                    return "\nDois";
                case 3:
                    return "\nTrês";
                case 4:
                    return "\nQuatro";
                case 5:
                    return "\nCinco";
                case 6:
                    return "\nSeis";
                case 7:
                    return "\nSete";
                case 8:
                    return "\nOito";
                case 9:
                    return "\nNove";
                case 10:
                    return "\nDez";
                default:
                    return "\nNão sei";
            }
        }
    }
}
