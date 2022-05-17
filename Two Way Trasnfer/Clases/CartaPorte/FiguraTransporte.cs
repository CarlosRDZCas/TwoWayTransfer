using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CartaPorte
{
    [Serializable()]
    public class FiguraTransporte
    {
        [XmlElement("TiposFigura", Namespace = "http://www.sat.gob.mx/CartaPorte20")]
        public TiposFigura TiposFigura { get; set; }
    }
}
