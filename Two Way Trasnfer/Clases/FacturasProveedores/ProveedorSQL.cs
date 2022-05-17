using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases.FacturasProveedores
{
    public class ProveedorSQL
    {
        public bool Procesar { get; set; }
        public int LogID { get; set; }
        public int Numero { get; set; }
        public string Proveedor { get; set; }
        public string RFC { get; set; }
        public int DiasCredito { get; set; }
        public string Tipo { get; set; } //
        public int Factura { get; set; }
        public double Importe { get; set; }
        public DateTime Fecha { get; set; }
        public string Ruta { get; set; }
        public string Usuario { get; set; }
        public string PDF { get; set; }
        public DateTime FechaPDF { get; set; }
        public string XML { get; set; }
        public DateTime FechaXML { get; set; }
        public string Soporte { get; set; }
        public DateTime FechaSoporte { get; set; }
        public string Correo { get; set; }//
        public int Cuenta { get; set; }//
        public int SubCuenta { get; set; }//
        public int SubSub { get; set; }//

    }
}
