using Microsoft.EntityFrameworkCore;
using NotesService.Models;
using NotesService.Models.RequestModels;
using OOP_PROJECT.Monolite.Service.IService;

namespace OOP_PROJECT.Monolite.Service
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;

        public NoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NoteModel>> GetNotesByNoteListAsync(int noteListId, string userId)
        {
            return await _context.Notes
                .Where(n => n.NoteListId == noteListId && n.UserId == userId)
                .ToListAsync();
        }

        public async Task<NoteModel> GetNoteAsync(int id, string userId)
        {
            return await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        }

        public async Task<NoteModel> CreateNoteAsync(CreateOrModifyNoteRequest noteRequest, string userId)
        {
            var note = new NoteModel
            {
                Name = noteRequest.Name,
                Description = noteRequest.Description,
                Content = noteRequest.Content,
                UserId = userId,
                NoteListId = noteRequest.NoteListId,
                Completed = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task<NoteModel> UpdateNoteAsync(int id, CreateOrModifyNoteRequest noteRequest, string userId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return null;

            note.Name = noteRequest.Name;
            note.Description = noteRequest.Description;
            note.Content = noteRequest.Content;
            note.Completed = false;
            note.UpdatedAt = DateTime.UtcNow;

            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task<bool> UpdateNoteCompletedAsync(int id, bool completed, string userId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return false;

            note.Completed = completed;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteNoteAsync(int id, string userId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return false;

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> NoteExistsAsync(int id)
        {
            return await _context.Notes.AnyAsync(e => e.Id == id);
        }

    }
}
