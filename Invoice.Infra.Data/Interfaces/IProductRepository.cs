using Invoice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> CreateProduct(Product product);
        Task<Product> GetById(int id);
        Task<Product> GetByProductCode(string productCode);
        Task<List<Product>> GetByCategory(string category);
    }
}
