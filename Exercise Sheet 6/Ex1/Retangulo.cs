using System;
using System.Collections.Generic;
using System.Text;

namespace Ex1
{
    class Retangulo : Figura
    {
        public override double GetArea()
        {
            return (this.pontos[0].GetDist(this.pontos[1]) * this.pontos[1].GetDist(this.pontos[2]));
        }

        public override double GetPerimetro()
        {
            return ((this.pontos[0].GetDist(this.pontos[1]) * 2) + (2 * this.pontos[1].GetDist(this.pontos[2])));
        }

        public Retangulo(int XLeftBottom, int YLeftBottom, int XRightTop, int YRightTop) : base(4)
        {
            pontos[0] = new Ponto(XLeftBottom, YLeftBottom);
            pontos[1] = new Ponto(XRightTop, YLeftBottom);
            pontos[2] = new Ponto(XRightTop, YRightTop);
            pontos[3] = new Ponto(XLeftBottom, YRightTop);
        }

        public Retangulo(Ponto LeftBottom, Ponto RightTop) : base(4)
        {
            pontos[0] = LeftBottom;
            pontos[1] = new Ponto(RightTop.GetX(), LeftBottom.GetY());
            pontos[2] = RightTop;
            pontos[3] = new Ponto(LeftBottom.GetX(), RightTop.GetY());
        }
    }
}
