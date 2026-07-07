using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskApi_DotNet.DTOs;
using TaskApi_DotNet.Models;
using TaskApi_DotNet.Services;

namespace TaskApi_DotNet.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }



    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetAllTasks(GetUserId());
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _taskService.GetTaskById(id , GetUserId());

        if (task == null)
            return NotFound("Task not found");

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var task = new TaskItem
        {
            Title = request.Title,
            Status = request.Status,
            UserId = Guid.Parse(userId)
        };

        var createdTask = await _taskService.CreateTask(task);

        return Created($"/api/tasks/{createdTask.Id}", createdTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskRequest request)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Status = request.Status
        };

        var updatedTask = await _taskService.UpdateTask(id, task , GetUserId());

        if (updatedTask == null)
            return NotFound("Task not found");

        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deleted = await _taskService.DeleteTask(id , GetUserId());

        if (!deleted)
            return NotFound("Task not found");

        return Ok("Task Deleted Successfully");
    }
}