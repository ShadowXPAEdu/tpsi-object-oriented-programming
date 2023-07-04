using System;
using System.Collections.Generic;
using System.Text;

namespace Ficha_5
{
    class Ponto
    {
        private int X, Y;

        public int GetX()
        {
            return this.X;
        }

        public int GetY()
        {
            return this.Y;
        }

        public void SetX(int X)
        {
            this.X = X;
        }

        public void SetY(int Y)
        {
            this.Y = Y;
        }

        public String GetCoordenadas()
        {
            return "O peixe está em [" + this.X + "," + this.Y + "]";
        }

        public bool ComparaCoordenadas(Ponto outro)
        {
            if (this.X == outro.GetX() && this.Y == outro.GetY())
                return true;
            return false;
        }

        public Ponto(int CoordX, int CoordY)
        {
            this.X = CoordX;
            this.Y = CoordY;
        }
    }
}
