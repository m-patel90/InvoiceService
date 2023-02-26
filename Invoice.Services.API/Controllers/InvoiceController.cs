using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Invoice.Applicaion.Interface;
using Invoice.Applicaion.Validations;
using Invoice.Domain;
using Invoice.Domain.DTO;
using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Invoice.Services.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceService _invoiceService;
        private IMapper _mapper;        
        public InvoiceController(IInvoiceService invoiceService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _invoiceService = invoiceService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoice([FromQuery] PagingRequestModel pagingModel)
        {
            Log.Information("Get Invoice API call");
            var invoices = _unitOfWork.InvoiceInfo.GetInvoiceWithPaging(pagingModel);
            //var invoiceInfo = await _unitOfWork.InvoiceInfo.GetAll().ToListAsync();
            //foreach (var item in invoiceInfo)
            //{
            //    var detail = _unitOfWork.InvoiceDetails.GetByInvoiceId(item.Id);
            //    item.invoiceDetails = detail;
            //}

            var metadata = new
            {
                invoices.TotalCount,
                invoices.PageSize,
                invoices.CurrentPage,
                invoices.TotalPages,
                invoices.hasPrivious,
                invoices.hasNext
            };
            invoices[0].TotalRecords = invoices.TotalCount;
            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(metadata));

            return Ok(invoices);
        }

        [HttpPost("SaveInvoice")]
        public async Task<IActionResult> SaveInvoice(InvoiceDTO invoiceDTO)
        {
            var invoiceInfo = _mapper.Map<InvoiceInfo>(invoiceDTO);

            InvoiceInfoValidator validator = new InvoiceInfoValidator();
            validator.ValidateAndThrow(invoiceInfo);


            //ValidationResult result = validator.Validate(invoiceInfo);
            //if (!result.IsValid)
            //{
            //    return BadRequest(result.Errors);
            //}

            await _invoiceService.SaveInvoice(invoiceInfo);
            return Ok(invoiceInfo);
        }
    }
}
