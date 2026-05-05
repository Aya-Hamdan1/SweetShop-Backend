using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SweetStore.Data;
using SweetStore.Model;
using SweetStore.ViewModels.User;

namespace SweetStore.Services
{
    public class AccountService
    {

        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        public AccountService(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<bool> RegisterUser(RegisterDto dto)
        {
            var exist = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if(exist != null)
            {
                throw new Exception("User already exist");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Role = "User",
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new Exception("Invalid email or password");
            }

            var token = _tokenService.CreateToken(user);

            return  token ;
        }
        public async Task<bool> UpdateUserRoleAsync(Guid userId, string Role)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            user.Role = Role;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
