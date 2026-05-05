using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model;
using SweetStore.Services;
using SweetStore.ViewModels.User;

namespace SweetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _accountService.RegisterUser(dto);
            return Ok(new { Success = true, Message = "User created" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
           var token = await _accountService.Login(dto);

            return Ok(new { Success = true, Message = "Login Successfully", Data = token });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> UpdateRole(Guid id, string Role)
        {
            await _accountService.UpdateUserRoleAsync(id, Role);

            return Ok(new { Success = true, Message = "User role updated" });
        }
    }
}
