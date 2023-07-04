using System;
using System.Collections.Generic;
using System.Text;

namespace Ex1
{
    class Pentagono : Figura
    {
        public override double GetPerimetro()
        {
            return (this.pontos[0].GetDist(this.pontos[1]) * 5);
        }

        public override double GetArea()
        {
            double l = (this.GetPerimetro() / 5.0);
            double aux = this.pontos[2].GetDist(this.pontos[3]);
            double h = (this.pontos[0].GetDist(aux / 2.0));

            return (5.0 * ((l * h) / 2.0));
        }

        public Pentagono() : base(5)
        {
            pontos[0] = new Ponto(0, 0);
            pontos[1] = new Ponto(5, 0);
            pontos[2] = new Ponto(6, 5);
            pontos[3] = new Ponto(2.5, YRightBottom);
            pontos[4] = new Ponto(XTop, YTop);
        }
    }
}
