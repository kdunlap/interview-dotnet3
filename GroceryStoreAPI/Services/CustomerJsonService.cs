using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Helpers;
using GroceryStoreAPI.Models;
using Newtonsoft.Json;

namespace GroceryStoreAPI.Services
{
    public class CustomerJsonService : ICustomerService
    {
        private readonly Dictionary<long, Customer> _customers;

        public CustomerJsonService(List<Customer> customers)
        {
            _customers = new Dictionary<long, Customer>();
            SeedData(customers);
        }
        
        public CustomerJsonService()
        {
            _customers = new Dictionary<long, Customer>();
            var customers = JsonLoader.LoadCustomers();
            SeedData(customers);
        }

        private void SeedData(IEnumerable<Customer> customers)
        {
            foreach(var customer in customers)
            {
                _customers.Add(customer.Id, customer);
            }
        }
        
        public async Task<List<Customer>> GetCustomers()
        {
            return await Task.FromResult(_customers.Values.ToList());
        }
        
        public async Task<Customer> GetCustomer(long id)
        {
            Customer customer = null;
            if (_customers.ContainsKey(id))
            {
                customer = _customers[id];
            }
            return await Task.FromResult<Customer>(customer);
        }
        
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            // simulate auto increment
            var keys = _customers.Keys.ToList();
            keys.Sort();
            var newId = keys.Last() + 1;

            customer.Id = newId;
            _customers.Add(newId, customer);
            return await Task.FromResult(customer);
        }
        
        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            if (!_customers.ContainsKey(customer.Id))
            {
                return await Task.FromResult<Customer>(null);
            }
            
            _customers[customer.Id] = customer;
            return await Task.FromResult(customer);
        }
        
        public async Task<Customer> DeleteCustomer(long id)
        {
            if (!_customers.ContainsKey(id))
            {
                return await Task.FromResult<Customer>(null);
            }

            var existingCustomer = _customers[id];
            _customers.Remove(id);
            return await Task.FromResult(existingCustomer);
        }
    }
}