using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStoreAPI.Data.Models;
using GroceryStoreAPI.Data.Repositories;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Requests;

namespace GroceryStoreAPI.Services
{
    /// <inheritdoc />
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="mapper"></param>
        public CustomerService(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        
        /// <inheritdoc />
        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(c => _mapper.Map<CustomerDto>(c)).ToList();
        }
        
        /// <inheritdoc />
        public async Task<CustomerDto> GetCustomer(long id)
        {
            Customer customer = await _customerRepository.GetAsync(id);
            if (customer == null)
            {
                return null;
            }
            
            return _mapper.Map<CustomerDto>(customer);
        }
        
        /// <inheritdoc />
        public async Task<CustomerDto> CreateCustomer(CustomerCreateRequest customerRequest)
        {
            Customer customer = await _customerRepository.AddAsync(_mapper.Map<Customer>(customerRequest));
            return _mapper.Map<CustomerDto>(customer);
        }

        /// <inheritdoc />
        public async Task<CustomerDto> UpdateCustomer(CustomerUpdateRequest customerUpdateRequest)
        {
            Customer customer = await _customerRepository.UpdateAsync(_mapper.Map<Customer>(customerUpdateRequest));
            if (customer == null)
            {
                return null;
            }
            
            return _mapper.Map<CustomerDto>(customer);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCustomer(long id)
        {
            Customer customer = await _customerRepository.DeleteAsync(id);
            return (customer != null);
        }
    }
}