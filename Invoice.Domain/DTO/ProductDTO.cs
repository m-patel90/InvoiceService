using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid UniqueId { get; set; }
        public decimal Weight { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public string HSNCode { get; set; } = string.Empty;
    }
}
