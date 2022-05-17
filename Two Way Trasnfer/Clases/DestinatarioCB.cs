using NServiceBus.Testing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases
{
    class DestinatarioCB
    {
        public string Destinatario { get; set; }
        public int DestinatarioID { get; set; }
        public string Referencia { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string CiudadDest { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }


        private string _nombreCompleto;

        public string NombreCompleto
        {
            get { return _nombreCompleto; }
            set { _nombreCompleto = Destinatario.Trim()+" - "+Referencia.Trim() + " - "+Estado.Trim() + " - "+Municipio.Trim(); }
        }

        public string NombreYNumDestinatario
        {

            get
            {

                return Destinatario;
            }
        }
        public int NumeroDestinatario
        {
            get
            {
                return DestinatarioID;

            }


        }

    }
}
