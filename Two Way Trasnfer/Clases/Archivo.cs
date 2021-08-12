using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases
{
     public class Archivo
    {
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string GetName()
        {
            return Nombre;
        }
    }
}
