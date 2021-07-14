using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Data.Repositories;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _customerRepository.GetAll();

            return customers.Select(CustomerToDto).ToList();
        }
        
        public async Task<CustomerDto> GetCustomer(long id)
        {
            var customer = await _customerRepository.Get(id);
            if (customer == null)
            {
                return null;
            }
            
            return CustomerToDto(customer);
        }
        
        public async Task<CustomerDto> CreateCustomer(CustomerDto customerDto)
        {
            var customer = await _customerRepository.Add(DtoToCustomer(customerDto));
            return CustomerToDto(customer);
        }
        
        public async Task<CustomerDto> UpdateCustomer(CustomerDto customerDto)
        {
            var customer = await _customerRepository.Update(DtoToCustomer(customerDto));
            if (customer == null)
            {
                return null;
            }
            
            return CustomerToDto(customer);
        }
        
        public async Task<CustomerDto> DeleteCustomer(long id)
        {
            var customer = await _customerRepository.Delete(id);
            if (customer == null)
            {
                return null;
            }
            
            return CustomerToDto(customer);
        }
        
        
        // todo - use automapper instead
        private static Customer DtoToCustomer(CustomerDto customerDto) =>
            new Customer
            {
                Id = customerDto.Id,
                Name = customerDto.Name,
            };
        
        private static CustomerDto CustomerToDto(Customer customer) =>
            new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
            };
    }
}