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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        public Login()
        {
            InitializeComponent();
            connection.Open();
            connection.Close();
            txtUsuario.Focus();
        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Login", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
            cmd.Parameters.AddWithValue("@Pass", txtPass.Password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                if (reader.Read())
                {
                    if (reader["ID"].ToString() == "1")
                    {
                        //if (reader["clave"].ToString() == "1" | reader["clave"].ToString() == "226")
                        //{
                            Main main = new Main(txtUsuario.Text);
                            this.Hide();
                            main.Show();
                            this.Close();
                            txtUsuario.Text = "";
                            txtPass.Password = "";
                            connection.Close();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Usuario no autorizado.");
                        //    connection.Close();
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Usuario y o contraseña incorrectos");
                        connection.Close();
                    }

                }
            }
            else
            {
                MessageBox.Show("Usuario y o contraseña incorrectos");
                connection.Close();
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnEntrar_Click(sender, e);
            }
        }
    }
}
