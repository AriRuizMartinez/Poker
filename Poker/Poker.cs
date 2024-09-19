using System;
using System.Collections.Generic;

namespace Poker
{
    public class Poker
    {
        public Poker() { }

        public void JugarPoker()
        {
            List<JugadorPoker> jugadores = InicializarJugadores();

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

                RondaDeApuestas(jugadores);

            }
            Console.WriteLine("");
            Console.WriteLine(jugadores[0].ToString() + " ha ganado el juego!!! :)");
            Console.WriteLine("");

        }

        private void RondaDeApuestas(List<JugadorPoker> jugadores)
        {
            int apuestaMasAlta = 0;
            int totalApostado = 0;

            jugadores[0].ApuestaActual = OpcionesJugador();

            apuestaMasAlta += jugadores[0].ApuestaActual;
            totalApostado += jugadores[0].ApuestaActual;

            ApuestasNPCS(jugadores, apuestaMasAlta, totalApostado, out apuestaMasAlta, out totalApostado);
        }

        private void ApuestasNPCS(List<JugadorPoker> jugadores, int apuestaMasAltaRead, int totalApostadoRead, out int apuestaMasAlta, out int totalApostado)
        {
            foreach (JugadorPoker jugador in jugadores)
            {
                ControlarApuestaIndividual(jugador, apuestaMasAltaRead, totalApostadoRead, out apuestaMasAltaRead, out totalApostadoRead);
            }

            totalApostado = totalApostadoRead;
            apuestaMasAlta = apuestaMasAltaRead;
        }

        private void ControlarApuestaIndividual(JugadorPoker jugador, int apuestaMasAltaRead, int totalApostadoRead, out int apuestaMasAlta, out int totalApostado)
        {
            apuestaMasAlta = apuestaMasAltaRead;
            totalApostado = totalApostadoRead;
            if (jugador.Id == 0)
                return;

            if (!jugador.Jugando)
                return;

            if (jugador.Cuenta < apuestaMasAltaRead - jugador.ApuestaActual)
                return;

            Random r = new Random();

            if (jugador.ApuestaActual < apuestaMasAltaRead)
            {
                OpcionesConApuestaMasBaja(jugador, r, apuestaMasAltaRead, totalApostadoRead, out apuestaMasAltaRead, out totalApostadoRead);
            }
            else
            {
                OpcionesConLaApuestaMasAlta(jugador, r, apuestaMasAltaRead, totalApostadoRead, out apuestaMasAltaRead, out totalApostadoRead);
            }
            apuestaMasAlta = apuestaMasAltaRead;
            totalApostado = totalApostadoRead;
        }

        private void OpcionesConLaApuestaMasAlta(JugadorPoker jugador, Random r, int apuestaMasAltaRead, int totalApostadoRead, out int apuestaMasAlta, out int totalApostado)
        {
            apuestaMasAlta = apuestaMasAltaRead;
            totalApostado = totalApostadoRead;
            if (jugador.Cuenta == 0)
                return;
            
            switch (r.Next(0, 2))
            {
                case 0:
                    //No hacemos nada porque el NPC decide no subir la apuesta
                    break;
                case 1:
                    int incrementoApuesta = r.Next(0, jugador.Cuenta + 1);
                    jugador.Cuenta -= incrementoApuesta;
                    jugador.ApuestaActual += incrementoApuesta;
                    apuestaMasAltaRead += incrementoApuesta;
                    break;
                default:
                    break;
            }
            apuestaMasAlta = apuestaMasAltaRead;
            totalApostado = totalApostadoRead;
        }

        private void OpcionesConApuestaMasBaja(JugadorPoker jugador, Random r, int apuestaMasAltaRead, int totalApostadoRead, out int apuestaMasAlta, out int totalApostado)
        {
            apuestaMasAlta = apuestaMasAltaRead;
            totalApostado = totalApostadoRead;
            if (jugador.Cuenta < apuestaMasAltaRead - jugador.ApuestaActual)
            {
                jugador.Jugando = false;
                return;
            }
            switch (r.Next(0, 3))
            {
                case 0:
                    jugador.Jugando = false;
                    break;
                case 1:
                    jugador.Cuenta -= apuestaMasAltaRead - jugador.ApuestaActual;
                    jugador.ApuestaActual = apuestaMasAltaRead;
                    break;
                case 2:
                    int incrementoApuesta = r.Next(0, jugador.Cuenta + 1);
                    jugador.Cuenta -= incrementoApuesta;
                    jugador.ApuestaActual += incrementoApuesta;
                    apuestaMasAltaRead += incrementoApuesta;
                    break;
                default:
                    break;
            }
            apuestaMasAlta = apuestaMasAltaRead;
            totalApostado = totalApostadoRead;
        }

        private int Apostar()
        {
            int apuesta = -1;
            while(apuesta < 0) 
            {
                Console.WriteLine("Cuanto quieres apostar?");
                int.TryParse(Console.ReadLine(), out apuesta);
            }
            return apuesta;
        }

        private List<JugadorPoker> InicializarJugadores()
        {
            List<JugadorPoker> jugadores = new List<JugadorPoker>();

            int numeroJugadores = Program.PedirNumero();

            for (int i = 0; i < numeroJugadores; i++)
                jugadores.Add(new JugadorPoker(i, 1000));

            Console.WriteLine("Todos los jugadores empezais con 1000$");

            return jugadores;
        }

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

            if (option == 2)
                return Apostar();

            return 0;
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
