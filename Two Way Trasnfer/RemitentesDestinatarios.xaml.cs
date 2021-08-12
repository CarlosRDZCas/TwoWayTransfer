using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
using MahApps.Metro.Controls;
using Two_Way_Trasnfer.Clases;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para RemitentesDestinatarios.xaml
    /// </summary>
    public partial class RemitentesDestinatarios : MetroWindow
    {
        public RemitenteDestinatario Remitente { get; set; } 


        SqlConnection Con = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=SAT; Initial Catalog=SAT ;User ID=sa; Password = Tw*way2408");
        public RemitentesDestinatarios()
        {
            InitializeComponent();
            BloquearControles();
            cbPais.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnGuardar.Background = new SolidColorBrush(Colors.Gray);
            btnAtras.IsEnabled = false;
            btnAtras.Background = new SolidColorBrush(Colors.Gray);
            connection.Open();
            SqlCommand cmd1 = new SqlCommand("sp_cbPais", connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector2 = cmd1.ExecuteReader();
            while (lector2.Read())
            {
                cbPais.Items.Add(lector2["c_Pais"].ToString());
            }
            connection.Close();
            Con.Open();
            SqlCommand cmd = new SqlCommand("sp_UltimoRD", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                txtID.Text = lector["ID"].ToString();
                txtNombre.Text = lector["Nombre"].ToString();          
                txtResFiscal.Text = lector["ResidenciaFiscal"].ToString();
                txtRFC.Text = lector["RFC"].ToString();
                txtNumRegIdTrip.Text = lector["NumRegIdTrip"].ToString();
                txtCalle.Text= lector["Calle"].ToString();
                txtNumExt.Text= lector["NumeroExterior"].ToString();
                txtNumInt.Text = lector["NumeroInterior"].ToString();
                cbColonia.Items.Add(lector["Colonia"].ToString());
                cbColonia.SelectedItem= lector["Colonia"].ToString();
                txtLocalidad.Text = lector["Localidad"].ToString();
                txtReferencia.Text = lector["Referencia"].ToString();
                txtMunicipio.Text = lector["Municipio"].ToString();
                txtEstado.Text= lector["Estado"].ToString();
                cbPais.SelectedItem= lector["Pais"].ToString();
                txtCodigoPostal.Text = lector["CodigoPostal"].ToString();
                txtNumMunicipio.Text = lector["NumMunicipio"].ToString();
                txtNumLocalidad.Text = lector["NumLocalidad"].ToString();
                txtNumColonia.Text = lector["NumColonia"].ToString();

            }
            Con.Close();
        }

        public void BloquearControles()
        {
            txtID.IsReadOnly = true;
            txtNombre.IsReadOnly = true;            
            txtRFC.IsReadOnly = true;
            txtNumRegIdTrip.IsReadOnly = true;
            txtResFiscal.IsReadOnly = true;
            txtCalle.IsReadOnly = true;
            txtNumExt.IsReadOnly = true;
            txtNumInt.IsReadOnly = true;
            cbColonia.IsEditable = false;
            cbColonia.IsEnabled = false;
            cbColonia.IsReadOnly = true;
            txtLocalidad.IsReadOnly = true;
            txtReferencia.IsReadOnly = true;
            txtNumColonia.IsReadOnly = true;
            txtNumLocalidad.IsReadOnly = true;
            txtNumMunicipio.IsReadOnly = true;
            txtMunicipio.IsReadOnly = true;
            txtEstado.IsReadOnly = true;
            cbPais.IsReadOnly = true;
            cbPais.IsEditable = false;
            cbPais.IsEnabled = false;            
            txtCodigoPostal.IsReadOnly = true;

        }
        public void DesbloquearControles()
        {
          
            txtNombre.IsReadOnly = false;           
            txtRFC.IsReadOnly = false;
            txtResFiscal.IsReadOnly = false;
            txtNumRegIdTrip.IsReadOnly = false;
            txtCalle.IsReadOnly = false;
            txtNumExt.IsReadOnly = false;
            txtNumInt.IsReadOnly = false;
            cbColonia.IsEditable = true;
            cbColonia.IsEnabled = true;
            cbColonia.IsReadOnly = false;
            txtLocalidad.IsReadOnly = false;
            txtReferencia.IsReadOnly = false;
            txtMunicipio.IsReadOnly = false;
            txtEstado.IsReadOnly = false;
            cbPais.IsReadOnly = false;
            cbPais.IsEditable = true;
            cbPais.IsEnabled = true; 
            txtCodigoPostal.IsReadOnly = false;

        }

        public void LimpiarControles()
        {
            txtID.Text = string.Empty;
            txtNombre.Text = string.Empty;            
            txtRFC.Text = string.Empty;
            txtResFiscal.Text = string.Empty;
            txtNumRegIdTrip.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtNumExt.Text = string.Empty;
            txtNumInt.Text = string.Empty;
            cbColonia.SelectedIndex = 0;
            txtLocalidad.Text = string.Empty;
            txtReferencia.Text = string.Empty;
            txtMunicipio.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtNumMunicipio.Text= string.Empty;
            txtNumLocalidad.Text= string.Empty;
            txtNumColonia.Text= string.Empty;
            cbPais.SelectedIndex = 0;
            txtCodigoPostal.Text = string.Empty;
            cbColonia.Items.Clear();


        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("sp_PrimerRD", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector = cmd.ExecuteReader();
            cbColonia.Items.Clear();
            while (lector.Read())
            {
                txtID.Text = lector["ID"].ToString();
                txtNombre.Text = lector["Nombre"].ToString();
                
                txtResFiscal.Text = lector["ResidenciaFiscal"].ToString();
                txtRFC.Text = lector["RFC"].ToString();
                txtNumRegIdTrip.Text = lector["NumRegIdTrip"].ToString();
                txtCalle.Text = lector["Calle"].ToString();
                txtNumExt.Text = lector["NumeroExterior"].ToString();
                txtNumInt.Text = lector["NumeroInterior"].ToString();
                cbColonia.Items.Add(lector["Colonia"].ToString());
                cbColonia.SelectedItem= lector["Colonia"].ToString();
                txtLocalidad.Text = lector["Localidad"].ToString();
                txtReferencia.Text = lector["Referencia"].ToString();
                txtMunicipio.Text = lector["Municipio"].ToString();
                txtEstado.Text = lector["Estado"].ToString();
                cbPais.SelectedItem = lector["Pais"].ToString();
                txtCodigoPostal.Text = lector["CodigoPostal"].ToString();
                txtNumMunicipio.Text = lector["NumMunicipio"].ToString();
                txtNumLocalidad.Text = lector["NumLocalidad"].ToString();
                txtNumColonia.Text = lector["NumColonia"].ToString();

            }
            Con.Close();
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
        
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("sp_AnteriorRD", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtID.Text));
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@ResidenciaFiscal", txtResFiscal.Text);
                cmd.Parameters.AddWithValue("@RFC", txtRFC.Text);
                cmd.Parameters.AddWithValue("@NumRegIdTrip", txtNumRegIdTrip.Text);
                cmd.Parameters.AddWithValue("@Calle", txtCalle.Text);
                cmd.Parameters.AddWithValue("@NumeroExterior", txtNumExt.Text);
                cmd.Parameters.AddWithValue("@NumeroInterior", txtNumInt.Text);
                cmd.Parameters.AddWithValue("@Colonia", cbColonia.SelectedItem);
                cmd.Parameters.AddWithValue("@Localidad", txtLocalidad.Text);
                cmd.Parameters.AddWithValue("@Referencia", txtReferencia.Text);
                cmd.Parameters.AddWithValue("@Municipio", txtMunicipio.Text);
                cmd.Parameters.AddWithValue("@Estado", txtEstado.Text);
                cmd.Parameters.AddWithValue("@Pais", cbPais.Text);
                cmd.Parameters.AddWithValue("@CodigoPostal", txtCodigoPostal.Text);
                cmd.Parameters.AddWithValue("NumMunicipio", txtNumMunicipio.Text);
                cmd.Parameters.AddWithValue("NumLocalidad", txtNumLocalidad.Text);
                cmd.Parameters.AddWithValue("NumColonia", txtNumColonia.Text);

                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    cbColonia.Items.Clear();
                    while (lector.Read())
                    {
                        txtID.Text = lector["ID"].ToString();
                        txtNombre.Text = lector["Nombre"].ToString();
                        txtRFC.Text = lector["RFC"].ToString();
                        txtResFiscal.Text = lector["ResidenciaFiscal"].ToString();
                        txtNumRegIdTrip.Text = lector["NumRegIdTrip"].ToString();
                        txtCalle.Text = lector["Calle"].ToString();
                        txtNumExt.Text = lector["NumeroExterior"].ToString();
                        txtNumInt.Text = lector["NumeroInterior"].ToString();
                        cbColonia.Items.Add(lector["Colonia"].ToString());
                        cbColonia.SelectedItem = lector["Colonia"].ToString();
                        txtLocalidad.Text = lector["Localidad"].ToString();
                        txtReferencia.Text = lector["Referencia"].ToString();
                        txtMunicipio.Text = lector["Municipio"].ToString();
                        txtEstado.Text = lector["Estado"].ToString();
                        cbPais.SelectedItem= lector["Pais"].ToString();
                        txtCodigoPostal.Text = lector["CodigoPostal"].ToString();
                        txtNumMunicipio.Text = lector["NumMunicipio"].ToString();
                        txtNumLocalidad.Text = lector["NumLocalidad"].ToString();
                        txtNumColonia.Text = lector["NumColonia"].ToString();

                    }
                }
                Con.Close();
            }
            catch (Exception)
            {

                Con.Close();
            }
           
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
  
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("sp_SiguienteRD", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtID.Text));
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@ResidenciaFiscal", txtResFiscal.Text);
                cmd.Parameters.AddWithValue("@RFC", txtRFC.Text);
                cmd.Parameters.AddWithValue("@NumRegIdTrip", txtNumRegIdTrip.Text);
                cmd.Parameters.AddWithValue("@Calle", txtCalle.Text);
                cmd.Parameters.AddWithValue("@NumeroExterior", txtNumExt.Text);
                cmd.Parameters.AddWithValue("@NumeroInterior", txtNumInt.Text);
                cmd.Parameters.AddWithValue("@Colonia", cbColonia.Text);
                cmd.Parameters.AddWithValue("@Localidad", txtLocalidad.Text);
                cmd.Parameters.AddWithValue("@Referencia", txtReferencia.Text);
                cmd.Parameters.AddWithValue("@Municipio", txtMunicipio.Text);
                cmd.Parameters.AddWithValue("@Estado", txtEstado.Text);
                cmd.Parameters.AddWithValue("@Pais", cbPais.Text);
                cmd.Parameters.AddWithValue("@CodigoPostal", txtCodigoPostal.Text);
                cmd.Parameters.AddWithValue("NumMunicipio", txtNumMunicipio.Text);
                cmd.Parameters.AddWithValue("NumLocalidad", txtNumLocalidad.Text);
                cmd.Parameters.AddWithValue("NumColonia", txtNumColonia.Text);


                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    cbColonia.Items.Clear();
                    while (lector.Read())
                    {
                        txtID.Text = lector["ID"].ToString();
                        txtNombre.Text = lector["Nombre"].ToString();
                        txtRFC.Text = lector["RFC"].ToString();
                        txtResFiscal.Text = lector["ResidenciaFiscal"].ToString();
                        txtNumRegIdTrip.Text = lector["NumRegIdTrip"].ToString();
                        txtCalle.Text = lector["Calle"].ToString();
                        txtNumExt.Text = lector["NumeroExterior"].ToString();
                        txtNumInt.Text = lector["NumeroInterior"].ToString();
                        cbColonia.Items.Add(lector["Colonia"].ToString());
                        cbColonia.SelectedItem = lector["Colonia"].ToString();
                        txtLocalidad.Text = lector["Localidad"].ToString();
                        txtReferencia.Text = lector["Referencia"].ToString();
                        txtMunicipio.Text = lector["Municipio"].ToString();
                        txtEstado.Text = lector["Estado"].ToString();
                        cbPais.SelectedItem= lector["Pais"].ToString();
                        txtCodigoPostal.Text = lector["CodigoPostal"].ToString();
                        txtNumMunicipio.Text = lector["NumMunicipio"].ToString();
                        txtNumLocalidad.Text = lector["NumLocalidad"].ToString();
                        txtNumColonia.Text = lector["NumColonia"].ToString();

                    }
                }
              
                Con.Close();
            }
            catch (Exception)
            {

                Con.Close();
            }
          
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("sp_UltimoRD", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector = cmd.ExecuteReader();
            cbColonia.Items.Clear();
            while (lector.Read())
            {
                txtID.Text = lector["ID"].ToString();
                txtNombre.Text = lector["Nombre"].ToString();
    
                txtRFC.Text = lector["RFC"].ToString();
                txtResFiscal.Text = lector["ResidenciaFiscal"].ToString();
                txtNumRegIdTrip.Text = lector["NumRegIdTrip"].ToString();
                txtCalle.Text = lector["Calle"].ToString();
                txtNumExt.Text = lector["NumeroExterior"].ToString();
                txtNumInt.Text = lector["NumeroInterior"].ToString();
                cbColonia.Items.Add(lector["Colonia"].ToString());
                cbColonia.SelectedItem= lector["Colonia"].ToString();
                txtLocalidad.Text = lector["Localidad"].ToString();
                txtReferencia.Text = lector["Referencia"].ToString();
                txtMunicipio.Text = lector["Municipio"].ToString();
                txtEstado.Text = lector["Estado"].ToString();
                cbPais.SelectedItem= lector["Pais"].ToString();
                txtCodigoPostal.Text = lector["CodigoPostal"].ToString();
                txtNumMunicipio.Text = lector["NumMunicipio"].ToString();
                txtNumLocalidad.Text = lector["NumLocalidad"].ToString();
                txtNumColonia.Text = lector["NumColonia"].ToString();

            }
            Con.Close();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            DesbloquearControles();
            btnGuardar.IsEnabled = true;
            btnGuardar.Background = new SolidColorBrush(Colors.Transparent);
            btnAtras.IsEnabled = true;
            btnAtras.Background = new SolidColorBrush(Colors.Transparent);
            btnPrimero.IsEnabled = false;
            btnPrimero.Background = new SolidColorBrush(Colors.Gray);
            btnAnterior.IsEnabled = false;
            btnAnterior.Background = new SolidColorBrush(Colors.Gray);
            btnSiguiente.IsEnabled = false;
            btnSiguiente.Background = new SolidColorBrush(Colors.Gray);
            btnUltimo.IsEnabled = false;
            btnUltimo.Background = new SolidColorBrush(Colors.Gray);
            btnEditar.IsEnabled = false;
            btnEditar.Background = new SolidColorBrush(Colors.Gray);
            btnNuevo.IsEnabled = false;
            btnNuevo.Background = new SolidColorBrush(Colors.Gray);
            btnBuscar.IsEnabled = false;
            btnBuscar.Background = new SolidColorBrush(Colors.Gray);
           
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            DesbloquearControles();
            btnGuardar.IsEnabled = true;
            btnGuardar.Background = new SolidColorBrush(Colors.Transparent);
            btnAtras.IsEnabled = true;
            btnAtras.Background = new SolidColorBrush(Colors.Transparent);
            btnPrimero.IsEnabled = false;
            btnPrimero.Background = new SolidColorBrush(Colors.Gray);
            btnAnterior.IsEnabled = false;
            btnAnterior.Background = new SolidColorBrush(Colors.Gray);
            btnSiguiente.IsEnabled = false;
            btnSiguiente.Background = new SolidColorBrush(Colors.Gray);
            btnUltimo.IsEnabled = false;
            btnUltimo.Background = new SolidColorBrush(Colors.Gray);
            btnEditar.IsEnabled = false;
            btnEditar.Background = new SolidColorBrush(Colors.Gray);
            btnNuevo.IsEnabled = false;
            btnNuevo.Background = new SolidColorBrush(Colors.Gray);
            btnBuscar.IsEnabled = false;
            btnBuscar.Background = new SolidColorBrush(Colors.Gray);         
            LimpiarControles();

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = false;
            btnGuardar.Background = new SolidColorBrush(Colors.Gray);
            btnAtras.IsEnabled = false;
            btnAtras.Background = new SolidColorBrush(Colors.Gray);
            btnPrimero.IsEnabled = true;
            btnPrimero.Background = new SolidColorBrush(Colors.Transparent);
            btnAnterior.IsEnabled = true;
            btnAnterior.Background = new SolidColorBrush(Colors.Transparent);
            btnSiguiente.IsEnabled = true;
            btnSiguiente.Background = new SolidColorBrush(Colors.Transparent);
            btnUltimo.IsEnabled = true;
            btnUltimo.Background = new SolidColorBrush(Colors.Transparent);
            btnEditar.IsEnabled = true;
            btnEditar.Background = new SolidColorBrush(Colors.Transparent);
            btnNuevo.IsEnabled = true;
            btnNuevo.Background = new SolidColorBrush(Colors.Transparent);
            btnBuscar.IsEnabled = true;
            btnBuscar.Background = new SolidColorBrush(Colors.Transparent);          
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("InsertRemitentesDestinatarios", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID",txtID.Text);
                cmd.Parameters.AddWithValue("@Nombre",txtNombre.Text);
                cmd.Parameters.AddWithValue("@ResidenciaFiscal", txtResFiscal.Text);
                cmd.Parameters.AddWithValue("@RFC", txtRFC.Text);
                cmd.Parameters.AddWithValue("@NumRegIdTrip", txtNumRegIdTrip.Text);
                cmd.Parameters.AddWithValue("@Calle", txtCalle.Text);
                cmd.Parameters.AddWithValue("@NumeroExterior", txtNumExt.Text);
                cmd.Parameters.AddWithValue("@NumeroInterior", txtNumInt.Text);
                cmd.Parameters.AddWithValue("@Colonia", cbColonia.Text);
                cmd.Parameters.AddWithValue("@Localidad", txtLocalidad.Text);
                cmd.Parameters.AddWithValue("@Referencia", txtReferencia.Text);
                cmd.Parameters.AddWithValue("@Municipio", txtMunicipio.Text);
                cmd.Parameters.AddWithValue("@Estado", txtEstado.Text);
                cmd.Parameters.AddWithValue("@Pais", cbPais.Text);
                cmd.Parameters.AddWithValue("@CodigoPostal", txtCodigoPostal.Text);
                cmd.Parameters.AddWithValue("NumMunicipio", txtNumMunicipio.Text);
                cmd.Parameters.AddWithValue("NumLocalidad", txtNumLocalidad.Text);
                cmd.Parameters.AddWithValue("NumColonia", txtNumColonia.Text);
                cmd.ExecuteNonQuery();
                Con.Close();
                BloquearControles();
                MessageBox.Show("Insertado Correctamente");
                Con.Open();
                SqlCommand cmd2 = new SqlCommand("sp_UltimoRD", Con);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlDataReader lector = cmd2.ExecuteReader();
                while (lector.Read())
                {
                    txtID.Text = lector["ID"].ToString();
                    txtNombre.Text = lector["Nombre"].ToString();

                    txtRFC.Text = lector["RFC"].ToString();
                    txtResFiscal.Text = lector["ResidenciaFiscal"].ToString();
                    txtNumRegIdTrip.Text = lector["NumRegIdTrip"].ToString();
                    txtCalle.Text = lector["Calle"].ToString();
                    txtNumExt.Text = lector["NumeroExterior"].ToString();
                    txtNumInt.Text = lector["NumeroInterior"].ToString();
                    cbColonia.Items.Add(lector["Colonia"].ToString());
                    cbColonia.SelectedItem = lector["Colonia"].ToString();
                    txtLocalidad.Text = lector["Localidad"].ToString();
                    txtReferencia.Text = lector["Referencia"].ToString();
                    txtMunicipio.Text = lector["Municipio"].ToString();
                    txtEstado.Text = lector["Estado"].ToString();
                    cbPais.SelectedItem= lector["Pais"].ToString();
                    txtCodigoPostal.Text = lector["CodigoPostal"].ToString();
                    txtNumMunicipio.Text = lector["NumMunicipio"].ToString();
                    txtNumLocalidad.Text = lector["NumLocalidad"].ToString();
                    txtNumColonia.Text = lector["NumColonia"].ToString();

                }
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar " + ex, "Error al insertar");
                Con.Close();
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            BloquearControles();
            btnGuardar.IsEnabled = false;
            btnGuardar.Background = new SolidColorBrush(Colors.Gray);
            btnAtras.IsEnabled = false;
            btnAtras.Background = new SolidColorBrush(Colors.Gray);
            btnPrimero.IsEnabled = true;
            btnPrimero.Background = new SolidColorBrush(Colors.Transparent);
            btnAnterior.IsEnabled = true;
            btnAnterior.Background = new SolidColorBrush(Colors.Transparent);
            btnSiguiente.IsEnabled = true;
            btnSiguiente.Background = new SolidColorBrush(Colors.Transparent);
            btnUltimo.IsEnabled = true;
            btnUltimo.Background = new SolidColorBrush(Colors.Transparent);
            btnEditar.IsEnabled = true;
            btnEditar.Background = new SolidColorBrush(Colors.Transparent);
            btnNuevo.IsEnabled = true;
            btnNuevo.Background = new SolidColorBrush(Colors.Transparent);
            btnBuscar.IsEnabled = true;
            btnBuscar.Background = new SolidColorBrush(Colors.Transparent);
            Con.Open();
            SqlCommand cmd = new SqlCommand("sp_UltimoRD", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector = cmd.ExecuteReader();
            LimpiarControles();
            while (lector.Read())
            {
                txtID.Text = lector["ID"].ToString();
                txtNombre.Text = lector["Nombre"].ToString();

                txtRFC.Text = lector["RFC"].ToString();
                txtResFiscal.Text = lector["ResidenciaFiscal"].ToString();
                txtNumRegIdTrip.Text = lector["NumRegIdTrip"].ToString();
                txtCalle.Text = lector["Calle"].ToString();
                txtNumExt.Text = lector["NumeroExterior"].ToString();
                txtNumInt.Text = lector["NumeroInterior"].ToString();
                cbColonia.Items.Add(lector["Colonia"].ToString());
                cbColonia.SelectedItem = lector["Colonia"].ToString();
                txtLocalidad.Text = lector["Localidad"].ToString();
                txtReferencia.Text = lector["Referencia"].ToString();
                txtMunicipio.Text = lector["Municipio"].ToString();
                txtEstado.Text = lector["Estado"].ToString();
                cbPais.SelectedItem= lector["Pais"].ToString();
                txtCodigoPostal.Text = lector["CodigoPostal"].ToString();
                txtNumMunicipio.Text = lector["NumMunicipio"].ToString();
                txtNumLocalidad.Text = lector["NumLocalidad"].ToString();
                txtNumColonia.Text = lector["NumColonia"].ToString();
            }
            Con.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            RemitenteDestinatario rd = new RemitenteDestinatario();
            BuscarRD brd = new BuscarRD();
            brd.ShowDialog();
            rd = brd.rds;
            if (rd is null)
            {
               MessageBox.Show("No se encontro el nombre buscado");
            }
            else
            {
                cbColonia.Items.Clear();
                txtID.Text = rd.ID.ToString();
                txtNombre.Text = rd.Nombre;
                txtRFC.Text = rd.RFC;
                txtResFiscal.Text = rd.ResidenciaFiscal;
                txtNumRegIdTrip.Text = rd.NumRegIdTrip;
                txtCalle.Text = rd.Calle;
                txtNumExt.Text = rd.NumExt;
                txtNumInt.Text = rd.NumInt;
                cbColonia.Items.Add(rd.Colonia);
                cbColonia.SelectedItem = rd.Colonia;
                txtLocalidad.Text = rd.Localidad;
                txtReferencia.Text = rd.Referencia;
                txtMunicipio.Text = rd.Municipio;
                txtEstado.Text = rd.Estado;
                cbPais.SelectedItem= rd.Pais;
                txtCodigoPostal.Text = rd.CodigoPostal;
                txtNumMunicipio.Text = rd.NumMunicipio;
                txtNumLocalidad.Text = rd.NumLocalidad;
                txtNumColonia.Text = rd.NumColonia;
            }
          
            BloquearControles();
        }     

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtID_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtRFC_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }


        private void txtResFiscal_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtResFiscal_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumEstacion_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNomEstacion_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumRegIdTrip_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtCalle_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumExt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumInt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtColonia_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtLocalidad_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtReferencia_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtMunicipio_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtEstado_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtCodigoPostal_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtPais_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumColonia_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumMunicipio_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumLocalidad_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtCodigoPostal_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void txtCodigoPostal_LostFocus(object sender, RoutedEventArgs e)
        {
            cbColonia.Items.Clear();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_CP", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CodigoPostal", txtCodigoPostal.Text);
            SqlDataReader lector = cmd.ExecuteReader();
            if (!lector.HasRows)
            {
                txtNumColonia.Text = string.Empty;
                txtNumMunicipio.Text = string.Empty;
                txtNumLocalidad.Text = string.Empty;
                txtLocalidad.Text = string.Empty;
                txtMunicipio.Text = string.Empty;
                txtEstado.Text = string.Empty;
                cbPais.SelectedIndex=0;

            }
            while (lector.Read())
            {
                cbColonia.Items.Add(lector["Colonia"].ToString());
                txtLocalidad.Text = lector["Localidad"].ToString();     
                txtMunicipio.Text = lector["Municipio"].ToString();
                txtEstado.Text = lector["EstadoAbrev"].ToString();
                cbPais.SelectedItem= lector["Pais"].ToString();
                txtNumMunicipio.Text = lector["NumMunicipio"].ToString();
                txtNumLocalidad.Text = lector["NumLocalidad"].ToString();
                txtNumColonia.Text = lector["NumColonia"].ToString();
            }
            cbColonia.SelectedIndex = 0;          
            cbColonia.IsDropDownOpen = true;

            connection.Close();
        }

        private void cbPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPais.SelectedIndex==0)
            {
                txtRFC.IsEnabled = true;
                txtRFC.IsReadOnly = false;
                txtNumRegIdTrip.IsEnabled = false;
                txtNumRegIdTrip.IsReadOnly = true;
            }
            else
            {
                txtRFC.IsEnabled = false;
                txtRFC.IsReadOnly = true;
                txtNumRegIdTrip.IsEnabled = true;
                txtNumRegIdTrip.IsReadOnly = false;
            }
        }
    }
}
