using CustomerApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.DTOs
{
    public class CustomerGroupDto
    {
        public int ZipCode { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
