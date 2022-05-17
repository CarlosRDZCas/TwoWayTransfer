using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases.FacturasEmitidas
{
    class FacturaEmitida 
    {
        [Name("Version CFDI")]
        public double VersionCFDI { get; set; }
        public string UUID { get; set; }
        public string Estatus { get; set; }
        [Name("Es Cancelable")]
        public string EsCancelable { get; set; }
        [Name("Estatus Cancelacion")]
        public string EstatusCancelacion { get; set; }
        [Name("Validacion EFOS")]
        public string ValidacionEFOS { get; set; }
        [Name("Tipo De Comprobante")]
        public string TipoComprobante { get; set; }
        [Name("Año")]
        public string Anio { get; set; }
        public string Mes { get; set; }
        public string Dia { get; set; }
        [Name("Fecha Emision")]
        public DateTime FechaEmision { get; set; }
        [Name("Fecha Timbrado")]
        public DateTime FechaTimbrado { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        [Name("Lugar Expedicion")]
        public string LugarExpedicion { get; set; }
        public string Confirmacion { get; set; }
        [Name("CFDi Relacionados")]
        public string CFDIRelacionados { get; set; }
        [Name("Forma Pago")]
        public string FormaPago { get; set; }
        [Name("Metodo Pago")]
        public string MetodoPago { get; set; }
        [Name("Condiciones De Pago")]
        public string CondicionesPago { get; set; }
        [Name("Tipo Cambio")]
        public string TipoCambio { get; set; }
        public string Moneda { get; set; }
        public double SubTotal { get; set; }
        public string Descuento { get; set; }
        public double Total { get; set; }
        [Name("Lista Negra")]
        public string ListaNegra { get; set; }
        public string Conceptos { get; set; }
        public string Exportacion { get; set; }
        [Name("Inf Global Periocidad")]
        public string InfGlobalPeriocidad { get; set; }
        [Name("Inf Global Meses")]
        public string InfGlobalMeses { get; set; }
        [Name("Inf Global Anio")]
        public string InfGlobalAnio { get; set; }
        [Name("RFC Emisor")]
        public string RFCEmisor { get; set; }
        [Name("Nombre Emisor")]
        public string NombreEmisor { get; set; }
        [Name("Regimen Fiscal Emisor")]
        public string RegimenFiscalEmisor { get; set; }
        [Name("Fac Atr Adquirente Emisor")]
        public string FacAtrAdquirenteEmisor { get; set; }
        [Name("RFC Receptor")]
        public string RFCReceptor { get; set; }
        [Name("Nombre Receptor")]
        public string NombreReceptor { get; set; }
        [Name("Domicilio Fiscal Receptor")]
        public string DomicilioFisclaReceptor { get; set; }
        [Name("Residencia Fiscal Receptor")]
        public string ResidenciaFiscalReceptor { get; set; }
        public string NumRegIdTrib { get; set; }
        [Name("Regimen Fiscal Receptor")]
        public string RegimenFiscalReceptor { get; set; }
        [Name("Uso CFDI Receptor")]
        public string UsoCFDIReceptor { get; set; }
        [Name("ISR Retenido")]
        public string ISRRetenido { get; set; }
        [Name("ISR Trasladado")]
        public string ISRTrasladado { get; set; }
        [Name("IVA Retenido Global")]
        public double IVARetenidoGlobal { get; set; }
        [Name("IVA Retenido 6%")]
        public double IVARetenido6 { get; set; }
        [Name("IVA Trasladado 16%")]
        public double IVATrasladado16 { get; set; }
        [Name("IVA Trasladado 8%")]
        public double IVATrasladado8 { get; set; }
        [Name("IVA Exento")]
        public string IVAExento { get; set; }
        [Name("Base IVA Exento")]
        public string BaseIVAExento { get; set; }
        [Name("IVA Tasa Cero")]
        public string IVATasaCero { get; set; }
        [Name("Base IVA Tasa Cero")]
        public string BaseIVATasaCero { get; set; }
        [Name("IEPS Retenido (Tasa)")]
        public string IEPSRetenidoTasa { get; set; }
        [Name("IEPS Trasladado (Tasa)")]
        public string IEPSTrasladadoTasa { get; set; }
        [Name("IEPS Retenido (Cuota)")]
        public string IEPSRetenidoCuota { get; set; }
        [Name("IEPS Trasladado (Cuota)")]
        public string IEPSTrasladadoCuota { get; set; }
        [Name("Total Impuestos Retenidos")]
        public string TotalImpuestosRetenidos { get; set; }
        [Name("Total Impuestos Trasladados")]
        public string TotalImpuestosTrasladados { get; set; }
        [Name("Total Retenciones Locales")]
        public string TotalRetencionesLocales { get; set; }
        [Name("Total Traslados Locales")]
        public string TotalTrasladosLocales { get; set; }
        [Name("Impuesto Local Retenido")]
        public string ImpuestoLocalRetenido { get; set; }
        [Name("Tasa de Retención (Local)")]
        public string TasaRetencionLocal { get; set; }
        [Name("Importe de Retención (Local)")]
        public string ImporteRetencionLocal { get; set; }
        [Name("Impuesto Local Trasladado")]
        public string ImpuestoLocalTrasladado { get; set; }
        [Name("Tasa de Traslado (Local)")]
        public string TasaTrasladoLocal { get; set; }
        [Name("Importe de Traslado (Local)")]
        public string ImporteTrasladoLocal { get; set; }
        [Name("PAC Certifico")]
        public string PACCertifico { get; set; }
        [Name("No Certificado")]
        public string NOCertificado { get; set; }       
        public string Certificado { get; set; }
        [Name("Ruta del XML")]
        public string RutaXML { get; set; }

    }
}
