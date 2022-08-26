using System;

namespace Ex6
{
    class Program
    {
        enum DiasSemana
        {
            dom = 1,
            seg,
            ter,
            qua,
            qui,
            sex,
            sab,
        }

        static void Main(string[] args)
        {
            ShowDay();
            ShowNumDay();

            Console.ReadKey();
        }

        private static void ShowDay()
        {
            int num;
            Boolean test = true;

            do
            {
                Console.Write("Introduza um número:\n > ");
                while (int.TryParse(Console.ReadLine(), out num) == false)
                {
                    Console.Write("\nNão é um número! Tente novamente...");
                    Console.Write("Introduza um número:\n > ");
                }


                //Alternativa (Mostra nome do enum)
                //Enum.GetName(typeof(DiasSemana), num);
                //Console.Write(String.Format("\n\n{0}\n\n", num));

                switch (num)
                {
                    case (int)DiasSemana.dom:
                        Console.Write("\n\nDomingo.\n\n");
                        test = false;
                        break;
                    case (int)DiasSemana.seg:
                        Console.Write("\n\nSegunda-Feira.\n\n");
                        test = false;
                        break;
                    case (int)DiasSemana.ter:
                        Console.Write("\n\nTerça-Feira.\n\n");
                        test = false;
                        break;
                    case (int)DiasSemana.qua:
                        Console.Write("\n\nQuarta-Feira.\n\n");
                        test = false;
                        break;
                    case (int)DiasSemana.qui:
                        Console.Write("\n\nQuinta-Feira.\n\n");
                        test = false;
                        break;
                    case (int)DiasSemana.sex:
                        Console.Write("\n\nSexta-Feira.\n\n");
                        test = false;
                        break;
                    case (int)DiasSemana.sab:
                        Console.Write("\n\nSabado.\n\n");
                        test = false;
                        break;
                    default:
                        Console.Write("\n\nDia inválido.\n\n");
                        break;
                }
            } while (test);
        }

        private static void ShowNumDay()
        {
            String dia;
            Console.Write("\nIntroduza o nome do dia:\n > ");
            dia = Console.ReadLine();

            if (String.Compare(dia, "domingo", true) == 0)
            {
                Console.Write("\n\nNúmero do dia da Semana: {0}\n\n", (int)DiasSemana.dom);
            }
            else if (String.Compare(dia, "segunda-feira", true) == 0)
            {
                Console.Write("\n\nNúmero do dia da Semana: {0}\n\n", (int)DiasSemana.seg);
            }
            else if (String.Compare(dia, "terça-feira", true) == 0)
            {
                Console.Write("\n\nNúmero do dia da Semana: {0}\n\n", (int)DiasSemana.ter);
            }
            else if (String.Compare(dia, "quarta-feira", true) == 0)
            {
                Console.Write("\n\nNúmero do dia da Semana: {0}\n\n", (int)DiasSemana.qua);
            }
            else if (String.Compare(dia, "quinta-feira", true) == 0)
            {
                Console.Write("\n\nNúmero do dia da Semana: {0}\n\n", (int)DiasSemana.qui);
            }
            else if (String.Compare(dia, "sexta-feira", true) == 0)
            {
                Console.Write("\n\nNúmero do dia da Semana: {0}\n\n", (int)DiasSemana.sex);
            }
            else if (String.Compare(dia, "sabado", true) == 0)
            {
                Console.Write("\n\nNúmero do dia da Semana: {0}\n\n", (int)DiasSemana.sab);
            }
            else
            {
                Console.Write("\n\nDia da Semana errado!\n\n");
            }

        }
    }
}
