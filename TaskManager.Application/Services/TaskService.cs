using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllAsync()
        {
            var tasks = await _repository.GetAllAsync();
            return tasks.Select(t => new TaskResponseDto(t));
        }

        public async Task<TaskResponseDto?> GetByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            return task is null ? null : new TaskResponseDto(task);
        }

        public async Task<TaskResponseDto> CreateAsync(TaskCreateDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status
            };

            await _repository.AddAsync(task);
            return new TaskResponseDto(task);
        }

        public async Task<TaskResponseDto> UpdateAsync(int id, TaskUpdateDto dto)
        {
            var existingTask = await _repository.GetByIdAsync(id);

            if (existingTask == null)
                throw new KeyNotFoundException("Task not found.");

            existingTask.Title = dto.Title;
            existingTask.Description = dto.Description;
            existingTask.SetStatus(dto.Status);

            await _repository.UpdateAsync(existingTask);

            return new TaskResponseDto(existingTask);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
