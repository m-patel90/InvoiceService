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
        public IProductInventoryRepository ProductInventory { get; }

        public UnitOfWork(AppDbContext appDbContext, IInvoiceInfoRepository invoiceInfoRepository, IInvoiceDetailsRepository invoiceDetailsRepository, IProductInventoryRepository productInventoryRepository)
        {
            _contex = appDbContext;
            InvoiceInfo = invoiceInfoRepository;
            InvoiceDetails = invoiceDetailsRepository;
            ProductInventory = productInventoryRepository;
        }

        public int complete()
        {
            return _contex.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _contex.SaveChangesAsync();
        }
    }
}
