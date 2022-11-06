using Invoice.Domain;
using Invoice.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Repository
{
    public class InvoiceInfoRepository : GenericRepository<InvoiceInfo>,IInvoiceInfoRepository
    {
        public InvoiceInfoRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<InvoiceInfo>> GetInvoiceByCustomerID(int custId)
        {
            throw new NotImplementedException();
        }
    }
}
