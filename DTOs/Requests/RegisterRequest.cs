using System.ComponentModel.DataAnnotations;

namespace TaskApi_DotNet.DTOs;

public class RegisterRequestDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = "";
}