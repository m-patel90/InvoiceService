using Invoice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Interfaces
{
    public interface IProductInventoryRepository : IGenericRepository<ProductInventory>
    {
        Task<ProductInventory> GetByProductId(int productId);
        Task<List<ProductInventory>> GetLowStockItems();
    }
}
