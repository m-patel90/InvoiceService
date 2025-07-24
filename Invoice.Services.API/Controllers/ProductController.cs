using AutoMapper;
using Invoice.Applicaion.CQRS.Commands;
using Invoice.Applicaion.CQRS.Notifications;
using Invoice.Applicaion.CQRS.Queries;
using Invoice.Domain.DTO;
using Invoice.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace Invoice.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IDistributedCache distributedCache, IMapper mapper)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var productCommand = new AddProductCommand() { product = product };
            var productRetrun = await _mediator.Send(productCommand);
            return Ok(productRetrun);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductDto(ProductDTO productDto)
        {
            Log.Information("Add Product API call for product: {ProductName}", productDto.Name);
            
            var product = _mapper.Map<Product>(productDto);
            product.UniqueId = Guid.NewGuid();
            product.CreatedDate = DateTime.UtcNow;
            
            var command = new AddProductCommand { product = product };
            var result = await _mediator.Send(command);
            
            var resultDto = _mapper.Map<ProductDTO>(result);
            return Ok(resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDto)
        {
            Log.Information("Update Product API call for product ID: {ProductId}", id);
            
            if (id != productDto.Id)
            {
                return BadRequest("Product ID mismatch");
            }
            
            var product = _mapper.Map<Product>(productDto);
            var command = new UpdateProductCommand { Product = product };
            var result = await _mediator.Send(command);
            
            var resultDto = _mapper.Map<ProductDTO>(result);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Log.Information("Delete Product API call for product ID: {ProductId}", id);
            
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
            {
                return NotFound($"Product with ID {id} not found");
            }
            
            return NoContent();
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct()
        {
            string serializedcustomerlist;
            var products = new List<Product>();
            products = await _mediator.Send(new GetProductQuery());
            serializedcustomerlist = JsonConvert.SerializeObject(products);
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            Log.Information("Get Products API call");
            
            var result = await _mediator.Send(new GetProductQuery());
            var resultDto = _mapper.Map<List<ProductDTO>>(result);
            return Ok(resultDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            Log.Information("Get Product API call for product ID: {ProductId}", id);
            
            var query = new GetProductByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
            {
                return NotFound($"Product with ID {id} not found");
            }
            
            var resultDto = _mapper.Map<ProductDTO>(result);
            return Ok(resultDto);
        }

        [HttpPost("with-inventory")]
        public async Task<IActionResult> CreateProductWithInventory(CreateProductWithInventoryRequest request)
        {
            Log.Information("Create Product with Inventory API call for product: {ProductName}", request.Product.Name);
            
            var product = _mapper.Map<Product>(request.Product);
            var command = new CreateProductWithInventoryCommand 
            { 
                Product = product,
                InitialQuantity = request.InitialQuantity,
                ReorderLevel = request.ReorderLevel,
                Location = request.Location
            };
            
            var result = await _mediator.Send(command);
            var resultDto = _mapper.Map<ProductDTO>(result);
            return Ok(resultDto);
        }
    }

    public class CreateProductWithInventoryRequest
    {
        public ProductDTO Product { get; set; }
        public int InitialQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
