using System;
using System.Collections.Generic;
using System.Text;

namespace Trabalho_Prático
{
    [Serializable]
    class Species
    {
        private static List<String> _ListaEspecies = new List<String>();
        public static List<String> ListaEspecies { get { return _ListaEspecies; } }
        private String Especie;                             // ID
        private List<EHabitat> Habitats;                    // Lista de Habitats

        public void SetNome(String Nome)
        {
            this.Especie = Nome;
        }

        public List<EHabitat> GetHabitats()
        {
            return new List<EHabitat>(this.Habitats);
        }

        public String GetNomeEspecie()
        {
            return this.Especie;
        }

        public bool AddHabitat(EHabitat Habitat)
        {
            if (!this.Habitats.Contains(Habitat))
            {
                this.Habitats.Add(Habitat);
                return true;
            }

            return false;
        }

        public bool RemoverHabitat(EHabitat Habitat)
        {
            if (this.Habitats.Contains(Habitat))
            {
                this.Habitats.Remove(Habitat);
                return true;
            }

            return false;
        }

        public String GetInfo()
        {
            return "Nome da espécie: " + this.Especie + this.GetInfoHabitats();
        }

        private String GetInfoHabitats()
        {
            String str = "\n";

            for (int i = 0; i < this.Habitats.Count; i++)
            {
                str += "\nHabitat [" + (i + 1) + "]:\nNome: " + this.Habitats[i].ToString().Replace('_', ' ') + "\n";
            }

            return str;
        }

        public String InfoToFile()
        {
            // File Layout
            // Espécie Habitats\n

            String str = "";
            str += this.Especie.Replace(' ', '_');

            for (int i = 0; i < this.Habitats.Count; i++)
            {
                str += " " + this.Habitats[i].ToString();
            }

            str += "\n";
            return str;
        }

        // Constructors
        public Species(String NomeEspecie, List<EHabitat> Habitats)
        {
            NomeEspecie = NomeEspecie.Replace('_', ' ').Trim();

            if (!_ListaEspecies.Contains(NomeEspecie))
            {
                _ListaEspecies.Add(NomeEspecie);
                this.Especie = NomeEspecie;
                this.Habitats = new List<EHabitat>(Habitats);
            }
            else
            {
                throw new Exception("Espécie já existe.");
            }
        }
    }
}
