using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.Domain.Entity;

namespace Invoice.Domain.DTO
{
    public class InvoiceInfoDTO
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string InvoiceNo { get; set; } = String.Empty;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = String.Empty;
        public List<InvoiceDetails> invoiceDetails { get; set; }

        public int TotalRecords { get; set; }
    }
}
