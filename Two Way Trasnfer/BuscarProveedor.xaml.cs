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
    /// Lógica de interacción para BuscarProveedor.xaml
    /// </summary>
    public partial class BuscarProveedor : MetroWindow
    {
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        List<Proveedor> Lista = new List<Proveedor>();
        public int numero { get; set; }

        public BuscarProveedor()
        {
            InitializeComponent();
            txtNombre.Focus();
            try
            {
                connection.Open();
                SqlCommand sqlComm = new SqlCommand("NumProv", connection);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = sqlComm.ExecuteReader();
                while (reader.Read())
                {
                    Proveedor prov = new Proveedor();
                    prov.Numero = Convert.ToInt32(reader["numero"].ToString());
                    prov.Nombre = reader["nombre"].ToString();
                    prov.RFC = reader["rfc"].ToString();
                    Lista.Add(prov);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
                dgvProveedores.ItemsSource = Lista;
                dgvProveedores.CanUserAddRows = false;
                dgvProveedores.CanUserDeleteRows = false;
            }           
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text!="")
            {
                List<Proveedor> Lista = new List<Proveedor>();
                try
                {
                    connection.Open();
                    SqlCommand sqlComm = new SqlCommand("ProveedorLike", connection);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    while (reader.Read())
                    {
                        Proveedor prov = new Proveedor();
                        prov.Numero = Convert.ToInt32(reader["numero"].ToString());
                        prov.Nombre = reader["nombre"].ToString();
                        prov.RFC = reader["rfc"].ToString();
                        Lista.Add(prov);
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                    dgvProveedores.ItemsSource = Lista;
                }               
            }
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            dgvProveedores.ItemsSource = Lista;
        }

        private void dgvProveedores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvProveedores.SelectedCells.Count > 0)
            {
                numero = Convert.ToInt32(GetSelectedValue(dgvProveedores));
                this.Close();
            }
        }
        private string GetSelectedValue(DataGrid grid)
        {
            DataGridCellInfo cellInfo = grid.SelectedCells[0];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }
    }
}
