using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.DTO
{
    public class InvoiceDetailsDTO
    {
        public string ProductName { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
        public int Qunatity { get; set; }
        public decimal Tax { get; set; }
        public double Total { get; set; }
    }
}
