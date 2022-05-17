using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Mercancias
    {
        [XmlAttribute("PesoBrutoTotal", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public double PesoBrutoTotal { get; set; }
        [XmlAttribute("UnidadPeso", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string UnidadPeso { get; set; }
        [XmlAttribute("PesoNetoTotal", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public double PesoNetoTotal { get; set; }
        [XmlAttribute("NumTotalMercancias", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public double NumTotalMercancias { get; set; }
        [XmlElement("Mercancia", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public List<Mercancia> Mercancia { get; set; }
        [XmlElement("Autotransporte", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public AutoTransporte AutoTransporte { get; set; }
    }
}
