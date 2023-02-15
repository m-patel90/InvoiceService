using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Repository
{
    public class InvoiceDetailsRepository : GenericRepository<InvoiceDetails>, IInvoiceDetailsRepository
    {
        private readonly AppDbContext appDb; 
        public InvoiceDetailsRepository(AppDbContext appDbContext): base(appDbContext)
        {
            appDb = appDbContext;
        }

        public List<InvoiceDetails> GetByInvoiceId(int invoiceId)
        {
            return appDb.InvoiceDetails.Where(x => x.InvoiceInfoId == invoiceId).ToList();
        }
    }
}
