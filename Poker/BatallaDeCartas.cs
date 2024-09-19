using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    public class BatallaDeCartas
    {
        public BatallaDeCartas() { }

        public void JugarBatallaDeCartas()
        {
            List<Jugador> jugadores = InicializarJugadores();

            Baraja b = InicializarBaraja(jugadores);
            int rondaN = 0;
            while (jugadores.Count > 1)
            {
                List<Carta> ronda = new List<Carta>();

                foreach (Jugador jugador in jugadores)
                    ronda.Add(jugador.DarCarta());

                List<Jugador> ganadores = ComprobarGanador(ronda);
                Jugador ganador;

                while (!HayGanador(ronda, jugadores, ganadores, out ganador)) ;

                ganador?.GanarRonda(ronda);

                EliminarJugadores(jugadores);
                rondaN++;
            }
            Console.WriteLine("");
            Console.WriteLine(jugadores[0].ToString() + " ha ganado el juego!!! :)");
            Console.WriteLine(rondaN);
        }

        private List<Jugador> InicializarJugadores()
        {
            int numeroJugadores = Program.PedirNumero();

            List<Jugador> jugadores = new List<Jugador>();

            for (int i = 0; i < numeroJugadores; i++)
                jugadores.Add(new Jugador(i));

            return jugadores;
        }

        private Baraja InicializarBaraja(List<Jugador> jugadores)
        {
            Baraja baraja = new Baraja();
            baraja.CrearBaraja();
            baraja.Barajar();

            Repartir(jugadores, baraja);

            return baraja;
        }

        public void Repartir(List<Jugador> jugadores, Baraja baraja)
        {
            Console.WriteLine("Repartimos las cartas.");
            int turno = 0;
            int residuo = baraja.Count % jugadores.Count;
            while (baraja.Count > residuo)
            {
                jugadores[turno].Add(baraja.RobarCarta());
                turno++;
                if (turno >= jugadores.Count)
                    turno = 0;
            }
            Console.WriteLine("Cada jugador empieza con " + jugadores[0].NumeroDeCartas + " cartas.");
        }

        private void AñadirCartasRondaEmpateALaRondaGeneral(List<Carta> ronda, List<Carta> rondaEmpate)
        {
            ronda.AddRange(rondaEmpate);
        }

        static List<Jugador> ComprobarGanador(List<Carta> ronda)
        {
            List<Jugador> ganadores = ComprobarCartaMasGrande(ronda);

            if (ganadores.Count == 1)
            {
                Console.WriteLine(@"
----------------------------------------------------
El ganador de la ronda ha sido " + ganadores[0].ToString());
            }

            return ganadores;
        }

        private static List<Jugador> ComprobarCartaMasGrande(List<Carta> ronda)
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
                }
                else if (carta.Num == max)
                    ganadores.Add(carta.poseedor);
            }
            return ganadores;
        }

        public bool HayGanador(List<Carta> ronda, List<Jugador> jugadores, List<Jugador> ganadores, out Jugador ganador)
        {
            List<Carta> nuevaRonda = PedirCartasJugadoresYComprobarSiPueden(ganadores, out string fraseEmpate, out ganador);

            if (ganadores.Count == 1)
            {
                AñadirCartasRondaEmpateALaRondaGeneral(ronda, nuevaRonda);
                ganador = ganadores[0];
                return true;
            }
            else if (ganadores.Count == 0)
            {
                AñadirCartasRondaEmpateALaRondaGeneral(ronda, nuevaRonda);
                return true;
            }

            Console.WriteLine(fraseEmpate);

            List<Jugador> nuevosGanadores = ComprobarCartaMasGrande(nuevaRonda);

            AñadirCartasRondaEmpateALaRondaGeneral(ronda, nuevaRonda);

            if (nuevosGanadores.Count == 1)
            {
                ganador = nuevosGanadores[0];

                Console.WriteLine(@"
----------------------------------------------------
El ganador de la ronda ha sido " + ganador.ToString());

                return true;
            }

            ganadores.Clear();
            ganadores.AddRange(nuevosGanadores);

            ganador = null;
            return false;
        }

        private List<Carta> PedirCartasJugadoresYComprobarSiPueden(List<Jugador> ganadores, out string fraseEmpate, out Jugador ganador)
        {
            List<Carta> nuevaRonda = new List<Carta>();
            fraseEmpate = "Ha habido un empate entre ";
            ganador = null;

            foreach (Jugador j in ganadores.ToList())
            {
                if (!j.SeHaQuedadoSinCartas())
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
            return nuevaRonda;
        }

        private void EliminarJugadores(List<Jugador> jugadores)
        {
            Console.WriteLine("");
            foreach (Jugador j in jugadores.ToList())
            {
                Console.WriteLine(j.ToString() + " tiene " + j.NumeroDeCartas + " cartas.");
                if (j.SeHaQuedadoSinCartas())
                {
                    Console.WriteLine("Por lo tanto ha sido eliminado del juego.");
                    jugadores.Remove(j);
                }
            }
        }

    }
}
