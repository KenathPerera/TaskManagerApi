using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        //for concurrrency conflicts 
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
