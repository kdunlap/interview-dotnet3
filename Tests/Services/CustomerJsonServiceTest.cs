using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;
using Xunit;

namespace Tests.Services
{
    public class CustomerJsonServiceTest
    {
        private readonly List<Customer> _testCustomers = new List<Customer>()
        {
            new Customer() {Id = 1, Name = "Test Customer 1"},
            new Customer() {Id = 2, Name = "Test Customer 2"},
            new Customer() {Id = 3, Name = "Test Customer 3"},
            new Customer() {Id = 4, Name = "Test Customer 4"},
        };

        private CustomerJsonService BuildCustomerJsonService()
        {
            return new CustomerJsonService(_testCustomers);
        }
        
        [Fact]
        public async Task GetCustomers_ReturnsAListOfCustomers()
        {
            var service = BuildCustomerJsonService();
            var result = await service.GetCustomers();

            Assert.Equal(_testCustomers.Count, result.Count);
        }
        
        [Fact]
        public async Task GetCustomer_ReturnsNullIfNotFound()
        {
            var service = BuildCustomerJsonService();
            var customer = await service.GetCustomer(99);
            
            Assert.Null(customer);
        }
        
        [Fact]
        public async Task CreateCustomer_Adds_Customer_By_Id()
        {
            var service = BuildCustomerJsonService();
            var addedCustomer = await service.CreateCustomer(new Customer
            {
                Name = "Test Customer 5"
            });

            var getCustomer = await service.GetCustomer(addedCustomer.Id);
            
            Assert.Equal(5, addedCustomer.Id);
            Assert.Equal(addedCustomer.Name, getCustomer.Name);
        }
        
        [Fact]
        public async Task UpdateCustomer_ReturnsNullIfNotFound()
        {
            var service = BuildCustomerJsonService();
            var customerToUpdate = new Customer
            {
                Id = 999,
                Name = "Test Customer Not Found"
            };

            var updated = await service.UpdateCustomer(customerToUpdate);
            Assert.Null(updated);
        }
        
        [Fact]
        public async Task UpdateCustomer_UpdatesCustomer()
        {
            var service = BuildCustomerJsonService();
            var customerToUpdate = new Customer
            {
                Id = 1,
                Name = "Test Customer 1 Renamed"
            };

            var updated = await service.UpdateCustomer(customerToUpdate);
            
            Assert.Equal(customerToUpdate.Id, updated.Id);
            Assert.Equal(customerToUpdate.Name, updated.Name);
        }
        
        [Fact]
        public async Task DeleteCustomer_ReturnsNullIfNotFound()
        {
            var service = BuildCustomerJsonService();
            var deleted = await service.DeleteCustomer(999);
            
            Assert.Null(deleted);
        }
        
        [Fact]
        public async Task DeleteCustomer_DeletesCustomerIfSuccessful()
        {
            var service = BuildCustomerJsonService();
            await service.DeleteCustomer(1);

            var deleted = await service.GetCustomer(1);
            
            Assert.Null(deleted);
        }
    }
}