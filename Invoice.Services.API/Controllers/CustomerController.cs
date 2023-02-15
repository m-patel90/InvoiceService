using Invoice.Applicaion.Interfaces;
using Invoice.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Services.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerDapperService _customerDapperService;
        public CustomerController(ICustomerDapperService customerDapperService)
        {
            _customerDapperService = customerDapperService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var customers = _customerDapperService.GetAll();
            return Ok(customers);
        }

        [HttpPost("SaveCustomer")]
        public IActionResult SaveCustomer(Customer customer)
        {
            _customerDapperService.Insert(customer);
            return Ok();
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _customerDapperService.GetById(id);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _customerDapperService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            _customerDapperService.Update(customer);
            return Ok();
        }
    }
}
