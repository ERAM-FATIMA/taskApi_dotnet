using System.ComponentModel.DataAnnotations;
namespace TaskApi_DotNet.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; } = "";

        public TaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow ;

        public DateTime? UpdatedAt { get; set; }
    }

}
