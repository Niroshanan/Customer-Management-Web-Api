﻿using CustomerApi.Application.DTOs;
using CustomerApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerDetailDto>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<bool> UpdateCustomerAsync(int id,CustomerEditDto customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<double> GetDistanceAsync(int id, CordinateDto cordinateDto);
        Task<IEnumerable<Customer>> SearchCustomerAsync(String searchStr);

        Task<IEnumerable<CustomerGroupDto>> GetCustomerListByZipCodeAsync();

    }
}
