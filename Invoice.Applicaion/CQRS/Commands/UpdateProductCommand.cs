using Invoice.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.CQRS.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public Product Product { get; set; }
    }
}
