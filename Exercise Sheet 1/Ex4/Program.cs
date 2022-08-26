using System;

namespace Ex4
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             *
             * x = (-b +- sqrt(b^2 - 4*a*c)) / (2a)
             * 
             */

            int a, b, c;
            double delta, x1, x2;

            Console.Write("Introduza um número (a):\n > ");
            while ((int.TryParse(Console.ReadLine(), out a)) == false)
            {
                Console.WriteLine("Número inválido. Tente novamente!");
                Console.Write("Introduza um número (a):\n > ");
            }
            Console.Write("Introduza um número (b):\n > ");
            while ((int.TryParse(Console.ReadLine(), out b)) == false)
            {
                Console.WriteLine("Número inválido. Tente novamente!");
                Console.Write("Introduza um número (b):\n > ");
            }
            Console.Write("Introduza um número (c):\n > ");
            while ((int.TryParse(Console.ReadLine(), out c)) == false)
            {
                Console.WriteLine("Número inválido. Tente novamente!");
                Console.Write("Introduza um número (c):\n > ");
            }

            delta = (Math.Pow(b, 2)) - (4 * a * c);
            x1 = (-b + Math.Sqrt(delta)) / (2 * a);
            x2 = (-b - Math.Sqrt(delta)) / (2 * a);
            if (delta < 0)
            {
                Console.Write("\n\n------------------------------------------------\nA = " + a + "\nB = " + b + "\nC = " + c + "\n\nNão existem raízes!\n");
            }
            else if (delta == 0)
            {
                Console.Write("\n\n------------------------------------------------\nA = " + a + "\nB = " + b + "\nC = " + c + "\n\nRaíz: " + x1 + "\n");
            }
            else
            {
                Console.Write("\n\n------------------------------------------------\nA = " + a + "\nB = " + b + "\nC = " + c + "\n\nRaízes: " + x1 + ", " + x2 + "\n");
            }
            Console.ReadKey();
        }
    }
}
