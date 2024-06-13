using NotesService.Models;
using NotesService.Models.RequestModels;

namespace OOP_PROJECT.Monolite.Service.IService
{
    public interface INoteService
    {
        Task<IEnumerable<NoteModel>> GetNotesByNoteListAsync(int noteListId, string userId);
        Task<NoteModel> GetNoteAsync(int id, string userId);
        Task<NoteModel> CreateNoteAsync(CreateOrModifyNoteRequest noteRequest, string userId);
        Task<NoteModel> UpdateNoteAsync(int id, CreateOrModifyNoteRequest noteRequest, string userId);
        Task<bool> UpdateNoteCompletedAsync(int id, bool completed, string userId);
        Task<bool> DeleteNoteAsync(int id, string userId);
        Task<bool> NoteExistsAsync(int id);
    }
}
