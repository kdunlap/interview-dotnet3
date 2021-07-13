using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(long id);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<Customer> DeleteCustomer(long id);
    }
}