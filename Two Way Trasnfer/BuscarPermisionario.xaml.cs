using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
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
    /// Lógica de interacción para BuscarPermisionario.xaml
    /// </summary>
    public partial class BuscarPermisionario : MetroWindow
    {
        string connectionString = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        List<Permisionario> list = new List<Permisionario>();
        public Permisionario perm { get; set; }

        public BuscarPermisionario()
        {
            InitializeComponent();
            txtNombre.Focus();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            list.Clear();
            dgvPerm.ItemsSource = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_BuscarPermisionario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreOperador", txtNombre.Text);
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        Permisionario perm = new Permisionario();
                        perm.ID = Convert.ToInt32(lector["ID"].ToString());
                        perm.NombreOperador = lector["NombreOperador"].ToString();
                        perm.NumLicencia = lector["NumLicencia"].ToString();
                        perm.RFCOperador = lector["RFCOperador"].ToString();
                        perm.NumRegIdTribOperador = lector["NumRegIdTribOperador"].ToString();
                        perm.ResidenciaFiscalOperador = lector["ResidenciaFiscalOperador"].ToString();
                        perm.Linea = lector["Linea"].ToString();
                        perm.Pais = lector["Pais"].ToString();
                        perm.Estado = lector["Estado"].ToString();
                        perm.CodigoPostal = lector["CodigoPostal"].ToString();
                        list.Add(perm);
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    con.Close();
                    dgvPerm.ItemsSource = list;
                }            
            } 
        }

        private void dgvPerm_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvPerm.SelectedItem == null) return;
             perm = dgvPerm.SelectedItem as Permisionario;
            this.Close();
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
