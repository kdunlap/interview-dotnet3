using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Services
{
    /// <summary>
    /// Interface for operations on CustomerDto
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Gets a list of customers
        /// </summary>
        Task<List<CustomerDto>> GetCustomers();
        
        /// <summary>
        /// Get a single customer
        /// </summary>
        Task<CustomerDto> GetCustomer(long id);
        
        /// <summary>
        /// Create a new customer
        /// </summary>
        Task<CustomerDto> CreateCustomer(CustomerCreateDto customer);
        
        /// <summary>
        /// Update a single customer
        /// </summary>
        Task<CustomerDto> UpdateCustomer(CustomerDto customer);
        
        /// <summary>
        /// Delete a single customer
        /// </summary>
        Task<bool> DeleteCustomer(long id);
    }
}