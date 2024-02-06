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

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            List<Customer> customers = await _db.Customers.Include(c => c.Address).ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            Customer customer = await _db.Customers.Include(c => c.Address).FirstOrDefaultAsync(c => c.CustomerId == id);
            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _db.Customers.Update(customer);
            return await Task.FromResult<bool>(await  _db.SaveChangesAsync() > 0);
        }
        public async Task<IEnumerable<IGrouping<int, Customer>>> GetCustomerListByZipCodeAsync()
        {
            var customersGrouped =  await _db.Customers
                .Include(c => c.Address)
                .GroupBy(c => c.Address.Zipcode)
                .ToListAsync(); ;

            return customersGrouped;
        }

    }
}
