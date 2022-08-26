using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    [Serializable]
    class Tubarao : Peixe
    {
        public override bool AlimentarPeixe(decimal Gramas, Aquario aq)
        {
            if (Gramas > 0.0M)
            {
                base.Peso += (Gramas * 0.25M);

                return true;
            }

            return false;
        }

        public Tubarao(String nome, String cor, Decimal grams) : base("Tubarão", cor, grams)
        {
            base.Nome += "_" + base.NumeroSerie;
        }
    }
}
