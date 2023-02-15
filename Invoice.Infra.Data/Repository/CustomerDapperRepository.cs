using Invoice.Infra.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Invoice.Domain.Entity;

namespace Invoice.Infra.Data.Repository
{
    public class CustomerDapperRepository : ICustomeDapperRepository
    {
        private IConfiguration _configuration;
        private readonly SqlConnection _sqlConnection;

        public CustomerDapperRepository(IConfiguration configuration)
        {
                _configuration = configuration;
                _sqlConnection = new SqlConnection(_configuration.GetConnectionString("InvoiceConnection"));
        }

        public List<Customer> GetAll()
        {
            var query = "select * from Customers";
            return _sqlConnection.Query<Customer>(query).ToList();
        }
        public void Insert(Customer customer)
        {
            var query = @"Insert into Customers(Name,Address) VALUES(@Name,@Address)";
            _sqlConnection.Execute(query,new { Name = customer.Name, Address = customer.Address});
        }
        public Customer Get(int id)
        {
            var query = "select * from Customers where Id=" + id;
            return _sqlConnection.QueryFirstOrDefault<Customer>(query);
        }
        public void Delete(int id)
        {
            var query = "delete from Customers where Id ="+ id;
            _sqlConnection.Execute(query);
        }
        public void Update(Customer customer)
        {
            var query = "update Customers set Name="+ customer.Name +", Address="+ customer.Address + " where Id="+ customer.Id;
            _sqlConnection.Execute(query);
        }
    }
}
