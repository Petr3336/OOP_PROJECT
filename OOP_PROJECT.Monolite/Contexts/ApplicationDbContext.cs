
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesService.Models;
using System.Reflection.Emit;


public class ApplicationDbContext : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
            Database.EnsureCreated();
    }

    public DbSet<FolderModel> Folders { get; set; }
    public DbSet<NoteListModel> NoteLists { get; set; }
    public DbSet<NoteModel> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Настройки для FolderModel
        builder.Entity<FolderModel>()
            .HasMany(f => f.SubFolders)
            .WithOne(p => p.ParentFolder)
            .HasForeignKey(p => p.ParentFolderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FolderModel>()
            .HasMany(f => f.NoteLists)
            .WithOne(nl => nl.Folder)
            .HasForeignKey(nl => nl.FolderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Настройки для NoteListModel
        builder.Entity<NoteListModel>()
            .HasMany(nl => nl.Notes)
            .WithOne(n => n.NoteList)
            .HasForeignKey(n => n.NoteListId)
            .OnDelete(DeleteBehavior.Cascade);

        // Настройки для UserModel
        builder.Entity<UserModel>()
            .HasOne(u => u.RootFolder)
            .WithOne()
            .HasForeignKey<FolderModel>(f => f.User)
            .OnDelete(DeleteBehavior.Restrict);

        // Настройки для NoteModel
        builder.Entity<NoteModel>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Дополнительные настройки для NoteListModel
        builder.Entity<NoteListModel>()
            .HasOne(nl => nl.User)
            .WithMany()
            .HasForeignKey(nl => nl.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<NoteListModel>()
            .HasOne(nl => nl.Folder)
            .WithMany(f => f.NoteLists)
            .HasForeignKey(nl => nl.FolderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<NoteModel>()
        .HasOne(n => n.NoteList)
        .WithMany(nl => nl.Notes)
        .HasForeignKey(n => n.NoteListId)
        .OnDelete(DeleteBehavior.Restrict); // Запрет на удаление родительского NoteListModel

        // Настройки для NoteListModel
        builder.Entity<NoteListModel>()
            .HasOne(nl => nl.Folder)
            .WithMany(f => f.NoteLists)
            .HasForeignKey(nl => nl.FolderId)
            .OnDelete(DeleteBehavior.Restrict); // Запрет на удаление родительского FolderModel

        // Настройки для FolderModel
        builder.Entity<FolderModel>()
            .HasOne(f => f.ParentFolder)
            .WithMany(p => p.SubFolders)
            .HasForeignKey(f => f.ParentFolderId)
            .OnDelete(DeleteBehavior.Restrict); // Запрет на удаление родительской папки

        builder.Entity<FolderModel>().HasIndex(f => f.UserId).IsUnique(false);
    }

}