using TaskManagerApi.Enums;
public class AuditLog
{
    public int Id { get; set; }
    public LogEntry Action { get; set; }
    public string Entity { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
