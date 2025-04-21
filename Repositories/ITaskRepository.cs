using TaskManagerApi.Models;
using TaskManagerApi.Dtos.Common;
using TaskManagerApi.Dtos.Task;

namespace TaskManagerApi.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);

        Task<PaginatedResult<TaskDto>> GetPagedDtoAsync(string? search, int page, int pageSize, string? sort);
        Task<List<TaskStatsDto>> GetTaskStatsAsync(int days);

    }
}
