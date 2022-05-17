using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
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
using Two_Way_Trasnfer.Clases.FormatoCostos;

namespace Two_Way_Trasnfer
{

    public partial class FormatoCostos : MetroWindow
    {
        string connectionstring2 = "Data Source=SOPORTE\\SQLEXPRESS; Database=FOXPRO; Initial Catalog=FOXPRO ;User ID=sa; Password = Twoway2408";

        public FormatoCostos()
        {
            InitializeComponent();
        }

        private void btnProcesar_Click(object sender, RoutedEventArgs e)
        {
            List<string> arregloremisiones = new List<string>();
            arregloremisiones = txtRemisiones.Text.Trim().Split('\n').ToList();
            foreach (string item in arregloremisiones)
            {
                string remision = item.Trim().Replace("\r", "");
                string lugar;
                string notalon;

                lugar = remision.Substring(0, 3);
                notalon = remision.Substring(3);

                GatalonFormato formato = new GatalonFormato();

                using (SqlConnection con = new SqlConnection(connectionstring2))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_selectGatalonFormatoCostos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Lugar", lugar);
                    cmd.Parameters.AddWithValue("@Talon", int.Parse(notalon));
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        formato.Delivery = reader["Concepto3"].ToString();
                        formato.NumeroCarro = int.Parse(reader["no_carro"].ToString());
                        formato.Kilometros = double.Parse(reader["Kilometros"].ToString());
                        formato.Referencia = reader["Material"].ToString();
                        formato.Moneda = reader["Moneda"].ToString();
                        formato.Flete = double.Parse(reader["Flete"].ToString());

                    }
                }
                if (formato.Delivery != "")
                {
                    object oMissing = System.Reflection.Missing.Value;
                    Excel.Application excelApp = null;
                    Excel.Workbook workbook = null;
                    Excel.Worksheet worksheet = null;

                    try
                    {
                        double valor;
                        if (formato.Delivery.Trim().Length>9 && double.TryParse(formato.Delivery.Trim().Substring(0, 10), out valor))
                        {
                            excelApp = new Excel.Application();
                            excelApp.DisplayAlerts = false;
                            excelApp.Visible = false;
                            workbook = excelApp.Workbooks.Open(@"P:\Apps\TwoWayTransfer\FORMATO COSTOS.xlsx", 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
                            worksheet = excelApp.ActiveSheet as Excel.Worksheet;

                            worksheet.Cells[2, 1].Value = valor;
                            if (formato.NumeroCarro == 1000 || formato.NumeroCarro == 1001)
                            {
                                worksheet.Cells[2, 2].Value = "THORTON";

                            }
                            else
                            {
                                worksheet.Cells[2, 2].Value = "TRAILER 53";
                            }

                            worksheet.Cells[2, 3].Value = formato.Kilometros;
                            worksheet.Cells[2, 4].Value = formato.Referencia.Trim().Substring(0, 11);
                            worksheet.Cells[2, 5].Value = formato.Moneda.Trim() == "PESOS" ? "MXN" : "";
                            worksheet.Cells[2, 6].Value = formato.Flete;
                            workbook.SaveAs(@"T:\COBRANZA\DUPONT\FORMATO COSTOS " + valor + ".xlsx");
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show($"Verifique que la remision {remision} tenga el delivery en la columna 3");
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("La remision " + lugar + "" + notalon + " no tiene el delivery en la columna 3 ");
                }

            }
        }
    }
}
