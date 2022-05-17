using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Traslado
    {
        [XmlAttribute("Base")]
        public double Base { get; set; }
        [XmlAttribute("Impuesto")]
        public string Impuesto { get; set; }
        [XmlAttribute("TipoFactor")]
        public string TipoFactor { get; set; }
        [XmlAttribute("TasaOCuota")]
        public double TasaOCuota { get; set; }
        [XmlAttribute("Importe")]
        public double Importe { get; set; }
    }
}
