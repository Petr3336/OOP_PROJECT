
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesService.Models;
using System.Reflection.Emit;


public class ApplicationDbContext : IdentityDbContext<UserModel>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
            Database.EnsureCreated();
    }

    public DbSet<NoteListModel> NoteLists { get; set; }
    public DbSet<NoteModel> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // NoteList - Note (1:Many)
        builder.Entity<NoteListModel>()
            .HasMany(nl => nl.Notes)
            .WithOne(n => n.NoteList)
            .HasForeignKey(n => n.NoteListId)
            .OnDelete(DeleteBehavior.Cascade);

        // Note - User (Many:1) 
        builder.Entity<NoteModel>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // NoteList - User (Many:1) 
        builder.Entity<NoteListModel>()
            .HasOne(nl => nl.User)
            .WithMany(u => u.NoteLists)
            .HasForeignKey(nl => nl.UserId) 
            .OnDelete(DeleteBehavior.Restrict);
    }

}