using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Seguros
    {
        [XmlAttribute("AseguraRespCivil", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string AseguraRespCivil { get; set; }
        [XmlAttribute("PolizaRespCivil", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string PolizaRespCivil { get; set; }
    }
}
