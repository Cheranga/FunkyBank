using FunkyBank.Core;

namespace FunkyBank.DTO.Requests
{
    public class GetSpecificCustomerRequest : IValidatable
    {
        public int Id { get; }

        public GetSpecificCustomerRequest(int id)
        {
            Id = id;
        }

        public bool IsValid() => Id > 0;
    }
}