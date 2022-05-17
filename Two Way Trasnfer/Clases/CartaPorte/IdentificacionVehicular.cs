using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class IdentificacionVehicular
    {
        [XmlAttribute("ConfigVehicular", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string ConfigVehicular { get; set; }
        [XmlAttribute("PlacaVM", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string PlacaVM { get; set; }
        [XmlAttribute("AnioModeloVM", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public int AnioModeloVM { get; set; }
        
    }
}
