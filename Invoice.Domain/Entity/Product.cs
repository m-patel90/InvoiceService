using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Entity
{
    public class Product
    {
        [Key]
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
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        [Obsolete("Use Price property instead")]
        public decimal price { get; set; }
    }
}
