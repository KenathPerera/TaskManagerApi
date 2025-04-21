using TaskManagerApi.Enums;
using TaskManagerApi.Models;
using TaskManagerApi.Data;

public class LogRepository : ILogRepository
{
    private readonly AppDbContext _context;

    public LogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddLogAsync(LogEntry action, string entity, int userId)
    {
        var log = new AuditLog
        {
            Action = action,
            Entity = entity,
            UserId = userId,
            Timestamp = DateTime.UtcNow
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}
