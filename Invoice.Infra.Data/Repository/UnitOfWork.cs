using Invoice.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _contex;
        public IInvoiceInfoRepository InvoiceInfo { get; }
        public IInvoiceDetailsRepository InvoiceDetails { get; }

        public UnitOfWork(AppDbContext appDbContext, IInvoiceInfoRepository invoiceInfoRepository, IInvoiceDetailsRepository invoiceDetailsRepository )
        {
            _contex = appDbContext;
            InvoiceInfo = invoiceInfoRepository;
            InvoiceDetails = invoiceDetailsRepository;
        }

        public int complete()
        {
            return _contex.SaveChanges();
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
