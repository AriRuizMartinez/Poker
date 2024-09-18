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
        int id;

        public Jugador(int id)
        {
            mano = new List<Carta>();
            this.id = id;
        }

        public void Add(Carta carta)
        {
            mano.Add(carta);
        }

        public bool IsEmpty()
        {
            return mano.Count == 0;
        }

        public Carta DarCarta()
        {
            Carta c = mano[0];
            mano.RemoveAt(0);
            return c;
        }

        public void GanarRonda(List<Carta> cartaList)
        {
            foreach(Carta carta in cartaList) 
                mano.Add(carta);
        }
    }
}
