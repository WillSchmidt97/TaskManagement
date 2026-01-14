namespace TaskManagement.Domain.Exceptions
{
    public class BusinessException(string? message = null, Exception? innerException = null) : Exception(message, innerException)
    {
    }
}
