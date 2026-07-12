using TaskApi_DotNet.DTOs;
using TaskApi_DotNet.Models;

namespace TaskApi_DotNet.Interfaces;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllTasks(Guid userId);

    Task<TaskItem?> GetTaskById(int id, Guid userId);

    Task<TaskItem> CreateTask(TaskItem task);

    Task<TaskItem?> UpdateTask(int id, TaskItem updatedTask, Guid userId);

    Task<bool> DeleteTask(int id, Guid userId);   
}