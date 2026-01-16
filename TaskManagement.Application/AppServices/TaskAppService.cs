using Mapster;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Domain.Values.Objects;
using ValidationException = TaskManagement.Domain.Exceptions.ValidationException;

namespace TaskManagement.Application.Services;

public class TaskAppService(ITaskRepository repository) : ITaskAppService
{
    private readonly ITaskRepository _repository = repository;

    public async Task<TaskDto> GetByIdAsync(long id)
    {
        if (id <= 0)
            throw new ValidationException("Invalid id");

        var entity = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException($"User not found with ID '{id}'");

        var dto = entity.Adapt<TaskDto>();
        return dto;
    }

    public async Task<PagedResult<TaskDto>> GetAllAsync(int page = 1, int size = 20, string? search = null)
    {
        var paged = await _repository.GetAllPagedAsync(page, size);
        return paged.ToPagedResult(x => x.Adapt<TaskDto>());
    }

    public async Task<TaskDto> CreateAsync(CreateTaskRequest request)
    {
        request.EnsureIsValid();

        // Checks if there is no task that already exists with this title.
        var taskByName = await _repository.GetFirstOrDefaultAsync(x => x.Title == request.Title.ToLower().Trim());
        if (taskByName != null)
            throw new ValidationException($"There is already a task registered with the name '{taskByName.Title}'!");

        // Setting entity with the entry values.
        var entity = request.Adapt<TaskItem>();
        entity.Title = request.Title.Trim();
        entity.Description = request.Description;
        entity.Status = "ok";

        var dto = entity.Adapt<TaskDto>();
        return dto;
    }

    public async Task<TaskDto> UpdateAsync(UpdateTaskRequest request)
    {
        request.EnsureIsValid();

        var entity = await _repository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Task not found with the ID '{request.Id}'");

        entity.Title = request.Title.Trim();
        entity.Description = request.Description;

        await _repository.UpdateAsync(entity);

        return entity.Adapt<TaskDto>();
    }

    public async Task DeleteAsync(long id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null) return;

        await _repository.DeleteAsync(task);
    }
}
