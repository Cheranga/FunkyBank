using FunkyBank.Core;

namespace FunkyBank.DTO.Requests
{
    public class GetCustomersRequest : IValidatable
    {
        public bool IsValid() => true;
    }
}