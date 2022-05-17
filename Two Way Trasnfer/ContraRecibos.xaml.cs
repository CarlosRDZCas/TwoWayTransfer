using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using System.Xml.Serialization;
using MahApps.Metro.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using Two_Way_Trasnfer.Clases;


namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para ContraRecibos.xaml
    /// </summary>
    public partial class ContraRecibos : MetroWindow
    {
        //string conexion sql y declaracion de propiedades publicas 
        SqlConnection connection = new SqlConnection("Data Source=TWL; Database=TWT; Initial Catalog=TWT ;User ID=sa; Password = Tw*way2408");
        List<Proveedor> Lista = new List<Proveedor>();
        List<Comprobante> Facturas = new List<Comprobante>();
        public string rfc { get; set; }
        public string nombre { get; set; }
        public string path { get; set; }

        //Constructor con inicializacion 
        public ContraRecibos()
        {
            InitializeComponent();
            txtContra.Focus();
            dtmFecha.SelectedDate = DateTime.Now;
            dgvFacturas.CanUserAddRows = false;
            dgvFacturas.CanUserDeleteRows = false;
            List<Comprobante> comp = new List<Comprobante>();
            dgvFacturas.ItemsSource = comp;
            connection.Open();               
                SqlCommand sqlComm = new SqlCommand("NumProv", connection);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = sqlComm.ExecuteReader();             
                while (reader.Read())
                {
                    Proveedor prov = new Proveedor();
                    prov.Numero = Convert.ToInt32(reader["numero"].ToString());
                    prov.Nombre = reader["nombre"].ToString();
                    prov.RFC = reader["rfc"].ToString();
                    Lista.Add(prov);
                }
            connection.Close();
            foreach (Proveedor proveedor in Lista)
            {
                cbNumProv.Items.Add(proveedor.Numero);
            }
            cbNumProv.SelectedItem = Lista[0].Numero;
            txtProveedor.Text = Lista[0].Nombre + "( " + Lista[0].RFC + " )";
            rfc = Lista[0].RFC;
            nombre = Lista[0].Nombre;
            connection.Open();
            SqlCommand cmd = new SqlCommand("ContraReciboFolio", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader1= cmd.ExecuteReader();
            while (reader1.Read())
            {
                int folio = Convert.ToInt32(reader1["folio"].ToString()) + 1;
                txtContra.Text = folio.ToString();
            }
            connection.Close();
        }

        //boton consultar para buscar la carpeta con todos los xmls 
        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            btnConsultar.IsEnabled = false;
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.InitialDirectory = @"P:\validaciones\xmlrecibidas";
            CommonFileDialogResult result = dialog.ShowDialog();
            try
            {
                path = dialog.FileName;
            }
            catch (Exception)
            {
                txtTotal.Text = "0";
                txtTotFact.Text = "0";
                Facturas = new List<Comprobante>();
            }     
            List<Archivo> list = new List<Archivo>();
            try
            {               
          
                foreach (var archivo in Directory.GetFiles(dialog.FileName, "*.xml"))
                {
                    Archivo arch = new Archivo();
                    arch.Nombre = archivo;
                    list.Add(arch);
                }    
             
            }
            catch (Exception)
            {
                txtTotal.Text = "0";
                txtTotFact.Text = "0";
            }
            BusyIndicator.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ProcesarXML(list);
            worker.RunWorkerCompleted += (s, ev) => BusyIndicator.IsBusy = false;
            worker.RunWorkerAsync();
            btnConsultar.IsEnabled = true;
           
              
            
        }

        // Metodo para procesar todos los xmls y devolver una lista de xmls 
        public void ProcesarXML(List<Archivo> list)
        {
            Facturas = new List<Comprobante>();
            List<Comprobante> Lista = new List<Comprobante>();
            foreach (var arch in list)
            {
                Comprobante Factura = new Comprobante();
                Clases.CartaPorte.Comprobante Factura2 = new Clases.CartaPorte.Comprobante();
                StreamReader reader = new StreamReader(arch.Nombre);
                try
                {
                XmlSerializer serializer = new XmlSerializer(typeof(Comprobante));
                Factura = (Comprobante)serializer.Deserialize(reader);
                }
                catch (Exception)
                {
                   
                    XmlSerializer serializer = new XmlSerializer(typeof(Clases.CartaPorte.Comprobante));
                    Factura2 = (Clases.CartaPorte.Comprobante)serializer.Deserialize(reader);
                }                 
                Factura.UUID = System.IO.Path.GetFileName(arch.Nombre);
                foreach (Concepto1 concepto in Factura.Concepto.concepto1)
                {
                    if (concepto.impuestos!= null)
                    {
                        if (concepto.impuestos.Traslados.traslado.Impuesto == 1)
                        {
                            Factura.IVA8 += concepto.impuestos.Traslados.traslado.Importe;
                        }
                        else if (concepto.impuestos.Traslados.traslado.Impuesto == 2)
                        {
                            Factura.IVA16 += concepto.impuestos.Traslados.traslado.Importe;
                        }
                        else if (concepto.impuestos.Traslados.traslado.Impuesto==3)
                        {
                            Factura.IEPS += concepto.impuestos.Traslados.traslado.Importe;
                        }                   
                    }
                }
                if (Factura.Impuest != null)
                {
                    Factura.Retencion = Factura.Impuest.Retencion;

                }
                if (Factura.Complemento.Pagos!=null)
                {
                    Factura.Subtotal = Factura.Complemento.Pagos.Pago.Monto;
                    Factura.Total = Factura.Complemento.Pagos.Pago.Monto;
                }
               
                string existe = "";
                connection.Open();
                SqlCommand cmd = new SqlCommand("ExistsUUID", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UUID", Factura.UUID);
                SqlDataReader reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                     existe = reader1["Existe"].ToString();                    
                }
                connection.Close();

                if (Factura.Emisor.Rfc==rfc && existe=="No existe")
                {
                    Lista.Add(Factura);
                }

                reader.Close();
            }
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                txtTotal.Text = "0";
                txtTotFact.Text = "0";
                dgvFacturas.ItemsSource = Lista;
                Facturas = Lista;
            }));
        }

        private void dgvFacturas_AutoGeneratedColumns(object sender, EventArgs e)
        {

        }

     //evento para evitar que se muestren unas columnas en el datagrid
        private void dgvFacturas_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Emisor")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Concepto")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Impuest")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Complemento")
            {
                e.Cancel = true;
            }
      
        }

        //boton procesar sube todas las facturas en el datagrid a la base de datos
        private void btnProcesar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvFacturas.Items.Count <= 0)
            {
                MessageBox.Show("No hay facturas para procesar","Sin facturas",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else {
                List<Comprobante> Lista = new List<Comprobante>();
                for (int i = 0; i < dgvFacturas.Items.Count; i++)
                {
                    Comprobante factura = dgvFacturas.Items[i] as Comprobante;
                    if (factura.Seleccionar == true)
                    {
                        Lista.Add(factura);
                    }
                }
                if (Lista.Count > 0)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertContraRecibo", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@RFC", rfc);
                    cmd.Parameters.AddWithValue("@Numero", cbNumProv.SelectedItem);
                    cmd.ExecuteNonQuery();

                    foreach (Comprobante comprobante in Lista)
                    {
                        SqlCommand cmd1 = new SqlCommand("InsertContraReciboDetalle", connection);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Serie", comprobante.Serie);
                        cmd1.Parameters.AddWithValue("@Factura", comprobante.Factura);
                        cmd1.Parameters.AddWithValue("@Fechadet", comprobante.Fecha);
                        cmd1.Parameters.AddWithValue("@NumOrden", comprobante.NumOrden);
                        cmd1.Parameters.AddWithValue("@NumRepExt", comprobante.NumRepExt);
                        cmd1.Parameters.AddWithValue("@Subtotal", comprobante.Subtotal);
                        cmd1.Parameters.AddWithValue("@Iva1", comprobante.IVA8);
                        cmd1.Parameters.AddWithValue("@IvaTasa1", 0.08);
                        cmd1.Parameters.AddWithValue("@Iva2", comprobante.IVA16);
                        cmd1.Parameters.AddWithValue("@IvaTasa2", 0.16);
                        cmd1.Parameters.AddWithValue("@Retencion", comprobante.Retencion);
                        cmd1.Parameters.AddWithValue("@IEPS", comprobante.IEPS);
                        cmd1.Parameters.AddWithValue("@Total", comprobante.Total);
                        cmd1.Parameters.AddWithValue("@UUID", comprobante.UUID);
                        cmd1.ExecuteNonQuery();
                    }
                    connection.Close();
                    MessageBox.Show("Contra Recibo Procesado","Completado",MessageBoxButton.OK,MessageBoxImage.Information);
                    txtTotal.Text = "";
                    txtTotFact.Text = "";
                    List<Comprobante> ListaClear = new List<Comprobante>();
                    dgvFacturas.ItemsSource = ListaClear;
                    connection.Open();
                    SqlCommand cmd2 = new SqlCommand("ContraReciboFolio", connection);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader1 = cmd2.ExecuteReader();
                    while (reader1.Read())
                    {
                        int folio = Convert.ToInt32(reader1["folio"].ToString()) + 1;
                        txtContra.Text = folio.ToString();
                    }
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("No se selecciono ninguna factura", "Seleccione", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
           
        }

        //Evento para actualizar el textbox del proveedor cada vez que se cambia el numero de proveedor en el combobox
        private void cbNumProv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Proveedor proveedor in Lista)
            {
                if (proveedor.Numero.ToString()==cbNumProv.SelectedItem.ToString())
                {
                    txtProveedor.Text = proveedor.Nombre + "( " + proveedor.RFC + " )";
                    rfc = proveedor.RFC;
                    nombre = proveedor.Nombre;
                }
            }
        }

        //Boton buscar sirve para abrir la ventana para buscar los proveedores
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarProveedor bp = new BuscarProveedor();
            bp.ShowDialog();
            cbNumProv.SelectedItem = bp.numero;
        }

        private void btnTerminar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Evento para abrir el pdf al darle click derecho a un uuid 
        private void dgvFacturas_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
           
        }

        //propiedad de la columna editada 
        public Comprobante rowedited { get; set; }


        //evento para enviar el row que se este editando en el datagrid a la propiedad rowedited
        private void dgvFacturas_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {

                rowedited = e.Row.Item as Comprobante;
                TextBox text = e.EditingElement as TextBox;
                string a = e.EditingElement.ToString();
                a = a.Replace("System.Windows.Controls.CheckBox Content: IsChecked:", "");
                int cont = 0;
                double total = 0;
                int i = Facturas.IndexOf(rowedited);
                if (a=="True")
                {
                    Facturas = (List<Comprobante>)dgvFacturas.ItemsSource;
                    Facturas[i].Seleccionar = true;
                    foreach (Comprobante item in Facturas)
                    {
                        if (item.Seleccionar == true)
                        {
                            cont = cont + 1;
                            total = total + item.Total;
                        }
                    }
                    txtTotFact.Text = cont.ToString();
                    txtTotal.Text = total.ToString();
                }
                if (a == "False")
                {
                    Facturas = (List<Comprobante>)dgvFacturas.ItemsSource;
                    Facturas[i].Seleccionar = false;
                    foreach (Comprobante item in Facturas)
                    {
                        if (item.Seleccionar == true)
                        {
                            cont = cont + 1;
                            total = total + item.Total;
                        }
                    }
                    txtTotFact.Text = cont.ToString();
                    txtTotal.Text = total.ToString();
                }
                if (e.Column.Header!=null)
                {
                    if (e.Column.Header.ToString() == "NumRepExt")
                    {
                        if (e.EditAction == DataGridEditAction.Cancel)
                        {

                            rowedited.NumRepExt = 0;
                        }
                        else
                        {
                            rowedited.NumRepExt = Convert.ToInt32(text.Text);
                        }

                    }
                    else if (e.Column.Header.ToString() == "NumOrden")
                    {
                        if (e.EditAction == DataGridEditAction.Cancel)
                        {
                            rowedited.NumOrden = 0;

                        }
                        else
                        {
                            rowedited.NumOrden = Convert.ToInt32(text.Text);
                        }

                    }                   
                }              

            }
            catch (Exception ex)
            {
               MessageBox.Show("Error "+ex.Message,"Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
       
        }
        //se verifica que el row edited las propiedades numorden o rep ext no existan en la base de datos twofacpro
        private void dgvFacturas_CurrentCellChanged(object sender, EventArgs e)
        {
       
            if (rowedited!=null)
            {
                string contra = "";
                string existe = "";
                DateTime fecha = DateTime.Now;
                if (rowedited.NumOrden!= 0)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("ValidarOrdenMto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NumOrden",rowedited.NumOrden);
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {

                        existe = reader1["Mensaje"].ToString();
                        if (existe != "No Existe")
                        {
                            contra = reader1["contra"].ToString();
                            fecha = Convert.ToDateTime(reader1["fecha"].ToString());
                        }
                        
                    }
                    connection.Close();
                    if (existe == "Existe")
                    {                     
                        MessageBox.Show("Esta orden de mantenimiento ya tiene factura \nContra recibo:"+ contra + " Fecha: "+fecha.ToShortDateString() ,"Error",MessageBoxButton.OK, MessageBoxImage.Warning);
                        int a =dgvFacturas.SelectedIndex;

                        List<Comprobante> comprobantes = new List<Comprobante>();
                        for (int i = 0; i < dgvFacturas.Items.Count; i++)
                        {
                            Comprobante factura = dgvFacturas.Items[i] as Comprobante;                         
                            comprobantes.Add(factura);                      
                        }
                        rowedited.NumOrden = 0;
                        comprobantes[a] = rowedited ;
                        dgvFacturas.ItemsSource = comprobantes;
                      
                    }

                }
                if (rowedited.NumRepExt != 0)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("ValidarOrdenRepExt", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NumRepExt", rowedited.NumRepExt);
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        existe = reader1["Mensaje"].ToString();
                        if (existe != "No Existe")
                        {
                            contra = reader1["contra"].ToString();
                            fecha = Convert.ToDateTime(reader1["fecha"].ToString());
                        }
                    }
                    connection.Close();
                    if (existe == "Existe")
                    {
                        MessageBox.Show("Esta orden de reparación externa ya tiene factura \nContra recibo:" + contra + " Fecha: " + fecha.ToShortDateString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                        int a = dgvFacturas.SelectedIndex;

                        List<Comprobante> comprobantes = new List<Comprobante>();
                        for (int i = 0; i < dgvFacturas.Items.Count; i++)
                        {
                            Comprobante factura = dgvFacturas.Items[i] as Comprobante;                          
                            comprobantes.Add(factura);                      
                        }
                        rowedited.NumRepExt = 0;
                        comprobantes[a] = rowedited;
                        dgvFacturas.ItemsSource = comprobantes;

                    }
                }
            }
           
        }

        private void dgvFacturas_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           
        }

        private void dgvFacturas_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void dgvFacturas_AutoGeneratedColumns_1(object sender, EventArgs e)
        {

        }

        private void dgvFacturas_MouseUp(object sender, MouseButtonEventArgs e)
        {
           

        }

        private void dgvFacturas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //var hit = VisualTreeHelper.HitTest((Visual)sender, e.GetPosition((IInputElement)sender));
            //DependencyObject cell = VisualTreeHelper.GetParent(hit.VisualHit);
            //while (cell != null && !(cell is System.Windows.Controls.DataGridCell)) cell = VisualTreeHelper.GetParent(cell);
            //System.Windows.Controls.DataGridCell targetCell = cell as System.Windows.Controls.DataGridCell;
            //try
            //{
            //    if (targetCell != null)
            //    {
            //        if (targetCell.Column.Header.ToString() == "Seleccionar")
            //        {
            //            int cont = 0;
            //            double total = 0;
            //            Facturas = (List<Comprobante>)dgvFacturas.ItemsSource;
            //            foreach (Comprobante item in Facturas)
            //            {
            //                if (item.Seleccionar == true)
            //                {
            //                    cont = cont + 1;
            //                    total = total + item.Total;
            //                }
            //            }
            //            txtTotFact.Text = cont.ToString();
            //            txtTotal.Text = total.ToString();
            //        }
            //    }
            //    else
            //    {
            //        int cont = 0;
            //        double total = 0;
            //        Facturas = (List<Comprobante>)dgvFacturas.ItemsSource;
            //        foreach (Comprobante item in Facturas)
            //        {
            //            if (item.Seleccionar == true)
            //            {
            //                cont = cont + 1;
            //                total = total + item.Total;
            //            }
            //        }
            //        txtTotFact.Text = cont.ToString();
            //        txtTotal.Text = total.ToString();
            //    }
            //}
            //catch (Exception)
            //{

            //}
        }

        private void dgvFacturas_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void CheckBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var hit = VisualTreeHelper.HitTest((Visual)sender, e.GetPosition((IInputElement)sender));
            //DependencyObject cell = VisualTreeHelper.GetParent(hit.VisualHit);
            //while (cell != null && !(cell is System.Windows.Controls.DataGridCell)) cell = VisualTreeHelper.GetParent(cell);
            //System.Windows.Controls.DataGridCell targetCell = cell as System.Windows.Controls.DataGridCell;
            //try
            //{
            //    if (targetCell != null)
            //    {
            //        if (targetCell.Column.Header.ToString() == "Seleccionar")
            //        {
                        
            //            int cont = 0;
            //            double total = 0;
            //            Facturas = (List<Comprobante>)dgvFacturas.ItemsSource;
            //            foreach (Comprobante item in Facturas)
            //            {
            //                if (item.Seleccionar == true)
            //                {
            //                    cont = cont + 1;
            //                    total = total + item.Total;
            //                }
            //            }
            //            txtTotFact.Text = cont.ToString();
            //            txtTotal.Text = total.ToString();
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //}

        }

      
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Seleccionar_Click(object sender, RoutedEventArgs e)
        {
       
        }

        private void dgvFacturas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void dgvFacturas_PreviewMouseRightButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var hit = VisualTreeHelper.HitTest((Visual)sender, e.GetPosition((IInputElement)sender));
            DependencyObject cell = VisualTreeHelper.GetParent(hit.VisualHit);
            while (cell != null && !(cell is System.Windows.Controls.DataGridCell)) cell = VisualTreeHelper.GetParent(cell);
            System.Windows.Controls.DataGridCell targetCell = cell as System.Windows.Controls.DataGridCell;
            try
            {
                if (targetCell != null)
                {
                    if (targetCell.Column.Header.ToString() == "UUID")
                    {
                        string target = targetCell.ToString().Replace("System.Windows.Controls.DataGridCell: ", "");
                        target = target.Replace("xml", "pdf");
                        System.Diagnostics.Process.Start(path + "\\" + target);

                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
