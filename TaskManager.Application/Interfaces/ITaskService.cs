using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetAllAsync();
        Task<TaskResponseDto?> GetByIdAsync(int id);
        Task<TaskResponseDto> CreateAsync(TaskCreateDto dto);
        Task<TaskResponseDto> UpdateAsync(int id, TaskUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
