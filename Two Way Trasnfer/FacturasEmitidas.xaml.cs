using CsvHelper;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
using Two_Way_Trasnfer.Clases.FacturasEmitidas;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para FacturasEmitidas.xaml
    /// </summary>
    public partial class FacturasEmitidas : MetroWindow
    {
        public string Usuario { get; set; }
        string connectionString = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        public FacturasEmitidas(string usuario)
        {
            InitializeComponent();
            Usuario = usuario;
        }

        private void btnProcesar_Click(object sender, RoutedEventArgs e)
        {
            int i = 0,contador=0;
            try
            {
                using (var reader = new StreamReader(txtCSV.Text))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<FacturaEmitida>();
                        List<FacturaEmitida> facturas = records.ToList();
                        foreach (var item in facturas)
                        {
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand("sp_insertFacturasEmitidas", con);
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UUID", item.UUID);
                                cmd.Parameters.AddWithValue("@Version", item.VersionCFDI);
                                cmd.Parameters.AddWithValue("@Estatus", item.Estatus);
                                cmd.Parameters.AddWithValue("@EsCancelable", item.EsCancelable);
                                cmd.Parameters.AddWithValue("@TipoDeComprobante", item.TipoComprobante);
                                cmd.Parameters.AddWithValue("@FechaEmision", item.FechaEmision);
                                cmd.Parameters.AddWithValue("@FechaTimbrado", item.FechaTimbrado);
                                cmd.Parameters.AddWithValue("@Serie", item.Serie);
                                cmd.Parameters.AddWithValue("@Folio", item.Folio);
                                cmd.Parameters.AddWithValue("@RFCReceptor", item.RFCReceptor);
                                cmd.Parameters.AddWithValue("@NombreReceptor", item.NombreReceptor);
                                cmd.Parameters.AddWithValue("@TipoCambio", item.TipoCambio);
                                cmd.Parameters.AddWithValue("@Moneda", item.Moneda);
                                cmd.Parameters.AddWithValue("@Subtotal", item.SubTotal);
                                cmd.Parameters.AddWithValue("@IVaTrasladado16", item.IVATrasladado16);
                                cmd.Parameters.AddWithValue("@IVARetenido4", item.IVARetenidoGlobal);
                                cmd.Parameters.AddWithValue("@Total", item.Total);
                                cmd.Parameters.AddWithValue("@UsuarioUPload", Usuario);
                                 i = cmd.ExecuteNonQuery();
                                if (i==1)
                                {
                                    contador++;
                                }
                                con.Close();
                            }
                        }
                        System.Windows.Forms.MessageBox.Show("Prcesado con exito!\n\nSe insertaron "+contador+" factura(s)","Procesado",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
      

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
                txtCSV.Text = openFileDialog.FileName;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
