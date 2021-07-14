using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetCustomers();
        Task<CustomerDto> GetCustomer(long id);
        Task<CustomerDto> CreateCustomer(CustomerDto customer);
        Task<CustomerDto> UpdateCustomer(CustomerDto customer);
        Task<CustomerDto> DeleteCustomer(long id);
    }
}