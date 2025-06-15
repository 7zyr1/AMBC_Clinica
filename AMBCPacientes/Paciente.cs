using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBCPacientes
{
    internal class Paciente
    {
        public int id {  get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int dni {  get; set; }
        public int edad {  get; set; }
        public Calle calle { get; set; }
        public int numeracion {  get; set; }
        public int telefono { get; set; }
        public Paciente(int id, string nombre, string apellido, int dni, int edad, Calle calle, int numeracion, int telefono)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.edad = edad;
            this.calle = calle;
            this.numeracion = numeracion;
            this.telefono = telefono;
        }
    }
}
