
using Asp.Versioning;
using CustomerApi.Application.DTOs;
using CustomerApi.Application.Services.Interfaces;
using CustomerApi.Application.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Web.Controllers.v1
{
    [EnableCors]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        //get all customers endpoint
        [Authorize(Roles =SD.ADMIN_ROLE)]
        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerServices.GetCustomersAsync();
                if (customers.Count() == 0)
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

        //get customer by id endpoint
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

        //update customerEditDto using id endpoint
        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(CustomerEditDto customerEditDto)
        {
            try
            {
                var customerToUpdate = await _customerServices.GetCustomerAsync(customerEditDto.CustomerId);
                if (customerToUpdate == null)
                {
                    return NotFound($"Error :Customer with id = {customerEditDto.CustomerId} not found");
                }
                bool res = await _customerServices.UpdateCustomerAsync(customerEditDto.CustomerId, customerEditDto);
                if (!res)
                {
                    return BadRequest($"Error :Customer with id = {customerEditDto.CustomerId} not updated");
                }
                return Ok(new ResponseDto { Status = "Success" , Message = $"Customer with Id = {customerEditDto.CustomerId} updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating data. {ex.Message}");
            }
        }

        //getdistance of a customer using id, latitude and longitude as parameters endpoint
        [HttpGet]
        [Route("GetDistance/{id}")]
        public async Task<IActionResult> GetDistance(int id, [FromQuery] CordinateDto cordinateDto)
        {
            try
            {
                var customer = await _customerServices.GetCustomerAsync(id);
                if (customer == null)
                {
                    return NotFound($"Error :Customer with id = {id} not found");
                }
                double distance = await _customerServices.GetDistanceAsync(id, cordinateDto);
                return Ok(new ResponseDto { Status = "Success", Message = $"Distance of customer with id = {id} from given latitude and longitude is {distance:F2} km" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database. {ex.Message} ");
            }
        }

        //Search customers for a given string endpoint
        [HttpGet]
        [Route("SearchCustomer/{searchString}")]
        public async Task<IActionResult> SearchCustomer(string searchString)
        {
            try
            {

                var searchedCustomers = await _customerServices.SearchCustomerAsync(searchString);
                if (searchedCustomers.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(searchedCustomers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database. {ex.Message}");
            }
        }


        //Get customer list grouped by Zip code
        [HttpGet]
        [Route("GetCustomerListByZipCode")]
        public async Task<IActionResult> GetCustomerListByZipCode()
        {
            try
            {
                var customers = await _customerServices.GetCustomerListByZipCodeAsync();
                if (customers.Count() == 0)
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
    }
}
