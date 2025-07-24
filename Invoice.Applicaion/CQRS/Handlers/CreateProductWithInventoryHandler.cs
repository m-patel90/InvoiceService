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
    public class CreateProductWithInventoryHandler : IRequestHandler<CreateProductWithInventoryCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProductWithInventoryHandler> _logger;

        public CreateProductWithInventoryHandler(
            IProductRepository productRepository,
            IProductInventoryRepository inventoryRepository,
            IUnitOfWork unitOfWork,
            ILogger<CreateProductWithInventoryHandler> logger)
        {
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Product> Handle(CreateProductWithInventoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating product with inventory. Product: {ProductName}", request.Product.Name);
            
            try
            {
                request.Product.UniqueId = Guid.NewGuid();
                request.Product.CreatedDate = DateTime.UtcNow;
                
                await _productRepository.Add(request.Product);
                await _unitOfWork.CompleteAsync();
                
                var inventory = new ProductInventory
                {
                    ProductId = request.Product.Id,
                    Quantity = request.InitialQuantity,
                    ReorderLevel = request.ReorderLevel,
                    Location = request.Location,
                    LastUpdated = DateTime.UtcNow
                };
                
                await _inventoryRepository.Add(inventory);
                await _unitOfWork.CompleteAsync();
                
                _logger.LogInformation("Product and inventory created successfully. Product ID: {ProductId}, Inventory ID: {InventoryId}", 
                    request.Product.Id, inventory.Id);
                
                return request.Product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product with inventory: {ProductName}", request.Product.Name);
                throw;
            }
        }
    }
}
