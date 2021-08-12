﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases
{
    [Serializable()]
    public class Traslado
    {
        [XmlAttribute("TasaOCuota")]
        public double Iva1 { get; set; }
        [XmlAttribute("Importe")]
        public double Importe { get; set; }
        [XmlAttribute("Impuesto")]
        public double Impuesto { get; set; }
    }
}
