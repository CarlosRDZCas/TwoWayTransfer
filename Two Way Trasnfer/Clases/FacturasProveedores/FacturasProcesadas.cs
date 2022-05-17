using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases.FacturasProveedores
{
    public class FacturasProcesadas
    {
        public string Fecha { get; set; }
        public string Empresa { get; set; }
        public int NumeroProveedor { get; set; }
        public string Proveedor { get; set; }
        public int Factura { get; set; }
        public double Importe { get; set; }
        public string Vencimiento { get; set; }
        // public string Remision { get; set; }
        public string Poliza { get; set; }


    }
}
