using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int i = 0;

            i = rnd.Next(1, 3);
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Resources\\" + i + ".jpg", UriKind.Relative);
            bi3.EndInit();
            image.Source = bi3;
            myBrush.ImageSource = image.Source;
            this.Background = myBrush;

        }
        public MainWindow()
        {
            InitializeComponent();
        }
        public string Usuario { get; set; }

        public MainWindow(string usuario)
        {
            InitializeComponent();
            /*FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "P:\\Apps\\TEST\\log";
            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;
            watcher.Filter = "*.txt";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            


            void OnChanged(object sender, FileSystemEventArgs e)
            {

                if (e.ChangeType == WatcherChangeTypes.Changed)
                {
                    string[] lines = System.IO.File.ReadAllLines("P:\\Apps\\TEST\\log\\LogCambios.txt");

                    System.Windows.Forms.MessageBox.Show(lines[lines.Length-1]+".\nFavor de cerrar la aplicacion y volver a abrirla para su correcto funcionamiento.\n\nAl presionar OK se cerrara la aplicacion.", "Cambio en el sistema", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

            }*/
            Usuario = usuario;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Resources\\1.jpg", UriKind.Relative);
            bi3.EndInit();
            image.Source = bi3;
            myBrush.ImageSource = image.Source;
            this.Background = myBrush;
            dispatcherTimer.Start();
        }

        private void Importes_Click(object sender, RoutedEventArgs e)
        {
            ImportesDiarios.Main importes = new ImportesDiarios.Main();
            importes.Show();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void PrintPDF_Click(object sender, RoutedEventArgs e)
        {
            PrintPDF.MainWindow frmPrintPDF = new PrintPDF.MainWindow();
            frmPrintPDF.Show();
        }

        private void Intranet_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.twt.com.mx/intranet/menu_clientes.asp");
        }

        private void ContraRecibos_Click(object sender, RoutedEventArgs e)
        {
            ContraRecibos cr = new ContraRecibos();
            cr.Show();
        }

        private void RemitentesDestinatarios_Click(object sender, RoutedEventArgs e)
        {
            RemitentesDestinatarios RD = new RemitentesDestinatarios();
            RD.Show();
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            Clientes cl = new Clientes(Usuario);
            cl.Show();
        }

        private void CatalogoCuentas_Click(object sender, RoutedEventArgs e)
        {
            CatalogoCuentas cc = new CatalogoCuentas();
            cc.Show();
        }

        private void Permisionarios_Click(object sender, RoutedEventArgs e)
        {
            Permisionarios perm = new Permisionarios();
            perm.Show();
        }

        private void Unidades_Permisionarios_Click(object sender, RoutedEventArgs e)
        {
            UnidadesPermisionarios up = new UnidadesPermisionarios();
            up.Show();
        }

        private void Celulares_Click(object sender, RoutedEventArgs e)
        {
            Celulares cel = new Celulares();
            cel.Show();
        }

        private void XPO_Click(object sender, RoutedEventArgs e)
        {
            XPO xpo = new XPO(Usuario);
            xpo.Show();
        }

        private void FacturasEmitidas_Click(object sender, RoutedEventArgs e)
        {
            FacturasEmitidas facturas = new FacturasEmitidas(Usuario);
            facturas.Show();
        }

        private void CAPSIN_Click(object sender, RoutedEventArgs e)
        {
            CAPSIN cap = new CAPSIN();
            cap.Show();
        }

        private void FormatoCostos_Click(object sender, RoutedEventArgs e)
        {
            FormatoCostos formato = new FormatoCostos();
            formato.Show();
        }

        private void FacturasProveedores_Click(object sender, RoutedEventArgs e)
        {
            FacturasProveedores fp = new FacturasProveedores(Usuario);
            fp.Show();
        }
    }
}
