using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs
{
    public class TaskCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Domain.Enums.TaskStatus Status { get; set; } = Domain.Enums.TaskStatus.Pending;

        public DateTime? CompletedAt { get; set; }
    }
}
