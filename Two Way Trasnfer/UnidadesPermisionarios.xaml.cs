using ClosedXML.Excel;
using MahApps.Metro.Controls;
using Microsoft.Win32;
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
using Two_Way_Trasnfer.Clases.UnidadPermisionario;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para UnidadesPermisionarios.xaml
    /// </summary>
    public partial class UnidadesPermisionarios : MetroWindow
    {
        string connectionString = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        string connectionString2 = "Data Source=TWL; Database=SAT; Initial Catalog=SAT ;User ID=sa; Password = Tw*way2408";
        UnidadPermisionario unidad = new UnidadPermisionario();

        public UnidadesPermisionarios()
        {
            InitializeComponent();
            txtUnidad.Focus();            
            using (SqlConnection con = new SqlConnection(connectionString2))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_selectConfigAutotransporte", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        cbConfigVehic.Items.Add(lector["ClaveNomenclatura"].ToString());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    con.Close();
                }              
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UltimaUnidadPermisionario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        unidad.ID = Convert.ToInt32(lector["ID"].ToString());
                        unidad.Unidad = lector["Unidad"].ToString();
                        unidad.Linea = lector["Linea"].ToString();
                        unidad.NombreAseg = lector["NombreAseg"].ToString();
                        unidad.NumPermSCT = lector["NumPermisoSCT"].ToString();
                        unidad.ConfigVehicular = lector["ConfigVehicular"].ToString();
                        unidad.NumPolizaSeguro = lector["NumPolizaSeguro"].ToString();
                        unidad.AnioModeloVM = lector["AnioModeloVM"].ToString();
                        unidad.PermSCT = lector["PermSCT"].ToString();
                        unidad.PlacaVM = lector["PlacaVM"].ToString();
                    }
                    txtAnio.Text = unidad.AnioModeloVM;
                    txtLinea.Text = unidad.Linea;
                    txtNombAseg.Text = unidad.NombreAseg;
                    txtNumPermSCT.Text = unidad.NumPermSCT;
                    txtNumPlacas.Text = unidad.PlacaVM;
                    if (unidad.ConfigVehicular != null)
                    {
                        cbConfigVehic.SelectedItem = unidad.ConfigVehicular.Trim();
                    }
                    txtNumPoliza.Text = unidad.NumPolizaSeguro;
                    txtPermSCT.Text = unidad.PermSCT;
                    txtUnidad.Text = unidad.Unidad;          
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    BloquearControles();
                    con.Close();
                }
            }          
        }

        public void BloquearControles()
        {
            txtAnio.IsReadOnly = true;
            txtLinea.IsReadOnly = true;
            txtNombAseg.IsReadOnly = true;
            txtNumPermSCT.IsReadOnly = true;
            txtNumPlacas.IsReadOnly = true;
            cbConfigVehic.IsEnabled = false;
            txtNumPoliza.IsReadOnly = true;
            txtPermSCT.IsReadOnly = true;
            txtUnidad.IsReadOnly = true;

        }
        public void DesbloquearControles()
        {
            txtAnio.IsReadOnly = false;
            txtLinea.IsReadOnly = false;
            txtNombAseg.IsReadOnly = false;
            txtNumPermSCT.IsReadOnly = false;
            txtNumPlacas.IsReadOnly = false;
            cbConfigVehic.IsEnabled = true;
            txtNumPoliza.IsReadOnly = false;
            txtPermSCT.IsReadOnly = false;
            txtUnidad.IsReadOnly = false;

        }
        public void LimpiarControles()
        {
            txtAnio.Text = string.Empty;
            txtLinea.Text = string.Empty;
            txtNombAseg.Text = string.Empty;
            txtNumPermSCT.Text = string.Empty;
            txtNumPlacas.Text = string.Empty;
            txtNumPoliza.Text = string.Empty;
            txtPermSCT.Text = string.Empty;
            txtUnidad.Text = string.Empty;
            cbConfigVehic.SelectedIndex = 0;

        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {          
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_PrimerUnidadPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {

                        unidad.ID = Convert.ToInt32(lector["ID"].ToString());
                        unidad.Unidad = lector["Unidad"].ToString();
                        unidad.Linea = lector["Linea"].ToString();
                        unidad.NombreAseg = lector["NombreAseg"].ToString();
                        unidad.NumPermSCT = lector["NumPermisoSCT"].ToString();
                        unidad.ConfigVehicular = lector["ConfigVehicular"].ToString();
                        unidad.NumPolizaSeguro = lector["NumPolizaSeguro"].ToString();
                        unidad.AnioModeloVM = lector["AnioModeloVM"].ToString();
                        unidad.PermSCT = lector["PermSCT"].ToString();
                        unidad.PlacaVM = lector["PlacaVM"].ToString();

                    }
                    txtAnio.Text = unidad.AnioModeloVM;
                    txtLinea.Text = unidad.Linea;
                    txtNombAseg.Text = unidad.NombreAseg;
                    txtNumPermSCT.Text = unidad.NumPermSCT;
                    txtNumPlacas.Text = unidad.PlacaVM;
                    cbConfigVehic.SelectedItem = unidad.ConfigVehicular.Trim();
                    txtNumPoliza.Text = unidad.NumPolizaSeguro;
                    txtPermSCT.Text = unidad.PermSCT;
                    txtUnidad.Text = unidad.Unidad;                 
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
            
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_AnteriorUnidadPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", unidad.ID);
                    cmd.Parameters.AddWithValue("@Unidad", unidad.Unidad);
                    cmd.Parameters.AddWithValue("@Linea", unidad.Linea);
                    cmd.Parameters.AddWithValue("@AnioModeloVM", unidad.AnioModeloVM);
                    cmd.Parameters.AddWithValue("@NombreAseg", unidad.NombreAseg);
                    cmd.Parameters.AddWithValue("@NumPermisoSCT", unidad.NumPermSCT);
                    cmd.Parameters.AddWithValue("@ConfigVehicular", unidad.ConfigVehicular);
                    cmd.Parameters.AddWithValue("@NumPolizaSeguro", unidad.NumPolizaSeguro);
                    cmd.Parameters.AddWithValue("@PermSCT", unidad.PermSCT);
                    cmd.Parameters.AddWithValue("@PlacaVM", unidad.PlacaVM);
                    SqlDataReader lector = cmd.ExecuteReader();
                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            unidad.ID = Convert.ToInt32(lector["ID"].ToString());
                            unidad.Unidad = lector["Unidad"].ToString();
                            unidad.Linea = lector["Linea"].ToString();
                            unidad.NombreAseg = lector["NombreAseg"].ToString();
                            unidad.NumPermSCT = lector["NumPermisoSCT"].ToString();
                            unidad.AnioModeloVM = lector["AnioModeloVM"].ToString();
                            unidad.ConfigVehicular = lector["ConfigVehicular"].ToString();
                            unidad.NumPolizaSeguro = lector["NumPolizaSeguro"].ToString();
                            unidad.PermSCT = lector["PermSCT"].ToString();
                            unidad.PlacaVM = lector["PlacaVM"].ToString();
                            txtAnio.Text = unidad.AnioModeloVM;
                            txtLinea.Text = unidad.Linea;
                            txtNombAseg.Text = unidad.NombreAseg;
                            txtNumPermSCT.Text = unidad.NumPermSCT;
                            txtNumPlacas.Text = unidad.PlacaVM;
                            cbConfigVehic.SelectedItem = unidad.ConfigVehicular.Trim();
                            txtNumPoliza.Text = unidad.NumPolizaSeguro;
                            txtPermSCT.Text = unidad.PermSCT;
                            txtUnidad.Text = unidad.Unidad;
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
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_SiguienteUnidadPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", unidad.ID);
                    cmd.Parameters.AddWithValue("@Unidad", unidad.Unidad);
                    cmd.Parameters.AddWithValue("@Linea", unidad.Linea);
                    cmd.Parameters.AddWithValue("@NombreAseg", unidad.NombreAseg);
                    cmd.Parameters.AddWithValue("@NumPermisoSCT", unidad.NumPermSCT);
                    cmd.Parameters.AddWithValue("@ConfigVehicular", unidad.ConfigVehicular);
                    cmd.Parameters.AddWithValue("@AnioModeloVM", unidad.AnioModeloVM);
                    cmd.Parameters.AddWithValue("@NumPolizaSeguro", unidad.NumPolizaSeguro);
                    cmd.Parameters.AddWithValue("@PermSCT", unidad.PermSCT);
                    cmd.Parameters.AddWithValue("@PlacaVM", unidad.PlacaVM);
                    SqlDataReader lector = cmd.ExecuteReader();
                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            unidad.ID = Convert.ToInt32(lector["ID"].ToString());
                            unidad.Unidad = lector["Unidad"].ToString();
                            unidad.Linea = lector["Linea"].ToString();
                            unidad.NombreAseg = lector["NombreAseg"].ToString();
                            unidad.NumPermSCT = lector["NumPermisoSCT"].ToString();
                            unidad.AnioModeloVM = lector["AnioModeloVM"].ToString();
                            unidad.ConfigVehicular = lector["ConfigVehicular"].ToString();
                            unidad.NumPolizaSeguro = lector["NumPolizaSeguro"].ToString();
                            unidad.PermSCT = lector["PermSCT"].ToString();
                            unidad.PlacaVM = lector["PlacaVM"].ToString();
                            txtAnio.Text = unidad.AnioModeloVM;
                            txtLinea.Text = unidad.Linea;
                            txtNombAseg.Text = unidad.NombreAseg;
                            txtNumPermSCT.Text = unidad.NumPermSCT;
                            txtNumPlacas.Text = unidad.PlacaVM;
                            cbConfigVehic.SelectedItem = unidad.ConfigVehicular.Trim();
                            txtNumPoliza.Text = unidad.NumPolizaSeguro;
                            txtPermSCT.Text = unidad.PermSCT;
                            txtUnidad.Text = unidad.Unidad;
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
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UltimaUnidadPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {

                        unidad.ID = Convert.ToInt32(lector["ID"].ToString());
                        unidad.Unidad = lector["Unidad"].ToString();
                        unidad.Linea = lector["Linea"].ToString();
                        unidad.NombreAseg = lector["NombreAseg"].ToString();
                        unidad.AnioModeloVM = lector["AnioModeloVM"].ToString();
                        unidad.NumPermSCT = lector["NumPermisoSCT"].ToString();
                        unidad.ConfigVehicular = lector["ConfigVehicular"].ToString();
                        unidad.NumPolizaSeguro = lector["NumPolizaSeguro"].ToString();
                        unidad.PermSCT = lector["PermSCT"].ToString();
                        unidad.PlacaVM = lector["PlacaVM"].ToString();

                    }
                    txtAnio.Text = unidad.AnioModeloVM;
                    txtLinea.Text = unidad.Linea;
                    txtNombAseg.Text = unidad.NombreAseg;
                    txtNumPermSCT.Text = unidad.NumPermSCT;
                    txtNumPlacas.Text = unidad.PlacaVM;
                    cbConfigVehic.SelectedItem = unidad.ConfigVehicular.Trim();
                    txtNumPoliza.Text = unidad.NumPolizaSeguro;
                    txtPermSCT.Text = unidad.PermSCT;
                    txtUnidad.Text = unidad.Unidad;                 
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
            txtUnidad.Focus();
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
            txtUnidad.Focus();
            unidad = new UnidadPermisionario();
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
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertUnidadPermisionario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", unidad.ID);
                    cmd.Parameters.AddWithValue("@Unidad", txtUnidad.Text);
                    cmd.Parameters.AddWithValue("@Linea", txtLinea.Text);
                    cmd.Parameters.AddWithValue("@AnioModeloVM", txtAnio.Text);
                    cmd.Parameters.AddWithValue("@NombreAseg", txtNombAseg.Text);
                    cmd.Parameters.AddWithValue("@NumPermisoSCT", txtNumPermSCT.Text);
                    cmd.Parameters.AddWithValue("@ConfigVehicular", cbConfigVehic.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@NumPolizaSeguro", txtNumPoliza.Text);
                    cmd.Parameters.AddWithValue("@PermSCT", txtPermSCT.Text);
                    cmd.Parameters.AddWithValue("@PlacaVM", txtNumPlacas.Text);
                    cmd.ExecuteNonQuery();                
                    MessageBox.Show("Agregado Con Exito!.", "Listo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {                    
                    MessageBox.Show("Error al agregar\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    con.Close();
                    BloquearControles();
                }
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
            btnExportar.IsEnabled = true;
            btnExportar.Background = new SolidColorBrush(Colors.Transparent);
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UltimaUnidadPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        unidad.ID = Convert.ToInt32(lector["ID"].ToString());
                        unidad.Unidad = lector["Unidad"].ToString();
                        unidad.Linea = lector["Linea"].ToString();
                        unidad.NombreAseg = lector["NombreAseg"].ToString();
                        unidad.NumPermSCT = lector["NumPermisoSCT"].ToString();
                        unidad.AnioModeloVM = lector["AnioModeloVM"].ToString();
                        unidad.ConfigVehicular = lector["ConfigVehicular"].ToString();
                        unidad.NumPolizaSeguro = lector["NumPolizaSeguro"].ToString();
                        unidad.PermSCT = lector["PermSCT"].ToString();
                        unidad.PlacaVM = lector["PlacaVM"].ToString();
                    }
                    txtAnio.Text = unidad.AnioModeloVM;
                    txtLinea.Text = unidad.Linea;
                    txtNombAseg.Text = unidad.NombreAseg;
                    txtNumPermSCT.Text = unidad.NumPermSCT;
                    txtNumPlacas.Text = unidad.PlacaVM;
                    cbConfigVehic.SelectedItem = unidad.ConfigVehicular.Trim();
                    txtNumPoliza.Text = unidad.NumPolizaSeguro;
                    txtPermSCT.Text = unidad.PermSCT;
                    txtUnidad.Text = unidad.Unidad;                   
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
                BuscarUnidadPermisionario bup = new BuscarUnidadPermisionario();
                bup.ShowDialog();
                unidad = bup.unidad;
                txtAnio.Text = unidad.AnioModeloVM;
                txtLinea.Text = unidad.Linea;
                txtNombAseg.Text = unidad.NombreAseg;
                txtNumPermSCT.Text = unidad.NumPermSCT;
                txtNumPlacas.Text = unidad.PlacaVM;
                cbConfigVehic.SelectedItem = unidad.ConfigVehicular.Trim();
                txtNumPoliza.Text = unidad.NumPolizaSeguro;
                txtPermSCT.Text = unidad.PermSCT;
                txtUnidad.Text = unidad.Unidad;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                BloquearControles();
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtUnidad_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtPermSCT_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumPermSCT_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNombAseg_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumPoliza_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtNumPlacas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtAnio_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void txtLinea_GotFocus(object sender, RoutedEventArgs e)
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
                    SqlCommand cmd = new SqlCommand("sp_selectPermisionariosUnidades", con);
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
    }
}
