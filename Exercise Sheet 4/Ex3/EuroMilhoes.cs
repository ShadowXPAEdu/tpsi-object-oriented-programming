using System;
using System.Collections.Generic;
using System.Text;

namespace Ex3
{
    class EuroMilhoes
    {
        public static int contagem;
        private List<int> num;
        private List<int> est;

        public bool AddNum(int x)
        {
            if (this.num.Count < 5)
            {
                if (x > 0 && x < 51)
                {
                    if (!this.num.Contains(x))
                    {
                        this.num.Add(x);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool AddEst(int y)
        {
            if (this.est.Count < 2)
            {
                if (y > 0 && y < 13)
                {
                    if (!this.est.Contains(y))
                    {
                        this.est.Add(y);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetListNum(out List<int> x)
        {
            if (this.num.Count < 5)
            {
                x = null;
                return false;
            }
            else
            {
                x = this.num;
                return true;
            }
        }

        public EuroMilhoes()
        {
            this.num = new List<int>();
            this.est = new List<int>();

            contagem++;
        }

        public EuroMilhoes(List<int> x, List<int> y)
        {
            this.num = new List<int>();
            this.est = new List<int>();

            foreach (int i in x)
            {
                AddNum(i);
            }

            foreach (int j in y)
            {
                AddEst(j);
            }

            contagem++;
        }
    }
}
