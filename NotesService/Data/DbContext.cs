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

        public DbSet<NoteModel> Folderts { get; set; }
        public DbSet<FolderModel> Folders { get; set; }
    }
}
