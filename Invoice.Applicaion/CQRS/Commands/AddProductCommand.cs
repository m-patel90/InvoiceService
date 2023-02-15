using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.Domain.Entity;
using MediatR;

namespace Invoice.Applicaion.CQRS.Commands
{
    public class AddProductCommand : IRequest<Product>
    {
        public Product product { get; set; }
    }
}
