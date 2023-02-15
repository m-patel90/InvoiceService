using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Repository
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        protected readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }
    }
}
