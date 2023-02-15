using Invoice.Domain;
using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Extention
{
    public static class Sort
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, string direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                direction == "asc" ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }

        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 10, int pageNumber = 1) where TModel : class
        {
            var count = query.Count(); 
            return pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
        }

        //public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 10, int pageNumber = 1) where TModel : class
        //       => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;

        public static IQueryable<InvoiceInfo> ApplySort(this IQueryable<InvoiceInfo> invoices, string orderbystring)
        {
            //if (!invoices.Any())
            //    return;

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
            //if (propertyInfos == null)
            //    return;

            queryBuilder.Append($"{propertyInfos.Name}");
            //}

            var orderQuery = queryBuilder.ToString();

            return invoices.OrderBy(x => propertyInfos.Name);
        }
    }
}
