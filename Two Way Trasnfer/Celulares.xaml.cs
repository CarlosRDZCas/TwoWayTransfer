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
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Two_Way_Trasnfer.Clases;
using Two_Way_Trasnfer.DataSet;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para Celulares.xaml
    /// </summary>
    public partial class Celulares : MetroWindow
    {
        string connectionstring2 = "Data Source=SOPORTE\\SQLEXPRESS; Database=FOXPRO; Initial Catalog=FOXPRO ;User ID=sa; Password = Twoway2408";
        string connectionString = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";

        List<Celular> cels = new List<Celular>();
        List<Empleado> empleados = new List<Empleado>();

        public Celulares()
        {
            InitializeComponent();          
            ActualizarDGV();
            LlenarCB();
        }

        public void LlenarCB() 
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring2))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarEmpleados", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        Empleado emp = new Empleado();
                        emp.Clave = lector["Clave"].ToString().Trim();
                        emp.Nombre = lector["Nombre"].ToString().Trim();
                        emp.Puesto = lector["Puesto"].ToString().Trim();
                        emp.Depto = lector["Depto"].ToString().Trim();
                        emp.Serie = lector["Encargado"].ToString().Trim();
                        emp.TelEmp = lector["Telemp"].ToString().Trim();
                        empleados.Add(emp);
                        cbEmpleado.Items.Add(emp.Nombre);                      
                    }
                    empleados.Sort((x, y) => x.Nombre.CompareTo(y.Nombre));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error:\n" + ex, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); 
            }        
        }

        public void ActualizarDGV()
        {
            try
            {
                dgvCelulares.ItemsSource = null;
                cels.Clear();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_selectCelulares", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        Celular cel = new Celular();
                        cel.ID = Convert.ToInt32(lector["ID"].ToString());
                        cel.Empleado = lector["Empleado"].ToString();
                        cel.NombreCelular = lector["NombreEquipo"].ToString();
                        cel.Departamento = lector["Departamento"].ToString();
                        cel.Puesto = lector["Puesto"].ToString();
                        cel.Serie = lector["Serie"].ToString();
                        cel.Equipo = lector["Equipo"].ToString();
                        cel.Descripcion = lector["Descripcion"].ToString();
                        cel.Compañia = lector["Compania"].ToString();
                        cel.NumTel = lector["NumTel"].ToString();
                        cel.Asignado = (bool)lector["Asignado"];
                        if (lector["FechaAsignado"].ToString() == "1/1/1990 12:00:00 AM")
                        {
                            cel.FechaAsignacion = null;
                        }
                        else
                        {
                            cel.FechaAsignacion = DateTime.Parse(lector["FechaAsignado"].ToString());
                        }
                        cel.Costo = Convert.ToDouble(lector["Costo"].ToString());
                        if (lector["FechaBaja"].ToString() == "1/1/1990 12:00:00 AM")
                        {
                            cel.FechaBaja = null;
                        }
                        else
                        {
                            cel.FechaBaja = DateTime.Parse(lector["FechaBaja"].ToString());
                        }
                        if (lector["FechaRetiro"].ToString()== "1/1/1990 12:00:00 AM")
                        {
                            cel.FechaRetiro = null;
                        }
                        else
                        {
                            cel.FechaRetiro = DateTime.Parse(lector["FechaRetiro"].ToString());
                        }
                        cels.Add(cel);
                    }
                    con.Close();
                }
                dgvCelulares.ItemsSource = cels;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error:\n" + ex, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Celular cel = new Celular();
                cel.Empleado = cbEmpleado.SelectedItem.ToString();
                cel.NombreCelular = txtNombreEquipo.Text;
                cel.Departamento = txtDepartamento.Text;
                cel.Puesto = txtPuesto.Text;
                cel.Serie = txtSerie.Text;
                cel.Equipo = txtEquipo.Text;
                cel.Descripcion = txtDescripcion.Text;
                cel.Compañia = txtCompañia.Text;
                cel.NumTel = txtNumTel.Text;
                cel.FechaAsignacion = dtmAsignacion.SelectedDate;
                cel.Asignado=(bool)chbAsignado.IsChecked ? cel.Asignado = true : cel.Asignado = false;
                double valorNumerico = 0;
                if (double.TryParse(txtCosto.Text, out valorNumerico))
                {
                    cel.Costo = Convert.ToDouble(valorNumerico);
                }
                cel.FechaBaja = dtmAsignacion.SelectedDate;
                cel.FechaRetiro = dtmFechaRetiro.SelectedDate;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_insertCelulares", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empleado", cel.Empleado);
                    cmd.Parameters.AddWithValue("@NombreEquipo", cel.NombreCelular);
                    cmd.Parameters.AddWithValue("@Departamento", cel.Departamento);
                    cmd.Parameters.AddWithValue("@Puesto", cel.Puesto);
                    cmd.Parameters.AddWithValue("@Serie", cel.Serie);
                    cmd.Parameters.AddWithValue("@Equipo", cel.Equipo);
                    cmd.Parameters.AddWithValue("@Descripcion", cel.Descripcion);
                    cmd.Parameters.AddWithValue("@Compania", cel.Compañia);
                    cmd.Parameters.AddWithValue("@NumTel", cel.NumTel);
                    cmd.Parameters.AddWithValue("@Asignado", cel.Asignado);
                    cmd.Parameters.AddWithValue("@FechaAsignado", cel.FechaAsignacion == null ? cel.FechaAsignacion = DateTime.Parse("01/01/1990") : cel.FechaAsignacion);
                    cmd.Parameters.AddWithValue("@Costo", cel.Costo);
                    cmd.Parameters.AddWithValue("@FechaBaja", cel.FechaBaja == null ? cel.FechaBaja = DateTime.Parse("01/01/1990") : cel.FechaBaja);
                    cmd.Parameters.AddWithValue("@FechaRetiro", cel.FechaRetiro == null ? cel.FechaRetiro = DateTime.Parse("01/01/1990") : cel.FechaRetiro);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                ActualizarDGV();
                System.Windows.Forms.MessageBox.Show("Agregado con exito!", "Agregado", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error:\n" + ex,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Celular cel = new Celular();
                cel.ID = Convert.ToInt32(txtID.Text);
                cel.Empleado = cbEmpleado.SelectedItem.ToString();
                cel.NombreCelular = txtNombreEquipo.Text;
                cel.Departamento = txtDepartamento.Text;
                cel.Asignado = (bool)chbAsignado.IsChecked;
                cel.Puesto = txtPuesto.Text;
                cel.Serie = txtSerie.Text;
                cel.Equipo = txtEquipo.Text;
                cel.Descripcion = txtDescripcion.Text;
                cel.Compañia = txtCompañia.Text;
                cel.NumTel = txtNumTel.Text;
                cel.FechaAsignacion = dtmAsignacion.SelectedDate;
                cel.FechaRetiro = dtmFechaRetiro.SelectedDate;
                double valorNumerico = 0;
                if (double.TryParse(txtCosto.Text, out valorNumerico))
                {
                    cel.Costo = Convert.ToDouble(valorNumerico);
                }
                cel.FechaBaja = dtmBaja.SelectedDate;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_editarCelular", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", cel.ID);
                    cmd.Parameters.AddWithValue("@Empleado", cel.Empleado);
                    cmd.Parameters.AddWithValue("@NombreEquipo", cel.NombreCelular);
                    cmd.Parameters.AddWithValue("@Departamento", cel.Departamento);
                    cmd.Parameters.AddWithValue("@Puesto", cel.Puesto);
                    cmd.Parameters.AddWithValue("@Serie", cel.Serie);
                    cmd.Parameters.AddWithValue("@Equipo", cel.Equipo);
                    cmd.Parameters.AddWithValue("@Descripcion", cel.Descripcion);
                    cmd.Parameters.AddWithValue("@Compania", cel.Compañia);
                    cmd.Parameters.AddWithValue("@NumTel", cel.NumTel);
                    cmd.Parameters.AddWithValue("@FechaAsignado", cel.FechaAsignacion == null ? cel.FechaAsignacion = DateTime.Parse("01/01/1990") : cel.FechaAsignacion);
                    cmd.Parameters.AddWithValue("@Costo", cel.Costo);
                    cmd.Parameters.AddWithValue("@Asignado", cel.Asignado);
                    cmd.Parameters.AddWithValue("@FechaBaja", cel.FechaBaja == null ? cel.FechaBaja = DateTime.Parse("01/01/1990") : cel.FechaBaja);
                    cmd.Parameters.AddWithValue("@FechaRetiro", cel.FechaRetiro == null ? cel.FechaRetiro = DateTime.Parse("01/01/1990") : cel.FechaRetiro);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                ActualizarDGV();
                txtID.Text = cel.ID.ToString();
                cbEmpleado.SelectedItem = cel.Empleado;
                txtNombreEquipo.Text = cel.NombreCelular;
                txtDepartamento.Text = cel.Departamento;
                txtPuesto.Text = cel.Puesto;
                chbAsignado.IsChecked = cel.Asignado;
                txtSerie.Text = cel.Serie;
                txtEquipo.Text = cel.Equipo;
                txtDescripcion.Text = cel.Descripcion;
                txtCompañia.Text = cel.Compañia;
                txtNumTel.Text = cel.NumTel;
                dtmAsignacion.SelectedDate = cel.FechaAsignacion;
                txtCosto.Text = cel.Costo.ToString();
                dtmBaja.SelectedDate = cel.FechaBaja;
                dtmFechaRetiro.SelectedDate = cel.FechaRetiro;
                System.Windows.Forms.MessageBox.Show("Modificado con exito!","Actualizado",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error:\n" + ex, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }     
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Celular cel = new Celular();
                int valorNumerico = 0;
                if (int.TryParse(txtID.Text, out valorNumerico))
                {
                    cel.ID = Convert.ToInt32(valorNumerico);
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_deleteCelular", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", cel.ID);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    ActualizarDGV();
                    System.Windows.Forms.MessageBox.Show("Eliminado con exito!", "Eliminado", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }               
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error:\n"+ex, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgvCelulares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Celular cel = (Celular)dgvCelulares.SelectedItem;
                if (cel != null)
                {
                    txtID.Text = cel.ID.ToString();
                    cbEmpleado.SelectedItem = cel.Empleado;
                    txtNombreEquipo.Text = cel.NombreCelular;
                    txtDepartamento.Text = cel.Departamento;
                    txtPuesto.Text = cel.Puesto;
                    txtSerie.Text = cel.Serie;
                    txtEquipo.Text = cel.Equipo;
                    txtDescripcion.Text = cel.Descripcion;
                    txtCompañia.Text = cel.Compañia;
                    chbAsignado.IsChecked = cel.Asignado;
                    txtNumTel.Text = cel.NumTel;
                    dtmAsignacion.SelectedDate = cel.FechaAsignacion;
                    txtCosto.Text = cel.Costo.ToString();
                    dtmBaja.SelectedDate = cel.FechaBaja;
                    dtmFechaRetiro.SelectedDate = cel.FechaRetiro;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error:\n" + ex, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void cbEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbEmpleado.SelectedItem != null)
                {
                    Empleado emp = empleados.Find(r => r.Nombre == cbEmpleado.SelectedItem.ToString());
                    Celular cel = cels.Find(r => r.Empleado == emp.Nombre);
                    if (cels.Contains(cel))
                    {
                        txtID.Text = cel.ID.ToString();
                        cbEmpleado.SelectedItem = cel.Empleado.Trim();
                        txtNombreEquipo.Text = cel.NombreCelular;
                        txtDepartamento.Text = cel.Departamento;
                        txtPuesto.Text = cel.Puesto;
                        txtSerie.Text = cel.Serie;
                        txtEquipo.Text = cel.Equipo;
                        chbAsignado.IsChecked = cel.Asignado;
                        txtDescripcion.Text = cel.Descripcion;
                        txtCompañia.Text = cel.Compañia;
                        txtNumTel.Text = cel.NumTel;
                        dtmAsignacion.SelectedDate = cel.FechaAsignacion;
                        txtCosto.Text = cel.Costo.ToString();
                        dtmBaja.SelectedDate = cel.FechaBaja;
                        dtmFechaRetiro.SelectedDate = cel.FechaRetiro;
                    }
                    else
                    {
                        txtNombreEquipo.Text = "";
                        txtCompañia.Text = "";
                        txtCosto.Text = "";
                        txtDescripcion.Text = "";
                        txtEquipo.Text = "";
                        dtmBaja.SelectedDate = null;
                        dtmAsignacion.SelectedDate = null;
                        txtID.Text = "";
                        chbAsignado.IsChecked = false;
                        txtPuesto.Text = emp.Puesto;
                        txtDepartamento.Text = emp.Depto;
                        txtSerie.Text = emp.Serie;
                        txtNumTel.Text = emp.TelEmp;
                        dtmFechaRetiro.SelectedDate = null;
                    }
                }
                else
                {
                    txtNombreEquipo.Text = "";
                    txtCompañia.Text = "";
                    txtCosto.Text = "";
                    dtmBaja.SelectedDate = null;
                    dtmAsignacion.SelectedDate = null;
                    txtDescripcion.Text = "";
                    txtEquipo.Text = "";
                    chbAsignado.IsChecked = false;
                    dtmFechaRetiro.SelectedDate = null;
                    txtID.Text = "";
                    txtPuesto.Text = "";
                    txtDepartamento.Text = "";
                    txtSerie.Text = "";
                    txtNumTel.Text = "";
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error:\n" + ex, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void txtCiudad_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtNombreEquipo_LostFocus(object sender, RoutedEventArgs e)
        {
            
                
        }

        private void btnBuscarNombre_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnBuscarNombre_Click_1(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FiltrarCelular", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreEquipo", txtNombreEquipo.Text);
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    dgvCelulares.ItemsSource = null;
                    cels.Clear();
                    while (lector.Read())
                    {
                        Celular cel = new Celular();
                        cel.ID = Convert.ToInt32(lector["ID"].ToString());
                        cel.Empleado = lector["Empleado"].ToString();
                        cel.NombreCelular = lector["NombreEquipo"].ToString();
                        cel.Departamento = lector["Departamento"].ToString();
                        cel.Puesto = lector["Puesto"].ToString();
                        cel.Serie = lector["Serie"].ToString();
                        cel.Equipo = lector["Equipo"].ToString();
                        cel.Descripcion = lector["Descripcion"].ToString();
                        cel.Compañia = lector["Compania"].ToString();
                        cel.NumTel = lector["NumTel"].ToString();
                        cel.Asignado = (bool)lector["Asignado"];
                        if (lector["FechaAsignado"].ToString() == "1/1/1990 12:00:00 AM")
                        {
                            cel.FechaAsignacion = null;
                        }
                        else
                        {
                            cel.FechaAsignacion = DateTime.Parse(lector["FechaAsignado"].ToString());
                        }
                        cel.Costo = Convert.ToDouble(lector["Costo"].ToString());
                        if (lector["FechaBaja"].ToString() == "1/1/1990 12:00:00 AM")
                        {
                            cel.FechaBaja = null;
                        }
                        else
                        {
                            cel.FechaBaja = DateTime.Parse(lector["FechaBaja"].ToString());
                        }
                        if (lector["FechaRetiro"].ToString() == "1/1/1990 12:00:00 AM")
                        {
                            cel.FechaRetiro = null;
                        }
                        else
                        {
                            cel.FechaRetiro = DateTime.Parse(lector["FechaRetiro"].ToString());
                        }
                        cels.Add(cel);
                    }
                    con.Close();
                }
                dgvCelulares.ItemsSource = cels;
            }
        }

        private void bntMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            ActualizarDGV();
        }

        private void btnExportarEquiposUsados_Click(object sender, RoutedEventArgs e)
        {
            if (cbEmpleado.SelectedItem != null)
            {
                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("sp_selectCelularesEmpleado", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Empleado", cbEmpleado.SelectedItem.ToString());
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
                    System.Windows.Forms.MessageBox.Show("Error al obtener los datos del empleado");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Seleccione un empleado");
            }           
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            if (cbEmpleado.SelectedItem!=null)
            {
                object oMissing = System.Reflection.Missing.Value;
                Excel.Application excelApp = null;
                Excel.Workbook workbook = null;
                Excel.Worksheet worksheet = null;

                try
                {
                    excelApp = new Excel.Application();
                    excelApp.DisplayAlerts = false;
                    excelApp.Visible = true;
                    workbook = excelApp.Workbooks.Open(@"P:\Apps\TwoWayTransfer\AsignacionEquipo.xlsx", 0, false, 5, "", "", true,Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
                    worksheet = excelApp.ActiveSheet as Excel.Worksheet;
                    worksheet.Cells[8, 1].Value = txtID.Text;
                    worksheet.Cells[13, 1].Value = txtNombreEquipo.Text;
                    worksheet.Cells[13, 3].Value = "Celular " + txtEquipo.Text;
                    worksheet.Cells[13, 6].Value = "$" + txtCosto.Text;
                    worksheet.Cells[14, 3].Value = txtSerie.Text;
                    worksheet.Cells[25, 3].Value = txtDescripcion.Text;
                    worksheet.Cells[27, 3].Value = txtNumTel.Text;
                    Empleado emp = empleados.Find(x => x.Nombre == cbEmpleado.SelectedItem.ToString());
                    worksheet.Cells[30, 3].Value = emp.Clave.ToString();
                    worksheet.Cells[33, 3].Value = emp.Nombre;
                    worksheet.Cells[35, 3].Value = emp.Puesto;
                    worksheet.Cells[41, 1].Value = "SON 1 ARTICULO(S) CON UN VALOR DE $" + txtCosto.Text + " PESOS";
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}

