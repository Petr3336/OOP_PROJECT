using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesService.Models;

namespace NotesService.Data
{
    // Контекст базы данных
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<FolderModel> Folders { get; set; }
        public DbSet<NoteListModel> NoteLists { get; set; }
        public DbSet<NoteModel> Notes { get; set; }

    }
}
