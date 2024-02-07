using CustomerApi.Data.Entities;

namespace CustomerApi.Application.DTOs
{
    public class CustomerGroupDto
    {
        public int ZipCode { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
