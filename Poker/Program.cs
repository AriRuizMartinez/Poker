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

            Jugador[] jugadores = new Jugador[numeroJugadores];
            int turno = 0;

            for (int i = 0; i < numeroJugadores; i++)
                jugadores[i] = new Jugador();

            Baraja baraja = new Baraja();
            baraja.CrearBaraja();
            baraja.Barajar();

            Repartir(jugadores, baraja);

        }

        private static void Repartir(Jugador[] jugadores, Baraja baraja)
        {
            int turno = 0;
            while ( !baraja.IsEmpty())
            {
                jugadores[turno].Add(baraja.RobarCarta());
                turno++;
                if (turno >= jugadores.Length)
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
