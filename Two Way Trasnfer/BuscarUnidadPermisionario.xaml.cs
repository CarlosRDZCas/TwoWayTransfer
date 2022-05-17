using MahApps.Metro.Controls;
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
    /// Lógica de interacción para BuscarUnidadPermisionario.xaml
    /// </summary>
    public partial class BuscarUnidadPermisionario : MetroWindow
    {
        string connectionString = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        public UnidadPermisionario unidad{ get; set; }
      
        public BuscarUnidadPermisionario()
        {
            InitializeComponent();
            txtUnidad.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {         
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    UnidadPermisionario unidadPermisionario = new UnidadPermisionario();
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("sp_BuscarUnidadPermisionario", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Unidad", txtUnidad.Text);
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        unidadPermisionario.ID = Convert.ToInt32(lector["ID"].ToString());
                        unidadPermisionario.Unidad = lector["Unidad"].ToString();
                        unidadPermisionario.Linea = lector["Linea"].ToString();
                        unidadPermisionario.NombreAseg = lector["NombreAseg"].ToString();
                        unidadPermisionario.NumPermSCT = lector["NumPermisoSCT"].ToString();
                        unidadPermisionario.ConfigVehicular = lector["ConfigVehicular"].ToString();
                        unidadPermisionario.NumPolizaSeguro = lector["NumPolizaSeguro"].ToString();
                        unidadPermisionario.AnioModeloVM = lector["AnioModeloVM"].ToString();
                        unidadPermisionario.PermSCT = lector["PermSCT"].ToString();
                        unidadPermisionario.PlacaVM = lector["PlacaVM"].ToString();
                    }
                    unidad = unidadPermisionario;
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {                
                    Con.Close();
                    this.Close();                     
                }               
            }
        }

        private void txtUnidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}
