using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Jugador
    {
        protected List<Carta> mano;
        public int Count {  get { return mano.Count; } }
        public int Id { get; }

        public Jugador(int id)
        {
            mano = new List<Carta>();
            this.Id = id;
        }

        public void Add(Carta carta)
        {
            carta.poseedor = this;
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
                Add(carta);
        }

        public override string ToString()
        {
            return "El jugador " + Id + " ";
        }

    }
}
