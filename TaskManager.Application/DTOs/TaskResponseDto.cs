using TaskManager.Domain.Entities;

namespace TaskManager.Application.DTOs
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Status { get; set; } = string.Empty;

        public TaskResponseDto(TaskItem task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            CreatedAt = task.CreatedAt;
            CompletedAt = task.CompletedAt;
            Status = task.Status.ToString();
        }
    }
}
