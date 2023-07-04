using System;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            Ponto[] pontos = new Ponto[2];
            pontos[0] = new Ponto(0, 0);
            pontos[1] = new Ponto(5, 5);
            Figura rect = new Retangulo(pontos[0], pontos[1]);
            Figura tri = new Triangulo(pontos[0], new Ponto(5, 0), new Ponto(0, 5));
            for (int i = 0; i < rect.GetNumVertices(); i++)
            {
                Console.WriteLine("Posição [" + (i + 1) + "]: " + rect.GetPosPonto(i));
            }
            Console.WriteLine("\nPerímetro: " + rect.GetPerimetro());
            Console.WriteLine("Área: " + rect.GetArea());
            Console.WriteLine();
            for (int i = 0; i < tri.GetNumVertices(); i++)
            {
                Console.WriteLine("Posição [" + (i + 1) + "]: " + tri.GetPosPonto(i));
            }
            Console.WriteLine("\nPerímetro: " + tri.GetPerimetro());
            Console.WriteLine("Área: " + tri.GetArea());
            Console.ReadKey(true);
        }
    }
}
