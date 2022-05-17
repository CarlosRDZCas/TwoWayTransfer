using ClosedXML.Excel;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Two_Way_Trasnfer.Clases.Permisionario;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para Permisionarios.xaml
    /// </summary>
    public partial class Permisionarios : MetroWindow
    {
        string connectionString = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        string conSAT = "Data Source=TWL; Database=SAT; Initial Catalog=SAT ;User ID=sa; Password = Tw*way2408";
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=SAT; Initial Catalog=SAT ;User ID=sa; Password = Tw*way2408");
        public bool bandera { get; set; } = true;
        Permisionario perm = new Permisionario();

        public Permisionarios()
        {
            InitializeComponent();
            txtNombre.Focus();
            try
            {
                connection.Open();
                SqlCommand cmd1 = new SqlCommand("sp_cbPais", connection);
                cmd1.CommandType = CommandType.StoredProcedure;
                SqlDataReader lector2 = cmd1.ExecuteReader();
                while (lector2.Read())
                {
                    cbPais.Items.Add(lector2["c_Pais"].ToString());
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
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UltimoPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        perm.ID = Convert.ToInt32(lector["ID"].ToString().Trim());
                        perm.NombreOperador = lector["NombreOperador"].ToString().Trim();
                        perm.NumLicencia = lector["NumLicencia"].ToString().Trim();
                        perm.RFCOperador = lector["RFCOperador"].ToString().Trim();
                        perm.NumRegIdTribOperador = lector["NumRegIdTribOperador"].ToString().Trim();
                        perm.ResidenciaFiscalOperador = lector["ResidenciaFiscalOperador"].ToString().Trim();
                        perm.Linea = lector["Linea"].ToString().Trim();
                        perm.Pais = lector["Pais"].ToString().Trim();
                        perm.Estado = lector["Estado"].ToString().Trim();
                        perm.CodigoPostal = lector["CodigoPostal"].ToString().Trim();
                    }
                    txtNombre.Text = perm.NombreOperador;
                    txtRFC.Text = perm.RFCOperador;
                    txtLicencia.Text = perm.NumLicencia;
                    txtNumRegIDTrib.Text = perm.NumRegIdTribOperador;
                    txtResidenciaFiscal.Text = perm.ResidenciaFiscalOperador;
                    txtLinea.Text = perm.Linea;
                    cbPais.SelectedItem = perm.Pais.Trim();
                    cbEstado.SelectedItem = perm.Estado.Trim();
                    txtCp.Text = perm.CodigoPostal;
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    BloquearControles();
                    Con.Close();
                }
            }
        }

        public void BloquearControles()
        {
            txtNombre.IsReadOnly = true;
            txtLinea.IsReadOnly = true;
            txtLicencia.IsReadOnly = true;
            txtNumRegIDTrib.IsReadOnly = true;
            txtResidenciaFiscal.IsReadOnly = true;
            txtRFC.IsReadOnly = true;
            txtCp.IsReadOnly = true;
            cbEstado.IsEnabled = false;
            cbPais.IsEnabled = false;
        }

        public void DesbloquearControles()
        {
            txtNombre.IsReadOnly = false;
            txtLinea.IsReadOnly = false;
            txtLicencia.IsReadOnly = false;
            txtNumRegIDTrib.IsReadOnly = false;
            txtResidenciaFiscal.IsReadOnly = false;
            txtRFC.IsReadOnly = false;
            txtCp.IsReadOnly = false;
            cbEstado.IsEnabled = true;
            cbPais.IsEnabled = true;
        }

        public void LimpiarControles()
        {
            txtLicencia.Text = string.Empty;
            txtLinea.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtNumRegIDTrib.Text = string.Empty;
            txtResidenciaFiscal.Text = string.Empty;
            txtRFC.Text = string.Empty;
            txtCp.Text = string.Empty;
            cbPais.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;

        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            bandera = true;
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_PrimerPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {

                        perm.ID = Convert.ToInt32(lector["ID"].ToString().Trim());
                        perm.NombreOperador = lector["NombreOperador"].ToString().Trim();
                        perm.NumLicencia = lector["NumLicencia"].ToString().Trim();
                        perm.RFCOperador = lector["RFCOperador"].ToString().Trim();
                        perm.NumRegIdTribOperador = lector["NumRegIdTribOperador"].ToString().Trim();
                        perm.ResidenciaFiscalOperador = lector["ResidenciaFiscalOperador"].ToString().Trim();
                        perm.Linea = lector["Linea"].ToString().Trim();
                        perm.Pais = lector["Pais"].ToString().Trim();
                        perm.Estado = lector["Estado"].ToString().Trim();
                        perm.CodigoPostal = lector["CodigoPostal"].ToString().Trim();

                    }
                    txtNombre.Text = perm.NombreOperador;
                    txtRFC.Text = perm.RFCOperador;
                    txtLicencia.Text = perm.NumLicencia;
                    txtNumRegIDTrib.Text = perm.NumRegIdTribOperador;
                    txtResidenciaFiscal.Text = perm.ResidenciaFiscalOperador;
                    txtLinea.Text = perm.Linea;
                    cbPais.SelectedItem = perm.Pais.Trim();
                    cbEstado.SelectedItem = perm.Estado.Trim();
                    txtCp.Text = perm.CodigoPostal;

                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    BloquearControles();
                    Con.Close();
                }

            }
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            bandera = true;
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_AnteriorPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", perm.ID);
                    cmd.Parameters.AddWithValue("@NombreOperador", perm.NombreOperador);
                    cmd.Parameters.AddWithValue("@RFCOperador", perm.RFCOperador);
                    cmd.Parameters.AddWithValue("@NumLicencia", perm.NumLicencia);
                    cmd.Parameters.AddWithValue("@NumRegIdTribOperador", perm.NumRegIdTribOperador);
                    cmd.Parameters.AddWithValue("@ResidenciaFiscalOperador", perm.ResidenciaFiscalOperador);
                    cmd.Parameters.AddWithValue("@Linea", perm.Linea);
                    cmd.Parameters.AddWithValue("@Pais", perm.Pais);
                    cmd.Parameters.AddWithValue("@Estado", perm.Estado);
                    cmd.Parameters.AddWithValue("@CodigoPostal", perm.CodigoPostal);
                    SqlDataReader lector = cmd.ExecuteReader();
                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            perm.ID = Convert.ToInt32(lector["ID"].ToString().Trim());
                            perm.NombreOperador = lector["NombreOperador"].ToString().Trim();
                            perm.NumLicencia = lector["NumLicencia"].ToString().Trim();
                            perm.RFCOperador = lector["RFCOperador"].ToString().Trim();
                            perm.NumRegIdTribOperador = lector["NumRegIdTribOperador"].ToString().Trim();
                            perm.ResidenciaFiscalOperador = lector["ResidenciaFiscalOperador"].ToString().Trim();
                            perm.Linea = lector["Linea"].ToString().Trim();
                            perm.Pais = lector["Pais"].ToString().Trim();
                            perm.Estado = lector["Estado"].ToString().Trim();
                            perm.CodigoPostal = lector["CodigoPostal"].ToString().Trim();
                            txtNombre.Text = perm.NombreOperador;
                            txtRFC.Text = perm.RFCOperador;
                            txtLicencia.Text = perm.NumLicencia;
                            txtNumRegIDTrib.Text = perm.NumRegIdTribOperador;
                            txtResidenciaFiscal.Text = perm.ResidenciaFiscalOperador;
                            txtLinea.Text = perm.Linea;
                            cbPais.SelectedItem = perm.Pais.Trim();
                            cbEstado.SelectedItem = perm.Estado.Trim();
                            txtCp.Text = perm.CodigoPostal;
                        }
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    BloquearControles();
                    Con.Close();
                }
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            bandera = true;
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_SiguientePermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", perm.ID);
                    cmd.Parameters.AddWithValue("@NombreOperador", perm.NombreOperador);
                    cmd.Parameters.AddWithValue("@RFCOperador", perm.RFCOperador);
                    cmd.Parameters.AddWithValue("@NumLicencia", perm.NumLicencia);
                    cmd.Parameters.AddWithValue("@NumRegIdTribOperador", perm.NumRegIdTribOperador);
                    cmd.Parameters.AddWithValue("@ResidenciaFiscalOperador", perm.ResidenciaFiscalOperador);
                    cmd.Parameters.AddWithValue("@Linea", perm.Linea);
                    cmd.Parameters.AddWithValue("@Pais", perm.Pais);
                    cmd.Parameters.AddWithValue("@Estado", perm.Estado);
                    cmd.Parameters.AddWithValue("@CodigoPostal", perm.CodigoPostal);
                    SqlDataReader lector = cmd.ExecuteReader();
                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            perm.ID = Convert.ToInt32(lector["ID"].ToString().Trim());
                            perm.NombreOperador = lector["NombreOperador"].ToString().Trim();
                            perm.NumLicencia = lector["NumLicencia"].ToString().Trim();
                            perm.RFCOperador = lector["RFCOperador"].ToString().Trim();
                            perm.NumRegIdTribOperador = lector["NumRegIdTribOperador"].ToString().Trim();
                            perm.ResidenciaFiscalOperador = lector["ResidenciaFiscalOperador"].ToString().Trim();
                            perm.Linea = lector["Linea"].ToString().Trim();
                            perm.Pais = lector["Pais"].ToString().Trim();
                            perm.Estado = lector["Estado"].ToString().Trim();
                            perm.CodigoPostal = lector["CodigoPostal"].ToString().Trim();
                            txtNombre.Text = perm.NombreOperador;
                            txtRFC.Text = perm.RFCOperador;
                            txtLicencia.Text = perm.NumLicencia;
                            txtNumRegIDTrib.Text = perm.NumRegIdTribOperador;
                            txtResidenciaFiscal.Text = perm.ResidenciaFiscalOperador;
                            txtLinea.Text = perm.Linea;
                            cbPais.SelectedItem = perm.Pais.Trim();
                            cbEstado.SelectedItem = perm.Estado.Trim();
                            txtCp.Text = perm.CodigoPostal;
                        }
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    BloquearControles();
                    Con.Close();
                }
            }
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            bandera = true;
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UltimoPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        perm.ID = Convert.ToInt32(lector["ID"].ToString().Trim());
                        perm.NombreOperador = lector["NombreOperador"].ToString().Trim();
                        perm.NumLicencia = lector["NumLicencia"].ToString().Trim();
                        perm.RFCOperador = lector["RFCOperador"].ToString().Trim();
                        perm.NumRegIdTribOperador = lector["NumRegIdTribOperador"].ToString().Trim();
                        perm.ResidenciaFiscalOperador = lector["ResidenciaFiscalOperador"].ToString().Trim();
                        perm.Linea = lector["Linea"].ToString().Trim();
                        perm.Pais = lector["Pais"].ToString().Trim();
                        perm.Estado = lector["Estado"].ToString().Trim();
                        perm.CodigoPostal = lector["CodigoPostal"].ToString().Trim();
                    }
                    txtNombre.Text = perm.NombreOperador;
                    txtRFC.Text = perm.RFCOperador;
                    txtLicencia.Text = perm.NumLicencia;
                    txtNumRegIDTrib.Text = perm.NumRegIdTribOperador;
                    txtResidenciaFiscal.Text = perm.ResidenciaFiscalOperador;
                    txtLinea.Text = perm.Linea;
                    cbPais.SelectedItem = perm.Pais.Trim();
                    cbEstado.SelectedItem = perm.Estado.Trim();
                    txtCp.Text = perm.CodigoPostal;
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    BloquearControles();
                    Con.Close();
                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            DesbloquearControles();
            txtNombre.Focus();


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
            btnExportar.IsEnabled = false;
            btnExportar.Background = new SolidColorBrush(Colors.Gray);
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            DesbloquearControles();
            txtNombre.Focus();

            if (cbPais.SelectedIndex > 0)
            {
                txtRFC.Text = "XEXX010101000";
                txtRFC.IsEnabled = false;
                txtNumRegIDTrib.IsEnabled = true;

            }
            else
            {
                txtRFC.Text = "";
                txtNumRegIDTrib.IsEnabled = false;
                txtRFC.IsEnabled = true;
            }

            perm = new Permisionario();
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
            btnExportar.IsEnabled = false;
            btnExportar.Background = new SolidColorBrush(Colors.Gray);
            LimpiarControles();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    if (!((cbPais.SelectedItem.ToString() == "MEX" && txtRFC.Text == "") || (cbPais.SelectedItem.ToString() != "MEX" && txtNumRegIDTrib.Text == "")))
                    {
                        if (txtCp.Text.Length >= 5)
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("sp_InsertPermisionario", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", perm.ID);
                            cmd.Parameters.AddWithValue("@RFCOperador", txtRFC.Text);
                            cmd.Parameters.AddWithValue("@NombreOperador", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@NumLicencia", txtLicencia.Text);
                            cmd.Parameters.AddWithValue("@NumRegIdTribOperador", txtNumRegIDTrib.Text);
                            cmd.Parameters.AddWithValue("@ResidenciaFiscalOperador", txtResidenciaFiscal.Text);
                            cmd.Parameters.AddWithValue("@Linea", txtLinea.Text);
                            cmd.Parameters.AddWithValue("@Pais", cbPais.SelectedItem);
                            cmd.Parameters.AddWithValue("@Estado", cbEstado.SelectedItem);
                            cmd.Parameters.AddWithValue("@CodigoPostal", txtCp.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Agregado Con Exito!.", "Agregado!", MessageBoxButton.OK, MessageBoxImage.Information);
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
                            btnExportar.IsEnabled = true;
                            btnExportar.Background = new SolidColorBrush(Colors.Transparent);
                        }
                        else
                        {
                            MessageBox.Show("El codigo postal debe contener al menos 5 caracteres", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Es necesario agregar el RFC o el Num Reg ID Trib del operador.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    con.Close();
                }

            }

        }

        private void txtCp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCp.Text.Trim() != "" && cbPais.SelectedItem.ToString() == "MEX")
            {
                using (SqlConnection con = new SqlConnection(conSAT))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_ValidarCP", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@c_CP", txtCp.Text);
                        SqlDataReader lector = cmd.ExecuteReader();
                        if (!lector.HasRows)
                        {
                            MessageBox.Show("El codigo postal no es valido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            btnGuardar.IsEnabled = false;

                        }
                        else
                        {
                            while (lector.Read())
                            {
                                cbEstado.SelectedItem = lector["c_Estado"].ToString();
                            }
                            btnGuardar.IsEnabled = true;

                        }

                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            BloquearControles();
            txtNumRegIDTrib.IsEnabled = true;
            txtRFC.IsEnabled = true;
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
            btnExportar.IsEnabled = true;
            btnExportar.Background = new SolidColorBrush(Colors.Transparent);
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UltimoPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        perm.ID = Convert.ToInt32(lector["ID"].ToString().Trim());
                        perm.NombreOperador = lector["NombreOperador"].ToString().Trim();
                        perm.NumLicencia = lector["NumLicencia"].ToString().Trim();
                        perm.RFCOperador = lector["RFCOperador"].ToString().Trim();
                        perm.NumRegIdTribOperador = lector["NumRegIdTribOperador"].ToString().Trim();
                        perm.ResidenciaFiscalOperador = lector["ResidenciaFiscalOperador"].ToString().Trim();
                        perm.Linea = lector["Linea"].ToString().Trim();
                        perm.Pais = lector["Pais"].ToString().Trim();
                        perm.Estado = lector["Estado"].ToString().Trim();
                        perm.CodigoPostal = lector["CodigoPostal"].ToString().Trim();
                    }
                    txtNombre.Text = perm.NombreOperador;
                    txtRFC.Text = perm.RFCOperador;
                    txtLicencia.Text = perm.NumLicencia;
                    txtNumRegIDTrib.Text = perm.NumRegIdTribOperador;
                    txtResidenciaFiscal.Text = perm.ResidenciaFiscalOperador;
                    txtLinea.Text = perm.Linea;
                    cbPais.SelectedItem = perm.Pais.Trim();
                    cbEstado.SelectedItem = perm.Estado.Trim();
                    txtCp.Text = perm.CodigoPostal;
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    BloquearControles();
                    Con.Close();
                }
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BuscarPermisionario bp = new BuscarPermisionario();
                bp.ShowDialog();
                perm = bp.perm;
                if (bp.perm != null)
                {
                    txtNombre.Text = perm.NombreOperador;
                    txtRFC.Text = perm.RFCOperador;
                    txtLicencia.Text = perm.NumLicencia;
                    txtNumRegIDTrib.Text = perm.NumRegIdTribOperador;
                    txtResidenciaFiscal.Text = perm.ResidenciaFiscalOperador;
                    txtLinea.Text = perm.Linea;
                    cbPais.SelectedItem = perm.Pais.Trim();
                    cbEstado.SelectedItem = perm.Estado.Trim();
                    txtCp.Text = perm.CodigoPostal;
                }

            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cbPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbEstado.Items.Clear();
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                SqlCommand cmd1 = new SqlCommand("sp_Estados", connection);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Pais", cbPais.SelectedItem.ToString());
                SqlDataReader lector2 = cmd1.ExecuteReader();
                while (lector2.Read())
                {
                    cbEstado.Items.Add(lector2["c_Estado"].ToString());
                }
                cbEstado.SelectedItem = null;
                lector2.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            if (bandera == false)
            {
                if (cbPais.SelectedIndex > 0)
                {
                    txtRFC.Text = "XEXX010101000";
                    txtRFC.IsEnabled = false;
                    txtNumRegIDTrib.IsEnabled = true;

                }
                else
                {
                    txtRFC.Text = "";
                    txtNumRegIDTrib.IsEnabled = false;
                    txtRFC.IsEnabled = true;
                }
            }
        }

        private void cbPais_DropDownOpened(object sender, EventArgs e)
        {
            bandera = false;
        }

        private void txtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtLinea_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtCp_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtRFC_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumRegIDTrib_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtResidenciaFiscal_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtLicencia_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_selectPermisionarios", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
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
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Error al obtener los datos");
            }
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtNombre.Text = RemoveSpecialCharacters(txtNombre.Text);

        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == 'ñ' || c == 'Ñ')
                {
                    sb.Append(c);

                }
                else
                {

                }
            }
            return sb.ToString();
        }

        private void txtLinea_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtLinea.Text = RemoveSpecialCharacters(txtLinea.Text);
        }

        private void txtRFC_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtRFC.Text = RemoveSpecialCharacters(txtRFC.Text);
        }

        private void txtLicencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtLicencia.Text = RemoveSpecialCharacters(txtLicencia.Text);
        }
    }
}
