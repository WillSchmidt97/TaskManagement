using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.Requests
{
    public class UpdateTaskRequest : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public void EnsureIsValid()
        {
            if (Id <= 0)
                throw new Exception("Invalid id");

            if (string.IsNullOrEmpty(Title))
                throw new ValidationException("The title cannot be empty!");
        }
    }
}
