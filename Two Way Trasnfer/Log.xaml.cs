using MahApps.Metro.Controls;
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

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para Log.xaml
    /// </summary>
    public partial class Log : MetroWindow
    {
        public bool Valor { get; set; }
        public int log { get; set; } = 0;
        public string Ruta { get; set; } = "";
        public int Kilometros { get; set; } = 0;
        public string Carro { get; set; }
        public string Caja { get; set; } = "";
        public string Cliente { get; set; } = "";

        string connectionstring2 = "Data Source=SOPORTE\\SQLEXPRESS; Database=FOXPRO; Initial Catalog=FOXPRO ;User ID=sa; Password = Twoway2408";
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public Log()
        {
            InitializeComponent();
            txtLog.Focus();
        }

        private void txtLog_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOk_Click(sender, e);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtLog.Text == string.Empty || txtLog.Text == "0")
                {
                    Valor = false;
                    System.Windows.Forms.MessageBox.Show("Es necesario agregar el log");
                }
                else
                {
                    log = Convert.ToInt32(txtLog.Text);
                    using (SqlConnection con = new SqlConnection(connectionstring2))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_ActualizarLOGT", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LOG", log);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Caja = reader["Caja"].ToString();
                                Cliente = reader["Cliente"].ToString();
                                Ruta = reader["Ruta"].ToString();
                                Carro = reader["Carro"].ToString();
                                Kilometros = Convert.ToInt32(Convert.ToDouble(reader["Kilometros"].ToString()));
                                Valor = true;
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("El Log no existe");
                            Valor = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problema al conectar con Visual Fox Pro");
            }           
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Valor = false;
            log = 0;
            Ruta = "";
            Kilometros = 0;
            Caja = "";
            Carro = "";
            Cliente = "";
            this.Close();
        }
        private void MetroWindow_Closed(object sender, EventArgs e)
        {
  
        }
    }
}
