using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Impuestos
    {
        [XmlAttribute("TotalImpuestosRetenidos")]
        public double TotalImpuestosRetenidos { get; set; }
        [XmlAttribute("TotalImpuestosTrasladados")]
        public double TotalImpuestosTrasladados { get; set; }
        [XmlElement("Traslados")]
        public Traslados Traslados { get; set; }
        [XmlElement("Retenciones")]
        public Retenciones Retenciones { get; set; }
    }
}
