using Invoice.Applicaion.CQRS.Commands;
using Invoice.Applicaion.CQRS.Queries;
using Invoice.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var productCommand = new AddProductCommand() { product = product };
            var products = await _mediator.Send(productCommand);
            return Ok(products);
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _mediator.Send(new GetProductQuery());
            return Ok(products);
        }
    }
}
