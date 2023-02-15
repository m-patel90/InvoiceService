//using Invoice.Domain;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invoice.Infra.Data
//{
//    public static class PagerExtension
//    {
//        public static async Task<PagedList<TModel>> PaginateAsync<TModel>(
//           this IQueryable<TModel> query,
//           int page,
//           int limit,
//           CancellationToken cancellationToken)
//           where TModel : class
//        {

//            var paged = new PagedList<TModel>();

//            page = (page < 0) ? 1 : page;

//            //paged.CurrentPage = page;
//            paged.PageSize = limit;



//            var startRow = (page - 1) * limit;
//            paged.Items = await query
//                       .Skip(startRow)
//                       .Take(limit)
//                       .ToListAsync(cancellationToken);

//            paged.TotalItems = await totalItemsCountTask;
//            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

//            return paged;
//        }
//    }
//}
