using Invoice.Domain;
using Invoice.Domain.DTO;
using Invoice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Interfaces
{
    public interface IInvoiceInfoRepository : IGenericRepository<InvoiceInfo>
    {
        Task<List<InvoiceInfo>> GetInvoiceByCustomerID(int custId);

        PagedList<InvoiceInfoDTO> GetInvoiceWithPaging(PagingRequestModel pagingModel);
    }
}
