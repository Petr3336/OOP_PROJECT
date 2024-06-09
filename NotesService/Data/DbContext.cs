using Microsoft.EntityFrameworkCore;
using NotesService.Models;

namespace NotesService.Data
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options)
                    : base(options)
            {
                Database.EnsureCreated();

            }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<FolderModel>()
        //        .HasMany(f => f.Notes)
        //        .WithOne(n => n.Folder)
        //        .HasForeignKey(n => n.FolderId);
        // фейк }

        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<FolderModel> Folders { get; set; }
    }
}
