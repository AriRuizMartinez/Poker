using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Baraja
    {
        List<Carta> cartas;
        public Baraja()
        {
            cartas = new List<Carta>();
        }

        public Carta RobarCarta()
        {
            Carta carta = cartas[0];
            cartas.Remove(carta);
            return carta;
        }

        public void Barajar()
        {
            Random r = new Random();

            int cont = cartas.Count;
            while(cont > 0)
            {
                int id = r.Next(0, cont);
                cartas.Add(cartas[id]);
                cartas.RemoveAt(id);
                cont--;
            }
        }

        public Carta RobarAlAzar()
        {
            Random r = new Random();

            Carta carta = cartas[r.Next(0, cartas.Count)];

            cartas.Remove(carta);

            return carta;
        }

        public Carta RobarCarta(int posicion)
        {
            if(posicion < 0 || posicion >= cartas.Count)
            {
                Console.WriteLine("No se puede robar esa carta. Posicion invalida.");
                return null;
            }

            Carta carta = cartas[posicion];
            cartas.Remove(carta);
            return carta;
        }
    }
}
