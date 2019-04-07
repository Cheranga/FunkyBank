using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dapper;
using FunkyBank.Core;
using FunkyBank.DataAccess.Core;
using FunkyBank.DataAccess.Core.Interfaces;
using FunkyBank.DataAccess.Core.Models;
using FunkyBank.DataAccess.Dapper.Repositories;
using FunkyBank.DTO.Requests;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FunkyBank.Services.Unit.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void Customer_Repository_Is_Mandatory_To_Customer_Service()
        {
            Assert.Throws<ArgumentNullException>(()=> new CustomerService(null, Mock.Of<ILogger<CustomerService>>() ));
        }

        [Fact]
        public void Logger_Is_Mandatory_To_Customer_Service()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerService(Mock.Of<ICustomerRepository>(), null));
        }

        [Fact]
        public async Task Cannot_Create_Customer_With_Null_Request()
        {
            //
            // Arrange
            //
            var service = new CustomerService(Mock.Of<ICustomerRepository>(), Mock.Of<ILogger<CustomerService>>());
            //
            // Act
            //
            var operationResult = await service.CreateCustomerAsync(null);
            //
            // Assert
            //
            Assert.False(operationResult.Status);
        }

        [Fact]
        public async Task Cannot_Create_Customer_With_Invalid_Request()
        {
            //
            // Arrange
            //
            var service = new CustomerService(Mock.Of<ICustomerRepository>(), Mock.Of<ILogger<CustomerService>>());
            //
            // Act
            //
            var operationResultwithoutName = await service.CreateCustomerAsync(new CreateCustomerRequest(null, "some address"));
            var operationResultwithoutAddress = await service.CreateCustomerAsync(new CreateCustomerRequest("some name", null));

            //
            // Assert
            //
            Assert.False(operationResultwithoutName.Status);
            Assert.False(operationResultwithoutAddress.Status);
        }

        [Fact]
        public async Task For_Valid_Request_Customer_Will_Be_Created()
        {
            //
            // Arrange
            //
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(x => x.CreateCustomerAsync(It.IsAny<Customer>())).ReturnsAsync(() => OperationResult<Customer>.Success(new Customer
            {
                Id = 1,
                Name = "some name",
                Address = "some address"
            }));
            var service = new CustomerService(repository.Object, Mock.Of<ILogger<CustomerService>>());
            //
            // Act
            //
            var operation = await service.CreateCustomerAsync(new CreateCustomerRequest("some name", "some address"));
            //
            // Assert
            //
            Assert.True(operation.Status);
        }

    }
}
