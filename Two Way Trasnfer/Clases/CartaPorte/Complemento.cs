using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Complemento
    {
        [XmlElement("CartaPorte", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public CartaPorte CartaPorte { get; set; }
        [XmlElement("TimbreFiscalDigital", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public TimbreFiscalDigital TimbreFiscalDigital { get; set; }
    }
}
