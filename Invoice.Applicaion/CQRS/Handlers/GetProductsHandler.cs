using Invoice.Applicaion.CQRS.Queries;
using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.CQRS.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<List<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
           return _productRepository.GetAll().ToListAsync();
        }
    }
}
