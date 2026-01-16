namespace TaskManagement.Application.Requests
{
    public interface IRequest
    {
        void EnsureIsValid();
    }
}
