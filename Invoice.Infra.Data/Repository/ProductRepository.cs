using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Product> GetById(int id)
        {
            return await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetByProductCode(string productCode)
        {
            return await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductCode == productCode);
        }

        public async Task<List<Product>> GetByCategory(string category)
        {
            return await _appDbContext.Products.Where(p => p.Description.Contains(category)).ToListAsync();
        }
    }
}
