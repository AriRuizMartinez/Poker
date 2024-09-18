using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

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

                List<Jugador> ganadores = ComprobarGanador(ronda);
                Jugador ganador;

                while (!HayGanador(ronda, jugadores, ganadores, out ganador)) ;

                ganador?.GanarRonda(ronda);

                Console.WriteLine("");
                foreach(Jugador j in jugadores.ToList())
                {
                    Console.WriteLine(j.ToString() + " tiene " + j.Count + " cartas.");
                    if (j.IsEmpty())
                    {
                        Console.WriteLine("Por lo tanto ha sido eliminado del juego.");
                        jugadores.Remove(j);
                    }
                }
            }
            Console.WriteLine("");
            Console.WriteLine(jugadores[0].ToString() + " ha ganado el juego!!! :)");
            Console.WriteLine("");
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

        static List<Jugador> ComprobarGanador(List<Carta> ronda)
        {
            List<Jugador> ganadores = new List<Jugador>();
            int max = -1;

            Console.WriteLine("----------------------------------------------------");

            foreach (Carta carta in ronda)
            {
                Console.WriteLine(carta.poseedor + " ha sacado " + carta.ToString());

                if (carta.Num > max)
                {
                    max = carta.Num;
                    ganadores.Clear();
                    ganadores.Add(carta.poseedor);
                }else if(carta.Num == max)
                    ganadores.Add(carta.poseedor);
            }

            if(ganadores.Count == 1)
            {
                Console.WriteLine(@"
----------------------------------------------------
El ganador de la ronda ha sido " + ganadores[0].ToString());
            }

            return ganadores;
        }
        
        private static bool HayGanador(List<Carta> ronda, List<Jugador> jugadores, List<Jugador> ganadores, out Jugador ganador)
        {
            string fraseEmpate = "Ha habido un empate entre ";
            List<Carta> nuevaRonda = new List<Carta>();
            ganador = null;

            foreach (Jugador j in ganadores.ToList())
            {
                if (!j.IsEmpty())
                {
                    nuevaRonda.Add(j.DarCarta());
                    fraseEmpate += j.ToString() + ", ";
                }
                else
                {
                    ganadores.Remove(j);
                    ganador = j;
                }
            }

            if (ganadores.Count == 1)
            {
                foreach (Carta c in nuevaRonda)
                    ronda.Add(c);
                ganador = ganadores[0];
                return true;
            }else if(ganadores.Count == 0)
            {
                foreach (Carta c in nuevaRonda)
                    ronda.Add(c);
                return true;
            }

            Console.WriteLine(fraseEmpate);

            List<Jugador> nuevosGanadores = new List<Jugador>();
            int max = -1;

            Console.WriteLine("----------------------------------------------------");

            foreach (Carta carta in nuevaRonda)
            {
                Console.WriteLine(carta.poseedor.ToString() + " ha sacado " + carta.ToString());

                if (carta.Num > max)
                {
                    max = carta.Num;
                    nuevosGanadores.Clear();
                    nuevosGanadores.Add(carta.poseedor);
                }
                else if (carta.Num == max)
                    nuevosGanadores.Add(carta.poseedor);
            }

            foreach(Carta c in nuevaRonda)
                ronda.Add(c);

            if(nuevosGanadores.Count == 1)
            {
                ganador = nuevosGanadores[0];
                
                Console.WriteLine(@"
----------------------------------------------------
El ganador de la ronda ha sido " + ganador.ToString());

                return true;
            }

            ganadores.Clear();
            foreach(Jugador j in nuevosGanadores) 
                ganadores.Add(j);

            ganador = null;
            return false;
        }
    }
       
 }
