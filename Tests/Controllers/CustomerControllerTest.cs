using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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

            Assert.Equal(_testCustomers.Count, response.Value.Count());
        }
        
        [Fact]
        public async Task WhenCustomerDoesNotExist_GetCustomer_ShouldReturn404()
        {
            _mockCustomerService.Setup(x => x.GetCustomer(99))
                .Returns(Task.FromResult<CustomerDto>(null));
            
            var response = await _customerController.GetCustomer(99);

            Assert.IsType<NotFoundResult>(response.Result);
        }
        
        [Fact]
        public async Task WhenCustomerExits_GetCustomer_ShouldReturnTheCustomer()
        {
            var sampleCustomer = new CustomerDto() {Id = 1, Name = "Test Customer 1"};
            _mockCustomerService.Setup(x => x.GetCustomer(1))
                .Returns(Task.FromResult(sampleCustomer));
            
            var response = await _customerController.GetCustomer(1);
            CustomerDto customer = response.Value;
            
            Assert.Equal(sampleCustomer.Id, customer.Id);
            Assert.Equal(sampleCustomer.Name, customer.Name);
        }
        
        [Fact]
        public async Task WhenCustomerDoesNotExist_PutCustomer_ShouldReturn404()
        {
            _mockCustomerService.Setup(x => x.UpdateCustomer(It.IsAny<CustomerDto>()))
                .Returns(Task.FromResult<CustomerDto>(null));
            var sampleCustomer = new CustomerDto() {Id = 1, Name = "Test Customer 1"};

            IActionResult response = await _customerController.PutCustomer(sampleCustomer);
            
            Assert.IsType<NotFoundResult>(response);
        }
        
        [Fact]
        public async Task WhenAllIsNormal_CreateCustomer_ShouldReturnACreatedAtActionResult()
        {
            var sampleCustomer = new CustomerCreateDto() {Name = "Test Customer 1"};
            var sampleResponse = new CustomerDto() {Id = 1, Name = "Test Customer 1"};
            _mockCustomerService.Setup(x => x.CreateCustomer(It.IsAny<CustomerCreateDto>()))
                .Returns(Task.FromResult(sampleResponse));

            IActionResult response = await _customerController.PostCustomer(sampleCustomer);
            
            Assert.IsType<CreatedAtActionResult>(response);
        }
        
        [Fact]
        public async Task WhenCustomerDoesNotExist_DeleteCustomer_ShouldReturn404()
        {
            _mockCustomerService.Setup(x => x.DeleteCustomer(It.IsAny<long>()))
                .Returns(Task.FromResult(false));

            IActionResult response = await _customerController.DeleteCustomer(1);
            
            Assert.IsType<NotFoundResult>(response);
        }
    }
}