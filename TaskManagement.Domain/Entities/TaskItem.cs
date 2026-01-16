namespace TaskManagement.Domain.Entities;

public class TaskItem : Entity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = "TODO";
}
