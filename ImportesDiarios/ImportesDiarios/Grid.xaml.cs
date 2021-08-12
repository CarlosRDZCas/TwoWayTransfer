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

namespace ImportesDiarios
{
    /// <summary>
    /// Lógica de interacción para Grid.xaml
    /// </summary>
    public partial class Grid : MetroWindow
    {
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        public string Usuario { get; set; }
        public Grid(string usuario)
        {
            InitializeComponent();
            Usuario = usuario;
            FillDGV();
        }
        public void FillDGV()
        {
            List<Importes> list = new List<Importes>();
            Importes importe;
            connection.Open();
            SqlCommand cmd = new SqlCommand("Importes", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dtRecord = new DataTable();
            SqlDataReader lector2 = cmd.ExecuteReader();
            while (lector2.Read())
            {
                importe = new Importes();
                importe.Factura = lector2["Factura"].ToString();
                importe.CRE = Convert.ToDouble(lector2["CRE"].ToString());
                importe.EnergexDerr = Convert.ToDouble(lector2["EnergexDerr"].ToString());
                importe.EnergexNLD = Convert.ToDouble(lector2["EnergexNLD"].ToString());
                importe.EnergexNLDConsumo= Convert.ToDouble(lector2["EnergexNLDConsumo"].ToString());
                importe.EnergexOrsanConsumo= Convert.ToDouble(lector2["EnergexOrsanConsumo"].ToString());
                importe.EstimuloIEPS = Convert.ToDouble(lector2["EstimuloIEPS"].ToString());
                importe.FCA= Convert.ToDouble(lector2["FCA"].ToString());
                importe.Fecha = Convert.ToDateTime(lector2["Fecha"].ToString());
                importe.Importe = Convert.ToDouble(lector2["Importe"].ToString());
                importe.Litros = Convert.ToDouble(lector2["Litros"].ToString());
                importe.Orsan = Convert.ToDouble(lector2["Orsan"].ToString());
                importe.OrsanConsumo = Convert.ToDouble(lector2["OrsanConsumo"].ToString());
                importe.PemexDerr= Convert.ToDouble(lector2["PemexDerr"].ToString());
                importe.PemexNLD = Convert.ToDouble(lector2["PemexNLD"].ToString());
                importe.TipoCambio = Convert.ToDouble(lector2["TipoCambio"].ToString());
                list.Add(importe);
            }
            dgvImportes.ItemsSource = list;
            
            connection.Close();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool valor = true;
            List<Importes> lista;
            lista= (List<Importes>)dgvImportes.ItemsSource;
            foreach (Importes item in lista)
            {
                try
                {                    
                    if (item.Fecha <= DateTime.Now)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("InsertarImportesDiarios", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Fecha", item.Fecha);
                        cmd.Parameters.AddWithValue("@EnergexNLD", item.EnergexNLD);
                        cmd.Parameters.AddWithValue("@EnergexDerr", item.EnergexDerr);
                        cmd.Parameters.AddWithValue("@Orsan", item.Orsan);
                        cmd.Parameters.AddWithValue("@FCA", item.FCA);
                        cmd.Parameters.AddWithValue("@CRE", item.CRE);
                        cmd.Parameters.AddWithValue("@PemexNLD", item.PemexNLD);
                        cmd.Parameters.AddWithValue("@PemexDerr", item.PemexDerr);
                        cmd.Parameters.AddWithValue("@TipoCambio", item.TipoCambio);
                        cmd.Parameters.AddWithValue("@EstimuloIEPS", item.EstimuloIEPS);
                        cmd.Parameters.AddWithValue("@Usuario", Usuario);
                        cmd.Parameters.AddWithValue("@Factura", item.Factura);
                        cmd.Parameters.AddWithValue("@Litros", item.Litros);
                        cmd.Parameters.AddWithValue("@Importe", item.Importe);
                        cmd.Parameters.AddWithValue("@EnergexNLDConsumo", item.EnergexNLDConsumo);
                        cmd.Parameters.AddWithValue("@EnergexOrsanConsumo", item.EnergexOrsanConsumo);
                        cmd.Parameters.AddWithValue("@OrsanConsumo", item.OrsanConsumo);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        valor = true;
                    }
                    else
                    {
                        MessageBox.Show("La fecha "+item.Fecha +" es mayor a la fecha actual", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        valor = false;
                    }

                }                
                catch (Exception)
                {
                    connection.Close();
                    MessageBox.Show("Verifique los datos ingresados", "Error al actualizar los importes.", MessageBoxButton.OK, MessageBoxImage.Error);
                    valor = false;
                }
            }
            if (valor)
            {
                MessageBox.Show("Datos guardados correctamente.");
            }
            FillDGV();
        }

        private void dgvImportes_AutoGeneratedColumns(object sender, EventArgs e)
        {

        }

        private void dgvImportes_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
        }
    }
    }

