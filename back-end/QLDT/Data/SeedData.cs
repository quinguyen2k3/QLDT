using Microsoft.AspNetCore.Identity;
using QLDT.Models;
using System;

namespace QLDT.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>();

                var admin = new User
                {
                    Username = "admin",
                    Name = "Administrator"
         
                };

                admin.Password = passwordHasher.HashPassword(admin, "admin123");

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }

    }
}
