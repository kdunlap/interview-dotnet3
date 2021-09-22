using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Requests;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomerService> _mockCustomerService = new();

        private readonly List<CustomerDto> _testCustomers = new()
        {
            new CustomerDto {Name = "Test Customer 1"},
            new CustomerDto {Name = "Test Customer 2"},
            new CustomerDto {Name = "Test Customer 3"},
            new CustomerDto {Name = "Test Customer 4"},
        };

        private readonly CustomerController _customerController;

        public CustomerControllerTest()
        {
            _customerController = new CustomerController(_mockCustomerService.Object);
        }


        [Fact]
        public async Task WhenAllIsNormal_GetCustomers_ShouldReturnAListOfCustomers()
        {
            _mockCustomerService.Setup(x => x.GetCustomers())
                .Returns(Task.FromResult(_testCustomers));
            
            var response = await _customerController.GetCustomers();

            Assert.Equal(_testCustomers.Count, response.Data.Count());
        }
        
        [Fact (Skip = "Needs to be implemented")]
        public async Task WhenCustomerDoesNotExist_GetCustomer_ShouldReturn404()
        {
            _mockCustomerService.Setup(x => x.GetCustomer(99))
                .Returns(Task.FromResult<CustomerDto>(null));
            
            var response = await _customerController.GetCustomer(99);

            Assert.IsType<NotFoundResult>(response.Data);
        }
        
        [Fact]
        public async Task WhenCustomerExits_GetCustomer_ShouldReturnTheCustomer()
        {
            var sampleCustomer = new CustomerDto() {Id = 1, Name = "Test Customer 1"};
            _mockCustomerService.Setup(x => x.GetCustomer(1))
                .Returns(Task.FromResult(sampleCustomer));
            
            var response = await _customerController.GetCustomer(1);
            
            Assert.Equal(sampleCustomer.Id, response.Data.Id);
            Assert.Equal(sampleCustomer.Name, response.Data.Name);
        }
        
        
        [Fact (Skip = "Needs to be implemented")]
        public async Task WhenCustomerDoesNotExist_PutCustomer_ShouldReturn404()
        {
            _mockCustomerService.Setup(x => x.UpdateCustomer(It.IsAny<CustomerUpdateRequest>()))
                .Returns(Task.FromResult<CustomerDto>(null));
            var sampleCustomer = new CustomerUpdateRequest() {Name = "Test Customer 1"};
        
            var response = await _customerController.PutCustomer(1, sampleCustomer);
            
            Assert.IsType<NotFoundResult>(response);
        }
        
        [Fact]
        public async Task WhenAllIsNormal_CreateCustomer_ShouldReturnTheCustomer()
        {
            var sampleCustomer = new CustomerCreateRequest() {Name = "Test Customer 1"};
            var sampleDto = new CustomerDto() {Id = 1, Name = "Test Customer 1"};
            _mockCustomerService.Setup(x => x.CreateCustomer(It.IsAny<CustomerCreateRequest>()))
                .Returns(Task.FromResult(sampleDto));

            var response = await _customerController.PostCustomer(sampleCustomer);
            
            Assert.Equal(response.Data.Name, sampleCustomer.Name);
        }
        
        [Fact (Skip = "Needs to be implemented")]
        public async Task WhenCustomerDoesNotExist_DeleteCustomer_ShouldReturn404()
        {
            _mockCustomerService.Setup(x => x.DeleteCustomer(It.IsAny<long>()))
                .Returns(Task.FromResult(false));

            var response = await _customerController.DeleteCustomer(1);
            
            Assert.IsType<NotFoundResult>(response);
        }
    }
}