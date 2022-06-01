using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using MahApps.Metro.Controls;
using Two_Way_Trasnfer.Clases.CartaPorte;
using Two_Way_Trasnfer.Clases.FacturasProveedores;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para FacturasProveedores.xaml
    /// </summary>
    public partial class FacturasProveedores : MetroWindow
    {
        string connectionString = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        string connectionstring2 = "Data Source=SOPORTE\\SQLEXPRESS; Database=FOXPRO; Initial Catalog=FOXPRO ;User ID=sa; Password = Twoway2408";
        string connectionstring3 = "Server=192.168.1.250;Database=ATEBCOFIDI;User Id=sa;Password=sax;";
        List<ProveedorSQL> list = new List<ProveedorSQL>();
        List<FacturasProcesadas> listTWT = new List<FacturasProcesadas>();
        List<FacturasProcesadas> listTWL = new List<FacturasProcesadas>();
        public bool banderaMarissa { get; set; }
        public bool banderaLozano { get; set; }
        public int contador { get; set; } = 0;
        public double sumLozano { get; set; }
        public double sumMarissa { get; set; }
        public string Usuario { get; set; }

        public FacturasProveedores(string user)
        {
            InitializeComponent();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_getName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Usuario", user);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario = reader["nombre"].ToString();
                }
            }
            AcutalizarDG();
        }

        private void dgProveedoresFacturas_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "LogID" || e.PropertyName == "Proveedor" || e.PropertyName == "RFC" || e.PropertyName == "Factura" || e.PropertyName == "Importe" || e.PropertyName == "Fecha" ||
                e.PropertyName == "Ruta" || e.PropertyName == "Usuario" || e.PropertyName == "PDF" || e.PropertyName == "XML" || e.PropertyName == "Soporte" || e.PropertyName == "FechaPDF" || e.PropertyName == "FechaXML" || e.PropertyName == "FechaSoporte")
            {
                e.Column.IsReadOnly = true;
            }
            if (e.PropertyName == "Numero")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "DiasCredito")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "Tipo")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "Correo")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "Cuenta")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "SubCuenta")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "SubSub")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "Importe")
            {
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "$0.00";
            }
            if (e.PropertyName == "FechaPDF")
            {
                e.Column.Header = "Fecha PDF";
            }
            if (e.PropertyName == "FechaXML")
            {
                e.Column.Header = "Fecha XML";
            }
            if (e.PropertyName == "FechaSoporte")
            {
                e.Column.Header = "Fecha Soporte";
            }
        }

        private void bntProcesar_Click(object sender, RoutedEventArgs e)
        {
            listTWT.Clear();
            listTWL.Clear();
            List<ProveedorSQL> proveedors = list.FindAll(r => r.Procesar == true);
            string success = "Facturas procesadas con exito:\n\n";
            string error = "";

            if (proveedors.Count > 0)
            {
                int consecutivoMarissa = 0;
                using (SqlConnection con = new SqlConnection(connectionstring2))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_getConsecutivoMarissa", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader4 = cmd.ExecuteReader();
                    while (reader4.Read())
                    {
                        string a = reader4["consecutivo"].ToString();
                        consecutivoMarissa =reader4["consecutivo"].ToString() == "" ? 1 : int.Parse(reader4["consecutivo"].ToString());
                    }
                }
                consecutivoMarissa++;
                int consecutivoLozano = 0;
                using (SqlConnection con = new SqlConnection(connectionstring2))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_getConsecutivoLozano", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader4 = cmd.ExecuteReader();
                    while (reader4.Read())
                    {
                        consecutivoLozano = reader4["consecutivo"].ToString() == "" ? 1 : int.Parse(reader4["consecutivo"].ToString());
                    }
                }
                consecutivoLozano++;
                foreach (ProveedorSQL proveedorSql in proveedors)
                {
                    bool coincideImporte = false;
                    string path;
                    Proveedor proveedor = new Proveedor();
                    XmlSerializer serializer = new XmlSerializer(typeof(Comprobante));
                    Comprobante comp = new Comprobante();

                    if (proveedorSql.Usuario == "GRUPO LOPINSA S.A. D")
                    {
                        int anio = proveedorSql.FechaXML.Year;
                        string mes = proveedorSql.FechaXML.Month < 10 ? "0" + proveedorSql.FechaXML.Month : proveedorSql.FechaXML.Month.ToString();
                        string dia = proveedorSql.FechaXML.Day < 10 ? "0" + proveedorSql.FechaXML.Day : proveedorSql.FechaXML.Day.ToString();
                        StreamReader reader = new StreamReader(@"P:\validaciones\xml\LOPINSA\" + anio + mes + "/" + dia + "/01/" + proveedorSql.XML);
                        comp = (Comprobante)serializer.Deserialize(reader);
                        if (comp.Folio.Trim() == proveedorSql.Factura)
                        {
                            if (comp.Moneda == "USD")
                            {
                                using (SqlConnection con = new SqlConnection(connectionstring2))
                                {
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("sp_selectTwoProvMarissa", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Nombre", proveedorSql.Proveedor);
                                    cmd.Parameters.AddWithValue("@RFC", proveedorSql.RFC);
                                    SqlDataReader reader2 = cmd.ExecuteReader();
                                    if (reader2.HasRows)
                                    {
                                        while (reader2.Read())
                                        {
                                            proveedorSql.Numero = int.Parse(reader2["Numero"].ToString());
                                            proveedorSql.DiasCredito = int.Parse(reader2["Dias_Cred"].ToString());
                                            proveedorSql.Tipo = reader2["Tipo"].ToString();
                                            proveedorSql.Correo = reader2["Email"].ToString().Trim();
                                            proveedorSql.Cuenta = int.Parse(reader2["Cuenta1"].ToString());
                                            proveedorSql.SubCuenta = int.Parse(reader2["Subcta1"].ToString());
                                            proveedorSql.SubSub = int.Parse(reader2["subsub1"].ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (SqlConnection con = new SqlConnection(connectionstring2))
                                {
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("sp_selectTwoProvLozano", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Nombre", proveedorSql.Proveedor);
                                    cmd.Parameters.AddWithValue("@RFC", proveedorSql.RFC);
                                    SqlDataReader reader3 = cmd.ExecuteReader();
                                    if (reader3.HasRows)
                                    {
                                        while (reader3.Read())
                                        {
                                            proveedorSql.Numero = int.Parse(reader3["Numero"].ToString());
                                            proveedorSql.DiasCredito = int.Parse(reader3["Dias_Cred"].ToString());
                                            proveedorSql.Tipo = reader3["Tipo"].ToString();
                                            proveedorSql.Correo = reader3["Email"].ToString().Trim();
                                            proveedorSql.Cuenta = int.Parse(reader3["Cuenta1"].ToString());
                                            proveedorSql.SubCuenta = int.Parse(reader3["Subcta1"].ToString());
                                            proveedorSql.SubSub = int.Parse(reader3["subsub1"].ToString());
                                        }
                                    }
                                }
                            }
                            reader.Close();
                            if (comp.Receptor.Rfc == "XEXX010101000" && comp.Moneda == "USD")
                            {
                                path = @"P:/marissa/twofacpro.dbf";
                                proveedor.Empresa = 1;
                                proveedor.NumeroProveedor = proveedorSql.Numero;
                                proveedor.Fecha = proveedorSql.Fecha;
                                string Lugar = proveedorSql.Factura.Substring(0, 3);
                                string Factura = proveedorSql.Factura.Substring(3);
                                proveedor.SubtotalXML = comp.SubTotal;
                                proveedor.Moneda = comp.Moneda;
                                proveedor.TipoCambio = comp.TipoCambio;
                                proveedor.Debe = comp.Total;
                                proveedor.Lugar = Lugar;
                                proveedor.Factura = comp.Folio.Substring(3);
                                proveedor.Rfcreceptor = comp.Receptor.Rfc;
                                proveedor.Reference = int.Parse(Factura);
                                foreach (var item in comp.Conceptos.Concepto)
                                {
                                    proveedor.Concepto = item.Descripcion + " Log: " + proveedorSql.LogID + " " + Lugar + " " + Factura;
                                    proveedor.ValorUnitario = item.ValorUnitario;
                                }
                                proveedor.Vence = DateTime.Parse(proveedorSql.Fecha.ToString()).AddDays(proveedorSql.DiasCredito);
                                proveedor.Nombre = comp.Emisor.Nombre;
                                proveedor.Tipo = proveedorSql.Tipo.Trim();
                                proveedor.UUID = comp.Complemento.TimbreFiscalDigital.UUID;
                                InsertDataFoxPro(proveedor, path, proveedorSql.XML);
                                string fact = proveedor.Factura;

                                Contabilidad(consecutivoMarissa, "TWL", proveedor.ValorUnitario, proveedor.Iva, proveedor.Retencion, proveedorSql.Cuenta, proveedorSql.SubCuenta, proveedorSql.SubSub, proveedor.UUID, proveedorSql.Proveedor, fact);
                                FacturasProcesadas contra = new FacturasProcesadas();
                                contra.Fecha = proveedorSql.Fecha.ToShortDateString();
                                contra.Empresa = "TWO WAY LOGISTICS INC.";
                                contra.NumeroProveedor = proveedor.NumeroProveedor;
                                contra.Proveedor = proveedor.Nombre;
                                contra.Factura = int.Parse(comp.Folio.Substring(3));
                                contra.Importe = proveedor.ValorUnitario;
                                contra.Vencimiento = proveedor.Vence.ToShortDateString();
                                contra.Poliza = "A " + consecutivoMarissa;
                                listTWL.Add(contra);
                                success += "Factura: " + fact + " Poliza: A " + consecutivoMarissa + " TWL" + "\n";
                                contador++;
                                if (proveedors.Count == contador)
                                {
                                    if (sumLozano > 0)
                                    {
                                        insertScpol94(consecutivoLozano, @"h:\lozano\scpol94.dbf", sumLozano, proveedorSql.Proveedor, "FACTURAS PROVEEDORES");
                                    }
                                    if (sumMarissa > 0)
                                    {
                                        insertScpol94(consecutivoMarissa, @"h:\marissa\scpol94.dbf", sumMarissa, proveedorSql.Proveedor, "FACTURAS PROVEEDORES");
                                    }
                                }
                            }
                            else if (comp.Receptor.Rfc != "XEXX010101000")
                            {
                                path = @"P:/lozano/twofacpro.dbf";
                                proveedor.Empresa = 1;
                                proveedor.NumeroProveedor = proveedorSql.Numero;
                                proveedor.Fecha = proveedorSql.Fecha;
                                string Lugar = proveedorSql.Factura.Substring(0, 3);
                                string Factura = proveedorSql.Factura.Substring(3);
                                proveedor.SubtotalXML = comp.SubTotal;
                                proveedor.Moneda = comp.Moneda;
                                proveedor.TipoCambio = comp.TipoCambio;
                                proveedor.Debe = proveedor.Moneda == "USD" ? comp.Total * 20 : comp.Total;
                                proveedor.Lugar = Lugar;
                                proveedor.Rfcreceptor = comp.Receptor.Rfc;
                                proveedor.Factura = comp.Folio.Substring(3);
                                proveedor.Reference = int.Parse(Factura);
                                foreach (var item in comp.Conceptos.Concepto)
                                {
                                    proveedor.Concepto = proveedor.Moneda == "USD" ? item.Descripcion + " ($" + comp.Total + " USD)" + " Log: " + proveedorSql.LogID + " " + Lugar + " " + Factura : item.Descripcion + " Log: " + proveedorSql.LogID + " " + Lugar + " " + Factura;
                                    proveedor.ValorUnitario = comp.Moneda == "USD" ? item.ValorUnitario * 20 : item.ValorUnitario;
                                    foreach (var impuesto in item.Impuestos.Traslados.Traslado)
                                    {
                                        proveedor.Iva = impuesto.TasaOCuota == 0.16 ? impuesto.Importe : 0;
                                    }
                                    foreach (var impuesto in item.Impuestos.Retenciones.Retencion)
                                    {
                                        proveedor.Retencion = impuesto.TasaOCuota == 0.04 ? impuesto.Importe : 0;
                                    }
                                }
                                proveedor.Vence = DateTime.Parse(proveedorSql.Fecha.ToString()).AddDays(proveedorSql.DiasCredito);
                                proveedor.Nombre = comp.Emisor.Nombre;
                                proveedor.Tipo = proveedorSql.Tipo.Trim();
                                proveedor.UUID = comp.Complemento.TimbreFiscalDigital.UUID;
                                InsertDataFoxPro(proveedor, path, proveedorSql.XML);
                                string fact = proveedor.Factura;
                                Contabilidad(consecutivoLozano, "TWT", proveedor.ValorUnitario, proveedor.Iva, proveedor.Retencion, proveedorSql.Cuenta, proveedorSql.SubCuenta, proveedorSql.SubSub, proveedor.UUID, proveedorSql.Proveedor, fact);
                                FacturasProcesadas contra = new FacturasProcesadas();
                                contra.Fecha = proveedorSql.Fecha.ToShortDateString();
                                contra.Empresa = "TWO WAY TRANSFER S.A. DE C.V.";
                                contra.NumeroProveedor = proveedor.NumeroProveedor;
                                contra.Proveedor = proveedor.Nombre;
                                contra.Factura = int.Parse(comp.Folio.Substring(3));
                                contra.Importe = proveedor.ValorUnitario + proveedor.Iva - proveedor.Retencion;
                                contra.Poliza = "A " + consecutivoLozano;
                                contra.Vencimiento = proveedor.Vence.ToShortDateString();
                                listTWT.Add(contra);
                                success += "Factura: " + fact + " Poliza: A " + consecutivoLozano + " TWT" + "\n";
                                contador++;
                                if (proveedors.Count == contador)
                                {
                                    if (sumLozano > 0)
                                    {
                                        insertScpol94(consecutivoLozano, @"h:\lozano\scpol94.dbf", sumLozano, proveedorSql.Proveedor, "FACTURAS PROVEEDORES");
                                    }
                                    if (sumMarissa > 0)
                                    {
                                        insertScpol94(consecutivoMarissa, @"h:\marissa\scpol94.dbf", sumMarissa, proveedorSql.Proveedor, "FACTURAS PROVEEDORES");
                                    }
                                }
                            }
                        }
                        else
                        {
                            error += "El numero de factura del XML no coincide con el numero de factura dado de alta en intranet Log: " + proveedorSql.LogID + "\n";
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand("sp_deleteProveedorFactura", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@XML", proveedorSql.XML);
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlConnection con = new SqlConnection(connectionstring2))
                            {
                                try
                                {
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("sp_updatetwoProveedores", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@log", proveedorSql.LogID);
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {
                                }
                            }
                            //SendEmail(proveedorSql.Correo, "Factura Con Importe Invalido", "La Factura: " + comp.Folio + " tiene un importe invalido");
                            SendEmail("adavila@twt.com.mx", "Numero de factura invalido Log: " + proveedorSql.LogID, "El numero de factura del XML no coincide con el numero de factura dado de alta en intranet Log: " + proveedorSql.LogID);
                        }
                    }
                    else
                    {
                        StreamReader reader = new StreamReader(@"P:/scanner/" + proveedorSql.XML);
                        comp = (Comprobante)serializer.Deserialize(reader);

                        if (comp.Folio.Trim() == proveedorSql.Factura)
                        {
                            if (comp.Moneda == "USD")
                            {
                                using (SqlConnection con = new SqlConnection(connectionstring2))
                                {
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("sp_selectTwoProvMarissa", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Nombre", proveedorSql.Proveedor);
                                    cmd.Parameters.AddWithValue("@RFC", proveedorSql.RFC);
                                    SqlDataReader reader2 = cmd.ExecuteReader();
                                    if (reader2.HasRows)
                                    {
                                        while (reader2.Read())
                                        {
                                            proveedorSql.Numero = int.Parse(reader2["Numero"].ToString());
                                            proveedorSql.DiasCredito = int.Parse(reader2["Dias_Cred"].ToString());
                                            proveedorSql.Tipo = reader2["Tipo"].ToString();
                                            proveedorSql.Correo = reader2["Email"].ToString().Trim();
                                            proveedorSql.Cuenta = int.Parse(reader2["Cuenta1"].ToString());
                                            proveedorSql.SubCuenta = int.Parse(reader2["Subcta1"].ToString());
                                            proveedorSql.SubSub = int.Parse(reader2["subsub1"].ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (SqlConnection con = new SqlConnection(connectionstring2))
                                {
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("sp_selectTwoProvLozano", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Nombre", proveedorSql.Proveedor);
                                    cmd.Parameters.AddWithValue("@RFC", proveedorSql.RFC);
                                    SqlDataReader reader3 = cmd.ExecuteReader();
                                    if (reader3.HasRows)
                                    {
                                        while (reader3.Read())
                                        {
                                            proveedorSql.Numero = int.Parse(reader3["Numero"].ToString());
                                            proveedorSql.DiasCredito = int.Parse(reader3["Dias_Cred"].ToString());
                                            proveedorSql.Tipo = reader3["Tipo"].ToString();
                                            proveedorSql.Correo = reader3["Email"].ToString().Trim();
                                            proveedorSql.Cuenta = int.Parse(reader3["Cuenta1"].ToString());
                                            proveedorSql.SubCuenta = int.Parse(reader3["Subcta1"].ToString());
                                            proveedorSql.SubSub = int.Parse(reader3["subsub1"].ToString());
                                        }
                                    }
                                }
                            }
                            reader.Close();
                            using (SqlConnection con = new SqlConnection(connectionstring2))
                            {
                                double valorunitario = 0;
                                foreach (var item in comp.Conceptos.Concepto)
                                {
                                    if (comp.Moneda == "USD")
                                    {

                                        valorunitario = comp.Total * 20;
                                    }
                                    else
                                    {
                                        valorunitario = item.ValorUnitario;
                                    }
                                }
                                con.Open();
                                SqlCommand cmd = new SqlCommand("sp_selectRutasPro", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@NumProveedor", proveedorSql.Numero);
                                cmd.Parameters.AddWithValue("@Importe", valorunitario);
                                SqlDataReader dataReader = cmd.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    if (valorunitario == double.Parse(dataReader["Importe"].ToString()))
                                    {
                                        coincideImporte = true;
                                        break;
                                    }
                                    else
                                    {
                                        coincideImporte = false;
                                    }
                                }
                            }
                            if (coincideImporte)
                            {
                                if (comp.Receptor.Rfc == "XEXX010101000" && comp.Moneda == "USD")
                                {
                                    path = @"P:/marissa/twofacpro.dbf";
                                    proveedor.Empresa = 1;
                                    proveedor.NumeroProveedor = proveedorSql.Numero;
                                    proveedor.Fecha = proveedorSql.Fecha;
                                    int index = proveedorSql.XML.IndexOf(' ');
                                    string Factura = proveedorSql.XML.Substring(0, index);
                                    int index2 = proveedorSql.XML.IndexOf('-');
                                    string Lugar = proveedorSql.XML.Substring(index, index2 - index).Trim();
                                    proveedor.SubtotalXML = comp.SubTotal;
                                    proveedor.Moneda = comp.Moneda;
                                    proveedor.TipoCambio = comp.TipoCambio;
                                    proveedor.Debe = comp.Total;
                                    proveedor.Lugar = Lugar;
                                    proveedor.Factura = comp.Folio;
                                    proveedor.Rfcreceptor = comp.Receptor.Rfc;
                                    proveedor.Reference = int.Parse(Factura);
                                    foreach (var item in comp.Conceptos.Concepto)
                                    {
                                        proveedor.Concepto = item.Descripcion + " Log: " + proveedorSql.LogID + " " + Lugar + " " + Factura;
                                        proveedor.ValorUnitario = item.ValorUnitario;
                                    }
                                    proveedor.Vence = DateTime.Parse(proveedorSql.Fecha.ToString()).AddDays(proveedorSql.DiasCredito);
                                    proveedor.Nombre = comp.Emisor.Nombre;
                                    proveedor.Tipo = proveedorSql.Tipo.Trim();
                                    proveedor.UUID = comp.Complemento.TimbreFiscalDigital.UUID;
                                    InsertDataFoxPro(proveedor, path, proveedorSql.XML);
                                    string fact = proveedor.Lugar + "" + proveedor.Factura;

                                    Contabilidad(consecutivoMarissa, "TWL", proveedor.ValorUnitario, proveedor.Iva, proveedor.Retencion, proveedorSql.Cuenta, proveedorSql.SubCuenta, proveedorSql.SubSub, proveedor.UUID, proveedorSql.Proveedor, fact);
                                    FacturasProcesadas contra = new FacturasProcesadas();
                                    contra.Fecha = proveedorSql.Fecha.ToShortDateString();
                                    contra.Empresa = "TWO WAY LOGISTICS INC.";
                                    contra.NumeroProveedor = proveedor.NumeroProveedor;
                                    contra.Proveedor = proveedor.Nombre;
                                    contra.Factura = int.Parse(comp.Folio);
                                    contra.Importe = proveedor.ValorUnitario;
                                    contra.Vencimiento = proveedor.Vence.ToShortDateString();
                                    contra.Poliza = "A " + consecutivoMarissa;
                                    listTWL.Add(contra);
                                    success += "Factura: " + fact + " Poliza: A " + consecutivoMarissa + " TWL" + "\n";
                                    contador++;
                                    if (proveedors.Count == contador)
                                    {
                                        if (sumLozano > 0)
                                        {
                                            insertScpol94(consecutivoLozano, @"h:\lozano\scpol94.dbf", sumLozano, proveedorSql.Proveedor, "FACTURAS PROVEEDORES");
                                        }
                                        if (sumMarissa > 0)
                                        {
                                            insertScpol94(consecutivoMarissa, @"h:\marissa\scpol94.dbf", sumMarissa, proveedorSql.Proveedor, "FACTURAS PROVEEDORES");
                                        }
                                    }
                                }
                                else if (comp.Receptor.Rfc != "XEXX010101000")
                                {
                                    path = @"P:/lozano/twofacpro.dbf";
                                    proveedor.Empresa = 1;
                                    proveedor.NumeroProveedor = proveedorSql.Numero;
                                    proveedor.Fecha = proveedorSql.Fecha;
                                    int index = proveedorSql.XML.IndexOf(' ');
                                    string Factura = proveedorSql.XML.Substring(0, index);
                                    int index2 = proveedorSql.XML.IndexOf('-');
                                    string Lugar = proveedorSql.XML.Substring(index, index2 - index).Trim();
                                    proveedor.SubtotalXML = comp.SubTotal;
                                    proveedor.Moneda = comp.Moneda;
                                    proveedor.TipoCambio = comp.TipoCambio;
                                    proveedor.Debe = proveedor.Moneda == "USD" ? comp.Total * 20 : comp.Total;
                                    proveedor.Lugar = Lugar;
                                    proveedor.Rfcreceptor = comp.Receptor.Rfc;
                                    proveedor.Factura = comp.Folio;
                                    proveedor.Reference = int.Parse(Factura);
                                    foreach (var item in comp.Conceptos.Concepto)
                                    {
                                        proveedor.Concepto = proveedor.Moneda == "USD" ? item.Descripcion + " ($" + comp.Total + " USD)" + " Log: " + proveedorSql.LogID + " " + Lugar + " " + Factura : item.Descripcion + " Log: " + proveedorSql.LogID + " " + Lugar + " " + Factura;
                                        proveedor.ValorUnitario = comp.Moneda == "USD" ? item.ValorUnitario * 20 : item.ValorUnitario;
                                        foreach (var impuesto in item.Impuestos.Traslados.Traslado)
                                        {
                                            proveedor.Iva = impuesto.TasaOCuota == 0.16 ? impuesto.Importe : 0;
                                        }
                                        foreach (var impuesto in item.Impuestos.Retenciones.Retencion)
                                        {
                                            proveedor.Retencion = impuesto.TasaOCuota == 0.04 ? impuesto.Importe : 0;
                                        }
                                    }
                                    proveedor.Vence = DateTime.Parse(proveedorSql.Fecha.ToString()).AddDays(proveedorSql.DiasCredito);
                                    proveedor.Nombre = comp.Emisor.Nombre;
                                    proveedor.Tipo = proveedorSql.Tipo.Trim();
                                    proveedor.UUID = comp.Complemento.TimbreFiscalDigital.UUID;
                                    InsertDataFoxPro(proveedor, path, proveedorSql.XML);
                                    string fact = proveedor.Lugar + "" + proveedor.Factura;
                                    Contabilidad(consecutivoLozano, "TWT", proveedor.ValorUnitario, proveedor.Iva, proveedor.Retencion, proveedorSql.Cuenta, proveedorSql.SubCuenta, proveedorSql.SubSub, proveedor.UUID, proveedorSql.Proveedor, fact);
                                    FacturasProcesadas contra = new FacturasProcesadas();
                                    contra.Fecha = proveedorSql.Fecha.ToShortDateString();
                                    contra.Empresa = "TWO WAY TRANSFER S.A. DE C.V.";
                                    contra.NumeroProveedor = proveedor.NumeroProveedor;
                                    contra.Proveedor = proveedor.Nombre;
                                    contra.Factura = int.Parse(comp.Folio);
                                    contra.Importe = proveedor.ValorUnitario + proveedor.Iva - proveedor.Retencion;
                                    contra.Poliza = "A " + consecutivoLozano;
                                    listTWT.Add(contra);
                                    success += "Factura: " + fact + " Poliza: A " + consecutivoLozano + " TWT" + "\n";
                                    contador++;
                                    if (proveedors.Count == contador)
                                    {
                                        if (sumLozano > 0)
                                        {
                                            insertScpol94(consecutivoLozano, @"h:\lozano\scpol94.dbf", sumLozano, proveedorSql.Proveedor, "FACTURAS PROVEEDORES");
                                        }
                                        if (sumMarissa > 0)
                                        {
                                            insertScpol94(consecutivoMarissa, @"h:\marissa\scpol94.dbf", sumMarissa, proveedorSql.Proveedor, "FACTURAS PROVEEDORES");
                                        }
                                    }
                                }
                                else
                                {
                                    error += "La factura con folio: " + comp.Folio + " tiene un importe invalido" + " \n";
                                    using (SqlConnection con = new SqlConnection(connectionString))
                                    {
                                        con.Open();
                                        SqlCommand cmd = new SqlCommand("sp_deleteProveedorFactura", con);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@XML", proveedorSql.XML);
                                        cmd.ExecuteNonQuery();
                                    }
                                    using (SqlConnection con = new SqlConnection(connectionstring2))
                                    {
                                        try
                                        {
                                            con.Open();
                                            SqlCommand cmd = new SqlCommand("sp_updatetwoProveedores", con);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@log", proveedorSql.LogID);
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (Exception)
                                        {
                                        }

                                    }
                                    //SendEmail(proveedorSql.Correo, "Factura Con Importe Invalido", "La Factura: " + comp.Folio + " tiene un importe invalido");
                                    SendEmail("adavila@twt.com.mx", "Factura Con Importe Invalido Log: " + proveedorSql.LogID, "La Factura: " + comp.Folio + " tiene un importe invalido");
                                }
                            }
                            else
                            {
                                error += "La factura con folio: " + comp.Folio + " tiene un importe invalido" + " \n";
                                using (SqlConnection con = new SqlConnection(connectionString))
                                {
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("sp_deleteProveedorFactura", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@XML", proveedorSql.XML);
                                    cmd.ExecuteNonQuery();
                                }
                                using (SqlConnection con = new SqlConnection(connectionstring2))
                                {
                                    try
                                    {
                                        con.Open();
                                        SqlCommand cmd = new SqlCommand("sp_updatetwoProveedores", con);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@log", proveedorSql.LogID);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                //SendEmail(proveedorSql.Correo, "Factura Con Importe Invalido", "La Factura: " + comp.Folio + " tiene un importe invalido");
                                SendEmail("adavila@twt.com.mx", "Factura Con Importe Invalido Log: " + proveedorSql.LogID, "La Factura: " + comp.Folio + " tiene un importe invalido");
                            }
                        }
                        else
                        {
                            error += "El numero de factura del XML no coincide con el numero de factura dado de alta en intranet Log: " + proveedorSql.LogID + "\n";
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand("sp_deleteProveedorFactura", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@XML", proveedorSql.XML);
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlConnection con = new SqlConnection(connectionstring2))
                            {
                                try
                                {
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("sp_updatetwoProveedores", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@log", proveedorSql.LogID);
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {
                                }
                            }
                            //SendEmail(proveedorSql.Correo, "Factura Con Importe Invalido", "La Factura: " + comp.Folio + " tiene un importe invalido");
                            SendEmail("adavila@twt.com.mx", "Numero de factura invalido Log: " + proveedorSql.LogID, "El numero de factura del XML no coincide con el numero de factura dado de alta en intranet Log: " + proveedorSql.LogID);
                        }
                    }



                }
                if (error != "")
                {
                    System.Windows.Forms.MessageBox.Show(error, "Error al procesar las facturas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (success != "Facturas procesadas con exito:\n")
                    {
                        if (listTWL.Count > 0)
                        {
                            ReporteFacturasProcesadas reporte = new ReporteFacturasProcesadas(listTWL);
                            reporte.ShowDialog();
                        }
                        if (listTWT.Count > 0)
                        {
                            ReporteFacturasProcesadas reporte = new ReporteFacturasProcesadas(listTWT);
                            reporte.ShowDialog();
                        }
                        System.Windows.Forms.MessageBox.Show(success, "Proceso terminado", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (listTWL.Count > 0)
                    {
                        ReporteFacturasProcesadas reporte = new ReporteFacturasProcesadas(listTWL);
                        reporte.ShowDialog();
                    }
                    if (listTWT.Count > 0)
                    {
                        ReporteFacturasProcesadas reporte = new ReporteFacturasProcesadas(listTWT);
                        reporte.ShowDialog();
                    }
                    System.Windows.Forms.MessageBox.Show(success, "Proceso terminado", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                AcutalizarDG();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Seleccione al menos una factura para procesar", "No hay facturas seleccionadas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        public void InsertDataFoxPro(Proveedor proveedor, string path, string xml)
        {
            if (path == @"P:/lozano/twofacpro.dbf")
            {
                DataTable YourResultSet = new DataTable();
                OleDbConnection yourConnectionHandler = new OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + path + ";");
                string insertStatement = "Insert Into twofacpro (Empresa,Numero,Fecha,Debe,Haber,Lugar,Faco,Factura,Reference,Concepto,Fecha_mtc,Folio,Importe,Pago,Date,Amount,Costumer," +
                    "Cheque,Fechapag,Contra,Vence,Saldo,Status,Fefe1,Aster,Cu,Pipro,Negativa,Nombre,Tipo,Poliza,Uuid,Repaext,Prov) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                OleDbCommand insertCommand = new OleDbCommand(insertStatement, yourConnectionHandler);
                insertCommand.Parameters.Add("Empresa", OleDbType.Numeric).Value = proveedor.Empresa;
                insertCommand.Parameters.Add("Numero", OleDbType.Numeric).Value = proveedor.NumeroProveedor;
                insertCommand.Parameters.Add("Fecha", OleDbType.Date).Value = proveedor.Fecha;
                insertCommand.Parameters.Add("Debe", OleDbType.Numeric).Value = 0.0;
                insertCommand.Parameters.Add("Haber", OleDbType.Numeric).Value = proveedor.Debe;
                insertCommand.Parameters.Add("Lugar", OleDbType.Char).Value = proveedor.Lugar;
                insertCommand.Parameters.Add("Faco", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Factura", OleDbType.Numeric).Value = int.Parse(proveedor.Factura);
                insertCommand.Parameters.Add("Reference", OleDbType.Numeric).Value = proveedor.Reference;
                insertCommand.Parameters.Add("Concepto", OleDbType.Char).Value = proveedor.Concepto;
                insertCommand.Parameters.Add("Fecha_mtc", OleDbType.Date).Value = null;
                insertCommand.Parameters.Add("Folio", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Importe", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Pago", OleDbType.Boolean).Value = null;
                insertCommand.Parameters.Add("Date", OleDbType.Date).Value = null;
                insertCommand.Parameters.Add("Amount", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Costumer", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Cheque", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Fechapag", OleDbType.Date).Value = null;
                insertCommand.Parameters.Add("Contra", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Vence", OleDbType.Date).Value = proveedor.Vence;
                insertCommand.Parameters.Add("Saldo", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Status", OleDbType.Char).Value = "";
                insertCommand.Parameters.Add("Fefe1", OleDbType.Date).Value = null;
                insertCommand.Parameters.Add("Aster", OleDbType.Char).Value = "";
                insertCommand.Parameters.Add("Cu", OleDbType.Char).Value = "";
                insertCommand.Parameters.Add("Pipro", OleDbType.Char).Value = "";
                insertCommand.Parameters.Add("Negativa", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Nombre", OleDbType.Char).Value = proveedor.Nombre;
                insertCommand.Parameters.Add("Tipo", OleDbType.Char).Value = proveedor.Tipo;
                insertCommand.Parameters.Add("Poliza", OleDbType.Integer).Value = 0;
                insertCommand.Parameters.Add("Uuid", OleDbType.Char).Value = proveedor.UUID;
                insertCommand.Parameters.Add("Repaext", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Prov", OleDbType.Char).Value = "";
                yourConnectionHandler.Open();
                try
                {
                    int count = insertCommand.ExecuteNonQuery();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_updateProveedores", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@XML", xml);
                        cmd.Parameters.AddWithValue("@rfcreceptor", proveedor.Rfcreceptor);
                        cmd.Parameters.AddWithValue("@monedaxml", proveedor.Moneda);
                        cmd.Parameters.AddWithValue("@SubtotalXML", proveedor.SubtotalXML);
                        cmd.Parameters.AddWithValue("@UsuarioProcesado", Usuario);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (OleDbException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Error al insertar en twofacpro", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    string error = DateTime.Now + " " + ex.Message + "\n";
                    File.AppendAllText("log.txt", error);
                }
                finally
                {
                    yourConnectionHandler.Close();
                }
            }
            else
            {
                DataTable YourResultSet = new DataTable();
                OleDbConnection yourConnectionHandler = new OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + path + ";");
                string insertStatement = "Insert Into twofacpro (Empresa,Numero,Fecha,Debe,Haber,Lugar,Faco,Factura,Reference,Concepto,Fecha_mtc,Folio,Importe,Pago,Date,Amount,Costumer,Cheque,Fechapag,Contra,Vence,Saldo," +
                    "Status,Fefe1,Aster,Cu,Pipro,Negativa,Nombre,Tipo) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                OleDbCommand insertCommand = new OleDbCommand(insertStatement, yourConnectionHandler);
                insertCommand.Parameters.Add("Empresa", OleDbType.Numeric).Value = proveedor.Empresa;
                insertCommand.Parameters.Add("Numero", OleDbType.Numeric).Value = proveedor.NumeroProveedor;
                insertCommand.Parameters.Add("Fecha", OleDbType.Date).Value = proveedor.Fecha;
                insertCommand.Parameters.Add("Debe", OleDbType.Numeric).Value = 0.0;
                insertCommand.Parameters.Add("Haber", OleDbType.Numeric).Value = proveedor.Debe;
                insertCommand.Parameters.Add("Lugar", OleDbType.Char).Value = proveedor.Lugar;
                insertCommand.Parameters.Add("Faco", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Factura", OleDbType.Numeric).Value = int.Parse(proveedor.Factura);
                insertCommand.Parameters.Add("Reference", OleDbType.Numeric).Value = proveedor.Reference;
                insertCommand.Parameters.Add("Concepto", OleDbType.Char).Value = proveedor.Concepto;
                insertCommand.Parameters.Add("Fecha_mtc", OleDbType.Date).Value = null;
                insertCommand.Parameters.Add("Folio", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Importe", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Pago", OleDbType.Boolean).Value = null;
                insertCommand.Parameters.Add("Date", OleDbType.Date).Value = null;
                insertCommand.Parameters.Add("Amount", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Costumer", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Cheque", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Fechapag", OleDbType.Date).Value = null;
                insertCommand.Parameters.Add("Contra", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Vence", OleDbType.Date).Value = proveedor.Vence;
                insertCommand.Parameters.Add("Saldo", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Status", OleDbType.Char).Value = "";
                insertCommand.Parameters.Add("Fefe1", OleDbType.Date).Value = null;
                insertCommand.Parameters.Add("Aster", OleDbType.Char).Value = "";
                insertCommand.Parameters.Add("Cu", OleDbType.Char).Value = "";
                insertCommand.Parameters.Add("Pipro", OleDbType.Char).Value = "";
                insertCommand.Parameters.Add("Negativa", OleDbType.Numeric).Value = 0;
                insertCommand.Parameters.Add("Nombre", OleDbType.Char).Value = proveedor.Nombre;
                insertCommand.Parameters.Add("Tipo", OleDbType.Char).Value = proveedor.Tipo;
                yourConnectionHandler.Open();
                try
                {
                    int count = insertCommand.ExecuteNonQuery();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_updateProveedores", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@XML", xml);
                        cmd.Parameters.AddWithValue("@rfcreceptor", proveedor.Rfcreceptor);
                        cmd.Parameters.AddWithValue("@monedaxml", proveedor.Moneda);
                        cmd.Parameters.AddWithValue("@SubtotalXML", proveedor.SubtotalXML);
                        cmd.Parameters.AddWithValue("@UsuarioProcesado", Usuario);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (OleDbException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Error al insertar en twofacpro", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    string error = DateTime.Now + " " + ex.Message + "\n";
                    File.AppendAllText("log.txt", error);
                }
                finally
                {
                    yourConnectionHandler.Close();
                }
            }
        }

        public void AcutalizarDG()
        {

            using (SqlConnection con = new SqlConnection(connectionstring3))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_selectFacturasLopinsa", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    using (SqlConnection con2 = new SqlConnection(connectionString))
                    {
                        con2.Open();
                        SqlCommand cmd2 = new SqlCommand("sp_insertFacturasProcesadasLopinsa", con2);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@ID", reader["ID"].ToString());
                        cmd2.ExecuteNonQuery();
                    }
                    string cf3 = reader["CustomField03"].ToString();
                    int index = cf3.IndexOf("Log:");
                    string log = index == -1 ? "0" : cf3.Substring(index + 4, 7);
                    index = log.Trim().IndexOf(" ");
                    log = index == -1 ? log.Trim() : log.Trim().Substring(0, index);
                    index = cf3.IndexOf("Ruta:");
                    string ruta = index == -1 ? "" : cf3.Substring(index + 5);
                    using (SqlConnection con3 = new SqlConnection(connectionString))
                    {
                        con3.Open();
                        SqlCommand cmd3 = new SqlCommand("sp_insertProvedoresFacturas", con3);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.AddWithValue("@Logid", Convert.ToInt32(log.Trim()));
                        cmd3.Parameters.AddWithValue("@proveedor", "GRUPO LOPINSA S.A. DE C.V.");
                        cmd3.Parameters.AddWithValue("@RFC", "GLO171020T80");
                        cmd3.Parameters.AddWithValue("@Factura", reader["Factura"].ToString());
                        cmd3.Parameters.AddWithValue("@importe", Convert.ToDouble(reader["Monto"].ToString()));
                        cmd3.Parameters.AddWithValue("@fecha", DateTime.Parse(reader["FechaEmision"].ToString()));
                        cmd3.Parameters.AddWithValue("@ruta", ruta.Trim());
                        cmd3.Parameters.AddWithValue("@Usuario", "GRUPO LOPINSA S.A. DE C.V.");
                        cmd3.Parameters.AddWithValue("@FechaXML", DateTime.Parse(reader["FechaEmision"].ToString()));
                        cmd3.Parameters.AddWithValue("@PDF", "");
                        cmd3.Parameters.AddWithValue("@XML", reader["Factura"].ToString() + "_" + reader["ID"].ToString() + ".xml");
                        cmd3.ExecuteNonQuery();
                        int anio = DateTime.Parse(reader["FechaEmision"].ToString()).Year;
                        string mes = DateTime.Parse(reader["FechaEmision"].ToString()).Month < 10 ? "0" + DateTime.Parse(reader["FechaEmision"].ToString()).Month : DateTime.Parse(reader["FechaEmision"].ToString()).Month.ToString();
                        string dia = DateTime.Parse(reader["FechaEmision"].ToString()).Day < 10 ? "0" + DateTime.Parse(reader["FechaEmision"].ToString()).Day : DateTime.Parse(reader["FechaEmision"].ToString()).Day.ToString();
                        if (!Directory.Exists(@"P:\validaciones\xml\LOPINSA\" + anio + mes + "/" + dia + "/01/")) {
                            Directory.CreateDirectory(@"P:\validaciones\xml\LOPINSA\" + anio + mes + "/" + dia + "/01/");
                        }
                        using (FileStream fs = File.Create(@"P:\validaciones\xml\LOPINSA\" + anio + mes + "/" + dia + "/01/" + reader["Factura"].ToString() + "_" + reader["ID"].ToString() + ".xml"))
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(reader["FacturaElectronica"].ToString());
                            fs.Write(info, 0, info.Length);
                        }
                    }
                }
            }

            dgProveedoresFacturas.ItemsSource = null;
            list.Clear();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_selectProveedores", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        ProveedorSQL proveedorSQL = new ProveedorSQL();
                        proveedorSQL.LogID = int.Parse(reader1["LogID"].ToString().Trim());
                        proveedorSQL.Proveedor = reader1["Proveedor"].ToString().Trim();
                        proveedorSQL.RFC = reader1["RFC"].ToString().Trim();
                        proveedorSQL.Factura = reader1["Factura"].ToString().Trim();
                        proveedorSQL.Importe = double.Parse(reader1["Importe"].ToString().Trim());
                        proveedorSQL.Fecha = DateTime.Parse(reader1["Fecha"].ToString().Trim());
                        proveedorSQL.Ruta = reader1["Ruta"].ToString().Trim();
                        proveedorSQL.Usuario = reader1["Usuario"].ToString().Trim();
                        proveedorSQL.PDF = reader1["PDF"].ToString().Trim();
                        proveedorSQL.XML = reader1["XML"].ToString().Trim();
                        proveedorSQL.Soporte = reader1["Soporte"].ToString().Trim();
                        proveedorSQL.FechaPDF = DateTime.Parse(reader1["FechaPDF"].ToString().Trim());
                        proveedorSQL.FechaXML = DateTime.Parse(reader1["FechaXML"].ToString().Trim());
                        proveedorSQL.FechaSoporte = DateTime.Parse(reader1["FechaSoporte"].ToString().Trim());
                        list.Add(proveedorSQL);
                    }
                }
            }
            dgProveedoresFacturas.ItemsSource = list;
        }

        public void SendEmail(string To, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("noreply@twt.com.mx", "No Reply TWT");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("noreply@twt.com.mx", "twoway00");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
                System.Windows.Forms.MessageBox.Show("Correo Enviado a:" + To);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se ha podido enviar el email \n" + ex.Message);
            }
            finally
            {
                smtp.Dispose();
            }
        }

        public void Contabilidad(int consecutivo, string empresa, double valunit, double iva, double retencion, int cuenta, int subcta, int subsub, string uuid, string beneficia, string desc)
        {
            string concepto = "";
            if (empresa == "TWL")
            {
                concepto = GetDataScctas(empresa, 1, 4113, 3, 0);
                insertScdoc95(consecutivo, @"h:\marissa\scdoc95.dbf", "+", valunit, 4113, 3, 0, uuid, beneficia, desc, concepto);
                if (iva != 0)
                {
                    concepto = GetDataScctas(empresa, 1, 1105, 7, 1);
                    insertScdoc95(consecutivo, @"h:\marissa\scdoc95.dbf", "+", iva, 1105, 7, 1, uuid, beneficia, desc, concepto);
                }
                if (retencion != 0)
                {
                    concepto = GetDataScctas(empresa, 1, 2102, 10, 0);
                    insertScdoc95(consecutivo, @"h:\marissa\scdoc95.dbf", "-", retencion, 2102, 10, 0, uuid, beneficia, desc, concepto);
                }
                double sum = valunit + iva - retencion;
                concepto = GetDataScctas(empresa, 1, cuenta, subcta, subsub);
                insertScdoc95(consecutivo, @"h:\marissa\scdoc95.dbf", "-", sum, cuenta, subcta, subsub, uuid, beneficia, desc, concepto);
                sumMarissa = sumMarissa + valunit + iva;

            }
            else if (empresa == "TWT")
            {
                concepto = GetDataScctas(empresa, 1, 4113, 36, 0);
                insertScdoc95(consecutivo, @"h:\lozano\scdoc95.dbf", "+", valunit, 4113, 36, 0, uuid, beneficia, desc, concepto);
                if (iva != 0)
                {
                    concepto = GetDataScctas(empresa, 1, 1105, 7, 1);
                    insertScdoc95(consecutivo, @"h:\lozano\scdoc95.dbf", "+", iva, 1105, 7, 1, uuid, beneficia, desc, concepto);
                }
                if (retencion != 0)
                {
                    concepto = GetDataScctas(empresa, 1, 2102, 10, 0);
                    insertScdoc95(consecutivo, @"h:\lozano\scdoc95.dbf", "-", retencion, 2102, 10, 0, uuid, beneficia, desc, concepto);
                }
                double sum = valunit + iva - retencion;
                concepto = GetDataScctas(empresa, 1, cuenta, subcta, subsub);
                insertScdoc95(consecutivo, @"h:\lozano\scdoc95.dbf", "-", sum, cuenta, subcta, subsub, uuid, beneficia, desc, concepto);
                sumLozano = sumLozano + valunit + iva;
            }
        }

        public void insertScpol94(int consecutivo, string path, double importe, string beneficia, string concepto)
        {
            string mes;
            OleDbConnection yourConnectionHandler6 = new OleDbConnection(@"Provider=VFPOLEDB.1;Data Source=" + path + ";");
            string insertStatement6 = "Insert Into scpol94 (no_cte,serie,dia,ano,fecha,clave_doc,no_doc,cheque,depto,c_a,importe,posicion,beneficia,concepto,modifica,cargo,abonos,total," +
                "movi,liquida,elaboro) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            OleDbCommand insertCommand6 = new OleDbCommand(insertStatement6, yourConnectionHandler6);
            insertCommand6.Parameters.Add("no_cte", OleDbType.Numeric).Value = 1;
            if (DateTime.Now.Month < 10)
            {
                mes = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                mes = DateTime.Now.Month.ToString();
            }
            insertCommand6.Parameters.Add("serie", OleDbType.Char).Value = mes;
            insertCommand6.Parameters.Add("dia", OleDbType.Numeric).Value = DateTime.Now.Day;
            insertCommand6.Parameters.Add("ano", OleDbType.Numeric).Value = DateTime.Now.Year;
            insertCommand6.Parameters.Add("Fecha", OleDbType.Date).Value = null;
            insertCommand6.Parameters.Add("clave_doc", OleDbType.Char).Value = "A";
            insertCommand6.Parameters.Add("no_doc", OleDbType.Numeric).Value = consecutivo;
            insertCommand6.Parameters.Add("cheque", OleDbType.Numeric).Value = consecutivo;
            insertCommand6.Parameters.Add("dpto", OleDbType.Numeric).Value = 0;
            insertCommand6.Parameters.Add("c_a", OleDbType.Char).Value = "";
            insertCommand6.Parameters.Add("importe", OleDbType.Numeric).Value = 0;
            insertCommand6.Parameters.Add("Posicion", OleDbType.Numeric).Value = 0;
            insertCommand6.Parameters.Add("beneficia", OleDbType.Char).Value = beneficia;
            insertCommand6.Parameters.Add("concepto", OleDbType.Char).Value = concepto;
            insertCommand6.Parameters.Add("Modifica", OleDbType.Boolean).Value = false;
            insertCommand6.Parameters.Add("cargo", OleDbType.Numeric).Value = importe;
            insertCommand6.Parameters.Add("abonos", OleDbType.Numeric).Value = importe;
            insertCommand6.Parameters.Add("total", OleDbType.Numeric).Value = 0;
            insertCommand6.Parameters.Add("movi", OleDbType.Numeric).Value = 0;
            insertCommand6.Parameters.Add("liquida", OleDbType.Numeric).Value = 0;
            insertCommand6.Parameters.Add("Elaboro", OleDbType.Char).Value = "";
            yourConnectionHandler6.Open();
            try
            {
                int count = insertCommand6.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error al insertar en scpol94", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                string error = DateTime.Now + " " + ex.Message + "\n";
                File.AppendAllText("log.txt", error);
            }
            finally
            {
                yourConnectionHandler6.Close();
            }
        }

        public void insertScdoc95(int consecutivo, string path, string c_a, double importe, int cuenta, int sub_cta, int sub_sub, string uuid, string beneficia, string concepto, string desc)
        {
            string mes;
            OleDbConnection yourConnectionHandler1 = new OleDbConnection(@"Provider=VFPOLEDB.1;Data Source=" + path + ";");
            string insertStatement1 = "Insert Into scdoc95 (no_cte,serie,dia,ano,fecha,clave_doc,no_doc,cheque,cuenta,sub_cta,sub_sub,subsub_cta,clave_cta,depto,c_a,importe,beneficia,concepto," +
                "lista,desc,movi,liquida,ban1) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            OleDbCommand insertCommand1 = new OleDbCommand(insertStatement1, yourConnectionHandler1);
            insertCommand1.Parameters.Add("no_cte", OleDbType.Numeric).Value = 1;
            if (DateTime.Now.Month < 10)
            {
                mes = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                mes = DateTime.Now.Month.ToString();
            }
            insertCommand1.Parameters.Add("serie", OleDbType.Char).Value = mes;
            insertCommand1.Parameters.Add("dia", OleDbType.Numeric).Value = DateTime.Now.Day;
            insertCommand1.Parameters.Add("ano", OleDbType.Numeric).Value = DateTime.Now.Year;
            insertCommand1.Parameters.Add("Fecha", OleDbType.Date).Value = null;
            insertCommand1.Parameters.Add("clave_doc", OleDbType.Char).Value = "A";
            insertCommand1.Parameters.Add("no_doc", OleDbType.Numeric).Value = consecutivo;
            insertCommand1.Parameters.Add("cheque", OleDbType.Numeric).Value = consecutivo;
            insertCommand1.Parameters.Add("cuenta", OleDbType.Numeric).Value = cuenta;
            insertCommand1.Parameters.Add("sub_cta", OleDbType.Numeric).Value = sub_cta;
            insertCommand1.Parameters.Add("sub_sub", OleDbType.Numeric).Value = sub_sub;
            insertCommand1.Parameters.Add("subsub_cta", OleDbType.Numeric).Value = 0;
            insertCommand1.Parameters.Add("clave_cta", OleDbType.Char).Value = "";
            insertCommand1.Parameters.Add("dpto", OleDbType.Char).Value = uuid;
            insertCommand1.Parameters.Add("c_a", OleDbType.Char).Value = c_a;
            insertCommand1.Parameters.Add("importe", OleDbType.Numeric).Value = importe;
            insertCommand1.Parameters.Add("beneficia", OleDbType.Char).Value = beneficia;
            insertCommand1.Parameters.Add("concepto", OleDbType.Char).Value = concepto;
            insertCommand1.Parameters.Add("Lista", OleDbType.Char).Value = "";
            insertCommand1.Parameters.Add("desc", OleDbType.Char).Value = desc;
            insertCommand1.Parameters.Add("Movi", OleDbType.Numeric).Value = 0;
            insertCommand1.Parameters.Add("liquida", OleDbType.Numeric).Value = 0;
            insertCommand1.Parameters.Add("ban1", OleDbType.Char).Value = "";
            yourConnectionHandler1.Open();
            try
            {
                int count = insertCommand1.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error al insertar en scdoc95", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                string error = DateTime.Now + " " + ex.Message + "\n";
                File.AppendAllText("log.txt", error);
            }
            finally
            {
                yourConnectionHandler1.Close();
            }
        }

        public string GetDataScctas(string empresa, int no_cte, int cuenta, int sub_cta, int sub_sub)
        {
            if (empresa == "TWL")
            {
                using (SqlConnection con = new SqlConnection(connectionstring2))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_selectNombreScctasMarissa", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@no_cte", no_cte);
                    cmd.Parameters.AddWithValue("@Cuenta", cuenta);
                    cmd.Parameters.AddWithValue("@sub_cta", sub_cta);
                    cmd.Parameters.AddWithValue("@sub_sub", sub_sub);
                    SqlDataReader reader = cmd.ExecuteReader();
                    string nombre = "";
                    while (reader.Read())
                    {
                        nombre = reader["Espanol"].ToString();
                    }
                    return nombre;
                    
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(connectionstring2))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_selectNombreScctasLozano", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@no_cte", no_cte);
                    cmd.Parameters.AddWithValue("@Cuenta", cuenta);
                    cmd.Parameters.AddWithValue("@sub_cta", sub_cta);
                    cmd.Parameters.AddWithValue("@sub_sub", sub_sub);
                    SqlDataReader reader = cmd.ExecuteReader();
                    string nombre = "";
                    while (reader.Read())
                    {
                        nombre = reader["Espanol"].ToString();
                    }
                    return nombre;
                }
            }
        }

    }
}
