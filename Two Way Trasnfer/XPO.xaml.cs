using iTextSharp.text;
using iTextSharp.text.pdf;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Two_Way_Trasnfer.Clases;
using Two_Way_Trasnfer.Clases.CartaPorte;
using Comprobante = Two_Way_Trasnfer.Clases.CartaPorte.Comprobante;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para XPO.xaml
    /// </summary>
    public partial class XPO : MetroWindow
    {
       // string connectionstringTest = "Data Source=TWL; Database=Desarrollo; Initial Catalog=Desarrollo ;User ID=sa; Password = Tw*way2408";
        string connectionstring = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        List<XPOCarta> xPOCartas = new List<XPOCarta>();
        public string Usuario { get; set; }

        public XPO()
        {
            InitializeComponent();
        }

        public XPO(string usuario)
        {
            InitializeComponent();
            Usuario = usuario;
            LlenarDGV();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
                txtXML.Text = openFileDialog.FileName;
        }

        public void LlenarDGV()
        {
            dgvDatos.ItemsSource = null;
            xPOCartas.Clear();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_selectCartasXPO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    XPOCarta xpo = new XPOCarta();
                    xpo.UUID = reader["UUID"].ToString();
                    xpo.Remision = Convert.ToInt32(reader["Remision"].ToString());
                    xpo.Liquidacion = Convert.ToInt32(reader["Liquidacion"].ToString());
                    xpo.Subtotal = Convert.ToDouble(reader["Subtotal"].ToString());
                    xpo.Moneda = reader["Moneda"].ToString();
                    xpo.Total = Convert.ToDouble(reader["Total"].ToString());
                    xpo.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                    xpo.RFCEmisor = reader["RFCEmisor"].ToString();
                    xpo.RFCReceptor = reader["RFCReceptor"].ToString();
                    xpo.ClaveProdServ = reader["ClaveProdServ"].ToString();
                    xpo.Cantidad = Convert.ToDouble(reader["Cantidad"].ToString());
                    xpo.ClaveUnidad = reader["ClaveUnidad"].ToString();
                    xpo.Unidad = reader["Unidad"].ToString();
                    xpo.Descripcion = reader["Descripcion"].ToString();
                    xpo.ValorUnitario = Convert.ToDouble(reader["ValorUnitario"].ToString());
                    xpo.Importe = Convert.ToDouble(reader["Importe"].ToString());
                    xpo.TasaOCuotaTraslado = Convert.ToDouble(reader["TasaOCuotaTraslado"].ToString());
                    xpo.ImporteTraslado = Convert.ToDouble(reader["ImporteTraslado"].ToString());
                    xpo.TasaOCuotaRetencion = Convert.ToDouble(reader["TasaOCuotaRetencion"].ToString());
                    xpo.ImporteRetencion = Convert.ToDouble(reader["ImporteRetencion"].ToString());
                    //origen
                    xpo.FechaSalida = DateTime.Parse(reader["FechaSalida"].ToString());
                    xpo.NombreRemitente = reader["NombreRemitente"].ToString();
                    xpo.RFCRemitente = reader["RFCRemitente"].ToString();
                    xpo.CodigoPostalRemitente = reader["CodigoPostalRemitente"].ToString();
                    xpo.ReferenciaRemitente = reader["ReferenciaRemitente"].ToString();
                    xpo.CalleRemitente = reader["CalleRemitente"].ToString();
                    xpo.TipoUbicacionRemitente = reader["TipoUbicacionRemitente"].ToString();
                    xpo.LocalidadRemitente = reader["LocalidadRemitente"].ToString();
                    xpo.MunicipioRemitente = reader["MunicipioRemitente"].ToString();
                    xpo.EstadoRemitente = reader["EstadoRemitente"].ToString();
                    //destino
                    xpo.FechaLlegada = DateTime.Parse(reader["FechaLlegada"].ToString());
                    xpo.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    xpo.RFCDestinatario = reader["RFCDestinatario"].ToString();
                    xpo.CodigoPostalDestinatario = reader["CodigoPostalDestinatario"].ToString();
                    xpo.ReferenciaDestinatario = reader["ReferenciaDestinatario"].ToString();
                    xpo.CalleDestinatario = reader["CalleDestinatario"].ToString();
                    xpo.TipoUbicacionDestinatario = reader["TipoUbicacionDestinatario"].ToString();
                    xpo.LocalidadDestinatario = reader["LocalidadDestinatario"].ToString();
                    xpo.MunicipioDestinatario = reader["MunicipioDestinatario"].ToString();
                    xpo.EstadoDestinatario = reader["EstadoDestinatario"].ToString();
                    xpo.PesoBrutoTotal = Convert.ToDouble(reader["PesoBrutoTotal"].ToString());
                    xpo.PlacaVM = reader["PlacaVM"].ToString();
                    xpo.RemolquePlaca = reader["RemolquePlaca"].ToString();
                    xpo.NombreFigura = reader["NombreFigura"].ToString();
                    xpo.RFCFigura = reader["RFCFigura"].ToString();
                    xpo.Log = Convert.ToInt32(reader["Log"].ToString());
                    xpo.Ruta = reader["Ruta"].ToString();
                    xpo.Kilometros = Convert.ToInt32(reader["Kilometros"].ToString());
                    xpo.Cliente = reader["Cliente"].ToString();
                    xpo.Carro = reader["Carro"].ToString();
                    xpo.Remolque = reader["Remolque"].ToString();
                    xPOCartas.Add(xpo);
                }
                dgvDatos.ItemsSource = xPOCartas;
                con.Close();
            }
        }

        private void btnProcesar_Click(object sender, RoutedEventArgs e)
        {
            string pdf = txtXML.Text;
            pdf = pdf.Replace(".xml", ".pdf");
            pdf = pdf.Replace(@"\\", @"\");
            if (File.Exists(@pdf))
            {
                byte[] bytespdf = File.ReadAllBytes(@pdf);
                if (bytespdf.Length > 1)
                {
                    bool valorlog = true;
                    int respuesta = 0;
                    bool valor = true;
                    Log log = new Log();
                    log.ShowDialog();
                    valorlog = log.Valor;
                    if (valorlog)
                    {
                        try
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(Comprobante));
                            Comprobante comp = new Comprobante();
                            StreamReader reader = new StreamReader(txtXML.Text);
                            comp = (Comprobante)serializer.Deserialize(reader);
                            reader.Close();
                            if (comp.Receptor.Rfc == "XLM201008NLA")
                            {
                                XPOCarta xpo = new XPOCarta();
                                List<XPOCarta> listxpo = new List<XPOCarta>();
                                xpo.Carro = log.Carro;
                                xpo.Remolque = log.Caja;
                                xpo.Log = log.log;
                                xpo.Ruta = log.Ruta;
                                xpo.Cliente = log.Cliente;
                                xpo.Remision = Convert.ToInt32(comp.Folio);
                                xpo.Kilometros = log.Kilometros;
                                xpo.Subtotal = comp.SubTotal;
                                xpo.Moneda = comp.Moneda;
                                xpo.FormaPago = comp.MetodoPago;
                                xpo.Total = comp.Total;
                                xpo.Fecha = DateTime.Parse(comp.Fecha);
                                xpo.RFCEmisor = comp.Emisor.Rfc;
                                xpo.RFCReceptor = comp.Receptor.Rfc;
                                if (comp.Conceptos != null)
                                {
                                    foreach (var item in comp.Conceptos.Concepto)
                                    {
                                        xpo.ClaveProdServ = item.ClaveProdServ;
                                        xpo.Cantidad = item.Cantidad;
                                        xpo.ClaveUnidad = item.ClaveUnidad;
                                        xpo.Unidad = item.Unidad;
                                        xpo.Descripcion = item.Descripcion;
                                        xpo.ValorUnitario = item.ValorUnitario;
                                        xpo.Importe = item.Importe;
                                        if (item.Impuestos.Traslados != null)
                                        {
                                            foreach (var item2 in item.Impuestos.Traslados.Traslado)
                                            {
                                                xpo.TasaOCuotaTraslado = item2.TasaOCuota;
                                                xpo.ImporteTraslado = item2.Importe;
                                            }
                                        }
                                        if (item.Impuestos.Retenciones != null)
                                        {
                                            foreach (var item3 in item.Impuestos.Retenciones.Retencion)
                                            {
                                                xpo.TasaOCuotaRetencion = item3.TasaOCuota;
                                                xpo.ImporteRetencion = item3.Importe;
                                            }
                                        }
                                    }
                                }
                                if (comp.Complemento.CartaPorte != null)
                                {
                                    if (comp.Complemento.CartaPorte.Ubicaciones != null)
                                    {
                                        foreach (var item in comp.Complemento.CartaPorte.Ubicaciones.Ubicacion)
                                        {
                                            if (item.TipoUbicacion == "Origen")
                                            {
                                                xpo.PaisRemitente = item.Domicilio.Pais;
                                                xpo.IDOrigen = item.IDUbicacion;
                                                xpo.FechaSalida = DateTime.Parse(item.FechaHoraSalidaLlegada);
                                                xpo.TipoEstacionRemitente = item.TipoEstacion;
                                                xpo.TipoUbicacionRemitente = item.TipoUbicacion;
                                                xpo.NombreRemitente = item.NombreRemitenteDestinatario;
                                                xpo.RFCRemitente = item.RFCRemitenteDestinatario;
                                                xpo.CodigoPostalRemitente = item.Domicilio.CodigoPostal;
                                                xpo.ReferenciaRemitente = item.Domicilio.Referencia;
                                                xpo.CalleRemitente = item.Domicilio.Calle;
                                                xpo.ColoniaRemitente = item.Domicilio.Colonia;
                                                xpo.LocalidadRemitente = item.Domicilio.Localidad;
                                                xpo.MunicipioRemitente = item.Domicilio.Municipio;
                                                xpo.EstadoRemitente = item.Domicilio.Estado;
                                                xpo.NumeroExteriorRemitente = item.Domicilio.NumeroExterior;
                                                xpo.NumeroInteriorRemitente = item.Domicilio.NumeroInterior;
                                            }
                                            if (item.TipoUbicacion == "Destino")
                                            {
                                                xpo.PaisDestinatario = item.Domicilio.Pais;
                                                xpo.TipoEstacionDestinatario = item.TipoEstacion;
                                                xpo.IDDestino = item.IDUbicacion;
                                                xpo.FechaLlegada = DateTime.Parse(item.FechaHoraSalidaLlegada);
                                                xpo.TipoUbicacionDestinatario = item.TipoUbicacion;
                                                xpo.NombreDestinatario = item.NombreRemitenteDestinatario;
                                                xpo.RFCDestinatario = item.RFCRemitenteDestinatario;
                                                xpo.CodigoPostalDestinatario = item.Domicilio.CodigoPostal;
                                                xpo.ReferenciaDestinatario = item.Domicilio.Referencia;
                                                xpo.CalleDestinatario = item.Domicilio.Calle;
                                                xpo.ColoniaDestinatario = item.Domicilio.Colonia;
                                                xpo.LocalidadDestinatario = item.Domicilio.Localidad;
                                                xpo.MunicipioDestinatario = item.Domicilio.Municipio;
                                                xpo.EstadoDestinatario = item.Domicilio.Estado;
                                                xpo.NumeroExteriorDestinatario = item.Domicilio.NumeroExterior;
                                                xpo.NumeroInteriorDestinatario = item.Domicilio.NumeroInterior;
                                            }
                                        }
                                    }
                                    if (comp.Complemento.CartaPorte.Mercancias != null)
                                    {
                                        xpo.PesoBrutoTotal = comp.Complemento.CartaPorte.Mercancias.PesoBrutoTotal;
                                        if (comp.Complemento.CartaPorte.Mercancias.AutoTransporte.IdentificacionVehicular != null)
                                        {
                                            xpo.PlacaVM = comp.Complemento.CartaPorte.Mercancias.AutoTransporte.IdentificacionVehicular.PlacaVM;
                                        }
                                        if (comp.Complemento.CartaPorte.Mercancias.AutoTransporte.Remolques != null)
                                        {
                                            foreach (var item in comp.Complemento.CartaPorte.Mercancias.AutoTransporte.Remolques.Remolque)
                                            {
                                                xpo.RemolquePlaca = item.Placa;
                                            }
                                        }
                                        if (comp.Complemento.CartaPorte.Mercancias.Mercancia != null)
                                        {
                                            xpo.Mercancia = comp.Complemento.CartaPorte.Mercancias.Mercancia;
                                        }
                                    }
                                    if (comp.Complemento.CartaPorte.FiguraTransporte != null)
                                    {
                                        if (comp.Complemento.CartaPorte.FiguraTransporte.TiposFigura.TipoFigura == "01")
                                        {
                                            xpo.NombreFigura = comp.Complemento.CartaPorte.FiguraTransporte.TiposFigura.NombreFigura;
                                            xpo.RFCFigura = comp.Complemento.CartaPorte.FiguraTransporte.TiposFigura.RFCFigura;
                                        }
                                    }
                                    xpo.UUID = comp.Complemento.TimbreFiscalDigital.UUID;
                                }
                                if (xpo.MunicipioDestinatario != "027")
                                {
                                    if (xpo.EstadoDestinatario != "TAM")
                                    {
                                        LiquidacionXPO lxpo = new LiquidacionXPO();
                                        lxpo.ShowDialog();
                                        valor = lxpo.Valor;
                                        xpo.Liquidacion = lxpo.Liquidacion;
                                    }
                                    else
                                    {
                                        valor = true;
                                    }
                                }
                                else
                                {
                                    if (xpo.EstadoDestinatario != "TAM")
                                    {
                                        LiquidacionXPO lxpo = new LiquidacionXPO();
                                        lxpo.ShowDialog();
                                        valor = lxpo.Valor;
                                        xpo.Liquidacion = lxpo.Liquidacion;
                                    }
                                    else
                                    {
                                        valor = true;
                                    }
                                }
                                DialogResult result = System.Windows.Forms.MessageBox.Show("Seguro que quiere agregar la Carta Porte con la siguiente informacion?\nLog: " + xpo.Log + "\nCaja: " + log.Caja.Trim() + "\nCliente: " + xpo.Cliente.Trim() + "\nFecha: " + xpo.Fecha + "\nOperador: " + xpo.NombreFigura + "\nDescripcion: " + xpo.Descripcion, "Confirmacion", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                                if (result == System.Windows.Forms.DialogResult.Yes)
                                {
                                    if (valor && valorlog)
                                    {
                                        using (SqlConnection con = new SqlConnection(connectionstring))
                                        {
                                            con.Open();
                                            SqlCommand cmd = new SqlCommand("sp_insertXPO", con);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@UUID", xpo.UUID);
                                            cmd.Parameters.AddWithValue("@RFCEmisor", xpo.RFCEmisor != null ? xpo.RFCEmisor : "");
                                            cmd.Parameters.AddWithValue("@Liquidacion", xpo.Liquidacion);
                                            cmd.Parameters.AddWithValue("@Fecha", xpo.Fecha);
                                            cmd.Parameters.AddWithValue("@RFCReceptor", xpo.RFCReceptor != null ? xpo.RFCReceptor : "");
                                            cmd.Parameters.AddWithValue("@Moneda", xpo.Moneda != null ? xpo.Moneda : "");
                                            cmd.Parameters.AddWithValue("@Subtotal", xpo.Subtotal);
                                            cmd.Parameters.AddWithValue("@Total", xpo.Total);
                                            cmd.Parameters.AddWithValue("@FormaPago", xpo.FormaPago);
                                            cmd.Parameters.AddWithValue("@ClaveProdServ", xpo.ClaveProdServ != null ? xpo.ClaveProdServ : "");
                                            cmd.Parameters.AddWithValue("@Cantidad", xpo.Cantidad);
                                            cmd.Parameters.AddWithValue("@ClaveUnidad", xpo.ClaveUnidad != null ? xpo.ClaveUnidad : "");
                                            cmd.Parameters.AddWithValue("@Unidad", xpo.Unidad != null ? xpo.Unidad : "");
                                            cmd.Parameters.AddWithValue("@Descripcion", xpo.Descripcion != null ? xpo.Descripcion : "");
                                            cmd.Parameters.AddWithValue("@ValorUnitario", xpo.ValorUnitario);
                                            cmd.Parameters.AddWithValue("@Importe", xpo.Importe);
                                            cmd.Parameters.AddWithValue("@TasaOCuotaTraslado", xpo.TasaOCuotaTraslado);
                                            cmd.Parameters.AddWithValue("@ImporteTraslado", xpo.ImporteTraslado);
                                            cmd.Parameters.AddWithValue("@TasaOCuotaRetencion", xpo.TasaOCuotaRetencion);
                                            cmd.Parameters.AddWithValue("@ImporteRetencion", xpo.ImporteRetencion);
                                            //Origen
                                            cmd.Parameters.AddWithValue("@FechaSalida", xpo.FechaSalida);
                                            cmd.Parameters.AddWithValue("@NombreRemitente", xpo.NombreRemitente != null ? xpo.NombreRemitente : "");
                                            cmd.Parameters.AddWithValue("@RFCRemitente", xpo.RFCRemitente != null ? xpo.RFCRemitente : "");
                                            cmd.Parameters.AddWithValue("@CodigoPostalRemitente", xpo.CodigoPostalRemitente != null ? xpo.CodigoPostalRemitente : "");
                                            cmd.Parameters.AddWithValue("@ReferenciaRemitente", xpo.ReferenciaRemitente != null ? xpo.ReferenciaRemitente : "");
                                            cmd.Parameters.AddWithValue("@CalleRemitente", xpo.CalleRemitente != null ? xpo.CalleRemitente : "");
                                            cmd.Parameters.AddWithValue("@TipoUbicacionRemitente", xpo.TipoUbicacionRemitente != null ? xpo.TipoUbicacionRemitente : "");
                                            cmd.Parameters.AddWithValue("@LocalidadRemitente", xpo.LocalidadRemitente != null ? xpo.LocalidadRemitente : "");
                                            cmd.Parameters.AddWithValue("@MunicipioRemitente", xpo.MunicipioRemitente != null ? xpo.MunicipioRemitente : "");
                                            cmd.Parameters.AddWithValue("@EstadoRemitente", xpo.EstadoRemitente != null ? xpo.EstadoRemitente : "");
                                            //destino
                                            cmd.Parameters.AddWithValue("@FechaLlegada", xpo.FechaLlegada);
                                            cmd.Parameters.AddWithValue("@NombreDestinatario", xpo.NombreDestinatario != null ? xpo.NombreDestinatario : "");
                                            cmd.Parameters.AddWithValue("@RFCDestinatario", xpo.RFCDestinatario != null ? xpo.RFCDestinatario : "");
                                            cmd.Parameters.AddWithValue("@CodigoPostalDestinatario", xpo.CodigoPostalDestinatario != null ? xpo.CodigoPostalDestinatario : "");
                                            cmd.Parameters.AddWithValue("@ReferenciaDestinatario", xpo.ReferenciaDestinatario != null ? xpo.ReferenciaDestinatario : "");
                                            cmd.Parameters.AddWithValue("@CalleDestinatario", xpo.CalleDestinatario != null ? xpo.CalleDestinatario : "");
                                            cmd.Parameters.AddWithValue("@TipoUbicacionDestinatario", xpo.TipoUbicacionDestinatario != null ? xpo.TipoUbicacionDestinatario : "");
                                            cmd.Parameters.AddWithValue("@LocalidadDestinatario", xpo.LocalidadDestinatario != null ? xpo.LocalidadDestinatario : "");
                                            cmd.Parameters.AddWithValue("@MunicipioDestinatario", xpo.MunicipioDestinatario != null ? xpo.MunicipioDestinatario : "");
                                            cmd.Parameters.AddWithValue("@EstadoDestinatario", xpo.EstadoDestinatario != null ? xpo.EstadoDestinatario : "");
                                            //Mercancias
                                            cmd.Parameters.AddWithValue("@PesoBrutoTotal", xpo.PesoBrutoTotal);
                                            //Placas
                                            cmd.Parameters.AddWithValue("@PlacaVM", xpo.PlacaVM != null ? xpo.PlacaVM : "");
                                            cmd.Parameters.AddWithValue("@RemolquePlaca", xpo.RemolquePlaca != null ? xpo.RemolquePlaca : "");
                                            //Figura
                                            cmd.Parameters.AddWithValue("@NombreFigura", xpo.NombreFigura != null ? xpo.NombreFigura : "");
                                            cmd.Parameters.AddWithValue("@RFCFigura", xpo.RFCFigura != null ? xpo.RFCFigura : "");
                                            cmd.Parameters.AddWithValue("@Usuario", Usuario);
                                            cmd.Parameters.AddWithValue("@Archivo", txtXML.Text);
                                            cmd.Parameters.AddWithValue("@Log", xpo.Log);
                                            cmd.Parameters.AddWithValue("@Ruta", xpo.Ruta.Trim());
                                            cmd.Parameters.AddWithValue("@Kilometros", xpo.Kilometros);
                                            cmd.Parameters.AddWithValue("@Remision", xpo.Remision);
                                            cmd.Parameters.AddWithValue("@Cliente", xpo.Cliente.Trim());
                                            cmd.Parameters.AddWithValue("@Remolque", xpo.Remolque.Trim());
                                            cmd.Parameters.AddWithValue("@Carro", xpo.Carro.Trim());
                                            SqlDataReader lector = cmd.ExecuteReader();
                                            while (lector.Read())
                                            {
                                                respuesta = Convert.ToInt32(lector["Mensaje"].ToString());
                                            }
                                            if (respuesta == 0)
                                            {
                                                System.Windows.Forms.MessageBox.Show("Procesado con Exito!");
                                                using (SqlConnection conn = new SqlConnection(connectionstring))
                                                {
                                                    conn.Open();
                                                    SqlCommand cmd1 = new SqlCommand("sp_InsertUpdateCFDCartaPorteUbicaciones", conn);
                                                    cmd1.CommandType = CommandType.StoredProcedure;
                                                    cmd1.Parameters.AddWithValue("@Tipo", 1);
                                                    cmd1.Parameters.AddWithValue("@Remision", "RMX" + xpo.Remision);
                                                    cmd1.Parameters.AddWithValue("@TipoEstacion", xpo.TipoEstacionRemitente != null ? xpo.TipoEstacionRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@DistanciaRecorrida", xpo.Kilometros);
                                                    cmd1.Parameters.AddWithValue("@IDOrigen", xpo.IDOrigen != null ? xpo.IDOrigen : "");
                                                    cmd1.Parameters.AddWithValue("@NombreRemitente", xpo.NombreRemitente != null ? xpo.NombreRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@ResidenciaFiscal", "" /*xpo.RFCRemitente == "XEXX010101000" ? "USA": "MEX"*/); //
                                                    cmd1.Parameters.AddWithValue("@NombreEstacion", ""); //
                                                    cmd1.Parameters.AddWithValue("@FechaSalida", xpo.FechaSalida.ToString("MM/dd/yyyy HH:mm:ss"));
                                                    cmd1.Parameters.AddWithValue("@RFCRemitente", xpo.RFCRemitente != null ? xpo.RFCRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@NumRegldTrib", ""); //
                                                    cmd1.Parameters.AddWithValue("@NumEstacion", ""); //
                                                    cmd1.Parameters.AddWithValue("@CodigoPostal", xpo.CodigoPostalRemitente != null ? xpo.CodigoPostalRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@Calle", xpo.CalleRemitente != null ? xpo.CalleRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@NumeroInterior", xpo.NumeroInteriorRemitente != null ? xpo.NumeroInteriorRemitente : ""); //
                                                    cmd1.Parameters.AddWithValue("@NumeroExterior", xpo.NumeroExteriorRemitente != null ? xpo.NumeroExteriorRemitente : ""); //
                                                    cmd1.Parameters.AddWithValue("@Colonia", xpo.ColoniaRemitente != null ? xpo.ColoniaRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@Referencia", xpo.ReferenciaRemitente != null ? xpo.ReferenciaRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@Localidad", xpo.LocalidadRemitente != null ? xpo.LocalidadRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@Municipio", xpo.MunicipioRemitente != null ? xpo.MunicipioRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@Estado", xpo.EstadoRemitente != null ? xpo.EstadoRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@Pais", xpo.PaisRemitente != null ? xpo.PaisRemitente : "");
                                                    cmd1.Parameters.AddWithValue("@IDDestino", xpo.IDDestino != null ? xpo.IDDestino : "");
                                                    cmd1.Parameters.AddWithValue("@NombreDestinatario", xpo.NombreDestinatario != null ? xpo.NombreDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@ResidenciaFiscalCDE", ""/* xpo.RFCDestinatario == "XEXX010101000" ? "USA" : "MEX"*/); //
                                                    cmd1.Parameters.AddWithValue("@NombreEstacionCDE", ""); //
                                                    cmd1.Parameters.AddWithValue("@FechaLLegada", xpo.FechaLlegada.ToString("MM/dd/yyyy HH:mm:ss"));
                                                    cmd1.Parameters.AddWithValue("@RFCDestinatario", xpo.RFCDestinatario != null ? xpo.RFCDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@NumRegldTribCDE", ""); //
                                                    cmd1.Parameters.AddWithValue("@NumEstacionCDE", ""); //
                                                    cmd1.Parameters.AddWithValue("@CodigoPostalCDE", xpo.CodigoPostalDestinatario != null ? xpo.CodigoPostalDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@CalleCDE", xpo.CalleDestinatario != null ? xpo.CalleDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@NumeroInteriorCDE", xpo.NumeroInteriorDestinatario != null ? xpo.NumeroInteriorDestinatario : ""); //
                                                    cmd1.Parameters.AddWithValue("@NumeroExteriorCDE", xpo.NumeroExteriorDestinatario != null ? xpo.NumeroExteriorDestinatario : ""); //
                                                    cmd1.Parameters.AddWithValue("@ColoniaCDE", xpo.ColoniaDestinatario != null ? xpo.ColoniaDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@ReferenciaCDE", xpo.ReferenciaDestinatario != null ? xpo.ReferenciaDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@LocalidadCDE", xpo.LocalidadDestinatario != null ? xpo.LocalidadDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@MunicipioCDE", xpo.MunicipioDestinatario != null ? xpo.MunicipioDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@EstadoCDE", xpo.EstadoDestinatario != null ? xpo.EstadoDestinatario : "");
                                                    cmd1.Parameters.AddWithValue("@PaisCDE", xpo.PaisDestinatario != null ? xpo.PaisDestinatario : "");
                                                    cmd1.ExecuteNonQuery();
                                                }
                                                foreach (Mercancia mercancia in xpo.Mercancia)
                                                {
                                                    using (SqlConnection conn = new SqlConnection(connectionstring))
                                                    {
                                                        conn.Open();
                                                        SqlCommand cmd1 = new SqlCommand("sp_InsertUpdateCFDCartaPorteMercancias", conn);
                                                        cmd1.CommandType = CommandType.StoredProcedure;
                                                        cmd1.Parameters.AddWithValue("@Tipo", 1);
                                                        cmd1.Parameters.AddWithValue("@Remision", "RMX" + xpo.Remision);
                                                        cmd1.Parameters.AddWithValue("@ClaveProdServCP", mercancia.BienesTransp != null ? mercancia.BienesTransp : "");
                                                        cmd1.Parameters.AddWithValue("@ClaveUnidad", mercancia.ClaveUnidad != null ? mercancia.ClaveUnidad : "");
                                                        cmd1.Parameters.AddWithValue("@Descripcion", mercancia.Descripcion != null ? mercancia.Descripcion : "");
                                                        cmd1.Parameters.AddWithValue("@CveMaterialPeligroso", "");
                                                        cmd1.Parameters.AddWithValue("@FraccionArancelaroia", "");
                                                        cmd1.Parameters.AddWithValue("@Cantidad", mercancia.Cantidad);
                                                        cmd1.Parameters.AddWithValue("@Unidad", "");
                                                        cmd1.Parameters.AddWithValue("@MaterialPeligroso", mercancia.MaterialPeligroso != null ? mercancia.MaterialPeligroso : "");
                                                        cmd1.Parameters.AddWithValue("@Embalaje", "");
                                                        cmd1.Parameters.AddWithValue("@PesoEnKg", mercancia.PesoEnKg);
                                                        cmd1.Parameters.AddWithValue("@Moneda", mercancia.Moneda != null ? mercancia.Moneda : "");
                                                        cmd1.Parameters.AddWithValue("@UUIDComercioExt", "");
                                                        cmd1.Parameters.AddWithValue("@IDOrigen", xpo.IDOrigen != null ? xpo.IDOrigen : "");
                                                        cmd1.Parameters.AddWithValue("@IDDestino", xpo.IDDestino != null ? xpo.IDDestino : "");
                                                        cmd1.Parameters.AddWithValue("@ClaveTransporte", "");
                                                        cmd1.Parameters.AddWithValue("@PesoBruto", mercancia.PesoEnKg);
                                                        cmd1.Parameters.AddWithValue("@PesoNeto", mercancia.PesoEnKg);
                                                        cmd1.Parameters.AddWithValue("@ClaveProdServCPDescripcion", mercancia.Descripcion != null ? mercancia.Descripcion : "");
                                                        cmd1.Parameters.AddWithValue("@ClaveUnidadNombre", mercancia.Unidad != null ? mercancia.Unidad : "");
                                                        cmd1.Parameters.AddWithValue("@FraccionArancelariaDescripcion", "");
                                                        cmd1.ExecuteNonQuery();
                                                    }
                                                }
                                                string anio = xpo.Fecha.Year.ToString();
                                                string mes = xpo.Fecha.Month < 10 ? "0" + xpo.Fecha.Month.ToString() : xpo.Fecha.Month.ToString();
                                                anio = anio + mes;
                                                string dia = xpo.Fecha.Day < 10 ? "0" + xpo.Fecha.Day.ToString() : xpo.Fecha.Day.ToString();
                                                CopiarArchivos(txtXML.Text, anio, dia, xpo.UUID);
                                                txtXML.Text = "";
                                            }
                                            else
                                            {
                                                System.Windows.Forms.MessageBox.Show("No se pudo procesar, UUID Duplicado");
                                            }
                                            con.Close();
                                        }
                                        LlenarDGV();
                                    }
                                    else
                                    {
                                        System.Windows.Forms.MessageBox.Show("Es necesario agregar el LOG y/o Liquidacion para poder procesar la carta porte");
                                    }
                                }
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("La carta porte no pertenece al Receptor con RFC: XLM201008NLA");
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("Error: \n" + ex.Message);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Es necesario agregar el LOG para poder procesar la Carta Porte");
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("El archivo pdf esta vacio o dañado");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No existe el archivo pdf, favor de descargar el archivo pdf en la misma carpeta o verifique que tenga el mismo nombre");
            }
        }

        public void CopiarArchivos(string archivo, string aniomes, string dia, string uuid)
        {
            string remision = "";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_RemisionXPO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@archivo", txtXML.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    remision = reader["Remision"].ToString();
                }
            }
            if (Directory.Exists(@"P:/validaciones/xml/" + aniomes + "/" + dia + "/01"))
            {
                string nombrearchivo = Path.GetFileName(archivo);
                nombrearchivo = "RMX" + remision + "_" + uuid + ".xml";
                File.Copy(archivo, @"P:/validaciones/xml/" + aniomes + "/" + dia + "/01/" + nombrearchivo, true);
                nombrearchivo = nombrearchivo.Replace(".xml", ".pdf");
                archivo = archivo.Replace(".xml", ".pdf");
                File.Copy(archivo, @"P:/validaciones/xml/" + aniomes + "/" + dia + "/01/" + nombrearchivo, true);
                archivo = archivo.Replace(".pdf", ".xml");
                string xmlstring = XDocument.Load(archivo).ToString();
                XDocument docum = XDocument.Parse(xmlstring);
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 6, iTextSharp.text.Font.NORMAL);
                byte[] byteArray = Encoding.ASCII.GetBytes(docum.ToString());
                MemoryStream stream = new MemoryStream(byteArray);
                StreamReader rdr = new StreamReader(stream);
                Document doc = new Document();
                nombrearchivo = nombrearchivo.Replace(".pdf", "");
                PdfWriter.GetInstance(doc, new FileStream(@"p:/validaciones/xml/" + aniomes + "/" + dia + "/01/" + nombrearchivo + "_pdf.pdf", FileMode.Create));
                doc.Open();
                doc.Add(new Paragraph(rdr.ReadToEnd(), font));
                doc.Close();
            }
            else
            {
                Directory.CreateDirectory(@"P:/validaciones/xml/" + aniomes + "/" + dia + "/01");
                string nombrearchivo = Path.GetFileName(archivo);
                nombrearchivo = "RMX" + remision + "_" + uuid + ".xml";
                File.Copy(archivo, @"P:/validaciones/xml/" + aniomes + "/" + dia + "/01/" + nombrearchivo, true);
                nombrearchivo = nombrearchivo.Replace(".xml", ".pdf");
                archivo = archivo.Replace(".xml", ".pdf");
                File.Copy(archivo, @"P:/validaciones/xml/" + aniomes + "/" + dia + "/01/" + nombrearchivo, true);
                archivo = archivo.Replace(".pdf", ".xml");
                string xmlstring = XDocument.Load(archivo).ToString();
                XDocument docum = XDocument.Parse(xmlstring);
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 6, iTextSharp.text.Font.NORMAL);
                byte[] byteArray = Encoding.ASCII.GetBytes(docum.ToString());
                MemoryStream stream = new MemoryStream(byteArray);
                StreamReader rdr = new StreamReader(stream);
                Document doc = new Document();
                nombrearchivo = nombrearchivo.Replace(".pdf", "");
                PdfWriter.GetInstance(doc, new FileStream(@"p:/validaciones/xml/" + aniomes + "/" + dia + "/01/" + nombrearchivo + "_pdf.pdf", FileMode.Create));
                doc.Open();
                doc.Add(new Paragraph(rdr.ReadToEnd(), font));
                doc.Close();
            }
        }

        private void dgvDatos_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "RFCEmisor")
            {
                e.Column.Header = "RFC Emisor";
            }
            if (e.PropertyName == "RFCReceptor")
            {
                e.Column.Header = "RFC Receptor";
            }
            if (e.PropertyName == "ClaveUnidad")
            {
                e.Column.Header = "Clave Unidad";
            }
            if (e.PropertyName == "ValorUnitario")
            {
                e.Column.Header = "Varlo Unitario";
            }
            if (e.PropertyName=="FormaPago")
            {
                e.Column.Header = "Forma de Pago";
            }
            if (e.PropertyName == "TasaOCuotaTraslado")
            {
                e.Column.Header = "Tasa O Cuota Traslado";
            }
            if (e.PropertyName == "ImporteTraslado")
            {
                e.Column.Header = "Importe Traslado";
            }
            if (e.PropertyName == "TasaOCuotaRetencion")
            {
                e.Column.Header = "Tasa O Cuota Retencion";
            }
            if (e.PropertyName == "ImporteRetencion")
            {
                e.Column.Header = "Importe Retencion";
            }
            if (e.PropertyName == "FechaSalida")
            {
                e.Column.Header = "Fecha Salida";
            }
            if (e.PropertyName == "TipoUbicacionRemitente")
            {
                e.Column.Header = "Tipo Ubicacion Remitente";
            }
            if (e.PropertyName == "NombreRemitente")
            {
                e.Column.Header = "Nombre Remitente";
            }
            if (e.PropertyName == "RFCRemitente")
            {
                e.Column.Header = "RFC Remitente";
            }
            if (e.PropertyName == "CodigoPostalRemitente")
            {
                e.Column.Header = "Codigo Postal Remitente";
            }
            if (e.PropertyName == "ReferenciaRemitente")
            {
                e.Column.Header = "Referencia Remitente";
            }
            if (e.PropertyName == "CalleRemitente")
            {
                e.Column.Header = "Calle Remitente";
            }
            if (e.PropertyName == "LocalidadRemitente")
            {
                e.Column.Header = "Localidad Remitente";
            }
            if (e.PropertyName == "MunicipioRemitente")
            {
                e.Column.Header = "Municipio Remitente";
            }
            if (e.PropertyName == "EstadoRemitente")
            {
                e.Column.Header = "Estado Remitente";
            }
            if (e.PropertyName == "FechaLlegada")
            {
                e.Column.Header = "Fecha Llegada";
            }
            if (e.PropertyName == "TipoUbicacionDestinatario")
            {
                e.Column.Header = "Tipo Ubicacion Destinatario";
            }
            if (e.PropertyName == "NombreDestinatario")
            {
                e.Column.Header = "Nombre Destinatario";
            }
            if (e.PropertyName == "RFCDestinatario")
            {
                e.Column.Header = "RFC Destinatario";
            }
            if (e.PropertyName == "CodigoPostalDestinatario")
            {
                e.Column.Header = "Codigo Postal Destinatario";
            }
            if (e.PropertyName == "ReferenciaDestinatario")
            {
                e.Column.Header = "Referencia Destinatario";
            }
            if (e.PropertyName == "CalleDestinatario")
            {
                e.Column.Header = "Calle Destinatario";
            }
            if (e.PropertyName == "LocalidadDestinatario")
            {
                e.Column.Header = "Localidad Destinatario";
            }
            if (e.PropertyName == "MunicipioDestinatario")
            {
                e.Column.Header = "Municipio Destinatario";
            }
            if (e.PropertyName == "EstadoDestinatario")
            {
                e.Column.Header = "Estado Destinatario";
            }
            if (e.PropertyName == "RemolquePlaca")
            {
                e.Column.Header = "Placa Remolque";
            }
            if (e.PropertyName == "NombreFigura")
            {
                e.Column.Header = "Operador";
            }
            if (e.PropertyName == "RFCFigura")
            {
                e.Column.Header = "RFC Operador";
            }
            if (e.PropertyName == "PesoBrutoTotal")
            {
                e.Column.Header = "Peso Bruto Total";
            }
            if (e.PropertyName =="Mercancia")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "IDOrigen")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "TipoEstacionRemitente")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "ColoniaRemitente")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "PaisRemitente")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "NumeroInteriorRemitente")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "NumeroExteriorRemitente")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "IDDestino")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "TipoEstacionDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "ColoniaDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "PaisDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "NumeroInteriorDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "NumeroExteriorDestinatario")
            {
                e.Cancel = true;
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
