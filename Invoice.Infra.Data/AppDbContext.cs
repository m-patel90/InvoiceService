using Invoice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<InvoiceInfo> InvoiceInfo => Set<InvoiceInfo>();
        public DbSet<InvoiceDetails> InvoiceDetails => Set<InvoiceDetails>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Customer> Customers => Set<Customer>();
    }
}
