using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GroceryStoreAPI.Services
{
    public class CustomerDbService : ICustomerService
    {
        private readonly CustomerContext _context;

        public CustomerDbService(CustomerContext context)
        {
            _context = context;
        }
        
        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        
        public async Task<Customer> GetCustomer(long id)
        {
            return await _context.Customers.FindAsync(id);
        }
        
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        
        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }
            
            return customer;
        }
        
        public async Task<Customer> DeleteCustomer(long id)
        {
            
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }
            
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
            return customer;
        }
    }
}