using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Domicilio
    {
        [XmlAttribute("Calle", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Calle { get; set; }
        [XmlAttribute("Colonia", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Colonia { get; set; }
        [XmlAttribute("Localidad", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Localidad { get; set; }
        [XmlAttribute("Referencia", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Referencia { get; set; }
        [XmlAttribute("Municipio", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Municipio { get; set; }
        [XmlAttribute("Estado", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Estado { get; set; }
        [XmlAttribute("Pais", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Pais { get; set; }
        [XmlAttribute("CodigoPostal", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string CodigoPostal { get; set; }
        [XmlAttribute("NumeroExterior", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string NumeroExterior { get; set; }
        [XmlAttribute("NumeroInterior", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string NumeroInterior { get; set; }

    }
}
