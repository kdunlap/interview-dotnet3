using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Requests;
using GroceryStoreAPI.Responses;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets a list of customers.
        /// </summary>
        // GET: api/Customer
        [HttpGet]
        public async Task<ResponseInfo<IEnumerable<CustomerDto>>> GetCustomers()
        {
            return await _service.GetCustomers();
        }

        /// <summary>
        /// Get a specific Customer.
        /// </summary>
        /// <param name="id"></param>
        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ResponseInfo<CustomerDto>> GetCustomer(long id)
        {
            CustomerDto customerUpdate = await _service.GetCustomer(id);
            
            // todo - 404 here
            
            return customerUpdate;
        }

        /// <summary>
        /// Update a Customer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerUpdateRequest"></param>
        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<ResponseInfo<CustomerDto>> PutCustomer(long id, CustomerUpdateRequest customerUpdateRequest)
        {
            customerUpdateRequest.Id = id;
            CustomerDto result = await _service.UpdateCustomer(customerUpdateRequest);
            
            return result;
        }

        /// <summary>
        /// Create a Customer.
        /// </summary>
        /// <param name="customerRequest"></param>
        // POST: api/Customer
        [HttpPost]
        public async Task<ResponseInfo<CustomerDto>> PostCustomer(CustomerCreateRequest customerRequest)
        {
            CustomerDto newCustomerUpdate = await _service.CreateCustomer(customerRequest);

            return newCustomerUpdate;
        }

        /// <summary>
        /// Delete a specific Customer.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ResponseInfo<bool>> DeleteCustomer(long id)
        {
            var result = await _service.DeleteCustomer(id);
            
            return result;
        }
    }
}
