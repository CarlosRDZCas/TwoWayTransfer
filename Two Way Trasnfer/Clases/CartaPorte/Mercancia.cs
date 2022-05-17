using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Mercancia
    {
        [XmlAttribute("BienesTransp", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string BienesTransp { get; set; }
        [XmlAttribute("Descripcion", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Descripcion { get; set; }
        [XmlAttribute("Cantidad", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public double Cantidad { get; set; }
        [XmlAttribute("ClaveUnidad", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string ClaveUnidad { get; set; }
        [XmlAttribute("Unidad", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Unidad { get; set; }
        [XmlAttribute("DescripEmbalaje", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string DescripEmbalaje { get; set; }
        [XmlAttribute("PesoEnKg", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public double PesoEnKg { get; set; }
        [XmlAttribute("Moneda", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string Moneda { get; set; }
        [XmlAttribute("MaterialPeligroso", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public string MaterialPeligroso { get; set; }
    }

}
