using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Carta
    {
        public int Num { get; }
        public ePalo Palo{ get; }

        public Carta(int num, ePalo palo)
        {
            this.Num = num;
            this.Palo = palo;
        }

        public override string ToString()
        {
            return " el " + Num + " de " + Palo.ToString();
        }
    }
}
