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
            modelBuilder.Entity<FolderNoteLink>()
                .HasKey(fn => new { fn.NoteId, fn.FolderId });

            modelBuilder.Entity<FolderNoteLink>()
                .HasOne(fn => fn.Note)
                .WithMany(n => n.FolderNoteLinks)
                .HasForeignKey(fn => fn.NoteId);

            modelBuilder.Entity<FolderNoteLink>()
                .HasOne(fn => fn.Folder)
                .WithMany(f => f.FolderNoteLinks)
                .HasForeignKey(fn => fn.FolderId);
        }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<FolderModel> Folders { get; set; }
    }
}
