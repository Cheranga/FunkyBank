using System.Collections.Generic;
using System.Threading.Tasks;
using FunkyBank.Core;
using FunkyBank.DataAccess.Core.Models;

namespace FunkyBank.DataAccess.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<OperationResult<List<Customer>>> GetCustomersAsync();
        Task<OperationResult<Customer>> GetCustomerAsync(int customerId);
        Task<OperationResult<Customer>> CreateCustomerAsync(Customer customer);
        Task<OperationResult<Customer>> UpdateCustomerAsync(Customer customer);
        Task<OperationResult> DeleteCustomerAsync(int customerId);
    }
}