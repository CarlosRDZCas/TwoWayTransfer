
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases
{
    public class Class1
    {
        // NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/3", IsNullable = false)]
        public partial class Comprobante
        {

            private ComprobanteEmisor emisorField;

            private ComprobanteReceptor receptorField;

            private ComprobanteConceptos conceptosField;

            private ComprobanteImpuestos impuestosField;

            private ComprobanteComplemento complementoField;

            private string[] textField;

            private uint? lugarExpedicionField;

            private string metodoPagoField;

            private string tipoDeComprobanteField;

            private decimal totalField;

            private string monedaField;

            private decimal subTotalField;

            private string certificadoField;

            private ulong noCertificadoField;

            private byte formaPagoField;

            private string selloField;

            private System.DateTime fechaField;

            private ushort folioField;
            private string serieField;

            public string Serie
            {
                get { return this.serieField; }
                set { this.serieField = value; }
            }


            private decimal versionField;

            /// <remarks/>
            public ComprobanteEmisor Emisor
            {
                get
                {
                    return this.emisorField;
                }
                set
                {
                    this.emisorField = value;
                }
            }

            /// <remarks/>
            public ComprobanteReceptor Receptor
            {
                get
                {
                    return this.receptorField;
                }
                set
                {
                    this.receptorField = value;
                }
            }

            /// <remarks/>
            public ComprobanteConceptos Conceptos
            {
                get
                {
                    return this.conceptosField;
                }
                set
                {
                    this.conceptosField = value;
                }
            }

            /// <remarks/>
            public ComprobanteImpuestos Impuestos
            {
                get
                {
                    return this.impuestosField;
                }
                set
                {
                    this.impuestosField = value;
                }
            }

            /// <remarks/>
            public ComprobanteComplemento Complemento
            {
                get
                {
                    return this.complementoField;
                }
                set
                {
                    this.complementoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public uint? LugarExpedicion
            {
                get
                {
                    return this.lugarExpedicionField;
                }
                set
                {
                    this.lugarExpedicionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string MetodoPago
            {
                get
                {
                    return this.metodoPagoField;
                }
                set
                {
                    this.metodoPagoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string TipoDeComprobante
            {
                get
                {
                    return this.tipoDeComprobanteField;
                }
                set
                {
                    this.tipoDeComprobanteField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Total
            {
                get
                {
                    return this.totalField;
                }
                set
                {
                    this.totalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Moneda
            {
                get
                {
                    return this.monedaField;
                }
                set
                {
                    this.monedaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal SubTotal
            {
                get
                {
                    return this.subTotalField;
                }
                set
                {
                    this.subTotalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Certificado
            {
                get
                {
                    return this.certificadoField;
                }
                set
                {
                    this.certificadoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ulong NoCertificado
            {
                get
                {
                    return this.noCertificadoField;
                }
                set
                {
                    this.noCertificadoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte FormaPago
            {
                get
                {
                    return this.formaPagoField;
                }
                set
                {
                    this.formaPagoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Sello
            {
                get
                {
                    return this.selloField;
                }
                set
                {
                    this.selloField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime Fecha
            {
                get
                {
                    return this.fechaField;
                }
                set
                {
                    this.fechaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort Folio
            {
                get
                {
                    return this.folioField;
                }
                set
                {
                    this.folioField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteEmisor
        {

            private ushort regimenFiscalField;

            private string nombreField;

            private string rfcField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort RegimenFiscal
            {
                get
                {
                    return this.regimenFiscalField;
                }
                set
                {
                    this.regimenFiscalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Nombre
            {
                get
                {
                    return this.nombreField;
                }
                set
                {
                    this.nombreField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Rfc
            {
                get
                {
                    return this.rfcField;
                }
                set
                {
                    this.rfcField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteReceptor
        {

            private string nombreField;

            private string rfcField;

            private string usoCFDIField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Nombre
            {
                get
                {
                    return this.nombreField;
                }
                set
                {
                    this.nombreField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Rfc
            {
                get
                {
                    return this.rfcField;
                }
                set
                {
                    this.rfcField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string UsoCFDI
            {
                get
                {
                    return this.usoCFDIField;
                }
                set
                {
                    this.usoCFDIField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteConceptos
        {

            private ComprobanteConceptosConcepto[] conceptoField;

            private string[] textField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Concepto")]
            public ComprobanteConceptosConcepto[] Concepto
            {
                get
                {
                    return this.conceptoField;
                }
                set
                {
                    this.conceptoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteConceptosConcepto
        {

            private ComprobanteConceptosConceptoImpuestos impuestosField;

            private string[] textField;

            private decimal importeField;

            private decimal valorUnitarioField;

            private string descripcionField;

            private byte cantidadField;

            private byte noIdentificacionField;

            private uint? claveProdServField;

            private string claveUnidadField;

            /// <remarks/>
            public ComprobanteConceptosConceptoImpuestos Impuestos
            {
                get
                {
                    return this.impuestosField;
                }
                set
                {
                    this.impuestosField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Importe
            {
                get
                {
                    return this.importeField;
                }
                set
                {
                    this.importeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal ValorUnitario
            {
                get
                {
                    return this.valorUnitarioField;
                }
                set
                {
                    this.valorUnitarioField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Descripcion
            {
                get
                {
                    return this.descripcionField;
                }
                set
                {
                    this.descripcionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Cantidad
            {
                get
                {
                    return this.cantidadField;
                }
                set
                {
                    this.cantidadField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte NoIdentificacion
            {
                get
                {
                    return this.noIdentificacionField;
                }
                set
                {
                    this.noIdentificacionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public uint? ClaveProdServ
            {
                get
                {
                    return this.claveProdServField;
                }
                set
                {
                    this.claveProdServField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ClaveUnidad
            {
                get
                {
                    return this.claveUnidadField;
                }
                set
                {
                    this.claveUnidadField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteConceptosConceptoImpuestos
        {

            private ComprobanteConceptosConceptoImpuestosTraslados trasladosField;

            private string[] textField;

            /// <remarks/>
            public ComprobanteConceptosConceptoImpuestosTraslados Traslados
            {
                get
                {
                    return this.trasladosField;
                }
                set
                {
                    this.trasladosField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteConceptosConceptoImpuestosTraslados
        {

            private ComprobanteConceptosConceptoImpuestosTrasladosTraslado trasladoField;

            /// <remarks/>
            public ComprobanteConceptosConceptoImpuestosTrasladosTraslado Traslado
            {
                get
                {
                    return this.trasladoField;
                }
                set
                {
                    this.trasladoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteConceptosConceptoImpuestosTrasladosTraslado
        {

            private decimal importeField;

            private decimal tasaOCuotaField;

            private string tipoFactorField;

            private byte impuestoField;

            private decimal baseField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Importe
            {
                get
                {
                    return this.importeField;
                }
                set
                {
                    this.importeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal TasaOCuota
            {
                get
                {
                    return this.tasaOCuotaField;
                }
                set
                {
                    this.tasaOCuotaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string TipoFactor
            {
                get
                {
                    return this.tipoFactorField;
                }
                set
                {
                    this.tipoFactorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Impuesto
            {
                get
                {
                    return this.impuestoField;
                }
                set
                {
                    this.impuestoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Base
            {
                get
                {
                    return this.baseField;
                }
                set
                {
                    this.baseField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteImpuestos
        {

            private ComprobanteImpuestosTraslados trasladosField;

            private string[] textField;

            private decimal totalImpuestosTrasladadosField;

            /// <remarks/>
            public ComprobanteImpuestosTraslados Traslados
            {
                get
                {
                    return this.trasladosField;
                }
                set
                {
                    this.trasladosField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal TotalImpuestosTrasladados
            {
                get
                {
                    return this.totalImpuestosTrasladadosField;
                }
                set
                {
                    this.totalImpuestosTrasladadosField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteImpuestosTraslados
        {

            private ComprobanteImpuestosTrasladosTraslado trasladoField;

            /// <remarks/>
            public ComprobanteImpuestosTrasladosTraslado Traslado
            {
                get
                {
                    return this.trasladoField;
                }
                set
                {
                    this.trasladoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteImpuestosTrasladosTraslado
        {

            private decimal importeField;

            private decimal tasaOCuotaField;

            private string tipoFactorField;

            private byte impuestoField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Importe
            {
                get
                {
                    return this.importeField;
                }
                set
                {
                    this.importeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal TasaOCuota
            {
                get
                {
                    return this.tasaOCuotaField;
                }
                set
                {
                    this.tasaOCuotaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string TipoFactor
            {
                get
                {
                    return this.tipoFactorField;
                }
                set
                {
                    this.tipoFactorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Impuesto
            {
                get
                {
                    return this.impuestoField;
                }
                set
                {
                    this.impuestoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
        public partial class ComprobanteComplemento
        {

            private TimbreFiscalDigital timbreFiscalDigitalField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
            public TimbreFiscalDigital TimbreFiscalDigital
            {
                get
                {
                    return this.timbreFiscalDigitalField;
                }
                set
                {
                    this.timbreFiscalDigitalField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital", IsNullable = false)]
        public partial class TimbreFiscalDigital
        {

            private decimal versionField;

            private string selloSATField;

            private ulong noCertificadoSATField;

            private string selloCFDField;

            private System.DateTime fechaTimbradoField;

            private string uUIDField;

            private string rfcProvCertifField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string SelloSAT
            {
                get
                {
                    return this.selloSATField;
                }
                set
                {
                    this.selloSATField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ulong NoCertificadoSAT
            {
                get
                {
                    return this.noCertificadoSATField;
                }
                set
                {
                    this.noCertificadoSATField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string SelloCFD
            {
                get
                {
                    return this.selloCFDField;
                }
                set
                {
                    this.selloCFDField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime FechaTimbrado
            {
                get
                {
                    return this.fechaTimbradoField;
                }
                set
                {
                    this.fechaTimbradoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string UUID
            {
                get
                {
                    return this.uUIDField;
                }
                set
                {
                    this.uUIDField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RfcProvCertif
            {
                get
                {
                    return this.rfcProvCertifField;
                }
                set
                {
                    this.rfcProvCertifField = value;
                }
            }
        }


    }
}
