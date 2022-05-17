using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Concepto
    {
        [XmlAttribute("ClaveProdServ")]
        public string ClaveProdServ { get; set; }
        [XmlAttribute("Cantidad")]
        public double Cantidad { get; set; }
        [XmlAttribute("ClaveUnidad")]
        public string ClaveUnidad { get; set; }
        [XmlAttribute("Unidad")]
        public string Unidad { get; set; }
        [XmlAttribute("Descripcion")]
        public string Descripcion { get; set; }
        [XmlAttribute("ValorUnitario")]
        public double ValorUnitario { get; set; }
        [XmlAttribute("Importe")]
        public double Importe { get; set; }
        [XmlElement("Impuestos")]
        public Impuestos Impuestos { get; set; }
    
    }
}
