using TaskManagerApi.Enums;
using TaskManagerApi.Models;

public interface ILogRepository
{
    Task AddLogAsync(LogEntry action, string entity, int userId);
}
