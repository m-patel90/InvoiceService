using FluentValidation.Results;
using Invoice.Applicaion.Interface;
using Invoice.Applicaion.Validations;
using Invoice.Domain;
using Invoice.Infra.Data.Interfaces;
using Invoice.Infra.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Runtime.CompilerServices;

namespace Invoice.Services.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceService _invoiceService;
        
        public InvoiceController(IInvoiceService invoiceService, IUnitOfWork unitOfWork)
        {
            _invoiceService = invoiceService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            throw new NullReferenceException();
            Log.Information("Get Invoice API call");
            var invoiceInfo = await _unitOfWork.InvoiceInfo.GetAll();
            foreach (var item in invoiceInfo)
            {
                var detail = _unitOfWork.InvoiceDetails.GetByInvoiceId(item.Id);
                item.invoiceDetails = detail;
            }
            return Ok(invoiceInfo);
        }

        [HttpPost("SaveInvoice")]
        public async Task<IActionResult> SaveInvoice(InvoiceInfo invoiceInfo)
        {
            InvoiceInfoValidator validator = new InvoiceInfoValidator();
            ValidationResult result = validator.Validate(invoiceInfo);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await _invoiceService.SaveInvoice(invoiceInfo);
            return Ok(invoiceInfo);
        }
    }
}
