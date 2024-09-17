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

        private static void Jugar()
        {

            int numeroJugadores = PedirNumero();

            List<Jugador> jugadores = new List<Jugador>();
            int turno = 0;

            for (int i = 0; i < numeroJugadores; i++)
                jugadores.Add(new Jugador(i));

            Baraja baraja = new Baraja();
            baraja.CrearBaraja();
            baraja.Barajar();

            Repartir(jugadores, baraja);

            while(jugadores.Count > 1)
            {


                for(int i = 0; i < jugadores.Count; i++)
                {
                    if (jugadores[i].IsEmpty())
                    {
                        jugadores.Remove(jugadores[i]);
                        i--;
                    }
                }
            }

        }

        private static void Repartir(List<Jugador> jugadores, Baraja baraja)
        {
            int turno = 0;
            int residuo = baraja.Count % jugadores.Count;
            while ( baraja.Count > residuo)
            {
                jugadores[turno].Add(baraja.RobarCarta());
                turno++;
                if (turno >= jugadores.Count)
                    turno = 0;
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
    }
       
 }
