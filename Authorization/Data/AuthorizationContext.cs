using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Authorization.Models;
namespace Authorization.Data
{
    public class AuthorizationContext : IdentityDbContext<UserModel>
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
