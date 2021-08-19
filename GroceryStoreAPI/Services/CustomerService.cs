using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStoreAPI.Data.Repositories;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Services
{
    /// <inheritdoc />
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        
        /// <inheritdoc />
        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _customerRepository.GetAll();

            return customers.Select(c => _mapper.Map<CustomerDto>(c)).ToList();
        }
        
        /// <inheritdoc />
        public async Task<CustomerDto> GetCustomer(long id)
        {
            var customer = await _customerRepository.Get(id);
            if (customer == null)
            {
                return null;
            }
            
            return _mapper.Map<CustomerDto>(customer);
        }
        
        /// <inheritdoc />
        public async Task<CustomerDto> CreateCustomer(CustomerCreateDto customerDto)
        {
            var customer = await _customerRepository.Add(_mapper.Map<Customer>(customerDto));
            return _mapper.Map<CustomerDto>(customer);
        }

        /// <inheritdoc />
        public async Task<CustomerDto> UpdateCustomer(CustomerDto customerDto)
        {
            var customer = await _customerRepository.Update(_mapper.Map<Customer>(customerDto));
            if (customer == null)
            {
                return null;
            }
            
            return _mapper.Map<CustomerDto>(customer);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCustomer(long id)
        {
            Customer customer = await _customerRepository.Delete(id);
            return (customer != null);
        }
    }
}