using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Two_Way_Trasnfer.Clases.CartaPorte;

namespace Two_Way_Trasnfer.Clases
{
    class XPOCarta
    {
        public string UUID { get; set; }
        public int Remision { get; set; } = 0; // ubi
        public int Liquidacion { get; set; } = 0;
        public int Log { get; set; }
        public string Carro { get; set; }
        public string Remolque { get; set; }
        public string Cliente { get; set; }
        public string Ruta { get; set; }
        public int Kilometros { get; set; } // ubi
        public DateTime Fecha { get; set; }
        public string RFCReceptor { get; set; }
        public string RFCEmisor { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }
        public string Moneda { get; set; }
        public string FormaPago { get; set; }
        public string ClaveProdServ { get; set; }
        public double Cantidad { get; set; }
        public string ClaveUnidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        public double ValorUnitario { get; set; }
        public double Importe { get; set; }
        public double TasaOCuotaTraslado { get; set; }
        public double ImporteTraslado { get; set; }
        public double TasaOCuotaRetencion { get; set; }
        public double ImporteRetencion { get; set; }
        //Mercancias
        public double PesoBrutoTotal { get; set; }
        //Mercancia
        public List<Mercancia> Mercancia { get; set; }
        //origen
        public string IDOrigen { get; set; } // ubi
        public string TipoUbicacionRemitente { get; set; }
        public string TipoEstacionRemitente { get; set; }
        public DateTime FechaSalida { get; set; } // ubi
        public string NombreRemitente { get; set; } // ubi
        public string RFCRemitente { get; set; } // ubi
        public string CodigoPostalRemitente { get; set; } // ubi
        public string ReferenciaRemitente { get; set; } // ubi
        public string CalleRemitente { get; set; }       // ubi
        public string LocalidadRemitente { get; set; } // ubi
        public string MunicipioRemitente { get; set; } // ubi
        public string EstadoRemitente { get; set; }  // ubi   
        public string ColoniaRemitente { get; set; } // ubi
        public string PaisRemitente { get; set; } // ubi
        public string NumeroInteriorRemitente { get; set; }
        public string NumeroExteriorRemitente { get; set; }
        //Destino
        public string IDDestino { get; set; } // ubi
        public string TipoEstacionDestinatario { get; set; }
        public string TipoUbicacionDestinatario { get; set; }
        public DateTime FechaLlegada { get; set; } // ubi
        public string NombreDestinatario { get; set; } // ubi
        public string RFCDestinatario { get; set; } // ubi
        public string CodigoPostalDestinatario { get; set; } // ubi
        public string ReferenciaDestinatario { get; set; } // ubi
        public string CalleDestinatario { get; set; } // ubi
        public string LocalidadDestinatario { get; set; } // ubi
        public string MunicipioDestinatario { get; set; } // ubi
        public string EstadoDestinatario { get; set; } // ubi
        public string ColoniaDestinatario { get; set; } // ubi
        public string PaisDestinatario { get; set; } // ubi
        public string NumeroInteriorDestinatario { get; set; }
        public string NumeroExteriorDestinatario { get; set; }
        //Vehiculo y remolque
        public string PlacaVM { get; set; }
        public string RemolquePlaca { get; set; }
        //Figura Transp
        public string NombreFigura { get; set; }
        public string RFCFigura { get; set; }


    }
}
