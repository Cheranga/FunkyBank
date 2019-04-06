using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using FunkyBank.Core;
using FunkyBank.DataAccess.Core.Interfaces;
using FunkyBank.DataAccess.Core.Models;
using FunkyBank.DTO.Models;
using FunkyBank.DTO.Requests;
using FunkyBank.DTO.Responses;
using Microsoft.Extensions.Logging;

namespace FunkyBank.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<OperationResult<CreateCustomerResponse>> CreateCustomerAsync(CreateCustomerRequest request)
        {
            _logger.LogInformation($"Calling {nameof(CreateCustomerAsync)}");

            var isValid = request.Validate();
            if (!isValid)
            {
                _logger.LogError("Error: Invalid request");
                return OperationResult<CreateCustomerResponse>.Failure("Invalid request. Cannot create customer");
            }

            var operationResult = await _customerRepository.CreateCustomerAsync(new Customer
            {
                Name = request.Name,
                Address = request.Address
            }).ConfigureAwait(false);

            if (operationResult.Status)
            {
                _logger.LogInformation("Customer created successfully");
                return OperationResult<CreateCustomerResponse>.Success(new CreateCustomerResponse(new CustomerDisplayModel(request.Name, request.Address)));
            }

            _logger.LogInformation("Error: Cannot create customer");
            return OperationResult<CreateCustomerResponse>.Failure("Error: Cannot create customer");
        }

        public async Task<OperationResult<UpdateCustomerResponse>> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            _logger.LogInformation($"Calling {nameof(UpdateCustomerAsync)}");

            var isValid = request.Validate();
            if (!isValid)
            {
                _logger.LogError("Error: Invalid request, cannot update customer");
                return OperationResult<UpdateCustomerResponse>.Failure("Invalid request, cannot update customer");
            }

            var operationResult = await _customerRepository.UpdateCustomerAsync(new Customer
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address
            }).ConfigureAwait(false);

            if (operationResult.Status)
            {
                _logger.LogInformation("Customer updated successfully");
                return OperationResult<UpdateCustomerResponse>.Success(new UpdateCustomerResponse(new CustomerDisplayModel(request.Name, request.Address)));
            }

            _logger.LogError("Error: Cannot update customer");
            return OperationResult<UpdateCustomerResponse>.Failure("Cannot update customer");
        }

        public async Task<OperationResult<GetCustomersResponse>> GetCustomersAsync(GetCustomersRequest request)
        {
            var isValid = request.Validate();
            if (!isValid)
            {
                _logger.LogError("Error: Invalid request");
                return OperationResult<GetCustomersResponse>.Failure("Invalid request");
            }

            var operationResult = await _customerRepository.GetCustomersAsync().ConfigureAwait(false);

            if (operationResult.Status)
            {
                _logger.LogInformation("Retrieved customers successfully");

                var displayCustomers = operationResult.Data.Select(x=>new CustomerDisplayModel(x.Name, x.Address));
                return OperationResult<GetCustomersResponse>.Success(new GetCustomersResponse(displayCustomers));
            }

            _logger.LogError("Error: Cannot get customers");
            return OperationResult<GetCustomersResponse>.Failure("Cannot get customers");
        }

        public async Task<OperationResult<CustomerDisplayModel>> GetSpecificCustomerAsync(GetSpecificCustomerRequest request)
        {
            var isValid = request.Validate();

            if (!isValid)
            {
                _logger.LogError("Error: Invalid request");
                return OperationResult<CustomerDisplayModel>.Failure("Invalid request");
            }

            var operationResult = await _customerRepository.GetCustomerAsync(request.Id).ConfigureAwait(false);

            if (operationResult.Status)
            {
                _logger.LogInformation("Customer retrieved successfully");

                return OperationResult<CustomerDisplayModel>.Success(new CustomerDisplayModel(operationResult.Data.Name, operationResult.Data.Address));
            }

            _logger.LogError("Error: Cannot get specific customer");
            return OperationResult<CustomerDisplayModel>.Failure("Cannot get specific customer");
        }

        public async Task<OperationResult> DeleteCustomerAsync(DeleteCustomerRequest request)
        {
            var isValid = request.Validate();

            if (!isValid)
            {
                _logger.LogError("Error: Invalid request");

                return OperationResult.Failure("Invalid request");
            }

            var operationResult = await _customerRepository.DeleteCustomerAsync(request.Id).ConfigureAwait(false);

            if (operationResult.Status)
            {
                _logger.LogInformation("Customer deleted successfully");

                return OperationResult.Success();

            }

            _logger.LogError("Error: Cannot delete customer");

            return OperationResult.Failure("Cannot delete customer");
        }
    }
}