using TaskApi_DotNet.Models;

namespace TaskApi_DotNet.Services;

public class TaskService
{
    private List<TaskItem> tasks = new()
    {
        new TaskItem
        {
            Id = 1,
            Title = "Learn .NET",
            Status = Models.TaskStatus.Done
        },
        new TaskItem
        {
            Id = 2,
            Title = "Build Task API",
            Status = Models.TaskStatus.ToDo
        }
    };

    public List<TaskItem> GetAllTasks()
    {
        return tasks;
    }
    public TaskItem? GetTaskById(int id)
    {
        return tasks.FirstOrDefault(t => t.Id == id);
    }
    public TaskItem CreateTask(TaskItem task)
    {
        task.Id = tasks.Count + 1;
        tasks.Add(task);
        return task;
    }

    public TaskItem? UpdateTask(int id, TaskItem updatedTask)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return null;
        }

        task.Title = updatedTask.Title;
        task.Status = updatedTask.Status;

        return task;
    }

    public bool DeleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return false;
        }

        tasks.Remove(task);
        return true;
    }
}