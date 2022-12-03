using Invoice.Applicaion.CQRS.Commands;
using Invoice.Domain;
using Invoice.Infra.Data.Interfaces;
using MediatR;

namespace Invoice.Applicaion.CQRS.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public AddProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.CreateProduct(request.product);
        }
    }
}
