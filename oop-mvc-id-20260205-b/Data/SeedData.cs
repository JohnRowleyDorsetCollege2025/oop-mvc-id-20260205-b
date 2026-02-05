using Microsoft.AspNetCore.Identity;

namespace oop_mvc_id_20260205_b.Data;

public static class AppRoles
{

    public const string Admin = "Admin";
    public const string Member = "Member";
}

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = AppRoles.Admin },
                new IdentityRole { Name = AppRoles.Member }
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(new IdentityRole(role.Name));
                }
            }

            string adminUserEmail = "admin@movies.com";
            string adminPassword = "Letmein01*";
            string adminUserName = "admin@movies.com";

            if (await userManager.FindByEmailAsync(adminUserEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminUserName,
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
                }
            }

            string memberUserEmail = "member@movies.com";
            string memberPassword = "Letmein01*";
            string memberUserName = "member@movies.com";

            if (await userManager.FindByEmailAsync(memberUserEmail) == null)
            {
                var memberUser = new IdentityUser
                {
                    UserName = memberUserName,
                    Email = memberUserEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(memberUser, memberPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(memberUser, AppRoles.Member);
                }
            }


        }

    }
}