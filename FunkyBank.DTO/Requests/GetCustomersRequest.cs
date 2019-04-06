using FunkyBank.Core;

namespace FunkyBank.DTO.Requests
{
    public class GetCustomersRequest : IValidatable
    {
        public int Page { get; }
        public int Count { get; }

        public GetCustomersRequest(int page, int count)
        {
            Page = page;
            Count = count;
        }

        public bool IsValid() => Page > 0 && Count > 0;
    }
}