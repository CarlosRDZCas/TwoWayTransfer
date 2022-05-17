using Microsoft.Reporting.WinForms;
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
using System.Windows.Shapes;
using Two_Way_Trasnfer.Clases.FacturasProveedores;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para ReporteFacturasProcesadas.xaml
    /// </summary>
    public partial class ReporteFacturasProcesadas : Window
    {
        public ReporteFacturasProcesadas(List<FacturasProcesadas> lista)
        {
            InitializeComponent();
            dataset = lista;
            dataset.Sort((x, y) => x.Empresa.CompareTo(y.Empresa));
            _reportViewer.Load += ReportViewer_Load;
        }
        List<FacturasProcesadas> dataset = new List<FacturasProcesadas>();
        private bool _isReportViewerLoaded;
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource();
                this._reportViewer.LocalReport.DataSources.Clear();
                this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset));
                this._reportViewer.LocalReport.ReportEmbeddedResource = "Two_Way_Trasnfer.Report2.rdlc";

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = _reportViewer.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);

                using (FileStream fs = new FileStream("output.pdf", FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
                _reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
