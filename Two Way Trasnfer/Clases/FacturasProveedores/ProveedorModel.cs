using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases.FacturasProveedores
{
    public class ProveedorModel
    {
        public bool Procesar { get; set; }
        public int Log { get; set; }
        public string Proveedor { get; set; }
        public string RFC { get; set; }
        public string Factura { get; set; }
        public double Importe { get; set; }
        public DateTime Fecha { get; set; }
        public string Ruta { get; set; }
        public string Usuario { get; set; }
        public string PDF { get; set; }
        public string XML { get; set; }
        public string Soporte { get; set; }
        public string RFCReceptor { get; set; }
    }
}
