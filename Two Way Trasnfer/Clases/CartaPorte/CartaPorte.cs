using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class CartaPorte
    {
        [XmlAttribute("Version", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Version { get; set; }
        [XmlAttribute("TranspInternac", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string TranspInternac { get; set; }
        [XmlAttribute("TotalDistRec", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string TotalDistRec { get; set; }
        [XmlElement("Ubicaciones", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public Ubicaciones Ubicaciones { get; set; }
        [XmlElement("Mercancias", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public Mercancias Mercancias { get; set; }
        [XmlElement("FiguraTransporte", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public FiguraTransporte FiguraTransporte { get; set; }
    }
}
