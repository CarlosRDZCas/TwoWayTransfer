using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class TiposFigura
    {
        [XmlAttribute("TipoFigura", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string TipoFigura { get; set; }
        [XmlAttribute("RFCFigura", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string RFCFigura { get; set; }
        [XmlAttribute("NumLicencia", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string NumLicencia { get; set; }
        [XmlAttribute("NombreFigura", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string NombreFigura { get; set; }
    }
}
