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
    public class ProductInventoryRepository : GenericRepository<ProductInventory>, IProductInventoryRepository
    {
        public ProductInventoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ProductInventory> GetByProductId(int productId)
        {
            return await appDb.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefaultAsync(pi => pi.ProductId == productId);
        }

        public async Task<List<ProductInventory>> GetLowStockItems()
        {
            return await appDb.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => pi.Quantity <= pi.ReorderLevel)
                .ToListAsync();
        }
    }
}
