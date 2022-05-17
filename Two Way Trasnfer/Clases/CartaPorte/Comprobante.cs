using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    [XmlRoot(Namespace = "http://www.sat.gob.mx/cfd/3", ElementName = "Comprobante", DataType = "string", IsNullable = true)]
    public class Comprobante
    {
        [XmlAttribute("cfdi")]
        public string cfdi { get; set; }
        [XmlAttribute("cartaporte20")]
        public string cartaporte20 { get; set; }
        [XmlAttribute("xsi")]
        public string xsi { get; set; }
        [XmlAttribute("schemaLocation")]
        public string schemaLocation { get; set; }
        [XmlAttribute("Version")]
        public string Version { get; set; }
        [XmlAttribute("Folio")]
        public string Folio { get; set; }
        [XmlAttribute("Fecha")]
        public string Fecha { get; set; }
        [XmlAttribute("Sello")]
        public string Sello { get; set; }
        [XmlAttribute("FormaPago")]
        public string FormaPago { get; set; }
        [XmlAttribute("NoCertificado")]
        public string NoCertificado { get; set; }
        [XmlAttribute("SubTotal")]
        public double SubTotal { get; set; }
        [XmlAttribute("Moneda")]
        public string Moneda { get; set; }
        [XmlAttribute("TipoCambio")]
        public double TipoCambio { get; set; }
        [XmlAttribute("Total")]
        public double Total { get; set; }
        [XmlAttribute("TipoDeComprobante")]
        public string TipoDeComprobante { get; set; }
        [XmlAttribute("MetodoPago")]
        public string MetodoPago { get; set; }
        [XmlAttribute("LugarExpedicion")]
        public string LugarExpedicion { get; set; }
        [XmlElement("Emisor")]
        public Emisor Emisor { get; set; }
        [XmlElement("Receptor")]
        public Receptor Receptor { get; set; }
        [XmlElement("Conceptos")]
        public Conceptos Conceptos { get; set; }
        [XmlElement("Impuestos")]
        public Impuestos Impuestos{ get; set; }
        [XmlElement("Complemento")]
        public Complemento Complemento { get; set; }

    }
}
