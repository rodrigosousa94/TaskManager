using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Repositories;

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
                Status = dto.Status,
                CompletedAt = dto.CompletedAt
            };

            if (task.CompletedAt.HasValue && task.CompletedAt < task.CreatedAt)
                throw new ArgumentException("Completion date cannot be earlier than creation date.");

            await _repository.AddAsync(task);
            return new TaskResponseDto(task);
        }

        public async Task<TaskResponseDto> UpdateAsync(int id, TaskUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Task not found.");

            if (dto.CompletedAt.HasValue && dto.CompletedAt < existing.CreatedAt)
                throw new ArgumentException("Completion date cannot be earlier than creation date.");

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Status = dto.Status;
            existing.CompletedAt = dto.CompletedAt;

            await _repository.UpdateAsync(existing);
            return new TaskResponseDto(existing);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
