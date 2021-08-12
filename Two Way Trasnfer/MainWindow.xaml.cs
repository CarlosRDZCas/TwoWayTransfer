using System;
using System.Collections.Generic;
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
            importes.ShowDialog();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void PrintPDF_Click(object sender, RoutedEventArgs e)
        {
            PrintPDF.MainWindow frmPrintPDF = new PrintPDF.MainWindow();
            frmPrintPDF.ShowDialog();
        }

        private void Intranet_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.twt.com.mx/intranet/menu_clientes.asp");
        }

        private void ContraRecibos_Click(object sender, RoutedEventArgs e)
        {
            ContraRecibos cr = new ContraRecibos();
                cr.ShowDialog();
        }

        private void RemitentesDestinatarios_Click(object sender, RoutedEventArgs e)
        {
            RemitentesDestinatarios RD = new RemitentesDestinatarios();
            RD.ShowDialog();
        }
    }
}
