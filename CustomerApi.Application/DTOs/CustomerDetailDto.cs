using CustomerApi.Data.Entities;

namespace CustomerApi.Application.DTOs
{
    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
    }
}
