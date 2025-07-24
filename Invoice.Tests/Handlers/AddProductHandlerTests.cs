using FluentAssertions;
using Invoice.Applicaion.CQRS.Commands;
using Invoice.Applicaion.CQRS.Handlers;
using Invoice.Applicaion.CQRS.Notifications;
using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Invoice.Tests.Handlers
{
    public class AddProductHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IMediator> _mockMediator;
        private readonly AddProductHandler _handler;

        public AddProductHandlerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMediator = new Mock<IMediator>();
            _handler = new AddProductHandler(_mockProductRepository.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task Handle_Should_Create_Product_Successfully()
        {
            var product = new Product
            {
                Name = "Test Product",
                ProductCode = "TP001",
                Price = 100.50m
            };

            var command = new AddProductCommand { product = product };

            _mockProductRepository
                .Setup(x => x.CreateProduct(It.IsAny<Product>()))
                .ReturnsAsync(product);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.UniqueId.Should().NotBe(Guid.Empty);
            result.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

            _mockProductRepository.Verify(x => x.CreateProduct(It.IsAny<Product>()), Times.Once);
            _mockMediator.Verify(x => x.Publish(It.IsAny<ProductAddedNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
