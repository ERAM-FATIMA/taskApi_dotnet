using Microsoft.EntityFrameworkCore;
using TaskApi_DotNet.Data;
using TaskApi_DotNet.Models;

namespace TaskApi_DotNet.Services;

public class TaskService
{
   private readonly AppDbContext _context;

   public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllTasks()
    {
        return await _context.Tasks.ToListAsync();
    }
    public async Task<TaskItem?> GetTaskById(int id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }
    public async Task<TaskItem> CreateTask(TaskItem task)
    {
        _context.Tasks.Add(task);
       await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem?> UpdateTask(int id, TaskItem updatedTask)
    {
        var task =await  _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return null;
        }

        task.Title = updatedTask.Title;
        task.Status = updatedTask.Status;
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteTask(int id)
    {
        var task =await  _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}