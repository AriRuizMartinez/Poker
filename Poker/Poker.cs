using System;
using System.Collections.Generic;

namespace Poker
{
    public class Poker
    {
        public Poker() { }

        public void JugarPoker()
        {
            int numeroJugadores = Program.PedirNumero();

            List<JugadorPoker> jugadores = new List<JugadorPoker>();

            for (int i = 0; i < numeroJugadores; i++)
                jugadores.Add(new JugadorPoker(i, 1000));

            Console.WriteLine("Todos los jugadores empezais con 1000$");

            Baraja baraja = new Baraja();

            while (jugadores.Count > 1)
            {
                baraja.CrearBaraja();
                baraja.Barajar();

                RepartirPareja(jugadores, baraja);

                Baraja flop = new Baraja();
                for (int i = 0; i < 3; i++)
                    flop.Add(baraja.RobarCarta());

                Console.WriteLine("Tu mano es " + jugadores[0].Mano());
                Console.WriteLine("Las cartas comunitarias son " + flop.ToString());
                int apuestaMasAlta = 0;
                int totalApostado = 0;
                switch (OpcionesJugador())
                {
                    case 1:

                        break;
                    case 2:
                        //int apostar = Apostar();
                        break;
                    default:
                        break;

                }

            }
            Console.WriteLine("");
            Console.WriteLine(jugadores[0].ToString() + " ha ganado el juego!!! :)");
            Console.WriteLine("");

        }

        /*private  int Apostar()
        {
            Console.WriteLine("Cuanto quieres apostar?");
            if()
        }*/

        private void RepartirPareja(List<JugadorPoker> jugadores, Baraja baraja)
        {
            foreach (JugadorPoker jugador in jugadores)
                for (int i = 0; i < 2; i++)
                    jugador.Add(baraja.RobarCarta());
        }

        int OpcionesJugador()
        {
            int option;

            do
            {
                Console.WriteLine(@"
┌────────────────────────────────────┐
│              OPCIONES              │
├────────────────────────────────────┤
│  (1)  - Foldear                    │
│  (2)  - Apostar                    │
└────────────────────────────────────┘
");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Opcion invalida");
                    option = -1;
                }

            } while (option < 1 || option > 2);

            return option;
        }

        int Apuesta()
        {
            int option;

            do
            {
                Console.WriteLine(@"
┌────────────────────────────────────┐
│              OPCIONES              │
├────────────────────────────────────┤
│  (1)  - Igualar apuesta            │
│  (2)  - Subir apuesta              │
│  (3)  - Retirarse                  │
└────────────────────────────────────┘
");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    option = -1;
                    Console.WriteLine("Opcion invalida");
                }

            } while (option < 1 || option > 3);

            return option;
        }
    }
}
