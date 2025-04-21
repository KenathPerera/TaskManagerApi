namespace TaskManagerApi.Dtos.Task
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

    }
}