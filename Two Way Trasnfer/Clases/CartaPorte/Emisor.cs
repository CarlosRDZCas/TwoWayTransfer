using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Emisor
    {
        [XmlAttribute("Rfc")]
        public string Rfc { get; set; }
        [XmlAttribute("Nombre")]
        public string Nombre { get; set; }
        [XmlAttribute("RegimenFiscal")]
        public string RegimenFiscal { get; set; }        
    }
}
