using Invoice.Applicaion.CQRS.Commands;
using Invoice.Applicaion.CQRS.Notifications;
using Invoice.Applicaion.CQRS.Queries;
using Invoice.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;

namespace Invoice.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public ProductController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var productCommand = new AddProductCommand() { product = product };
            var productRetrun = await _mediator.Send(productCommand);
            return Ok(productRetrun);
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct()
        {
            string serializedcustomerlist;
            var redisProductList = await _distributedCache.GetAsync("productlist");
            var products = new List<Product>();
            if (redisProductList != null)
            {
                serializedcustomerlist = Encoding.UTF8.GetString(redisProductList);
                products = JsonConvert.DeserializeObject<List<Product>>(serializedcustomerlist);
            } 
            else {
                products = await _mediator.Send(new GetProductQuery());
                serializedcustomerlist = JsonConvert.SerializeObject(products);
                redisProductList = Encoding.UTF8.GetBytes(serializedcustomerlist);
                var options =
                    new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync("productlist", redisProductList, options);
            }
            return Ok(products);
        }
    }
}
