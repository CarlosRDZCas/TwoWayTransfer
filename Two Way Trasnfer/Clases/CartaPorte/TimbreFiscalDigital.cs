using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class TimbreFiscalDigital
    {
        [XmlAttribute("Version", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public string Version { get; set; }
        [XmlAttribute("UUID", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public string UUID { get; set; }
        [XmlAttribute("FechaTimbrado", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public string FechaTimbrado { get; set; }
        [XmlAttribute("RfcProvCertif", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public string RfcProvCertif { get; set; }
        [XmlAttribute("SelloCFD", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public string SelloCFD { get; set; }
        [XmlAttribute("NoCertificadoSAT", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public string NoCertificadoSAT { get; set; }
        [XmlAttribute("SelloSAT", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public string SelloSAT { get; set; }
    }
}
