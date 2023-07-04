using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            Peixe p = new Peixe("Peixinho", "Red", 92.0M);
            p.SetCoordPeixe(1, 2);
            Stream file = File.Create("lala.txt");
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(file, p);
            file.Close();
        }
    }
}
