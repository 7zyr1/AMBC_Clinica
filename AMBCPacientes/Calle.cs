using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBCPacientes
{
    public class Calle
    {
        public int id_calle { get; set; }
        public string nombre { get; set; }
        public Calle(int id, string nombre)
        {
            this.id_calle = id;
            this.nombre = nombre;
        }
        public override string ToString()
        {
            return $"{nombre}";
        }
    }
}