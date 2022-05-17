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
using System.Windows.Shapes;

namespace Two_Way_Trasnfer
{
    /// <summary>
    /// Lógica de interacción para reporteCuentasSAT.xaml
    /// </summary>
    public partial class reporteCuentasSAT : Window
    {
        public reporteCuentasSAT()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }
        private bool _isReportViewerLoaded;
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                TWTDataSet dataset = new TWTDataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "TWTDataSet"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = dataset.sp_selectscctas;

                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.ReportEmbeddedResource = "Two_Way_Trasnfer.Report1.rdlc";
                

                dataset.EndInit();

                //fill data into adventureWorksDataSet
                TWTDataSetTableAdapters.sp_selectscctasTableAdapter sp_SelectscctasTableAdapter = new TWTDataSetTableAdapters.sp_selectscctasTableAdapter();
                sp_SelectscctasTableAdapter.ClearBeforeFill = true;
                sp_SelectscctasTableAdapter.Fill(dataset.sp_selectscctas);

                _reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
