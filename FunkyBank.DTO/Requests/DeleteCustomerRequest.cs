using FunkyBank.Core;

namespace FunkyBank.DTO.Requests
{
    public class DeleteCustomerRequest : IValidatable
    {
        public int Id { get; }

        public DeleteCustomerRequest(int id)
        {
            Id = id;
        }

        public bool IsValid() => Id > 0;
    }
}