using TaskManagerApi.Models;
using TaskManagerApi.Repositories;
using TaskManagerApi.Dtos.Common;
using TaskManagerApi.Dtos.Task;

namespace TaskManagerApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<TaskItem?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<TaskItem> CreateAsync(TaskItem task) => await _repo.CreateAsync(task);

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            return await _repo.UpdateAsync(task);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(existing);
            return true;
        }
        public async Task<PaginatedResult<TaskDto>> GetPagedAsync(string? search, int page, int pageSize, string? sort)
        {
            return await _repo.GetPagedDtoAsync(search, page, pageSize, sort);
        }
        public async Task<List<TaskStatsDto>> GetTaskStatsAsync(int days)
        {
            return await _repo.GetTaskStatsAsync(days);
        }

    }
}
