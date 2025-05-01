using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserLoginForm.Models;

namespace UserLoginForm.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                // Look for any users.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                var user = new ApplicationUser
                {
                    UserName = "testuser@example.com",
                    Email = "testuser@example.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "Test@1234");

                var admin = new ApplicationUser
                {
                    UserName = "d.shekudko@knu.ua",
                    Email = "123123",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Shedi@1234");
            }
        }
    }
}
