using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.DTO
{
    public class InvoiceDTO
    {
        public int BillTo { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public double SubTotal { get; set; }
        public double TotalTax { get; set; }
        public double GrandTotal { get; set; }
        public string InvoiceNote { get; set; }
        public List<InvoiceDetailsDTO> Details { get; set; }
    }
}
