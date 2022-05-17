using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClosedXML.Excel;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Two_Way_Trasnfer.Clases;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : MetroWindow
    {
        string connectionstring2 = "Data Source=SOPORTE\\SQLEXPRESS; Database=FOXPRO; Initial Catalog=FOXPRO ;User ID=sa; Password = Twoway2408";
        string connectionstring = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        List<Cliente> Lclientes = new List<Cliente>();
        List<TarifasRutas> LTR = new List<TarifasRutas>();
        List<RemitenteCB> Lremitentes = new List<RemitenteCB>();
        List<DestinatarioCB> Ldestinatario = new List<DestinatarioCB>();
        List<Tarifas> Ltarifas = new List<Tarifas>();
        List<RutasCB> Lrutas = new List<RutasCB>();
        List<string> Tipo = new List<string>();
        List<string> Moneda = new List<string>();
        List<string> Remitente = new List<string>();
        List<string> SubCliente = new List<string>();
        List<string> Destinatario = new List<string>();
        List<string> Ruta = new List<string>();
        List<string> Transp = new List<string>();
        List<string> UsoDeTarifa = new List<string>();
        public string Usuario { get; set; }

        public Clientes(string usuario)
        {
            
            InitializeComponent();
            Usuario = usuario;
            using (SqlConnection con = new SqlConnection(connectionstring2))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ActualizarClientes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cl = new Cliente();
                    cl.Clave = Convert.ToInt32(reader["clave"].ToString());
                    cl.NombreCliente = reader["nombre"].ToString().TrimStart();
                    Lclientes.Add(cl);
                    SubCliente.Add(cl.NombreCliente);
                }               
            }
            Lclientes.Sort((x, y) => x.NombreCliente.CompareTo(y.NombreCliente));
            foreach (Cliente cliente in Lclientes)
            {
                cbCliente.Items.Add(cliente.NombreCliente);
            }
            txtNumCliente.Focus();
            Transp.Add("Sí");
            Transp.Add("No");
            Tipo.Add("Importacion");
            Tipo.Add("Exportacion");
            Tipo.Add("Nacional");
            Moneda.Add("MXN");
            Moneda.Add("USD");
            UsoDeTarifa.Add("Carretera");
            UsoDeTarifa.Add("Trasnfer");          
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_selectRemitentesDestinatarios", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                    RemitenteCB rem = new RemitenteCB();
                    rem.RemitenteID = Convert.ToInt32(reader1["ID"].ToString());
                    rem.NomRemitente = reader1["Nombre"].ToString();
                    rem.Referencia = reader1["Referencia"].ToString();
                    rem.Estado = reader1["Estado"].ToString();
                    rem.Municipio = reader1["Municipio"].ToString();
                    rem.Calle = reader1["Calle"].ToString().Trim();
                    rem.CiudadRemitente = reader1["Municipio"].ToString().Trim();
                    rem.NumeroExterior = reader1["NumeroExterior"].ToString().Trim();
                    rem.NumeroInterior = reader1["NumeroInterior"].ToString().Trim();
                    rem.Colonia = reader1["Colonia"].ToString().Trim();
                    string displayText = rem.NomRemitente.Trim() + " - " + rem.Referencia.Trim() + " - " + rem.Estado.Trim() + " - " + rem.Municipio.Trim();
                    rem.NombreCompleto = displayText;
                    Lremitentes.Add(rem);
                    Remitente.Add(displayText.Trim());
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            try
            {
                connection.Open();
                SqlCommand sqlComm2 = new SqlCommand("sp_selectRemitentesDestinatarios", connection);
                sqlComm2.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader2 = sqlComm2.ExecuteReader();
                while (reader2.Read())
                {
                    DestinatarioCB dest = new DestinatarioCB();
                    dest.DestinatarioID = Convert.ToInt32(reader2["ID"].ToString());
                    dest.Destinatario = reader2["Nombre"].ToString();
                    dest.Referencia = reader2["Referencia"].ToString();
                    dest.Estado = reader2["Estado"].ToString();
                    dest.Municipio = reader2["Municipio"].ToString();
                    dest.Calle = reader2["Calle"].ToString().Trim();
                    dest.CiudadDest = reader2["Municipio"].ToString().Trim();
                    dest.NumeroExterior = reader2["NumeroExterior"].ToString().Trim();
                    dest.NumeroInterior = reader2["NumeroInterior"].ToString().Trim();
                    dest.Colonia = reader2["Colonia"].ToString().Trim();
                    string displayText = dest.Destinatario.Trim() + " - " + dest.Referencia.Trim() + " - " + dest.Estado.Trim() + " - " + dest.Municipio.Trim();
                    dest.NombreCompleto = displayText;
                    Destinatario.Add(displayText.Trim());
                    Ldestinatario.Add(dest);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring2))
                {
                    con.Open();
                    SqlCommand sqlComm3 = new SqlCommand("sp_ActualizarRutas", con);
                    sqlComm3.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader3 = sqlComm3.ExecuteReader();
                    while (reader3.Read())
                    {
                        RutasCB rut = new RutasCB();
                        rut.Kilometros = Convert.ToInt32(reader3["Kilometros"].ToString());
                        rut.Ruta = reader3["ruta"].ToString().Trim();
                        Lrutas.Add(rut);
                        Lrutas.Sort((x, y) => x.Ruta.CompareTo(y.Ruta));
                        Ruta.Add(rut.Ruta);
                    }
                }                
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            cbCliente.SelectedIndex = 0;
            dgvTarifas.CanUserAddRows = true;
            dgvTarifas.ItemsSource = Ltarifas;
            dgvRutasHijos.CanUserAddRows = true;
            dgvRutasHijos.ItemsSource = LTR;
        }

        private void dgvTarifas_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "IDRemitente")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "PorcionUSA")
            {
                e.Column.Header = "Porcion USA";
            }
            if (e.PropertyName == "CiudadRem")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "CalleRemitente")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "NumeroExteriorRem")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "NumeroInteriorRem")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "ColoniaRemitente")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "IDDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "CiudadDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "CalleDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "NumeroExteriorDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "NumeroInteriorDest")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "ColoniaDestinatario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "ID")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "IDTarifa")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "Usuario")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "FechaMod")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "IDRemitente" | e.PropertyName == "IDDestinatario" | e.PropertyName == "Ruta" | e.PropertyName == "Kilometros" | e.PropertyName == "CiudadRem" | e.PropertyName == "CalleRemitente" |
                e.PropertyName == "NumeroExteriorRem" | e.PropertyName == "NumeroInteriorRem" | e.PropertyName == "ColoniaRemitente" | e.PropertyName == "CiudadDestinatario" | e.PropertyName == "CalleDestinatario" |
                e.PropertyName == "NumeroExteriorDestinatario" | e.PropertyName == "NumeroInteriorDest" | e.PropertyName == "ColoniaDestinatario")
            {
                e.Column.IsReadOnly = true;
            }
            if (e.PropertyName == "FechaInicio" | e.PropertyName == "FechaFin")
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            }
            if (e.PropertyName == "Tipo")
            {
                var cb = new DataGridComboBoxColumn();
                cb.ItemsSource = Tipo;
                cb.Header = "Tipo";
                cb.IsReadOnly = false;
                cb.SelectedItemBinding = new Binding("Tipo");
                e.Column = cb;
            }
            if (e.PropertyName == "Moneda")
            {
                var cb = new DataGridComboBoxColumn();
                cb.Header = "Moneda";
                cb.SelectedItemBinding = new Binding("Moneda");
                cb.IsReadOnly = false;
                cb.ItemsSource = Moneda;
                e.Column = cb;
            }
            if (e.PropertyName == "SubCliente")
            { 
                var cb = new DataGridComboBoxColumn();                
                cb.ItemsSource = SubCliente;
                cb.Header = "SubCliente";
                cb.IsReadOnly = false;          
                cb.SelectedItemBinding = new Binding("SubCliente");
                e.Column = cb;
                cb.EditingElementStyle = new Style(typeof(ComboBox))
                {
                    
                    Setters =
                {
                    new EventSetter(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnComboBoxSubCteSelectionChanged))
                }
                };
            }
            if (e.PropertyName == "ClaveSubcte")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "Remitente")
            {
                var cb = new DataGridComboBoxColumn();
                cb.ItemsSource = Remitente;
                cb.Header = "Remitente";
                cb.IsReadOnly = false;
                cb.SelectedItemBinding = new Binding("Remitente");
                e.Column = cb;
                cb.EditingElementStyle = new Style(typeof(ComboBox))
                {
                    Setters =
                {
                    new EventSetter(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnComboBoxRemSelectionChanged))
                }
                };
            }
            if (e.PropertyName == "Destinatario")
            {
                var cb = new DataGridComboBoxColumn();
                cb.ItemsSource = Destinatario;
                cb.Header = "Destinatario";
                cb.IsReadOnly = false;
                cb.SelectedItemBinding = new Binding("Destinatario");
                e.Column = cb;
                cb.EditingElementStyle = new Style(typeof(ComboBox))
                {
                    Setters =
                {
                    new EventSetter(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnComboBoxDestSelectionChanged))
                }
                };
            }
            if (e.PropertyName == "Ruta")
            {
                var cb = new DataGridComboBoxColumn();
                cb.ItemsSource = Ruta;
                cb.Header = "Ruta";
                cb.IsReadOnly = false;
                cb.SelectedItemBinding = new Binding("Ruta");
                e.Column = cb;
                var style = new Style(typeof(ComboBox));
                style.Setters.Add(new EventSetter(ComboBox.SelectionChangedEvent, new SelectionChangedEventHandler(OnComboBoxRutaSelectionChanged)));
                cb.EditingElementStyle = style;
            }

            if (e.PropertyName == "TransporteInternacional")
            {
                var cb = new DataGridComboBoxColumn();
                cb.ItemsSource = Transp;
                cb.Header = "Transporte Internacional";
                cb.IsReadOnly = false;
                cb.SelectedItemBinding = new Binding("TransporteInternacional");
                e.Column = cb;
            }
            if (e.PropertyName == "UsoDeTarifa")
            {
                var cb = new DataGridComboBoxColumn();
                cb.Header = "Uso De Tarifa";
                cb.SelectedItemBinding = new Binding("UsoDeTarifa");
                cb.IsReadOnly = false;
                cb.ItemsSource = UsoDeTarifa;
                e.Column = cb;
            }
        }

        private void cmb_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = this;
            var combo = sender as ComboBox;
            try
            {
                var item = (Tarifas)dgvTarifas.SelectedItem;
                if (item != null)
                {
                    item.ID = Convert.ToInt32(txtNumCliente.Text);
                    Ltarifas.Remove(item);
                    var remitente = Lremitentes.Find(r => r.NombreCompleto == combo.SelectedItem.ToString());
                    item.Remitente = remitente.NomRemitente.Trim();
                    item.IDRemitente = remitente.RemitenteID;
                    item.CiudadRem = remitente.CiudadRemitente;
                    item.CalleRemitente = remitente.Calle;
                    item.NumeroExteriorRem = remitente.NumeroExterior;
                    item.ColoniaRemitente = remitente.Colonia;
                    item.NumeroInteriorRem = remitente.NumeroInterior;
                    Ltarifas.Add(item);
                }
            }
            catch (Exception)
            {
                Tarifas item = new Tarifas();
                item.ID = Convert.ToInt32(txtNumCliente.Text);
                Ltarifas.Remove(item);
                var remitente = Lremitentes.Find(r => r.NombreCompleto == combo.SelectedItem.ToString());
                item.Remitente = remitente.NomRemitente.Trim();
                item.IDRemitente = remitente.RemitenteID;
                item.CiudadRem = remitente.CiudadRemitente;
                item.CalleRemitente = remitente.Calle;
                item.NumeroExteriorRem = remitente.NumeroExterior;
                item.ColoniaRemitente = remitente.Colonia;
                item.NumeroInteriorRem = remitente.NumeroInterior;
                Ltarifas.Add(item);
            }
        }

        private void cmb_lostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
        }

        private void cmb_gotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.IsDropDownOpen = true;
        }

        private void OnComboBoxRutaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (Tarifas)dgvTarifas.SelectedItem;
                item.ID = Convert.ToInt32(txtNumCliente.Text);
                Ltarifas.Remove(item);
                var ruta = Lrutas.Find(r => r.Ruta.Trim() == (string)e.AddedItems[0]);
                item.Ruta = ruta.Ruta.Trim();
                item.Kilometros = ruta.Kilometros;
                Ltarifas.Add(item);
                dgvTarifas.ItemsSource = Ltarifas;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnComboBoxSubCteSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (Tarifas)dgvTarifas.SelectedItem;
                item.ID = Convert.ToInt32(txtNumCliente.Text);
                Ltarifas.Remove(item);
                var Subcte = Lclientes.Find(r => r.NombreCliente.Trim() == (string)e.AddedItems[0]);
                item.SubCliente = Subcte.NombreCliente.Trim();
                item.ClaveSubcte = Subcte.Clave;
                Ltarifas.Add(item);
                dgvTarifas.ItemsSource = Ltarifas;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnComboBoxDestSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (Tarifas)dgvTarifas.SelectedItem;
                item.ID = Convert.ToInt32(txtNumCliente.Text);
                Ltarifas.Remove(item);
                var dest = Ldestinatario.Find(r => r.NombreCompleto.Trim() == (string)e.AddedItems[0]);
                item.Destinatario = dest.Destinatario.Trim();
                item.IDDestinatario = dest.DestinatarioID;
                item.CiudadDestinatario = dest.CiudadDest;
                item.CalleDestinatario = dest.Calle;
                item.NumeroExteriorDestinatario = dest.NumeroExterior;
                item.ColoniaDestinatario = dest.Colonia;
                item.NumeroInteriorDest = dest.NumeroInterior;
                Ltarifas.Add(item);
                dgvTarifas.ItemsSource = Ltarifas;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnComboBoxRemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (Tarifas)dgvTarifas.SelectedItem;
                item.ID = Convert.ToInt32(txtNumCliente.Text);
                Ltarifas.Remove(item);
                var remitente = Lremitentes.Find(r => r.NombreCompleto.Trim() == (string)e.AddedItems[0]);
                item.Remitente = remitente.NomRemitente.Trim();
                item.IDRemitente = remitente.RemitenteID;
                item.CiudadRem = remitente.CiudadRemitente;
                item.CalleRemitente = remitente.Calle;
                item.NumeroExteriorRem = remitente.NumeroExterior;
                item.ColoniaRemitente = remitente.Colonia;
                item.NumeroInteriorRem = remitente.NumeroInterior;
                Ltarifas.Add(item);
                dgvTarifas.ItemsSource = Ltarifas;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgcbRem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public void ClientesTarifasSQL()
        {
            dgvRutasHijos.ItemsSource = null;
            LTR.Clear();
            Ltarifas = new List<Tarifas>();
            dgvTarifas.ItemsSource = null;
            dgvTarifas.ItemsSource = Ltarifas;
            foreach (Cliente cl in Lclientes)
            {
                if (cl.NombreCliente == cbCliente.SelectedItem.ToString())
                {
                    txtNumCliente.Text = cl.Clave.ToString();
                    try
                    {
                        connection.Open();
                        SqlCommand sqlComm = new SqlCommand("sp_clientestarifas", connection);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        sqlComm.Parameters.AddWithValue("@ID", cl.Clave);
                        SqlDataReader reader = sqlComm.ExecuteReader();
                        while (reader.Read())
                        {
                            Tarifas tar = new Tarifas();
                            tar.ID = Convert.ToInt32(reader["IDCliente"].ToString());
                            tar.IDTarifa = Convert.ToInt32(reader["ID"].ToString());
                            tar.Remitente = reader["Remitente"].ToString().Trim();
                            tar.IDRemitente = Convert.ToInt32(reader["RemitenteID"].ToString());
                            tar.CiudadRem = reader["CiudadRemitente"].ToString().Trim();
                            tar.CalleRemitente = reader["CalleRemitente"].ToString().Trim();
                            tar.NumeroExteriorRem = reader["NumeroExteriorRemitente"].ToString().Trim();
                            tar.ColoniaRemitente = reader["ColoniaRemitente"].ToString().Trim();
                            tar.Destinatario = reader["Destinatario"].ToString().Trim();
                            tar.IDDestinatario = Convert.ToInt32(reader["DestinatarioID"].ToString());
                            tar.CiudadDestinatario = reader["CiudadDestinatario"].ToString().Trim();
                            tar.CalleDestinatario = reader["CalleDestinatario"].ToString().Trim();
                            tar.NumeroExteriorDestinatario = reader["NumeroExteriorDestinatario"].ToString().Trim();
                            tar.ColoniaDestinatario = reader["ColoniaDestinatario"].ToString().Trim();
                            tar.FechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString());
                            tar.FechaFin = Convert.ToDateTime(reader["FechaFin"].ToString());
                            tar.Ruta = reader["Ruta"].ToString().Trim();
                            tar.Moneda = reader["Moneda"].ToString().Trim();
                            tar.Tipo = reader["Tipo"].ToString().Trim();
                            tar.Kilometros = Convert.ToInt32(reader["Kilometros"].ToString());
                            tar.Flete = Convert.ToDouble(reader["Flete"].ToString());
                            tar.Seguro = Convert.ToDouble(reader["Seguro"].ToString());
                            tar.Autopistas = Convert.ToDouble(reader["Autopistas"].ToString());
                            tar.Recoleccion = Convert.ToDouble(reader["Recoleccion"].ToString());
                            tar.Cruce = Convert.ToDouble(reader["Cruce"].ToString());
                            tar.Maniobras = Convert.ToDouble(reader["Maniobras"].ToString());
                            tar.Operador = Convert.ToDouble(reader["Operador"].ToString());
                            tar.Notas = reader["Notas"].ToString().Trim();
                            tar.TransporteInternacional = reader["TransporteInternacional"].ToString().Trim();
                            tar.CPO = Convert.ToInt32(reader["CPO"].ToString());
                            tar.PorcionUSA = Convert.ToDouble(reader["PorcionUSA"].ToString());
                            tar.Accesorios = Convert.ToDouble(reader["Accesorios"].ToString());
                            tar.SubCliente = reader["Subcte"].ToString();
                            tar.ClaveSubcte = Convert.ToInt32(reader["ClaveSubcte"].ToString());
                            tar.Correo = reader["Correo"].ToString();
                            tar.Usuario = reader["Usuario"].ToString();
                            tar.FechaMod = Convert.ToDateTime(reader["FechaModificacion"].ToString());
                            tar.UsoDeTarifa = reader["UsoDeTarifa"].ToString();
                            RemitenteCB rem = Lremitentes.Find(r => r.RemitenteID == tar.IDRemitente);
                            tar.Remitente = rem.NombreCompleto.Trim();
                            DestinatarioCB dest = Ldestinatario.Find(r => r.DestinatarioID == tar.IDDestinatario);
                            tar.Destinatario = dest.NombreCompleto.Trim();
                            Ltarifas.Add(tar);
                            Ltarifas = Ltarifas.OrderBy(r => r.SubCliente).ThenBy(x=> x.Remitente).ToList();
                        }
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        connection.Close();
                        dgvTarifas.ItemsSource = Ltarifas;
                        dgvTarifas.Items.Refresh();
                    }
                }
            }
        }

        private void cbCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClientesTarifasSQL();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Ltarifas = (List<Tarifas>)dgvTarifas.ItemsSource;
            bool Exito = true;
            try
            {
                foreach (Tarifas item in Ltarifas)
                {
                    string nombreRem = item.Remitente;
                    string nombreDest = item.Destinatario;
                    if (nombreRem == null || nombreRem == ""  )
                    {
                        MessageBox.Show("Falta llenar el campo Remitente");
                        Exito = false;
                        break;
                    }
                    else if (nombreDest == null || nombreDest == "")
                    {
                        MessageBox.Show("Falta llenar el campo Destinatario");
                        Exito = false;
                        break;
                    }
                    else if (item.Ruta == null || item.Ruta == "")
                    {
                        MessageBox.Show("Falta llenar el campo Ruta");
                        Exito = false;
                        break;
                    }
                    else if ( item.Tipo == null || item.Tipo == "")
                    {
                        MessageBox.Show("Falta llenar el campo Tipo");
                        Exito = false;
                        break;
                    }
                    else if (item.Moneda == null|| item.Moneda=="")
                    {
                        MessageBox.Show("Falta llenar el campo Moneda");
                        Exito = false;
                        break;
                    }
                    else if (item.TransporteInternacional == null || item.TransporteInternacional=="")
                    {
                        MessageBox.Show("Falta llenar el campo Transporte Internacional");
                        Exito = false;
                        break;
                    }
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_SelectNomRemDest", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", item.IDRemitente);
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            nombreRem = reader1["nombre"].ToString();
                        }
                    }
                    connection.Close();
                    connection.Open();
                    SqlCommand cmd2 = new SqlCommand("sp_SelectNomRemDest", connection);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@ID", item.IDDestinatario);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            nombreDest = reader2["nombre"].ToString();
                        }
                    }
                    connection.Close();
                    connection.Open();
                    SqlCommand sqlComm = new SqlCommand("sp_Insertarclientestarifas", connection);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@IDCliente", item.ID);
                    sqlComm.Parameters.AddWithValue("@Remitente", nombreRem);
                    sqlComm.Parameters.AddWithValue("@RemitenteID", item.IDRemitente);
                    sqlComm.Parameters.AddWithValue("@CiudadRemitente", item.CiudadRem);
                    sqlComm.Parameters.AddWithValue("@CalleRemitente", item.CalleRemitente);
                    sqlComm.Parameters.AddWithValue("@NumeroExteriorRemitente", item.NumeroExteriorRem);
                    sqlComm.Parameters.AddWithValue("@NumeroInteriorRemitente", item.NumeroInteriorRem);
                    sqlComm.Parameters.AddWithValue("@ColoniaRemitente", item.ColoniaRemitente);
                    sqlComm.Parameters.AddWithValue("@Destinatario", nombreDest);
                    sqlComm.Parameters.AddWithValue("@DestinatarioID", item.IDDestinatario);
                    sqlComm.Parameters.AddWithValue("@CiudadDestinatario", item.CiudadDestinatario);
                    sqlComm.Parameters.AddWithValue("@CalleDestinatario", item.CalleDestinatario);
                    sqlComm.Parameters.AddWithValue("@NumeroExteriorDestinatario", item.NumeroExteriorDestinatario);
                    sqlComm.Parameters.AddWithValue("@NumeroInteriorDestinatario", item.NumeroInteriorDest);
                    sqlComm.Parameters.AddWithValue("@ColoniaDestinatario", item.ColoniaDestinatario);
                    sqlComm.Parameters.AddWithValue("@FechaInicio", item.FechaInicio);
                    sqlComm.Parameters.AddWithValue("@FechaFin", item.FechaFin);
                    sqlComm.Parameters.AddWithValue("@Ruta", item.Ruta);
                    sqlComm.Parameters.AddWithValue("@Kilometros", item.Kilometros);
                    sqlComm.Parameters.AddWithValue("@Tipo", item.Tipo);
                    sqlComm.Parameters.AddWithValue("@Moneda", item.Moneda);
                    sqlComm.Parameters.AddWithValue("@Flete", item.Flete);
                    sqlComm.Parameters.AddWithValue("@Seguro", item.Seguro);
                    sqlComm.Parameters.AddWithValue("@Autopistas", item.Autopistas);
                    sqlComm.Parameters.AddWithValue("@Recoleccion", item.Recoleccion);
                    sqlComm.Parameters.AddWithValue("@Cruce", item.Cruce);
                    sqlComm.Parameters.AddWithValue("@Maniobras", item.Maniobras);
                    sqlComm.Parameters.AddWithValue("@Operador", item.Operador);
                    sqlComm.Parameters.AddWithValue("@Notas", item.Notas);
                    sqlComm.Parameters.AddWithValue("@TransporteInternacional", item.TransporteInternacional);
                    sqlComm.Parameters.AddWithValue("@ID", item.IDTarifa);
                    sqlComm.Parameters.AddWithValue("@CPO", item.CPO);
                    sqlComm.Parameters.AddWithValue("@PorcionUSA", item.PorcionUSA);
                    sqlComm.Parameters.AddWithValue("@Accesorios", item.Accesorios);
                    sqlComm.Parameters.AddWithValue("@Subcte", item.SubCliente);
                    sqlComm.Parameters.AddWithValue("@ClaveSubcte", item.ClaveSubcte);
                    sqlComm.Parameters.AddWithValue("@Usuario", item.Usuario);
                    sqlComm.Parameters.AddWithValue("@FechaModificaion", item.FechaMod);
                    sqlComm.Parameters.AddWithValue("@Correo", item.Correo);
                    sqlComm.Parameters.AddWithValue("@UsoDeTarifa", item.UsoDeTarifa);
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    connection.Close();
                }
                if (Exito)
                {
                    MessageBox.Show("Tarifas agregadas correctamente", "Agregado!", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClientesTarifasSQL();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar:\n" + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                connection.Close();
            }
        }

        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
        }

        private void txtNumCliente_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente result = Lclientes.Find(x => x.Clave == Convert.ToInt32(txtNumCliente.Text));
                LTR.Clear();
                dgvRutasHijos.ItemsSource = null;
                if (result == null)
                {
                    MessageBox.Show("El cliente no existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtNumCliente.Text = "4";
                    cbCliente.SelectedItem = "TWO WAY LOGISTICS.";
                }
                else
                {
                    cbCliente.SelectedItem = result.NombreCliente;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgvTarifas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void dgvTarifas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (e.OriginalSource == dg)
            {
                LTR.Clear();
                try
                {
                    var item = (Tarifas)dgvTarifas.SelectedItem;

                    if (item != null)
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand sqlComm = new SqlCommand("sp_selectTarifasRutas", connection);
                            sqlComm.Parameters.AddWithValue("@ID", item.IDTarifa);
                            sqlComm.CommandType = CommandType.StoredProcedure;
                            SqlDataReader reader = sqlComm.ExecuteReader();
                            while (reader.Read())
                            {
                                TarifasRutas TR = new TarifasRutas();
                                TR.IDTarifa = Convert.ToInt32(reader["ID"].ToString());
                                TR.Cliente = Convert.ToInt32(reader["Cliente"].ToString());
                                TR.Destinatario = reader["Destinatario"].ToString();
                                TR.Remitente = reader["Remitente"].ToString();
                                TR.Ruta = reader["Ruta"].ToString();
                                TR.Destino = Convert.ToInt32(reader["IDDestino"].ToString());
                                TR.Origen = Convert.ToInt32(reader["IDOrigen"].ToString());
                                TR.Kilometros = Convert.ToInt32(reader["Kilometros"].ToString());
                                RemitenteCB rem = Lremitentes.Find(r => r.NomRemitente.Trim() == TR.Remitente.Trim());
                                TR.Remitente = rem.NombreCompleto;
                                DestinatarioCB dest = Ldestinatario.Find(r => r.Destinatario.Trim() == TR.Destinatario.Trim());
                                TR.Destinatario = dest.NombreCompleto;
                                LTR.Add(TR);
                            }
                            dgvRutasHijos.ItemsSource = LTR;
                            dgvRutasHijos.Items.Refresh();
                            connection.Close();
                        }
                        catch (Exception)
                        {
                            connection.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    LTR.Clear();
                    dgvRutasHijos.ItemsSource = LTR;
                    dgvRutasHijos.Items.Refresh();
                }
            }
          
          
        }

        private void btnGuardarHijo_Click(object sender, RoutedEventArgs e)
        {
            LTR = (List<TarifasRutas>)dgvRutasHijos.ItemsSource;
            var items = (Tarifas)dgvTarifas.SelectedItem;
            try
            {
                connection.Open();
                SqlCommand sqlComm2 = new SqlCommand("sp_DeleteTarifasRutas", connection);
                sqlComm2.CommandType = CommandType.StoredProcedure;
                sqlComm2.Parameters.AddWithValue("@Cliente", items.ID);
                sqlComm2.Parameters.AddWithValue("@ID", items.IDTarifa);
                sqlComm2.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                connection.Close();
            }
            try
            {
                bool Exito = true;
                foreach (TarifasRutas item in LTR)
                {
                    string nombreRem = item.Remitente;
                    int index = nombreRem.IndexOf('-');
                    nombreRem = nombreRem.Substring(0, index);
                    string nombreDest = item.Destinatario;
                    int index2 = nombreDest.IndexOf('-');
                    nombreDest = nombreDest.Substring(0, index2);
                    connection.Open();
                    SqlCommand sqlComm = new SqlCommand("sp_InsertTarifasRutas", connection);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@ID", item.IDTarifa);
                    sqlComm.Parameters.AddWithValue("@Cliente", item.Cliente);
                    sqlComm.Parameters.AddWithValue("@Ruta", item.Ruta);
                    sqlComm.Parameters.AddWithValue("@IDOrigen", item.Origen);
                    sqlComm.Parameters.AddWithValue("@IDDestino", item.Destino);
                    sqlComm.Parameters.AddWithValue("@Remitente", nombreRem);
                    sqlComm.Parameters.AddWithValue("@Destinatario", nombreDest);
                    sqlComm.Parameters.AddWithValue("@Kilometros", item.Kilometros);
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    while (reader.Read())
                    {
                        if (Convert.ToInt32(reader["Codigo"].ToString()) == 2)
                        {
                            MessageBox.Show("Rutas duplicadas:\nLa Ruta " + reader["Ruta"].ToString() + "ya existe", "Duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                            Exito = false;
                        }
                    }
                    connection.Close();
                }
                if (Exito)
                {
                    MessageBox.Show("Rutas agregadas correctamente", "Completado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar:\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                connection.Close();
            }
        }

        private void dgvRutasHijos_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                e.Column.IsReadOnly = true;
                if (e.PropertyName == "IDTarifa")
                {
                    e.Cancel = true;
                }
                if (e.PropertyName == "Origen" | e.PropertyName == "Destino")
                {
                    e.Column.IsReadOnly = true;
                }
                if (e.PropertyName == "Remitente")
                {
                    var cb = new DataGridComboBoxColumn();
                    cb.ItemsSource = Remitente;
                    cb.Header = "Remitente";
                    cb.IsReadOnly = false;
                    cb.SelectedItemBinding = new Binding("Remitente");
                    e.Column = cb;
                    cb.EditingElementStyle = new Style(typeof(ComboBox))
                    {
                        Setters =
                {
                    new EventSetter(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnComboBoxRemitenteRutaChanged))
                }
                    };
                }
                if (e.PropertyName == "Destinatario")
                {
                    var cb = new DataGridComboBoxColumn();
                    cb.ItemsSource = Destinatario;
                    cb.Header = "Destinatario";
                    cb.IsReadOnly = false;
                    cb.SelectedItemBinding = new Binding("Destinatario");
                    e.Column = cb;
                    cb.EditingElementStyle = new Style(typeof(ComboBox))
                    {
                        Setters =
                {
                    new EventSetter(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnComboBoxDestinatarioRutaChanged))
                }
                    };
                }
                if (e.PropertyName == "Ruta")
                {
                    var cb = new DataGridComboBoxColumn();
                    cb.ItemsSource = Ruta;
                    cb.Header = "Ruta";
                    cb.IsReadOnly = false;
                    cb.SelectedItemBinding = new Binding("Ruta");
                    e.Column = cb;
                    var style = new Style(typeof(ComboBox));
                    style.Setters.Add(new EventSetter(ComboBox.SelectionChangedEvent, new SelectionChangedEventHandler(OnComboBoxRutaRutasSelectionChanged)));
                    cb.EditingElementStyle = style;
                }
                if (e.PropertyName == "Kilometros")
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        private void txtNumCliente_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void dgvTarifas_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
        }

        private void dgvRutasHijos_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            try
            {
                var items = (Tarifas)dgvTarifas.SelectedItem;
                if (items != null)
                {
                    TarifasRutas tr = new TarifasRutas();
                    tr.IDTarifa = items.IDTarifa;
                    tr.Cliente = items.ID;                   
                    e.NewItem = tr;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione una tarifa", "Seleccione", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dgvRutasHijos_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            try
            {
                var items = (Tarifas)dgvTarifas.SelectedItem;
                if (items == null || items.Remitente == null || items.Destinatario == null)
                {
                    MessageBox.Show("Seleccione una tarifa valida", "Seleccione", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                e.Cancel = true;
            }
        }

        private void dgvTarifas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null)
                {
                    DataGridRow dgr = (DataGridRow)(dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex));
                    if (e.Key == Key.Delete && !dgr.IsEditing)
                    {
                        var items = (Tarifas)dgvTarifas.SelectedItem;
                        var result = MessageBox.Show(
                            "Quieres eliminar esta tarifa?",
                            "Eliminar",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question,
                            MessageBoxResult.No);
                        if (result == MessageBoxResult.Yes)
                        {
                            connection.Open();
                            SqlCommand sqlComm2 = new SqlCommand("sp_Deleteclientestarifas", connection);
                            sqlComm2.CommandType = CommandType.StoredProcedure;
                            sqlComm2.Parameters.AddWithValue("@ID", items.IDTarifa);
                            sqlComm2.ExecuteNonQuery();
                            connection.Close();
                            LTR.Clear();
                            try
                            {
                                var item = (Tarifas)dgvTarifas.SelectedItem;
                                if (item != null)
                                {
                                    try
                                    {
                                        connection.Open();
                                        SqlCommand sqlComm = new SqlCommand("sp_selectTarifasRutas", connection);
                                        sqlComm.Parameters.AddWithValue("@ID", item.IDTarifa);
                                        sqlComm.CommandType = CommandType.StoredProcedure;
                                        SqlDataReader reader = sqlComm.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            TarifasRutas TR = new TarifasRutas();
                                            TR.IDTarifa = Convert.ToInt32(reader["ID"].ToString());
                                            TR.Cliente = Convert.ToInt32(reader["Cliente"].ToString());
                                            TR.Destinatario = reader["Destinatario"].ToString();
                                            TR.Remitente = reader["Remitente"].ToString();
                                            TR.Ruta = reader["Ruta"].ToString();
                                            TR.Destino = Convert.ToInt32(reader["IDDestino"].ToString());
                                            TR.Origen = Convert.ToInt32(reader["IDOrigen"].ToString());
                                            TR.Kilometros = Convert.ToInt32(reader["Kilometros"].ToString());
                                            RemitenteCB rem = Lremitentes.Find(r => r.NomRemitente.Trim() == TR.Remitente.Trim());
                                            TR.Remitente = rem.NombreCompleto;
                                            DestinatarioCB dest = Ldestinatario.Find(r => r.Destinatario.Trim() == TR.Destinatario.Trim());
                                            TR.Destinatario = dest.NombreCompleto;
                                            LTR.Add(TR);
                                        }
                                    }
                                    catch (Exception er)
                                    {
                                        MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    finally
                                    {
                                        dgvRutasHijos.ItemsSource = LTR;
                                        dgvRutasHijos.Items.Refresh();
                                        connection.Close();
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        e.Handled = (result == MessageBoxResult.No);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvTarifas_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
        }

        private void dgvTarifas_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            try
            {
                Tarifas tar = new Tarifas();
                e.NewItem = tar;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        private void OnComboBoxRemitenteRutaChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (TarifasRutas)dgvRutasHijos.SelectedItem;
                LTR.Remove(item);
                var rem = Lremitentes.Find(r => r.NombreCompleto == (string)e.AddedItems[0]);
                item.Remitente = rem.NomRemitente.Trim();
                item.Origen = rem.RemitenteID;
                LTR.Add(item);
                dgvTarifas.ItemsSource = Ltarifas;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnComboBoxDestinatarioRutaChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (TarifasRutas)dgvRutasHijos.SelectedItem;
                LTR.Remove(item);
                var dest = Ldestinatario.Find(r => r.NombreCompleto == (string)e.AddedItems[0]);
                item.Destinatario = dest.Destinatario.Trim();
                item.Destino = dest.DestinatarioID;
                LTR.Add(item);
                dgvTarifas.ItemsSource = Ltarifas;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnComboBoxRutaRutasSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (TarifasRutas)dgvRutasHijos.SelectedItem;             
                LTR.Remove(item);
                var ruta = Lrutas.Find(r => r.Ruta.Trim() == (string)e.AddedItems[0]);
                item.Ruta = ruta.Ruta.Trim();
                item.Kilometros = ruta.Kilometros;
                LTR.Add(item);
                dgvTarifas.ItemsSource = Ltarifas;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgvTarifas_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var item = (Tarifas)dgvTarifas.SelectedItem;
            Ltarifas.Remove(item);
            item.Usuario = Usuario;
            item.FechaMod = DateTime.Now;
            Ltarifas.Add(item);          
            dgvTarifas.ItemsSource = Ltarifas;
        }

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            foreach (Cliente cliente in Lclientes)
            {
                if (cliente.NombreCliente==cbCliente.SelectedItem.ToString())
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand sqlComm = new SqlCommand("sp_clientestarifas", connection);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        sqlComm.Parameters.AddWithValue("@ID", cliente.Clave);
                        da = new SqlDataAdapter(sqlComm);
                        da.Fill(dt);
                        da.Dispose();
                    }
                    dt.TableName = "Hoja1";
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel WorkBook|*.xlsx";
                    bool? dr = sfd.ShowDialog();
                    switch (dr)
                    {
                        case true:
                            try
                            {
                                using (XLWorkbook workbook = new XLWorkbook())
                                {
                                    workbook.Worksheets.Add(dt);
                                    workbook.SaveAs(sfd.FileName);
                                    MessageBox.Show("Archivo exportado con exito");
                                    System.Diagnostics.Process.Start(sfd.FileName);
                                }
                            }
                            catch (Exception ex)
                            {
                               MessageBox.Show("Error al guardar el archivo\n" + ex);
                            }
                            break;
                        case false:
                            break;
                        default:
                            break;
                    }
                }
            }          
        }
    }
}
