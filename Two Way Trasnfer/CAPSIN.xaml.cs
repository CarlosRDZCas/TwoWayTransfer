using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Globalization;
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
using Two_Way_Trasnfer.Clases.CAPSIN;
using System.IO;
using Aspose.Cells;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para CAPSIN.xaml
    /// </summary>
    public partial class CAPSIN : MetroWindow
    {
        string connectionString = "Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408";
        public CAPSIN()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "XLSX files (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == true)
                txtArchivo.Text = openFileDialog.FileName;
        }

        private void btnProcesar_Click_1(object sender, RoutedEventArgs e)
        {
            List<CapsinModel> list = new List<CapsinModel>();
            string filename = txtArchivo.Text;
            try
            {
                if (filename != "")
                {
                    FileStream fstream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    Workbook workbook = new Workbook(fstream);
                    Worksheet worksheet = workbook.Worksheets[0];
                    DataTable dataTable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.Rows.Count, 25, true);
                    fstream.Close();
                    foreach (DataRow item in dataTable.Rows)
                    {
                        CapsinModel caps = new CapsinModel();
                        DateTime clearindate;
                        if (item["Clearing Date"].ToString() != "")
                        {
                            clearindate = DateTime.Parse(item["Clearing Date"].ToString().Trim(), CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            clearindate = DateTime.Parse("01/01/1990", CultureInfo.InvariantCulture);
                        }
                        caps.ClearingDate = clearindate;
                        caps.VendorInvoiceNumber = item["Vendor Invoice Number"].ToString();
                        DateTime invoicedate;
                        if (item["Invoice Date"].ToString() != "")
                        {
                            invoicedate = DateTime.Parse(item["Invoice Date"].ToString().Trim(), CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            invoicedate = DateTime.Parse("01/01/1990", CultureInfo.InvariantCulture);
                        }
                        caps.InvoiceDate = invoicedate;
                        caps.InvoiceStatus = item["Invoice Status"].ToString();
                        DateTime invoicedue;
                        if (item["Invoice Due Date"].ToString() != "")
                        {
                            invoicedue = DateTime.Parse(item["Invoice Due Date"].ToString().Trim(), CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            invoicedue = DateTime.Parse("01/01/1990", CultureInfo.InvariantCulture);
                        }
                        caps.InvoiceDueDate = invoicedue;
                        caps.Amount = double.Parse(item["Amount"].ToString());
                        list.Add(caps);
                    }
                    foreach (CapsinModel item in list)
                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("sp_insertCAPSIN", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ClearingDate", item.ClearingDate);
                            cmd.Parameters.AddWithValue("@VendorInvoiceNumber", item.VendorInvoiceNumber);
                            cmd.Parameters.AddWithValue("@InvoiceDate", item.InvoiceDate);
                            cmd.Parameters.AddWithValue("@InvoiceStatus", item.InvoiceStatus);
                            cmd.Parameters.AddWithValue("@InvoiceDueDate", item.InvoiceDueDate);
                            cmd.Parameters.AddWithValue("@Amount", item.Amount);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Archivo Prcesado");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Seleccione un archivo primero");
                }
            }
            catch (Exception er)
            {

                System.Windows.Forms.MessageBox.Show(er.ToString());
            }
          
        }
    }
}




