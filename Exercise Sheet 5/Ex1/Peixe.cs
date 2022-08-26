using System;
using System.Collections.Generic;
using System.Text;

namespace Ficha_5
{
    class Peixe
    {
        private static UInt16 _numSerie = 0;
        private String Nome, Cor;
        private Decimal Peso;
        private Ponto CoordenadasPeixe;
        private UInt16 NumeroSerie;
        public static UInt16 Numero_Serie { get { return _numSerie; } }

        public Decimal GetPeso()
        {
            return this.Peso;
        }

        public UInt16 GetNumSerie()
        {
            return this.NumeroSerie;
        }

        public Boolean AlimentarPeixe(Decimal Gramas, Aquario aq)
        {
            if (Gramas > 0.0M)
            {
                this.Peso += (Gramas / 2);

                if (Verifica100G())
                {
                    this.Peso /= 2;
                    Peixe novo = new Peixe(this.Nome + "C", this.Cor, this.Peso);
                    aq.AddPeixe(novo);
                }

                return true;
            }

            return false;
        }

        public Ponto GetCoordPeixe()
        {
            return this.CoordenadasPeixe;
        }

        public void SetCoordPeixe(int x, int y)
        {
            this.CoordenadasPeixe.SetX(x);
            this.CoordenadasPeixe.SetY(y);
        }

        public void SetCoordPeixe(Ponto novoCoord)
        {
            this.CoordenadasPeixe = novoCoord;
        }

        private Boolean Verifica100G()
        {
            if (this.Peso >= 100.0M)
            {
                return true;
            }

            return false;
        }

        public String GetInfo()
        {
            return "Peixe " + this.Nome + " com cor " + this.Cor + ", número " + this.NumeroSerie + ", peso " + this.Peso + ". " + this.CoordenadasPeixe.GetCoordenadas();
        }

        private void VerificaMorte(Aquario aq)
        {
            if (this.Peso < 10.0M)
            {
                aq.RemovePeixe(this);
            }
        }

        public void EmagreçerPeixe(Aquario aq)
        {
            this.Peso -= this.Peso * 0.1M;
            VerificaMorte(aq);
        }

        public Peixe(String Nome, String Cor, Decimal Gramas)
        {
            this.NumeroSerie = ++_numSerie;
            this.Nome = Nome;
            this.Cor = Cor;
            this.Peso = Gramas;
        }

    }
}
