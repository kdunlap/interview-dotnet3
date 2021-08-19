using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using GroceryStoreAPI.Services;

namespace GroceryStoreAPI.Controllers
{
    /// <summary>
    /// Customer related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets a list of customers.
        /// </summary>
        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            return await _service.GetCustomers();
        }

        /// <summary>
        /// Get a specific Customer.
        /// </summary>
        /// <param name="id"></param>
        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(long id)
        {
            CustomerDto customer = await _service.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        /// <summary>
        /// Update a Customer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerDto"></param>
        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            CustomerDto result = await _service.UpdateCustomer(customerDto);
            if (result == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        /// <summary>
        /// Create a Customer.
        /// </summary>
        /// <param name="customerDto"></param>
        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerDto customerDto)
        {
            customerDto = await _service.CreateCustomer(customerDto);

            return CreatedAtAction("GetCustomer", new { id = customerDto.Id }, customerDto);
        }

        /// <summary>
        /// Delete a specific Customer.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _service.DeleteCustomer(id);
            if (!customer)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
