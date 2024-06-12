using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Authorization.Models;
using Microsoft.AspNetCore.Identity;
namespace Authorization.Data
{
    public class AuthorizationContext : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
