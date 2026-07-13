using TaskApi_DotNet.DTOs;
using TaskApi_DotNet.DTOs.Requests;
using TaskApi_DotNet.Models;

namespace TaskApi_DotNet.Interfaces;

public interface IUserService
{
    Task<bool> Register(RegisterRequestDto dto);

    Task<User?> Login(LoginRequestDto dto);
}