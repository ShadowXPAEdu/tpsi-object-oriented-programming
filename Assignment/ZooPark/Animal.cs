using System;
using System.Collections.Generic;
using System.Text;

namespace Trabalho_Prático
{
    [Serializable]
    class Animal
    {
        private static List<String> _ListaAnimais = new List<String>();
        public static List<String> ListaAnimais { get { return _ListaAnimais; } }
        private String Especie;
        private int ID_Animal;
        private String Nome;
        private Double Peso;
        private String Localizacao;
        private List<String> Pais = new List<String>();
        private List<String> Filhos = new List<String>();

        public String GetLocal()
        {
            return this.Localizacao;
        }

        public void SetNome(String Nome)
        {
            this.Nome = Nome;
        }

        public void SetEspecie(String Nome)
        {
            this.Especie = Nome;
        }

        public List<String> GetPais()
        {
            return new List<String>(this.Pais);
        }

        public List<String> GetFilhos()
        {
            return new List<String>(this.Filhos);
        }

        public String GetNome()
        {
            return this.Nome;
        }

        public String GetEspecie()
        {
            return this.Especie;
        }

        public void SetLocalizacao(String Area)
        {
            this.Localizacao = Area;
        }

        public bool AddPais(List<String> Pais)
        {
            if ((Pais.Count > 0) && ((this.Pais.Count + Pais.Count) <= 2))
            {
                for (int i = 0; i < Pais.Count; i++)
                {
                    if (!this.Pais.Contains(Pais[i]))
                    {
                        this.Pais.Add(Pais[i]);
                    }
                }
                return true;
            }

            return false;
        }

        public bool AddFilhos(List<String> Filhos)
        {
            if (Filhos.Count > 0)
            {
                for (int i = 0; i < Filhos.Count; i++)
                {
                    if (!this.Filhos.Contains(Filhos[i]))
                    {
                        this.Filhos.Add(Filhos[i]);
                    }
                }
                return true;
            }

            return false;
        }

        public bool RemovePai(String Pai)
        {
            if (this.Pais.Contains(Pai))
            {
                this.Pais.Remove(Pai);
                return true;
            }

            return false;
        }

        public bool RemoveFilho(String Filho)
        {
            if (this.Filhos.Contains(Filho))
            {
                this.Filhos.Remove(Filho);
                return true;
            }

            return false;
        }

        public Double GetPeso()
        {
            return this.Peso;
        }

        public int GetID_Animal()
        {
            return this.ID_Animal;
        }

        public String GetInfo()
        {
            return "ID do animal por espécie: " + this.ID_Animal + "\nEspécie: " + this.Especie + "\nNome: " + this.Nome + "\nPeso: " + this.Peso + " kg\nLocalização: " + this.Localizacao + this.GetInfoFamilia();
        }

        private String GetInfoFamilia()
        {
            String str = "";

            if (this.Pais.Count > 0)
                str += GetInfoPais();
            if (this.Filhos.Count > 0)
                str += GetInfoFilhos();

            return str;
        }

        private String GetInfoPais()
        {
            String str = "\n";

            for (int i = 0; i < this.Pais.Count; i++)
            {
                str += "\nPais [" + (i + 1) + "]:\nNome: " + this.Pais[i] + "\n";
            }

            return str;
        }

        private String GetInfoFilhos()
        {
            String str = "\n";

            for (int i = 0; i < this.Filhos.Count; i++)
            {
                str += "\nFilhos [" + (i + 1) + "]:\nNome: " + this.Filhos[i] + "\n";
            }

            return str;
        }

        public String InfoToFile()
        {
            // File Layout
            // Espécie Nome Peso Área Num_Filhos Filhos Pais\n

            String str = "";
            str += this.Especie.Replace(' ', '_') + " " + this.Nome.Replace(' ', '_') + " " + this.Peso + " " + this.Localizacao.Replace(' ', '_') + " " + this.Filhos.Count;

            for (int i = 0; i < this.Filhos.Count; i++)
            {
                str += " " + this.Filhos[i].Replace(' ', '_');
            }

            for (int i = 0; i < this.Pais.Count; i++)
            {
                str += " " + this.Pais[i].Replace(' ', '_');
            }

            str += "\n";
            return str;
        }

        public Animal(String Especie, String Nome, Double Peso, String Localizacao, List<String> Pais, List<String> Filhos)
        {
            Especie = Especie.Replace('_', ' ').Trim();
            Nome = Nome.Replace('_', ' ').Trim();
            Localizacao = Localizacao.Replace('_', ' ').Trim();
            for (int i = 0; i < Pais.Count; i++)
            {
                Pais[i] = Pais[i].Replace('_', ' ').Trim();
            }
            for (int i = 0; i < Filhos.Count; i++)
            {
                Filhos[i] = Filhos[i].Replace('_', ' ').Trim();
            }

            // Se espécie não existir ou Área não existir não cria animal
            if (Area.ListaAreas.Contains(Localizacao))
            {
                if (Species.ListaEspecies.Contains(Especie))
                {
                    if (!_ListaAnimais.Contains(Nome))
                    {
                        _ListaAnimais.Add(Nome);
                        this.Especie = Especie;
                        this.Nome = Nome;
                        this.Peso = Peso;
                        this.Localizacao = Localizacao;
                        this.Pais = new List<String>(Pais);
                        // Imposível ter mais que 2 pais... a não ser que sejam adotados?... Remove tudo o que estiver depois dos 2 primeiros pais
                        if (this.Pais.Count > 2)
                            this.Pais.RemoveRange(2, (this.Pais.Count - 2));
                        this.Filhos = new List<String>(Filhos);
                        // Maneira que arranjei para dar ID's separados por espécie
                        // Lista de inteiros (contadores) para cada espécie e cada vez que um animal é criado incrementa o contador com index da lista de espécies
                        // Program.listaSpecies_ID[index da espécie]
                        // como index da espécie é igual ao index do contador da espécie funciona bem ^^
                        Program.listaSpecies_ID[Species.ListaEspecies.IndexOf(Especie)]++;
                        this.ID_Animal = Program.listaSpecies_ID[Species.ListaEspecies.IndexOf(Especie)];
                    }
                    else
                    {
                        throw new Exception("Animal já existe.");
                    }
                }
                else
                {
                    throw new Exception("Espécie não existe.");
                }
            }
            else
            {
                throw new Exception("Área não existe.");
            }
        }
    }
}
