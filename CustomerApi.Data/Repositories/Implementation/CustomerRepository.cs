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
            List<Customer> customers = _db.Customers.Include(c => c.Address).ToList();
            return Task.FromResult<IEnumerable<Customer>>(customers);
        }

        public Task<Customer> GetCustomerAsync(int id)
        {
            Customer customer = _db.Customers.Find(id);
            return Task.FromResult<Customer>(customer);
        }

        public Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _db.Customers.Update(customer);
            return Task.FromResult<bool>(_db.SaveChanges() > 0);
        }
        public Task<bool> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IGrouping<int, Customer>>> GetCustomerListByZipCodeAsync()
        {
            var customersGrouped = _db.Customers
                .Include(c => c.Address)
                .GroupBy(c => c.Address.Zipcode)
                .AsEnumerable();

            return Task.FromResult<IEnumerable<IGrouping<int, Customer>>>(customersGrouped);
        }
    }
}
