using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Dtos.Task;

namespace TaskManagerApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskItemsController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                IsCompleted = taskDto.IsCompleted
            };

            var result = await _service.CreateAsync(task);
            return CreatedAtAction(nameof(Get), new { id = result.TaskId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem updatedTask)
        {
            if (id != updatedTask.TaskId)
                return BadRequest("Task ID mismatch");

            var result = await _service.UpdateAsync(updatedTask);
            if (!result)
                return NotFound("Task not found");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _service.DeleteAsync(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sort = "dueDate_desc")
        {
            var result = await _service.GetPagedAsync(search, page, pageSize, sort);
            return Ok(result);
        }

        [HttpGet("chart")]
        public async Task<IActionResult> GetStatsByDueDate([FromQuery] int days = 7)
        {
            var stats = await _service.GetTaskStatsAsync(days);
            return Ok(stats);
        }

    }
}
