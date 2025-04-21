namespace TaskManagerApi.Dtos.Task;
public class TaskStatsDto
{
    public string DueDate { get; set; } = string.Empty; // yyyy-MM-dd
    public int Count { get; set; }
}
