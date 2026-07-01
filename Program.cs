using TaskApi_DotNet.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

List<TaskItem> tasks = new()
{
    new TaskItem
    {
        Id = 1,
        Title = "Learn .NET",
        Status = "To-Do"
    },
    new TaskItem
    {
        Id = 2,
        Title = "Build Task API",
        Status = "Done"
    }
};

app.MapGet("/tasks", () =>
{
    return tasks;
});


app.MapGet("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task == null)
    {
        return Results.NotFound("Task not found");
    }

    return Results.Ok(task);
});


app.MapPost("/tasks", (TaskItem task) =>
{
    task.Id = tasks.Count + 1;

    tasks.Add(task);

    return Results.Created($"/tasks/{task.Id}", task);
});

app.Run();