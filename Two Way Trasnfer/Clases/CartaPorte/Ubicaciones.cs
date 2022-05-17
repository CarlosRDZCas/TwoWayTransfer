using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class Ubicaciones
    {
        [XmlElement("Ubicacion", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public List<Ubicacion> Ubicacion { get; set; }
    }
}
