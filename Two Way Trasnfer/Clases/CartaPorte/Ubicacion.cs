using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Ubicacion
    {
        [XmlAttribute("DistanciaRecorrida", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public double DistanciaRecorrida { get; set; }
        [XmlAttribute("TipoEstacion", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string TipoEstacion { get; set; }
        [XmlAttribute("TipoUbicacion", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string TipoUbicacion { get; set; }
        [XmlAttribute("IDUbicacion", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string IDUbicacion { get; set; }
        [XmlAttribute("RFCRemitenteDestinatario", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string RFCRemitenteDestinatario { get; set; }
        [XmlAttribute("NombreRemitenteDestinatario", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string NombreRemitenteDestinatario { get; set; }
        [XmlAttribute("FechaHoraSalidaLlegada", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string FechaHoraSalidaLlegada { get; set; }     
        [XmlElement("Domicilio", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public Domicilio Domicilio { get; set; }
    }
}
