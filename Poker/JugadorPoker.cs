using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class JugadorPoker : Jugador
    {
        public int Cuenta { get; set; }
        public int ApuestaActual { get; set; }

        public JugadorPoker(int id, int cuenta) : base(id)
        {
            Cuenta = cuenta;
        }

        public string Mano()
        {
            string frase = "";
            foreach (Carta carta in mano)
                frase += carta.ToString() + " ";
            return frase;
        }
    }
}
