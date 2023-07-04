using System;
using System.Collections.Generic;
using System.Text;

namespace Ex1
{
    class Figura
    {
        protected Ponto[] pontos;
        private int Num_Vertices;

        public String GetPosPonto(int index)
        {
            return "[" + pontos[index].GetX() + ", " + pontos[index].GetY() + "]";
        }

        public int GetNumVertices()
        {
            return this.Num_Vertices;
        }
        
        public virtual double GetArea()
        {
            return 0;
        }

        public virtual double GetPerimetro()
        {
            return 0;
        }

        public Figura(int Vertices)
        {
            pontos = new Ponto[Vertices];
            for (int i = 0; i < Vertices; i++)
            {
                pontos[i] = new Ponto(0, 0);
            }
            this.Num_Vertices = Vertices;
        }
    }
}
