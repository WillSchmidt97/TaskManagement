namespace TaskManagement.Domain.Exceptions
{
    public class NotFoundException(string? message = null) : BusinessException(message) { }
}
