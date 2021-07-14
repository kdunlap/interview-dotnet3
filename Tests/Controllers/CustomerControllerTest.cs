using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class CustomerControllerTest
    {
        private Mock<ICustomerService> _mockCustomerService;

        private readonly List<CustomerDto> _testCustomers = new List<CustomerDto>()
        {
            new CustomerDto() {Name = "Test Customer 1"},
            new CustomerDto() {Name = "Test Customer 2"},
            new CustomerDto() {Name = "Test Customer 3"},
            new CustomerDto() {Name = "Test Customer 4"},
        };

        private CustomerController BuildCustomerController()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            
            return new CustomerController(_mockCustomerService.Object);
        }

        [Fact]
        public async Task GetCustomers_ReturnsAListOfCustomers()
        {
            var customerController = BuildCustomerController();
            _mockCustomerService.Setup(x => x.GetCustomers())
                .Returns(Task.FromResult(_testCustomers));
            
            var response = await customerController.GetCustomers();

            Assert.Equal(_testCustomers.Count, response.Value.Count());
        }
        
        [Fact]
        public async Task GetCustomer_Returns404IfNotFound()
        {
            var customerController = BuildCustomerController();
            _mockCustomerService.Setup(x => x.GetCustomer(99))
                .Returns(Task.FromResult<CustomerDto>(null));
            
            var response = await customerController.GetCustomer(99);

            Assert.IsType<NotFoundResult>(response.Result);
        }
        
        [Fact]
        public async Task GetCustomer_ReturnsCustomerIfFound()
        {
            var customerController = BuildCustomerController();
            var sampleCustomer = new CustomerDto() {Id = 1, Name = "Test Customer 1"};
            _mockCustomerService.Setup(x => x.GetCustomer(1))
                .Returns(Task.FromResult<CustomerDto>(sampleCustomer));
            
            var response = await customerController.GetCustomer(1);
            var customer = response.Value;
            
            Assert.Equal(sampleCustomer.Id, customer.Id);
            Assert.Equal(sampleCustomer.Name, customer.Name);
        }
        
        [Fact]
        public async Task PutCustomer_ReturnsBadRequestWhenParametersAreInvalid()
        {
            var customerController = BuildCustomerController();
            var sampleCustomer = new CustomerDto() {Id = 2, Name = "Test Customer 1"};

            var response = await customerController.PutCustomer(1, sampleCustomer);
            
            Assert.IsType<BadRequestResult>(response);
        }
        
        [Fact]
        public async Task PutCustomer_Returns404IfNotFound()
        {
            var customerController = BuildCustomerController();
            _mockCustomerService.Setup(x => x.UpdateCustomer(It.IsAny<CustomerDto>()))
                .Returns(Task.FromResult<CustomerDto>(null));
            var sampleCustomer = new CustomerDto() {Id = 1, Name = "Test Customer 1"};

            var response = await customerController.PutCustomer(1, sampleCustomer);
            
            Assert.IsType<NotFoundResult>(response);
        }
        
        [Fact]
        public async Task CreateCustomer_ReturnsCreatedAtActionResultIfValid()
        {
            var customerController = BuildCustomerController();
            var sampleCustomer = new CustomerDto() {Id = 1, Name = "Test Customer 1"};
            _mockCustomerService.Setup(x => x.CreateCustomer(It.IsAny<CustomerDto>()))
                .Returns(Task.FromResult<CustomerDto>(sampleCustomer));

            var response = await customerController.PostCustomer(sampleCustomer);
            
            Assert.IsType<CreatedAtActionResult>(response);
        }
        
        [Fact]
        public async Task DeleteCustomer_Returns404IfNotFound()
        {
            var customerController = BuildCustomerController();
            _mockCustomerService.Setup(x => x.DeleteCustomer(It.IsAny<long>()))
                .Returns(Task.FromResult<CustomerDto>(null));

            var response = await customerController.DeleteCustomer(1);
            
            Assert.IsType<NotFoundResult>(response);
        }
    }
}