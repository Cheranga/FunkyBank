using FunkyBank.Core;

namespace FunkyBank.DTO.Requests
{
    public class UpdateCustomerRequest : IValidatable
    {
        public int Id { get; }
        public string Name { get; }
        public string Address { get; }

        public UpdateCustomerRequest(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public bool IsValid() => Id > 0 && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Address);
    }
}