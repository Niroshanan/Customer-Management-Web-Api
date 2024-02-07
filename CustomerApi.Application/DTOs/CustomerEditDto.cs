using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Application.DTOs
{
    public class CustomerEditDto
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
