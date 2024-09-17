using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Jugador
    {
        List<Carta> mano;

        public Jugador()
        {
            mano = new List<Carta>();
        }

        public void Add(Carta carta)
        {
            mano.Add(carta);
        }

        public bool IsEmpty()
        {
            return mano.Count == 0;
        }
    }
}
