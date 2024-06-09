using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;
using NotesService.Services.Interface;

namespace NotesService.Services.Class
{
    public class NoteService : INoteService
    {
        private readonly NotesContext _context;

        public NoteService(NotesContext context)
        {
            _context = context;
        }

        public async Task<FolderModel> GetFolderWithNotesAsync(int folderId)
        {
            return await _context.Folders
             .Include(f => f.FolderNoteLinks)
             .ThenInclude(fn => fn.Note)
             .FirstOrDefaultAsync(f => f.Id == folderId);
        }

        public async Task ConnectFolderToNoteAsync(int folderId, int noteId)
        {
            var folder = await _context.Folders.FindAsync(folderId);
            var note = await _context.Notes.FindAsync(noteId);

            if (folder != null && note != null)
            {
                if (folder.FolderNoteLinks == null)
                    folder.FolderNoteLinks = new List<FolderNoteLink>();

                folder.FolderNoteLinks.Add(new FolderNoteLink { Folder = folder, Note = note });
                await _context.SaveChangesAsync();
            }
        }

        public async Task DisconnectFolderFromNoteAsync(int folderId, int noteId)
        {
            var folder = await _context.Folders
                .Include(f => f.FolderNoteLinks)
                .FirstOrDefaultAsync(f => f.Id == folderId);

            if (folder != null)
            {
                var link = folder.FolderNoteLinks.FirstOrDefault(fn => fn.NoteId == noteId);
                if (link != null)
                {
                    folder.FolderNoteLinks.Remove(link);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

}
