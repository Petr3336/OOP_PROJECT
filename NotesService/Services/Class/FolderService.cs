using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;
using NotesService.Services.Interface;

namespace NotesService.Services.Class
{
    public class FolderService : IFolderService
    {
        private readonly NotesContext _context;

        public FolderService(NotesContext context)
        {
            _context = context;
        }

        public async Task<FolderModel> GetFolderWithNotesAsync(int folderId)
        {
            return await _context.Folders
                .Include(f => f.Notes)
                .FirstOrDefaultAsync(f => f.Id == folderId);
        }

        public async Task AddNoteToFolderAsync(int folderId, NoteModel note)
        {
            var folder = await _context.Folders.FindAsync(folderId);
            if (folder != null)
            {
                folder.Notes.Add(note);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveNoteFromFolderAsync(int folderId, int noteId)
        {
            var folder = await _context.Folders
                .Include(f => f.Notes)
                .FirstOrDefaultAsync(f => f.Id == folderId);

            if (folder != null)
            {
                var note = folder.Notes.FirstOrDefault(n => n.Id == noteId);
                if (note != null)
                {
                    folder.Notes.Remove(note);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

}
