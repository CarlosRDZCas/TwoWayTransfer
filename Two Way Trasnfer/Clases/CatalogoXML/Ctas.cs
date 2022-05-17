using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CatalogoXML
{
    [XmlRoot("Ctas", Namespace = "http://www.sat.gob.mx/esquemas/ContabilidadE/1_3/CatalogoCuentas")]
    public class Ctas
    {
        [XmlAttribute]
        public string Natur { get; set; }
        [XmlAttribute]
        public int Nivel { get; set; }
        [XmlAttribute]
        public string Desc { get; set; }
        [XmlAttribute]
        public string NumCta { get; set; }
        [XmlAttribute]
        public string SubCtaDe { get; set; }
        [XmlAttribute]
        public string CodAgrup { get; set; }

    }
}
