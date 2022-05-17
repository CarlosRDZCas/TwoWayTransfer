using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases.CAPSIN
{
    internal class CapsinModel
    {
        public DateTime ClearingDate { get; set; }
        public string VendorInvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public double Amount { get; set; } 
    }
}
