using System;
using System.Collections.Generic;

namespace Ex7
{
    class Program
    {
        public enum FAD
        {
            // Caracteres especiais funcionam... exepto o ã... okay... o.o
            Funcionário,
            Aluno,
            Direcção,
        }
        public struct Automovel
        {
            public String marca, modelo, matricula;
            public int ano;
            public FAD enumeracao;
        }

        static void Main(string[] args)
        {
            Automovel teste;
            FAD alunos = (FAD)1;
            List<Automovel> carro = new List<Automovel>();

            teste.marca = "Mazda";
            teste.modelo = "RX-7";
            teste.matricula = "03-22-IA";
            teste.ano = 2014;
            teste.enumeracao = (FAD)0;
            carro.Add(teste);

            teste.marca = "Subaru";
            teste.modelo = "Impreza";
            teste.matricula = "14-03-SS";
            teste.ano = 2022;
            teste.enumeracao = (FAD)2;
            carro.Add(teste);

            teste.marca = "Toyota";
            teste.modelo = "Sprinter Trueno";
            teste.matricula = "SS-03-14";
            teste.ano = 2022;
            teste.enumeracao = (FAD)1;
            carro.Add(teste);

            teste.marca = "Citroën";
            teste.modelo = "Saxo";
            teste.matricula = "48-49-RS";
            teste.ano = 2001;
            teste.enumeracao = (FAD)1;
            carro.Add(teste);

            for (int i = 0; i < carro.Count; i++)
            {
                if (carro[i].enumeracao == alunos) // Mostra o carro de todos os alunos...
                {
                    Console.WriteLine(carro[i].marca);
                    Console.WriteLine(carro[i].modelo);
                    Console.WriteLine(carro[i].matricula);
                    Console.WriteLine(carro[i].ano);
                    Console.WriteLine(carro[i].enumeracao);
                    Console.WriteLine("-------------------------");
                }
            }

            Console.ReadKey();
        }
    }
}
