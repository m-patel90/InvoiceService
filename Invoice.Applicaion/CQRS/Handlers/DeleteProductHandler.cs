using Invoice.Applicaion.CQRS.Commands;
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
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProductHandler> _logger;

        public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<DeleteProductHandler> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", request.Id);
            
            var product = await _productRepository.GetById(request.Id);
            if (product == null)
            {
                _logger.LogWarning("Product not found with ID: {ProductId}", request.Id);
                return false;
            }

            await _productRepository.Delete(product);
            await _unitOfWork.CompleteAsync();
            
            _logger.LogInformation("Product deleted successfully with ID: {ProductId}", request.Id);
            return true;
        }
    }
}
