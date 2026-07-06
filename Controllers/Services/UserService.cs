using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskApi_DotNet.Data;
using TaskApi_DotNet.DTOs;
using TaskApi_DotNet.Models;

namespace TaskApi_DotNet.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Register(RegisterRequestDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return false;

        User user = new()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(user, dto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Login(LoginRequestDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null)
            return false;

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password);

        return result == PasswordVerificationResult.Success;
    }
}