using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        public void SetStatus(TaskStatus newStatus)
        {
            Status = newStatus;

            if (newStatus == TaskStatus.Completed)
                CompletedAt = DateTime.Now;
            else
                CompletedAt = null;
        }
    }
}
