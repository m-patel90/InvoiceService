using Invoice.Applicaion.Interface;
using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
                _unitOfWork = unitOfWork;
        }

        public async Task SaveInvoice(InvoiceInfo invoiceInfo)
        {
            await _unitOfWork.InvoiceInfo.Add(invoiceInfo);
            foreach(InvoiceDetails inv in invoiceInfo.Details)
            {
                await _unitOfWork.InvoiceDetails.Add(inv);
            }
            _unitOfWork.complete();
        }
    }
}
