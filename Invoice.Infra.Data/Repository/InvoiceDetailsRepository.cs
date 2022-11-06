using Invoice.Domain;
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
        public InvoiceDetailsRepository(AppDbContext appDbContext): base(appDbContext)
        {
        }
    }
}
