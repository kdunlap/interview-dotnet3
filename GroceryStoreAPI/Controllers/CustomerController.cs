using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Entities.Response;
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
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetCustomers()
        {
            return await _service.GetCustomers();
        }

        /// <summary>
        /// Get a specific Customer.
        /// </summary>
        /// <param name="id"></param>
        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomer(long id)
        {
            CustomerResponse customerUpdate = await _service.GetCustomer(id);

            if (customerUpdate == null)
            {
                return NotFound();
            }

            return customerUpdate;
        }

        /// <summary>
        /// Update a Customer.
        /// </summary>
        /// <param name="customerUpdateRequest"></param>
        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResponse>> PutCustomer(CustomerUpdateRequest customerUpdateRequest)
        {
            CustomerResponse result = await _service.UpdateCustomer(customerUpdateRequest);
            if (result == null)
            {
                return NotFound();
            }
            
            return result;
        }

        /// <summary>
        /// Create a Customer.
        /// </summary>
        /// <param name="customerRequest"></param>
        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> PostCustomer(CustomerCreateRequest customerRequest)
        {
            CustomerResponse newCustomerUpdate = await _service.CreateCustomer(customerRequest);

            return CreatedAtAction("GetCustomer", new { id = newCustomerUpdate.Id }, customerRequest);
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
