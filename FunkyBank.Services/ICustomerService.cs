using System.Collections.Generic;
using System.Threading.Tasks;
using FunkyBank.Core;
using FunkyBank.DTO.Models;
using FunkyBank.DTO.Requests;
using FunkyBank.DTO.Responses;

namespace FunkyBank.Services
{
    public interface ICustomerService
    {
        Task<OperationResult<CreateCustomerResponse>> CreateCustomerAsync(CreateCustomerRequest request);
        Task<OperationResult<UpdateCustomerResponse>> UpdateCustomerAsync(UpdateCustomerRequest request);
        Task<OperationResult<GetCustomersResponse>> GetCustomersAsync(GetCustomersRequest request);
        Task<OperationResult<CustomerDisplayModel>> GetSpecificCustomerAsync(GetSpecificCustomerRequest request);
        Task<OperationResult> DeleteCustomerAsync(DeleteCustomerRequest request);
    }
}