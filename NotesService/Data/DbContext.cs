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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FolderModel>()
                .HasMany(f => f.Notes)
                .WithOne()
                .HasForeignKey("FolderId")
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<FolderModel> Folders { get; set; }
    }
}
