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
        //    modelBuilder.Entity<FolderModel>()
        //        .HasOne(f=>f.NoteList)
        //        .WithOne()
        //        .HasForeignKey("FolderIdls")
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<NoteListModel>()
        //        .HasMany(l=>l.Notes)
        //        .WithOne()
        //        .HasForeignKey("NoteListId")
        //        .OnDelete(DeleteBehavior.Cascade);

        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<FolderModel> Folders { get; set; }
        public DbSet<NoteListModel> NoteLists { get; set; }
    }
}
