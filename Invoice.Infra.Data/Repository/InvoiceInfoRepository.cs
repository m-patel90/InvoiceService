using Invoice.Domain;
using Invoice.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
//using System.Linq.Dynamic.Core;
using System.Text;
using System.Security.Cryptography;
using Invoice.Domain.DTO;
using Invoice.Infra.Data.Extention;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading;
using Invoice.Domain.Entity;
using System.Runtime.InteropServices;

namespace Invoice.Infra.Data.Repository
{
    public class InvoiceInfoRepository : GenericRepository<InvoiceInfo>,IInvoiceInfoRepository
    {
        private readonly AppDbContext appDb;
        public InvoiceInfoRepository(AppDbContext context) : base(context)
        {
            appDb = context;
        }

        public Task<List<InvoiceInfo>> GetInvoiceByCustomerID(int custId)
        {
            throw new NotImplementedException();
        }

        public PagedList<InvoiceInfoDTO> GetInvoiceWithPaging(PagingRequestModel paging)
        {
            IQueryable<InvoiceInfoDTO> data = from invoice in appDb.InvoiceInfo
                                              join cust in appDb.Customers
                                              on invoice.BillTo equals cust.Id
                                              orderby invoice.InvoiceDate ascending
                                              select new InvoiceInfoDTO
                                              {
                                                  Id = invoice.Id,
                                                  Customer = cust.Name,
                                                  InvoiceNo = invoice.InvoiceNo,
                                                  InvoiceDate = invoice.InvoiceDate,
                                                  DueDate = invoice.DueDate,
                                                  Status = invoice.Status
                                              };

            var result = data.OrderByDynamic<InvoiceInfoDTO>(paging.orderBy, paging.direction).Paging<InvoiceInfoDTO>(paging.PageSize, paging.PageNumber).ToList();            
            return new PagedList<InvoiceInfoDTO>(result, data.Count(), paging.PageNumber, paging.PageSize);
        }

        private void ApplySort(ref IQueryable<InvoiceInfo> invoices, string orderbystring, string byOrder)
        {
            if (!invoices.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderbystring))
            {
                invoices = invoices.OrderBy(x => x.InvoiceDate);
            }

            //var orderParams = orderbystring.Trim().Split(",");
            var queryBuilder = new StringBuilder();
            //foreach(var param in orderParams)
            //{
            //    if (string.IsNullOrWhiteSpace(param))
            //        continue;

                var propertyInfos = typeof(InvoiceInfo).GetProperty(orderbystring);
            if (propertyInfos == null)
                return;

                queryBuilder.Append($"{propertyInfos.Name}");
            //}

            var orderQuery = queryBuilder.ToString();

            invoices.OrderBy(x => propertyInfos.Name);
        }

        private void ApplyPagination(ref IQueryable<InvoiceInfo> invoices, PagingRequestModel paging)
        {
            if (!invoices.Any())
                return;

            invoices.Skip((paging.PageNumber -1) * paging.PageSize).Take(paging.PageSize).ToList();
        }
    }
}
