using TaskManagement.Application.DTOs;
using TaskManagement.Application.Requests;
using TaskManagement.Domain.Values.Objects;

namespace TaskManagement.Application.Interfaces;

public interface ITaskAppService
{
    Task<TaskDto> GetByIdAsync(long id);
    Task<PagedResult<TaskDto>> GetAllAsync(int page = 1, int size = 20, string? search = null);
    Task<TaskDto> CreateAsync(CreateTaskRequest request);
    Task<TaskDto> UpdateAsync(UpdateTaskRequest request);
    Task DeleteAsync(long id);
}
