using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportesDiarios
{
    class Importes
    {
        public Importes()
        { 
        
        }
        public string Factura { get; set; }
        public DateTime Fecha { get; set; }
        public double EnergexNLD{ get; set; }
        public double EnergexDerr { get; set; }
        public double Orsan { get; set; }
        public double FCA { get; set; }
        public double CRE { get; set; }
        public double PemexNLD { get; set; }
        public double PemexDerr { get; set; }
        public double TipoCambio { get; set; }
        public double EstimuloIEPS { get; set; }
        public double EnergexNLDConsumo { get; set; }
        public double EnergexOrsanConsumo { get; set; }
        public double OrsanConsumo { get; set; }     
        public double Litros { get; set; }
        public double Importe { get; set; }
    }
}
