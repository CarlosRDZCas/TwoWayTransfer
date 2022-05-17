using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Remolques
    {
        [XmlElement("Remolque", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public List<Remolque> Remolque { get; set; }
    }
}
