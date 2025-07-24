using Invoice.Applicaion.CQRS.Commands;
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
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProductHandler> _logger;

        public UpdateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<UpdateProductHandler> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", request.Product.Id);
            
            request.Product.UpdatedDate = DateTime.UtcNow;
            await _productRepository.Update(request.Product);
            await _unitOfWork.CompleteAsync();
            
            _logger.LogInformation("Product updated successfully with ID: {ProductId}", request.Product.Id);
            return request.Product;
        }
    }
}
