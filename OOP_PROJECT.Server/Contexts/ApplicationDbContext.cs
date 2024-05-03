using Microsoft.EntityFrameworkCore;
using OOP_PROJECT.Server.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProductModel> Products { get; set; }
}
