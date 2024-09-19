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
                        BatallaDeCartas bt = new BatallaDeCartas();
                        bt.JugarBatallaDeCartas();
                        break;
                    case 2:
                        Poker p = new Poker();
                        p.JugarPoker();
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
│  (1)  - Jugar batalla de cartas   │
│  (2)  - Jugar poker               │
│  (0)  - Salir                     │
└───────────────────────────────────┘
");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    option = -1;
                    Console.WriteLine("Opcion invalida");
                }

            } while (option < 0 || option > 2);

            return option;
        }
        public static int PedirNumero()
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
    public enum ePalo
    {
        espadas,
        bastos,
        oros,
        copas
    }

    public enum ePaloFrances
    {
        corazones,
        diamantes,
        picas,
        treboles
    }
}
