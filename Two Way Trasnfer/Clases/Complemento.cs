﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Two_Way_Trasnfer.Clases
{
    [Serializable()]
    public class Complemento
    {
        [XmlElement("Pagos", Namespace= "http://www.sat.gob.mx/Pagos")]
        public Pago10Pagos Pagos { get; set; }
    }
}
