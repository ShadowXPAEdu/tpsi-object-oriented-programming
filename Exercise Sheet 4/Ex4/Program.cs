using System;
using System.Collections.Generic;

namespace Ex4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ContaBancaria> contas = new List<ContaBancaria>();

            do
            {
            } while (Menu(contas));
        }

        private static Boolean Menu(List<ContaBancaria> x)
        {
            ushort num;

            try
            {
                Console.Clear();
                Console.ForegroundColor = (ConsoleColor)0x0A;
                Console.WriteLine("Menu Conta Bancária\n");
                Console.ForegroundColor = (ConsoleColor)0x0B;
                Console.WriteLine("Número de Contas: " + ContaBancaria.Numero);
                Console.ForegroundColor = (ConsoleColor)0x0C;
                Console.WriteLine("[1] - Criar conta bancária");
                Console.WriteLine("[2] - Ativar conta bancária");
                Console.WriteLine("[3] - Levantar quantia");
                Console.WriteLine("[4] - Depositar quantia");
                Console.WriteLine("[5] - Transferir quantia");
                Console.WriteLine("[0] - Sair");
                Console.Write(" > ");
                Console.ForegroundColor = (ConsoleColor)0x0F;
                num = (ushort)Math.Abs(int.Parse(Console.ReadLine()));

                switch (num)
                {
                    case 0:
                        return false;
                    case 1:
                        // Ativar conta
                        break;
                    case 2:
                        CriarConta(x);
                        break;
                    case 3:
                        // Depositar quantia
                        break;
                    case 4:
                        // Transferir quantia
                        break;
                    case 5:
                        break;
                    default:
                        return true;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Ocurreu um erro!\nMensagem: " + erro.Message);
            }

            return true;
        }

        private static String CriarConta(List<ContaBancaria> x)
        {
            String nome;
            Decimal saldo;



            //ContaBancaria novaConta = new ContaBancaria(nome, saldo);


            return "Occureu um erro!";
        }
    }
}
