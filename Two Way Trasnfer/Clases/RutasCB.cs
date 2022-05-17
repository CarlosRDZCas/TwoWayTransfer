using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases
{
    class RutasCB
    {
        public string Ruta { get; set; }
        public int Kilometros { get; set; }

        public string NombreRuta
        {

            get
            {

                return Ruta;
            }
        }
    }
}
