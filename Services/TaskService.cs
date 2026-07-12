using Microsoft.EntityFrameworkCore;
using TaskApi_DotNet.Data;
using TaskApi_DotNet.Models;
using TaskApi_DotNet.Interfaces;

namespace TaskApi_DotNet.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllTasks(Guid userId)
    {
        return await _context.Tasks
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskById(int id, Guid userId)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }

    public async Task<TaskItem> CreateTask(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem?> UpdateTask(int id, TaskItem updatedTask, Guid userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
        {
            return null;
        }

        task.Title = updatedTask.Title;
        task.Status = updatedTask.Status;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteTask(int id, Guid userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return true;
    }
}