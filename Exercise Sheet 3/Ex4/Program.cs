using System;
using System.IO;
using System.Text;

namespace Ex4
{
    class Program
    {
        static void Main(string[] args)
        {
            bool menu = true;
            int opçao = -1, num = 0;
            string nomeFich = "tabuada.dat";

            do
            {
                // Menu
                do
                {
                    menu = ShowMenu(ref opçao, num);
                } while (menu);

                // Resto
                switch (opçao)
                {
                    case 0:
                        break;
                    case 1:
                        ReceberNumTabuada(ref num);
                        break;
                    case 2:
                        GravarFicheiro(nomeFich, num);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    default:
                        Console.Write("\n\nErro....\n\n");
                        break;
                }

            } while (opçao != 0);
        }

        private static bool ShowMenu(ref int x, int y)
        {
            do
            {
                try
                {
                    Console.Write("\n####################### Menu #######################\n\n");
                    Console.Write("~> Número a ser guardado: " + y + "\n\n");
                    Console.Write("[1] - Receber número da tabuada\n");
                    Console.Write("[2] - Gravar ficheiro (tabuada.dat)\n");
                    Console.Write("[3] - Ler ficheiro (tabuada.dat)\n");
                    Console.Write("[4] - Copiar ficheiro (tabuada.dat)\n");
                    Console.Write("[5] - Apagar ficheiro (tabuada.dat)\n");
                    Console.Write("[6] - Criar directoria backup\n");
                    Console.Write("[7] - Copiar ficheiros para o backup\n");
                    Console.Write("[0] - Sair\n\n");

                    Console.Write("Opção:\n > ");
                    x = int.Parse(Console.ReadLine());
                }
                catch (FormatException erro)
                {
                    Console.Write("\nErro! Não é um número! Tente novamente!!\n" + erro.Message + "\n\n");
                }
                catch (Exception erro)
                {
                    Console.Write("\nOcurreu um erro...\n" + erro + "\n\n");
                }
            } while (x < 0 || x > 7);

            return false;
        }

        private static void ReceberNumTabuada(ref int x)
        {
            bool invalido = true;

            do
            {
                try
                {
                    Console.Write("Introduza um número:\n > ");
                    x = int.Parse(Console.ReadLine());
                    invalido = false;
                }
                catch (FormatException erro)
                {
                    Console.Write("\nErro! Não é um número! Tente novamente!!\n" + erro.Message + "\n\n");
                }
                catch (Exception erro)
                {
                    Console.Write("\nOcurreu um erro...\n" + erro + "\n\n");
                }
            } while (invalido);
        }

        private static void GravarFicheiro(string y, int x)
        {
            FileStream fich1 = null;
            BinaryWriter escre = null;

            try
            {
                fich1 = File.Create(y);
                escre = new BinaryWriter(fich1, Encoding.Unicode);
                string tabuada = "";

                for (int i = 0; i < 10; i++)
                {
                    tabuada += (i + 1) + "\tx\t" + x + "\t=\t" + ((i+1) * x) + "\n";
                }

                escre.Write(tabuada);

                Console.Write("\nFicheiro guardado com tabuada do \"" + x + "\"\n\n");
            }
            catch (FileNotFoundException erro)
            {
                Console.Write(erro);
            }
            catch (Exception erro)
            {
                Console.Write(erro);
            }
            finally
            {
                if (escre != null)
                {
                    escre.Dispose();
                    escre.Close();
                }

                if (fich1 != null)
                {
                    fich1.Dispose();
                    fich1.Close();
                }
            }
        }
    }
}
