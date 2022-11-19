using Invoice.Applicaion.Interface;
using Invoice.Domain;
using Invoice.Infra.Data.Interfaces;
using Invoice.Infra.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Invoice.Services.API.Controllers
{
    [Authorize]
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
            await _invoiceService.SaveInvoice(invoiceInfo);
            return Ok(invoiceInfo);
        }
    }
}
