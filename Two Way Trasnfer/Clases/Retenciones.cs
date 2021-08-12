using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases
{
    [Serializable()]
    public class Retenciones
    {
        [XmlElement("Retencion")]
        public Retencion retencion { get; set; }
    }
}
