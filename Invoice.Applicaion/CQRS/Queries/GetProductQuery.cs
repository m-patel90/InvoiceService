using Invoice.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.CQRS.Queries
{
    public class GetProductQuery : IRequest<List<Product>>
    {
        IEnumerable<Product> product;
    }
}
