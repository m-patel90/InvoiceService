using Invoice.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.CQRS.Commands
{
    public class CreateProductWithInventoryCommand : IRequest<Product>
    {
        public Product Product { get; set; }
        public int InitialQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
