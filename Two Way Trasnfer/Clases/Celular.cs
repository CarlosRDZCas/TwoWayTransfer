using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases
{
    class Celular
    {
        public int ID { get; set; }
        public string Empleado { get; set; }
        public string NombreCelular { get; set; }
        public string Departamento { get; set; }
        public string Puesto { get; set; }
        public string Serie { get; set; }
        public string Equipo { get; set; }
        public string Descripcion { get; set; }
        public string Compañia { get; set; }
        public string NumTel { get; set; }
        public bool Asignado { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public double Costo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public DateTime? FechaRetiro { get; set; }

    }
}
