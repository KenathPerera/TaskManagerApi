using TaskManagerApi.Models;
using TaskManagerApi.Dtos.Common;
using TaskManagerApi.Dtos.Task;

namespace TaskManagerApi.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);

        Task<PaginatedResult<TaskDto>> GetPagedAsync(string? search, int page, int pageSize, string? sort);
        Task<List<TaskStatsDto>> GetTaskStatsAsync(int days);

    }
}
