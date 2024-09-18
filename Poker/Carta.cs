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
        public Palo Palo{ get; }

        public Carta(int num, Palo palo)
        {
            this.Num = num;
            this.Palo = palo;
        }
    }
}
