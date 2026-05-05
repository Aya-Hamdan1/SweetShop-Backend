using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model;

namespace SweetStore.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            // إذا في يوزرز لا تضيف
            if (context.Users.Any())
                return;

            var admin = new User
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Email = "ayahamdan235@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Aya@1234"),
                Role = "Admin"
            };

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}
