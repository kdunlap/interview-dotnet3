using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStoreAPI.Data;
using GroceryStoreAPI.Data.Repositories;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Entities.Response;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;
using Moq;
using Xunit;

namespace Tests.Services
{
    public class CustomerServiceTest
    {
        private readonly Mock<IRepository<Customer>> _mockRepository = new();

        private readonly List<Customer> _testCustomers = new()
        {
            new Customer {Id = 1, Name = "Test Customer 1"},
            new Customer {Id = 2, Name = "Test Customer 2"},
            new Customer {Id = 3, Name = "Test Customer 3"}
        };

        private readonly ICustomerService _service; 

        public CustomerServiceTest()
        {
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);
            
            _service = new CustomerService(_mockRepository.Object, mapper);
        }
        
        [Fact]
        public async Task WhenAllIsNormal_GetCustomers_ShouldReturnAListOfCustomers()
        {
            _mockRepository.Setup(r => r.GetAll()).ReturnsAsync(_testCustomers);
            var result = await _service.GetCustomers();

            Assert.Equal(3, result.Count);
        }
        
        [Fact]
        public async Task WhenCustomerDoesNotExist_GetCustomer_ShouldReturnNull()
        {
            _mockRepository.Setup(r => r.Get(99)).ReturnsAsync(null as Customer);
            CustomerResponse customerUpdate = await _service.GetCustomer(99);
            
            Assert.Null(customerUpdate);
        }
        
        [Fact]
        public async Task WhenAllIsNormal_CreateCustomer_ShouldReturnTheCustomer()
        {
            var customerToAdd = new Customer() {Id = 99, Name = "Test Customer 4"};
            _mockRepository.Setup(r => r.Add(It.IsAny<Customer>())).ReturnsAsync(customerToAdd);
            CustomerResponse addedCustomerUpdate = await _service.CreateCustomer(new CustomerCreateRequest{Name = "Test Customer 4"});
            
            Assert.Equal(customerToAdd.Id, addedCustomerUpdate.Id);
            Assert.Equal(customerToAdd.Name, addedCustomerUpdate.Name);
        }
        
        [Fact]
        public async Task WhenCustomerDoesNotExist_UpdateCustomer_ShouldReturnNull()
        {
            _mockRepository.Setup(r => r.Update(It.IsAny<Customer>())).ReturnsAsync(null as Customer);
            var customerToUpdate = new CustomerUpdateRequest
            {
                Id = 999,
                Name = "Test Customer Not Found"
            };

            CustomerResponse updated = await _service.UpdateCustomer(customerToUpdate);
            Assert.Null(updated);
        }
        
        [Fact]
        public async Task WhenAllIsNormal_UpdateCustomer_ShouldReturnTheUpdatedCustomer()
        {
            _mockRepository.Setup(r => r.Update(It.IsAny<Customer>())).ReturnsAsync(new Customer(){Id=1, Name="Customer 1 Renamed"});
            var customerToUpdate = new CustomerUpdateRequest
            {
                Id = 1,
                Name = "Customer 1 Renamed"
            };

            CustomerResponse updated = await _service.UpdateCustomer(customerToUpdate);
            
            Assert.Equal(customerToUpdate.Id, updated.Id);
            Assert.Equal(customerToUpdate.Name, updated.Name);
        }
        
        [Fact]
        public async Task WhenCustomerDoesNotExist_DeleteCustomer_ShouldReturnsFalse()
        {
            _mockRepository.Setup(r => r.Delete(999)).ReturnsAsync(null as Customer);
            var result = await _service.DeleteCustomer(999);
            
            Assert.False(result);
        }
        
        [Fact]
        public async Task WhenAllIsNormal_DeleteCustomer_ShouldReturnTrue()
        {
            var customerToDelete = new Customer() {Id = 1, Name = "Customer 1"};
            _mockRepository.Setup(r => r.Delete(1)).ReturnsAsync(customerToDelete);
            var result = await _service.DeleteCustomer(1);

            Assert.True(result);
        }
    }
}