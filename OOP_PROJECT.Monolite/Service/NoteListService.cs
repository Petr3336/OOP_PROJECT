using Microsoft.EntityFrameworkCore;
using NotesService.Models;
using NotesService.Models.RequestModels;
using OOP_PROJECT.Monolite.Service.IService;

namespace OOP_PROJECT.Monolite.Service
{
    public class NoteListService : INoteListService
    {
        private readonly ApplicationDbContext _context;

        public NoteListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NoteListModel>> GetNoteListsAsync(string userId)
        {
            return await _context.NoteLists
                .Where(nl => nl.UserId == userId)
                .ToListAsync();
        }

        public async Task<NoteListModel> GetNoteListAsync(int id, string userId)
        {
            return await _context.NoteLists
                .Include(nl => nl.Notes)
                .FirstOrDefaultAsync(nl => nl.Id == id && nl.UserId == userId);
        }

        public async Task<NoteListModel> CreateNoteListAsync(CreateOrModifyNoteListRequest noteListRequest, string userId)
        {
            var noteList = new NoteListModel
            {
                Name = noteListRequest.Name,
                Description = noteListRequest.Description,
                Position = noteListRequest.Position,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.NoteLists.Add(noteList);
            await _context.SaveChangesAsync();

            return noteList;
        }

        public async Task<bool> UpdateNoteListAsync(int id, CreateOrModifyNoteListRequest noteListRequest, string userId)
        {
            var noteList = await _context.NoteLists
                .Include(nl => nl.Notes)
                .FirstOrDefaultAsync(nl => nl.Id == id && nl.UserId == userId);

            if (noteList == null) return false;

            noteList.Name = noteListRequest.Name;
            noteList.Description = noteListRequest.Description;
            noteList.Position = noteListRequest.Position;
            noteList.UpdatedAt = DateTime.UtcNow;

            _context.Entry(noteList).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteNoteListAsync(int id, string userId)
        {
            var noteList = await _context.NoteLists
                .FirstOrDefaultAsync(nl => nl.Id == id && nl.UserId == userId);

            if (noteList == null) return false;

            _context.NoteLists.Remove(noteList);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> NoteListExistsAsync(int id)
        {
            return await _context.NoteLists.AnyAsync(e => e.Id == id);
        }
    }
}
