using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Entities.Response;
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
        Task<List<CustomerResponse>> GetCustomers();
        
        /// <summary>
        /// Get a single customer
        /// </summary>
        Task<CustomerResponse> GetCustomer(long id);
        
        /// <summary>
        /// Create a new customer
        /// </summary>
        Task<CustomerResponse> CreateCustomer(CustomerCreateRequest customer);
        
        /// <summary>
        /// Update a single customer
        /// </summary>
        Task<CustomerResponse> UpdateCustomer(CustomerUpdateRequest customerUpdate);
        
        /// <summary>
        /// Delete a single customer
        /// </summary>
        Task<bool> DeleteCustomer(long id);
    }
}