using Invoice.Applicaion.CQRS.Queries;
using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.CQRS.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductByIdHandler> _logger;

        public GetProductByIdHandler(IProductRepository productRepository, ILogger<GetProductByIdHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting product with ID: {ProductId}", request.Id);
            
            var product = await _productRepository.GetById(request.Id);
            
            if (product == null)
            {
                _logger.LogWarning("Product not found with ID: {ProductId}", request.Id);
            }
            else
            {
                _logger.LogInformation("Product retrieved successfully with ID: {ProductId}", request.Id);
            }
            
            return product;
        }
    }
}
