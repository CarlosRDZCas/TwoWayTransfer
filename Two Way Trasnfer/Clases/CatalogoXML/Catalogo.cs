using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CatalogoXML
{
    [XmlRoot("Catalogo",Namespace = "http://www.sat.gob.mx/esquemas/ContabilidadE/1_3/CatalogoCuentas")]
    public class Catalogo
    {
        [XmlAttribute]
        public int Anio { get; set; }
        [XmlAttribute]
        public string Mes { get; set; }
        [XmlAttribute]
        public string RFC { get; set; }
        [XmlAttribute]
        public string Version { get; set; }
        [XmlAttribute(Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation { get; set; } = "http://www.sat.gob.mx/esquemas/ContabilidadE/1_3/CatalogoCuentas http://www.sat.gob.mx/esquemas/ContabilidadE/1_3/CatalogoCuentas/CatalogoCuentas_1_3.xsd";
        [XmlElement]
        public List<Ctas> Ctas { get; set; }
    }
}
