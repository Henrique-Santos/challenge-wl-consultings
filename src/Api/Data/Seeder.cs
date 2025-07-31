using Microsoft.AspNetCore.Identity;

namespace Api.Data;

public static class Seeder
{
    public static List<ApplicationUser> GetUsers()
    {
        return
        [
            new ApplicationUser
            {
                Id = "fd20f81f-6920-4f67-b42a-75dd48a811fe",
                UserName = "john_doe",
                Email = "john@gmail.com",
                EmailConfirmed = true,
                Password = "a6LZWob^u$NPEu4"
            },
            new ApplicationUser
            {
                Id = "7b116278-ea36-473c-94d6-8179e624aa4f",
                UserName = "mary_doe",
                Email = "mary@gmail.com",
                EmailConfirmed = true,
                Password = "jwt*m#U@kR*c2RS"
            }
        ];
    }
}

public class ApplicationUser : IdentityUser
{
    public string Password { get; set; } = string.Empty;
}