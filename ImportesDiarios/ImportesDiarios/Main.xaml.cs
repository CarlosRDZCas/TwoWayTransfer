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
using ClosedXML.Excel;
using MahApps.Metro.Controls;
using Microsoft.Win32;

namespace ImportesDiarios
{
    /// <summary>
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        SqlConnection connection2 = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal EnergexNLD { get; set; }
        public double EnergexDerr { get; set; }
        public double Orsan { get; set; }
        public double FCA { get; set; }
        public double CRE { get; set; }
        public double PemexNLD { get; set; }
        public double PemexDerr { get; set; }
        public double TipoCambio { get; set; }
        public double EstimuloIEPS { get; set; }
        public string Factura { get; set; }
        public double Litros { get; set; }
        public double Importe { get; set; }

        public Main()
        {
            InitializeComponent();
        
            this.DataContext = this;
        }

        public Main(string usuario)
        {
            InitializeComponent();
         
            Usuario = usuario;      
            dtmFecha.SelectedDate = DateTime.Now;        
            connection.Open();
            SqlCommand cmd = new SqlCommand("DatosImportesDiarios", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fecha", dtmFecha.SelectedDate);
            SqlDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                Fecha = DateTime.Parse(lector["Fecha"].ToString());
                EnergexNLD = Decimal.Parse(lector["EnergexNLD"].ToString());
                EnergexDerr =Convert.ToDouble( lector["EnergexDerr"].ToString());
                Orsan= Convert.ToDouble(lector["Orsan"].ToString());
                FCA = Convert.ToDouble( lector["FCA"].ToString());
                CRE= Convert.ToDouble(lector["CRE"].ToString());
                PemexNLD = Convert.ToDouble( lector["PemexNLD"].ToString());
                PemexDerr= Convert.ToDouble(lector["PemexDerr"].ToString());
                TipoCambio = Convert.ToDouble( lector["TipoCambio"].ToString());
                EstimuloIEPS = Convert.ToDouble( lector["EstimuloIEPS"].ToString());
                Factura = lector["Factura"].ToString();
                Litros = Convert.ToDouble(lector["Litros"].ToString());
                Importe = Convert.ToDouble(lector["Importe"].ToString());

            }
            connection.Close();
            this.DataContext = this;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtmFecha.SelectedDate <= DateTime.Now)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertarImportesDiarios", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fecha", dtmFecha.SelectedDate);
                    cmd.Parameters.AddWithValue("@EnergexNLD", EnergexNLD);
                    cmd.Parameters.AddWithValue("@EnergexDerr", EnergexDerr);
                    cmd.Parameters.AddWithValue("@Orsan", Orsan);
                    cmd.Parameters.AddWithValue("@FCA", FCA);
                    cmd.Parameters.AddWithValue("@CRE", CRE);
                    cmd.Parameters.AddWithValue("@PemexNLD", PemexNLD);
                    cmd.Parameters.AddWithValue("@PemexDerr", PemexDerr);
                    cmd.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                    cmd.Parameters.AddWithValue("@EstimuloIEPS",EstimuloIEPS);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Factura", Factura);
                    cmd.Parameters.AddWithValue("@Litros", Litros);
                    cmd.Parameters.AddWithValue("@Importe", Importe);
                    if (System.Windows.MessageBox.Show("Seguro que quiere actualizar los importes?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {

                        cmd.ExecuteNonQuery();


                        MessageBox.Show("Importes actualizados.", "Actualizacion Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("La fecha seleccionada es mayor a la fecha actual", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
              
            }
            catch (Exception)
            {
                connection.Close();
               MessageBox.Show("Verifique los datos ingresados", "Error al actualizar los importes.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dtmFecha_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void dtmFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            connection2.Open();
          
            SqlCommand cmd = new SqlCommand("ImportesDiariosPasados", connection2);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fecha", dtmFecha.SelectedDate);
            SqlDataReader lector = cmd.ExecuteReader();
            if (lector.HasRows )
            {
                while (lector.Read())
                {                    
                    EnergexNLD = Decimal.Parse(lector["EnergexNLD"].ToString());
                    txtEnergexNLD.Text = EnergexNLD.ToString();
                   EnergexDerr = Convert.ToDouble(lector["EnergexDerr"].ToString());
                    txtEnergexDerr.Text = EnergexDerr.ToString();
                   Orsan = Convert.ToDouble(lector["Orsan"].ToString());
                    txtOrsan.Text = Orsan.ToString();
                    FCA = Convert.ToDouble(lector["FCA"].ToString());
                    txtFCA.Text = FCA.ToString();
                    CRE = Convert.ToDouble(lector["CRE"].ToString());
                    txtCRE.Text = CRE.ToString();
                    PemexNLD = Convert.ToDouble(lector["PemexNLD"].ToString());
                    txtPemexNLD.Text = PemexNLD.ToString();
                    PemexDerr = Convert.ToDouble(lector["PemexDerr"].ToString());
                    txtPemexDerr.Text = PemexDerr.ToString();
                    TipoCambio = Convert.ToDouble(lector["TipoCambio"].ToString());
                    txtTipoCambio.Text = TipoCambio.ToString();
                    EstimuloIEPS = Convert.ToDouble(lector["EstimuloIEPS"].ToString());
                    txtEstimulo.Text = EstimuloIEPS.ToString();
                    Factura = lector["Factura"].ToString();
                    txtFactura.Text = Factura.ToString();
                    Litros = Convert.ToDouble(lector["Litros"].ToString());
                    txtLitros.Text = Litros.ToString();
                    Importe = Convert.ToDouble(lector["Importe"].ToString());
                    txtImporte.Text = Importe.ToString();
                }
                this.DataContext = this;
            }
            else
            {
               EnergexNLD = 0;
                txtEnergexNLD.Text = "0.00";
               EnergexDerr = 0.0;
                txtEnergexDerr.Text = "0.00";
                Orsan = 0.0;
                txtOrsan.Text = "0.00";
                FCA = 0.0;
                txtFCA.Text = "0.00";
                CRE= 0.0;
                txtCRE.Text = "0.00";
                PemexNLD= 0.0;
                txtPemexNLD.Text = "0.00";
                PemexDerr=0.0;
                txtPemexDerr.Text = "0.00";
                TipoCambio = 0.0;
                txtTipoCambio.Text = "0.00";
                EstimuloIEPS = 0.0;
                txtEstimulo.Text = "0.00";
                Factura ="";
                txtFactura.Text = "";
                Litros = 0.0;
                txtLitros.Text = "0.00";
                Importe = 0.0;
                txtImporte.Text = "0.00";


                this.DataContext = this;
            }
            connection2.Close();
      
        }

        private void txtEnergexNLD_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtEnergexNLD_TextChanged(object sender, TextChangedEventArgs e)
        {           
           
        }
        private void EnergexNLD_texbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtEnergexNLD_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtEnergexDerr_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtOrsan_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtFCA_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtCRE_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtPemexNLD_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtPemexDerr_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtTipoCambio_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtEstimulo_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtImporte_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();

        }

        private void txtLitros_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void txtFactura_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel WorkBook|*.xlsx";
            bool? dr = sfd.ShowDialog();
            switch (dr)
            {
                case true:
                    try
                    {
                        DataSet1 importes = GetData();
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(importes);
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Archivo exportado con exito");
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
  

                    }
                    catch (Exception ex)
                    {

                       MessageBox.Show("Error al guardar el archivo\n"+ex);
                    }
                    break;
                case false:
                    
                    break;
                default:
                    break;
            }
        }

        private DataSet1 GetData()
        {

            SqlConnection Con = new SqlConnection("Data Source=192.168.1.240; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
            SqlCommand cmd = new SqlCommand("Importes");
            using (Con)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand = cmd;
                    using (DataSet1 importes = new DataSet1())
                    {
                        sda.Fill(importes, "Importes");
                        return importes;
                    }
                }
            }
        }

        private void btnGrid_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = new Grid(Usuario);
            grid.Show();
            this.Close();

        }
    }


    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo culture)
        {
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");

            if (value == null)
            {
                return new ValidationResult(false, "Ingrese un valor.");

            }
            else if (value.ToString()==".")
            {
                return new ValidationResult(false, "Valor Invalido.");
            }
            else if (!regex.IsMatch(value.ToString()))
            {
                return new ValidationResult(false, "Ingrese solo numeros");
            }
            else if (Convert.ToDouble(value.ToString()) > 99)
            {
                return new ValidationResult(false, "Valor Invalido.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
