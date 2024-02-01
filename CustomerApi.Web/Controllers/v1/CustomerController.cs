using CustomerApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Web.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        //get all customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customerServices.GetCustomersAsync();
                if (customers == null)
                {
                    return NotFound();
                }
                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        //get customer by id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerServices.GetCustomerAsync(id);
                if (customer == null)
                {
                    return NotFound($"Customer with id = {id} not found");
                }
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


    }
}
