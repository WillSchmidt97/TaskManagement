using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController(ITaskAppService service) : ControllerBase
    {
        private readonly ITaskAppService _service = service;

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int size = 20, string? search = null)
            => Ok(await _service.GetAllAsync(page, size, search));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(CreateTaskRequest request)
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateTaskRequest request)
        {
            var result = await _service.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
