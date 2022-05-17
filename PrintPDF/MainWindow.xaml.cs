using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using MahApps.Metro.Controls;
using System.Windows.Forms;
using System.Diagnostics;
using Spire.Pdf;

namespace PrintPDF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        PrintDocument pd = new PrintDocument();        
        PrinterSettings pntr = new PrinterSettings();
        public MainWindow()
        {
            InitializeComponent();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                cbImpresora.Items.Add(printer);
            }
            PrintQueue printerDef = LocalPrintServer.GetDefaultPrintQueue();
             cbImpresora.SelectedValue=printerDef.Name;
            rb1.IsChecked = true;
            dgvArchivos.CanUserAddRows = false;         
            dgvArchivos.CanUserDeleteRows = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;            
            CommonFileDialogResult result = dialog.ShowDialog();
            try
            {
                txtDirectorio.Text = dialog.FileName;
                List<Archivo> list = new List<Archivo>();
                foreach (var archivo in Directory.GetFiles(dialog.FileName, "*.pdf"))
                {
                    Archivo arch = new Archivo();
                    arch.Nombre = archivo;
                    list.Add(arch);
                }
                dgvArchivos.ItemsSource = list;
            }
            catch (Exception)
            {
                
            }
           
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
            if (dgvArchivos.Items.Count>0)
            {
                if (rb1.IsChecked == true)
                {
                    a = dgvArchivos.Items.Count;
                    if (System.Windows.MessageBox.Show("Seguro que quiere imprimir una copia de " + a + " archivo(s)?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        foreach (Archivo archivo in dgvArchivos.Items)
                        {
                            PdfDocument doc = new PdfDocument();
                            try
                            {
                                doc.LoadFromFile(archivo.Nombre);
                                doc.PrintSettings.Copies = 1;
                                string sub = archivo.Nombre.Substring(archivo.Nombre.LastIndexOf(("\\")) + 1);
                                doc.PrintSettings.DocumentName = sub;
                                doc.DocumentInformation.Title = sub;
                                doc.PrintDocument.DocumentName = sub;
                                doc.PrintDocument.Print();
                            }
                            catch (Exception)
                            {

                                System.Windows.Forms.MessageBox.Show("No se puede imprimir el archivo " + archivo.Nombre + ", verifique que el archivo exista o se haya modificado el nombre", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }
                          
                        }
                    }

                }
                else if (rb2.IsChecked == true)
                {
                    a = dgvArchivos.Items.Count;
                    if (System.Windows.MessageBox.Show("Seguro que quiere imprimir dos copias de " + a + " archivo(s)?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        foreach (Archivo archivo in dgvArchivos.Items)
                        {
                            try
                            {
                                PdfDocument doc = new PdfDocument();
                                doc.LoadFromFile(archivo.Nombre);
                                string sub = archivo.Nombre.Substring(archivo.Nombre.LastIndexOf(("\\")) + 1);
                                doc.PrintSettings.DocumentName = sub;
                                doc.DocumentInformation.Title = sub;
                                doc.PrintDocument.DocumentName = sub;
                                doc.PrintDocument.PrinterSettings.Copies = 2;
                                doc.PrintDocument.Print();
                            }
                            catch (Exception)
                            {

                                System.Windows.Forms.MessageBox.Show("No se puede imprimir el archivo " + archivo.Nombre + ", verifique que el archivo exista o se haya modificado el nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No hay archivos para imprimir","Error",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtDirectorio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtDirectorio.Text != "")
            {
                btnImprimir.IsEnabled = true;
            }
        }
    }
}
