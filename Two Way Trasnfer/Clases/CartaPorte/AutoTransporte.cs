using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class AutoTransporte
    {
        [XmlAttribute("PermSCT", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string PermSCT { get; set; }
        [XmlAttribute("NumPermisoSCT", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string NumPermisoSCT { get; set; }
        [XmlElement("IdentificacionVehicular", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public IdentificacionVehicular IdentificacionVehicular { get; set; }
        [XmlElement("Seguros", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public Seguros Seguros { get; set; }
        [XmlElement("Remolques", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public Remolques Remolques { get; set; }
    }
}
