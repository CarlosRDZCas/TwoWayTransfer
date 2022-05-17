using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases
{
    [Serializable()]
    [XmlRoot(Namespace = "http://www.sat.gob.mx/cfd/4", ElementName = "Comprobante", DataType = "string", IsNullable = true)]
    public class Comprobante4 
    {
        private bool _seleccionar;

        public bool Seleccionar
        {
            get { return _seleccionar; }
            set { _seleccionar = value;
                

            }
        }

        public int ID { get; set; }
        [XmlAttribute("Serie")]
        public string Serie { get; set; } = "";
        [XmlAttribute("Folio")]
        public string Factura { get; set; } = ""; 
        [XmlAttribute( "Fecha")]
        public DateTime Fecha { get; set; }
        public int NumOrden { get; set; } = 0;
        public int NumRepExt { get; set; } = 0;
        [XmlElement("Emisor")]
        public Emisor Emisor { get; set; }
        [XmlElement("Conceptos")]
        public Concepto Concepto { get; set; }
        [XmlElement("Complemento")]
        public Complemento Complemento { get; set; }
        [XmlAttribute("SubTotal")]
        public double Subtotal { get; set; }
        [XmlElement("Impuestos")]
        public Impuesto Impuest { get; set; }
        public double IVA8 { get; set; }
        public double IVA16 { get; set; }
        public double Retencion { get; set; }
        public double IEPS { get; set; } = 0;
        [XmlAttribute("Total")]
        public double Total { get; set; }
        public string UUID { get; set; }

     
    }
}
