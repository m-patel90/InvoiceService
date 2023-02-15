using Invoice.Applicaion.CQRS.Commands;
using Invoice.Applicaion.CQRS.Notifications;
using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using MediatR;

namespace Invoice.Applicaion.CQRS.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;
        public AddProductHandler(IProductRepository productRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _mediator = mediator;
        }

        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.CreateProduct(request.product);
            await _mediator.Publish(new ProductAddedNotification() { product = result });
            return result;
        }
    }
}
