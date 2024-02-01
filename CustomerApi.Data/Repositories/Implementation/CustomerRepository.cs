using CustomerApi.Data.Data;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Data.Repositories.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            List<Customer> customers = _db.Customers.ToList();
            return Task.FromResult<IEnumerable<Customer>>(customers);
        }

        public Task<Customer> GetCustomerAsync(int id)
        {
            Customer customer = _db.Customers.Find(id);
            return Task.FromResult<Customer>(customer);
        }

        public Task<bool> UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
