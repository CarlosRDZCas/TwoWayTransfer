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
    /// Lógica de interacción para BuscarRD.xaml
    /// </summary>
    public partial class BuscarRD : MetroWindow
    {
        public RemitenteDestinatario rds { get; set; }
        SqlConnection Con = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        public BuscarRD()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            RemitenteDestinatario rd = new RemitenteDestinatario();
            Con.Open();
            SqlCommand cmd = new SqlCommand("BuscarRD", Con);
            cmd.CommandType = CommandType.StoredProcedure;     
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);           
            SqlDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                rd.ID= Convert.ToInt32(lector["ID"].ToString());
                rd.Nombre= lector["Nombre"].ToString();   
                rd.ResidenciaFiscal = lector["ResidenciaFiscal"].ToString();
                rd.RFC= lector["RFC"].ToString();
                rd.NumRegIdTrip= lector["NumRegIdTrip"].ToString();
                rd.Calle= lector["Calle"].ToString();
                rd.NumExt = lector["NumeroExterior"].ToString();
                rd.NumInt= lector["NumeroInterior"].ToString();
                rd.Colonia= lector["Colonia"].ToString();
                rd.Localidad= lector["Localidad"].ToString();
                rd.Referencia= lector["Referencia"].ToString();
                rd.Municipio= lector["Municipio"].ToString();
                rd.Estado= lector["Estado"].ToString();
                rd.Pais= lector["Pais"].ToString();
                rd.CodigoPostal= lector["CodigoPostal"].ToString();
                rd.NumMunicipio = lector["NumMunicipio"].ToString();
                rd.NumLocalidad = lector["NumLocalidad"].ToString();
                rd.NumColonia = lector["NumColonia"].ToString();
            }
            rds = rd;
            Con.Close();
            Close();
        }

      
    }
}
