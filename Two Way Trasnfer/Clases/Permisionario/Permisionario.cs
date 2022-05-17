using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases.Permisionario
{
     public class Permisionario
    {
        public int ID { get; set; } = 0;
        public string RFCOperador { get; set; }
        public string NombreOperador { get; set; }
        public string NumLicencia { get; set; }
        public string NumRegIdTribOperador { get; set; }
        public string ResidenciaFiscalOperador { get; set; }
        public string Linea { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }

    }
}
