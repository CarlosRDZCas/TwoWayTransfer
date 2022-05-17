using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para LiquidacionXPO.xaml
    /// </summary>
    public partial class LiquidacionXPO : MetroWindow
    {
        public bool Valor { get; set; }
        public int Liquidacion { get; set; } = 0;
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public LiquidacionXPO()
        {
            InitializeComponent();
            txtLiquidacion.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtLiquidacion.Text== string.Empty || txtLiquidacion.Text == "0")
            {
                Valor = false;
                System.Windows.Forms.MessageBox.Show("Es necesario agregar la liquidacion");
            }
            else
            {
                Valor = true;
                Liquidacion = Convert.ToInt32(txtLiquidacion.Text);
                this.Close();
            }
        
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Valor = false;
            Liquidacion = 0;
            this.Close();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
       
        }

        private void txtLiquidacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOk_Click(sender, e);
            }
        }
    }
}
