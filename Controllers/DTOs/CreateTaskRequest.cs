namespace TaskApi_DotNet.DTOs;

public class TaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}