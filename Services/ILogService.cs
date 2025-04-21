
using TaskManagerApi.Enums;
namespace TaskManagerApi.Services
{
    public interface ILogService
    {
        Task LogAsync(LogEntry action, string entity, int userId);
    }
}