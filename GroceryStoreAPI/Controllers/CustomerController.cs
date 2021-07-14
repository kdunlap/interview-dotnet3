using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using GroceryStoreAPI.Services;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            return await _service.GetCustomers();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(long id)
        {
            var customer = await _service.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            var result = await _service.UpdateCustomer(customerDto);
            if (result == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerDto customerDto)
        {
            customerDto = await _service.CreateCustomer(customerDto);

            return CreatedAtAction("GetCustomer", new { id = customerDto.Id }, customerDto);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _service.DeleteCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
