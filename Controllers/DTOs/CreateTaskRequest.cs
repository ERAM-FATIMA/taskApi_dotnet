namespace TaskApi_DotNet.DTOs;
using TaskApi_DotNet.Models;
using System.ComponentModel.DataAnnotations;

public class TaskRequest
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public TaskStatus Status { get; set; } 
}