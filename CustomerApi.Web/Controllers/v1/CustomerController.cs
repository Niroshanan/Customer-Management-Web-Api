using CustomerApi.Application.DTOs;
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
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database. {ex.Message}");
            }
        }

        //get customer by id
        [HttpGet]
        [Route("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerServices.GetCustomerAsync(id);
                if (customer == null)
                {
                    return NotFound($"Error :Customer with id = {id} not found");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database. {ex.Message} ");
            }
        }

        //update customerEditDto using id
        [HttpPut]
        [Route("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerEditDto customerEditDto)
        {
            try
            {
                var customerToUpdate = await _customerServices.GetCustomerAsync(id);
                if (customerToUpdate == null)
                {
                    return NotFound($"Error :Customer with id = {id} not found");
                }
                bool res = await _customerServices.UpdateCustomerAsync(id, customerEditDto);
                if (!res)
                {
                    return BadRequest($"Error :Customer with id = {id} not updated");
                }
                return Ok($"Customer with Id = {id} updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating data. {ex.Message}");
            }
        }


    }
}
