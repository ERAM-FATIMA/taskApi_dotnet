using Microsoft.AspNetCore.Mvc;
using TaskApi_DotNet.Models;
using TaskApi_DotNet.Services;
using TaskApi_DotNet.DTOs;
using System.Data;

namespace TaskApi_DotNet.Controllers;


[ApiController]

[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }
    
    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(_taskService.GetAllTasks());
    }


    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        var task = _taskService.GetTaskById(id);

        if (task == null)
        {
            return NotFound("Task not found");
        }

        return Ok(task);
    }


    [HttpPost]
    public IActionResult CreateTask(TaskRequest request)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Status = request.Status
        };
         var createdTask = _taskService.CreateTask(task);
        return Created($"/api/tasks/{createdTask.Id}" , createdTask);
    }

    
    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TaskRequest request)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Status = request.Status
        };

        var updatedTask = _taskService.UpdateTask(id, task);
        if(UpdateTask == null)
        {
            return NotFound("Task not found");
        }

        return Ok(updatedTask);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deleted = await _taskService.DeleteTask(id);

    if(!deleted)
    {
        return NotFound("Task not found");
    }

    return Ok("Task Deleted Successfully");
    }

}