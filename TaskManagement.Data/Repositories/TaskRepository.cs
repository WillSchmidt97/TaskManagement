using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Data.Repositories
{
    public class TaskRepository(AppDbContext context) : BaseRepository<AppDbContext, TaskItem>(context), ITaskRepository { }
}
