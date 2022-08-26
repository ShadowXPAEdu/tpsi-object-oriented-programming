using System;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime data = new DateTime(1997, 12, 23, 6, 0, 0);
            Pessoa pessoa1 = new Pessoa("Pedro Alves", data, 15969694, 'M');

            pessoa1.SetMorada("Rua 14, Nº 22, 2014-003, Cidade 22");
            pessoa1.SetNumTel(920227242);

            Console.WriteLine("Nome: " + pessoa1.GetName() + "\nData de Nascimento: " + pessoa1.GetDataNasc() + "\nIdade: " + pessoa1.GetIdade() + "\nCC: " + pessoa1.GetCC() + "\nMorada: " + pessoa1.GetMorada() + "\nSexo: " + pessoa1.GetSexo() + "\nNúmero de telefone: " + pessoa1.GetNumTelefone());

            Console.ReadKey();
        }
    }
}
