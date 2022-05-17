using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases.FacturasProveedores
{
    public class Proveedor
    {

        public int Empresa { get; set; }
        public int NumeroProveedor { get; set; }
        public string Rfcreceptor { get; set; }
        public DateTime Fecha { get; set; }
        public double Debe { get; set; }
        public string Moneda { get; set; }
        public double TipoCambio { get; set; }
        public string Lugar { get; set; }
        public int Factura { get; set; }
        public int Reference { get; set; }
        public string Concepto { get; set; }
        public DateTime Vence { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string UUID { get; set; }
        public double ValorUnitario { get; set; }
        public double Iva { get; set; }
        public double Retencion { get; set; }
    }
}
