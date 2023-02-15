using Invoice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Interfaces
{
    public interface ICustomeDapperRepository
    {
        List<Customer> GetAll();
        Customer Get(int id);
        void Insert(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}
