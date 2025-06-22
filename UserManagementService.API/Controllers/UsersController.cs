using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagementService.API.DTOs;
using UserManagementService.Application.DTOs;
using UserManagementService.Application.UsesCases;

namespace UserManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(RegisterUserUseCase registerUser, LoginUserUseCase loginUser)
        : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase = registerUser;
        private readonly LoginUserUseCase _loginUserUseCase = loginUser;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto dto)
        {
            var passwordHash = HashPassword(dto.Password);
            await _registerUserUseCase.ExecuteAsync(
                dto.Email,
                passwordHash,
                dto.RoleId,
                dto.FirstName,
                dto.LastName
            );
            return Ok("Usuario registrado.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var passwordHash = HashPassword(dto.Password);
            var token = await _loginUserUseCase.ExecuteAsync(dto.Email, passwordHash);
            if (token == null)
                return Unauthorized("Credenciales inválidas.");
            return Ok(new { token = token.Token });
        }

        private static string HashPassword(string password)
        {
            var hash = SHA512.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(hash);
        }
    }
}
