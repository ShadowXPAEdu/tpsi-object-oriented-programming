using System;
using System.Collections.Generic;
using System.Text;

namespace Trabalho_Prático
{
    class Area
    {
        private static List<String> _ListaAreas = new List<String>();
        public static List<String> ListaAreas { get { return _ListaAreas; } }
        public static int Contagem { get; private set; }                                // ?????????????????????????????????????????????
        private String Nome;                                                            // Nome da área ou ID da área
        private EHabitat Habitat;                                                       // Habitat da área
        private Double Capacidade;                                                      // Peso total capaz de suportar de todos os animais da área
        private Double CapacidadeAtual;                                                 // Peso atual de todos os animais da área -- CapacidadeAtual tem de ser <= á Capacidade
        private List<String> Fronteira;                                                 // Áreas fronteiras
        private int NumFronteiras = 0;                                                  // Numero de áreas que faz fronteira -- Max 3
        private ETipoArea TipoArea = (ETipoArea)(-1);                                   // Tipo de área... Não é guardado em ficheiro por isso irá ser sempre ETipoArea.Null cada vez que é carregado a partir do ficheiro
        private List<Animal> Animals = new List<Animal>();                              // Conter lista de animais CapacidadeAtual = Peso de todos os animais

        public void SetCapacidade(Double Cap)
        {
            this.Capacidade = Cap;
        }

        public void SetHabitat(EHabitat Habitat)
        {
            this.Habitat = Habitat;
        }

        public void SetNome(String Nome)
        {
            this.Nome = Nome;
        }

        public List<Animal> GetAnimals()
        {
            return new List<Animal>(this.Animals);
        }

        public bool AddAnimal(Animal XAnimal, List<Species> YSpecies)
        {
            if (!this.Animals.Contains(XAnimal))
            {
                if (YSpecies[Species.ListaEspecies.IndexOf(XAnimal.GetEspecie())].GetHabitats().Contains(this.Habitat))
                {
                    if ((this.Capacidade - this.CapacidadeAtual) >= XAnimal.GetPeso())
                    {
                        this.Animals.Add(XAnimal);
                        this.CapacidadeAtual += XAnimal.GetPeso();
                        // Em principio o animal terá a localização já na sua instância... Mas só para ter a certeza...
                        XAnimal.SetLocalizacao(this.Nome);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool RemoverAnimal(Animal XAnimal)
        {
            if (this.Animals.Contains(XAnimal))
            {
                this.Animals.Remove(XAnimal);
                this.CapacidadeAtual -= XAnimal.GetPeso();
                // Remover animal do zoo depois desta função...
                XAnimal.SetLocalizacao("");
            }

            return false;
        }

        public bool SetETipoArea(ETipoArea TipoArea)
        {
            if (TipoArea != ETipoArea.Jaula && TipoArea != ETipoArea.Vedado)
            {
                return false;
            }
            else
            {
                this.TipoArea = TipoArea;
                return true;
            }
        }

        public List<String> GetFronteiras()
        {
            return this.Fronteira;
        }

        public List<String> GetCpFronteiras()
        {
            return new List<String>(this.Fronteira);
        }

        public void SetNumFronteiras(int NumFronteiras)
        {
            this.NumFronteiras = NumFronteiras;
        }

        public String GetNome()
        {
            return this.Nome;
        }

        public String GetHabitat()
        {
            return this.Habitat.ToString().Replace('_', ' ');
        }

        public Double GetCapacidade()
        {
            return this.Capacidade;
        }

        public Double GetCapacidadeAtual()
        {
            return this.CapacidadeAtual;
        }

        public int GetNumFronteiras()
        {
            return this.NumFronteiras;
        }

        public String GetInfo()
        {
            String str = "Nome: " + this.GetNome() + "\nHabitat: " + this.GetHabitat() + "\nCapacidade total: " + this.GetCapacidade() + " kg\nCapacidade atual: " + this.GetCapacidadeAtual() + " kg\nNúmero de fronteiras: " + this.GetNumFronteiras() + "\nNúmero de animais: " + this.Animals.Count;
            if (this.TipoArea != ETipoArea.Null)
            {
                str += "\nTipo de área: " + this.TipoArea.ToString();
            }
            str += this.GetInfoFronteiras() + this.GetInfoAnimals();
            return str;
        }

        private String GetInfoFronteiras()
        {
            String str = "\n";

            for (int i = 0; i < this.Fronteira.Count; i++)
            {
                str += "\nFronteira [" + (i + 1) + "]:\nNome: " + this.Fronteira[i] + "\n";
            }

            return str;
        }

        private String GetInfoAnimals()
        {
            String str = "\n";

            for (int i = 0; i < this.Animals.Count; i++)
            {
                str += "\nAnimal [" + (i + 1) + "]:\nNome: " + this.Animals[i].GetNome() + "\n";
            }

            return str;
        }

        // Adiciona uma nova fronteira à área se não existir
        public String AddFronteira(Area Area)
        {
            if (this.NumFronteiras > 2 || Area.NumFronteiras > 2)
            {
                return "Impossível adicionar mais fronteiras a esta área.";
            }
            else
            {
                if (!this.Fronteira.Contains(Area.GetNome()))
                {
                    this.NumFronteiras++;
                    this.Fronteira.Add(Area.GetNome());
                }
                if (!Area.Fronteira.Contains(this.GetNome()))
                {
                    Area.NumFronteiras++;
                    Area.Fronteira.Add(this.GetNome());
                }
                return "";
            }
        }

        // Remove fronteira caso exista
        public String RemoverFronteira(Area Area)
        {
            if (this.Fronteira.Count > 0)
            {
                if (this.Fronteira.Contains(Area.GetNome()))
                {
                    this.NumFronteiras--;
                    this.Fronteira.Remove(Area.GetNome());
                    Area.NumFronteiras--;
                    Area.Fronteira.Remove(this.GetNome());
                    return "";
                }

                return "Fronteira (" + Area.GetNome() + ") não encontrada.";
            }
            else
            {
                return "Impossível remover fronteiras a áreas que não têm fronteiras. Área: (" + this.GetNome() + ").";
            }
        }

        public void RenomearFronteira(String OldName, String NewName)
        {
            if (this.Fronteira.Contains(OldName))
            {
                this.Fronteira[this.Fronteira.IndexOf(OldName)] = NewName;
            }
        }

        // Informação para o ficheiro
        public String InfoToFile()
        {
            // File Layout
            // Área Habitat Capacidade(Peso Max) Num_Fronteiras Fronteiras\n

            String str = "";
            str += this.GetNome().Replace(' ', '_') + " " + this.Habitat.ToString() + " " + this.GetCapacidade() + " " + this.GetNumFronteiras();

            for (int i = 0; i < this.Fronteira.Count; i++)
            {
                str += " " + this.Fronteira[i].Replace(' ', '_');
            }

            str += "\n";
            return str;
        }

        // Constructors
        public Area(String Nome, EHabitat Habitat, Double Capacidade, List<String> Fronteiras)
        {
            Nome = Nome.Replace('_', ' ').Trim();

            if (!_ListaAreas.Contains(Nome))
            {
                _ListaAreas.Add(Nome);
                this.Nome = Nome;
                this.Habitat = Habitat;
                this.Capacidade = Capacidade;
                this.CapacidadeAtual = 0;
                this.Fronteira = new List<String>(Fronteiras);
                this.NumFronteiras = this.Fronteira.Count;
                Contagem++;
            }
            else
            {
                throw new Exception("Área já existe.");
            }
        }
    }
}
