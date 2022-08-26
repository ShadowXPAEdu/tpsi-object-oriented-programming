using System;
using System.IO;
using System.Text;

namespace Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            int num_pedido = 0;
            string nome_fich = "tabuada.dat";

            Gravar(ref num_pedido, nome_fich);

            Ler(nome_fich);

            CopiarCriarDirectorioBackupEBackup(nome_fich);

            ApagarFicheiro(nome_fich);

            VerDirectorio();

            Console.ReadLine();
        }

        private static void Gravar(ref int x, string y)
        {
            Console.Write("Introduza um número:\n > ");
            while (int.TryParse(Console.ReadLine(), out x) == false)
            {
                Console.Write("\n\nErro. Não é um número!\n");
                Console.Write("Introduza um número:\n > ");
            }

            FileStream fich1 = File.Create(y);
            BinaryWriter escrever = new BinaryWriter(fich1, Encoding.Unicode);

            string linhas = "";

            for (int i = 0; i < 10; i++)
            {
                linhas += (i + 1) + "\tx\t" + x + "\t=\t" + ((i + 1) * x) + "\n";
            }

            escrever.Write(linhas);

            escrever.Dispose();
            fich1.Dispose();
            escrever.Close();
            fich1.Close();
        }

        private static void Ler(string y)
        {
            FileStream fich1 = File.OpenRead(y);
            BinaryReader ler = new BinaryReader(fich1, Encoding.Unicode);
            string linha = "";

            while (ler.PeekChar() != -1)
            {
               linha += ler.ReadString();
            }
            Console.WriteLine(linha);

            ler.Dispose();
            fich1.Dispose();
            ler.Close();
            fich1.Close();
        }

        private static void CopiarCriarDirectorioBackupEBackup(string y)
        {
            FileInfo fich1 = new FileInfo(y);

            if (fich1.Exists)
            {
                int i = 0;
                Console.Write("\nIntroduza o nome do novo ficheiro:\n > ");
                string nome = Console.ReadLine(), novo_nome;

                do
                {
                    novo_nome = nome + "_" + i + ".bak.dat";
                    i++;
                } while (File.Exists(@"backups\" + novo_nome));

                CriarDiretorioBackup();

                fich1.CopyTo(@"backups\" + novo_nome);
            }
            else
            {
                Console.Write("Ficheiro não existe!");
            }
        }

        private static void CriarDiretorioBackup()
        {
            if (!Directory.Exists("backups"))
            {
                Directory.CreateDirectory("backups");
            }
        }

        private static void ApagarFicheiro(string y)
        {
            File.Delete(y);
        }

        private static void VerDirectorio()
        {
            string[] ficheiros;
            Console.Write("Introduza o nome do directorio:\n > ");
            string nome = Console.ReadLine();

            if (!Directory.Exists(nome))
            {
                Console.Write("\nDirectorio inexistente!\n\n");
            }
            else
            {
                ficheiros = Directory.GetFiles(nome, "*.*", SearchOption.AllDirectories);
                Console.WriteLine();
                Console.WriteLine();
                for (int i = 0; i < ficheiros.Length; i++)
                {
                    Console.WriteLine(ficheiros[i]);
                }
            }
        }
    }
}
