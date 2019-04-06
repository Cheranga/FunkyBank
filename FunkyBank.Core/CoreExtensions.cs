namespace FunkyBank.Core
{
    public static class CoreExtensions
    {
        public static bool Validate(this IValidatable validatable)
        {
            var status = validatable?.IsValid();

            return status ?? false;
        }
    }
}