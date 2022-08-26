using System;
using System.Collections.Generic;

namespace Ex3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowPosition(0, 0);
            List<EuroMilhoes> pessoaS = new List<EuroMilhoes>();

            for (int i = 0; i < 10; i++)
            {
                Random randomNum = new Random(DateTime.Now.GetHashCode());
                List<int> nums = new List<int>() { randomNum.Next(1, 51), randomNum.Next(1, 51), randomNum.Next(1, 51), randomNum.Next(1, 51), randomNum.Next(1, 51) };
                List<int> ests = new List<int>() { randomNum.Next(1, 13), randomNum.Next(1, 13) };
                EuroMilhoes pessoaX = new EuroMilhoes(nums, ests);
                pessoaS.Add(pessoaX);
                EuroMilhoes solucao = new EuroMilhoes(nums, ests);
            }


            List<int> numsT = new List<int>();
            Console.Write("Olá\n" + EuroMilhoes.contagem + "\n");
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = (ConsoleColor)(i + 2);
                if (pessoaS[i].GetListNum(out numsT))
                {
                    for (int j = 0; j < numsT.Count; j++)
                    {
                        if (pessoaS[i].GetListNum(out numsT))
                        {
                            Console.Write(numsT.ToArray()[j] + "\n");
                        }
                    }
                    Console.WriteLine();
                }
            }

            Console.ReadKey(true);
        }
    }
}
