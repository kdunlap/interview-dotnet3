using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStoreAPI.Data;
using GroceryStoreAPI.Data.Repositories;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;
using Moq;
using Xunit;

namespace Tests.Services
{
    public class CustomerServiceTest
    {
        private readonly Mock<IRepository<Customer>> _mockRepository = new Mock<IRepository<Customer>>();
        private readonly IMapper _mapper;

        private readonly List<Customer> _testCustomers = new List<Customer>()
        {
            new Customer() {Id = 1, Name = "Test Customer 1"},
            new Customer() {Id = 2, Name = "Test Customer 2"},
            new Customer() {Id = 3, Name = "Test Customer 3"}
        };

        public CustomerServiceTest()
        {
            // todo - should be able to do this once for all tests
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
        }
        
        private CustomerService BuildCustomerService()
        {
            var service = new CustomerService(_mockRepository.Object, _mapper);
            
            return service;
        }
        
        [Fact]
        public async Task GetCustomers_ReturnsAListOfCustomers()
        {
            _mockRepository.Setup(r => r.GetAll()).ReturnsAsync(_testCustomers);
            var service = BuildCustomerService();
            var result = await service.GetCustomers();

            Assert.Equal(3, result.Count);
        }
        
        [Fact]
        public async Task GetCustomer_ReturnsNullIfNotFound()
        {
            _mockRepository.Setup(r => r.Get(99)).ReturnsAsync(null as Customer);
            var service = BuildCustomerService();
            var customer = await service.GetCustomer(99);
            
            Assert.Null(customer);
        }
        
        [Fact]
        public async Task CreateCustomer_ReturnsAddedCustomer()
        {
            var customerToAdd = new Customer() {Id = 99, Name = "Test Customer 4"};
            _mockRepository.Setup(r => r.Add(It.IsAny<Customer>())).ReturnsAsync(customerToAdd);
            var service = BuildCustomerService();
            var addedCustomer = await service.CreateCustomer(new CustomerDto{Name = "Test Customer 4"});
            
            Assert.Equal(customerToAdd.Id, addedCustomer.Id);
            Assert.Equal(customerToAdd.Name, addedCustomer.Name);
        }
        
        [Fact]
        public async Task UpdateCustomer_ReturnsNullIfNotFound()
        {
            _mockRepository.Setup(r => r.Update(It.IsAny<Customer>())).ReturnsAsync(null as Customer);
            var service = BuildCustomerService();
            var customerToUpdate = new CustomerDto
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
            _mockRepository.Setup(r => r.Update(It.IsAny<Customer>())).ReturnsAsync(new Customer(){Id=1, Name="Customer 1 Renamed"});
            var service = BuildCustomerService();
            var customerToUpdate = new CustomerDto
            {
                Id = 1,
                Name = "Customer 1 Renamed"
            };

            var updated = await service.UpdateCustomer(customerToUpdate);
            
            Assert.Equal(customerToUpdate.Id, updated.Id);
            Assert.Equal(customerToUpdate.Name, updated.Name);
        }
        
        [Fact]
        public async Task DeleteCustomer_ReturnsNullIfNotFound()
        {
            _mockRepository.Setup(r => r.Delete(999)).ReturnsAsync(null as Customer);
            var service = BuildCustomerService();
            var deleted = await service.DeleteCustomer(999);
            
            Assert.Null(deleted);
        }
        
        [Fact]
        public async Task DeleteCustomer_ReturnsDeletedCustomer()
        {
            var customerToDelete = new Customer() {Id = 1, Name = "Customer 1"};
            _mockRepository.Setup(r => r.Delete(1)).ReturnsAsync(customerToDelete);
            var service = BuildCustomerService();
            var deletedCustomer = await service.DeleteCustomer(1);

            Assert.Equal(customerToDelete.Id, deletedCustomer.Id);
            Assert.Equal(customerToDelete.Name, deletedCustomer.Name);
        }
    }
}