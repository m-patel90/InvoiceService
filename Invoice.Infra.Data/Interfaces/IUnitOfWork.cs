using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Interfaces
{
    public interface IUnitOfWork //: IDisposable
    {
        IInvoiceInfoRepository InvoiceInfo { get; }
        IInvoiceDetailsRepository InvoiceDetails { get; }
        int complete();
    }
}
