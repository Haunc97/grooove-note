using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace PersonalNotesAPI.Data
{
    public class SeedDatabase
    {
        public static async Task CreateUserRoleAsync(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var adminRoleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!adminRoleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //Adding User Role
            var userRoleCheck = await RoleManager.RoleExistsAsync("User");
            if (!userRoleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("User"));
            }
            ApplicationUser user01 = await UserManager.FindByNameAsync("annv@gmail.com");
            await UserManager.AddToRoleAsync(user01, "Admin");
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<PersonalNotesDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //ApplicationUser user01 = new ApplicationUser()
            //{
            //    Email = "user01@gmail.com",
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    UserName = "user01@gmail.com",
            //    FullName = "User 01"
            //};
            ApplicationUser user02 = new ApplicationUser()
            {
                Email = "user02@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "user02@gmail.com",
                FullName = "User 02"
            };
            ApplicationUser user03 = new ApplicationUser()
            {
                Email = "user03@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "user03@gmail.com",
                FullName = "User 03"
            };
            //userManager.CreateAsync(user01, "User01@123");
            userManager.CreateAsync(user02, "User02@123");
            userManager.CreateAsync(user03, "User03@123");
        }
    }
}
