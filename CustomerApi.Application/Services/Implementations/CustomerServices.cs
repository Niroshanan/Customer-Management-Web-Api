using CustomerApi.Application.Services.Interfaces;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Services.Implementations
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerServices(ICustomerRepository customerRepository) { 
            _customerRepository = customerRepository;
        }
        public Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return _customerRepository.GetCustomersAsync();
        }
        public Task<Customer> GetCustomerAsync(int id)
        {
            return _customerRepository.GetCustomerAsync(id);
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
