using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Carta
    {
        int num;
        Palo palo;
        public int Num { get { return num; } set { num = value; } }
        public Palo Palo{ get { return palo; } set { palo = value; } }

        public Carta(int num, Palo palo)
        {
            this.num = num;
            this.palo = palo;
        }

        public Carta(int num, int palo)
        {
            this.num = num;
            if (palo < 0 || palo >= 4)
                palo = 0;
            this.palo = (Palo) palo;

            Enum.GetValues(typeof(Palo));
        }

    }
}
