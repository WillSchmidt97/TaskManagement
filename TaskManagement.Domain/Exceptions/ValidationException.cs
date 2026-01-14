using System.Runtime.CompilerServices;

namespace TaskManagement.Domain.Exceptions
{
    public class ValidationException(string? message = null) : BusinessException(message)
    {
        public static T ThrowIfNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value is null || (value is string s && string.IsNullOrWhiteSpace(s)))
            {
                throw new ValidationException($"Necessário informar a propriedade '{paramName}'!");
            }

            return value;
        }
    }
}
