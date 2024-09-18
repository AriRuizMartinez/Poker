using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                switch (Menu())
                {
                    case 1:
                        Jugar();
                        break;
                    case 0:
                        return;
                }
            }
        }

        static int Menu()
        {
            int option;

            do
            {
                Console.WriteLine(@"
┌───────────────────────────────────┐
│        MENU  PRINCIPAL            │
├───────────────────────────────────┤
│  (1)  - Jugar                     │
│  (0)  - Salir                     │
└───────────────────────────────────┘
");

                if (!int.TryParse(Console.ReadLine(), out option))
                    Console.WriteLine("Opcion invalida");

            } while (option < 0 || option > 1);

            return option;
        }

        private static void Jugar()
        {

            int numeroJugadores = PedirNumero();

            List<Jugador> jugadores = new List<Jugador>();

            for (int i = 0; i < numeroJugadores; i++)
                jugadores.Add(new Jugador(i));

            Baraja baraja = new Baraja();
            baraja.CrearBaraja();
            baraja.Barajar();

            Repartir(jugadores, baraja);

            while(jugadores.Count > 1)
            {
                List<Carta> ronda = new List<Carta>();

                foreach(Jugador jugador in jugadores) 
                    ronda.Add(jugador.DarCarta());

                int id = ComprobarGanador(ronda);

                jugadores[id].GanarRonda(ronda);

                Console.WriteLine("");
                for(int i = 0; i < jugadores.Count; i++)
                {
                    Console.WriteLine("El jugador " + jugadores[i].Id + " tiene " + jugadores[i].Count + " cartas.");
                    if (jugadores[i].IsEmpty())
                    {
                        Console.WriteLine("Por lo tanto ha sido eliminado del juego.");
                        jugadores.Remove(jugadores[i]);
                        i--;
                    }
                }
            }

        }

        static int PedirNumero()
        {
            Console.WriteLine("Cuantos jugadores vais a ser?");

            if (int.TryParse(Console.ReadLine(), out int numeroJugadores))
            {
                if (numeroJugadores >= 2 && numeroJugadores <= 5)
                    return numeroJugadores;

                Console.WriteLine("El numero minimo de jugadores es 2 y el maximo 5.");
                return PedirNumero();
            }
            else
            {
                Console.WriteLine("Debes introducir un numero ");
                return PedirNumero();
            }

        }

        private static void Repartir(List<Jugador> jugadores, Baraja baraja)
        {
            Console.WriteLine("Repartimos las cartas.");
            int turno = 0;
            int residuo = baraja.Count % jugadores.Count;
            while ( baraja.Count > residuo)
            {
                jugadores[turno].Add(baraja.RobarCarta());
                turno++;
                if (turno >= jugadores.Count)
                    turno = 0;
            }
            Console.WriteLine("Cada jugador empieza con " + jugadores[0].Count + " cartas.");
        }

        static int ComprobarGanador(List<Carta> ronda)
        {
            int id = -1;
            int ganador = -1;
            int cont = 0;

            Console.WriteLine("----------------------------------------------------");

            foreach (Carta carta in ronda)
            {
                Console.WriteLine("El jugador " + cont + " ha sacado el " +  carta.Num + " de " + carta.Palo.ToString());
                if (carta.Num > ganador)
                {
                    ganador = carta.Num;
                    id = cont;
                }
                cont++;
            }

            Console.WriteLine(@"
----------------------------------------------------
El ganador de la ronda ha sido el jugador " +  id);
            return id;
        }
    }
       
 }
