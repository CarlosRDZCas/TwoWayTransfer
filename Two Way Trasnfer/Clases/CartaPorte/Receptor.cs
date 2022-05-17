using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{   [Serializable()]
    public class Receptor
    {
        [XmlAttribute("Rfc")]
        public string Rfc { get; set; }
        [XmlAttribute("Nombre")]
        public string Nombre { get; set; }
        [XmlAttribute("UsoCFDI")]
        public string UsoCFDI { get; set; }
    }
}
