using AutoMapper;
using CustomerApi.Application.DTOs;
using CustomerApi.Application.Helpers;
using CustomerApi.Application.Services.Interfaces;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Repositories.Interfaces;

namespace CustomerApi.Application.Services.Implementations
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerServices(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CustomerDetailDto>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDetailDto>>(customers);
        }
        public Task<Customer> GetCustomerAsync(int id)
        {
            return _customerRepository.GetCustomerAsync(id);
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerEditDto customerEditDto)
        {
            Customer customerToUpdate = _customerRepository.GetCustomerAsync(id).Result;
            if (customerEditDto.Name != null)
            {
                customerToUpdate.Name = customerEditDto.Name;
            }
            if (customerEditDto.Email != null)
            {
                customerToUpdate.Email = customerEditDto.Email;
            }
            if (customerEditDto.Phone != null)
            {
                customerToUpdate.Phone = customerEditDto.Phone;
            }
            return await _customerRepository.UpdateCustomerAsync(customerToUpdate);

        }
        public Task<bool> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<double> GetDistanceAsync(int id, CordinateDto cordinateDto)
        {
            Customer customer = _customerRepository.GetCustomerAsync(id).Result;
            double distance = DistanceCalculator.CalculateDistance(customer, cordinateDto);
            return distance;
        }

        public async Task<IEnumerable<Customer>> SearchCustomerAsync(string searchStr)
        {
            List<Customer> searchedCustomers = _customerRepository.GetCustomersAsync().Result
                .Where(c =>
                c.EyeColor.Contains(searchStr) ||
                c.Name.Contains(searchStr) ||
                c.Gender.Contains(searchStr) ||
                c.Company.Contains(searchStr) ||
                c.Email.Contains(searchStr) ||
                c.Phone.Contains(searchStr) ||
                c.Address.Street.Contains(searchStr) ||
                c.Address.City.Contains(searchStr) ||
                c.Address.State.Contains(searchStr) ||
                c.Address.Zipcode.ToString().Contains(searchStr) ||
                c.About.Contains(searchStr) ||
                c.Tags.Contains(searchStr)
                )
                .ToList();
            return searchedCustomers;
        }

        public async Task<IEnumerable<CustomerGroupDto>> GetCustomerListByZipCodeAsync()
        {
            var customersListByZipCode = await _customerRepository.GetCustomerListByZipCodeAsync();
            var result = customersListByZipCode.Select(group => new CustomerGroupDto
            {
                ZipCode = group.Key,
                Customers = group.ToList()
            });
            return result;

        }
    }
}
