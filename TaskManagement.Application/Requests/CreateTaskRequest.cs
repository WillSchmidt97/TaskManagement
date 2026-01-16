using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.Requests
{
    public class CreateTaskRequest : IRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public void EnsureIsValid()
        {
            if (string.IsNullOrEmpty(Title))
                throw new ValidationException("The title cannot be empty!");
        }
    }
}
