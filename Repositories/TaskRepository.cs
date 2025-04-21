using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Dtos.Task;
using TaskManagerApi.Dtos.Common;

namespace TaskManagerApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public TaskRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync() => await _context.TaskItems.ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(int id) => await _context.TaskItems.FindAsync(id);

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            var existingTask = await _context.TaskItems.FindAsync(task.TaskId);
            if (existingTask == null)
                return false;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.IsCompleted = task.IsCompleted;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(TaskItem task)
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResult<TaskDto>> GetPagedDtoAsync(string? search, int page, int pageSize, string? sort)
        {
            var query = _context.TaskItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t =>
                    t.Title.Contains(search) || t.Description.Contains(search));
            }

            query = sort switch
            {
                "title_asc" => query.OrderBy(t => t.Title),
                "title_desc" => query.OrderByDescending(t => t.Title),
                "dueDate_asc" => query.OrderBy(t => t.DueDate),
                _ => query.OrderByDescending(t => t.DueDate)
            };

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginatedResult<TaskDto> { Items = items, TotalCount = total };
        }

        public async Task<List<TaskStatsDto>> GetTaskStatsAsync(int days)
        {
            var today = DateTime.UtcNow.Date;
            var endDate = today.AddDays(days);

            return await _context.TaskItems
                .Where(t => t.DueDate >= today && t.DueDate <= endDate)
                .GroupBy(t => t.DueDate.Date)
                .OrderBy(g => g.Key)
                .ProjectTo<TaskStatsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

    }
}
