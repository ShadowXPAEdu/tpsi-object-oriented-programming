using System;

namespace Ex3
{
    class Program
    {
        static void Main(string[] args)
        {
            String nome;
            int idade = -1;

            Console.Write("Introduza o Nome:\n > ");
            nome = Console.ReadLine();
            Console.Write("Introduza a Idade:\n > ");

            do
            {
                while ((int.TryParse(Console.ReadLine(), out idade)) == false)
                {
                    Console.WriteLine("Tem de ser um número. Tente novamente!");
                    Console.Write("Introduza a Idade:\n > ");
                }

                if (idade < 0)
                {
                    Console.WriteLine("Idade inválida. Tente novamente!");
                    Console.Write("Introduza a Idade:\n > ");
                }
                else if (idade < 10)
                {
                    Console.Write("\n\n------------------------------------------------------------------\nNome: " + nome + "\nIdade: " + idade + "\n\nOlá menino!\n");
                }
                else if (idade < 21)
                {
                    Console.Write("\n\n------------------------------------------------------------------\nNome: " + nome + "\nIdade: " + idade + "\n\nOlá!\n");
                }
                else
                {
                    Console.Write("\n\n------------------------------------------------------------------\nNome: " + nome + "\nIdade: " + idade + "\n\nOlá Sr.(a)!\n");
                }
            } while (idade < 0);

            Console.ReadKey();
        }
    }
}
