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
using System.Xml;
using System.Xml.Serialization;
using Two_Way_Trasnfer.Clases.CatalogoXML;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para CatalogoCuentas.xaml
    /// </summary>
    public partial class CatalogoCuentas : MetroWindow
    {
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");

        public CatalogoCuentas()
        {
            InitializeComponent();
        }

        private void Generar_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializerNamespaces xmlnsEmpty = new XmlSerializerNamespaces();
            xmlnsEmpty.Add("catalogocuentas", "http://www.sat.gob.mx/esquemas/ContabilidadE/1_3/CatalogoCuentas");
            xmlnsEmpty.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            Catalogo catalogo = new Catalogo();         
            catalogo.Version = "1.3";
            catalogo.RFC = "TWT021108EJ5";
            if (DateTime.Now.Month >= 10)
            {
                catalogo.Mes = DateTime.Now.Month.ToString();
            }
            else
            {
                catalogo.Mes = "0" + DateTime.Now.Month.ToString();
            }
            catalogo.Anio = DateTime.Now.Year;
            List<Ctas> listctas = new List<Ctas>();
            connection.Open();
            SqlCommand sqlComm = new SqlCommand("sp_selectscctas", connection);
            sqlComm.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                Ctas cts = new Ctas();
                string numcta = "";
                if (reader["codigo"].ToString().Contains(".00"))
                {
                    cts.CodAgrup = reader["codigo"].ToString().Replace(".00","");
                }
                else
                {
                    cts.CodAgrup = reader["codigo"].ToString();
                }
                numcta = reader["cuenta"].ToString();
                cts.Desc = reader["espanol"].ToString();
                numcta = numcta+"-"+reader["sub_cta"].ToString();
                numcta = numcta + "-" + reader["sub_sub"].ToString();
                cts.NumCta = numcta;
                cts.Nivel = Convert.ToInt32(reader["nivel"].ToString());
                if (reader["clave_cta"].ToString().Substring(0, 1) == "A")
                {
                    cts.Natur = "D";
                }
                else if (reader["clave_cta"].ToString().Substring(0,1)=="P")
                {
                    cts.Natur = "A";
                }
                 else if (reader["clave_cta"].ToString().Substring(0, 1) == "C")
                {
                    cts.Natur = "A";
                }
                else if (reader["clave_cta"].ToString().Substring(0, 1) == "I")
                {
                    cts.Natur = "A";
                }
                else if (reader["clave_cta"].ToString().Substring(0, 1) == "G")
                {
                    cts.Natur = "D";
                }
                listctas.Add(cts);
            }
            connection.Close();
            catalogo.Ctas = listctas;     
            using (var writer = new System.IO.StreamWriter(@"P:\validaciones\sat\TWT021108EJ5"+catalogo.Anio+catalogo.Mes+"CT.xml"))
            {
                var serializer = new XmlSerializer(catalogo.GetType());
                
                serializer.Serialize(writer,catalogo,xmlnsEmpty);
                writer.Flush();
                reporteCuentasSAT reporte = new reporteCuentasSAT();
                reporte.ShowDialog();
                MessageBox.Show("Proceso completado","Completaod",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }
    }
}
