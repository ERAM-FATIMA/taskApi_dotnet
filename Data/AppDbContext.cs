using Microsoft.EntityFrameworkCore;
using TaskApi_DotNet.Models;

namespace TaskApi_DotNet.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<TaskItem> Tasks {get ; set ;}
    public DbSet<User> Users {get; set;}
    
}