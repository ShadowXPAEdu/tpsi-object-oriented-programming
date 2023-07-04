using System;
using System.Collections.Generic;
using System.Text;

namespace Ex1
{
    class Triangulo : Figura
    {
        public override double GetPerimetro()
        {
            return (this.pontos[0].GetDist(this.pontos[1]) + this.pontos[1].GetDist(this.pontos[2]) + this.pontos[2].GetDist(this.pontos[0]));
        }

        public override double GetArea()
        {
            double i = (this.GetPerimetro() / 2.0);
            return Math.Sqrt(i * (i - this.pontos[0].GetDist(this.pontos[1])) * (i - this.pontos[1].GetDist(this.pontos[2])) * (i - this.pontos[2].GetDist(this.pontos[0])));
        }

        private double GetHipotnusa()
        {
            double Hipotnusa, aux, aux2;
            aux = this.pontos[0].GetDist(this.pontos[1]);
            aux2 = this.pontos[1].GetDist(this.pontos[2]);
            Hipotnusa = aux;
            if (aux < aux2)
            {
                Hipotnusa = aux2;
            }
            aux = this.pontos[2].GetDist(this.pontos[0]);
            if (aux2 < aux)
            {
                Hipotnusa = aux;
            }

            return Hipotnusa;
        }

        public Triangulo(int XLeftBottom, int YLeftBottom, int XRightBottom, int YRightBottom, int XTop, int YTop) : base(3)
        {
            pontos[0] = new Ponto(XLeftBottom, YLeftBottom);
            pontos[1] = new Ponto(XRightBottom, YRightBottom);
            pontos[2] = new Ponto(XTop, YTop);
        }

        public Triangulo(Ponto BottomLeft, Ponto BottomRight, Ponto Top) : base(3)
        {
            pontos[0] = BottomLeft;
            pontos[1] = BottomRight;
            pontos[2] = Top;
        }
    }
}
