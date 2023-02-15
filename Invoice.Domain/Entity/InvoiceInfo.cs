using System.ComponentModel.DataAnnotations;

namespace Invoice.Domain.Entity
{
    public class InvoiceInfo
    {
        [Key]
        public int Id { get; set; }
        public int BillTo { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public List<InvoiceDetails> invoiceDetails { get; set; }
    }
}