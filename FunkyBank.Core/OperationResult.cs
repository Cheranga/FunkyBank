namespace FunkyBank.Core
{
    public class OperationResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public static OperationResult Failure(string message)
        {
            return new OperationResult
            {
                Status = false,
                Message = message
            };
        }

        public static OperationResult Success()
        {
            return new OperationResult
            {
                Status = true
            };
        }
    }

    public class OperationResult<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T>
            {
                Status = true,
                Data = data
            };
        }

        public static OperationResult<T> Success(T data, string message)
        {
            return new OperationResult<T>
            {
                Status = true,
                Data = data,
               Message = message
            };
        }

        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>
            {
                Status = false,
                Message = message
            };
        }
    }
}
