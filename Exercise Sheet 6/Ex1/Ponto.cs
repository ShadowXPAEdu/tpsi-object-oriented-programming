using System;
using System.Collections.Generic;
using System.Text;

namespace Ex1
{
    class Ponto    {
        private double x, y;

        public double GetDistX(Ponto other)
        {
            return Math.Abs(other.GetX() - this.x);
        }

        public double GetDistX(Double other)
        {
            return Math.Abs(other - this.x);
        }

        public double GetDistY(Ponto other)
        {
            return Math.Abs(other.GetY() - this.y);
        }

        public double GetDistY(Double other)
        {
            return Math.Abs(other - this.y);
        }

        public double GetDist(Ponto other)
        {
            return Math.Sqrt(Math.Pow(this.GetDistX(other), 2) + Math.Pow(this.GetDistY(other), 2));
        }

        public double GetDist(Double other)
        {
            return Math.Sqrt(Math.Pow(this.GetDistX(other), 2) + Math.Pow(this.GetDistY(other), 2));
        }

        public void SetX(double x)
        {
            this.x = x;
        }

        public void SetY(double y)
        {
            this.y = y;
        }

        public double GetX()
        {
            return this.x;
        }

        public double GetY()
        {
            return this.y;
        }

        public Ponto(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
