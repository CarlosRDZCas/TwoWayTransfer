using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases.CatalogoXML
{
    [XmlRoot("catalogocuentas", Namespace = "http://www.sat.gob.mx/esquemas/ContabilidadE/1_3/CatalogoCuentas")]
    public class catalogocuentas
    {
        [XmlElement(ElementName ="Catalogo")]
        public Catalogo Catalogo { get; set; }
    }
}
