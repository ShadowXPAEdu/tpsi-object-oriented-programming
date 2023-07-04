using System;
using System.Collections.Generic;
using System.Text;

namespace Ficha_5
{
    class Aquario
    {
        private static int _numSerie = 0;
        public static int NumeroSerie_Aq { get { return _numSerie; } }
        private UInt16 MaxPeixes;
        public String Nome { get; }
        List<Peixe> Peixes;
        List<Peixe> PeixesTemp;
        private Ponto Tamanho;

        private Random rnd;

        public Boolean AddPeixe(Peixe novo)
        {
            if (!(this.Peixes.Contains(novo)) && (this.Peixes.Count < this.MaxPeixes))
            {
                this.Peixes.Add(novo);
                return true;
            }

            return false;
        }

        public Boolean RemovePeixe(Peixe peixeRem)
        {
            if (this.Peixes.Contains(peixeRem))
            {
                this.Peixes.Remove(peixeRem);
                return true;
            }

            return false;
        }

        public void Abanar()
        {
            foreach (Peixe p in Peixes)
            {
                p.EmagreçerPeixe(this);
            }

            foreach (Peixe p in Peixes)
            {
                int x = rnd.Next(p.GetCoordPeixe().GetX() - 2, p.GetCoordPeixe().GetX() + 2);
                int y = rnd.Next(p.GetCoordPeixe().GetY() - 2, p.GetCoordPeixe().GetY() + 2);
                Ponto aux = new Ponto(x, y);
                p.SetCoordPeixe(aux);
            }

            foreach (Peixe p in Peixes)
            {
                foreach (Peixe outro in Peixes)
                {
                    if (p != outro)
                    {
                        if (p.GetCoordPeixe().ComparaCoordenadas(outro.GetCoordPeixe()))
                        {
                            if (p.GetPeso() < outro.GetPeso())
                            {
                                RemovePeixe(p);
                            }
                            else if (p.GetPeso() > outro.GetPeso())
                            {
                                RemovePeixe(outro);
                            }
                        }
                    }
                }
            }
        }

        public void Alimenta(int quant)
        {
            int q = quant / Peixes.Count;
            foreach (Peixe p in Peixes)
            {
                p.AlimentarPeixe(q, this);
            }
        }

        public bool RemovePeixeNumSerie(int serie)
        {
            foreach (Peixe p in Peixes)
            {
                if (p.GetNumSerie() == serie)
                {
                    Peixes.Remove(p);
                    return true;
                }
            }
            return false;
        }

        public String GetInfoAquario()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Aquário com dimensões [" + this.Tamanho.GetX() + "," + this.Tamanho.GetY() + "], número máximo de peixes " + this.MaxPeixes + "");
            foreach (Peixe p in Peixes)
            {
                sb.Append(p.GetInfo());
            }
            return sb.ToString();
        }

        public Aquario(int MaxCapacity, Ponto Tamanho)
        {
            this.MaxPeixes = (UInt16)Math.Abs(MaxCapacity);
            _numSerie++;
            Peixes = new List<Peixe>();
            this.Tamanho = Tamanho;
            this.Nome = "Aquario " + _numSerie;

            rnd = new Random();
        }
    }
}
