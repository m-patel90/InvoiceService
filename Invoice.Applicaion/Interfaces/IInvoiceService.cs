using Invoice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Interface
{
    public interface IInvoiceService
    {
        Task SaveInvoice(InvoiceInfo invoiceInfo);
    }
}
