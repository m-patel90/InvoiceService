using Invoice.Applicaion.Interfaces;
using Invoice.Domain.Entity;
using Invoice.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Services
{
    public class CustomerDapperService : ICustomerDapperService
    {
        private ICustomeDapperRepository _customeDapperRepository;
        public CustomerDapperService(ICustomeDapperRepository customeDapperRepository)
        {
            _customeDapperRepository = customeDapperRepository;
        }

        public List<Customer> GetAll()
        {
            return _customeDapperRepository.GetAll();
        }

        public Customer GetById(int id)
        {
            return _customeDapperRepository.Get(id);
        }

        public void Insert(Customer customer)
        {
            _customeDapperRepository.Insert(customer);
        }
        public void Update(Customer customer)
        {
            _customeDapperRepository.Update(customer);
        }
        public void Delete(int id)
        {
            _customeDapperRepository.Delete(id);
        }
    }
}
